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
    /// 클  래  스: MAT070501_M1<br/>
    /// 클래스요약: 원부자재 재공 현황 주차 입고계획 상세 POPUP<br/>
    /// 작  성  자: 하나마이크론 이희석<br/>
    /// 최초작성일: 2020-09-02<br/>
    /// 상세  설명: 원부자재 재공 현황 주차 입고계획 상세 조회 POPUP<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class MAT070501_M1 : Form
    {
        public MAT070501_M1()
        {
            InitializeComponent();
        }

        public MAT070501_M1(string Week,String[] Day, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " 상세 조회";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("MAT_ID", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(Week, 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(Day[0], 1, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn(Day[1], 1, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn(Day[2], 1, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn(Day[3], 1, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn(Day[4], 1, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn(Day[5], 1, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn(Day[6], 1, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_MerageHeaderColumnSpan(0, 1, 7);
                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData.DataSource = dt;
            //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 5);
            //spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

            spdData.RPT_AutoFit(false);

            //int frmWidth = 0;
            //for(int i=0; i<spdData.ActiveSheet.Columns.Count; i++)
            //{
            //    frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            //}
            //Width = frmWidth + 70;
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