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
    /// <summary>
    /// 클  래  스: PRD010219<br/>
    /// 클래스요약: SAW 작업지시서<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2013-06-21<br/>
    /// 상세  설명: SAW 작업지시서 (이영찬 요청)<br/>
    /// 2013-07-09-임종우 : HOLD 재공 제외(단 H15%는 포함), FABLESS & COB & BGN 은 SAW TAT TIME 반영 안 함 (이영찬 요청)
    /// 2013-08-26-임종우 : 계획 테이블 변경 CWIPPLNWEK_N -> RWIPPLNWEK
    /// 2013-11-07-임종우 : TAT 부분 제수 0 에러 수정
    /// </summary>
    public partial class PRD010219 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PRD010219 : Program Initial "

        // 전역변수 선언
        string sToday = string.Empty;
        string sThisWeek = string.Empty;
        string sNextWeek = string.Empty;        

        public PRD010219()
        {
            InitializeComponent();            
            SortInit();
            GridColumnInit();
            GridColumnInit2();
            GridColumnInit3();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #endregion


        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            try
            {
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("Operational PKG List", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("PKG", 1, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("a daily goal", 1, 1, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("a daily progress", 1, 2, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("L/N ~ BG WIP", 1, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("SAW WIP", 1, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Required number", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 0, 6);

                spdData.RPT_AddBasicColumn("Blade list", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Z1", 1, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Z2", 1, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 6, 2);

                spdData.RPT_AddBasicColumn("Standard Goal", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("UPEH", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);

                // Group항목이 있을경우 반드시 선언해줄것.
                //spdData.RPT_ColumnConfigFromTable(btnSort);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        private void GridColumnInit2()
        {
            try
            {
                spdData2.RPT_ColumnInit();

                spdData2.RPT_AddBasicColumn("Blade list", 0, 0, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData2.RPT_AddBasicColumn("Z1", 1, 0, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData2.RPT_AddBasicColumn("Z2", 1, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData2.RPT_MerageHeaderColumnSpan(0, 0, 2);

                spdData2.RPT_AddBasicColumn("Number of possession", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData2.RPT_AddBasicColumn("Required number", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData2.RPT_AddBasicColumn("spare count", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);

                spdData2.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData2.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData2.RPT_MerageHeaderRowSpan(0, 4, 2);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        private void GridColumnInit3()
        {
            try
            {
                spdData3.RPT_ColumnInit();

                spdData3.RPT_AddBasicColumn("DOWN Equipment List", 0, 0, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData3.RPT_AddBasicColumn("Equipment", 1, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData3.RPT_AddBasicColumn("Equipment type", 1, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 90);
                spdData3.RPT_AddBasicColumn("Z1", 1, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData3.RPT_AddBasicColumn("Z2", 1, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData3.RPT_AddBasicColumn("BD usage", 1, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData3.RPT_AddBasicColumn("Down time", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData3.RPT_MerageHeaderColumnSpan(0, 0, 6);

                ;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }
        #endregion


        #region " SortInit : Group By 설정 "

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).Clear();
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Vendor description", "VENDOR", "VENDOR", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material classification", "MAT_TYPE", "MAT_TYPE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "MODEL", "MODEL", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Standard", "MAT_DESC", "MAT_DESC", false);
        }

        #endregion


        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
                       
            // 쿼리
            strSqlString.Append("SELECT A.PART" + "\n");
            strSqlString.Append("     , B.DAY_TARGET " + "\n");
            strSqlString.Append("     , ROUND(A.STD_PLAN / B.DAY_TARGET * 100,0) AS JINDO" + "\n");
            strSqlString.Append("     , A.LN_BG" + "\n");
            strSqlString.Append("     , A.SAW" + "\n");
            strSqlString.Append("     , ROUND(A.NEED_RAS, 1) AS NEED_RAS" + "\n");
            strSqlString.Append("     , A.Z1_NORM" + "\n");
            strSqlString.Append("     , A.Z2_NORM" + "\n");
            strSqlString.Append("     , A.STD_PLAN" + "\n");
            strSqlString.Append("     , A.UPEH" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                    ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("               END AS PART" + "\n");
            strSqlString.Append("             , SUM(WIP.LN_BG) AS LN_BG, SUM(WIP.SAW) AS SAW" + "\n");
            strSqlString.Append("             , SUM(PLN.STD_PLAN) AS STD_PLAN, Z1_NORM, Z2_NORM, SUM(CNT) AS CNT" + "\n");
            strSqlString.Append("             , SUM(UPH.UPEH) AS UPEH   " + "\n");
            strSqlString.Append("             , SUM(((LN_BG/2) + SAW) / UPEH / 24 / CASE WHEN MAT_GRP_1 <> 'SE' THEN 1" + "\n");
            strSqlString.Append("                                                   WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN 1" + "\n");
            strSqlString.Append("                                                   ELSE DECODE(TAT, 0, 4.5, TAT)" + "\n");
            strSqlString.Append("                                              END) AS NEED_RAS" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0020' AND 'A0080' THEN QTY_1 ELSE 0 END) AS LN_BG" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0100' AND 'A0200' THEN QTY_1 ELSE 0 END) AS SAW" + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS" + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND OPER BETWEEN 'A0020' AND 'A0200'" + "\n");
            strSqlString.Append("                   AND OPER <> 'A0180'" + "\n");
            strSqlString.Append("                   AND LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                   AND (HOLD_CODE = ' ' OR HOLD_CODE LIKE 'H15%')" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) WIP" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT DISTINCT CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MAT_ID END AS MAT_ID" + "\n");
            strSqlString.Append("                     , ROUND(PLAN_QTY / 7, 0) AS STD_PLAN" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , CASE WHEN TO_CHAR(TO_DATE('" + sToday + "', 'YYYYMMDD'), 'D') IN ('3','4','5','6') THEN W2_QTY" + "\n");
            strSqlString.Append("                                    ELSE W1_QTY" + "\n");
            strSqlString.Append("                               END PLAN_QTY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + sThisWeek + "', WW_QTY, 0)) AS W1_QTY " + "\n");
            strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + sNextWeek + "', WW_QTY, 0)) AS W2_QTY " + "\n");
            strSqlString.Append("                                  FROM RWIPPLNWEK" + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND GUBUN = '3' " + "\n");
            strSqlString.Append("                                   AND PLAN_WEEK IN ('" + sThisWeek + "','" + sNextWeek + "')" + "\n");            
            strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                               ) A     " + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND W1_QTY + W2_QTY > 0" + "\n");
            strSqlString.Append("                       ) A" + "\n");
            strSqlString.Append("                     , MESMGR.HRTDMCPROUT@RPTTOMES B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_KEY(+)  " + "\n");
            strSqlString.Append("               ) PLN" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT PARTNUMBER AS MAT_ID" + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN RNK = 1 THEN MATCODE END) AS Z1_MATCODE          " + "\n");
            strSqlString.Append("                     , CASE WHEN MAX(CASE WHEN RNK = 2 THEN MATCODE END) IS NULL THEN MAX(CASE WHEN RNK = 1 THEN MATCODE END)" + "\n");
            strSqlString.Append("                            ELSE MAX(CASE WHEN RNK = 2 THEN MATCODE END)" + "\n");
            strSqlString.Append("                       END Z2_MATCODE" + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN RNK = 1 THEN NORM END) AS Z1_NORM     " + "\n");
            strSqlString.Append("                     , CASE WHEN MAX(CASE WHEN RNK = 2 THEN MATCODE END) IS NULL THEN MAX(CASE WHEN RNK = 1 THEN NORM END)" + "\n");
            strSqlString.Append("                            ELSE MAX(CASE WHEN RNK = 2 THEN NORM END)" + "\n");
            strSqlString.Append("                       END Z2_NORM" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT A.PARTNUMBER, A.MATCODE, B.NORM, B.MODEL" + "\n");
            strSqlString.Append("                             , ROW_NUMBER() OVER(PARTITION BY PARTNUMBER ORDER BY MODEL DESC, MATCODE DESC) AS RNK" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT PARTNUMBER, MATCODE, DESCRIPT, CREATE_DT" + "\n");
            strSqlString.Append("                                     , ROW_NUMBER() OVER(PARTITION BY PARTNUMBER ORDER BY CREATE_DT DESC, MATCODE DESC) AS RNK" + "\n");
            strSqlString.Append("                                  FROM CWIPBOMDEF" + "\n");
            strSqlString.Append("                                 WHERE 1=1                           " + "\n");
            strSqlString.Append("                                   AND RESV_FIELD_2 = 'BD'" + "\n");
            strSqlString.Append("                                   AND STEPID = 'A0200'" + "\n");
            strSqlString.Append("                                   AND RESV_FLAG_1 = 'Y'" + "\n");
            strSqlString.Append("                                   AND DELFLAG = ' '   " + "\n");
            strSqlString.Append("                               ) A" + "\n");
            strSqlString.Append("                             , CWIPMATDEF@RPTTOMES B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.MATCODE = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.RNK < 3" + "\n");
            strSqlString.Append("                           AND B.FACTORY(+) = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY PARTNUMBER" + "\n");
            strSqlString.Append("               ) BLA" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT FACTORY, RES_STS_2 AS MAT_ID, COUNT(*) AS CNT" + "\n");
            strSqlString.Append("                  FROM MRASRESDEF " + "\n");
            strSqlString.Append("                 WHERE 1 = 1  " + "\n");
            strSqlString.Append("                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND RES_STS_8 LIKE 'A0200'" + "\n");
            strSqlString.Append("                 GROUP BY FACTORY,RES_STS_2" + "\n");
            strSqlString.Append("               ) RAS" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID, UPEH" + "\n");
            strSqlString.Append("                  FROM CRASUPHDEF" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND OPER = 'A0200'" + "\n");
            strSqlString.Append("                   AND RES_MODEL = 'DFD-6361'" + "\n");
            strSqlString.Append("                   AND UPEH_GRP = 'B'" + "\n");
            strSqlString.Append("               ) UPH" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.*, SAP_CODE, OPER, NVL(TAT_DAY,0) + NVL(TAT_DAY_WAIT,0) AS TAT" + "\n");
            strSqlString.Append("                     , (SELECT OPER FROM MWIPFLWOPR@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OPER = 'A0200' AND FLOW = A.FIRST_FLOW) AS CHK" + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                     , MESMGR.CWIPSAPTAT@RPTTOMES B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("                   AND A.VENDOR_ID = B.SAP_CODE(+)" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                   AND RESV_FIELD_1(+) = ' '  " + "\n");
            strSqlString.Append("                   AND OPER(+) = 'A0200'" + "\n");
            strSqlString.Append("               ) MAT     " + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND WIP.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("           AND WIP.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND WIP.MAT_ID = BLA.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND WIP.MAT_ID = RAS.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND WIP.MAT_ID = UPH.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND MAT.CHK IS NOT NULL" + "\n");            
            strSqlString.Append("         GROUP BY CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5  " + "\n");
            strSqlString.Append("                       ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5   " + "\n");
            strSqlString.Append("                  END, Z1_NORM, Z2_NORM " + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT RESV_FIELD_2 AS PART" + "\n");            
            strSqlString.Append("             , CASE WHEN MAX(SAW) = 0 THEN MIN(SAW) ELSE MAX(SAW) END AS DAY_TARGET" + "\n");            
            strSqlString.Append("          FROM RSUMOPRREM" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND WORK_DATE = '" + sToday + "'" + "\n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND RESV_FIELD_1 = 'TARGET'" + "\n");
            strSqlString.Append("           AND SAW > 0" + "\n");
            strSqlString.Append("         GROUP BY RESV_FIELD_2" + "\n");
            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.PART = B.PART(+)" + "\n");
            strSqlString.Append(" ORDER BY JINDO, DAY_TARGET, STD_PLAN, PART" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlString2()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT Z1_NORM, Z2_NORM, CNT, NEED_RAS" + "\n");
            strSqlString.Append("     , CNT - NEED_RAS AS DEF" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT Z1_NORM, Z2_NORM     " + "\n");
            strSqlString.Append("             , SUM(CNT) AS CNT     " + "\n");
            strSqlString.Append("             , ROUND(SUM(((LN_BG/2) + SAW) / UPEH / 24 / CASE WHEN MAT_GRP_1 <> 'SE' THEN 1" + "\n");
            strSqlString.Append("                                                              WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN 1" + "\n");
            strSqlString.Append("                                                              ELSE DECODE(TAT, 0, 4.5, TAT)" + "\n");
            strSqlString.Append("                                                         END), 1) AS NEED_RAS" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(LN_BG) AS LN_BG" + "\n");
            strSqlString.Append("                     , SUM(SAW) AS SAW" + "\n");
            strSqlString.Append("                     , SUM(CNT) AS CNT" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0020' AND 'A0080' THEN QTY_1 ELSE 0 END) AS LN_BG" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0100' AND 'A0200' THEN QTY_1 ELSE 0 END) AS SAW" + "\n");
            strSqlString.Append("                             , 0 AS CNT" + "\n");
            strSqlString.Append("                          FROM RWIPLOTSTS" + "\n");
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND OPER BETWEEN 'A0020' AND 'A0200'" + "\n");
            strSqlString.Append("                           AND OPER <> 'A0180'" + "\n");
            strSqlString.Append("                           AND LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                           AND (HOLD_CODE = ' ' OR HOLD_CODE LIKE 'H15%')" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                         UNION ALL " + "\n");
            strSqlString.Append("                        SELECT RES_STS_2 AS MAT_ID" + "\n");
            strSqlString.Append("                             , 0" + "\n");
            strSqlString.Append("                             , 0" + "\n");
            strSqlString.Append("                             , COUNT(*) AS CNT" + "\n");
            strSqlString.Append("                          FROM MRASRESDEF " + "\n");
            strSqlString.Append("                         WHERE 1 = 1  " + "\n");
            strSqlString.Append("                           AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                           AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                           AND RES_STS_8 LIKE 'A0200'" + "\n");
            strSqlString.Append("                         GROUP BY FACTORY,RES_STS_2" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) WIP             " + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT PARTNUMBER AS MAT_ID" + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN RNK = 1 THEN MATCODE END) AS Z1_MATCODE          " + "\n");
            strSqlString.Append("                     , CASE WHEN MAX(CASE WHEN RNK = 2 THEN MATCODE END) IS NULL THEN MAX(CASE WHEN RNK = 1 THEN MATCODE END)" + "\n");
            strSqlString.Append("                            ELSE MAX(CASE WHEN RNK = 2 THEN MATCODE END)" + "\n");
            strSqlString.Append("                       END Z2_MATCODE" + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN RNK = 1 THEN NORM END) AS Z1_NORM     " + "\n");
            strSqlString.Append("                     , CASE WHEN MAX(CASE WHEN RNK = 2 THEN MATCODE END) IS NULL THEN MAX(CASE WHEN RNK = 1 THEN NORM END)" + "\n");
            strSqlString.Append("                            ELSE MAX(CASE WHEN RNK = 2 THEN NORM END)" + "\n");
            strSqlString.Append("                       END Z2_NORM" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT A.PARTNUMBER, A.MATCODE, B.NORM, B.MODEL" + "\n");
            strSqlString.Append("                             , ROW_NUMBER() OVER(PARTITION BY PARTNUMBER ORDER BY MODEL DESC, MATCODE DESC) AS RNK" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT PARTNUMBER, MATCODE, DESCRIPT, CREATE_DT" + "\n");
            strSqlString.Append("                                     , ROW_NUMBER() OVER(PARTITION BY PARTNUMBER ORDER BY CREATE_DT DESC, MATCODE DESC) AS RNK" + "\n");
            strSqlString.Append("                                  FROM CWIPBOMDEF" + "\n");
            strSqlString.Append("                                 WHERE 1=1                           " + "\n");
            strSqlString.Append("                                   AND RESV_FIELD_2 = 'BD'" + "\n");
            strSqlString.Append("                                   AND STEPID = 'A0200'" + "\n");
            strSqlString.Append("                                   AND RESV_FLAG_1 = 'Y'" + "\n");
            strSqlString.Append("                                   AND DELFLAG = ' '   " + "\n");
            strSqlString.Append("                               ) A" + "\n");
            strSqlString.Append("                             , CWIPMATDEF@RPTTOMES B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.MATCODE = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.RNK < 3" + "\n");
            strSqlString.Append("                           AND B.FACTORY(+) = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY PARTNUMBER" + "\n");
            strSqlString.Append("               ) BLA" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID, UPEH" + "\n");
            strSqlString.Append("                  FROM CRASUPHDEF" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND OPER = 'A0200'" + "\n");
            strSqlString.Append("                   AND RES_MODEL = 'DFD-6361'" + "\n");
            strSqlString.Append("                   AND UPEH_GRP = 'B'" + "\n");
            strSqlString.Append("               ) UPH" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.*, SAP_CODE, OPER, NVL(TAT_DAY,0) + NVL(TAT_DAY_WAIT,0) AS TAT" + "\n");
            strSqlString.Append("                     , (SELECT OPER FROM MWIPFLWOPR@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OPER = 'A0200' AND FLOW = A.FIRST_FLOW) AS CHK" + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                     , MESMGR.CWIPSAPTAT@RPTTOMES B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("                   AND A.VENDOR_ID = B.SAP_CODE(+)" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                   AND RESV_FIELD_1(+) = ' '  " + "\n");
            strSqlString.Append("                   AND OPER(+) = 'A0200'" + "\n");
            strSqlString.Append("               ) MAT     " + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND WIP.MAT_ID = MAT.MAT_ID           " + "\n");
            strSqlString.Append("           AND WIP.MAT_ID = BLA.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND WIP.MAT_ID = UPH.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND MAT.CHK IS NOT NULL" + "\n");
            strSqlString.Append("         GROUP BY Z1_NORM, Z2_NORM" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" ORDER BY Z1_NORM, Z2_NORM" + "\n");

            //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            //{
            //    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            //}

            return strSqlString.ToString();
        }

        private string MakeSqlString3()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT RES_ID, MODEL, Z1_NORM, Z2_NORM, ROUND(USAGE/1000, 0) AS USAGE, DOWN_TIME" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT RES_ID, RES_GRP_6 AS MODEL, RES_STS_2 AS MAT_ID, LAST_DOWN_TIME, ROUND((SYSDATE - TO_DATE(LAST_DOWN_TIME, 'YYYYMMDDHH24MISS')) * 24, 1)  AS DOWN_TIME" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAX(USAGE1) KEEP(DENSE_RANK FIRST ORDER BY EVENT_TIME DESC) AS USAGE" + "\n");
            strSqlString.Append("                  FROM TDA_PROC_USAGE@RPTTOFA" + "\n");
            strSqlString.Append("                 WHERE OPER = 'A0200'" + "\n");
            strSqlString.Append("                   AND RES_ID = A.RES_ID           " + "\n");
            //strSqlString.Append("                   AND P_ZONE_INFO = 'Z1'" + "\n");
            strSqlString.Append("                   AND WORK_DAY BETWEEN TO_CHAR(TO_DATE('" + sToday + "', 'YYYYMMDD')-3, 'YYYYMMDD') AND '" + sToday + "'" + "\n");
            strSqlString.Append("                 GROUP BY RES_ID" + "\n");
            strSqlString.Append("               ) USAGE" + "\n");
            strSqlString.Append("          FROM MRASRESDEF A" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND RES_STS_8 LIKE 'A0200'" + "\n");
            strSqlString.Append("           AND RES_UP_DOWN_FLAG = 'D'" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT PARTNUMBER AS MAT_ID" + "\n");
            strSqlString.Append("             , MAX(CASE WHEN RNK = 1 THEN MATCODE END) AS Z1_MATCODE          " + "\n");
            strSqlString.Append("             , CASE WHEN MAX(CASE WHEN RNK = 2 THEN MATCODE END) IS NULL THEN MAX(CASE WHEN RNK = 1 THEN MATCODE END)" + "\n");
            strSqlString.Append("                    ELSE MAX(CASE WHEN RNK = 2 THEN MATCODE END)" + "\n");
            strSqlString.Append("               END Z2_MATCODE" + "\n");
            strSqlString.Append("             , MAX(CASE WHEN RNK = 1 THEN NORM END) AS Z1_NORM     " + "\n");
            strSqlString.Append("             , CASE WHEN MAX(CASE WHEN RNK = 2 THEN MATCODE END) IS NULL THEN MAX(CASE WHEN RNK = 1 THEN NORM END)" + "\n");
            strSqlString.Append("                    ELSE MAX(CASE WHEN RNK = 2 THEN NORM END)" + "\n");
            strSqlString.Append("               END Z2_NORM" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT A.PARTNUMBER, A.MATCODE, B.NORM, B.MODEL" + "\n");
            strSqlString.Append("                     , ROW_NUMBER() OVER(PARTITION BY PARTNUMBER ORDER BY MODEL DESC, MATCODE DESC) AS RNK" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT PARTNUMBER, MATCODE, DESCRIPT, CREATE_DT" + "\n");
            strSqlString.Append("                             , ROW_NUMBER() OVER(PARTITION BY PARTNUMBER ORDER BY CREATE_DT DESC, MATCODE DESC) AS RNK" + "\n");
            strSqlString.Append("                          FROM CWIPBOMDEF" + "\n");
            strSqlString.Append("                         WHERE 1=1                           " + "\n");
            strSqlString.Append("                           AND RESV_FIELD_2 = 'BD'" + "\n");
            strSqlString.Append("                           AND STEPID = 'A0200'" + "\n");
            strSqlString.Append("                           AND RESV_FLAG_1 = 'Y'" + "\n");
            strSqlString.Append("                           AND DELFLAG = ' '   " + "\n");
            strSqlString.Append("                       ) A" + "\n");
            strSqlString.Append("                     , CWIPMATDEF@RPTTOMES B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.MATCODE = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND A.RNK < 3" + "\n");
            strSqlString.Append("                   AND B.FACTORY(+) = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY PARTNUMBER" + "\n");
            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append(" ORDER BY RES_ID" + "\n");

            //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            //{
            //    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            //}

            return strSqlString.ToString();
        }

        // 금일, 금주, 차주 정보 가져오기
        private string MakeSqlStringDay()
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT SYS_DATE, PLAN_YEAR||LPAD(PLAN_WEEK,2,'0') AS PLAN_WEEK " + "\n");
            sqlString.Append("  FROM MWIPCALDEF" + "\n");
            sqlString.Append(" WHERE CALENDAR_ID = 'OTD'" + "\n");
            sqlString.Append("   AND SYS_DATE IN (TO_CHAR(SYSDATE + 2/24, 'YYYYMMDD'), TO_CHAR(SYSDATE + 7 + 2/24, 'YYYYMMDD'))" + "\n");

            return sqlString.ToString();
        }

        #endregion

                
        #region " Button Event "
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void btnView_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            DataTable dt2 = null;
            DataTable dt3 = null;
            DataTable dtDay = null;

            if (CheckField() == false) return;

            LoadingPopUp.LoadIngPopUpShow(this);

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                this.Refresh();

                GridColumnInit();
                GridColumnInit2();
                GridColumnInit3();
                                           
                dtDay = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringDay());
                sToday = dtDay.Rows[0][0].ToString();
                sThisWeek = dtDay.Rows[0][1].ToString();
                sNextWeek = dtDay.Rows[1][1].ToString();

                // Query문으로 데이터를 검색한다.
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());
                dt3 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString3());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }
                                
                //spdData.isShowZero = true;

                //// Sub Total
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null);

                //// Total
                spdData.RPT_FillDataSelectiveCells("Total", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);

                ////4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                int[] rowType2 = spdData2.RPT_DataBindingWithSubTotal(dt2, 0, 0, 1, null, null);
                spdData2.RPT_FillDataSelectiveCells("Total", 0, 2, 0, 1, true, Align.Center, VerticalAlign.Center);
                spdData2.RPT_AutoFit(false);
                spdData2.ActiveSheet.Columns[1].BackColor = Color.LightYellow;

                int[] rowType3 = spdData3.RPT_DataBindingWithSubTotal(dt3, 0, 0, 1, null, null);
                spdData3.RPT_FillDataSelectiveCells("Total", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);
                spdData3.RPT_AutoFit(false);

                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 2, 0, false);                

                dt.Dispose();
                dt2.Dispose();
                dt3.Dispose();

            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;
            //try
            //{
            //    spdData.ExportExcel();
            //}
            //catch (Exception ex)
            //{
            //    CmnFunction.ShowMsgBox(ex.Message);
            //}
            //finally
            //{
            //    Cursor.Current = Cursors.Default;
            //}

            //ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, " ^ ", " ^ ");
        }

        #endregion


        private void PRD010219_Load(object sender, EventArgs e)
        {
            // 테이블레이아웃 챠트부분 셀 병합            
            tableLayoutPanel1.SetRowSpan(spdData, 2);
        }        
    }
}
