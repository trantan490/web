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
    public partial class PRD011003 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011003<br/>
        /// 클래스요약: SAP DATA<br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2012-01-27<br/>
        /// 상세  설명: Retest 이력<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: <br />
        /// 
        /// 2011-11-30-김민우 : 샘플
        /// </summary>

        public PRD011003()
        {
            InitializeComponent();
            this.cdvFactory.sFactory = GlobalVariable.gsTestDefaultFactory;
            cdvFromToDate.AutoBinding();
            SortInit();           
            GridColumnInit();
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;
        }

       
        #region 유효성 검사 및 초기화

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
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Vendor description", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Pintype", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);            
            spdData.RPT_AddBasicColumn("SAP code", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("MES Part No", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("IN", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("OUT", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("LOSS", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("CV", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
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
            string QueryCond4 = null;

            string sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
            string sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();            
            
            string bbbb = string.Empty;
                                    
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;
            
            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond4);
            strSqlString.Append("      , A.OPER,A.RUN_ID,A.LOT_ID,B.RETEST_COUNT,A.PGM,A.IN_TIME, A.OUT_TIME, A.END_RES_ID " + "\n");
            strSqlString.Append("      , A.USER_ID, B.RETEST_INQTY,B.RETEST_GOODQTY,B.RETEST_REJECTQTY,B.RETEST_YIELD " + "\n");
            strSqlString.Append("   FROM ( " + "\n");
            strSqlString.AppendFormat("         SELECT {0} " + "\n", QueryCond3);
            strSqlString.Append("              , OPER, RUN_ID, LOT_ID, LOT_TYPE, PGM " + "\n");
            strSqlString.Append("              , IN_TIME, OUT_TIME, END_RES_ID " + "\n");
            strSqlString.Append("              , (SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID = TRAN_USER_ID) || '(' || TRAN_USER_ID || ')' AS USER_ID " + "\n");
            strSqlString.Append("              , IN_QTY, OUT_QTY, TOTAL_LOSS " + "\n");
            strSqlString.Append("              , ROUND(OUT_QTY / IN_QTY * 100, 2) AS YIELD " + "\n");
            strSqlString.Append("           FROM ( " + "\n");
            strSqlString.AppendFormat("                 SELECT {0} " + "\n", QueryCond2);
            strSqlString.Append("                      , A.OPER" + "\n");
            strSqlString.Append("                      , A.RUN_ID" + "\n");
            strSqlString.Append("                      , A.LOT_ID" + "\n");
            strSqlString.Append("                      , A.LOT_CMF_5 AS LOT_TYPE" + "\n");
            strSqlString.Append("                      , A.IN_TIME " + "\n");
            strSqlString.Append("                      , A.OUT_TIME" + "\n");
            strSqlString.Append("                      , A.END_RES_ID " + "\n");
            strSqlString.Append("                      , A.TRAN_USER_ID" + "\n");
            strSqlString.Append("                      , B.TOTAL_LOSS + A.OUT_QTY AS IN_QTY" + "\n");
            strSqlString.Append("                      , A.OUT_QTY " + "\n");
            strSqlString.Append("                      , B.LOSS_CODE" + "\n");
            strSqlString.Append("                      , B.LOSS_QTY  " + "\n");
            strSqlString.Append("                      , B.TOTAL_LOSS  " + "\n");
            strSqlString.Append("                      , A.PGM AS PGM " + "\n");
            strSqlString.Append("                   FROM (  " + "\n");
            strSqlString.Append("                         SELECT OLD_OPER AS OPER, MAT_ID, LOT_ID " + "\n");
            strSqlString.Append("                              , LOT_CMF_5, OLD_OPER_IN_TIME AS IN_TIME, TRAN_TIME AS OUT_TIME " + "\n");
            strSqlString.Append("                              , END_RES_ID, TRAN_USER_ID, QTY_1 AS OUT_QTY " + "\n");
            strSqlString.Append("                              , TRAN_CMF_3 AS PGM " + "\n");
            strSqlString.Append("                              , LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                           FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("                          WHERE OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                            AND MAT_VER = 1  " + "\n");

            if (cmbOper.Text.Equals("T0100"))
            {
                strSqlString.Append("                            AND OLD_OPER = 'T0100'" + "\n");
            }
            else if (cmbOper.Text.Equals("T0400"))
            {
                strSqlString.Append("                            AND OLD_OPER = 'T0400'" + "\n");
            }
            else 
            {
                strSqlString.Append("                            AND OLD_OPER IN ('T0100','T0400')" + "\n");
            }
            //strSqlString.Append("                            AND OLD_OPER IN ('T0100','T0400')" + "\n");
            strSqlString.Append("                            AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                            AND TRAN_CODE IN ('END','SHIP') " + "\n");
            strSqlString.AppendFormat("                            AND TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            strSqlString.Append("                        ) A " + "\n");
            strSqlString.Append("                      , (         " + "\n");
            strSqlString.Append("                         SELECT LOT_ID, OPER, LOSS_CODE, LOSS_QTY, SUM(LOSS_QTY) OVER(PARTITION BY LOT_ID, OPER) AS TOTAL_LOSS " + "\n");
            strSqlString.Append("                           FROM RWIPLOTLSM  " + "\n");
            strSqlString.Append("                          WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                            AND MAT_VER = 1  " + "\n");
            
            if (cmbOper.Text.Equals("T0100"))
            {
                strSqlString.Append("                            AND OPER = 'T0100'" + "\n");
            }
            else if (cmbOper.Text.Equals("T0400"))
            {
                strSqlString.Append("                            AND OPER = 'T0400'" + "\n");
            }
            else 
            {
                strSqlString.Append("                            AND OPER IN ('T0100','T0400')" + "\n");
            }
            
            
            //strSqlString.Append("                            AND OPER IN ('T0100','T0400')"  + "\n");
            strSqlString.Append("                            AND HIST_DEL_FLAG = ' '   " + "\n");
            strSqlString.Append("                        ) B " + "\n");
            strSqlString.Append("                      , (   " + "\n");
            strSqlString.Append("                         SELECT ATTR_KEY,ATTR_VALUE  " + "\n");
            strSqlString.Append("                           FROM MATRNAMSTS  " + "\n");
            strSqlString.Append("                          WHERE FACTORY ='" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                            AND ATTR_TYPE = 'MAT_TEST_PGM' " + "\n");
            strSqlString.Append("                            AND ATTR_NAME = 'TEST_PROGRAM'  " + "\n");
            strSqlString.Append("                        ) C " + "\n");
            strSqlString.Append("                        , MWIPMATDEF D " + "\n");
            strSqlString.Append("                  WHERE 1 = 1 " + "\n");
            strSqlString.Append("                    AND A.LOT_ID = B.LOT_ID " + "\n");
            strSqlString.Append("                    AND A.OPER = B.OPER " + "\n");
            strSqlString.Append("                    AND A.MAT_ID = C.ATTR_KEY(+) " + "\n");
            strSqlString.Append("                    AND A.MAT_ID = D.MAT_ID " + "\n");
            strSqlString.Append("                    AND D.FACTORY  = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                    AND LOSS_CODE LIKE '%' " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("            AND D.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("            AND D.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("            AND D.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("            AND D.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("            AND D.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("            AND D.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("            AND D.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("            AND D.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("            AND D.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("                 ) DAT" + "\n");
            strSqlString.AppendFormat("                 GROUP BY {0}, OPER, RUN_ID, LOT_ID, LOT_TYPE, PGM, IN_TIME, OUT_TIME, END_RES_ID, TRAN_USER_ID, IN_QTY, OUT_QTY, TOTAL_LOSS " + "\n", QueryCond1);
            //strSqlString.AppendFormat("                 ORDER BY {0}, OPER, RUN_ID, LOT_ID, LOT_TYPE, PGM, IN_TIME, OUT_TIME, END_RES_ID, TRAN_USER_ID, IN_QTY, OUT_QTY, TOTAL_LOSS " + "\n", QueryCond1);
            strSqlString.Append("                ) A" + "\n");
            //strSqlString.Append("               , MWIPLOTRTT@RPTTOMES B" + "\n");
            strSqlString.Append("               , (SELECT LOT_ID, OPER,TRAN_TIME" + "\n");
            strSqlString.Append("                       , DECODE(G_CODE,'00111110','TOTAL',RETEST_COUNT) AS RETEST_COUNT" + "\n");
            strSqlString.Append("                       , DECODE(G_CODE,'00111110',MAX(RETEST_INQTY) OVER(PARTITION BY LOT_ID,TRAN_TIME),RETEST_INQTY) AS RETEST_INQTY" + "\n");
            strSqlString.Append("                       , DECODE(G_CODE,'00111110',SUM(RETEST_GOODQTY) OVER(PARTITION BY LOT_ID,TRAN_TIME),RETEST_GOODQTY) AS RETEST_GOODQTY" + "\n");
            strSqlString.Append("                       , DECODE(G_CODE,'00111110',MIN(RETEST_REJECTQTY) OVER(PARTITION BY LOT_ID,TRAN_TIME),RETEST_REJECTQTY) AS RETEST_REJECTQTY" + "\n");
            strSqlString.Append("                       , DECODE(G_CODE,'00111110',(ROUND(SUM(RETEST_GOODQTY) OVER(PARTITION BY LOT_ID,TRAN_TIME)/MAX(RETEST_INQTY) OVER(PARTITION BY LOT_ID,TRAN_TIME) * 100,2)), RETEST_YIELD) AS RETEST_YIELD" + "\n");
            strSqlString.Append("                    FROM (" + "\n");
            strSqlString.Append("                          SELECT LOT_ID,OPER,RETEST_COUNT,RETEST_INQTY,RETEST_GOODQTY,RETEST_REJECTQTY,RETEST_YIELD, TRAN_TIME" + "\n");
            strSqlString.Append("                               , GROUPING(LOT_ID)||GROUPING(OPER)||GROUPING(RETEST_COUNT)||GROUPING(RETEST_INQTY)||GROUPING(RETEST_GOODQTY)||GROUPING(RETEST_REJECTQTY)||GROUPING(RETEST_YIELD)||GROUPING(TRAN_TIME) AS G_CODE" + "\n");
            strSqlString.Append("                            FROM MWIPLOTRTT@RPTTOMES B" + "\n");
            strSqlString.Append("                           WHERE 1=1" + "\n");
            strSqlString.Append("                             AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");

            if (cmbOper.Text.Equals("T0100"))
            {
                strSqlString.Append("                             AND OPER = 'T0100'" + "\n");
            }
            else if (cmbOper.Text.Equals("T0400"))
            {
                strSqlString.Append("                             AND OPER = 'T0400'" + "\n");
            }
            else
            {
                strSqlString.Append("                             AND OPER IN ('T0100','T0400')" + "\n");
            }
            
            //strSqlString.Append("                             AND OPER IN ('T0100','T0400')" + "\n");
            strSqlString.Append("                             AND HIST_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                             AND RETEST_INQTY <> 0" + "\n");
            strSqlString.Append("                         GROUP BY ROLLUP(LOT_ID,TRAN_TIME,OPER,RETEST_COUNT,RETEST_INQTY,RETEST_GOODQTY,RETEST_REJECTQTY,RETEST_YIELD)" + "\n");
            strSqlString.Append("                   )  " + "\n");
            strSqlString.Append("                    WHERE G_CODE IN ('00000000','00111110') " + "\n");
            //strSqlString.Append("                   ORDER BY TRAN_TIME,DECODE(RETEST_COUNT,'1차',1,'2차',2,'3차',3,'4차',4,'5차',5,'TOTAL',6) " + "\n");
            strSqlString.Append("                   ) B " + "\n");



            strSqlString.Append("    WHERE 1=1" + "\n");
            strSqlString.Append("      AND A.LOT_ID = B.LOT_ID" + "\n");
            strSqlString.Append("      AND A.OPER = B.OPER" + "\n");
            if (txtSearchProduct.Text != "" && txtSearchProduct.Text != "%")
            {
                strSqlString.Append("      AND A.MAT_ID(+) LIKE '" + txtSearchProduct.Text.Trim() + "'" + "\n");
            }
            strSqlString.AppendFormat("    ORDER BY {0}, OPER,RUN_ID,LOT_ID,B.TRAN_TIME,DECODE(RETEST_COUNT,'1차',1,'2차',2,'3차',3,'4차',4,'5차',5,'TOTAL',6) " + "\n", QueryCond4);
            
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
            spdData_Sheet1.RowCount = 0;
            
            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);               
                this.Refresh();

                GridColumnInit();
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;

                /*
                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 9, null, null, btnSort);

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);
                */
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
            this.SetFactory(cdvFactory.txtValue);                        
        }
        #endregion
    }
}
