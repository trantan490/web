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
using System.Text.RegularExpressions;

namespace Hana.PRD
{
    /// <summary>
    /// 클  래  스: PRD010221_P1<br/>
    /// 클래스요약: 팝업 화면<br/>
    /// 작  성  자: 에이스텍 김태호<br/>
    /// 최초작성일: 2013-10-07<br/>
    /// 상세  설명: 팝업 화면<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PRD010221_P1 : Form
    {
        public PRD010221_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="strTotal"></param>
        public PRD010221_P1(string title, DataTable dtLotInfo, DataTable dtOperInfo)
        {
            InitializeComponent();

            this.Text = title;

            //spdDataLotInfo.RPT_ColumnInit();
            spdDataOperInfo.RPT_ColumnInit();

            try
            {
                //spdDataLotInfo.RPT_AddBasicColumn("Classification", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                //spdDataLotInfo.RPT_AddBasicColumn("PART", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 180);
                //spdDataLotInfo.RPT_AddBasicColumn("QTY", 0, 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                //spdDataLotInfo.RPT_AddBasicColumn("the place of shipment", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                //spdDataLotInfo.RPT_AddBasicColumn("General Manager (Eng'r )", 0, 4, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                //spdDataLotInfo.RPT_AddBasicColumn("General Manager (production operation )", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                //spdDataLotInfo.RPT_AddBasicColumn("General Manager (Sales)", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                //spdDataLotInfo.RPT_AddBasicColumn("Special Notice", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 180);

                spdDataOperInfo.RPT_AddBasicColumn("코드", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdDataOperInfo.RPT_AddBasicColumn("Operation", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 180);
                spdDataOperInfo.RPT_AddBasicColumn("WIP", 0, 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdDataOperInfo.RPT_AddBasicColumn("Recipe existence and nonexistence", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                spdDataOperInfo.RPT_AddBasicColumn("Reserve hold status", 0, 4, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                spdDataOperInfo.RPT_AddBasicColumn("Operation special note", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 180);
                spdDataOperInfo.RPT_AddBasicColumn("goal", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                spdDataOperInfo.RPT_AddBasicColumn("Completion", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            if (dtLotInfo != null && dtLotInfo.Rows.Count > 0)
            {
                txtLot.Text = dtLotInfo.Rows[0][0].ToString();
                txtPartNo.Text = dtLotInfo.Rows[0][1].ToString();
                txtQty.Text = UsrNumericCheck(dtLotInfo.Rows[0][2].ToString());
                txtPlace.Text = dtLotInfo.Rows[0][3].ToString();
                txtEngineer.Text = dtLotInfo.Rows[0][4].ToString();
                txtProduct.Text = dtLotInfo.Rows[0][5].ToString();
                txtBusiness.Text = dtLotInfo.Rows[0][6].ToString();
                txtNotice.Text = dtLotInfo.Rows[0][7].ToString();
            }

            //spdDataLotInfo.DataSource = dtLotInfo;
            spdDataOperInfo.DataSource = dtOperInfo;

            //spdDataLotInfo.RPT_AutoFit(false);
            //spdDataOperInfo.RPT_AutoFit(false);


            //spdDataLotInfo.Dispose();
            //spdDataOperInfo.Dispose();
        }

        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //spdDataLotInfo.ExportExcel();
            spdDataOperInfo.ExportExcel();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string UsrNumericCheck(string strInputData)
        {
            Regex r = new Regex("(?<=[0-9])(?=([0-9][0-9][0-9])+(?![0-9]))");
            string result = r.Replace(strInputData, ",");
            return result;
        }

    }
}