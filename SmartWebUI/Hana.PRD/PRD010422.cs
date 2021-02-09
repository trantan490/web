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
    public partial class PRD010422 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010422<br/>
        /// 클래스요약: 실적 TREND<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-08-01<br/>
        /// 상세  설명: 실적 TREND (김권수 요청)<br/> 
        /// 변경  내용: <br/>    
        /// 2013-08-05-임종우 : 공정그룹 검색 기능 추가 (임태성 요청)
        /// 2013-08-06-임종우 : Customer Type 추가 & Total, 평균, Wire Count 추가 & 주간, 월간 조회 가능하도록 수정(임태성 요청)
        /// 2013-08-27-임종우 : SBA (A1300), SST (A1750) 공정 추가 (임태성 요청)
        /// 2013-09-03-임종우 : 환산수량 조회 시 SAW 공정도 Wafer 매수로 표시 되도록 수정 (김권수 요청)
        /// 2013-12-03-임종우 : C/A (A0333) 공정 그룹 추가 (임태성 요청)
        /// 2013-12-26-임종우 : 공정 그룹 검색 기능 멀티 선택으로 변경 (김권수 요청)
        /// 2014-02-25-임종우 : SST (A1800, A1900) 공정 추가, HMK3A 실적에서 COB, SAWN 제품 제외 (김권수 요청)
        /// 2014-02-26-임종우 : COB, SAWN 제품 제외 (임태성 요청)
        /// 2014-06-16-임종우 : B/G (A0030) 공정 추가, COB & SAWN 제품 다시 추가 (임태성 요청)
        /// 2014-06-17-임종우 : DP 실적 검색 기능 추가 (임태성 요청)
        /// 2014-07-28-임종우 : LOT TYPE 추가 (임태성 요청)
        /// 2014-07-30-임종우 : NET_DIE 0 이어서 오류 발생 부분 수정
        /// 2014-11-21-임종우 : 일자별 검색 기간 제한(31일까지)...누군가 30년치 데이터 조회하여 DB에 library cache lock 발생 함..
        /// 2015-04-30-임종우 : 1st 칩에만 COMP_QTY 적용 -> 전체 차수에 대해 COMP_QTY 적용 (임태성K 요청)
        /// 2016-03-30-임종우 : BUMP 부분 다시 개발..설비 데이터 표현을 위해 테이블 변경-설비가 없는 공정은 표시 안함 (이병길K 요청)
        /// </summary>
        public PRD010422()
        {
            InitializeComponent();

            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            //cdvFromToDate.DaySelector.Enabled = false;

            BaseFormType = eBaseFormType.WIP_BASE;
            pnlWIPDetail.Visible = false;

            rdbMain.Checked = true;

            rdbConvert.Checked = false;
            rdbConvert.Enabled = true;

            ckdDP.Checked = false;
            ckdDP.Enabled = true;

            cdvBigOperGroup.Visible = false;

            cdvOperGroup.ConditionText = "Operation group";
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

            if (cdvFromToDate.SubtractBetweenFromToDate > 30)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD056", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            string QueryCond = null;
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond = tableForm.SelectedValueToQueryContainNull;

            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();

            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                spdData.RPT_AddBasicColumn("Type", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);

                spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Bumping Type", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation Flow", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Layer classification", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG Type", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("RDL Plating", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Final Bump", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Sub. Material", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Size", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Thickness", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Flat Type", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 12, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Product", 0, 13, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                
                //if(QueryCond.IndexOf("OPER_BIG_GRP") > -1)
                //    spdData.RPT_AddBasicColumn("Operation large group", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                //if (QueryCond.IndexOf("OPER_GRP") > -1)
                //    spdData.RPT_AddBasicColumn("Operation subgroup", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Operation large group", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation subgroup", 0, 15, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation", 0, 16, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Equipment name", 0, 17, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Total", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Average", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);

            }
            else
            {
                spdData.RPT_AddBasicColumn("Type", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);

                spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Major Code", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Family", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation group", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation", 0, 13, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Total", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Average", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);

                if (rdbConvert.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Wire Count", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
            }

            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            spdData.RPT_ColumnConfigFromTable_New(btnSort);
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type", "CUSTOMER_TYPE", "DECODE(CUSTOMER_TYPE, 'SEC', 1, 'HYNIX', 2, 'Fabless', 3, 4)", "CUSTOMER_TYPE", true);

                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "DECODE(MAT_GRP_1, 'SE', 1, 'HX', 2, 'HH', 9, 3), MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND FACTORY = '" + cdvFactory.Text + "' AND ROWNUM=1) AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "MAT_CMF_14", "MAT_CMF_14", "MAT_CMF_14", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "MAT_CMF_2", "MAT_CMF_2", "MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "MAT_CMF_3", "MAT_CMF_3", "MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("WAFER ORIENTATION", "MAT_CMF_4", "MAT_CMF_4", "MAT_CMF_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT_ID", "MAT_ID", false);

                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation large group", "OPER_BIG_GRP", "OPER_BIG_GRP", "OPER_BIG_GRP", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation subgroup", "OPER_GRP", "OPER_GRP", "OPER_GRP", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation large group", "OPER_BIG_GRP", "OPER", "OPER_BIG_GRP", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation subgroup", "OPER_GRP", "OPER", "OPER_GRP", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "OPER", "OPER", "OPER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "RES_ID", "RES_ID", "RES_ID", true);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type", "CUSTOMER_TYPE", "DECODE(CUSTOMER_TYPE, 'SEC', 1, 'HYNIX', 2, 'Fabless', 3, 4)", "CUSTOMER_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "DECODE(MAT_GRP_1, 'SE', 1, 'HX', 2, 'HH', 9, 3), MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_10", "MAT_GRP_10", "MAT_GRP_10", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT_ID", "MAT_ID", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation group", "OPER_GRP", "DECODE(OPER_GRP, 'B/G', 1, 'SAW', 2, 'C/A', 3, 'D/A', 4, 'W/B', 5, 'MOLD', 6, 7)", "OPER_GRP", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "OPER", "OPER", "OPER", true);
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
                        
            string sFrom = null;
            string sTo = null;
            string strDate = null;
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값

            sFrom = cdvFromToDate.HmFromDay;
            sTo = cdvFromToDate.HmToDay;

            string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            selectDate = cdvFromToDate.getSelectDate();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            
            sFrom = cdvFromToDate.HmFromDay;
            sTo = cdvFromToDate.HmToDay;

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }

            switch (cdvFromToDate.DaySelector.SelectedValue.ToString())
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


            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                if (rdbMain.Checked == true)
                {
                    #region 메인
                    strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond3);
                    strSqlString.Append("     , ROUND(SUM(END_QTY) / " + sKpcsValue + ", 0) AS TTL" + "\n");

                    for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(DECODE(WORK_DATE, '{0}', END_QTY,0)) / {1}, 0) AS END_QTY{2}" + "\n", selectDate[i].ToString(), sKpcsValue, i.ToString());
                    }

                    strSqlString.AppendFormat("     , ROUND(SUM(END_QTY) / {0} / {1}, 1) AS AVG" + "\n", sKpcsValue, cdvFromToDate.SubtractBetweenFromToDate + 1);
                    strSqlString.Append("  FROM (" + "\n");
                    strSqlString.Append("        SELECT DAT.WORK_DATE" + "\n");
                    strSqlString.Append("             , DAT.OPER_BIG_GRP" + "\n");
                    strSqlString.Append("             , DAT.OPER_GRP" + "\n");
                    strSqlString.Append("             , DAT.OPER" + "\n");
                    strSqlString.Append("             , DAT.RES_ID" + "\n");
                    strSqlString.Append("             , DAT.END_QTY" + "\n");
                    strSqlString.Append("             , CASE WHEN MAT_GRP_1 = 'HH' THEN 'HT Micron'" + "\n");
                    strSqlString.Append("                    WHEN MAT_GRP_1 = 'HX' THEN 'HYNIX'" + "\n");
                    strSqlString.Append("                    WHEN MAT_GRP_1 = 'SE' THEN 'SEC'" + "\n");
                    strSqlString.Append("                    ELSE 'Fabless'" + "\n");
                    strSqlString.Append("               END AS CUSTOMER_TYPE" + "\n");
                    strSqlString.Append("             , MAT.*" + "\n");
                    strSqlString.Append("          FROM MWIPMATDEF MAT " + "\n");
                    strSqlString.Append("             , ( " + "\n");
                    strSqlString.Append("                SELECT A.WORK_DAY AS WORK_DATE" + "\n");
                    strSqlString.Append("                     , A.MAT_ID" + "\n");
                    strSqlString.Append("                     , B.OPER_GRP_1 AS OPER_BIG_GRP" + "\n");
                    strSqlString.Append("                     , B.OPER_GRP_4 AS OPER_GRP" + "\n");
                    strSqlString.Append("                     , A.RES_ID" + "\n");
                    strSqlString.Append("                     , B.OPER " + "\n");
                    strSqlString.Append("                     , SUM(QTY) AS END_QTY" + "\n");
                    strSqlString.Append("                  FROM CSUMRASMOV@RPTTOMES A " + "\n");
                    strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                    strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
                    strSqlString.Append("                   AND A.OPER = B.OPER " + "\n");
                    strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                    strSqlString.Append("                   AND A.WORK_DAY BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");                    

                    if (cdvBigOperGroup.Text != "ALL" && cdvBigOperGroup.Text != "")
                        strSqlString.AppendFormat("                   AND B.OPER_GRP_1 {0} " + "\n", cdvBigOperGroup.SelectedValueToQueryString);


                    if (cdvOperGroup.Text != "ALL" && cdvOperGroup.Text != "")
                    {
                        strSqlString.Append("                   AND B.OPER_GRP_4 " + cdvOperGroup.SelectedValueToQueryString + "\n");
                    }

                    //if (cdvType.Text != "ALL")
                    //{
                    //    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
                    //}

                    strSqlString.Append("                 GROUP BY A.WORK_DAY, A.MAT_ID, B.OPER_GRP_1, B.OPER_GRP_4, A.RES_ID, B.OPER" + "\n");
                    strSqlString.Append("               ) DAT" + "\n");
                    strSqlString.Append("         WHERE 1 = 1 " + "\n");
                    strSqlString.Append("           AND DAT.MAT_ID = MAT.MAT_ID " + "\n");
                    strSqlString.Append("           AND MAT.FACTORY = '" + cdvFactory.Text + "'  " + "\n");
                    strSqlString.Append("           AND DAT.MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
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
                    #endregion

                    strSqlString.Append("       ) " + "\n");                  
                    strSqlString.AppendFormat("GROUP BY {0}" + "\n", QueryCond1);
                    strSqlString.AppendFormat("ORDER BY {0}" + "\n", QueryCond2);                   
           

                    #endregion
                }
            }
            else
            {
                if (rdbMain.Checked == true)
                {
                    #region 메인
                    strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond3);
                    strSqlString.Append("     , ROUND(SUM(END_QTY) / " + sKpcsValue + ", 0) AS TTL" + "\n");

                    for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(DECODE(WORK_DATE, '{0}', END_QTY,0)) / {1}, 0) AS END_QTY{2}" + "\n", selectDate[i].ToString(), sKpcsValue, i.ToString());
                    }

                    strSqlString.AppendFormat("     , ROUND(SUM(END_QTY) / {0} / {1}, 1) AS AVG" + "\n", sKpcsValue, cdvFromToDate.SubtractBetweenFromToDate + 1);
                    strSqlString.Append("  FROM (" + "\n");
                    strSqlString.Append("        SELECT * FROM" + "\n");
                    strSqlString.Append("        (" + "\n");
                    strSqlString.Append("        SELECT DAT.WORK_DATE" + "\n");
                    strSqlString.Append("             , DAT.OPER_GRP" + "\n");
                    strSqlString.Append("             , DAT.OPER" + "\n");

                    // 2014-06-17-임종우 : DP 실적 조회 체크시 Stack 구분없이 전체 조회 되도록
                    if (ckdDP.Checked == true)
                    {
                        strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN NVL(END_QTY,0)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13))" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("             , CASE WHEN DAT.OPER IN ('A0030','A0040','A0200') AND MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') " + "\n");
                        strSqlString.Append("                         THEN DECODE(MAT.MAT_GRP_5, '2nd', NVL(END_QTY,0), 0)" + "\n");
                        strSqlString.Append("                    WHEN MAT.MAT_GRP_5 NOT IN ('-', '1st','Merge') AND MAT.MAT_GRP_5 NOT LIKE 'Middle%' THEN 0" + "\n");
                        strSqlString.Append("                    WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN NVL(END_QTY,0)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13))" + "\n");
                    }

                    strSqlString.Append("                    WHEN DAT.OPER IN ('A0030','A0040','A0200') THEN NVL(END_QTY,0)/NVL(GCM.DATA_1,1)" + "\n");
                    strSqlString.Append("                    ELSE END_QTY" + "\n");
                    strSqlString.Append("               END AS END_QTY" + "\n");
                    strSqlString.Append("             , CASE WHEN MAT_GRP_1 = 'HH' THEN 'HT Micron'" + "\n");
                    strSqlString.Append("                    WHEN MAT_GRP_1 = 'HX' THEN 'HYNIX'" + "\n");
                    strSqlString.Append("                    WHEN MAT_GRP_1 = 'SE' THEN 'SEC'" + "\n");
                    strSqlString.Append("                    ELSE 'Fabless'" + "\n");
                    strSqlString.Append("               END AS CUSTOMER_TYPE" + "\n");
                    strSqlString.Append("             , MAT.*" + "\n");
                    strSqlString.Append("          FROM MWIPMATDEF MAT " + "\n");
                    strSqlString.Append("             , ( " + "\n");
                    strSqlString.Append("                SELECT " + strDate + " AS WORK_DATE, MAT_ID" + "\n");
                    strSqlString.Append("                     , CASE WHEN OPER IN ('A0030', 'A0040') THEN 'B/G'" + "\n");
                    strSqlString.Append("                            WHEN OPER = 'A0200' THEN 'SAW'" + "\n");
                    strSqlString.Append("                            WHEN OPER = 'A0333' THEN 'C/A'" + "\n");
                    strSqlString.Append("                            WHEN OPER = 'A1000' THEN 'MOLD'" + "\n");
                    strSqlString.Append("                            WHEN OPER = 'A1300' THEN 'SBA'" + "\n");
                    strSqlString.Append("                            WHEN OPER IN ('A1750', 'A1800', 'A1900') THEN 'SST'" + "\n");
                    strSqlString.Append("                            WHEN OPER LIKE 'A040%' THEN 'D/A'" + "\n");
                    strSqlString.Append("                            ELSE 'W/B'" + "\n");
                    strSqlString.Append("                       END OPER_GRP" + "\n");
                    strSqlString.Append("                     , OPER" + "\n");
                    strSqlString.Append("                     , S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1 AS END_QTY" + "\n");
                    strSqlString.Append("                  FROM RSUMWIPMOV " + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND (OPER IN ('A0030','A0040','A0200', 'A0333', 'A1000', 'A1300', 'A1750', 'A1800', 'A1900') OR OPER LIKE 'A040%' OR OPER LIKE 'A060%') " + "\n");
                    strSqlString.Append("                   AND S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1 > 0 " + "\n");

                    if (cdvType.Text != "ALL")
                    {
                        strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
                    }

                    strSqlString.Append("                 UNION ALL" + "\n");
                    strSqlString.Append("                SELECT " + strDate + " AS WORK_DATE, MAT_ID" + "\n");
                    strSqlString.Append("                     , 'HMK3A' AS OPER_GRP " + "\n");
                    strSqlString.Append("                     , 'AZ010' AS OPER" + "\n");
                    strSqlString.Append("                     , S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1 AS END_QTY " + "\n");
                    strSqlString.Append("                  FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W'  " + "\n");
                    strSqlString.Append("                   AND FACTORY NOT IN ('RETURN') " + "\n");
                    strSqlString.Append("                   AND CM_KEY_1='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1 > 0 " + "\n");

                    if (cdvType.Text != "ALL")
                    {
                        strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
                    }

                    strSqlString.Append("                   AND MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND DELETE_FLAG = ' ' AND MAT_GRP_3 IN ('COB', 'SAWN')) " + "\n");
                    strSqlString.Append("               ) DAT" + "\n");
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT KEY_1 AS MAT_ID, DATA_1" + "\n");
                    strSqlString.Append("                  FROM MGCMTBLDAT " + "\n");
                    strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                   AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
                    strSqlString.Append("               ) GCM" + "\n");
                    strSqlString.Append("         WHERE 1=1 " + "\n");
                    strSqlString.Append("           AND DAT.MAT_ID = MAT.MAT_ID " + "\n");
                    strSqlString.Append("           AND DAT.MAT_ID = GCM.MAT_ID(+) " + "\n");
                    strSqlString.Append("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                    strSqlString.Append("           AND DAT.MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");
                    strSqlString.Append("           )" + "\n");
                    strSqlString.Append("           WHERE 1=1" + "\n");
                    strSqlString.Append("           AND MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ')" + "\n");


                    if (cdvOperGroup.Text != "ALL")
                    {
                        strSqlString.Append("           AND OPER_GRP " + cdvOperGroup.SelectedValueToQueryString + "\n");
                    }

                    // 상세 조회에 따른 SQL문 생성                        
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

                    strSqlString.Append("       ) " + "\n");
                    strSqlString.AppendFormat("GROUP BY {0}" + "\n", QueryCond1);
                    strSqlString.AppendFormat("ORDER BY {0}" + "\n", QueryCond2);
                    #endregion
                }
                else
                {
                    #region 환산
                    strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond3);
                    strSqlString.Append("     , ROUND(SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') OR OPER IN ('A0030', 'A0040', 'A0200') THEN END_QTY/NET_DIE ELSE END_QTY END) / " + sKpcsValue + ", 0) AS TTL" + "\n");

                    for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(CASE WHEN WORK_DATE = '{0}' THEN (CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') OR OPER IN ('A0030', 'A0040', 'A0200') THEN END_QTY/NET_DIE ELSE END_QTY END)" + "\n", selectDate[i].ToString());
                        strSqlString.AppendFormat("                    ELSE 0 END) / " + sKpcsValue + ", 0) AS END_QTY{0}" + "\n", i.ToString());
                    }

                    strSqlString.AppendFormat("     , ROUND(SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') OR OPER IN ('A0030', 'A0040', 'A0200') THEN END_QTY/NET_DIE ELSE END_QTY END) / {0} / {1}, 1) AS AVG_QTY" + "\n", sKpcsValue, cdvFromToDate.SubtractBetweenFromToDate + 1);

                    if (tableForm.SelectedValueToQueryContainNull.Contains("MAT_ID") == true)
                    {
                        strSqlString.Append("     , MAX(TO_NUMBER(WIRE_CNT)) AS WIRE_CNT" + "\n");
                    }

                    strSqlString.Append("  FROM (" + "\n");
                    strSqlString.Append("        SELECT * FROM" + "\n");
                    strSqlString.Append("        (" + "\n");
                    strSqlString.Append("        SELECT DAT.WORK_DATE" + "\n");
                    strSqlString.Append("             , DAT.OPER_GRP" + "\n");
                    strSqlString.Append("             , DAT.OPER" + "\n");
                    strSqlString.Append("             , WIR.WIRE_CNT" + "\n");

                    // 2014-06-17-임종우 : DP 실적 조회 체크시 Stack 구분없이 전체 조회 되도록
                    if (ckdDP.Checked == true)
                    {
                        strSqlString.Append("             , CASE WHEN DAT.OPER IN ('A0030','A0040','A0200') THEN NVL(END_QTY,0)/NVL(GCM.DATA_1,1)" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("             , CASE WHEN DAT.OPER IN ('A0030','A0040','A0200') AND MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') " + "\n");
                        strSqlString.Append("                         THEN DECODE(MAT.MAT_GRP_5, '2nd', NVL(END_QTY,0), 0)" + "\n");
                        strSqlString.Append("                    WHEN MAT.MAT_GRP_5 NOT IN ('-', '1st','Merge') AND MAT.MAT_GRP_5 NOT LIKE 'Middle%' THEN 0" + "\n");
                        strSqlString.Append("                    WHEN DAT.OPER IN ('A0030','A0040','A0200') THEN NVL(END_QTY,0)/NVL(GCM.DATA_1,1)" + "\n");
                    }

                    //strSqlString.Append("                    WHEN DAT.OPER LIKE 'A040%' AND MAT.MAT_GRP_5 = '1st' THEN NVL(END_QTY,0) * NVL(GCM.DATA_1,1)" + "\n");
                    strSqlString.Append("                    WHEN DAT.OPER LIKE 'A040%' THEN NVL(END_QTY,0) * NVL(GCM.DATA_1,1)" + "\n");
                    strSqlString.Append("                    WHEN DAT.OPER LIKE 'A060%' THEN NVL(END_QTY,0) * (CASE WHEN WIR.WIRE_CNT IS NOT NULL THEN WIR.WIRE_CNT" + "\n");
                    strSqlString.Append("                                                                           WHEN MAT.MAT_GRP_6 NOT IN ('-','0') THEN MAT.MAT_GRP_6" + "\n");
                    strSqlString.Append("                                                                           ELSE '1' END)" + "\n");
                    strSqlString.Append("                    ELSE END_QTY" + "\n");
                    strSqlString.Append("               END AS END_QTY" + "\n");
                    strSqlString.Append("             , CASE WHEN MAT_GRP_1 = 'HH' THEN 'HT Micron'" + "\n");
                    strSqlString.Append("                    WHEN MAT_GRP_1 = 'HX' THEN 'HYNIX'" + "\n");
                    strSqlString.Append("                    WHEN MAT_GRP_1 = 'SE' THEN 'SEC'" + "\n");
                    strSqlString.Append("                    ELSE 'Fabless'" + "\n");
                    strSqlString.Append("               END AS CUSTOMER_TYPE" + "\n");
                    strSqlString.Append("             , MAT.*, DECODE(MAT_CMF_13, '0', 1, ' ', 1, '-', 1, MAT_CMF_13) AS NET_DIE" + "\n");
                    strSqlString.Append("          FROM MWIPMATDEF MAT " + "\n");
                    strSqlString.Append("             , ( " + "\n");
                    strSqlString.Append("                SELECT " + strDate + " AS WORK_DATE, MAT_ID" + "\n");
                    strSqlString.Append("                     , CASE WHEN OPER IN ('A0030', 'A0040') THEN 'B/G'" + "\n");
                    strSqlString.Append("                            WHEN OPER = 'A0200' THEN 'SAW'" + "\n");
                    strSqlString.Append("                            WHEN OPER = 'A0333' THEN 'C/A'" + "\n");
                    strSqlString.Append("                            WHEN OPER = 'A1000' THEN 'MOLD'" + "\n");
                    strSqlString.Append("                            WHEN OPER = 'A1300' THEN 'SBA'" + "\n");
                    strSqlString.Append("                            WHEN OPER IN ('A1750', 'A1800', 'A1900') THEN 'SST'" + "\n");
                    strSqlString.Append("                            WHEN OPER LIKE 'A040%' THEN 'D/A'" + "\n");
                    strSqlString.Append("                            ELSE 'W/B'" + "\n");
                    strSqlString.Append("                       END OPER_GRP" + "\n");
                    strSqlString.Append("                     , OPER" + "\n");
                    strSqlString.Append("                     , S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1 AS END_QTY" + "\n");
                    strSqlString.Append("                  FROM RSUMWIPMOV " + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND (OPER IN ('A0030','A0040','A0200', 'A0333', 'A1000', 'A1300', 'A1750', 'A1800', 'A1900') OR OPER LIKE 'A040%' OR OPER LIKE 'A060%') " + "\n");
                    strSqlString.Append("                   AND S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1 > 0 " + "\n");

                    if (cdvType.Text != "ALL")
                    {
                        strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
                    }

                    strSqlString.Append("                 UNION ALL" + "\n");
                    strSqlString.Append("                SELECT " + strDate + " AS WORK_DATE, MAT_ID" + "\n");
                    strSqlString.Append("                     , 'HMK3A' AS OPER_GRP " + "\n");
                    strSqlString.Append("                     , 'AZ010' AS OPER" + "\n");
                    strSqlString.Append("                     , S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1 AS END_QTY " + "\n");
                    strSqlString.Append("                  FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W'  " + "\n");
                    strSqlString.Append("                   AND FACTORY NOT IN ('RETURN') " + "\n");
                    strSqlString.Append("                   AND CM_KEY_1='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1 > 0 " + "\n");

                    if (cdvType.Text != "ALL")
                    {
                        strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
                    }

                    //strSqlString.Append("                   AND MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND DELETE_FLAG = ' ' AND MAT_GRP_3 IN ('COB', 'SAWN')) " + "\n");
                    strSqlString.Append("               ) DAT" + "\n");
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT KEY_1 AS MAT_ID, DATA_1" + "\n");
                    strSqlString.Append("                  FROM MGCMTBLDAT " + "\n");
                    strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                   AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
                    strSqlString.Append("               ) GCM" + "\n");
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT MAT_ID, OPER, TCD_CMF_2 AS WIRE_CNT" + "\n");
                    strSqlString.Append("                  FROM CWIPTCDDEF@RPTTOMES " + "\n");
                    strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                   AND OPER LIKE 'A060%'" + "\n");
                    strSqlString.Append("                   AND TCD_CMF_2 <> ' '" + "\n");
                    strSqlString.Append("                   AND SET_FLAG = 'Y'" + "\n");
                    strSqlString.Append("               ) WIR" + "\n");
                    strSqlString.Append("         WHERE 1=1 " + "\n");
                    strSqlString.Append("           AND DAT.MAT_ID = MAT.MAT_ID " + "\n");
                    strSqlString.Append("           AND DAT.MAT_ID = GCM.MAT_ID(+) " + "\n");
                    strSqlString.Append("           AND DAT.MAT_ID = WIR.MAT_ID(+) " + "\n");
                    strSqlString.Append("           AND DAT.OPER = WIR.OPER(+) " + "\n");
                    strSqlString.Append("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                    strSqlString.Append("           AND MAT.DELETE_FLAG = ' ' " + "\n");
                    //strSqlString.Append("           AND MAT.MAT_GRP_3 NOT IN ('COB', 'SAWN') " + "\n");
                    strSqlString.Append("           AND DAT.MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");
                    strSqlString.Append("           )" + "\n");
                    strSqlString.Append("           WHERE 1=1" + "\n");
                    strSqlString.Append("           AND MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ')" + "\n");

                    if (cdvOperGroup.Text != "ALL")
                    {
                        strSqlString.Append("           AND OPER_GRP " + cdvOperGroup.SelectedValueToQueryString + "\n");
                    }

                    // 상세 조회에 따른 SQL문 생성                        
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

                    strSqlString.Append("       ) " + "\n");
                    strSqlString.AppendFormat("GROUP BY {0}" + "\n", QueryCond1);
                    strSqlString.AppendFormat("ORDER BY {0}" + "\n", QueryCond2);
                    #endregion
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

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);


                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    //by John
                    //1.Griid 합계 표시
                    //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, spdData.ActiveSheet.ColumnCount - 4, null, null, btnSort);
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 17, null, null, btnSort);
                    //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                    //3. Total부분 셀머지
                    //spdData.RPT_FillDataSelectiveCells("Total", 0, spdData.ActiveSheet.ColumnCount - 4, 0, 1, true, Align.Center, VerticalAlign.Center);
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 17, 0, 1, true, Align.Center, VerticalAlign.Center);
                }
                else
                {
                    //by John
                    //1.Griid 합계 표시
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort);
                    //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                    //3. Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);
                }
                               
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

                rdbMain.Checked = true;

                rdbConvert.Checked = false;
                rdbConvert.Enabled = false;

                ckdDP.Checked = false;
                ckdDP.Enabled = false;

                cdvBigOperGroup.Visible = true;

                cdvOperGroup.ConditionText = "Operation subgroup";

                ckbKpcs.Checked = false;

                cdvType.Enabled = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;

                rdbMain.Checked = true;

                rdbConvert.Checked = false;
                rdbConvert.Enabled = true;

                ckdDP.Checked = false;
                ckdDP.Enabled = true;

                cdvBigOperGroup.Visible = false;

                cdvOperGroup.ConditionText = "Operation group";

                cdvType.Enabled = true;
            }

            this.SetFactory(cdvFactory.txtValue);
            cdvOperGroup.Init();
            cdvBigOperGroup.Init();

            SortInit();
        }
        #endregion

        private void cdvOperGroup_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                strQuery += "SELECT DISTINCT OPER_GRP_4 AS Code, '' AS Data" + "\n";
                strQuery += "  FROM MWIPOPRDEF " + "\n";
                strQuery += " WHERE 1=1 " + "\n";
                strQuery += "   AND FACTORY = 'HMKB1' " + "\n";
                strQuery += "ORDER BY OPER_GRP_4 " + "\n";
            }
            else
            {
                strQuery += "SELECT DECODE(ROWNUM, 1, A, 2, B, 3, C, 4, D, 5, E, 6, F, 7, G, 8, H, 9, I) AS Code, '' AS Data" + "\n";
                strQuery += "  FROM (SELECT 1 FROM DUAL CONNECT BY LEVEL <= 9) " + "\n";
                strQuery += "     , ( " + "\n";
                strQuery += "        SELECT 'B/G' AS A" + "\n";
                strQuery += "             , 'SAW' AS B, 'C/A' AS C " + "\n";
                strQuery += "             , 'D/A' AS D, 'W/B' AS E " + "\n";
                strQuery += "             , 'MOLD' AS F, 'SBA' AS G " + "\n";
                strQuery += "             , 'SST' AS H, 'HMK3A' AS I " + "\n";
                strQuery += "           FROM DUAL " + "\n";
                strQuery += "       ) " + "\n";
            }

            cdvOperGroup.sDynamicQuery = strQuery;
        }

        private void cdvBigOperGroup_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                strQuery += "SELECT DISTINCT OPER_GRP_1 AS Code, '' AS Data" + "\n";
                strQuery += "  FROM MWIPOPRDEF " + "\n";
                strQuery += " WHERE 1=1 " + "\n";
                strQuery += "   AND FACTORY = 'HMKB1' " + "\n";
                strQuery += "ORDER BY OPER_GRP_1 " + "\n";               

                cdvBigOperGroup.sDynamicQuery = strQuery;
            }
        }


    }
}