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
    /// 클  래  스: PQC030701_P1<br/>
    /// 클래스요약: 업체별 상세 조회 POPUP<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2010-04-01<br/>
    /// 상세  설명: 업체별 상세 조회 POPUP<br/>
    /// 변경  내용: <br/>
    /// 2010-06-17-임종우 : 상세 팝업창에 제품 규격 표시 (김민직 요청)
    /// </summary>
    public partial class PQC030701_P1 : Form
    {
        public PQC030701_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 업체명 과 팝업에서 보여줄 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        public PQC030701_P1(string title, DataTable dt)
        {
            InitializeComponent();
            
            this.Text = title + " 상세 조회";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("Vendor description", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Material classification", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Product", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Standard", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("IQC_NO", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                spdData.RPT_AddBasicColumn("investigation subject", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("LOT", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("UNIT", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 5, 2);

                spdData.RPT_AddBasicColumn("Pass rate (%)", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOT", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                spdData.RPT_AddBasicColumn("UNIT", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 7, 2);

                spdData.RPT_AddBasicColumn("Inspection quantity", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOT", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("UNIT", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 9, 2);

                spdData.RPT_AddBasicColumn("Defect quantity", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOT", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("UNIT", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 11, 2);

                spdData.RPT_AddBasicColumn("Defective rate(%)", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOT", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                spdData.RPT_AddBasicColumn("UNIT", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 13, 2);

                spdData.RPT_AddBasicColumn("yield", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("inspector", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Reception date", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Inspection date", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);                
                spdData.RPT_MerageHeaderRowSpan(0, 15, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 16, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 17, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 18, 2);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            //spdData.DataSource = dt;
            int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 5);
            spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

            spdData.RPT_AutoFit(false);

            // GrandTotal에 백분율 부분 계산
            double sample_lot = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 9].Value); // 샘플랏수
            double sample_qty = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 10].Value); // 샘플수량

            // 샘플수량이 0이면 나누기 계산을 위해 1로 변환.
            if (sample_lot == 0)
            {
                sample_lot = 1;
            }

            if (sample_qty == 0)
            {
                sample_qty = 1;
            }

            spdData.ActiveSheet.Cells[0, 13].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 11].Value) / sample_lot) * 100; // 불량율(LOT)
            spdData.ActiveSheet.Cells[0, 14].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 12].Value) / sample_qty) * 100; // 불량율(UNIT)
            spdData.ActiveSheet.Cells[0, 7].Value = 100 - Convert.ToDouble(spdData.ActiveSheet.Cells[0, 13].Value); // 합격율(LOT)
            spdData.ActiveSheet.Cells[0, 8].Value = 100 - Convert.ToDouble(spdData.ActiveSheet.Cells[0, 14].Value); // 합격율(UNIT)

            int frmWidth = 0;
            for(int i=0; i<spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }
            Width = frmWidth + 60;

        }
    }
}