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
    /// 클  래  스: MAT070503_P1<br/>
    /// 클래스요약: 원부자재투입점검 팝업 화면<br/>
    /// 작  성  자: 에이스텍 김태호<br/>
    /// 최초작성일: 2013-08-21<br/>
    /// 상세  설명: 유효기간별 수량 팝업 화면 (원부자재 기준 유효기간별 수량 조회)<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class MAT070503_P1 : Form
    {
        public MAT070503_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="strTotal"></param>
        public MAT070503_P1(string title, DataTable dt, string strTotal)
        {
            InitializeComponent();

            this.Text = title;

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("Material Code", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Vendor", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Quantity by Expiration Date", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("TTL", 1, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("More than 3 months", 1, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("2-3 months", 1, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("1 ~ 2 months", 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("0 to 1 month", 1, 6, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("excess", 1, 7, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);

                spdData.RPT_MerageHeaderColumnSpan(0, 2, 6);
                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
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