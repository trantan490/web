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

namespace Hana.PRD
{
    /// <summary>
    /// 클  래  스: PRD010220_P1<br/>
    /// 클래스요약: HOLD 현황(TOAL) 팝업 화면<br/>
    /// 작  성  자: 에이스텍 김태호<br/>
    /// 최초작성일: 2013-09-27<br/>
    /// 상세  설명: HOLD 현황(TOAL) 팝업 화면 (Grand Total의 Hold 컬럼 클릭 시 조회된 전체 hold 현황 조회)<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD010220_P1 : Form
    {
        public PRD010220_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="strTotal"></param>
        public PRD010220_P1(string title, DataTable dt)
        {
            InitializeComponent();

            this.Text = title;

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("Vendor description", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("PACKAGE", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("LD_COUNT", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PART", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("STEP", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("LOT_QTY", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("QTY", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 60);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData.DataSource = dt;

            spdData.RPT_AutoFit(false);

            SetTotalCell();

            dt.Dispose();
        }

        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            spdData.ExportExcel();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// sub total 과 grand total 에 해당하는 cell merge 및 color 변환
        /// </summary>
        private void SetTotalCell()
        {
            //grand total color
            System.Drawing.Color clrGrdTot = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            //sub total color
            System.Drawing.Color clrSubTot = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));
            //group column color
            System.Drawing.Color clrGrpCol = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218)));


            spdData.ActiveSheet.Columns[0, 4].BackColor = clrGrpCol;

            string strCustomer = "";

            for (int i = 0; i < spdData.ActiveSheet.Rows.Count-1; i++)
            {
                if (spdData.ActiveSheet.Cells[i, 1].Value == null)
                {
                    spdData.ActiveSheet.Cells[i, 1, i, spdData.ActiveSheet.Columns.Count - 1].BackColor = clrSubTot;
                    spdData.ActiveSheet.Cells[i, 1].ColumnSpan = 4;
                    spdData.ActiveSheet.Cells[i, 1].Text = strCustomer + " Total";
                    spdData.ActiveSheet.Cells[i, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                }
                else
                {
                    strCustomer = spdData.ActiveSheet.Cells[i, 0].Text;
                }
            }

            spdData.ActiveSheet.Rows[spdData.ActiveSheet.Rows.Count - 1].BackColor = clrGrdTot;
            spdData.ActiveSheet.Cells[spdData.ActiveSheet.Rows.Count - 1, 0].ColumnSpan = 5;
            spdData.ActiveSheet.Cells[spdData.ActiveSheet.Rows.Count - 1, 0].Text = "Total";
            spdData.ActiveSheet.Cells[spdData.ActiveSheet.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
    }
}