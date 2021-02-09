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

namespace Hana.PRD
{
    /// <summary>
    /// 클  래  스: PRD010309_P2<br/>
    /// 클래스요약: 정체, HOLD 조회 상세 팝업<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2013-10-11<br/>
    /// 상세  설명: 정체, HOLD 조회 상세 팝업<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD010309_P2 : Form
    {
        public PRD010309_P2()
        {
            InitializeComponent();
        }

        public PRD010309_P2(string title, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " 상세 조회";


            spdData22.RPT_ColumnInit();
            try
            {
              
                spdData22.RPT_AddBasicColumn("Step", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData22.RPT_AddBasicColumn("Pin Type", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData22.RPT_AddBasicColumn("Product", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData22.RPT_AddBasicColumn("Lot Id", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData22.RPT_AddBasicColumn("Type", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData22.RPT_AddBasicColumn("Status", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData22.RPT_AddBasicColumn("H Code", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData22.RPT_AddBasicColumn("H Desc", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData22.RPT_AddBasicColumn("Action Department", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData22.RPT_AddBasicColumn("RELEASE department", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData22.RPT_AddBasicColumn("Operation (days)", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData22.RPT_AddBasicColumn("TAT Target", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData22.RPT_AddBasicColumn("Delay in comparison to target", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData22.RPT_AddBasicColumn("QTY", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData22.RPT_AddBasicColumn("Comment", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);                 
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData22.DataSource = dt;
            //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 5);
            //spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

            spdData22.RPT_AutoFit(false);

            int frmWidth = 0;

            for (int i = 0; i < spdData22.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData22.ActiveSheet.GetColumnWidth(i);
            }

            Width = frmWidth + 70;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            spdData22.ExportExcel();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}