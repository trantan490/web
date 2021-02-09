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
    /// 클  래  스: MAT070503_P4<br/>
    /// 클래스요약: SAP 코드 목록 팝업 화면<br/>
    /// 작  성  자: 에이스텍 김태호<br/>
    /// 최초작성일: 2013-09-07<br/>
    /// 상세  설명: SAP 코드 목록 팝업 화면<br/>g
    /// 변경  내용: <br/>
    /// </summary>
    public partial class MAT070503_P4 : Form
    {
        public MAT070503_P4()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="strTotal"></param>
        public MAT070503_P4(string title, DataTable dt, string strMatCode)
        {
            InitializeComponent();

            this.Text = title;

            spdData.RPT_ColumnInit();

            try
            {
                spdData.RPT_AddBasicColumn("Material Code", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("SAP CODE", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
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