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
    /// 클  래  스: PRD011016_P1<br/>
    /// 클래스요약: CAPA SIMULATION 기준정보 미등록 팝업<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2013-11-14<br/>
    /// 상세  설명: CAPA SIMULATION 기준정보 미등록 팝업<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD011016_P1 : Form
    {
        public PRD011016_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lot ID 와 팝업에서 보여줄 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">LotId</param>
        /// <param name="dt">Lot별 Loss 상세 조회</param>
        public PRD011016_P1(DataTable dt)
        {
            InitializeComponent();

            this.Text = "List of Standard information Unregistered product";

            spdData.RPT_ColumnInit();
            try
            {                
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 70);                
            }
            catch(Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData.DataSource = dt;
            //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 3);

            //spdData.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 1, true, Align.Center, VerticalAlign.Center);
            spdData.RPT_AutoFit(false);

            int frmWidth = 0;
            // 컨트롤 너비 지정(컬럼 너비 + 60[여백])
            for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }
            Width = frmWidth + 60;
        }

        private void spdData_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}