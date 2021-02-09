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
using FarPoint.Win.Spread;

namespace Hana.PQC
{
    /// <summary>
    /// 클  래  스: PQC030706_P1<br/>
    /// 클래스요약: 월별 재발 현황 상세 조회 POPUP<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2010-12-06<br/>
    /// 상세  설명: 월별 재발 현황 상세 조회 POPUP<br/>
    /// 변경  내용: <br/>    
    /// </summary>
    public partial class PQC030706_P1 : Form
    {
        public PQC030706_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 업체명 과 팝업에서 보여줄 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        public PQC030706_P1(DataTable dt)
        {
            InitializeComponent();
            
            this.Text = " 상세 조회";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("Vendor description", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Material classification", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Product", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Defect name", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Recurrence", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
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

        }
    }
}