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
    /// 클  래  스: MAT070504_P1<br/>
    /// 클래스요약: SMT 외주 관리 팝업 화면 1)<br/>
    /// 작  성  자: 에이스텍 황종환<br/>
    /// 최초작성일: 2013-09-03<br/>
    /// 상세  설명: 자재 코드 클릭 시 팝업이 실행된다.<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class MAT070504_P1 : Form
    {
        public MAT070504_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="strTotal"></param>
        public MAT070504_P1(string matCode, DataTable dt,string[] weekList)
        {
            InitializeComponent();

            this.Text = this.Text + "(자재코드 : " + matCode+")";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("PKG", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("LEAD", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("PKG CODE", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("WIP\n(DA~)", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("SOP PLAN", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("" + weekList[1] + "Weekly remaining volume", 1, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("" + weekList[1] + "주 계획", 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("" + weekList[2] + "주 계획", 1, 6, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);

                spdData.RPT_MerageHeaderColumnSpan(0, 4, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
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