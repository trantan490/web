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
    /// Ŭ  ��  ��: PRD010903_P1<br/>
    /// Ŭ�������: �Ｚ IN / OUT PLAN LOT�� �� ��ȸ POPUP<br/>
    /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
    /// �����ۼ���: 2010-07-23<br/>
    /// ��  ����: �Ｚ IN / OUT PLAN LOT�� �� ��ȸ POPUP<br/>
    /// ����  ����: <br/>
    /// </summary>
    public partial class PRD010903_P1 : Form
    {
        public PRD010903_P1()
        {
            InitializeComponent();
        }

        public PRD010903_P1(string title, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " �� ��ȸ";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("NO", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("STEP", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);                
                spdData.RPT_AddBasicColumn("PART NO", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LOT NO", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                
                spdData.RPT_AddBasicColumn("QTY", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                
                spdData.RPT_AddBasicColumn("STATUS", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TAT", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 70);                
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