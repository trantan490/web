using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Miracom.SmartWeb.UI
{
    public partial class STD1902 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1902()
        {
            InitializeComponent();
        }

        private void STD1902_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string QueryCond = null;
                        
            CheckField();
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtLotID.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, (chkDeleteHis.Checked == true ? "1" : "0"));

            spdData_Sheet1.RowCount = 0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1902", QueryCond);
            if (dt.Rows.Count == 0) { spdData.Visible = false; }
            else { spdData.Visible = true; }
            spdData_Sheet1.DataSource = dt;
            CmnFunction.FitColumnHeader(spdData);
            dt.Dispose();
        }

        private Boolean CheckField()
        {
            Boolean Check = false;

            if (txtLotID.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD009", GlobalVariable.gcLanguage), "STD1902");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1902");
                return;
            }

            CmnExcelFunction.ExportToExcel(spdData, "Traceability", "", true, false, false, -1, -1);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnCloseLayoutForm();
                this.Dispose();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }
    }
}