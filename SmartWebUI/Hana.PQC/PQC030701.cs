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

using System.Windows.Forms.DataVisualization.Charting;

namespace Hana.PQC
{
    public partial class PQC030701 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private DataTable dtWeek = null;
        private string strVendor = string.Empty;

        #region " PQC030701 : Program Initial "

        /// <summary>
        /// 클  래  스: PQC030701<br/>
        /// 클래스요약: 수입검사 불량율 집계<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2009-12-10<br/>
        /// 상세  설명: 수입검사 불량율 집계 화면<br/>
        /// 2010-02-25-임종우 : NCR 추가, 탈출율 추가        
        /// 2010-03-09-임종우 : 접수 구분 조회 추가
        /// 2010-03-30-임종우 : Chart 추가 (5 종류)
        /// 2010-04-01-임종우 : 상세 데이터용 POP UP 창 & 수율 부분 추가
        /// 2010-04-09-임종우 : 상세 POP UP창에 검사자 추가
        /// 2010-04-19-임종우 : 성적서 & SKIP 검사 데이터 표시되도록 수정(김행수K 요청)
        /// 2010-04-19-임종우 : 검사 TYPE 선택 되면 해당 이력만 가져오기, 전체이면 성적서 & Skip 검사 모두 가져오기
        /// 2010-06-11-임종우 : LRR Chart 관련 주별 목표값 표시 되도록 수정(김민직 요청)
        /// 2010-06-14-임종우 : 탈출율 건수 표시 없앰(김민직 요청)
        /// 2010-06-17-임종우 : 상세 팝업창에 제품 규격 표시 (김민직 요청)
        /// 2010-11-05-김민우 : CTQ 선정율(%) 표시 (김행수K 요청)
        /// 2011-02-24-임종우 : Wafer 수입검사 데이터 표시 요청 (송희석 요청)
        /// 2011-09-21-배수민 : VENDOR와 CUSTOMER 함께 보여주기, GCM테이블이 아닌 쿼리 이용 (QI파트 송희석S 요청)
        /// 2012-03-02-김민우 : GROUP에 MAT_ID 추가
        /// 2012-03-27-배수민 : NCR, 탈출율 가져오는 기준시간 체크인(CHECK_IN_TIME)기준 -> 불량률 발생시점(NOTICE_TIME)으로 변경 (QI파트 송희석S 요청)
        /// 2015-01-21-임종우 : 업체 GCM 정보 H_VENDOR -> VENDOR 정보로 변경
        /// 2015-03-10-오득연 : ChartFX -> MS Chart로 변경
        /// 2015-08-03-임종우 : 기존 처럼 ASSY 전용으로 고정 함.
        /// 2015-08-05-임종우 : 불량 데이터 가져오는 부분 CIQCEDCHIS -> CIQCLOTSTS 로 변경 (CIQCEDCHIS 에 데이터가 누락 되는 경우 발생...원인파악 필요)
        /// </summary>
        /// 
        public PQC030701()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
           
            udcMSChart1.RPT_1_ChartInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvVendor.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvMatType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvModel.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvDesc.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvRcvType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvChart.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        #endregion


        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvChart.Text == "")
            {
                //원래 메세지는 Chart를 선택해주세요지만 Chart ID를 선택하여 주십시오랑 같은 의미여서 통일
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD042", GlobalVariable.gcLanguage));
                return false;
            }

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


                spdData.RPT_AddBasicColumn("account", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Material classification", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Model", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Standard", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Item Code", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);


                spdData.RPT_AddBasicColumn("investigation subject", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Number", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("LOT", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("UNIT", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 5, 3);

                spdData.RPT_AddBasicColumn("Pass rate (%)", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOT", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                spdData.RPT_AddBasicColumn("UNIT", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 8, 2);

                spdData.RPT_AddBasicColumn("Inspection quantity", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOT", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("UNIT", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 10, 2);

                spdData.RPT_AddBasicColumn("Defect quantity", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOT", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("UNIT", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("NCR", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 12, 3);

                spdData.RPT_AddBasicColumn("Defective rate(%)", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOT", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                spdData.RPT_AddBasicColumn("UNIT", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double4, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 15, 2);

                spdData.RPT_AddBasicColumn("Escape rate(%)", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("건", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                spdData.RPT_AddBasicColumn("LOT", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                spdData.RPT_AddBasicColumn("UNIT", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 17, 3);
                // 2010-11-05- 김민우 CTQ 선정율 컬럼 추가
                spdData.RPT_AddBasicColumn("CTQ Selection Rate (%)", 0, 20, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Double2, 100);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                // 2010-11-05- 김민우 CTQ 선정율 로우 합치기
                spdData.RPT_MerageHeaderRowSpan(0, 20, 2);
                
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("account", "VENDOR", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME IN ('VENDOR', 'H_CUSTOMER') AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND KEY_1 = A.VENDOR) AS VENDOR", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material classification", "MAT_TYPE", "A.MAT_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "MODEL", "A.MODEL", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Standard", "MAT_DESC", "A.MAT_DESC", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Item Code", "MAT_ID", "A.MAT_ID", false);
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
            string QueryCond2 = null;
            //string strFromDate = null, strToDate = null;

            //strFromDate = cdvStartDate.SelectedValue() + "220000";
            //strToDate = cdvEndDate.SelectedValue() + "215959";

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            // 쿼리

            strSqlString.Append("SELECT " + QueryCond2 + " \n");
            strSqlString.Append("     , A.TOT_QTY_0 " + "\n");
            strSqlString.Append("     , A.TOT_QTY_2 " + "\n");
            strSqlString.Append("     , A.TOT_QTY_1 " + "\n");
            strSqlString.Append("     , A.PASS_LOT_PER " + "\n");
            strSqlString.Append("     , A.PASS_PER " + "\n");
            strSqlString.Append("     , A.SP_LOT_QTY " + "\n");
            strSqlString.Append("     , A.SP_QTY " + "\n");
            strSqlString.Append("     , A.LOSS_LOT_QTY " + "\n");
            strSqlString.Append("     , A.LOSS_QTY " + "\n");
            strSqlString.Append("     , B.NCR_QTY  " + "\n");
            strSqlString.Append("     , A.LOSS_LOT_PER " + "\n");
            strSqlString.Append("     , A.LOSS_PER " + "\n");
            strSqlString.Append("     , NVL(ROUND((B.NCR_QTY/A.TOT_QTY_0) * 100, 2), 0) AS NCR_PER " + "\n");
            strSqlString.Append("     , NVL(ROUND((B.NCR_QTY/A.TOT_QTY_2) * 100, 2), 0) AS NCR_LOT_PER " + "\n");
            strSqlString.Append("     , NVL(ROUND((B.NCR_QTY/A.TOT_QTY_1) * 1000000, 2), 0) AS NCR_UNIT_PER" + "\n");
            // 2010-11-05-김민우: CTQ
            strSqlString.Append("     , NVL(ROUND((TOT_CTQ_QTY/TOT_QTY_0) * 100, 2), 0) AS CTQ_PER" + "\n");
            
            strSqlString.Append("  FROM ( " + " \n");
            strSqlString.Append("        SELECT " + QueryCond1 + "\n");            
            strSqlString.Append("             , COUNT(IQC_NO) AS TOT_QTY_0 " + "\n");
            strSqlString.Append("             , SUM(TOT_QTY_2) AS TOT_QTY_2 " + "\n");
            strSqlString.Append("             , SUM(TOT_QTY_1) AS TOT_QTY_1 " + "\n");
            strSqlString.Append("             , NVL(100 - ROUND((SUM(LOSS_LOT_QTY)/SUM(SP_LOT_QTY)) * 100, 2), 100) AS PASS_LOT_PER" + "\n");
            strSqlString.Append("             , NVL(100 - ROUND((SUM(LOSS_QTY)/SUM(SP_QTY)) * 100, 2), 100) AS PASS_PER" + "\n");
            strSqlString.Append("             , SUM(SP_LOT_QTY) AS SP_LOT_QTY" + "\n");
            strSqlString.Append("             , SUM(SP_QTY) AS SP_QTY" + "\n");
            strSqlString.Append("             , SUM(LOSS_LOT_QTY) AS LOSS_LOT_QTY" + "\n");
            strSqlString.Append("             , SUM(LOSS_QTY) AS LOSS_QTY" + "\n");
            strSqlString.Append("             , NVL(ROUND((SUM(LOSS_LOT_QTY)/SUM(SP_LOT_QTY)) * 100, 2), 0) AS LOSS_LOT_PER" + "\n");
            strSqlString.Append("             , NVL(ROUND((SUM(LOSS_QTY)/SUM(SP_QTY)) * 100, 4), 0) AS LOSS_PER" + "\n");
            // 2010-11-05-김민우
            strSqlString.Append("             , SUM(CTQ_QTY) AS TOT_CTQ_QTY" + "\n");

            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT BAT.IQC_NO" + "\n");
            strSqlString.Append("                     , BAT.MAT_ID" + "\n");
            strSqlString.Append("                     , MAT.MAT_TYPE" + "\n");
            strSqlString.Append("                     , MAT.MAT_DESC" + "\n");
            strSqlString.Append("                     , CMAT.MODEL" + "\n");
            strSqlString.Append("                     , BAT.VENDOR" + "\n");
            strSqlString.Append("                     , BAT.FINAL_DECISION" + "\n");
            strSqlString.Append("                     , BAT.TOT_QTY_1" + "\n");
            strSqlString.Append("                     , BAT.TOT_QTY_2" + "\n");
            strSqlString.Append("                     , LOT.SP_LOT_QTY" + "\n");
            strSqlString.Append("                     , LOT.SP_QTY" + "\n");
            strSqlString.Append("                     , HIS.LOSS_LOT_QTY" + "\n");
            strSqlString.Append("                     , HIS.LOSS_QTY" + "\n");
            strSqlString.Append("                     , BAT.CREATE_TIME AS CREATE_DATE" + "\n");
            strSqlString.Append("                     , BAT.UPDATE_TIME AS QC_DATE" + "\n");
            strSqlString.Append("                     , DECODE(BAT.RESV_FIELD_6, 'CTQ', 1, 0) AS CTQ_QTY" + "\n");
            strSqlString.Append("                  FROM CIQCBATSTS@RPTTOMES BAT" + "\n");
            strSqlString.Append("                     , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                     , CWIPMATDEF@RPTTOMES CMAT " + "\n");
            strSqlString.Append("                     , (" + "\n");

            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("                        SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS SP_LOT_QTY, SUM(SAMPLE_QTY) AS SP_QTY" + "\n");
                strSqlString.Append("                          FROM CIQCLOTSTS@RPTTOMES" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                // 2010-11-05-김민우
                strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                           AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                         GROUP BY IQC_NO, MAT_ID" + "\n");
            }
            else
            {
                strSqlString.Append("                        SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS SP_LOT_QTY, SUM(SAMPLE_QTY) AS SP_QTY" + "\n");
                strSqlString.Append("                          FROM CIQCEDCHIS@RPTTOMES" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                           AND TRAN_CODE = '" + cdvQCType.Text + "'" + "\n");
                strSqlString.Append("                           AND QC_FLAG <> 'X'" + "\n"); // 검사하지 않은 항목은 제회함.
                strSqlString.Append("                         GROUP BY IQC_NO, MAT_ID" + "\n");
            }

            strSqlString.Append("                       ) LOT" + "\n");
            strSqlString.Append("                     , (" + "\n");

            // 2015-08-06-임종우 : CIQCEDCHIS 에 데이터가 누락 되어 임시로 CIQCLOTSTS로 변경함.
            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("                        SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS LOSS_LOT_QTY, SUM(VISUAL_QTY + DEMENSION_QTY + MEASURE_QTY) AS LOSS_QTY" + "\n");
                strSqlString.Append("                          FROM CIQCLOTSTS@RPTTOMES" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                           AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                           AND VISUAL_QTY + DEMENSION_QTY + MEASURE_QTY > 0" + "\n");
                strSqlString.Append("                         GROUP BY IQC_NO, MAT_ID" + "\n");
            }
            else
            {
                strSqlString.Append("                        SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS LOSS_LOT_QTY, SUM(DEFECT_QTY) AS LOSS_QTY" + "\n");
                strSqlString.Append("                          FROM CIQCEDCHIS@RPTTOMES" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");

                if (cdvQCType.Text == "ALL")
                {
                    strSqlString.Append("                           AND TRAN_CODE <> 'Final'" + "\n");
                }
                else
                {
                    strSqlString.Append("                           AND TRAN_CODE = '" + cdvQCType.Text + "'" + "\n");
                }

                strSqlString.Append("                           AND DEFECT_QTY > 0" + "\n");
                strSqlString.Append("                         GROUP BY IQC_NO, MAT_ID" + "\n");
            }

            strSqlString.Append("                       ) HIS" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND BAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND BAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                   AND BAT.FACTORY = CMAT.FACTORY(+)" + "\n");
            strSqlString.Append("                   AND BAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                   AND BAT.MAT_ID = CMAT.MAT_ID(+)" + "\n");

            // 2010-04-19-임종우 : 검사 TYPE 선택 되면 해당 이력만 가져오기, 전체이면 성적서 & Skip 검사 모두 가져오기
            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("                   AND BAT.IQC_NO = LOT.IQC_NO(+)" + "\n");
            }
            else
            {
                strSqlString.Append("                   AND BAT.IQC_NO = LOT.IQC_NO" + "\n");
            }

            strSqlString.Append("                   AND BAT.IQC_NO = HIS.IQC_NO(+)" + "\n");

            //if (cdvResult.Text == "ALL")
            if (cdvResult.SelectedIndex == 0)
            {
                strSqlString.Append("                   AND BAT.FINAL_DECISION <> ' '" + "\n"); // 최종판정 된 것 만 가져옴.
            }
            //else if (cdvResult.Text == "합격")
            else if (cdvResult.SelectedIndex == 1)
            {
                strSqlString.Append("                   AND BAT.FINAL_DECISION = 'Y'" + "\n"); // 합격 된 것 만 가져옴.
            }
            //else if (cdvResult.Text == "불합격")
            else if (cdvResult.SelectedIndex == 2)
            {
                strSqlString.Append("                   AND BAT.FINAL_DECISION = 'N'" + "\n"); // 불합격 된 것 만 가져옴.
            }

            strSqlString.Append("                   AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND CMAT.DELETE_FLAG(+) = ' '" + "\n");
            //strSqlString.Append("                   AND MAT.MAT_TYPE NOT IN ('GW','SB','FG')" + "\n"); // 성적서 검사는 제외함.
            //strSqlString.Append("           AND LOT.SP_LOT_QTY > 0" + "\n"); // 샘플Lot 있는것. 즉 성적서 검사만 하는 자재는 제외함.

            // 모델 조회
            if (cdvModel.Text != "ALL" && cdvModel.Text != "")
            {
                strSqlString.Append("                   AND CMAT.MODEL(+) " + cdvModel.SelectedValueToQueryString + "\n"); 
            }

            // 규격 조회
            if (cdvDesc.Text != "ALL" && cdvDesc.Text != "")
            {
                strSqlString.Append("                   AND MAT.MAT_DESC " + cdvDesc.SelectedValueToQueryString + "\n");
            }

            // 업체 조회
            if (cdvVendor.Text != "ALL" && cdvVendor.Text != "")
            {
                strSqlString.Append("                   AND BAT.VENDOR " + cdvVendor.SelectedValueToQueryString + "\n");
            }

            // 2010-03-09-임종우 : 접수 구분 조회 추가
            if (cdvRcvType.Text != "ALL" && cdvRcvType.Text != "")
            {
                strSqlString.Append("                   AND BAT.IQC_TYPE " + cdvRcvType.SelectedValueToQueryString + "\n");
            }

            // 검사일, 접수일 조회
            if (rdbDate1.Checked == true)
            {
                strSqlString.Append("                   AND BAT.UPDATE_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "'" + "\n");
            }
            else
            {
                strSqlString.Append("                   AND BAT.CREATE_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "'" + "\n");
            }
            
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            
            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
            {
                strSqlString.Append("           AND MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append("         GROUP BY " + QueryCond1 + "\n");
            strSqlString.Append("       ) A " + " \n");
            strSqlString.Append("     , ( " + " \n");
            strSqlString.Append("        SELECT VENDOR " + " \n");
            strSqlString.Append("             , MAT_TYPE " + " \n");
            strSqlString.Append("             , COUNT(*) AS NCR_QTY " + " \n");
            strSqlString.Append("          FROM CQCMNCRDAT@RPTTOMES " + " \n");
            strSqlString.Append("         WHERE 1=1 " + " \n");
            strSqlString.Append("           AND NOTICE_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "'" + "\n");
            strSqlString.Append("           AND HIST_DELETE_FLAG <> 'Y' " + " \n");
            strSqlString.Append("           AND NCR_TYPE = '공정불량NCR' " + " \n");
            strSqlString.Append("           AND MAT_TYPE NOT IN ('AD','PL','SM','TR') " + " \n");
            strSqlString.Append("         GROUP BY VENDOR, MAT_TYPE" + " \n");
            strSqlString.Append("       ) B " + " \n");
            strSqlString.Append(" WHERE 1=1 " + " \n");
            strSqlString.Append("   AND A.VENDOR = B.VENDOR(+) " + " \n");
            strSqlString.Append("   AND A.MAT_TYPE = B.MAT_TYPE(+) " + " \n");
            strSqlString.Append(" ORDER BY " + QueryCond1 + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region " MakeChart : Chart 처리 "

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        private string MakeChart()
        {
            StringBuilder strSqlString = new StringBuilder();

            // 쿼리

            //strSqlString.Append("SELECT VENDOR || '(' || MAT_TYPE || ')' AS VENDOR " + "\n");
            strSqlString.Append("SELECT VENDOR " + "\n");

            for (int i = 0; i < dtWeek.Rows.Count; i++)
            {
                if (cdvChart.Text == "수입검사 불량률 (LRR)")
                    strSqlString.Append("     , SUM(DECODE(QC_DATE, '" + dtWeek.Rows[i][0].ToString() + "', LOSS_LOT_PER, 0)) AS \"" + dtWeek.Rows[i][0].ToString() + "\"" + "\n");
                else if (cdvChart.Text == "수입검사 불량률 (UNIT)")
                    strSqlString.Append("     , SUM(DECODE(QC_DATE, '" + dtWeek.Rows[i][0].ToString() + "', LOSS_PER, 0)) AS \"" + dtWeek.Rows[i][0].ToString() + "\"" + "\n");
                //else if (cdvChart.Text == "원자재 탈출률 (NCR건/검사대상건)")
                //    strSqlString.Append("     , SUM(DECODE(QC_DATE, '" + dtWeek.Rows[i][0].ToString() + "', NCR_PER, 0)) AS \"" + dtWeek.Rows[i][0].ToString() + "\"" + "\n");
                else if (cdvChart.Text == "원자재 탈출률 (LRR)")
                    strSqlString.Append("     , SUM(DECODE(QC_DATE, '" + dtWeek.Rows[i][0].ToString() + "', NCR_LOT_PER, 0)) AS \"" + dtWeek.Rows[i][0].ToString() + "\"" + "\n");
                else
                    strSqlString.Append("     , SUM(DECODE(QC_DATE, '" + dtWeek.Rows[i][0].ToString() + "', NCR_UNIT_PER, 0)) AS \"" + dtWeek.Rows[i][0].ToString() + "\"" + "\n");
            }

            strSqlString.Append("  FROM ( " + "\n");
            //strSqlString.Append("         SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME='H_VENDOR' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND KEY_1 = A.VENDOR) AS VENDOR " + "\n");
            //strSqlString.Append("              , A.MAT_TYPE, A.QC_DATE, A.LOSS_LOT_PER, A.LOSS_PER " + "\n");
            strSqlString.Append("         SELECT 'TOTAL' AS VENDOR, A.QC_DATE, A.LOSS_LOT_PER, A.LOSS_PER " + "\n");
            strSqlString.Append("              , NVL(ROUND((B.NCR_QTY/A.TOT_QTY_0) * 100, 2), 0) AS NCR_PER " + "\n");
            strSqlString.Append("              , NVL(ROUND((B.NCR_QTY/A.TOT_QTY_2) * 100, 2), 0) AS NCR_LOT_PER " + "\n");
            strSqlString.Append("              , NVL(ROUND((B.NCR_QTY/A.TOT_QTY_1) * 1000000, 2), 0) AS NCR_UNIT_PER " + "\n");
            strSqlString.Append("           FROM (" + "\n");
            //strSqlString.Append("                  SELECT VENDOR, MAT_TYPE, QC_DATE " + "\n");
            strSqlString.Append("                  SELECT QC_DATE " + "\n");
            strSqlString.Append("                       , COUNT(IQC_NO) AS TOT_QTY_0 " + "\n");
            strSqlString.Append("                       , SUM(TOT_QTY_1) AS TOT_QTY_1 " + "\n");
            strSqlString.Append("                       , SUM(TOT_QTY_2) AS TOT_QTY_2 " + "\n");
            strSqlString.Append("                       , NVL(ROUND((SUM(LOSS_LOT_QTY)/SUM(SP_LOT_QTY)) * 100, 2), 0) AS LOSS_LOT_PER " + "\n");
            strSqlString.Append("                       , NVL(ROUND((SUM(LOSS_QTY)/SUM(SP_QTY)) * 100, 2), 0) AS LOSS_PER " + "\n");
            strSqlString.Append("                    FROM (" + "\n");
            strSqlString.Append("                          SELECT BAT.IQC_NO, BAT.MAT_ID, MAT.MAT_TYPE, MAT.MAT_DESC, CMAT.MODEL " + "\n");
            strSqlString.Append("                               , BAT.VENDOR, BAT.FINAL_DECISION, BAT.TOT_QTY_1, BAT.TOT_QTY_2 " + "\n");
            strSqlString.Append("                               , LOT.SP_LOT_QTY, LOT.SP_QTY, HIS.LOSS_LOT_QTY, HIS.LOSS_QTY " + "\n");            

            // 검사일, 접수일 조회
            if (rdbDate1.Checked == true)
            {                
                strSqlString.Append("                               , GET_WORK_DATE(BAT.UPDATE_TIME, 'W') AS QC_DATE" + "\n");
            }
            else
            {
                strSqlString.Append("                               , GET_WORK_DATE(BAT.CREATE_TIME, 'W') AS QC_DATE" + "\n");
            }

            strSqlString.Append("                            FROM CIQCBATSTS@RPTTOMES BAT" + "\n");
            strSqlString.Append("                               , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                               , CWIPMATDEF@RPTTOMES CMAT " + "\n");
            strSqlString.Append("                               , (" + "\n");

            if (cdvQCType.Text == "ALL")
            {                
                strSqlString.Append("                                  SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS SP_LOT_QTY, SUM(SAMPLE_QTY) AS SP_QTY" + "\n");
                strSqlString.Append("                                    FROM CIQCLOTSTS@RPTTOMES" + "\n");
                strSqlString.Append("                                   WHERE 1=1" + "\n");
                strSqlString.AppendFormat("                                     AND FACTORY = '{0}'  \n", cdvFactory.Text.Trim());
                strSqlString.Append("                                   GROUP BY IQC_NO, MAT_ID" + "\n");
            }
            else
            {
                strSqlString.Append("                                  SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS SP_LOT_QTY, SUM(SAMPLE_QTY) AS SP_QTY" + "\n");
                strSqlString.Append("                                    FROM CIQCEDCHIS@RPTTOMES" + "\n");
                strSqlString.Append("                                   WHERE 1=1" + "\n");
                strSqlString.AppendFormat("                                     AND FACTORY = '{0}'  \n", cdvFactory.Text.Trim());
                strSqlString.Append("                                     AND TRAN_CODE = '" + cdvQCType.Text + "'" + "\n");
                strSqlString.Append("                                     AND QC_FLAG <> 'X'" + "\n"); // 검사하지 않은 항목은 제회함.
                strSqlString.Append("                                   GROUP BY IQC_NO, MAT_ID" + "\n");
            }

            strSqlString.Append("                                 ) LOT" + "\n");
            strSqlString.Append("                               , (" + "\n");
            strSqlString.Append("                                  SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS LOSS_LOT_QTY, SUM(DEFECT_QTY) AS LOSS_QTY" + "\n");
            strSqlString.Append("                                    FROM CIQCEDCHIS@RPTTOMES" + "\n");
            strSqlString.Append("                                   WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                                     AND FACTORY = '{0}' \n", cdvFactory.Text.Trim());

            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("                                     AND TRAN_CODE <> 'Final'" + "\n");
            }
            else
            {
                strSqlString.Append("                                     AND TRAN_CODE = '" + cdvQCType.Text + "'" + "\n");
            }

            strSqlString.Append("                                     AND DEFECT_QTY > 0" + "\n");
            strSqlString.Append("                                   GROUP BY IQC_NO, MAT_ID" + "\n");
            strSqlString.Append("                                 ) HIS" + "\n");
            strSqlString.Append("                           WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                             AND BAT.FACTORY = '{0}' \n", cdvFactory.Text.Trim());
            strSqlString.Append("                             AND BAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                             AND BAT.FACTORY = CMAT.FACTORY(+)" + "\n");
            strSqlString.Append("                             AND BAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                             AND BAT.MAT_ID = CMAT.MAT_ID(+)" + "\n");

            // 2010-04-19-임종우 : 검사 TYPE 선택 되면 해당 이력만 가져오기, 전체이면 성적서 & Skip 검사 모두 가져오기
            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("                             AND BAT.IQC_NO = LOT.IQC_NO(+)" + "\n");
            }
            else
            {                
                strSqlString.Append("                             AND BAT.IQC_NO = LOT.IQC_NO" + "\n");
            }
                        
            strSqlString.Append("                             AND BAT.IQC_NO = HIS.IQC_NO(+)" + "\n");

            //if (cdvResult.Text == "ALL")
            if (cdvResult.SelectedIndex == 0)
            {
                strSqlString.Append("                             AND BAT.FINAL_DECISION <> ' '" + "\n"); // 최종판정 된 것 만 가져옴.
            }
            //else if (cdvResult.Text == "합격")
            else if (cdvResult.SelectedIndex == 1)
            {
                strSqlString.Append("                             AND BAT.FINAL_DECISION = 'Y'" + "\n"); // 합격 된 것 만 가져옴.
            }
            //else if (cdvResult.Text == "불합격")
            else if (cdvResult.SelectedIndex == 2)
            {
                strSqlString.Append("                             AND BAT.FINAL_DECISION = 'N'" + "\n"); // 불합격 된 것 만 가져옴.
            }

            strSqlString.Append("                             AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                             AND CMAT.DELETE_FLAG(+) = ' '" + "\n");
            //strSqlString.Append("                             AND MAT.MAT_TYPE NOT IN ('GW','SB','FG')" + "\n"); // 성적서 검사는 제외함.
            //strSqlString.Append("           AND LOT.SP_LOT_QTY > 0" + "\n"); // 샘플Lot 있는것. 즉 성적서 검사만 하는 자재는 제외함.

            // 모델 조회
            if (cdvModel.Text != "ALL" && cdvModel.Text != "")
            {
                strSqlString.Append("                             AND CMAT.MODEL(+) " + cdvModel.SelectedValueToQueryString + "\n");
            }

            // 규격 조회
            if (cdvDesc.Text != "ALL" && cdvDesc.Text != "")
            {
                strSqlString.Append("                             AND MAT.MAT_DESC " + cdvDesc.SelectedValueToQueryString + "\n");
            }

            // 업체 조회
            if (cdvVendor.Text != "ALL" && cdvVendor.Text != "")
            {
                strSqlString.Append("                             AND BAT.VENDOR " + cdvVendor.SelectedValueToQueryString + "\n");
            }

            // 2010-03-09-임종우 : 접수 구분 조회 추가
            if (cdvRcvType.Text != "ALL" && cdvRcvType.Text != "")
            {
                strSqlString.Append("                             AND BAT.IQC_TYPE " + cdvRcvType.SelectedValueToQueryString + "\n");
            }

            // 검사일, 접수일 조회
            if (rdbDate1.Checked == true)
            {
                strSqlString.Append("                             AND BAT.UPDATE_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "'" + "\n");
            }
            else
            {
                strSqlString.Append("                             AND BAT.CREATE_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "'" + "\n");
            }

            strSqlString.Append("                         )" + "\n");
            strSqlString.Append("                   WHERE 1=1 " + "\n");

            // 제품 TYPE 조회
            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
            {
                strSqlString.Append("                     AND MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");
            }

            //strSqlString.Append("                   GROUP BY VENDOR, MAT_TYPE, QC_DATE" + "\n");
            strSqlString.Append("                   GROUP BY QC_DATE" + "\n");
            strSqlString.Append("                ) A " + "\n");
            strSqlString.Append("              , ( " + " \n");
            //strSqlString.Append("                 SELECT VENDOR, MAT_TYPE, QC_DATE " + " \n");
            strSqlString.Append("                 SELECT QC_DATE " + " \n");
            strSqlString.Append("                      , COUNT(*) AS NCR_QTY " + " \n");
            strSqlString.Append("                   FROM ( " + " \n");
            strSqlString.Append("                         SELECT VENDOR, MAT_TYPE " + " \n");
            strSqlString.Append("                              , GET_WORK_DATE(NOTICE_TIME,'W') AS QC_DATE " + " \n");            
            strSqlString.Append("                           FROM CQCMNCRDAT@RPTTOMES " + " \n");
            strSqlString.Append("                          WHERE 1=1 " + " \n");
            strSqlString.Append("                            AND NOTICE_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "'" + "\n");
            strSqlString.Append("                            AND HIST_DELETE_FLAG <> 'Y' " + " \n");
            strSqlString.Append("                            AND NCR_TYPE = '공정불량NCR' " + " \n");
            strSqlString.Append("                            AND MAT_TYPE NOT IN ('AD','PL','SM','TR') " + " \n");

            // 업체 조회
            if (cdvVendor.Text != "ALL" && cdvVendor.Text != "")
            {
                strSqlString.Append("                            AND VENDOR " + cdvVendor.SelectedValueToQueryString + "\n");
            }

            // 제품 TYPE 조회
            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
            {
                strSqlString.Append("                            AND MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append("                        ) " + " \n");
            //strSqlString.Append("                  GROUP BY VENDOR, MAT_TYPE, QC_DATE " + " \n");
            strSqlString.Append("                  GROUP BY QC_DATE " + " \n");
            strSqlString.Append("                ) B " + " \n");
            strSqlString.Append("          WHERE 1=1 " + " \n");
            //strSqlString.Append("            AND A.VENDOR = B.VENDOR(+) " + " \n");
            //strSqlString.Append("            AND A.MAT_TYPE = B.MAT_TYPE(+) " + " \n");
            strSqlString.Append("            AND A.QC_DATE = B.QC_DATE(+) " + " \n");

            // 2010-06-11-임종우 : 수입검사 LRR 선택시 목표 값 표시
            if (cdvChart.Text == "수입검사 불량률 (LRR)")
            {
                strSqlString.Append("          UNION ALL " + "\n");
                strSqlString.Append("         SELECT '목표', KEY_1, TO_NUMBER(DATA_1), 0, 0, 0, 0 " + "\n");
                strSqlString.Append("           FROM MGCMTBLDAT " + "\n");
                strSqlString.Append("          WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("            AND TABLE_NAME = 'H_IQC_NCR_WEEK_PLAN' " + "\n");
                strSqlString.Append("            AND KEY_1 BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "' " + "\n");
                strSqlString.Append("            AND KEY_2 = 'IQC'  " + "\n");
            }

            // 2010-06-11-임종우 : 원자재 LRR 선택시 목표 값 표시
            if (cdvChart.Text == "원자재 탈출률 (LRR)")
            {
                strSqlString.Append("          UNION ALL " + "\n");
                strSqlString.Append("         SELECT '목표', KEY_1, 0, 0, 0, TO_NUMBER(DATA_1), 0 " + "\n");
                strSqlString.Append("           FROM MGCMTBLDAT " + "\n");
                strSqlString.Append("          WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("            AND TABLE_NAME = 'H_IQC_NCR_WEEK_PLAN' " + "\n");
                strSqlString.Append("            AND KEY_1 BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "' " + "\n");
                strSqlString.Append("            AND KEY_2 = 'IQC'  " + "\n");
            }

            strSqlString.Append("       ) " + " \n");
            strSqlString.Append(" GROUP BY VENDOR " + "\n");
            //strSqlString.Append(" ORDER BY DECODE(VENDOR, '목표', 1, 2)  " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        /// <summary>
        /// 검색 기간의 Week List 를 가져온다.
        /// </summary>
        private string MakeWeek()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT DISTINCT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0') AS WEEK " + "\n");
            strSqlString.Append("  FROM MWIPCALDEF " + "\n");
            strSqlString.Append(" WHERE CALENDAR_ID = 'HM' " + "\n");
            strSqlString.Append("   AND SYS_DATE BETWEEN '" + cdvFromToDate.HmFromDay + "' AND '" + cdvFromToDate.HmToDay + "' " + "\n");
            strSqlString.Append(" ORDER BY WEEK " + "\n");

            return strSqlString.ToString();
        }

        #region "POP용 쿼리"
        private string MakeSqlStringForPopup(string strVendor)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME IN ('VENDOR', 'H_CUSTOMER') AND FACTORY = '" + cdvFactory.Text.Trim() + "' AND KEY_1 = BAT.VENDOR) AS VENDOR" + "\n");
            strSqlString.Append("     , MAT.MAT_TYPE" + "\n");
            strSqlString.Append("     , BAT.MAT_ID" + "\n");
            strSqlString.Append("     , MAT.MAT_DESC" + "\n");
            strSqlString.Append("     , BAT.IQC_NO" + "\n");            
            strSqlString.Append("     , BAT.TOT_QTY_2" + "\n");
            strSqlString.Append("     , BAT.TOT_QTY_1" + "\n");
            strSqlString.Append("     , NVL(100 - ROUND((LOSS_LOT_QTY/SP_LOT_QTY) * 100, 2), 100) AS PASS_LOT_PER" + "\n");
            strSqlString.Append("     , NVL(100 - ROUND((LOSS_QTY/SP_QTY) * 100, 2), 100) AS PASS_PER" + "\n");
            strSqlString.Append("     , LOT.SP_LOT_QTY" + "\n");
            strSqlString.Append("     , LOT.SP_QTY" + "\n");
            strSqlString.Append("     , HIS.LOSS_LOT_QTY" + "\n");
            strSqlString.Append("     , HIS.LOSS_QTY" + "\n");
            strSqlString.Append("     , NVL(ROUND((LOSS_LOT_QTY/SP_LOT_QTY) * 100, 2), 0) AS LOSS_LOT_PER" + "\n");
            strSqlString.Append("     , NVL(ROUND((LOSS_QTY/SP_QTY) * 100, 2), 0) AS LOSS_PER" + "\n");
            strSqlString.Append("     , BAT.RESV_FIELD_2 AS YIELD" + "\n");
            strSqlString.Append("     , (SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID = BAT.QC_OPERATOR) AS QC_OPERATOR" + "\n");
            strSqlString.Append("     , BAT.CREATE_TIME AS CREATE_DATE" + "\n");
            strSqlString.Append("     , BAT.UPDATE_TIME AS QC_DATE" + "\n");
            strSqlString.Append("  FROM CIQCBATSTS@RPTTOMES BAT" + "\n");
            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("     , CWIPMATDEF@RPTTOMES CMAT " + "\n");
            strSqlString.Append("     , (" + "\n");

            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("        SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS SP_LOT_QTY, SUM(SAMPLE_QTY) AS SP_QTY" + "\n");
                strSqlString.Append("          FROM CIQCLOTSTS@RPTTOMES" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                // 2010-11-05-김민우
                strSqlString.AppendFormat("           AND FACTORY = '{0}' \n", cdvFactory.Text.Trim());
                strSqlString.Append("           AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("         GROUP BY IQC_NO, MAT_ID" + "\n");
            }
            else
            {
                strSqlString.Append("        SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS SP_LOT_QTY, SUM(SAMPLE_QTY) AS SP_QTY" + "\n");
                strSqlString.Append("          FROM CIQCEDCHIS@RPTTOMES" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("           AND FACTORY = '{0}' \n", cdvFactory.Text.Trim());
                strSqlString.Append("           AND TRAN_CODE = '" + cdvQCType.Text + "'" + "\n");
                strSqlString.Append("           AND QC_FLAG <> 'X'" + "\n"); // 검사하지 않은 항목은 제회함.
                strSqlString.Append("         GROUP BY IQC_NO, MAT_ID" + "\n");
            }

            strSqlString.Append("       ) LOT" + "\n");


            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT IQC_NO, MAT_ID, COUNT(LOT_ID) AS LOSS_LOT_QTY, SUM(DEFECT_QTY) AS LOSS_QTY" + "\n");
            strSqlString.Append("          FROM CIQCEDCHIS@RPTTOMES" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.AppendFormat("           AND FACTORY = '{0}' \n", cdvFactory.Text.Trim());

            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("           AND TRAN_CODE <> 'Final'" + "\n");
            }
            else
            {
                strSqlString.Append("           AND TRAN_CODE = '" + cdvQCType.Text + "'" + "\n");
            }
                        
            strSqlString.Append("           AND DEFECT_QTY > 0" + "\n");
            strSqlString.Append("         GROUP BY IQC_NO, MAT_ID" + "\n");
            strSqlString.Append("       ) HIS" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.AppendFormat("   AND BAT.FACTORY = '{0}' \n", cdvFactory.Text.Trim());
            strSqlString.Append("   AND BAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("   AND BAT.FACTORY = CMAT.FACTORY(+)" + "\n");
            strSqlString.Append("   AND BAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("   AND BAT.MAT_ID = CMAT.MAT_ID(+)" + "\n");

            // 2010-04-19-임종우 : 검사 TYPE 선택 되면 해당 이력만 가져오기, 전체이면 성적서 & Skip 검사 모두 가져오기
            if (cdvQCType.Text == "ALL")
            {
                strSqlString.Append("   AND BAT.IQC_NO = LOT.IQC_NO(+)" + "\n");                
            }
            else
            {
                strSqlString.Append("   AND BAT.IQC_NO = LOT.IQC_NO" + "\n");
            }
                        
            strSqlString.Append("   AND BAT.IQC_NO = HIS.IQC_NO(+)" + "\n");

            // 검사 판정 조회
            //if (cdvResult.Text == "ALL")
            if (cdvResult.SelectedIndex == 0)
            {
                strSqlString.Append("   AND BAT.FINAL_DECISION <> ' '" + "\n"); // 최종판정 된 것 만 가져옴.
            }
            //else if (cdvResult.Text == "합격")
            else if (cdvResult.SelectedIndex == 1)
            {
                strSqlString.Append("   AND BAT.FINAL_DECISION = 'Y'" + "\n"); // 합격 된 것 만 가져옴.
            }
            //else if (cdvResult.Text == "불합격")
            else if (cdvResult.SelectedIndex == 2)
            {
                strSqlString.Append("   AND BAT.FINAL_DECISION = 'N'" + "\n"); // 불합격 된 것 만 가져옴.
            }

            // 모델 조회
            if (cdvModel.Text != "ALL" && cdvModel.Text != "")
            {
                strSqlString.Append("   AND CMAT.MODEL(+) " + cdvModel.SelectedValueToQueryString + "\n");
            }

            // 규격 조회
            if (cdvDesc.Text != "ALL" && cdvDesc.Text != "")
            {
                strSqlString.Append("   AND MAT.MAT_DESC " + cdvDesc.SelectedValueToQueryString + "\n");
            }


            strSqlString.Append("   AND BAT.VENDOR = (SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME IN ('VENDOR', 'H_CUSTOMER') AND FACTORY = '" + cdvFactory.Text.Trim() + "' AND DATA_1 = '" + strVendor + "')" + "\n");


            // 접수 구분 조회
            if (cdvRcvType.Text != "ALL" && cdvRcvType.Text != "")
            {
                strSqlString.Append("   AND BAT.IQC_TYPE " + cdvRcvType.SelectedValueToQueryString + "\n");
            }

            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
            {
                strSqlString.Append("   AND MAT.MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append("   AND BAT.FINAL_DECISION <> ' '" + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("   AND CMAT.DELETE_FLAG(+) = ' '" + "\n");
            //strSqlString.Append("   AND MAT.MAT_TYPE NOT IN ('GW','SB','FG')" + "\n"); // 성적서 검사는 제외함.

            // 검사일, 접수일 조회
            if (rdbDate1.Checked == true)
                strSqlString.Append("   AND BAT.UPDATE_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "'" + "\n");
            else
                strSqlString.Append("   AND BAT.CREATE_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "'" + "\n");

            strSqlString.Append(" ORDER BY MAT_TYPE, MAT_ID, IQC_NO" + "\n"); // 성적서 검사는 제외함.

            return strSqlString.ToString();
        }
        #endregion

        #region ShowChart

        private void ShowChart()
        {
            // 검색 기간 주차 리스트 가져오기
            dtWeek = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeWeek());

            DataTable dtChart = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChart());

            // 차트 설정

            ////udcMSChart1.RPT_2_ClearData();
            udcMSChart1.RPT_3_OpenData(2, dtWeek.Rows.Count);

            int[] wip_columns = new Int32[dtWeek.Rows.Count];
            int[] tat_columns = new Int32[dtWeek.Rows.Count];
            int arrCnt = 0;
      
            string[] columnsHeader = new string[dtChart.Columns.Count - 1]; //데이터 테이블의 2째 컬럼헤더 부터 사용하기에 -1을 한다.
            
            // 데이터 테이블의 컬럼헤더를 저장한다. X축 표시값으로 사용하려...
            for (int x = 1; x < dtChart.Columns.Count; x++)
            {
                columnsHeader[arrCnt] = dtChart.Columns[x].ToString();

                arrCnt++;
            }

            for (int i = 0; i < wip_columns.Length; i++)
            {
                wip_columns[i] = 1 + i;
                tat_columns[i] = 1 + i;
            }

            int cnt = 0;
            double max1 = 0;
            double max_temp = 0;

            for (int j = 0; j < dtChart.Rows.Count; j++)
            {
                ////max1 = udcChartFX1.RPT_4_AddData(dtChart, new int[] { cnt }, wip_columns, SeriseType.Column);
                //max1 = udcChartFX1.RPT_4_AddData_Original(dtChart, new int[] { cnt }, wip_columns, SeriseType.Rows, DataTypes.Initeger);

                max1 = udcMSChart1.RPT_4_AddData_Original(dtChart, new int[] { cnt }, wip_columns, SeriseType.Rows, DataTypes.Initeger);

                if (max1 > max_temp)
                {
                    max_temp = max1;
                }

                cnt++;

            }
            max1 = max_temp;

            int[] LegBox = new int[cnt + 1];

            cnt = 0;

            // 2010-06-11-임종우 : 목표값 보이는 챠트는 LINE, BAR 두개로 표시 함.
            if (cdvChart.Text == "수입검사 불량률 (LRR)" || cdvChart.Text == "원자재 탈출률 (LRR)")
            {
                
                //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                udcMSChart1.RPT_6_SetGallery(SeriesChartType.Column, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);

                for (int j = 0; j < dtChart.Rows.Count; j++)
                {
                    cnt++;
                    //udcChartFX1.RPT_6_SetGallery(ChartType.Line, cnt, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                    udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, cnt, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                    
                    LegBox[cnt] = j;
                }
      
            }
            else
            {                
                for (int j = 0; j < dtChart.Rows.Count; j++)
                {
                    //udcChartFX1.RPT_6_SetGallery(ChartType.Line, cnt, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                    udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, cnt, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                    LegBox[cnt] = j;
                    cnt++;
                }
                
            }

            udcMSChart1.RPT_7_SetXAsixTitle(columnsHeader);
            udcMSChart1.RPT_8_SetSeriseLegend(dtChart, 0, dtChart.Rows.Count, 0, System.Windows.Forms.DataVisualization.Charting.Docking.Right);
            udcMSChart1.Series[0].LabelForeColor = Color.Blue;
            udcMSChart1.Series[0].IsValueShownAsLabel = true;
            udcMSChart1.Series[0]["DrawingStyle"] = "Cylinder";
            udcMSChart1.Series[1].IsVisibleInLegend = false;            
            udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;

            // 2010-06-11-임종우 : 목표값 보이는 챠트는 목표값 부분은 빨강색으로..
            if (cdvChart.Text == "수입검사 불량률 (LRR)" || cdvChart.Text == "원자재 탈출률 (LRR)")
            {
                udcMSChart1.Series[1].Color = Color.Red;
                                
            }
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

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            try
            {
                // 검색중 화면 표시
             //   LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                GridColumnInit();
                //udcChartFX1.RPT_1_ChartInit();
                udcMSChart1.RPT_1_ChartInit();

                // Query문으로 데이터를 검색한다.

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //spdData.DataSource = dt;

                spdData.isShowZero = true;

                //// Sub Total
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 5, null, null);

                //// Total
                spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

                ////4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                // GrandTotal에 백분율 부분 계산
                double sample_lot = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 10].Value); // 샘플랏수
                double sample_qty = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 11].Value); // 샘플수량
                double ncr_qty = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 14].Value); // NCR 수량
                // 샘플수량이 0이면 나누기 계산을 위해 1로 변환.
                if (sample_lot == 0)
                {
                    sample_lot = 1;
                }

                if (sample_qty == 0)
                {
                    sample_qty = 1;
                }

                spdData.ActiveSheet.Cells[0, 15].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 12].Value) / sample_lot) * 100; // 불량율(LOT)
                spdData.ActiveSheet.Cells[0, 16].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 13].Value) / sample_qty) * 100; // 불량율(UNIT)
                spdData.ActiveSheet.Cells[0, 8].Value = 100 - Convert.ToDouble(spdData.ActiveSheet.Cells[0, 15].Value); // 합격율(LOT)
                spdData.ActiveSheet.Cells[0, 9].Value = 100 - Convert.ToDouble(spdData.ActiveSheet.Cells[0, 16].Value); // 합격율(UNIT)
                spdData.ActiveSheet.Cells[0, 17].Value = (ncr_qty / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 5].Value)) * 100; // 탈출율(건);
                spdData.ActiveSheet.Cells[0, 18].Value = (ncr_qty / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 6].Value)) * 100; // 탈출율(LOT);;
                spdData.ActiveSheet.Cells[0, 19].Value = (ncr_qty / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 7].Value)) * 1000000; // 탈출율(UNIT);;
                SetAvgVertical2(20);
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
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            try
            {
                spdData.ExportExcel();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        private void cdvDesc_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT MAT_DESC Code, '' Data" + "\n";
            strQuery += "  FROM MWIPMATDEF " + "\n";
            strQuery += " WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' \n";
            strQuery += "   AND MAT_TYPE <> 'FG' " + "\n";
            strQuery += "   AND MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n";
            strQuery += "   AND DELETE_FLAG = ' ' " + "\n";
            strQuery += "ORDER BY MAT_DESC " + "\n";

            cdvDesc.sDynamicQuery = strQuery;
        }

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
            strQuery += "           AND FACTORY = '" + cdvFactory.Text.Trim() + "' ) B " + "\n";
            strQuery += "     , (SELECT KEY_1, DATA_1 " + "\n";
            strQuery += "          FROM MGCMTBLDAT " + "\n";
            strQuery += "         WHERE TABLE_NAME = 'H_CUSTOMER' " + "\n";
            strQuery += "           AND FACTORY = '" +  cdvFactory.Text.Trim() + "' ) C " + "\n";
            strQuery += " WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' " + "\n";
            strQuery += "   AND A.VENDOR = B.KEY_1(+) " + "\n";
            strQuery += "   AND A.VENDOR = C.KEY_1(+)  " + "\n";
            strQuery += "   AND A.CREATE_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "' " + "\n";
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

        private void cdvModel_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT MODEL Code, '' Data" + "\n";
            strQuery += "  FROM ( " + "\n";
            strQuery += "         SELECT CDEF.MODEL " + "\n";            
            strQuery += "           FROM MWIPMATDEF MDEF, CWIPMATDEF@RPTTOMES CDEF" + "\n";
            strQuery += "          WHERE MDEF.FACTORY = CDEF.FACTORY " + "\n";
            strQuery += "            AND MDEF.MAT_ID = CDEF.MAT_ID " + "\n";
            strQuery += "            AND MDEF.FACTORY = '" + cdvFactory.Text.Trim() + "' " + "\n";            
            strQuery += "            AND MDEF.MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n";
            strQuery += "            AND MDEF.DELETE_FLAG = ' ' " + "\n";
            strQuery += "            AND CDEF.DELETE_FLAG = ' ' " + "\n";
            strQuery += "       ) " + "\n";
            strQuery += "ORDER BY MODEL " + "\n";

            cdvModel.sDynamicQuery = strQuery;
        }

        //2010-02-26-임종우 : Chart 5가지 종류 표시를 위해 추가 함.
        private void cdvChart_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DECODE (ROWNUM, 1, A, 2, B, 3, C, 4, D) AS Code, '' Data" + "\n";
            strQuery += "  FROM ( " + "\n";
            strQuery += "         SELECT 1 " + "\n";
            strQuery += "           FROM DUAL" + "\n";
            strQuery += "        CONNECT BY LEVEL <=4 " + "\n";
            strQuery += "       ) " + "\n";
            strQuery += "     , ( " + "\n";
            strQuery += "         SELECT '수입검사 불량률 (LRR)' AS A " + "\n";
            strQuery += "              , '수입검사 불량률 (UNIT)' AS B " + "\n";
            strQuery += "              , '원자재 탈출률 (LRR)' AS C " + "\n";
            strQuery += "              , '원자재 탈출률 (NCR건/검사대상UNIT)' AS D " + "\n";
            strQuery += "           FROM DUAL" + "\n";
            strQuery += "       ) " + "\n";

            cdvChart.sDynamicQuery = strQuery;
        }

        //2010-03-09-임종우 : 접수 구분별 조회하기 위해 추가 함.
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

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {             
            if (spdData.ActiveSheet.Columns[e.Column].Label == "account")
            {
                strVendor = spdData.ActiveSheet.Cells[e.Row, e.Column].Text;

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup(strVendor));

                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Windows.Forms.Form frm = new PQC030701_P1(strVendor, dt);
                    frm.ShowDialog();
                }
            }
        }

        public void SetAvgVertical2(int nColPos)
        {
            double sum = 0;
            double divide = 0;


            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);

                if (spdData.ActiveSheet.Cells[i, nColPos].Value != null)
                {
                    divide += 1;
                }
            }
            if (divide == 0)
            {
                divide = 1;
            }
            spdData.ActiveSheet.Cells[0, nColPos].Value = sum / divide;
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {

        }

       
        // 2011-02-24-임종우 : Wafer 도 조회 하기 위해 직접 쿼리 작성 함.
        //private void cdvMatType_ValueButtonPress(object sender, EventArgs e)
        //{
        //    string strQuery = string.Empty;

        //    strQuery += "SELECT KEY_1 Code, DATA_1 Data" + "\n";
        //    strQuery += "  FROM ( " + "\n";
        //    strQuery += "         SELECT * " + "\n";
        //    strQuery += "           FROM MGCMTBLDAT" + "\n";
        //    strQuery += "          WHERE TABLE_NAME = 'MATERIAL_TYPE' " + "\n";
        //    strQuery += "            AND DATA_2 = 'Y' " + "\n";
        //    strQuery += "          UNION ALL " + "\n";
        //    strQuery += "         SELECT * " + "\n";
        //    strQuery += "           FROM MGCMTBLDAT" + "\n";
        //    strQuery += "          WHERE TABLE_NAME = 'MATERIAL_TYPE' " + "\n";
        //    strQuery += "            AND KEY_1 = 'FG' " + "\n";
        //    strQuery += "       ) " + "\n";
        //    strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
        //    strQuery += " ORDER BY DECODE(KEY_1, 'FG', 'ZZZ') DESC " + "\n";

        //    cdvMatType.sDynamicQuery = strQuery;
        //}


    }
}
