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
    /// 클  래  스: PRD010423_P1<br/>
    /// 클래스요약: 주요 공정별 실적 모니터링 설비 상세 조회 POPUP<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2014-03-31<br/>
    /// 상세  설명: 주요 공정별 실적 모니터링 설비 상세 조회 POPUP<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD010423_P1 : Form
    {
        public PRD010423_P1()
        {
            InitializeComponent();
        }

        public PRD010423_P1(string title, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " 상세 조회";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("STATE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("MODEL", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);                
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("RES ID", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("CODE", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);              
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