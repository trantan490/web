using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public partial class Sample01 : Miracom.SmartWeb.UI.Controls.udcCUSReport001
    {
        public Sample01()
        {
            InitializeComponent();
        }

        private Boolean CheckField()
        {
            //Boolean Check = false;

            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1110");
            //    Check = false;
            //}
            //else { Check = true; }

            return true;
        }

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("      SELECT RS.OPER AS OPERATION, MO.OPER_DESC AS OPER_DESC, SUM(RS.TOT_LOT) AS TOT_LOT, " + "\n");
            strSqlString.Append("             SUM(RS.TOT_QTY_1) AS TOT_QTY_1, SUM(RS.TOT_QTY_2) AS TOT_QTY_2, " + "\n");
            strSqlString.Append("             SUM(RS.START_LOT + RS.END_LOT) AS PROCESS_LOT, " + "\n");
            strSqlString.Append("             SUM(RS.START_QTY_1 + RS.END_QTY_1) AS PROCESS_QTY_1, " + "\n");
            strSqlString.Append("             SUM(RS.START_QTY_2 + RS.END_QTY_2) AS PROCESS_QTY_2, " + "\n");
            strSqlString.Append("             SUM(RS.HOLD_LOT) AS HOLD_LOT, ");
            strSqlString.Append("             SUM(RS.HOLD_QTY_1) AS HOLD_QTY_1, SUM(RS.HOLD_QTY_2) AS HOLD_QTY_2, SUM(RS.RWK_LOT) AS RWK_LOT, " + "\n");
            strSqlString.Append("             SUM(RS.RWK_QTY_1) AS RWK_QTY_1, SUM(RS.RWK_QTY_2) AS RWK_QTY_2 " + "\n");
            strSqlString.Append("        FROM RSUMWIPSTS RS, MWIPOPRDEF MO ");
            strSqlString.AppendFormat(" WHERE RS.FACTORY=MO.FACTORY AND RS.OPER=MO.OPER AND RS.FACTORY = '{0}' " + "\n", CmnFunction.Trim(cdvFactory.txtValue));

            if (CmnFunction.Trim(cdvLotType.txtValue) != "")
            {
                strSqlString.AppendFormat("   AND RS.LOT_TYPE = '{0}' " + "\n", CmnFunction.Trim(cdvLotType.txtValue));
            }

            strSqlString.Append("    GROUP BY RS.OPER, MO.OPER_DESC ORDER BY RS.OPER " + "\n");

            return strSqlString.ToString();
        }

        private void MakeChart(DataTable DT)
        {
            ultraChart1.Visible = true;

            DataTable Table = new DataTable();
            Table = RptUChart.GetData(DT, 0, 3, 3);

            RptUChart.UChartOption.Legend.ShowLegend(ultraChart1, "Right");
            RptUChart.UChartOption.UChartTitle(ultraChart1, "Sample ");
            RptUChart.UChartOption.DataPointLabel(ultraChart1, true, "ColumnChart", "<DATA_VALUE:0>");
            RptUChart.ShowUChart(ultraChart1, Table, "ColumnChart");
            RptUChart.UChartOption.AxisItemFormat(ultraChart1, "", "<DATA_VALUE:0.0>");

            DT.Dispose();
        }

        private void Sample01_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
        }
        
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            CheckField();
            spdData_Sheet1.RowCount = 0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
            if (dt.Rows.Count == 0) { spdData.Visible = false; }
            else { spdData.Visible = true; }
            CmnSpdFunction.DataBindingWithSubTotal(spdData, dt, 0, 1, 2);
            ultraChart1.Visible = false;
            if (dt.Rows.Count > 0)
            {
                CmnFunction.FitColumnHeader(spdData);
                MakeChart(dt);
                ultraChart1.SaveTo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\myChart.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            dt.Dispose();
        }

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

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            udcCUSFromToCondition1.sFactory = cdvFactory.txtValue;
        }
    }
}

