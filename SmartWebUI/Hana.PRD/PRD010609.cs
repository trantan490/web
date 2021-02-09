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
    public partial class PRD010609 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        //string[] selectDate = null;

        /// <summary>
        /// 클  래  스: PRD010609<br/>
        /// 클래스요약: 입고 현황 TREND<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-03-22<br/>
        /// 상세  설명: 입고 현황 시간대별 TREND 조회 화면<br/>
        /// 변경  내용: <br/>        
        /// 2011-02-09-임종우 : 시간대별 및 제품별 조회 가능하도록 수정 및 챠트 삭제(김문한 요청)     
        /// 2011-03-07-임종우 : 삼성 제품에 한해서 MAJOR CODE 추가 표시 요청 (김문한 요청)
        /// 2011-05-26-임종우 : 영업팀 Paperless Summary용 조회 추가 (문영만 요청)
        /// 2011-11-21-임종우 : 월 계획 제품 중복 오류 수정
        /// 2012-05-15-임종우 : MCP, DDP, QDP PKG 분리 작업
        /// 2013-10-17-김민우: LOT TYPE ALL, P%, E% 구분으로변경
        /// </summary>
        /// 
        public PRD010609()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.Enabled = false;
            this.cdvOper.sFactory = GlobalVariable.gsAssyDefaultFactory;
            SortInit();           
            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();
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

            // Customer 무조건 선택 하여야 함.
            //if (udcWIPCondition1.Text == "ALL" || udcWIPCondition1.Text == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD033", GlobalVariable.gcLanguage));
            //    return false;
            //}

            // Family 무조건 선택 하여야 함. (Spread Data는 문제가 안되나 Chart 데이터 뿌려질때 여러 PKG 출력하면 구분이 안되어 필수 선택으로 설정함.
            //if (udcWIPCondition2.Text == "ALL" || udcWIPCondition2.Text == "")
            //{
            //    CmnFunction.ShowMsgBox("Family 값을 선택 하여 주세요");
            //    return false;
            //}

            return true;
        }

        /// <summary 2. 헤더 생성> 
        ///
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;

            //selectDate = cdvFromToDate.getSelectDate();
            try
            {
                spdData.RPT_ColumnInit();

                if (rdbBasic.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Major Code", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Family", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);                    
                    spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Cust Device", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG_CODE", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                    if (ckbTimeBase.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("Classification", 0, 13, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("Classification", 0, 13, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    }

                    if (cdvFactory.Text != "")
                    {
                        if (ckbKpcs.Checked == false)
                        {
                            spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                            spdData.RPT_AddBasicColumn("TOTAL", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("Average", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 60);
                        }
                        else
                        {
                            spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                            spdData.RPT_AddBasicColumn("TOTAL", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 60);
                            spdData.RPT_AddBasicColumn("Average", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 60);
                        }

                    }
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Major Code", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Family", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Cust Device", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG_CODE", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
               
                    spdData.RPT_AddBasicColumn("Monthly plan", 0, 13, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 80);

                    spdData.RPT_AddBasicColumn("Wafer warehousing Contribution Status", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("BOH", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Warehousing Performance", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Total warehousing quantity", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Total warehousing rate ", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
                    spdData.RPT_MerageHeaderColumnSpan(0, 14, 4);

                    spdData.RPT_AddBasicColumn("Remaining quantity", 0, 18, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("a daily goal", 0, 19, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);

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
                    spdData.RPT_MerageHeaderRowSpan(0, 18, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 19, 2);
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "B.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "B.MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Major Code", "MAT_GRP_9", "B.MAT_GRP_9", "MAT_GRP_9", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "B.MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "B.MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "B.MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "B.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "B.MAT_CMF_10", "MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "B.MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "B.MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "B.MAT_ID", "MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "MAT_CMF_7", "B.MAT_CMF_7", "MAT_CMF_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG_CODE", "PKG_CODE", "A.PKG_CODE", "PKG_CODE", false);
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
            string sStart_Tran_Time = string.Empty;
            string sEnd_Tran_Time = string.Empty;
            string bbbb = string.Empty;
            string strDecode = string.Empty;
            int iCnt = cdvFromToDate.SubtractBetweenFromToDate + 1;
                                 
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;            

            sStart_Tran_Time = cdvFromToDate.ExactFromDate;
            sEnd_Tran_Time = cdvFromToDate.ExactToDate;

            if (rdbBasic.Checked == true)
            {
                if (ckbKpcs.Checked == false)
                {
                    strDecode += cdvFromToDate.getDecodeQuery("SUM(DECODE(CREATE_DAY", "QTY, 0))", "A").Replace(", SUM(DECODE(", "     , SUM(DECODE(");
                }
                else
                {
                    strDecode += cdvFromToDate.getDecodeQuery("ROUND(SUM(DECODE(CREATE_DAY", "QTY, 0))/1000,1)", "A").Replace(", ROUND(SUM(DECODE(", "     , ROUND(SUM(DECODE(");
                }


                strSqlString.AppendFormat("SELECT {0}, TIME_BASE " + "\n", QueryCond3);


                strSqlString.Append(strDecode);

                if (ckbKpcs.Checked == false)
                {
                    strSqlString.Append("     , SUM(QTY) AS QTY" + "\n");
                    strSqlString.Append("     , ROUND(SUM(QTY)/" + iCnt.ToString() + ",1) AS A_AVG" + "\n");
                }
                else
                {
                    strSqlString.Append("     , ROUND(SUM(QTY)/1000,1) AS QTY" + "\n");
                    strSqlString.Append("     , ROUND((SUM(QTY)/" + iCnt.ToString() + ")/1000,1) AS A_AVG" + "\n");
                }

                strSqlString.Append("  FROM (" + "\n");
                strSqlString.AppendFormat("        SELECT {0} " + "\n", QueryCond2);
                strSqlString.Append("             , A.CREATE_DAY, A.TIME_BASE, SUM(A.QTY_1) AS QTY" + "\n");
                strSqlString.Append("          FROM (" + "\n");

                if (ckbTimeBase.Checked == true && cdvOper.txtValue != "ISSUE")
                {
                    if (cdvOper.txtValue != "" && cdvOper.txtValue != "A0000")
                    {
                        strSqlString.Append("                SELECT FACTORY, MAT_ID, SUBSTR(MAT_ID, -3) AS PKG_CODE, WORK_DATE AS CREATE_DAY, (S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1) AS QTY_1, '' AS TIME_BASE" + "\n");
                        strSqlString.Append("                  FROM RSUMWIPMOV " + "\n");
                        strSqlString.Append("                 WHERE WORK_DATE BETWEEN '" + cdvFromToDate.HmFromDay + "' AND '" + cdvFromToDate.HmToDay + "' " + "\n");
                        strSqlString.Append("                   AND FACTORY  = '" + cdvFactory.Text + "'" + "\n");
                        strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("                   AND OPER = '" + cdvOper.txtValue + "'" + "\n");
                        strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%'" + "\n");
                        //strSqlString.Append("                   AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                        if (cdvLotType.Text != "ALL")
                        {
                            strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                        }
                        
                        strSqlString.Append("                 UNION ALL " + "\n");
                        strSqlString.Append("                SELECT FACTORY, MAT_ID, SUBSTR(MAT_ID, -3) AS PKG_CODE, WORK_DATE AS CREATE_DAY, (S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1) AS QTY_1, '' AS TIME_BASE" + "\n");
                        strSqlString.Append("                  FROM CSUMWIPMOV " + "\n");
                        strSqlString.Append("                 WHERE WORK_DATE BETWEEN '" + cdvFromToDate.HmFromDay + "' AND '" + cdvFromToDate.HmToDay + "' " + "\n");
                        strSqlString.Append("                   AND FACTORY  = '" + cdvFactory.Text + "'" + "\n");
                        strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("                   AND OPER = '" + cdvOper.txtValue + "'" + "\n");
                        strSqlString.Append("                   AND MAT_ID LIKE 'HX%'" + "\n");
                        //strSqlString.Append("                   AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                        if (cdvLotType.Text != "ALL")
                        {
                            strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                        }
                    }
                    else
                    {
                        strSqlString.Append("                SELECT FACTORY, MAT_ID, SUBSTR(MAT_ID, -3) AS PKG_CODE, WORK_DATE AS CREATE_DAY, (S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1) AS QTY_1, '' AS TIME_BASE" + "\n");
                        strSqlString.Append("                  FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                 WHERE WORK_DATE BETWEEN '" + cdvFromToDate.HmFromDay + "' AND '" + cdvFromToDate.HmToDay + "' " + "\n");
                        strSqlString.Append("                   AND FACTORY  = '" + cdvFactory.Text + "'" + "\n");
                        strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("                   AND OPER = 'A0000' " + "\n");
                        strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%'" + "\n");
                        //strSqlString.Append("                   AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                        if (cdvLotType.Text != "ALL")
                        {
                            strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                        }
                        
                        strSqlString.Append("                 UNION ALL " + "\n");
                        strSqlString.Append("                SELECT FACTORY, MAT_ID, SUBSTR(MAT_ID, -3) AS PKG_CODE, WORK_DATE AS CREATE_DAY, (S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1) AS QTY_1, '' AS TIME_BASE" + "\n");
                        strSqlString.Append("                  FROM CSUMFACMOV " + "\n");
                        strSqlString.Append("                 WHERE WORK_DATE BETWEEN '" + cdvFromToDate.HmFromDay + "' AND '" + cdvFromToDate.HmToDay + "' " + "\n");
                        strSqlString.Append("                   AND FACTORY  = '" + cdvFactory.Text + "'" + "\n");
                        strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("                   AND OPER = 'A0000' " + "\n");
                        strSqlString.Append("                   AND MAT_ID LIKE 'HX%'" + "\n");
                        //strSqlString.Append("                   AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                        if (cdvLotType.Text != "ALL")
                        {
                            strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                        }
                    }
                }
                else
                {
                    strSqlString.Append("                SELECT LOT_ID, FACTORY, MAT_ID, SUBSTR(MAT_ID, -3) AS PKG_CODE, QTY_1, OPER_IN_TIME, GET_WORK_DATE(OPER_IN_TIME,'D') AS CREATE_DAY" + "\n");

                    // TIME_BASE 선택에 의한 기준 시간 변경.
                    if (cdvTimeBase.Text == "SE")
                    {
                        strSqlString.Append("                     , CASE WHEN ('210000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '090000') THEN '주간(09시-21시)'" + "\n");
                        strSqlString.Append("                            WHEN ('220000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '210000') THEN '야간(21시-22시)'" + "\n");
                        strSqlString.Append("                            ELSE '새벽(22시-09시)'" + "\n");
                    }
                    else if (cdvTimeBase.Text == "2HR")
                    {
                        strSqlString.Append("                     , CASE WHEN ('020000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '000000') THEN '00시-02시'" + "\n");
                        strSqlString.Append("                            WHEN ('040000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '020000') THEN '02시-04시'" + "\n");
                        strSqlString.Append("                            WHEN ('060000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '040000') THEN '04시-06시'" + "\n");
                        strSqlString.Append("                            WHEN ('080000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '060000') THEN '06시-08시'" + "\n");
                        strSqlString.Append("                            WHEN ('100000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '080000') THEN '08시-10시'" + "\n");
                        strSqlString.Append("                            WHEN ('120000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '100000') THEN '10시-12시'" + "\n");
                        strSqlString.Append("                            WHEN ('140000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '120000') THEN '12시-14시'" + "\n");
                        strSqlString.Append("                            WHEN ('160000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '140000') THEN '14시-16시'" + "\n");
                        strSqlString.Append("                            WHEN ('180000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '160000') THEN '16시-18시'" + "\n");
                        strSqlString.Append("                            WHEN ('200000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '180000') THEN '18시-20시'" + "\n");
                        strSqlString.Append("                            WHEN ('220000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '200000') THEN '20시-22시'" + "\n");
                        strSqlString.Append("                            ELSE '22시-00시'" + "\n");
                    }
                    else if (cdvTimeBase.Text == "4HR")
                    {
                        strSqlString.Append("                     , CASE WHEN ('060000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '020000') THEN '02시-06시'" + "\n");
                        strSqlString.Append("                            WHEN ('100000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '060000') THEN '06시-10시'" + "\n");
                        strSqlString.Append("                            WHEN ('140000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '100000') THEN '10시-14시'" + "\n");
                        strSqlString.Append("                            WHEN ('180000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '140000') THEN '14시-18시'" + "\n");
                        strSqlString.Append("                            WHEN ('220000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '180000') THEN '18시-22시'" + "\n");
                        strSqlString.Append("                            ELSE '22시-02시'" + "\n");
                    }
                    else if (cdvTimeBase.Text == "6HR")
                    {
                        strSqlString.Append("                     , CASE WHEN ('100000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '040000') THEN '04시-10시'" + "\n");
                        strSqlString.Append("                            WHEN ('160000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '100000') THEN '10시-16시'" + "\n");
                        strSqlString.Append("                            WHEN ('220000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '160000') THEN '16시-22시'" + "\n");
                        strSqlString.Append("                            ELSE '22시-04시'" + "\n");
                    }
                    else if (cdvTimeBase.Text == "8HR")
                    {
                        strSqlString.Append("                     , CASE WHEN ('140000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '060000') THEN '06시-14시'" + "\n");
                        strSqlString.Append("                            WHEN ('220000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '140000') THEN '14시-22시'" + "\n");
                        strSqlString.Append("                            ELSE '22시-06시'" + "\n");
                    }
                    else if (cdvTimeBase.Text == "12HR")
                    {
                        strSqlString.Append("                     , CASE WHEN ('220000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '100000') THEN '10시-22시'" + "\n");
                        strSqlString.Append("                            ELSE '22시-10시'" + "\n");
                    }

                    strSqlString.Append("                       END AS TIME_BASE" + "\n");
                    strSqlString.Append("                  FROM RWIPLOTHIS " + "\n");
                    strSqlString.Append("                 WHERE FACTORY  = '" + cdvFactory.Text + "'" + "\n");
                    strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
                    
                    if (cdvOper.txtValue != "")
                    {
                        if (cdvOper.txtValue == "A0000")
                        {
                            strSqlString.Append("                   AND OPER = '" + cdvOper.txtValue + "'" + "\n");
                            strSqlString.Append("                   AND TRAN_CODE = 'CREATE' " + "\n");
                        }
                        else if (cdvOper.txtValue == "ISSUE")
                        {
                            strSqlString.Append("                   AND OLD_OPER = 'A0000'" + "\n");
                            strSqlString.Append("                   AND TRAN_CODE = 'END' " + "\n");

                        }
                        else
                        {
                            strSqlString.Append("                   AND OPER = '" + cdvOper.txtValue + "'" + "\n");
                            strSqlString.Append("                   AND TRAN_CODE = 'END' " + "\n");
                        }
                    }
                    else
                    {
                        if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
                        {
                            strSqlString.Append("                   AND OLD_OPER IN ('A0000', 'A000N') " + "\n");
                            strSqlString.Append("                   AND TRAN_CODE = 'CREATE' " + "\n");
                        }
                        else if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
                        {
                            strSqlString.Append("                   AND OLD_OPER IN ('T0000', 'T000N') " + "\n");
                            strSqlString.Append("                   AND TRAN_CODE = 'CREATE' " + "\n");
                        }
                        
                        
                    }
                    strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "' " + "\n");
                    //strSqlString.Append("                   AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }
                }

                strSqlString.Append("               ) A" + "\n");
                strSqlString.Append("             , MWIPMATDEF B " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = B.FACTORY  " + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID            " + "\n");
                strSqlString.Append("           AND B.MAT_VER = 1  " + "\n");

                //상세 조회에 따른 SQL문 생성                        
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

                strSqlString.AppendFormat("        GROUP BY {0}, A.CREATE_DAY, A.TIME_BASE" + "\n", QueryCond2);
                strSqlString.Append("       )" + "\n");
                strSqlString.AppendFormat("GROUP BY {0}, TIME_BASE" + "\n", QueryCond1);

                if (ckbTimeBase.Checked == true)
                {
                    strSqlString.AppendFormat("ORDER BY {0}" + "\n", QueryCond1);

                }
                else
                {
                    // TIME_BASE 선택에 의한 데이터 정렬
                    if (cdvTimeBase.Text == "SE")
                    {
                        strSqlString.AppendFormat("ORDER BY {0}, DECODE(TIME_BASE,'새벽(22시-09시)',1,'주간주간(09시-21시)',2,3)" + "\n", QueryCond1);
                    }
                    else if (cdvTimeBase.Text == "2HR")
                    {
                        strSqlString.AppendFormat("ORDER BY {0}, DECODE(TIME_BASE,'22시-00시',1,'00시-02시',2,'02시-04시',3,'04시-06시',4,'06시-08시',5,'08시-10시',6,'10시-12시',7,'12시-14시',8,'14시-16시',9,'16시-18시',10,'18시-20시',11,'20시-22시',12,13)" + "\n", QueryCond1);
                    }
                    else if (cdvTimeBase.Text == "4HR")
                    {
                        strSqlString.AppendFormat("ORDER BY {0}, DECODE(TIME_BASE,'22시-02시',1,'02시-06시',2,'06시-10시',3,'10시-14시',4,'14시-18시',5,'18시-22시',6,7)" + "\n", QueryCond1);
                    }
                    else if (cdvTimeBase.Text == "6HR")
                    {
                        strSqlString.AppendFormat("ORDER BY {0}, DECODE(TIME_BASE,'22시-04시',1,'04시-10시',2,'10시-16시',3,4)" + "\n", QueryCond1);
                    }
                    else if (cdvTimeBase.Text == "8HR")
                    {
                        strSqlString.AppendFormat("ORDER BY {0}, DECODE(TIME_BASE,'22시-06시',1,'06시-14시',2,3)" + "\n", QueryCond1);
                    }
                    else if (cdvTimeBase.Text == "12HR")
                    {
                        strSqlString.AppendFormat("ORDER BY {0}, DECODE(TIME_BASE,'22시-10시',1,2)" + "\n", QueryCond1);
                    }
                }

            }
            else
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);                                
                strSqlString.Append("     , SUM(NVL(PLAN_QTY_ASSY,0)) AS PLAN_QTY_ASSY" + "\n");
                strSqlString.Append("     , SUM(NVL(BOH_QTY,0)) AS BOH_QTY" + "\n");
                strSqlString.Append("     , SUM(NVL(MOV_QTY,0)) AS MOV_QTY" + "\n");
                strSqlString.Append("     , SUM(NVL(BOH_QTY,0)) + SUM(NVL(MOV_QTY,0)) AS TOTAL_IN" + "\n");
                strSqlString.Append("     , DECODE(SUM(NVL(PLAN_QTY_ASSY,0)), 0, 0, ROUND((SUM(NVL(BOH_QTY,0)) + SUM(NVL(MOV_QTY,0))) / SUM(NVL(PLAN_QTY_ASSY,0)) * 100,2)) AS IN_PER" + "\n");
                strSqlString.Append("     , CASE WHEN SUM(NVL(PLAN_QTY_ASSY,0)) - (SUM(NVL(BOH_QTY,0)) + SUM(NVL(MOV_QTY,0))) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE SUM(NVL(PLAN_QTY_ASSY,0)) - (SUM(NVL(BOH_QTY,0)) + SUM(NVL(MOV_QTY,0))) " + "\n");
                strSqlString.Append("       END AS REMAIN_QTY" + "\n");
                strSqlString.Append("     , CASE WHEN SUM(NVL(PLAN_QTY_ASSY,0)) - (SUM(NVL(BOH_QTY,0)) + SUM(NVL(MOV_QTY,0))) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND((SUM(NVL(PLAN_QTY_ASSY,0)) - (SUM(NVL(BOH_QTY,0)) + SUM(NVL(MOV_QTY,0))))" + "\n");
                strSqlString.Append("                 / (CASE WHEN (SELECT LAST_DAY(SYSDATE) - SYSDATE -3 FROM DUAL) < 0 THEN 1 ELSE (SELECT LAST_DAY(SYSDATE) - SYSDATE -3 FROM DUAL) END), 2) " + "\n");
                strSqlString.Append("       END AS TARGET" + "\n");
                strSqlString.Append("  FROM (SELECT A.*, SUBSTR(MAT_ID, -3) AS PKG_CODE FROM MWIPMATDEF A) MAT" + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT FACTORY,MAT_ID, PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
                strSqlString.Append("          FROM ( " + "\n");
                strSqlString.Append("                SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
                strSqlString.Append("                  FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                      SELECT MAT_ID " + "\n");
                strSqlString.Append("                                        FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                       WHERE 1=1 " + "\n");
                strSqlString.Append("                                         AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                         AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                     ) " + "\n");
                strSqlString.Append("                 GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");
                strSqlString.Append("                 UNION ALL " + "\n");
                strSqlString.Append("                SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , TO_CHAR(SYSDATE,'YYYYMM') AS PLAN_MONTH " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                          FROM CWIPPLNDAY " + "\n");
                strSqlString.Append("                         WHERE 1=1 " + "\n");
                strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                           AND PLAN_DAY BETWEEN TO_CHAR(SYSDATE,'YYYYMM') || '01' AND TO_CHAR(LAST_DAY(SYSDATE),'YYYYMMDD')" + "\n");
                strSqlString.Append("                           AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                           AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                         GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                         UNION ALL " + "\n");
                strSqlString.Append("                        SELECT CM_KEY_1 AS FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                             , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                         WHERE 1=1 " + "\n");
                strSqlString.Append("                           AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                           AND WORK_DATE BETWEEN TO_CHAR(SYSDATE,'YYYYMM') || '01' AND TRUNC(SYSDATE, 'IW') - 1" + "\n");
                strSqlString.Append("                           AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                           AND CM_KEY_3 LIKE 'P%'                           " + "\n");
                strSqlString.Append("                         GROUP BY CM_KEY_1, MAT_ID " + "\n");
                strSqlString.Append("                       ) A " + "\n");
                strSqlString.Append("                       , MWIPMATDEF B " + "\n");
                strSqlString.Append("                   WHERE 1=1  " + "\n");
                strSqlString.Append("                     AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                     AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                     AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                     AND B.MAT_GRP_9 = 'S-LSI'                      " + "\n");
                strSqlString.Append("                   GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("               ) " + "\n");
                strSqlString.Append("         WHERE PLAN_MONTH = TO_CHAR(SYSDATE,'YYYYMM')" + "\n");
                strSqlString.Append("           AND PLAN_QTY_ASSY >0           " + "\n");
                strSqlString.Append("       ) PLN" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT STS.MAT_ID, SUM(QTY_1) AS BOH_QTY " + "\n");
                strSqlString.Append("          FROM RWIPLOTSTS_BOH STS" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT *" + "\n");
                strSqlString.Append("                  FROM MWIPMATDEF" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'           " + "\n");
                strSqlString.Append("                   AND MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("                   AND ((MAT_GRP_3 IN ('MCP', 'DDP', 'QDP')' AND MAT_GRP_5 IN ('1st', 'Middle', 'Merge'))" + "\n");
                strSqlString.Append("                       OR MAT_GRP_3 <> 'MCP'" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("               ) MAT" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND STS.FACTORY = MAT.FACTORY" + "\n");
                strSqlString.Append("           AND STS.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.Append("           AND STS.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND CUTOFF_DT = TO_CHAR(TO_DATE(TO_CHAR(SYSDATE, 'YYYYMM') || '01')-1, 'YYYYMMDD') || '22'" + "\n");
                strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("         GROUP BY STS.MAT_ID" + "\n");
                strSqlString.Append("       ) BOH" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT MOV.MAT_ID, SUM(QTY_1) AS MOV_QTY" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT FACTORY, MAT_ID, WORK_DATE AS CREATE_DAY, (S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1) AS QTY_1" + "\n");
                strSqlString.Append("                  FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                 WHERE WORK_DATE LIKE TO_CHAR(SYSDATE, 'YYYYMM') || '%'" + "\n");
                strSqlString.Append("                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND OPER = 'A0000' " + "\n");
                strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%'" + "\n");
                strSqlString.Append("                   AND CM_KEY_3 LIKE '%'" + "\n");
                strSqlString.Append("                 UNION ALL " + "\n");
                strSqlString.Append("                SELECT FACTORY, MAT_ID, WORK_DATE AS CREATE_DAY, (S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1) AS QTY_1" + "\n");
                strSqlString.Append("                  FROM CSUMFACMOV " + "\n");
                strSqlString.Append("                 WHERE WORK_DATE LIKE TO_CHAR(SYSDATE, 'YYYYMM') || '%'" + "\n");
                strSqlString.Append("                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND OPER = 'A0000' " + "\n");
                strSqlString.Append("                   AND MAT_ID LIKE 'HX%'" + "\n");
                strSqlString.Append("                   AND CM_KEY_3 LIKE '%'" + "\n");
                strSqlString.Append("               ) MOV" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT *" + "\n");
                strSqlString.Append("                  FROM MWIPMATDEF" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'           " + "\n");
                strSqlString.Append("                   AND MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("                   AND ((MAT_GRP_3 IN ('MCP', 'DDP', 'QDP') AND MAT_GRP_5 = '1st')" + "\n");
                strSqlString.Append("                       OR MAT_GRP_3 <> 'MCP'" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("               ) MAT" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND MOV.FACTORY = MAT.FACTORY" + "\n");
                strSqlString.Append("           AND MOV.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.Append("         GROUP BY MOV.MAT_ID" + "\n");
                strSqlString.Append("       ) MOV" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND MAT.FACTORY = PLN.FACTORY(+)   " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = BOH.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = MOV.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("   AND MAT.MAT_TYPE = 'FG' " + "\n");

                //상세 조회에 따른 SQL문 생성                        
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

                strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond1);
                strSqlString.Append("HAVING NVL(SUM(PLAN_QTY_ASSY),0) + NVL(SUM(BOH_QTY),0) + NVL(SUM(MOV_QTY),0) > 0 " + "\n");
                strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond1);
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #region ShowChart

        private void ShowChart(int rowCount)
        {

            // 차트 설정
            udcChartFX1.RPT_2_ClearData();

            udcChartFX1.RPT_3_OpenData(2, cdvFromToDate.SubtractBetweenFromToDate + 1); // Serise수량, X축에 표시할 항목 수
            int[] wip_columns = new Int32[cdvFromToDate.SubtractBetweenFromToDate + 1]; // 보여줄 날짜 만큼 배열 생성
            int[] tat_columns = new Int32[cdvFromToDate.SubtractBetweenFromToDate + 1];
            int[] columnsHeader = new Int32[cdvFromToDate.SubtractBetweenFromToDate + 1];

            for (int i = 0; i < wip_columns.Length; i++)
            {
                columnsHeader[i] = 4 + i; //보여줄 Column 의 위치 저장
                wip_columns[i] = 4 + i;
                tat_columns[i] = 4 + i;
            }

            int cnt = 0;
            double max1 = 0;
            double max2 = 0;

            int[] rows = new Int32[spdData.ActiveSheet.Rows.Count];

            Color color = spdData.ActiveSheet.Cells[1, 4].BackColor;

            //Grand Total 용 Chart
            max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, wip_columns, SeriseType.Rows);  // 데이터, 표시될 데이터 Row , Column위치, Row 별로 챠트 표시

            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {

                if (spdData.ActiveSheet.Cells[j, 4].BackColor == color)
                {
                    cnt++;
                    // 데이타 용 Chart
                    max2 = udcChartFX1.RPT_4_AddData(spdData, new int[] { cnt }, tat_columns, SeriseType.Rows);
                }
            }

            udcChartFX1.RPT_5_CloseData();

            // Grand Total 용 Chart 설정
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "TOT", AsixType.Y, DataTypes.Initeger, max1 * 1.2);

            udcChartFX1.Series[0].Color = System.Drawing.Color.Black;
            int[] LegBox = new int[cnt + 1];
            LegBox[0] = 0;
            cnt = 0;
            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                if (spdData.ActiveSheet.Cells[j, 4].BackColor == color)
                {
                    cnt++;
                    // 데이타 용 Chart 설정
                    udcChartFX1.RPT_6_SetGallery(ChartType.Bar, cnt, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                    LegBox[cnt] = j;
                }
            }

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader); // X축 표시 설정. (데이터, Row header 시작위치, 보여줄 Column header의 위치)
            udcChartFX1.RPT_8_SetSeriseLegend(spdData, LegBox, 3, SoftwareFX.ChartFX.Docked.Right); // Legend Title 설정. (데이터, 표시 Row, 표시 Column, 표시 위치)

            //udcChartFX1.PointLabels = true;
            udcChartFX1.Series[0].PointLabels = true;
            udcChartFX1.Series[0].PointLabelColor = Color.Blue;
            udcChartFX1.RightGap = 10;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;
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
                udcChartFX1.RPT_1_ChartInit();

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

                if (rdbBasic.Checked == true)
                {
                    //1.Griid 합계 표시
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 14, null, null, btnSort);
                    //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                    //3. Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);
                }
                else
                {
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort);
                    
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);
                }
                
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                // 2011-02-09-임종우 : 챠트 사용 중지 함.
                //ShowChart(0);
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
            ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
            //spdData.ExportExcel();           
        }
        #endregion

        private void PRD010609_Load(object sender, EventArgs e)
        {
            //기본적으로 Detail이 보이도록..
            pnlWIPDetail.Visible = true;
        }

        private void ckbTimeBase_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTimeBase.Checked == true)
            {
                cdvTimeBase.Enabled = false;
            }
            else
            {
                cdvTimeBase.Enabled = true;
            }
        }

        private void cdvOper_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                strQuery += " SELECT '' AS Code, '' AS Data FROM DUAL" + "\n";

            }
            else
            {

                strQuery += "SELECT DECODE(OPER,'A000N','ISSUE',OPER) AS Code" + "\n";
                strQuery += "     , DECODE(OPER_DESC,'ASSY INTRANSIT','ISSUE',OPER_DESC) AS Data" + "\n";
                strQuery += "  FROM MWIPOPRDEF " + "\n";
                strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
                strQuery += "   AND OPER LIKE 'A%' " + "\n";
                strQuery += " ORDER BY DECODE(OPER,'A0000',0,'ISSUE',1,2),OPER" + "\n";
            }

            cdvOper.sDynamicQuery = strQuery;

        }

        
    }
}
