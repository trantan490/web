using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.MAT
{
    public partial class MAT070603 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        /// <summary>
        /// 클  래  스: MAT070603<br/>
        /// 클래스요약: PCB 입고현황<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2015-11-13<br/>
        /// 상세  설명: PCB 입고현황(요청자 : 임태성K)<br/>
        /// 변경  내용: <br/>
        /// 2015-11-17-임종우 : Version 검색 기능 추가. 이전 4개까지만 조회 되도록...(임태성K 요청)
        ///                   : BOM을 통한 거래선, PKG_CODE 정보 한 줄로 나래비하여 표시(임태성K 요청)
        /// 2015-12-08-임종우 : Version 검색 10개까지 추가 (이승희 요청)
        /// 2016-07-07-임종우 : 엑셀 업로드 방식에서 VSCM 데이터 DB Link 방식으로 변경 & Version 검색 월,목기준 4개까지만.. (이승희 요청)
        /// 2016-07-11-임종우 : PB, LF, TE 검색 기능 추가 & Mat Description 추가 (이승희 요청)
        /// 2016-07-11-임종우 : 45일까지 표시 되도록 수정 (이승희 요청)
        /// </summary>
        private string[] DateArray = new string[45];
        private string[] DateArray2 = new string[45];

        public MAT070603()
        {
            InitializeComponent();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.udcWIPCondition1.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Enabled = false;
            SortInit();
            GridColumnInit();            
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material code", "MAT_CODE", "A.MAT_CODE", "MAT.MAT_CODE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material name", "MAT_CODE_DESC", "A.MAT_CODE_DESC", "MAT.MAT_CODE_DESC", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP CODE", "VENDOR_ID", "A.VENDOR_ID", "MAT.VENDOR_ID", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT_CMF_11", "A.MAT_CMF_11", "MAT.MAT_CMF_11", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "A.MAT_ID", "MAT.MAT_ID", false);
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            GetDayArray();
  
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer base", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Material code", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("MAT DESC", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("VENDOR", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Classification", 0, 5, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);

            if (cdvVersion.Text != "")
            {
                for (int i = 0; i <= 44; i++)
                {
                    spdData.RPT_AddBasicColumn(DateArray[i].ToString(), 0, 6 + i, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                }
            }

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
        }

        private void GetDayArray()
        {
            //DataTable dtMaxDate = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", "SELECT TO_CHAR(TO_DATE(MAX(PLAN_DATE), 'YYYYMMDD'), 'YYYY-MM-DD') AS PLAN_DATE FROM CMATPLNINP@RPTTOMES");            
            //DateTime Now = Convert.ToDateTime(dtMaxDate.Rows[0][0].ToString());

            if (cdvVersion.Text != "")
            {
                DateTime Now = Convert.ToDateTime(cdvVersion.Text);

                Now = Now.AddDays(1);

                for (int i = 0; i <= 44; i++)
                {
                    DateArray[i] = Now.ToString("MM-dd");
                    DateArray2[i] = Now.ToString("yyyyMMdd");
                    Now = Now.AddDays(1);
                }
            }
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!CheckField()) return;

            
            DataTable dt = null;

            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;

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

                spdData.RPT_AutoFit(false);

                //1. column header
                for (int i = 0; i <= 4; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }

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

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvVersion.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD063", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion

        #region MakeSqlString

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            //string QueryCond1 = null;
            //string QueryCond2 = null;
            //string QueryCond3 = null;
            string strKpcs = null;

            //udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            //QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            //QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            //QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
                
            if (ckbKpcs.Checked == true)
            {
                strKpcs = "1000";
            }
            else
            {
                strKpcs = "1";
            }            

            // 쿼리
            strSqlString.Append("SELECT B.MAT_GRP_1, B.MAT_CMF_11, A.MAT_ID, A.MAT_DESC, A.VENDOR_CODE, A.GUBUN" + "\n");            

            for (int i = 0; i <= 44; i++)
            {
                strSqlString.Append("     , ROUND(D" + i + " / " + strKpcs + ", 0) AS D" + i + "\n");
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, MAT_DESC, VENDOR_CODE, GUBUN" + "\n");

            for (int i = 0; i <= 44; i++)
            {
                strSqlString.Append("             , SUM(DECODE(PLAN_DATE, '" + DateArray2[i].ToString() + "', QTY, 0)) AS D" + i + "\n");
            }

            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MAT_ID, MAT_DESC, VENDOR_CODE, PLAN_DATE" + "\n");
            strSqlString.Append("                     , DECODE(RN, 1, 'PLAN', 2, 'SHIP', 3, 'ACT', 4, 'BAL') AS GUBUN " + "\n");
            strSqlString.Append("                     , DECODE(RN, 1, PLAN_QTY, 2, VSCM_QTY, 3, ACT_QTY, 4, DEF_QTY) AS QTY " + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT A.MAT_ID" + "\n");
            strSqlString.Append("                             , A.MAT_DESC" + "\n");
            strSqlString.Append("                             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = A.VENDOR_CODE) AS VENDOR_CODE" + "\n");
            strSqlString.Append("                             , A.PLAN_DATE" + "\n");
            strSqlString.Append("                             , NVL(A.PLAN_QTY,0) AS PLAN_QTY" + "\n");
            strSqlString.Append("                             , NVL(B.ACT_QTY,0) AS ACT_QTY" + "\n");
            strSqlString.Append("                             , NVL(C.VSCM_QTY,0) AS VSCM_QTY" + "\n");
            strSqlString.Append("                             , SUM(NVL(B.ACT_QTY,0) - NVL(A.PLAN_QTY,0)) OVER(PARTITION BY A.MAT_ID, A.VENDOR_CODE ORDER BY A.PLAN_DATE) AS DEF_QTY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT MAT_ID, MAT_DESC, VENDOR_CODE, PLAN_DATE, PLAN_QTY" + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT PLN.MAT_ID, MAT.MAT_DESC, PLN.VENDOR_CODE, PLN.PLAN_DATE" + "\n");
            strSqlString.Append("                                             , SUM(PLN.PLAN_QTY) AS PLAN_QTY" + "\n");
            strSqlString.Append("                                             , SUM(SUM(PLN.PLAN_QTY)) OVER(PARTITION BY PLN.MAT_ID, PLN.VENDOR_CODE) AS TTL_QTY" + "\n");
            strSqlString.Append("                                          FROM (" + "\n");
            //strSqlString.Append("                                                SELECT MAT_ID, VENDOR_CODE" + "\n");
            //strSqlString.Append("                                                     , RN AS DAY_SEQ" + "\n");
            //strSqlString.Append("                                                     , TO_CHAR(TO_DATE(SUBSTR(PLAN_DATE,1,6) || '01', 'YYYYMMDD') + RN - 1, 'YYYYMMDD') AS PLAN_DATE" + "\n");
            //strSqlString.Append("                                                     , CASE WHEN RN = 1 THEN D_1  WHEN RN = 2 THEN D_2  WHEN RN = 3 THEN D_3  WHEN RN = 4 THEN D_4  WHEN RN = 5 THEN D_5" + "\n");
            //strSqlString.Append("                                                            WHEN RN = 6 THEN D_6  WHEN RN = 7 THEN D_7  WHEN RN = 8 THEN D_8  WHEN RN = 9 THEN D_9  WHEN RN = 10 THEN D_10" + "\n");
            //strSqlString.Append("                                                            WHEN RN = 11  THEN D_11  WHEN RN = 12  THEN D_12  WHEN RN = 13  THEN D_13  WHEN RN = 14  THEN D_14  WHEN RN = 15  THEN D_15 " + "\n");
            //strSqlString.Append("                                                            WHEN RN = 16  THEN D_16  WHEN RN = 17  THEN D_17  WHEN RN = 18  THEN D_18  WHEN RN = 19  THEN D_19  WHEN RN = 20  THEN D_20 " + "\n");
            //strSqlString.Append("                                                            WHEN RN = 21  THEN D_21  WHEN RN = 22  THEN D_22  WHEN RN = 23  THEN D_23  WHEN RN = 24  THEN D_24  WHEN RN = 25  THEN D_25 " + "\n");
            //strSqlString.Append("                                                            WHEN RN = 26  THEN D_26  WHEN RN = 27  THEN D_27  WHEN RN = 28  THEN D_28  WHEN RN = 29  THEN D_29  WHEN RN = 30  THEN D_30 " + "\n");
            //strSqlString.Append("                                                            WHEN RN = 31  THEN D_31  WHEN RN = 32  THEN D_32  WHEN RN = 33  THEN D_33  WHEN RN = 34  THEN D_34  WHEN RN = 35  THEN D_35 " + "\n");
            //strSqlString.Append("                                                            WHEN RN = 36  THEN D_36  WHEN RN = 37  THEN D_37  WHEN RN = 38  THEN D_38  WHEN RN = 39  THEN D_39  WHEN RN = 40  THEN D_40 " + "\n");
            //strSqlString.Append("                                                            WHEN RN = 41  THEN D_41  WHEN RN = 42  THEN D_42  WHEN RN = 43  THEN D_43  WHEN RN = 44  THEN D_44  WHEN RN = 45  THEN D_45" + "\n");
            //strSqlString.Append("                                                            ELSE 0" + "\n");
            //strSqlString.Append("                                                       END PLAN_QTY" + "\n");
            //strSqlString.Append("                                                  FROM CMATPLNINP@RPTTOMES" + "\n");
            //strSqlString.Append("                                                     , (SELECT ROWNUM RN FROM DUAL CONNECT BY LEVEL <= 45) SEQ" + "\n");
            //strSqlString.Append("                                                 WHERE 1=1" + "\n");
            //strSqlString.Append("                                                   AND PLAN_DATE = '" + DateArray2[0].ToString() + "'" + "\n");

            strSqlString.Append("                                                SELECT MATNR AS MAT_ID, SUBSTR(LIFNR, -6) AS VENDOR_CODE" + "\n");
            strSqlString.Append("                                                     , RN AS DAY_SEQ" + "\n");
            strSqlString.Append("                                                     , TO_CHAR(TO_DATE(PDATE, 'YYYYMMDD') + RN, 'YYYYMMDD') AS PLAN_DATE" + "\n");
            strSqlString.Append("                                                     , CASE WHEN RN = 1 THEN D001  WHEN RN = 2 THEN D002  WHEN RN = 3 THEN D003  WHEN RN = 4 THEN D004  WHEN RN = 5 THEN D005" + "\n");
            strSqlString.Append("                                                            WHEN RN = 6 THEN D006  WHEN RN = 7 THEN D007  WHEN RN = 8 THEN D008  WHEN RN = 9 THEN D009  WHEN RN = 10 THEN D010" + "\n");
            strSqlString.Append("                                                            WHEN RN = 11  THEN D011  WHEN RN = 12  THEN D012  WHEN RN = 13  THEN D013  WHEN RN = 14  THEN D014 WHEN RN = 15  THEN D015" + "\n");
            strSqlString.Append("                                                            WHEN RN = 16  THEN D016  WHEN RN = 17  THEN D017  WHEN RN = 18  THEN D018  WHEN RN = 19  THEN D019  WHEN RN = 20  THEN D020 " + "\n");
            strSqlString.Append("                                                            WHEN RN = 21  THEN D021  WHEN RN = 22  THEN D022  WHEN RN = 23  THEN D023  WHEN RN = 24  THEN D024  WHEN RN = 25  THEN D025 " + "\n");
            strSqlString.Append("                                                            WHEN RN = 26  THEN D026  WHEN RN = 27  THEN D027  WHEN RN = 28  THEN D028  WHEN RN = 29  THEN D029  WHEN RN = 30  THEN D030 " + "\n");
            strSqlString.Append("                                                            WHEN RN = 31  THEN D031  WHEN RN = 32  THEN D032  WHEN RN = 33  THEN D033  WHEN RN = 34  THEN D034  WHEN RN = 35  THEN D035 " + "\n");
            strSqlString.Append("                                                            WHEN RN = 36  THEN D036  WHEN RN = 37  THEN D037  WHEN RN = 38  THEN D038  WHEN RN = 39  THEN D039  WHEN RN = 40  THEN D040 " + "\n");
            strSqlString.Append("                                                            WHEN RN = 41  THEN D041  WHEN RN = 42  THEN D042  WHEN RN = 43  THEN D043  WHEN RN = 44  THEN D044  WHEN RN = 45  THEN D045" + "\n");
            strSqlString.Append("                                                            ELSE 0" + "\n");
            strSqlString.Append("                                                       END PLAN_QTY" + "\n");
            strSqlString.Append("                                                  FROM ZHMMT200@SAPREAL" + "\n");
            strSqlString.Append("                                                     , (SELECT ROWNUM RN FROM DUAL CONNECT BY LEVEL <= 45) SEQ" + "\n");
            strSqlString.Append("                                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                                   AND PDATE = '" + cdvVersion.Text.Replace("-", "") + "'" + "\n");
            strSqlString.Append("                                               ) PLN" + "\n");
            strSqlString.Append("                                             , MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                                         WHERE 1=1" + "\n");
            strSqlString.Append("                                           AND PLN.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                                           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");

            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
                strSqlString.AppendFormat("                                           AND MAT.MAT_TYPE {0}" + "\n", cdvMatType.SelectedValueToQueryString);

            strSqlString.Append("                                           AND PLN.PLAN_DATE BETWEEN '" + DateArray2[0].ToString() + "' AND '" + DateArray2[44].ToString() + "'" + "\n");
            strSqlString.Append("                                         GROUP BY PLN.MAT_ID, MAT.MAT_DESC, PLN.VENDOR_CODE, PLN.PLAN_DATE" + "\n");
            strSqlString.Append("                                       )" + "\n");
            strSqlString.Append("                                 WHERE TTL_QTY > 0 " + "\n");
            strSqlString.Append("                                 ORDER BY MAT_ID, MAT_DESC, VENDOR_CODE, PLAN_DATE " + "\n");
            strSqlString.Append("                               ) A" + "\n");
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT MAT_ID " + "\n");
            strSqlString.Append("                                     , VENDOR_CODE " + "\n");
            strSqlString.Append("                                     , BU_DATE" + "\n");
            strSqlString.Append("                                     , SUM(NVL(QUANTITY,0)) AS ACT_QTY " + "\n");
            strSqlString.Append("                                 FROM CWMSLOTHIS@RPTTOMES " + "\n");
            strSqlString.Append("                                WHERE MOVEMENT_CODE IN ('A01','A02','A31') " + "\n");
            strSqlString.Append("                                  AND BU_DATE BETWEEN '" + DateArray2[0].ToString() + "' AND '" + DateArray2[44].ToString() + "'" + "\n");
            strSqlString.Append("                                  AND RESV_FIELD_3 = ' '   " + "\n");
            strSqlString.Append("                                GROUP BY BU_DATE, MAT_ID, VENDOR_CODE " + "\n");
            strSqlString.Append("                               ) B" + "\n");
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT MATNR AS MAT_ID, SUBSTR(LIFNR, -6) AS VENDOR_CODE, ZGIDT, SUM(ZINVQTY) AS VSCM_QTY" + "\n");
            strSqlString.Append("                                  FROM ZHMMT017@SAPREAL" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND ZGIDT BETWEEN '" + DateArray2[0].ToString() + "' AND '" + DateArray2[44].ToString() + "'" + "\n");
            strSqlString.Append("                                 GROUP BY MATNR, LIFNR, ZGIDT" + "\n");
            strSqlString.Append("                               ) C" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.VENDOR_CODE = B.VENDOR_CODE(+)" + "\n");
            strSqlString.Append("                           AND A.VENDOR_CODE = C.VENDOR_CODE(+)" + "\n");
            strSqlString.Append("                           AND A.PLAN_DATE = B.BU_DATE(+)" + "\n");
            strSqlString.Append("                           AND A.PLAN_DATE = C.ZGIDT(+) " + "\n");

            if (txtMatCode.Text.Trim() != "%" && txtMatCode.Text.Trim() != "")
            {
                strSqlString.Append("                           AND A.MAT_ID LIKE '" + txtMatCode.Text + "'" + "\n");
            }

            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                     , (SELECT ROWNUM RN FROM DUAL CONNECT BY LEVEL <= 4)" + "\n");
            strSqlString.Append("               )             " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID, MAT_DESC, VENDOR_CODE, GUBUN  " + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , (" + "\n");            
            strSqlString.Append("        SELECT A.MATCODE, A.MAT_GRP_1, B.MAT_CMF_11" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MATCODE, WM_CONCAT(MAT_GRP_1) AS MAT_GRP_1" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT DISTINCT B.MATCODE, A.MAT_GRP_1" + "\n");
            strSqlString.Append("                          FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                             , CWIPBOMDEF B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.PARTNUMBER" + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND B.RESV_FLAG_1 = 'Y'   " + "\n");
            //strSqlString.Append("                           AND B.RESV_FIELD_2 IN ('PB')" + "\n");

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.Append("                           AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
            #endregion

            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MATCODE " + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MATCODE, WM_CONCAT(MAT_CMF_11) AS MAT_CMF_11" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT DISTINCT B.MATCODE, A.MAT_CMF_11" + "\n");
            strSqlString.Append("                          FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                             , CWIPBOMDEF B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.PARTNUMBER" + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND B.RESV_FLAG_1 = 'Y'   " + "\n");
            //strSqlString.Append("                           AND B.RESV_FIELD_2 IN ('PB')" + "\n");

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.Append("                           AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
            #endregion

            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MATCODE" + "\n");
            strSqlString.Append("               ) B" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.MATCODE = B.MATCODE(+) " + "\n");
            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");

            // 제품 검색이 한개라도 존재하면...
            if ((txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "") || (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "") || (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "") ||
                (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "") || (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "") || (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "") ||
                (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "") || (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "") || (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "") || (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != ""))
            {
                strSqlString.Append("   AND A.MAT_ID = B.MATCODE" + "\n");
            }
            else
            {
                strSqlString.Append("   AND A.MAT_ID = B.MATCODE(+)" + "\n");
            }

            strSqlString.Append(" ORDER BY MAT_GRP_1, MAT_CMF_11, MAT_ID, MAT_DESC, VENDOR_CODE, DECODE(GUBUN, 'PLAN', 1, 'SHIP', 2, 'ACT', 3, 4)" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #endregion

        #region Event Handler

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
               
        #endregion

        private void cdvVersion_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            //strQuery += "SELECT PLAN_DATE AS Code, '' AS Data" + "\n";
            //strQuery += "  FROM ( " + "\n";
            //strQuery += "        SELECT TO_CHAR(TO_DATE(PLAN_DATE, 'YYYYMMDD'), 'YYYY-MM-DD') AS PLAN_DATE " + "\n";
            //strQuery += "             , ROW_NUMBER() OVER(ORDER BY PLAN_DATE DESC) AS RK " + "\n";
            //strQuery += "          FROM CMATPLNINP@RPTTOMES " + "\n";
            //strQuery += "         WHERE 1=1 " + "\n";
            //strQuery += "           AND PLAN_DATE > TO_CHAR(ADD_MONTHS(SYSDATE,-3), 'YYYYMMDD') " + "\n";
            //strQuery += "         GROUP BY PLAN_DATE " + "\n";
            //strQuery += "       ) " + "\n";
            //strQuery += " WHERE RK <= 10 " + "\n";            
            //strQuery += " ORDER BY PLAN_DATE DESC" + "\n";

            // 2016-07-07-임종우 : 엑셀 업로드 방식에서 VSCM 데이터 DB Link 방식으로 변경
            strQuery += "SELECT PLAN_DATE AS Code, '' AS Data" + "\n";
            strQuery += "  FROM ( " + "\n";
            strQuery += "        SELECT TO_CHAR(TO_DATE(PDATE, 'YYYYMMDD'), 'YYYY-MM-DD') AS PLAN_DATE " + "\n";
            strQuery += "             , ROW_NUMBER() OVER(ORDER BY PDATE DESC) AS RK " + "\n";
            strQuery += "          FROM ZHMMT200@SAPREAL " + "\n";
            strQuery += "         WHERE 1=1 " + "\n";
            strQuery += "           AND PDATE > TO_CHAR(ADD_MONTHS(SYSDATE,-1), 'YYYYMMDD') " + "\n";
            strQuery += "           AND TO_CHAR(TO_DATE(PDATE, 'YYYYMMDD'), 'DY') IN ('월','목') " + "\n";
            strQuery += "         GROUP BY PDATE " + "\n";
            strQuery += "       ) " + "\n";
            strQuery += " WHERE RK <= 4 " + "\n";
            strQuery += " ORDER BY PLAN_DATE DESC" + "\n"; 

            cdvVersion.sDynamicQuery = strQuery;
        }

        private void cdvMatType_ValueButtonPress(object sender, EventArgs e)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT KEY_1 AS Code, DATA_1 AS Data " + "\n");
            strSqlString.Append("  FROM MGCMTBLDAT " + "\n");
            strSqlString.Append(" WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("   AND TABLE_NAME = 'MATERIAL_TYPE' " + "\n");
            strSqlString.Append("   AND KEY_1 IN ('TE','PB','LF') " + "\n");
            strSqlString.Append(" ORDER BY KEY_1 " + "\n");

            cdvMatType.sDynamicQuery = strSqlString.ToString();
        }

    }
}

