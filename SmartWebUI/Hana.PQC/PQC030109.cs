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
 * Comment : 품질 검사원 Gate불량 검출현황
 *
 * Created By : bee-jae jung (2010-05-11-화요일)
 *
 * Modified By : bee-jae jung (2010-05-11-화요일)
 ****************************************************/

namespace Hana.PQC
{
    public partial class PQC030109 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PQC030109 : Program Initial "

        public PQC030109()
        {
            InitializeComponent();
			fnSSInitial(SS01);
			fnSSInitial(SS02);
            cdvFromToDate.AutoBinding();
			fnSSSortInit();
			this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
			cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
		}

        #endregion
        

        #region " Common Function "

		private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
		{
			/****************************************************
			 * Comment : SS의 Header를 설정한다.
			 * 
			 * Created By : bee-jae jung(2010-05-11-화요일)
			 * 
			 * Modified By : bee-jae jung(2010-05-11-화요일)
			 ****************************************************/
			int iIdx = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				SS.RPT_ColumnInit();
				// CUSTOMER
				if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
				{
					iIdx = 0;
					SS.RPT_AddBasicColumn("CUSTOMER", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
				}
				// FAMILY
				if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
				{
					iIdx++;
					SS.RPT_AddBasicColumn("FAMILY", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
				}
				// PACKAGE
				if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
				{
					iIdx++;
					SS.RPT_AddBasicColumn("PACKAGE", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
				}
				// TYPE1
				if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
				{
					iIdx++;
					SS.RPT_AddBasicColumn("TYPE1", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
				}
				// TYPE2
				if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
				{
					iIdx++;
					SS.RPT_AddBasicColumn("TYPE2", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
				}
				// LEAD COUNT
				if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
				{
					iIdx++;
					SS.RPT_AddBasicColumn("LEAD_COUNT", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
				}
				// DENSITY
				if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
				{
					iIdx++;
					SS.RPT_AddBasicColumn("DENSITY", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
				}
				// GENERATION
				if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
				{
					iIdx++;
					SS.RPT_AddBasicColumn("GENERATION", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
				}

				if (iIdx > 0)
				{
					iIdx++;
				}
				switch (SS.Name.ToUpper())
				{
					case "SS01":
						SS.RPT_AddBasicColumn("USER_ID", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
						SS.RPT_AddBasicColumn("USER_DESC", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
						SS.RPT_AddBasicColumn("FAIL_PERCENT", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
						SS.RPT_AddBasicColumn("RETURN_PERCENT", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
						SS.RPT_AddBasicColumn("TOTAL_QTY", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
						SS.RPT_AddBasicColumn("PASS_QTY", 0, iIdx + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
						SS.RPT_AddBasicColumn("FAIL_QTY", 0, iIdx + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
						SS.RPT_AddBasicColumn("RETURN_QTY", 0, iIdx + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
						SS.RPT_AddBasicColumn("DEFECT_QTY", 0, iIdx + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
						break;

					case "SS02":
						SS.RPT_AddBasicColumn("DEFECT_CODE", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
						SS.RPT_AddBasicColumn("DEFECT_DESC", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
						SS.RPT_AddBasicColumn("DEFECT_QTY", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
						// 2010-05-11-정비재 : 일자별로 가로로 출력하는 것은 추후에 작업할 것
						//spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
						break;
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
			 * Created By : bee-jae jung(2010-05-11-화요일)
			 * 
			 * Modified By : bee-jae jung(2010-05-11-화요일)
			 ****************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				((udcTableForm)(this.btnSort.BindingForm)).Clear();
				// CUSTOMER
				if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
				{
					((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
				}
				// FAMILY
				if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
				{
					((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "MAT_GRP_2", "B.MAT_GRP_2", true);
				}
				// PACKAGE
				if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
				{
					((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "MAT_GRP_3", "B.MAT_GRP_3", true);
				}
				// TYPE1
				if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
				{
					((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "MAT_GRP_4", "B.MAT_GRP_4", true);
				}
				// TYPE2
				if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
				{
					((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "MAT_GRP_5", "B.MAT_GRP_5", true);
				}
				// LEAD COUNT
				if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
				{
					((udcTableForm)(this.btnSort.BindingForm)).AddRow("LEAD_COUNT", "B.MAT_GRP_6", "MAT_GRP_6", "B.MAT_GRP_6", true);
				}
				// DENSITY
				if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
				{
					((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "MAT_GRP_7", "B.MAT_GRP_7", true);
				}
				// GENERATION
				if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
				{
					((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "MAT_GRP_8", "B.MAT_GRP_8", true);
				}
				((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "MAT_ID", "A.MAT_ID", true);
				((udcTableForm)(this.btnSort.BindingForm)).AddRow("RETURN_TYPE", "A.RETURN_TYPE", "RETURN_TYPE", "A.RETURN_TYPE", true);
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

		private bool fnDataFind(string sUserID)
		{
			/****************************************************
			 * Comment : 프로그램에서 사용할 Query문을 생성한다.
			 * 
			 * Created By : bee-jae jung(2010-05-11-화요일)
			 * 
			 * Modified By : bee-jae jung(2010-05-11-화요일)
			 ****************************************************/
			StringBuilder sbQuery = new StringBuilder();
			DataTable dtData = null;
			string QRY = "", strFromDate = "", strToDate = "";
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				strFromDate = DateTime.Parse(cdvFromToDate.FromDate.Text).AddDays(-1).ToString("yyyyMMdd") + "220001";
				strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "220000";

				QRY = "SELECT A.DEFECT_CODE AS DEFECT_CODE \n"
					+ "     , B.DESC_1 AS DEFECT_DESC \n"
					+ "     , COUNT(A.LOT_ID) AS DEFECT_COUNT \n"
					+ "  FROM CABRLOTSTS@RPTTOMES A, CABRDFTDEF@RPTTOMES B \n"
					+ " WHERE A.DEFECT_CODE = B.DEFECT_CODE \n"
					+ "   AND A.LOT_ID IN (SELECT LOT_ID \n"
					+ "                      FROM CPQCLOTHIS@RPTTOMES \n"
					+ "                     WHERE FACTORY = '" + cdvFactory.Text + "' \n"
					+ "                       AND TRAN_TIME >= '" + strFromDate + "' \n"
					+ "                       AND TRAN_TIME <= '" + strToDate + "' \n"
					+ "                       AND QC_OPER LIKE '" + (cdvQCOper.Text.Trim() == "ALL" ? "" : cdvQCOper.Text.Trim()) + "%' \n"
					+ "                       AND QC_TYPE LIKE '" + (cdvQCType.Text.Trim() == "ALL" ? "" : cdvQCType.Text.Trim()) + "%' \n"
					+ "                       AND HIST_DEL_FLAG = ' ' \n"
					+ "                       AND MAT_ID LIKE '" + txtSearchProduct.Text.Trim() + "%' \n"
					+ "                       AND TRAN_USER_ID = '" + sUserID + "' \n"
					+ "                       AND QC_FLAG = 'N') \n"
					+ "   AND B.FACTORY = '" + cdvFactory.Text + "' \n"
					+ " GROUP BY A.DEFECT_CODE, B.DESC_1 \n"
					+ " ORDER BY A.DEFECT_CODE ASC";

				if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
				{
					Clipboard.SetText(QRY);
				}

				dtData = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY);

				if (dtData.Rows.Count <= 0)
				{
					dtData.Dispose();
					//CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
					return false;
				}
				SS02.DataSource = dtData;
				SS02.RPT_AutoFit(false);
				dtData.Dispose();

				return true;
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
				return false;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

        #endregion

        
        #region " Common Query "

        private string fnMakeQuery(int iDisplayColumn)
        {
			/****************************************************
			 * Comment : 프로그램에서 사용할 Query문을 생성한다.
			 * 
			 * Created By : bee-jae jung(2010-05-11-화요일)
			 * 
			 * Modified By : bee-jae jung(2010-05-11-화요일)
			 ****************************************************/
            StringBuilder sbQuery = new StringBuilder();
			string QRY = "", strFromDate = "", strToDate = "";
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 검색기간
				switch (cdvFromToDate.DaySelector.SelectedValue.ToString())
				{
					case "DAY":
						strFromDate = DateTime.Parse(cdvFromToDate.FromDate.Text).AddDays(-1).ToString("yyyyMMdd") + "220001";
						strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "220000";
						break;

					default:
						MessageBox.Show("기간은 일(Day) 단위로만 할 수 있습니다!" + "\r\n\r\n"
									  + "일(Day) 단위를 선택하여 주십시오!", this.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return "";
				}

				// 2010-05-12-정비재 : 해당 step에 맞는 데이터를 조회한다.
				QRY = "SELECT A.USER_ID \n"
					+ "     , B.USER_DESC \n";
				switch (iDisplayColumn)
				{
					case 2:		// Fail_Percent
						QRY += "     , ROUND((SUM(A.FAIL_QTY) / COUNT(A.LOT_ID)) * 100, 2) AS FAIL_PERCENT \n";
						break;
					case 3:		// Return_Total
						QRY += "     , ROUND((SUM(A.RETURN_QTY) / COUNT(A.PASS_QTY)) * 100, 2) AS RETURN_PERCENT \n";
						break;
					case 4:		// Total_Qty
						QRY += "     , COUNT(A.LOT_ID) AS TOTAL_QTY \n";
						break;
					case 5:		// Pass_Qty
						QRY += "     , SUM(A.PASS_QTY) AS PASS_QTY \n";
						break;
					case 6:		// Fail_Qty
						QRY += "     , SUM(A.FAIL_QTY) AS FAIL_QTY \n";
						break;
					case 7:		// Return_Qty
						QRY += "     , SUM(A.RETURN_QTY) AS RETURN_QTY \n";
						break;
					case 8:		// Defect_Qty
						QRY += "     , SUM(A.DEFECT_QTY) AS DEFECT_QTY \n";
						break;
					default:		// 전체조회
						QRY += "     , ROUND((SUM(A.FAIL_QTY) / COUNT(A.LOT_ID)) * 100, 2) AS FAIL_PERCENT \n"
							 + "     , ROUND((SUM(A.RETURN_QTY) / COUNT(A.PASS_QTY)) * 100, 2) AS RETURN_PERCENT \n"
							 + "     , COUNT(A.LOT_ID) AS TOTAL_QTY \n"
							 + "     , SUM(A.PASS_QTY) AS PASS_QTY \n"
							 + "     , SUM(A.FAIL_QTY) AS FAIL_QTY \n"
							 + "     , SUM(A.RETURN_QTY) AS RETURN_QTY \n"
							 + "     , SUM(A.DEFECT_QTY) AS DEFECT_QTY \n";
						break;
				}
				QRY += "  FROM (SELECT FACTORY AS FACTORY \n"
				 	 + "             , TRAN_USER_ID AS USER_ID \n"
					 + "             , LOT_ID AS LOT_ID  \n"
					 + "             , CASE WHEN QC_FLAG = 'Y' THEN 1 ELSE 0 END AS PASS_QTY \n"
					 + "             , CASE WHEN QC_FLAG <> 'Y' THEN 1 ELSE 0 END AS FAIL_QTY \n"
					 + "             , CASE WHEN QC_FLAG <> 'Y' THEN TOTAL_DEFECT_QTY_1 ELSE 0 END AS DEFECT_QTY \n"
					 + "             , CASE WHEN NVL((SELECT LOT_ID AS LOT_ID \n"
					 + "                                FROM RWIPLOTHIS \n"
					 + "                               WHERE LOT_ID = 'U' || A.LOT_ID \n"
					 + "                                 AND HIST_SEQ = 1), ' ') <> ' ' THEN 1 ELSE 0 END AS RETURN_QTY \n"
					 + "          FROM CPQCLOTHIS@RPTTOMES A \n"
					 + "         WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
					 + "           AND A.TRAN_TIME >= '" + strFromDate + "' \n"
					 + "           AND A.TRAN_TIME <= '" + strToDate + "' \n"
					 + "           AND A.QC_OPER LIKE '" + (cdvQCOper.Text.Trim() == "ALL" ? "" : cdvQCOper.Text.Trim()) + "%' \n"
					 + "           AND A.QC_TYPE LIKE '" + (cdvQCType.Text.Trim() == "ALL" ? "" : cdvQCType.Text.Trim()) + "%' \n"
					 + "           AND A.MAT_ID LIKE '%' \n"
					 + "           AND A.HIST_DEL_FLAG = ' ') A, MSECUSRDEF@RPTTOMES B \n"
					 + " WHERE A.FACTORY = B.FACTORY \n"
					 + "   AND A.USER_ID = B.USER_ID \n"
					 + " GROUP BY A.USER_ID, B.USER_DESC \n"
					 + " ORDER BY A.USER_ID ASC";

				if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
				{
					Clipboard.SetText(QRY);
				}

				return QRY;
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
				return "";
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
        }

        #endregion


        #region " Common Chart "

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcChartFX CF, int iDisplayColumn)
        {
			/****************************************************
			 * Comment : DataTable을 Chart에 표시한다.
			 * 
			 * Created By : bee-jae jung(2010-05-12-수요일)
			 * 
			 * Modified By : bee-jae jung(2010-05-12-수요일)
			 ****************************************************/
			DataTable dtChart = null;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2010-05-12-정비재 : Chart 초기화
				CF.RPT_1_ChartInit();
				CF.RPT_2_ClearData();

				// 2010-05-12-정비재 : Chart Font 설정
				CF.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
				CF.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
				CF.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
				// 2010-05-12-정비재 : Chart Button 설정
				CF.PointLabels = true; 
				CF.ToolBar = true; 
				CF.Chart3D = true; 
				CF.Cluster = true;
				CF.SerLegBox = true;
				// 2010-05-12-정비재 : Chart Title 설정
				CF.AxisX.Title.Text = "Number";
				CF.AxisY.Title.Text = "사원";
				CF.AxisX.Title.Text = "품질팀 불량검출 현황";
				// 2010-05-12-정비재 : Chart 종류 설정
				CF.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
				CF.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
				//CF.AxisY.Max = CF.AxisY.Max;

				dtChart = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", fnMakeQuery(iDisplayColumn));
				CF.DataSource = dtChart;
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

        private void btnView_Click(object sender, EventArgs e)
        {
			/****************************************************
			 * Comment : View Button을 클릭하면
			 * 
			 * Created By : bee-jae jung(2010-05-11-화요일)
			 * 
			 * Modified By : bee-jae jung(2010-05-11-화요일)
			 ****************************************************/
            DataTable dtData = null;
			string strQuery = "";
            try
            {
				Cursor.Current = Cursors.WaitCursor;
				LoadingPopUp.LoadIngPopUpShow(this);

                // 검색중 화면 표시
				fnSSInitial(SS01);
				fnSSInitial(SS02);

                // Query문으로 데이터를 검색한다.
				strQuery = fnMakeQuery(0);
				if (strQuery.Trim() == "")
                {
                    return;
                }
				dtData = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strQuery);

				if (dtData.Rows.Count <= 0)
                {
					dtData.Dispose();
					cfUser.RPT_1_ChartInit();
					cfUser.RPT_2_ClearData();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }
				SS01.DataSource = dtData;
                SS01.RPT_AutoFit(false);
				dtData.Dispose();

				// 2010-05-12-정비재 : DataTable을 Chart에 표시한다.
				fnMakeChart(cfUser, 0);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
				return;
            }
            finally
            {
				LoadingPopUp.LoadingPopUpHidden();
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
        
		private void txtSearchProduct_Leave(object sender, EventArgs e)
        {
			/****************************************************
			 * Comment : 검색할 제품코드를 입력하면
			 * 
			 * Created By : bee-jae jung(2010-05-11-화요일)
			 * 
			 * Modified By : bee-jae jung(2010-05-11-화요일)
			 ****************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				txtSearchProduct.Text = txtSearchProduct.Text.ToUpper();
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
			 * Comment : SS01의 Cell을 선택하였을 때 
			 * 
			 * Created By : bee-jae jung(2010-05-12-수요일)
			 * 
			 * Modified By : bee-jae jung(2010-05-12-수요일)
			 ****************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				if (e.Column > 1 && e.Column < SS01.Sheets[0].ColumnCount)
				{
					// 2010-05-12-정비재 : 선택한 항목에 대한 Chart를 표시한다.
					fnMakeChart(cfUser, e.Column);
					// 2010-05-12-정비재 : 선택한 항목에 대하여 일자별 자료를 조회한다.
					fnDataFind(SS01.Sheets[0].Cells[e.Row, 0].Text);
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
