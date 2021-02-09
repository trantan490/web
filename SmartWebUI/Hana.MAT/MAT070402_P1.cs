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
    /// 클  래  스: MAT070402_P1<br/>
    /// 클래스요약: 원부자재 소요량 조회 화면에서 발주량 클릭시 팝업 <br/>
    /// 작  성  자: 하나마이크론 김미경<br/>
    /// 최초작성일: 2019-11-21<br/>
    /// 상세  설명: SAP 자재에 대한 구매 문서 팝업<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class MAT070402_P1 : Form
    {
        public MAT070402_P1()
        {
            InitializeComponent();
        }

        public MAT070402_P1(string title, DataTable dt, int iTarget)
        {
            InitializeComponent();
            
            this.Text = title + "자재에 대한 구매 문서";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("MATCODE", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Categories", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PO", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Order quantity", 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Remaining Order", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70); 
                spdData.RPT_AddBasicColumn("Unit", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Order date", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Delivery date", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            //spdData.DataSource = dt;

            //CmnSpdFunction.DataBindingWithSubTotal(spdData, dt, 0, 1, 3);

            int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 3);

            spdData.RPT_AutoFit(false);

            int frmWidth = 0;

            for(int i=0; i<spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }

            Width = frmWidth + 70;           
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
