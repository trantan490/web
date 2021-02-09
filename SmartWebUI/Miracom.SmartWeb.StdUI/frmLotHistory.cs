using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public partial class frmLotHistory : Form
    {
        private string sLot_ID = null;

        public frmLotHistory()
        {
            InitializeComponent();
        }

        public frmLotHistory(string Lot_ID)
        {
            InitializeComponent();
            sLot_ID = Lot_ID;
            frmLotHistory_Load(null, null);
        }

        private void frmLotHistory_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
            WinXpStyle.FormLoad(sender, e, this);
            if (sLot_ID != null)
            {
                txtLotID.Text = sLot_ID;
                btnView_Click(null, null);
            }            
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
            spdData_Sheet1.DataSource = dt;
            dt.Dispose();
        }

        private Boolean CheckField()
        {
            Boolean Check = false;

            if (txtLotID.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD009", GlobalVariable.gcLanguage), "ViewLotHistory");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "ViewLotHistory");
                return;
            }

            CmnExcelFunction.ExportToExcel(spdData, "View Lot History", "", true, false, false, -1, -1);
        }

    }
}