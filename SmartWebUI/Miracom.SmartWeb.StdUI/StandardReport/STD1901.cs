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
    public partial class STD1901 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1901()
        {
            InitializeComponent();
        }

        private void STD1901_Load(object sender, EventArgs e)
        {
            //CmnInitFunction.InitSpread(spdData);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string QueryCond = null;
                        
            CheckField();
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtLotID.Text);
            
            spdData_Sheet1.RowCount = 0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1901", QueryCond);
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

            if (txtLotID.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD009", GlobalVariable.gcLanguage), "STD1901");
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
            spdData.Sheets[0].RowCount = 65;
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
            
            spdData.Sheets[0].Cells[1, 0].Value = "Lot ID:";
            spdData.Sheets[0].Cells[1, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[0]); // LOT_ID
            spdData.Sheets[0].Cells[1, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[1, 5].Value = "Factory:";
            spdData.Sheets[0].Cells[1, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[2]); // FACTORY
            spdData.Sheets[0].Cells[1, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[3, 0].Value = "Description:";
            spdData.Sheets[0].Cells[3, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[1]); // LOT_DESC
            spdData.Sheets[0].Cells[3, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[3, 1].ColumnSpan = 3;
            spdData.Sheets[0].Cells[5, 0].Value = "Material:";
            spdData.Sheets[0].Cells[5, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[3]); // MAT_ID
            spdData.Sheets[0].Cells[5, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[7, 0].Value = "Flow:";
            spdData.Sheets[0].Cells[7, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[4]); // FLOW
            spdData.Sheets[0].Cells[7, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[9, 0].Value = "Operation:";
            spdData.Sheets[0].Cells[9, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[5]); // OPER
            spdData.Sheets[0].Cells[9, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[11, 0].Value = "Qty 1/2/3:";
            spdData.Sheets[0].Cells[11, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[6]); // QTY_1
            spdData.Sheets[0].Cells[11, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[11, 2].Value = Convert.ToString(dt.Rows[0].ItemArray[7]); // QTY_2
            spdData.Sheets[0].Cells[11, 2].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[11, 3].Value = Convert.ToString(dt.Rows[0].ItemArray[8]); // QTY_3
            spdData.Sheets[0].Cells[11, 3].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[17, 0].Value = "Lot Type:";
            spdData.Sheets[0].Cells[17, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[9]); // LOT_TYPE
            spdData.Sheets[0].Cells[17, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[13, 5].Value = "Owner Code:";
            spdData.Sheets[0].Cells[13, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[10]); // OWNER_CODE
            spdData.Sheets[0].Cells[13, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[11, 5].Value = "Create Code:";
            spdData.Sheets[0].Cells[11, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[11]); // CREATE_CODE
            spdData.Sheets[0].Cells[11, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[21, 0].Value = "Lot Priority:";
            spdData.Sheets[0].Cells[21, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[12]); // LOT_PRIORITY
            spdData.Sheets[0].Cells[21, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[19, 0].Value = "Lot Status:";
            spdData.Sheets[0].Cells[19, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[13]); // LOT_STATUS
            spdData.Sheets[0].Cells[19, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[15, 5].Value = "Hold Code:";
            spdData.Sheets[0].Cells[15, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[15]); // HOLD_CODE
            spdData.Sheets[0].Cells[15, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[15, 0].Value = "Oper In Qty 1/2/3:";
            spdData.Sheets[0].Cells[15, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[17]); // OPER_IN_QTY_1
            spdData.Sheets[0].Cells[15, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[15, 2].Value = Convert.ToString(dt.Rows[0].ItemArray[18]); // OPER_IN_QTY_2
            spdData.Sheets[0].Cells[15, 2].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[15, 3].Value = Convert.ToString(dt.Rows[0].ItemArray[19]); // OPER_IN_QTY_3
            spdData.Sheets[0].Cells[15, 3].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[13, 0].Value = "Create Qty 1/2/3:";
            spdData.Sheets[0].Cells[13, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[20]); // CREATE_QTY_1
            spdData.Sheets[0].Cells[13, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[13, 2].Value = Convert.ToString(dt.Rows[0].ItemArray[21]); // CREATE_QTY_2
            spdData.Sheets[0].Cells[13, 2].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[13, 3].Value = Convert.ToString(dt.Rows[0].ItemArray[22]); // CREATE_QTY_3
            spdData.Sheets[0].Cells[13, 3].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[27, 5].Value = "Inventory Unit:";
            spdData.Sheets[0].Cells[27, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[26]); // INV_UNIT
            spdData.Sheets[0].Cells[27, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[35, 5].Value = "Rework Code:";
            spdData.Sheets[0].Cells[35, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[28]); // RWK_CODE
            spdData.Sheets[0].Cells[35, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[37, 5].Value = "Rework Count:";
            spdData.Sheets[0].Cells[37, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[29]); // RWK_COUNT
            spdData.Sheets[0].Cells[37, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[39, 5].Value = "Rework Return Flow:";
            spdData.Sheets[0].Cells[39, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[30]); // RWK_RET_FLOW
            spdData.Sheets[0].Cells[39, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[41, 5].Value = "Rework Return Oper:";
            spdData.Sheets[0].Cells[41, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[31]); // RWK_RET_OPER
            spdData.Sheets[0].Cells[41, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[43, 5].Value = "Rework End Flow:";
            spdData.Sheets[0].Cells[43, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[32]); // RWK_END_FLOW
            spdData.Sheets[0].Cells[43, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[45, 5].Value = "Rework End Oper:";
            spdData.Sheets[0].Cells[45, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[33]); // RWK_END_OPER
            spdData.Sheets[0].Cells[45, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[35, 0].Value = "Start Time:";
            spdData.Sheets[0].Cells[35, 1].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[43])); // START_TIME
            spdData.Sheets[0].Cells[35, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[29, 0].Value = "Start Resource ID:";
            spdData.Sheets[0].Cells[29, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[44]); // START_RES_ID
            spdData.Sheets[0].Cells[29, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[37, 0].Value = "End time:";
            spdData.Sheets[0].Cells[37, 1].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[46])); // END_TIME
            spdData.Sheets[0].Cells[37, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[31, 0].Value = "End Resource ID:";
            spdData.Sheets[0].Cells[31, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[47]); // END_RES_ID
            spdData.Sheets[0].Cells[31, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[25, 0].Value = "Sample Result:";
            spdData.Sheets[0].Cells[25, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[50]); // SAMPLE_RESULT
            spdData.Sheets[0].Cells[25, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[23, 0].Value = "Split From Lot ID:";
            spdData.Sheets[0].Cells[23, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[51]); // SPLIT_FROM_LOT_ID
            spdData.Sheets[0].Cells[23, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[17, 5].Value = "Ship Code:";
            spdData.Sheets[0].Cells[17, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[52]); // SHIP_CODE
            spdData.Sheets[0].Cells[17, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[39, 0].Value = "Ship Time:";
            spdData.Sheets[0].Cells[39, 1].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[53])); // SHIP_TIME
            spdData.Sheets[0].Cells[39, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[41, 0].Value = "Original Due Time:";
            spdData.Sheets[0].Cells[41, 1].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[54])); // ORG_DUE_TIME
            spdData.Sheets[0].Cells[41, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[43, 0].Value = "Schedule Deu Time:";
            spdData.Sheets[0].Cells[43, 1].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[55])); // SCH_DUE_TIME
            spdData.Sheets[0].Cells[43, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[45, 0].Value = "Create Time:";
            spdData.Sheets[0].Cells[45, 1].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[56])); // CREATE_TIME
            spdData.Sheets[0].Cells[45, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[47, 0].Value = "Factory In Time:";
            spdData.Sheets[0].Cells[47, 1].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[57])); // FAC_IN_TIME
            spdData.Sheets[0].Cells[47, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[49, 0].Value = "Flow In Time:";
            spdData.Sheets[0].Cells[49, 1].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[58])); // FLOW_IN_TIME
            spdData.Sheets[0].Cells[49, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[51, 0].Value = "Oper In Time:";
            spdData.Sheets[0].Cells[51, 1].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[59])); // OPER_IN_TIME
            spdData.Sheets[0].Cells[51, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[33, 0].Value = "Reserve Resource ID:";
            spdData.Sheets[0].Cells[33, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[60]); // RESERVE_RES_ID
            spdData.Sheets[0].Cells[33, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[19, 5].Value = "Batch ID:";
            spdData.Sheets[0].Cells[19, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[61]); // BATCH_ID
            spdData.Sheets[0].Cells[19, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[21, 5].Value = "Batch Seq:";
            spdData.Sheets[0].Cells[21, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[62]); // BATCH_SEQ
            spdData.Sheets[0].Cells[21, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[23, 5].Value = "Order ID:";
            spdData.Sheets[0].Cells[23, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[63]); // ORDER_ID
            spdData.Sheets[0].Cells[23, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[25, 5].Value = "Lot Location:";
            spdData.Sheets[0].Cells[25, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[67]); // LOT_LOCATION
            spdData.Sheets[0].Cells[25, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[27, 0].Value = "Lot Delete Reason:";
            spdData.Sheets[0].Cells[27, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[89]); // LOT_DEL_REASON
            spdData.Sheets[0].Cells[27, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[53, 0].Value = "Lot Delete Time:";
            spdData.Sheets[0].Cells[53, 1].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[90])); // LOT_DEL_TIME
            spdData.Sheets[0].Cells[53, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[61, 0].Value = "BOM Set ID:";
            spdData.Sheets[0].Cells[61, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[91]); // BOM_SET_ID
            spdData.Sheets[0].Cells[61, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[63, 0].Value = "BOM Ser Version:";
            spdData.Sheets[0].Cells[63, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[92]); // BOM_SET_VERSION
            spdData.Sheets[0].Cells[63, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[55, 0].Value = "Last Tran Code:";
            spdData.Sheets[0].Cells[55, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[95]); // LAST_TRAN_CODE
            spdData.Sheets[0].Cells[55, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[57, 0].Value = "Last Tran Time:";
            spdData.Sheets[0].Cells[57, 1].Value = RptComFunction.AttachDateFormat(Convert.ToString(dt.Rows[0].ItemArray[96])); // LAST_TRAN_TIME
            spdData.Sheets[0].Cells[57, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[59, 0].Value = "Last Comment:";
            spdData.Sheets[0].Cells[59, 1].Value = Convert.ToString(dt.Rows[0].ItemArray[97]); // LAST_COMMENT
            spdData.Sheets[0].Cells[59, 1].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[59, 1].ColumnSpan = 3;
            spdData.Sheets[0].Cells[47, 5].Value = "Last Active Hist Seq:";
            spdData.Sheets[0].Cells[47, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[99]); // LAST_ACTIVE_HIST_SEQ
            spdData.Sheets[0].Cells[47, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            spdData.Sheets[0].Cells[49, 5].Value = "Last Hist Seq:";
            spdData.Sheets[0].Cells[49, 6].Value = Convert.ToString(dt.Rows[0].ItemArray[98]); // LAST_HIST_SEQ
            spdData.Sheets[0].Cells[49, 6].Border = new FarPoint.Win.LineBorder(Color.Blue, 2);
            
        }
                
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1901");
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