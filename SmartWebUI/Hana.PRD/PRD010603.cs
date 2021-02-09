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
    public partial class PRD010603 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010603<br/>
        /// 클래스요약: Ship Lot 조회<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: Ship Lot 조회<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 
        /// 2009-09-18-임종우 : Group 목록에 Run Id 추가 
        /// 2009-10-16-임종우 : SHIP SITE 컬럼 추가
        /// 2010-10-15-임종우 : NCF_CODE Column 추가 (임태성 요청)
        /// 2010-11-24-임종우 : 주차 코드 추가 (김권수 요청)
        /// 2011-01-20-임종우 : 자사 Ship 된 Lot 조회 가능 하도록 수정 (임태성 요청)
        /// 2011-03-07-임종우 : FromFactory / ToTest 추가 요청 (김현경 요청)
        /// 2011-05-31-임종우 : Test Site 조회 조건 추가 요청 (김영규 요청)
        /// 2011-06-07-임종우 : 전제품 Wafer Qty 수량 표시 되도록 수정 (김동인 요청)
        /// 2012-02-24-김민우 : RETURN 여부 표시
        /// 2012-09-06-임종우 : ASSY 조회시 삼성 RUN MERGE 여부 표시 되도록 수정 (김성업 요청)
        /// 2013-01-22-임종우 : TAT 기준 - 모 Lot 생성 기준과 자신의 Lot 생성 기준으로 선택하여 조회 가능하도록 수정 (박종호 요청)
        /// 2013-10-17-김민우 : LOT TYPE ALL, P%, E% 구분으로변경
        /// 2014-08-05-장한별 : SZ010추가
        /// 2014-11-28-임종우 : ASSY_TAT(ISSUE ~ SHIP) 추가, 시간기준 조회 기능 추가 (조형진 요청)
        /// 2015-05-07-임종우 : NCH CODE 추가 (박형순 요청)
        /// 2017-11-07-임종우 : SO NO 추가 (백성호D 요청)
        /// 2020-01-30-김미경 : FGS Factory 조회 오류 수정 (이동욱 D) 
        ///  </summary>
        /// 
        public PRD010603()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();           
            GridColumnInit();            
        }

        #region 유효성 검사 및 초기화
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
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
            if (cdvFactory.txtValue == "HMKB1")
            {
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Bumping Type", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation Flow", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Layer classification", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Final Bump", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Sub. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Size", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Product", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 13, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Run Id", 0, 14, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Invoice No", 0, 15, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Ship Site", 0, 16, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Lot Id", 0, 17, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("SHIP", 0, 18, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Daily standard", 0, 19, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type", 0, 20, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("In Qty", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Ship Qty", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Yield", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("TAT", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);

                if (ckbWafer.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Wafer Qty", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Wafer Qty", 0, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                }

                spdData.RPT_AddBasicColumn("Loss Qty", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("C/V", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("C/V 100", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("C/V 991", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("BCV", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("ECV", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HBCV", 0, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HDCV", 0, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HECV", 0, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HQCV", 0, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HSCV", 0, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Bonus", 0, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("NCF Code", 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Work Week", 0, 39, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                if (ckbSite.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("FromFactory", 0, 40, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ToTest", 0, 41, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RETURN", 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("RETURN", 0, 40, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                }

                // 2012-09-06-임종우 : 삼성 Run Merge 표시 되도록 조건 추가 (김성업 요청)
                if (ckbSecMerge.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("SEC_Run_Merge", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                }

                spdData.RPT_ColumnConfigFromTable_New(btnSort);
                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }
            else
            {
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Run Id", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Invoice No", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Ship Site", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Lot Id", 0, 14, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("SHIP", 0, 15, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Daily standard", 0, 16, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type", 0, 17, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("In Qty", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Ship Qty", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Yield", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("TAT", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);

                if (ckbWafer.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Wafer Qty", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Wafer Qty", 0, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                }

                spdData.RPT_AddBasicColumn("Loss Qty", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("C/V", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("C/V 100", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("C/V 991", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("BCV", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("ECV", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HBCV", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HDCV", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HECV", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HQCV", 0, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HSCV", 0, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Bonus", 0, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("NCF Code", 0, 35, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("NCH Code", 0, 36, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Work Week", 0, 37, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                if (ckbSite.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("FromFactory", 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ToTest", 0, 39, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RETURN", 0, 40, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("RETURN", 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                }

                // 2012-09-06-임종우 : 삼성 Run Merge 표시 되도록 조건 추가 (김성업 요청)
                if (ckbSecMerge.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("SEC_Run_Merge", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                }

                spdData.RPT_AddBasicColumn("SO NO", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                spdData.RPT_ColumnConfigFromTable_New(btnSort);
                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            if (cdvFactory.txtValue == "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "A.MAT_GRP_1", "B.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Bumping Type", "A.MAT_GRP_2", "B.MAT_GRP_2", "A.MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation Flow", "A.MAT_GRP_3", "B.MAT_GRP_3", "A.MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "A.MAT_GRP_4", "B.MAT_GRP_4", "A.MAT_GRP_4", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Type", "A.MAT_GRP_5", "B.MAT_GRP_5", "A.MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL Plating", "A.MAT_GRP_6", "B.MAT_GRP_6", "A.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Final Bump", "A.MAT_GRP_7", "B.MAT_GRP_7", "A.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Sub. Material", "A.MAT_GRP_8", "B.MAT_GRP_8", "A.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Size", "A.MAT_CMF_14", "B.MAT_CMF_14", "A.MAT_CMF_14", false); // \"SIZE\"
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Thickness", "A.MAT_CMF_2", "B.MAT_CMF_2", "A.MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Flat Type", "A.MAT_CMF_3", "B.MAT_CMF_3", "A.MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Wafer Orientation", "A.MAT_CMF_4", "B.MAT_CMF_4", "A.MAT_CMF_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.MAT_ID", "B.MAT_ID", "A.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "A.CUSTOMER_MAT_ID", "B.CUSTOMER_MAT_ID", "A.CUSTOMER_MAT_ID", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "A.MAT_CMF_7", "B.MAT_CMF_7", "A.MAT_CMF_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Run Id", "A.LOT_CMF_4", "A.LOT_CMF_4", "A.LOT_CMF_4", false);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "A.MAT_GRP_1", "B.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "A.MAT_GRP_2", "B.MAT_GRP_2", "A.MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "A.MAT_GRP_3", "B.MAT_GRP_3", "A.MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "A.MAT_GRP_4", "B.MAT_GRP_4", "A.MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "A.MAT_GRP_5", "B.MAT_GRP_5", "A.MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "A.MAT_GRP_6", "B.MAT_GRP_6", "A.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "A.MAT_GRP_7", "B.MAT_GRP_7", "A.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "A.MAT_GRP_8", "B.MAT_GRP_8", "A.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "A.MAT_CMF_10", "B.MAT_CMF_10", "A.MAT_CMF_10", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.MAT_ID", "B.MAT_ID", "A.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "A.MAT_CMF_7", "B.MAT_CMF_7", "A.MAT_CMF_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Run Id", "A.LOT_CMF_4", "A.LOT_CMF_4", "A.LOT_CMF_4", false);
            }
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

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3  = null;

            string sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
            string sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();
            string stime = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            if (ckbTime.Checked == true)
            {
                stime = "24";
            }
            else
            {
                stime = "1";
            }

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
            strSqlString.Append("     , A.INVOICE_NO" + "\n");

            // SHIP SITE 표시 추가, FGS IML 을 위해 추가함 (2009.10.16 임종우)
            if (cdvFactory.txtValue == "FGS")
            {
                strSqlString.Append("     , E.DEST_SITE " + "\n");
            }
            else
            {
                strSqlString.Append("     , A.LOT_CMF_11 " + "\n");
            }

            strSqlString.Append("     , A.LOT_ID" + "\n");
            strSqlString.Append("     , A.TRAN_TIME             " + "\n");
            strSqlString.Append("     , A.WORK_DATE" + "\n");
            strSqlString.Append("     , A.LOT_CMF_5" + "\n");
            strSqlString.Append("     , SUM(NVL(A.QTY_1,0)) +  SUM(NVL(LOSS_QTY,0)) - SUM(NVL(D.TOTAL_BONUS_QTY,0)) AS IN_QTY " + "\n");
            strSqlString.Append("     , NVL(SUM(A.QTY_1),0) AS SHIP_QTY  " + "\n");
            strSqlString.Append("     , DECODE( NVL(SUM(A.QTY_1), 0), 0, 0, ROUND((SUM(A.QTY_1)/   (SUM(NVL(A.QTY_1,0)) + SUM(NVL(LOSS_QTY,0)) - SUM(NVL(D.TOTAL_BONUS_QTY,0))))*100,3)) AS YIELD" + "\n");

            // 2013-01-22-임종우 : TAT 기준 - 모 Lot 생성 기준과 자신의 Lot 생성 기준으로 선택하여 조회 가능하도록 수정 (박종호 요청)
            //if (cdvTAT.Text == "Receive standard")
            if (cdvTAT.SelectedIndex == 0)
            {
                strSqlString.Append("     , NVL(ROUND(SUM(TO_DATE(A.TRAN_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS')) * " + stime + ",2), 0) AS TAT" + "\n");
            }
            //else if (cdvTAT.Text == "Create 기준")
            else if (cdvTAT.SelectedIndex == 1)
            {
                strSqlString.Append("     , NVL(ROUND(SUM(TO_DATE(A.TRAN_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(A.CREATE_TIME, 'YYYYMMDDHH24MISS')) * " + stime + ",2), 0) AS TAT" + "\n");
            }
            else // 2014-11-28-임종우 : TAT 기준 - Issue 기준 추가 (조형진 요청)
            {
                strSqlString.Append("     , NVL(ROUND(SUM(CASE WHEN A.RESV_FIELD_1 <> ' ' THEN TO_DATE(A.TRAN_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') END) * " + stime + ",2), 0) AS TAT" + "\n");
            }

            // 2011-06-07-임종우 : 전제품 Wafer Qty 수량 표시 되도록 수정 (김동인 요청)
            if (ckbWafer.Checked == true)
            {
                strSqlString.Append("     , SUM(DECODE(A.MAT_GRP_1, 'SE', NVL(A.WAFER_QTY,0), NVL(A.QTY_2,0))) AS WAFER_QTY " + "\n");
            }
            else
            {
                strSqlString.Append("     , 0 AS WAFER_QTY " + "\n");
            }

            strSqlString.Append("     , SUM(NVL(C.LOSS_QTY,0)) " + "\n");
            strSqlString.Append("     , SUM(NVL(C.CV100,0))" + "\n");
            strSqlString.Append("     , SUM(NVL(C.CV200,0))" + "\n");
            strSqlString.Append("     , SUM(NVL(C.CV991,0))" + "\n");
            strSqlString.Append("     , SUM(NVL(C.BCV,0))" + "\n");
            strSqlString.Append("     , SUM(NVL(C.ECV,0))" + "\n");
            strSqlString.Append("     , SUM(NVL(C.HBCV,0))" + "\n");
            strSqlString.Append("     , SUM(NVL(C.HDCV,0))" + "\n");
            strSqlString.Append("     , SUM(NVL(C.HECV,0))" + "\n");
            strSqlString.Append("     , SUM(NVL(C.HQCV,0))" + "\n");
            strSqlString.Append("     , SUM(NVL(C.HSCV,0))" + "\n");
            strSqlString.Append("     , SUM(NVL(D.TOTAL_BONUS_QTY,0)) AS BONUS" + "\n");
            strSqlString.Append("     , A.NCF" + "\n");
            strSqlString.Append("     , A.NCH" + "\n");

            // 2010-11-24-임종우 : 주차 코드 추가 (김권수 요청)
            strSqlString.Append("     , A.LOT_CMF_10" + "\n");

            // 2011-03-07-임종우 : FromFactory / ToTest 추가 요청 (김현경 요청)
            if (ckbSite.Checked == true)
            {                
                strSqlString.Append("     , F.FACTORY" + "\n");
                strSqlString.Append("     , F.TEST_SITE" + "\n");
            }

            // 2012-02-24-김민우 : RETURN 표시 여부
            strSqlString.Append("     , DECODE(A.CREATE_CODE,'PROD', '','Y') AS RETN" + "\n");

            // 2012-09-06-임종우 : ASSY 조회시 삼성 RUN MERGE 여부 표시 되도록 수정 (김성업 요청)
            if (ckbSecMerge.Checked == true)
            {
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT ATTR_VALUE" + "\n");
                strSqlString.Append("          FROM MATRNAMSTS@RPTTOMES" + "\n");
                strSqlString.Append("         WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");       // 'HMKB1'은 있음
                strSqlString.Append("           AND ATTR_TYPE = 'LOT_SEC_INFO'" + "\n");                // b1 공장에 없음 ?????
                strSqlString.Append("           AND ATTR_NAME = 'NCBCODE'" + "\n");                     // b1 공장에 없음 ?????
                strSqlString.Append("           AND ATTR_KEY = A.LOT_ID" + "\n");
                strSqlString.Append("       ) ATTR_VALUE " + "\n");
            }

            strSqlString.Append("     , A.LOT_CMF_3" + "\n");
            strSqlString.Append("  FROM (" + "\n");

            //strSqlString.Append("        SELECT B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, B.MAT_CMF_10, A.MAT_ID, B.MAT_CMF_7, A.LOT_CMF_4 " + "\n");

            if (cdvFactory.txtValue == "HMKB1")
            {
                strSqlString.Append("        SELECT B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, B.MAT_CMF_14, B.MAT_CMF_2, B.MAT_CMF_3, B.MAT_CMF_4, A.MAT_ID, B.CUSTOMER_MAT_ID, A.LOT_CMF_4 " + "\n");                
            }
            else
            {
                strSqlString.Append("        SELECT B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, B.MAT_CMF_10, A.MAT_ID, B.MAT_CMF_7, A.LOT_CMF_4 " + "\n");
            }

            strSqlString.Append("             , A.FACTORY" + "\n");
            strSqlString.Append("             , A.INVOICE_NO" + "\n");
            strSqlString.Append("             , A.LOT_CMF_10 " + "\n");
            strSqlString.Append("             , A.LOT_CMF_11 " + "\n");
            strSqlString.Append("             , A.LOT_CMF_14 " + "\n");
            strSqlString.Append("             , A.LOT_ID" + "\n");
            strSqlString.Append("             , NVL(SUM(A.QTY_1),0) QTY_1" + "\n");
            strSqlString.Append("             , NVL(SUM(A.QTY_2),0) QTY_2" + "\n");
            strSqlString.Append("             , (SELECT COUNT(A.LOT_ID) FROM CWIPLOTWFR WHERE LOT_ID = A.LOT_ID) AS WAFER_QTY" + "\n");             // LOT_ID 존재 확인 필요 ?????
            strSqlString.Append("             , GET_WORK_DATE(A.TRAN_TIME,'D') AS WORK_DATE" + "\n");            
            strSqlString.Append("             , A.TRAN_TIME     " + "\n");
            strSqlString.Append("             , A.LOT_CMF_5     " + "\n");            
            strSqlString.Append("             , NVL(SUM(A.IN_QTY),0) IN_QTY" + "\n");
            strSqlString.Append("             , MESMGR.F_GET_MCP_NCF_SINGLE@RPTTOMES(A.LOT_ID) AS NCF" + "\n");                                     // 동작 하나 ?????         
            strSqlString.Append("             , MESMGR.F_GET_SEC_NCX@RPTTOMES(A.FACTORY, 'NCHCODE', A.LOT_ID, 'V') AS NCH" + "\n");                 // 동작 하나 ?????   
            strSqlString.Append("             , A.CREATE_CODE" + "\n");
            strSqlString.Append("             , A.CREATE_TIME" + "\n");
            strSqlString.Append("             , A.RESV_FIELD_1" + "\n");
            strSqlString.Append("             , A.LOT_CMF_3" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT FROM_FACTORY AS FACTORY" + "\n");
            strSqlString.Append("                     , INVOICE_NO" + "\n");
            strSqlString.Append("                     , MAT_ID" + "\n");
            strSqlString.Append("                     , LOT_ID" + "\n");
            strSqlString.Append("                     , SHIP_QTY_1 AS QTY_1" + "\n");
            strSqlString.Append("                     , SHIP_QTY_2 AS QTY_2 " + "\n");
            strSqlString.Append("                     , TRAN_TIME   " + "\n");
            strSqlString.Append("                     , LOT_CMF_3   " + "\n");
            strSqlString.Append("                     , LOT_CMF_4   " + "\n");
            strSqlString.Append("                     , LOT_CMF_5   " + "\n");
            strSqlString.Append("                     , LOT_CMF_10   " + "\n");
            strSqlString.Append("                     , LOT_CMF_11   " + "\n");
            strSqlString.Append("                     , LOT_CMF_14   " + "\n");
            
            if(cdvFactory.txtValue == "FGS")
            {
                strSqlString.Append("                     , FGS_IN_QTY AS IN_QTY  " + "\n");
            }
            else
            {
                strSqlString.Append("                     , IN_QTY " + "\n");
            }
            
            strSqlString.Append("                     , CREATE_CODE " + "\n");
            strSqlString.Append("                     , CREATE_TIME " + "\n");
            strSqlString.Append("                     , RESV_FIELD_1 " + "\n");
            strSqlString.Append("                  FROM VWIPSHPLOT   " + "\n");
            strSqlString.Append("                 WHERE 1=1   " + "\n");
            strSqlString.Append("                   AND FROM_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND OWNER_CODE = 'PROD' " + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory || cdvFactory.txtValue == "HMKB1")
            {
                strSqlString.Append("                   AND TO_FACTORY IN ('CUSTOMER', 'FGS') " + "\n");
            }
            else if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                //if (cboShipFactory.Text == "ALL")
                if (cboShipFactory.SelectedIndex == 0)
                {
                    strSqlString.Append("                   AND TO_FACTORY IN ('CUSTOMER', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                }
                //else if (cboShipFactory.Text == "Customer standard")
                else if (cboShipFactory.SelectedIndex == 1)
                {
                    strSqlString.Append("                   AND TO_FACTORY = 'CUSTOMER' " + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND TO_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                }
            }

            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND FROM_OPER IN ('AZ010','AZ009','TZ010','EZ010','SZ010','F0000', 'BZ010')" + "\n");//★               // b1 추가      
            strSqlString.AppendFormat("                   AND TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            /*
            if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
            {
                strSqlString.Append("                   AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            }
            */
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }
            if (cdvTestSite.txtValue != "" && cdvTestSite.txtValue != "ALL")
            {
                strSqlString.Append("                   AND LOT_CMF_8 " + cdvTestSite.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append("             ) A " + "\n");
            strSqlString.Append("             , MWIPMATDEF@RPTTOMES B " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("           AND B.MAT_VER(+) = 1 " + "\n");

            
            #region 상세 조회에 따른 SQL문 생성
            if (cdvFactory.txtValue == "HMKB1")
            {
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
            }
            else
            {
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

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
            }
            #endregion

            if (cdvFactory.txtValue == "HMKB1")
            {
                strSqlString.Append("         GROUP BY B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, B.MAT_CMF_14, B.MAT_CMF_2, B.MAT_CMF_3, B.MAT_CMF_4, B.CUSTOMER_MAT_ID, A.MAT_ID" + "\n");                
            }
            else
            {
                strSqlString.Append("         GROUP BY B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, B.MAT_CMF_10, A.MAT_ID, B.MAT_CMF_7" + "\n");
            }

            strSqlString.Append("                , A.FACTORY, A.INVOICE_NO, A.LOT_CMF_10, A.LOT_CMF_11, A.LOT_CMF_14, A.LOT_ID, A.TRAN_TIME,  A.LOT_CMF_4, A.LOT_CMF_5, A.CREATE_CODE, A.CREATE_TIME, A.RESV_FIELD_1, A.LOT_CMF_3" + "\n");            
            strSqlString.Append("       ) A" + "\n");            
            strSqlString.Append("       , ( " + "\n");
            strSqlString.Append("          SELECT FACTORY, MAT_VER, LOT_ID, SUM(LOSS_QTY) AS LOSS_QTY, SUM(CV100) CV100, SUM(CV200) CV200, SUM(CV991) CV991 , SUM(BCV) BCV, SUM(ECV) ECV, SUM(HBCV) HBCV, SUM(HDCV) HDCV, SUM(HECV) HECV, SUM(HQCV) HQCV, SUM(HSCV) HSCV" + "\n");
            strSqlString.Append("            FROM ( " + "\n");
            strSqlString.Append("                  SELECT FACTORY, MAT_ID, MAT_VER, LOT_ID   " + "\n");
            strSqlString.Append("                       , SUM(DECODE(LOSS_CODE, '100', LOSS_QTY, 0)) AS CV100  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(LOSS_CODE, '200', LOSS_QTY, 0)) AS CV200  " + "\n");                           // 모두 확인 필요 ?????
            strSqlString.Append("                       , SUM(DECODE(LOSS_CODE, '991', LOSS_QTY, 0)) AS CV991  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(LOSS_CODE, '992-BCV', LOSS_QTY, 0)) AS BCV  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(LOSS_CODE, '992-ECV', LOSS_QTY, 0)) AS ECV  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(LOSS_CODE, '992-HBCV', LOSS_QTY, 0)) AS HBCV  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(LOSS_CODE, '992-HDCV', LOSS_QTY, 0)) AS HDCV  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(LOSS_CODE, '992-HECV', LOSS_QTY, 0)) AS HECV  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(LOSS_CODE, '992-HQCV', LOSS_QTY, 0)) AS HQCV  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(LOSS_CODE, '992-HSCV', LOSS_QTY, 0)) AS HSCV  " + "\n");
            strSqlString.Append("                       , SUM(LOSS_QTY) AS LOSS_QTY  " + "\n");
            strSqlString.Append("                    FROM RWIPLOTLSM  " + "\n");
            strSqlString.Append("                   WHERE HIST_DEL_FLAG= ' '  " + "\n");
            strSqlString.Append("                     AND LOT_ID IN ( " + "\n");
            strSqlString.Append("                                    SELECT LOT_ID" + "\n");
            strSqlString.Append("                                      FROM VWIPSHPLOT   " + "\n");
            strSqlString.Append("                                     WHERE 1=1   " + "\n");
            strSqlString.Append("                                       AND FROM_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                                       AND OWNER_CODE = 'PROD' " + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                                       AND TO_FACTORY IN ('CUSTOMER', 'FGS') " + "\n");
            }
            else if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                //if (cboShipFactory.Text == "ALL")
                if (cboShipFactory.SelectedIndex == 0)
                {
                    strSqlString.Append("                                       AND TO_FACTORY IN ('CUSTOMER', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                }
                //else if (cboShipFactory.Text == "Customer standard")
                else if (cboShipFactory.SelectedIndex == 1)
                {
                    strSqlString.Append("                                       AND TO_FACTORY = 'CUSTOMER' " + "\n");
                }
                else
                {
                    strSqlString.Append("                                       AND TO_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                }
            }
            else
            {
                strSqlString.Append("                                       AND TO_FACTORY = 'CUSTOMER' " + "\n");
            }

            strSqlString.Append("                                       AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                       AND FROM_OPER IN ('AZ010','AZ009','TZ010','EZ010','SZ010','F0000', 'BZ010')" + "\n");//★       // B1 공장 추가
            strSqlString.AppendFormat("                                       AND TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            /*
            if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
            {
                strSqlString.Append("                                       AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            }
            */
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                                       AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                                   ) " + "\n");
            strSqlString.Append("                   GROUP BY FACTORY, MAT_ID, MAT_VER, LOT_ID " + "\n");
            strSqlString.Append("                 )  " + "\n");
            strSqlString.Append("           GROUP BY FACTORY, MAT_VER, LOT_ID" + "\n");            
            strSqlString.Append("         ) C  " + "\n");
            strSqlString.Append("         , RWIPLOTBNS D " + "\n");

            // FGS 일 경우 SHIP_SITE 가져오는 테이블 (2009.10.16 임종우)
            if (cdvFactory.txtValue == "FGS")
            {
                strSqlString.Append("         , ( " + "\n");
                strSqlString.Append("            SELECT FACTORY, SLOT_ID, INVOICE_NO, DEST_SITE " + "\n");
                strSqlString.Append("              FROM CWIPLBOXHIS@RPTTOMES " + "\n");
                strSqlString.Append("             GROUP BY FACTORY, SLOT_ID, INVOICE_NO, DEST_SITE " + "\n");
                strSqlString.Append("           ) E " + "\n");
            }

            // 2011-03-07-임종우 : FromFactory / ToTest 추가 요청 (김현경 요청)
            if (ckbSite.Checked == true)
            {
                strSqlString.Append("         , CWIPHSOHIS@RPTTOMES F " + "\n");                                            // 확인 필요 ?????
            }

            strSqlString.Append("     WHERE 1=1  " + "\n");
            strSqlString.Append("       AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");            
            strSqlString.Append("       AND A.FACTORY = C.FACTORY(+)  " + "\n");
            strSqlString.Append("       AND A.FACTORY = D.FACTORY(+) " + "\n");

            if (cdvFactory.txtValue == "FGS")
            {
                strSqlString.Append("       AND A.FACTORY = E.FACTORY(+) " + "\n");
                strSqlString.Append("       AND A.INVOICE_NO = E.INVOICE_NO(+) " + "\n");
                strSqlString.Append("       AND A.LOT_ID = E.SLOT_ID(+)  " + "\n");
            }
             
            // 2011-03-07-임종우 : FromFactory / ToTest 추가 요청 (김현경 요청)
            if (ckbSite.Checked == true)
            {
                strSqlString.Append("       AND A.LOT_CMF_4 = F.RUN_NO(+) " + "\n");
            }

            strSqlString.Append("       AND A.LOT_ID = C.LOT_ID(+) " + "\n");
            strSqlString.Append("       AND A.LOT_ID = D.LOT_ID(+) " + "\n");            
            strSqlString.Append("       AND C.MAT_VER(+)=1   " + "\n");
            strSqlString.Append("       AND D.MAT_VER(+)=1  " + "\n");
            
            if(txtSearchProduct.Text != "%" && txtSearchProduct.Text != "")
            {
                strSqlString.Append("       AND A.MAT_ID LIKE '" + txtSearchProduct.Text.Trim() + "'  " + "\n");
            }

            if (cdvFactory.txtValue == "FGS")
            {
                strSqlString.AppendFormat(" GROUP BY {0}, A.MAT_ID " + "\n", QueryCond1);
                strSqlString.Append("        , A.INVOICE_NO, E.DEST_SITE, A.LOT_ID, A.TRAN_TIME, A.WORK_DATE, A.LOT_CMF_5, A.NCF, A.NCH, A.LOT_CMF_10, A.CREATE_CODE, A.LOT_CMF_3  " + "\n");
                strSqlString.AppendFormat(" ORDER BY {0}, A.MAT_ID " + "\n", QueryCond1);
                strSqlString.Append("        , A.INVOICE_NO, E.DEST_SITE, A.LOT_ID, A.TRAN_TIME, A.WORK_DATE, A.LOT_CMF_5, A.NCF, A.NCH, A.LOT_CMF_10  " + "\n");

            }
            else
            {
                // 2011-03-07-임종우 : FromFactory / ToTest 추가 요청 (김현경 요청)
                if (ckbSite.Checked == true)
                {
                    if (cdvFactory.txtValue == "HMKB1")
                    {
                        strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
                        strSqlString.Append("        , A.INVOICE_NO, A.LOT_CMF_11, A.LOT_ID, A.TRAN_TIME, A.WORK_DATE, A.LOT_CMF_5, A.NCF, A.NCH, A.LOT_CMF_10, F.FACTORY, F.TEST_SITE,A.CREATE_CODE  " + "\n");
                        strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond1);
                        strSqlString.Append("        , A.INVOICE_NO, A.LOT_CMF_11, A.LOT_ID, A.TRAN_TIME, A.WORK_DATE, A.LOT_CMF_5, A.NCF, A.NCH, A.LOT_CMF_10, F.FACTORY, F.TEST_SITE  " + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat(" GROUP BY {0}, A.MAT_ID " + "\n", QueryCond1);
                        strSqlString.Append("        , A.INVOICE_NO, A.LOT_CMF_11, A.LOT_ID, A.TRAN_TIME, A.WORK_DATE, A.LOT_CMF_5, A.NCF, A.NCH, A.LOT_CMF_10, F.FACTORY, F.TEST_SITE,A.CREATE_CODE, A.LOT_CMF_3  " + "\n");
                        strSqlString.AppendFormat(" ORDER BY {0}, A.MAT_ID " + "\n", QueryCond1);
                        strSqlString.Append("        , A.INVOICE_NO, A.LOT_CMF_11, A.LOT_ID, A.TRAN_TIME, A.WORK_DATE, A.LOT_CMF_5, A.NCF, A.NCH, A.LOT_CMF_10, F.FACTORY, F.TEST_SITE  " + "\n");
                    }
                }
                else
                {
                    if (cdvFactory.txtValue == "HMKB1")
                    {
                        strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
                        strSqlString.Append("        , A.INVOICE_NO, A.LOT_CMF_11, A.LOT_ID, A.TRAN_TIME, A.WORK_DATE, A.LOT_CMF_5, A.NCF, A.NCH, A.LOT_CMF_10,A.CREATE_CODE  " + "\n");
                        strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond1);
                        strSqlString.Append("        , A.INVOICE_NO, A.LOT_CMF_11, A.LOT_ID, A.TRAN_TIME, A.WORK_DATE, A.LOT_CMF_5, A.NCF, A.NCH, A.LOT_CMF_10  " + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat(" GROUP BY {0}, A.MAT_ID " + "\n", QueryCond1);
                        strSqlString.Append("        , A.INVOICE_NO, A.LOT_CMF_11, A.LOT_ID, A.TRAN_TIME, A.WORK_DATE, A.LOT_CMF_5, A.NCF, A.NCH, A.LOT_CMF_10,A.CREATE_CODE, A.LOT_CMF_3  " + "\n");
                        strSqlString.AppendFormat(" ORDER BY {0}, A.MAT_ID " + "\n", QueryCond1);
                        strSqlString.Append("        , A.INVOICE_NO, A.LOT_CMF_11, A.LOT_ID, A.TRAN_TIME, A.WORK_DATE, A.LOT_CMF_5, A.NCF, A.NCH, A.LOT_CMF_10  " + "\n");
                    }
                }
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion


        #region EVENT 처리
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

                //spdData.DataSource = dt;

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                int nGroupCount = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    //1.Griid 합계 표시
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 21, null, null, btnSort);           // In Qty


                    //3. Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 21, 0, 1, true, Align.Center, VerticalAlign.Center);     // In Qty
                }
                else
                {
                    //1.Griid 합계 표시
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 18, null, null, btnSort);           // In Qty


                    //3. Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 18, 0, 1, true, Align.Center, VerticalAlign.Center);     // In Qty
                }

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    spdData.ActiveSheet.Columns[11].AllowAutoSort = true;        // Product : 뭐로 변경 할지 모르겠음.
                    spdData.ActiveSheet.Columns[14].AllowAutoSort = true;       // Invoice No
                    spdData.ActiveSheet.Columns[16].AllowAutoSort = true;       // Lot Id
                    spdData.ActiveSheet.Columns[17].AllowAutoSort = true;       // SHIP

                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 23, nGroupCount, true);  // Yield
                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 24, nGroupCount, true);  // TAT                    
                }
                else
                {
                    spdData.ActiveSheet.Columns[9].AllowAutoSort = true;        // Product
                    spdData.ActiveSheet.Columns[12].AllowAutoSort = true;       // Invoice No
                    spdData.ActiveSheet.Columns[14].AllowAutoSort = true;       // Lot Id
                    spdData.ActiveSheet.Columns[15].AllowAutoSort = true;       // SHIP

                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 20, nGroupCount, true);   // Yield
                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 21, nGroupCount, true);   // TAT                    
                }

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
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvFactory.txtValue == "HMKB1")
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
            //cdvLotType.sFactory = cdvFactory.txtValue;

            // 2011-01-20-임종우 : HMKA1 선택시 Ship Factory 검색 조건 활성화 시킴.
            // 2012-09-06-임종우 : HMKA1 선택시 삼성 Run Merge 조건 활성화 시킴.
            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                cboShipFactory.Enabled = true;
                ckbSecMerge.Enabled = true;
            }
            else
            {
                cboShipFactory.SelectedIndex = 0;
                cboShipFactory.Enabled = false;
                ckbSecMerge.Enabled = false;
                ckbSecMerge.Checked = false;
            }

            SortInit(); //Add. 20150602
         }
         #endregion
       
        // Loss 수량 클릭시 Poup 창 띄우기
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string strLotId = "";
            Color BackColor;

            try
            {
                if (spdData.ActiveSheet.Columns[e.Column].Label == "Loss Qty") //if (e.Column == 23)
                {
                    if (cdvFactory.txtValue == "HMKB1")
                        BackColor = spdData.ActiveSheet.Cells[1, 26].BackColor;                 // Loss Qty
                    else
                        BackColor = spdData.ActiveSheet.Cells[1, 23].BackColor;                 // Loss Qty
                    

                    // subTotal 을 제외한 나머지 부분 클릭시 실행되도록...
                    if (spdData.ActiveSheet.Cells[e.Row, e.Column].BackColor == BackColor) //spdData.ActiveSheet.Cells[e.Row, 23].BackColor == BackColor
                    {
                        if (cdvFactory.txtValue == "HMKB1")
                            strLotId = spdData.ActiveSheet.Cells[e.Row, 17].Value.ToString();       // Lot Id
                        else
                            strLotId = spdData.ActiveSheet.Cells[e.Row, 14].Value.ToString();       // Lot Id
                        

                        //string strLotId = spdData.ActiveSheet.Cells[e.Row, 14].Value.ToString();

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup(strLotId));

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            System.Windows.Forms.Form frm = new PRD010603_P1(strLotId, dt);
                            frm.ShowDialog();
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("PRD010603.spdData_CellClick()" + "\r\n" + ex.Message);
            }
        }

        // Lot별 Loss 상세 보기(POPUP 창)
        private string MakeSqlStringForPopup(string strLotId)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT LSM.LOT_ID, " + "\n");
            strSqlString.Append("       LSM.LOSS_CODE, " + "\n");
            strSqlString.Append("       DEF.DATA_1, " + "\n");
            strSqlString.Append("       SUM(LSM.LOSS_QTY) AS LOSS_QTY " + "\n");
            strSqlString.Append("  FROM RWIPLOTLSM LSM, " + "\n");
            strSqlString.Append("       MGCMTBLDAT@RPTTOMES DEF " + "\n");
            strSqlString.Append(" WHERE LSM.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND LSM.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND DEF.TABLE_NAME = 'LOSS_CODE' " + "\n");
            strSqlString.Append("   AND LSM.LOT_ID = '" + strLotId + "' " + "\n");
            strSqlString.Append("   AND LSM.FACTORY = DEF.FACTORY " + "\n");
            strSqlString.Append("   AND LSM.LOSS_CODE = DEF.KEY_1 " + "\n");
            strSqlString.Append("GROUP BY LSM.LOT_ID,LSM.LOSS_CODE,DEF.DATA_1 " + "\n");
            strSqlString.Append("ORDER BY LOSS_QTY DESC " + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

     }
}
