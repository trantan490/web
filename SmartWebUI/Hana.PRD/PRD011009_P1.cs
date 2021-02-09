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
    /// 클  래  스: PRD010501_P1<br/>
    /// 클래스요약: 설비 Arrange 제품기준 팝업<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2008-12-10<br/>
    /// 상세  설명: 설비 Arrange 제품기준 팝업<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD011009_P1 : Form
    {

        public PRD011009_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 공정그룹 이름과 팝업에서 보여줄 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">공정그룹 명</param>
        /// <param name="dt">해당 공정그룹의 Arrange 현황</param>
        public PRD011009_P1(string title, DataTable dt)
        {
            InitializeComponent();

            this.Text = title + " Arrange Information";

            spdData.RPT_ColumnInit();

            spdData.ActiveSheet.ColumnHeader.Rows.Count = 2;
            
            try
            {
                spdData.RPT_AddBasicColumn("Equipment model", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("댓 수", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("UPEH", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("CAPA", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Equipment Status", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WAIT", 1, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("RUN", 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DOWN", 1, 6, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Replacement of product", 1, 7, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 4, 4);

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

            int frmWidth = 0;
            for(int i=0; i<spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }
            Width = frmWidth + 60;

            spdData.ActiveSheet.Rows[dt.Rows.Count -1].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
        }

        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Close();
        }
    }
}