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
    public partial class STD1905 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1905()
        {
            InitializeComponent();
        }

        private void STD1905_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData, true);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable RetDT = null;
            string QueryCond = null;
            if (CheckField() == false) return;

            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            if (rbtWaferID.Checked)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, "1");
            }
            else if (rbtInLotID.Checked)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, "2");
            }
            else if (rbtOutLotID.Checked)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, "3");
            }
            else if (rbtLasermark.Checked)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, "4");
            }

            QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtID.Text);
            
            spdData_Sheet1.RowCount = 0;
            this.Refresh();

            RetDT = CmnFunction.oComm.GetFuncDataTable("STD1905", QueryCond);
            
            if (RetDT.Rows.Count == 0) { spdData.Visible = false; }
            else
            {
                spdData.Visible = true;
                spdData_Sheet1.DataSource = RetDT;
            }

            CmnFunction.FitColumnHeader(spdData);

            RetDT.Dispose();
        }

        private Boolean CheckField()
        {
            Boolean Check = false;

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1905");
                Check = false;
            }
            else { Check = true; }

            if (txtID.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1905");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {

            Control[] obj = null;
            obj = new Control[1];
            obj[0] = spdData;

            CmnExcelFunction.ExportToExcelEx(obj, null, 1, "Production Output", "", true, false, false, -1, -1);
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

        private void cdvFactory_ButtonPress(object sender, EventArgs e)
        {
            cdvFactory.Init();
            CmnInitFunction.InitListView(cdvFactory.GetListView);
            cdvFactory.Columns.Add("Factory", 50, HorizontalAlignment.Left);
            cdvFactory.Columns.Add("Desc", 100, HorizontalAlignment.Left);
            cdvFactory.SelectedSubItemIndex = 0;

            RptListFunction.ViewFactoryList(cdvFactory.GetListView);
        }
    }
}