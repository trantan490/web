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
    public partial class PRD010421 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010421<br/>
        /// 클래스요약: 긴급 제품 조회<br/>
        /// 작  성  자: 에이스텍 황종환<br/>
        /// 최초작성일: 2013-07-31<br/>
        /// 상세  설명: 긴급 제품 조회(김권수 대리 요청)<br/>
        /// 변경  내용: <br/>  
        /// 2013-07-31-황종환 : 최초 생성
        /// 2013-08-22-황종환 : 데이터 불일치 오류 (D/A, W/B 의 마지막 차수만 가져오게 수정)
        /// 2013-08-29-황종환 : 구분이 PART일 때 납기 이후의 실적은 화면에 포함이 안되고 있는 것을 납기 이 후의 생산량도 포함하도록 변경(김권수 대리 요청)
        /// 2013-08-30-황종환 : 팝업 화면의 잔량 관련해서 DA의 잔량은 목표값 – start 시점이후부터의 누계 AO 실적 – HMK3A – Finish 재공 –WB Merge Part 재공  
        ///                     WB Merge Part 제공은 맨뒤 히든 칼럼으로 설정하여 팝업창에 파라미터로 전달하도록 함 (김권수 대리 요청) 
        /// 2013-12-04-임종우 : DA 설비 Arrange 현황에 A0333 공정 추가 (임태성 요청)
        /// 2013-12-20-임종우 : LOSS 수량에서 BUFFER 공정 AUTO LOSS CODE (N01) 제외 (임태성 요청)
        /// 2013-12-24-임종우 : 설비 효율 통일화 -> DA : 70%, WB : 75% (임태성 요청)
        /// 2015-07-06-임종우 : 설비 대수 집계 시 DISPATCH 기준 정보가 'Y" 인 설비만 집계 (임태성K 요청)
        /// </summary>

        Color colorFixedColumn = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218)));
        Color colorRemain = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(220)), ((System.Byte)(219)));
        Color colorOver = System.Drawing.Color.FromArgb(((System.Byte)(235)), ((System.Byte)(241)), ((System.Byte)(222)));

        // 색상 변화및 팝업에서 사용할 column index number view 버튼을 누를 때 값이 세팅된다.
        int wipStartPos = 0;
        int wipEndPos = 0;
        int partNoPos = 0;
        //int tatPos = 0;
        //int lossPos = 0;

        public PRD010421()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                InitializeComponent();
                SortInit();
                GridColumnInit();
                lblTitle.Text = GlobalVariable.gsSelFuncName;
                this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
                cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
                cdvDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Equals(""))
            {
                //공정 정보를 입력하시기 바랍니다. 와 공정을 입력하세요가 같은 Message여서 STD064로 통일
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD064", GlobalVariable.gcLanguage));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 75);
                spdData.RPT_AddBasicColumn("Part no", 0, 4, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("ITEM", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Classification", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Purpose of input", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Production Status", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Time limit of delivery", 1, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("input", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("plan", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WIP TOTAL", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("a daily goal", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("START_TIME", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.String, 10);
                spdData.RPT_AddBasicColumn("END_TIME", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.String, 10);

                spdData.RPT_MerageHeaderColumnSpan(0, 8, 9);

                spdData.RPT_AddBasicColumn("Standard TAT", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("WIP", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("HMK3A", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("FINISH", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("MOLD", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("WB", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("DA", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SAW", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("STOCK", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_MerageHeaderColumnSpan(0, 18, 7);

                spdData.RPT_AddBasicColumn("Equipment Arrange Status", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("W/B", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("D/A", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                spdData.RPT_MerageHeaderColumnSpan(0, 25, 8);

                spdData.RPT_AddBasicColumn("Number of operations", 2, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("CAPA", 2, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Today's performance", 2, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Today's forecast", 2, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                spdData.RPT_MerageHeaderColumnSpan(1, 25, 4);

                spdData.RPT_AddBasicColumn("Number of operations", 2, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("CAPA", 2, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Today's performance", 2, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Today's forecast", 2, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("LOSS", 0, 33, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("WIP_WB_LAST", 0, 34, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                spdData.RPT_MerageHeaderColumnSpan(1, 29, 4);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 3);

                spdData.RPT_MerageHeaderRowSpan(1, 8, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 9, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 10, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 11, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 12, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 13, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 14, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 15, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 16, 2);

                spdData.RPT_MerageHeaderRowSpan(0, 17, 3);

                
                spdData.RPT_MerageHeaderRowSpan(1, 18, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 19, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 20, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 21, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 22, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 23, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 24, 2);
                spdData.RPT_ColumnConfigFromTable(btnSort);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            try
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-')", "HEAD.CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Major Code", "MAT.MAT_GRP_9", "HEAD.MAT_GRP_9", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_10", "HEAD.MAT_GRP_10", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "HEAD.MAT_GRP_6", true);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
            finally
            {
                Cursor.Current = Cursor.Current;
            }
        }
        #endregion


        #region " Common Function "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns> 
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1;
            string QueryCond2;
            string strDayString = cdvDate.Value.ToString("yyyyMMdd");
            string strPrevDayString = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");

            bool isToday = false;

            if (cdvDate.Value.ToString("yyyyMMdd").Equals(DateTime.Now.ToString("yyyyMMdd")))
            {
                isToday = true;
            }
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            strSqlString.Append("WITH     " +"\n"); 
            strSqlString.Append("EMERGENCY AS (     " +"\n"); 
            strSqlString.Append("    SELECT KEY_1 AS DATA, KEY_2 AS GUBUN, DATA_1 AS MOKJUK, DATA_2 AS PLAN, DATA_3 AS START_TIME, DATA_4 AS END_TIME     " +"\n"); 
            strSqlString.Append("      FROM MGCMTBLDAT     " +"\n"); 
            strSqlString.Append("     WHERE 1=1     " +"\n"); 
            strSqlString.Append("       AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("       AND TABLE_NAME = 'H_EMERGENCY_SET'     " +"\n");

            /* 테스트용 샘플데이터
            strSqlString.Append("    UNION ALL         \n");
            strSqlString.Append("    SELECT 'SEK%JWI' AS DATA, 'PART' AS GUBUN, '샘플' AS MOKJUK, '530532' AS PLAN, '20130801000000' AS START_TIME, '20130815000000' AS END_TIME            \n");
            strSqlString.Append("      FROM DUAL         \n");
            */
            strSqlString.Append(")     " +"\n"); 
            strSqlString.Append("         SELECT " +   QueryCond1 + "\n"); 
            strSqlString.Append("              , DECODE(EMERGENCY.GUBUN, 'PART', EMERGENCY.DATA, MAT.MAT_ID)  AS MAT_ID     " +"\n");
            strSqlString.Append("              , ' '  " + "\n");
            strSqlString.Append("              , EMERGENCY.GUBUN AS GUBUN     " +"\n"); 
            strSqlString.Append("              , EMERGENCY.MOKJUK AS MOKJUK     " +"\n");
            strSqlString.Append("              , TO_CHAR(TO_DATE(EMERGENCY.END_TIME, 'YYYYMMDDHH24MISS'), 'MM/DD HH24:MI')         \n");
            strSqlString.Append("              , TO_CHAR(TO_DATE(EMERGENCY.START_TIME, 'YYYYMMDDHH24MISS'), 'MM/DD HH24:MI')             \n");
            strSqlString.Append("              , EMERGENCY.PLAN AS PLAN              \n");
            strSqlString.Append("              , NVL(FACMOV.QTY,0)    AS QTY              \n");
            strSqlString.Append("              , EMERGENCY.PLAN - NVL(FACMOV.QTY,0) REMAIN          \n");
            strSqlString.Append("              , WIP.HMK3A + WIP.FINISH + WIP.MOLD + WIP.WB + WIP.DA + WIP.SAW + WIP.STOCK AS TOTAL_WIP          \n");
            if (isToday)
            {
                strSqlString.Append("          , CASE WHEN EMERGENCY.PLAN - NVL(FACMOV.QTY,0) <= 0 THEN 0 ELSE \n");
                strSqlString.Append("                CASE WHEN ROUND((EMERGENCY.PLAN - NVL(FACMOV.QTY_PREV_DAY,0)) /   DECODE(TO_DATE(EMERGENCY.END_TIME,'YYYYMMDDHH24MISS') - TO_DATE(GET_WORK_DATE(TO_CHAR(SYSDATE-1, 'YYYYMMDDHH24MISS'), 'D')||'220000' ,'YYYYMMDDHH24MISS'),0,1), 0) <= 0 THEN EMERGENCY.PLAN - NVL(FACMOV.QTY,0)         \n");

                strSqlString.Append("                     WHEN (TO_DATE(EMERGENCY.END_TIME,'YYYYMMDDHH24MISS') - TO_DATE(GET_WORK_DATE(TO_CHAR(SYSDATE-1, 'YYYYMMDDHH24MISS'), 'D')||'220000' ,'YYYYMMDDHH24MISS')) <= 1  THEN EMERGENCY.PLAN - NVL(FACMOV.QTY,0)         \n");
                strSqlString.Append("                     ELSE ROUND((EMERGENCY.PLAN - NVL(FACMOV.QTY_PREV_DAY,0)) /   DECODE(TO_DATE(EMERGENCY.END_TIME,'YYYYMMDDHH24MISS') - TO_DATE(GET_WORK_DATE(TO_CHAR(SYSDATE-1, 'YYYYMMDDHH24MISS'), 'D')||'220000' ,'YYYYMMDDHH24MISS'),0,1), 0) END END IL_MOK         \n");
            }
            else 
            {
                strSqlString.Append("          , CASE WHEN EMERGENCY.PLAN - NVL(FACMOV.QTY,0) <= 0 THEN 0 ELSE \n");
                strSqlString.Append("               CASE WHEN ROUND((EMERGENCY.PLAN - NVL(FACMOV.QTY_PREV_DAY,0)) /   DECODE(TO_DATE(EMERGENCY.END_TIME,'YYYYMMDDHH24MISS') - TO_DATE('" + strPrevDayString + "220000' ,'YYYYMMDDHH24MISS'),0,1), 0) <= 0 THEN EMERGENCY.PLAN - NVL(FACMOV.QTY,0)         \n");
                strSqlString.Append("                     ELSE ROUND((EMERGENCY.PLAN - NVL(FACMOV.QTY_PREV_DAY,0)) /   DECODE(TO_DATE(EMERGENCY.END_TIME,'YYYYMMDDHH24MISS') - TO_DATE('" + strPrevDayString + "220000' ,'YYYYMMDDHH24MISS'),0,1), 0) END END IL_MOK         \n");
            }
            strSqlString.Append("              , TO_CHAR(TO_DATE(EMERGENCY.START_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS')              \n");
            strSqlString.Append("              , TO_CHAR(TO_DATE(EMERGENCY.END_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS')              \n");
            strSqlString.Append("              , MAT.TAT     " +"\n"); 
            strSqlString.Append("              , WIP.HMK3A     " +"\n"); 
            strSqlString.Append("              , WIP.FINISH     " +"\n"); 
            strSqlString.Append("              , WIP.MOLD     " +"\n"); 
            strSqlString.Append("              , WIP.WB     " +"\n"); 
            strSqlString.Append("              , WIP.DA     " +"\n"); 
            strSqlString.Append("              , WIP.SAW     " +"\n"); 
            strSqlString.Append("              , WIP.STOCK     " +"\n"); 
            strSqlString.Append("              , RESARR.WB_EQP_CNT     " +"\n"); 
            strSqlString.Append("              , RESARR.WB_CAPA     " +"\n"); 
            strSqlString.Append("              , RESARR.WB_QTY     " +"\n");
            if (isToday)
            {
                strSqlString.Append("              , RESARR.WB_EXP_QTY WB_EXP_QTY     " + "\n"); 
            }
            else
            {
                strSqlString.Append("              , RESARR.WB_QTY WB_EXP_QTY     " + "\n"); 
            }
            strSqlString.Append("              , RESARR.DA_EQP_CNT     " +"\n"); 
            strSqlString.Append("              , RESARR.DA_CAPA     " +"\n"); 
            strSqlString.Append("              , RESARR.DA_QTY     " +"\n");
            if (isToday)
            {
                strSqlString.Append("              , RESARR.DA_EXP_QTY DA_EXP_QTY     " + "\n");
            }
            else
            {
                strSqlString.Append("              , RESARR.DA_QTY DA_EXP_QTY     " + "\n");
            }
            strSqlString.Append("              , (   SELECT SUM(LOSS_QTY) AS LOSS          \n");
            strSqlString.Append("                      FROM RWIPLOTLSM LSM          \n");
            strSqlString.Append("                         , MWIPMATDEF MAT         \n");
            strSqlString.Append("                     WHERE 1=1          \n");
            strSqlString.Append("                       AND MAT.FACTORY = LSM.FACTORY          \n");
            strSqlString.Append("                       AND MAT.MAT_ID = LSM.MAT_ID         \n");
            strSqlString.Append("                       AND MAT.DELETE_FLAG = ' '         \n");
            strSqlString.Append("                       AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
            strSqlString.Append("                       AND LSM.TRAN_TIME >= EMERGENCY.START_TIME         \n");
            strSqlString.Append("                       AND LSM.HIST_DEL_FLAG = ' '         \n");
            strSqlString.Append("                       AND EMERGENCY.GUBUN = 'PART'         \n");
            strSqlString.Append("                       AND LSM.LOSS_CODE <> 'N01'          \n");
            strSqlString.Append("                       AND MAT.MAT_ID LIKE EMERGENCY.DATA||'%'          \n");            
            strSqlString.Append("                       AND LSM.MAT_ID LIKE EMERGENCY.DATA||'%') LOSS          \n");
            
            strSqlString.Append("              , WIP.WB_LAST          \n");
            strSqlString.Append("           FROM EMERGENCY     " +"\n"); 
            strSqlString.Append("              ,(SELECT EMERGENCY.DATA     " +"\n"); 
            strSqlString.Append("                     , MAX(MAT_GRP_1)              AS MAT_GRP_1     " +"\n");
            strSqlString.Append("                     , MAX(MAT_GRP_9)              AS MAT_GRP_9     " + "\n"); 
            strSqlString.Append("                     , MAX(MAT_GRP_10)             AS MAT_GRP_10     " +"\n"); 
            strSqlString.Append("                     , MAX(MAT_GRP_6)              AS MAT_GRP_6     " +"\n"); 
            strSqlString.Append("                     , MAX(FACTORY)                AS FACTORY     " +"\n"); 
            strSqlString.Append("                     , MAX(MAT_TYPE)               AS MAT_TYPE     " +"\n"); 
            strSqlString.Append("                     , MAX(MWIPMATDEF.MAT_ID)      AS MAT_ID     " +"\n"); 
            strSqlString.Append("                     , MAX(MWIPMATDEF.VENDOR_ID)   AS VENDOR_ID     " +"\n"); 
            strSqlString.Append("                     , MIN(TAT.TAT)                AS TAT     " +"\n"); 
            strSqlString.Append("                  FROM MWIPMATDEF     " +"\n"); 
            strSqlString.Append("                     , EMERGENCY     " +"\n"); 
            strSqlString.Append("                     , (SELECT SAP.SAP_CODE, MAT.VENDOR_ID, MAT.MAT_ID, SUM(SAP.TAT_DAY+SAP.TAT_DAY_WAIT) AS TAT     " +"\n"); 
            strSqlString.Append("                          FROM CWIPSAPTAT@RPTTOMES SAP     " +"\n"); 
            strSqlString.Append("                             , MWIPMATDEF MAT     " +"\n"); 
            strSqlString.Append("                         WHERE SAP.SAP_CODE = MAT.VENDOR_ID     " +"\n");
            strSqlString.Append("                           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
            strSqlString.Append("                           AND MAT.FACTORY = SAP.FACTORY         \n");
            strSqlString.Append("                           AND MAT.MAT_TYPE = 'FG'         \n");
            strSqlString.Append("                           AND SAP.RESV_FIELD_1 = ' '          \n");
            strSqlString.Append("                         GROUP BY SAP.SAP_CODE, MAT.VENDOR_ID, MAT.MAT_ID     " +"\n"); 
            strSqlString.Append("                       ) TAT     " +"\n"); 
            strSqlString.Append("                 WHERE EMERGENCY.GUBUN = 'PART'     " +"\n"); 
            strSqlString.Append("                   AND ( MWIPMATDEF.MAT_ID LIKE EMERGENCY.DATA  OR MWIPMATDEF.MAT_ID = EMERGENCY.DATA )     " +"\n"); 
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                   AND DELETE_FLAG = ' '     " +"\n"); 
            strSqlString.Append("                   AND MAT_TYPE = 'FG'     " +"\n"); 
            strSqlString.Append("                   AND MWIPMATDEF.MAT_ID LIKE '%'     " +"\n"); 
            strSqlString.Append("                   AND MWIPMATDEF.MAT_ID = TAT.MAT_ID(+)     " +"\n");

            // 상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MWIPMATDEF.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND MWIPMATDEF.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND MWIPMATDEF.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND MWIPMATDEF.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND MWIPMATDEF.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND MWIPMATDEF.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND MWIPMATDEF.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND MWIPMATDEF.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND MWIPMATDEF.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("                 GROUP BY DATA     " +"\n"); 
            strSqlString.Append("                ) MAT     " +"\n"); 
            strSqlString.Append("              , (     " +"\n"); 
            strSqlString.Append("                SELECT EMERGENCY.DATA     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN CASE WHEN A.MAT_GRP_5 IN ('2nd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V0,0)/NVL(LOSS.DATA_1,1) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                                WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V0,0)/NVL(LOSS.DATA_1,1) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                                ELSE NVL(F.V0,0)/NVL(LOSS.DATA_1,1)     " +"\n"); 
            strSqlString.Append("                                END),0)     " +"\n"); 
            strSqlString.Append("                          ) AS STOCK     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V1,0)/NVL(LOSS.DATA_1,1)+NVL(F.V2,0)/NVL(LOSS.DATA_1,1), 'Merge', NVL(F.V1,0)/NVL(LOSS.DATA_1,1)+NVL(F.V2,0)/NVL(LOSS.DATA_1,1), 0)     " +"\n"); 
            strSqlString.Append("                                WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V1,0)/NVL(LOSS.DATA_1,1)+NVL(F.V2,0)/NVL(LOSS.DATA_1,1) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                                ELSE NVL(F.V1,0)/NVL(LOSS.DATA_1,1)+NVL(F.V2,0)/NVL(LOSS.DATA_1,1)     " +"\n"); 
            strSqlString.Append("                                END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS SAW     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V3,0)/NVL(LOSS.DATA_1,1)+NVL(F.V4,0), 'Merge', NVL(F.V3,0)/NVL(LOSS.DATA_1,1)+NVL(F.V4,0), 0)     " +"\n"); 
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V3,0)/NVL(LOSS.DATA_1,1)+NVL(F.V4,0) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                               ELSE NVL(F.V3,0)/NVL(LOSS.DATA_1,1)+NVL(F.V4,0)     " +"\n"); 
            strSqlString.Append("                                 END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS DA     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V5+F.V16,0), 'Merge', NVL(F.V5+F.V16,0), 0)     " +"\n"); 
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V5+F.V16,0) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                               ELSE NVL(F.V5+F.V16,0)     " +"\n"); 
            strSqlString.Append("                               END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS WB     " +"\n");
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', 0, 'Merge', NVL(F.V5+F.V16,0), 0)              \n");
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('Merge') THEN NVL(F.V5+F.V16,0) ELSE 0 END              \n");
            strSqlString.Append("                               ELSE NVL(F.V5+F.V16,0)              \n");
            strSqlString.Append("                               END),0)              \n");
            strSqlString.Append("                           ) AS WB_LAST             \n");
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V6+F.V7,0), 'Merge', NVL(F.V6+F.V7,0), 0)     " +"\n"); 
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V6+F.V7,0) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                               ELSE NVL(F.V6+F.V7,0)     " +"\n"); 
            strSqlString.Append("                          END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS MOLD     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0), 'Merge', NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0), 0)     " +"\n"); 
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                               ELSE NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0)     " +"\n"); 
            strSqlString.Append("                               END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS FINISH     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V15,0), 'Merge', NVL(F.V15,0), 0)     " +"\n"); 
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V15,0) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                               ELSE NVL(F.V15,0)     " +"\n"); 
            strSqlString.Append("                               END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS HMK3A     " +"\n"); 
            strSqlString.Append("                  FROM (     " +"\n"); 
            strSqlString.Append("                        SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8     " +"\n"); 
            strSqlString.Append("                             , DECODE(MAT.MAT_GRP_1,'SE',MAT.MAT_GRP_9,' ') AS MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13, MAT.MAT_CMF_11     " +"\n"); 
            strSqlString.Append("                          FROM MWIPMATDEF MAT     " +"\n"); 
            strSqlString.Append("                         WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                           AND MAT.MAT_TYPE= 'FG'     " +"\n"); 
            strSqlString.Append("                           AND MAT.DELETE_FLAG <> 'Y'     " +"\n"); 
            strSqlString.Append("                         GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13, MAT.MAT_CMF_11     " +"\n"); 
            strSqlString.Append("                       ) A     " +"\n"); 
            strSqlString.Append("                     , (     " +"\n"); 
            strSqlString.Append("                        SELECT LOT.MAT_ID, MAT.MAT_GRP_3     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'HMK2A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V0     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'B/G', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V1     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'SAW', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V2     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V3     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'D/A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V4     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V5     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'MOLD', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V6     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'CURE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V7     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'M/K', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V8     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'TRIM', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V9     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'TIN', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V10     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/B/A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V11     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'SIG', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V12     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'AVI', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V13     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'V/I', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V14     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'HMK3A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V15     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'GATE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V16     " +"\n"); 
            strSqlString.Append("                          FROM (     " +"\n"); 
            strSqlString.Append("                                SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1     " +"\n"); 
            strSqlString.Append("                                     , SUM(A.QTY_1) QTY     " +"\n");
            if (isToday)
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS A     " + "\n");
            }
            else
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS_BOH A     " + "\n");
            }
            strSqlString.Append("                                     , MWIPOPRDEF B     " +"\n"); 
            strSqlString.Append("                                 WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY(+)     " +"\n"); 
            strSqlString.Append("                                   AND A.OPER = B.OPER(+)     " +"\n"); 
            strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                                   AND A.LOT_TYPE = 'W'     " +"\n"); 
            strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' '     " +"\n"); 
            strSqlString.Append("                                   AND A.LOT_CMF_5 LIKE '%'     " +"\n");
            if (!isToday)
            {
                strSqlString.Append("                                   AND A.CUTOFF_DT = '"+strDayString+"22'              \n");
            }
            strSqlString.Append("                                 GROUP BY A.FACTORY, A.MAT_ID, B.OPER_GRP_1     " +"\n"); 
            strSqlString.Append("                               ) LOT     " +"\n"); 
            strSqlString.Append("                             , MWIPMATDEF MAT     " +"\n"); 
            strSqlString.Append("                         WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                           AND LOT.FACTORY = MAT.FACTORY     " +"\n"); 
            strSqlString.Append("                           AND LOT.MAT_ID = MAT.MAT_ID     " +"\n"); 
            strSqlString.Append("                           AND MAT.DELETE_FLAG <> 'Y'     " +"\n"); 
            strSqlString.Append("                           AND MAT.MAT_GRP_2 <> '-'     " +"\n"); 
            strSqlString.Append("                         GROUP BY LOT.MAT_ID ,MAT.MAT_GRP_3     " +"\n"); 
            strSqlString.Append("                       ) F,     " +"\n"); 
            strSqlString.Append("                       (     " +"\n"); 
            strSqlString.Append("                        SELECT KEY_1,DATA_1     " +"\n"); 
            strSqlString.Append("                          FROM MGCMTBLDAT     " +"\n"); 
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                           AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')     " +"\n"); 
            strSqlString.Append("                       ) LOSS     " +"\n"); 
            strSqlString.Append("                       , EMERGENCY     " +"\n"); 
            strSqlString.Append("                 WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                   AND A.MAT_ID = F.MAT_ID(+)     " +"\n"); 
            strSqlString.Append("                   AND A.MAT_ID = LOSS.KEY_1(+)     " +"\n"); 
            strSqlString.Append("                   AND (A.MAT_ID LIKE EMERGENCY.DATA OR A.MAT_ID = EMERGENCY.DATA)     " +"\n"); 
            strSqlString.Append("                 GROUP BY EMERGENCY.DATA     " +"\n"); 
            strSqlString.Append("                ) WIP     " +"\n"); 
            strSqlString.Append("              , (     " +"\n"); 
            strSqlString.Append("                SELECT EMERGENCY.DATA     " +"\n"); 
            strSqlString.Append("                     , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS QTY     " +"\n");
            if (isToday)
            {
                strSqlString.Append("                     , SUM(DECODE (WORK_DATE, GET_WORK_DATE(TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS'), 'D') ,0,S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1)) QTY_PREV_DAY         \n");
            }
            else
            {
                strSqlString.Append("                     , SUM(DECODE (WORK_DATE, '" + strPrevDayString + "' ,0,S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1)) QTY_PREV_DAY         \n");
            }
            strSqlString.Append("                  FROM RSUMFACMOV, EMERGENCY     " +"\n"); 
            strSqlString.Append("                 WHERE 1=1     " +"\n"); 
            strSqlString.Append("                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                   AND CM_KEY_2 = 'PROD'     " +"\n"); 
            strSqlString.Append("                   AND CM_KEY_3 LIKE '%'     " +"\n"); 
            strSqlString.Append("                   AND FACTORY <> 'RETURN'     " +"\n"); 
            strSqlString.Append("                   AND EMERGENCY.GUBUN = 'PART'     " +"\n"); 
            strSqlString.Append("                   AND ( RSUMFACMOV.MAT_ID LIKE EMERGENCY.DATA  OR RSUMFACMOV.MAT_ID = EMERGENCY.DATA )     " +"\n"); 
            strSqlString.Append("                   AND WORK_DATE BETWEEN TO_CHAR(ADD_MONTHS(SYSDATE, -1), 'YYYYMMDD')  AND TO_CHAR(SYSDATE, 'YYYYMMDD')     " +"\n");
            if (isToday)
            {
                strSqlString.Append("                   AND WORK_DATE >= GET_WORK_DATE(EMERGENCY.START_TIME, 'D')      " + "\n");
            }
            else 
            {
                strSqlString.Append("                   AND WORK_DATE BETWEEN GET_WORK_DATE(EMERGENCY.START_TIME, 'D') AND '"+strDayString+"'         \n");
            }
            
            strSqlString.Append("                 GROUP BY EMERGENCY.DATA     " +"\n"); 
            strSqlString.Append("                ) FACMOV     " +"\n"); 
            strSqlString.Append("              , (     " +"\n"); 
            strSqlString.Append("                SELECT DATA     " +"\n"); 
            strSqlString.Append("                     , SUM(WB_EQP_CNT)  WB_EQP_CNT     " +"\n"); 
            strSqlString.Append("                     , SUM(WB_CAPA)     WB_CAPA     " +"\n"); 
            strSqlString.Append("                     , SUM(WB_QTY)      WB_QTY     " +"\n"); 
            strSqlString.Append("                     , SUM(WB_EXP_QTY)  WB_EXP_QTY     " +"\n"); 
            strSqlString.Append("                     , SUM(DA_EQP_CNT)  DA_EQP_CNT     " +"\n"); 
            strSqlString.Append("                     , SUM(DA_CAPA)     DA_CAPA     " +"\n"); 
            strSqlString.Append("                     , SUM(DA_QTY)      DA_QTY     " +"\n"); 
            strSqlString.Append("                     , SUM(DA_EXP_QTY)  DA_EXP_QTY     " +"\n"); 
            strSqlString.Append("                  FROM (     " +"\n"); 
            strSqlString.Append("                        SELECT DATA     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'WB', SUM(EQP_CNT), 0)    WB_EQP_CNT     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'WB', SUM(CAPA), 0)       WB_CAPA     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'WB', SUM(QTY), 0)        WB_QTY     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'WB', SUM(EXPECT_QTY), 0) WB_EXP_QTY     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'DA', SUM(EQP_CNT), 0)    DA_EQP_CNT     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'DA', SUM(CAPA), 0)       DA_CAPA     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'DA', SUM(QTY), 0)        DA_QTY     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'DA', SUM(EXPECT_QTY), 0) DA_EXP_QTY     " +"\n"); 
            strSqlString.Append("                          FROM (     " +"\n");
            strSqlString.Append("                                SELECT RES.DATA     " + "\n"); 
            strSqlString.Append("                                     , MAT.MAT_ID     " +"\n"); 
            strSqlString.Append("                                     , SUM(RES.EQP_CNT) AS EQP_CNT     " +"\n");
            strSqlString.Append("                                     , SUM(CASE WHEN RES.OPER LIKE 'A06%' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.EQP_CNT, 0)) " + "\n");             
            strSqlString.Append("                                                WHEN RES.OPER LIKE 'A04%' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.7 * RES.EQP_CNT, 0)) " +"\n");
            strSqlString.Append("                                                ELSE TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.7 * RES.EQP_CNT, 0)) " + "\n"); 
            strSqlString.Append("                                           END) AS CAPA " +"\n");
            strSqlString.Append("                                     , DECODE(SUBSTR(RES.OPER, 1,3), 'A03', 'DA', 'A04', 'DA', 'A06', 'WB') AS OPER     " + "\n"); 
            strSqlString.Append("                                     , MAX(WIPMOV.QTY) AS QTY     " +"\n"); 
            strSqlString.Append("                                     , CASE     " +"\n"); 
            strSqlString.Append("                                        WHEN TO_CHAR(SYSDATE, 'HH24') >= 22 THEN ROUND(SUM(WIPMOV.QTY)/ (SYSDATE - TO_DATE(TO_CHAR(SYSDATE, 'YYYYMMDD')||'220000', 'YYYYMMDDHH24MISS')), 0)     " +"\n"); 
            strSqlString.Append("                                        ELSE ROUND(SUM(WIPMOV.QTY)/ (SYSDATE - TO_DATE(TO_CHAR(SYSDATE-1, 'YYYYMMDD')||'220000', 'YYYYMMDDHH24MISS')), 0)     " +"\n"); 
            strSqlString.Append("                                       END     " +"\n"); 
            strSqlString.Append("                                     AS EXPECT_QTY     " +"\n"); 
            strSqlString.Append("                                  FROM (     " +"\n");
            strSqlString.Append("                                        SELECT EMERGENCY.DATA, FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS EQP_CNT     " + "\n");
            strSqlString.Append("                                          FROM MRASRESDEF, EMERGENCY     " + "\n"); 
            strSqlString.Append("                                         WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                                           AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                                           AND RES_CMF_9 = 'Y'     " +"\n");
            strSqlString.Append("                                           AND RES_CMF_7 = 'Y'     " + "\n"); 
            strSqlString.Append("                                           AND DELETE_FLAG = ' '     " +"\n"); 
            strSqlString.Append("                                           AND (RES_STS_8 LIKE 'A06%' OR RES_STS_8 LIKE 'A04%' OR RES_STS_8 = 'A0333')     " +"\n");
            strSqlString.Append("                                           AND ( RES_STS_2 LIKE EMERGENCY.DATA  OR RES_STS_2 = EMERGENCY.DATA )   " + "\n");
            strSqlString.Append("                                         GROUP BY EMERGENCY.DATA,FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7     " + "\n"); 
            strSqlString.Append("                                       ) RES     " +"\n"); 
            strSqlString.Append("                                     , CRASUPHDEF UPH     " +"\n"); 
            strSqlString.Append("                                     , MWIPMATDEF MAT     " +"\n"); 
            strSqlString.Append("                                     , (     " +"\n"); 
            strSqlString.Append("                                        SELECT MAT_ID, OPER AS OPER, SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 ) QTY     " +"\n"); 
            strSqlString.Append("                                          FROM RSUMWIPMOV    " +"\n"); 
            strSqlString.Append("                                         WHERE 1=1     " +"\n");
            if (isToday)
            {
                strSqlString.Append("                                           AND WORK_DATE = GET_WORK_DATE(TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS'), 'D')     " + "\n"); 
            }
            else
            {
                strSqlString.Append("                                           AND WORK_DATE = '"+strDayString+"'         \n");
            }
            strSqlString.Append("                                           AND LOT_TYPE = 'W'     " +"\n"); 
            strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                                           AND (OPER LIKE 'A04%' OR OPER LIKE 'A06%' OR OPER = 'A0333') " +"\n"); 
            strSqlString.Append("                                         GROUP BY MAT_ID, OPER " +"\n"); 
            strSqlString.Append("                                        ) WIPMOV     " +"\n"); 
            strSqlString.Append("                                 WHERE 1 = 1     " +"\n");
            strSqlString.Append("                                     AND   MAT.DELETE_FLAG=' '                        " + "\n");
            strSqlString.Append("                                     AND   RES.FACTORY=MAT.FACTORY                    " + "\n");
            strSqlString.Append("                                     AND   RES.RES_STS_2 = MAT.MAT_ID                 " + "\n");
            strSqlString.Append("                                     AND   RES.FACTORY=UPH.FACTORY(+)                 " + "\n");
            strSqlString.Append("                                     AND   RES.OPER=UPH.OPER(+)                       " + "\n");
            strSqlString.Append("                                     AND   RES.RES_MODEL = UPH.RES_MODEL(+)           " + "\n");
            strSqlString.Append("                                     AND   RES.UPEH_GRP = UPH.UPEH_GRP(+)             " + "\n");
            strSqlString.Append("                                     AND   RES.RES_STS_2 = UPH.MAT_ID(+)              " + "\n");
            strSqlString.Append("                                     AND   RES.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'                     " + "\n");
            strSqlString.Append("                                     AND   RES.OPER NOT IN ('00001','00002')          " + "\n");
            strSqlString.Append("                                     AND   RES.RES_STS_2 = WIPMOV.MAT_ID(+)           " + "\n");
            strSqlString.Append("                                     AND   RES.OPER = WIPMOV.OPER(+)                  " + "\n");
            strSqlString.Append("                                     AND   ((RES.OPER LIKE 'A040%' AND RES.OPER = 'A040'||DECODE(MAT.MAT_GRP_4,'SD2','2','SD3','3','SD4','4','SD5','5','SD8','8','0' ) ) OR " + "\n");
            strSqlString.Append("                                            (RES.OPER LIKE 'A060%' AND RES.OPER = 'A060'||DECODE(MAT.MAT_GRP_4,'SD2','2','SD3','3','SD4','4','SD5','5','SD8','8','0' ) ) OR RES.OPER = 'A0333')" + "\n");
            strSqlString.Append("                                   GROUP BY DATA, MAT.MAT_ID, SUBSTR(RES.OPER, 1,3)   " + "\n");
            strSqlString.Append("                                ORDER BY MAT.MAT_ID     " +"\n"); 
            strSqlString.Append("                        )     " +"\n"); 
            strSqlString.Append("                         GROUP BY DATA, OPER     " +"\n"); 
            strSqlString.Append("                     )     " +"\n"); 
            strSqlString.Append("                 GROUP BY DATA     " +"\n"); 
            strSqlString.Append("                ) RESARR     " +"\n"); 
            strSqlString.Append("          WHERE 1=1     " +"\n"); 
            strSqlString.Append("            AND EMERGENCY.GUBUN = 'PART'     " +"\n"); 
            strSqlString.Append("            AND ( MAT.MAT_ID LIKE EMERGENCY.DATA OR MAT.MAT_ID = EMERGENCY.DATA )     " +"\n"); 
            strSqlString.Append("            AND EMERGENCY.DATA = RESARR.DATA(+)     " +"\n"); 
            strSqlString.Append("            AND EMERGENCY.DATA = WIP.DATA(+)     " +"\n"); 
            strSqlString.Append("            AND EMERGENCY.DATA = FACMOV.DATA(+)     " +"\n");
            strSqlString.Append("            AND MAT_ID LIKE '" + txtMatID.Text + "'     " + "\n");
            strSqlString.Append("        UNION ALL     " +"\n"); 
            strSqlString.Append("         SELECT " + QueryCond2 + "\n"); 
            strSqlString.Append("              , NVL((SELECT MAT_ID FROM RWIPLOTSTS WHERE (LOT_ID = EMERGENCY.DATA OR LOT_ID LIKE EMERGENCY.DATA) AND ROWNUM = 1), '-' ) AS MAT_ID     " +"\n");
            strSqlString.Append("              , EMERGENCY.DATA     " + "\n"); 
            strSqlString.Append("              , EMERGENCY.GUBUN AS GUBUN     " +"\n"); 
            strSqlString.Append("              , EMERGENCY.MOKJUK AS MOKJUK     " +"\n");
            strSqlString.Append("              , TO_CHAR(TO_DATE(EMERGENCY.END_TIME, 'YYYYMMDDHH24MISS'), 'MM/DD HH24:MI')          \n");
            strSqlString.Append("              , TO_CHAR(TO_DATE(EMERGENCY.START_TIME, 'YYYYMMDDHH24MISS'), 'MM/DD HH24:MI')             \n");
            strSqlString.Append("              , EMERGENCY.PLAN AS PLAN              \n");
            strSqlString.Append("              , NVL(RUN_LOT.QTY,0)              \n");
            strSqlString.Append("              , EMERGENCY.PLAN - NVL(RUN_LOT.QTY,0) REMAIN            \n");
            strSqlString.Append("              , WIP_RUN_LOT.HMK3A + WIP_RUN_LOT.FINISH + WIP_RUN_LOT.MOLD + WIP_RUN_LOT.WB + WIP_RUN_LOT.DA + WIP_RUN_LOT.SAW + WIP_RUN_LOT.STOCK AS TOTAL_WIP           \n");
            if (isToday)
            {
                strSqlString.Append("              , CASE WHEN EMERGENCY.PLAN - NVL(RUN_LOT.QTY,0) <= 0 THEN 0 ELSE   \n");
                strSqlString.Append("                CASE WHEN ROUND((EMERGENCY.PLAN - NVL(RUN_LOT.QTY_PREV_DAY,0)) /   DECODE(TO_DATE(EMERGENCY.END_TIME,'YYYYMMDDHH24MISS') - TO_DATE(GET_WORK_DATE(TO_CHAR(SYSDATE-1, 'YYYYMMDDHH24MISS'), 'D')||'220000' ,'YYYYMMDDHH24MISS'),0,1), 0) <= 0 THEN EMERGENCY.PLAN - NVL(RUN_LOT.QTY,0)         \n");
                strSqlString.Append("                     WHEN (TO_DATE(EMERGENCY.END_TIME,'YYYYMMDDHH24MISS') - TO_DATE(GET_WORK_DATE(TO_CHAR(SYSDATE-1, 'YYYYMMDDHH24MISS'), 'D')||'220000' ,'YYYYMMDDHH24MISS')) <= 1  THEN EMERGENCY.PLAN - NVL(RUN_LOT.QTY,0)         \n");
                strSqlString.Append("                     ELSE ROUND((EMERGENCY.PLAN - NVL(RUN_LOT.QTY_PREV_DAY,0)) /   DECODE(TO_DATE(EMERGENCY.END_TIME,'YYYYMMDDHH24MISS') - TO_DATE(GET_WORK_DATE(TO_CHAR(SYSDATE-1, 'YYYYMMDDHH24MISS'), 'D')||'220000' ,'YYYYMMDDHH24MISS'),0,1), 0) END END IL_MOK                  \n");
            }
            else
            {
                strSqlString.Append("              , CASE WHEN EMERGENCY.PLAN - NVL(RUN_LOT.QTY,0) <= 0 THEN 0 ELSE   \n");
                strSqlString.Append("                CASE WHEN ROUND((EMERGENCY.PLAN - NVL(RUN_LOT.QTY_PREV_DAY,0)) /   DECODE(TO_DATE(EMERGENCY.END_TIME,'YYYYMMDDHH24MISS') - TO_DATE('" + strPrevDayString + "220000' ,'YYYYMMDDHH24MISS'),0,1), 0) <= 0 THEN EMERGENCY.PLAN - NVL(RUN_LOT.QTY,0)         \n");
                strSqlString.Append("                     ELSE ROUND((EMERGENCY.PLAN - NVL(RUN_LOT.QTY_PREV_DAY,0)) /   DECODE(TO_DATE(EMERGENCY.END_TIME,'YYYYMMDDHH24MISS') - TO_DATE('" + strPrevDayString + "220000' ,'YYYYMMDDHH24MISS'),0,1), 0) END END IL_MOK                  \n");
            }
            strSqlString.Append("              , TO_CHAR(TO_DATE(EMERGENCY.START_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS')              \n");
            strSqlString.Append("              , TO_CHAR(TO_DATE(EMERGENCY.END_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS')              \n");
            strSqlString.Append("              , TAT.TAT     " +"\n"); 
            strSqlString.Append("              , WIP_RUN_LOT.HMK3A     " +"\n"); 
            strSqlString.Append("              , WIP_RUN_LOT.FINISH     " +"\n"); 
            strSqlString.Append("              , WIP_RUN_LOT.MOLD     " +"\n"); 
            strSqlString.Append("              , WIP_RUN_LOT.WB     " +"\n"); 
            strSqlString.Append("              , WIP_RUN_LOT.DA     " +"\n"); 
            strSqlString.Append("              , WIP_RUN_LOT.SAW     " +"\n"); 
            strSqlString.Append("              , WIP_RUN_LOT.STOCK     " +"\n"); 
            strSqlString.Append("              , RESARR_LOT.WB_EQP_CNT     " +"\n"); 
            strSqlString.Append("              , RESARR_LOT.WB_CAPA     " +"\n"); 
            strSqlString.Append("              , RESARR_LOT.WB_QTY     " +"\n");
            if (isToday)
            {
                strSqlString.Append("              , RESARR_LOT.WB_EXP_QTY WB_EXP_QTY     " + "\n");
            }
            else
            {
                strSqlString.Append("              , RESARR_LOT.WB_QTY WB_EXP_QTY     " + "\n");
            } 
            strSqlString.Append("              , RESARR_LOT.DA_EQP_CNT     " +"\n"); 
            strSqlString.Append("              , RESARR_LOT.DA_CAPA     " +"\n"); 
            strSqlString.Append("              , RESARR_LOT.DA_QTY     " +"\n");
            if (isToday)
            {
                strSqlString.Append("              , RESARR_LOT.DA_EXP_QTY DA_EXP_QTY     " + "\n");
            }
            else
            {
                strSqlString.Append("              , RESARR_LOT.DA_QTY DA_EXP_QTY     " + "\n");
            }
            strSqlString.Append("              , (SELECT SUM(LOSS_QTY) AS LOSS          \n");
            strSqlString.Append("                   FROM RWIPLOTLSM         \n");
            strSqlString.Append("                  WHERE TRAN_TIME >= EMERGENCY.START_TIME          \n");
            strSqlString.Append("                    AND HIST_DEL_FLAG = ' '          \n");
            strSqlString.Append("                    AND LOT_ID LIKE EMERGENCY.DATA||'%' ) LOSS            \n");
            strSqlString.Append("              , WIP_RUN_LOT.WB_LAST \n");
            strSqlString.Append("           FROM EMERGENCY     " +"\n"); 
            strSqlString.Append("           LEFT OUTER JOIN (     " +"\n"); 
            strSqlString.Append("                SELECT EMERGENCY.DATA     " +"\n"); 
            strSqlString.Append("                     , MAX(WIPMAT.MAT_GRP_1)     AS MAT_GRP_1     " +"\n"); 
            strSqlString.Append("                     , MAX(WIPMAT.MAT_GRP_10)    AS MAT_GRP_10     " +"\n"); 
            strSqlString.Append("                     , MAX(WIPMAT.MAT_GRP_6)     AS MAT_GRP_6     " +"\n"); 
            strSqlString.Append("                     , MAX(WIPMAT.FACTORY)       AS FACTORY     " +"\n"); 
            strSqlString.Append("                     , MAX(WIPMAT.MAT_TYPE)      AS MAT_TYPE     " +"\n"); 
            strSqlString.Append("                     , MAX(WIPMAT.MAT_ID)        AS MAT_ID     " +"\n"); 
            strSqlString.Append("                     , MAX(WIPMAT.DELETE_FLAG)   AS DELETE_FLAG     " +"\n"); 
            strSqlString.Append("                     , MAX(WIPMAT.VENDOR_ID)     AS VENDOR_ID     " +"\n"); 
            strSqlString.Append("                  FROM MWIPMATDEF WIPMAT, RWIPLOTSTS LOT, EMERGENCY     " +"\n"); 
            strSqlString.Append("                 WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                   AND WIPMAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                   AND WIPMAT.DELETE_FLAG = ' '     " +"\n"); 
            strSqlString.Append("                   AND WIPMAT.MAT_TYPE = 'FG'     " +"\n"); 
            strSqlString.Append("                   AND WIPMAT.MAT_ID LIKE '%'     " +"\n"); 
            strSqlString.Append("                   AND WIPMAT.MAT_ID = LOT.MAT_ID     " +"\n"); 
            strSqlString.Append("                   AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                   AND LOT.LOT_DEL_FLAG = ' '     " +"\n"); 
            strSqlString.Append("                   AND LOT.LOT_TYPE = 'W'     " +"\n");
            strSqlString.Append("                   AND ( ( EMERGENCY.GUBUN = 'RUN' AND ( LOT.LOT_ID LIKE EMERGENCY.DATA||'%' ) ) OR     " + "\n");
            strSqlString.Append("                         ( EMERGENCY.GUBUN = 'LOT' AND ( LOT.LOT_ID LIKE EMERGENCY.DATA||'%' ) ) )      " + "\n");
            strSqlString.Append("                 GROUP BY EMERGENCY.DATA     " +"\n"); 
            strSqlString.Append("                ) MAT ON EMERGENCY.DATA = MAT.DATA     " +"\n"); 
            strSqlString.Append("           LEFT OUTER JOIN (     " +"\n"); 
            strSqlString.Append("                SELECT EMERGENCY.DATA     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN CASE WHEN A.MAT_GRP_5 IN ('2nd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V0,0)/NVL(LOSS.DATA_1,1) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                                WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V0,0)/NVL(LOSS.DATA_1,1) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                                ELSE NVL(F.V0,0)/NVL(LOSS.DATA_1,1)     " +"\n"); 
            strSqlString.Append("                                END),0)     " +"\n"); 
            strSqlString.Append("                          ) AS STOCK     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V1,0)/NVL(LOSS.DATA_1,1)+NVL(F.V2,0)/NVL(LOSS.DATA_1,1), 'Merge', NVL(F.V1,0)/NVL(LOSS.DATA_1,1)+NVL(F.V2,0)/NVL(LOSS.DATA_1,1), 0)     " +"\n"); 
            strSqlString.Append("                                WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V1,0)/NVL(LOSS.DATA_1,1)+NVL(F.V2,0)/NVL(LOSS.DATA_1,1) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                                ELSE NVL(F.V1,0)/NVL(LOSS.DATA_1,1)+NVL(F.V2,0)/NVL(LOSS.DATA_1,1)     " +"\n"); 
            strSqlString.Append("                                END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS SAW     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V3,0)/NVL(LOSS.DATA_1,1)+NVL(F.V4,0), 'Merge', NVL(F.V3,0)/NVL(LOSS.DATA_1,1)+NVL(F.V4,0), 0)     " +"\n"); 
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V3,0)/NVL(LOSS.DATA_1,1)+NVL(F.V4,0) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                               ELSE NVL(F.V3,0)/NVL(LOSS.DATA_1,1)+NVL(F.V4,0)     " +"\n"); 
            strSqlString.Append("                                 END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS DA     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V5+F.V16,0), 'Merge', NVL(F.V5+F.V16,0), 0)     " +"\n"); 
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V5+F.V16,0) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                               ELSE NVL(F.V5+F.V16,0)     " +"\n"); 
            strSqlString.Append("                               END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS WB     " +"\n");
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', 0, 'Merge', NVL(F.V5+F.V16,0), 0)              \n");
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('Merge') THEN NVL(F.V5+F.V16,0) ELSE 0 END              \n");
            strSqlString.Append("                               ELSE NVL(F.V5+F.V16,0)              \n");
            strSqlString.Append("                               END),0)              \n");
            strSqlString.Append("                           ) AS WB_LAST             \n");
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V6+F.V7,0), 'Merge', NVL(F.V6+F.V7,0), 0)     " + "\n"); 
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V6+F.V7,0) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                               ELSE NVL(F.V6+F.V7,0)     " +"\n"); 
            strSqlString.Append("                          END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS MOLD     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0), 'Merge', NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0), 0)     " +"\n"); 
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                               ELSE NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0)     " +"\n"); 
            strSqlString.Append("                               END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS FINISH     " +"\n"); 
            strSqlString.Append("                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V15,0), 'Merge', NVL(F.V15,0), 0)     " +"\n"); 
            strSqlString.Append("                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V15,0) ELSE 0 END     " +"\n"); 
            strSqlString.Append("                               ELSE NVL(F.V15,0)     " +"\n"); 
            strSqlString.Append("                               END),0)     " +"\n"); 
            strSqlString.Append("                           ) AS HMK3A     " +"\n"); 
            strSqlString.Append("                  FROM (     " +"\n"); 
            strSqlString.Append("                        SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8     " +"\n"); 
            strSqlString.Append("                             , DECODE(MAT.MAT_GRP_1,'SE',MAT.MAT_GRP_9,' ') AS MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13, MAT.MAT_CMF_11     " +"\n"); 
            strSqlString.Append("                          FROM MWIPMATDEF MAT     " +"\n"); 
            strSqlString.Append("                         WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                           AND MAT.MAT_TYPE= 'FG'     " +"\n"); 
            strSqlString.Append("                           AND MAT.DELETE_FLAG <> 'Y'     " +"\n"); 
            strSqlString.Append("                         GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13, MAT.MAT_CMF_11     " +"\n"); 
            strSqlString.Append("                       ) A     " +"\n"); 
            strSqlString.Append("                     , (     " +"\n");
            strSqlString.Append("                        SELECT EMERGENCY.DATA, LOT.MAT_ID, MAT.MAT_GRP_3     " + "\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'HMK2A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V0     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'B/G', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V1     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'SAW', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V2     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V3     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'D/A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V4     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V5     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'MOLD', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V6     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'CURE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V7     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'M/K', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V8     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'TRIM', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V9     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'TIN', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V10     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/B/A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V11     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'SIG', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V12     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'AVI', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V13     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'V/I', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V14     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'HMK3A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V15     " +"\n"); 
            strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'GATE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V16     " +"\n"); 
            strSqlString.Append("                          FROM (     " +"\n");
            strSqlString.Append("                                SELECT A.FACTORY, A.MAT_ID, A.LOT_ID, B.OPER_GRP_1, A.LOT_CMF_4     " + "\n"); 
            strSqlString.Append("                                     , SUM(A.QTY_1) QTY     " +"\n");
            if (isToday)
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS A     " + "\n"); 
            }
            else
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS_BOH A     " + "\n"); 
            }
            strSqlString.Append("                                     , MWIPOPRDEF B     " +"\n"); 
            strSqlString.Append("                                 WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY(+)     " +"\n"); 
            strSqlString.Append("                                   AND A.OPER = B.OPER(+)     " +"\n"); 
            strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                                   AND A.LOT_TYPE = 'W'     " +"\n"); 
            strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' '     " +"\n"); 
            strSqlString.Append("                                   AND A.LOT_CMF_5 LIKE '%'     " +"\n");
            if (!isToday)
            {
                strSqlString.Append("                                   AND A.CUTOFF_DT = '"+strDayString+"22'         \n");
            }
            strSqlString.Append("                                 GROUP BY A.FACTORY, A.MAT_ID, A.LOT_ID, B.OPER_GRP_1, A.LOT_CMF_4     " + "\n"); 
            strSqlString.Append("                               ) LOT     " +"\n"); 
            strSqlString.Append("                             , MWIPMATDEF MAT     " +"\n"); 
            strSqlString.Append("                             , EMERGENCY     " +"\n"); 
            strSqlString.Append("                         WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                           AND LOT.FACTORY = MAT.FACTORY     " +"\n"); 
            strSqlString.Append("                           AND LOT.MAT_ID = MAT.MAT_ID     " +"\n"); 
            strSqlString.Append("                           AND MAT.DELETE_FLAG <> 'Y'     " +"\n"); 
            strSqlString.Append("                           AND MAT.MAT_GRP_2 <> '-'     " +"\n");
            strSqlString.Append("                           AND ( ( EMERGENCY.GUBUN = 'RUN' AND ( LOT.LOT_CMF_4 LIKE EMERGENCY.DATA ) ) OR              \n");
            strSqlString.Append("                                 ( EMERGENCY.GUBUN = 'LOT' AND ( LOT.LOT_ID LIKE EMERGENCY.DATA||'%'  OR LOT.LOT_ID = EMERGENCY.DATA ) ) )         \n");
            strSqlString.Append("                         GROUP BY EMERGENCY.DATA, LOT.MAT_ID ,MAT.MAT_GRP_3     " + "\n"); 
            strSqlString.Append("                       ) F,     " +"\n"); 
            strSqlString.Append("                       (     " +"\n"); 
            strSqlString.Append("                        SELECT KEY_1,DATA_1     " +"\n"); 
            strSqlString.Append("                          FROM MGCMTBLDAT     " +"\n"); 
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                           AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')     " +"\n"); 
            strSqlString.Append("                       ) LOSS     " +"\n"); 
            strSqlString.Append("                       , EMERGENCY     " +"\n"); 
            strSqlString.Append("                 WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                   AND A.MAT_ID = F.MAT_ID(+)     " +"\n"); 
            strSqlString.Append("                   AND A.MAT_ID = LOSS.KEY_1(+)     " +"\n");
            strSqlString.Append("                   AND F.DATA = EMERGENCY.DATA     " + "\n"); 
            strSqlString.Append("                 GROUP BY EMERGENCY.DATA     " +"\n"); 
            strSqlString.Append("                ) WIP_RUN_LOT     " +"\n"); 
            strSqlString.Append("                  ON EMERGENCY.DATA = WIP_RUN_LOT.DATA     " +"\n"); 
            strSqlString.Append("           LEFT OUTER JOIN (     " +"\n");
            strSqlString.Append("                        SELECT EMERGENCY.DATA       AS DATA     " + "\n");
            strSqlString.Append("                             , SUM(QTY_1)           AS QTY     " + "\n");
            strSqlString.Append("                             , SUM(DECODE (GET_WORK_DATE(LOT.TRAN_TIME, 'D'),'" + strDayString + "' ,0,QTY_1)) QTY_PREV_DAY      " + "\n"); 
            strSqlString.Append("                          FROM RWIPLOTHIS LOT     " +"\n"); 
            strSqlString.Append("                             , EMERGENCY     " +"\n"); 
            strSqlString.Append("                        WHERE 1=1     " +"\n"); 
            strSqlString.Append("                           AND LOT.TRAN_CODE = 'SHIP'     " +"\n");
            strSqlString.Append("                           AND LOT.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                           AND LOT.TRAN_TIME >= EMERGENCY.START_TIME     " + "\n");
            if (isToday)
            {
                strSqlString.Append("                           AND LOT.TRAN_TIME BETWEEN TO_CHAR(ADD_MONTHS(SYSDATE, -1), 'YYYYMMDD')||'000000'  AND TO_CHAR(SYSDATE, 'YYYYMMDD')||'235959'     " + "\n");
            }
            else
            {
                strSqlString.Append("                           AND LOT.TRAN_TIME BETWEEN '"+strPrevDayString+"220000'  AND '"+strDayString+"215959'         \n");
            }
            strSqlString.Append("                           AND LOT.HIST_DEL_FLAG = ' '     " +"\n"); 
            strSqlString.Append("                           AND LOT.LOT_TYPE = 'W'     " +"\n");
            strSqlString.Append("                           AND ( ( EMERGENCY.GUBUN = 'RUN' AND  LOT.LOT_CMF_4 LIKE EMERGENCY.DATA ) OR                   \n");
            strSqlString.Append("                                 ( EMERGENCY.GUBUN = 'LOT' AND  LOT.LOT_ID LIKE EMERGENCY.DATA||'%')  )         \n");
            strSqlString.Append("                         GROUP BY EMERGENCY.DATA    " +"\n"); 
            strSqlString.Append("                ) RUN_LOT     " +"\n"); 
            strSqlString.Append("                  ON EMERGENCY.DATA = RUN_LOT.DATA     " +"\n"); 
            strSqlString.Append("           LEFT OUTER JOIN (     " +"\n"); 
            strSqlString.Append("                SELECT DATA     " +"\n"); 
            strSqlString.Append("                     , SUM(WB_EQP_CNT)  WB_EQP_CNT     " +"\n"); 
            strSqlString.Append("                     , SUM(WB_CAPA)     WB_CAPA     " +"\n"); 
            strSqlString.Append("                     , SUM(WB_QTY)      WB_QTY     " +"\n"); 
            strSqlString.Append("                     , SUM(WB_EXP_QTY)  WB_EXP_QTY     " +"\n"); 
            strSqlString.Append("                     , SUM(DA_EQP_CNT)  DA_EQP_CNT     " +"\n"); 
            strSqlString.Append("                     , SUM(DA_CAPA)     DA_CAPA     " +"\n"); 
            strSqlString.Append("                     , SUM(DA_QTY)      DA_QTY     " +"\n"); 
            strSqlString.Append("                     , SUM(DA_EXP_QTY)  DA_EXP_QTY     " +"\n"); 
            strSqlString.Append("                  FROM (     " +"\n"); 
            strSqlString.Append("                        SELECT DATA     " +"\n"); 
            strSqlString.Append("                             , MAT_ID     " +"\n"); 
            strSqlString.Append("                             , LOT_ID     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'WB', SUM(EQP_CNT), 0)    WB_EQP_CNT     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'WB', SUM(CAPA), 0)       WB_CAPA     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'WB', SUM(QTY), 0)        WB_QTY     " +"\n");
            strSqlString.Append("                             , DECODE(OPER, 'WB', SUM(EXPECT_QTY), 0) WB_EXP_QTY     " + "\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'DA', SUM(EQP_CNT), 0)    DA_EQP_CNT     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'DA', SUM(CAPA), 0)       DA_CAPA     " +"\n"); 
            strSqlString.Append("                             , DECODE(OPER, 'DA', SUM(QTY), 0)        DA_QTY     " +"\n");
            strSqlString.Append("                             , DECODE(OPER, 'DA', SUM(EXPECT_QTY), 0) DA_EXP_QTY     " + "\n"); 
            strSqlString.Append("                          FROM (     " +"\n"); 
            strSqlString.Append("                                SELECT WIPMOV.DATA     " +"\n"); 
            strSqlString.Append("                                     , MAT.MAT_ID     " +"\n"); 
            strSqlString.Append("                                     , WIPMOV.LOT_ID     " +"\n"); 
            strSqlString.Append("                                     , SUM(RES.EQP_CNT) AS EQP_CNT     " +"\n");
            strSqlString.Append("                                     , SUM(CASE WHEN RES.OPER LIKE 'A06%' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.EQP_CNT, 0)) " + "\n");             
            strSqlString.Append("                                                WHEN RES.OPER LIKE 'A04%' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.7 * RES.EQP_CNT, 0)) " + "\n");
            strSqlString.Append("                                                ELSE TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.7 * RES.EQP_CNT, 0)) " + "\n"); 
            strSqlString.Append("                                           END ) AS CAPA " +"\n"); 
            strSqlString.Append("                                     , DECODE(SUBSTR(WIPMOV.OPER, 1,3), 'A03', 'DA', 'A04', 'DA', 'A06', 'WB') AS OPER     " +"\n"); 
            strSqlString.Append("                                     , MAX(WIPMOV.QTY) AS QTY     " +"\n"); 
            strSqlString.Append("                                     , CASE     " +"\n"); 
            strSqlString.Append("                                        WHEN TO_CHAR(SYSDATE, 'HH24') >= 22 THEN ROUND(SUM(WIPMOV.QTY)/ (SYSDATE - TO_DATE(TO_CHAR(SYSDATE, 'YYYYMMDD')||'220000', 'YYYYMMDDHH24MISS')), 0)     " +"\n"); 
            strSqlString.Append("                                        ELSE ROUND(SUM(WIPMOV.QTY)/ (SYSDATE - TO_DATE(TO_CHAR(SYSDATE-1, 'YYYYMMDD')||'220000', 'YYYYMMDDHH24MISS')), 0)     " +"\n"); 
            strSqlString.Append("                                       END     " +"\n"); 
            strSqlString.Append("                                     AS EXPECT_QTY     " +"\n"); 
            strSqlString.Append("                                  FROM (     " +"\n"); 
            strSqlString.Append("                                        SELECT FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, LOT_ID, COUNT(RES_ID) AS EQP_CNT     " +"\n"); 
            strSqlString.Append("                                          FROM MRASRESDEF     " +"\n"); 
            strSqlString.Append("                                         WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                                           AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                                           AND RES_CMF_9 = 'Y'     " +"\n");
            strSqlString.Append("                                           AND RES_CMF_7 = 'Y'     " + "\n"); 
            strSqlString.Append("                                           AND DELETE_FLAG = ' '     " +"\n"); 
            strSqlString.Append("                                           AND (RES_STS_8 LIKE 'A06%' OR RES_STS_8 LIKE 'A04%' OR RES_STS_8 = 'A0333') " +"\n"); 
            strSqlString.Append("                                         GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7, LOT_ID     " +"\n"); 
            strSqlString.Append("                                       ) RES     " +"\n"); 
            strSqlString.Append("                                     , CRASUPHDEF UPH     " +"\n"); 
            strSqlString.Append("                                     , (SELECT * FROM MWIPMATDEF WHERE MAT_GRP_5 IN ('Merge', '-')) MAT     " +"\n"); 
            strSqlString.Append("                                     , (     " +"\n"); 
            strSqlString.Append("                                        SELECT EMERGENCY.DATA, MAT_ID, LOT_ID, OLD_OPER AS OPER , SUM(QTY_1) QTY     " +"\n"); 
            strSqlString.Append("                                          FROM RWIPLOTHIS, EMERGENCY,     " +"\n");
            if (isToday)
            {
                strSqlString.Append("                                               (SELECT CASE WHEN TO_CHAR(SYSDATE, 'HH24') >= 22 THEN TO_CHAR(SYSDATE, 'YYYYMMDD')||'220000' ELSE TO_CHAR(SYSDATE-1, 'YYYYMMDD')||'220000' END START_T, " + "\n");
                strSqlString.Append("                                                       CASE WHEN TO_CHAR(SYSDATE, 'HH24') >= 22 THEN TO_CHAR(SYSDATE+1, 'YYYYMMDD')||'215959' ELSE TO_CHAR(SYSDATE, 'YYYYMMDD')||'215959' END END_T    " + "\n");
                strSqlString.Append("                                                  FROM DUAL) TIME_TABLE " + "\n");
            }
            else
            {
                strSqlString.Append("                                               (SELECT '"+strPrevDayString+"220000' START_T, '"+strDayString+"215959' END_T             \n");
                strSqlString.Append("                                                  FROM DUAL) TIME_TABLE         \n");
            }
            strSqlString.Append("                                         WHERE 1 = 1     " +"\n");
            strSqlString.Append("                                           AND ( ( EMERGENCY.GUBUN = 'RUN' AND ( RWIPLOTHIS.LOT_ID LIKE EMERGENCY.DATA||'%'  OR RWIPLOTHIS.LOT_ID LIKE EMERGENCY.DATA ) ) OR " + "\n");
            strSqlString.Append("                                                 ( EMERGENCY.GUBUN = 'LOT' AND ( RWIPLOTHIS.LOT_ID LIKE EMERGENCY.DATA||'%'  OR RWIPLOTHIS.LOT_ID = EMERGENCY.DATA ) ) )    " + "\n"); 
            strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " +"\n"); 
            strSqlString.Append("                                           AND TRAN_CODE = 'END'     " +"\n"); 
            strSqlString.Append("                                           AND TRAN_TIME BETWEEN TIME_TABLE.START_T AND TIME_TABLE.END_T     " +"\n"); 
            strSqlString.Append("                                           AND (OLD_OPER LIKE 'A04%' OR OLD_OPER LIKE 'A06%' OR OLD_OPER = 'A0333') " +"\n"); 
            strSqlString.Append("                                           AND HIST_DEL_FLAG = ' ' " +"\n"); 
            strSqlString.Append("                                           AND LOT_TYPE = 'W' " +"\n"); 
            strSqlString.Append("                                         GROUP BY EMERGENCY.DATA, MAT_ID, LOT_ID, OLD_OPER     " +"\n"); 
            strSqlString.Append("                                        ) WIPMOV     " +"\n"); 
            strSqlString.Append("                                 WHERE 1 = 1     " +"\n"); 
            strSqlString.Append("                                   AND RES.LOT_ID = WIPMOV.LOT_ID     " +"\n"); 
            strSqlString.Append("                                   AND MAT.DELETE_FLAG = ' '     " +"\n"); 
            strSqlString.Append("                                   AND RES.FACTORY = UPH.FACTORY(+)     " +"\n"); 
            strSqlString.Append("                                   AND RES.OPER = UPH.OPER(+)     " +"\n"); 
            strSqlString.Append("                                   AND RES.RES_MODEL = UPH.RES_MODEL(+)     " +"\n"); 
            strSqlString.Append("                                   AND RES.UPEH_GRP = UPH.UPEH_GRP(+)     " +"\n"); 
            strSqlString.Append("                                   AND RES.RES_STS_2 = UPH.MAT_ID(+)     " +"\n"); 
            strSqlString.Append("                                   AND RES.FACTORY = MAT.FACTORY     " +"\n"); 
            strSqlString.Append("                                   AND RES.RES_STS_2 = MAT.MAT_ID     " +"\n"); 
            strSqlString.Append("                                   AND MAT.MAT_ID = WIPMOV.MAT_ID     " +"\n"); 
            strSqlString.Append("                                   AND RES.OPER = WIPMOV.OPER     " +"\n");
            strSqlString.Append("                                   AND ((RES.OPER LIKE 'A040%' AND RES.OPER = 'A040'||DECODE(MAT.MAT_GRP_4,'SD2','2','SD3','3','SD4','4','SD5','5','SD8','8','0' ) ) OR " + "\n");
            strSqlString.Append("                                        (RES.OPER LIKE 'A060%' AND RES.OPER = 'A060'||DECODE(MAT.MAT_GRP_4,'SD2','2','SD3','3','SD4','4','SD5','5','SD8','8','0' ) ) OR RES.OPER = 'A0333')  " + "\n");
            strSqlString.Append("                                 GROUP BY WIPMOV.DATA, MAT.MAT_ID, SUBSTR(WIPMOV.OPER, 1,3), WIPMOV.LOT_ID     " +"\n"); 
            strSqlString.Append("                        )     " +"\n"); 
            strSqlString.Append("                         GROUP BY DATA, MAT_ID, OPER, LOT_ID     " +"\n"); 
            strSqlString.Append("                     )     " +"\n"); 
            strSqlString.Append("                 GROUP BY DATA     " +"\n"); 
            strSqlString.Append("                ) RESARR_LOT     " +"\n"); 
            strSqlString.Append("                  ON EMERGENCY.DATA = RESARR_LOT.DATA     " +"\n");
            strSqlString.Append("           LEFT OUTER JOIN (     " + "\n");
            strSqlString.Append("                SELECT SAP.SAP_CODE, MAT.VENDOR_ID, MAT.MAT_ID, SUM(SAP.TAT_DAY+SAP.TAT_DAY_WAIT) AS TAT     " + "\n");
            strSqlString.Append("                  FROM CWIPSAPTAT@RPTTOMES SAP     " + "\n");
            strSqlString.Append("                     , MWIPMATDEF MAT     " + "\n");
            strSqlString.Append("                 WHERE SAP.SAP_CODE = MAT.VENDOR_ID     " + "\n");
            strSqlString.Append("                   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
            strSqlString.Append("                   AND MAT.FACTORY = SAP.FACTORY         \n");
            strSqlString.Append("                   AND MAT.MAT_TYPE = 'FG'         \n");
            strSqlString.Append("                   AND SAP.RESV_FIELD_1 = ' '          \n");
            strSqlString.Append("                 GROUP BY SAP.SAP_CODE, MAT.VENDOR_ID, MAT.MAT_ID     " + "\n");
            strSqlString.Append("                ) TAT ON MAT.MAT_ID = TAT.MAT_ID     " + "\n");
            strSqlString.Append("            LEFT OUTER JOIN (       " + "\n");
            strSqlString.Append("                SELECT C.DATA, B.MAT_ID, MAX(B.MAT_GRP_10) AS MAT_GRP_10,  MAX(B.MAT_GRP_9) AS MAT_GRP_9, MAX(B.MAT_GRP_6) AS MAT_GRP_6, MAX(D.DATA_1 ) AS CUSTOMER, MAX(B.MAT_GRP_1) AS MAT_GRP_1  " + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS A, MWIPMATDEF B, EMERGENCY C, (SELECT DATA_1, KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER') D       " + "\n");
            strSqlString.Append("                 WHERE A.LOT_ID = C.DATA       " + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID       " + "\n");
            strSqlString.Append("                   AND D.KEY_1 = B.MAT_GRP_1       " + "\n");
            strSqlString.Append("                 GROUP BY C.DATA, B.MAT_ID) HEAD ON EMERGENCY.DATA = HEAD.DATA           " + "\n");
            strSqlString.Append("          WHERE 1=1     " + "\n");
            strSqlString.Append("            AND EMERGENCY.GUBUN <> 'PART'     " + "\n");
            strSqlString.Append("            AND MAT_ID LIKE '"+txtMatID.Text+"'     " + "\n");

            // 상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND HEAD.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND HEAD.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("          ORDER BY 1,2,3,MAT_ID     " + "\n");


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region " Controls Event "

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

                spdData.DataSource = dt;

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                getColumnPosition();

                changeSpreadColor();

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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                spdData.ExportExcel();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 스프레드를 클릭했을 때 Part 에 해당하는 잔량 및 cutoff day를 화면에 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // PART NO 를 클릭 했을 때 새로운 창으로 실행
            if (e.Column != partNoPos) 
                return;

            string strCustomer = spdData.ActiveSheet.GetText(e.Row, 0).Replace(",", "");
            string strType = spdData.ActiveSheet.GetText(e.Row, 6).Replace(",", "");
            string strMatId = spdData.ActiveSheet.GetText(e.Row, 4).Replace(",", "");
            string strLotId = spdData.ActiveSheet.GetText(e.Row, 5).Replace(",", "");
            string strStartDate = spdData.ActiveSheet.GetText(e.Row, 15).Replace(",", "");
            string strEndDate = spdData.ActiveSheet.GetText(e.Row, 16).Replace(",", "");
            string strPlanQty = spdData.ActiveSheet.GetText(e.Row, 10).Replace(",", "");
            string idName = strType.Equals("PART") ? "PART NO" : strType.Equals("RUN") ? "RUN ID" : "LOT ID";

            string wipHMK3A = spdData.ActiveSheet.GetText(e.Row, 18).Replace(",", "");
            string wipFINISH = spdData.ActiveSheet.GetText(e.Row, 19).Replace(",", "");
            string wipMOLD = spdData.ActiveSheet.GetText(e.Row, 20).Replace(",", "");
            string wipWB = spdData.ActiveSheet.GetText(e.Row, 21).Replace(",", "");
            string wipDA = spdData.ActiveSheet.GetText(e.Row, 22).Replace(",", "");
            string wipSAW = spdData.ActiveSheet.GetText(e.Row, 23).Replace(",", "");
            string wipSTOCK = spdData.ActiveSheet.GetText(e.Row, 24).Replace(",", "");
            string wipWBLAST = spdData.ActiveSheet.GetText(e.Row, 34).Replace(",", "");

            LoadingPopUp.LoadIngPopUpShow(this);
            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup(strCustomer, strType, strMatId, strLotId, strStartDate, strEndDate, strPlanQty));

            if (dt != null && dt.Rows.Count > 0)
            {
                LoadingPopUp.LoadingPopUpHidden();
                System.Windows.Forms.Form frm = new PRD010421_P1("Operation remains and CutOff Day", dt, idName, wipHMK3A.Equals("") ? "0" : wipHMK3A, wipFINISH.Equals("") ? "0" : wipFINISH, wipMOLD.Equals("") ? "0" : wipMOLD, wipWB.Equals("") ? "0" : wipWB, wipDA.Equals("") ? "0" : wipDA, wipSAW.Equals("") ? "0" : wipSAW, wipSTOCK.Equals("") ? "0" : wipSTOCK, wipWBLAST.Equals("") ? "0" : wipWBLAST);
                frm.ShowDialog();
            }
            else
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD055", GlobalVariable.gcLanguage));
            }
        }

        #endregion

        /// <summary>
        /// Part에 해당하는 잔량 및 cutoff day를 구하기 위한 쿼리 생성
        /// </summary>
        /// <param name="strType">타입('PART', 'RUN', 'LOT')</param>
        /// <param name="strMatId">파트 넘버</param>
        /// <param name="strLotId">랏 넘버</param>
        /// <param name="strStartDate">시작일</param>
        /// <param name="strEndDate">납기</param>
        /// <param name="strPlanQty">계획량</param>
        /// 
        /// <returns>쿼리</returns>
        private string MakeSqlStringForPopup(string strCustomer, string strType, string strMatId, string strLotId, string strStartDate, string strEndDate, string strPlanQty)
        {
            StringBuilder strSqlString = new StringBuilder();
            if ("PART".Equals(strType.Trim()))
            {
                strSqlString.Append("WITH                   \n");
                strSqlString.Append("TAT AS(                   \n");
                strSqlString.Append("SELECT CASE WHEN OPER IN ('A0200','A0230') THEN 'SAW'         \n");
                strSqlString.Append("            WHEN OPER LIKE 'A040%' THEN 'DA'         \n");
                strSqlString.Append("            WHEN OPER LIKE 'A060%' THEN 'WB'         \n");
                strSqlString.Append("            WHEN OPER = 'A1000' THEN 'MOLD'         \n");
                strSqlString.Append("            ELSE 'UNKNOWN' END AS OPER         \n");
                strSqlString.Append("     , CUM_TOTAL AS TAT         \n");
                strSqlString.Append("  FROM (SELECT SAP_CODE, OPER, MAX(SEQ_NUM), MIN(SEQ_NUM), MAX(TAT), SUM(MAX(TAT)) OVER(ORDER BY MAX(SEQ_NUM) DESC) AS CUM_TOTAL         \n");
                strSqlString.Append("             , CASE WHEN OPER LIKE 'A040%' THEN SUBSTR(OPER,5,1)         \n");
                strSqlString.Append("                    WHEN OPER LIKE 'A060%' THEN SUBSTR(OPER,5,1)         \n");
                strSqlString.Append("                    ELSE '-' END AS STEP         \n");
                strSqlString.Append("             , CASE WHEN OPER LIKE 'A040%' THEN 'DA'         \n");
                strSqlString.Append("                    WHEN OPER LIKE 'A060%' THEN 'WB'         \n");
                strSqlString.Append("                    ELSE '-' END AS OPER_NAME         \n");
                strSqlString.Append("             , CASE WHEN OPER LIKE 'A040%' THEN RANK() OVER (PARTITION BY SUBSTR(OPER,1,4) ORDER BY OPER DESC)         \n");
                strSqlString.Append("                    WHEN OPER LIKE 'A060%' THEN RANK() OVER (PARTITION BY SUBSTR(OPER,1,4) ORDER BY OPER DESC)         \n");
                strSqlString.Append("                    ELSE 0 END AS STEP_SEQ         \n");
                strSqlString.Append("          FROM (SELECT MAT_ID, SAP_CODE, OPER, SEQ_NUM, TAT, TAT_SUM, RANK() OVER ( ORDER BY TAT_SUM,SAP_CODE) RK         \n");
                strSqlString.Append("                  FROM (SELECT MAT_ID         \n");
                strSqlString.Append("                             , SAP.SAP_CODE         \n");
                strSqlString.Append("                             , SAP.OPER         \n");
                strSqlString.Append("                             , FLW.SEQ_NUM         \n");
                strSqlString.Append("                             , SAP.TAT_DAY+SAP.TAT_DAY_WAIT TAT         \n");
                strSqlString.Append("                             , SUM(SAP.TAT_DAY+SAP.TAT_DAY_WAIT) OVER (PARTITION BY MAT_ID) TAT_SUM         \n");
                strSqlString.Append("                          FROM MWIPMATDEF MAT         \n");
                strSqlString.Append("                          LEFT OUTER JOIN CWIPSAPTAT@RPTTOMES SAP         \n");
                strSqlString.Append("                            ON MAT.VENDOR_ID = SAP.SAP_CODE         \n");
                strSqlString.Append("                           AND MAT.FACTORY = SAP.FACTORY         \n");
                strSqlString.Append("                          LEFT OUTER JOIN MWIPFLWOPR@RPTTOMES FLW         \n");
                strSqlString.Append("                            ON MAT.FIRST_FLOW = FLW.FLOW         \n");
                strSqlString.Append("                           AND FLW.OPER = SAP.OPER         \n");
                strSqlString.Append("                         WHERE 1 = 1          \n");
                strSqlString.Append("                           AND MAT.MAT_ID LIKE '"+strMatId+"'         \n");
                strSqlString.Append("                           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'                  \n");
                strSqlString.Append("                           AND MAT.MAT_TYPE = 'FG'                  \n");
                strSqlString.Append("                           AND SAP.RESV_FIELD_1 = ' '          \n");
                strSqlString.Append("                         ORDER BY 3         \n");
                strSqlString.Append("                       )         \n");
                strSqlString.Append("                 ORDER BY TAT_SUM         \n");
                strSqlString.Append("              ) WHERE RK = 1         \n");
                strSqlString.Append("         GROUP BY SAP_CODE, OPER         \n");
                strSqlString.Append("         ORDER BY MAX(SEQ_NUM), OPER         \n");
                strSqlString.Append("       )         \n");
                strSqlString.Append(" WHERE OPER IN ('A0200','A0230', 'A1000')          \n");
                strSqlString.Append("    OR (OPER_NAME IN ( 'DA', 'WB') AND STEP_SEQ = 1)                  \n");
                strSqlString.Append(")                   \n");

                strSqlString.Append("SELECT '" + strCustomer + "'          " + "\n");
                strSqlString.Append("     , '" + strMatId + "'          " + "\n");
                strSqlString.Append("     , 'PART'          " + "\n");
                strSqlString.Append("     , OPER          " + "\n");
                strSqlString.Append("     , " + strPlanQty + " - SUM(TOTAL) AS TOTAL          " + "\n");
                strSqlString.Append("     , '-'          " + "\n");
                strSqlString.Append("     , '-'          " + "\n");
                strSqlString.Append("     , CASE WHEN OPER = 'SAW' THEN  TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS') - (SELECT SUM(TAT) FROM TAT WHERE OPER IN ( 'SAW')), 'MM/DD HH24:MI')          " + "\n");
                strSqlString.Append("            WHEN OPER = 'DA' THEN TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS') - (SELECT SUM(TAT) FROM TAT WHERE OPER IN ( 'DA')), 'MM/DD HH24:MI')          " + "\n");
                strSqlString.Append("            WHEN OPER = 'WB' THEN TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS') - (SELECT SUM(TAT) FROM TAT WHERE OPER IN ( 'WB')), 'MM/DD HH24:MI')          " + "\n");
                strSqlString.Append("            WHEN OPER = 'MOLD' THEN TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS') - (SELECT SUM(TAT) FROM TAT WHERE OPER = 'MOLD'), 'MM/DD HH24:MI')          " + "\n");
                strSqlString.Append("            WHEN OPER = 'FINISH' THEN TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS'), 'MM/DD HH24:MI')          " + "\n");
                strSqlString.Append("            ELSE TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS'), 'MM/DD HH24:MI')          " + "\n");
                strSqlString.Append("       END TAT          " + "\n");
                strSqlString.Append("  FROM (          " + "\n");
                strSqlString.Append("        SELECT B.MAT_ID          " + "\n");
                strSqlString.Append("            , CASE WHEN A.OPER IN ('A0200', 'A0230') THEN 'SAW'          " + "\n");
                strSqlString.Append("                   WHEN A.OPER LIKE 'A040%' THEN 'DA'          " + "\n");
                strSqlString.Append("                   WHEN A.OPER LIKE 'A060%' THEN 'WB'          " + "\n");
                strSqlString.Append("                   WHEN A.OPER = 'A1000' THEN 'MOLD'          " + "\n");
                strSqlString.Append("                   WHEN A.OPER = 'AZ010' THEN 'FINISH'          " + "\n");
                strSqlString.Append("                   ELSE '-'          " + "\n");
                strSqlString.Append("              END OPER          " + "\n");
                strSqlString.Append("            , (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'SZ010') THEN SHIP_QTY_1 WHEN OPER IN( 'A0200', 'A0230') THEN NVL(END_QTY_1,0)/NVL(GCM.DATA_1,1) ELSE END_QTY_1 END) TOTAL          " + "\n");
                strSqlString.Append("         FROM (          " + "\n");
                strSqlString.Append("              SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3          " + "\n");
                strSqlString.Append("                   , SUM(END_LOT) AS END_LOT          " + "\n");
                strSqlString.Append("                   , SUM(END_QTY_1) AS END_QTY_1          " + "\n");
                strSqlString.Append("                   , SUM(END_QTY_2) AS END_QTY_2          " + "\n");
                strSqlString.Append("                   , SUM(SHIP_QTY_1) AS SHIP_QTY_1          " + "\n");
                strSqlString.Append("                   , SUM(SHIP_QTY_2) AS SHIP_QTY_2          " + "\n");
                strSqlString.Append("                FROM (          " + "\n");
                strSqlString.Append("                      SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3          " + "\n");
                strSqlString.Append("                           , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_LOT+S2_OPER_IN_LOT+S3_OPER_IN_LOT),SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT)) END_LOT          " + "\n");
                strSqlString.Append("                           , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1          " + "\n");
                strSqlString.Append("                           , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2),SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2+S1_END_RWK_QTY_2 + S2_END_RWK_QTY_2 + S3_END_RWK_QTY_2)) END_QTY_2          " + "\n");
                strSqlString.Append("                           , 0 SHIP_QTY_1          " + "\n");
                strSqlString.Append("                           , 0 SHIP_QTY_2          " + "\n");
                strSqlString.Append("                        FROM RSUMWIPMOV          " + "\n");
                strSqlString.Append("                       WHERE OPER NOT IN ('AZ010','TZ010','F0000','EZ010', 'SZ010')          " + "\n");
                strSqlString.Append("                    GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3          " + "\n");
                strSqlString.Append("                   UNION ALL          " + "\n");
                strSqlString.Append("                      SELECT CM_KEY_1 AS FACTORY, MAT_ID          " + "\n");
                strSqlString.Append("                           , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010','HMKS1','SZ010') OPER          " + "\n");
                strSqlString.Append("                           , LOT_TYPE, MAT_VER,  WORK_DATE,CM_KEY_3          " + "\n");
                strSqlString.Append("                           , 0 END_LOT          " + "\n");
                strSqlString.Append("                           , 0 END_QTY_1          " + "\n");
                strSqlString.Append("                           , 0 END_QTY_2          " + "\n");
                strSqlString.Append("                           , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) SHIP_QTY_1          " + "\n");
                strSqlString.Append("                           , SUM(S1_FAC_OUT_QTY_2+S2_FAC_OUT_QTY_2+S3_FAC_OUT_QTY_2) SHIP_QTY_2          " + "\n");
                strSqlString.Append("                        FROM RSUMFACMOV          " + "\n");
                strSqlString.Append("                       WHERE FACTORY NOT IN ('RETURN')          " + "\n");
                strSqlString.Append("                    GROUP BY CM_KEY_1, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3          " + "\n");
                strSqlString.Append("                     )          " + "\n");
                strSqlString.Append("            GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3          " + "\n");
                strSqlString.Append("            )A          " + "\n");
                strSqlString.Append("          , MWIPMATDEF B          " + "\n");
                strSqlString.Append("          , (                      " + "\n"); 
                strSqlString.Append("            SELECT KEY_1 AS MAT_ID, DATA_1  " + "\n");
                strSqlString.Append("              FROM MGCMTBLDAT   " + "\n");
                strSqlString.Append("             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                strSqlString.Append("               AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  " + "\n");
                strSqlString.Append("            ) GCM                 " + "\n");
                strSqlString.Append("        WHERE 1=1          " + "\n");
                strSqlString.Append("        AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'          " + "\n");
                strSqlString.Append("        AND A.FACTORY = B.FACTORY          " + "\n");
                strSqlString.Append("        AND A.MAT_ID = B.MAT_ID          " + "\n");
                strSqlString.Append("        AND A.MAT_ID = GCM.MAT_ID(+)     " + "\n");
                strSqlString.Append("        AND A.MAT_VER = 1          " + "\n");
                strSqlString.Append("        AND ((A.OPER LIKE 'A040%' AND A.OPER = 'A040'||DECODE(B.MAT_GRP_4,'SD2','2','SD3','3','SD4','4','SD5','5','SD8','8','0' ) ) OR         \n");
                strSqlString.Append("             (A.OPER LIKE 'A060%' AND A.OPER = 'A060'||DECODE(B.MAT_GRP_4,'SD2','2','SD3','3','SD4','4','SD5','5','SD8','8','0' ) ) OR         \n");
                strSqlString.Append("             (A.OPER NOT LIKE 'A040%' AND A.OPER NOT LIKE 'A060%'))          \n");
                strSqlString.Append("        AND ((A.OPER IN ('A0200', 'A0230') AND B.MAT_GRP_5 IN ( '-', '1st', 'Merge')) OR           " + "\n");
                strSqlString.Append("              A.OPER NOT IN ('A0200', 'A0230') AND B.MAT_GRP_5 IN ( 'Merge', '-'))           " + "\n");
                strSqlString.Append("        AND B.MAT_VER = 1          " + "\n");
                strSqlString.Append("        AND B.MAT_TYPE = 'FG'          " + "\n");
                strSqlString.Append("        AND A.OPER LIKE '%'          " + "\n");
                strSqlString.Append("        AND A.MAT_ID LIKE '" + strMatId + "'          " + "\n");
                strSqlString.Append("        AND A.OPER NOT IN ('00001','00002')          " + "\n");
                strSqlString.Append("        AND A.WORK_DATE >=  GET_WORK_DATE(TO_CHAR(TO_DATE( '" + strStartDate + "', 'YYYY-MM-DD HH24:MI:SS'), 'YYYYMMDDHH24MISS'), 'D')          " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                strSqlString.Append("         (SELECT ' ', 'SAW', 0 FROM DUAL UNION SELECT ' ', 'DA', 0 FROM DUAL UNION SELECT ' ', 'WB', 0 FROM DUAL UNION SELECT ' ', 'MOLD', 0 FROM DUAL UNION SELECT ' ', 'FINISH', 0 FROM DUAL) " + "\n");
                strSqlString.Append("       ) FACMOV          " + "\n");
                strSqlString.Append(" WHERE 1 = 1          " + "\n");
                strSqlString.Append("   AND OPER <> '-'          " + "\n");
                strSqlString.Append(" GROUP BY OPER          " + "\n");
                strSqlString.Append(" ORDER BY DECODE(OPER, 'SAW', 1, 'DA', 2, 'WB', 3, 'MOLD', 4, 'FINISH', 5)          " + "\n");
            }
            else
            {
                strSqlString.Append("WITH                   \n");
                strSqlString.Append("TAT AS(                   \n");
                strSqlString.Append("SELECT CASE WHEN OPER IN ('A0200','A0230') THEN 'SAW'         \n");
                strSqlString.Append("            WHEN OPER LIKE 'A040%' THEN 'DA'         \n");
                strSqlString.Append("            WHEN OPER LIKE 'A060%' THEN 'WB'         \n");
                strSqlString.Append("            WHEN OPER = 'A1000' THEN 'MOLD'         \n");
                strSqlString.Append("            ELSE 'UNKNOWN' END AS OPER         \n");
                strSqlString.Append("     , CUM_TOTAL AS TAT         \n");
                strSqlString.Append("  FROM (SELECT SAP_CODE, OPER, MAX(SEQ_NUM), MIN(SEQ_NUM), MAX(TAT), SUM(MAX(TAT)) OVER(ORDER BY MAX(SEQ_NUM) DESC) AS CUM_TOTAL         \n");
                strSqlString.Append("             , CASE WHEN OPER LIKE 'A040%' THEN SUBSTR(OPER,5,1)         \n");
                strSqlString.Append("                    WHEN OPER LIKE 'A060%' THEN SUBSTR(OPER,5,1)         \n");
                strSqlString.Append("                    ELSE '-' END AS STEP         \n");
                strSqlString.Append("             , CASE WHEN OPER LIKE 'A040%' THEN 'DA'         \n");
                strSqlString.Append("                    WHEN OPER LIKE 'A060%' THEN 'WB'         \n");
                strSqlString.Append("                    ELSE '-' END AS OPER_NAME         \n");
                strSqlString.Append("             , CASE WHEN OPER LIKE 'A040%' THEN RANK() OVER (PARTITION BY SUBSTR(OPER,1,4) ORDER BY OPER DESC)         \n");
                strSqlString.Append("                    WHEN OPER LIKE 'A060%' THEN RANK() OVER (PARTITION BY SUBSTR(OPER,1,4) ORDER BY OPER DESC)         \n");
                strSqlString.Append("                    ELSE 0 END AS STEP_SEQ         \n");
                strSqlString.Append("          FROM (SELECT MAT_ID, SAP_CODE, OPER, SEQ_NUM, TAT, TAT_SUM, RANK() OVER ( ORDER BY TAT_SUM,SAP_CODE) RK         \n");
                strSqlString.Append("                  FROM (SELECT MAT_ID         \n");
                strSqlString.Append("                             , SAP.SAP_CODE         \n");
                strSqlString.Append("                             , SAP.OPER         \n");
                strSqlString.Append("                             , FLW.SEQ_NUM         \n");
                strSqlString.Append("                             , SAP.TAT_DAY+SAP.TAT_DAY_WAIT TAT         \n");
                strSqlString.Append("                             , SUM(SAP.TAT_DAY+SAP.TAT_DAY_WAIT) OVER (PARTITION BY MAT_ID) TAT_SUM         \n");
                strSqlString.Append("                          FROM MWIPMATDEF MAT         \n");
                strSqlString.Append("                          LEFT OUTER JOIN CWIPSAPTAT@RPTTOMES SAP         \n");
                strSqlString.Append("                            ON MAT.VENDOR_ID = SAP.SAP_CODE         \n");
                strSqlString.Append("                           AND MAT.FACTORY = SAP.FACTORY         \n");
                strSqlString.Append("                          LEFT OUTER JOIN MWIPFLWOPR@RPTTOMES FLW         \n");
                strSqlString.Append("                            ON MAT.FIRST_FLOW = FLW.FLOW         \n");
                strSqlString.Append("                           AND FLW.OPER = SAP.OPER         \n");
                strSqlString.Append("                         WHERE 1 = 1          \n");
                strSqlString.Append("                           AND MAT.MAT_ID LIKE '" + strMatId + "'         \n");
                strSqlString.Append("                           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'                  \n");
                strSqlString.Append("                           AND MAT.MAT_TYPE = 'FG'                  \n");
                strSqlString.Append("                           AND SAP.RESV_FIELD_1 = ' '          \n");
                strSqlString.Append("                         ORDER BY 3         \n");
                strSqlString.Append("                       )         \n");
                strSqlString.Append("                 ORDER BY TAT_SUM         \n");
                strSqlString.Append("              ) WHERE RK = 1         \n");
                strSqlString.Append("         GROUP BY SAP_CODE, OPER         \n");
                strSqlString.Append("         ORDER BY MAX(SEQ_NUM), OPER         \n");
                strSqlString.Append("       )         \n");
                strSqlString.Append(" WHERE OPER IN ('A0200','A0230', 'A1000')          \n");
                strSqlString.Append("    OR (OPER_NAME IN ( 'DA', 'WB') AND STEP_SEQ = 1)                  \n");
                strSqlString.Append(")                   \n");

                strSqlString.Append("SELECT '" + strCustomer + "'          " + "\n");
                strSqlString.Append("     , '" + strLotId + "'                            " + "\n");
                strSqlString.Append("     , '" + strType + "'                            " + "\n");
                strSqlString.Append("     , OPER                            " + "\n");
                if (strType.Equals("RUN")){
                    strSqlString.Append("     , " + strPlanQty + " - SUM(TOTAL) AS TOTAL                            " + "\n");
                } else {
                    strSqlString.Append("     , CASE WHEN OPER = 'SAW' THEN 0 ELSE " + strPlanQty + " - SUM(TOTAL) END AS TOTAL /* LOT 으로 지정 시 SAW 공정 잔량 0 으로 표시 */                           " + "\n");
                }
                strSqlString.Append("     , '-'          " + "\n");
                strSqlString.Append("     , '-'          " + "\n");
                strSqlString.Append("     , CASE WHEN OPER = 'SAW' THEN  TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS') - (SELECT SUM(TAT) FROM TAT WHERE OPER IN (  'SAW')), 'MM/DD HH24:MI')                            " + "\n");
                strSqlString.Append("            WHEN OPER = 'DA' THEN TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS') - (SELECT SUM(TAT) FROM TAT WHERE OPER IN ('DA')), 'MM/DD HH24:MI')                            " + "\n");
                strSqlString.Append("            WHEN OPER = 'WB' THEN TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS') - (SELECT SUM(TAT) FROM TAT WHERE OPER IN ('WB')), 'MM/DD HH24:MI')                            " + "\n");
                strSqlString.Append("            WHEN OPER = 'MOLD' THEN TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS') - (SELECT SUM(TAT) FROM TAT WHERE OPER = 'MOLD'), 'MM/DD HH24:MI')                            " + "\n");
                strSqlString.Append("            WHEN OPER = 'FINISH' THEN TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS'), 'MM/DD HH24:MI')                            " + "\n");
                strSqlString.Append("            ELSE TO_CHAR(TO_DATE('" + strEndDate + "', 'YYYY-MM-DD HH24:MI:SS'), 'MM/DD HH24:MI')                            " + "\n");
                strSqlString.Append("       END TAT                            " + "\n");
                strSqlString.Append("  FROM (                            " + "\n");
                strSqlString.Append("        SELECT RWIPLOTHIS.MAT_ID " + "\n");
                strSqlString.Append("              , CASE WHEN OLD_OPER IN ('A0200', 'A0230') THEN 'SAW'                            " + "\n");
                strSqlString.Append("                    WHEN OLD_OPER LIKE 'A040%' THEN 'DA'                            " + "\n");
                strSqlString.Append("                    WHEN OLD_OPER LIKE 'A060%' THEN 'WB'                            " + "\n");
                strSqlString.Append("                    WHEN OLD_OPER = 'A1000' THEN 'MOLD'                            " + "\n");
                strSqlString.Append("                    WHEN OLD_OPER = 'AZ010' THEN 'FINISH'                            " + "\n");
                strSqlString.Append("                   ELSE '-'                            " + "\n");
                strSqlString.Append("                END OPER                          " + "\n");
                strSqlString.Append("              , CASE WHEN OLD_OPER IN ('A0200', 'A0230') THEN NVL(SUM(QTY_1),0)/(SELECT NVL(MAX(DATA_1),1) FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = '" + strMatId + "')                          " + "\n");
                strSqlString.Append("                     ELSE SUM(QTY_1) END AS TOTAL   " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS,  MWIPMATDEF                            " + "\n");
                strSqlString.Append("         WHERE 1 = 1                            " + "\n");
                if (strType.Equals("RUN")) {
                    strSqlString.Append("           AND LOT_CMF_4 LIKE '" + strLotId + "'                            " + "\n");
                }
                else {
                    strSqlString.Append("           AND LOT_ID LIKE '" + strLotId + "%'                            " + "\n");
                }
                strSqlString.Append("           AND RWIPLOTHIS.MAT_ID = MWIPMATDEF.MAT_ID(+)      \n");
                strSqlString.Append("           AND MWIPMATDEF.MAT_VER = 1   \n");
                strSqlString.Append("           AND MWIPMATDEF.MAT_TYPE = 'FG'   \n");
                strSqlString.Append("           AND MWIPMATDEF.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   \n");
                strSqlString.Append("           AND TRAN_CODE IN ('END', 'SHIP')                            " + "\n");
                strSqlString.Append("           AND HIST_DEL_FLAG = ' '                            " + "\n");
                strSqlString.Append("           AND LOT_TYPE = 'W'                            " + "\n");
                strSqlString.Append("           AND ((OLD_OPER LIKE 'A040%' AND OLD_OPER = 'A040'||DECODE(MWIPMATDEF.MAT_GRP_4,'SD2','2','SD3','3','SD4','4','SD5','5','SD8','8','0' ) ) OR         \n");
                strSqlString.Append("                (OLD_OPER LIKE 'A060%' AND OLD_OPER = 'A060'||DECODE(MWIPMATDEF.MAT_GRP_4,'SD2','2','SD3','3','SD4','4','SD5','5','SD8','8','0' ) ) OR         \n");
                strSqlString.Append("                (OLD_OPER NOT LIKE 'A040%' AND OLD_OPER NOT LIKE 'A060%'))          \n");
                strSqlString.Append("           AND ((OLD_OPER IN ('A0200', 'A0230') AND MAT_GRP_5 IN ( '-', '1st', 'Merge')) OR                                            " + "\n");
                strSqlString.Append("                 OLD_OPER NOT IN ('A0200', 'A0230') AND MAT_GRP_5 IN ( 'Merge', '-'))                                         " + "\n");
                strSqlString.Append("           AND TRAN_TIME >=  TO_CHAR(TO_DATE( '" + strStartDate + "', 'YYYY-MM-DD HH24:MI:SS'), 'YYYYMMDDHH24MISS')          " + "\n");
                strSqlString.Append("         GROUP BY RWIPLOTHIS.MAT_ID, OLD_OPER                            " + "\n");
                strSqlString.Append("         UNION ALL                                    " + "\n");
                strSqlString.Append("         (SELECT '" + strMatId + "' MAT_ID, 'SAW', 0 FROM DUAL UNION SELECT '" + strMatId + "' MAT_ID,'DA', 0 FROM DUAL UNION SELECT '" + strMatId + "' MAT_ID,'WB', 0 FROM DUAL UNION SELECT '" + strMatId + "' MAT_ID,'MOLD', 0 FROM DUAL UNION SELECT '" + strMatId + "' MAT_ID,'FINISH', 0 FROM DUAL) " + "\n");
                strSqlString.Append("       ) FACMOV                            " + "\n");
                strSqlString.Append(" WHERE 1 = 1                            " + "\n");
                strSqlString.Append("   AND OPER <> '-'                            " + "\n");
                strSqlString.Append(" GROUP BY OPER                            " + "\n");
                strSqlString.Append(" ORDER BY DECODE(OPER, 'SAW', 1, 'DA', 2, 'WB', 3, 'MOLD', 4, 'FINISH', 5)                            " + "\n");

            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private void changeSpreadColor()
        {
            spdData.ActiveSheet.Columns[0].BackColor = colorFixedColumn;
            spdData.ActiveSheet.Columns[1].BackColor = colorFixedColumn;
            spdData.ActiveSheet.Columns[2].BackColor = colorFixedColumn;
            spdData.ActiveSheet.Columns[3].BackColor = colorFixedColumn;

            // 제공의 잔량에 대한 음영 표시 ( 해당 공정까지의 wip을 합쳐서 잔량을 넘어서면 색상 변화
            for (int row = 0; row < spdData.ActiveSheet.RowCount; row++)
            {
                int remain = 0;
                int wipSum = 0;
                string strRemainWip = spdData.ActiveSheet.GetText(row, 12).Replace(",", ""); // 9번째 칼럼이 잔량
                // 잔량이 "" 으로 표현되어 있으면 0으로 처리
                if ( strRemainWip.Equals("") ) {
                    strRemainWip = "0";
                }
                bool result = Int32.TryParse(strRemainWip, out remain); 
                if (result == false)
                {
                    continue;
                }
                if (remain <= 0)
                {
                    continue;
                }
                for (int col = wipStartPos; col <= wipEndPos; col++)
                { // 제공 부분
                    int wip = 0;
                    string strWip = spdData.ActiveSheet.GetText(row, col).Replace(",", "");
                    // 제공이  "" 으로 표현되어 있으면 0으로 처리
                    if (strWip.Equals(""))
                    {
                        strWip = "0";
                    }
                    result = Int32.TryParse(strWip, out wip);
                    if (result == false)
                    {
                        continue;
                    }
                    wipSum += wip;
                    if (remain > wipSum)// 잔량이 많이 남았을 때 붉은색
                    {
                        spdData.ActiveSheet.Cells[row, col].BackColor = colorRemain;
                    }
                    else // 잔량 보다 제공이 큰 공정을 지나면 break;
                    {
                        spdData.ActiveSheet.Cells[row, col].BackColor = colorOver;
                        break;
                    }

                }
            }
            // 이미 테스트로 넘어간 제품들의 색을 회색으로 변경
            for (int row = 0; row < spdData.ActiveSheet.RowCount; row++)
            {
                if ((spdData.ActiveSheet.GetText(row, 10).Replace(",", "").Equals("") ? 0 : Convert.ToInt32(spdData.ActiveSheet.GetText(row, 10).Replace(",", ""))) /* 계획 */
                    - (spdData.ActiveSheet.GetText(row, 11).Replace(",", "").Equals("") ? 0 : Convert.ToInt32(spdData.ActiveSheet.GetText(row, 11).Replace(",", ""))) /* 실적 */
                    - (spdData.ActiveSheet.GetText(row, 33).Replace(",", "").Equals("") ? 0 : Convert.ToInt32(spdData.ActiveSheet.GetText(row, 33).Replace(",", ""))) /* LOSS */ 
                    <= 0)
                {
                    for ( int col = 4 ; col < spdData.ActiveSheet.ColumnCount ; col++) {
                        spdData.ActiveSheet.Cells[row, col].BackColor = Color.LightGray;
                    }
                }
            }

        }

        private Boolean getColumnPosition()
        {
            // 1 제공 시작 위치
            wipStartPos = 18;
            wipEndPos = 24;
            partNoPos = 4;
            //tatPos = 17;
            //lossPos = 33;

            return true;
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvOper.sFactory = cdvFactory.txtValue;
            //cdvCustomer.sFactory = cdvFactory.txtValue;
        }

        private void btnExcelExport_Click_1(object sender, EventArgs e)
        {
            spdData.ExportExcel();
        }
    }
}
