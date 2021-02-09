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

/****************************************************
 * Comment : 제품별 공정불량현황
 *
 * Created By : bee-jae jung (2010-06-05-토요일)
 *
 * Modified By : bee-jae jung (2010-06-05-토요일)
 ****************************************************/

namespace Hana.PQC
{
    public partial class PQC030115 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PQC030115 : Program Initial "

        public PQC030115()
        {
            InitializeComponent();
			fnSSInitial(SS01);
			fnSSSortInit();
			this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
			cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
			cdvBaseDate.Value = DateTime.Today;
		}

        #endregion
        

        #region " Common Function "

		private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
		{
			/****************************************************
			 * Comment : SS의 Header를 설정한다.
			 * 
			 * Created By : bee-jae jung(2010-06-05-토요일)
			 * 
			 * Modified By : bee-jae jung(2010-06-05-토요일)
			 ****************************************************/
			int iindex = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				SS.RPT_ColumnInit();
				SS.RPT_AddBasicColumn("Customer", 0, iindex + 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
				SS.RPT_AddBasicColumn("PACKAGE", 0, iindex + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
				SS.RPT_AddBasicColumn("Operation", 0, iindex + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
				SS.RPT_AddBasicColumn("Defect Code", 0, iindex + 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
				SS.RPT_AddBasicColumn("MM-2", 0, iindex + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("MM-1", 0, iindex + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("MM-0", 0, iindex + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("ww-3", 0, iindex + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("ww-2", 0, iindex + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("ww-1", 0, iindex + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("ww-0", 0, iindex + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("Fri.", 0, iindex + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("Sat.", 0, iindex + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("일", 0, iindex + 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("월", 0, iindex + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("Tue.", 0, iindex + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("Wed.", 0, iindex + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
				SS.RPT_AddBasicColumn("Thur.", 0, iindex + 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
				return;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void fnSSSortInit()
		{
			/****************************************************
			 * Comment : SS의 데이터의 정렬규칙을 설정하다.
			 * 
			 * Created By : bee-jae jung(2010-05-31-월요일)
			 * 
			 * Modified By : bee-jae jung(2010-05-31-월요일)
			 ****************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
				return;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		// 2010-07-06-정비재 : Oracle Procedure / Package를 이용하여 데이터를 조회하는 함수
        private bool fnDataFind()
        {
            /****************************************************
             * Comment : DataBase에 저장된 데이터를 조회한다.
             * 
             * Created By : bee-jae jung(2010-06-05-토요일)
             * 
             * Modified By : bee-jae jung(2010-08-12-목요일)
             ****************************************************/
            String sPackage = "", sParameter = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                // 2010-06-24-정비재 : Sheet / Listview를 초기화 한다.
                CmnInitFunction.ClearList(SS01, true);

                if (rbS01.Checked == true)
                {
                    // 2010-08-12-정비재 : PPM
                    sPackage = "PACKAGE_PQC030115.PROC_PQC030115_PPM";
                    sParameter = "P_FACTORY:" + cdvFactory.Text
                            + "│ P_BASE_MONTH:" + cdvBaseDate.Value.ToString("yyyyMM")
                            + "│ P_BASE_TIME:" + cdvBaseDate.Value.ToString("yyyyMMdd")
                            + "│ P_CUSTOMER:" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "%"
                            + "│ P_PACKAGE:" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "%"
                            + "│ P_OPER:" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "%"
                            + "│ P_PREV_FLAG:" + (chkPrevFlag.Checked == true ? 'Y' : 'N');
                }
                else if (rbS02.Checked == true)
                {
                    // 2010-08-12-정비재 : LRR
                    sPackage = "PACKAGE_PQC030115.PROC_PQC030115_LRR";
                    sParameter = "P_FACTORY:" + cdvFactory.Text
                            + "│ P_BASE_MONTH:" + cdvBaseDate.Value.ToString("yyyyMM")
                            + "│ P_BASE_TIME:" + cdvBaseDate.Value.ToString("yyyyMMdd")
                            + "│ P_CUSTOMER:" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "%"
                            + "│ P_PACKAGE:" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "%"
                            + "│ P_OPER:" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "%"
                            + "│ P_PREV_FLAG:" + (chkPrevFlag.Checked == true ? 'Y' : 'N');
                }
                else if (rbS03.Checked == true)
                {
                    // 2010-08-12-정비재 : YIELD
                    sPackage = "PACKAGE_PQC030115.PROC_PQC030115_YIELD";
                    sParameter = "P_FACTORY:" + cdvFactory.Text
                            + "│ P_BASE_MONTH:" + cdvBaseDate.Value.ToString("yyyyMM")
                            + "│ P_BASE_TIME:" + cdvBaseDate.Value.ToString("yyyyMMdd")
                            + "│ P_CUSTOMER:" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "%"
                            + "│ P_PACKAGE:" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "%"
                            + "│ P_OPER:" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "%"
                            + "│ P_PREV_FLAG:" + (chkPrevFlag.Checked == true ? 'Y' : 'N');
                }

                DataTable dt = CmnFunction.oComm.fnExecutePackage("DYNAMIC", sPackage, sParameter);

                if (dt.Rows.Count <= 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return false;
                }

                SS01.DataSource = dt;
                SS01.RPT_AutoFit(false);

                fnMakeChart(SS01, 0);

                return true;
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                Cursor.Current = Cursors.Default;
            }
        }

        //private bool fnDataFind()
        //{
        //    /****************************************************
        //     * Comment : DataBase에 저장된 데이터를 조회한다.
        //     * 
        //     * Created By : bee-jae jung(2010-06-05-토요일)
        //     * 
        //     * Modified By : bee-jae jung(2010-06-05-토요일)
        //     ****************************************************/
        //    DataTable dt = null;
        //    string QRY = "";
        //    string V_MONTH = "";
        //    try
        //    {
        //        Cursor.Current = Cursors.WaitCursor;
        //        LoadingPopUp.LoadIngPopUpShow(this);

        //        // 2010-06-24-정비재 : Sheet / Listview를 초기화 한다.
        //        CmnInitFunction.ClearList(SS01, true);

        //        // 2010-07-06-정비재 : 전월불량 TOP5 기준일 경우
        //        if (chkPrevFlag.Checked == true)
        //        {
        //            V_MONTH = "-1";
        //        }
        //        else
        //        {
        //            V_MONTH = "-0";
        //        }


        //        if (rbS01.Checked == true)
        //        {
        //            // 2010-06-25-정비재 : 제품별 공정불량현황 PPM을 조회한다.
        //            QRY	= "SELECT A.CUSTOMER AS CUSTOMER  \n"
        //                + "		 , A.PACKAGE AS PACKAGE \n"
        //                + "		 , A.OPER AS OPER \n"
        //                + "		 , A.LOSS_CODE || ' ' || B.LOSS_DESC AS LOSS_CODE \n"
        //                + "	     , SUM(A.LOSS_PPM_MM_1) AS LOSS_PPM_MM_1 \n"
        //                + "	     , SUM(A.LOSS_PPM_MM_2) AS LOSS_PPM_MM_2 \n"
        //                + "	     , SUM(A.LOSS_PPM_MM_3) AS LOSS_PPM_MM_3 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_1) AS LOSS_PPM_WW_1 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_2) AS LOSS_PPM_WW_2 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_3) AS LOSS_PPM_WW_3 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_4) AS LOSS_PPM_WW_4 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_1) AS LOSS_PPM_DD_1 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_2) AS LOSS_PPM_DD_2 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_3) AS LOSS_PPM_DD_3 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_4) AS LOSS_PPM_DD_4 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_5) AS LOSS_PPM_DD_5 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_6) AS LOSS_PPM_DD_6 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_7) AS LOSS_PPM_DD_7 \n"
        //                + "	  FROM (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	             , A.PACKAGE AS PACKAGE \n"
        //                + "	             , A.OPER AS OPER \n"
        //                + "	             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_MM_1 > 0 AND B.START_QTY_MM_1 > 0 THEN ROUND((A.LOSS_QTY_MM_1 * 1000000) / B.START_QTY_MM_1, 2) ELSE 0 END AS LOSS_PPM_MM_1 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_MM_2 > 0 AND B.START_QTY_MM_2 > 0 THEN ROUND((A.LOSS_QTY_MM_2 * 1000000) / B.START_QTY_MM_2, 2) ELSE 0 END AS LOSS_PPM_MM_2 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_MM_3 > 0 AND B.START_QTY_MM_3 > 0 THEN ROUND((A.LOSS_QTY_MM_3 * 1000000) / B.START_QTY_MM_3, 2) ELSE 0 END AS LOSS_PPM_MM_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_1 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_2 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_1 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_2 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_3 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_5 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_6 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_7 \n"
        //                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                     , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 \n"
        //                + "	                     , SUM(A.LOSS_QTY_MM_2) AS LOSS_QTY_MM_2 \n"
        //                + "	                     , SUM(A.LOSS_QTY_MM_3) AS LOSS_QTY_MM_3 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_2 \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_3 \n"
        //                + "	                          FROM RSUMLOTLSD A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') \n"
        //                + "	                           AND A.TRAN_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	             , (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , SUM(A.START_QTY_MM_1) AS START_QTY_MM_1 \n"
        //                + "	                     , SUM(A.START_QTY_MM_2) AS START_QTY_MM_2 \n"
        //                + "	                     , SUM(A.START_QTY_MM_3) AS START_QTY_MM_3 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_1  \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_2 \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_3 \n"
        //                + "	                          FROM RSUMLOTLSM A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') \n"
        //                + "	                           AND A.TRAN_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B \n"
        //                + "	             , (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	                     , A.PACKAGE AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                             , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL \n"
        //                + "	                          FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                                     , A.MAT_ID AS MAT_ID \n"
        //                + "	                                     , A.OPER AS OPER \n"
        //                + "	                                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                                  FROM RSUMLOTLSD A  \n"
        //                + "	                                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), " + V_MONTH + "), 'YYYYMM') \n"
        //                + "	                                   AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                             , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                         WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                           AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	                             , DATA_1 AS LOSS_DESC \n"
        //                + "	                             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	                           AND DATA_1 <> 'ETC' \n"
        //                + "	                           AND DATA_2 <> 'Y') B \n"
        //                + "	                 WHERE A.LOSS_CODE = B.LOSS_CODE \n"
        //                + "	                   AND A.LOSS_LEVEL <= " + cmbDisplayCount.Text + ") C \n"
        //                + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = B.PACKAGE \n"
        //                + "	           AND A.OPER = B.OPER \n"
        //                + "	           AND A.CUSTOMER = C.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = C.PACKAGE \n"
        //                + "	           AND A.OPER = C.OPER \n"
        //                + "	           AND A.LOSS_CODE = C.LOSS_CODE \n"
        //                + "	        UNION ALL \n"
        //                + "	        SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	             , A.PACKAGE AS PACKAGE \n"
        //                + "	             , A.OPER AS OPER \n"
        //                + "	             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	             , 0 AS LOSS_PPM_MM_1 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_2 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_3 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_1 > 0 AND B.START_QTY_WW_1 > 0 THEN ROUND((A.LOSS_QTY_WW_1 * 1000000) / B.START_QTY_WW_1, 2) ELSE 0 END AS LOSS_PPM_WW_1 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_2 > 0 AND B.START_QTY_WW_2 > 0 THEN ROUND((A.LOSS_QTY_WW_2 * 1000000) / B.START_QTY_WW_2, 2) ELSE 0 END AS LOSS_PPM_WW_2 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_3 > 0 AND B.START_QTY_WW_3 > 0 THEN ROUND((A.LOSS_QTY_WW_3 * 1000000) / B.START_QTY_WW_3, 2) ELSE 0 END AS LOSS_PPM_WW_3 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_4 > 0 AND B.START_QTY_WW_4 > 0 THEN ROUND((A.LOSS_QTY_WW_4 * 1000000) / B.START_QTY_WW_4, 2) ELSE 0 END AS LOSS_PPM_WW_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_1 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_2 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_3 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_5 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_6 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_7 \n"
        //                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_1) AS LOSS_QTY_WW_1 \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_2) AS LOSS_QTY_WW_2 \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_3) AS LOSS_QTY_WW_3 \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_4) AS LOSS_QTY_WW_4 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_1 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_2 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_3 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_4 \n"
        //                + "	                          FROM RSUMLOTLSD A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B  \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	             , (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , SUM(A.START_QTY_WW_1) AS START_QTY_WW_1 \n"
        //                + "	                     , SUM(A.START_QTY_WW_2) AS START_QTY_WW_2 \n"
        //                + "	                     , SUM(A.START_QTY_WW_3) AS START_QTY_WW_3 \n"
        //                + "	                     , SUM(A.START_QTY_WW_4) AS START_QTY_WW_4 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_1 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_2 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_3 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_4 \n"
        //                + "	                          FROM RSUMLOTLSM A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A \n"
        //                + "	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B  \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B \n"
        //                + "	             , (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	                     , A.PACKAGE AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                             , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL \n"
        //                + "	                          FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                                     , A.MAT_ID AS MAT_ID \n"
        //                + "	                                     , A.OPER AS OPER \n"
        //                + "	                                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                                  FROM RSUMLOTLSD A  \n"
        //                + "	                                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), " + V_MONTH + "), 'YYYYMM') \n"
        //                + "	                                   AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                             , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                         WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                           AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	                             , DATA_1 AS LOSS_DESC \n"
        //                + "	                             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	                           AND DATA_1 <> 'ETC' \n"
        //                + "	                           AND DATA_2 <> 'Y') B \n"
        //                + "	                 WHERE A.LOSS_CODE = B.LOSS_CODE \n"
        //                + "	                   AND A.LOSS_LEVEL <= " + cmbDisplayCount.Text + ") C \n"
        //                + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = B.PACKAGE \n"
        //                + "	           AND A.OPER = B.OPER \n"
        //                + "	           AND A.CUSTOMER = C.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = C.PACKAGE \n"
        //                + "	           AND A.OPER = C.OPER \n"
        //                + "	           AND A.LOSS_CODE = C.LOSS_CODE \n"
        //                + "	        UNION ALL \n"
        //                + "			SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	             , A.PACKAGE AS PACKAGE \n"
        //                + "	             , A.OPER AS OPER \n"
        //                + "	             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	             , 0 AS LOSS_PPM_MM_1 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_2 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_1 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_2 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_4 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_1 > 0 AND B.START_QTY_DD_1 > 0 THEN ROUND((A.LOSS_QTY_DD_1 * 1000000) / B.START_QTY_DD_1, 2) ELSE 0 END AS LOSS_PPM_DD_1 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_2 > 0 AND B.START_QTY_DD_2 > 0 THEN ROUND((A.LOSS_QTY_DD_2 * 1000000) / B.START_QTY_DD_2, 2) ELSE 0 END AS LOSS_PPM_DD_2 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_3 > 0 AND B.START_QTY_DD_3 > 0 THEN ROUND((A.LOSS_QTY_DD_3 * 1000000) / B.START_QTY_DD_3, 2) ELSE 0 END AS LOSS_PPM_DD_3 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_4 > 0 AND B.START_QTY_DD_4 > 0 THEN ROUND((A.LOSS_QTY_DD_4 * 1000000) / B.START_QTY_DD_4, 2) ELSE 0 END AS LOSS_PPM_DD_4 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_5 > 0 AND B.START_QTY_DD_5 > 0 THEN ROUND((A.LOSS_QTY_DD_5 * 1000000) / B.START_QTY_DD_5, 2) ELSE 0 END AS LOSS_PPM_DD_5 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_6 > 0 AND B.START_QTY_DD_6 > 0 THEN ROUND((A.LOSS_QTY_DD_6 * 1000000) / B.START_QTY_DD_6, 2) ELSE 0 END AS LOSS_PPM_DD_6 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_7 > 0 AND B.START_QTY_DD_7 > 0 THEN ROUND((A.LOSS_QTY_DD_7 * 1000000) / B.START_QTY_DD_7, 2) ELSE 0 END AS LOSS_PPM_DD_7 \n"
        //                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_1) AS LOSS_QTY_DD_1 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_2) AS LOSS_QTY_DD_2 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_3) AS LOSS_QTY_DD_3 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_4) AS LOSS_QTY_DD_4 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_5) AS LOSS_QTY_DD_5 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_6) AS LOSS_QTY_DD_6 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_7) AS LOSS_QTY_DD_7 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_1  \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_2 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_3 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_4 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_5 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_6 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_7 \n"
        //                + "	                          FROM RSUMLOTLSD A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	             , (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , SUM(A.START_QTY_DD_1) AS START_QTY_DD_1 \n"
        //                + "	                     , SUM(A.START_QTY_DD_2) AS START_QTY_DD_2 \n"
        //                + "	                     , SUM(A.START_QTY_DD_3) AS START_QTY_DD_3 \n"
        //                + "	                     , SUM(A.START_QTY_DD_4) AS START_QTY_DD_4 \n"
        //                + "	                     , SUM(A.START_QTY_DD_5) AS START_QTY_DD_5 \n"
        //                + "	                     , SUM(A.START_QTY_DD_6) AS START_QTY_DD_6 \n"
        //                + "	                     , SUM(A.START_QTY_DD_7) AS START_QTY_DD_7 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_1  \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_2 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_3 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_4 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_5 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_6 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_7 \n"
        //                + "	                          FROM RSUMLOTLSM A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B \n"
        //                + "	             , (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	                     , A.PACKAGE AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                             , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL \n"
        //                + "	                          FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                                     , A.MAT_ID AS MAT_ID \n"
        //                + "	                                     , A.OPER AS OPER \n"
        //                + "	                                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                                  FROM RSUMLOTLSD A  \n"
        //                + "	                                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), " + V_MONTH + "), 'YYYYMM') \n"
        //                + "	                                   AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                             , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                         WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                           AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	                             , DATA_1 AS LOSS_DESC \n"
        //                + "	                             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	                           AND DATA_1 <> 'ETC' \n"
        //                + "	                           AND DATA_2 <> 'Y') B \n"
        //                + "	                 WHERE A.LOSS_CODE = B.LOSS_CODE \n"
        //                + "	                   AND A.LOSS_LEVEL <= " + cmbDisplayCount.Text + ") C \n"
        //                + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = B.PACKAGE \n"
        //                + "	           AND A.OPER = B.OPER \n"
        //                + "	           AND A.CUSTOMER = C.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = C.PACKAGE \n"
        //                + "	           AND A.OPER = C.OPER \n"
        //                + "	           AND A.LOSS_CODE = C.LOSS_CODE) A \n"
        //                + "		 , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	             , DATA_1 AS LOSS_DESC \n"
        //                + "	             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	           AND DATA_1 <> 'ETC' \n"
        //                + "	           AND DATA_2 <> 'Y') B \n"
        //                + "	 WHERE A.LOSS_CODE = B.LOSS_CODE  \n"
        //                + "	 GROUP BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC \n"
        //                + "	 ORDER BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC";
        //        }
        //        else if (rbS02.Checked == true)
        //        {
        //            // 2010-06-24-정비재 : 제품별 공정불량현황 LRR을 조회한다.
        //            QRY	= "SELECT A.CUSTOMER AS CUSTOMER  \n"
        //                + "		 , A.PACKAGE AS PACKAGE \n"
        //                + "		 , A.OPER AS OPER \n"
        //                + "		 , A.LOSS_CODE || ' ' || B.LOSS_DESC AS LOSS_CODE \n"
        //                + "	     , SUM(A.LOSS_PPM_MM_1) AS LOSS_PPM_MM_1 \n"
        //                + "	     , SUM(A.LOSS_PPM_MM_2) AS LOSS_PPM_MM_2 \n"
        //                + "	     , SUM(A.LOSS_PPM_MM_3) AS LOSS_PPM_MM_3 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_1) AS LOSS_PPM_WW_1 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_2) AS LOSS_PPM_WW_2 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_3) AS LOSS_PPM_WW_3 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_4) AS LOSS_PPM_WW_4 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_1) AS LOSS_PPM_DD_1 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_2) AS LOSS_PPM_DD_2 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_3) AS LOSS_PPM_DD_3 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_4) AS LOSS_PPM_DD_4 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_5) AS LOSS_PPM_DD_5 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_6) AS LOSS_PPM_DD_6 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_7) AS LOSS_PPM_DD_7 \n"
        //                + "	  FROM (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	             , A.PACKAGE AS PACKAGE \n"
        //                + "	             , A.OPER AS OPER \n"
        //                + "	             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_MM_1 > 0 AND B.START_QTY_MM_1 > 0 THEN ROUND((A.LOSS_QTY_MM_1 * 100) / B.START_QTY_MM_1, 2) ELSE 0 END AS LOSS_PPM_MM_1 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_MM_2 > 0 AND B.START_QTY_MM_2 > 0 THEN ROUND((A.LOSS_QTY_MM_2 * 100) / B.START_QTY_MM_2, 2) ELSE 0 END AS LOSS_PPM_MM_2 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_MM_3 > 0 AND B.START_QTY_MM_3 > 0 THEN ROUND((A.LOSS_QTY_MM_3 * 100) / B.START_QTY_MM_3, 2) ELSE 0 END AS LOSS_PPM_MM_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_1 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_2 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_1 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_2 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_3 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_5 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_6 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_7 \n"
        //                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                     , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 \n"
        //                + "	                     , SUM(A.LOSS_QTY_MM_2) AS LOSS_QTY_MM_2 \n"
        //                + "	                     , SUM(A.LOSS_QTY_MM_3) AS LOSS_QTY_MM_3 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_2 \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_3 \n"
        //                + "	                          FROM RSUMLOTLSD A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') \n"
        //                + "	                           AND A.TRAN_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	             , (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , SUM(A.START_QTY_MM_1) AS START_QTY_MM_1 \n"
        //                + "	                     , SUM(A.START_QTY_MM_2) AS START_QTY_MM_2 \n"
        //                + "	                     , SUM(A.START_QTY_MM_3) AS START_QTY_MM_3 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_MM_1  \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_MM_2 \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_MM_3 \n"
        //                + "	                          FROM RSUMLOTLSM A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') \n"
        //                + "	                           AND A.TRAN_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B \n"
        //                + "	             , (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	                     , A.PACKAGE AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                             , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL \n"
        //                + "	                          FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                                     , A.MAT_ID AS MAT_ID \n"
        //                + "	                                     , A.OPER AS OPER \n"
        //                + "	                                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                                     , SUM(A.LOSS_LOT_QTY_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                                  FROM RSUMLOTLSD A  \n"
        //                + "	                                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), " + V_MONTH + "), 'YYYYMM') \n"
        //                + "	                                   AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                             , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                         WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                           AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	                             , DATA_1 AS LOSS_DESC \n"
        //                + "	                             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	                           AND DATA_1 <> 'ETC' \n"
        //                + "	                           AND DATA_2 <> 'Y') B \n"
        //                + "	                 WHERE A.LOSS_CODE = B.LOSS_CODE \n"
        //                + "	                   AND A.LOSS_LEVEL <= 5) C \n"
        //                + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = B.PACKAGE \n"
        //                + "	           AND A.OPER = B.OPER \n"
        //                + "	           AND A.CUSTOMER = C.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = C.PACKAGE \n"
        //                + "	           AND A.OPER = C.OPER \n"
        //                + "	           AND A.LOSS_CODE = C.LOSS_CODE \n"
        //                + "	        UNION ALL \n"
        //                + "	        SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	             , A.PACKAGE AS PACKAGE \n"
        //                + "	             , A.OPER AS OPER \n"
        //                + "	             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	             , 0 AS LOSS_PPM_MM_1 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_2 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_3 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_1 > 0 AND B.START_QTY_WW_1 > 0 THEN ROUND((A.LOSS_QTY_WW_1 * 100) / B.START_QTY_WW_1, 2) ELSE 0 END AS LOSS_PPM_WW_1 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_2 > 0 AND B.START_QTY_WW_2 > 0 THEN ROUND((A.LOSS_QTY_WW_2 * 100) / B.START_QTY_WW_2, 2) ELSE 0 END AS LOSS_PPM_WW_2 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_3 > 0 AND B.START_QTY_WW_3 > 0 THEN ROUND((A.LOSS_QTY_WW_3 * 100) / B.START_QTY_WW_3, 2) ELSE 0 END AS LOSS_PPM_WW_3 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_4 > 0 AND B.START_QTY_WW_4 > 0 THEN ROUND((A.LOSS_QTY_WW_4 * 100) / B.START_QTY_WW_4, 2) ELSE 0 END AS LOSS_PPM_WW_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_1 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_2 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_3 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_5 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_6 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_7 \n"
        //                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_1) AS LOSS_QTY_WW_1 \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_2) AS LOSS_QTY_WW_2 \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_3) AS LOSS_QTY_WW_3 \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_4) AS LOSS_QTY_WW_4 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_1 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_2 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_3 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_4 \n"
        //                + "	                          FROM RSUMLOTLSD A  \n"
        //                + "	                         WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B  \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	             , (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , SUM(A.START_QTY_WW_1) AS START_QTY_WW_1 \n"
        //                + "	                     , SUM(A.START_QTY_WW_2) AS START_QTY_WW_2 \n"
        //                + "	                     , SUM(A.START_QTY_WW_3) AS START_QTY_WW_3 \n"
        //                + "	                     , SUM(A.START_QTY_WW_4) AS START_QTY_WW_4 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_WW_1 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_WW_2 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_WW_3 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_WW_4 \n"
        //                + "	                          FROM RSUMLOTLSM A  \n"
        //                + "	                         WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A \n"
        //                + "	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B  \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B \n"
        //                + "	             , (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	                     , A.PACKAGE AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                             , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL \n"
        //                + "	                          FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                                     , A.MAT_ID AS MAT_ID \n"
        //                + "	                                     , A.OPER AS OPER \n"
        //                + "	                                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                                     , SUM(A.LOSS_LOT_QTY_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                                  FROM RSUMLOTLSD A  \n"
        //                + "	                                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), " + V_MONTH + "), 'YYYYMM') \n"
        //                + "	                                   AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                             , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                         WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                           AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	                             , DATA_1 AS LOSS_DESC \n"
        //                + "	                             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	                           AND DATA_1 <> 'ETC' \n"
        //                + "	                           AND DATA_2 <> 'Y') B \n"
        //                + "	                 WHERE A.LOSS_CODE = B.LOSS_CODE \n"
        //                + "	                   AND A.LOSS_LEVEL <= " + cmbDisplayCount.Text + ") C \n"
        //                + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = B.PACKAGE \n"
        //                + "	           AND A.OPER = B.OPER \n"
        //                + "	           AND A.CUSTOMER = C.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = C.PACKAGE \n"
        //                + "	           AND A.OPER = C.OPER \n"
        //                + "	           AND A.LOSS_CODE = C.LOSS_CODE \n"
        //                + "	        UNION ALL \n"
        //                + "			SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	             , A.PACKAGE AS PACKAGE \n"
        //                + "	             , A.OPER AS OPER \n"
        //                + "	             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	             , 0 AS LOSS_PPM_MM_1 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_2 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_1 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_2 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_4 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_1 > 0 AND B.START_QTY_DD_1 > 0 THEN ROUND((A.LOSS_QTY_DD_1 * 100) / B.START_QTY_DD_1, 2) ELSE 0 END AS LOSS_PPM_DD_1 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_2 > 0 AND B.START_QTY_DD_2 > 0 THEN ROUND((A.LOSS_QTY_DD_2 * 100) / B.START_QTY_DD_2, 2) ELSE 0 END AS LOSS_PPM_DD_2 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_3 > 0 AND B.START_QTY_DD_3 > 0 THEN ROUND((A.LOSS_QTY_DD_3 * 100) / B.START_QTY_DD_3, 2) ELSE 0 END AS LOSS_PPM_DD_3 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_4 > 0 AND B.START_QTY_DD_4 > 0 THEN ROUND((A.LOSS_QTY_DD_4 * 100) / B.START_QTY_DD_4, 2) ELSE 0 END AS LOSS_PPM_DD_4 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_5 > 0 AND B.START_QTY_DD_5 > 0 THEN ROUND((A.LOSS_QTY_DD_5 * 100) / B.START_QTY_DD_5, 2) ELSE 0 END AS LOSS_PPM_DD_5 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_6 > 0 AND B.START_QTY_DD_6 > 0 THEN ROUND((A.LOSS_QTY_DD_6 * 100) / B.START_QTY_DD_6, 2) ELSE 0 END AS LOSS_PPM_DD_6 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_7 > 0 AND B.START_QTY_DD_7 > 0 THEN ROUND((A.LOSS_QTY_DD_7 * 100) / B.START_QTY_DD_7, 2) ELSE 0 END AS LOSS_PPM_DD_7 \n"
        //                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_1) AS LOSS_QTY_DD_1 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_2) AS LOSS_QTY_DD_2 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_3) AS LOSS_QTY_DD_3 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_4) AS LOSS_QTY_DD_4 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_5) AS LOSS_QTY_DD_5 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_6) AS LOSS_QTY_DD_6 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_7) AS LOSS_QTY_DD_7 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_1  \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_2 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_3 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_4 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_5 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_6 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_7 \n"
        //                + "	                          FROM RSUMLOTLSD A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	             , (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , SUM(A.START_QTY_DD_1) AS START_QTY_DD_1 \n"
        //                + "	                     , SUM(A.START_QTY_DD_2) AS START_QTY_DD_2 \n"
        //                + "	                     , SUM(A.START_QTY_DD_3) AS START_QTY_DD_3 \n"
        //                + "	                     , SUM(A.START_QTY_DD_4) AS START_QTY_DD_4 \n"
        //                + "	                     , SUM(A.START_QTY_DD_5) AS START_QTY_DD_5 \n"
        //                + "	                     , SUM(A.START_QTY_DD_6) AS START_QTY_DD_6 \n"
        //                + "	                     , SUM(A.START_QTY_DD_7) AS START_QTY_DD_7 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_1  \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_2 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_3 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_4 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_5 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_6 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_7 \n"
        //                + "	                          FROM RSUMLOTLSM A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B \n"
        //                + "	             , (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	                     , A.PACKAGE AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                             , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL \n"
        //                + "	                          FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                                     , A.MAT_ID AS MAT_ID \n"
        //                + "	                                     , A.OPER AS OPER \n"
        //                + "	                                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                                     , SUM(A.LOSS_LOT_QTY_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                                  FROM RSUMLOTLSD A  \n"
        //                + "	                                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), " + V_MONTH + "), 'YYYYMM') \n"
        //                + "	                                   AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                             , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                         WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                           AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	                             , DATA_1 AS LOSS_DESC \n"
        //                + "	                             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	                           AND DATA_1 <> 'ETC' \n"
        //                + "	                           AND DATA_2 <> 'Y') B \n"
        //                + "	                 WHERE A.LOSS_CODE = B.LOSS_CODE \n"
        //                + "	                   AND A.LOSS_LEVEL <= " + cmbDisplayCount.Text + ") C \n"
        //                + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = B.PACKAGE \n"
        //                + "	           AND A.OPER = B.OPER \n"
        //                + "	           AND A.CUSTOMER = C.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = C.PACKAGE \n"
        //                + "	           AND A.OPER = C.OPER \n"
        //                + "	           AND A.LOSS_CODE = C.LOSS_CODE) A \n"
        //                + "		 , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	             , DATA_1 AS LOSS_DESC \n"
        //                + "	             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	           AND DATA_1 <> 'ETC' \n"
        //                + "	           AND DATA_2 <> 'Y') B \n"
        //                + "	 WHERE A.LOSS_CODE = B.LOSS_CODE  \n"
        //                + "	 GROUP BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC \n"
        //                + "	 ORDER BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC";

        //        }
        //        else if (rbS03.Checked == true)
        //        {
        //            // 2010-06-28-정비재 : 제품별 공정불량현황 YIELD를 조회한다.
        //            QRY	= "SELECT A.CUSTOMER AS CUSTOMER  \n"
        //                + "		 , A.PACKAGE AS PACKAGE \n"
        //                + "		 , A.OPER AS OPER \n"
        //                + "		 , A.LOSS_CODE || ' ' || B.LOSS_DESC AS LOSS_CODE \n"
        //                + "	     , SUM(A.LOSS_PPM_MM_1) AS LOSS_PPM_MM_1 \n"
        //                + "	     , SUM(A.LOSS_PPM_MM_2) AS LOSS_PPM_MM_2 \n"
        //                + "	     , SUM(A.LOSS_PPM_MM_3) AS LOSS_PPM_MM_3 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_1) AS LOSS_PPM_WW_1 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_2) AS LOSS_PPM_WW_2 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_3) AS LOSS_PPM_WW_3 \n"
        //                + "	     , SUM(A.LOSS_PPM_WW_4) AS LOSS_PPM_WW_4 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_1) AS LOSS_PPM_DD_1 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_2) AS LOSS_PPM_DD_2 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_3) AS LOSS_PPM_DD_3 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_4) AS LOSS_PPM_DD_4 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_5) AS LOSS_PPM_DD_5 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_6) AS LOSS_PPM_DD_6 \n"
        //                + "	     , SUM(A.LOSS_PPM_DD_7) AS LOSS_PPM_DD_7 \n"
        //                + "	  FROM (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	             , A.PACKAGE AS PACKAGE \n"
        //                + "	             , A.OPER AS OPER \n"
        //                + "	             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_MM_1 > 0 AND B.START_QTY_MM_1 > 0 THEN ROUND((A.LOSS_QTY_MM_1 * 100) / B.START_QTY_MM_1, 2) ELSE 0 END AS LOSS_PPM_MM_1 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_MM_2 > 0 AND B.START_QTY_MM_2 > 0 THEN ROUND((A.LOSS_QTY_MM_2 * 100) / B.START_QTY_MM_2, 2) ELSE 0 END AS LOSS_PPM_MM_2 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_MM_3 > 0 AND B.START_QTY_MM_3 > 0 THEN ROUND((A.LOSS_QTY_MM_3 * 100) / B.START_QTY_MM_3, 2) ELSE 0 END AS LOSS_PPM_MM_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_1 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_2 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_1 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_2 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_3 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_5 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_6 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_7 \n"
        //                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                     , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 \n"
        //                + "	                     , SUM(A.LOSS_QTY_MM_2) AS LOSS_QTY_MM_2 \n"
        //                + "	                     , SUM(A.LOSS_QTY_MM_3) AS LOSS_QTY_MM_3 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_2 \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_3 \n"
        //                + "	                          FROM RSUMLOTLSD A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') \n"
        //                + "	                           AND A.TRAN_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	             , (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , SUM(A.START_QTY_MM_1) AS START_QTY_MM_1 \n"
        //                + "	                     , SUM(A.START_QTY_MM_2) AS START_QTY_MM_2 \n"
        //                + "	                     , SUM(A.START_QTY_MM_3) AS START_QTY_MM_3 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_1  \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_2 \n"
        //                + "	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_3 \n"
        //                + "	                          FROM RSUMLOTLSM A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') \n"
        //                + "	                           AND A.TRAN_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B \n"
        //                + "	             , (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	                     , A.PACKAGE AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                             , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL \n"
        //                + "	                          FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                                     , A.MAT_ID AS MAT_ID \n"
        //                + "	                                     , A.OPER AS OPER \n"
        //                + "	                                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                                  FROM RSUMLOTLSD A  \n"
        //                + "	                                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), " + V_MONTH + "), 'YYYYMM') \n"
        //                + "	                                   AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                             , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                         WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                           AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	                             , DATA_1 AS LOSS_DESC \n"
        //                + "	                             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	                           AND DATA_1 <> 'ETC' \n"
        //                + "	                           AND DATA_2 <> 'Y') B \n"
        //                + "	                 WHERE A.LOSS_CODE = B.LOSS_CODE \n"
        //                + "	                   AND A.LOSS_LEVEL <= " + cmbDisplayCount.Text + ") C \n"
        //                + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = B.PACKAGE \n"
        //                + "	           AND A.OPER = B.OPER \n"
        //                + "	           AND A.CUSTOMER = C.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = C.PACKAGE \n"
        //                + "	           AND A.OPER = C.OPER \n"
        //                + "	           AND A.LOSS_CODE = C.LOSS_CODE \n"
        //                + "	        UNION ALL \n"
        //                + "	        SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	             , A.PACKAGE AS PACKAGE \n"
        //                + "	             , A.OPER AS OPER \n"
        //                + "	             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	             , 0 AS LOSS_PPM_MM_1 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_2 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_3 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_1 > 0 AND B.START_QTY_WW_1 > 0 THEN ROUND((A.LOSS_QTY_WW_1 * 100) / B.START_QTY_WW_1, 2) ELSE 0 END AS LOSS_PPM_WW_1 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_2 > 0 AND B.START_QTY_WW_2 > 0 THEN ROUND((A.LOSS_QTY_WW_2 * 100) / B.START_QTY_WW_2, 2) ELSE 0 END AS LOSS_PPM_WW_2 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_3 > 0 AND B.START_QTY_WW_3 > 0 THEN ROUND((A.LOSS_QTY_WW_3 * 100) / B.START_QTY_WW_3, 2) ELSE 0 END AS LOSS_PPM_WW_3 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_WW_4 > 0 AND B.START_QTY_WW_4 > 0 THEN ROUND((A.LOSS_QTY_WW_4 * 100) / B.START_QTY_WW_4, 2) ELSE 0 END AS LOSS_PPM_WW_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_1 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_2 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_3 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_4 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_5 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_6 \n"
        //                + "	             , 0 AS LOSS_PPM_DD_7 \n"
        //                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_1) AS LOSS_QTY_WW_1 \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_2) AS LOSS_QTY_WW_2 \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_3) AS LOSS_QTY_WW_3 \n"
        //                + "	                     , SUM(A.LOSS_QTY_WW_4) AS LOSS_QTY_WW_4 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_1 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_2 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_3 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_4 \n"
        //                + "	                          FROM RSUMLOTLSD A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B  \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	             , (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , SUM(A.START_QTY_WW_1) AS START_QTY_WW_1 \n"
        //                + "	                     , SUM(A.START_QTY_WW_2) AS START_QTY_WW_2 \n"
        //                + "	                     , SUM(A.START_QTY_WW_3) AS START_QTY_WW_3 \n"
        //                + "	                     , SUM(A.START_QTY_WW_4) AS START_QTY_WW_4 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_1 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_2 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_3 \n"
        //                + "	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                                            END \n"
        //                + "	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
        //                + "	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_4 \n"
        //                + "	                          FROM RSUMLOTLSM A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A \n"
        //                + "	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B  \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B \n"
        //                + "	             , (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	                     , A.PACKAGE AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                             , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL \n"
        //                + "	                          FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                                     , A.MAT_ID AS MAT_ID \n"
        //                + "	                                     , A.OPER AS OPER \n"
        //                + "	                                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                                  FROM RSUMLOTLSD A  \n"
        //                + "	                                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), " + V_MONTH + "), 'YYYYMM') \n"
        //                + "	                                   AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                             , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                         WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                           AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	                             , DATA_1 AS LOSS_DESC \n"
        //                + "	                             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	                           AND DATA_1 <> 'ETC' \n"
        //                + "	                           AND DATA_2 <> 'Y') B \n"
        //                + "	                 WHERE A.LOSS_CODE = B.LOSS_CODE \n"
        //                + "	                   AND A.LOSS_LEVEL <= " + cmbDisplayCount.Text + ") C \n"
        //                + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = B.PACKAGE \n"
        //                + "	           AND A.OPER = B.OPER \n"
        //                + "	           AND A.CUSTOMER = C.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = C.PACKAGE \n"
        //                + "	           AND A.OPER = C.OPER \n"
        //                + "	           AND A.LOSS_CODE = C.LOSS_CODE \n"
        //                + "	        UNION ALL \n"
        //                + "			SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	             , A.PACKAGE AS PACKAGE \n"
        //                + "	             , A.OPER AS OPER \n"
        //                + "	             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	             , 0 AS LOSS_PPM_MM_1 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_2 \n"
        //                + "	             , 0 AS LOSS_PPM_MM_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_1 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_2 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_3 \n"
        //                + "	             , 0 AS LOSS_PPM_WW_4 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_1 > 0 AND B.START_QTY_DD_1 > 0 THEN ROUND((A.LOSS_QTY_DD_1 * 100) / B.START_QTY_DD_1, 2) ELSE 0 END AS LOSS_PPM_DD_1 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_2 > 0 AND B.START_QTY_DD_2 > 0 THEN ROUND((A.LOSS_QTY_DD_2 * 100) / B.START_QTY_DD_2, 2) ELSE 0 END AS LOSS_PPM_DD_2 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_3 > 0 AND B.START_QTY_DD_3 > 0 THEN ROUND((A.LOSS_QTY_DD_3 * 100) / B.START_QTY_DD_3, 2) ELSE 0 END AS LOSS_PPM_DD_3 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_4 > 0 AND B.START_QTY_DD_4 > 0 THEN ROUND((A.LOSS_QTY_DD_4 * 100) / B.START_QTY_DD_4, 2) ELSE 0 END AS LOSS_PPM_DD_4 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_5 > 0 AND B.START_QTY_DD_5 > 0 THEN ROUND((A.LOSS_QTY_DD_5 * 100) / B.START_QTY_DD_5, 2) ELSE 0 END AS LOSS_PPM_DD_5 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_6 > 0 AND B.START_QTY_DD_6 > 0 THEN ROUND((A.LOSS_QTY_DD_6 * 100) / B.START_QTY_DD_6, 2) ELSE 0 END AS LOSS_PPM_DD_6 \n"
        //                + "	             , CASE WHEN A.LOSS_QTY_DD_7 > 0 AND B.START_QTY_DD_7 > 0 THEN ROUND((A.LOSS_QTY_DD_7 * 100) / B.START_QTY_DD_7, 2) ELSE 0 END AS LOSS_PPM_DD_7 \n"
        //                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_1) AS LOSS_QTY_DD_1 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_2) AS LOSS_QTY_DD_2 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_3) AS LOSS_QTY_DD_3 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_4) AS LOSS_QTY_DD_4 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_5) AS LOSS_QTY_DD_5 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_6) AS LOSS_QTY_DD_6 \n"
        //                + "	                     , SUM(A.LOSS_QTY_DD_7) AS LOSS_QTY_DD_7 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_1  \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_2 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_3 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_4 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_5 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_6 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_7 \n"
        //                + "	                          FROM RSUMLOTLSD A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	             , (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                     , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , SUM(A.START_QTY_DD_1) AS START_QTY_DD_1 \n"
        //                + "	                     , SUM(A.START_QTY_DD_2) AS START_QTY_DD_2 \n"
        //                + "	                     , SUM(A.START_QTY_DD_3) AS START_QTY_DD_3 \n"
        //                + "	                     , SUM(A.START_QTY_DD_4) AS START_QTY_DD_4 \n"
        //                + "	                     , SUM(A.START_QTY_DD_5) AS START_QTY_DD_5 \n"
        //                + "	                     , SUM(A.START_QTY_DD_6) AS START_QTY_DD_6 \n"
        //                + "	                     , SUM(A.START_QTY_DD_7) AS START_QTY_DD_7 \n"
        //                + "	                  FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                             , A.MAT_ID AS MAT_ID \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_1  \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_2 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_3 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_4 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_5 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_6 \n"
        //                + "	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_7 \n"
        //                + "	                          FROM RSUMLOTLSM A  \n"
        //                + "	                         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
        //                + "	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
        //                + "	                                              END \n"
        //                + "	                           AND A.TRAN_TIME <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
        //                + "	                           AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A \n"
        //                + "	                     , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                 WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                   AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                   AND B.MAT_GRP_1 LIKE '" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "' || '%' \n"
        //                + "	                   AND B.MAT_GRP_3 LIKE '" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "' || '%' \n"
        //                + "	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B \n"
        //                + "	             , (SELECT A.CUSTOMER AS CUSTOMER \n"
        //                + "	                     , A.PACKAGE AS PACKAGE \n"
        //                + "	                     , A.OPER AS OPER \n"
        //                + "	                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n"
        //                + "	                             , B.MAT_GRP_3 AS PACKAGE \n"
        //                + "	                             , A.OPER AS OPER \n"
        //                + "	                             , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL \n"
        //                + "	                          FROM (SELECT A.FACTORY AS FACTORY \n"
        //                + "	                                     , A.MAT_ID AS MAT_ID \n"
        //                + "	                                     , A.OPER AS OPER \n"
        //                + "	                                     , A.LOSS_CODE AS LOSS_CODE \n"
        //                + "	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1  \n"
        //                + "	                                  FROM RSUMLOTLSD A  \n"
        //                + "	                                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), " + V_MONTH + "), 'YYYYMM') \n"
        //                + "	                                   AND A.OPER LIKE '" + (cdvOper.Text == "ALL" ? "%" : cdvOper.Text) + "' || '%' \n"
        //                + "	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                             , MESMGR.MWIPMATDEF@RPTTOMES B \n"
        //                + "	                         WHERE A.FACTORY = B.FACTORY \n"
        //                + "	                           AND A.MAT_ID = B.MAT_ID \n"
        //                + "	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A \n"
        //                + "	                     , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	                             , DATA_1 AS LOSS_DESC \n"
        //                + "	                             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	                           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	                           AND DATA_1 <> 'ETC' \n"
        //                + "	                           AND DATA_2 <> 'Y') B \n"
        //                + "	                 WHERE A.LOSS_CODE = B.LOSS_CODE \n"
        //                + "	                   AND A.LOSS_LEVEL <= " + cmbDisplayCount.Text + ") C \n"
        //                + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = B.PACKAGE \n"
        //                + "	           AND A.OPER = B.OPER \n"
        //                + "	           AND A.CUSTOMER = C.CUSTOMER \n"
        //                + "	           AND A.PACKAGE = C.PACKAGE \n"
        //                + "	           AND A.OPER = C.OPER \n"
        //                + "	           AND A.LOSS_CODE = C.LOSS_CODE) A \n"
        //                + "		 , (SELECT KEY_1 AS LOSS_CODE \n"
        //                + "	             , DATA_1 AS LOSS_DESC \n"
        //                + "	             , DATA_5 AS LOSS_OPER_DESC \n"
        //                + "	          FROM MESMGR.MGCMTBLDAT@RPTTOMES \n"
        //                + "	         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
        //                + "	           AND TABLE_NAME = 'LOSS_CODE' \n"
        //                + "	           AND DATA_1 <> 'ETC' \n"
        //                + "	           AND DATA_2 <> 'Y') B \n"
        //                + "	 WHERE A.LOSS_CODE = B.LOSS_CODE  \n"
        //                + "	 GROUP BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC \n"
        //                + "	 ORDER BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC";
        //        }

        //        if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
        //        {
        //            System.Windows.Forms.Clipboard.SetText(QRY.Replace((char)Keys.Tab, ' '));
        //        }

        //        dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY.Replace((char)Keys.Tab, ' '));

        //        if (dt.Rows.Count == 0)
        //        {
        //            dt.Dispose();
        //            LoadingPopUp.LoadingPopUpHidden();
        //            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
        //            return false;
        //        }
        //        SS01.DataSource = dt;

        //        fnMakeChart(SS01, 0);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LoadingPopUp.LoadingPopUpHidden();
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        LoadingPopUp.LoadingPopUpHidden();
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

		private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
		{
			/****************************************************
			 * Comment : SS의 데이터를 Chart에 표시한다.
			 * 
			 * Created By : bee-jae jung(2010-06-28-월요일)
			 * 
			 * Modified By : bee-jae jung(2010-06-28-월요일)
			 ****************************************************/
			int[] ich_mm = new int[3]; int[] icols_mm = new int[3]; int[] irows_mm = new int[5]; string[] stitle_mm = new string[5];
			int[] ich_ww = new int[4]; int[] icols_ww = new int[4]; int[] irows_ww = new int[5]; string[] stitle_ww = new string[5];
			int[] ich_dd = new int[7]; int[] icols_dd = new int[7]; int[] irows_dd = new int[5]; string[] stitle_dd = new string[5]; 
            //int icol = 0, irow = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2010-06-01-정비재 : SS에 데이터가 없으면 종료한다.
				//if (SS.Sheets[0].RowCount <= 0)
				//{
				//    return;
				//}

				//cf01.RPT_1_ChartInit(); cf02.RPT_1_ChartInit(); cf03.RPT_1_ChartInit();
				//cf01.RPT_2_ClearData(); cf02.RPT_2_ClearData(); cf03.RPT_2_ClearData();

				//// 2010-06-28-정비재 : 월별현황을 Chart에 Display한다.
				//cf01.AxisX.Title.Text = "Monthly Status";
				//if (rbS01.Checked == true)
				//{
				//    cf01.AxisY.Title.Text = "PPM";
				//}
				//else if (rbS02.Checked == true)
				//{
				//    cf01.AxisY.Title.Text = "LRR";
				//}
				//else if (rbS03.Checked == true)
				//{
				//    cf01.AxisY.Title.Text = "YIELD";
				//}
				//cf01.Gallery = SoftwareFX.ChartFX.Gallery.Curve;
				//cf01.AxisY.DataFormat.Format = 0;
				//cf01.AxisY.DataFormat.Decimals = 2;
				//cf01.AxisX.Staggered = false;
				//cf01.PointLabels = true;
				//for (icol = 0; icol < ich_mm.Length; icol++)
				//{
				//    ich_mm[icol] = icol + 4;
				//    icols_mm[icol] = icol + 4;
				//}
				//// 2010-06-28-정비재 : 선택한 Row에서 아래로 5개의 Row만 Display한다.
				//for (irow = 0; irow < 5; irow++)
				//{
				//    if ((iselrow + irow) >= SS.Sheets[0].RowCount)
				//    {
				//        break;
				//    }
				//    irows_mm[irow] = iselrow + irow;
				//    stitle_mm[irow] = SS.Sheets[0].Cells[iselrow + irow, 3].Text;
				//}
				//cf01.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
				//cf01.RPT_4_AddData(SS, irows_mm, icols_mm, SeriseType.Rows);
				//cf01.RPT_5_CloseData();
				//cf01.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 4);
				//cf01.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Top);


				//// 2010-06-28-정비재 : 월별현황을 Chart에 Display한다.
				//cf02.AxisX.Title.Text = "Weekly Status";
				//if (rbS01.Checked == true)
				//{
				//    cf02.AxisY.Title.Text = "PPM";
				//}
				//else if (rbS02.Checked == true)
				//{
				//    cf02.AxisY.Title.Text = "LRR";
				//}
				//else if (rbS03.Checked == true)
				//{
				//    cf02.AxisY.Title.Text = "YIELD";
				//}
				//cf02.Gallery = SoftwareFX.ChartFX.Gallery.Curve;
				//cf02.AxisY.DataFormat.Format = 0;
				//cf02.AxisY.DataFormat.Decimals = 2;
				//cf02.AxisX.Staggered = false;
				//cf02.PointLabels = true;
				//for (icol = 0; icol < ich_ww.Length; icol++)
				//{
				//    ich_ww[icol] = icol + 7;
				//    icols_ww[icol] = icol + 7;
				//}
				//// 2010-06-28-정비재 : 선택한 Row에서 아래로 5개의 Row만 Display한다.
				//for (irow = 0; irow < 5; irow++)
				//{
				//    if ((iselrow + irow) >= SS.Sheets[0].RowCount)
				//    {
				//        break;
				//    }
				//    irows_ww[irow] = iselrow + irow;
				//    stitle_ww[irow] = SS.Sheets[0].Cells[iselrow + irow, 3].Text;
				//}
				//cf02.RPT_3_OpenData(irows_ww.Length, icols_ww.Length);
				//cf02.RPT_4_AddData(SS, irows_ww, icols_ww, SeriseType.Rows);
				//cf02.RPT_5_CloseData();
				//cf02.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 7);
				//cf02.RPT_8_SetSeriseLegend(stitle_ww, SoftwareFX.ChartFX.Docked.Top);


				//// 2010-06-28-정비재 : 월별현황을 Chart에 Display한다.
				//cf03.AxisX.Title.Text = "Daily Status";
				//if (rbS01.Checked == true)
				//{
				//    cf03.AxisY.Title.Text = "PPM";
				//}
				//else if (rbS02.Checked == true)
				//{
				//    cf03.AxisY.Title.Text = "LRR";
				//}
				//else if (rbS03.Checked == true)
				//{
				//    cf03.AxisY.Title.Text = "YIELD";
				//}
				//cf03.Gallery = SoftwareFX.ChartFX.Gallery.Curve;
				//cf03.AxisY.DataFormat.Format = 0;
				//cf03.AxisY.DataFormat.Decimals = 2;
				//cf03.AxisX.Staggered = false;
				//cf03.PointLabels = true;
				//for (icol = 0; icol < ich_dd.Length; icol++)
				//{
				//    ich_dd[icol] = icol + 11;
				//    icols_dd[icol] = icol + 11;
				//}
				//// 2010-06-28-정비재 : 선택한 Row에서 아래로 5개의 Row만 Display한다.
				//for (irow = 0; irow < 5; irow++)
				//{
				//    if ((iselrow + irow) >= SS.Sheets[0].RowCount)
				//    {
				//        break;
				//    }
				//    irows_dd[irow] = iselrow + irow;
				//    stitle_dd[irow] = SS.Sheets[0].Cells[iselrow + irow, 3].Text;
				//}
				//cf03.RPT_3_OpenData(irows_dd.Length, icols_dd.Length);
				//cf03.RPT_4_AddData(SS, irows_dd, icols_dd, SeriseType.Rows);
				//cf03.RPT_5_CloseData();
				//cf03.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 11);
				//cf03.RPT_8_SetSeriseLegend(stitle_dd, SoftwareFX.ChartFX.Docked.Top);
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
				return;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

        #endregion


        #region " Form Event "

		private void rbS_CheckedChanged(object sender, EventArgs e)
		{
			/****************************************************
			 * Comment : 검색구분을 선택하면
			 * 
			 * Created By : bee-jae jung(2010-06-05-토요일)
			 * 
			 * Modified By : bee-jae jung(2010-06-28-월요일)
			 ****************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				//cf01.RPT_1_ChartInit(); cf02.RPT_1_ChartInit(); cf03.RPT_1_ChartInit();
				//cf01.RPT_2_ClearData(); cf02.RPT_2_ClearData(); cf03.RPT_2_ClearData();

				fnSSInitial(SS01);
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
				return;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

        private void btnView_Click(object sender, EventArgs e)
        {
			/****************************************************
			 * Comment : View Button을 클릭하면
			 * 
			 * Created By : bee-jae jung(2010-05-11-화요일)
			 * 
			 * Modified By : bee-jae jung(2010-05-11-화요일)
			 ****************************************************/
            try
            {
				Cursor.Current = Cursors.WaitCursor;

				//cf01.RPT_1_ChartInit(); cf02.RPT_1_ChartInit(); cf03.RPT_1_ChartInit();
				//cf01.RPT_2_ClearData(); cf02.RPT_2_ClearData(); cf03.RPT_2_ClearData();

				fnSSInitial(SS01);

				// 2010-06-02-정비재 : 데이터를 조회한다.
				fnDataFind();
            }
            catch (Exception ex)
            {
				CmnFunction.ShowMsgBox(ex.Message);
				return;
            }
            finally
            {
				Cursor.Current = Cursors.Default;
            }
        }

		private void btnExcelExport_Click(object sender, EventArgs e)
		{
			/****************************************************
			 * Comment : Excel Export Button을 클릭하면
			 * 
			 * Created By : bee-jae jung(2010-05-11-화요일)
			 * 
			 * Modified By : bee-jae jung(2010-05-11-화요일)
			 ****************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				SS01.ExportExcel();
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
				return;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void SS01_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
		{
			/****************************************************
			 * Comment : SS의 Cell을 선택하면
			 * 
			 * Created By : bee-jae jung(2010-06-05-토요일)
			 * 
			 * Modified By : bee-jae jung(2010-06-05-토요일)
			 ****************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				if (e.Row >= 0)
				{
					fnMakeChart(SS01, e.Row);
				}
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
				return;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

        #endregion

        
    }
}
