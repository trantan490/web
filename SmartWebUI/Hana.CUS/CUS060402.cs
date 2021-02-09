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
    public partial class CUS060402 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: CUS060402<br/>
        /// 클래스요약: 정기평가 Trend<br/>
        /// 작  성  자: 미라콤 김태순<br/>
        /// 최초작성일: 2008-10-09<br/>
        /// 상세  설명: 정기평가 트렌드를 조회한다.<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public CUS060402()
        {
            InitializeComponent();
            udcDurationDate1.AutoBinding();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리 
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("외주업체", "A.FACILITY", "FACILITY", "MIN(FAC_SEQ)", true);
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("외주업체", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
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
