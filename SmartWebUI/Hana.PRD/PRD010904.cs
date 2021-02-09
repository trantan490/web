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
    public partial class PRD010904 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010904<br/>
        /// 클래스요약: 투입관리<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2011-05-11<br/>
        /// 상세  설명: 투입관리<br/>
        /// 변경  내용: <br/>
        /// 2011-07-13-임종우 : H+0 클릭시 POP UP 창 표시, H+0~H+2 실제 시간으로 표시, FINISH 재공에서 HMK3 재공 제외, 잔여시간 24시 기준으로 변경(임태성 요청)
        /// 2011-07-15-임종우 : SLSI 제외 한 제품의 투입 관리 추가 (임태성 요청)
        /// 2011-07-18-임종우 : 투입 목표 추가, LOT_TYPE 검색 추가 (임태성 요청)
        /// 2011-07-20-임종우 : 일 목표 추가, SEC_VERSION 추가 (임태성 요청)
        /// 2011-07-28-임종우 : D/A 1차 CHIP 만의 CAPA를 구하기 위해 A0400, A0401 공정으로 제한 함 (임태성 요청)
        /// 2011-07-29-임종우 : POPUP창에 PRODUCT 정보 표시 추가 (임태성 요청)
        /// 2011-08-25-임종우 : 1, 2순위 로직 변경 -> CAPA 존재하면 무조건 CAPA 기준, 없으면 계획 기준
        ///                     적정 재공량 로직 변경 -> D/A Capa * TAT * 여유율(115%, 130%, 145%, 160%, 175%, 200%)
        ///                     OVER 여부 추가 표시 -> 적정재공량 - 전체 WIP (B/G ~ PVI)
        /// 2011-11-21-임종우 : 월 계획 제품 중복 오류 수정
        /// 2012-02-01-김민우 : MRASRESSTS의 공정정보 RES_STS_9에서 RES_STS_8로 변경
        
        /// </summary>
        public PRD010904()
        {
            InitializeComponent();

            
            SortInit();
            GridColumnInit();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #region " Constant Definition "

        #endregion

        #region " Function Definition "

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
            int iTimeBase = Convert.ToInt32(cdvTime.Text) * 2;

            String strH0 = DateTime.Now.ToString("HH");
            String strH1 = DateTime.Now.AddHours(Convert.ToDouble(cdvTime.Text)).ToString("HH");
            String strH2 = DateTime.Now.AddHours(iTimeBase).ToString("HH");

            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);            
            spdData.RPT_AddBasicColumn("Product", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Sec Version", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("a daily goal", 0, 5, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("Daily input residual quantity", 0, 6, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("Over status", 0, 7, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("DieBank", 0, 8, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);

            spdData.RPT_AddBasicColumn("INPUT", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn(strH0 + "시", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn(strH1 + "시", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn(strH2 + "시", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_MerageHeaderColumnSpan(0, 9, 3);

            spdData.RPT_AddBasicColumn("WIP", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("B/G-SAW", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("DA", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("WB", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("MOLD", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("FINISH", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("TTL", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_MerageHeaderColumnSpan(0, 12, 6);

            spdData.RPT_AddBasicColumn("Proper WIP", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

            spdData.RPT_AddBasicColumn("DA", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Capa", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("count", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_MerageHeaderColumnSpan(0, 19, 2);
                        
            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 18, 2);
                        
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {

        }
        
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            #region SLSI 조회
            if (rdbSLSI.Checked == true)
            {
                strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER" + "\n");
                strSqlString.Append("     , MAT_GRP_3, MAT_GRP_6, MAT_ID, ATTR_VALUE, ROUND(DAY_PLAN, 0) AS DAY_PLAN, ROUND(IN_PLAN, 0) AS IN_PLAN, OPT_WIP - ETC AS OVER_QTY, STOCK" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN STA_QTY = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN STA_QTY >= (BASE_QTY * " + cdvTime.Text + ") THEN (BASE_QTY * " + cdvTime.Text + ")" + "\n");
                strSqlString.Append("                  ELSE STA_QTY" + "\n");
                strSqlString.Append("             END, 0) H0" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN STA_QTY = 0 THEN 0" + "\n");                
                strSqlString.Append("                  WHEN STA_QTY >= (BASE_QTY * " + cdvTime.Text + " * 2) THEN (BASE_QTY * " + cdvTime.Text + ")" + "\n");
                strSqlString.Append("                  WHEN STA_QTY - (BASE_QTY * " + cdvTime.Text + ") <= 0 THEN 0" + "\n");
                strSqlString.Append("                  ELSE STA_QTY - (BASE_QTY * " + cdvTime.Text + ")" + "\n");
                strSqlString.Append("             END, 0) H1" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN STA_QTY = 0 THEN 0" + "\n");                
                strSqlString.Append("                  WHEN STA_QTY >= (BASE_QTY * " + cdvTime.Text + " * 3) THEN (BASE_QTY * " + cdvTime.Text + ")" + "\n");
                strSqlString.Append("                  WHEN STA_QTY - (BASE_QTY * " + cdvTime.Text + " * 2) <= 0 THEN 0" + "\n");
                strSqlString.Append("                  ELSE STA_QTY - (BASE_QTY * " + cdvTime.Text + " * 2)" + "\n");
                strSqlString.Append("             END, 0) H2     " + "\n");
                //strSqlString.Append("     , ROUND(CASE WHEN STOCK >= ((IN_PLAN / REMAIN_TIME) * " + cdvTime.Text + ") THEN ((IN_PLAN / REMAIN_TIME) * " + cdvTime.Text + ")" + "\n");
                //strSqlString.Append("                  ELSE STOCK" + "\n");
                //strSqlString.Append("             END, 0) H0" + "\n");
                //strSqlString.Append("     , ROUND(CASE WHEN STOCK >= ((IN_PLAN / REMAIN_TIME) * " + cdvTime.Text + " * 2) THEN ((IN_PLAN / REMAIN_TIME) * " + cdvTime.Text + ")" + "\n");
                //strSqlString.Append("                  WHEN STOCK = 0 THEN 0" + "\n");
                //strSqlString.Append("                  WHEN STOCK - ((IN_PLAN / REMAIN_TIME) * " + cdvTime.Text + ") <= 0 THEN 0" + "\n");
                //strSqlString.Append("                  ELSE STOCK - ((IN_PLAN / REMAIN_TIME) * " + cdvTime.Text + ")" + "\n");
                //strSqlString.Append("             END, 0) H1" + "\n");
                //strSqlString.Append("     , ROUND(CASE WHEN STOCK >= ((IN_PLAN / REMAIN_TIME) * " + cdvTime.Text + " * 3) THEN ((IN_PLAN / REMAIN_TIME) * " + cdvTime.Text + ")" + "\n");
                //strSqlString.Append("                  WHEN STOCK = 0 THEN 0" + "\n");
                //strSqlString.Append("                  WHEN STOCK - ((IN_PLAN / REMAIN_TIME) * " + cdvTime.Text + " * 2) <= 0 THEN 0" + "\n");
                //strSqlString.Append("                  ELSE STOCK - ((IN_PLAN / REMAIN_TIME) * " + cdvTime.Text + " * 2)" + "\n");
                //strSqlString.Append("             END, 0) H2     " + "\n");
                strSqlString.Append("     , SAW, DA, WB, MOLD, FINISH, ETC, OPT_WIP, CAPA, RES_CNT" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT MAT_GRP_1, MAT_GRP_3, MAT_GRP_6, MAT_ID, ATTR_VALUE" + "\n");
                strSqlString.Append("             , STOCK, SAW, DA, WB, MOLD, FINISH, ETC, PLAN_QTY, DAY_PLAN, REMAIN_QTY" + "\n");
                strSqlString.Append("             , CASE WHEN NVL(STOCK,0) = 0 OR NVL(IN_PLAN,0) <= 0 THEN 0 " + "\n");
                strSqlString.Append("                    WHEN NVL(STOCK,0) > NVL(IN_PLAN,0) THEN NVL(IN_PLAN,0) " + "\n");
                strSqlString.Append("                    ELSE NVL(STOCK,0) " + "\n");
                strSqlString.Append("                END AS STA_QTY " + "\n");
                strSqlString.Append("             , IN_PLAN, OPT_WIP * ( " + cboPer.Text + " / 100) AS OPT_WIP, CAPA, RES_CNT, BASE_QTY" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_3, MAT.MAT_GRP_6, MAT.MAT_ID, ATTR_VALUE, STOCK, SAW, DA, WB, MOLD, FINISH, ETC, PLAN_QTY" + "\n");
                strSqlString.Append("                     , ((NVL(MON_PLAN_QTY,0) - NVL(SHP_QTY,0)) / (SELECT LAST_DAY(SYSDATE) - SYSDATE LASTDAY FROM DUAL)) AS DAY_PLAN" + "\n");
                strSqlString.Append("                     , (NVL(PLAN_QTY,0) - NVL(ETC,0)) AS REMAIN_QTY" + "\n");
                strSqlString.Append("                     , NVL(CASE WHEN NVL(CAPA, 0) > 0 THEN CAPA" + "\n");
                strSqlString.Append("                                ELSE NVL(PLAN_QTY,0) - NVL(ETC,0) " + "\n");                
                strSqlString.Append("                           END,0) AS IN_PLAN" + "\n");
                //strSqlString.Append("                     , NVL(CASE WHEN (NVL(PLAN_QTY,0) - NVL(ETC,0)) < 0 THEN 0" + "\n");
                //strSqlString.Append("                                ELSE (CASE WHEN (NVL(PLAN_QTY,0) - NVL(ETC,0)) > NVL(CAPA,0) THEN (NVL(PLAN_QTY,0) - NVL(ETC,0))" + "\n");
                //strSqlString.Append("                                           ELSE CAPA" + "\n");
                //strSqlString.Append("                                      END)" + "\n");
                //strSqlString.Append("                           END,0) AS IN_PLAN" + "\n");
                strSqlString.Append("                     , CASE WHEN NVL(CAPA,0) > 0 AND NVL(TAT,0) > 0 THEN CAPA * TAT" + "\n");
                strSqlString.Append("                            WHEN NVL(CAPA,0) > 0 AND NVL(TAT,0) = 0 THEN CAPA" + "\n");
                strSqlString.Append("                            ELSE 0" + "\n");
                strSqlString.Append("                        END AS OPT_WIP" + "\n");
                strSqlString.Append("                     , CAPA, RES_CNT" + "\n");
                strSqlString.Append("                     , (SELECT TRUNC((TO_DATE(TO_CHAR(SYSDATE , 'YYYYMMDD')||'235959', 'YYYYMMDDHH24MISS') - SYSDATE) * 24,0) FROM DUAL) AS REMAIN_TIME" + "\n");
                strSqlString.Append("                     , ROUND(NVL(CASE WHEN NVL(CAPA, 0) > 0 THEN CAPA" + "\n");
                strSqlString.Append("                                      ELSE NVL(PLAN_QTY,0) - NVL(ETC,0)" + "\n");                
                strSqlString.Append("                                 END,0) " + "\n");

                // 2011-07-11-임종우 : 잔여시간 대신 하루 24시간으로 변경 함.
                strSqlString.Append("                              / 24 " + "\n");
                //strSqlString.Append("                              / (SELECT TRUNC((TO_DATE(TO_CHAR(SYSDATE , 'YYYYMMDD')||'235959', 'YYYYMMDDHH24MISS') - SYSDATE) * 24,0) FROM DUAL) " + "\n");

                strSqlString.Append("                            , 0) AS BASE_QTY" + "\n");
                //strSqlString.Append("                     , ROUND(NVL(CASE WHEN (NVL(PLAN_QTY,0) - NVL(ETC,0)) < 0 THEN 0" + "\n");
                //strSqlString.Append("                                      ELSE (CASE WHEN (NVL(PLAN_QTY,0) - NVL(ETC,0)) > NVL(CAPA,0) THEN (NVL(PLAN_QTY,0) - NVL(ETC,0))" + "\n");
                //strSqlString.Append("                                                 ELSE CAPA" + "\n");
                //strSqlString.Append("                                            END)" + "\n");
                //strSqlString.Append("                                 END,0) " + "\n");                
                //strSqlString.Append("                              / 24 " + "\n");
                //strSqlString.Append("                            , 0) AS BASE_QTY" + "\n");
                strSqlString.Append("                  FROM MWIPMATDEF@RPTTOMES MAT" + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT MAT_ID" + "\n");
                strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'HMK2A', QTY_1, 0)) AS STOCK" + "\n");
                strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'B/G', QTY_1, 'SAW', QTY_1, 0)) AS SAW" + "\n");
                strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/P', QTY_1, 'D/A', QTY_1, 0)) AS DA" + "\n");
                strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', QTY_1, 'GATE', QTY_1, 0)) AS WB" + "\n");
                strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'MOLD', QTY_1, 'CURE', QTY_1, 0)) AS MOLD" + "\n");
                strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'M/K', QTY_1, 'TRIM', QTY_1, 'TIN', QTY_1, 'S/B/A', QTY_1, 'SIG', QTY_1, 'AVI', QTY_1, 'V/I', QTY_1, 0)) AS FINISH" + "\n");
                strSqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'HMK2A', 0, 'HMK3A', 0, QTY_1)) AS ETC" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT MAT_ID, LOT.OPER, OPER_GRP_1, QTY_1     " + "\n");
                strSqlString.Append("                                  FROM RWIPLOTSTS LOT" + "\n");
                strSqlString.Append("                                     , MWIPOPRDEF OPR " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND LOT.FACTORY = OPR.FACTORY " + "\n");
                strSqlString.Append("                                   AND LOT.OPER = OPR.OPER " + "\n");
                strSqlString.Append("                                   AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND LOT.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                   AND LOT.LOT_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("                                   AND LOT.MAT_ID LIKE 'SES%'   " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                   AND LOT.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                       ) WIP" + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT RES.RES_STS_2 AS MAT_ID" + "\n");
                strSqlString.Append("                             , SUM(RES.RES_CNT) AS RES_CNT" + "\n");
                strSqlString.Append("                             , SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.85 * RES.RES_CNT, 0))) AS CAPA" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RES_CNT " + "\n");
                strSqlString.Append("                                  FROM MRASRESDEF " + "\n");
                strSqlString.Append("                                 WHERE 1 = 1  " + "\n");
                strSqlString.Append("                                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND RES_CMF_9 = 'Y'" + "\n");
                strSqlString.Append("                                   AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                                   AND RES_TYPE='EQUIPMENT'" + "\n");

                // 2011-07-28-임종우 : D/A 1차 CHIP 만의 CAPA를 구하기 위해 A0400, A0401 공정으로 제한 함 (임태성 요청)
                strSqlString.Append("                                   AND RES_STS_8 IN ('A0400', 'A0401')" + "\n");

                strSqlString.Append("                                 GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7" + "\n");
                strSqlString.Append("                               ) RES" + "\n");
                strSqlString.Append("                             , CRASUPHDEF UPH" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND RES.FACTORY=UPH.FACTORY(+)" + "\n");
                strSqlString.Append("                           AND RES.OPER=UPH.OPER(+)" + "\n");
                strSqlString.Append("                           AND RES.RES_MODEL = UPH.RES_MODEL(+)" + "\n");
                strSqlString.Append("                           AND RES.UPEH_GRP = UPH.UPEH_GRP(+)" + "\n");
                strSqlString.Append("                           AND RES.RES_STS_2 = UPH.MAT_ID(+)" + "\n");
                strSqlString.Append("                           AND RES.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
                strSqlString.Append("                           AND RES.RES_STS_2 LIKE 'SES%'" + "\n");
                strSqlString.Append("                         GROUP BY RES.RES_STS_2" + "\n");
                strSqlString.Append("                       ) RAS" + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT KEY_2, KEY_3, SUM(DATA_2) AS TAT" + "\n");
                strSqlString.Append("                          FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("                         WHERE TABLE_NAME = 'H_RPT_TAT_HANA'" + "\n");
                strSqlString.Append("                           AND KEY_1 <= TO_CHAR(SYSDATE, 'YYYYMMDD')" + "\n");
                strSqlString.Append("                           AND DATA_1 >= TO_CHAR(SYSDATE, 'YYYYMMDD')" + "\n");
                strSqlString.Append("                         GROUP BY KEY_2, KEY_3" + "\n");
                strSqlString.Append("                       ) TAT" + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT FACTORY, MAT_ID ,SUM(PLAN_QTY) AS PLAN_QTY" + "\n");
                strSqlString.Append("                          FROM CWIPPLNDAY" + "\n");
                strSqlString.Append("                         WHERE 1=1 " + "\n");
                strSqlString.Append("                           AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                           AND PLAN_DAY BETWEEN TO_CHAR(SYSDATE, 'YYYYMMDD') AND TO_CHAR(SYSDATE + 2, 'YYYYMMDD')" + "\n");
                strSqlString.Append("                           AND IN_OUT_FLAG = 'OUT'" + "\n");
                strSqlString.Append("                           AND CLASS = 'ASSY'" + "\n");
                strSqlString.Append("                         GROUP BY FACTORY, MAT_ID" + "\n");
                strSqlString.Append("                       ) PLN" + "\n");
                strSqlString.Append("                     , ( " + "\n");
                strSqlString.Append("                        SELECT A.MAT_ID, SUM(A.PLAN_QTY) AS MON_PLAN_QTY " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                  FROM CWIPPLNDAY " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND PLAN_DAY BETWEEN TO_CHAR(TRUNC(SYSDATE, 'MM'), 'YYYYMMDD') AND TO_CHAR(TRUNC(LAST_DAY(SYSDATE)), 'YYYYMMDD')" + "\n");
                strSqlString.Append("                                   AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                   AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                 UNION ALL " + "\n");
                strSqlString.Append("                                SELECT CM_KEY_1 AS FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                     , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                  FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                   AND WORK_DATE BETWEEN TO_CHAR(TRUNC(SYSDATE, 'MM'), 'YYYYMMDD') AND TO_CHAR(TRUNC(SYSDATE, 'IW') - 1, 'YYYYMMDD')" + "\n");
                strSqlString.Append("                                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                 GROUP BY CM_KEY_1, MAT_ID " + "\n");
                strSqlString.Append("                               ) A " + "\n");
                strSqlString.Append("                             , MWIPMATDEF B " + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
                strSqlString.Append("                           AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                           AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                           AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                         GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");                
                strSqlString.Append("                       ) PLN2   " + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT CM_KEY_1 AS FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                             , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS SHP_QTY " + "\n");
                strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                         WHERE 1=1 " + "\n");
                strSqlString.Append("                           AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                           AND WORK_DATE BETWEEN TO_CHAR(TRUNC(SYSDATE, 'MM'), 'YYYYMMDD') AND TO_CHAR(TRUNC(LAST_DAY(SYSDATE)), 'YYYYMMDD')" + "\n");
                strSqlString.Append("                           AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                         GROUP BY CM_KEY_1, MAT_ID  " + "\n");
                strSqlString.Append("                       ) SHP" + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT * " + "\n");
                strSqlString.Append("                          FROM MATRNAMSTS " + "\n");
                strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                           AND ATTR_TYPE = 'MAT_ETC' " + "\n");
                strSqlString.Append("                           AND ATTR_NAME = 'SEC_VERSION'  " + "\n");
                strSqlString.Append("                       ) ATT" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
                strSqlString.Append("                   AND MAT.MAT_ID = RAS.MAT_ID(+)" + "\n");
                strSqlString.Append("                   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
                strSqlString.Append("                   AND MAT.MAT_ID = PLN2.MAT_ID(+)" + "\n");
                strSqlString.Append("                   AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
                strSqlString.Append("                   AND MAT.MAT_ID = ATT.ATTR_KEY(+)" + "\n");
                strSqlString.Append("                   AND MAT.MAT_GRP_1 = TAT.KEY_2(+)" + "\n");
                strSqlString.Append("                   AND MAT.MAT_GRP_3 = TAT.KEY_3(+)   " + "\n");
                strSqlString.Append("                   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND MAT.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("                   AND MAT.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND MAT.MAT_ID LIKE 'SES%'" + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.Append("                   AND MAT.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
                #endregion

                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         WHERE NVL(STOCK,0) + NVL(SAW,0) + NVL(DA,0) + NVL(OPT_WIP,0) + NVL(RES_CNT,0) > 0" + "\n");
                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" ORDER BY MAT_GRP_1, MAT_GRP_3, MAT_GRP_6, MAT_ID" + "\n");

            }
            #endregion

            #region ETC 조회
            else
            {
                strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER" + "\n");
                strSqlString.Append("     , MAT_GRP_3, MAT_GRP_6, PKG_CODE, ATTR_VALUE, ROUND(DAY_PLAN, 0) AS DAY_PLAN, ROUND(IN_PLAN, 0) AS IN_PLAN, OPT_WIP - ETC AS OVER_QTY, STOCK" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN STA_QTY >= (BASE_QTY * " + cdvTime.Text + ") THEN (BASE_QTY * " + cdvTime.Text + ")" + "\n");
                strSqlString.Append("                  ELSE STA_QTY" + "\n");
                strSqlString.Append("             END, 0) H0" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN STA_QTY >= (BASE_QTY * " + cdvTime.Text + " * 2) THEN (BASE_QTY * " + cdvTime.Text + ")" + "\n");
                strSqlString.Append("                  WHEN STA_QTY = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN STA_QTY - (BASE_QTY * " + cdvTime.Text + ") <= 0 THEN 0" + "\n");
                strSqlString.Append("                  ELSE STA_QTY - (BASE_QTY * " + cdvTime.Text + ")" + "\n");
                strSqlString.Append("             END, 0) H1" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN STA_QTY >= (BASE_QTY * " + cdvTime.Text + " * 3) THEN (BASE_QTY * " + cdvTime.Text + ")" + "\n");
                strSqlString.Append("                  WHEN STA_QTY = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN STA_QTY - (BASE_QTY * " + cdvTime.Text + " * 2) <= 0 THEN 0" + "\n");
                strSqlString.Append("                  ELSE STA_QTY - (BASE_QTY * " + cdvTime.Text + " * 2)" + "\n");
                strSqlString.Append("             END, 0) H2     " + "\n");
                strSqlString.Append("     , SAW, DA, WB, MOLD, FINISH, ETC, OPT_WIP, CAPA, RES_CNT" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT MAT_GRP_1, MAT_GRP_3, MAT_GRP_6, PKG_CODE, ATTR_VALUE" + "\n");
                strSqlString.Append("             , STOCK, SAW, DA, WB, MOLD, FINISH, ETC, DA_WIP, PLAN_QTY, DAY_PLAN, REMAIN_QTY" + "\n");
                strSqlString.Append("             , CASE WHEN NVL(STOCK,0) = 0 OR NVL(IN_PLAN,0) = 0 THEN 0 " + "\n");
                strSqlString.Append("                    WHEN NVL(STOCK,0) > NVL(IN_PLAN,0) THEN NVL(IN_PLAN,0) " + "\n");
                strSqlString.Append("                    ELSE NVL(STOCK,0) " + "\n");
                strSqlString.Append("                END AS STA_QTY " + "\n");
                strSqlString.Append("             , IN_PLAN, OPT_WIP * ( " + cboPer.Text + "/ 100) AS OPT_WIP, CAPA, RES_CNT " + "\n");
                strSqlString.Append("             , ROUND(NVL(CASE WHEN NVL(REMAIN_QTY,0) <= 0 THEN 0" + "\n");
                strSqlString.Append("                              ELSE (CASE WHEN NVL(REMAIN_QTY,0) < NVL(CAPA,0) THEN CAPA" + "\n");
                strSqlString.Append("                                         ELSE NVL(REMAIN_QTY,0)" + "\n");
                strSqlString.Append("                                    END)" + "\n");
                strSqlString.Append("                         END,0) " + "\n");
                strSqlString.Append("                      / 24 " + "\n");
                strSqlString.Append("                , 0) AS BASE_QTY" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT MAT_GRP_1, MAT_GRP_3, MAT_GRP_6, PKG_CODE, ATTR_VALUE" + "\n");
                strSqlString.Append("                     , STOCK, SAW, DA, WB, MOLD, FINISH, ETC, DA_WIP, PLAN_QTY " + "\n");
                strSqlString.Append("                     , TAT, DA_TAT" + "\n");
                strSqlString.Append("                     , ((NVL(PLAN_QTY,0) - NVL(SHP_QTY,0)) / (SELECT LAST_DAY(SYSDATE) - SYSDATE LASTDAY FROM DUAL)) AS DAY_PLAN" + "\n");
                strSqlString.Append("                     , (((NVL(PLAN_QTY,0) - NVL(SHP_QTY,0)) / (SELECT LAST_DAY(SYSDATE) - SYSDATE LASTDAY FROM DUAL)) * NVL(DA_TAT, 1)) - NVL(DA_WIP, 0) AS REMAIN_QTY" + "\n");
                strSqlString.Append("                     , NVL(CASE WHEN (((NVL(PLAN_QTY,0) - NVL(SHP_QTY,0)) / (SELECT LAST_DAY(SYSDATE) - SYSDATE LASTDAY FROM DUAL)) * NVL(DA_TAT, 1)) - NVL(DA_WIP, 0) <= 0 THEN 0" + "\n");
                strSqlString.Append("                                ELSE (CASE WHEN (((NVL(PLAN_QTY,0) - NVL(SHP_QTY,0)) / (SELECT LAST_DAY(SYSDATE) - SYSDATE LASTDAY FROM DUAL)) * NVL(DA_TAT, 1)) - NVL(DA_WIP, 0) < NVL(CAPA,0) THEN CAPA" + "\n");
                strSqlString.Append("                                           ELSE (((NVL(PLAN_QTY,0) - NVL(SHP_QTY,0)) / (SELECT LAST_DAY(SYSDATE) - SYSDATE LASTDAY FROM DUAL)) * NVL(DA_TAT, 1)) - NVL(DA_WIP, 0)" + "\n");
                strSqlString.Append("                                      END)" + "\n");
                strSqlString.Append("                           END,0) AS IN_PLAN" + "\n");
                strSqlString.Append("                     , CASE WHEN NVL(CAPA,0) > 0 AND NVL(TAT,0) > 0 THEN CAPA * TAT" + "\n");
                strSqlString.Append("                            WHEN NVL(CAPA,0) > 0 AND NVL(TAT,0) = 0 THEN CAPA" + "\n");
                strSqlString.Append("                            ELSE 0" + "\n");
                strSqlString.Append("                        END AS OPT_WIP" + "\n");
                strSqlString.Append("                     , CAPA, RES_CNT" + "\n");
                strSqlString.Append("                     , (SELECT TRUNC((TO_DATE(TO_CHAR(SYSDATE , 'YYYYMMDD')||'235959', 'YYYYMMDDHH24MISS') - SYSDATE) * 24,0) FROM DUAL) AS REMAIN_TIME" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_3, MAT.MAT_GRP_6, SUBSTR(MAT.MAT_ID, -3) AS PKG_CODE, NVL(ATTR_VALUE, ' ') AS ATTR_VALUE" + "\n");
                strSqlString.Append("                             , SUM(STOCK) AS STOCK, SUM(SAW) AS SAW, SUM(DA) AS DA, SUM(WB) AS WB" + "\n");
                strSqlString.Append("                             , SUM(MOLD) AS MOLD, SUM(FINISH) AS FINISH, SUM(ETC) AS ETC, SUM(DA_WIP) AS DA_WIP, SUM(PLAN_QTY) AS PLAN_QTY" + "\n");
                strSqlString.Append("                             , AVG(TAT) AS TAT, AVG(DA_TAT) AS DA_TAT, SUM(SHP_QTY) AS SHP_QTY, SUM(CAPA) AS CAPA, SUM(RES_CNT) AS RES_CNT" + "\n");
                strSqlString.Append("                          FROM MWIPMATDEF@RPTTOMES MAT" + "\n");
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT MAT_ID" + "\n");
                strSqlString.Append("                                     , SUM(DECODE(OPER_GRP_1, 'HMK2A', QTY_1, 0)) AS STOCK" + "\n");
                strSqlString.Append("                                     , SUM(DECODE(OPER_GRP_1, 'B/G', QTY_1, 'SAW', QTY_1, 0)) AS SAW" + "\n");
                strSqlString.Append("                                     , SUM(DECODE(OPER_GRP_1, 'S/P', QTY_1, 'D/A', QTY_1, 0)) AS DA" + "\n");
                strSqlString.Append("                                     , SUM(DECODE(OPER_GRP_1, 'W/B', QTY_1, 'GATE', QTY_1, 0)) AS WB" + "\n");
                strSqlString.Append("                                     , SUM(DECODE(OPER_GRP_1, 'MOLD', QTY_1, 'CURE', QTY_1, 0)) AS MOLD" + "\n");
                strSqlString.Append("                                     , SUM(DECODE(OPER_GRP_1, 'M/K', QTY_1, 'TRIM', QTY_1, 'TIN', QTY_1, 'S/B/A', QTY_1, 'SIG', QTY_1, 'AVI', QTY_1, 'V/I', QTY_1, 0)) AS FINISH" + "\n");
                strSqlString.Append("                                     , SUM(DECODE(OPER_GRP_1, 'HMK2A', 0, 'HMK3A', 0, QTY_1)) AS ETC" + "\n");
                strSqlString.Append("                                     , SUM(DECODE(OPER_GRP_1, 'B/G', QTY_1, 'SAW', QTY_1, 'S/P', QTY_1, 'D/A', QTY_1, 0)) AS DA_WIP                             " + "\n");
                strSqlString.Append("                                  FROM (" + "\n");
                strSqlString.Append("                                        SELECT MAT_ID, LOT.OPER, OPER_GRP_1, QTY_1 " + "\n");
                strSqlString.Append("                                          FROM RWIPLOTSTS LOT" + "\n");
                strSqlString.Append("                                             , MWIPOPRDEF OPR " + "\n");
                strSqlString.Append("                                         WHERE 1=1 " + "\n");
                strSqlString.Append("                                           AND LOT.FACTORY = OPR.FACTORY " + "\n");
                strSqlString.Append("                                           AND LOT.OPER = OPR.OPER " + "\n");
                strSqlString.Append("                                           AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                           AND LOT.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                           AND LOT.LOT_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("                                           AND LOT.MAT_ID NOT LIKE 'SES%'   " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                           AND LOT.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                                       )" + "\n");
                strSqlString.Append("                                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                               ) WIP" + "\n");
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT RES.RES_STS_2 AS MAT_ID" + "\n");
                strSqlString.Append("                                     , SUM(RES.RES_CNT) AS RES_CNT" + "\n");
                strSqlString.Append("                                     , SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.85 * RES.RES_CNT, 0))) AS CAPA" + "\n");
                strSqlString.Append("                                  FROM (" + "\n");
                strSqlString.Append("                                        SELECT FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RES_CNT " + "\n");
                strSqlString.Append("                                          FROM MRASRESDEF " + "\n");
                strSqlString.Append("                                         WHERE 1 = 1  " + "\n");
                strSqlString.Append("                                           AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                           AND RES_CMF_9 = 'Y'" + "\n");
                strSqlString.Append("                                           AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                                           AND RES_TYPE='EQUIPMENT'" + "\n");

                // 2011-07-28-임종우 : D/A 1차 CHIP 만의 CAPA를 구하기 위해 A0400, A0401 공정으로 제한 함 (임태성 요청)
                strSqlString.Append("                                           AND RES_STS_8 IN ('A0400', 'A0401')" + "\n");

                strSqlString.Append("                                         GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7" + "\n");
                strSqlString.Append("                                       ) RES" + "\n");
                strSqlString.Append("                                     , CRASUPHDEF UPH" + "\n");
                strSqlString.Append("                                 WHERE 1=1" + "\n");
                strSqlString.Append("                                   AND RES.FACTORY=UPH.FACTORY(+)" + "\n");
                strSqlString.Append("                                   AND RES.OPER=UPH.OPER(+)" + "\n");
                strSqlString.Append("                                   AND RES.RES_MODEL = UPH.RES_MODEL(+)" + "\n");
                strSqlString.Append("                                   AND RES.UPEH_GRP = UPH.UPEH_GRP(+)" + "\n");
                strSqlString.Append("                                   AND RES.RES_STS_2 = UPH.MAT_ID(+)" + "\n");
                strSqlString.Append("                                   AND RES.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
                strSqlString.Append("                                   AND RES.RES_STS_2 NOT LIKE 'SES%'" + "\n");
                strSqlString.Append("                                 GROUP BY RES.RES_STS_2" + "\n");
                strSqlString.Append("                               ) RAS" + "\n");
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT KEY_2, KEY_3, SUM(DATA_2) AS TAT" + "\n");
                strSqlString.Append("                                     , SUM(DECODE(KEY_4, 'SAW', DATA_2, 'D/A', DATA_2, 0)) AS DA_TAT" + "\n");
                strSqlString.Append("                                  FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("                                 WHERE TABLE_NAME = 'H_RPT_TAT_HANA'" + "\n");
                strSqlString.Append("                                   AND KEY_1 <= TO_CHAR(SYSDATE, 'YYYYMMDD')" + "\n");
                strSqlString.Append("                                   AND DATA_1 >= TO_CHAR(SYSDATE, 'YYYYMMDD')" + "\n");
                strSqlString.Append("                                 GROUP BY KEY_2, KEY_3" + "\n");
                strSqlString.Append("                               ) TAT" + "\n");
                strSqlString.Append("                             , ( " + "\n");
                strSqlString.Append("                                SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY AS PLAN_QTY, PLAN_MONTH " + "\n");
                strSqlString.Append("                                  FROM ( " + "\n");
                strSqlString.Append("                                        SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
                strSqlString.Append("                                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                                         WHERE 1=1 " + "\n");
                strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                           AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                              SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                                FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                               WHERE 1=1 " + "\n");
                strSqlString.Append("                                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                                             ) " + "\n");
                strSqlString.Append("                                         GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");
                strSqlString.Append("                                         UNION ALL " + "\n");
                strSqlString.Append("                                        SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , TO_CHAR(SYSDATE, 'YYYYMM') AS PLAN_MONTH " + "\n");
                strSqlString.Append("                                          FROM ( " + "\n");
                strSqlString.Append("                                                SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                                  FROM CWIPPLNDAY " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                                   AND PLAN_DAY BETWEEN TO_CHAR(TRUNC(SYSDATE, 'MM'), 'YYYYMMDD') AND TO_CHAR(TRUNC(LAST_DAY(SYSDATE)), 'YYYYMMDD')" + "\n");
                strSqlString.Append("                                                   AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                                   AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                                 GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                                 UNION ALL " + "\n");
                strSqlString.Append("                                                SELECT CM_KEY_1 AS FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                                     , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                                  FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                                   AND WORK_DATE BETWEEN TO_CHAR(TRUNC(SYSDATE, 'MM'), 'YYYYMMDD') AND TO_CHAR(TRUNC(SYSDATE, 'IW') - 1, 'YYYYMMDD')" + "\n");
                strSqlString.Append("                                                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                                   AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                                   AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                                 GROUP BY CM_KEY_1, MAT_ID " + "\n");
                strSqlString.Append("                                               ) A " + "\n");
                strSqlString.Append("                                               , MWIPMATDEF B " + "\n");
                strSqlString.Append("                                           WHERE 1=1  " + "\n");
                strSqlString.Append("                                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                           GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                                       ) " + "\n");
                strSqlString.Append("                                 WHERE PLAN_MONTH = TO_CHAR(SYSDATE, 'YYYYMM')" + "\n");
                strSqlString.Append("                                   AND PLAN_QTY_ASSY <> 0" + "\n");
                strSqlString.Append("                               ) PLN   " + "\n");
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT CM_KEY_1 AS FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                     , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS SHP_QTY " + "\n");
                strSqlString.Append("                                  FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                   AND WORK_DATE BETWEEN TO_CHAR(TRUNC(SYSDATE, 'MM'), 'YYYYMMDD') AND TO_CHAR(TRUNC(LAST_DAY(SYSDATE)), 'YYYYMMDD')" + "\n");
                strSqlString.Append("                                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                                 GROUP BY CM_KEY_1, MAT_ID  " + "\n");
                strSqlString.Append("                               ) SHP" + "\n");
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT * " + "\n");
                strSqlString.Append("                                  FROM MATRNAMSTS " + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND ATTR_TYPE = 'MAT_ETC' " + "\n");
                strSqlString.Append("                                   AND ATTR_NAME = 'SEC_VERSION'  " + "\n");
                strSqlString.Append("                               ) ATT" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = RAS.MAT_ID(+)" + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = ATT.ATTR_KEY(+)" + "\n");
                strSqlString.Append("                           AND MAT.MAT_GRP_1 = TAT.KEY_2(+)" + "\n");
                strSqlString.Append("                           AND MAT.MAT_GRP_3 = TAT.KEY_3(+)   " + "\n");
                strSqlString.Append("                           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                           AND MAT.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("                           AND MAT.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID NOT LIKE 'SES%'" + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.Append("                           AND MAT.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("                         GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_3, MAT.MAT_GRP_6, SUBSTR(MAT.MAT_ID, -3), NVL(ATTR_VALUE, ' ')" + "\n");
                strSqlString.Append("                        HAVING SUM(NVL(STOCK,0)) + SUM(NVL(ETC,0)) + SUM(NVL(PLAN_QTY,0)) + SUM(NVL(SHP_QTY,0)) > 0" + "\n");
                strSqlString.Append("                       )                 " + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         WHERE NVL(STOCK,0) + NVL(ETC,0) + NVL(IN_PLAN,0) + NVL(OPT_WIP,0) + NVL(CAPA,0) + NVL(RES_CNT,0) > 0" + "\n");
                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" ORDER BY MAT_GRP_1, MAT_GRP_3, MAT_GRP_6, PKG_CODE, ATTR_VALUE" + "\n");
            }

            #endregion

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
                                
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 2, 5, null, null, btnSort);
                
                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);


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



        #region H+0 Cell을 클릭 했을 경우의 팝업창
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //string stType = spdData.ActiveSheet.Cells[e.Row, e.Column].Column.Label.ToString();
                        
            string stH0 = null;
            string[] dataArry = new string[4];

            // H+0 PLAN 이면서 Null 값이 아닌경우 클릭 시 팝업 창 띄움.
            if (e.Column == 9 && spdData.ActiveSheet.Cells[e.Row, e.Column].Text != "")
            {
                Color BackColor = spdData.ActiveSheet.Cells[1, 9].BackColor;

                // GrandTotal 과 SubTotal 제외하기 위해
                if (e.Row != 0 && spdData.ActiveSheet.Cells[e.Row, 9].BackColor == BackColor)
                {
                    // Group 정보 가져와서 담기... 상세 팝업에서 조건값으로 사용하기 위해
                    for (int i = 0; i < 4; i++)
                    {
                        dataArry[i] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                    }

                    // 고객사 명을 고객사 코드로 변경하기 위해..
                    if (dataArry[0] != " ")
                    {
                        DataTable dtCustomer = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlCustomer(dataArry[0].ToString()));

                        dataArry[0] = dtCustomer.Rows[0][0].ToString();
                    }


                    //stMatID = spdData.ActiveSheet.Cells[e.Row, 3].Value.ToString();
                    stH0 = spdData.ActiveSheet.Cells[e.Row, 9].Value.ToString();

                    DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(dataArry, stH0));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        System.Windows.Forms.Form frm = new PRD010904_P1("", dt);
                        frm.ShowDialog();
                    }
                }
            }
            else
            {
                return;
            }
        }

        #region MakeSqlCustomer
        //고객사 명으로 고객사 코드 가져오기 위해..
        private string MakeSqlCustomer(string customerName)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND DATA_1 = '" + customerName + "' AND ROWNUM=1" + "\n");

            return strSqlString.ToString();

        }
        #endregion

        private string MakeSqlDetail(string[] dataArry, string stH0)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT MAT_ID, LOT_ID, LOT_CMF_4, LOT_CMF_5, QTY_1, QTY_2, LOT_PRIORITY, CREATE_TIME" + "\n");
            strSqlString.AppendFormat("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT RNK, MAT_ID, LOT_ID, LOT_CMF_4, LOT_CMF_5, QTY_1, QTY_2, LOT_PRIORITY, CREATE_TIME, SUM_QTY" + "\n");
            strSqlString.AppendFormat("             , SUM(CHECKD) OVER(ORDER BY SUM_QTY ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS LIMIT" + "\n");
            strSqlString.AppendFormat("          FROM ( " + "\n");
            strSqlString.AppendFormat("                SELECT RNK, MAT_ID, LOT_ID, LOT_CMF_4, LOT_CMF_5, QTY_1, QTY_2, LOT_PRIORITY, CREATE_TIME" + "\n");
            strSqlString.AppendFormat("                     , SUM(QTY_1) OVER(ORDER BY RNK ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SUM_QTY" + "\n");
            strSqlString.AppendFormat("                     , CASE WHEN SUM(QTY_1) OVER(ORDER BY RNK ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) - " + stH0 + " >= 0 THEN 1 ELSE 0 END AS CHECKD " + "\n");
            strSqlString.AppendFormat("                  FROM ( " + "\n");
            strSqlString.AppendFormat("                        SELECT A.MAT_ID, A.LOT_ID, A.LOT_CMF_4, A.LOT_CMF_5, A.QTY_1, A.QTY_2, A.LOT_PRIORITY, A.CREATE_TIME " + "\n");
            strSqlString.AppendFormat("                             , ROW_NUMBER() OVER(ORDER BY A.LOT_PRIORITY, A.CREATE_TIME) AS RNK " + "\n");
            strSqlString.AppendFormat("                          FROM RWIPLOTSTS A" + "\n");
            strSqlString.AppendFormat("                             , MWIPMATDEF B" + "\n");
            strSqlString.AppendFormat("                         WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("                           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.AppendFormat("                           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.AppendFormat("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                           AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.AppendFormat("                           AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("                           AND A.OPER = 'A0000'" + "\n");

            if (rdbSLSI.Checked == true)
            {
                strSqlString.AppendFormat("                           AND A.MAT_ID = '" + dataArry[3] + "'" + "\n");
            }
            else
            {
                if (dataArry[0] != " ")
                    strSqlString.AppendFormat("                           AND B.MAT_GRP_1 = '" + dataArry[0] + "'" + "\n");

                if (dataArry[1] != " ")
                    strSqlString.AppendFormat("                           AND B.MAT_GRP_3 = '" + dataArry[1] + "'" + "\n");

                if (dataArry[2] != " ")
                    strSqlString.AppendFormat("                           AND B.MAT_GRP_6 = '" + dataArry[2] + "'" + "\n");

                if (dataArry[3] != " ")
                    strSqlString.AppendFormat("                           AND SUBSTR(B.MAT_ID, -3) = '" + dataArry[3] + "'" + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.AppendFormat("                       ) " + "\n");
            strSqlString.AppendFormat("               ) " + "\n");
            strSqlString.AppendFormat("       )" + "\n");
            strSqlString.AppendFormat(" WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("   AND LIMIT <= 1" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion
    }
}
