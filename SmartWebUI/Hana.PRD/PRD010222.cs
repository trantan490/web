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
    public partial class PRD010222 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();
        private String sDispWeek = null;

        /// <summary>
        /// 클  래  스: PRD010222<br/>
        /// 클래스요약: 삼성 투입 관리<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2014-06-20<br/>
        /// 상세  설명: 삼성 투입 관리(임태성 요청)<br/>
        /// 변경  내용: <br/>    
        /// 2014-06-26-임종우 : DA CAPA 대비 DA 재공이 50% 이하이면 음영표시 (임태성K 요청)
        /// 2014-08-20-임종우 : 재공, 실적 COMP 제품 로직 반영 A0395 공정 까지.. (임태성K 요청)
        ///                   : DA Capa 효율 75% -> 70%로 변경 (임태성K 요청)
        /// 2014-09-25-임종우 : A0250 공정 재공 SAW -> DA 포함 되도록 변경 (김권수D 요청)
        /// 2019-02-07-임종우 : 창고 재고 과거 데이터 테이블 변경 ZHMMT111@SAPREAL -> CWMSLOTSTS_BOH
        /// </summary>
        public PRD010222()
        {
            InitializeComponent();                                   
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;    
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
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
            
            String ss = DateTime.Now.ToString("MM-dd");
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");

            LabelTextChange();

            try
            {               
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("LD Count", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Product", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("Code", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);

                if (ckdInform.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Standard information", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Thick", 1, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Tape", 1, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Net", 1, 9, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 50);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Standard information", 0, 7, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Thick", 1, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Tape", 1, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Net", 1, 9, Visibles.False, Frozen.True, Align.Right, Merge.True, Formatter.Number, 50);
                }

                spdData.RPT_MerageHeaderColumnSpan(0, 7, 3);

                spdData.RPT_AddBasicColumn("CTRL", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                              
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    spdData.RPT_AddBasicColumn(sDispWeek + " 주차 (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + " 기준)", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn(sDispWeek + " 주차 (" + cdvDate.Value.ToString("yy.MM.dd") + " 22:00 기준)", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                }

                spdData.RPT_AddBasicColumn("Plan", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Input rate", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 11, 4);

                spdData.RPT_AddBasicColumn("Stock", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Kit Rate Status", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("Wip", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Combine", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Kit 율", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Input required", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 16, 4);

                spdData.RPT_AddBasicColumn("PCB stock", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("WIP", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("B/G", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Saw", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TTL", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 21, 4);

                spdData.RPT_AddBasicColumn("Working week remaining volume", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("B/G", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Saw", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_MerageHeaderColumnSpan(0, 25, 2);

                spdData.RPT_AddBasicColumn("Work daily goal", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("B/G", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Saw", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_MerageHeaderColumnSpan(0, 27, 2);

                spdData.RPT_AddBasicColumn("D/A CAPA", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 15, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 20, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 29, 2);
                
                spdData.RPT_ColumnConfigFromTable(btnSort);              

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
            /*
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "G.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = G.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MIN(FAC_SEQ)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "G.MAT_GRP_2", "G.MAT_GRP_2 AS FAMILY", "MIN(FAB_SEQ) AS FAMILY", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "G.MAT_GRP_10", "G.MAT_GRP_10 AS PACKAGE", true);


            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "G.MAT_GRP_4", "G.MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "G.MAT_GRP_5", "G.MAT_GRP_5 AS TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "G.MAT_GRP_6", "G.MAT_GRP_6 AS \"LD COUNT\"", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "G.MAT_GRP_7", "G.MAT_GRP_7 AS DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "G.MAT_GRP_8", "G.MAT_GRP_8 AS GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "G.MAT_CMF_10", "G.MAT_CMF_10 AS PIN_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID AS PRODUCT", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "G.MAT_CMF_7", "G.MAT_CMF_7 AS CUST_DEVICE", false);
            */
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

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;
            string QueryCond4;

            string today;
            string sKpcsValue;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            today = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }

            strSqlString.Append("SELECT MAT_GRP_1, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_ID, PKG_CODE, MAT_GRP_5" + "\n");
            strSqlString.Append("     , BG_1, WM_TYPE1, MAT_CMF_13, CTRL" + "\n");
            strSqlString.Append("     , ROUND(WEEK_PLAN / " + sKpcsValue + ", 0) AS WEEK_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SHP_QTY / " + sKpcsValue + ", 0) AS SHP_QTY" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN WEEK_PLAN = 0 THEN 0" + "\n");
            strSqlString.Append("                  ELSE SHP_QTY / WEEK_PLAN * 100" + "\n");
            strSqlString.Append("             END, 2) AS IN_PER" + "\n");            
            strSqlString.Append("     , ROUND((WEEK_PLAN - SHP_QTY) / " + sKpcsValue + ", 0) AS IN_DEF" + "\n");
            strSqlString.Append("     , ROUND(STOCK / " + sKpcsValue + ", 0) AS STOCK" + "\n");
            strSqlString.Append("     , ROUND(LINE / " + sKpcsValue + ", 0) AS LINE" + "\n");
            strSqlString.Append("     , ROUND(COMBINE / " + sKpcsValue + ", 0) AS COMBINE" + "\n");
            strSqlString.Append("     , ROUND(DECODE(COMBINE, 0, 0, LINE / COMBINE * 100),0) AS KIT_PER" + "\n");
            strSqlString.Append("     , ROUND((COMBINE - LINE) / " + sKpcsValue + ", 0) AS NEED_IN" + "\n");
            strSqlString.Append("     , ROUND(MAT_TTL / " + sKpcsValue + ", 0) AS MAT_TTL" + "\n");
            strSqlString.Append("     , ROUND(BG / " + sKpcsValue + ", 0) AS COMBINE" + "\n");
            strSqlString.Append("     , ROUND(SAW / " + sKpcsValue + ", 0) AS SAW" + "\n");
            strSqlString.Append("     , ROUND(DA / " + sKpcsValue + ", 0) AS DA" + "\n");
            strSqlString.Append("     , ROUND(TTL / " + sKpcsValue + ", 0) AS TTL" + "\n");
            strSqlString.Append("     , ROUND(((WEEK_PLAN - SHP_QTY) + BG) / " + sKpcsValue + ", 0) AS WEEK_DEF_BG" + "\n");
            strSqlString.Append("     , ROUND(((WEEK_PLAN - SHP_QTY) + BG + SAW) / " + sKpcsValue + ", 0) AS WEEK_DEF_SAW" + "\n");
            strSqlString.Append("     , ROUND(((WEEK_PLAN - SHP_QTY) + BG) / DEF_DAY / " + sKpcsValue + ", 0) AS DAY_TARGET_BG" + "\n");
            strSqlString.Append("     , ROUND(((WEEK_PLAN - SHP_QTY) + BG + SAW) / DEF_DAY / " + sKpcsValue + ", 0) AS DAY_TARGET_SAW" + "\n");
            strSqlString.Append("     , ROUND(CAPA / " + sKpcsValue + ", 0) AS CAPA" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT MAT_GRP_1, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT.MAT_ID, PKG_CODE, MAT_GRP_5" + "\n");
            strSqlString.Append("             , BG_1, WM_TYPE1, MAT_CMF_13, CTRL" + "\n");
            strSqlString.Append("             , NVL(WEEK_PLAN,0) AS WEEK_PLAN" + "\n");
            strSqlString.Append("             , CASE WHEN MAT_GRP_5 IN ('1st','2nd') THEN NVL(AO_QTY,0) + NVL(LINE,0) + NVL(MID_MER,0)" + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_5 = '3rd' THEN NVL(AO_QTY,0) + NVL(LINE,0) + NVL(MIDDLE1,0) + NVL(MIDDLE2,0) + NVL(MER,0)" + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_5 = '4th' THEN NVL(AO_QTY,0) + NVL(LINE,0) + NVL(MIDDLE2,0) + NVL(MER,0)" + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_5 = '5th' THEN NVL(AO_QTY,0) + NVL(LINE,0) + NVL(MER,0)" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END AS SHP_QTY" + "\n");
            //strSqlString.Append("             , NVL(AO_QTY,0) + NVL(LINE,0) + NVL(MID_MER,0) AS SHP_QTY" + "\n");
            //strSqlString.Append("             , ROUND(CASE WHEN NVL(WEEK_PLAN,0) = 0 THEN 0" + "\n");
            //strSqlString.Append("                          ELSE (NVL(AO_QTY,0) + NVL(LINE,0) + NVL(MID_MER,0)) / WEEK_PLAN * 100" + "\n");
            //strSqlString.Append("                     END, 2) AS IN_PER" + "\n");
            //strSqlString.Append("             , NVL(WEEK_PLAN,0) - (NVL(AO_QTY,0) + NVL(LINE,0) + NVL(MID_MER,0)) AS IN_DEF" + "\n");
            strSqlString.Append("             , NVL(STOCK,0) AS STOCK" + "\n");
            strSqlString.Append("             , NVL(LINE,0) AS LINE" + "\n");
            strSqlString.Append("             , CASE WHEN MAT_GRP_5 IN ('-', '1st') THEN NVL(WMS.MAT_TTL,0) - NVL(LINE,0) ELSE 0 END AS MAT_TTL" + "\n");            
            strSqlString.Append("             , CASE WHEN MAT_GRP_5 IN ('1st','2nd') THEN NVL(ST1,0)" + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_5 = '3rd' THEN NVL(ST1,0) + NVL(MIDDLE,0)" + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_5 = '4th' THEN NVL(ST1,0) + NVL(MIDDLE,0) + NVL(MIDDLE1,0)" + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_5 = '5th' THEN NVL(ST1,0) + NVL(MIDDLE,0) + NVL(MIDDLE1,0) + NVL(MIDDLE2,0)" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END AS COMBINE" + "\n");
            strSqlString.Append("             , BG, SAW, DA, TTL" + "\n");
            strSqlString.Append("             , CASE WHEN MAT_GRP_5 = '2nd' THEN " + lblDef.Text + " + 0.5" + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_5 = '3rd' THEN " + lblDef.Text + " + 1" + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_5 = '4th' THEN " + lblDef.Text + " + 1.5" + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_5 = '5th' THEN " + lblDef.Text + " + 2" + "\n");

            // 과거이면서 월요일은 workday(7) - 경과일(7) 이 되어 0이 되기에 오류 발생으로 하기와 같이 처리 함
            if (lblDef.Text == "0")
            {
                strSqlString.Append("                    ELSE 0.0000001" + "\n");
            }
            else
            {
                strSqlString.Append("                    ELSE " + lblDef.Text + "\n");
            }

            strSqlString.Append("               END AS DEF_DAY" + "\n");
            strSqlString.Append("             , NVL(CAP.CAPA,0) AS CAPA" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MAT_GRP_1, MAT_GRP_5, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10" + "\n");
            strSqlString.Append("                     , CTRL, MAT_ID, PKG_CODE" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0000', QTY_1, 0)) AS STOCK" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0000', 0, QTY_1)) AS LINE" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0001' AND 'A0040' THEN QTY_1 ELSE 0 END) AS BG" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0041' AND 'A0300' AND OPER <> 'A0250' THEN QTY_1 ELSE 0 END) AS SAW" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0301' AND 'A0409' OR OPER = 'A0250' THEN QTY_1 ELSE 0 END) AS DA   " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0001' AND 'A0409' THEN QTY_1 ELSE 0 END) AS TTL" + "\n");
            strSqlString.Append("                     , MAX(BG_1) AS BG_1" + "\n");
            strSqlString.Append("                     , MAX(WM_TYPE1) AS WM_TYPE1" + "\n");
            strSqlString.Append("                     , MAX(MAT_CMF_13) AS MAT_CMF_13" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT A.MAT_GRP_1, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_9, A.MAT_GRP_10               " + "\n");
            strSqlString.Append("                             , CASE WHEN A.MAT_ID LIKE 'SEKS3%' THEN SUBSTR(A.MAT_ID, INSTR(A.MAT_ID, '-')-3, 3)" + "\n");
            strSqlString.Append("                                    ELSE ' '" + "\n");
            strSqlString.Append("                               END CTRL" + "\n");
            strSqlString.Append("                             , CASE WHEN A.MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(A.MAT_ID, -3)" + "\n");
            strSqlString.Append("                                    ELSE A.MAT_ID" + "\n");
            strSqlString.Append("                               END MAT_ID" + "\n");
            strSqlString.Append("                             , SUBSTR(A.MAT_ID, -3) AS PKG_CODE" + "\n");
            strSqlString.Append("                             , B.OPER " + "\n");
            strSqlString.Append("                             , CASE WHEN B.OPER > 'A0395' THEN B.QTY_1 " + "\n");
            strSqlString.Append("                                    ELSE B.QTY_1 / NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) ,1) " + "\n");
            strSqlString.Append("                               END QTY_1" + "\n");
            strSqlString.Append("                             , C.BG_1" + "\n");
            strSqlString.Append("                             , C.WM_TYPE1" + "\n");
            strSqlString.Append("                             , A.MAT_CMF_13 " + "\n");
            strSqlString.Append("                          FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                             , CLOTCRDDAT@RPTTOMES C" + "\n");

            // 금일조회 기준
            if (DateTime.Now.ToString("yyyyMMdd") == today)
            {
                strSqlString.Append("                             , RWIPLOTSTS B" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.Append("                             , RWIPLOTSTS_BOH B" + "\n");
                strSqlString.Append("                         WHERE CUTOFF_DT = '" + today + "22' " + "\n");
            }

            strSqlString.Append("                           AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("                           AND A.FACTORY = C.FACTORY(+)" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND A.MAT_GRP_1 = 'SE'" + "\n");
            strSqlString.Append("                           AND A.MAT_GRP_5 <> 'Merge'" + "\n");
            strSqlString.Append("                           AND A.MAT_GRP_5 NOT LIKE 'Middle%'" + "\n");
            strSqlString.Append("                           AND B.LOT_DEL_FLAG(+) = ' ' " + "\n");
            strSqlString.Append("                           AND B.LOT_TYPE(+) = 'W'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND B.LOT_CMF_5(+) LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            // Product
            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat(" AND A.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_GRP_1, MAT_GRP_5, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10, CTRL, MAT_ID, PKG_CODE" + "\n");
            strSqlString.Append("               ) MAT" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");

            //if (cdvWeek.Text == "기본")
            if (cdvWeek.SelectedIndex == 0)
            {
                strSqlString.Append("                     , CASE WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'DY') IN ('일','월','토') THEN WEEK_PLAN_THIS" + "\n");
                strSqlString.Append("                            ELSE WEEK_PLAN_TTL" + "\n");
                strSqlString.Append("                       END AS WEEK_PLAN" + "\n");
            }
            //else if (cdvWeek.Text == "당주")
            else if (cdvWeek.SelectedIndex == 1)
            {
                strSqlString.Append("                     , WEEK_PLAN_THIS AS WEEK_PLAN" + "\n");
            }
            else
            {
                strSqlString.Append("                     , WEEK_PLAN_TTL AS WEEK_PLAN" + "\n");
            }

            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT CASE WHEN MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(MAT_ID, -3)" + "\n");
            strSqlString.Append("                                    ELSE MAT_ID" + "\n");
            strSqlString.Append("                               END MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', WW_QTY, 0)) AS WEEK_PLAN_THIS" + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.NextWeek + "', WW_QTY, 0)) AS WEEK_PLAN_NEXT" + "\n");
            strSqlString.Append("                             , SUM(WW_QTY) AS WEEK_PLAN_TTL" + "\n");
            strSqlString.Append("                          FROM RWIPPLNWEK" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND GUBUN = 3" + "\n");
            strSqlString.Append("                           AND CUSTOMER = 'SE'" + "\n");
            strSqlString.Append("                           AND PLAN_WEEK IN ('" + FindWeek.ThisWeek + "','" + FindWeek.NextWeek + "')" + "\n");
            strSqlString.Append("                         GROUP BY CASE WHEN MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(MAT_ID, -3) ELSE MAT_ID END" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("               ) PLN" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT CASE WHEN A.MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(A.MAT_ID, -3)" + "\n");
            strSqlString.Append("                            ELSE A.MAT_ID" + "\n");
            strSqlString.Append("                       END MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN B.MAT_GRP_5 <> '1st' THEN 0 " + "\n");
            strSqlString.Append("                                WHEN A.OPER > 'A0395' THEN QTY_1 " + "\n");
            strSqlString.Append("                                ELSE QTY_1 / NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) ,1) " + "\n");
            strSqlString.Append("                           END) ST1 " + "\n");
            //strSqlString.Append("                     , SUM(DECODE(B.MAT_GRP_5, '1st', QTY_1, 0)) AS ST1 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(B.MAT_GRP_5, 'Middle', QTY_1, 0)) AS MIDDLE " + "\n");
            strSqlString.Append("                     , SUM(DECODE(B.MAT_GRP_5, 'Middle 1', QTY_1, 0)) AS MIDDLE1" + "\n");
            strSqlString.Append("                     , SUM(DECODE(B.MAT_GRP_5, 'Middle 2', QTY_1, 0)) AS MIDDLE2" + "\n");
            strSqlString.Append("                     , SUM(DECODE(B.MAT_GRP_5, 'Merge', QTY_1, 0)) AS MER" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN REGEXP_LIKE(B.MAT_GRP_5, 'Middle*|Merge') THEN QTY_1 ELSE 0 END) AS MID_MER" + "\n");

            // 금일조회 기준
            if (DateTime.Now.ToString("yyyyMMdd") == today)
            {
                strSqlString.Append("                  FROM RWIPLOTSTS A " + "\n");
                strSqlString.Append("                     , MWIPMATDEF B " + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("                     , MWIPMATDEF B " + "\n");
                strSqlString.Append("                 WHERE CUTOFF_DT = '" + today + "22' " + "\n");
            }

            strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND A.LOT_TYPE = 'W'  " + "\n");
            strSqlString.Append("                   AND A.OPER <> 'A0000'" + "\n");
            strSqlString.Append("                   AND B.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                   AND B.MAT_GRP_1 = 'SE' " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                 GROUP BY CASE WHEN A.MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(A.MAT_ID, -3) ELSE A.MAT_ID END " + "\n");
            strSqlString.Append("               ) WIP" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT CASE WHEN MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(MAT_ID, -3)" + "\n");
            strSqlString.Append("                            ELSE MAT_ID" + "\n");
            strSqlString.Append("                       END MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS AO_QTY" + "\n");
            strSqlString.Append("                  FROM RSUMFACMOV" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + today + "'" + "\n");
            strSqlString.Append("                   AND FACTORY <> 'RETURN'" + "\n");
            strSqlString.Append("                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND MAT_ID LIKE 'SE%'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                 GROUP BY CASE WHEN MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(MAT_ID, -3) ELSE MAT_ID END" + "\n");
            strSqlString.Append("               ) SHP" + "\n");
            strSqlString.Append("             , (         " + "\n");
            strSqlString.Append("                SELECT CASE WHEN RES.MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(RES.MAT_ID, -3)" + "\n");
            strSqlString.Append("                            ELSE RES.MAT_ID" + "\n");
            strSqlString.Append("                       END MAT_ID" + "\n");
            strSqlString.Append("                     , STACK" + "\n");
            strSqlString.Append("                     , SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.7 * RES.RAS_CNT, 0))) AS CAPA       " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT FACTORY, RES_STS_2 AS MAT_ID" + "\n");
            strSqlString.Append("                             , RES_STS_8 AS OPER" + "\n");
            strSqlString.Append("                             , CASE WHEN RES_STS_8 IN ('A0333','A0400') THEN '-'" + "\n");
            strSqlString.Append("                                    WHEN RES_STS_8 = 'A0401' THEN '1st'" + "\n");
            strSqlString.Append("                                    WHEN RES_STS_8 = 'A0402' THEN '2nd'" + "\n");
            strSqlString.Append("                                    WHEN RES_STS_8 = 'A0403' THEN '3rd'" + "\n");
            strSqlString.Append("                                    WHEN RES_STS_8 = 'A0404' THEN '4th'" + "\n");
            strSqlString.Append("                                    WHEN RES_STS_8 = 'A0405' THEN '5th'" + "\n");
            strSqlString.Append("                                    ELSE ''" + "\n");
            strSqlString.Append("                               END STACK" + "\n");
            strSqlString.Append("                             , RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RAS_CNT " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == today)
            {
                strSqlString.Append("                          FROM MRASRESDEF " + "\n");
                strSqlString.Append("                         WHERE 1 = 1  " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM MRASRESDEF_BOH " + "\n");
                strSqlString.Append("                         WHERE 1 = 1  " + "\n");
                strSqlString.Append("                           AND CUTOFF_DT = '" + today + "22' " + "\n");
            }


            strSqlString.Append("                           AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                           AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                           AND RES_STS_2 LIKE 'SE%'" + "\n");
            strSqlString.Append("                           AND (RES_STS_8 LIKE 'A04%' OR RES_STS_8 = 'A0333')" + "\n");
            strSqlString.Append("                         GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7 " + "\n");
            strSqlString.Append("                       ) RES " + "\n");
            strSqlString.Append("                     , CRASUPHDEF UPH" + "\n");
            strSqlString.Append("                 WHERE 1 = 1 " + "\n");
            strSqlString.Append("                   AND RES.FACTORY = UPH.FACTORY(+) " + "\n");
            strSqlString.Append("                   AND RES.OPER = UPH.OPER(+) " + "\n");
            strSqlString.Append("                   AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("                   AND RES.UPEH_GRP = UPH.UPEH_GRP(+) " + "\n");
            strSqlString.Append("                   AND RES.MAT_ID = UPH.MAT_ID(+)            " + "\n");
            strSqlString.Append("                 GROUP BY CASE WHEN RES.MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(RES.MAT_ID, -3) ELSE RES.MAT_ID END, STACK" + "\n");
            strSqlString.Append("               ) CAP" + "\n");

            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT CASE WHEN MAT.MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(MAT.MAT_ID, -3)" + "\n");
            strSqlString.Append("                            ELSE MAT.MAT_ID" + "\n");
            strSqlString.Append("                       END MAT_ID " + "\n");
            strSqlString.Append("                     , MAT.MAT_GRP_5 AS STACK" + "\n");
            strSqlString.Append("                     , MAX(NVL(SMM.UNIT_QTY,0)) AS USAGE" + "\n");
            strSqlString.Append("                     , MAX(NVL(WIP_MAT.TTL,0)) AS MAT_TTL " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF MAT  " + "\n");
            strSqlString.Append("                     , (   " + "\n");
            strSqlString.Append("                        SELECT DISTINCT NVL(P.MAT_ID,B.PARTNUMBER) PARTNUMBER, B.MATCODE, B.DESCRIPT, B.RESV_FIELD_2 AS MAT_TYPE, B.UNIT AS UNIT_1, B.UNIT_QTY, B.STEPID AS OPER " + "\n");
            strSqlString.Append("                          FROM CWIPMATDEF@RPTTOMES A  " + "\n");
            strSqlString.Append("                             , CWIPBOMDEF B  " + "\n");
            strSqlString.Append("                             , HRTDMCPROUT@RPTTOMES P    " + "\n");
            strSqlString.Append("                         WHERE 1=1  " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MATCODE  " + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND B.RESV_FIELD_2 <> ' '  " + "\n");
            strSqlString.Append("                           AND B.RESV_FLAG_1 = 'Y'  " + "\n");
            strSqlString.Append("                           AND B.STEPID <> 'A0300'  " + "\n");
            strSqlString.Append("                           AND B.MATCODE NOT LIKE '%-O'  " + "\n");
            strSqlString.Append("                           AND P.MAT_KEY(+) = B.PARTNUMBER   " + "\n");
            strSqlString.Append("                           AND B.RESV_FIELD_2 IN ('LF', 'PB')" + "\n");
            strSqlString.Append("                         ORDER BY MATCODE " + "\n");
            strSqlString.Append("                       ) SMM " + "\n");
            strSqlString.Append("                     , (  " + "\n");
            strSqlString.Append("                        SELECT REPLACE(A.MAT_ID, '-O', '') AS MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(NVL(B.INV_QTY,0)) + SUM(NVL(C.INL_QTY,0)) + SUM(NVL(D.WIK_WIP,0)) AS TTL " + "\n");
            strSqlString.Append("                          FROM MWIPMATDEF A     " + "\n");
            strSqlString.Append("                             , (     " + "\n");
            strSqlString.Append("                                SELECT MAT_ID " + "\n");
            strSqlString.Append("                                     , SUM(INV_QTY) AS INV_QTY" + "\n");
            strSqlString.Append("                                  FROM ( " + "\n");
            strSqlString.Append("                                        SELECT MAT_ID  " + "\n");
            strSqlString.Append("                                             , SUM(QUANTITY) AS INV_QTY" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == today)
            {
                strSqlString.Append("                                          FROM CWMSLOTSTS@RPTTOMES " + "\n");
                strSqlString.Append("                                         WHERE 1=1     " + "\n");                
            }
            else
            {
                strSqlString.Append("                                          FROM CWMSLOTSTS_BOH@RPTTOMES " + "\n");
                strSqlString.Append("                                         WHERE CUTOFF_DT = '" + today + "22'" + "\n");  
            }

            strSqlString.Append("                                           AND QUANTITY> 0     " + "\n");
            strSqlString.Append("                                           AND STORAGE_LOCATION IN ('1000', '1001', '1003')     " + "\n");
            strSqlString.Append("                                         GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("                                         UNION ALL " + "\n");
            strSqlString.Append("                                        SELECT MAT_ID, SUM(QTY_1) AS INV_QTY" + "\n");
            strSqlString.Append("                                          FROM CWIPMATSLP@RPTTOMES " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND RECV_FLAG = ' ' " + "\n");
            strSqlString.Append("                                           AND IN_TIME BETWEEN '" + cdvDate.Value.AddDays(-2).ToString("yyyyMMdd") + "000000' AND '" + today + "235959' " + "\n");
            strSqlString.Append("                                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                                       ) " + "\n");
            strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                               ) B     " + "\n");
            strSqlString.Append("                             , (     " + "\n");
            strSqlString.Append("                                SELECT MAT_ID " + "\n");
            strSqlString.Append("                                     , SUM(QTY_1) AS INL_QTY" + "\n");

            // 금일조회 기준
            if (DateTime.Now.ToString("yyyyMMdd") == today)
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS" + "\n");
                strSqlString.Append("                                 WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS_BOH" + "\n");
                strSqlString.Append("                                 WHERE CUTOFF_DT = '" + today + "22' " + "\n");
            }

            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                   AND LOT_TYPE != 'W'  " + "\n");
            strSqlString.Append("                                   AND LOT_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("                                   AND LOT_CMF_2 = '-'   " + "\n");
            strSqlString.Append("                                   AND LOT_CMF_9 != ' '   " + "\n");
            strSqlString.Append("                                   AND QTY_1 > 0   " + "\n");
            strSqlString.Append("                                   AND OPER NOT IN  ('00001', '00002', 'V0000')   " + "\n");
            strSqlString.Append("                                 GROUP BY MAT_ID    " + "\n");
            strSqlString.Append("                               ) C     " + "\n");
            strSqlString.Append("                             , ( " + "\n");
            strSqlString.Append("                                SELECT MAT_ID, SUM(LOT_QTY) AS WIK_WIP " + "\n");
            strSqlString.Append("                                  FROM ISTMWIKWIP@RPTTOMES " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND CUTOFF_DT = '" + today + "' || TO_CHAR(SYSDATE, 'HH24')" + "\n");
            strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                               ) D " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID(+) " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = D.MAT_ID(+)            " + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                         GROUP BY REPLACE(A.MAT_ID, '-O', '') " + "\n");
            strSqlString.Append("                         HAVING SUM(NVL(B.INV_QTY,0)) + SUM(NVL(C.INL_QTY,0)) + SUM(NVL(D.WIK_WIP,0)) > 0 " + "\n");
            strSqlString.Append("                       ) WIP_MAT " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = SMM.PARTNUMBER " + "\n");
            strSqlString.Append("                   AND SMM.MATCODE = WIP_MAT.MAT_ID(+) " + "\n");
            strSqlString.Append("                   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("                   AND MAT.MAT_GRP_5 IN ('-', '1st')" + "\n");
            strSqlString.Append("                   AND SMM.UNIT_QTY > 0 " + "\n");
            strSqlString.Append("                   AND WIP_MAT.TTL > 0" + "\n");
            strSqlString.Append("                 GROUP BY CASE WHEN MAT.MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(MAT.MAT_ID, -3) ELSE MAT.MAT_ID END,MAT.MAT_GRP_5" + "\n");
            strSqlString.Append("               ) WMS" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = CAP.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = WMS.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_GRP_5 = CAP.STACK(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_GRP_5 = WMS.STACK(+)" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND WEEK_PLAN + SHP_QTY + STOCK + LINE + COMBINE + CAPA > 0" + "\n");

            if (ckdPlan.Checked == true)
            {
                strSqlString.Append("   AND WEEK_PLAN > 0" + "\n");
            }

            strSqlString.Append(" ORDER BY MAT_GRP_1, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_ID, PKG_CODE, MAT_GRP_5" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {                       
            DataTable dt = null;
            double iDAWip = 0;
            double iDACapa = 0;
            double fDaPer = 0;

            if (CheckField() == false) return;

            GridColumnInit();
            LabelTextChange();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                                
                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2); 

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 6, 11, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
                //spdData.Sheets[0].FrozenColumnCount = 3;
                //spdData.RPT_AutoFit(false);
                //spdData.DataSource = dt;
                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_AutoFit(false);

                //for (int i = 0; i <= 10; i++)
                //{
                //    spdData.ActiveSheet.Columns[i].BackColor = Color.LightYellow;
                //}
                
                Color color = spdData.ActiveSheet.Cells[1, 0].BackColor;
                
                // PKG CODE 부분 제외하고 Sub Total, Grand Total 삭제 하기...
                // DA CAPA 대비 DA 재공이 50% 이하이면 음영표시
                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, 0].BackColor != color)
                    {
                        spdData.ActiveSheet.Rows[i].Remove();
                        i = i - 1;
                    }

                    if (spdData.ActiveSheet.Cells[i, 1].BackColor != color)
                    {
                        spdData.ActiveSheet.Rows[i].Remove();
                        i = i - 1;
                    }

                    if (spdData.ActiveSheet.Cells[i, 2].BackColor != color)
                    {
                        spdData.ActiveSheet.Rows[i].Remove();
                        i = i - 1;
                    }

                    if (spdData.ActiveSheet.Cells[i, 3].BackColor != color)
                    {
                        spdData.ActiveSheet.Rows[i].Remove();
                        i = i - 1;
                    }

                    if (spdData.ActiveSheet.Cells[i, 4].BackColor != color)
                    {
                        spdData.ActiveSheet.Rows[i].Remove();
                        i = i - 1; // 삭제하게 되면 한줄 위로 올라가기에 -1 해서 다시 루프
                    }

                    // PKG CODE 기준의 SubTotal 은 표시 하되 안에 데이터는 지운다.
                    if (spdData.ActiveSheet.Cells[i, 5].BackColor != color)
                    {
                        //spdData.ActiveSheet.Rows[i].ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));
                        for (int y = 5; y < spdData.ActiveSheet.ColumnCount; y++)
                        {
                            spdData.ActiveSheet.Cells[i, y].Text = "";
                        }
                    }

                    iDAWip = Convert.ToInt32(spdData.ActiveSheet.Cells[i, 23].Value);
                    iDACapa = Convert.ToInt32(spdData.ActiveSheet.Cells[i, 29].Value);

                    // DA CAPA 대비 DA 재공이 50% 이하이면 음영표시
                    if (iDAWip != 0 && iDACapa != 0)
                    {
                        fDaPer = iDAWip / iDACapa * 100;

                        if (fDaPer < 50)
                        {
                            spdData.ActiveSheet.Cells[i, 23].BackColor = Color.Red;
                        }
                    }
                    
                }
                
                spdData.ActiveSheet.Rows[0].Remove();

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
                               
        private string MakeSqlString2(string sDate)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT TO_CHAR(TO_DATE('" + sDate + "', 'YYYYMMDD'), 'DY') AS TODAY" + "\n");
            strSqlString.Append("     , CASE WHEN TO_CHAR(TO_DATE('" + sDate + "', 'YYYYMMDD'), 'DY') IN ('일', '월') THEN '100'" + "\n");
            strSqlString.Append("            WHEN TO_CHAR(TO_DATE('" + sDate + "', 'YYYYMMDD'), 'DY') = '화' THEN '30'" + "\n");
            strSqlString.Append("            WHEN TO_CHAR(TO_DATE('" + sDate + "', 'YYYYMMDD'), 'DY') = '수' THEN '40'" + "\n");
            strSqlString.Append("            WHEN TO_CHAR(TO_DATE('" + sDate + "', 'YYYYMMDD'), 'DY') = '목' THEN '50'" + "\n");
            strSqlString.Append("            WHEN TO_CHAR(TO_DATE('" + sDate + "', 'YYYYMMDD'), 'DY') = '금' THEN '70'" + "\n");
            strSqlString.Append("            ELSE '90'" + "\n");
            strSqlString.Append("       END TARGET" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == sDate)
            {
                strSqlString.Append("     , ROUND(CASE TO_CHAR(TO_DATE('" + sDate + "', 'YYYYMMDD'), 'DY')" + "\n");
                strSqlString.Append("                  WHEN  '일' THEN 6 - TO_NUMBER(TRUNC(SYSDATE) + 22/24 - SYSDATE)" + "\n");
                strSqlString.Append("                  WHEN  '월' THEN 7 - TO_NUMBER(TRUNC(SYSDATE) + 22/24 - SYSDATE)" + "\n");
                strSqlString.Append("                  WHEN  '화' THEN 8 - TO_NUMBER(TRUNC(SYSDATE) + 22/24 - SYSDATE)" + "\n");
                strSqlString.Append("                  WHEN  '수' THEN 9 - TO_NUMBER(TRUNC(SYSDATE) + 22/24 - SYSDATE)" + "\n");
                strSqlString.Append("                  WHEN  '목' THEN 10 - TO_NUMBER(TRUNC(SYSDATE) + 22/24 - SYSDATE)" + "\n");
                strSqlString.Append("                  WHEN  '금' THEN 11 - TO_NUMBER(TRUNC(SYSDATE) + 22/24 - SYSDATE)" + "\n");
                strSqlString.Append("                  ELSE 5 - TO_NUMBER(TRUNC(SYSDATE) + 22/24 - SYSDATE)" + "\n");
                strSqlString.Append("             END,2) AS PROG_DAY " + "\n");
            }
            else
            {
                strSqlString.Append("     , ROUND(CASE TO_CHAR(TO_DATE('" + sDate + "', 'YYYYMMDD'), 'DY')" + "\n");
                strSqlString.Append("                  WHEN  '일' THEN 6" + "\n");
                strSqlString.Append("                  WHEN  '월' THEN 7" + "\n");
                strSqlString.Append("                  WHEN  '화' THEN 8" + "\n");
                strSqlString.Append("                  WHEN  '수' THEN 9" + "\n");
                strSqlString.Append("                  WHEN  '목' THEN 10" + "\n");
                strSqlString.Append("                  WHEN  '금' THEN 11" + "\n");
                strSqlString.Append("                  ELSE 5" + "\n");
                strSqlString.Append("             END,2) AS PROG_DAY " + "\n");
            }

            strSqlString.Append("   , CASE WHEN TO_CHAR(TO_DATE('" + sDate + "', 'YYYYMMDD'), 'DY') IN ('일', '월', '토') THEN 7" + "\n");
            strSqlString.Append("          ELSE 14" + "\n");
            strSqlString.Append("     END WORK_DAY_MIX" + "\n");
            strSqlString.Append("   , 7 AS WORK_DAY_THIS" + "\n");
            strSqlString.Append("   , 14 AS WORK_DAY_NEXT" + "\n");
            strSqlString.Append("FROM DUAL" + "\n");

            return strSqlString.ToString();
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                //StringBuilder Condition = new StringBuilder();
                //Condition.AppendFormat("기준일자: {0}     today: {1}      workday: {2}     remain: {3}      표준진도율: {4} " + "\n", cdvDate.Text, lblToday.Text.ToString(), lblTarget.Text.ToString(), lblWorkday.Text.ToString(), lblJindo.Text.ToString());
                //Condition.Append("        단위 : PKG (pcs) , COB (매) ");
                

                //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {
            DataTable dt = null;
            string sToday;
            string sTarget;
            string sProg_day;
            string sWork_day;
            
            // 지난주차의 마지막일 가져오기            
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(cdvDate.SelectedValue()));
            sToday = dt.Rows[0][0].ToString();
            sTarget = dt.Rows[0][1].ToString();
            sProg_day = dt.Rows[0][2].ToString();

            //if (cdvWeek.Text == "기본")
            if (cdvWeek.SelectedIndex == 0)
            {
                sWork_day = dt.Rows[0][3].ToString();

                if (sToday == "일" || sToday == "월" || sToday == "토")
                {
                    sDispWeek = FindWeek.ThisWeek.Substring(4, 2);
                }
                else
                {
                    sDispWeek = FindWeek.ThisWeek.Substring(4, 2) + "+" + FindWeek.NextWeek.Substring(4, 2);
                }
            }
            //else if (cdvWeek.Text == "당주")
            else if (cdvWeek.SelectedIndex == 1)
            {
                sWork_day = dt.Rows[0][4].ToString();
                sDispWeek = FindWeek.ThisWeek.Substring(4, 2);
            }
            else
            {
                sWork_day = dt.Rows[0][5].ToString();
                sDispWeek = FindWeek.ThisWeek.Substring(4, 2) + "+" + FindWeek.NextWeek.Substring(4, 2);
            }

            double def_day = (Convert.ToDouble(sWork_day)) - Convert.ToDouble(sProg_day);

            lblToday.Text = sToday + "요일";
            lblTarget.Text = sTarget + "%";
            lblJindo.Text = sProg_day;
            lblWorkday.Text = sWork_day;
            lblDef.Text = def_day.ToString();
            
        }
        #endregion
    }       
}
