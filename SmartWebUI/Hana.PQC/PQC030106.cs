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
    /// 클  래  스: PQC030106<br/>
    /// 클래스요약: 부적합현황<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2010-05-14<br/>
    /// 상세  설명: 기존 부적합현황 대신 신규로 화면 개발<br/>
    /// 변경  내용: <br/>
    /// 2010-08-27-임종우 : Spread에 작업자 정보 추가(최민호 요청)
    /// 2010-09-29-임종우 : 작업자별부적합 Chart 내림차순으로 표시 및 사용자 선택에 의한 X 축 제한 기능 추가 (김용복 요청)
    /// 2010-09-29-임종우 : QCN Step2 원인분석의 MAT 내용 추가 표시 (김용복 요청)
    /// 2011-01-18-김민우 : 공정표기 순서 변경 (김잔디 요청)
    /// 2011-02-24-임종우 : 원인분석 내용 전체 표시로 변경 (박기배 요청)
    /// 2011-02-25-임종우 : 장비명 추가 표시 (김용복 요청)
    /// 2011-02-25-임종우 : 공정명, 원인 대책 추가 표시 (김잔디 요청)
    /// 2011-04-06-김민우 : CABRLOTSTS에서 삭제된 이력은 안 보여줌
    /// 2011-07-14-임종우 : 원인, 대책에 따른 중복 현상 수정 -> 연결자를 통해 한 ROW에 표시 함 (박기배 요청)
    /// 2012-05-23-김민우 : HMKT1 선택 시 부적합 정보 안보이는 문제 수정
    /// 2012-05-23-김민우 : 부적합코드도 보이도록 (김우정 요청)
    /// 2015-03-16-오득연 : ChartFX -> MS Chart로 변경
    /// </summary>
    /// 

    public partial class PQC030106 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public PQC030106()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
            cdvFromTo.AutoBinding();
            //this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            //cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

            this.cdvCp.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvStep.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvFromOper.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvToOper.sFactory = GlobalVariable.gsAssyDefaultFactory;

            udcMSChart1.RPT_1_ChartInit();  //차트 초기화. 
            
            cboGraph.SelectedIndexChanged -= cboGraph_SelectedIndexChanged;
            cboGraph.SelectedIndex = 0;
            cboGraph.SelectedIndexChanged += cboGraph_SelectedIndexChanged;
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

            if (cdvFromOper.Text.Trim().Length == 0 || cdvToOper.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {   
            try
            {
                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    spdData.RPT_ColumnInit();

                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Sub. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Size", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Product", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("Status of nonconformance", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Nonconformance type", 1, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Issue Number", 1, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Issue time", 1, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Generation operation", 1, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Generation operation name", 1, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Defect Code", 1, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Defect name", 1, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Equipment name", 1, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Lot No.", 1, 21, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Lot Qty", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Defect quantity", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Number of samples", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_MerageHeaderColumnSpan(0, 13, 12);

                    spdData.RPT_AddBasicColumn("WF Yield", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("(%)", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);

                    spdData.RPT_AddBasicColumn("Now", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("STEP", 1, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);

                    spdData.RPT_AddBasicColumn("End time", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

                    spdData.RPT_AddBasicColumn("TAT", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 40);
                    spdData.RPT_AddBasicColumn("(days)", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 40);

                    spdData.RPT_AddBasicColumn("Issue Department", 0, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("publisher", 0, 30, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("worker", 0, 31, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

                    spdData.RPT_AddBasicColumn("Cause", 0, 32, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Man-content", 1, 32, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mat-contents", 1, 33, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mcn-content", 1, 34, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mtd-content", 1, 35, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Env-content", 1, 36, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 32, 5);

                    spdData.RPT_AddBasicColumn("Countermeasure", 0, 37, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Man-content", 1, 37, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mat-contents", 1, 38, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mcn-content", 1, 39, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mtd-content", 1, 40, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Env-content", 1, 41, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 37, 5);

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
                    spdData.RPT_MerageHeaderRowSpan(0, 27, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 29, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 30, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 31, 2);

                    // Group항목이 있을경우 반드시 선언해줄것.
                    spdData.RPT_ColumnConfigFromTable_New(btnSort);
                    //spdData.RPT_ColumnConfigFromTable(btnSort);
                }
                else
                {
                    spdData.RPT_ColumnInit();

                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);                
                    spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("Status of nonconformance", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Nonconformance type", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Issue Number", 1, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Issue time", 1, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Discovery operation", 1, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Discovery Operation Name", 1, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Generation operation", 1, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Generation operation name", 1, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Defect Code", 1, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Defect name", 1, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Equipment name", 1, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Lot No.", 1, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Lot Qty", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Defect quantity", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Number of samples", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_MerageHeaderColumnSpan(0, 10, 14);

                    spdData.RPT_AddBasicColumn("Defective rate", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("(ppm)", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    
                    spdData.RPT_AddBasicColumn("Now", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("STEP", 1, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);

                    spdData.RPT_AddBasicColumn("End time", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

                    spdData.RPT_AddBasicColumn("TAT", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 40);
                    spdData.RPT_AddBasicColumn("(days)", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 40);

                    spdData.RPT_AddBasicColumn("Issue Department", 0, 28, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("publisher", 0, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("worker", 0, 30, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

                    spdData.RPT_AddBasicColumn("Cause", 0, 31, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Man-content", 1, 31, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mat-contents", 1, 32, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mcn-content", 1, 33, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mtd-content", 1, 34, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Env-content", 1, 35, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 31, 5);

                    spdData.RPT_AddBasicColumn("Countermeasure", 0, 36, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Man-content", 1, 36, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mat-contents", 1, 37, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mcn-content", 1, 38, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Mtd-content", 1, 39, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Env-content", 1, 40, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 35, 5);

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
                    spdData.RPT_MerageHeaderRowSpan(0, 26, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 28, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 29, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 30, 2);

                    // Group항목이 있을경우 반드시 선언해줄것.
                    spdData.RPT_ColumnConfigFromTable_New(btnSort);
                    //spdData.RPT_ColumnConfigFromTable(btnSort);
                }
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
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS BUMPING_TYPE", "BUMPING_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PROCESS_FLOW", "PROCESS_FLOW", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS LAYER", "LAYER", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS PKG_TYPE", "PKG_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS RDL_PLATING", "RDL_PLATING", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS FINAL_BUMP", "FINAL_BUMP", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS SUB_MATERIAL", "SUB_MATERIAL", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "MAT.MAT_CMF_14", "MAT.MAT_CMF_14 AS WF_SIZE", "WF_SIZE", false); // \"SIZE\"
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "MAT.MAT_CMF_2", "MAT.MAT_CMF_2 AS THICKNESS", "THICKNESS", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "MAT.MAT_CMF_3", "MAT.MAT_CMF_3 AS FLAT_TYPE", "FLAT_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("WAFER ORIENTATION", "MAT.MAT_CMF_4", "MAT.MAT_CMF_4 AS WAFER_ORIENTATION", "WAFER_ORIENTATION", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT.MAT_ID", "MAT.MAT_ID AS PRODUCT", "PRODUCT", true);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS FAMILY", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10 AS PIN_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PACKAGE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS TYPE1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS TYPE2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS \"LD COUNT\"", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS DENSITY", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS GENERATION", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT.MAT_ID", "MAT.MAT_ID AS PRODUCT", true);
            }
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        #region Spread 생성을 위한 쿼리
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
            
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;
            
            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            
            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
                strSqlString.Append("     , ABN_TYPE " + "\n");
                strSqlString.Append("     , ABN_CODE " + "\n");
                strSqlString.Append("     , CREATE_TIME " + "\n");
                strSqlString.Append("     , OPER " + "\n");
                strSqlString.Append("     , OPER_DEC " + "\n");
                strSqlString.Append("     , FAIL_CODE " + "\n");
                strSqlString.Append("     , FAIL_DESC " + "\n");
                strSqlString.Append("     , RES_ID " + "\n");
                strSqlString.Append("     , LOT_ID " + "\n");
                strSqlString.Append("     , QTY_1 " + "\n");
                strSqlString.Append("     , FAIL_COUNT " + "\n");
                strSqlString.Append("     , SAMPLE_COUNT " + "\n");
                strSqlString.Append("     , WAFER_YIELD " + "\n");
                strSqlString.Append("     , ABN_STATUS " + "\n");
                strSqlString.Append("     , MACEPT_TIME " + "\n");
                strSqlString.Append("     , TRUNC(DECODE(MACEPT_TIME, '', SYSDATE, TO_DATE(MACEPT_TIME, 'YYYYMMDDHH24MISS')) - TO_DATE(CREATE_TIME, 'YYYYMMDDHH24MISS'), 2) AS TAT " + "\n");
                strSqlString.Append("     , DEP " + "\n");
                strSqlString.Append("     , QC_USER " + "\n");
                strSqlString.Append("     , OPER_USER " + "\n");
                strSqlString.Append("     , MAN1 " + "\n");
                strSqlString.Append("     , MAT1 " + "\n");
                strSqlString.Append("     , MCN1 " + "\n");
                strSqlString.Append("     , MTD1 " + "\n");
                strSqlString.Append("     , ENV1 " + "\n");
                strSqlString.Append("     , MAN2 " + "\n");
                strSqlString.Append("     , MAT2 " + "\n");
                strSqlString.Append("     , MCN2 " + "\n");
                strSqlString.Append("     , MTD2 " + "\n");
                strSqlString.Append("     , ENV2 " + "\n");
                strSqlString.Append("FROM " + "\n");
                strSqlString.Append("( " + "\n");
                strSqlString.AppendFormat("  SELECT {0} " + "\n", QueryCond2);
                strSqlString.Append("     , MNG.ABN_TYPE AS ABN_TYPE " + "\n");
                strSqlString.Append("     , MNG.ABN_CODE AS ABN_CODE " + "\n");
                strSqlString.Append("     , MNG.CREATE_TIME " + "\n");
                strSqlString.Append("     , MNG.OPER " + "\n");
                strSqlString.Append("     , (SELECT OPER_DESC FROM MWIPOPRDEF WHERE FACTORY = MNG.FACTORY AND OPER = MNG.OPER) AS OPER_DEC " + "\n");
                strSqlString.Append("     , FAI.FAIL_CODE " + "\n");
                strSqlString.Append("     , FAI.FAIL_DESC " + "\n");
                strSqlString.Append("     , MNG.RES_ID " + "\n");
                strSqlString.Append("     , MNG.LOT_ID " + "\n");
                strSqlString.Append("     , STS.QTY_1 " + "\n");    //LOT Qty 
                strSqlString.Append("     , MNG.FAIL_COUNT " + "\n");
                strSqlString.Append("     , MNG.SAMPLE_COUNT " + "\n"); //샘플수
                strSqlString.Append("     , NVL(ROUND((QTY_1 - FAIL_COUNT) / QTY_1 * 100, 2), 0) AS WAFER_YIELD " + "\n");
                //strSqlString.Append("     , DECODE(MNG.ABN_STATUS, 1, '부적합발행', 2, '발생원인도출', 3, '개선대책수립', 4, '기술승인', 5, '품질부서확인', 6, '자재처리') AS ABN_STATUS " + "\n"); // ABN_STATUS  하드코딩으로 해야함.
                strSqlString.Append("     , CASE WHEN MNG.ABN_STATUS = 1 THEN '부적합발행' " + "\n");
                strSqlString.Append("            WHEN MNG.ABN_STATUS = 2 THEN '발생원인도출' " + "\n");
                strSqlString.Append("            WHEN MNG.ABN_STATUS = 3 THEN '개선대책수립' " + "\n");
                strSqlString.Append("            WHEN MNG.ABN_STATUS = 4 THEN '기술승인' " + "\n");
                strSqlString.Append("            WHEN MNG.ABN_STATUS = 5 THEN '품질부서확인' " + "\n");
                strSqlString.Append("            WHEN MNG.ABN_STATUS = 6 " + "\n");
                strSqlString.Append("             AND MNG.USR_CMF_10 <> 'COMPLETE' THEN '자재처리' " + "\n");
                strSqlString.Append("            WHEN MNG.ABN_STATUS = 6 " + "\n");
                strSqlString.Append("             AND MNG.USR_CMF_10 = 'COMPLETE' THEN '승인' " + "\n");
                strSqlString.Append("       END ABN_STATUS " + "\n");
                //strSqlString.Append("     , MNG.MACEPT_TIME " + "\n"); //자재처리 완료시간
                strSqlString.Append("     , (SELECT ABN_HIS_TIME FROM CQCMABNHIS@RPTTOMES WHERE FACTORY = MNG.FACTORY AND ABN_CODE = MNG.ABN_CODE AND USR_CMF_10 = 'COMPLETE' AND ROWNUM = 1 ) AS MACEPT_TIME " + "\n");
                //strSqlString.Append("     , TRUNC(DECODE(MNG.MACEPT_TIME, ' ', SYSDATE, TO_DATE(MNG.MACEPT_TIME, 'YYYYMMDDHH24MISS')) - TO_DATE(MNG.CREATE_TIME, 'YYYYMMDDHH24MISS'), 2) AS TAT " + "\n");
                strSqlString.Append("     , MNG.DEPARTMENT AS DEP  " + "\n");
                strSqlString.Append("     , (SELECT USER_DESC || '(' || USER_ID || ')' FROM RWEBUSRDEF WHERE USER_ID = MNG.USER_ID) AS QC_USER " + "\n");
                strSqlString.Append("     , (SELECT USER_DESC || '(' || USER_ID || ')' FROM RWEBUSRDEF WHERE USER_ID = FAI.OPERATOR) AS OPER_USER " + "\n");
                strSqlString.Append("     , DAT.MAN AS MAN1 " + "\n");
                strSqlString.Append("     , DAT.MAT AS MAT1 " + "\n");
                strSqlString.Append("     , DAT.MCN AS MCN1 " + "\n");
                strSqlString.Append("     , DAT.MTD AS MTD1 " + "\n");
                strSqlString.Append("     , DAT.ENV AS ENV1 " + "\n");
                strSqlString.Append("     , DAT2.MAN AS MAN2 " + "\n");
                strSqlString.Append("     , DAT2.MAT AS MAT2 " + "\n");
                strSqlString.Append("     , DAT2.MCN AS MCN2 " + "\n");
                strSqlString.Append("     , DAT2.MTD AS MTD2 " + "\n");
                strSqlString.Append("     , DAT2.ENV AS ENV2" + "\n");

                strSqlString.Append("  FROM CQCMABNMNG@RPTTOMES MNG " + "\n");
                strSqlString.Append("     , CQCMABFAIL@RPTTOMES FAI " + "\n");
                strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                strSqlString.Append("     , MWIPLOTSTS STS   " + "\n");

                strSqlString.Append("     , (  " + "\n");
                strSqlString.Append("        SELECT ABN_SEQ " + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MAN, '|')), '|'), ' | ', '') AS MAN" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MAT, '|')), '|'), ' | ', '') AS MAT" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MCN, '|')), '|'), ' | ', '') AS MCN" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MTD, '|')), '|'), ' | ', '') AS MTD" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (ENV, '|')), '|'), ' | ', '') AS ENV" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT ABN_SEQ " + "\n");
                strSqlString.Append("                     , MAN AS MAN" + "\n");
                strSqlString.Append("                     , MAT AS MAT" + "\n");
                strSqlString.Append("                     , MACHINE AS MCN" + "\n");
                strSqlString.Append("                     , METHODE AS MTD" + "\n");
                strSqlString.Append("                     , ENVIRONMENT AS ENV" + "\n");
                strSqlString.Append("                     , ROW_NUMBER () OVER (PARTITION BY ABN_SEQ ORDER BY UPDATE_TIME) AS RNUM " + "\n");
                strSqlString.Append("                  FROM CQCMABANAL@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND ABN_SEQ IN (SELECT MNG.ABN_SEQ " + "\n");
                strSqlString.Append("                                    FROM CQCMABNMNG@RPTTOMES MNG  " + "\n");
                strSqlString.Append("                                       , CQCMABFAIL@RPTTOMES FAI  " + "\n");
                strSqlString.Append("                                   WHERE 1=1 " + "\n");
                strSqlString.Append("                                     AND MNG.ABN_SEQ = FAI.ABN_SEQ " + "\n");
                strSqlString.Append("                                     AND MNG.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND MNG.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                strSqlString.Append("                                     AND MNG.ABN_TYPE " + cdvCp.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND FAI.FAIL_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND MNG.ABN_STATUS " + cdvStep.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND MNG.OPER BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                strSqlString.Append("                                     AND MNG.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                                  )" + "\n");
                strSqlString.Append("                   AND DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("               )         " + "\n");
                // 하기 조건문 변경 및 추가
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("         AND ABN_SEQ = ABN_SEQ " + "\n");
                strSqlString.Append("         START WITH RNUM = 1" + "\n");
                strSqlString.Append("       CONNECT BY PRIOR ABN_SEQ = ABN_SEQ " + "\n");
                strSqlString.Append("           AND RNUM = (RNUM - 1) " + "\n");
                strSqlString.Append("         GROUP BY ABN_SEQ" + "\n");
                strSqlString.Append("       ) DAT " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT ABN_SEQ" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MAN, '|')), '|'), ' | ', '') AS MAN" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MAT, '|')), '|'), ' | ', '') AS MAT" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MCN, '|')), '|'), ' | ', '') AS MCN" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MTD, '|')), '|'), ' | ', '') AS MTD" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (ENV, '|')), '|'), ' | ', '') AS ENV             " + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT ABN_SEQ" + "\n");
                strSqlString.Append("                     , MAN AS MAN" + "\n");
                strSqlString.Append("                     , MAT AS MAT" + "\n");
                strSqlString.Append("                     , MACHINE AS MCN" + "\n");
                strSqlString.Append("                     , METHODE AS MTD" + "\n");
                strSqlString.Append("                     , ENVIRONMENT AS ENV" + "\n");
                strSqlString.Append("                     , ROW_NUMBER () OVER (PARTITION BY ABN_SEQ ORDER BY UPDATE_TIME) AS RNUM                   " + "\n");
                strSqlString.Append("                  FROM CQCMABACTN@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND ABN_SEQ IN (SELECT MNG.ABN_SEQ " + "\n");
                strSqlString.Append("                                    FROM CQCMABNMNG@RPTTOMES MNG  " + "\n");
                strSqlString.Append("                                       , CQCMABFAIL@RPTTOMES FAI  " + "\n");
                strSqlString.Append("                                   WHERE 1=1 " + "\n");
                strSqlString.Append("                                     AND MNG.ABN_SEQ = FAI.ABN_SEQ " + "\n");
                strSqlString.Append("                                     AND MNG.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND MNG.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                strSqlString.Append("                                     AND MNG.ABN_TYPE " + cdvCp.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND FAI.FAIL_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND MNG.ABN_STATUS " + cdvStep.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND MNG.OPER BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                strSqlString.Append("                                     AND MNG.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                                  )" + "\n");
                strSqlString.Append("                   AND DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("               )        " + "\n");
                // 하기 조건문 변경 및 추가
                strSqlString.Append("         WHERE ABN_SEQ = ABN_SEQ" + "\n");
                strSqlString.Append("         START WITH RNUM = 1" + "\n");
                strSqlString.Append("       CONNECT BY PRIOR ABN_SEQ = ABN_SEQ " + "\n");
                strSqlString.Append("           AND RNUM = (RNUM - 1) " + "\n");
                strSqlString.Append("         GROUP BY ABN_SEQ" + "\n");
                strSqlString.Append("       ) DAT2" + "\n");

                strSqlString.Append("  WHERE 1=1 " + "\n");
                strSqlString.Append("    AND MNG.ABN_SEQ = FAI.ABN_SEQ " + "\n");
                strSqlString.Append("    AND MNG.ABN_CODE = FAI.ABN_CODE " + "\n");
                strSqlString.Append("    AND FAI.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("    AND MNG.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.Append("    AND MNG.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("    AND MNG.LOT_ID = STS.LOT_ID " + "\n");
                strSqlString.Append("    AND MNG.ABN_SEQ = DAT.ABN_SEQ(+) " + "\n");
                strSqlString.Append("    AND MNG.ABN_SEQ = DAT2.ABN_SEQ(+) " + "\n");
                strSqlString.Append("    AND MNG.FACTORY = '" + cdvFactory.Text + "'" + "\n");

                strSqlString.Append("   AND MNG.CREATE_TIME BETWEEN '" + cdvFromTo.ExactFromDate + "' AND '" + cdvFromTo.ExactToDate + "'" + "\n");
                strSqlString.Append("   AND MNG.ABN_TYPE " + cdvCp.SelectedValueToQueryString + "\n");
                strSqlString.Append("   AND MNG.ABN_STATUS " + cdvStep.SelectedValueToQueryString + "\n");

                strSqlString.Append("   AND FAI.FAIL_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                
                strSqlString.Append("   AND MNG.OPER BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                strSqlString.Append("   AND MNG.DELETE_FLAG <> 'Y' " + "\n");

                if (!ckbEndData.Checked)
                {
                    strSqlString.Append("   AND MNG.USR_CMF_10 <> 'COMPLETE' " + "\n");
                    //strSqlString.Append("   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
                }

                #region 상세 조회에 따른 SQL문 생성
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                #endregion

                strSqlString.Append(") A " + "\n");
                strSqlString.Append("WHERE 1=1 " + "\n");
                strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond3);
            }
            else
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
                strSqlString.Append("     , SUBSTR(STS.ABR_NO, 1, 3) AS ABR_TYPE " + "\n");
                strSqlString.Append("     , STS.ABR_NO " + "\n");
                strSqlString.Append("     , STEP10_TIME " + "\n");
                strSqlString.Append("     , OPER_1 " + "\n");
                //strSqlString.Append("     , (SELECT OPER_DESC FROM MWIPOPRDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OPER = OPER_1) AS OPER_1_DEC " + "\n");
                strSqlString.Append("     , (SELECT OPER_DESC FROM MWIPOPRDEF WHERE FACTORY = STS.FACTORY AND OPER = OPER_1) AS OPER_1_DEC " + "\n");
                strSqlString.Append("     , OPER_2 " + "\n");
                //strSqlString.Append("     , (SELECT OPER_DESC FROM MWIPOPRDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OPER = OPER_2) AS OPER_2_DEC " + "\n");
                strSqlString.Append("     , (SELECT OPER_DESC FROM MWIPOPRDEF WHERE FACTORY = STS.FACTORY AND OPER = OPER_2) AS OPER_2_DEC " + "\n");
                //strSqlString.Append("     , (SELECT DESC_1 FROM CABRDFTDEF@RPTTOMES WHERE DEL_FLAG=' ' AND FACTORY = STS.FACTORY AND DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT_CODE " + "\n");
                strSqlString.Append("     , (SELECT DEFECT_CODE FROM CABRDFTDEF@RPTTOMES WHERE DEL_FLAG=' ' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT_CODE " + "\n");
                strSqlString.Append("     , (SELECT DESC_1 FROM CABRDFTDEF@RPTTOMES WHERE DEL_FLAG=' ' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT_CODE " + "\n");
                strSqlString.Append("     , RES_ID " + "\n");
                strSqlString.Append("     , LOT_ID " + "\n");
                strSqlString.Append("     , QTY_1 " + "\n");
                strSqlString.Append("     , FAIL_QTY " + "\n");
                strSqlString.Append("     , INSP_QTY " + "\n");
                strSqlString.Append("     , DECODE(INSP_QTY, 0, 0, ROUND((FAIL_QTY / INSP_QTY) * 1000000, 2)) AS LOSS_PER " + "\n");
                strSqlString.Append("     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_ABR_STEP' AND FACTORY = STS.FACTORY AND KEY_1 = STS.CUR_STEP) AS CUR_STEP " + "\n");
                strSqlString.Append("     , CLOSE_TIME " + "\n");
                strSqlString.Append("     , TRUNC(DECODE(CLOSE_TIME, ' ', SYSDATE, TO_DATE(CLOSE_TIME, 'YYYYMMDDHH24MISS')) - TO_DATE(STEP10_TIME, 'YYYYMMDDHH24MISS'), 2) AS TAT " + "\n");
                strSqlString.Append("     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_DEPARTMENT' AND FACTORY = STS.FACTORY AND KEY_1 = STS.STEP10_DEPT) AS DEP " + "\n");
                strSqlString.Append("     , (SELECT USER_DESC || '(' || USER_ID || ')' FROM RWEBUSRDEF WHERE USER_ID = STS.STEP10_USER) AS QC_USER " + "\n");
                strSqlString.Append("     , (SELECT USER_DESC || '(' || USER_ID || ')' FROM RWEBUSRDEF WHERE USER_ID = STS.QA_OPER) AS OPER_USER " + "\n");
                strSqlString.Append("     , DAT.MAN " + "\n");
                strSqlString.Append("     , DAT.MAT " + "\n");
                strSqlString.Append("     , DAT.MCN " + "\n");
                strSqlString.Append("     , DAT.MTD " + "\n");
                strSqlString.Append("     , DAT.ENV " + "\n");
                strSqlString.Append("     , DAT2.MAN " + "\n");
                strSqlString.Append("     , DAT2.MAT " + "\n");
                strSqlString.Append("     , DAT2.MCN " + "\n");
                strSqlString.Append("     , DAT2.MTD " + "\n");
                strSqlString.Append("     , DAT2.ENV " + "\n");
                strSqlString.Append("  FROM CABRLOTSTS@RPTTOMES STS " + "\n");
                strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                strSqlString.Append("     , (  " + "\n");
                strSqlString.Append("        SELECT ABR_NO" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MAN, '|')), '|'), ' | ', '') AS MAN" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MAT, '|')), '|'), ' | ', '') AS MAT" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MCN, '|')), '|'), ' | ', '') AS MCN" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MTD, '|')), '|'), ' | ', '') AS MTD" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (ENV, '|')), '|'), ' | ', '') AS ENV" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT FACTORY" + "\n");
                strSqlString.Append("                     , ABR_NO" + "\n");
                strSqlString.Append("                     , MAN_CONTENTS AS MAN" + "\n");
                strSqlString.Append("                     , MAT_CONTENTS AS MAT" + "\n");
                strSqlString.Append("                     , MCN_CONTENTS AS MCN" + "\n");
                strSqlString.Append("                     , MTD_CONTENTS AS MTD" + "\n");
                strSqlString.Append("                     , ENV_CONTENTS AS ENV" + "\n");
                strSqlString.Append("                     , ROW_NUMBER () OVER (PARTITION BY ABR_NO ORDER BY UPDATE_TIME) AS RNUM " + "\n");
                strSqlString.Append("                  FROM CABR4MPDAT@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND SUB_TYPE = 'TYPE_AN'" + "\n");
                strSqlString.Append("                   AND ABR_NO IN (SELECT ABR_NO " + "\n");
                strSqlString.Append("                                    FROM CABRLOTSTS@RPTTOMES " + "\n");
                strSqlString.Append("                                   WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                                     AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                strSqlString.Append("                                     AND SUBSTR(ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND OPER_1 BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                strSqlString.Append("                                     AND DEL_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                                  )" + "\n");
                strSqlString.Append("               )         " + "\n");
                // 하기 조건문 변경 및 추가
                strSqlString.Append("         WHERE ABR_NO = ABR_NO" + "\n");
                strSqlString.Append("         START WITH RNUM = 1" + "\n");
                strSqlString.Append("       CONNECT BY PRIOR ABR_NO = ABR_NO " + "\n");
                strSqlString.Append("           AND RNUM = (RNUM - 1) " + "\n");
                strSqlString.Append("         GROUP BY ABR_NO" + "\n");
                //strSqlString.Append("         START WITH RNUM = 1" + "\n");
                //strSqlString.Append("       CONNECT BY PRIOR RNUM = (RNUM - 1)" + "\n");
                //strSqlString.Append("           AND ABR_NO = ABR_NO" + "\n");
                //strSqlString.Append("         GROUP BY ABR_NO" + "\n");
                strSqlString.Append("       ) DAT " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT ABR_NO" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MAN, '|')), '|'), ' | ', '') AS MAN" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MAT, '|')), '|'), ' | ', '') AS MAT" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MCN, '|')), '|'), ' | ', '') AS MCN" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (MTD, '|')), '|'), ' | ', '') AS MTD" + "\n");
                strSqlString.Append("             , REPLACE(LTRIM(MAX(SYS_CONNECT_BY_PATH (ENV, '|')), '|'), ' | ', '') AS ENV             " + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT FACTORY" + "\n");
                strSqlString.Append("                     , ABR_NO" + "\n");
                strSqlString.Append("                     , MAN_CONTENTS AS MAN" + "\n");
                strSqlString.Append("                     , MAT_CONTENTS AS MAT" + "\n");
                strSqlString.Append("                     , MCN_CONTENTS AS MCN" + "\n");
                strSqlString.Append("                     , MTD_CONTENTS AS MTD" + "\n");
                strSqlString.Append("                     , ENV_CONTENTS AS ENV" + "\n");
                strSqlString.Append("                     , ROW_NUMBER () OVER (PARTITION BY ABR_NO ORDER BY UPDATE_TIME) AS RNUM                   " + "\n");
                strSqlString.Append("                  FROM CABR4MPDAT@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND SUB_TYPE = 'TYPE_MS'" + "\n");
                strSqlString.Append("                   AND ABR_NO IN (SELECT ABR_NO " + "\n");
                strSqlString.Append("                                    FROM CABRLOTSTS@RPTTOMES " + "\n");
                strSqlString.Append("                                   WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                                     AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                strSqlString.Append("                                     AND SUBSTR(ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                     AND OPER_1 BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                strSqlString.Append("                                     AND DEL_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                                  )" + "\n");
                strSqlString.Append("               )        " + "\n");
                // 하기 조건문 변경 및 추가
                strSqlString.Append("         WHERE ABR_NO = ABR_NO" + "\n");
                strSqlString.Append("         START WITH RNUM = 1" + "\n");
                strSqlString.Append("       CONNECT BY PRIOR ABR_NO = ABR_NO " + "\n");
                strSqlString.Append("           AND RNUM = (RNUM - 1) " + "\n");
                strSqlString.Append("         GROUP BY ABR_NO" + "\n");
                //strSqlString.Append("         START WITH RNUM = 1" + "\n");
                //strSqlString.Append("       CONNECT BY PRIOR RNUM = (RNUM - 1)" + "\n");
                //strSqlString.Append("           AND ABR_NO = ABR_NO" + "\n");
                //strSqlString.Append("         GROUP BY ABR_NO" + "\n");
                strSqlString.Append("       ) DAT2" + "\n");
                //strSqlString.Append("     , (SELECT * FROM CABR4MPDAT@RPTTOMES WHERE FACTORY = '" + cdvFactory.Text + "' AND SUB_TYPE = 'TYPE_AN') DAT " + "\n");
                //strSqlString.Append("     , (SELECT * FROM CABR4MPDAT@RPTTOMES WHERE FACTORY = '" + cdvFactory.Text + "' AND SUB_TYPE = 'TYPE_MS') DAT2 " + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND STS.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.Append("   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("   AND STS.ABR_NO = DAT.ABR_NO(+) " + "\n");
                strSqlString.Append("   AND STS.ABR_NO = DAT2.ABR_NO(+) " + "\n");
                strSqlString.Append("   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");

                strSqlString.Append("   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                strSqlString.Append("   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                strSqlString.Append("   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

                // 2011-01-18-김민우 : 공정표기 순서 변경
                //strSqlString.Append("   AND STS.OPER_1 IN (" + cdvFromOper.getInQuery() + ")" + "\n");
                strSqlString.Append("   AND STS.OPER_1 BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                //2011-04-06-김민우 : 삭제된 이력은 안 보여줌
                strSqlString.Append("   AND STS.DEL_FLAG <> 'Y' " + "\n");

                if (!ckbEndData.Checked)
                {
                    strSqlString.Append("   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
                }

                #region 상세 조회에 따른 SQL문 생성
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

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond1);
            }
                        

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region Chart 생성을 위한 쿼리
        private string MakeChartSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
            
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;

            switch (cboGraph.SelectedIndex)
            {
                case 0: // 1. 부적합 종류별  발생 건 & TAT
                    {
                        #region Chart No.1
                        if (cdvFactory.Text.Trim() == "HMKB1")
                        {
                            strSqlString.Append("SELECT ABN_TYPE, COUNT(ABN_TYPE) AS CNT, ROUND(AVG(TAT), 2) AS TAT " + "\n");
                            strSqlString.Append("  FROM ( " + "\n");
                            strSqlString.Append("        SELECT MNG.ABN_TYPE " + "\n");
                            strSqlString.Append("             , TRUNC(DECODE(MNG.MACEPT_TIME, ' ', SYSDATE, TO_DATE(MNG.MACEPT_TIME, 'YYYYMMDDHH24MISS')) - TO_DATE(MNG.CREATE_TIME, 'YYYYMMDDHH24MISS'), 2) AS TAT " + "\n");
                            strSqlString.Append("        FROM CQCMABNMNG@RPTTOMES MNG " + "\n");
                            strSqlString.Append("           , CQCMABFAIL@RPTTOMES FAI " + "\n");
                            strSqlString.Append("           , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("        WHERE 1=1 " + "\n");
                            strSqlString.Append("          AND MNG.ABN_SEQ = FAI.ABN_SEQ " + "\n");
                            strSqlString.Append("          AND MNG.ABN_CODE = FAI.ABN_CODE " + "\n");
                            strSqlString.Append("          AND MNG.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("          AND MNG.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("          AND MNG.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("          AND MNG.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("          AND MNG.ABN_TYPE " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("          AND MNG.ABN_STATUS " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("          AND MNG.OPER BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                            strSqlString.Append("          AND FAI.FAIL_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            //strSqlString.Append("          AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("   AND MNG.USR_CMF_10 <> 'COMPLETE' " + "\n");                                
                            }

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("       )" + "\n");
                            strSqlString.Append(" GROUP BY ABN_TYPE " + "\n");
                        }
                        else
                        {
                            strSqlString.Append("SELECT ABR_TYPE, COUNT(ABR_TYPE) AS CNT, ROUND(AVG(TAT), 2) AS TAT " + "\n");
                            strSqlString.Append("  FROM ( " + "\n");
                            strSqlString.Append("        SELECT SUBSTR(ABR_NO,1,3) AS ABR_TYPE " + "\n");
                            strSqlString.Append("             , TRUNC(DECODE(CLOSE_TIME, ' ', SYSDATE, TO_DATE(CLOSE_TIME, 'YYYYMMDDHH24MISS')) - TO_DATE(STEP10_TIME, 'YYYYMMDDHH24MISS'), 2) AS TAT" + "\n");
                            strSqlString.Append("          FROM CABRLOTSTS@RPTTOMES STS " + "\n");
                            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append(" WHERE 1=1 " + "\n");
                            strSqlString.Append("   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("   AND STS.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            strSqlString.Append("   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("   AND STS.OPER_1 BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
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

                            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("       )" + "\n");
                            strSqlString.Append(" GROUP BY ABR_TYPE " + "\n");
                        }
                        #endregion
                    }
                    break;
                case 1: // 2. 기간 별 부적합 발생 건
                    {
                        #region Chart No.2
                        if (cdvFactory.Text.Trim() == "HMKB1")
                        {
                            strSqlString.Append("SELECT TRAN_TIME, COUNT(TRAN_TIME) AS CNT " + "\n");
                            strSqlString.Append("  FROM ( " + "\n");

                            if (cdvFromTo.DaySelector.SelectedValue.Equals("DAY"))
                            {
                                strSqlString.Append("        SELECT  GET_WORK_DATE(CREATE_TIME,'D') AS TRAN_TIME " + "\n");
                            }
                            else if (cdvFromTo.DaySelector.SelectedValue.Equals("WEEK"))
                            {
                                strSqlString.Append("        SELECT  GET_WORK_DATE(CREATE_TIME,'W') AS TRAN_TIME " + "\n");
                            }
                            else
                            {
                                strSqlString.Append("        SELECT  GET_WORK_DATE(CREATE_TIME,'M') AS TRAN_TIME " + "\n");
                            }

                            strSqlString.Append("        FROM CQCMABNMNG@RPTTOMES MNG " + "\n");
                            strSqlString.Append("           , CQCMABFAIL@RPTTOMES FAI " + "\n");
                            strSqlString.Append("           , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("        WHERE 1=1 " + "\n");
                            strSqlString.Append("          AND MNG.ABN_SEQ = FAI.ABN_SEQ " + "\n");
                            strSqlString.Append("          AND MNG.ABN_CODE = FAI.ABN_CODE " + "\n");
                            strSqlString.Append("          AND MNG.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("          AND MNG.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("          AND MNG.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("          AND MNG.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("          AND MNG.ABN_TYPE " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("          AND MNG.ABN_STATUS " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("          AND MNG.OPER BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                            strSqlString.Append("          AND FAI.FAIL_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            //strSqlString.Append("          AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("   AND MNG.USR_CMF_10 <> 'COMPLETE' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("       )" + "\n");
                            strSqlString.Append(" GROUP BY TRAN_TIME " + "\n");
                        }
                        else
                        {
                            strSqlString.Append("SELECT TRAN_TIME, COUNT(TRAN_TIME) AS CNT " + "\n");
                            strSqlString.Append("  FROM ( " + "\n");

                            if (cdvFromTo.DaySelector.SelectedValue.Equals("DAY"))
                            {
                                strSqlString.Append("        SELECT  GET_WORK_DATE(STEP10_TIME,'D') AS TRAN_TIME " + "\n");
                            }
                            else if (cdvFromTo.DaySelector.SelectedValue.Equals("WEEK"))
                            {
                                strSqlString.Append("        SELECT  GET_WORK_DATE(STEP10_TIME,'W') AS TRAN_TIME " + "\n");
                            }
                            else
                            {
                                strSqlString.Append("        SELECT  GET_WORK_DATE(STEP10_TIME,'M') AS TRAN_TIME " + "\n");
                            }

                            strSqlString.Append("          FROM CABRLOTSTS@RPTTOMES STS " + "\n");
                            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append(" WHERE 1=1 " + "\n");
                            strSqlString.Append("   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("   AND STS.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            strSqlString.Append("   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("   AND STS.OPER_1 BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
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

                            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("       )" + "\n");
                            strSqlString.Append(" GROUP BY TRAN_TIME " + "\n");
                        }
                        #endregion
                    }
                    break;
                case 2: // 3. 불량별 파레토
                    {
                        #region Chart No.3
                        if (cdvFactory.Text.Trim() == "HMKB1")
                        {
                            strSqlString.Append("SELECT DEFECT_CODE, QTY " + "\n");
                            strSqlString.Append("     , ROUND(QTY_S/QTY_A,4)*100 AS ADDED_RATIO  " + "\n");
                            strSqlString.Append("  FROM ( " + "\n");
                            strSqlString.Append("        SELECT DEFECT_CODE, QTY, SUM(QTY) OVER(ORDER BY QTY DESC, DEFECT_CODE) AS QTY_S, SUM(QTY) OVER() AS QTY_A " + "\n");
                            strSqlString.Append("          FROM ( " + "\n");
                            strSqlString.Append("                SELECT DISTINCT FAI.FAIL_DESC AS DEFECT_CODE " + "\n");
                            strSqlString.Append("                     , COUNT(FAI.FAIL_CODE) AS QTY " + "\n");
                            strSqlString.Append("                FROM CQCMABNMNG@RPTTOMES MNG " + "\n");
                            strSqlString.Append("                   , CQCMABFAIL@RPTTOMES FAI " + "\n");
                            strSqlString.Append("                   , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("                WHERE 1=1 " + "\n");
                            strSqlString.Append("                  AND MNG.ABN_SEQ = FAI.ABN_SEQ " + "\n");
                            strSqlString.Append("                  AND MNG.ABN_CODE = FAI.ABN_CODE " + "\n");
                            strSqlString.Append("                  AND FAI.DELETE_FLAG <> 'Y' " + "\n");
                            strSqlString.Append("                  AND MNG.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("                  AND MNG.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("                  AND MNG.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("                  AND MNG.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("                  AND MNG.ABN_TYPE " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("                  AND MNG.ABN_STATUS " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("                  AND MNG.OPER BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                            strSqlString.Append("                  AND FAI.FAIL_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            //strSqlString.Append("                AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("              AND MNG.USR_CMF_10 <> 'COMPLETE' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("                 GROUP BY MNG.FACTORY, FAI.FAIL_CODE, FAI.FAIL_DESC " + "\n");
                            strSqlString.Append("               )" + "\n");
                            strSqlString.Append("       )" + "\n");
                            strSqlString.Append(" ORDER BY QTY_S " + "\n");
                        }
                        else
                        {
                            strSqlString.Append("SELECT DEFECT_CODE, QTY " + "\n");
                            strSqlString.Append("     , ROUND(QTY_S/QTY_A,4)*100 AS ADDED_RATIO  " + "\n");
                            strSqlString.Append("  FROM ( " + "\n");
                            strSqlString.Append("        SELECT DEFECT_CODE, QTY, SUM(QTY) OVER(ORDER BY QTY DESC, DEFECT_CODE) AS QTY_S, SUM(QTY) OVER() AS QTY_A " + "\n");
                            strSqlString.Append("          FROM ( " + "\n");
                            strSqlString.Append("                SELECT (SELECT DESC_1 FROM CABRDFTDEF@RPTTOMES WHERE DEL_FLAG=' ' AND FACTORY = STS.FACTORY AND DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT_CODE " + "\n");
                            strSqlString.Append("                     , COUNT(STS.DEFECT_CODE) AS QTY " + "\n");
                            strSqlString.Append("                  FROM CABRLOTSTS@RPTTOMES STS " + "\n");
                            strSqlString.Append("                     , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("                 WHERE 1=1 " + "\n");
                            strSqlString.Append("                   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("                   AND STS.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("                   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("                   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("                   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("                   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            strSqlString.Append("                   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("                   AND STS.OPER_1 BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("                   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("                 GROUP BY STS.FACTORY, STS.DEFECT_CODE " + "\n");
                            strSqlString.Append("               )" + "\n");
                            strSqlString.Append("       )" + "\n");
                            strSqlString.Append(" ORDER BY QTY_S " + "\n");
                        }
                        #endregion
                    }
                    break;
                case 3: // 4. 불량별 부적합 발생 건 & 생산량
                    {                        
                        #region Chart No.4
                        String stColumnName = String.Empty;
                        DataTable loss_code = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeLossCodeSqlString());
                        String strDecodeQuery = CmnFunction.GetDecodeQueryStringFromDataTable("     , SUM(DECODE(DEFECT_CODE, ", loss_code, ", 1, 0)) AS ", true);

                        for (int i = 0; i < loss_code.Rows.Count; i++)
                        {
                            stColumnName += "     , \"" + loss_code.Rows[i][0] + "\" " + "\n";
                        }

                        if (cdvFactory.Text.Trim() == "HMKB1")
                        {
                            strSqlString.Append("SELECT TRAN_TIME, SHP_QTY  " + "\n");
                            strSqlString.Append(stColumnName);
                            strSqlString.Append("  FROM ( " + "\n");
                            strSqlString.Append("        SELECT TRAN_TIME " + "\n");
                            strSqlString.Append(strDecodeQuery);
                            strSqlString.Append("          FROM ( " + "\n");

                            if (cdvFromTo.DaySelector.SelectedValue.Equals("DAY"))
                            {
                                strSqlString.Append("                SELECT GET_WORK_DATE(MNG.CREATE_TIME,'D') AS TRAN_TIME " + "\n");
                            }
                            else if (cdvFromTo.DaySelector.SelectedValue.Equals("WEEK"))
                            {
                                strSqlString.Append("                SELECT GET_WORK_DATE(MNG.CREATE_TIME,'W') AS TRAN_TIME " + "\n");
                            }
                            else
                            {
                                strSqlString.Append("                SELECT GET_WORK_DATE(MNG.CREATE_TIME,'M') AS TRAN_TIME " + "\n");
                            }

                            strSqlString.Append("                          , FAI.FAIL_DESC AS DEFECT_CODE " + "\n");
                            strSqlString.Append("                    FROM CQCMABNMNG@RPTTOMES MNG " + "\n");
                            strSqlString.Append("                       , CQCMABFAIL@RPTTOMES FAI " + "\n");
                            strSqlString.Append("                       , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("                    WHERE 1=1 " + "\n");
                            strSqlString.Append("                      AND MNG.ABN_SEQ = FAI.ABN_SEQ " + "\n");
                            strSqlString.Append("                      AND MNG.ABN_CODE = FAI.ABN_CODE " + "\n");
                            strSqlString.Append("                      AND FAI.DELETE_FLAG <> 'Y' " + "\n");
                            strSqlString.Append("                      AND MNG.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("                      AND MNG.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("                      AND MNG.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("                      AND MNG.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("                      AND MNG.ABN_TYPE " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("                      AND MNG.ABN_STATUS " + cdvStep.SelectedValueToQueryString + "\n");                            
                            strSqlString.Append("                      AND FAI.FAIL_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            
                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("                 AND MNG.USR_CMF_10 <> 'COMPLETE' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("               ) " + "\n");
                            strSqlString.Append("         GROUP BY TRAN_TIME " + "\n");
                            strSqlString.Append("       ) A" + "\n");
                            strSqlString.Append("     , (" + "\n");
                            strSqlString.Append("        SELECT WORK_DATE, SUM(SHP_QTY_1) AS SHP_QTY " + "\n");
                            strSqlString.Append("          FROM VSUMWIPSHP LOT " + "\n");
                            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("         WHERE LOT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("           AND LOT.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("           AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("           AND WORK_DATE BETWEEN '" + cdvFromTo.HmFromDay + "' AND '" + cdvFromTo.HmToDay + "'" + "\n");
                            strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
                            strSqlString.Append("         GROUP BY WORK_DATE " + "\n");
                            strSqlString.Append("       ) B" + "\n");
                            strSqlString.Append(" WHERE A.TRAN_TIME = B.WORK_DATE " + "\n");
                        }
                        else
                        {
                            strSqlString.Append("SELECT TRAN_TIME, SHP_QTY  " + "\n");
                            strSqlString.Append(stColumnName);
                            strSqlString.Append("  FROM ( " + "\n");
                            strSqlString.Append("        SELECT TRAN_TIME " + "\n");
                            strSqlString.Append(strDecodeQuery);
                            strSqlString.Append("          FROM ( " + "\n");

                            if (cdvFromTo.DaySelector.SelectedValue.Equals("DAY"))
                            {
                                strSqlString.Append("                SELECT GET_WORK_DATE(STEP10_TIME,'D') AS TRAN_TIME " + "\n");
                            }
                            else if (cdvFromTo.DaySelector.SelectedValue.Equals("WEEK"))
                            {
                                strSqlString.Append("                SELECT GET_WORK_DATE(STEP10_TIME,'W') AS TRAN_TIME " + "\n");
                            }
                            else
                            {
                                strSqlString.Append("                SELECT GET_WORK_DATE(STEP10_TIME,'M') AS TRAN_TIME " + "\n");
                            }

                            strSqlString.Append("                     , (SELECT DESC_1 FROM CABRDFTDEF@RPTTOMES WHERE DEL_FLAG=' ' AND FACTORY = STS.FACTORY AND DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT_CODE " + "\n");
                            strSqlString.Append("                  FROM CABRLOTSTS@RPTTOMES STS " + "\n");
                            strSqlString.Append("                     , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("                 WHERE 1=1 " + "\n");
                            strSqlString.Append("                   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("                   AND STS.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("                   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("                   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("                   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("                   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            strSqlString.Append("                   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                            //  strSqlString.Append("                   AND STS.OPER_1 IN (" + cdvFromOper.getInQuery() + ")" + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("                   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("               ) " + "\n");
                            strSqlString.Append("         GROUP BY TRAN_TIME " + "\n");
                            strSqlString.Append("       ) A" + "\n");
                            strSqlString.Append("     , (" + "\n");
                            strSqlString.Append("        SELECT WORK_DATE, SUM(SHP_QTY_1) AS SHP_QTY " + "\n");
                            strSqlString.Append("          FROM VSUMWIPSHP LOT " + "\n");
                            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("         WHERE LOT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("           AND LOT.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("           AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("           AND WORK_DATE BETWEEN '" + cdvFromTo.HmFromDay + "' AND '" + cdvFromTo.HmToDay + "'" + "\n");
                            strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
                            strSqlString.Append("         GROUP BY WORK_DATE " + "\n");
                            strSqlString.Append("       ) B" + "\n");
                            strSqlString.Append(" WHERE A.TRAN_TIME = B.WORK_DATE " + "\n");
                        }
                        #endregion
                    }
                    break;
                case 4: // 5. 불량별 부적합 제품 발생 건
                    {
                        #region Chart No.5
                        DataTable loss_code = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeLossCodeSqlString());
                        String strDecodeQuery = CmnFunction.GetDecodeQueryStringFromDataTable("     , SUM(DECODE(DEFECT_CODE, ", loss_code, ", 1, 0)) AS ", true);

                        if (cdvFactory.Text.Trim() == "HMKB1")
                        {
                            strSqlString.Append("SELECT MAT_ID  " + "\n");
                            strSqlString.Append(strDecodeQuery);
                            strSqlString.Append("  FROM ( " + "\n");
                            strSqlString.Append("        SELECT MNG.MAT_ID " + "\n");
                            strSqlString.Append("             , FAI.FAIL_DESC AS DEFECT_CODE " + "\n");
                            strSqlString.Append("          FROM CQCMABNMNG@RPTTOMES MNG " + "\n");
                            strSqlString.Append("             , CQCMABFAIL@RPTTOMES FAI " + "\n");
                            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("          WHERE 1=1 " + "\n");
                            strSqlString.Append("            AND MNG.ABN_SEQ = FAI.ABN_SEQ " + "\n");
                            strSqlString.Append("            AND MNG.ABN_CODE = FAI.ABN_CODE " + "\n");
                            strSqlString.Append("            AND FAI.DELETE_FLAG <> 'Y' " + "\n");
                            strSqlString.Append("            AND MNG.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("            AND MNG.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("            AND MNG.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("            AND MNG.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("            AND MNG.ABN_TYPE " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("            AND MNG.ABN_STATUS " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("            AND MNG.OPER BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                            strSqlString.Append("            AND FAI.FAIL_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            //strSqlString.Append("          AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("        AND MNG.USR_CMF_10 <> 'COMPLETE' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("       ) " + "\n");
                            strSqlString.Append(" GROUP BY MAT_ID " + "\n");
                        }
                        else
                        {
                            strSqlString.Append("SELECT MAT_ID  " + "\n");
                            strSqlString.Append(strDecodeQuery);
                            strSqlString.Append("  FROM ( " + "\n");
                            strSqlString.Append("        SELECT STS.MAT_ID " + "\n");
                            strSqlString.Append("             , (SELECT DESC_1 FROM CABRDFTDEF@RPTTOMES WHERE DEL_FLAG=' ' AND FACTORY = STS.FACTORY AND DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT_CODE " + "\n");
                            strSqlString.Append("          FROM CABRLOTSTS@RPTTOMES STS " + "\n");
                            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("         WHERE 1=1 " + "\n");
                            strSqlString.Append("           AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("           AND STS.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("           AND STS.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("           AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("           AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("           AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            strSqlString.Append("           AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("   AND STS.OPER_1 BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("           AND STS.CLOSE_FLAG <> 'Y' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("       ) " + "\n");
                            strSqlString.Append(" GROUP BY MAT_ID " + "\n");
                        }
                        #endregion
                    }
                    break;
                case 5: // 6. STEP 별 부적합 발생 건
                    {
                        #region Chart No.6
                        if (cdvFactory.Text.Trim() == "HMKB1")
                        {
                            strSqlString.Append("SELECT DECODE(MNG.ABN_STATUS, 1, '부적합발행', 2, '발생원인도출', 3, '개선대책수립', 4, '기술승인', 5, '품질부서확인', 6, '자재처리') AS ABN_STATUS " + "\n");
                            strSqlString.Append("     , COUNT(ABN_STATUS) AS CNT " + "\n");
                            strSqlString.Append("          FROM CQCMABNMNG@RPTTOMES MNG " + "\n");
                            strSqlString.Append("             , CQCMABFAIL@RPTTOMES FAI " + "\n");
                            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("          WHERE 1=1 " + "\n");
                            strSqlString.Append("            AND MNG.ABN_SEQ = FAI.ABN_SEQ " + "\n");
                            strSqlString.Append("            AND MNG.ABN_CODE = FAI.ABN_CODE " + "\n");
                            strSqlString.Append("            AND FAI.DELETE_FLAG <> 'Y' " + "\n");
                            strSqlString.Append("            AND MNG.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("            AND MNG.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("            AND MNG.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("            AND MNG.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("            AND MNG.ABN_TYPE " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("            AND MNG.ABN_STATUS " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("            AND MNG.OPER BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                            strSqlString.Append("            AND FAI.FAIL_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            //strSqlString.Append("          AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("        AND MNG.USR_CMF_10 <> 'COMPLETE' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append(" GROUP BY MNG.FACTORY, ABN_STATUS " + "\n");
                        }
                        else
                        {
                            strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_ABR_STEP' AND FACTORY = STS.FACTORY AND KEY_1 = STS.CUR_STEP) AS CUR_STEP " + "\n");
                            strSqlString.Append("     , COUNT(CUR_STEP) AS CNT " + "\n");
                            strSqlString.Append("  FROM CABRLOTSTS@RPTTOMES STS " + "\n");
                            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append(" WHERE 1=1 " + "\n");
                            strSqlString.Append("   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("   AND STS.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            strSqlString.Append("   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("   AND STS.OPER_1 BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
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

                            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append(" GROUP BY STS.FACTORY, CUR_STEP " + "\n");
                        }
                        #endregion
                    }
                    break;
                case 6: // 7. 작업자별 부적합 발생 건
                    {
                        #region Chart No.7
                        if (cdvFactory.Text.Trim() == "HMKB1")
                        {
                            strSqlString.Append("SELECT OPER_USER, CNT " + "\n");
                            strSqlString.Append("  FROM ( " + "\n");
                            strSqlString.Append("        SELECT (SELECT USER_DESC || '(' || USER_ID || ')' FROM RWEBUSRDEF WHERE USER_ID = FAI.OPERATOR) AS OPER_USER " + "\n");
                            strSqlString.Append("             , COUNT(OPERATOR) AS CNT " + "\n");
                            strSqlString.Append("             , ROW_NUMBER() OVER(ORDER BY COUNT(OPERATOR) DESC) AS NUM " + "\n");
                            strSqlString.Append("          FROM CQCMABNMNG@RPTTOMES MNG " + "\n");
                            strSqlString.Append("             , CQCMABFAIL@RPTTOMES FAI " + "\n");
                            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("          WHERE 1=1 " + "\n");
                            strSqlString.Append("            AND MNG.ABN_SEQ = FAI.ABN_SEQ " + "\n");
                            strSqlString.Append("            AND MNG.ABN_CODE = FAI.ABN_CODE " + "\n");
                            strSqlString.Append("            AND FAI.DELETE_FLAG <> 'Y' " + "\n");
                            strSqlString.Append("            AND MNG.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("            AND MNG.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("            AND MNG.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("            AND MNG.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("            AND MNG.ABN_TYPE " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("            AND MNG.ABN_STATUS " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("            AND MNG.OPER BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                            strSqlString.Append("            AND FAI.FAIL_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            //strSqlString.Append("          AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("        AND MNG.USR_CMF_10 <> 'COMPLETE' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                                strSqlString.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("         GROUP BY OPERATOR " + "\n");
                            strSqlString.Append("         ORDER BY CNT DESC, OPER_USER " + "\n");
                            strSqlString.Append("       ) " + "\n");

                            if (cboUserCnt.Text == "10명")
                                strSqlString.Append(" WHERE NUM < 11 " + "\n");

                            if (cboUserCnt.Text == "20명")
                                strSqlString.Append(" WHERE NUM < 21 " + "\n");
                        }
                        else
                        {
                            strSqlString.Append("SELECT OPER_USER, CNT " + "\n");
                            strSqlString.Append("  FROM ( " + "\n");
                            strSqlString.Append("        SELECT (SELECT USER_DESC || '(' || USER_ID || ')' FROM RWEBUSRDEF WHERE USER_ID = STS.QA_OPER) AS OPER_USER " + "\n");
                            strSqlString.Append("             , COUNT(QA_OPER) AS CNT " + "\n");
                            strSqlString.Append("             , ROW_NUMBER() OVER(ORDER BY COUNT(QA_OPER) DESC) AS NUM " + "\n");
                            strSqlString.Append("          FROM CABRLOTSTS@RPTTOMES STS " + "\n");
                            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
                            strSqlString.Append("         WHERE 1=1 " + "\n");
                            strSqlString.Append("           AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                            strSqlString.Append("           AND STS.FACTORY = MAT.FACTORY " + "\n");
                            strSqlString.Append("           AND STS.MAT_ID = MAT.MAT_ID " + "\n");
                            strSqlString.Append("           AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                            strSqlString.Append("           AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                            strSqlString.Append("           AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                            strSqlString.Append("           AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                            strSqlString.Append("           AND STS.OPER_1 BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");

                            if (!ckbEndData.Checked)
                            {
                                strSqlString.Append("           AND STS.CLOSE_FLAG <> 'Y' " + "\n");
                            }

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                                strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                            #endregion

                            strSqlString.Append("         GROUP BY QA_OPER " + "\n");
                            strSqlString.Append("         ORDER BY CNT DESC, OPER_USER " + "\n");
                            strSqlString.Append("       ) " + "\n");

                            if (cboUserCnt.Text == "10명")
                                strSqlString.Append(" WHERE NUM < 11 " + "\n");

                            if (cboUserCnt.Text == "20명")
                                strSqlString.Append(" WHERE NUM < 21 " + "\n");
                        }

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

        #region 조회된 데이터중 불량명만을 구하기 위한 쿼리
        private string MakeLossCodeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                strSqlString.Append("SELECT DISTINCT FAI.FAIL_DESC AS DEFECT_CODE " + "\n");
                strSqlString.Append("  FROM CQCMABNMNG@RPTTOMES MNG, CQCMABFAIL@RPTTOMES FAI, MWIPMATDEF MAT " + "\n");                
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND MNG.ABN_SEQ = FAI.ABN_SEQ " + "\n");
                strSqlString.Append("   AND MNG.ABN_CODE = FAI.ABN_CODE " + "\n");
                strSqlString.Append("   AND FAI.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("   AND MNG.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("   AND MNG.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.Append("   AND MNG.MAT_ID = MAT.MAT_ID " + "\n");

                strSqlString.Append("   AND MNG.CREATE_TIME BETWEEN '" + cdvFromTo.ExactFromDate + "' AND '" + cdvFromTo.ExactToDate + "'" + "\n");
                strSqlString.Append("   AND SUBSTR(MNG.ABN_TYPE, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");                
                strSqlString.Append("   AND MNG.ABN_STATUS " + cdvStep.SelectedValueToQueryString + "\n");
                strSqlString.Append("   AND MNG.OPER BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");
                strSqlString.Append("   AND FAI.FAIL_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                if (!ckbEndData.Checked)
                {
                    strSqlString.Append("   AND MNG.USR_CMF_10 <> 'COMPLETE' " + "\n");
                }

                #region 상세 조회에 따른 SQL문 생성
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                #endregion
            }
            else
            {
                strSqlString.Append("SELECT DISTINCT (SELECT DESC_1 FROM CABRDFTDEF@RPTTOMES WHERE DEL_FLAG=' ' AND FACTORY = STS.FACTORY AND DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT_CODE " + "\n");
                strSqlString.Append("  FROM CABRLOTSTS@RPTTOMES STS " + "\n");
                strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("   AND STS.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.Append("   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("   AND STEP10_TIME BETWEEN '" + cdvFromTo.ExactFromDate + "' AND '" + cdvFromTo.ExactToDate + "'" + "\n");
                strSqlString.Append("   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                strSqlString.Append("   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                strSqlString.Append("   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                strSqlString.Append("   AND STS.OPER_1 BETWEEN '" + cdvFromOper.Text + "'" + "AND '" + cdvToOper.Text + "'" + "\n");

                if (!ckbEndData.Checked)
                {
                    strSqlString.Append("   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
                }

                #region 상세 조회에 따른 SQL문 생성
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

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion
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
            // 차트 설정            
            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartSqlString());
            
            if (dt == null || dt.Rows.Count < 1)
                return;

            
            udcMSChart1.RPT_1_ChartInit();
            //udcMSChart1.RPT_2_ClearData();

            switch (cboGraph.SelectedIndex)
            {
                case 0: // 1. 부적합 종류별  발생 건 & TAT
                    {
                        #region Chart No.1

                        udcMSChart1.DataSource = dt;
                        udcMSChart1.RPT_3_OpenData(2, dt.Rows.Count);

                        for (int i = 0; i < dt.Columns.Count - 1; i++)
                        {
                            udcMSChart1.Series[i].XValueMember = Convert.ToString(dt.Columns[0]);
                            udcMSChart1.Series[i].YValueMembers = Convert.ToString(dt.Columns[i + 1]);

                            udcMSChart1.Series[i].IsValueShownAsLabel = true;
                            udcMSChart1.Series[i].IsVisibleInLegend = true;
                            udcMSChart1.Series[i].LegendText = dt.Columns[i + 1].ToString();
                        }
                        udcMSChart1.DataBind();

                        udcMSChart1.Series[0].ChartType = SeriesChartType.Column;
                        udcMSChart1.Series[0].LabelForeColor = Color.Blue;
                        udcMSChart1.Series[1].YAxisType = AxisType.Secondary;
                        udcMSChart1.Series[1].ChartType = SeriesChartType.Line;
                        udcMSChart1.Series[1].LabelForeColor = Color.Red;
                        udcMSChart1.Series[1].MarkerStyle = MarkerStyle.Circle;
                        udcMSChart1.Series[1].MarkerSize = 10;

                        udcMSChart1.Legends[0].Docking = Docking.Top;
                        udcMSChart1.Legends[0].Alignment = StringAlignment.Center;

                        udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        udcMSChart1.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 0;
                        udcMSChart1.ChartAreas[0].AxisY.Maximum = udcMSChart1.ChartAreas[0].AxisY.Maximum * 1.2;
                        
                        #endregion
                    }
                    break;

                case 1: // 2. 기간 별 부적합 발생 건
                    {
                        #region Chart No.2
                        
                        int rowCount = dt.Rows.Count;
                       
                        udcMSChart1.RPT_3_OpenData(1, rowCount);

                        int[] cnt_rows = new Int32[rowCount];

                        for (int i = 0; i < cnt_rows.Length; i++)
                        {
                            cnt_rows[i] = i;
                        }

                        // 건수                        
                        double cnt = udcMSChart1.RPT_4_AddData(dt, cnt_rows, new int[] { 1 }, SeriseType.Column);
                       
                        //각 Serise별로 다른 타입을 사용할 경우
                        //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "Number", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
                        //udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);
                        //udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "Number" }, SoftwareFX.ChartFX.Docked.Top);

                        udcMSChart1.RPT_6_SetGallery(SeriesChartType.Column, 0, 1, "Number", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
                        
                        udcMSChart1.RPT_8_SetSeriseLegend(new string[] { "Number" }, System.Windows.Forms.DataVisualization.Charting.Docking.Top);
                        
                        
                        // 기타 설정
                        udcMSChart1.Series[0].IsValueShownAsLabel = true;
                        udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        
                        //Series series1 = null;
                        //series1 = new Series();
                        //series1.Name = "Number";                        
                        //chart1.Series.Add(series1);
                        //series1.ChartType = SeriesChartType.Column;                        
                        //chart1.Series[0].Points.DataBindXY(dt.Rows, "TRAN_TIME", dt.Rows, "CNT");
                        //chart1.Series[0].Legend = "Default";
                        //chart1.Series[0]["DrawingStyle"] = "Cylinder";
                        //chart1.Series[0].IsValueShownAsLabel = true;
                        //chart1.Series[0].IsVisibleInLegend = true;
                        //chart1.Series[0].LabelForeColor = Color.Blue;
                        //chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        //chart1.ChartAreas[0].AxisY.Interval = 1;
                        //chart1.ChartAreas[0].AxisY.Title = "Number";
                        //chart1.ChartAreas[0].AxisY.IsInterlaced = true;
                        //chart1.ChartAreas[0].AxisY.InterlacedColor = Color.FromArgb(80, Color.DarkGray);

                        #endregion
                    }
                    break;

                case 2: // 3. 불량별 파레토
                    {
                        #region Chart No.3
                        
                        int rowCount = dt.Rows.Count;

                        udcMSChart1.RPT_3_OpenData(2, rowCount);

                        int[] cnt_rows = new Int32[rowCount];

                        for (int i = 0; i < cnt_rows.Length; i++)
                        {
                            cnt_rows[i] = i;
                        }

                        // 건수                        
                        double cnt = udcMSChart1.RPT_4_AddData(dt, cnt_rows, new int[] { 1 }, SeriseType.Column);

                        // 누적점유율                        
                        double percent = udcMSChart1.RPT_4_AddData(dt, cnt_rows, new int[] { 2 }, SeriseType.Column);

                        //각 Serise별로 다른 타입을 사용할 경우
                        //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "Number", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
                        //udcChartFX1.RPT_6_SetGallery(ChartType.Line, 1, 1, "Cumulative occupation", AsixType.Y2, DataTypes.Initeger, percent);

                        udcMSChart1.RPT_6_SetGallery(SeriesChartType.Column, 0, 1, "Number", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
                        udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, 1, 1, "Cumulative occupation", AsixType.Y2, DataTypes.Initeger, percent);

                        //각 Serise별로 동일한 타입을 사용할 경우
                        //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, yield * 1.2);

                        //udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);
                        //udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "Number", "Cumulative occupation" }, SoftwareFX.ChartFX.Docked.Top);
                        
                        //udcMSChart1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);
                        udcMSChart1.RPT_8_SetSeriseLegend(new string[] { "Number", "Cumulative occupation" }, System.Windows.Forms.DataVisualization.Charting.Docking.Top);


                        // 기타 설정
                        udcMSChart1.Series[1].BorderWidth = 2;
                        udcMSChart1.Series[1].MarkerStyle = MarkerStyle.Circle;
                        //chart1.Series[1].MarkerSize = 10;                        
                        udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        udcMSChart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                        udcMSChart1.ChartAreas[0].AxisY.IsInterlaced = true;
                        udcMSChart1.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 0;


                        //Series series1 = null;
                        //Series series2 = null;
                        //series1 = new Series();
                        //series2 = new Series();
                        //series1.Name = "Number";
                        //series2.Name = "Cumulative occupation";

                        //chart1.Series.Add(series1);
                        //chart1.Series.Add(series2);
                        //series1.ChartType = SeriesChartType.Column;
                        //series2.ChartType = SeriesChartType.Line;

                        //chart1.Series[0].Points.DataBindXY(dt.Rows, "DEFECT_CODE", dt.Rows, "QTY");
                        //chart1.Series[1].Points.DataBindXY(dt.Rows, "DEFECT_CODE", dt.Rows, "ADDED_RATIO");

                        //chart1.Series[0].Legend = "Default";
                        //chart1.Series[0]["DrawingStyle"] = "Cylinder";
                        //chart1.Series[0].IsVisibleInLegend = true;
                        //chart1.Series[0].YAxisType = AxisType.Primary;
                        //chart1.Series[1].Legend = "Default";
                        //chart1.Series[1].IsVisibleInLegend = true;
                        //chart1.Series[1].MarkerStyle = MarkerStyle.Circle;
                        //chart1.Series[1].MarkerSize = 10;
                        //chart1.Series[1].BorderWidth = 3;                        
                        //chart1.Series[1].YAxisType = AxisType.Secondary;
                        //chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        //chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                        //chart1.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 0;                        
                        //chart1.ChartAreas[0].AxisY.Title = "Number";
                        //chart1.ChartAreas[0].AxisY2.Title = "Cumulative occupation";
                        //chart1.ChartAreas[0].AxisY.IsInterlaced = true;
                        //chart1.ChartAreas[0].AxisY.InterlacedColor = Color.FromArgb(80, Color.DarkGray);

                        #endregion
                    }
                    break;

                case 3: // 4. 불량별 부적합 발생 건 & 생산량
                    {
                        #region Chart No.4
                        
                        double cnt = 0;
                        double max = 0;
                        double max_temp = 0;
                        int arrCnt = 0;

                        int rowCount = dt.Rows.Count;

                        udcMSChart1.RPT_3_OpenData(dt.Columns.Count - 1, rowCount);                        

                        string[] LegBox = new string[dt.Columns.Count - 1]; //데이터 테이블의 2째 컬럼헤더 부터 사용하기에 -1을 한다.
                        int[] loss_rows = new Int32[rowCount];
                        int[] tot_rows = new Int32[rowCount];

                        for (int i = 0; i < loss_rows.Length; i++)
                        {
                            loss_rows[i] = i;
                            tot_rows[i] = i;
                        }

                        // 데이터 테이블의 컬럼헤더를 저장한다. LegBox 표시 값으로 사용하려...
                        for (int x = 1; x < dt.Columns.Count; x++)
                        {
                            LegBox[arrCnt] = dt.Columns[x].ToString();

                            arrCnt++;
                        }

                        // 생산 수량 표시                        
                        cnt = udcMSChart1.RPT_4_AddData(dt, tot_rows, new int[] { 1 }, SeriseType.Column);

                        for (int i = 2; i < dt.Columns.Count; i++)
                        {
                            // 각 불량 수 표시                            
                            max = udcMSChart1.RPT_4_AddData(dt, loss_rows, new int[] { i }, SeriseType.Column);

                            if (max > max_temp)
                            {
                                max_temp = max;
                            }
                        }
                        max = max_temp;
                                                
                        udcMSChart1.RPT_6_SetGallery(SeriesChartType.Column, 0, 1, "output", AsixType.Y2, DataTypes.Initeger, cnt * 1.2);

                        for (int i = 1; i < dt.Columns.Count-1; i++)
                        {                            
                            udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, i, 1, "", AsixType.Y, DataTypes.Initeger, max * 1.2);
                        }

                        //udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);                        
                        udcMSChart1.RPT_8_SetSeriseLegend(LegBox, System.Windows.Forms.DataVisualization.Charting.Docking.Right);

                        // 기타 설정                        
                        udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        udcMSChart1.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 0;

                        //Series series = null;
                        //legend1.Docking = Docking.Right;
                        
                        //chart1.DataSource = dt;
                        //for (int i = 0; i < dt.Columns.Count - 1; i++)
                        //{
                        //    series = new Series();
                        //    series.Name = Convert.ToString(dt.Columns[i + 1]);
                        //    series.IsVisibleInLegend = true;
                        //    if(i == 0)
                        //        series.ChartType = SeriesChartType.Column;
                        //    else
                        //        series.ChartType = SeriesChartType.Line;

                        //    chart1.Series.Add(series);
                        //    chart1.Series[i].Legend = "Default";
                            
                        //    if (i == 0)
                        //    {
                        //        chart1.Series[i]["DrawingStyle"] = "Cylinder";
                        //        chart1.Series[i].YAxisType = AxisType.Secondary;                                
                        //    }
                        //    else
                        //    {
                        //        chart1.Series[i].YAxisType = AxisType.Primary;
                        //        chart1.Series[i].MarkerStyle = MarkerStyle.Circle;
                        //        chart1.Series[i].MarkerSize = 10;
                        //        chart1.Series[i].BorderWidth = 3;
                        //    }

                        //    chart1.Series[i].XValueMember = Convert.ToString(dt.Columns[0]);
                        //    chart1.Series[i].YValueMembers = Convert.ToString(dt.Columns[i + 1]);
                        //}
                        //chart1.DataBind();

                        //chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        //chart1.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 0;
                        //chart1.ChartAreas[0].AxisY.IsInterlaced = true;
                        //chart1.ChartAreas[0].AxisY.InterlacedColor = Color.FromArgb(80, Color.DarkGray);
                        //chart1.ChartAreas[0].AxisY2.Title = "output";

                        #endregion
                    }
                    break;
                case 4: // 5. 불량별 부적합 제품 발생 건
                    {
                        #region Chart No.5
                        
                        double max = 0;
                        double max_temp = 0;
                        int arrCnt = 0;

                        int rowCount = dt.Rows.Count;

                        udcMSChart1.RPT_3_OpenData(dt.Columns.Count - 1, rowCount);
                        
                        string[] LegBox = new string[dt.Columns.Count - 1]; //데이터 테이블의 2째 컬럼헤더 부터 사용하기에 -1을 한다.
                        int[] loss_rows = new Int32[rowCount];

                        for (int i = 0; i < loss_rows.Length; i++)
                        {
                            loss_rows[i] = i;
                        }

                        // 데이터 테이블의 컬럼헤더를 저장한다. LegBox 표시 값으로 사용하려...
                        for (int x = 1; x < dt.Columns.Count; x++)
                        {
                            LegBox[arrCnt] = dt.Columns[x].ToString();

                            arrCnt++;
                        }

                        for (int i = 1; i < dt.Columns.Count; i++)
                        {
                            // 각 불량 수 표시                            
                            max = udcMSChart1.RPT_4_AddData(dt, loss_rows, new int[] { i }, SeriseType.Column);

                            if (max > max_temp)
                            {
                                max_temp = max;
                            }
                        }
                        max = max_temp;

                        for (int i = 0; i < dt.Columns.Count - 1; i++)
                        {                            
                            udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, i, 1, "", AsixType.Y, DataTypes.Initeger, max * 1.2);

                            udcMSChart1.Series[i].BorderWidth = 1;
                            udcMSChart1.Series[i].MarkerStyle = MarkerStyle.Circle;
                            udcMSChart1.Series[i].MarkerSize = 7;
                        }

                        //udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);                        
                        udcMSChart1.RPT_8_SetSeriseLegend(LegBox, System.Windows.Forms.DataVisualization.Charting.Docking.Right);

                        // 기타 설정
                        udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        udcMSChart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                        udcMSChart1.ChartAreas[0].AxisY.IsInterlaced = true;
                        
                        //Series series = null;
                        //legend1.Docking = Docking.Right;
                        //legend1.Alignment = StringAlignment.Center;

                        //chart1.DataSource = dt;
                        //for (int i = 0; i < dt.Columns.Count - 1; i++)
                        //{
                        //    series = new Series();
                        //    series.Name = Convert.ToString(dt.Columns[i + 1]);
                        //    series.IsVisibleInLegend = true;                            
                        //    series.ChartType = SeriesChartType.Line;

                        //    chart1.Series.Add(series);
                        //    chart1.Series[i].Legend = "Default";
                        //    chart1.Series[i].MarkerStyle = MarkerStyle.Circle;
                        //    chart1.Series[i].MarkerSize = 10;
                        //    chart1.Series[i].BorderWidth = 3;                            

                        //    chart1.Series[i].XValueMember = Convert.ToString(dt.Columns[0]);
                        //    chart1.Series[i].YValueMembers = Convert.ToString(dt.Columns[i + 1]);
                        //}
                        //chart1.DataBind();
                                                
                        //chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        //chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                        //chart1.ChartAreas[0].AxisY.IsInterlaced = true;
                        //chart1.ChartAreas[0].AxisY.InterlacedColor = Color.FromArgb(80, Color.DarkGray);
                        
                        #endregion
                    }
                    break;
                case 5: // 6. STEP 별 부적합 발생 건
                    {
                        #region Chart No.6
                        
                        int rowCount = dt.Rows.Count;

                        udcMSChart1.RPT_3_OpenData(1, rowCount);

                        int[] cnt_rows = new Int32[rowCount];

                        for (int i = 0; i < cnt_rows.Length; i++)
                        {
                            cnt_rows[i] = i;
                        }

                        // 건수                        
                        double cnt = udcMSChart1.RPT_4_AddData(dt, cnt_rows, new int[] { 1 }, SeriseType.Column);

                        //각 Serise별로 다른 타입을 사용할 경우                        
                        udcMSChart1.RPT_6_SetGallery(SeriesChartType.Column, 0, 1, "Number", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
                        
                        //udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);                        
                        udcMSChart1.RPT_8_SetSeriseLegend(new string[] { "Number" }, System.Windows.Forms.DataVisualization.Charting.Docking.Top);

                        // 기타 설정
                        udcMSChart1.Series[0]["DrawingStyle"] = "Cylinder";
                        udcMSChart1.Series[0].IsValueShownAsLabel = true;
                        udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        udcMSChart1.ChartAreas[0].AxisY.IsInterlaced = true;
                        udcMSChart1.Legends[0].Alignment = StringAlignment.Center;

                        //Series series1 = null;
                        //series1 = new Series();
                        //series1.Name = "Number";
                        //chart1.Series.Add(series1);
                        //series1.ChartType = SeriesChartType.Column;

                        //chart1.Series[0].Points.DataBindXY(dt.Rows, "CUR_STEP", dt.Rows, "CNT");
                        //chart1.Series[0].Legend = "Default";
                        //chart1.Series[0]["DrawingStyle"] = "Cylinder";
                        //chart1.Series[0].IsValueShownAsLabel = true;
                        //chart1.Series[0].IsVisibleInLegend = true;
                        //chart1.Series[0].LabelForeColor = Color.Blue;
                        //chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        //chart1.ChartAreas[0].AxisY.Interval = 1;
                        //chart1.ChartAreas[0].AxisY.Title = "Number";
                        //chart1.ChartAreas[0].AxisY.IsInterlaced = true;
                        //chart1.ChartAreas[0].AxisY.InterlacedColor = Color.FromArgb(80, Color.DarkGray);

                        #endregion
                    }
                    break;
                case 6: // 7. 작업자별 부적합 발생 건
                    {
                        #region Chart No.7
                        
                        int rowCount = dt.Rows.Count;

                        udcMSChart1.RPT_3_OpenData(1, rowCount);

                        int[] cnt_rows = new Int32[rowCount];

                        for (int i = 0; i < cnt_rows.Length; i++)
                        {
                            cnt_rows[i] = i;
                        }

                        // 건수                        
                        double cnt = udcMSChart1.RPT_4_AddData(dt, cnt_rows, new int[] { 1 }, SeriseType.Column);

                        //각 Serise별로 다른 타입을 사용할 경우                        
                        udcMSChart1.RPT_6_SetGallery(SeriesChartType.Column, 0, 1, "Number", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
                                                
                        udcMSChart1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);                        
                        udcMSChart1.RPT_8_SetSeriseLegend(new string[] { "Number" }, System.Windows.Forms.DataVisualization.Charting.Docking.Top);

                        // 기타 설정                        
                        udcMSChart1.Series[0]["DrawingStyle"] = "Cylinder";
                        udcMSChart1.Series[0].IsValueShownAsLabel = true;
                        udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        udcMSChart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                        udcMSChart1.ChartAreas[0].AxisX.Interval = 1;
                        udcMSChart1.ChartAreas[0].AxisY.IsInterlaced = true;
                        udcMSChart1.Legends[0].Alignment = StringAlignment.Center;
                                                
                        //Series series1 = null;
                        //series1 = new Series();
                        //series1.Name = "Number";

                        //chart1.Series.Add(series1);
                        //series1.ChartType = SeriesChartType.Column;

                        //chart1.Series[0].Points.DataBindXY(dt.Rows, "OPER_USER", dt.Rows, "CNT");
                        //chart1.Series[0].Legend = "Default";
                        //chart1.Series[0]["DrawingStyle"] = "Cylinder";
                        //chart1.Series[0].IsValueShownAsLabel = true;
                        //chart1.Series[0].IsVisibleInLegend = true;
                        //chart1.Series[0].LabelForeColor = Color.Blue;
                        //chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        //chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                        //chart1.ChartAreas[0].AxisY.Interval = 1;
                        //chart1.ChartAreas[0].AxisY.Title = "Number";
                        //chart1.ChartAreas[0].AxisY.IsInterlaced = true;
                        //chart1.ChartAreas[0].AxisY.InterlacedColor = Color.FromArgb(80, Color.DarkGray);

                        #endregion
                    }
                    break;
            }

            //udcChartFX1.AxisX.Title.Text = "Nonconformance type";
        }


        private void GetLossData()
        {
            string strQuery = string.Empty;

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                strQuery += "SELECT DISTINCT FAI.FAIL_CODE AS Code, FAI.FAIL_DESC AS Data" + "\n";
                strQuery += "  FROM CQCMABFAIL@RPTTOMES FAI, CQCMABNMNG@RPTTOMES MNG " + "\n";
                strQuery += " WHERE 1=1 " + "\n";
                strQuery += " AND FAI.ABN_SEQ = MNG.ABN_SEQ  " + "\n";
                strQuery += " AND FAI.ABN_CODE = MNG.ABN_CODE " + "\n";
                strQuery += " AND MNG.FACTORY = 'HMKB1' " + "\n";
                strQuery += " ORDER BY FAIL_CODE" + "\n";

            }
            else
            {
                strQuery += "SELECT DEFECT_CODE AS Code, DESC_1 AS Data" + "\n";
                strQuery += "  FROM CABRDFTDEF@RPTTOMES " + "\n";
                strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
                strQuery += " ORDER BY DEFECT_CODE" + "\n";
            }

            cdvLoss.sDynamicQuery = strQuery;
        }

        private void GetStepData()
        {
            string strQuery = string.Empty;
            
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                cdvStep.sCodeColumnName = "Code";
                cdvStep.sValueColumnName = "Data";

                //ABN_STATUS 해당 TABLE 없음.
                strQuery += " SELECT '1' AS Code, '부적합발행' AS Data " + "\n";
                strQuery += " FROM DUAL " + "\n";
                strQuery += " UNION ALL " + "\n";
                strQuery += " SELECT '2' AS Code, '발생원인도출' AS Data " + "\n";
                strQuery += " FROM DUAL " + "\n";
                strQuery += " UNION ALL " + "\n";
                strQuery += " SELECT '3' AS Code, '개선대책수립' AS Data " + "\n";
                strQuery += " FROM DUAL " + "\n";
                strQuery += " UNION ALL " + "\n";
                strQuery += " SELECT '4' AS Code, '기술승인' AS Data " + "\n";
                strQuery += " FROM DUAL " + "\n";
                strQuery += " UNION ALL " + "\n";
                strQuery += " SELECT '5' AS Code, '품질부서확인' AS Data " + "\n";
                strQuery += " FROM DUAL " + "\n";
                strQuery += " UNION ALL " + "\n";
                strQuery += " SELECT '6' AS Code, '자재처리' AS Data " + "\n";
                strQuery += " FROM DUAL " + "\n";

                cdvStep.sDynamicQuery = strQuery;
            }
            else
            {
                cdvStep.sTableName = "H_ABR_STEP"; 
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

            DataTable dt = null;            
            GridColumnInit();

            try
            {
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);
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
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 10, null, null, btnSort);

                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);

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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.Instance.subMakeMsChartExcel(spdData, udcMSChart1, this.lblTitle.Text, null, null);

            //ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
            ////spdData.ExportExcel();
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

        private void cdvLoss_ValueButtonPress(object sender, EventArgs e)
        {
            GetLossData();

            //string strQuery = string.Empty;

            //strQuery += "SELECT DEFECT_CODE AS Code, DESC_1 AS Data" + "\n";
            //strQuery += "  FROM CABRDFTDEF@RPTTOMES " + "\n";
            //strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
            //strQuery += " ORDER BY DEFECT_CODE" + "\n";

            //cdvLoss.sDynamicQuery = strQuery;
        }

        private void cdvStep_ValueButtonPress(object sender, EventArgs e)
        {
            GetStepData();
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
            
            this.SetFactory(cdvFactory.txtValue);

            cdvLoss.sFactory = cdvFactory.txtValue;
            cdvFromOper.sFactory = cdvFactory.txtValue;

            SortInit();     //add 150529
        }

        private void cboGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Step별 부적합 그래프 조회시 기본으로 종료건 포함시키지 않기 위해(임성빈 요청)
            if (cboGraph.SelectedIndex.ToString() == "5")
            {
                ckbEndData.Checked = false;
            }
            else // 그 외 그래프는 기본적으로 종료건 포함.
            {
                ckbEndData.Checked = true;
            }

            // 2010-09-29-임종우 : 작업자별 부적합 발생건 그래프 조회시 X축 작업자 수 선택 할 수 있도록 (김용복 요청)
            if (cboGraph.SelectedIndex.ToString() == "6")
            {
                cboUserCnt.Visible = true;
            }
            else
            {
                cboUserCnt.Visible = false;
            }
        }

        // 2011-01-18: 김민우

        private void cdvFromOper_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                strQuery += " SELECT '' AS Code, '' AS Data FROM DUAL" + "\n";
                
            }
            else
            {                
                strQuery += "SELECT OPER AS Code, OPER_DESC AS Data FROM MWIPOPRDEF" + "\n";
                strQuery += " WHERE FACTORY = '" + cdvFactory.Text + "' " + "\n";
                strQuery += "   AND OPER_GRP_1 <> '-' " + "\n";
                strQuery += " ORDER BY OPER" + "\n";
            }
            
            cdvFromOper.sDynamicQuery = strQuery;
        }

        private void cdvToOper_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                strQuery += " SELECT '' AS Code, '' AS Data FROM DUAL" + "\n";

            }
            else
            {
                strQuery += "SELECT OPER AS Code, OPER_DESC AS Data FROM MWIPOPRDEF" + "\n";
                strQuery += " WHERE FACTORY = '" + cdvFactory.Text + "' " + "\n";
                strQuery += "   AND OPER_GRP_1 <> '-' " + "\n";
                strQuery += " ORDER BY OPER" + "\n";
            }

            cdvToOper.sDynamicQuery = strQuery;
        }

    }
}