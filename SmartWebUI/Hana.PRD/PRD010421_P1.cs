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
    /// 클  래  스: PRD010421_P1<br/>
    /// 클래스요약: 긴급 제품 관리 팝업 화면<br/>
    /// 작  성  자: 에이스텍 황종환<br/>
    /// 최초작성일: 2013-08-01<br/>
    /// 상세  설명: 긴급 제품 팝업 화면 (해당 프로덕트의 잔량 및 cutoff day 조회)<br/>
    /// 변경  내용: 2013-08-39-황종환 : 잔량관련 로직 변경(임태성 과장 요청)<br/>
    ///                                    -. 현재 : set-up 한 날짜 기준 이후의 발생한 해당 공정의 실적값
    ///                                         -> 문제점 : 위 Logic 은 투입할때부터 관리하는 제품인 경우만 적용이 되는 Logic임
    ///                                                     중간에 긴급진행 요청이 들어올 경우는 위 Logic 이 맞지 않음
    ///                                    -.변경 : Finish : 목표값 – start 시점이후부터의 누계 AO 실적
    ///                                             Mold   : 목표값 – start 시점이후부터의 누계 AO 실적 – HMK3A –Finish 재공
    ///                                             WB     : 목표값 – start 시점이후부터의 누계 AO 실적 – HMK3A –Finish 재공 –Mold 재공
    ///                                             DA     : 목표값 – start 시점이후부터의 누계 AO 실적 – HMK3A –Finish 재공 –WB Merge Part 재공 
    ///                                             SAW    : 목표값 – start 시점이후부터의 누계 AO 실적 – HMK3A –Finish 재공 –WB 재공-da 재공
    ///             2013-09-13-황종환 : Form의 Width를 스프레드 넓이에 따라 가변적으로 변화게 구성
    /// </summary>
    public partial class PRD010421_P1 : Form
    {
        public PRD010421_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 공정그룹 이름과 팝업에서 보여줄 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">공정그룹 명</param>
        /// <param name="dt">해당 공정그룹의 Arrange 현황</param>
        public PRD010421_P1(string title, DataTable dt, string idName, string wipHMK3A, string wipFINISH, string wipMOLD, string wipWB, string wipDA, string wipSAW, string wipSTOCK,string wipWBLAST)
        {
            InitializeComponent();

            this.Text = title;

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn("Vendor description", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn(idName, 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("Classification", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("residual quantity", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("Operation WIP", 0, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("Cumulative WIP", 0, 6, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("Cutoff Day", 0, 7, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 110);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData.DataSource = dt;
            
            int finishQTY = spdData.ActiveSheet.GetText(4, 4).Replace(",", "").Trim() == "" ? 0 : Convert.ToInt32(spdData.ActiveSheet.GetText(4, 4).Replace(",", "").Trim());
            for (int row = 0; row < spdData.ActiveSheet.RowCount; row++)
            {
                if (spdData.ActiveSheet.GetText(row, 3).Equals("SAW"))
                {
                    spdData.ActiveSheet.SetText(row, 4, (finishQTY - Convert.ToInt32(wipHMK3A) - Convert.ToInt32(wipFINISH) - Convert.ToInt32(wipMOLD) - Convert.ToInt32(wipWB) - Convert.ToInt32(wipDA)).ToString());
                    spdData.ActiveSheet.SetText(row, 5, Convert.ToInt32(wipSAW).ToString());
                    spdData.ActiveSheet.SetText(row, 6, ((Int32)(Convert.ToInt32(wipSAW) + Convert.ToInt32(wipSTOCK))).ToString() );
                }
                else if (spdData.ActiveSheet.GetText(row, 3).Equals("DA"))
                {
                    spdData.ActiveSheet.SetText(row, 4, (finishQTY - Convert.ToInt32(wipHMK3A) - Convert.ToInt32(wipFINISH) - Convert.ToInt32(wipMOLD) - Convert.ToInt32(wipWBLAST)).ToString());
                    spdData.ActiveSheet.SetText(row, 5, Convert.ToInt32(wipDA).ToString());
                    spdData.ActiveSheet.SetText(row, 6, ((Int32)(Convert.ToInt32(wipSAW) + Convert.ToInt32(wipSTOCK) + Convert.ToInt32(wipDA) )).ToString());
                }
                else if (spdData.ActiveSheet.GetText(row, 3).Equals("WB"))
                {
                    spdData.ActiveSheet.SetText(row, 4, (finishQTY - Convert.ToInt32(wipHMK3A) - Convert.ToInt32(wipFINISH) - Convert.ToInt32(wipMOLD)).ToString());
                    spdData.ActiveSheet.SetText(row, 5, Convert.ToInt32(wipWB).ToString());
                    spdData.ActiveSheet.SetText(row, 6, ((Int32)(Convert.ToInt32(wipSAW) + Convert.ToInt32(wipSTOCK) + Convert.ToInt32(wipDA) + Convert.ToInt32(wipWB))).ToString());
                }
                else if (spdData.ActiveSheet.GetText(row, 3).Equals("MOLD"))
                {
                    spdData.ActiveSheet.SetText(row, 4, (finishQTY - Convert.ToInt32(wipHMK3A) - Convert.ToInt32(wipFINISH)).ToString());
                    spdData.ActiveSheet.SetText(row, 5, Convert.ToInt32(wipMOLD).ToString());
                    spdData.ActiveSheet.SetText(row, 6, ((Int32)(Convert.ToInt32(wipSAW) + Convert.ToInt32(wipSTOCK) + Convert.ToInt32(wipDA) + Convert.ToInt32(wipWB) + Convert.ToInt32(wipMOLD))).ToString());
                }
                else if (spdData.ActiveSheet.GetText(row, 3).Equals("FINISH"))
                {
                    spdData.ActiveSheet.SetText(row, 5, Convert.ToInt32(wipFINISH).ToString());
                    spdData.ActiveSheet.SetText(row, 6, ((Int32)(Convert.ToInt32(wipSAW) + Convert.ToInt32(wipSTOCK) + Convert.ToInt32(wipDA) + Convert.ToInt32(wipWB) + Convert.ToInt32(wipMOLD) + Convert.ToInt32(wipFINISH))).ToString());
                }
            }

            spdData.RPT_AutoFit(false);
            int frmWidth = 0;
            for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }
            Width = frmWidth + 70;
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