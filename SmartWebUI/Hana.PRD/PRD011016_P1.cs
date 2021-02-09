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
    /// Ŭ  ��  ��: PRD011016_P1<br/>
    /// Ŭ�������: CAPA SIMULATION �������� �̵�� �˾�<br/>
    /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
    /// �����ۼ���: 2013-11-14<br/>
    /// ��  ����: CAPA SIMULATION �������� �̵�� �˾�<br/>
    /// ����  ����: <br/>
    /// </summary>
    public partial class PRD011016_P1 : Form
    {
        public PRD011016_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lot ID �� �˾����� ������ DataTable�� �Է¹޴� ������
        /// </summary>
        /// <param name="title">LotId</param>
        /// <param name="dt">Lot�� Loss �� ��ȸ</param>
        public PRD011016_P1(DataTable dt)
        {
            InitializeComponent();

            this.Text = "List of Standard information Unregistered product";

            spdData.RPT_ColumnInit();
            try
            {                
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 70);                
            }
            catch(Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData.DataSource = dt;
            //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 3);

            //spdData.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 1, true, Align.Center, VerticalAlign.Center);
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