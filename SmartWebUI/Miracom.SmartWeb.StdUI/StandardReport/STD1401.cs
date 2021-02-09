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
    public partial class STD1401 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1401()
        {
            InitializeComponent();
        }

        private void STD1401_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
        }

        private void btnView_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            string QueryCond = null;
            string FromDate="";
            string ToDate="";

            CheckField();
            if (optYesterday.Checked == true)
            {
                FromDate =DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8) + "000000"; ;
                ToDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8) + "235959"; ;
            }            
            else if (optDate.Checked == true)
            {
                FromDate = dtpDate.Value.ToString("yyyyMMdd") + "000000"; ;
                ToDate = dtpDate.Value.ToString("yyyyMMdd") + "235959"; ;
            }
            else if (optToday.Checked == true)
            {
                FromDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 8) + "000000";
                ToDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 8) + "235959";
            }            

            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvRes.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, FromDate);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, ToDate);
            
            spdData_Sheet1.RowCount=0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1401", QueryCond);
            if (dt.Rows.Count == 0) { spdData.Visible = false; }
            else { spdData.Visible = true; }
            spdData_Sheet1.DataSource = dt;
            CmnFunction.FitColumnHeader(spdData);
            dt.Dispose();

        }

        private Boolean CheckField()
        {
            Boolean Check = false;

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1401");
                Check = false;
            }
            else if (cdvRes.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD008", GlobalVariable.gcLanguage), "STD1401");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1401");
                return;
            }

            CmnExcelFunction.ExportToExcel(spdData, "Productivity", "", true, false, false, -1, -1);
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

        private void optDate_CheckedChanged(object sender, EventArgs e)
        {
            if (optDate.Checked == true)
            {
                dtpDate.Visible = true;
            }
            else
            {
                dtpDate.Visible = false;
            }
        }

        private void cdvRes_ButtonPress(object sender, EventArgs e)
        {
            cdvRes.Init();
            CmnInitFunction.InitListView(cdvRes.GetListView);
            cdvRes.Columns.Add("Factory", 50, HorizontalAlignment.Left);
            cdvRes.Columns.Add("Desc", 100, HorizontalAlignment.Left);
            cdvRes.SelectedSubItemIndex = 0;

            RptListFunction.ViewResourceList(cdvRes.GetListView, cdvFactory.Text.TrimEnd());
        }       
    }
}