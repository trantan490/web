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
    /// Ŭ  ��  ��: PRD010904_P1<br/>
    /// Ŭ�������: ���԰��� LOT�� �� ��ȸ POPUP<br/>
    /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
    /// �����ۼ���: 2011-07-13<br/>
    /// ��  ����: ���԰��� LOT�� �� ��ȸ POPUP<br/>
    /// ����  ����: <br/>
    /// </summary>
    public partial class PRD010904_P1 : Form
    {
        public PRD010904_P1()
        {
            InitializeComponent();
        }

        public PRD010904_P1(string title, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " �� ��ȸ";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("LOT NO", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("RUN NO", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOT TYPE", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("QTY", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);                
                spdData.RPT_AddBasicColumn("WF QTY", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("PRIORITY", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("CREATE TIME", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);                
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