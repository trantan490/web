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
    /// 클  래  스: PRD010603_P1<br/>
    /// 클래스요약: Ship Lot 조회 Loss 상세 팝업<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 20089-07-01<br/>
    /// 상세  설명: Ship Lot 조회 Loss 상세 팝업<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD010603_P1 : Form
    {
        public PRD010603_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lot ID 와 팝업에서 보여줄 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">LotId</param>
        /// <param name="dt">Lot별 Loss 상세 조회</param>
        public PRD010603_P1(string title, DataTable dt)
        {
            InitializeComponent();


            this.Text = title + " " + "Loss Detail Search";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn(title + " Loss 상세 조회", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
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
            // 컨트롤 너비 지정(컬럼 너비 + 60[여백])
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