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
    /// 클  래  스: PRD011010_P2<br/>
    /// 클래스요약: 설비번호 목록 화면 팝업 창<br/>
    /// 작  성  자: 에이스텍 김태호<br/>
    /// 최초작성일: 2013-10-23<br/>
    /// 상세  설명: 설비번호 목록 화면 팝업 창<br/>
    /// 변경  내용: <br/>
    /// 2013-12-26-임종우 : 설비 상태 관련 데이터 추가 (김권수 요청)
    /// 2014-07-30-임종우 : DOWN TIME 추가 (백성호 요청)
    /// </summary>
    public partial class PRD011010_P2 : Form
    {

        public PRD011010_P2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 공정그룹 이름과 팝업에서 보여줄 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">공정그룹 명</param>
        /// <param name="dt">해당 공정그룹의 Arrange 현황</param>
        public PRD011010_P2(string title, DataTable dt)
        {
            InitializeComponent();

            this.Text = "Arrange and Status of Equipment List ("+title+")";

            spdData.RPT_ColumnInit();

            try
            {
                spdData.RPT_AddBasicColumn("Product Group", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("Equipment number", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("STATE", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("CODE", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DOWN TIME", 0, 5, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 100);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData.DataSource = dt;

            spdData.RPT_AutoFit(false);

            int frmWidth = 0;

            for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }

            Width = frmWidth + 70;
        }

        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Close();
        }
    }
}