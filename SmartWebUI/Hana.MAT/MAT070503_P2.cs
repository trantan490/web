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
    /// 클  래  스: MAT070503_P2<br/>
    /// 클래스요약: 계획 대비 입고 현황 팝업 화면<br/>
    /// 작  성  자: 에이스텍 김태호<br/>
    /// 최초작성일: 2013-08-23<br/>
    /// 상세  설명: 계획 대비 입고 현황 팝업 화면<br/>g
    /// 변경  내용: <br/>
    /// </summary>
    public partial class MAT070503_P2 : Form
    {
        public MAT070503_P2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="strTotal"></param>
        public MAT070503_P2(string title, DataTable dt, string selectDate)
        {
            InitializeComponent();

            this.Text = title;

            spdData.RPT_ColumnInit();

            DateTime dtp = DateTime.Parse(selectDate);
            try
            {
                spdData.RPT_AddBasicColumn("Material Code", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Classification", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Warehousing status compared to Plan", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("TTL", 1, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);


                spdData.RPT_AddBasicColumn(string.Format("{0}", dtp.ToString("MM.dd")), 1, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn(string.Format("{0}", dtp.AddDays(-1).ToString("MM.dd")), 1, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn(string.Format("{0}", dtp.AddDays(-2).ToString("MM.dd")), 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn(string.Format("{0}", dtp.AddDays(-3).ToString("MM.dd")), 1, 6, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn(string.Format("{0}", dtp.AddDays(-4).ToString("MM.dd")), 1, 7, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn(string.Format("{0}", dtp.AddDays(-5).ToString("MM.dd")), 1, 8, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn(string.Format("{0}", dtp.AddDays(-6).ToString("MM.dd")), 1, 9, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 90);
                
                spdData.RPT_MerageHeaderColumnSpan(0, 2, 8);

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

            SetTotal();
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

        private void SetTotal()
        {
            int iValue = 0;

            for (int i = spdData.ActiveSheet.Columns.Count-1; i >3 ; i--)
            {
                iValue = Convert.ToInt32(spdData.ActiveSheet.GetValue(2, i).Equals("") ? "0" : spdData.ActiveSheet.GetValue(2, i))
                    + Convert.ToInt32(spdData.ActiveSheet.GetValue(2, i-1).Equals("") ? "0" : spdData.ActiveSheet.GetValue(2, i-1));

                spdData.ActiveSheet.SetValue(2, i-1, iValue);
            }
            spdData.ActiveSheet.SetValue(2, 2, iValue);
        }
    }
}