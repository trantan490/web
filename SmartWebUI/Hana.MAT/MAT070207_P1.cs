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

namespace Hana.MAT
{
    /// <summary>
    /// 클  래  스: MAT070207_P1<br/>
    /// 클래스요약: 원부자재 유효기간 초과 제품 관리 상세 팝업<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2013-12-11<br/>
    /// 상세  설명: 원부자재 유효기간 초과 제품 관리 상세 팝업<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class MAT070207_P1 : Form
    {
        public MAT070207_P1()
        {
            InitializeComponent();
        }

        public MAT070207_P1(string title, DataTable dt)
        {
            InitializeComponent();           
           
            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("Material code", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Material name", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Storage location", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LOT ID", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Effective date", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("quantity", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("MONTHS", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Elapsed months", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Elapsed days", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);                
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            //spdData.DataSource = dt;
            int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 4);
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