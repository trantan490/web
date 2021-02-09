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
    /// 클  래  스: MAT070503_P3<br/>
    /// 클래스요약: 입고 계획 팝업 화면<br/>
    /// 작  성  자: 에이스텍 김태호<br/>
    /// 최초작성일: 2013-09-03<br/>
    /// 상세  설명: 입고 계획 팝업 화면<br/>g
    /// 변경  내용: <br/>
    /// </summary>
    public partial class MAT070503_P3 : Form
    {
        public MAT070503_P3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="strTotal"></param>
        public MAT070503_P3(string title, DataTable dt, string strMatCode, DateTime dtPlanDate)
        {
            InitializeComponent();

            this.Text = title;

            spdData.RPT_ColumnInit();

            DateTime dtp = DateTime.Parse(dtPlanDate.ToString("yyyy-MM") + "-01");

            int iPlanDate = Convert.ToInt32(dtPlanDate.ToString("dd"));
            try
            {
                spdData.RPT_AddBasicColumn("Material Code", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Vendor description", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Classification", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 90);

                int dayCnt = 0;
                for (int i = 0; i < dt.Columns.Count-3; i++)
                {
                    if (i+1 < iPlanDate)
                    {
                        spdData.RPT_AddBasicColumn(string.Format("{0}", dtp.AddDays(i).ToString("MM.dd")), 0, 3 + i, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                    }
                    else
                    {
                        if (dayCnt < 31)
                        {
                            spdData.RPT_AddBasicColumn(string.Format("{0}", dtp.AddDays(i).ToString("MM.dd")), 0, 3 + i, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                            dayCnt++;
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn(string.Format("{0}", dtp.AddDays(i).ToString("MM.dd")), 0, 3 + i, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            spdData.DataSource = dt;

            spdData.RPT_AutoFit(false);

            dt.Dispose();

            setTotalColumn(strMatCode, dt.Rows.Count);

        }

        private void setTotalColumn(string strMatCode, int rowSpan)
        {
            

            spdData.ActiveSheet.Cells[0, 0, 2, 0].Text = strMatCode;
            spdData.ActiveSheet.Cells[0, 1, 2, 1].Text = "TOTAL";

            spdData.ActiveSheet.Cells[0, 0].RowSpan = rowSpan;

            for (int i = 0; i < spdData.ActiveSheet.Rows.Count / 3; i++)
            {
                spdData.ActiveSheet.Cells[i*3, 1].RowSpan = 3;
            }

            spdData.ActiveSheet.Rows[0].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            spdData.ActiveSheet.Rows[1].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            spdData.ActiveSheet.Rows[2].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));

            for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                spdData.ActiveSheet.Cells[i, 0].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
            }
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