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
    public partial class PRD010405 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010405<br/>
        /// 클래스요약: 일보<br/>
        /// 작  성  자: 하나마이크론 김준용<br/>
        /// 최초작성일: 2009-02-14<br/>
        /// 상세  설명: LOT 별 작업 이력<br/>
        /// 공정을 기준으로 설비,LOT,제품별 작업 이력을 조회 함<br />
        /// 특정 공정의 설비,LOT,제품의 생산 이력을 조회하여 일일 보고서 양식을 만들기 위해서 주로 사용 됨(Excel Macro 이용)<br />
        /// RWIPLOTHIS 테이블에서 TRAN CODE 가 END,SHIP 인 이력을 가져와서 OLD_OPER 를 기준으로 OPER_IN_QTY_1 과 QTY_1 을 사용함<br />
        /// MES Client LOT END 화면에서 사용자가 입력한 Epoxy,Lead Frame, Gold Wire , EMC 의 정보를 가져와서 보여 주어야 함<br />
        /// RWIPLOTHIS 테이블 TRAN_COMMENT 항목에 각각의 정보가 콤마(,)를 기준으로 입력되어 있으므로 TRAN_COMMENT 를 PARSING 하는 Method 를 추가함<br />
        /// <br />
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2010-11-23-임종우 : partial ship Logic 추가 함.
        /// 2011-10-27-임종우 : 속도 개선 위해 쿼리 튜팅 함.
        /// 2012-05-07-임종우 : Wafer 수량 표시 (김문한 요청)
        /// 2012-07-16-김민우 : 설비모델, UPEH 추가 (김상천C 요청)
        /// 2013-03-15-임종우 : START 작업자 표시 (김경환직장 요청)
        /// 2013-03-15-임종우 : ASSY SITE 표시 (황혜리 요청)
        /// 2013-03-21-임종우 : CV 수량 표시
        /// 2013-04-25-임종우 : SHIP SITE 표시 (김문한 요청)
        /// 2013-08-23-임종우 : Rework 데이터 추가 요청 (박상현 요청)
        /// 2014-01-16-임종우 : 인쇄흐림 -> Damage Ball 명칭 변경 (김광일 요청)
        /// 2014-07-24-장한별 : 구분 체크박스 체크 시 생산 ,PCB 정보를 추가
        /// 2016-01-26-임종우 : HMKB1 검색의 경우 IN/OUT QTY = QTY2 정보로, W/F 수량 = QTY1 정보로 변경 (김권수D 요청)
        /// 2017-11-07-임종우 : SO NO 추가 (백성호D 요청)
        /// 2019-05-16-임종우 : Bump 조회시에도 공정코드 표시 되도록 수정 (백성호대리 요청)
        /// </summary>
        public PRD010405()
        {
            InitializeComponent();
            //OptionInit(); // 검색 조건 초기화
            cdvFromToDate.AutoBinding();
            SortInit(); // Grouping 조건 초기화 
            GridColumnInit(); // Sheet 초기화
        }

        #region " Function Definition "

        /// <summary>
        /// 0. 검색 옵션 초기화
        /// </summary>
        //private void OptionInit()
        //{
        //    cdvStartDate.Text = DateTime.Now.AddDays(-1).ToString(); // 검색 시작일 초기화
        //    cdvEndDate.Text = DateTime.Now.ToString(); // 검색 종료일 초기화
        //    //cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory; // Factory 검색 초기화
        //    //cdvLotType.sFactory = cdvFactory.txtValue; // LOT TYPE 검색 Factory 셋팅
        //    //cdvOper.sFactory = cdvFactory.txtValue; // LOT TYPE 검색 Factory 셋팅
        //    //txtSearchProduct.Text = "SEK4T1G044QQ-EIX2L6";
        //}

        /// <summary> 
        /// 1. 유효성 검사
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (String.IsNullOrEmpty(cdvFactory.Text.Trim()))
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
                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    spdData.RPT_ColumnInit();
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Sub. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Size", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Cust Device", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Oper", 0, 13, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);

                    spdData.RPT_AddBasicColumn("Product", 0, 14, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Lot ID", 0, 15, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RAS", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("IN QTY", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("END QTY", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Yield", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("IN TIME", 0, 21, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("END TIME", 0, 22, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("START operator", 0, 23, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("END worker", 0, 24, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Work Week", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("H/D", 0, 26, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PGM", 0, 27, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("CRR", 0, 28, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EPOXY", 0, 29, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("L/F", 0, 30, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("G/W", 0, 31, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Metal Card", 0, 32, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EMC Lot", 0, 33, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EMC Can", 0, 34, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EMC Type", 0, 35, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TRAN COMMENT", 0, 36, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 140);
                    spdData.RPT_AddBasicColumn("W/F quantity", 0, 37, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("CV", 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);

                    if (ckUPEH.Checked)
                    {
                        spdData.RPT_AddBasicColumn("MODEL", 0, 39, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("UPEH", 0, 40, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("ASSY SITE", 0, 41, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("SHIP SITE", 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("BALL", 0, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Alien substance", 0, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Damage Ball", 0, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("ASSY SITE", 0, 39, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("SHIP SITE", 0, 40, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("BALL", 0, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Alien substance", 0, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Damage Ball", 0, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }

                    spdData.RPT_ColumnConfigFromTable_New(btnSort);
                    //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
                }
                else
                {
                    spdData.RPT_ColumnInit();
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PKG", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Oper", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("Product", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Lot ID", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RAS", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("IN QTY", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("END QTY", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Yield", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("IN TIME", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("END TIME", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("START operator", 0, 20, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("END worker", 0, 21, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Work Week", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("H/D", 0, 23, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PGM", 0, 24, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("CRR", 0, 25, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EPOXY", 0, 26, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("L/F", 0, 27, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("G/W", 0, 28, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Metal Card", 0, 29, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EMC Lot", 0, 30, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EMC Can", 0, 31, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EMC Type", 0, 32, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TRAN COMMENT", 0, 33, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 140);
                    spdData.RPT_AddBasicColumn("W/F quantity", 0, 34, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("CV", 0, 35, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);

                    if (ckUPEH.Checked)
                    {
                        spdData.RPT_AddBasicColumn("MODEL", 0, 36, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("UPEH", 0, 37, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("ASSY SITE", 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("SHIP SITE", 0, 39, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("BALL", 0, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Alien substance", 0, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Damage Ball", 0, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("ASSY SITE", 0, 36, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("SHIP SITE", 0, 37, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("BALL", 0, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Alien substance", 0, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Damage Ball", 0, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }

                    spdData.RPT_AddBasicColumn("SO NO", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                    spdData.RPT_ColumnConfigFromTable_New(btnSort);
                    //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Bumping Type", "MAT.MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation Flow", "MAT.MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT.MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Type", "MAT.MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL Plating", "MAT.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Final Bump", "MAT.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Sub. Material", "MAT.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Size", "MAT.MAT_CMF_14", false); // \"SIZE\"
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Thickness", "MAT.MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Flat Type", "MAT.MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Wafer Orientation", "MAT.MAT_CMF_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Oper", "SUMMERY.OPER", true);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Oper", "SUMMERY.OPER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7", false);
            }
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            string QueryCond1 = null;
            string QueryCond2 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            // 시간 관련 셋팅
            //string strStartDate = cdvStartDate.Value.ToString("yyyyMMdd");
            //strStartDate = strStartDate + "220000";

            //string strEndDate = cdvEndDate.Value.ToString("yyyyMMdd");
            //strEndDate = strEndDate + "215959";

            string sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
            string sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();

            StringBuilder strSqlString = new StringBuilder();

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                strSqlString.Append("SELECT " + QueryCond1 + "\n");
                strSqlString.Append("     , SUMMERY.MAT_ID " + "\n");
                strSqlString.Append("     , SUMMERY.LOT_ID " + "\n");
                strSqlString.Append("     , SUMMERY.LOT_CMF_5 " + "\n");
                strSqlString.Append("     , SUMMERY.END_RES_ID " + "\n");
                strSqlString.Append("     , SUMMERY.QTY_2 + SUMMERY.LOSS_QTY AS OPER_IN_QTY_1 " + "\n");
                strSqlString.Append("     , SUMMERY.QTY_2 " + "\n");
                strSqlString.Append("     , ROUND(SUMMERY.QTY_2/(SUMMERY.QTY_2 + SUMMERY.LOSS_QTY)*100,3) AS YIELD " + "\n");
                //strSqlString.Append("     , SUMMERY.QTY_1 + SUMMERY.LOSS_QTY AS OPER_IN_QTY_1 " + "\n");
                //strSqlString.Append("     , SUMMERY.QTY_1 " + "\n");
                //strSqlString.Append("     , ROUND(SUMMERY.QTY_1/(SUMMERY.QTY_1 + SUMMERY.LOSS_QTY)*100,3) AS YIELD " + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(SUMMERY.OPER_IN_TIME,'yyyymmddhh24miss')) AS OPER_IN_TIME " + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(SUMMERY.TRAN_TIME,'yyyymmddhh24miss')) AS TRAN_TIME " + "\n");

                //2013-03-15-임종우 : START 작업자 표시 (김경환직장 요청)
                strSqlString.Append("     , DECODE(SUMMERY.TRAN_CMF_18, ' ', ' ', SUMMERY.IN_USER_DESC || '(' || SUMMERY.TRAN_CMF_18 || ')') AS IN_USER_ID " + "\n");
                strSqlString.Append("     , SUMMERY.END_USER_DESC || '(' || SUMMERY.TRAN_USER_ID || ')' AS END_USER_ID " + "\n");
                strSqlString.Append("     , SUMMERY.LOT_CMF_10 " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_CMF_2 AS HD " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_CMF_3 AS PGM " + "\n");
                strSqlString.Append("     , SUMMERY.CRR_ID " + "\n");
                strSqlString.Append("     , '' EPOXY " + "\n");
                strSqlString.Append("     , '' LF " + "\n");
                strSqlString.Append("     , '' GW " + "\n");
                strSqlString.Append("     , '' MATAL_CARD " + "\n");
                strSqlString.Append("     , '' EMC_LOT " + "\n");
                strSqlString.Append("     , '' EMC_CAN " + "\n");
                strSqlString.Append("     , '' EMC_TYPE " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_COMMENT " + "\n");
                strSqlString.Append("     , SUMMERY.QTY_1 " + "\n");
                //strSqlString.Append("     , SUMMERY.QTY_2 " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_CMF_17 AS CV " + "\n");

                // 2012-07-17-김민우 : 모델,UPEG 체크박스 체크 시 설비 MODEL, UPEH 추가
                if (ckUPEH.Checked)
                {
                    strSqlString.Append("     , RES_GRP_6" + "\n");
                    strSqlString.Append("     , (SELECT UPEH FROM CRASUPHDEF WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + " AND OPER = SUMMERY.OPER AND RES_MODEL = RES_GRP_6 AND UPEH_GRP = RES_GRP_7 AND MAT_ID = SUMMERY.MAT_ID) AS UPEH " + "\n");                    
                }

                //2013-03-15-임종우 : ASSY SITE 표시 (황혜리 요청)
                strSqlString.Append("     , SUMMERY.LOT_CMF_7 AS ASSY_SITE " + "\n");

                //2013-04-25-임종우 : SHIP SITE 표시 (김문한 요청)
                strSqlString.Append("     , SUMMERY.LOT_CMF_11 AS SHIP_SITE " + "\n");

                // 2013-08-23-임종우 : Rework 데이터 추가 요청 (박상현 요청)
                strSqlString.Append("     , SUMMERY.TRAN_CMF_7 AS REWORK_BALL " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_CMF_8 AS REWORK_CLEAN " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_CMF_9 AS REWORK_PRINT " + "\n");
                strSqlString.Append("  FROM (  " + "\n");
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , LOT_ID " + "\n");
                strSqlString.Append("             , LOT_CMF_5 " + "\n");
                strSqlString.Append("             , LOT_CMF_10 " + "\n");
                strSqlString.Append("             , OLD_OPER AS OPER " + "\n");
                strSqlString.Append("             , END_RES_ID " + "\n");
                strSqlString.Append("             , TRAN_CMF_2 " + "\n");
                strSqlString.Append("             , TRAN_CMF_3 " + "\n");
                strSqlString.Append("             , NVL(( " + "\n");
                strSqlString.Append("                    SELECT SUM(QTY_2) AS LOSS_QTY " + "\n");
                strSqlString.Append("                      FROM RWIPLOTHIS " + "\n");
                strSqlString.Append("                     WHERE LOT_ID = A.LOT_ID " + "\n");
                strSqlString.Append("                       AND OPER = A.OLD_OPER " + "\n");
                strSqlString.Append("                       AND HIST_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                       AND TRAN_CODE = 'LOSS' " + "\n");
                strSqlString.Append("                  ), 0) AS LOSS_QTY " + "\n");
                //strSqlString.Append("             , NVL(( " + "\n");
                //strSqlString.Append("                    SELECT SUM(LOSS_QTY) AS LOSS_QTY " + "\n");
                //strSqlString.Append("                      FROM RWIPLOTLSM " + "\n");
                //strSqlString.Append("                     WHERE LOT_ID = A.LOT_ID " + "\n");
                //strSqlString.Append("                       AND OPER = A.OLD_OPER " + "\n");
                //strSqlString.Append("                     GROUP BY LOT_ID " + "\n");
                //strSqlString.Append("                  ), 0) AS LOSS_QTY " + "\n");
                // 2010-11-23-임종우 : partial ship Logic 추가 함.            
                strSqlString.Append("             , CASE WHEN TRAN_CODE = 'SHIP' AND FACTORY = OLD_FACTORY THEN OLD_QTY_1 - QTY_1 " + "\n");
                strSqlString.Append("                    ELSE QTY_1 " + "\n");
                strSqlString.Append("               END QTY_1 " + "\n");
                strSqlString.Append("             , DECODE(START_TIME, ' ', OLD_OPER_IN_TIME, START_TIME) AS OPER_IN_TIME " + "\n");
                strSqlString.Append("             , TRAN_TIME " + "\n");
                strSqlString.Append("             , TRAN_USER_ID " + "\n");
                strSqlString.Append("             , CRR_ID " + "\n");
                strSqlString.Append("             , TRAN_COMMENT " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT USER_DESC FROM RWEBUSRDEF WHERE FACTORY = 'SYSTEM' AND USER_ID = A.TRAN_USER_ID " + "\n");
                strSqlString.Append("               ) AS END_USER_DESC " + "\n");

                // 2012-05-07-임종우 : Wafer 수량 표시 (김문한 요청)
                strSqlString.Append("             , QTY_2 " + "\n");
                strSqlString.Append("             , TRAN_CMF_18 " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT USER_DESC FROM RWEBUSRDEF WHERE FACTORY = 'SYSTEM' AND USER_ID = A.TRAN_CMF_18 " + "\n");
                strSqlString.Append("               ) AS IN_USER_DESC " + "\n");
                strSqlString.Append("             , LOT_CMF_7 " + "\n");
                strSqlString.Append("             , TRAN_CMF_17 " + "\n");
                strSqlString.Append("             , LOT_CMF_11 " + "\n");
                strSqlString.Append("             , TRAN_CMF_7 " + "\n");
                strSqlString.Append("             , TRAN_CMF_8 " + "\n");
                strSqlString.Append("             , TRAN_CMF_9 " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND TRAN_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "' " + "\n");
                strSqlString.Append("           AND OLD_FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND TRAN_CODE IN ('END','SHIP') " + "\n");

                //2014-07-24-장한별  : 구분 체크박스 체크 시 생산 ,PCB 정보를 추가하기 위해
                if (cboGubun.Text == "PCB")
                {
                    strSqlString.Append("           AND LOT_TYPE = 'P' " + "\n");
                }
                else
                {
                    strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
                }
                

                strSqlString.Append("           AND HIST_DEL_FLAG = ' ' " + "\n");
                if (!txtSearchProduct.Text.Equals("%") && !String.IsNullOrEmpty(txtSearchProduct.Text.Trim()))
                {
                    strSqlString.Append("           AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");
                }
                strSqlString.Append("           AND OLD_OPER " + cdvOper.SelectedValueToQueryString + "\n");
                strSqlString.Append("           AND END_RES_ID " + cdvRas.SelectedValueToQueryString + "\n");
                if (!String.IsNullOrEmpty(txtSearchLotId.Text))
                {
                    strSqlString.Append("           AND LOT_ID = '" + txtSearchLotId.Text + "' " + "\n");
                }
                strSqlString.Append("           AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                strSqlString.Append("       ) SUMMERY " + "\n");
                strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                // 2012-07-17-김민우 : 모델,UPEG 체크박스 체크 시 설비 MODEL, UPEH 정보를 추가하기 위해
                if (ckUPEH.Checked)
                {
                    strSqlString.Append("     , MRASRESDEF RAS " + "\n");
                }
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND SUMMERY.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                // 2012-07-17-김민우 : 모델,UPEG 체크박스 체크 시 설비 MODEL, UPEH 정보를 추가하기 위해
                if (ckUPEH.Checked)
                {
                    //strSqlString.Append("   AND RAS.FACTORY(+) = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("   AND RAS.FACTORY(+) " + cdvFactory.SelectedValueToQueryString + "\n");
                    strSqlString.Append("   AND SUMMERY.END_RES_ID = RAS.RES_ID(+) " + "\n");
                }

                // 2014-07-24-장한별  : 구분 체크박스 체크 시 생산 ,PCB 정보를 추가하기 위해
                if (cboGubun.Text == "PCB")
                {
                    strSqlString.Append("   AND MAT.MAT_TYPE = 'PB' " + "\n");
                }
                else
                {
                    strSqlString.Append("   AND MAT.MAT_TYPE = 'FG' " + "\n");
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
                strSqlString.Append("SELECT " + QueryCond1 + "\n");
                strSqlString.Append("     , SUMMERY.MAT_ID " + "\n");
                strSqlString.Append("     , SUMMERY.LOT_ID " + "\n");
                strSqlString.Append("     , SUMMERY.LOT_CMF_5 " + "\n");
                strSqlString.Append("     , SUMMERY.END_RES_ID " + "\n");
                strSqlString.Append("     , SUMMERY.QTY_1 + SUMMERY.LOSS_QTY AS OPER_IN_QTY_1 " + "\n");
                strSqlString.Append("     , SUMMERY.QTY_1 " + "\n");
                strSqlString.Append("     , ROUND(SUMMERY.QTY_1/(SUMMERY.QTY_1 + SUMMERY.LOSS_QTY)*100,3) AS YIELD " + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(SUMMERY.OPER_IN_TIME,'yyyymmddhh24miss')) AS OPER_IN_TIME " + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(SUMMERY.TRAN_TIME,'yyyymmddhh24miss')) AS TRAN_TIME " + "\n");

                //2013-03-15-임종우 : START 작업자 표시 (김경환직장 요청)
                strSqlString.Append("     , DECODE(SUMMERY.TRAN_CMF_18, ' ', ' ', SUMMERY.IN_USER_DESC || '(' || SUMMERY.TRAN_CMF_18 || ')') AS IN_USER_ID " + "\n");
                strSqlString.Append("     , SUMMERY.END_USER_DESC || '(' || SUMMERY.TRAN_USER_ID || ')' AS END_USER_ID " + "\n");
                strSqlString.Append("     , SUMMERY.LOT_CMF_10 " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_CMF_2 AS HD " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_CMF_3 AS PGM " + "\n");
                strSqlString.Append("     , SUMMERY.CRR_ID " + "\n");
                strSqlString.Append("     , '' EPOXY " + "\n");
                strSqlString.Append("     , '' LF " + "\n");
                strSqlString.Append("     , '' GW " + "\n");
                strSqlString.Append("     , '' MATAL_CARD " + "\n");
                strSqlString.Append("     , '' EMC_LOT " + "\n");
                strSqlString.Append("     , '' EMC_CAN " + "\n");
                strSqlString.Append("     , '' EMC_TYPE " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_COMMENT " + "\n");
                strSqlString.Append("     , SUMMERY.QTY_2 " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_CMF_17 AS CV " + "\n");

                // 2012-07-17-김민우 : 모델,UPEG 체크박스 체크 시 설비 MODEL, UPEH 추가
                if (ckUPEH.Checked)
                {
                    strSqlString.Append("     , RES_GRP_6" + "\n");
                    strSqlString.Append("     , (SELECT UPEH FROM CRASUPHDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OPER = SUMMERY.OPER AND RES_MODEL = RES_GRP_6 AND UPEH_GRP = RES_GRP_7 AND MAT_ID = SUMMERY.MAT_ID) AS UPEH " + "\n");
                }

                //2013-03-15-임종우 : ASSY SITE 표시 (황혜리 요청)
                strSqlString.Append("     , SUMMERY.LOT_CMF_7 AS ASSY_SITE " + "\n");

                //2013-04-25-임종우 : SHIP SITE 표시 (김문한 요청)
                strSqlString.Append("     , SUMMERY.LOT_CMF_11 AS SHIP_SITE " + "\n");

                // 2013-08-23-임종우 : Rework 데이터 추가 요청 (박상현 요청)
                strSqlString.Append("     , SUMMERY.TRAN_CMF_7 AS REWORK_BALL " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_CMF_8 AS REWORK_CLEAN " + "\n");
                strSqlString.Append("     , SUMMERY.TRAN_CMF_9 AS REWORK_PRINT " + "\n");
                strSqlString.Append("     , LOT_CMF_3 " + "\n");
                strSqlString.Append("  FROM (  " + "\n");
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , LOT_ID " + "\n");
                strSqlString.Append("             , LOT_CMF_5 " + "\n");
                strSqlString.Append("             , LOT_CMF_10 " + "\n");
                strSqlString.Append("             , OLD_OPER AS OPER " + "\n");
                strSqlString.Append("             , END_RES_ID " + "\n");
                strSqlString.Append("             , TRAN_CMF_2 " + "\n");
                strSqlString.Append("             , TRAN_CMF_3 " + "\n");
                strSqlString.Append("             , NVL(( " + "\n");
                strSqlString.Append("                    SELECT SUM(LOSS_QTY) AS LOSS_QTY " + "\n");
                strSqlString.Append("                      FROM RWIPLOTLSM " + "\n");
                strSqlString.Append("                     WHERE LOT_ID = A.LOT_ID " + "\n");
                strSqlString.Append("                       AND OPER = A.OLD_OPER " + "\n");
                strSqlString.Append("                     GROUP BY LOT_ID " + "\n");
                strSqlString.Append("                  ), 0) AS LOSS_QTY " + "\n");

                // 2010-11-23-임종우 : partial ship Logic 추가 함.            
                strSqlString.Append("             , CASE WHEN TRAN_CODE = 'SHIP' AND FACTORY = OLD_FACTORY THEN OLD_QTY_1 - QTY_1 " + "\n");
                strSqlString.Append("                    ELSE QTY_1 " + "\n");
                strSqlString.Append("               END QTY_1 " + "\n");
                strSqlString.Append("             , DECODE(START_TIME, ' ', OLD_OPER_IN_TIME, START_TIME) AS OPER_IN_TIME " + "\n");
                strSqlString.Append("             , TRAN_TIME " + "\n");
                strSqlString.Append("             , TRAN_USER_ID " + "\n");
                strSqlString.Append("             , CRR_ID " + "\n");
                strSqlString.Append("             , TRAN_COMMENT " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT USER_DESC FROM RWEBUSRDEF WHERE FACTORY = 'SYSTEM' AND USER_ID = A.TRAN_USER_ID " + "\n");
                strSqlString.Append("               ) AS END_USER_DESC " + "\n");

                // 2015-09-14-정비재 : rfid로 start한 정보를 표시하기 위하여 추가함.
                // 2012-05-07-임종우 : Wafer 수량 표시 (김문한 요청)
                strSqlString.Append("             , QTY_2 " + "\n");
                strSqlString.Append("             , CASE WHEN A.OLD_OPER LIKE 'A040%' AND A.RESV_FLAG_5 = 'Y' AND (SELECT MAX(CASE WHEN (TRAN_CMF_4 = 'RFID TAG WRITE : SUCCESS') " + "\n");
                strSqlString.Append("                                                                                            OR (RESV_FLAG_5 = 'Y' AND TRAN_CMF_19 = 'RFID' AND TRAN_USER_ID = 'EIS') THEN 'RFID' " + "\n");
                strSqlString.Append("                                                                                          ELSE ' ' " + "\n");
                strSqlString.Append("                                                                                     END) KEEP(DENSE_RANK FIRST ORDER BY TRAN_TIME DESC)  " + "\n");
                strSqlString.Append("                                                                                FROM RWIPLOTHIS " + "\n");
                strSqlString.Append("                                                                               WHERE FACTORY = A.OLD_FACTORY " + "\n");
                strSqlString.Append("                                                                                 AND LOT_TYPE = A.OLD_LOT_TYPE " + "\n"); 
                strSqlString.Append("                                                                                 AND LOT_ID = A.LOT_ID " + "\n");
                strSqlString.Append("                                                                                 AND OPER = A.OLD_OPER " + "\n");
                strSqlString.Append("                                                                                 AND TRAN_TIME < A.TRAN_TIME " + "\n");
                strSqlString.Append("                                                                                 AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                                                                 AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                                                                 AND RESV_FLAG_5 = 'Y' " + "\n");
                strSqlString.Append("                                                                                 AND TRAN_CODE = 'START' " + "\n");
                strSqlString.Append("                                                                                 AND HIST_DEL_FLAG = ' ') = 'RFID' THEN TRAN_CMF_18 || '[RFID]' " + "\n");
                strSqlString.Append("                     WHEN A.OLD_OPER NOT LIKE 'A040%' AND A.RESV_FLAG_5 = 'Y' AND A.TRAN_CMF_19 = 'RFID' AND A.TRAN_USER_ID = 'EIS' THEN TRAN_CMF_18 || '[' || TRIM(TRAN_CMF_19) || ']' " + "\n");
                strSqlString.Append("                     ELSE TRAN_CMF_18 " + "\n");
                strSqlString.Append("               END AS TRAN_CMF_18 " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT USER_DESC FROM RWEBUSRDEF WHERE FACTORY = 'SYSTEM' AND USER_ID = A.TRAN_CMF_18 " + "\n");
                strSqlString.Append("               ) AS IN_USER_DESC " + "\n");
                strSqlString.Append("             , LOT_CMF_7 " + "\n");
                strSqlString.Append("             , TRAN_CMF_17 " + "\n");
                strSqlString.Append("             , LOT_CMF_11 " + "\n");
                strSqlString.Append("             , TRAN_CMF_7 " + "\n");
                strSqlString.Append("             , TRAN_CMF_8 " + "\n");
                strSqlString.Append("             , TRAN_CMF_9 " + "\n");
                strSqlString.Append("             , LOT_CMF_3 " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND TRAN_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "' " + "\n");
                strSqlString.Append("           AND OLD_FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND TRAN_CODE IN ('END','SHIP') " + "\n");

                //2014-07-24-장한별  : 구분 체크박스 체크 시 생산 ,PCB 정보를 추가하기 위해
                if (cboGubun.Text == "PCB")
                {
                    strSqlString.Append("           AND LOT_TYPE = 'P' " + "\n");
                }
                else
                {
                    strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
                }

                strSqlString.Append("           AND HIST_DEL_FLAG = ' ' " + "\n");
                if (!txtSearchProduct.Text.Equals("%") && !String.IsNullOrEmpty(txtSearchProduct.Text.Trim()))
                {
                    strSqlString.Append("           AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");
                }
                strSqlString.Append("           AND OLD_OPER " + cdvOper.SelectedValueToQueryString + "\n");
                strSqlString.Append("           AND END_RES_ID " + cdvRas.SelectedValueToQueryString + "\n");
                if (!String.IsNullOrEmpty(txtSearchLotId.Text))
                {
                    strSqlString.Append("           AND LOT_ID = '" + txtSearchLotId.Text + "' " + "\n");
                }
                strSqlString.Append("           AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                strSqlString.Append("       ) SUMMERY " + "\n");
                strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                // 2012-07-17-김민우 : 모델,UPEG 체크박스 체크 시 설비 MODEL, UPEH 정보를 추가하기 위해
                if (ckUPEH.Checked)
                {
                    strSqlString.Append("     , MRASRESDEF RAS " + "\n");
                }
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND SUMMERY.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                // 2012-07-17-김민우 : 모델,UPEG 체크박스 체크 시 설비 MODEL, UPEH 정보를 추가하기 위해
                if (ckUPEH.Checked)
                {
                    strSqlString.Append("   AND RAS.FACTORY(+) = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("   AND SUMMERY.END_RES_ID = RAS.RES_ID(+) " + "\n");
                }

                // 2014-07-24-장한별  : 구분 체크박스 체크 시 생산 ,PCB 정보를 추가하기 위해
                if (cboGubun.Text == "PCB")
                {
                    strSqlString.Append("   AND MAT.MAT_TYPE = 'PB' " + "\n");
                }
                else
                {
                    strSqlString.Append("   AND MAT.MAT_TYPE = 'FG' " + "\n");
                }


                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
                
                #endregion
            }


            strSqlString.Append(" ORDER BY " + QueryCond1 + ",SUMMERY.END_RES_ID,LOT_ID,MAT_ID " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>
        private void MakeChart(DataTable DT)
        {

        }

        #endregion

        #region " Event 처리 "

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;
                this.Refresh();
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

                ////by John (한줄짜리)
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 15, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;
                //spdData.DataSource = dt;
                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 15, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //5. 데이타가 0인 부분은 제거(Add by John. 2009.1.13)
                //spdData.RPT_RemoveZeroColumn(10,1);

                //6. Trans Comment 잘라내서 넣기
                if (cdvFactory.Text.Trim() == "HMKB1")
                    MaterialCellFill(spdData, 36);                
                else
                    MaterialCellFill(spdData, 33);
                
                // YIELD 부분의  TOTAL값 및 SUB TOTAL을 계산하지 않고 직접 계산 
                string subtotal = null;

                for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "Yield")
                    {
                        spdData.ActiveSheet.Cells[0, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 2].Value)) * 100).ToString();

                        for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)
                        {
                            for (int k = 0; k < sub + 1; k++)
                            {
                                if (spdData.ActiveSheet.Cells[j, k].Value != null)
                                    subtotal = spdData.ActiveSheet.Cells[j, k].Value.ToString();

                                subtotal.Trim();
                                if (subtotal.Length > 5)
                                {
                                    if (subtotal.Substring(subtotal.Length - 5, 5) == "Total")
                                    {
                                        if (Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 2].Value) != 0)
                                        {
                                            spdData.ActiveSheet.Cells[j, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 2].Value)) * 100).ToString();
                                        }
                                        else
                                        {
                                            spdData.ActiveSheet.Cells[j, i].Value = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
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
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();
        }

        /// <summary>
        /// Factory 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
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
            cdvOper.sFactory = cdvFactory.txtValue;
            cdvLotType.sFactory = cdvFactory.txtValue;
            cdvRas.sFactory = cdvFactory.txtValue;

            SortInit();     //add 150601
        }
        #endregion

        #region " USER DEFINE Method "
        /// <summary>
        /// Material 자재 내용 채우기
        /// </summary>
        /// <param name="spdData"></param>
        /// <param name="pos"></param>
        private void MaterialCellFill(udcFarPoint spdData,int pos)
        {
            String comment = String.Empty;

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    comment = spdData.ActiveSheet.Cells[i, pos].Value.ToString();

                    // Epoxy
                    spdData.ActiveSheet.Cells[i, 28].Value = GetMaterialFromComment("Epoxy", comment);

                    // Lead Frame
                    spdData.ActiveSheet.Cells[i, 29].Value = GetMaterialFromComment("LeadFrame", comment);

                    // GW
                    spdData.ActiveSheet.Cells[i, 30].Value = GetMaterialFromComment("GoldWire", comment);

                    // Metal Card
                    spdData.ActiveSheet.Cells[i, 31].Value = GetMaterialFromComment("MetalCard", comment);

                    // EMC LOT
                    spdData.ActiveSheet.Cells[i, 32].Value = GetMaterialFromComment("EMCLOT", comment);

                    // EMC CAN
                    spdData.ActiveSheet.Cells[i, 33].Value = GetMaterialFromComment("EMCCAN", comment);

                    // EMC Type
                    spdData.ActiveSheet.Cells[i, 34].Value = GetMaterialFromComment("EMCTYPE", comment);
                }
            }
            else
            {
                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    comment = spdData.ActiveSheet.Cells[i, pos].Value.ToString();

                    // Epoxy
                    spdData.ActiveSheet.Cells[i, 26].Value = GetMaterialFromComment("Epoxy", comment);

                    // Lead Frame
                    spdData.ActiveSheet.Cells[i, 27].Value = GetMaterialFromComment("LeadFrame", comment);

                    // GW
                    spdData.ActiveSheet.Cells[i, 28].Value = GetMaterialFromComment("GoldWire", comment);

                    // Metal Card
                    spdData.ActiveSheet.Cells[i, 29].Value = GetMaterialFromComment("MetalCard", comment);

                    // EMC LOT
                    spdData.ActiveSheet.Cells[i, 30].Value = GetMaterialFromComment("EMCLOT", comment);

                    // EMC CAN
                    spdData.ActiveSheet.Cells[i, 31].Value = GetMaterialFromComment("EMCCAN", comment);

                    // EMC Type
                    spdData.ActiveSheet.Cells[i, 32].Value = GetMaterialFromComment("EMCTYPE", comment);
                }
            }
        }

        /// <summary>
        /// 주어진 문자열에서 주어진 Type 의 자재를 검색함
        /// </summary>
        /// <param name="type"></param>
        /// <param name="comment"></param>
        private String GetMaterialFromComment(String type, String comment)
        {
            if (String.IsNullOrEmpty(comment))
            {
                return String.Empty;
            }

            String returnString = String.Empty;

            if (type.Equals("LeadFrame"))
            {
                String[] arrSpliter = { "L/F_LOT_NO1:" };
                return GetMaterialStringFromArray(arrSpliter, comment);
            }
            else if (type.Equals("Epoxy"))
            {
                String[] arrSpliter = { "EPOXY_LOT_NO1:", "EPOXY_LOT_NO2:", "EPOXY_LOT_NO3:" };
                return GetMaterialStringFromArray(arrSpliter, comment);
            }
            else if (type.Equals("GoldWire"))
            {
                String[] arrSpliter = { "G/W_LOT_NO1:", "G/W_LOT_NO2:" };
                return GetMaterialStringFromArray(arrSpliter, comment);
            }
            else if (type.Equals("MetalCard"))
            {
                String[] arrSpliter = { "METAL_CARD:" };
                return GetMaterialStringFromArray(arrSpliter, comment);
            }
            else if (type.Equals("EMCLOT"))
            {
                String[] arrSpliter = { "EMCLOTNO:" };
                return GetMaterialStringFromArray(arrSpliter, comment);
            }
            else if (type.Equals("EMCCAN"))
            {
                String[] arrSpliter = { "EMCCANNO:" };
                return GetMaterialStringFromArray(arrSpliter, comment);
            }
            else if (type.Equals("EMCTYPE"))
            {
                String[] arrSpliter = { "EMCTYPE:" };
                return GetMaterialStringFromArray(arrSpliter, comment);
            }

            return returnString;
        }

        /// <summary>
        /// 주어진 pattern 배열을 가지고 Comment 를 Parsing 하여 문자열을 Return 한다.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="Comment"></param>
        /// <returns></returns>
        private String GetMaterialStringFromArray(String[] pattern, String comment)
        {
            String returnString = String.Empty;

            String[] arrComment = comment.Split(',');
            foreach (String str in arrComment)
            {
                foreach (String spliter in pattern)
                {
                    int idxPattern = -1;

                    idxPattern = str.IndexOf(spliter);

                    if (idxPattern != -1 && !String.IsNullOrEmpty(str.Replace(spliter, "")))
                    {
                        if (!String.IsNullOrEmpty(returnString)) returnString += ",";
                        returnString += str.Replace(spliter, "");
                    }
                }
            }

            return returnString;
        }

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
