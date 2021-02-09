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
    /// 클  래  스: PRD010213_P1<br/>
    /// 클래스요약: COB 관리 상세 조회 POPUP<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2013-09-06<br/>
    /// 상세  설명: COB 관리 상세 조회 POPUP<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD010213_P1 : Form
    {
        public PRD010213_P1()
        {
            InitializeComponent();
        }

        public PRD010213_P1(string title, DataTable dt, int iTarget)
        {
            InitializeComponent();
            
            //this.Text = title + " 상세 조회";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("PART NO", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("STEP", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);                                
                spdData.RPT_AddBasicColumn("LOT NO", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("QTY", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                
                spdData.RPT_AddBasicColumn("Cumulative congestion time", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);                
                spdData.RPT_AddBasicColumn("Cumulative QTY", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
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

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int value = Convert.ToInt32(spdData.ActiveSheet.Cells[i, 5].Value);                

                if (iTarget > value)
                {
                    spdData.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    spdData.ActiveSheet.Rows[i].BackColor = Color.Pink;
                }
                else
                {
                    spdData.ActiveSheet.Rows[i].ForeColor = Color.Blue;
                    spdData.ActiveSheet.Rows[i].BackColor = Color.LightGreen;
                    break;
                }                
            }
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