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
 * comment : EIS - 공절별 실적현황
 *
 * created by : bee-jae jung (2010-12-22-수요일)
 *
 * modified by : bee-jae jung (2010-12-22-수요일)
 ****************************************************/

namespace Hana.TRN
{
    public partial class TRN090103 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " TRN090103 : Program Initial "

        public TRN090103()
        {
            InitializeComponent();
			fnSSInitial(SS01);
			fnSSSortInit();

            // 2010-11-30-정비재 : Factory의 초기값을 설정한다.
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            // 2010-11-30-정비재 : 초기값은 K 단위로 사용한다.
            chkKpcs.Checked = true;
            // 2010-11-30-정비재 : 일자별의 검색기간의 초기값을 설정한다.
            cdvFromToDate.FromDate.Value = DateTime.Now.AddMonths(-1);  
            cdvFromToDate.ToDate.Value = DateTime.Now;
            // 2010-11-30-정비재 : 월별의 검색기간의 초기값을 설정한다.
            cdvFromToDate.FromYearMonth.Value = DateTime.Now.AddMonths(-12);
            cdvFromToDate.ToYearMonth.Value = DateTime.Now;
        }

        #endregion
        

        #region " Common Function "

		private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
		{
            /****************************************************
             * Comment : SS의 Header를 설정한다.
             * 
             * Created By : bee-jae jung(2010-12-22-수요일)
             * 
             * Modified By : bee-jae jung(2010-12-22-수요일)
             ****************************************************/
            int iIdx = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

                SS.RPT_ColumnInit();

                if (rbSearch01.Checked == true || rbSearch02.Checked == true)
                {
                    SS.RPT_AddBasicColumn("Customer", 0, iIdx + 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    SS.RPT_AddBasicColumn("PACKAGE", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    if (rbSearch01.Checked == true)
                    {
                        // 2010-11-29-정비재 : 일자별
                        SS.RPT_AddBasicColumn("WORK_DATE", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    }
                    else if(rbSearch02.Checked == true)
                    {
                        // 2010-11-29-정비재 : 월별
                        SS.RPT_AddBasicColumn("WORK_MONTH", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    }
                    SS.RPT_AddBasicColumn("ASSY_ISSUE_LOT", 0, iIdx + 3, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("ASSY_ISSUE_QTY", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("BACKGRIND_LOT", 0, iIdx + 5, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("BACKGRIND_QTY", 0, iIdx + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("SAWING_LOT", 0, iIdx + 7, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("SAWING_QTY", 0, iIdx + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("DIEATTACH_LOT", 0, iIdx + 9, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("DIEATTACH_QTY", 0, iIdx + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("WIREBONDING_LOT", 0, iIdx + 11, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("WIREBONDING_QTY", 0, iIdx + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("MOLDING_LOT", 0, iIdx + 13, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("MOLDING_QTY", 0, iIdx + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("PVI_LOT", 0, iIdx + 15, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("PVI_QTY", 0, iIdx + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("TEST_ISSUE_LOT", 0, iIdx + 17, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("TEST_ISSUE_QTY", 0, iIdx + 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("FINALTEST_LOT", 0, iIdx + 19, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("FINALTEST_QTY", 0, iIdx + 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("PACKING_LOT", 0, iIdx + 21, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("PACKING_QTY", 0, iIdx + 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("FGS_LOT", 0, iIdx + 23, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("FGS_QTY", 0, iIdx + 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                }
                else if (rbSearch03.Checked == true || rbSearch04.Checked == true)
                {
                    if (rbSearch03.Checked == true)
                    {
                        // 2010-11-29-정비재 : 일자별
                        SS.RPT_AddBasicColumn("WORK_DATE", 0, iIdx + 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    }
                    else if (rbSearch04.Checked == true)
                    {
                        // 2010-11-29-정비재 : 월별
                        SS.RPT_AddBasicColumn("WORK_MONTH", 0, iIdx + 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    }
                    SS.RPT_AddBasicColumn("ASSY_ISSUE_LOT", 0, iIdx + 1, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("ASSY_ISSUE_QTY", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("BACKGRIND_LOT", 0, iIdx + 3, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("BACKGRIND_QTY", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("SAWING_LOT", 0, iIdx + 5, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("SAWING_QTY", 0, iIdx + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("DIEATTACH_LOT", 0, iIdx + 7, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("DIEATTACH_QTY", 0, iIdx + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("WIREBONDING_LOT", 0, iIdx + 9, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("WIREBONDING_QTY", 0, iIdx + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("MOLDING_LOT", 0, iIdx + 11, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("MOLDING_QTY", 0, iIdx + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("PVI_LOT", 0, iIdx + 13, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("PVI_QTY", 0, iIdx + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("TEST_ISSUE_LOT", 0, iIdx + 15, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("TEST_ISSUE_QTY", 0, iIdx + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("FINALTEST_LOT", 0, iIdx + 17, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("FINALTEST_QTY", 0, iIdx + 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("PACKING_LOT", 0, iIdx + 19, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("PACKING_QTY", 0, iIdx + 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("FGS_LOT", 0, iIdx + 21, (chkLotVisibleTrue.Checked == true ? Visibles.True : Visibles.False), Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("FGS_QTY", 0, iIdx + 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
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

		private void fnSSSortInit()
		{
            /****************************************************
             * Comment : SS의 데이터의 정렬규칙을 설정하다.
             * 
             * Created By : bee-jae jung(2010-12-22-수요일)
             * 
             * Modified By : bee-jae jung(2010-12-22-수요일)
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

        private Boolean fnBusinessRule()
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : bee-jae jung(2010-12-22-수요일)
             * 
             * Modified By : bee-jae jung(2010-12-22-수요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private Boolean fnDataFind()
        {
            /****************************************************
             * Comment : DataBase에 저장된 데이터를 조회한다.
             * 
             * Created By : bee-jae jung(2010-12-22-수요일)
             * 
             * Modified By : bee-jae jung(2010-12-22-수요일)
             ****************************************************/
            String Qry = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                if (fnBusinessRule() == false)
                {
                    return false;
                }

                if (chkAccumulate.Checked == true)
                {
                    // 2011-03-26-정비재 : 누적계산을 위하여 추가함.
                    if (rbSearch01.Checked == true || rbSearch02.Checked == true)
                    {
                        Qry = "SELECT CUSTOMER AS CUSTOMER \n"
                            + "     , PACKAGE AS PACKAGE \n";
                        if (rbSearch01.Checked == true)
                        {
                            // 2010-12-22-정비재 : 일자별 데이터를 검색한다.
                            Qry += "     , WORK_DATE AS WORK_DATE \n";
                        }
                        else if (rbSearch02.Checked == true)
                        {
                            // 2010-12-22-정비재 : 월별 데이터를 검색한다.
                            Qry += "     , WORK_MONTH AS WORK_MONTH \n";
                        }
                        Qry += "     , SUM(ASSY_ISSUE_LOT) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS ASSY_ISSUE_LOT \n"
                             + "     , SUM(ASSY_ISSUE_QTY) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS ASSY_ISSUE_QTY \n"
                             + "     , SUM(BACKGRIND_LOT) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS BACKGRIND_LOT \n"
                             + "     , SUM(BACKGRIND_QTY) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS BACKGRIND_QTY \n"
                             + "     , SUM(SAWING_LOT) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SAWING_LOT \n"
                             + "     , SUM(SAWING_QTY) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SAWING_QTY \n"
                             + "     , SUM(DIEATTACH_LOT) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS DIEATTACH_LOT \n"
                             + "     , SUM(DIEATTACH_QTY) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS DIEATTACH_QTY \n"
                             + "     , SUM(WIREBONDING_LOT) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS WIREBONDING_LOT \n"
                             + "     , SUM(WIREBONDING_QTY) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS WIREBONDING_QTY \n"
                             + "     , SUM(MOLDING_LOT) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS MOLDING_LOT \n"
                             + "     , SUM(MOLDING_QTY) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS MOLDING_QTY \n"
                             + "     , SUM(PVI_LOT) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS PVI_LOT \n"
                             + "     , SUM(PVI_QTY) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS PVI_QTY \n"
                             + "     , SUM(TEST_ISSUE_LOT) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TEST_ISSUE_LOT \n"
                             + "     , SUM(TEST_ISSUE_QTY) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TEST_ISSUE_QTY \n"
                             + "     , SUM(FINALTEST_LOT) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS FINALTEST_LOT \n"
                             + "     , SUM(FINALTEST_QTY) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS FINALTEST_QTY \n"
                             + "     , SUM(PACKING_LOT) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS PACKING_LOT \n"
                             + "     , SUM(PACKING_QTY) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS PACKING_QTY \n"
                             + "     , SUM(FGS_LOT) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS FGS_LOT \n"
                             + "     , SUM(FGS_QTY) OVER(ORDER BY CUSTOMER, PACKAGE, " + (rbSearch01.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS FGS_QTY \n"
                             + "  FROM (SELECT CUSTOMER AS CUSTOMER \n"
                             + "             , PACKAGE AS PACKAGE \n";
                        if (rbSearch01.Checked == true)
                        {
                            // 2010-12-22-정비재 : 일자별 데이터를 검색한다.
                            Qry += "             , TO_CHAR(TO_DATE(WORK_DATE, 'YYYYMMDDHH24'), 'YYYY-MM-DD') AS WORK_DATE \n";
                        }
                        else if (rbSearch02.Checked == true)
                        {
                            // 2010-12-22-정비재 : 월별 데이터를 검색한다.
                            Qry += "              , TO_CHAR(TO_DATE(WORK_MONTH, 'YYYYMM'), 'YYYY-MM') AS WORK_MONTH \n";
                        }
                        Qry += "              , SUM(ASSY_ISSUE_LOT) AS ASSY_ISSUE_LOT \n"
                             + "              , SUM(ASSY_ISSUE_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS ASSY_ISSUE_QTY \n"
                             + "              , SUM(BACKGRIND_LOT) AS BACKGRIND_LOT \n"
                             + "              , SUM(BACKGRIND_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS BACKGRIND_QTY \n"
                             + "              , SUM(SAWING_LOT) AS SAWING_LOT \n"
                             + "              , SUM(SAWING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS SAWING_QTY \n"
                             + "              , SUM(DIEATTACH_LOT) AS DIEATTACH_LOT \n"
                             + "              , SUM(DIEATTACH_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS DIEATTACH_QTY \n"
                             + "              , SUM(WIREBONDING_LOT) AS WIREBONDING_LOT \n"
                             + "              , SUM(WIREBONDING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS WIREBONDING_QTY \n"
                             + "              , SUM(MOLDING_LOT) AS MOLDING_LOT \n"
                             + "              , SUM(MOLDING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS MOLDING_QTY \n"
                             + "              , SUM(PVI_LOT) AS PVI_LOT \n"
                             + "              , SUM(PVI_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS PVI_QTY \n"
                             + "              , SUM(TEST_ISSUE_LOT) AS TEST_ISSUE_LOT \n"
                             + "              , SUM(TEST_ISSUE_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS TEST_ISSUE_QTY \n"
                             + "              , SUM(FINALTEST_LOT) AS FINALTEST_LOT \n"
                             + "              , SUM(FINALTEST_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS FINALTEST_QTY \n"
                             + "              , SUM(PACKING_LOT) AS PACKING_LOT \n"
                             + "              , SUM(PACKING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS PACKING_QTY \n"
                             + "              , SUM(FGS_LOT) AS FGS_LOT \n"
                             + "              , SUM(FGS_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS FGS_QTY \n"
                             + "           FROM REISWIPEND \n"
                             + "          WHERE CUSTOMER LIKE '" + cdvCustomer.Text + "%' \n"
                             + "            AND PACKAGE LIKE '" + cdvPackage.Text + "%' \n";
                        if (rbSearch01.Checked == true)
                        {
                            // 2010-11-29-정비재 : 일자별 데이터를 검색한다.
                            Qry += "            AND WORK_DATE >= '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' \n"
                                 + "            AND WORK_DATE <= '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' \n"
                                 + "          GROUP BY CUSTOMER, PACKAGE, WORK_DATE) \n"
                                 + " ORDER BY CUSTOMER ASC, PACKAGE ASC, WORK_DATE DESC";
                        }
                        else if (rbSearch02.Checked == true)
                        {
                            // 2010-11-29-정비재 : 월별 데이터를 검색한다.
                            Qry += "            AND WORK_MONTH >= '" + cdvFromToDate.FromYearMonth.Text.Replace("-", "") + "' \n"
                                 + "            AND WORK_MONTH <= '" + cdvFromToDate.ToYearMonth.Text.Replace("-", "") + "' \n"
                                 + "          GROUP BY CUSTOMER, PACKAGE, WORK_MONTH) \n"
                                 + " ORDER BY CUSTOMER ASC, PACKAGE ASC, WORK_MONTH DESC";
                        }
                    }
                    else if (rbSearch03.Checked == true || rbSearch04.Checked == true)
                    {
                        if (rbSearch03.Checked == true)
                        {
                            // 2010-11-29-정비재 : 일자별 데이터를 검색한다.
                            Qry = "SELECT WORK_DATE AS WORK_DATE \n";
                        }
                        else if (rbSearch04.Checked == true)
                        {
                            // 2010-11-29-정비재 : 월별 데이터를 검색한다.
                            Qry = "SELECT WORK_MONTH AS WORK_MONTH \n";
                        }
                        Qry += "     , SUM(ASSY_ISSUE_LOT) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS ASSY_ISSUE_LOT \n"
                             + "     , SUM(ASSY_ISSUE_QTY) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS ASSY_ISSUE_QTY \n"
                             + "     , SUM(BACKGRIND_LOT) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS BACKGRIND_LOT \n"
                             + "     , SUM(BACKGRIND_QTY) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS BACKGRIND_QTY \n"
                             + "     , SUM(SAWING_LOT) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SAWING_LOT \n"
                             + "     , SUM(SAWING_QTY) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SAWING_QTY \n"
                             + "     , SUM(DIEATTACH_LOT) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS DIEATTACH_LOT \n"
                             + "     , SUM(DIEATTACH_QTY) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS DIEATTACH_QTY \n"
                             + "     , SUM(WIREBONDING_LOT) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS WIREBONDING_LOT \n"
                             + "     , SUM(WIREBONDING_QTY) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS WIREBONDING_QTY \n"
                             + "     , SUM(MOLDING_LOT) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS MOLDING_LOT \n"
                             + "     , SUM(MOLDING_QTY) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS MOLDING_QTY \n"
                             + "     , SUM(PVI_LOT) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS PVI_LOT \n"
                             + "     , SUM(PVI_QTY) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS PVI_QTY \n"
                             + "     , SUM(TEST_ISSUE_LOT) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TEST_ISSUE_LOT \n"
                             + "     , SUM(TEST_ISSUE_QTY) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TEST_ISSUE_QTY \n"
                             + "     , SUM(FINALTEST_LOT) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS FINALTEST_LOT \n"
                             + "     , SUM(FINALTEST_QTY) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS FINALTEST_QTY \n"
                             + "     , SUM(PACKING_LOT) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS PACKING_LOT \n"
                             + "     , SUM(PACKING_QTY) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS PACKING_QTY \n"
                             + "     , SUM(FGS_LOT) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS FGS_LOT \n"
                             + "     , SUM(FGS_QTY) OVER(ORDER BY " + (rbSearch03.Checked == true ? "WORK_DATE" : "WORK_MONTH") + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS FGS_QTY \n";
                        if (rbSearch03.Checked == true)
                        {
                            // 2010-11-29-정비재 : 일자별 데이터를 검색한다.
                            Qry += "  FROM (SELECT TO_CHAR(TO_DATE(WORK_DATE, 'YYYYMMDDHH24'), 'YYYY-MM-DD') AS WORK_DATE \n";
                        }
                        else if (rbSearch04.Checked == true)
                        {
                            // 2010-11-29-정비재 : 월별 데이터를 검색한다.
                            Qry += "  FROM (SELECT TO_CHAR(TO_DATE(WORK_MONTH, 'YYYYMM'), 'YYYY-MM') AS WORK_MONTH \n";
                        }
                        Qry += "              , SUM(ASSY_ISSUE_LOT) AS ASSY_ISSUE_LOT \n"
                             + "              , SUM(ASSY_ISSUE_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS ASSY_ISSUE_QTY \n"
                             + "              , SUM(BACKGRIND_LOT) AS BACKGRIND_LOT \n"
                             + "              , SUM(BACKGRIND_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS BACKGRIND_QTY \n"
                             + "              , SUM(SAWING_LOT) AS SAWING_LOT \n"
                             + "              , SUM(SAWING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS SAWING_QTY \n"
                             + "              , SUM(DIEATTACH_LOT) AS DIEATTACH_LOT \n"
                             + "              , SUM(DIEATTACH_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS DIEATTACH_QTY \n"
                             + "              , SUM(WIREBONDING_LOT) AS WIREBONDING_LOT \n"
                             + "              , SUM(WIREBONDING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS WIREBONDING_QTY \n"
                             + "              , SUM(MOLDING_LOT) AS MOLDING_LOT \n"
                             + "              , SUM(MOLDING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS MOLDING_QTY \n"
                             + "              , SUM(PVI_LOT) AS PVI_LOT \n"
                             + "              , SUM(PVI_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS PVI_QTY \n"
                             + "              , SUM(TEST_ISSUE_LOT) AS TEST_ISSUE_LOT \n"
                             + "              , SUM(TEST_ISSUE_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS TEST_ISSUE_QTY \n"
                             + "              , SUM(FINALTEST_LOT) AS FINALTEST_LOT \n"
                             + "              , SUM(FINALTEST_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS FINALTEST_QTY \n"
                             + "              , SUM(PACKING_LOT) AS PACKING_LOT \n"
                             + "              , SUM(PACKING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS PACKING_QTY \n"
                             + "              , SUM(FGS_LOT) AS FGS_LOT \n"
                             + "              , SUM(FGS_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS FGS_QTY \n"
                             + "           FROM REISWIPEND \n"
                             + "          WHERE CUSTOMER LIKE '" + cdvCustomer.Text + "%' \n"
                             + "            AND PACKAGE LIKE '" + cdvPackage.Text + "%' \n";
                        if (rbSearch03.Checked == true)
                        {
                            // 2010-11-29-정비재 : 일자별 데이터를 검색한다.
                            Qry += "            AND WORK_DATE >= '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' \n"
                                 + "            AND WORK_DATE <= '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' \n"
                                 + "          GROUP BY WORK_DATE) \n"
                                 + " ORDER BY WORK_DATE DESC";
                        }
                        else if (rbSearch04.Checked == true)
                        {
                            // 2010-11-29-정비재 : 월별 데이터를 검색한다.
                            Qry += "            AND WORK_MONTH >= '" + cdvFromToDate.FromYearMonth.Text.Replace("-", "") + "' \n"
                                 + "            AND WORK_MONTH <= '" + cdvFromToDate.ToYearMonth.Text.Replace("-", "") + "' \n"
                                 + "          GROUP BY WORK_MONTH) \n"
                                 + " ORDER BY WORK_MONTH DESC";
                        }
                    }
                }
                else
                {
                    if (rbSearch01.Checked == true || rbSearch02.Checked == true)
                    {
                        Qry = "SELECT CUSTOMER AS CUSTOMER \n"
                            + "     , PACKAGE AS PACKAGE \n";
                        if (rbSearch01.Checked == true)
                        {
                            // 2010-12-22-정비재 : 일자별 데이터를 검색한다.
                            Qry += "     , TO_CHAR(TO_DATE(WORK_DATE, 'YYYYMMDDHH24'), 'YYYY-MM-DD') AS WORK_DATE \n";
                        }
                        else if (rbSearch02.Checked == true)
                        {
                            // 2010-12-22-정비재 : 월별 데이터를 검색한다.
                            Qry += "     , TO_CHAR(TO_DATE(WORK_MONTH, 'YYYYMM'), 'YYYY-MM') AS WORK_MONTH \n";
                        }
                        Qry += "     , SUM(ASSY_ISSUE_LOT) AS ASSY_ISSUE_LOT \n"
                             + "     , SUM(ASSY_ISSUE_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS ASSY_ISSUE_QTY \n"
                             + "     , SUM(BACKGRIND_LOT) AS BACKGRIND_LOT \n"
                             + "     , SUM(BACKGRIND_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS BACKGRIND_QTY \n"
                             + "     , SUM(SAWING_LOT) AS SAWING_LOT \n"
                             + "     , SUM(SAWING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS SAWING_QTY \n"
                             + "     , SUM(DIEATTACH_LOT) AS DIEATTACH_LOT \n"
                             + "     , SUM(DIEATTACH_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS DIEATTACH_QTY \n"
                             + "     , SUM(WIREBONDING_LOT) AS WIREBONDING_LOT \n"
                             + "     , SUM(WIREBONDING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS WIREBONDING_QTY \n"
                             + "     , SUM(MOLDING_LOT) AS MOLDING_LOT \n"
                             + "     , SUM(MOLDING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS MOLDING_QTY \n"
                             + "     , SUM(PVI_LOT) AS PVI_LOT \n"
                             + "     , SUM(PVI_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS PVI_QTY \n"
                             + "     , SUM(TEST_ISSUE_LOT) AS TEST_ISSUE_LOT \n"
                             + "     , SUM(TEST_ISSUE_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS TEST_ISSUE_QTY \n"
                             + "     , SUM(FINALTEST_LOT) AS FINALTEST_LOT \n"
                             + "     , SUM(FINALTEST_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS FINALTEST_QTY \n"
                             + "     , SUM(PACKING_LOT) AS PACKING_LOT \n"
                             + "     , SUM(PACKING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS PACKING_QTY \n"
                             + "     , SUM(FGS_LOT) AS FGS_LOT \n"
                             + "     , SUM(FGS_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS FGS_QTY \n"
                             + "  FROM REISWIPEND \n"
                             + " WHERE CUSTOMER LIKE '" + cdvCustomer.Text + "%' \n"
                             + "   AND PACKAGE LIKE '" + cdvPackage.Text + "%' \n";
                        if (rbSearch01.Checked == true)
                        {
                            // 2010-11-29-정비재 : 일자별 데이터를 검색한다.
                            Qry += "   AND WORK_DATE >= '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' \n"
                                 + "   AND WORK_DATE <= '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' \n"
                                 + " GROUP BY CUSTOMER, PACKAGE, WORK_DATE \n"
                                 + " ORDER BY CUSTOMER ASC, PACKAGE ASC, WORK_DATE DESC";
                        }
                        else if (rbSearch02.Checked == true)
                        {
                            // 2010-11-29-정비재 : 월별 데이터를 검색한다.
                            Qry += "   AND WORK_MONTH >= '" + cdvFromToDate.FromYearMonth.Text.Replace("-", "") + "' \n"
                                 + "   AND WORK_MONTH <= '" + cdvFromToDate.ToYearMonth.Text.Replace("-", "") + "' \n"
                                 + " GROUP BY CUSTOMER, PACKAGE, WORK_MONTH \n"
                                 + " ORDER BY CUSTOMER ASC, PACKAGE ASC, WORK_MONTH DESC";
                        }
                    }
                    else if (rbSearch03.Checked == true || rbSearch04.Checked == true)
                    {
                        if (rbSearch03.Checked == true)
                        {
                            // 2010-11-29-정비재 : 일자별 데이터를 검색한다.
                            Qry = "SELECT TO_CHAR(TO_DATE(WORK_DATE, 'YYYYMMDDHH24'), 'YYYY-MM-DD') AS WORK_DATE \n";
                        }
                        else if (rbSearch04.Checked == true)
                        {
                            // 2010-11-29-정비재 : 월별 데이터를 검색한다.
                            Qry = "SELECT TO_CHAR(TO_DATE(WORK_MONTH, 'YYYYMM'), 'YYYY-MM') AS WORK_MONTH \n";
                        }
                        Qry += "     , SUM(ASSY_ISSUE_LOT) AS ASSY_ISSUE_LOT \n"
                             + "     , SUM(ASSY_ISSUE_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS ASSY_ISSUE_QTY \n"
                             + "     , SUM(BACKGRIND_LOT) AS BACKGRIND_LOT \n"
                             + "     , SUM(BACKGRIND_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS BACKGRIND_QTY \n"
                             + "     , SUM(SAWING_LOT) AS SAWING_LOT \n"
                             + "     , SUM(SAWING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS SAWING_QTY \n"
                             + "     , SUM(DIEATTACH_LOT) AS DIEATTACH_LOT \n"
                             + "     , SUM(DIEATTACH_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS DIEATTACH_QTY \n"
                             + "     , SUM(WIREBONDING_LOT) AS WIREBONDING_LOT \n"
                             + "     , SUM(WIREBONDING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS WIREBONDING_QTY \n"
                             + "     , SUM(MOLDING_LOT) AS MOLDING_LOT \n"
                             + "     , SUM(MOLDING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS MOLDING_QTY \n"
                             + "     , SUM(PVI_LOT) AS PVI_LOT \n"
                             + "     , SUM(PVI_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS PVI_QTY \n"
                             + "     , SUM(TEST_ISSUE_LOT) AS TEST_ISSUE_LOT \n"
                             + "     , SUM(TEST_ISSUE_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS TEST_ISSUE_QTY \n"
                             + "     , SUM(FINALTEST_LOT) AS FINALTEST_LOT \n"
                             + "     , SUM(FINALTEST_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS FINALTEST_QTY \n"
                             + "     , SUM(PACKING_LOT) AS PACKING_LOT \n"
                             + "     , SUM(PACKING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS PACKING_QTY \n"
                             + "     , SUM(FGS_LOT) AS FGS_LOT \n"
                             + "     , SUM(FGS_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS FGS_QTY \n"
                             + "  FROM REISWIPEND \n"
                             + " WHERE CUSTOMER LIKE '" + cdvCustomer.Text + "%' \n"
                             + "   AND PACKAGE LIKE '" + cdvPackage.Text + "%' \n";
                        if (rbSearch03.Checked == true)
                        {
                            // 2010-11-29-정비재 : 일자별 데이터를 검색한다.
                            Qry += "   AND WORK_DATE >= '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' \n"
                                 + "   AND WORK_DATE <= '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' \n"
                                 + " GROUP BY WORK_DATE \n"
                                 + " ORDER BY WORK_DATE DESC";
                        }
                        else if (rbSearch04.Checked == true)
                        {
                            // 2010-11-29-정비재 : 월별 데이터를 검색한다.
                            Qry += "   AND WORK_MONTH >= '" + cdvFromToDate.FromYearMonth.Text.Replace("-", "") + "' \n"
                                 + "   AND WORK_MONTH <= '" + cdvFromToDate.ToYearMonth.Text.Replace("-", "") + "' \n"
                                 + " GROUP BY WORK_MONTH \n"
                                 + " ORDER BY WORK_MONTH DESC";
                        }
                    }
                }

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Qry);

                if (dt.Rows.Count <= 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return false;
                }

                SS01.DataSource = dt;
                SS01.RPT_AutoFit(false);
                // 2010-11-29-정비재 : 검색된 내용을 Sheet에 Display한다.
                fnMakeChart(SS01);          

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

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
		{
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : bee-jae jung(2010-12-22-수요일)
             * 
             * Modified By : bee-jae jung(2010-12-22-수요일)
             ****************************************************/
            int[] iRowDataY1;
            int[] iRowDataY2;
			int iRow = 0;
            double dMaxY = 0, dMaxY_Temp = 0, dMaxY2 = 0, dMaxY2_Temp = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2010-11-29-정비재 : SS에 데이터가 없으면 종료한다.
                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                // 2010-11-29-정비재 : Chart에서 Ship수량을 빼고 Chart를 그리기 위하여
                iRowDataY1 = new int[SS.Sheets[0].RowCount];
                iRowDataY2 = new int[SS.Sheets[0].RowCount];

                // 2010-09-01-정비재 : Qty 및 Time에 대한 Bar Chart를 Display한다.
                for (iRow = 0; iRow < iRowDataY1.Length; iRow++)
                {
                    iRowDataY1[iRow] = (iRowDataY1.Length - 1) - iRow;
                    iRowDataY2[iRow] = (iRowDataY1.Length - 1) - iRow;
                }

                // 2015-12-02-정비재 : chart에 사용한 title을 설정한다.
                String[] strSeries = new String[] { "ASSY_ISSUE_LOT", "ASSY_ISSUE_QTY", "BACKGRIND_LOT", "BACKGRIND_QTY"
                                                  , "SAWING_LOT", "SAWING_QTY", "DIEATTACH_LOT", "DIEATTACH_QTY"
                                                  , "WIREBONDING_LOT", "WIREBONDING_QTY", "MOLDING_LOT", "MOLDING_QTY"
                                                  , "PVI_LOT", "PVI_QTY", "TEST_ISSUE_LOT", "TEST_ISSUE_QTY"
                                                  , "FINALTEST_LOT", "FINALTEST_QTY", "PACKING_LOT", "PACKING_QTY"
                                                  , "FGS_LOT", "FGS_QTY"};


                // STEP 1 : Chart를 이미지를 초기화 함
                udcMSChart1.RPT_1_ChartInit();
                // STEP 2 : Chart를 Display하는 데이터를 초기화 함.
                udcMSChart1.RPT_2_ClearData();
                // STEP 3 : Chart를 그리기 위하여 Open
                udcMSChart1.RPT_3_OpenData(strSeries.Length, iRowDataY1.Length);

                if (rbSearch01.Checked == true || rbSearch02.Checked == true)
                {
                    // STEP 4 : 입력된 데이터로 Chart를 Display한다.
                    // 2010-11-29-정비재 : BarChart 그리기 및 BarChart의 최대값 계산
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 3 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 5 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 7 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 9 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 11 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 13 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 15 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 17 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 19 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 21 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 23 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);

                    // 2010-11-29-정비재 : LineChart 그리기 및 LineChart의 최대값 계산
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 4 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 6 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 8 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 10 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 12 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 14 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 16 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 18 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 20 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 22 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 24 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);

                    // STEP 5 : Chart를 닫음
                    udcMSChart1.RPT_5_CloseData();

                    // STEP 6 : Chart를 Display한다.
                    // 2010-11-29-정비재 : BarChart를 그림
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 0, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 1, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 2, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 3, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 4, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 5, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 6, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 7, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 8, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 9, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 10, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);

                    // 2010-09-01-정비재 : LineChart를 그림
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 11, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 12, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 13, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 14, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 15, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 16, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 17, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 18, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 19, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 20, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 21, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                }
                else if (rbSearch03.Checked == true || rbSearch04.Checked == true)
                {
                    // STEP 4 : 입력된 데이터로 Chart를 Display한다.
                    // 2010-11-30-정비재 : BarChart 그리기 및 BarChart의 최대값 계산
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 1 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 3 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 5 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 7 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 9 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 11 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 13 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 15 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 17 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 19 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 21 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);

                    // 2010-11-30-정비재 : LineChart 그리기 및 LineChart의 최대값 계산
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 2 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 4 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 6 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 8 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 10 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 12 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 14 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 16 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 18 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 20 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 22 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);

                    // STEP 5 : Chart를 닫음
                    udcMSChart1.RPT_5_CloseData();

                    // STEP 6 : Chart를 Display한다.
                    // 2010-11-30-정비재 : BarChart를 그림
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 0, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 1, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 2, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 3, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 4, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 5, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 6, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 7, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 8, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 9, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 10, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);

                    // 2010-09-01-정비재 : LineChart를 그림
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 11, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 12, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 13, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 14, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 15, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 16, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 17, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 18, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 19, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 20, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 21, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                }

                // STEP 7 : Chart의 X축의 Title을 설정한다.
                udcMSChart1.RPT_7_SetXAsixTitleUsingSpread(SS, iRowDataY1, strSeries.Length);
                udcMSChart1.RPT_8_SetSeriseLegend(strSeries, System.Windows.Forms.DataVisualization.Charting.Docking.Top);
			}
			catch (Exception ex)
			{
                LoadingPopUp.LoadingPopUpHidden();
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

        private void rbSearch_CheckedChanged(object sender, EventArgs e)
        {
            /****************************************************
             * comment : 일/월을 선택하면 발생되는 이벤트
             * 
             * created by : bee-jae jung(2010-12-22-수요일)
             * 
             * modified by : bee-jae jung(2010-12-22-수요일)
             ****************************************************/
            try
            {
				Cursor.Current = Cursors.WaitCursor;

                if (rbSearch01.Checked == true || rbSearch03.Checked == true)
                {
                    cdvFromToDate.DaySelector.Text = "일"; 
                }
                else if (rbSearch02.Checked == true || rbSearch04.Checked == true)
                {
                    cdvFromToDate.DaySelector.Text = "월";
                }

                fnSSInitial(SS01);
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

        private void btnView_Click(object sender, EventArgs e)
        {
            /****************************************************
             * comment : View Button을 클릭하면
             * 
             * created by : bee-jae jung(2010-12-22-수요일)
             * 
             * modified by : bee-jae jung(2010-12-22-수요일)
             ****************************************************/
            try
            {
				Cursor.Current = Cursors.WaitCursor;

                udcMSChart1.RPT_1_ChartInit();
                udcMSChart1.RPT_2_ClearData();

				fnSSInitial(SS01);                  // 2010-08-30-정비재 : Sheet Control을 초기화 한다.
                fnDataFind();                       // 2010-11-29-정비재 : 검색조건에 일치하는 데이터를 검색한다.
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

		private void btnExcelExport_Click(object sender, EventArgs e)
		{
            /****************************************************
             * Comment : Excel Export Button을 클릭하면
             * 
             * Created By : bee-jae jung(2010-12-22-수요일)
             * 
             * Modified By : bee-jae jung(2010-12-22-수요일)
             ****************************************************/
            try
			{
				Cursor.Current = Cursors.WaitCursor;

				SS01.ExportExcel();
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

        #endregion        

       
    }
}
