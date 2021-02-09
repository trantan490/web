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

namespace Hana.MAT
{
    /// <summary>
    /// 클  래  스: MAT070504_P2<br/>
    /// 클래스요약: SMT 외주 관리 팝업 화면<br/>
    /// 작  성  자: 에이스텍 황종환<br/>
    /// 최초작성일: 2013-09-03<br/>
    /// 상세  설명: 외주 재고 클릭 시 팝업 화면<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class MAT070504_P2 : Form
    {
        public MAT070504_P2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="strTotal"></param>
        public MAT070504_P2(string matCode, DataTable dt)
        {
            InitializeComponent();

            this.Text = this.Text + "(자재코드 : " + matCode + ")";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("Outsourcing stock", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("TTL", 1, 0, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("STOCK", 1, 1, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("WIP", 1, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("SHIP", 1, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);

                spdData.RPT_MerageHeaderColumnSpan(0, 0, 4);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData.DataSource = dt;

            spdData.RPT_AutoFit(false);

            spdData.ActiveSheet.Rows[dt.Rows.Count - 1].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));

            dt.Dispose();
        }

        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Close();
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