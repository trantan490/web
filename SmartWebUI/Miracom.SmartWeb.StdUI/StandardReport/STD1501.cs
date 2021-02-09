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
    public partial class STD1501 : Miracom.SmartWeb.FWX.udcUIBase
    {


        public STD1501()
        {
            InitializeComponent();
        }

        private void STD1501_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string QueryCond = null;
                        
            CheckField();
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvRes.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, StartDate());
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, EndDate());
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvOper.Text);
            
            spdData_Sheet1.RowCount=0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1501", QueryCond);
            if (dt.Rows.Count == 0) { spdData.Visible = false; }
            else { spdData.Visible = true; }
            spdData.Sheets[0].DataSource = dt;
            CmnFunction.FitColumnHeader(spdData);
            dt.Dispose();  

        }

        private Boolean CheckField()
        {
            Boolean Check = false;

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1501");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }


        private string StartDate()
        {
            string FromDate = null;

            if (optYesterday.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastWeek.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) - 7).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastMonth.Checked == true)
            {
                FromDate = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd").Substring(0, 6) + "01";
            }
            else if (optPeriod.Checked == true)
            {
                FromDate = dtpFromDate.Value.ToString("yyyyMMdd");
            }
            else if (optToday.Checked == true)
            {
                FromDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisWeek.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek)).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisMonth.Checked == true)
            {
                FromDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6) + "01";
            }
            return FromDate+"000000";
        }

        private string EndDate()
        {
            string ToDate = null;

            if (optYesterday.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastWeek.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) - 1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastMonth.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-(DateTime.Now.Day)).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optPeriod.Checked == true)
            {
                ToDate = dtpToDate.Value.ToString("yyyyMMdd");
            }
            else if (optToday.Checked == true)
            {
                ToDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisWeek.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) + 6).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisMonth.Checked == true)
            {
                ToDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6) + "31";
            }
            return ToDate + "235959";
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1501");
                return;
            }
            CmnExcelFunction.ExportToExcel(spdData, "Resource Analysis", "", true, false, false, -1, -1);
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
              
        private void cdvRes_ButtonPress(object sender, EventArgs e)
        {
            cdvRes.Init();
            CmnInitFunction.InitListView(cdvRes.GetListView);
            cdvRes.Columns.Add("Factory", 50, HorizontalAlignment.Left);
            cdvRes.Columns.Add("Desc", 100, HorizontalAlignment.Left);
            cdvRes.SelectedSubItemIndex = 0;

            RptListFunction.ViewResourceList(cdvRes.GetListView, cdvFactory.Text.TrimEnd());
        }

        private void optPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (optPeriod.Checked == true)
            {
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
            }
            else
            {
                dtpFromDate.Visible = false;
                dtpToDate.Visible = false;
            }
        }

        private void cdvOper_ButtonPress(object sender, EventArgs e)
        {
            cdvOper.Init();
            CmnInitFunction.InitListView(cdvOper.GetListView);
            cdvOper.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
            cdvOper.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
            cdvOper.SelectedSubItemIndex = 0;

            RptListFunction.ViewOperList(cdvOper.GetListView, cdvFactory.Text, "");
        }

    }
}