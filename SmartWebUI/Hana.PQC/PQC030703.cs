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

namespace Hana.PQC
{
    /// <summary>
    /// 클  래  스: PQC030703<br/>
    /// 클래스요약: 불량율 조회<br/>
    /// 작  성  자: 하나마이크론 정병주<brPQC030703
    /// 최초작성일: 2009-12-21<br/>
    /// 상세  설명: 불량율 조회<br/>
    /// 2010-03-09-임종우 : 접수 구분 조회 추가'
    /// 2010-06-10-임종우 : 불량수, 불량율(UNIT) 챠트 삭제 및 NCR LRR 및 목표 추가 함 (김행수 요청)
    /// 2011-01-10-임종우 : 공정불량율 Chart에 업체, 제품타입 검색 되도록 수정 함.
    /// 2015-01-21-임종우 : 업체 GCM 정보 H_VENDOR -> VENDOR 정보로 변경
    /// </summary>
    public partial class PQC030703 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PQC030703 : Program Initial "

        public PQC030703()
        {            
            InitializeComponent();
            SortInit();

            udcFromToDate.DaySelector.SelectedValue = "MONTH";
            udcFromToDate.DaySelector.Visible = false;
            udcFromToDate.AutoBindingUserSetting(DateTime.Now.Year + "-01", DateTime.Now.Year + "-12");
            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX2.RPT_1_ChartInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvVendor.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvMatType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvModel.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvDesc.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvRcvType.sFactory = GlobalVariable.gsAssyDefaultFactory;
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
            int j = 4;

            try
            {
                spdData.RPT_ColumnInit();

                string[] strDate = udcFromToDate.getSelectDate();

                spdData.RPT_AddBasicColumn("account", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Material classification", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Model", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Standard", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);

                for (int i = 0; i <= udcFromToDate.SubtractBetweenFromToDate; i++)
                {
                    spdData.RPT_AddBasicColumn("Sample LOT", 0, j, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Sample of UNIT", 0, j + 1, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn(strDate[i], 0, j + 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Defect quantity", 1, j + 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("LOT", 2, j + 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("UNIT", 2, j + 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, j + 2, 2);
                    spdData.RPT_AddBasicColumn("Defective rate(%)", 1, j + 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("LOT", 2, j + 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 60);
                    spdData.RPT_AddBasicColumn("UNIT", 2, j + 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, j + 4, 2);
                    spdData.RPT_MerageHeaderColumnSpan(0, j + 2, 4);

                    j = j + 6;
                }

                spdData.RPT_MerageHeaderRowSpan(0, 0, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 3);
                // Group항목이 있을경우 반드시 선언해줄것.
                spdData.RPT_ColumnConfigFromTable(btnSort);
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
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("account", "VENDOR", "VENDOR", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material classification", "MAT_TYPE", "MAT_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "MODEL", "MODEL", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Standard", "MAT_DESC", "MAT_DESC", false);
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
            string[] strDate = udcFromToDate.getSelectDate();

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            // 쿼리
            strSqlString.Append("SELECT " + QueryCond1 + "\n");

            for (int i = 0; i <= udcFromToDate.SubtractBetweenFromToDate; i++)
            {
                strSqlString.Append("     , SUM(DECODE(QC_DATE,'" + strDate[i] + "',SP_LOT_QTY)) AS SL_QTY" + i + "\n");
                strSqlString.Append("     , SUM(DECODE(QC_DATE,'" + strDate[i] + "',SP_QTY)) AS SU_QTY" + i + "\n");
                strSqlString.Append("     , SUM(DECODE(QC_DATE,'" + strDate[i] + "',LOSS_LOT_QTY)) AS LL_QTY" + i + "\n");
                strSqlString.Append("     , SUM(DECODE(QC_DATE,'" + strDate[i] + "',LOSS_QTY)) AS LU_QTY" + i + "\n");
                strSqlString.Append("     , ROUND((SUM(DECODE(QC_DATE,'" + strDate[i] + "',LOSS_LOT_QTY)) / NVL(SUM(DECODE(QC_DATE,'" + strDate[i] + "',SP_LOT_QTY)),1)) * 100, 2) AS L_PER" + i + "\n");
                strSqlString.Append("     , ROUND((SUM(DECODE(QC_DATE,'" + strDate[i] + "',LOSS_QTY)) / NVL(SUM(DECODE(QC_DATE,'" + strDate[i] + "',SP_QTY)),1)) * 100, 2) AS U_PER" + i + "\n");
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT BAT.IQC_NO" + "\n");
            strSqlString.Append("             , BAT.MAT_ID" + "\n");
            strSqlString.Append("             , MAT.MAT_TYPE" + "\n");
            strSqlString.Append("             , MAT.MAT_DESC" + "\n");
            strSqlString.Append("             , CMAT.MODEL" + "\n");
            strSqlString.Append("             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME IN ('VENDOR', 'H_CUSTOMER') AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND KEY_1 = BAT.VENDOR) AS VENDOR" + "\n");
            strSqlString.Append("             , BAT.FINAL_DECISION" + "\n");
            strSqlString.Append("             , BAT.TOT_QTY_1" + "\n");
            strSqlString.Append("             , BAT.TOT_QTY_2" + "\n");
            strSqlString.Append("             , LOT.SP_LOT_QTY" + "\n");
            strSqlString.Append("             , LOT.SP_QTY" + "\n");
            strSqlString.Append("             , HIS.LOSS_LOT_QTY" + "\n"); 
            strSqlString.Append("             , HIS.LOSS_QTY" + "\n");
            strSqlString.Append("             , GET_WORK_DATE(BAT.CREATE_TIME, 'M') AS QC_DATE" + "\n");
            strSqlString.Append("             , BAT.UPDATE_TIME AS UPDATE_DATE" + "\n");
            strSqlString.Append("          FROM CIQCBATSTS@RPTTOMES BAT" + "\n");
            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("             , CWIPMATDEF@RPTTOMES CMAT " + "\n");
            strSqlString.Append("           , (" + "\n");

            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("               SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS SP_LOT_QTY, SUM(SAMPLE_QTY) AS SP_QTY" + "\n");
                strSqlString.Append("                 FROM CIQCLOTSTS@RPTTOMES" + "\n");
                strSqlString.Append("                WHERE 1=1" + "\n");
                strSqlString.Append("                GROUP BY IQC_NO, MAT_ID" + "\n");
            }
            else
            {
                strSqlString.Append("               SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS SP_LOT_QTY, SUM(SAMPLE_QTY) AS SP_QTY" + "\n");
                strSqlString.Append("                 FROM CIQCEDCHIS@RPTTOMES" + "\n");
                strSqlString.Append("                WHERE 1=1" + "\n");
                strSqlString.Append("                  AND TRAN_CODE = '" + cdvQCType.Text + "'" + "\n");
                strSqlString.Append("                  AND QC_FLAG <> 'X'" + "\n"); // 검사하지 않은 항목은 제회함.
                strSqlString.Append("                GROUP BY IQC_NO, MAT_ID" + "\n");
            }

            strSqlString.Append("             ) LOT" + "\n");
            strSqlString.Append("           , (" + "\n");
            strSqlString.Append("               SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS LOSS_LOT_QTY, SUM(DEFECT_QTY) AS LOSS_QTY" + "\n");
            strSqlString.Append("                 FROM CIQCEDCHIS@RPTTOMES" + "\n");
            strSqlString.Append("                WHERE 1=1" + "\n");
            strSqlString.Append("                  AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");

            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("                  AND TRAN_CODE <> 'Final'" + "\n");
            }
            else
            {
                strSqlString.Append("                  AND TRAN_CODE = '" + cdvQCType.Text + "'" + "\n");
            }

            strSqlString.Append("                  AND DEFECT_QTY > 0" + "\n");
            strSqlString.Append("                GROUP BY IQC_NO, MAT_ID" + "\n");
            strSqlString.Append("             ) HIS" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND BAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("           AND BAT.FACTORY = CMAT.FACTORY(+)" + "\n");
            strSqlString.Append("           AND BAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("           AND BAT.MAT_ID = CMAT.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND BAT.IQC_NO = LOT.IQC_NO(+)" + "\n");
            strSqlString.Append("           AND BAT.IQC_NO = HIS.IQC_NO(+)" + "\n");
            strSqlString.Append("           AND BAT.FINAL_DECISION <> ' '" + "\n"); // 최종판정 된 것 만 가져옴.
            strSqlString.Append("           AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("           AND CMAT.DELETE_FLAG(+) = ' '" + "\n");
            //strSqlString.Append("           AND MAT.MAT_TYPE NOT IN ('GW','SB','FG')" + "\n"); // 성적서 검사는 제외함.

            // 2010-03-09-임종우 : 접수 구분 조회 추가
            if (cdvRcvType.Text != "ALL" && cdvRcvType.Text != "")
            {
                strSqlString.Append("           AND BAT.IQC_TYPE " + cdvRcvType.SelectedValueToQueryString + "\n");
            }

            //strSqlString.Append("           AND BAT.IQC_TYPE = '양산' " + "\n"); // 양산 자재만 조회되게..
            
            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
            {
                strSqlString.Append("           AND MAT.MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");
            }
            // 모델 조회
            if (cdvModel.Text != "ALL" && cdvModel.Text != "")
            {
                strSqlString.Append("           AND CMAT.MODEL(+) " + cdvModel.SelectedValueToQueryString + "\n");
            }

            // 규격 조회
            if (cdvDesc.Text != "ALL" && cdvDesc.Text != "")
            {
                strSqlString.Append("           AND MAT.MAT_DESC " + cdvDesc.SelectedValueToQueryString + "\n");
            }

            // 업체 조회
            if (cdvVendor.Text != "ALL" && cdvVendor.Text != "")
            {
                strSqlString.Append("           AND BAT.VENDOR " + cdvVendor.SelectedValueToQueryString + "\n");
            }

            // 조회
            strSqlString.Append("           AND BAT.CREATE_TIME BETWEEN '" + udcFromToDate.ExactFromDate + "' AND '" + udcFromToDate.ExactToDate + "'" + "\n");

            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
            {
                strSqlString.Append("   AND MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append(" GROUP BY " + QueryCond1 + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond1 + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            return strSqlString.ToString();
        }


        #endregion


        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeChartSqlString()
        {
            // 2010-06-10-임종우 : 불량수, 불량율(UNIT) 챠트 삭제 및 NCR LRR 추가 함 (김행수 요청)
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            // 쿼리
            strSqlString.Append("SELECT C.DATE_1 " + "\n");
            //strSqlString.Append("     , NVL(A.LOSS_LOT_QTY,0) AS \"불량수(LOT)\" " + "\n");
            //strSqlString.Append("     , NVL(A.LOSS_QTY,0) AS \"불량수(UNIT)\" " + "\n");

            // 2010-06-10-임종우 : IQC LRR, NCR LRR 두개의 챠트에 표시

            strSqlString.Append("     , NVL(A.LOSS_LOT_PER,0) AS \"불량율(LRR)\" " + "\n");
            strSqlString.Append("     , NVL(A.DATA_1, 0) AS \"목표\" " + "\n");
            strSqlString.Append("     , NVL(ROUND((B.NCR_QTY/A.TOT_QTY_2) * 100, 2), 0) AS \"불량율(LRR)\" " + "\n");
            strSqlString.Append("     , NVL(B.DATA_1, 0) AS \"목표\" " + "\n");            

            //strSqlString.Append("     , NVL(A.LOSS_PER,0) AS \"불량율(UNIT)\" " + "\n");
            strSqlString.Append("FROM ( " + "\n");
            strSqlString.Append("       SELECT KEY_1 AS DATE_1, TOT_QTY_2, LOSS_LOT_PER, DATA_1" + "\n");
            strSqlString.Append("         FROM ( " + "\n");
            strSqlString.Append("               SELECT DATE_1" + "\n");
            strSqlString.Append("                    , SUM(TOT_QTY_2) AS TOT_QTY_2 " + "\n");
            strSqlString.Append("                    , SUM(TOT_QTY_1) AS TOT_QTY_1 " + "\n");
            strSqlString.Append("                    , SUM(LOSS_LOT_QTY) AS LOSS_LOT_QTY" + "\n");
            strSqlString.Append("                    , SUM(LOSS_QTY) AS LOSS_QTY" + "\n");
            strSqlString.Append("                    , NVL(ROUND((SUM(LOSS_LOT_QTY)/SUM(SP_LOT_QTY)) * 100, 2), 0) AS LOSS_LOT_PER" + "\n");
            strSqlString.Append("                    , NVL(ROUND((SUM(LOSS_QTY)/SUM(SP_QTY)) * 100, 2), 0) AS LOSS_PER" + "\n");
            strSqlString.Append("                 FROM (" + "\n");
            strSqlString.Append("                       SELECT GET_WORK_DATE(CREATE_DATE,'M') AS DATE_1" + "\n");
            strSqlString.Append("                            , MAT_ID, MAT_TYPE, MAT_DESC, MODEL, VENDOR, FINAL_DECISION, TOT_QTY_1, TOT_QTY_2, SP_LOT_QTY, SP_QTY, LOSS_LOT_QTY, LOSS_QTY, CREATE_DATE, UPDATE_DATE" + "\n");
            strSqlString.Append("                         FROM (" + "\n");
            strSqlString.Append("                               SELECT BAT.IQC_NO" + "\n");
            strSqlString.Append("                                    , BAT.MAT_ID" + "\n");
            strSqlString.Append("                                    , MAT.MAT_TYPE" + "\n");
            strSqlString.Append("                                    , MAT.MAT_DESC" + "\n");
            strSqlString.Append("                                    , CMAT.MODEL" + "\n");
            strSqlString.Append("                                    , (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME IN ('VENDOR', 'H_CUSTOMER') AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND KEY_1 = BAT.VENDOR) AS VENDOR" + "\n");
            strSqlString.Append("                                    , BAT.FINAL_DECISION" + "\n");
            strSqlString.Append("                                    , BAT.TOT_QTY_1" + "\n");
            strSqlString.Append("                                    , BAT.TOT_QTY_2" + "\n");
            strSqlString.Append("                                    , LOT.SP_LOT_QTY" + "\n");
            strSqlString.Append("                                    , LOT.SP_QTY" + "\n");
            strSqlString.Append("                                    , HIS.LOSS_LOT_QTY" + "\n");
            strSqlString.Append("                                    , HIS.LOSS_QTY" + "\n");
            strSqlString.Append("                                    , BAT.CREATE_TIME AS CREATE_DATE" + "\n");
            strSqlString.Append("                                    , BAT.UPDATE_TIME AS UPDATE_DATE" + "\n");
            strSqlString.Append("                                 FROM CIQCBATSTS@RPTTOMES BAT" + "\n");
            strSqlString.Append("                                    , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                                    , CWIPMATDEF@RPTTOMES CMAT " + "\n");
            strSqlString.Append("                                    , (" + "\n");
            
            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("                                       SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS SP_LOT_QTY, SUM(SAMPLE_QTY) AS SP_QTY" + "\n");
                strSqlString.Append("                                         FROM CIQCLOTSTS@RPTTOMES" + "\n");
                strSqlString.Append("                                        WHERE 1=1" + "\n");
                strSqlString.Append("                                        GROUP BY IQC_NO, MAT_ID" + "\n");
            }
            else
            {
                strSqlString.Append("                                       SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS SP_LOT_QTY, SUM(SAMPLE_QTY) AS SP_QTY" + "\n");
                strSqlString.Append("                                         FROM CIQCEDCHIS@RPTTOMES" + "\n");
                strSqlString.Append("                                        WHERE 1=1" + "\n");
                strSqlString.Append("                                          AND TRAN_CODE = '" + cdvQCType.Text + "'" + "\n");
                strSqlString.Append("                                          AND QC_FLAG <> 'X'" + "\n"); // 검사하지 않은 항목은 제회함.
                strSqlString.Append("                                        GROUP BY IQC_NO, MAT_ID" + "\n");
            }

            strSqlString.Append("                                      ) LOT" + "\n");
            strSqlString.Append("                                    , (" + "\n");
            strSqlString.Append("                                       SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS LOSS_LOT_QTY, SUM(DEFECT_QTY) AS LOSS_QTY" + "\n");
            strSqlString.Append("                                         FROM CIQCEDCHIS@RPTTOMES" + "\n");
            strSqlString.Append("                                        WHERE 1=1" + "\n");
            strSqlString.Append("                                          AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");

            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("                                          AND TRAN_CODE <> 'Final'" + "\n");
            }
            else
            {
                strSqlString.Append("                                          AND TRAN_CODE = '" + cdvQCType.Text + "'" + "\n");
            }

            strSqlString.Append("                                          AND DEFECT_QTY > 0" + "\n");
            strSqlString.Append("                                        GROUP BY IQC_NO, MAT_ID" + "\n");
            strSqlString.Append("                                      ) HIS" + "\n");
            strSqlString.Append("                                WHERE 1=1" + "\n");
            strSqlString.Append("                                  AND BAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                                  AND BAT.FACTORY = CMAT.FACTORY(+)" + "\n");
            strSqlString.Append("                                  AND BAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                                  AND BAT.MAT_ID = CMAT.MAT_ID(+)" + "\n");
            strSqlString.Append("                                  AND BAT.IQC_NO = LOT.IQC_NO(+)" + "\n");
            strSqlString.Append("                                  AND BAT.IQC_NO = HIS.IQC_NO(+)" + "\n");
            //strSqlString.Append("                        AND BAT.FINAL_DECISION <> ' '" + "\n"); // 최종판정 된 것 만 가져옴.
            strSqlString.Append("                                  AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                  AND CMAT.DELETE_FLAG(+) = ' '" + "\n");
            //strSqlString.Append("                        AND MAT.MAT_TYPE NOT IN ('GW','SB','FG')" + "\n"); // 성적서 검사는 제외함.

            // 2010-03-09-임종우 : 접수 구분 조회 추가
            if (cdvRcvType.Text != "ALL" && cdvRcvType.Text != "")
            {
                strSqlString.Append("                                  AND BAT.IQC_TYPE " + cdvRcvType.SelectedValueToQueryString + "\n");
            }
            
            //strSqlString.Append("                        AND BAT.IQC_TYPE = '양산' " + "\n"); // 양산 자재만 조회되게..
            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
            {
                strSqlString.Append("                                  AND MAT.MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");
            }
            // 모델 조회
            if (cdvModel.Text != "ALL" && cdvModel.Text != "")
            {
                strSqlString.Append("                                  AND CMAT.MODEL(+) " + cdvModel.SelectedValueToQueryString + "\n");
            }

            // 규격 조회
            if (cdvDesc.Text != "ALL" && cdvDesc.Text != "")
            {
                strSqlString.Append("                                  AND MAT.MAT_DESC " + cdvDesc.SelectedValueToQueryString + "\n");
            }

            // 업체 조회
            if (cdvVendor.Text != "ALL" && cdvVendor.Text != "")
            {
                strSqlString.Append("                                  AND BAT.VENDOR " + cdvVendor.SelectedValueToQueryString + "\n");
            }

            // 조회
            strSqlString.Append("                                  AND BAT.CREATE_TIME BETWEEN '" + udcFromToDate.ExactFromDate + "' AND '" + udcFromToDate.ExactToDate + "'" + "\n");            
            strSqlString.Append("                              ) " + "\n");            
            strSqlString.Append("                      )" + "\n");
            strSqlString.Append("                GROUP BY DATE_1 " + "\n");            
            strSqlString.Append("             ) DAT " + "\n");
            strSqlString.Append("           , ( " + "\n");
            strSqlString.Append("              SELECT KEY_1, KEY_2, DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_IQC_NCR_PLAN' AND KEY_1 BETWEEN '" + udcFromToDate.FromYearMonth.Text.Replace("-", "") + "' AND '" + udcFromToDate.ToYearMonth.Text.Replace("-", "") + "' AND KEY_2 = 'IQC' " + "\n");
            strSqlString.Append("             ) PLN" + "\n");
            strSqlString.Append("       WHERE 1=1 " + "\n");
            strSqlString.Append("         AND DAT.DATE_1(+) = PLN.KEY_1 " + "\n");
            strSqlString.Append("     ) A " + "\n");
            strSqlString.Append("   , ( " + "\n");
            strSqlString.Append("      SELECT KEY_1 AS DATE_1, NCR_QTY, DATA_1 " + "\n");
            strSqlString.Append("        FROM ( " + "\n");
            strSqlString.Append("              SELECT GET_WORK_DATE(CHECK_IN_TIME,'M') AS DATE_1 " + "\n");
            strSqlString.Append("                   , COUNT(*) AS NCR_QTY " + "\n");
            strSqlString.Append("                FROM CQCMNCRDAT@RPTTOMES " + "\n");
            strSqlString.Append("               WHERE 1=1 " + " \n");
            strSqlString.Append("                 AND CHECK_IN_TIME BETWEEN '" + udcFromToDate.ExactFromDate + "' AND '" + udcFromToDate.ExactToDate + "'" + "\n");
            strSqlString.Append("                 AND HIST_DELETE_FLAG <> 'Y' " + " \n");
            strSqlString.Append("                 AND NCR_TYPE = '공정불량NCR' " + " \n");
            strSqlString.Append("                 AND MAT_TYPE NOT IN ('AD','PL','SM','TR') " + " \n");

            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
            {
                strSqlString.Append("                 AND MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");
            }

            // 업체 조회
            if (cdvVendor.Text != "ALL" && cdvVendor.Text != "")
            {
                strSqlString.Append("                 AND VENDOR " + cdvVendor.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append("               GROUP BY GET_WORK_DATE(CHECK_IN_TIME,'M')" + " \n");
            strSqlString.Append("             ) DAT " + "\n");
            strSqlString.Append("           , ( " + "\n");
            strSqlString.Append("              SELECT KEY_1, KEY_2, DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_IQC_NCR_PLAN' AND KEY_1 BETWEEN '" + udcFromToDate.FromYearMonth.Text.Replace("-", "") + "' AND '" + udcFromToDate.ToYearMonth.Text.Replace("-", "") + "' AND KEY_2 = 'NCR' " + "\n");
            strSqlString.Append("             ) PLN" + "\n");
            strSqlString.Append("       WHERE 1=1 " + "\n");
            strSqlString.Append("         AND DAT.DATE_1(+) = PLN.KEY_1 " + "\n");
            strSqlString.Append("     ) B " + "\n");
            strSqlString.Append("   , ( " + "\n");
            strSqlString.Append("       SELECT DISTINCT DATA_1 AS DATE_1 " + "\n");
            strSqlString.Append("       FROM ( " + "\n");
            strSqlString.Append("              SELECT SYS_YEAR || CASE WHEN SYS_MONTH < 10 THEN '0' || TO_CHAR(SYS_MONTH) " + "\n");
            strSqlString.Append("                                      ELSE TO_CHAR(SYS_MONTH) " + "\n");
            strSqlString.Append("                                  END AS DATA_1 " + "\n");
            strSqlString.Append("                FROM MWIPCALDEF WHERE SYS_DATE BETWEEN '" + udcFromToDate.ExactFromDate + "' AND '" + udcFromToDate.ExactToDate + "'  \n");
            strSqlString.Append("            ) " + "\n");
            strSqlString.Append("     ) C " + "\n");
            strSqlString.Append(" WHERE A.DATE_1(+) = C.DATE_1 " + "\n");
            strSqlString.Append("   AND B.DATE_1(+) = C.DATE_1 " + "\n");
            strSqlString.Append(" ORDER BY DATE_1" + "\n");



            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            return strSqlString.ToString();
        }


        #endregion

        #region " MakeChart : Chart 처리 "

        /// <summary>
        ///  Chart 생성
        /// </summary>
        private void ShowChart()
        {
            // 차트 설정

            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            udcChartFX2.RPT_1_ChartInit();
            udcChartFX2.RPT_2_ClearData();

            //udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartSqlString());

            if (dt == null || dt.Rows.Count < 1)
                return;

            double cnt = 0;
            double max = 0;
            double cnt1 = 0;
            double max1 = 0;
            int arrCnt = 0;

            int rowCount = dt.Rows.Count;

            udcChartFX1.RPT_3_OpenData(2, rowCount);
            udcChartFX2.RPT_3_OpenData(2, rowCount);

            string[] LegBox = new string[2]; //각각 불량율, 목표 두개 씩만 보여주기에...
            int[] loss_rows = new Int32[rowCount];
            int[] tot_rows = new Int32[rowCount];

            for (int i = 0; i < loss_rows.Length; i++)
            {
                loss_rows[i] = i;
                tot_rows[i] = i;
            }

            // 데이터 테이블의 컬럼헤더를 저장한다. LegBox 표시 값으로 사용하려...
            for (int x = 1; x < 3; x++)
            {
                LegBox[arrCnt] = dt.Columns[x].ToString();

                arrCnt++;
            }

            // IQC
            cnt = udcChartFX1.RPT_4_AddData(dt, tot_rows, new int[] { 1 }, SeriseType.Column);
            max = udcChartFX1.RPT_4_AddData(dt, loss_rows, new int[] { 2 }, SeriseType.Column);

            // NCR
            cnt1 = udcChartFX2.RPT_4_AddData(dt, tot_rows, new int[] { 3 }, SeriseType.Column);
            max1 = udcChartFX2.RPT_4_AddData(dt, loss_rows, new int[] { 4 }, SeriseType.Column);

            if (max > cnt)
            {
                cnt = max;
            }

            max = cnt;

            if (max1 > cnt1)
            {
                cnt1 = max1;
            }

            max1 = cnt1;

            udcChartFX1.RPT_5_CloseData();
            udcChartFX2.RPT_5_CloseData();

            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "", AsixType.Y, DataTypes.Initeger, max * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Line,1, 1, "", AsixType.Y, DataTypes.Initeger, max * 1.2);

            udcChartFX2.RPT_6_SetGallery(ChartType.Bar, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcChartFX2.RPT_6_SetGallery(ChartType.Line, 1, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);
            udcChartFX2.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);

            udcChartFX1.RPT_8_SetSeriseLegend(LegBox, SoftwareFX.ChartFX.Docked.Bottom);
            udcChartFX2.RPT_8_SetSeriseLegend(LegBox, SoftwareFX.ChartFX.Docked.Bottom);

            // 기타 설정
            udcChartFX1.PointLabels = false;
            udcChartFX1.AxisY.LabelsFormat.Decimals = 2;
            udcChartFX1.AxisY.DataFormat.Decimals = 2;
            udcChartFX1.AxisX.LabelAngle = 90;
            udcChartFX1.AxisX.Staggered = false;
            udcChartFX1.Series[1].Color = Color.Red;
            udcChartFX1.Titles[0].Text = "Import inspection defective rate (LRR)";
            
            udcChartFX2.PointLabels = false;
            udcChartFX2.AxisY.LabelsFormat.Decimals = 2;
            udcChartFX2.AxisY.DataFormat.Decimals = 2;
            udcChartFX2.AxisX.LabelAngle = 90;
            udcChartFX2.AxisX.Staggered = false;
            udcChartFX2.Series[1].Color = Color.Red;
            udcChartFX2.Titles[0].Text = "Raw Material Quality Accidents (LRR)";
            
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

            if (CheckField() == false) return;

            LoadingPopUp.LoadIngPopUpShow(this);

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                // 검색중 화면 표시
             //   LoadingPopUp.LoadIngPopUpShow(this);
                int j = 4;
                this.Refresh();

                GridColumnInit();
                udcChartFX1.RPT_1_ChartInit();
                udcChartFX2.RPT_1_ChartInit();

                // Query문으로 데이터를 검색한다.

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;

                //spdData.isShowZero = true;

                //// Sub Total
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 4, null, null);

                ////// Total
                spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center);

                ////4. Column Auto Fit
                spdData.RPT_AutoFit(false);
               // spdData.RPT_ColumnInit();

                for (int i = 0; i <= udcFromToDate.SubtractBetweenFromToDate; i++)
                {
                    // GrandTotal에 백분율 부분 계산
                    double sample_lot = Convert.ToDouble(spdData.ActiveSheet.Cells[0, j].Value); // 샘플랏수
                    double sample_qty = Convert.ToDouble(spdData.ActiveSheet.Cells[0, j + 1].Value); // 샘플수량

                    // 샘플수량이 0이면 나누기 계산을 위해 1로 변환.
                    if (sample_lot == 0)
                    {
                        sample_lot = 1;
                    }

                    if (sample_qty == 0)
                    {
                        sample_qty = 1;
                    }

                    spdData.ActiveSheet.Cells[0, j + 4].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, j + 2].Value) / sample_lot) * 100;
                    spdData.ActiveSheet.Cells[0, j + 5].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, j + 3].Value) / sample_qty) * 100;                  

                    j = j + 6;
                }

                ShowChart();
                //dt.Dispose();

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

            ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, " ^ ", " ^ ");
        }

        #endregion

        private void cdvDesc_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT MAT_DESC Code, '' Data" + "\n";
            strQuery += "  FROM MWIPMATDEF " + "\n";
            strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
            strQuery += "   AND MAT_TYPE <> 'FG' " + "\n";
            strQuery += "   AND MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n";
            strQuery += "   AND DELETE_FLAG = ' ' " + "\n";
            strQuery += "ORDER BY MAT_DESC " + "\n";

            cdvDesc.sDynamicQuery = strQuery;
        }

        private void cdvModel_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT MODEL Code, '' Data" + "\n";
            strQuery += "  FROM ( " + "\n";
            strQuery += "         SELECT CDEF.MODEL " + "\n";            
            strQuery += "           FROM MWIPMATDEF MDEF, CWIPMATDEF@RPTTOMES CDEF" + "\n";
            strQuery += "          WHERE MDEF.FACTORY = CDEF.FACTORY " + "\n";
            strQuery += "            AND MDEF.MAT_ID = CDEF.MAT_ID " + "\n";
            strQuery += "            AND MDEF.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";            
            strQuery += "            AND MDEF.MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n";
            strQuery += "            AND MDEF.DELETE_FLAG = ' ' " + "\n";
            strQuery += "            AND CDEF.DELETE_FLAG = ' ' " + "\n";
            strQuery += "       ) " + "\n";
            strQuery += "ORDER BY MODEL " + "\n";

            cdvModel.sDynamicQuery = strQuery;
        }

        private void PQC030703_Load(object sender, EventArgs e)
        {
            // 테이블레이아웃 챠트부분 셀 병합
            //tableLayoutPanel1.SetColumnSpan(spdData, 2);            
        }

        private void cdvRcvType_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DECODE (ROWNUM, 1, A, 2, B, 3, C) AS Code, '' Data" + "\n";
            strQuery += "  FROM ( " + "\n";
            strQuery += "         SELECT 1 " + "\n";
            strQuery += "           FROM DUAL" + "\n";
            strQuery += "        CONNECT BY LEVEL <=3 " + "\n";
            strQuery += "       ) " + "\n";
            strQuery += "     , ( " + "\n";
            strQuery += "         SELECT '양산' AS A " + "\n";
            strQuery += "              , 'QUAL' AS B " + "\n";
            strQuery += "              , 'ER' AS C " + "\n";
            strQuery += "           FROM DUAL" + "\n";
            strQuery += "       ) " + "\n";

            cdvRcvType.sDynamicQuery = strQuery;
        }

        // 2011-09-21-배수민 : VENDOR와 CUSTOMER 함께 보여주기, GCM테이블이 아닌 쿼리 이용 (QI파트 송희석S 요청)
        // 2011-09-21-배수민 : VENDOR와 CUSTOMER 함께 보여주기, GCM테이블이 아닌 쿼리 이용 (QI파트 송희석S 요청)
        private void cdvVendor_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT VENDOR AS Code " + "\n";
            strQuery += "     , DECODE(ORDER_ID, 'FG', C.DATA_1, B.DATA_1) AS Data " + "\n";
            strQuery += "  FROM CIQCBATSTS A " + "\n";
            strQuery += "     , (SELECT KEY_1, DATA_1 " + "\n";
            strQuery += "          FROM MGCMTBLDAT " + "\n";
            strQuery += "         WHERE TABLE_NAME = 'VENDOR' " + "\n";
            strQuery += "           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' ) B " + "\n";
            strQuery += "     , (SELECT KEY_1, DATA_1 " + "\n";
            strQuery += "          FROM MGCMTBLDAT " + "\n";
            strQuery += "         WHERE TABLE_NAME = 'H_CUSTOMER' " + "\n";
            strQuery += "           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' ) C " + "\n";
            strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
            strQuery += "   AND A.VENDOR = B.KEY_1(+) " + "\n";
            strQuery += "   AND A.VENDOR = C.KEY_1(+)  " + "\n";
            strQuery += "   AND A.CREATE_TIME BETWEEN '" + udcFromToDate.ExactFromDate + "' AND '" + udcFromToDate.ExactToDate + "' " + "\n";
            strQuery += "   AND A.ORDER_ID " + cdvMatType.SelectedValueToQueryString + "\n";
            strQuery += "ORDER BY DECODE(LENGTH(VENDOR), '2', 2, 1) ASC " + "\n";

            cdvVendor.sDynamicQuery = strQuery;

            // 2011-09-21-임종우 : 리스트 초기화 후 다시 담기 위해...약간 이상하지만 예상으로 데이터는 해당 이벤트 종료시점에 담기는 거 같음.
            // 전에 리스트가 한개라도 존재하면 리셋, 한개도 존재하지 않으면 "ALL" 처리
            if (cdvVendor.ValueItems.Count > 0)
            {
                cdvVendor.ResetText();
            }
            else
            {
                // 이 부분 처리 안하면 다음번 부터 데이터 안 들어감.
                cdvVendor.Text = "ALL";
            }
        }


    }
}
