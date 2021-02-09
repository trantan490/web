using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;
using FarPoint.Win.Spread;

namespace Hana.TRN
{
    public partial class TRNPOPUP : Form
    {
        public TRNPOPUP()
        {
            InitializeComponent();
        }

        public TRNPOPUP(String sFormName, String sTitle, DataTable dt)
        {
            /****************************************************
             * comment : 선택한 항목에 대한 상세내역을 조회한다.
             * 
             * created by : bee-jae jung(2010-09-02-목요일)
             * 
             * modified by : bee-jae jung(2010-09-02-목요일)
             ****************************************************/
            int iIdx = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                InitializeComponent();

                this.Text = sTitle;

                SS.RPT_ColumnInit();

                switch (sFormName.ToUpper())
                {
                    case "TRN090102":       // 2010-09-02-정비재 : Package별 재공이동상태
                        SS.RPT_AddBasicColumn("MAT_ID", 0, iIdx + 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                        SS.RPT_AddBasicColumn("LOT_ID", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                        SS.RPT_AddBasicColumn("FLOW", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                        SS.RPT_AddBasicColumn("OPER", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                        SS.RPT_AddBasicColumn("LOT_TYPE", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                        SS.RPT_AddBasicColumn("QTY_1", 0, iIdx + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        SS.RPT_AddBasicColumn("WAIT_TIME(일)", 0, iIdx + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
                        SS.RPT_AddBasicColumn("START_TIME(일)", 0, iIdx + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
                        SS.RPT_AddBasicColumn("HOLD_TIME (days)", 0, iIdx + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
                        SS.RPT_AddBasicColumn("REWORK_TIME (days)", 0, iIdx + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
                        break;
                }

                SS.DataSource = dt;
                SS.RPT_AutoFit(false);

                int iWidth = 0;
                for (int iCol = 0; iCol < SS.Sheets[0].Columns.Count; iCol++)
                {
                    iWidth += SS.ActiveSheet.GetColumnWidth(iCol);
                }
                this.Width = iWidth + 70;
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

        private void bntExcelExport_Click(object sender, EventArgs e)
        {
            /****************************************************
             * comment : Sheet의 데이터를 Excel로 내보낸다.
             * 
             * created by : bee-jae jung(2010-09-02-목요일)
             * 
             * modified by : bee-jae jung(2010-09-02-목요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS.ExportExcel();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            /****************************************************
             * comment : 확인 버튼을 클릭하면
             * 
             * created by : bee-jae jung(2010-09-02-목요일)
             * 
             * modified by : bee-jae jung(2010-09-02-목요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                this.Close();
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
    }
}