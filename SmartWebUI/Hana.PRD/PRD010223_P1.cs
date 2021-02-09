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
    /// 클  래  스: PRD010223_P1<br/>
    /// 클래스요약: 투입단 관리 상세 조회 POPUP<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2014-07-29<br/>
    /// 상세  설명: 투입단 관리 상세 조회 POPUP<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD010223_P1 : Form
    {
        public PRD010223_P1()
        {
            InitializeComponent();
        }

        public PRD010223_P1(string title, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " 상세 조회";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("MERGE PART", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("MAT_ID", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("차수", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("DA WIP", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("순수", 1, 3, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Merge", 1, 4, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TTL", 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 3, 3);

                spdData.RPT_AddBasicColumn("WIP Status by degree", 0, 6, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("KIT shortage status", 0, 7, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("WIP", 0, 8, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TTL", 1, 8, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("UV", 1, 9, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Gate", 1, 10, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DDS", 1, 11, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SAW", 1, 12, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("BG", 1, 13, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Steath", 1, 14, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Pre GR", 1, 15, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Lami", 1, 16, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Stock", 1, 17, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 8, 10);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            //spdData.DataSource = dt;
            int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 3);
            spdData.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 1, true, Align.Center, VerticalAlign.Center);

            spdData.RPT_AutoFit(false);

            int frmWidth = 0;

            for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
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