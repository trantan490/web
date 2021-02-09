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

namespace Hana.MAT
{
    /// <summary>
    /// 클  래  스: MAT070101_P1<br/>
    /// 클래스요약: 자재별 상세 조회 POPUP<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2009-01-22<br/>
    /// 상세  설명: 자재별 상세 조회 POPUP.<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class MAT070101_P1 : Form
    {
        public MAT070101_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 공정그룹 이름과 팝업에서 보여줄 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">공정그룹 명</param>
        /// <param name="dt">해당 공정그룹의 Arrange 현황</param>
        public MAT070101_P1(string title, string mat_type, DataTable dt)
        {
            InitializeComponent();
            
            //this.Text = title + " Information";

            spdData.RPT_ColumnInit();
            try
            {
                spdData.RPT_AddBasicColumn(title + " 현황", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Item Code", 1, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                //spdData.RPT_AddBasicColumn("Family", 1, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                //spdData.RPT_AddBasicColumn("Package", 1, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                //spdData.RPT_AddBasicColumn("Type1", 1, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("Type2", 1, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("LD Count", 1, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("Density", 1, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("Generation", 1, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Item specification", 1, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 170);

                if (mat_type.Equals("GW"))
                {
                    if (title.Equals("설비") || title.Equals("TOTAL"))
                    {
                        spdData.RPT_AddBasicColumn("Equipment name", 1, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                        spdData.RPT_AddBasicColumn("Equipment condition", 1, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("Equipment name", 1, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                        spdData.RPT_AddBasicColumn("Equipment condition", 1, 3, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                    }
                    spdData.RPT_AddBasicColumn("Material number", 1, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("quantity", 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Unit", 1, 6, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 50);

                    spdData.RPT_AddBasicColumn("Re-stroage", 1, 7, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Aging start time", 1, 8, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Aging end time", 1, 9, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Time limit", 1, 10, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Equipment name", 1, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Equipment condition", 1, 3, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);

                    spdData.RPT_AddBasicColumn("Material number", 1, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("quantity", 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Unit", 1, 6, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 50);

                    spdData.RPT_AddBasicColumn("Re-stroage", 1, 7, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Aging start time", 1, 8, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Aging end time", 1, 9, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("Time limit", 1, 10, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                }

                //spdData.RPT_AddBasicColumn("Material number", 1, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                //spdData.RPT_AddBasicColumn("수량", 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 50);
                //spdData.RPT_AddBasicColumn("단위", 1, 6, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 50);

                spdData.RPT_MerageHeaderColumnSpan(0, 0, 11);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }

            //spdData.DataSource = dt;
            int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 5);
            spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

            spdData.RPT_AutoFit(false);

            //int rowCount = spdData.ActiveSheet.Rows.Count;
            //List<string> ltime = new List<string>();

            //for (int i = 1; rowCount > i; i++)
            //{                
                //if (spdData.ActiveSheet.Cells[i, 10].Value == null)
                //    ltime.Add("0");
                //else
                //    ltime.Add(spdData.ActiveSheet.Cells[i, 10].Value.ToString());
                
                //spdData.ActiveSheet.Rows[i].BackColor = Color.FromName("ffdfd9");

                //Cells CurCell = spdData.ActiveSheet.Cells[i, 10];
                //CurCell.ForeColor = Color.Red;
            //}

            int frmWidth = 0;
            for(int i=0; i<spdData.ActiveSheet.Columns.Count; i++)
            {
                frmWidth += spdData.ActiveSheet.GetColumnWidth(i);
            }
            Width = frmWidth + 60;
        }
    }
}