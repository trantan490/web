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

namespace Hana.YLD
{
    /// <summary>
    /// Ŭ  ��  ��: YLD040603_P1<br/>
    /// Ŭ�������: ������ YIELD �� ��ȸ POPUP<br/>
    /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
    /// �����ۼ���: 2014-02-06<br/>
    /// ��  ����: ������ YIELD �� ��ȸ POPUP<br/>
    /// ����  ����: <br/>
    /// </summary>
    public partial class YLD040603_P1 : Form
    {
        public YLD040603_P1()
        {
            InitializeComponent();
        }

        public YLD040603_P1(string title, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " �� ��ȸ";

            spdData.RPT_ColumnInit();
            try
            {                
                spdData.RPT_AddBasicColumn("Defect name", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("CODE", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Imputation Operation", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("QTY", 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Share compared to defective quantity", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("Cumulative value", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Share of production", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("PPM", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                
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