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
    public partial class PRD010410 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010410<br/>
        /// 클래스요약: 삼성 MCP 결산<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-12-15<br/>
        /// 상세  설명: 삼성 MCP 결산 전용 조회 화면<br/>
        /// 변경  내용: <br/>     
        /// 2011-02-01-임종우 : YIELD 로직 오류로 인한 수정.
        /// 2012-05-15-임종우 : MCP, DDP, QDP PKG 분리 작업
        /// 2013-03-25-임종우 : ACT_IN_QTY -> PC_IN_QTY 명칭 변경, 공손에서 Combine 수량 별도로 분리, Yield 로직 수정 (문영만 요청)
        /// 2013-04-01-임종우 : PC_IN_QTY - 기존 1차칩 : 마지막 칩 수 -> 2차 칩 수 로 변경 (문영만 요청)
        /// 2013-07-25-임종우 : PC_IN_QTY : Merge Part 만 표시 , PC_OUT_QTY 추가 (문영만 요청)
        /// 2013-07-26-임종우 : 데이터 검증을 위해 기존 Act-In 데이터 표시 (문영만 요청)
        /// 2014-07-21-임종우 : 삼성 MCP COMBINE 룰에 의한 데이터 정렬 기능 추가 (임태성K 요청)
        /// 2015-07-21-임종우 : 신규 Summary 데이터 검색 기능 추가
        /// 2015-07-27-임종우 : 금일일자 이지만 22시가 넘었을때는 RWIPLOTSTS_BOH 테이블을 보도록 수정
        /// 2015-08-10-임종우 : 신규 Part Chang 데이터 검색 기능 추가
        /// 2015-09-18-임종우 : EOH 업로드 데이터 검색 기능 추가
        /// 2015-11-25-임종우 : 재공 실사 검색 기능 추가 
        /// 2016-11-23-임종우 : EOH 업로드 Aging 표시 유무 기능 추가
        /// 2016-12-14-임종우 : OMS 업로드용 최종 결삼 데이터 조회 기능 추가
        /// 2017-12-12-임종우 : Combine Relation 정보 변경 - HRTDMCPROUT -> RWIPMCPBOM 테이블로 변경
        /// </summary>
        /// 
        public PRD010410()
        {
            InitializeComponent();

            cdvFromToDate.DaySelector.SelectedValue = "MONTH";
            cdvFromToDate.AutoBindingUserSetting(DateTime.Now.ToString("yyyy-MM") + "-01", DateTime.Now.ToString("yyyy-MM-dd"));  
            //cdvFromToDate.AutoBinding(DateTime.Now.ToString("yyyy-MM") + "-01", DateTime.Now.ToString("yyyy-MM-dd"));
            cdvFromToDate.ToYearMonth.Visible = false;            
            cdvFromToDate.DaySelector.Enabled = false;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
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

                //if (cdvType.Text == "Summary")
                if (cdvType.SelectedIndex == 3)
                {
                    spdData.RPT_AddBasicColumn("PACK CODE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MERGE PART", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG CODE", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TYPE1", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("TYPE2", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Basic stock", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("input", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("RECEIVE", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PART OUT", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PART IN", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("TTL", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(0, 8, 4);

                    spdData.RPT_AddBasicColumn("Closing stock", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PC-IN", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PC-OUT", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Production Quantity", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("LOSS_REAL", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("LOSS_LOGIC", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Transfer", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Input Return", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Warehousing return", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                    
                    spdData.RPT_AddBasicColumn("BILL QTY", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("YIELD", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
                    spdData.RPT_AddBasicColumn("TTL DIF", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    for (int i = 0; i <= 23; i++)
                    {
                        if (i < 8 || i > 11)
                        {
                            spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                        }
                    }
                }
                //else if (cdvType.Text == "Part Change")
                else if (cdvType.SelectedIndex == 2)
                {
                    spdData.RPT_AddBasicColumn("OUT_PART_NO", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 150);
                    spdData.RPT_AddBasicColumn("OUT_QTY", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("IN_PART_NO", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 150);
                    spdData.RPT_AddBasicColumn("IN_QTY", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);                    
                }
                //else if (cdvType.Text == "EOH Upload")
                else if (cdvType.SelectedIndex == 1)
                {
                    spdData.RPT_AddBasicColumn("SITE", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("OEM_CODE", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("CUT_OFF", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PRODUCT_CODE", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("AREA", 0, 4, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LOT_TYPE", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RETURN_TYPE", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("WAFER_QTY", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("CHIP_QTY", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("PRODUCT_NAME", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);

                    if (ckdAging.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("AGING", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("AGING", 0, 10, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    }
                }
                //else if (cdvType.Text == "OMS Upload")
                else if (cdvType.SelectedIndex == 4)
                {
                    spdData.RPT_AddBasicColumn("SITE", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("OEM_CODE", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("CUT_OFF", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);                    
                    spdData.RPT_AddBasicColumn("AREA", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PC_OUT_PART", 0, 4, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("PC_OUT_LOT_TYPE", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PC_OUT_RETURN_TYPE", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PC_OUT_QTY", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("PC_IN_PART", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("PC_IN_LOT_TYPE", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PC_IN_RETURN_TYPE", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PC_IN_QTY", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);                    
                    spdData.RPT_AddBasicColumn("Type", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Sort", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                }
                //else if (cdvType.Text == "재공 실사")
                else if (cdvType.SelectedIndex == 0)
                {
                    spdData.RPT_AddBasicColumn("Business code", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("Initial input date", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("AREA", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RUN NO", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LOT NO", 0, 4, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Quantity", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("CHIP Quantity", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Operation", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("STEP IN TIME", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Operation congestion days", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                    spdData.RPT_AddBasicColumn("Total number of days of congestion", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                    spdData.RPT_AddBasicColumn("Reason for congestion", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 150);
                    spdData.RPT_AddBasicColumn("note", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                }
                else
                {
                    //if (cdvType.Text == "Combine Rule")
                    //{
                    //    spdData.RPT_AddBasicColumn("MERGE PART", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                    //}
                    //else
                    //{
                    spdData.RPT_AddBasicColumn("MERGE PART", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                    //}

                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PACK CODE", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("TYPE1", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("TYPE2", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Basic stock", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Input quantity", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Closing stock", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PC-IN", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PC-OUT", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Production Quantity", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Operation loss (LOSS)", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Transfer", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Input Return", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Warehousing return", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("BILL QTY", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("YIELD", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
                    spdData.RPT_AddBasicColumn("ACT-IN", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                }

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "A.MAT_CMF_10", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACK CODE", "MAT_CMF_11", "A.MAT_CMF_11", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "A.MAT_ID", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "A.MAT_GRP_4", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "A.MAT_GRP_5", true);
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

            string QueryCond1;
            string QueryCond2;
            string sStart_Tran_Time = null;
            string sEnd_Tran_Time = null;
            string yesterday = null;
            string sMonth = null;

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                sMonth = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
            }
            else
            {
                DateTime fromdate = DateTime.Parse(cdvFromToDate.FromDate.Text);
                yesterday = fromdate.AddDays(-1).ToString("yyyyMMdd");

                sStart_Tran_Time = cdvFromToDate.ExactFromDate;
                sEnd_Tran_Time = cdvFromToDate.ExactToDate;
            }

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            //if (cdvType.Text == "Summary")
            if (cdvType.SelectedIndex == 3)
            {
                #region 0.Summary (신규)
                strSqlString.Append("SELECT PKG_CODE AS PACK_CODE, SUBSTR(MCP_TO_PART, 3) AS MCP_TO_PART, PIN_TYPE, SUBSTR(MAT_ID, 3) AS MAT_ID, PKG_CODE, TYPE1, TYPE2 " + "\n");
                strSqlString.Append("     , BOH_QTY, RECEIVE_QTY, PART_OUT, PART_IN, IN_TTL_QTY, EOH_QTY " + "\n");
                strSqlString.Append("     , PC_IN_QTY, PC_OUT_QTY, SHIP_QTY " + "\n");
                strSqlString.Append("     , LOSS_QTY_REAL AS LOSS_REAL" + "\n");
                strSqlString.Append("     , LOSS_QTY_LOGIC AS LOSS_LOGIC " + "\n");
                strSqlString.Append("     , TRANSFER_QTY, RT_WAFER_QTY, RT_IN_QTY " + "\n");
                strSqlString.Append("     , NVL(SHIP_QTY,0) - NVL(RT_IN_QTY,0) AS BILL_QTY" + "\n");
                strSqlString.Append("     , YIELD " + "\n");
                strSqlString.Append("     , NVL(BOH_QTY,0) + NVL(IN_TTL_QTY,0) + NVL(PC_IN_QTY,0) + NVL(RT_IN_QTY,0) - NVL(EOH_QTY,0) - NVL(PC_OUT_QTY,0) - NVL(SHIP_QTY,0) - NVL(LOSS_QTY_REAL,0) - NVL(TRANSFER_QTY,0) - NVL(RT_WAFER_QTY,0) AS TTL_DIF " + "\n");                                
                strSqlString.Append("  FROM RSUMSECMCP" + "\n");
                strSqlString.Append(" WHERE 1=1  " + "\n");
                strSqlString.Append("   AND WORK_MONTH = '" + sMonth + "' " + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("   AND LOT_TYPE = '" + cbLotType.Text + "' " + "\n");
                }

                if (txtSearchProduct.Text != "" && txtSearchProduct.Text != "%")
                {
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' \n");
                }

                strSqlString.Append("   AND MAT_ID NOT LIKE 'SEKY%' " + "\n");
                //strSqlString.Append("   AND (MAT_ID NOT LIKE 'SEKY%' OR TYPE2 = '1st') " + "\n");
                strSqlString.Append(" ORDER BY PACK_CODE, MCP_TO_PART, DECODE(TYPE2,'1st', 1, '2nd', 2, 'Middle', 3, '3rd', 4, 'Middle 1', 5, '4th', 6, 'Middle 2', 7, '5th', 8, 'Middle 3', 9, '6th', 10, 'Middle 4', 11, '7th', 12, 'Middle 5', 13, '8th', 14, 'Middle 6', 15, '9th', 16, 'Merge', 17), PIN_TYPE, MAT_ID, TYPE1" + "\n");
                
                #endregion
            }
            //else if (cdvType.Text == "Part Change")
            else if (cdvType.SelectedIndex == 2)
            {
                #region 1.Part Change (신규)
                strSqlString.Append("WITH DT AS " + "\n");
                strSqlString.Append("( " + "\n");

                strSqlString.Append("SELECT LOT_ID, HIST_SEQ, OLD_HIST_SEQ, MAT_ID, OLD_MAT_ID, SUBSTR(LOT_CMF_5, 1, 1) AS LOT_CMF_5, OPER, QTY_1" + "\n");
                strSqlString.Append("  FROM MWIPADTHIS@RPTTOMES" + "\n");
                strSqlString.Append(" WHERE LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("   AND TRAN_CODE = 'ADAPT'" + "\n");
                strSqlString.Append("   AND TRAN_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "' " + "\n");                
                strSqlString.Append("   AND MAT_ID LIKE 'SE%'" + "\n");
                strSqlString.Append("   AND TRAN_CMF_5 = 'ADAPT-PART_CHANGE'" + "\n");
                strSqlString.Append("   AND HIST_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("   AND OPER LIKE 'A%'" + "\n");
                strSqlString.Append("   AND MAT_ID NOT LIKE 'SEKY%'" + "\n");
                strSqlString.Append(")" + "\n");
                strSqlString.Append("SELECT SUBSTR(OLD_MAT_ID, 3) AS OUT_PART_NO, SUM(QTY_1) AS OUT_QTY" + "\n");
                strSqlString.Append("     , SUBSTR(MAT_ID, 3) AS IN_PART_NO, SUM(QTY_1) AS IN_QTY" + "\n");                
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.*, B.LOT_ID AS B_LOT_ID" + "\n");
                strSqlString.Append("          FROM DT A" + "\n");
                strSqlString.Append("             , MWIPLOTCMB@RPTTOMES B" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.LOT_ID = B.LOT_ID(+)" + "\n");
                strSqlString.Append("           AND A.OLD_HIST_SEQ = B.HIST_SEQ(+)" + "\n");
                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND B_LOT_ID IS NULL" + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("   AND LOT_CMF_5 LIKE '" + cbLotType.Text + "' " + "\n");                    
                }

                if (txtSearchProduct.Text != "" && txtSearchProduct.Text != "%")
                {
                    strSqlString.Append("   AND OLD_MAT_ID LIKE '" + txtSearchProduct.Text + "' \n");
                }

                strSqlString.Append(" GROUP BY OLD_MAT_ID, MAT_ID, LOT_CMF_5" + "\n");
                strSqlString.Append(" ORDER BY OLD_MAT_ID, MAT_ID, LOT_CMF_5" + "\n");                
                #endregion
            }
            //else if (cdvType.Text == "EOH Upload")
            else if (cdvType.SelectedIndex == 1)
            {
                #region 2.EOH Upload (신규)
                strSqlString.Append("SELECT SITE, OEM_CODE, WORK_MONTH AS CUT_OFF " + "\n");
                strSqlString.Append("     , SUBSTR(MAT_ID, 3) AS PRODUCT_CODE " + "\n");
                strSqlString.Append("     , AREA " + "\n");
                strSqlString.Append("     , SUBSTR(LOT_TYPE, 1, 1) AS LOT_TYPE" + "\n");
                strSqlString.Append("     , RETURN_TYPE " + "\n");

                if (ckdAging.Checked == true)
                {
                    strSqlString.Append("     , WAFER_QTY" + "\n");
                    strSqlString.Append("     , CHIP_QTY " + "\n");
                    strSqlString.Append("     , PRODUCT_NAME " + "\n");
                    strSqlString.Append("     , AGING " + "\n");
                }
                else
                {
                    strSqlString.Append("     , SUM(WAFER_QTY) AS WAFER_QTY" + "\n");
                    strSqlString.Append("     , SUM(CHIP_QTY) AS CHIP_QTY " + "\n");
                    strSqlString.Append("     , PRODUCT_NAME " + "\n");                    
                }

                strSqlString.Append("  FROM RSUMSECMCP_EOH" + "\n");
                strSqlString.Append(" WHERE 1=1  " + "\n");
                strSqlString.Append("   AND WORK_MONTH = '" + sMonth + "' " + "\n");
                strSqlString.Append("   AND MAT_ID NOT LIKE 'SEKY%' " + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("   AND LOT_TYPE = '" + cbLotType.Text + "' " + "\n");
                }

                if (txtSearchProduct.Text != "" && txtSearchProduct.Text != "%")
                {
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' \n");
                }

                if (ckdAging.Checked == true)
                {
                    strSqlString.Append(" ORDER BY SUBSTR(MAT_ID, -3), MAT_ID, AGING " + "\n");
                }
                else
                {
                    strSqlString.Append(" GROUP BY SITE, OEM_CODE, WORK_MONTH, MAT_ID, AREA, LOT_TYPE, RETURN_TYPE, PRODUCT_NAME " + "\n");
                    strSqlString.Append(" ORDER BY SUBSTR(MAT_ID, -3), MAT_ID " + "\n");
                }

                #endregion
            }
            //else if (cdvType.Text == "OMS Upload")
            else if (cdvType.SelectedIndex == 4)
            {
                #region 3.OMS Upload (신규)
                strSqlString.Append("SELECT SITE " + "\n");
                strSqlString.Append("     , OEM_CODE " + "\n");
                strSqlString.Append("     , WORK_MONTH AS CUT_OFF " + "\n");
                strSqlString.Append("     , AREA " + "\n");
                strSqlString.Append("     , SUBSTR(PC_OUT_PART, 3) AS PC_OUT_PART " + "\n");                
                strSqlString.Append("     , SUBSTR(PC_OUT_LOT_TYPE, 1, 1) AS PC_OUT_LOT_TYPE" + "\n");
                strSqlString.Append("     , PC_OUT_RETURN_TYPE " + "\n");
                strSqlString.Append("     , PC_OUT_QTY" + "\n");
                strSqlString.Append("     , SUBSTR(PC_IN_PART, 3) AS PC_IN_PART " + "\n");
                strSqlString.Append("     , SUBSTR(PC_IN_LOT_TYPE, 1, 1) AS PC_IN_LOT_TYPE" + "\n");
                strSqlString.Append("     , PC_IN_RETURN_TYPE " + "\n");
                strSqlString.Append("     , PC_IN_QTY" + "\n");
                strSqlString.Append("     , GUBUN_1" + "\n");
                strSqlString.Append("     , GUBUN_2" + "\n");
                strSqlString.Append("  FROM RSUMSECMCP_OMS" + "\n");
                strSqlString.Append(" WHERE 1=1  " + "\n");
                strSqlString.Append("   AND WORK_MONTH = '" + sMonth + "' " + "\n");                

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("   AND PC_OUT_LOT_TYPE = '" + cbLotType.Text + "' " + "\n");
                }

                if (txtSearchProduct.Text != "" && txtSearchProduct.Text != "%")
                {
                    strSqlString.Append("   AND PC_OUT_PART LIKE '" + txtSearchProduct.Text + "' \n");
                }

                strSqlString.Append(" ORDER BY GUBUN_1, GUBUN_2, PKG_CODE, DECODE(GUBUN_2, '1_PC', PC_OUT_PART, PC_IN_PART), SORT " + "\n");
               
                #endregion
            }
            else
            {
                #region 4.재공 실사 (신규)
                strSqlString.Append("SELECT SUBSTR(LOT.MAT_ID,3) AS PRODUCT" + "\n");
                strSqlString.Append("     , DECODE(LOT.LOT_CMF_14, ' ', ' ', TO_CHAR(TO_DATE(LOT.LOT_CMF_14, 'YYYYMMDDHH24MISS'), 'YYYYMMDD')) AS RECV_TIME" + "\n");
                strSqlString.Append("     , 'ASSY' AS AREA" + "\n");
                strSqlString.Append("     , LOT.LOT_CMF_4 AS RUN_NO" + "\n");
                strSqlString.Append("     , LOT.LOT_ID AS LOT_NO" + "\n");
                strSqlString.Append("     , LOT.QTY_1 AS PKG_QTY" + "\n");
                strSqlString.Append("     , CASE WHEN LOT.OPER <= 'A0395' THEN LOT.QTY_1 " + "\n");
                strSqlString.Append("            WHEN MAT.MAT_GRP_5 = 'Merge' THEN LOT.QTY_1" + "\n");
                strSqlString.Append("            WHEN MAT.MAT_GRP_5 = 'Middle' THEN LOT.QTY_1 * (2 - NVL(MIR.MIRROR_CNT,0))" + "\n");
                strSqlString.Append("            WHEN MAT.MAT_GRP_5 = 'Middle 1' THEN LOT.QTY_1 * (3 - NVL(MIR.MIRROR_CNT,0))" + "\n");
                strSqlString.Append("            WHEN MAT.MAT_GRP_5 = 'Middle 2' THEN LOT.QTY_1 * (4 - NVL(MIR.MIRROR_CNT,0))" + "\n");
                strSqlString.Append("            WHEN MAT.MAT_GRP_5 = 'Middle 3' THEN LOT.QTY_1 * (5 - NVL(MIR.MIRROR_CNT,0))" + "\n");
                strSqlString.Append("            WHEN MAT.MAT_GRP_5 = 'Middle 4' THEN LOT.QTY_1 * (6 - NVL(MIR.MIRROR_CNT,0))" + "\n");
                strSqlString.Append("            WHEN MAT.MAT_GRP_5 = 'Middle 5' THEN LOT.QTY_1 * (7 - NVL(MIR.MIRROR_CNT,0))" + "\n");
                strSqlString.Append("            WHEN MAT.MAT_GRP_5 = 'Middle 6' THEN LOT.QTY_1 * (8 - NVL(MIR.MIRROR_CNT,0))" + "\n");
                strSqlString.Append("            WHEN MAT.MAT_GRP_5 = 'Middle 7' THEN LOT.QTY_1 * (9 - NVL(MIR.MIRROR_CNT,0))" + "\n");
                strSqlString.Append("            WHEN MAT.MAT_GRP_5 = 'Middle 8' THEN LOT.QTY_1 * (10 - NVL(MIR.MIRROR_CNT,0))" + "\n");
                strSqlString.Append("            WHEN MAT.MAT_GRP_5 = 'Middle 9' THEN LOT.QTY_1 * (11 - NVL(MIR.MIRROR_CNT,0))" + "\n");
                strSqlString.Append("            ELSE LOT.QTY_1 * NVL(MAT.COMP_CNT,1)" + "\n");
                strSqlString.Append("       END CHIP_QTY" + "\n");
                strSqlString.Append("     , LOT.OPER || '(' || OPR.OPER_DESC || ')' AS STEP " + "\n");
                strSqlString.Append("     , DECODE(LOT.OPER_IN_TIME, ' ', ' ', TO_CHAR(TO_DATE(LOT.OPER_IN_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS')) AS STEP_IN_TIME  " + "\n");
                strSqlString.Append("     , TRUNC(TO_DATE('" + cdvFromToDate.HmFromDay + "220000', 'YYYYMMDDHH24MISS') - TO_DATE(NVL(TRIM(LOT.OPER_IN_TIME),'" + cdvFromToDate.HmFromDay + "220000'), 'YYYYMMDDHH24MISS'), 2) AS OPERINTIMEDIFF" + "\n");
                strSqlString.Append("     , TRUNC(TO_DATE('" + cdvFromToDate.HmFromDay + "220000', 'YYYYMMDDHH24MISS') - TO_DATE(LOT.LOT_CMF_14, 'YYYYMMDDHH24MISS'), 2) AS CREATETIMEDIFF" + "\n");
                strSqlString.Append("     , LOT.LAST_COMMENT" + "\n");
                strSqlString.Append("     , CASE WHEN MAT.MAT_GRP_5 LIKE 'Middle%' THEN 'Middle Part' ELSE ' ' END AS ETC" + "\n");
                strSqlString.Append("  FROM RWIPLOTSTS_BOH LOT" + "\n");
                strSqlString.Append("     , VWIPMATDEF MAT" + "\n");
                strSqlString.Append("     , MWIPOPRDEF OPR " + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT DISTINCT MAT_ID, MIRROR_CNT" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT A.* " + "\n");
                strSqlString.Append("                     , CASE WHEN MAT_GRP_5 = 'Middle' THEN SUM(CASE WHEN MAT_ID LIKE 'SEKY%' AND MAT_GRP_5 IN ('1st','2nd') THEN 1 ELSE 0 END) OVER(PARTITION BY MCP_TO_PART, MAT_CMF_11)" + "\n");
                strSqlString.Append("                            WHEN MAT_GRP_5 = 'Middle 1' THEN SUM(CASE WHEN MAT_ID LIKE 'SEKY%' AND MAT_GRP_5 IN ('1st','2nd','3rd') THEN 1 ELSE 0 END) OVER(PARTITION BY MCP_TO_PART, MAT_CMF_11)" + "\n");
                strSqlString.Append("                            WHEN MAT_GRP_5 = 'Middle 2' THEN SUM(CASE WHEN MAT_ID LIKE 'SEKY%' AND MAT_GRP_5 IN ('1st','2nd','3rd','4th') THEN 1 ELSE 0 END) OVER(PARTITION BY MCP_TO_PART, MAT_CMF_11)" + "\n");
                strSqlString.Append("                            WHEN MAT_GRP_5 = 'Middle 3' THEN SUM(CASE WHEN MAT_ID LIKE 'SEKY%' AND MAT_GRP_5 IN ('1st','2nd','3rd','4th','5th') THEN 1 ELSE 0 END) OVER(PARTITION BY MCP_TO_PART, MAT_CMF_11)" + "\n");
                strSqlString.Append("                            WHEN MAT_GRP_5 = 'Middle 4' THEN SUM(CASE WHEN MAT_ID LIKE 'SEKY%' AND MAT_GRP_5 IN ('1st','2nd','3rd','4th','5th','6th') THEN 1 ELSE 0 END) OVER(PARTITION BY MCP_TO_PART, MAT_CMF_11)" + "\n");
                strSqlString.Append("                            WHEN MAT_GRP_5 = 'Middle 5' THEN SUM(CASE WHEN MAT_ID LIKE 'SEKY%' AND MAT_GRP_5 IN ('1st','2nd','3rd','4th','5th','6th','7th') THEN 1 ELSE 0 END) OVER(PARTITION BY MCP_TO_PART, MAT_CMF_11)" + "\n");
                strSqlString.Append("                            WHEN MAT_GRP_5 = 'Middle 6' THEN SUM(CASE WHEN MAT_ID LIKE 'SEKY%' AND MAT_GRP_5 IN ('1st','2nd','3rd','4th','5th','6th','7th','8th') THEN 1 ELSE 0 END) OVER(PARTITION BY MCP_TO_PART, MAT_CMF_11)" + "\n");
                strSqlString.Append("                            WHEN MAT_GRP_5 = 'Middle 7' THEN SUM(CASE WHEN MAT_ID LIKE 'SEKY%' AND MAT_GRP_5 IN ('1st','2nd','3rd','4th','5th','6th','7th','8th','9th') THEN 1 ELSE 0 END) OVER(PARTITION BY MCP_TO_PART, MAT_CMF_11)" + "\n");
                strSqlString.Append("                            ELSE 0" + "\n");
                strSqlString.Append("                       END AS MIRROR_CNT " + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT DISTINCT A.MCP_TO_PART, A.MAT_ID, B.MAT_CMF_10, B.MAT_CMF_11, B.MAT_GRP_4, B.MAT_GRP_5, B.COMP_CNT" + "\n");
                strSqlString.Append("                          FROM RWIPMCPBOM A" + "\n");
                strSqlString.Append("                             , VWIPMATDEF B" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("                           AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                           AND B.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                           AND B.MAT_ID LIKE 'SEK%'" + "\n");
                strSqlString.Append("                       ) A" + "\n");
                strSqlString.Append("               ) " + "\n");
                strSqlString.Append("         WHERE MAT_GRP_5 LIKE 'Middle%'" + "\n");
                strSqlString.Append("           AND MIRROR_CNT > 0" + "\n");
                strSqlString.Append("       ) MIR" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND LOT.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.Append("   AND LOT.FACTORY = OPR.FACTORY " + "\n");
                strSqlString.Append("   AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("   AND LOT.MAT_ID = MIR.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND LOT.OPER = OPR.OPER " + "\n");
                strSqlString.Append("   AND LOT.MAT_VER = 1 " + "\n");
                strSqlString.Append("   AND LOT.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("   AND LOT.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("   AND LOT.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("   AND LOT.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("   AND LOT.MAT_ID LIKE 'SEK%'" + "\n");
                strSqlString.Append("   AND LOT.MAT_ID NOT LIKE 'SEKY%'" + "\n");
                strSqlString.Append("   AND LOT.CUTOFF_DT = '" + cdvFromToDate.HmFromDay + "22'  " + "\n");

                //strSqlString.Append("   AND MAT.MAT_GRP_3 IN ('MCP', 'DDP', 'QDP')" + "\n");  --재공실사는 싱글제품 포함하여 메모리 전체를 다 한다.

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("   AND LOT.LOT_CMF_5 LIKE '" + cbLotType.Text + "' " + "\n");
                }

                if (txtSearchProduct.Text != "" && txtSearchProduct.Text != "%")
                {
                    strSqlString.Append("   AND LOT.MAT_ID LIKE '" + txtSearchProduct.Text + "' \n");
                }

                strSqlString.Append(" ORDER BY LOT.OPER, LOT.MAT_ID, CREATETIMEDIFF DESC, LOT.LOT_ID" + "\n");

                #endregion
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

                //if (cdvType.Text == "Part Change" || cdvType.Text == "EOH Upload" || cdvType.Text == "OMS Upload")
                if (cdvType.SelectedIndex == 1 || cdvType.SelectedIndex == 2 || cdvType.SelectedIndex == 4)
                {
                    spdData.DataSource = dt;
                }
                else
                {
                    //if (cdvType.Text == "재공 실사")
                    if (cdvType.SelectedIndex == 0)
                    {
                        //1.Griid 합계 표시
                        int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 5, null, null, btnSort);
                        //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함                                        

                        //3. Total부분 셀머지
                        spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);
                    }
                    //else if (cdvType.Text == "Summary")
                    else if (cdvType.SelectedIndex == 3)
                    {
                        //1.Griid 합계 표시
                        int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 7, null, null, btnSort);
                        //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함                                        

                        //3. Total부분 셀머지
                        spdData.RPT_FillDataSelectiveCells("Total", 0, 7, 0, 1, true, Align.Center, VerticalAlign.Center);
                    }
                    else
                    {
                        //1.Griid 합계 표시
                        int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 6, null, null, btnSort);
                        //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함                    

                        //3. Total부분 셀머지
                        spdData.RPT_FillDataSelectiveCells("Total", 0, 6, 0, 1, true, Align.Center, VerticalAlign.Center);
                    }

                    
                }                

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                // SEKY 로 시작하는 것은 색상 표시. 1차칩만 표시 됨.
                //if (cdvType.Text == "Summary")
                //{
                //    Color BackColor = spdData.ActiveSheet.Cells[1, 3].BackColor;

                //    for (int i = 1; i < spdData.ActiveSheet.RowCount; i++)
                //    {
                //        // subTotal 을 제외한 나머지 부분 클릭시 실행되도록...
                //        if (spdData.ActiveSheet.Cells[i, 3].BackColor == BackColor)
                //        {
                //            string sMatID = spdData.ActiveSheet.Cells[i, 3].Value.ToString().Substring(0, 4);

                //            if (sMatID == "SEKY")
                //            {
                //                spdData.ActiveSheet.Rows[i].BackColor = Color.Pink;

                //                //for (int y = 17; y <= 29; y++)
                //                //{
                //                //    spdData.ActiveSheet.Cells[i, y].BackColor = Color.Pink;
                //                //}

                //            }
                //        }
                //    }
                //}

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

        private void PRD010410_Load(object sender, EventArgs e)
        {

        }

        private void cdvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cdvType.Text == "EOH Upload")
            if (cdvType.SelectedIndex == 1)
            {
                ckdAging.Visible = true;
            }
            else
            {
                ckdAging.Visible = false;
            }

            //if (cdvType.Text == "Summary" || cdvType.Text == "EOH Upload" || cdvType.Text == "OMS Upload")
            if (cdvType.SelectedIndex == 1 || cdvType.SelectedIndex == 3 || cdvType.SelectedIndex == 4)
            {
                cdvFromToDate.DaySelector.SelectedValue = "MONTH";
                //cdvFromToDate.AutoBinding(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                cdvFromToDate.ToYearMonth.Visible = false;                
            }
            else
            {
                cdvFromToDate.DaySelector.SelectedValue = "DAY";
                //cdvFromToDate.AutoBinding(DateTime.Now.ToString("yyyy-MM") + "-01", DateTime.Now.ToString("yyyy-MM-dd"));

                //if (cdvType.Text == "재공 실사")
                if (cdvType.SelectedIndex == 0)
                {
                    cdvFromToDate.ToDate.Visible = false;
                }
                else
                {
                    cdvFromToDate.ToDate.Visible = true;                                   
                }
                
            }
        }
    }
}
