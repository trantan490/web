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
    public partial class CUS060401 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// Ŭ  ��  ��: CUS060401<br/>
        /// Ŭ�������: ������ Main<br/>
        /// ��  ��  ��: �̶��� ���¼�<br/>
        /// �����ۼ���: 2008-10-09<br/>
        /// ��  ����: ������ ����� ��ȸ�Ѵ�.<br/>
        /// ����  ����: <br/>
        /// </summary>
        public CUS060401()
        {
            InitializeComponent();
            udcDurationDate1.AutoBinding();

            SortInit();
            GridColumnInit(); //��� ����¥�� 
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "A.FACILITY", "FACILITY", "MIN(FAC_SEQ)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "A.FAB", "FAB", "MIN(FAB_SEQ)", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "B.FAMILY", "FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "B.TECHNOLOGY", "TECHNOLOGY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "B.DENSITY", "DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.PROD_MODE", "PROD_MODE", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "B.GENERATION", "GENERATION", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "B.ORGANIZATION", "ORGANIZATION", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.PRODUCT", "PRODUCT", true);
        }

        #endregion

        #region �����������

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("��ŷ", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("1��", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("2��", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("3��", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("4��", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("5��", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("6��", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group�׸��� ������� �ݵ�� �������ٰ�.
        }

        #endregion

        #region ��ȸ

        /// <summary>
        /// ��ȸ
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
            if (ultraChart1.Visible == false && spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1202");
                return;
            }

            Control[] obj = null;
            string sImageFileName = null;
            obj = new Control[1];
            obj[0] = spdData;

            sImageFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\myChart.png";
            CmnExcelFunction.ExportToExcelEx(obj, sImageFileName, 1, "Production Output", "", true, false, false, -1, -1);
        }

        #endregion

    }
}

