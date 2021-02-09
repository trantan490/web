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
    public partial class PRD010409 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010409<br/>
        /// 클래스요약: TEST 고객사 정산<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2009-06-17<br/>
        /// 상세  설명: TEST 고객사 정산<br/>
        /// 변경  내용: <br/>
        /// 2009-08-06-김준용 : Group 조건 추가        
        /// 2009-09-15-임종우 : FCI 고객사 추가
        /// 2010-05-10-임종우 : Test End(T0100) 삭제되었던 정보 표시(위철호K 요청)
        /// 2010-09-01-임종우 : 엑셀 저장으로 변경(조윤진 요청)
        /// 2011-04-06-김민우 : LOT_TYPE 구분 컬럼 추가(CWIPTSTHIS테이블의 LOT_CMF_5 컬럼 사용)        
        /// 2011-12-26-임종우 : MWIPCALDEF 의 작년,올해 마지막 주차 겹치는 에러 발생으로 SYS_YEAR -> PLAN_YEAR 으로 변경
        /// 2012-01-25-김민우 : 현재일의 해당 주차에서 시작일 구하기 쿼리 속도가 너무 늦어 에러 발생으로 인한 쿼리 수정
        /// 2013-12-17-임종우 : NMI 추가 (박종혁 요청)
        /// 2014-08-08-임종우 : Freescale 추가 (박종혁D 요청)
        ///                   : Test In 존재 하지 않으면 Test Out + Loss 로 표시 되도록 수정
        /// 2014-09-17-임종우 : FINAL TEST LOSS 추가
        /// 2014-11-07-임종우 : IML 을 제외한 업체의 PO_NO 는 LOT_CMF_3 사용
        /// </summary>
        public PRD010409()
        {
            InitializeComponent();
            OptionInIt(); // 초기화
            SortInit();
            GridColumnInit(); //헤더 한줄짜리 
        }

        /// <summary>
        /// 
        /// </summary>
        private void OptionInIt()
        {
            DataTable dt1 = null;

            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());

            cdvFromToDate.AutoBinding(dt1.Rows[0][0].ToString(), DateTime.Now.ToString());

            //cdvFromToDate.AutoBinding(DateTime.Now.AddDays(-2).ToString(), DateTime.Now.AddDays(-1).ToString());
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 as Family", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 as PKG", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 as Type1", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 as Type2", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 as LEAD", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 as Density", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 as GEN", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PO NO", "PO_NO", "PO_NO", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Invoice NO", "NVL(MESMGR.F_GET_IML_INVOICENO@RPTTOMES(LOTHIS.LOT_ID),'N/A') AS INVOICENO", "' '", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RUN ID", "RUN_ID", "RUN_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "LOTHIS.MAT_ID AS PRODUCT", "LOTHIS.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LOT ID", "LOTHIS.LOT_ID AS LOT_ID", "LOTHIS.MAT_ID", true);            
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Family", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Package", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type1", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density", 0, 5, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("PO NO", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Invoice NO", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Run ID", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Pin Type", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Lot ID", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("Delete Time", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("DA Out", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("PVI Out", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Assembly Loss", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("TEST In", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("TEST Out", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("TEST Loss", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("LOT TYPE", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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
            //if (!CheckField()) return;

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

                //spdData.DataSource = dt;

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(Convert.ToInt16(txtSubTotal.Text));

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                spdData.Sheets[0].FrozenColumnCount = 12;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

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

        #endregion

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private Boolean CheckField()
        //{
        //if (cdvFactory.Text.TrimEnd() == "")
        //{
        //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
        //    return false;
        //}

        //return true;
        //}

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            string QueryCond1 = null;
            string QueryCond2 = null;

            //string sStart_Tran_Time = null;
            //string sEnd_Tran_Time = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            StringBuilder strSqlString = new StringBuilder();
            strSqlString.AppendFormat("SELECT  {0}  " + "\n", QueryCond1);
            //strSqlString.Append("SELECT MAT.MAT_CMF_10 AS PIN_TYPE, " + "\n");
            //strSqlString.Append("       MAT.MAT_ID AS PRODUCT, " + "\n");
            //strSqlString.Append("       LOT_ID, " + "\n");

            //2010-05-10-임종우 : Test End(T0100) 삭제되었던 정보 표시(위철호K 요청)
            strSqlString.Append("     , NVL(MESMGR.F_GET_END_DEL_TIME@RPTTOMES(LOTHIS.LOT_ID,'*'), ' ' ) " + "\n");

            strSqlString.Append("     , DA_OUT " + "\n");
            strSqlString.Append("     , PVI_OUT " + "\n");
            strSqlString.Append("     , ASSAY_LOSS " + "\n");
            //strSqlString.Append("     , TEST_IN " + "\n");
            strSqlString.Append("     , CASE WHEN TEST_IN IS NULL THEN NVL(TEST_OUT,0) + NVL(FINAL_TEST_LOSS,0) ELSE TEST_IN END TEST_IN " + "\n");
            strSqlString.Append("     , TEST_OUT " + "\n");
            strSqlString.Append("     , TEST_Loss " + "\n");
            strSqlString.Append("     , LOT_CMF_5 " + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT HIS.PO_NO " + "\n");
            strSqlString.Append("             , HIS.RUN_ID " + "\n");
            strSqlString.Append("             , HIS.TEST_END_LOT_ID AS LOT_ID " + "\n");
            strSqlString.Append("             , HIS.MAT_ID" + "\n");
            strSqlString.Append("             , HIS.LOT_CMF_5" + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT SUM(QTY_1) " + "\n");
            strSqlString.Append("                  FROM CWIPTSTHIS@RPTTOMES " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND OLD_OPER LIKE 'A040%' " + "\n");
            strSqlString.Append("                   AND TRAN_CODE = 'END' " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND TEST_END_LOT_ID = HIS.TEST_END_LOT_ID " + "\n");
            strSqlString.Append("               ) \"DA_OUT\" " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT SUM(QTY_1) " + "\n");
            strSqlString.Append("                  FROM CWIPTSTHIS@RPTTOMES " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
			strSqlString.Append("                   AND OLD_OPER = 'AZ010' " + "\n");
			strSqlString.Append("                   AND TRAN_CODE = 'SHIP' " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND TEST_END_LOT_ID = HIS.TEST_END_LOT_ID " + "\n");
            strSqlString.Append("               ) \"PVI_OUT\" " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT SUM(OLD_QTY_1 - QTY_1) " + "\n");
            strSqlString.Append("                  FROM CWIPTSTHIS@RPTTOMES " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND TRAN_CODE = 'LOSS' " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND TEST_END_LOT_ID = HIS.TEST_END_LOT_ID" + "\n");
            strSqlString.Append("               ) \"ASSAY_LOSS\"" + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT SUM(QTY_1) " + "\n");
            strSqlString.Append("                  FROM CWIPTSTHIS@RPTTOMES " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");

            // 2010-07-19-김준용 : T0400 을 지나는 제품이 있음
            strSqlString.Append("                   AND OLD_OPER IN ('T0100','T0400') " + "\n");
            strSqlString.Append("                   AND TRAN_CODE = 'START' " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND TEST_END_LOT_ID = HIS.TEST_END_LOT_ID " + "\n");
            strSqlString.Append("               ) \"TEST_IN\" " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT SUM(QTY_1) " + "\n");
            strSqlString.Append("                  FROM CWIPTSTHIS@RPTTOMES " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            
            // 2010-07-19-김준용 : T0400 을 지나는 제품이 있음
            strSqlString.Append("                   AND OLD_OPER IN ('T0100','T0400') " + "\n");
            strSqlString.Append("                   AND TRAN_CODE = 'END' " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND TEST_END_LOT_ID = HIS.TEST_END_LOT_ID " + "\n");
            strSqlString.Append("               ) \"TEST_OUT\" " + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT SUM(OLD_QTY_1 - QTY_1) " + "\n");
            strSqlString.Append("                  FROM CWIPTSTHIS@RPTTOMES " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND TRAN_CODE = 'LOSS' " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");            
            strSqlString.Append("                   AND TEST_END_LOT_ID = HIS.TEST_END_LOT_ID" + "\n");
            strSqlString.Append("               ) \"TEST_LOSS\" " + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT SUM(OLD_QTY_1 - QTY_1) " + "\n");
            strSqlString.Append("                  FROM CWIPTSTHIS@RPTTOMES " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND TRAN_CODE = 'LOSS' " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND OPER IN ('T0100','T0400') " + "\n");
            strSqlString.Append("                   AND TEST_END_LOT_ID = HIS.TEST_END_LOT_ID" + "\n");
            strSqlString.Append("               ) \"FINAL_TEST_LOSS\"" + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID,TEST_END_LOT_ID" + "\n");
            strSqlString.Append("                     , CASE WHEN LOT_CMF_2 = 'IM' THEN NVL(PO.PO_NO,'N/A')" + "\n");
            strSqlString.Append("                            ELSE NVL(LOT_CMF_3,'N/A')" + "\n");
            strSqlString.Append("                       END AS PO_NO" + "\n");
            strSqlString.Append("                     , LOT_CMF_4 AS RUN_ID, LOT_CMF_5" + "\n");
            strSqlString.Append("                  FROM CWIPTSTHIS@RPTTOMES HIS" + "\n");
            strSqlString.Append("                     , CWIPCUSORD@RPTTOMES PO" + "\n");
			strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND HIS.LOT_CMF_4 = PO.RUN_ID(+) " + "\n");
            strSqlString.Append("                   AND TRAN_CODE='END' " + "\n");
            
            // 2010-07-19-김준용 : T0400 을 지나는 제품이 있음
            strSqlString.Append("                   AND OLD_OPER IN ('T0400','T0100') " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND TEST_END_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "' " + "\n");
            strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + txtLotType.Text + "' " + "\n");

            if (chkReTest.Checked == true) strSqlString.Append("                   AND LOT_CMF_4 LIKE 'R%' " + "\n");

            // 고객사 선택 (2009.09.15 임종우)
            if (rdbIML.Checked == true)
            {
                strSqlString.Append("                   AND LOT_CMF_2 = 'IM' " + "\n");
            }
            else if (rdbNMI.Checked == true)
            {
                strSqlString.Append("                   AND LOT_CMF_2 = 'NM' " + "\n");
            }
            else
            {
                strSqlString.Append("                   AND LOT_CMF_2 = 'FS' " + "\n");
            }
            
            strSqlString.Append("               ) HIS " + "\n");
            strSqlString.Append("         GROUP BY PO_NO,RUN_ID,HIS.MAT_ID,TEST_END_LOT_ID, LOT_CMF_5  " + "\n");
            strSqlString.Append("       ) LOTHIS " + "\n");
            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND LOTHIS.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            //if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //    strSqlString.AppendFormat("                          AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //    strSqlString.AppendFormat("                          AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //    strSqlString.AppendFormat("                          AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //    strSqlString.AppendFormat("                          AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //    strSqlString.AppendFormat("                          AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //    strSqlString.AppendFormat("                          AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //    strSqlString.AppendFormat("                          AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //    strSqlString.AppendFormat("                          AND LOTHIS.MAT_ID {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);


            strSqlString.AppendFormat(" ORDER BY {0} " + "\n",QueryCond2);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // 현재일의 해당 주차에서 시작일 구하기
        private string MakeSqlString2()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT TO_CHAR(TO_DATE(MIN(SYS_DATE),'YYYYMMDD'),'YYYY-MM-DD') " + "\n");
            strSqlString.Append("  FROM MWIPCALDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND CALENDAR_ID='HM' " + "\n");
            //strSqlString.Append("   AND PLAN_YEAR=SUBSTR(GET_WORK_DATE(TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'),'W'),1,4) " + "\n");
            //strSqlString.Append("   AND PLAN_WEEK=SUBSTR(GET_WORK_DATE(TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'),'W'),5,2) " + "\n");
            //2012-01-25-김민우: 기존 쿼리 속도가 너무 늦어 에러 발생으로 인한 쿼리 수정
            strSqlString.Append("   AND PLAN_YEAR=(SELECT PLAN_YEAR " + "\n");
            strSqlString.Append("                    FROM MWIPCALDEF " + "\n");
            strSqlString.Append("                   WHERE 1=1 " + "\n");
            strSqlString.Append("                     AND CALENDAR_ID='HM' " + "\n");
            strSqlString.Append("                     AND SYS_DATE=TO_CHAR(SYSDATE,'YYYYMMDD'))" + "\n");
            strSqlString.Append("   AND PLAN_WEEK=(SELECT PLAN_WEEK " + "\n");
            strSqlString.Append("                    FROM MWIPCALDEF " + "\n");
            strSqlString.Append("                   WHERE 1=1 " + "\n");
            strSqlString.Append("                     AND CALENDAR_ID='HM' " + "\n");
            strSqlString.Append("                     AND SYS_DATE=TO_CHAR(SYSDATE,'YYYYMMDD'))" + "\n");


            
            

            return strSqlString.ToString();

        }


        #endregion

        #region ToExcel

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage));
                return;
            }

            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();
        }
        #endregion

        private void txtProduct_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    btnView_Click(sender, e);
            //}
        }

        private void PRD010409_Load_1(object sender, EventArgs e)
        {
            //txtProduct.Focus();
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            //this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }
    }
}

