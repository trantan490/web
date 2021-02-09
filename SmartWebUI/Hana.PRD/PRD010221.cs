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
    public partial class PRD010221 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010221<br/>
        /// 클래스요약: ER_CS긴급제품모니터링<br/>
        /// 작  성  자: 김태호<br/>
        /// 최초작성일: 2013-09-17<br/>
        /// 상세  설명: 생산진척현황_TEST<br/>
        /// 변경  내용: <br/>    
        /// 2013-11-28-임종우 : 투입일정, 출하요청일 쿼리 수정 -> A0000, AZ010 공정의 TARGET 일로 고정함(임태성 요청)
        ///                     H_ERCS_LOTINFO 삭제 FLAG 생성 됨에 따라 쿼리 수정
        /// 2013-12-03-임종우 : POP UP 창 오류 수정, 재공에 없는 제품도 표시 되도록 수정 (임태성 요청)
        /// 2013-12-05-임종우 : POP UP 창 Tatget Date 전 공정 표시 (임태성 요청)
        /// 2013-12-10-임종우 : 계획 값 중복 되는 오류 수정
        /// 2015-07-06-임종우 : 설비 대수 집계 시 DISPATCH 기준 정보가 'Y" 인 설비만 집계 (임태성K 요청)
        /// </summary>
        GlobalVariable.FindWeek FindWeek_SOP_A = new GlobalVariable.FindWeek();
        GlobalVariable.FindWeek FindWeek_SOP_T = new GlobalVariable.FindWeek();
        GlobalVariable.FindWeek FindWeek_RTF = new GlobalVariable.FindWeek();

        public PRD010221()
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
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            GetWorkDay();
            spdData.RPT_ColumnInit();


            DateTime Select_date;

            Select_date = cdvDate.Value;

            try
            {
                spdData.RPT_AddBasicColumn("업체", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Lead", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Part no", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LOT_ID", 0, 5, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Input schedule", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Shipment Request Date", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("the place of shipment", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("WIP Status", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TOTAL", 2, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HMK3A", 2, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("FINISH", 2, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("MOLD", 2, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB", 2, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA", 2, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SAW", 2, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("STOCK", 2, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 12, 8);

                spdData.RPT_AddBasicColumn("Equipment Arrange Status", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("댓수", 2, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("댓수", 2, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 20, 2);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 3);

                spdData.RPT_MerageHeaderRowSpan(0, 6, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 3);

                spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 14, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 15, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 16, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 17, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 18, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 19, 2);

                //spdData.RPT_ColumnConfigFromTable(btnSort);
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
        }
        #endregion

        #region 시간관련 함수
        private void GetWorkDay()
        {
            DateTime Now = cdvDate.Value;
            FindWeek_SOP_A = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
            FindWeek_SOP_T = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "SE");
            FindWeek_RTF = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "QC");

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
            string date;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            date = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());


            strSqlString.Append("SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_ID, ASSY_WIP.LOT_ID " + "\n");
            strSqlString.Append("     , ASSY_WIP.IN_DAY AS IN_DAY " + "\n");
            strSqlString.Append("     , ASSY_WIP.OUT_DAY AS OUT_DAY " + "\n");
            strSqlString.Append("     , ASSY_WIP.KEY_4 AS \"출하지\" " + "\n");
            strSqlString.Append("     , ASSY_WIP.PLAN AS PLAN " + "\n");
            strSqlString.Append("     , ASSY_WIP.SHIP AS SHIP " + "\n");
            strSqlString.Append("     , ASSY_WIP.DEF " + "\n");
            strSqlString.Append("     , ASSY_WIP.TOTAL " + "\n");
            strSqlString.Append("     , ASSY_WIP.HMK3A " + "\n");
            strSqlString.Append("     , ASSY_WIP.FINISH " + "\n");
            strSqlString.Append("     , ASSY_WIP.MOLD " + "\n");
            strSqlString.Append("     , ASSY_WIP.WB " + "\n");
            strSqlString.Append("     , ASSY_WIP.DA " + "\n");
            strSqlString.Append("     , ASSY_WIP.SAW " + "\n");
            strSqlString.Append("     , ASSY_WIP.STOCK " + "\n");
            strSqlString.Append("     , NVL(RES.DA_EQP_CNT,0) AS DA_EQP_CNT " + "\n");
            strSqlString.Append("     , NVL(RES.WB_EQP_CNT,0) AS WB_EQP_CNT  " + "\n");
            strSqlString.Append("  FROM MWIPMATDEF MAT  " + "\n");
            strSqlString.Append("     , (  " + "\n");
            strSqlString.Append("        SELECT MAT_ID, LOT_ID, MAT_GRP_3 " + "\n");
            strSqlString.Append("             , MAX(IN_DAY) AS IN_DAY " + "\n");
            strSqlString.Append("             , MAX(OUT_DAY) AS OUT_DAY " + "\n");
            strSqlString.Append("             , MAX(KEY_4) AS KEY_4 " + "\n");
            strSqlString.Append("             , MAX(KEY_3) AS PLAN " + "\n");
            strSqlString.Append("             , SUM(NVL(SHIP_QTY ,0)) AS SHIP " + "\n");
            strSqlString.Append("             , SUM(NVL(KEY_3,0)-NVL(SHIP_QTY ,0)) AS DEF " + "\n");
            strSqlString.Append("             , SUM(NVL(V0+V1+V2+V3+V4+V5+V6+V7+V8+V9+V10+V11+V12+V13+V14+V15+V16+V17,0)) AS TOTAL " + "\n");
            strSqlString.Append("             , SUM(NVL(V16,0)) AS HMK3A " + "\n");
            strSqlString.Append("             , SUM(NVL(V9+V10+V11+V12+V13+V14+V15,0)) AS FINISH " + "\n");
            strSqlString.Append("             , SUM(NVL(V7+V8,0)) AS MOLD " + "\n");
            strSqlString.Append("             , SUM(NVL(V6+V17,0)) AS WB " + "\n");
            strSqlString.Append("             , SUM(NVL(V3+V4+V5,0)) AS DA " + "\n");
            strSqlString.Append("             , SUM(NVL(V1+V2,0)) AS SAW " + "\n");
            strSqlString.Append("             , SUM(NVL(V0,0)) AS STOCK " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT LOT.MAT_ID, LOT.LOT_ID, MAT.MAT_GRP_3  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK2A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V0  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'B/G', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V1  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SAW', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V2  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V3  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SMT', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V4  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'D/A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V5  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'W/B', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V6  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'MOLD', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V7  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'CURE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V8  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'M/K', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V9  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TRIM', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V10  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TIN', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V11  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'S/B/A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V12  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SIG', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V13  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'AVI', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V14  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'V/I', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V15  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK3A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V16  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'GATE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V17  " + "\n");
            strSqlString.Append("                     , MAX(LOT.KEY_4) AS KEY_4  " + "\n");
            strSqlString.Append("                     , MAX(LOT.KEY_3) AS KEY_3  " + "\n");
            strSqlString.Append("                     , SUM(NVL(SHIP_QTY,0)) AS SHIP_QTY " + "\n");
            strSqlString.Append("                     , MAX(LOT.IN_DAY) AS IN_DAY  " + "\n");
            strSqlString.Append("                     , MAX(LOT.OUT_DAY) AS OUT_DAY  " + "\n");
            strSqlString.Append("                  FROM (  " + "\n");
            strSqlString.Append("                        SELECT B.FACTORY, B.PART_NO AS MAT_ID, B.LOT_ID, A.OPER_GRP_1, A.OPER " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
            strSqlString.Append("                                        ELSE QTY_1 " + "\n");
            strSqlString.Append("                                   END) QTY " + "\n");
            strSqlString.Append("                             , MAX(B.KEY_4) AS KEY_4 " + "\n");
            strSqlString.Append("                             , MAX(B.IN_DAY) AS IN_DAY " + "\n");
            strSqlString.Append("                             , MAX(B.OUT_DAY) AS OUT_DAY " + "\n");
            strSqlString.Append("                             , MAX(KEY_3) AS KEY_3 " + "\n");
            strSqlString.Append("                             , SUM(NVL(SHIP_QTY,0)) AS SHIP_QTY " + "\n");
            strSqlString.Append("                          FROM (  " + "\n");
            strSqlString.Append("                                SELECT A.FACTORY, A.MAT_ID, A.LOT_ID, B.OPER_GRP_1, B.OPER, A.QTY_1  " + "\n");
            strSqlString.Append("                                     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT  " + "\n");

            if (cdvDate.Value.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS A  " + "\n");
                strSqlString.Append("                                     , MWIPOPRDEF B  " + "\n");
                strSqlString.Append("                                 WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS_BOH A  " + "\n");
                strSqlString.Append("                                     , MWIPOPRDEF B  " + "\n");
                strSqlString.Append("                                 WHERE 1=1  " + "\n");
                strSqlString.Append("                                   AND A.CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "22' " + "\n");
            }

            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY  " + "\n");
            strSqlString.Append("                                   AND A.OPER = B.OPER  " + "\n");
            strSqlString.Append("                                   AND A.FACTORY = '" + cdvFactory.Text + "'  " + "\n");
            strSqlString.Append("                                   AND A.LOT_TYPE = 'W'  " + "\n");
            strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("                               ) A  " + "\n");
            strSqlString.Append("                             , ( " + "\n");
            strSqlString.Append("                                SELECT LOT.FACTORY, LOT.KEY_1 AS LOT_ID, LOT.KEY_2 AS PART_NO, LOT.KEY_3, LOT.KEY_4, OPER.IN_DAY, OPER.OUT_DAY " + "\n");
            strSqlString.Append("                                  FROM ( " + "\n");
            strSqlString.Append("                                        SELECT FACTORY, KEY_1, KEY_2, KEY_3, KEY_4 " + "\n");
            strSqlString.Append("                                          FROM CWIPSITDEF " + "\n");
            strSqlString.Append("                                         WHERE TABLE_NAME = 'H_ERCS_LOTINFO' " + "\n");
            strSqlString.Append("                                           AND KEY_8 = 'N' " + "\n");
            strSqlString.Append("                                       ) LOT " + "\n");
            strSqlString.Append("                                     , ( " + "\n");
            strSqlString.Append("                                        SELECT FACTORY, KEY_1 " + "\n");
            strSqlString.Append("                                             , MAX(DECODE(KEY_3, 'A0000', KEY_5)) AS IN_DAY " + "\n");
            strSqlString.Append("                                             , MAX(DECODE(KEY_3, 'AZ010', KEY_5)) AS OUT_DAY " + "\n");
            strSqlString.Append("                                          FROM CWIPSITDEF " + "\n");                        
            strSqlString.Append("                                         WHERE TABLE_NAME = 'H_ERCS_OPERINFO' " + "\n");
            strSqlString.Append("                                           AND KEY_3 IN ('A0000', 'AZ010') " + "\n");            
            strSqlString.Append("                                         GROUP BY FACTORY, KEY_1 " + "\n");
            strSqlString.Append("                                       ) OPER " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND LOT.FACTORY = OPER.FACTORY(+) " + "\n");
            strSqlString.Append("                                   AND LOT.KEY_1 = OPER.KEY_1(+) " + "\n");
            strSqlString.Append("                               ) B " + "\n");
            strSqlString.Append("                             , (  " + "\n");
            strSqlString.Append("                                SELECT B.FACTORY, B.MAT_ID, B.LOT_ID " + "\n");
            strSqlString.Append("                                     , SUM(A.SHIP_QTY) AS SHIP_QTY " + "\n");
            strSqlString.Append("                                  FROM ( " + "\n");
            strSqlString.Append("                                        SELECT LOT_ID " + "\n");
            strSqlString.Append("                                             , HIST_SEQ " + "\n");
            strSqlString.Append("                                             , TRAN_TIME " + "\n");
            strSqlString.Append("                                             , OLD_FACTORY FACTORY " + "\n");
            strSqlString.Append("                                             , MAT_ID " + "\n");
            strSqlString.Append("                                             , OLD_OPER FROM_OPER " + "\n");
            strSqlString.Append("                                             , DECODE (FACTORY, OLD_FACTORY, OLD_QTY_1 - QTY_1, QTY_1) AS SHIP_QTY " + "\n");
            strSqlString.Append("                                             , OWNER_CODE " + "\n");
            strSqlString.Append("                                             , CREATE_CODE " + "\n");
            strSqlString.Append("                                             FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("                                            WHERE 1 = 1 " + "\n");

            if (cdvDate.Value.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                                              AND TRAN_TIME BETWEEN '" + DateTime.Now.AddMonths(-1).ToString("yyyyMMddHHmmss") + "' AND '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "' " + "\n");
            }
            else
            {
                strSqlString.Append("                                              AND TRAN_TIME BETWEEN '" + cdvDate.Value.AddMonths(-1).ToString("yyyyMMdd") + "220000' AND '" + cdvDate.Value.ToString("yyyyMMdd") + "220000' " + "\n");
            }

            strSqlString.Append("                                              AND OLD_FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                                              AND TRAN_CODE = 'SHIP' " + "\n");
            strSqlString.Append("                                              AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                                              AND LOT_CMF_5 LIKE 'E%' " + "\n");
            strSqlString.Append("                                              AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                              AND LOT_ID IN (SELECT LOT_ID FROM CWIPSITDEF WHERE TABLE_NAME = 'H_ERCS_LOTINFO' AND KEY_8 = 'N' AND LOT_ID LIKE KEY_1||'%') " + "\n");
            strSqlString.Append("                                       ) A " + "\n");
            strSqlString.Append("                                     , ( " + "\n");
            strSqlString.Append("                                        SELECT FACTORY, KEY_1 AS LOT_ID, KEY_2 AS MAT_ID FROM CWIPSITDEF WHERE TABLE_NAME = 'H_ERCS_LOTINFO' AND KEY_8 = 'N'" + "\n");
            strSqlString.Append("                                       ) B " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("                                   AND A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("                                   AND A.LOT_ID LIKE B.LOT_ID||'%' " + "\n");
            strSqlString.Append("                                 GROUP BY B.FACTORY, B.MAT_ID, B.LOT_ID " + "\n");
            strSqlString.Append("                               ) C " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND B.FACTORY = A.FACTORY(+) " + "\n");
            strSqlString.Append("                           AND B.FACTORY = C.FACTORY(+) " + "\n");
            strSqlString.Append("                           AND B.PART_NO = A.MAT_ID(+) " + "\n");
            strSqlString.Append("                           AND B.PART_NO = C.MAT_ID(+) " + "\n");
            strSqlString.Append("                           AND A.LOT_ID(+) LIKE B.LOT_ID||'%' " + "\n");
            strSqlString.Append("                           AND B.LOT_ID = C.LOT_ID(+) " + "\n");
            strSqlString.Append("                           AND B.LOT_ID <> ' ' " + "\n");
            strSqlString.Append("                         GROUP BY B.FACTORY, B.PART_NO, B.LOT_ID, A.OPER_GRP_1, A.OPER " + "\n");
            strSqlString.Append("                       ) LOT  " + "\n");
            strSqlString.Append("                     , MWIPMATDEF MAT  " + "\n");
            strSqlString.Append("                 WHERE 1=1  " + "\n");
            strSqlString.Append("                   AND LOT.FACTORY = MAT.FACTORY  " + "\n");
            strSqlString.Append("                   AND LOT.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("                   AND MAT.DELETE_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("                   AND MAT.MAT_GRP_2 <> '-'  " + "\n");
            
            strSqlString.AppendFormat("                   AND MAT.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

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

            strSqlString.Append("                 GROUP BY LOT.MAT_ID, LOT.LOT_ID, MAT.MAT_GRP_3  " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID, LOT_ID, MAT_GRP_3 " + "\n");
            strSqlString.Append("       ) ASSY_WIP  " + "\n");
            strSqlString.Append("     , (  " + "\n");
            strSqlString.Append("        SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("             , SUM(NVL(CASE WHEN RES_STS_8 LIKE 'A04%' THEN EQP_CNT  " + "\n");
            strSqlString.Append("                    ELSE 0  " + "\n");
            strSqlString.Append("                END,0)) AS DA_EQP_CNT  " + "\n");
            strSqlString.Append("             , SUM(NVL(CASE WHEN RES_STS_8 LIKE 'A06%' THEN EQP_CNT  " + "\n");
            strSqlString.Append("                    ELSE 0  " + "\n");
            strSqlString.Append("                END,0)) AS WB_EQP_CNT  " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT FACTORY, RES_STS_2 AS MAT_ID, LOT_ID, RES_STS_8, COUNT(RES_ID) AS EQP_CNT   " + "\n");
            strSqlString.Append("                  FROM MRASRESDEF  " + "\n");
            strSqlString.Append("                 WHERE 1 = 1    " + "\n");
            strSqlString.Append("                   AND FACTORY  = '" + cdvFactory.Text + "'   " + "\n");
            strSqlString.Append("                   AND RES_CMF_9 = 'Y'   " + "\n");
            strSqlString.Append("                   AND RES_CMF_7 = 'Y'   " + "\n");
            strSqlString.Append("                   AND DELETE_FLAG = ' '   " + "\n");
            strSqlString.Append("                   AND (RES_STS_8 LIKE 'A04%' OR RES_STS_8 LIKE 'A06%')  " + "\n");

            strSqlString.AppendFormat("                   AND RES_STS_2 LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

            strSqlString.Append("                 GROUP BY FACTORY, RES_STS_2, LOT_ID, RES_STS_8 " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("       ) RES  " + "\n");
            strSqlString.Append(" WHERE 1=1  " + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "'  " + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' '  " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = ASSY_WIP.MAT_ID(+)  " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = RES.MAT_ID(+)  " + "\n");

            strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

            strSqlString.Append("   AND NVL(ASSY_WIP.TOTAL,0) + NVL(ASSY_WIP.PLAN,0) <> 0 " + "\n");
            strSqlString.Append(" ORDER BY DECODE(MAT.MAT_GRP_1, 'SE', 1, 'HX', 2, 3), MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_ID, ASSY_WIP.LOT_ID  " + "\n");



            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlStringForPopup_1(string lotId)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT KEY_1 AS \"구분\", KEY_2 AS PART, KEY_3 AS QTY, KEY_4 AS \"출하지\", KEY_5 AS \"총괄담당(Eng'r)\", KEY_6 AS \"총괄담당(생관)\", KEY_7 AS \"총괄담당(영업)\", LONG_DATA_1 AS \"Special Notice\" " + "\n");
            strSqlString.Append("  FROM CWIPSITDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND TABLE_NAME = 'H_ERCS_LOTINFO' " + "\n");
            strSqlString.Append("   AND KEY_8 = 'N' " + "\n");
            strSqlString.Append("   AND KEY_1 = '" + lotId + "' " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlStringForPopup_2(string lotId)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT SIT.KEY_3 AS \"코드\", SIT.KEY_4 AS \"Process\" " + "\n");
            strSqlString.Append("     , WIP.VL AS \"WIP\", SIT.KEY_6 AS \"Recipe 유무\", SIT.KEY_7 AS \"예약hold 유무\", SIT.LONG_DATA_1 AS \"공정특이사항\", SIT.KEY_5 AS \"목표\", DECODE(OUT.OUT_TIME, ' ', ' ', TO_CHAR(TO_DATE(OUT.OUT_TIME, 'YYYYMMDDHH24MISS'), 'MM/DD HH24:MI')) AS \"완료\" " + "\n");
            strSqlString.Append("  FROM ( " + "\n");            
            strSqlString.Append("        SELECT FACTORY, KEY_1, KEY_2, KEY_3, KEY_4, KEY_5, KEY_6, KEY_7, LONG_DATA_1 " + "\n");
            strSqlString.Append("          FROM CWIPSITDEF " + "\n");
            strSqlString.Append("         WHERE TABLE_NAME = 'H_ERCS_OPERINFO' " + "\n");
            strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'  " + "\n");
            strSqlString.Append("           AND KEY_1 = '" + lotId + "' " + "\n");            
            strSqlString.Append("       ) SIT " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT LOT.FACTORY,LOT. MAT_ID, LOT.LOT_ID, LOT.OPER, SUM(DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)) VL  " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID, LOT_ID, OPER, SUM(QTY_1) QTY " + "\n");

            if (cdvDate.Value.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                  FROM RWIPLOTSTS " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "22' " + "\n");
            }

            strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND MAT_VER = 1  " + "\n");
            strSqlString.Append("                   AND LOT_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID, LOT_ID, OPER " + "\n");
            strSqlString.Append("               ) LOT " + "\n");
            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("         WHERE LOT.FACTORY = MAT.FACTORY  " + "\n");
            strSqlString.Append("           AND LOT.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("           AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("           AND MAT.MAT_GRP_2 <> '-'  " + "\n");
            strSqlString.Append("         GROUP BY LOT.FACTORY, LOT.MAT_ID, LOT.LOT_ID, LOT.OPER, MAT.MAT_GRP_3  " + "\n");
            strSqlString.Append("       ) WIP " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT FACTORY, LOT_ID, OLD_OPER AS OPER, TRAN_TIME AS OUT_TIME  " + "\n");
            strSqlString.Append("          FROM CWIPLOTEND LOT  " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY  = '" + cdvFactory.Text + "'  " + "\n");
            strSqlString.Append("           AND OWNER_CODE='PROD'  " + "\n");
            strSqlString.Append("           AND MAT_VER=1  " + "\n");
            strSqlString.Append("           AND LOT_TYPE='W'  " + "\n");
            strSqlString.Append("           AND END_RES_ID != ' '  " + "\n");
            strSqlString.Append("       ) OUT " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND SIT.FACTORY = WIP.FACTORY(+) " + "\n");
            strSqlString.Append("   AND SIT.KEY_1 = WIP.LOT_ID(+) " + "\n");
            strSqlString.Append("   AND SIT.KEY_3 = WIP.OPER(+) " + "\n");
            strSqlString.Append("   AND SIT.FACTORY = OUT.FACTORY(+)  " + "\n");
            strSqlString.Append("   AND SIT.KEY_1 = OUT.LOT_ID(+)  " + "\n");
            strSqlString.Append("   AND SIT.KEY_3 = OUT.OPER(+) " + "\n");
            strSqlString.Append("   AND SIT.FACTORY = '" + cdvFactory.Text + "'  " + "\n");
            strSqlString.Append("   AND SIT.KEY_1 = '" + lotId + "' " + "\n");
            strSqlString.Append(" ORDER BY SIT.FACTORY, SIT.KEY_1, SIT.KEY_2 " + "\n");

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

            if (CheckField() == false) return;

            GridColumnInit();

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

                spdData.DataSource = dt;
                //spdData.RPT_AutoFit(false);

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
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
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
        /// spred cell 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader == true) return;

            if (e.Column == 5) //LOT_ID 클릭
            {
                string lotId = spdData.ActiveSheet.Cells[e.Row, e.Column].Value.ToString();
                DataTable dtLotInfo = null;
                DataTable dtOperInfo = null;

                dtLotInfo = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup_1(lotId));
                dtOperInfo = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup_2(lotId));

                if ((dtLotInfo != null && dtLotInfo.Rows.Count > 0) && (dtOperInfo != null && dtLotInfo.Rows.Count > 0))
                {
                    System.Windows.Forms.Form frm = new PRD010221_P1("ER/CS 진도", dtLotInfo, dtOperInfo);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("세부 정보가 없습니다.");
                }
            }
        }

        #endregion

       
    }
}
