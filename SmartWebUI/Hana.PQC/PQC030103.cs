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
using SoftwareFX.ChartFX;


namespace Hana.PQC
{
    /// <summary>
    /// 클  래  스: PQC030103<br/>
    /// 클래스요약: 부적합현황<br/>
    /// 작  성  자: 미라콤 김민규<br/>
    /// 최초작성일: 2008-01-21<br/>
    /// 상세  설명: 부적합현황<br/>
    /// 변경  내용: [2009.08.14 장은희]  <br/>
    /// </summary>
    /// 

    public partial class PQC030103 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public PQC030103()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
            cdvFromTo.AutoBinding(); 
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvCp.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvLoss.sFactory = GlobalVariable.gsAssyDefaultFactory;

            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 

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
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("FAMILY", 0, 1, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("PACKAGE", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("TYPE1", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("TYPE2", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("LD COUNT", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("DENSITY", 0, 6, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("GENERATION", 0, 7, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 130);

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 8, 2);

            if (cboGraph.SelectedIndex == 6)  // STEP 별 부적합 발생 건
            {

                spdData.RPT_AddBasicColumn("Now" + "\n" + "STEP", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Status of nonconformance", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Nonconformance type", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Issue Number", 1, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of occurrence", 1, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Customer", 1, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Defect name", 1, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("Device", 1, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot No.", 1, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot Qty", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defect quantity", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PKG Type", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LEAD수", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PKG", 1, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Density", 1, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defective rate" + "\n" + "(ppm)", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("End date", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TAT" + "\n" + "(days)", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("Issue Department", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("publisher", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("The details", 0, 27, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);

                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
                spdData.RPT_MerageHeaderColumnSpan(0, 10, 9);
                spdData.RPT_MerageHeaderColumnSpan(0, 19, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 22, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 23, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 24, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 25, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 26, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 27, 2);

            }

            else if (cboGraph.SelectedIndex == 8 || cboGraph.SelectedIndex == 9)  
            {
                spdData.RPT_AddBasicColumn("Status of nonconformance", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

                if (cboGraph.SelectedIndex == 8)
                {
                    spdData.RPT_AddBasicColumn("Cause Classification", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                }
                else if (cboGraph.SelectedIndex == 9)
                {
                    spdData.RPT_AddBasicColumn("Countermeasure classification", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                }
                spdData.RPT_AddBasicColumn("Nonconformance type", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Issue Number", 1, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of occurrence", 1, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Customer", 1, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Defect name", 1, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("Device", 1, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot No.", 1, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot Qty", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defect quantity", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PKG Type", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LEAD수", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PKG", 1, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Density", 1, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defective rate" + "\n" + "(ppm)", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Now" + "\n" + "STEP", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("End date", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TAT" + "\n" + "(days)", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("Issue Department", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("publisher", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("The details", 0, 28, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);    

                spdData.RPT_MerageHeaderColumnSpan(0, 9, 10);
                spdData.RPT_MerageHeaderColumnSpan(0, 19, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 22, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 23, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 24, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 25, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 26, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 27, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 28, 2);

            }               

            else
            {
                spdData.RPT_AddBasicColumn("Status of nonconformance", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

                if (cboGraph.SelectedIndex == 1)  //주기 별 부적합 발생 건
                {
                    spdData.RPT_AddBasicColumn("Date of occurrence", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Nonconformance type", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Issue Number", 1, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 1, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Defect name", 1, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                }

                else if (cboGraph.SelectedIndex == 2)   //주기 별 고객사 발생 건
                {
                    spdData.RPT_AddBasicColumn("Date of occurrence", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Issue Number", 1, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Nonconformance type", 1, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Defect name", 1, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                }
                else if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0 || cboGraph.SelectedIndex == 3 ) //0.부적합 별 발생 건 , 3.부적합 종류 별 TAT 
                {
                    spdData.RPT_AddBasicColumn("Nonconformance type", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Issue Number", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Date of occurrence", 1, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 1, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Defect name", 1, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                }

                else if (cboGraph.SelectedIndex == 4) //고객사 별 부적합 발생 건
                {
                    spdData.RPT_AddBasicColumn("Customer", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Nonconformance type", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Issue Number", 1, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Date of occurrence", 1, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Defect name", 1, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                }

                else if (cboGraph.SelectedIndex == 5) //  5.부적합 별 고객사 발생 건
                {
                    spdData.RPT_AddBasicColumn("Nonconformance type", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Issue Number", 1, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Date of occurrence", 1, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Defect name", 1, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                }

                else if (cboGraph.SelectedIndex == 7) //불량명 별 파레토 
                {
                    spdData.RPT_AddBasicColumn("Defect name", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Issue Number", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Date of occurrence", 1, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Nonconformance type", 1, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 1, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                }

                                
                spdData.RPT_AddBasicColumn("Device", 1, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot No.", 1, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot Qty", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defect quantity", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PKG Type", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LEAD수", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PKG", 1, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Density", 1, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defective rate" + "\n" + "(ppm)", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Now" + "\n" + "STEP", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("End date", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TAT" + "\n" + "(days)", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("Issue Department", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("publisher", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("The details", 0, 27, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);    
                
                spdData.RPT_MerageHeaderColumnSpan(0, 9, 9);
                spdData.RPT_MerageHeaderColumnSpan(0, 18, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 21, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 22, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 23, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 24, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 25, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 26, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 27, 2);
            }

            // Group항목이 있을경우 반드시 선언해줄것.
            spdData.RPT_ColumnConfigFromTable(btnSort);
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
        #region Chart 생성을 위한 쿼리
        private string MakeSqlString(int Step)
        {
            StringBuilder strSqlString = new StringBuilder();

            string[] selectDate1 = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;
            string strDate = string.Empty;
            string QueryCond1 = null;
            
            //추가
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

                    //  SPREAD 
                    {

                        strSqlString.Append("     SELECT  " + QueryCond1 + " \n");

                        if (cboGraph.SelectedIndex == 6)  // STEP 별 부적합 발생 건
                        {
                            strSqlString.Append("             , DECODE(A.CUR_STEP,8,'완료',A.CUR_STEP||'Step') CUR_STEP " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_TYPE' AND KEY_1 = SUBSTR(A.ABR_NO, 0, 3) AND ROWNUM=1), ' ') AS KIND " + "\n");
                            strSqlString.Append("             , ABR_NO  " + "\n");
                            strSqlString.Append("             , TO_CHAR(TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER  " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");
                            strSqlString.Append("             , A.MAT_ID " + "\n");
                            strSqlString.Append("             , LOT_ID " + "\n");
                            strSqlString.Append("             , QTY_1 " + "\n");
                            strSqlString.Append("             , FAIL_QTY               " + "\n");
                            strSqlString.Append("             , B.MAT_GRP_6, B.MAT_GRP_3, B.MAT_GRP_7 " + "\n");
                            strSqlString.Append("             , CASE WHEN QTY_1 > 0    " + "\n");
                            strSqlString.Append("                    THEN ROUND((FAIL_QTY/QTY_1)*1000000 , 1)  " + "\n");
                            strSqlString.Append("                    ELSE 0  " + "\n");
                            strSqlString.Append("               END AS YIELD " + "\n");

                        }

                        else
                        {

                            if (cboGraph.SelectedIndex == 1)   //1.주기 별 부적합 발생 건
                            {
                                strSqlString.Append("             , TO_CHAR(TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_TYPE' AND KEY_1 = SUBSTR(A.ABR_NO, 0, 3) AND ROWNUM=1), ' ') AS KIND " + "\n");
                                strSqlString.Append("             , ABR_NO  " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER  " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");

                            }

                            else if (cboGraph.SelectedIndex == 2) // 2.주기 별 고객사 발생 건
                            {
                                strSqlString.Append("             , TO_CHAR(TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER  " + "\n");
                                strSqlString.Append("             , ABR_NO  " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_TYPE' AND KEY_1 = SUBSTR(A.ABR_NO, 0, 3) AND ROWNUM=1), ' ') AS KIND " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");

                            }
                            else if (cboGraph.SelectedIndex == 0 || cboGraph.SelectedIndex == 3) //0.부적합 별 발생 건 , 4.부적합 종류 별 TAT 
                            {

                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_TYPE' AND KEY_1 = SUBSTR(A.ABR_NO, 0, 3) AND ROWNUM=1), ' ') AS KIND " + "\n");
                                strSqlString.Append("             , ABR_NO  " + "\n");
                                strSqlString.Append("             , TO_CHAR(TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER  " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");
                            }

                            else if (cboGraph.SelectedIndex == 4)   // 5.고객사 별 부적합 발생 건
                            {

                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER  " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_TYPE' AND KEY_1 = SUBSTR(A.ABR_NO, 0, 3) AND ROWNUM=1), ' ') AS KIND " + "\n");
                                strSqlString.Append("             , ABR_NO  " + "\n");
                                strSqlString.Append("             , TO_CHAR(TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");

                            }

                            else if (cboGraph.SelectedIndex == 5)   // 6.부적합 별 고객사 발생 건
                            {

                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_TYPE' AND KEY_1 = SUBSTR(A.ABR_NO, 0, 3) AND ROWNUM=1), ' ') AS KIND " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER  " + "\n");
                                strSqlString.Append("             , ABR_NO  " + "\n");
                                strSqlString.Append("             , TO_CHAR(TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");

                            }

                            else if (cboGraph.SelectedIndex == 7)   // 8.불량명 별 파레토
                            {

                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");
                                strSqlString.Append("             , ABR_NO  " + "\n");
                                strSqlString.Append("             , TO_CHAR(TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_TYPE' AND KEY_1 = SUBSTR(A.ABR_NO, 0, 3) AND ROWNUM=1), ' ') AS KIND " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER  " + "\n");

                            }

                            else if (cboGraph.SelectedIndex == 8 ) 
                            {
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CAUSE' AND KEY_1 = A.RESV_FIELD_9 AND ROWNUM=1), '-') AS REA " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_TYPE' AND KEY_1 = SUBSTR(A.ABR_NO, 0, 3) AND ROWNUM=1), ' ') AS KIND " + "\n");
                                strSqlString.Append("             , ABR_NO  " + "\n");
                                strSqlString.Append("             , TO_CHAR(TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER  " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");
                            }

                            else if (cboGraph.SelectedIndex == 9)
                            {
                                strSqlString.Append("             ,  NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CAUSE' AND KEY_1 = A.RESV_FIELD_10 AND ROWNUM=1), '-') AS SOL " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_TYPE' AND KEY_1 = SUBSTR(A.ABR_NO, 0, 3) AND ROWNUM=1), ' ') AS KIND " + "\n");
                                strSqlString.Append("             , ABR_NO  " + "\n");
                                strSqlString.Append("             , TO_CHAR(TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER  " + "\n");
                                strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");
                            }

                            strSqlString.Append("             , A.MAT_ID " + "\n");
                            strSqlString.Append("             , LOT_ID " + "\n");
                            strSqlString.Append("             , QTY_1 " + "\n");
                            strSqlString.Append("             , FAIL_QTY               " + "\n");
                            strSqlString.Append("             , B.MAT_GRP_6, B.MAT_GRP_3, B.MAT_GRP_7 " + "\n");
                            strSqlString.Append("             , CASE WHEN QTY_1 > 0    " + "\n");
                            strSqlString.Append("                    THEN ROUND((FAIL_QTY/QTY_1)*1000000 , 1)  " + "\n");
                            strSqlString.Append("                    ELSE 0  " + "\n");
                            strSqlString.Append("               END AS YIELD " + "\n");
                            strSqlString.Append("             , DECODE(A.CUR_STEP,8,'완료',A.CUR_STEP||'Step') CUR_STEP " + "\n");
                        }


                        strSqlString.Append("             , CASE WHEN CLOSE_FLAG <> ' '  " + "\n");
                        // strSqlString.Append("                    THEN TO_CHAR(TO_DATE(CLOSE_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD')  " + "\n");
                        strSqlString.Append("                      THEN DECODE(A.CLOSE_TIME,' ',' ',TO_CHAR(TO_DATE(A.CLOSE_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD'))  " + "\n");
                        strSqlString.Append("                    ELSE ' ' " + "\n");
                        strSqlString.Append("               END AS END_DAY " + "\n");
                        strSqlString.Append("             , CASE WHEN CLOSE_TIME <> ' '  " + "\n");
                        //                        strSqlString.Append("                    THEN TRUNC(TO_DATE(CLOSE_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'),0)  " + "\n");
                        strSqlString.Append("                    THEN ROUND(TO_DATE(CLOSE_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'),2)  " + "\n");
                        strSqlString.Append("                    ELSE 0 " + "\n");
                        strSqlString.Append("               END AS TAT " + "\n");
                        strSqlString.Append("            ,  NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = STEP1_DEPT AND ROWNUM=1), ' ') AS STEP1_DEPT  " + "\n");
                        strSqlString.Append("            , DECODE(A.QA_OPER,' ', (SELECT USER_ID||'('||USER_DESC||')' FROM  RWEBUSRDEF WHERE USER_ID=A.STEP1_USER)  " + "\n");
                        strSqlString.Append("             , (SELECT USER_ID||'('||USER_DESC||')' FROM  RWEBUSRDEF WHERE USER_ID=A.QA_OPER)) AS  QA_OPER " + "\n");
                        // strSqlString.Append("             , QA_OPER " + "\n");
                        strSqlString.Append("             , CASE CUR_STEP WHEN '1' THEN ' ' " + "\n");
                        strSqlString.Append("                             WHEN '2' THEN STEP1_CMT " + "\n");
                        strSqlString.Append("                             WHEN '3' THEN STEP2_CMT " + "\n");
                        strSqlString.Append("                             WHEN '4' THEN STEP3_CMT " + "\n");
                        strSqlString.Append("                             WHEN '5' THEN STEP4_CMT " + "\n");
                        strSqlString.Append("                             WHEN '6' THEN STEP5_CMT " + "\n");
                        strSqlString.Append("                             WHEN '7' THEN STEP6_CMT " + "\n");
                        strSqlString.Append("                             WHEN '8' THEN STEP7_CMT " + "\n");
                        strSqlString.Append("                             ELSE ' '  " + "\n");
                        strSqlString.Append("               END AS STEP_COMMENT " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("         WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_VER(+) = 0  " + "\n");
                        strSqlString.Append("           AND B.MAT_VER(+) = 1  " + "\n");
                        strSqlString.Append("           AND A.ABR_NO NOT IN ' '  " + "\n");
                        strSqlString.Append("           AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("           AND SUBSTR(A.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

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
                        #endregion

                        strSqlString.Append("ORDER BY   " + QueryCond1 + " \n");

                        if (cboGraph.SelectedIndex == 6)  // STEP 별 부적합 발생 건
                        {
                            strSqlString.Append("               , A.CUR_STEP   , A.ABR_NO" + "\n");

                        }

                        else
                        {

                            if (cboGraph.SelectedIndex == 1)    //1.주기 별 부적합 발생 건
                            {
                                strSqlString.Append("               ,START_DAY  , A.ABR_NO  " + "\n");
                            }

                            else if (cboGraph.SelectedIndex == 2)   //2.주기 별 고객사 발생 건
                            {
                                strSqlString.Append("               , START_DAY , CUSTOMER  " + "\n");
                            }

                            else if (cboGraph.SelectedIndex == 4)  // 4.고객사 별 부적합 발생 건
                            {
                                strSqlString.Append("               , CUSTOMER, A.ABR_NO  " + "\n");
                            }

                            else if (cboGraph.SelectedIndex == 5)   // 5.부적합 별 고객사 발생 건
                            {
                                strSqlString.Append("               , A.ABR_NO  , CUSTOMER " + "\n");
                            }

                            else if (cboGraph.SelectedIndex == 7)   //7.불량명 별 파레토
                            {
                                strSqlString.Append("               , DEFECT_CODE  , A.ABR_NO  " + "\n");
                            }
                            else if (cboGraph.SelectedIndex == 8)   
                            {
                                strSqlString.Append("               , REA DESC , A.ABR_NO ,START_DAY    " + "\n");
                            }
                            else if (cboGraph.SelectedIndex == 9)   
                            {
                                strSqlString.Append("               , SOL DESC , A.ABR_NO ,START_DAY    " + "\n");
                            }

                            else   // 0,4 
                            {
                                strSqlString.Append("               , A.ABR_NO ,START_DAY  " + "\n");
                            }
                        }

                        if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                        {
                            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
                        }

                    }
                    break;

                case 1:   // 부적합종류별발생건(기간) 
                    {
                        #region Chart No.1
                        strSqlString.AppendFormat("     SELECT {0} " + "\n", strDate);
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'ANM', 1, 0)) AS ANM " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'CCN', 1, 0)) AS CCN  " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'CDN', 1, 0)) AS CDN " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'CPK', 1, 0)) AS CPK " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'ICN', 1, 0)) AS ICN  " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'QCN', 1, 0)) AS QCN " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'TAY', 1, 0)) AS TAY " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'IPA', 1, 0)) AS IPA " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'OCN', 1, 0)) AS OCN " + "\n");
                        strSqlString.Append("       FROM ( " + "\n");
                        strSqlString.Append("            SELECT SUBSTR(ABR_NO, 0, 3) AS ABR_NO " + "\n");
                        strSqlString.Append("                 , GET_WORK_DATE(STEP1_TIME, 'D') AS WORK_DATE " + "\n");
                        strSqlString.Append("                 , GET_WORK_DATE(STEP1_TIME, 'W') AS WORK_WEEK " + "\n");
                        strSqlString.Append("                 , GET_WORK_DATE(STEP1_TIME, 'M') AS WORK_MONTH " + "\n");
                        strSqlString.Append("              FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("                 , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("             WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("               AND A.FACTORY = B.FACTORY(+)  " + "\n");
                        strSqlString.Append("               AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
                        strSqlString.Append("               AND A.MAT_VER(+) = 0  " + "\n");
                        strSqlString.Append("               AND B.MAT_VER(+) = 1  " + "\n");
                        strSqlString.Append("               AND A.ABR_NO NOT IN ' '  " + "\n");
                        strSqlString.Append("               AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("               AND SUBSTR(A.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("            )  " + "\n");
                        strSqlString.AppendFormat("   GROUP BY {0}  " + "\n", strDate);
                        strSqlString.AppendFormat("   ORDER BY {0} " + "\n", strDate);
                        #endregion
                    }
                    break;

                case 2:  //고객사별발생건(기간)   
                    {
                        #region Chart No.2
                        strSqlString.Append("     SELECT CUS_ID  " + "\n");

                        for (int i = 0; i < Between; i++)
                        {
                            strSqlString.AppendFormat("          , SUM(DECODE({0}, '{1}', 1, 0)) as \"{1}\"" + "\n", strDate, selectDate1[i].ToString());
                        }

                        strSqlString.Append("       FROM (  " + "\n");
                        strSqlString.Append("            SELECT CUS_ID  " + "\n");
                        strSqlString.Append("                 , GET_WORK_DATE(STEP1_TIME, 'D')  AS WORK_DATE " + "\n");
                        strSqlString.Append("                 , GET_WORK_DATE(STEP1_TIME, 'W')  AS WORK_WEEK " + "\n");
                        strSqlString.Append("                 , GET_WORK_DATE(STEP1_TIME, 'M')  AS WORK_MONTH " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("         WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_VER(+) = 0  " + "\n");
                        strSqlString.Append("           AND B.MAT_VER(+) = 1  " + "\n");
                        strSqlString.Append("           AND A.ABR_NO NOT IN ' '  " + "\n");
                        strSqlString.Append("           AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("           AND SUBSTR(A.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("            )   " + "\n");
                        strSqlString.Append("   GROUP BY CUS_ID   " + "\n");
                        strSqlString.Append("   ORDER BY CUS_ID" + "\n");
                        #endregion
                    }
                    break;

                case 3:  //부적합종류별발생건 
                    {
                        #region Chart No.3
                        strSqlString.Append("        SELECT ' ' " + "\n");

                        strSqlString.Append("             , SUM(DECODE(ABR_NO, 'ANM', 1, 0)) AS ANM " + "\n");
                        strSqlString.Append("             , SUM(DECODE(ABR_NO, 'CCN', 1, 0)) AS CCN  " + "\n");
                        strSqlString.Append("             , SUM(DECODE(ABR_NO, 'CDN', 1, 0)) AS CDN " + "\n");
                        strSqlString.Append("             , SUM(DECODE(ABR_NO, 'CPK', 1, 0)) AS CPK " + "\n");
                        strSqlString.Append("             , SUM(DECODE(ABR_NO, 'ICN', 1, 0)) AS ICN " + "\n");
                        strSqlString.Append("             , SUM(DECODE(ABR_NO, 'QCN', 1, 0)) AS QCN " + "\n");
                        strSqlString.Append("             , SUM(DECODE(ABR_NO, 'TAY', 1, 0)) AS TAY " + "\n");
                        strSqlString.Append("             , SUM(DECODE(ABR_NO, 'IPA', 1, 0)) AS IPA " + "\n");
                        strSqlString.Append("             , SUM(DECODE(ABR_NO, 'OCN', 1, 0)) AS OCN " + "\n");
                        strSqlString.Append("          FROM (SELECT SUBSTR(ABR_NO, 0, 3) ABR_NO   " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("         WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_VER(+) = 0  " + "\n");
                        strSqlString.Append("           AND B.MAT_VER(+) = 1  " + "\n");
                        strSqlString.Append("           AND A.ABR_NO NOT IN ' '  " + "\n");
                        strSqlString.Append("           AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("           AND SUBSTR(A.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("               )   " + "\n");

                        #endregion
                    }
                    break;

                case 4:  //   부적합종류별TAT 
                    {

                        #region Chart No.4

                        strSqlString.Append("  SELECT ABR_NO , ROUND(SUM(TAT)/COUNT(ABR_NO),2) TAT   " + "\n");
                        strSqlString.Append("    FROM ( " + "\n");
                        strSqlString.Append("        SELECT SUBSTR(A.ABR_NO,0,3) AS ABR_NO  " + "\n");
                        strSqlString.Append("             , CASE WHEN CLOSE_TIME <> ' ' " + "\n");
                        // strSqlString.Append("                  THEN TRUNC(TO_DATE(CLOSE_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'),0) " + "\n");
                        strSqlString.Append("                  THEN ROUND(TO_DATE(CLOSE_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS'),2)  " + "\n");
                        strSqlString.Append("                  ELSE 0 " + "\n");
                        strSqlString.Append("               END AS TAT " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("         WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_VER(+) = 0  " + "\n");
                        strSqlString.Append("           AND B.MAT_VER(+) = 1  " + "\n");
                        strSqlString.Append("           AND A.ABR_NO NOT IN ' '  " + "\n");
                        strSqlString.Append("           AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("           AND SUBSTR(A.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("      )   " + "\n");
                        strSqlString.Append("  GROUP BY ABR_NO   " + "\n");
                        strSqlString.Append("  ORDER BY ABR_NO   " + "\n");
                        #endregion
                    }
                    break;

                case 5:  //부적합별고객사현황  
                    {
                        #region Chart No.5
                        strSqlString.Append("     SELECT CUS_ID " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'ANM', 1, 0)) AS ANM  " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'CCN', 1, 0)) AS CCN   " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'CDN', 1, 0)) AS CDN  " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'CPK', 1, 0)) AS CPK  " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'ICN', 1, 0)) AS ICN   " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'QCN', 1, 0)) AS QCN  " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'TAY', 1, 0)) AS TAY           " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'IPA', 1, 0)) AS IPA " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'OCN', 1, 0)) AS OCN " + "\n");
                        strSqlString.Append("       FROM (  " + "\n");
                        strSqlString.Append("            SELECT CUS_ID " + "\n");
                        strSqlString.Append("                 , SUBSTR(ABR_NO, 0, 3) AS ABR_NO " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("         WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_VER(+) = 0  " + "\n");
                        strSqlString.Append("           AND B.MAT_VER(+) = 1  " + "\n");
                        strSqlString.Append("           AND A.ABR_NO NOT IN ' '  " + "\n");
                        strSqlString.Append("           AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("           AND SUBSTR(A.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("            )   " + "\n");
                        strSqlString.Append("   GROUP BY CUS_ID   " + "\n");
                        strSqlString.Append("   ORDER BY CUS_ID  " + "\n");

                        #endregion
                    }
                    break;

                case 6:  //고객사별부적합현황
                    {
                        #region Chart No.6

                        strSqlString.Append("     SELECT CUS_ID " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'ANM', 1, 0)) AS ANM  " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'CCN', 1, 0)) AS CCN   " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'CDN', 1, 0)) AS CDN  " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'CPK', 1, 0)) AS CPK  " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'ICN', 1, 0)) AS ICN   " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'QCN', 1, 0)) AS QCN  " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'TAY', 1, 0)) AS TAY           " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'IPA', 1, 0)) AS IPA " + "\n");
                        strSqlString.Append("          , SUM(DECODE(ABR_NO, 'OCN', 1, 0)) AS OCN " + "\n");
                        strSqlString.Append("       FROM (  " + "\n");
                        strSqlString.Append("            SELECT CUS_ID " + "\n");
                        strSqlString.Append("                 , SUBSTR(ABR_NO, 0, 3) AS ABR_NO " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("         WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_VER(+) = 0  " + "\n");
                        strSqlString.Append("           AND B.MAT_VER(+) = 1  " + "\n");
                        strSqlString.Append("           AND A.ABR_NO NOT IN ' '  " + "\n");
                        strSqlString.Append("           AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("           AND SUBSTR(A.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("            )   " + "\n");
                        strSqlString.Append("   GROUP BY CUS_ID   " + "\n");
                        strSqlString.Append("   ORDER BY CUS_ID  " + "\n");
                        #endregion
                    }
                    break;

                case 7:  // STEP별진행현황 
                    {
                        #region Chart No.7

                        strSqlString.Append("     SELECT CUR_STEP  " + "\n");
                        strSqlString.Append("          , SUM(ANM) AS ANM, SUM(CCN) AS CCN, SUM(CDN) AS CDN, SUM(CPK) AS CPK, SUM(ICN) AS ICN, SUM(QCN) AS QCN,SUM(TAY) AS TAY , SUM(IPA) AS IPA , SUM(OCN) AS OCN " + "\n");
                        strSqlString.Append("      FROM (   " + "\n");
                        strSqlString.Append("          SELECT  CUR_STEP   " + "\n");
                        strSqlString.Append("              , SUM(DECODE(ABR_NO, 'ANM', 1, 0)) AS ANM  " + "\n");
                        strSqlString.Append("              , SUM(DECODE(ABR_NO, 'CCN', 1, 0)) AS CCN " + "\n");
                        strSqlString.Append("              , SUM(DECODE(ABR_NO, 'CDN', 1, 0)) AS CDN  " + "\n");
                        strSqlString.Append("              , SUM(DECODE(ABR_NO, 'CPK', 1, 0)) AS CPK  " + "\n");
                        strSqlString.Append("              , SUM(DECODE(ABR_NO, 'ICN', 1, 0)) AS ICN   " + "\n");
                        strSqlString.Append("              , SUM(DECODE(ABR_NO, 'QCN', 1, 0)) AS QCN  " + "\n");
                        strSqlString.Append("              , SUM(DECODE(ABR_NO, 'TAY', 1, 0)) AS TAY  " + "\n");
                        strSqlString.Append("              , SUM(DECODE(ABR_NO, 'IPA', 1, 0)) AS IPA " + "\n");
                        strSqlString.Append("              , SUM(DECODE(ABR_NO, 'OCN', 1, 0)) AS OCN " + "\n");
                        strSqlString.Append("          FROM (  " + "\n");
                        strSqlString.Append("            SELECT SUBSTR(ABR_NO, 0, 3) AS ABR_NO " + "\n");
                        strSqlString.Append("                 , CUR_STEP                   " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("         WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
                        strSqlString.Append("           AND A.MAT_VER(+) = 0  " + "\n");
                        strSqlString.Append("           AND B.MAT_VER(+) = 1  " + "\n");
                        strSqlString.Append("           AND  A.CUR_STEP <> 8   " + "\n");
                        //strSqlString.Append("           AND A.ABR_NO NOT IN ' '  " + "\n");
                        strSqlString.Append("           AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("           AND SUBSTR(A.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("               AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("            )   " + "\n");
                        strSqlString.Append("      GROUP BY CUR_STEP   " + "\n");
                        strSqlString.Append("       UNION ALL   " + "\n");
                        strSqlString.Append("      SELECT TO_CHAR(ROWNUM)  CUR_STEP, 0,0,0,0,0,0,0,0 ,0 FROM DICT WHERE ROWNUM <=9    " + "\n");
                        strSqlString.Append("     )  " + "\n");
                        strSqlString.Append("   GROUP BY CUR_STEP   " + "\n");
                        strSqlString.Append("   ORDER BY CUR_STEP  " + "\n");
                        #endregion
                    }
                    break;

                case 8:   // 불량별파레토도
                    {
                        #region Chart No.8

                        strSqlString.Append("       SELECT DEFECT_CODE AS OPER, CNT,ROUND(((CNT_S/CNT_A)*100),2) CNT_P  " + "\n");
                        strSqlString.Append("        FROM (   " + "\n");
                        strSqlString.Append("          SELECT DEFECT_CODE, CNT, SUM(CNT) OVER ( ORDER BY CNT DESC, DEFECT_CODE) CNT_S ,SUM(CNT) OVER ( ORDER BY ' ') CNT_A   " + "\n");
                        strSqlString.Append("           FROM (   " + "\n");
                        strSqlString.Append("              SELECT (SELECT DATA_1 FROM  MGCMTBLDAT WHERE ROWNUM=1 AND TABLE_NAME='DEFECT_CODE'AND  KEY_1 =A.DEFECT_CODE ) DEFECT_CODE   " + "\n");
                        strSqlString.Append("                      , SUM(DECODE(DEFECT_CODE, DEFECT_CODE, 1, 0)) AS CNT  " + "\n");
                        strSqlString.Append("                FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("                       , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("              WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("                  AND A.FACTORY = B.FACTORY(+)  " + "\n");
                        strSqlString.Append("                  AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
                        strSqlString.Append("                  AND A.MAT_VER(+) = 0  " + "\n");
                        strSqlString.Append("                  AND B.MAT_VER(+) = 1  " + "\n");
                        strSqlString.Append("                  AND A.ABR_NO NOT IN ' '  " + "\n");
                        strSqlString.Append("                  AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("                  AND SUBSTR(A.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("                  AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                        strSqlString.Append("                  AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("                  GROUP BY DEFECT_CODE   ORDER BY CNT ) " + "\n");
                        strSqlString.Append("       )  ORDER BY  CNT_S   " + "\n");

                        #endregion
                    }
                    break;

                case 9:   // 원인분류에 따른 비율 조회 (사람,설비,재료,방법,환경)
                    {
                        #region Chart No.8

                        strSqlString.Append("          SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CAUSE' AND KEY_1 = A.RESV_FIELD_9 AND ROWNUM=1), '-') AS REA, COUNT(*) AS CNT   " + "\n");
                        strSqlString.Append("                FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("                       , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("              WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("                  AND A.FACTORY = B.FACTORY(+)  " + "\n");
                        strSqlString.Append("                  AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
                        strSqlString.Append("                  AND A.MAT_VER(+) = 0  " + "\n");
                        strSqlString.Append("                  AND B.MAT_VER(+) = 1  " + "\n");
                        strSqlString.Append("                  AND A.ABR_NO NOT IN ' '  " + "\n");
                        strSqlString.Append("                  AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("                  AND SUBSTR(A.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("                  AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                        strSqlString.Append("                  AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("                  GROUP BY A.RESV_FIELD_9 " + "\n");
                        #endregion
                    }
                    break;

                case 10:   // 대책분류에 따른 비율 조회 (사람,설비,재료,방법,환경)
                    {
                        #region Chart No.8
                        strSqlString.Append("          SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CAUSE' AND KEY_1 = A.RESV_FIELD_10 AND ROWNUM=1), '-') AS SOL, COUNT(*) AS CNT   " + "\n");
                        strSqlString.Append("                FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("                       , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("              WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("                  AND A.FACTORY = B.FACTORY(+)  " + "\n");
                        strSqlString.Append("                  AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
                        strSqlString.Append("                  AND A.MAT_VER(+) = 0  " + "\n");
                        strSqlString.Append("                  AND B.MAT_VER(+) = 1  " + "\n");
                        strSqlString.Append("                  AND A.ABR_NO NOT IN ' '  " + "\n");
                        strSqlString.Append("                  AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("                  AND SUBSTR(A.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("                  AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
                        strSqlString.Append("                  AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("                  AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("                  GROUP BY A.RESV_FIELD_10 " + "\n");

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
            // ToolBar 보여주기
            udcChartFX1.ToolBar = true;

            //3D 
            udcChartFX1.Chart3D = true;

            // contion attribute 를 이용한 0 point label hidden
            ConditionalAttributes contion = udcChartFX1.ConditionalAttributes[0];
            contion.Condition.From = 0;
            contion.Condition.To = 0;
            contion.PointLabels = false ;

            // 일단 Default 값으로 소수점을 제하고
            udcChartFX1.AxisY.DataFormat.Decimals = 0;

            udcChartFX1.MultipleColors = false;
            //  udcChartFX1.Palette = "Nature.Adventure";

            switch (cboGraph.SelectedIndex)
            {
                case 0:  //부적합 별 발생 건
                    {
                        #region  //Chart No.0
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(3));
                        dt_graph = GetRotatedDataTable(ref dt_graph);
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.MultipleColors = true;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.AxisX.Title.Text = "Nonconformance type";
                        #endregion
                    }
                    break;
                case 1:  //주기 별 부적합 발생 건
                    {
                        #region Chart No.1

                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(1));
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.Chart3D = false;
                        // 선굵기 지정
                        udcChartFX1.LineWidth = 3;
                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Lines ;
                        udcChartFX1.AxisX.Title.Text = "기간";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";

                      

                        #endregion
                    }
                    break;

                case 2:  //주기 별 고객사 발생 건
                    {
                        #region Chart No.2
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(2));
                        dt_graph = GetRotatedDataTable(ref dt_graph);
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.Chart3D = false;
                        udcChartFX1.MultipleColors = true;
                        // 선굵기 지정
                        udcChartFX1.LineWidth = 3;
                        contion.PointLabels = false;
                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Lines;
                        udcChartFX1.AxisX.Title.Text = "기간";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";
                        #endregion
                    }
                    break;

                

                case 3:  //부적합종류별TAT 
                    {
                        #region Chart No.3
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(4));
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.MultipleColors = true;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.PointLabels = true;
                        udcChartFX1.AxisY.DataFormat.Decimals = 2;

                        udcChartFX1.AxisX.Title.Text = "Nonconformance type";
                        udcChartFX1.AxisY.Title.Text = "TAT (days)";
                        #endregion
                    }
                    break;

                case 4:  //고객사 별 부적합 발생 건
                    {
                        #region Chart No.4
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(5));
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.AxisX.Title.Text = "Customer";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";
                        #endregion
                    }
                    break;

                case 5:  //부적합 별 고객사 발생 건
                    {
                        #region Chart No.5
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(6));
                        dt_graph = GetRotatedDataTable(ref dt_graph);
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.AxisX.Title.Text = "Nonconformance type";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";
                        #endregion
                    }
                    break;

                case 6:  //STEP 별 부적합 발생 건
                    {
                        #region Chart No.6 
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(7));
                        udcChartFX1.DataSource = dt_graph;

                        //udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.AxisX.Title.Text = "STEP";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";
                        #endregion
                    }
                    break;

                case 7:  //불량별파레토
                    {
                        #region Chart No.7

                        udcChartFX1.RPT_1_ChartInit();
                        udcChartFX1.RPT_2_ClearData();
                        udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);


                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(8));

                        udcChartFX1.DataSource = dt_graph;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;

                        udcChartFX1.Series[1].YAxis = SoftwareFX.ChartFX.YAxis.Secondary;
                        udcChartFX1.Series[1].Gallery = SoftwareFX.ChartFX.Gallery.Lines ;
                        
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.PointLabels = true;
                        udcChartFX1.RecalcScale();
                        udcChartFX1.AxisY.DataFormat.Decimals = 0;
                        udcChartFX1.AxisY2.DataFormat.Decimals = 2;


                        #endregion
                    }
                    break;
                case 8:  //원인분류에 따른 비율 조회 (사람,설비,재료,방법,환경)
                    {
                        #region Chart No.7

                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(9));
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.AxisY.DataFormat.Decimals = 2;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max;


                        #endregion
                    }
                    break;

                case 9:  //대책분류에 따른 비율 조회 (사람,설비,재료,방법,환경)

                    {
                        #region Chart No.7

                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(10));
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.AxisY.DataFormat.Decimals = 2;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max;
                        

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

            DataTable dt = null;
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

        private void cboGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;                
            spdData.RPT_ColumnInit();
            GridColumnInit();
            SortInit();            
            //ShowChart(0);
        }
// Spread 에 조회된 목록 중에서 원하는 건을 클릭하면 PopUp 창이 떠서 Step1~7까지 내용을 보여준다
        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string strAbrNo = null;
            string strLotId = null;

            if (cboGraph.SelectedIndex == 6 || cboGraph.SelectedIndex == 8 || cboGraph.SelectedIndex == 9)  // STEP 별 부적합 발생 건
            {
                strAbrNo = spdData.ActiveSheet.Cells[e.Row, 11].Value.ToString();
                strLotId = spdData.ActiveSheet.Cells[e.Row, 16].Value.ToString();
            }

            else if (cboGraph.SelectedIndex == 1 || cboGraph.SelectedIndex == 2 || cboGraph.SelectedIndex == 4 || cboGraph.SelectedIndex == 5)
            {
                strAbrNo = spdData.ActiveSheet.Cells[e.Row, 11].Value.ToString();
                strLotId = spdData.ActiveSheet.Cells[e.Row, 15].Value.ToString();
            }

            else if (cboGraph.SelectedIndex == 0 || cboGraph.SelectedIndex == 3 || cboGraph.SelectedIndex == 7)
            {
                strAbrNo = spdData.ActiveSheet.Cells[e.Row, 10].Value.ToString();
                strLotId = spdData.ActiveSheet.Cells[e.Row, 15].Value.ToString();
            }



            //     System.Windows.Forms.Form frm = new PQC030102_P1(strAbrNo, strLotid);
            System.Windows.Forms.Form frm = new PQC030102_P1(strAbrNo, strLotId);

            frm.ShowDialog();

        }

        

       
    }
}