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
    /// 클  래  스: PRD010306_P1<br/>
    /// 클래스요약: 정체 재공 현황 팝업<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2009-02-03<br/>
    /// 상세  설명: 정체 재공 현황 팝업<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD010306_P1 : Form
    {
        public PRD010306_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 팝업에서 보여줄 Title과 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="dt">해당 공정그룹의 정체 Lot 현황</param>
        public PRD010306_P1(string title, DataTable dt)
        {
            InitializeComponent();

            this.Text = title;

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Package", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot", 0, 5, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Step", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Qty", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Our company day", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Operation Date", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 50);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData.DataSource = dt;
            //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 2, 3);
            //spdData.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 1, true, Align.Center, VerticalAlign.Center);

            spdData.RPT_AutoFit(false);

            int frmWidth = 0;
            for(int i=0; i<spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }
            Width = frmWidth + 60;
        }

        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Close();
        }
    }
}