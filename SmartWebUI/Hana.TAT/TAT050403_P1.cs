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

namespace Hana.TAT
{
    /// <summary>
    /// Ŭ  ��  ��: TAT050403_P1<br/>
    /// Ŭ�������: ���� TAT ���� �� ��ȸ POPUP<br/>
    /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
    /// �����ۼ���: 2014-08-14<br/>
    /// ��  ����: ���� TAT ���� �� ��ȸ POPUP<br/>
    /// ����  ����: <br/>
    /// 2014-08-22-������ : ���� Target �߰�, ���� Target ��� ���� TAT ���� ǥ�� (��Ǽ�D ��û)
    /// </summary>
    public partial class TAT050403_P1 : Form
    {
        public TAT050403_P1()
        {
            InitializeComponent();
        }

        public TAT050403_P1(string title, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " �� ��ȸ";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 160);
                spdData.RPT_AddBasicColumn("OPER", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LOT ID", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("QTY_1", 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Operation TAT", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Cumulative TAT", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Cumulative Target", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
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

            for (int i = 0; i < spdData.ActiveSheet.RowCount; i++)
            {
                if (Convert.ToDouble(spdData.ActiveSheet.Cells[i, 5].Value) > Convert.ToDouble(spdData.ActiveSheet.Cells[i, 6].Value))
                {
                    spdData.ActiveSheet.Cells[i, 5].BackColor = Color.Red;
                }                
            }

            dt.Dispose();
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