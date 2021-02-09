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
    /// 클  래  스: PRD010501_P1<br/>
    /// 클래스요약: 설비 Arrange 제품기준 팝업<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2008-12-10<br/>
    /// 상세  설명: 설비 Arrange 제품기준 팝업<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD010501_P1 : Form
    {
        public PRD010501_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 공정그룹 이름과 팝업에서 보여줄 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">공정그룹 명</param>
        /// <param name="dt">해당 공정그룹의 Arrange 현황</param>
        public PRD010501_P1(string title, DataTable dt)
        {
            InitializeComponent();

            this.Text = title + " Arrange Information";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn(title + " Arrange 현황", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Customer", 1, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                //spdData.RPT_AddBasicColumn("Family", 1, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                //spdData.RPT_AddBasicColumn("Package", 1, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                //spdData.RPT_AddBasicColumn("Type1", 1, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("Type2", 1, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("LD Count", 1, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("Density", 1, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("Generation", 1, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 1, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 170);
                spdData.RPT_AddBasicColumn("Equipment Model", 1, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("count", 1, 3, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("CAPA", 1, 4, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 50);

                spdData.RPT_MerageHeaderColumnSpan(0, 0, 5);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            //spdData.DataSource = dt;
            int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 2, 3);
            spdData.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 1, true, Align.Center, VerticalAlign.Center);

            spdData.RPT_AutoFit(false);

            int frmWidth = 0;
            for(int i=0; i<spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }
            Width = frmWidth + 60;
        }

        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Close();
        }
    }
}