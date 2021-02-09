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

namespace Hana.PRD
{
    /// <summary>
    /// 클  래  스: PRD011011_P1<br/>
    /// 클래스요약: RFID TREND 실패내역 팝업<br/>
    /// 작  성  자: 에이스텍 황종환<br/>
    /// 최초작성일: 2013-08-19<br/>
    /// 상세  설명: RFID TREND 실패내역 팝업 화면 <br/>
    /// 변경  내용: 2013-08-27-황종환 RFID -> PRD로 네임스페이스 변경<br/>
    /// </summary>
    public partial class PRD011011_P1 : Form
    {
        public PRD011011_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 공정그룹 이름과 팝업에서 보여줄 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">공정그룹 명</param>
        /// <param name="dt">해당 공정그룹의 Arrange 현황</param>
        public PRD011011_P1(string title, DataTable dt)
        {
            InitializeComponent();

            this.Text = title;

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("Part No", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Lot ID", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("QTY", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("Operation", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Equipment NO", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Fail History", 0, 5, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 150);

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