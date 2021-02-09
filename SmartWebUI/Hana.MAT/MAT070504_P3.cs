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
    /// 클  래  스: MAT070504_P3<br/>
    /// 클래스요약: SMT 외주 관리 팝업 화면<br/>
    /// 작  성  자: 에이스텍 황종환<br/>
    /// 최초작성일: 2013-09-03<br/>
    /// 상세  설명: 팝업 화면 당주+차주 PCB부족 클릭 시 팝업<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class MAT070504_P3 : Form
    {
        public MAT070504_P3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="strTotal"></param>
        public MAT070504_P3(string title, DataTable dt, string strTotal)
        {
            InitializeComponent();

            this.Text = title;

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("Material Code", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Quantity by Expiration Date", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("TTL", 1, 1, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("More than 3 months", 1, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("2-3 months", 1, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("1 ~ 2 months", 1, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("0 to 1 month", 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("excess", 1, 6, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);

                spdData.RPT_MerageHeaderColumnSpan(0, 1, 6);
                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData.DataSource = dt;

            spdData.RPT_AutoFit(false);

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