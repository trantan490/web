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
    /// Ŭ  ��  ��: PRD011015_P2<br/>
    /// Ŭ�������: ��ȹ �ܷ� ����͸� �� ��ȸ POPUP - ����κ�<br/>
    /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
    /// �����ۼ���: 2015-03-23<br/>
    /// ��  ����: ��ȹ �ܷ� ����͸� �� ��ȸ POPUP - ����κ�<br/>    
    /// 2015-04-07-������ : ���� STATUS �κ� �߰� (���¼�K ��û)
    /// </summary>
    public partial class PRD011015_P2 : Form
    {
        public PRD011015_P2()
        {
            InitializeComponent();
        }

        public PRD011015_P2(string title, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " �� ��ȸ";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("Equipment model", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("�� ��", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("UPEH", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("CAPA", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Equipment Status", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("RUN", 1, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WAIT", 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DOWN", 1, 6, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Replacement of product", 1, 7, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 4, 4);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);                
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            //spdData.DataSource = dt;
                        
            int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null);

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