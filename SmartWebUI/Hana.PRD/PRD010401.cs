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
    public partial class PRD010401 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010401<br/>
        /// 클래스요약: 공정별 실적 현황<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: 공정별 실적 현황<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2010-04-07-임종우 : Hynix용(06시 기준) 데이터 조회 가능 하도록 Time Base 추가 함. (김동인 요청)
        /// 2011-02-23-임종우 : Wafer Qty 조회 가능하도록 수정 함. (권문석 요청)
        /// 2011-03-20-김민우 : 원부자재 조회 안되도록 수정 함. (김문한 요청)
        /// 2011-03-25-배수민 : MAJOR CODE 추가 (김문한 요청)
        /// 2011-06-07-임종우 : Wafer Qty 공정별 수량 조회 가능 하도록 수정 함. (김문한 요청)
        /// 2011-12-09-임종우 : HMKS1 Factory 출하 수량 검색 되도록 수정 함.
        /// 2013-04-30-임종우 : LOT TYPE을 P%, E%로 변경 함 (오세만 요청)
        /// </summary>
        public PRD010401()
        {
            InitializeComponent();

            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
            cboTimeBase.SelectedIndex = 1;
        }


        #region 유효성검사 및 초기화
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
            if (cdvOper.Text.TrimEnd() == "")
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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Step", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Bumping Type", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation Flow", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Layer classification", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG Type", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("RDL Plating", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Final Bump", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Sub. Material", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Size", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Thickness", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Flat Type", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 12, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Product", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 14, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("SAP_CODE", 0, 15, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Total", 0, 16, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Average production", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Double1, 80);

                spdData.RPT_ColumnConfigFromTable_New(btnSort);
                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }
            else
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Step", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Major Code", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Family", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("SAP_CODE", 0, 13, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Total", 0, 14, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Average production", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Double1, 80);

                spdData.RPT_ColumnConfigFromTable_New(btnSort);
                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", "MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "B.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "B.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "B.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "B.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "B.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "B.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7",false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "B.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "B.MAT_CMF_14", "MAT_CMF_14", "MAT_CMF_14", "MAT_CMF_14", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "B.MAT_CMF_2", "MAT_CMF_2", "MAT_CMF_2", "MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "B.MAT_CMF_3", "MAT_CMF_3", "MAT_CMF_3", "MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("WAFER ORIENTATION", "B.MAT_CMF_4", "MAT_CMF_4", "MAT_CMF_4", "MAT_CMF_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "B.MAT_ID", "MAT_ID", "MAT_ID", "MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("STEP", "A.OPER", "OPER", "OPER", "DECODE(OPER,'AISSUE','A0001','TISSUE','T0001','EISSUE','E0001',OPER)", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "B.CUSTOMER_MAT_ID", "CUSTOMER_MAT_ID", "CUSTOMER_MAT_ID", "CUSTOMER_MAT_ID", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "B.MAT_CMF_7", "MAT_CMF_7", "MAT_CMF_7", "MAT_CMF_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP_CODE", "BASE_MAT_ID,VENDOR_MAT_ID", "BASE_MAT_ID,VENDOR_MAT_ID", "CASE WHEN OPER <= 'A0200' THEN VENDOR_MAT_ID ELSE BASE_MAT_ID END SAP_CODE", "BASE_MAT_ID,VENDOR_MAT_ID", true);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", "MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "B.MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "B.MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "B.MAT_ID", "MAT_ID", "MAT_ID", "MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("STEP", "A.OPER", "OPER", "OPER", "DECODE(OPER,'AISSUE','A0001','TISSUE','T0001','EISSUE','E0001',OPER)", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "B.MAT_CMF_7", "MAT_CMF_7", "MAT_CMF_7", "MAT_CMF_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP_CODE", "BASE_MAT_ID,VENDOR_MAT_ID", "BASE_MAT_ID,VENDOR_MAT_ID", "CASE WHEN OPER <= 'A0200' THEN VENDOR_MAT_ID ELSE BASE_MAT_ID END SAP_CODE", "BASE_MAT_ID,VENDOR_MAT_ID", true);
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
            string QueryCond3 = null;
            string QueryCond4 = null;

            string strDate = string.Empty;
            string sFrom = null;
            string sTo = null;
            string sGroupBy = string.Empty;
            string sHeader = string.Empty;
            string strVal1 = string.Empty;
            string strVal2 = string.Empty;
            string bbbb = string.Empty;
            string sSelectQty = string.Empty;

            string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            selectDate = cdvFromToDate.getSelectDate();

            string strFacIni = null;

            if (cdvFactory.Text != "FGS")
            {
                strFacIni = cdvFactory.ValueSelectedItem.Text.Substring(3, 1); // Factory 문자가져오기(A,T,E) -> Issue값 0000다음에 정렬하기위함.
            }

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            switch (cdvFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    sFrom = cdvFromToDate.FromDate.Text.Replace("-", "");
                    sTo = cdvFromToDate.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    break;
                case "WEEK":
                    sFrom = cdvFromToDate.FromWeek.SelectedValue.ToString();
                    sTo = cdvFromToDate.ToWeek.SelectedValue.ToString();
                    strDate = "WORK_WEEK";
                    break;
                case "MONTH":
                    sFrom = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
                    sTo = cdvFromToDate.ToYearMonth.Value.ToString("yyyMM");
                    strDate = "WORK_MONTH";
                    break;
                default:
                    sFrom = cdvFromToDate.FromDate.Text.Replace("-", "");
                    sTo = cdvFromToDate.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    break;
            }

            // 2011-06-07-임종우 : Chip Qty와 Wafer Qty 표시 하기 위한 구분자
            if (ckbWafer.Checked == true)
                sSelectQty = "2"; // Wafer Qty
            else
                sSelectQty = "1"; // Chip Qty

            strSqlString.AppendFormat("        SELECT {0}" + "\n", QueryCond3);

            //strSqlString.Append("             , CASE WHEN OPER <= 'A0200' THEN VENDOR_MAT_ID ELSE BASE_MAT_ID END SAP_CODE" + "\n");            

            strSqlString.Append("             , SUM(TOTAL) AS TOTAL" + "\n");            

            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlString.AppendFormat("             , SUM(ASSY_END_QTY{0} ) VAL{0}" + "\n", i.ToString());
            }           
            
            strSqlString.AppendFormat("             , ROUND(SUM(TOTAL)/{0}, 1) AVG" + "\n", cdvFromToDate.SubtractBetweenFromToDate+1);

            strSqlString.Append("          FROM (" + "\n");
            strSqlString.AppendFormat("               SELECT {0} " + "\n", QueryCond1);

            if (ckbKpcs.Checked == false)   // Kpcs 구분
            {
                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("                    , DECODE({0}, '{1}', (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'SZ010', 'BZ010') THEN SHIP_QTY_{2} ELSE END_QTY_{3} END),0) ASSY_END_QTY{4}" + "\n", strDate, selectDate[i].ToString(), sSelectQty, sSelectQty, i.ToString());
                }

                strSqlString.AppendFormat("                    , (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'SZ010', 'BZ010') THEN SHIP_QTY_{0} ELSE END_QTY_{1} END) TOTAL" + "\n", sSelectQty, sSelectQty);  // 평균및 합계 구하기 위해 전달
            }
            else
            {
                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("                    , DECODE({0}, '{1}', (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'SZ010', 'BZ010') THEN ROUND(SHIP_QTY_{2}/1000,3) ELSE ROUND(END_QTY_{3}/1000,3) END),0) ASSY_END_QTY{4}" + "\n", strDate, selectDate[i].ToString(), sSelectQty, sSelectQty, i.ToString());
                }

                strSqlString.AppendFormat("                    , (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'SZ010', 'BZ010') THEN ROUND(SHIP_QTY_{0}/1000,3) ELSE ROUND(END_QTY_{1}/1000,3) END) TOTAL" + "\n", sSelectQty, sSelectQty);  // 평균및 합계 구하기 위해 전달  (Kpcs)                
            }
            //strSqlString.Append("                 , BASE_MAT_ID,VENDOR_MAT_ID" + "\n"); 
            strSqlString.Append("                 FROM (" + "\n");
            strSqlString.AppendFormat("                      SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, {0}, CM_KEY_3" + "\n", strDate);
            strSqlString.Append("                           , SUM(END_LOT) AS END_LOT" + "\n");
            strSqlString.Append("                           , SUM(END_QTY_1) AS END_QTY_1" + "\n");
            strSqlString.Append("                           , SUM(END_QTY_2) AS END_QTY_2" + "\n");
            strSqlString.Append("                           , SUM(SHIP_QTY_1) AS SHIP_QTY_1" + "\n");
            strSqlString.Append("                           , SUM(SHIP_QTY_2) AS SHIP_QTY_2" + "\n");
            strSqlString.Append("                        FROM (" + "\n");
            strSqlString.AppendFormat("                             SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, {0}, CM_KEY_3" + "\n", strDate);
            strSqlString.Append("                           , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_LOT+S2_OPER_IN_LOT+S3_OPER_IN_LOT),SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT)) END_LOT" + "\n");
            strSqlString.Append("                           , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1" + "\n");
            strSqlString.Append("                           , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2),SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2+S1_END_RWK_QTY_2 + S2_END_RWK_QTY_2 + S3_END_RWK_QTY_2)) END_QTY_2" + "\n");
            strSqlString.Append("                           , 0 SHIP_QTY_1" + "\n");
            strSqlString.Append("                           , 0 SHIP_QTY_2" + "\n");

            //if (cboTimeBase.Text.Equals("22시"))
            if (cboTimeBase.SelectedIndex == 1)
            {
                strSqlString.Append("                        FROM RSUMWIPMOV " + "\n");
            }
            else
            {
                strSqlString.Append("                        FROM CSUMWIPMOV " + "\n");
            }

            strSqlString.Append("                       WHERE OPER NOT IN ('AZ010','TZ010','F0000','EZ010', 'SZ010', 'BZ010')" + "\n");
            strSqlString.AppendFormat("                    GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, {0}, CM_KEY_3" + "\n", strDate);
            strSqlString.Append("                   UNION ALL" + "\n");
            strSqlString.Append("                      SELECT CM_KEY_1 AS FACTORY, MAT_ID" + "\n");
            strSqlString.Append("                           , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010','HMKS1','SZ010', 'BZ010') OPER" + "\n");
            strSqlString.AppendFormat("                           , LOT_TYPE, MAT_VER,  {0},CM_KEY_3" + "\n", strDate);
            strSqlString.Append("                           , 0 END_LOT" + "\n");
            strSqlString.Append("                           , 0 END_QTY_1" + "\n");
            strSqlString.Append("                           , 0 END_QTY_2" + "\n");
            strSqlString.Append("                           , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) SHIP_QTY_1" + "\n");
            strSqlString.Append("                           , SUM(S1_FAC_OUT_QTY_2+S2_FAC_OUT_QTY_2+S3_FAC_OUT_QTY_2) SHIP_QTY_2" + "\n");

            //if (cboTimeBase.Text.Equals("22시"))
            if (cboTimeBase.SelectedIndex == 1)
            {
                strSqlString.Append("                        FROM RSUMFACMOV " + "\n");
            }
            else
            {
                strSqlString.Append("                        FROM CSUMFACMOV " + "\n");
            }
                        
            //strSqlString.Append("                       WHERE FACTORY NOT IN ('RETURN')" + "\n");

            if (cdvFactory.ValueSelectedItem.Text.Equals(GlobalVariable.gsTestDefaultFactory)) // TEST이면 ASSY 로 이동한 값 제외
            {
                strSqlString.Append("                       WHERE FACTORY NOT IN ('RETURN','" + GlobalVariable.gsAssyDefaultFactory + "') " + "\n");
            }
            else
            {
                strSqlString.Append("                       WHERE FACTORY NOT IN ('RETURN')" + "\n");
            }

            strSqlString.AppendFormat("                    GROUP BY CM_KEY_1, MAT_ID, OPER, LOT_TYPE, MAT_VER, {0}, CM_KEY_3" + "\n", strDate);

            if (cdvFactory.ValueSelectedItem.Text.Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                if (checkShip.Checked == true)
                {
                    strSqlString.Append("                   UNION ALL" + "\n");
                    strSqlString.Append("                      SELECT CM_KEY_1 AS FACTORY, MAT_ID" + "\n");
                    strSqlString.Append("                           , 'SHIP' OPER " + "\n");
                    strSqlString.AppendFormat("                           , LOT_TYPE, MAT_VER,  {0},CM_KEY_3" + "\n", strDate);
                    strSqlString.Append("                           , 0 END_LOT" + "\n");
                    strSqlString.Append("                           , 0 END_QTY_1" + "\n");
                    strSqlString.Append("                           , 0 END_QTY_2" + "\n");
                    strSqlString.Append("                           , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) SHIP_QTY_1" + "\n");
                    strSqlString.Append("                           , SUM(S1_FAC_OUT_QTY_2+S2_FAC_OUT_QTY_2+S3_FAC_OUT_QTY_2) SHIP_QTY_2" + "\n");

                    //if (cboTimeBase.Text.Equals("22시"))
                    if (cboTimeBase.SelectedIndex == 1)
                    {
                        strSqlString.Append("                        FROM RSUMFACMOV " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                        FROM CSUMFACMOV " + "\n");
                    }
                                        
                    strSqlString.Append("                       WHERE FACTORY = 'CUSTOMER' " + "\n");
                    strSqlString.Append("                         AND CM_KEY_1='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.AppendFormat("                    GROUP BY CM_KEY_1, MAT_ID, OPER, LOT_TYPE, MAT_VER, {0}, CM_KEY_3" + "\n", strDate);
                }
            }

            if (!cdvFactory.ValueSelectedItem.Text.Equals("FGS"))
            {
                if (checkIssue.Checked == true)
                {
                    strSqlString.Append("                   UNION ALL" + "\n");
                    strSqlString.AppendFormat("                    SELECT FACTORY, MAT_ID, '" + strFacIni + "ISSUE' OPER, LOT_TYPE, MAT_VER, {0}, CM_KEY_3" + "\n", strDate);
                    strSqlString.Append("                           , SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT) END_LOT" + "\n");
                    strSqlString.Append("                           , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1) END_QTY_1" + "\n");
                    strSqlString.Append("                           , SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2+S1_END_RWK_QTY_2 + S2_END_RWK_QTY_2 + S3_END_RWK_QTY_2) END_QTY_2" + "\n");
                    strSqlString.Append("                           , 0 SHIP_QTY_1" + "\n");
                    strSqlString.Append("                           , 0 SHIP_QTY_2" + "\n");

                    //if (cboTimeBase.Text.Equals("22시"))
                    if (cboTimeBase.SelectedIndex == 1)
                    {
                        strSqlString.Append("                        FROM RSUMWIPMOV " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                        FROM CSUMWIPMOV " + "\n");
                    }
                                        
                    strSqlString.Append("                       WHERE OPER LIKE '%0000' " + "\n");
                    strSqlString.AppendFormat("                    GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, {0}, CM_KEY_3" + "\n", strDate);
                }
            }

            strSqlString.Append("                             )" + "\n");
            strSqlString.AppendFormat("                    GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, {0}, CM_KEY_3" + "\n", strDate);
            strSqlString.Append("                    )A" + "\n");
            strSqlString.Append("                  , MWIPMATDEF B " + "\n");
            strSqlString.Append("              WHERE 1=1 " + "\n");
            strSqlString.Append("                AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                AND A.MAT_ID = B.MAT_ID                " + "\n");
            strSqlString.Append("                AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                AND B.MAT_VER = 1 " + "\n");
            strSqlString.Append("                AND B.MAT_TYPE = 'FG' " + "\n");

            // 체크박스 유무에 따라 issue, ship 보여줌.
            if (checkIssue.Checked == true || checkShip.Checked==true) // 둘중에 한개라도 체크되어있다면..
            {
                if (checkShip.Checked == false) //Ship이 체크가 아니면 Issue가 체크
                {
                    strSqlString.Append("                AND (A.OPER " + cdvOper.SelectedValueToQueryString + " OR A.OPER IN ('" + strFacIni + "ISSUE'))" + "\n");
                }
                else if (checkIssue.Checked == false) // Issue가 체크가 아니면 Ship이 체크
                {
                    strSqlString.Append("                AND (A.OPER " + cdvOper.SelectedValueToQueryString + " OR A.OPER IN ('SHIP'))" + "\n");
                }
                else // 둘다 False가 아니면 모두 True
                {
                    strSqlString.Append("                AND (A.OPER " + cdvOper.SelectedValueToQueryString + " OR A.OPER IN ('" + strFacIni + "ISSUE','SHIP'))" + "\n");
                }
            }
            else
            {
                strSqlString.Append("                AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            }
            strSqlString.AppendFormat("                AND A.MAT_ID LIKE '{0}'  " + "\n", txtSearchProduct.Text);

            // 2013-04-30-임종우 : LOT TYPE P%, E%로 변경 함.
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.AppendFormat("                AND A.CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                AND A.OPER NOT IN ('00001','00002') " + "\n");
            strSqlString.AppendFormat("                AND A.{0} BETWEEN '{1}' AND '{2}' " + "\n", strDate, sFrom, sTo);

            
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
                    strSqlString.AppendFormat("                AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            }
            #endregion

            strSqlString.Append("         )   " + "\n");
            strSqlString.Append("   WHERE TOTAL > 0           " + "\n");
            strSqlString.AppendFormat("GROUP BY {0}" + "\n", QueryCond2);
            strSqlString.AppendFormat("ORDER BY {0}" + "\n", QueryCond4);

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

            GridColumnInit();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                int sub;
                if (cdvFactory.txtValue == "HMKB1")
                {
                    //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                    sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(1);
                }
                else
                {
                    //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                    sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                }

                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 12, null, null, btnSort);

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 7, 10, null, null, btnSort);
                //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함


                //2. 칼럼 고정(필요하다면..)
                spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);


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
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;

                ckbWafer.Text = "Chip Qty";

                ckbKpcs.Checked = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;

                ckbWafer.Text = "Wafer Qty";
            }

            this.SetFactory(cdvFactory.txtValue);            
            cdvOper.sFactory = cdvFactory.txtValue;

            SortInit(); //Add. 20150602
        }
        #endregion
    }
}