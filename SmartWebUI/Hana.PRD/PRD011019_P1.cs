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
    /// 클  래  스: PRD011019_P1<br/>
    /// 클래스요약: BIP 모니터링 상세 조회 POPUP<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2015-09-09<br/>
    /// 상세  설명: BIP 모니터링 상세 조회 POPUP<br/>
    /// 변경  내용: <br/>    
    /// </summary>
    public partial class PRD011019_P1 : Form
    {
        public PRD011019_P1()
        {
            InitializeComponent();
        }

        public PRD011019_P1(string title, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " 상세 조회";

            spdData.RPT_ColumnInit();
            try
            {                
                spdData.RPT_AddBasicColumn("Step", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Run No", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot No", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("QTY", 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Status", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Hold Code", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Hold Desc", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Stagnation time", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Comment", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            //spdData.DataSource = dt;
                        
            int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 1, null, null);

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