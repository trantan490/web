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
    /// Ŭ  ��  ��: PRD010603_P1<br/>
    /// Ŭ�������: Ship Lot ��ȸ Loss �� �˾�<br/>
    /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
    /// �����ۼ���: 20089-07-01<br/>
    /// ��  ����: Ship Lot ��ȸ Loss �� �˾�<br/>
    /// ����  ����: <br/>
    /// </summary>
    public partial class PRD010603_P1 : Form
    {
        public PRD010603_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lot ID �� �˾����� ������ DataTable�� �Է¹޴� ������
        /// </summary>
        /// <param name="title">LotId</param>
        /// <param name="dt">Lot�� Loss �� ��ȸ</param>
        public PRD010603_P1(string title, DataTable dt)
        {
            InitializeComponent();


            this.Text = title + " " + "Loss Detail Search";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn(title + " Loss �� ��ȸ", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOT ID", 1, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Defect Code", 1, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Defect name", 1, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("quantity", 1, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 0, 4);
            }
            catch(Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 3);

            spdData.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 1, true, Align.Center, VerticalAlign.Center);
            spdData.RPT_AutoFit(false);

            int frmWidth = 0;
            // ��Ʈ�� �ʺ� ����(�÷� �ʺ� + 60[����])
            for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }
            Width = frmWidth + 60;
        }

        private void spdData_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}