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
    /// Ŭ  ��  ��: PRD010911_P1<br/>
    /// Ŭ�������: ��¥��Ʈ Plan vs Actual �� ��ȸ POPUP<br/>
    /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
    /// �����ۼ���: 2016-06-16<br/>
    /// ��  ����: ��¥��Ʈ Plan vs Actual �� ��ȸ POPUP<br/>
    /// ����  ����: <br/>        
    /// </summary>
    public partial class PRD010911_P1 : Form
    {
        public PRD010911_P1()
        {
            InitializeComponent();
        }

        public PRD010911_P1(string title, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " �� ��ȸ";

            spdData.RPT_ColumnInit();
            try
            {                
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("PART NO", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("RUN ID", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LOT ID", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("QTY", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("WF QTY", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("LOT TYPE", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("STATUS", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);                
                spdData.RPT_AddBasicColumn("Stagnant days", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("COMMENT", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
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