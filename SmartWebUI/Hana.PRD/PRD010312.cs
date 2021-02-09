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
    public partial class PRD010312 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010312<br/>
        /// 클래스요약: NEW_PART별 재공 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2011-04-27<br/>
        /// 상세  설명: 기존 PART별 제공 현황 조회 대신 신규로 추가 함.<br/>
        /// 변경  내용: <br/>
        /// 작  성  자: 하나마이크론 김준용<br />
        /// 실적조회 체크박스를 체크 할 경우 일A/0,주A/O,월A/O 가 나오던것을 일Ship 만 조회 되도록 변경<br />
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2010-08-27-임종우 : 고객사별 PIN_TYPE 별 A/O 값 표시 및 재공대비 색상 표시 추가 (정기문 요청)
        /// 2010-11-04-임종우 : Kpcs 조회시 1000미만 데이터 표시 되도록 수정 (민재훈 요청)
        /// 2010-11-12-임종우 : Group 에 Version 추가 (임태성 요청)
        /// 2011-02-09-임종우 : Time Base 00시 기준 추가 (임태성 요청)
        /// 2011-04-11-배수민 : 설비 Arrange 대수 체크옵션 주기. 체크 시, 공정별 재공옆에 설비Arrange대수 표시 (TEST공장은 제외)
        /// 2011-05-02-임종우 : 삼성 SLSI 제품에 한해서 SOP 데이터 표시 (권문석 요청)
        /// 2011-05-02-임종우 : HMKA1일 경우 SOP 데이터의 월부족에 의한 재공 색상 표시 (권문석 요청)
        /// 2011-05-19-임종우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 (김권수 요청)
        /// 2011-06-14-김민우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 원복 (김권수 요청)
        /// 2011-09-26-임종우 : Repair & Return Stock 의 재공을 원래 공정에 재공 표시 되도록 추가 (임태성 요청)
        /// 2012-02-01-김민우 : MRASRESSTS의 공정정보 RES_STS_9에서 RES_STS_8로 변경
        /// 2013-09-02-임종우 : Qual Lot (DOE, PPQ, PPQE, 시양산) 조회 기능 추가 (임태성 요청)
        /// 2013-09-03-임종우 : Lot Type 검색 기능 수정 (ALL, P%, E%)
        /// 2014-09-15-임종우 : 재공이 없는 공정도 표시 한다.(최인남이사 요청)
        /// 2014-09-16-임종우 : 재공 유무에 따른 공정 표시를 선택에 따라 볼 수 있도록 변경 (최인남이사 요청)
        /// 2015-01-13-임종우 : 14시 재공 조회 검색 기능 추가 (백성호사원 요청)
        /// 2015-07-16-임종우 : 삼성 MCP KIT 조회 기능 추가 (김성업D 요청)
        /// 2016-10-06-임종우 : 04시 재공 조회 검색 기능 추가 (한성희C 요청)
        /// </summary>
        public PRD010312()
        {
            InitializeComponent();
            //udcDurationDate1.AutoBinding();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리 

            cboHolddiv.SelectedIndex = 0;
            cdvDate.Value = DateTime.Today;
            cboTimeBase.SelectedIndex = 4;
        }


        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();

            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                if (ckbMcpKit.Checked == true)
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Code", "MAT.MAT_CMF_11 AS PKG_CODE", "MAT.MAT_CMF_11", "MAT_GRP_3", "MAT_GRP_3 AS PACKAGE", "MAT_GRP_3", true);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MCP To Part", "MAT.MCP_TO_PART", "MAT.MCP_TO_PART", "MAT_GRP_4", "MAT_GRP_4 AS TYPE1", "MAT_GRP_4", true);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10 AS PIN_TYPE", "MAT_CMF_10", true);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID AS PRODUCT", "MAT.MAT_ID", "WIP.MAT_ID", "WIP.MAT_ID", "WIP.MAT_ID", true);                    
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 AS TYPE2", "MAT.MAT_GRP_5", "MAT_GRP_6", "MAT_GRP_6 AS LDCOUNT", "MAT_GRP_6", true);                    
                }
                else
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT_GRP_1", true);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 AS FAMILY", "MAT.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2 AS FAMILY", "MAT_GRP_2", false);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 AS PACKAGE", "MAT.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3 AS PACKAGE", "MAT_GRP_3", false);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 AS TYPE1", "MAT.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4 AS TYPE1", "MAT_GRP_4", false);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 AS TYPE2", "MAT.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5 AS TYPE2", "MAT_GRP_5", false);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 AS LDCOUNT", "MAT.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6 AS LDCOUNT", "MAT_GRP_6", false);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 AS DENSITY", "MAT.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7 AS DENSITY", "MAT_GRP_7", false);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 AS GENERATION", "MAT.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8 AS GENERATION", "MAT_GRP_8", false);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10 AS PIN_TYPE", "MAT_CMF_10", true);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EMC_1", "WIP.EMC_1 AS EMC_1", "WIP.EMC_1", "EMC_1", "EMC_1 AS EMC_1", "EMC_1", false);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID AS PRODUCT", "MAT.MAT_ID", "WIP.MAT_ID", "WIP.MAT_ID", "WIP.MAT_ID", true);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust_Device", "MAT.MAT_CMF_7 AS CUST_DEVICE", "MAT.MAT_CMF_7", "MAT_CMF_7", "MAT_CMF_7 AS CUST_DEVICE", "MAT_CMF_7", false);
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Version", "WIP.VERSION", "WIP.VERSION", "''", "MAX((SELECT ATTR_VALUE FROM MATRNAMSTS WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND ATTR_TYPE = 'MAT_ETC' AND ATTR_NAME = DECODE(MAT_GRP_1, 'SE', 'SEC_VERSION', 'HX', 'HX_VERSION') AND ATTR_KEY = MAT.MAT_ID)) AS VERSION", "VERSION", false);
                }
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Bumping Type TYPE", "MAT.MAT_GRP_2 AS BUMPING_TYPE", "MAT.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2 AS BUMPING_TYPE", "MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation Flow", "MAT.MAT_GRP_3 AS PROCESS_FLOW", "MAT.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3 AS PROCESS_FLOW", "MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT.MAT_GRP_4 AS LAYER", "MAT.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4 AS LAYER", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Type", "MAT.MAT_GRP_5 AS PKG_TYPE", "MAT.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5 AS PKG_TYPE", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL Plating", "MAT.MAT_GRP_6 AS RDL_PLATING", "MAT.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6 AS RDL_PLATING", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Final Bump", "MAT.MAT_GRP_7 AS FINAL_BUMP", "MAT.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7 AS FINAL_BUMP", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. Material", "MAT.MAT_GRP_8 AS SUB_MATERIAL", "MAT.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8 AS SUB_MATERIAL", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Size", "MAT.MAT_CMF_14 AS WF_SIZE", "MAT.MAT_CMF_14", "MAT_CMF_14", "MAT_CMF_14 AS WF_SIZE", "MAT_CMF_14", false); // \"SIZE\"
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Thickness", "MAT.MAT_CMF_2 AS THICKNESS", "MAT.MAT_CMF_2", "MAT_CMF_2", "MAT_CMF_2 AS THICKNESS", "MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Flat Type", "MAT.MAT_CMF_3 AS FLAT_TYPE", "MAT.MAT_CMF_3", "MAT_CMF_3", "MAT_CMF_3 AS FLAT_TYPE", "MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Wafer ORI", "MAT.MAT_CMF_4 AS WAFER_ORIENTATION", "MAT.MAT_CMF_4", "MAT_CMF_4", "MAT_CMF_4 AS WAFER_ORIENTATION", "MAT_CMF_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID AS PRODUCT", "MAT.MAT_ID", "WIP.MAT_ID", "WIP.MAT_ID", "WIP.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust_Device", "MAT.MAT_CMF_7 AS CUST_DEVICE", "MAT.MAT_CMF_7", "MAT_CMF_7", "MAT_CMF_7 AS CUST_DEVICE", "MAT_CMF_7", false);
            }
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;            

            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1; // 초기화 

            #region Assy, Test
            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                if (chkResCnt.Checked == true)
                {
                    spdData.RPT_ColumnInit();
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("EMC_1", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Cust_Device", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Version", 0, 12, Visibles.False, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Total WIP", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("the total number", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    

                    // 2011-04-13-임종우 : 설비Arrange대수 체크 시, 해당 공정마다 재공&대수 같이 보여주기.
                    for (int i = 0; i < spdData.ActiveSheet.ColumnCount; i++)
                    {
                        spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                    }

                    ListView arrOper = cdvOper.getItemsFromToValue();

                    for (int i = 0; i < arrOper.Items.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(arrOper.Items[i].Text, 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("WIP", 1, spdData.ActiveSheet.ColumnCount - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("count", 1, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnCount - 2, 2);
                    }

                    // 2011-04-13-배수민 : 실적조회 체크 시, 일SHIP 보여주기.
                    if (chkViewAO.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("일SHIP", 1, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    }

                }
                else // 설비Arrange대수 체크옵션 해제 (재공만 보여주기)
                {
                    spdData.RPT_ColumnInit();

                    if (ckbMcpKit.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("PKG Code", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("MCP To Part", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Pin Type", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Product", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);                        
                        spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("TOTAL", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddDynamicColumn(cdvOper, 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                        if (cdvFactory.Text != "")
                        {                            
                            if (chkViewAO.Checked == true)
                            {
                                spdData.RPT_AddBasicColumn("일SHIP", 0, cdvOper.CountFromToValue + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                            }
                            else
                            {
                                spdData.RPT_AddBasicColumn("일SHIP", 0, cdvOper.CountFromToValue + 6, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                            }                            

                            if (ckbSOP.Checked == true)
                            {
                                spdData.RPT_AddBasicColumn("plan", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                                spdData.RPT_AddBasicColumn("actual", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                                spdData.RPT_AddBasicColumn("Month shortage", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                            }
                            else
                            {
                                spdData.RPT_AddBasicColumn("plan", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                                spdData.RPT_AddBasicColumn("actual", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                                spdData.RPT_AddBasicColumn("Month shortage", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                            }
                        }
                    }
                    else
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
                        spdData.RPT_AddBasicColumn("EMC_1", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Cust_Device", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Version", 0, 12, Visibles.False, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("TOTAL", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddDynamicColumn(cdvOper, 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                        if (cdvFactory.Text != "")
                        {
                            if (cdvFactory.Text.ToString() == GlobalVariable.gsTestDefaultFactory)
                            {
                                spdData.RPT_AddBasicColumn("F0000", 0, cdvOper.CountFromToValue + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                                spdData.RPT_AddBasicColumn("F000N", 0, cdvOper.CountFromToValue + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                                // TEST는 실적을 보지 않는다. 이유는?? 글쎄...
                                spdData.RPT_AddBasicColumn("일SHIP", 0, cdvOper.CountFromToValue + 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                            }
                            else if (cdvFactory.Text.ToString() == GlobalVariable.gsAssyDefaultFactory)
                            {
                                if (chkViewAO.Checked == true)
                                {
                                    spdData.RPT_AddBasicColumn("일SHIP", 0, cdvOper.CountFromToValue + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                                }
                                else
                                {
                                    spdData.RPT_AddBasicColumn("일SHIP", 0, cdvOper.CountFromToValue + 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                                }
                            }

                            if (ckbSOP.Checked == true)
                            {
                                spdData.RPT_AddBasicColumn("plan", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                                spdData.RPT_AddBasicColumn("actual", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                                spdData.RPT_AddBasicColumn("Month shortage", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                            }
                            else
                            {
                                spdData.RPT_AddBasicColumn("plan", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                                spdData.RPT_AddBasicColumn("actual", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                                spdData.RPT_AddBasicColumn("Month shortage", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                            }
                        }
                    }
                }
            }
            #endregion

            #region Bump
            else   // Bump Factory
            {
                if (chkResCnt.Checked == true)
                {
                    spdData.RPT_ColumnInit();
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Bump Type", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Layer", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Final BUMP", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("SUB. Material", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 110);
                    spdData.RPT_AddBasicColumn("Size", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Wafer", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("Product", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);

                    spdData.RPT_AddBasicColumn("Cust_Device", 0, 13, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

                    spdData.RPT_AddBasicColumn("Total WIP", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("the total number", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    // 2011-04-13-임종우 : 설비Arrange대수 체크 시, 해당 공정마다 재공&대수 같이 보여주기.
                    for (int i = 0; i < spdData.ActiveSheet.ColumnCount; i++)
                    {
                        spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                    }

                    ListView arrOper = cdvOper.getItemsFromToValue();

                    for (int i = 0; i < arrOper.Items.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(arrOper.Items[i].Text, 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("WIP", 1, spdData.ActiveSheet.ColumnCount - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("count", 1, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnCount - 2, 2);
                    }

                    // 2011-04-13-배수민 : 실적조회 체크 시, 일SHIP 보여주기.
                    if (chkViewAO.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("일SHIP", 1, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    }

                }
                else // 설비Arrange대수 체크옵션 해제 (재공만 보여주기)
                {
                    spdData.RPT_ColumnInit();
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Bump Type", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Layer", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Final BUMP", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("SUB. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 110);
                    spdData.RPT_AddBasicColumn("Size", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Wafer", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("Product", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                    spdData.RPT_AddBasicColumn("Cust_Device", 0, 13, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("TOTAL", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddDynamicColumn(cdvOper, 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    if (chkViewAO.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("일SHIP", 0, cdvOper.CountFromToValue + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("일SHIP", 0, cdvOper.CountFromToValue + 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }

                    if (ckbSOP.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("plan", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("actual", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("Month shortage", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("plan", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("actual", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("Month shortage", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                }
            }
            #endregion

            // Group항목이 있을경우 반드시 선언해줄것.
            spdData.RPT_ColumnConfigFromTable_New(btnSort);
            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            int iOperEnd = 0;
            int[] rowType = null;

            if (!CheckField()) return;

            DataTable dt = null;

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

                if (cdvFactory.Text.ToString() != "HMKB1")
                {
                    if (ckbMcpKit.Checked == true)
                    {
                        ////1.Griid 합계 표시                
                        rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 2, 5, null, null, btnSort);     // 재공Total / TOTAL

                        ////3. Total부분 셀머지
                        spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);     // 재공Total / TOTAL

                        //4. Column Auto Fit
                        spdData.RPT_AutoFit(false);

                        // 2014-09-16-임종우 : 재공 유무에 따른 공정 표시를 선택에 따라 볼 수 있도록 변경
                        if (ckbOper.Checked == true)
                        {
                            //5. 데이타가 0인 부분은 제거(Add by John. 2009.1.13)
                            if (chkResCnt.Checked == false)
                            {
                                spdData.RPT_RemoveZeroColumn(5, 1);        // 재공Total / TOTAL
                            }
                        }
                    }
                    else
                    {
                        //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                        int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                        ////1.Griid 합계 표시                
                        rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort);     // 재공Total / TOTAL

                        ////2. 칼럼 고정(필요하다면..)
                        //spdData.Sheets[0].FrozenColumnCount = 10;

                        ////3. Total부분 셀머지
                        spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);     // 재공Total / TOTAL

                        //4. Column Auto Fit
                        spdData.RPT_AutoFit(false);

                        // 2014-09-16-임종우 : 재공 유무에 따른 공정 표시를 선택에 따라 볼 수 있도록 변경
                        if (ckbOper.Checked == true)
                        {
                            //5. 데이타가 0인 부분은 제거(Add by John. 2009.1.13)
                            // 2011-04-13-임종우 : 설비 Cnt 체크 안하는 경우만 해당 함수 사용(기본)
                            if (chkResCnt.Checked == false)
                            {
                                spdData.RPT_RemoveZeroColumn(13, 1);        // 재공Total / TOTAL
                            }
                        }
                    }
                }
                else
                {
                    //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                    int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                 
                    ////1.Griid 합계 표시                
                    rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 14, null, null, btnSort);     // 재공Total / TOTAL

                    ////2. 칼럼 고정(필요하다면..)
                    //spdData.Sheets[0].FrozenColumnCount = 10;

                    ////3. Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);     // 재공Total / TOTAL

                    //4. Column Auto Fit
                    spdData.RPT_AutoFit(false);

                    // 2014-09-16-임종우 : 재공 유무에 따른 공정 표시를 선택에 따라 볼 수 있도록 변경
                    if (ckbOper.Checked == true)
                    {
                        //5. 데이타가 0인 부분은 제거(Add by John. 2009.1.13)
                        // 2011-04-13-임종우 : 설비 Cnt 체크 안하는 경우만 해당 함수 사용(기본)
                        if (chkResCnt.Checked == false)
                        {
                            spdData.RPT_RemoveZeroColumn(14, 1);        // 재공Total / TOTAL
                        }
                    }
                 
                }

                //6.엑셀 저장시 부분합계 칼럼정보 저장.(부분합계칼럼수^ 반복행 수)
                //spdData.ActiveSheet.Tag = "2^1";

                if (ckbSOP.Checked == true && cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory && ckbMcpKit.Checked == false)
                {
                    // A/O 목표값이 있는 컬럼 위치 찾기
                    int aoColumns = spdData.ActiveSheet.ColumnHeader.Columns.Count - 1;

                    for (int i = 14; i <= aoColumns - 3; i++)
                    {
                        // 공정의 시작 컬럼과 끝나는 컬럼을 찾기 위해...                    
                        if (spdData.ActiveSheet.ColumnHeader.Columns[i].Label == "AZ010")
                        {
                            iOperEnd = i;
                        }                        
                    }

                    if (spdData.ActiveSheet.ColumnHeader.Columns[aoColumns].Label == "Month shortage" && iOperEnd > 0)
                    {
                        for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                        {
                            int sum = 0;
                            int value = 0;

                            if (spdData.ActiveSheet.Cells[i, aoColumns].BackColor.IsEmpty)
                            {
                                if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, aoColumns].Value) != 0)
                                {
                                    for (int y = iOperEnd; y > 13; y--)
                                    {
                                        value = Convert.ToInt32(spdData.ActiveSheet.Cells[i, y].Value);
                                        sum += value;

                                        if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, aoColumns].Value) > sum)
                                        {
                                            spdData.ActiveSheet.Cells[i, y].ForeColor = Color.Red;
                                            spdData.ActiveSheet.Cells[i, y].BackColor = Color.Pink;
                                        }
                                        else
                                        {
                                            spdData.ActiveSheet.Cells[i, y].ForeColor = Color.Blue;
                                            spdData.ActiveSheet.Cells[i, y].BackColor = Color.LightGreen;
                                            break;
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

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvOper.FromText.Trim().Length == 0 || cdvOper.ToText.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion


        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;
            string QueryCond5 = null;
           
            string strVal1 = null;
            string strCnt1 = null;
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값
            string strGroupCheck = null; // Group 선택 항목 체크 하기 위해 선언...

            String cutOffTime = String.Empty;
            String cutOffDate = String.Empty;
            String summeryTable = "RSUMFACMOV";
            int icntOperFromTo = 0;

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }

            string strDate = cdvDate.Value.ToString("yyyyMMdd");

            //if (cboTimeBase.Text == "06시")
            if (cboTimeBase.SelectedIndex == 2)
            {
                cutOffTime = strDate + "055959";
                cutOffDate = strDate + "06";
                summeryTable = "CSUMFACMOV";
            }            
            //else if (cboTimeBase.Text == "22시")
            else if (cboTimeBase.SelectedIndex == 4)
            {
                cutOffTime = strDate + "215959";
                cutOffDate = strDate + "22";
                summeryTable = "RSUMFACMOV";
            }
            //else if (cboTimeBase.Text == "14시")
            else if (cboTimeBase.SelectedIndex == 3)
            {
                cutOffTime = strDate + "215959";
                cutOffDate = strDate + "14";
                summeryTable = "RSUMFACMOV";
            }
            //else if (cboTimeBase.Text == "04시")
            else if (cboTimeBase.SelectedIndex == 1)
            {
                cutOffTime = strDate + "215959";
                cutOffDate = strDate + "04";
                summeryTable = "RSUMFACMOV";
            }
            else
            {
                cutOffTime = strDate + "235959";
                cutOffDate = strDate + "00";
                summeryTable = "RSUMFACMOV";
            }

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            strGroupCheck = tableForm.SelectedValue5ToQuery;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;
            QueryCond5 = tableForm.SelectedValue5ToQueryContainNull;

            #region 설비Arrange현황 체크 시
            // 설비Arrange현황 체크 시
            if (chkResCnt.Checked == true) 
            {
                //금일조회일 경우 조회조건은 REALTIME
                //if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.Text == "22시")
                if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.SelectedIndex == 4)
                {
                    cutOffTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                                        
                    strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond4);
                    strSqlString.AppendFormat("     , ROUND(SUM(NVL(TOTAL, 0))/" + sKpcsValue + ",0) AS TOTAL " + "\n");
                    strSqlString.AppendFormat("     , ROUND(SUM(NVL(CNT_TOTAL, 0))/" + sKpcsValue + ",0) AS CNT_TOTAL " + "\n");

                    ListView Oper = new ListView();
                    Oper = cdvOper.getItemsFromToValue();
                    for (int i = 0; i < Oper.Items.Count; i++)
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(NVL(V" + i + ", 0))/" + sKpcsValue + ", 0) AS Val" + i + " " + "\n");
                        strSqlString.AppendFormat("     , ROUND(SUM(NVL(C" + i + ", 0))/" + sKpcsValue + ", 0) AS Cnt" + i + " " + "\n");
                    }                    

                    //ASSY OUT 실적체크 하면 월/주/일별 AO출력
                    if (chkViewAO.Checked == true)
                    {
                        strSqlString.AppendFormat("     , TRUNC(SUM(NVL(DAO, 0))/" + sKpcsValue + ",0) AS DAO " + "\n");                        
                    }
                   
                    strSqlString.AppendFormat("  FROM ( " + "\n");
                    strSqlString.AppendFormat("        SELECT A.MAT_ID, C.EMC_1 " + "\n");
                    strSqlString.AppendFormat("             , SUM(QTY_1) AS TOTAL " + "\n");
                    strVal1 = cdvOper.getDecodeQuery("SUM(DECODE(A.OPER", "QTY_1,0))", "V").Replace(", SUM(DECODE", "             , SUM(DECODE");
                    strSqlString.AppendFormat("{0} ", strVal1);

                    strSqlString.AppendFormat("          FROM RWIPLOTSTS A, CLOTCRDDAT@RPTTOMES C " + "\n");
                    strSqlString.AppendFormat("         WHERE A.FACTORY = '{0}' " + "\n", CmnFunction.Trim(cdvFactory.Text));
                    strSqlString.AppendFormat("           AND A.OPER IN (" + cdvOper.getInQuery() + ")" + "\n");
                    
                    // Lot Type
                    if (cdvLotType.Text != "ALL")
                        strSqlString.Append("           AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "' \n");                   

                    // 2013-09-02-임종우 : Qual 구분 검색 조건 추가
                    if (cdvQual.Text != "ALL" && cdvQual.Text.Trim() != "")
                        strSqlString.Append("           AND A.LOT_CMF_17 " + cdvQual.SelectedValueToQueryString.Replace("''", "' '") + " \n");

                    strSqlString.AppendFormat("           AND A.OWNER_CODE = 'PROD'" + "\n");
                    strSqlString.AppendFormat("           AND A.FACTORY = C.FACTORY(+) " + "\n");
                    strSqlString.AppendFormat("           AND A.MAT_ID = C.MAT_ID(+) " + "\n");
                    strSqlString.AppendFormat("           AND A.LOT_DEL_FLAG = ' ' " + "\n");
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");
                    
                    if (cboHolddiv.Text == "Hold")
                        strSqlString.AppendFormat("           AND A.HOLD_FLAG = 'Y' " + "\n");
                    else if (cboHolddiv.Text == "Non Hold")
                        strSqlString.AppendFormat("           AND A.HOLD_FLAG = ' ' " + "\n");

                    strSqlString.AppendFormat("         GROUP BY A.MAT_ID, C.EMC_1 " + "\n");
                    strSqlString.AppendFormat("       ) WIP " + "\n");

                    strSqlString.AppendFormat("     , ( " + "\n");
                    strSqlString.AppendFormat("        SELECT A.RES_STS_2 AS MAT_ID, COUNT(A.RES_ID) AS CNT_TOTAL " + "\n");

                    strCnt1 = cdvOper.getDecodeQuery("SUM(DECODE(A.RES_STS_8", "1,0))", "C").Replace(", SUM(DECODE", "             , SUM(DECODE");

                    strSqlString.AppendFormat("{0} ", strCnt1);

                    //06시:HYNIX기준, 22시:SUMSUNG기준  
                    strSqlString.AppendFormat("          FROM MRASRESDEF A, MWIPMATDEF B " + "\n");
                    strSqlString.AppendFormat("         WHERE A.FACTORY = '{0}' " + "\n", CmnFunction.Trim(cdvFactory.Text));
                    strSqlString.AppendFormat("           AND A.RES_STS_8 IN (" + cdvOper.getInQuery() + ")" + "\n");
                    strSqlString.AppendFormat("           AND A.DELETE_FLAG = ' '   " + "\n");
                    strSqlString.AppendFormat("           AND A.RES_STS_2 = B.MAT_ID  " + "\n");
                    strSqlString.AppendFormat("           AND B.DELETE_FLAG = ' ' " + "\n");
                    if (cdvFactory.Text.Trim() == "HMKB1")
                        strSqlString.AppendFormat("           AND B.MAT_TYPE = 'FG'  " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (cdvFactory.Text.Trim() != "HMKB1")
                    {
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
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                    }
                    else
                    {
                        if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                        if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                        if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                        if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                        if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                        if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                        if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                        if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                        if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                        if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                        if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                        if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                    }
                    #endregion

                    strSqlString.AppendFormat("         GROUP BY A.RES_STS_2 " + "\n");

                    //수량과 실적이 없는 PRODUCT는 출력하지 않음.
                    if (chkViewAO.Checked == true)
                    {
                        strSqlString.AppendFormat("         HAVING (ROUND(COUNT(A.RES_ID)/" + sKpcsValue + ",0)) > 0     " + "\n");                       
                    }
                    else
                    {
                        strSqlString.AppendFormat("         HAVING COUNT(A.RES_ID) > 0     " + "\n");
                    }
                    strSqlString.AppendFormat("       ) RES " + "\n");

                    // 실적조회 체크 시 
                    if (chkViewAO.Checked == true)
                    {                       
                        strSqlString.AppendFormat("     , ( " + "\n");
                        strSqlString.AppendFormat("        SELECT A.MAT_ID " + "\n");
                        strSqlString.AppendFormat("             , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS DAO  " + "\n");
                        strSqlString.AppendFormat("          FROM " + summeryTable + " A, CLOTCRDDAT@RPTTOMES C   " + "\n");
                        strSqlString.AppendFormat("         WHERE A.CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                        strSqlString.AppendFormat("           AND A.MAT_ID = C.MAT_ID " + "\n");
                        strSqlString.AppendFormat("           AND A.CM_KEY_1 = C.FACTORY " + "\n");
                        strSqlString.AppendFormat("           AND WORK_DATE = GET_WORK_DATE('" + cutOffTime + "', 'D') " + "\n");
                        strSqlString.AppendFormat("           AND A.FACTORY NOT IN ('RETURN')" + "\n");
                        strSqlString.AppendFormat("         GROUP BY A.MAT_ID, WORK_MONTH, WORK_WEEK, WORK_DATE  " + "\n");
                        strSqlString.AppendFormat("       ) SHP " + "\n");
                    }

                    strSqlString.AppendFormat("     , MWIPMATDEF MAT" + "\n");
                    strSqlString.AppendFormat("  WHERE MAT.MAT_ID = WIP.MAT_ID(+) " + "\n");
                    strSqlString.AppendFormat("    AND MAT.MAT_ID = RES.MAT_ID(+) " + "\n");

                    // 실적조회 체크 시 
                    if (chkViewAO.Checked == true)
                    {
                        strSqlString.AppendFormat("AND MAT.MAT_ID = SHP.MAT_ID(+) " + "\n");
                    }

                    strSqlString.AppendFormat("    AND MAT.FACTORY = '{0}' " + "\n", CmnFunction.Trim(cdvFactory.Text));
                    strSqlString.AppendFormat("    AND MAT.MAT_VER = 1  " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (cdvFactory.Text.Trim() != "HMKB1")
                    {
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
                    }
                    else
                    {
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
                    }
                    #endregion

                    strSqlString.AppendFormat("  GROUP BY {0}" + "\n", QueryCond3);

                    //수량과 실적이 없는 PRODUCT는 출력하지 않음.
                    if (chkViewAO.Checked == true)
                    {                        
                        strSqlString.AppendFormat(" HAVING(ROUND(SUM(NVL(TOTAL, 0))/" + sKpcsValue + ",0) + TRUNC(SUM(NVL(DAO, 0))/" + sKpcsValue + ",0) + TRUNC(SUM(NVL(CNT_TOTAL, 0))/" + sKpcsValue + ",0)) > 0     " + "\n");                       
                    }
                    else
                    {
                        strSqlString.AppendFormat(" HAVING SUM(NVL(TOTAL, 0)) + SUM(NVL(CNT_TOTAL, 0)) > 0     " + "\n");
                    }

                    strSqlString.AppendFormat("  ORDER BY {0}" + "\n", QueryCond5);

                }

                else    //과거 시점 조회 기준
                {
                    strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond4);
                    strSqlString.AppendFormat("     , ROUND(SUM(NVL(TOTAL, 0))/" + sKpcsValue + ",0) AS TOTAL " + "\n");
                    strSqlString.AppendFormat("     , ROUND(SUM(NVL(CNT_TOTAL, 0))/" + sKpcsValue + ",0) AS CNT_TOTAL " + "\n");

                    ListView Oper = new ListView();
                    Oper = cdvOper.getItemsFromToValue();
                    for (int i = 0; i < Oper.Items.Count; i++)
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(NVL(V" + i + ", 0))/" + sKpcsValue + ", 0) AS Val" + i + " " + "\n");
                        strSqlString.AppendFormat("     , ROUND(SUM(NVL(C" + i + ", 0))/" + sKpcsValue + ", 0) AS Cnt" + i + " " + "\n");
                    }

                    strSqlString.AppendFormat("{0} ", strVal1);
                    strSqlString.AppendFormat("{0} ", strCnt1);
                   

                    //ASSY OUT 실적체크 하면 월/주/일별 AO출력
                    if (chkViewAO.Checked == true)
                    {                       
                        strSqlString.AppendFormat("     , TRUNC(SUM(NVL(DAO, 0))/" + sKpcsValue + ",0) AS DAO " + "\n");                       
                    }

                    strSqlString.AppendFormat("  FROM ( " + "\n");
                    strSqlString.AppendFormat("       SELECT A.MAT_ID, C.EMC_1 " + "\n");
                    strSqlString.AppendFormat("            , SUM(QTY_1) AS TOTAL " + "\n");
                    strVal1 = cdvOper.getDecodeQuery("SUM(DECODE(A.OPER", "QTY_1,0))", "V").Replace(", SUM(DECODE", "            , SUM(DECODE");
                                        
                    strSqlString.AppendFormat("{0} ", strVal1);
                    strSqlString.AppendFormat("          FROM RWIPLOTSTS_BOH A, CLOTCRDDAT@RPTTOMES C " + "\n");
                    strSqlString.AppendFormat("         WHERE A.FACTORY = '{0}' " + "\n", CmnFunction.Trim(cdvFactory.Text));
                    
                    // Lot Type
                    if (cdvLotType.Text != "ALL")
                        strSqlString.Append("           AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "' \n");

                    // 2013-09-02-임종우 : Qual 구분 검색 조건 추가
                    if (cdvQual.Text != "ALL" && cdvQual.Text.Trim() != "")
                        strSqlString.Append("           AND A.LOT_CMF_17 " + cdvQual.SelectedValueToQueryString.Replace("''", "' '") + " \n");

                    strSqlString.AppendFormat("           AND A.OWNER_CODE = 'PROD'" + "\n");
                    strSqlString.AppendFormat("           AND A.CUTOFF_DT = '" + cutOffDate + "' " + "\n");
                    strSqlString.AppendFormat("           AND A.OPER BETWEEN '" + cdvOper.FromText.ToString() + "' AND '" + cdvOper.ToText.ToString() + "' " + "\n");
                    strSqlString.AppendFormat("           AND A.FACTORY = C.FACTORY(+) " + "\n");
                    strSqlString.AppendFormat("           AND A.MAT_ID = C.MAT_ID(+) " + "\n");
                    strSqlString.AppendFormat("           AND A.LOT_DEL_FLAG = ' ' " + "\n");
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");
                    
                    if (cboHolddiv.Text == "Hold")
                        strSqlString.AppendFormat("           AND A.HOLD_FLAG = 'Y' " + "\n");
                    else if (cboHolddiv.Text == "Non Hold")
                        strSqlString.AppendFormat("           AND A.HOLD_FLAG = ' ' " + "\n");

                    strSqlString.AppendFormat("         GROUP BY A.MAT_ID, C.EMC_1 " + "\n");
                    strSqlString.AppendFormat("       ) WIP " + "\n");                    
                    strSqlString.AppendFormat("     , ( " + "\n");
                    strSqlString.AppendFormat("        SELECT A.RES_STS_2 AS MAT_ID, COUNT(A.RES_ID) AS CNT_TOTAL " + "\n");

                    strCnt1 = cdvOper.getDecodeQuery("SUM(DECODE(A.RES_STS_8", "1,0))", "C").Replace(", SUM(DECODE", "            , SUM(DECODE");

                    strSqlString.AppendFormat("{0} ", strCnt1);

                    //06시:HYNIX기준, 22시:SUMSUNG기준 

                    strSqlString.AppendFormat("          FROM MRASRESDEF A, MWIPMATDEF B " + "\n");
                    strSqlString.AppendFormat("         WHERE A.FACTORY = '{0}' " + "\n", CmnFunction.Trim(cdvFactory.Text));
                    strSqlString.AppendFormat("           AND A.RES_STS_8 IN (" + cdvOper.getInQuery() + ")" + "\n");
                    strSqlString.AppendFormat("           AND A.DELETE_FLAG = ' '   " + "\n");
                    strSqlString.AppendFormat("           AND A.RES_STS_2 = B.MAT_ID  " + "\n");
                    strSqlString.AppendFormat("           AND B.DELETE_FLAG = ' ' " + "\n");
                    if (cdvFactory.Text.Trim() == "HMKB1")
                        strSqlString.AppendFormat("           AND B.MAT_TYPE = 'FG'  " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (cdvFactory.Text.Trim() != "HMKB1")
                    {
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
                            strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                    }
                    else
                    {
                        if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                        if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                        if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                        if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                        if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                        if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                        if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                        if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                        if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                        if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                        if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                        if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                    }
                    #endregion

                    strSqlString.AppendFormat("         GROUP BY A.RES_STS_2 " + "\n");

                    //수량과 실적이 없는 PRODUCT는 출력하지 않음.
                    if (chkViewAO.Checked == true)
                    {
                        strSqlString.AppendFormat("     HAVING (ROUND(COUNT(A.RES_ID)/" + sKpcsValue + ",0)) > 0     " + "\n");                        
                    }
                    else
                    {
                        strSqlString.AppendFormat("     HAVING COUNT(A.RES_ID) > 0     " + "\n");
                    }

                    strSqlString.AppendFormat("       ) RES " + "\n");

                    // 실적조회 체크 시 
                    if (chkViewAO.Checked == true)
                    {
                        if (cdvFactory.Text.ToString() != GlobalVariable.gsTestDefaultFactory)
                        {
                            strSqlString.AppendFormat("     , (SELECT A.MAT_ID " + "\n");
                        }                        
                        
                        strSqlString.AppendFormat("             , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS DAO  " + "\n");
                        strSqlString.AppendFormat("          FROM " + summeryTable + " A, CLOTCRDDAT@RPTTOMES C   " + "\n");
                        strSqlString.AppendFormat("         WHERE A.CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                        strSqlString.AppendFormat("           AND A.MAT_ID = C.MAT_ID " + "\n");
                        strSqlString.AppendFormat("           AND A.CM_KEY_1 = C.FACTORY " + "\n");
                        strSqlString.AppendFormat("           AND A.MAT_ID = C.MAT_ID " + "\n");
                        strSqlString.AppendFormat("           AND A.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");
                        strSqlString.AppendFormat("           AND WORK_DATE = GET_WORK_DATE('" + cutOffTime + "', 'D') " + "\n");
                        strSqlString.AppendFormat("           AND A.FACTORY NOT IN ('RETURN')" + "\n");
                        strSqlString.AppendFormat("         GROUP BY A.MAT_ID, WORK_MONTH, WORK_WEEK, WORK_DATE  " + "\n");
                        strSqlString.AppendFormat("        ) SHP " + "\n");
                    }
                    
                    strSqlString.AppendFormat("     , MWIPMATDEF MAT" + "\n");

                    strSqlString.AppendFormat("  WHERE MAT.MAT_ID = WIP.MAT_ID(+) " + "\n");
                    strSqlString.AppendFormat("    AND MAT.MAT_ID = RES.MAT_ID(+) " + "\n");

                    // 실적조회 체크 시 
                    if (chkViewAO.Checked == true)
                    {
                        strSqlString.AppendFormat("AND MAT.MAT_ID = SHP.MAT_ID(+) " + "\n");
                    }

                    strSqlString.AppendFormat("    AND MAT.FACTORY = '{0}' " + "\n", CmnFunction.Trim(cdvFactory.Text));
                    strSqlString.AppendFormat("    AND MAT.MAT_VER = 1  " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (cdvFactory.Text.Trim() != "HMKB1")
                    {
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
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                    }
                    else
                    {
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
                    }
                    #endregion

                    strSqlString.AppendFormat("  GROUP BY {0}" + "\n", QueryCond3);

                    //수량과 실적이 없는 PRODUCT는 출력하지 않음.
                    if (chkViewAO.Checked == true)
                    {                        
                        strSqlString.AppendFormat("HAVING(ROUND(SUM(NVL(TOTAL, 0))/" + sKpcsValue + ",0) + TRUNC(SUM(NVL(DAO, 0))/" + sKpcsValue + ",0) + TRUNC(SUM(NVL(CNT_TOTAL, 0))/" + sKpcsValue + ",0)) > 0     " + "\n");                        
                    }
                    else
                    {
                        strSqlString.AppendFormat("HAVING SUM(NVL(TOTAL, 0)) + SUM(NVL(CNT_TOTAL, 0)) > 0     " + "\n");
                    }

                    strSqlString.AppendFormat("  ORDER BY {0}" + "\n", QueryCond5);

                }

            }
            #endregion

            #region 기본 조회시
            if (chkResCnt.Checked == false) //기존 (설비Arrange현황 체크 해제)
            {
                DataTable dt = null;

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(strDate));
                string This_Week_First_Day = dt.Rows[0][0].ToString(); // 금주 시작일
                string This_Week_Last_Day = dt.Rows[0][1].ToString(); // 금주 마지막 날

                
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);                                
                strSqlString.AppendFormat("     , ROUND(SUM(TOTAL)/" + sKpcsValue + ",0) AS TOTAL " + "\n");

                strVal1 = cdvOper.getMultyRepeatQuery("ROUND(SUM(V?)/" + sKpcsValue + ",0) AS ", "Val").Replace(", ROUND", "     , ROUND");
                strSqlString.AppendFormat("{0}", strVal1);
                
                if (cdvFactory.Text.ToString() == GlobalVariable.gsTestDefaultFactory)
                {
                    icntOperFromTo = cdvOper.CountFromToValue;
                    strSqlString.AppendFormat("     , ROUND(SUM(V" + icntOperFromTo + ")/" + sKpcsValue + ",0) AS Val" + icntOperFromTo + "\n");
                    icntOperFromTo += 1;
                    strSqlString.AppendFormat("     , ROUND(SUM(V" + icntOperFromTo + ")/" + sKpcsValue + ",0) AS Val" + icntOperFromTo + "\n");
                }

                strSqlString.AppendFormat("     , TRUNC(SUM(SHP_QTY)/" + sKpcsValue + ",0) AS SHP_QTY " + "\n");
                strSqlString.AppendFormat("     , ROUND(DECODE(TO_CHAR(TO_DATE('" + strDate + "','YYYYMMDD'),'D'), 2, SUM(NVL(PLN.PLAN_W1,0)), 3, SUM(NVL(PLN.PLAN_W1,0)), SUM(NVL(PLN.PLAN_W2,0)) + SUM(NVL(SHP1.ASSY_MONDAY,0)))/" + sKpcsValue + ",0) AS PLAN_W1" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(NVL(SHP1.ASSY_WEEK,0))/" + sKpcsValue + ",0)" + "\n");
                strSqlString.AppendFormat("     , ROUND((DECODE(TO_CHAR(TO_DATE('" + strDate + "','YYYYMMDD'),'D'), 2, SUM(NVL(PLN.PLAN_W1,0)), 3, SUM(NVL(PLN.PLAN_W1,0)), SUM(NVL(PLN.PLAN_W2,0)) + SUM(NVL(SHP1.ASSY_MONDAY,0)))) - SUM(NVL(SHP1.ASSY_WEEK,0))/" + sKpcsValue + ",0) AS LACK_WEEK" + "\n");
                                

                if (ckbMcpKit.Checked == true)
                {
                    strSqlString.AppendFormat("  FROM ( " + "\n");
                    strSqlString.AppendFormat("        SELECT DISTINCT FACTORY, DECODE(B.MAT_KEY, NULL, A.MAT_ID, B.MAT_KEY) AS MCP_TO_PART " + "\n");
                    strSqlString.AppendFormat("             , A.MAT_ID, A.MAT_GRP_10, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_CMF_10, A.MAT_CMF_11 " + "\n");
                    strSqlString.AppendFormat("          FROM MWIPMATDEF A " + "\n");
                    strSqlString.AppendFormat("             , HRTDMCPROUT@RPTTOMES B " + "\n");
                    strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
                    strSqlString.AppendFormat("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                    strSqlString.AppendFormat("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.AppendFormat("           AND A.DELETE_FLAG = ' ' " + "\n");
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE 'SEK%' " + "\n");
                    strSqlString.AppendFormat("       ) MAT" + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("  FROM MWIPMATDEF MAT " + "\n");
                }

                strSqlString.AppendFormat("     , ( " + "\n");
                strSqlString.AppendFormat("        SELECT A.MAT_ID, MAX(B.EMC_1) AS EMC_1 " + "\n");
                strSqlString.AppendFormat("             , SUM(QTY_1) AS TOTAL " + "\n");
                strSqlString.AppendFormat("             , MAX((SELECT ATTR_VALUE FROM MATRNAMSTS WHERE FACTORY = '" + cdvFactory.Text + "' AND ATTR_TYPE = 'MAT_ETC' AND ATTR_NAME = DECODE(LOT_CMF_2, 'SE', 'SEC_VERSION', 'HX', 'HX_VERSION') AND ATTR_KEY = A.MAT_ID)) AS VERSION " + "\n");

                strVal1 = cdvOper.getDecodeQuery("SUM(DECODE(A.OPER", "QTY_1,0))", "V").Replace(", SUM(DECODE", "             , SUM(DECODE");
                strSqlString.AppendFormat("{0}", strVal1);

                if (cdvFactory.Text.ToString() == GlobalVariable.gsTestDefaultFactory)
                {
                    icntOperFromTo = cdvOper.CountFromToValue;
                    strSqlString.AppendFormat("             , SUM(DECODE(OPER , 'F0000' , QTY_1,0)) AS V{0}" + "\n", icntOperFromTo);
                    icntOperFromTo += 1;
                    strSqlString.AppendFormat("             , SUM(DECODE(OPER , 'F000N' , QTY_1,0)) AS V{0}" + "\n", icntOperFromTo);
                }

                //금일조회일 경우 조회조건은 REALTIME
                //if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.Text == "22시")
                if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.SelectedIndex == 4)
                {
                    // 2011-09-26-임종우 : Repair & Return Stock 의 재공을 원래 공정에 재공 표시 되도록 추가 (임태성 요청)
                    if (ckbRepair.Checked == true)
                    {
                        strSqlString.AppendFormat("          FROM ( " + "\n");
                        strSqlString.AppendFormat("                SELECT FACTORY, LOT_TYPE, MAT_ID " + "\n");
                        strSqlString.AppendFormat("                     , DECODE(REP_FLAG, 'Y', REP_RET_OPER, OPER) AS OPER" + "\n");
                        strSqlString.AppendFormat("                     , QTY_1 " + "\n");
                        strSqlString.AppendFormat("                     , LOT_DEL_FLAG " + "\n");
                        strSqlString.AppendFormat("                     , LOT_CMF_2 " + "\n");
                        strSqlString.AppendFormat("                     , LOT_CMF_5 " + "\n");      // 2011-10-01-정비재 : LOT TYPE를 추가함.
                        strSqlString.AppendFormat("                     , LOT_CMF_17 " + "\n");
                        strSqlString.AppendFormat("                  FROM RWIPLOTSTS " + "\n");
                        strSqlString.AppendFormat("                 WHERE 1=1 " + "\n");
                        strSqlString.AppendFormat("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                        strSqlString.AppendFormat("                   AND LOT_DEL_FLAG = ' ' " + "\n");
                        strSqlString.AppendFormat("                   AND LOT_TYPE = 'W' " + "\n");
                        strSqlString.AppendFormat("                   AND NSTD_FLAG <> 'R' " + "\n");
                        strSqlString.AppendFormat("                 UNION ALL " + "\n");
                        strSqlString.AppendFormat("                SELECT FACTORY, LOT_TYPE, MAT_ID " + "\n");
                        strSqlString.AppendFormat("                     , ( " + "\n");
                        strSqlString.AppendFormat("                        SELECT OLD_OPER " + "\n");
                        strSqlString.AppendFormat("                          FROM RWIPLOTHIS " + "\n");
                        strSqlString.AppendFormat("                         WHERE 1=1 " + "\n");
                        strSqlString.AppendFormat("                           AND LOT_ID = A.LOT_ID " + "\n");
                        strSqlString.AppendFormat("                           AND HIST_SEQ = A.LAST_ACTIVE_HIST_SEQ " + "\n");
                        strSqlString.AppendFormat("                           AND HIST_DEL_FLAG = ' ' " + "\n");
                        strSqlString.AppendFormat("                       ) AS OPER " + "\n");
                        strSqlString.AppendFormat("                     , QTY_1 " + "\n");
                        strSqlString.AppendFormat("                     , LOT_DEL_FLAG " + "\n");
                        strSqlString.AppendFormat("                     , LOT_CMF_2 " + "\n");
                        strSqlString.AppendFormat("                     , LOT_CMF_5 " + "\n");      // 2011-10-01-정비재 : LOT TYPE를 추가함.
                        strSqlString.AppendFormat("                     , LOT_CMF_17 " + "\n");
                        strSqlString.AppendFormat("                  FROM RWIPLOTSTS A " + "\n");
                        strSqlString.AppendFormat("                 WHERE 1=1 " + "\n");
                        strSqlString.AppendFormat("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                        strSqlString.AppendFormat("                   AND LOT_DEL_FLAG = ' ' " + "\n");
                        strSqlString.AppendFormat("                   AND LOT_TYPE = 'W' " + "\n");
                        strSqlString.AppendFormat("                   AND NSTD_FLAG = 'R'" + "\n");
                        strSqlString.AppendFormat("               ) A " + "\n");
                        strSqlString.AppendFormat("             , CLOTCRDDAT@RPTTOMES B " + "\n");
                        strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("          FROM RWIPLOTSTS A, CLOTCRDDAT@RPTTOMES B " + "\n");
                        strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
                    }
                }
                else
                {
                    // 2011-09-26-임종우 : Repair & Return Stock 의 재공을 원래 공정에 재공 표시 되도록 추가 (임태성 요청)
                    if (ckbRepair.Checked == true)
                    {
                        strSqlString.AppendFormat("          FROM ( " + "\n");
                        strSqlString.AppendFormat("                SELECT FACTORY, LOT_TYPE, MAT_ID " + "\n");
                        strSqlString.AppendFormat("                     , DECODE(REP_FLAG, 'Y', REP_RET_OPER, OPER) AS OPER" + "\n");
                        strSqlString.AppendFormat("                     , QTY_1 " + "\n");
                        strSqlString.AppendFormat("                     , LOT_DEL_FLAG " + "\n");
                        strSqlString.AppendFormat("                     , LOT_CMF_2 " + "\n");
                        strSqlString.AppendFormat("                     , LOT_CMF_5 " + "\n");      // 2011-10-01-정비재 : LOT TYPE를 추가함.
                        strSqlString.AppendFormat("                     , LOT_CMF_17 " + "\n");
                        strSqlString.AppendFormat("                  FROM RWIPLOTSTS_BOH " + "\n");
                        strSqlString.AppendFormat("                 WHERE 1=1 " + "\n");
                        strSqlString.AppendFormat("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                        strSqlString.AppendFormat("                   AND CUTOFF_DT = '" + cutOffDate + "' " + "\n");
                        strSqlString.AppendFormat("                   AND LOT_DEL_FLAG = ' ' " + "\n");
                        strSqlString.AppendFormat("                   AND LOT_TYPE = 'W' " + "\n");
                        strSqlString.AppendFormat("                   AND NSTD_FLAG <> 'R' " + "\n");
                        strSqlString.AppendFormat("                 UNION ALL " + "\n");
                        strSqlString.AppendFormat("                SELECT FACTORY, LOT_TYPE, MAT_ID " + "\n");
                        strSqlString.AppendFormat("                     , ( " + "\n");
                        strSqlString.AppendFormat("                        SELECT OLD_OPER " + "\n");
                        strSqlString.AppendFormat("                          FROM RWIPLOTHIS " + "\n");
                        strSqlString.AppendFormat("                         WHERE 1=1 " + "\n");
                        strSqlString.AppendFormat("                           AND LOT_ID = A.LOT_ID " + "\n");
                        strSqlString.AppendFormat("                           AND HIST_SEQ = A.LAST_ACTIVE_HIST_SEQ " + "\n");
                        strSqlString.AppendFormat("                           AND HIST_DEL_FLAG = ' ' " + "\n");
                        strSqlString.AppendFormat("                       ) AS OPER " + "\n");
                        strSqlString.AppendFormat("                     , QTY_1 " + "\n");
                        strSqlString.AppendFormat("                     , LOT_DEL_FLAG " + "\n");
                        strSqlString.AppendFormat("                     , LOT_CMF_2 " + "\n");
                        strSqlString.AppendFormat("                     , LOT_CMF_5 " + "\n");      // 2011-10-01-정비재 : LOT TYPE를 추가함.
                        strSqlString.AppendFormat("                     , LOT_CMF_17 " + "\n");
                        strSqlString.AppendFormat("                  FROM RWIPLOTSTS_BOH A " + "\n");
                        strSqlString.AppendFormat("                 WHERE 1=1 " + "\n");
                        strSqlString.AppendFormat("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                        strSqlString.AppendFormat("                   AND CUTOFF_DT = '" + cutOffDate + "' " + "\n");
                        strSqlString.AppendFormat("                   AND LOT_DEL_FLAG = ' ' " + "\n");
                        strSqlString.AppendFormat("                   AND LOT_TYPE = 'W' " + "\n");
                        strSqlString.AppendFormat("                   AND NSTD_FLAG = 'R'" + "\n");
                        strSqlString.AppendFormat("               ) A " + "\n");
                        strSqlString.AppendFormat("             , CLOTCRDDAT@RPTTOMES B " + "\n");
                        strSqlString.AppendFormat("         WHERE 1=1 " + "\n");                        
                    }
                    else
                    {
                        strSqlString.AppendFormat("          FROM RWIPLOTSTS_BOH A, CLOTCRDDAT@RPTTOMES B " + "\n");
                        strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
                        strSqlString.AppendFormat("           AND A.CUTOFF_DT = '" + cutOffDate + "' " + "\n");
                    }
                }                
                                
                strSqlString.AppendFormat("           AND A.FACTORY = B.FACTORY(+) " + "\n");
                strSqlString.AppendFormat("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

                if (cdvFactory.Text.ToString() == GlobalVariable.gsTestDefaultFactory)
                {
                    strSqlString.AppendFormat("           AND A.FACTORY IN ('{0}','FGS') " + "\n", CmnFunction.Trim(cdvFactory.Text));
                    strSqlString.AppendFormat("           AND (A.OPER BETWEEN '" + cdvOper.FromText.ToString() + "' AND '" + cdvOper.ToText.ToString() + "' OR OPER IN ('F0000','F000N'))" + "\n");
                    //strSqlString.AppendFormat("           AND A.HOLD_CODE <> 'H72' " + "\n"); // 2011-05-19-임종우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 (김권수 요청)
                }
                else
                {
                    strSqlString.AppendFormat("           AND A.FACTORY = '{0}' " + "\n", CmnFunction.Trim(cdvFactory.Text));
                    strSqlString.AppendFormat("           AND A.OPER BETWEEN '" + cdvOper.FromText.ToString() + "' AND '" + cdvOper.ToText.ToString() + "' " + "\n");
                }
                                    
                // Lot Type
                if (cdvLotType.Text != "ALL")
                    strSqlString.Append("           AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "' \n");

                // 2013-09-02-임종우 : Qual 구분 검색 조건 추가
                if (cdvQual.Text != "ALL" && cdvQual.Text.Trim() != "")
                    strSqlString.Append("           AND A.LOT_CMF_17 " + cdvQual.SelectedValueToQueryString.Replace("''", "' '") + " \n");

                // HOLD 구분
                if (cboHolddiv.Text == "Hold")
                    strSqlString.AppendFormat("           AND A.HOLD_FLAG = 'Y' " + "\n");
                else if (cboHolddiv.Text == "Non Hold")
                    strSqlString.AppendFormat("           AND A.HOLD_FLAG = ' ' " + "\n");

                strSqlString.AppendFormat("         GROUP BY A.MAT_ID " + "\n");
                strSqlString.AppendFormat("       ) WIP " + "\n");
                strSqlString.AppendFormat("     , ( " + "\n");
                strSqlString.AppendFormat("        SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("             , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS SHP_QTY  " + "\n");
                strSqlString.AppendFormat("          FROM " + summeryTable + "\n");
                strSqlString.AppendFormat("         WHERE CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");                
                strSqlString.AppendFormat("           AND MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");
                strSqlString.AppendFormat("           AND WORK_DATE = '" + strDate + "'" + "\n");
                strSqlString.AppendFormat("           AND FACTORY NOT IN ('RETURN')" + "\n");
                strSqlString.AppendFormat("           AND LOT_TYPE = 'W'" + "\n");
                strSqlString.AppendFormat("         GROUP BY MAT_ID " + "\n");
                strSqlString.AppendFormat("       ) SHP " + "\n");
                strSqlString.AppendFormat("     , ( " + "\n");
                strSqlString.AppendFormat("        SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("             , SUM(DECODE(WORK_DATE, '" + This_Week_First_Day + "', NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0), 0)) AS ASSY_MONDAY  " + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0)) AS ASSY_WEEK " + "\n");
                strSqlString.AppendFormat("          FROM RSUMFACMOV " + "\n");
                strSqlString.AppendFormat("         WHERE CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.AppendFormat("           AND LOT_TYPE = 'W'" + "\n");
                strSqlString.AppendFormat("           AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.AppendFormat("           AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.AppendFormat("           AND MAT_ID LIKE 'SES%' " + "\n");
                strSqlString.AppendFormat("           AND WORK_DATE BETWEEN '" + This_Week_First_Day + "' AND '" + This_Week_Last_Day + "'" + "\n");                
                strSqlString.AppendFormat("         GROUP BY MAT_ID " + "\n");
                strSqlString.AppendFormat("       ) SHP1 " + "\n");
                strSqlString.AppendFormat("     , ( " + "\n");
                strSqlString.AppendFormat("        SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("             , SUM(PLAN_QTY) PLAN_W1 " + "\n");
                strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DAY, '" + This_Week_First_Day + "', 0, PLAN_QTY)) AS PLAN_W2 " + "\n");
                strSqlString.AppendFormat("          FROM ( " + "\n");
                strSqlString.AppendFormat("                SELECT * " + "\n");
                strSqlString.AppendFormat("                  FROM CWIPPLNDAY " + "\n");
                strSqlString.AppendFormat("                 WHERE 1=1 " + "\n");
                strSqlString.AppendFormat("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.AppendFormat("                   AND PLAN_DAY BETWEEN '" + This_Week_First_Day + "' AND '" + This_Week_Last_Day + "'" + "\n");

                if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
                {
                    strSqlString.AppendFormat("                   AND IN_OUT_FLAG = 'OUT'" + "\n");
                    strSqlString.AppendFormat("                   AND CLASS = 'ASSY'" + "\n");
                }

                if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
                {
                    strSqlString.AppendFormat("                   AND IN_OUT_FLAG = 'IN'" + "\n");
                    strSqlString.AppendFormat("                   AND CLASS = 'SLIS'" + "\n");
                }

                strSqlString.AppendFormat("               ) B" + "\n");
                strSqlString.AppendFormat("         GROUP BY MAT_ID " + "\n");
                strSqlString.AppendFormat("       ) PLN " + "\n"); 
                strSqlString.AppendFormat(" WHERE 1=1 " + "\n");
                strSqlString.AppendFormat("   AND MAT.MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ') " + "\n");
                strSqlString.AppendFormat("   AND MAT.MAT_ID = WIP.MAT_ID(+) " + "\n");
                strSqlString.AppendFormat("   AND MAT.MAT_ID = SHP.MAT_ID(+) " + "\n");
                strSqlString.AppendFormat("   AND MAT.MAT_ID = SHP1.MAT_ID(+) " + "\n");
                strSqlString.AppendFormat("   AND MAT.MAT_ID = PLN.MAT_ID(+) " + "\n");
                strSqlString.AppendFormat("   AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (cdvFactory.Text.Trim() != "HMKB1")
                {
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
                }
                else
                {
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
                }
                #endregion

                strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond2);
                strSqlString.AppendFormat("HAVING (NVL(SUM(TOTAL),0) + NVL(SUM(SHP_QTY),0)  + NVL(SUM(ASSY_WEEK),0) + NVL(SUM(PLAN_W1),0)) > 0     " + "\n");
                strSqlString.AppendFormat(" ORDER BY {0} ", QueryCond2);
            }
            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion MakeSqlString

        #region 금주 시작일, 종료일 구하기
        private string MakeSqlString2(string strDate)   // 현재 주차의 첫째날,마지막날과 다음주차의 마지막날의 가져옴
        {
            StringBuilder strSqlString = new StringBuilder();
            
            strSqlString.Append("SELECT MIN(SYS_DATE) AS This_Week_First_Day " + "\n");
            strSqlString.Append("     , MAX(SYS_DATE) AS This_Week_Last_Day" + "\n");            
            strSqlString.Append("  FROM MWIPCALDEF" + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND CALENDAR_ID = 'SE'" + "\n");
            strSqlString.Append("   AND (PLAN_YEAR,PLAN_WEEK) IN (" + "\n");
            strSqlString.Append("                                 SELECT PLAN_YEAR,PLAN_WEEK " + "\n");
            strSqlString.Append("                                   FROM MWIPCALDEF " + "\n");
            strSqlString.Append("                                  WHERE 1=1" + "\n");
            strSqlString.Append("                                    AND CALENDAR_ID = 'SE'" + "\n");            
            strSqlString.Append("                                    AND SYS_DATE = '" + strDate + "')" + "\n");

            return strSqlString.ToString();
        }

        #endregion 금주 시작일, 종료일 구하기

        #endregion 조회

        #region ToExcel

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, false);
            //spdData.ExportExcel();           
        }
        #endregion

        //Factory 변경시 각각의 상속받은 Class의 Factory값들도 함께 변경
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            this.cdvOper.sFactory = cdvFactory.txtValue;

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;

                ckbKpcs.Checked = false;

                ckbMcpKit.Checked = false;
                ckbMcpKit.Enabled = false;
            }
            else
            {
                // 2011-04-14-배수민 : HMKA1만 설비Arrange현황 조회 할 수 있게...
                if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                {
                    chkResCnt.Enabled = true;
                                        
                    ckbMcpKit.Enabled = true;
                }
                else
                {
                    chkResCnt.Enabled = false;

                    ckbMcpKit.Checked = false;
                    ckbMcpKit.Enabled = false;
                }

                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;
            }

            SortInit();
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader && e.Column > 11 && e.Column < 12 + cdvOper.CountFromToValue)
            {
                StringBuilder strSqlString = new StringBuilder();
                DataTable dtOperDesc = null;

                strSqlString.Append("        select oper_desc from MWIPoprdef where oper = '" + spdData.ActiveSheet.Columns[e.Column].Label + "' ");
                strSqlString.Append("        AND ROWNUM = 1  " + "\n");

                dtOperDesc = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

                ToolTip tipOperDesc = new ToolTip();
                tipOperDesc.Show(dtOperDesc.Rows[0][0].ToString(), this.spdData, e.X, e.Y, 600);
            }
        }

        private void cboTimeBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboTimeBase.Text == "00시" || cboTimeBase.Text == "04시")
            if (cboTimeBase.SelectedIndex == 0 || cboTimeBase.SelectedIndex == 1)
                lblNotice.Visible = true;
            else
                lblNotice.Visible = false;
        }

        private void ckbMcpKit_CheckedChanged(object sender, EventArgs e)
        {
            SortInit();
        }
    }
}
