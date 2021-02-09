using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.UI.Controls;


namespace Hana.CUS
{
    public partial class CUS060301 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: CUS060301<br/>
        /// 클래스요약: Matrial View<br/>
        /// 작  성  자: 미라콤 김태순<br/>
        /// 최초작성일: 2008-10-08<br/>
        /// 상세  설명: Matrial 데이터를 조회한다.<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public CUS060301()
        {
            InitializeComponent();

            SortInit();
            GridColumnInit1(); //헤더 두줄짜리 
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("고객사", "A.FACILITY", "FACILITY", "MIN(FAC_SEQ)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("월계획", "A.FAB", "FAB", "MIN(FAB_SEQ)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("실적", "B.FAMILY", "FAMILY", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("자재코드", "B.TECHNOLOGY", "TECHNOLOGY", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Vender", "B.DENSITY", "DENSITY", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG1", "B.PROD_MODE", "PROD_MODE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG2", "B.GENERATION", "GENERATION", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "B.ORGANIZATION", "ORGANIZATION", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TECH", "A.PRODUCT", "PRODUCT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DEVICE", "A.PRODUCT", "PRODUCT", true);
        }

        #endregion

        #region 두줄헤더생성

        /// <summary>
        /// 두줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit1()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("고객사", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("월계획", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("실적", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("자재코드", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Vender", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("PKG1", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("PKG2", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("TECH", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("DEVICE", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("보유현황", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("창고", 1, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Line", 1, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("발주량", 1, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("재고합계", 1, 13, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("입고일정", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("10/1", 1, 14, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("10/2", 1, 15, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("10/3", 1, 16, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("10/4", 1, 17, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("10/5", 1, 18, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("10/6", 1, 19, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("10/7", 1, 20, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("10/8", 1, 21, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 70);

            //spdData.RPT_AddDynamicColumn(udcDurationDate1, new string[] { "입고일정" },
            //    1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, new Formatter[] { Formatter.Number, Formatter.Number, Formatter.Double2, Formatter.Number, Formatter.Number }, 60);

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
            spdData.RPT_MerageHeaderColumnSpan(0, 10, 3);
            spdData.RPT_MerageHeaderColumnSpan(0, 14, 7);
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            CheckField();
        }

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            return true;
        }

        #endregion

        #endregion

        #region ToExcel

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1202");
                return;
            }

            Control[] obj = null;
            string sImageFileName = null;
            obj = new Control[1];
            obj[0] = spdData;

            CmnExcelFunction.ExportToExcelEx(obj, sImageFileName, 1, "Production Output", "", true, false, false, -1, -1);
        }

        #endregion
    }
}
