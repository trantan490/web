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
    /// 클  래  스: PRD011010_P1<br/>
    /// 클래스요약: DA / WB 차수별 Capa대비 실적 화면 팝업 창<br/>
    /// 작  성  자: 에이스텍 황종환<br/>
    /// 최초작성일: 2013-08-22<br/>
    /// 상세  설명: DA / WB 차수별 Capa대비 실적 화면 팝업 창<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD011010_P1 : Form
    {
        private DataTable _dt;
        private string[] _groupList;
        Hana.PRD.PRD011010 _pfrm;

        public PRD011010_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 공정그룹 이름과 팝업에서 보여줄 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">공정그룹 명</param>
        /// <param name="dt">해당 공정그룹의 Arrange 현황</param>
        public PRD011010_P1(string title, DataTable dt, PRD011010 pfrm)
        {
            InitializeComponent();

            _pfrm = pfrm;
            _dt = dt;
            _groupList = title.Split(',');

            this.Text = "Arrange and Status of Equipment ("+title+")";

            spdData.RPT_ColumnInit();

            spdData.ActiveSheet.ColumnHeader.Rows.Count = 2;

            try
            {
                spdData.RPT_AddBasicColumn("Equipment model", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("대 수", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("UPEH", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("CAPA", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Equipment Status", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("RUN", 1, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WAIT", 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
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

            spdData.DataSource = _dt;

            spdData.RPT_AutoFit(false);

            int frmWidth = 0;
            for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }
            Width = frmWidth + 60;

            spdData.ActiveSheet.Rows[_dt.Rows.Count - 1].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
        }

        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Close();
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader == true) return;

            string strColumn = "";
            string strModelName = "";
            DataTable dtPop;

            if (e.Column == 4 || e.Column == 5 || e.Column == 6 || e.Column == 7)
            {
                strColumn = "SUM(" + _dt.Columns[e.Column].ToString() + ")";

                strModelName = spdData.ActiveSheet.Cells[e.Row, 0].Value.ToString();

                if (_groupList.Length == 8)
                {
                    //Hana.PRD.PRD011010 pfrm = new Hana.PRD.PRD011010();

                    dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", _pfrm.popupWindowResListSQL(_groupList[0].Trim(), _groupList[1].Trim(), _groupList[2].Trim(), _groupList[3].Trim(), _groupList[4].Trim(), _groupList[5].Trim(), _groupList[6].Trim(), strColumn, strModelName));

                    if (dtPop != null && dtPop.Rows.Count > 0)
                    {
                        LoadingPopUp.LoadingPopUpHidden();
                        System.Windows.Forms.Form frm = new PRD011010_P2(_groupList[0] + "," + _groupList[1] + "," + _groupList[2] + "," + _groupList[3] + "," + _groupList[4] + "," + _groupList[5] + "," + _groupList[6], dtPop);
                        frm.ShowDialog();
                    }
                }
            }

        }
    }
}