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
    public partial class STD1903 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1903()
        {
            InitializeComponent();
        }

        private void STD1903_Load(object sender, EventArgs e)
        {
            //CmnInitFunction.InitSpread(spdData);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string QueryCond = null;
                        
            CheckField();
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvRes.Text);
            
            spdData_Sheet1.RowCount = 0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1903", QueryCond);
            if (dt.Rows.Count != 0)
            {
                spdData.Visible = true;
                FillSpread(dt);
            }
            else
            {
                spdData.Sheets[0].RowCount = 0;
                spdData.Sheets[0].ColumnCount = 0;
                spdData.Visible = false;
            }
            CmnFunction.FitColumnHeader(spdData);
            dt.Dispose();
        }


        private Boolean CheckField()
        {
            Boolean Check = false;

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1903");
                Check = false;
            }
            else if (cdvRes.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD008", GlobalVariable.gcLanguage), "STD1903");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

        private void FillSpread(DataTable dt)
        {
            int i;
            int j;
            spdData.BackColor = Color.Gray;
            spdData.Sheets[0].ColumnHeaderRowCount = 0;
            spdData.Sheets[0].RowHeaderColumnCount = 0;
            spdData.Sheets[0].RowCount = 30;
            spdData.Sheets[0].ColumnCount = 7;            
                        
            for (i = 0; i < spdData.Sheets[0].ColumnCount; i++)
            {
                if (i == 4) { spdData.Sheets[0].Columns[i].Width = 5; }
                else { spdData.Sheets[0].Columns[i].Width = 120; }
            }

            for (i = 0; i < spdData.Sheets[0].RowCount; i++)
            {
                if (i % 2 == 0) { spdData.Sheets[0].Rows[i].Height = 5; }
                for (j = 0; j < spdData.Sheets[0].ColumnCount; j++)
                {
                    spdData.Sheets[0].Cells[i, j].Border = new FarPoint.Win.LineBorder(Color.White, 2);
                }
            }
            
            spdData.Sheets[0].Cells[1, 0].Value = "Factory:";
            spdData.Sheets[0].Cells[1, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[0]); // FACTORY
            spdData.Sheets[0].Cells[1, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[3, 0].Value = "Resource:";
            spdData.Sheets[0].Cells[3, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[1]); // RES_ID
            spdData.Sheets[0].Cells[3, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[5, 0].Value = "Description:";
            spdData.Sheets[0].Cells[5, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[2]); // RES_DESC
            spdData.Sheets[0].Cells[5, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[5, 1].ColumnSpan = 3;
            spdData.Sheets[0].Cells[13, 0].Value = "Resource Type:";
            spdData.Sheets[0].Cells[13, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[3]); // RES_TYPE
            spdData.Sheets[0].Cells[13, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[21, 0].Value = "Area ID:";
            spdData.Sheets[0].Cells[21, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[35]); // AREA_ID
            spdData.Sheets[0].Cells[21, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[23, 0].Value = "Sub Area ID:";
            spdData.Sheets[0].Cells[23, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[36]); // SUB_AREA_ID
            spdData.Sheets[0].Cells[23, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[25, 0].Value = "Location:";
            spdData.Sheets[0].Cells[25, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[37]); // RES_LOCATION
            spdData.Sheets[0].Cells[25, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[27, 0].Value = "Proc Mode:";
            spdData.Sheets[0].Cells[27, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[38]); // PROC_RULE
            spdData.Sheets[0].Cells[27, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[19, 0].Value = "Max Proc Count:";
            spdData.Sheets[0].Cells[19, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[39]); // MAX_PROC_COUNT
            spdData.Sheets[0].Cells[19, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[19, 5].Value = "Res Delete User:";
            spdData.Sheets[0].Cells[19, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[46]); // DELETE_USER_ID
            spdData.Sheets[0].Cells[19, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[21, 5].Value = "Res Delete Time:";
            spdData.Sheets[0].Cells[21, 6].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[47])); // DELETE_TIME
            spdData.Sheets[0].Cells[21, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[23, 5].Value = "Create User:";
            spdData.Sheets[0].Cells[23, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[48]); // CREATE_USER_ID
            spdData.Sheets[0].Cells[23, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[25, 5].Value = "Create Time:";
            spdData.Sheets[0].Cells[25, 6].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[49])); // CREATE_TIME
            spdData.Sheets[0].Cells[25, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[27, 5].Value = "Update User:";
            spdData.Sheets[0].Cells[27, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[50]); // UPDATE_USER_ID
            spdData.Sheets[0].Cells[27, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[29, 5].Value = "Update Time:";
            spdData.Sheets[0].Cells[29, 6].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[51])); // UPDATE_TIME
            spdData.Sheets[0].Cells[29, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[9, 0].Value = "Up Down Flag:";
            spdData.Sheets[0].Cells[9, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[52]); // RES_UP_DOWN_FLAG
            spdData.Sheets[0].Cells[9, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[7, 0].Value = "Resource Status:";
            spdData.Sheets[0].Cells[7, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[54]); // RES_STS_1
            spdData.Sheets[0].Cells[7, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[27, 0].Value = "Proc Mode:";
            spdData.Sheets[0].Cells[27, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[68]); // RES_PROC_MODE
            spdData.Sheets[0].Cells[27, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[11, 0].Value = "Proc Count:";
            spdData.Sheets[0].Cells[11, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[70]); // PROC_COUNT
            spdData.Sheets[0].Cells[11, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[7, 5].Value = "Last Start Time:";
            spdData.Sheets[0].Cells[7, 6].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[71])); // LAST_START_TIME
            spdData.Sheets[0].Cells[7, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[9, 5].Value = "Last End Time:";
            spdData.Sheets[0].Cells[9, 6].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[72])); // LAST_END_TIME
            spdData.Sheets[0].Cells[9, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[11, 5].Value = "Last Event:";
            spdData.Sheets[0].Cells[11, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[75]); // LAST_EVENT_ID
            spdData.Sheets[0].Cells[11, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[13, 5].Value = "Last Event Time:";
            spdData.Sheets[0].Cells[13, 6].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[76])); // LAST_EVENT_TIME
            spdData.Sheets[0].Cells[13, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[15, 5].Value = "Last Act Hist Seq:";
            spdData.Sheets[0].Cells[15, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[77]); // LAST_ACTIVE_HIST_SEQ
            spdData.Sheets[0].Cells[15, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[17, 5].Value = "Last Hist Seq:";
            spdData.Sheets[0].Cells[17, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[78]); // LAST_HIST_SEQ
            spdData.Sheets[0].Cells[17, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
        }
                
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1903");
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
    }
}