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
    /// 클  래  스: PRD011016_P2<br/>
    /// 클래스요약: Capa simulation 상세 팝업<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2014-03-07<br/>
    /// 상세  설명: Capa simulation 상세 팝업<br/>
    /// 변경  내용: <br/>
    /// 2014-04-08-임종우 : 성능효율 추가 (박민정 요청)
    /// </summary>
    public partial class PRD011016_P2 : Form
    {
        public PRD011016_P2()
        {
            InitializeComponent();
        }

        public PRD011016_P2(string title, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " 상세 조회";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("System", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Handler", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Test Time", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Index Time", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("yield", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Performance efficiency", 0, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Para", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Capa", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);                
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData.DataSource = dt;
            //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 5);
            //spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

            spdData.RPT_AutoFit(false);

            int frmWidth = 0;

            for(int i=0; i<spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }

            Width = frmWidth + 70;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            spdData.ExportExcel();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}