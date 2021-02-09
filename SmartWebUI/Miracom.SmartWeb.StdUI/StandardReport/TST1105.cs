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
    public partial class TST1105 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public TST1105()
        {
            InitializeComponent();
        }

        private void TST1105_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
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

        private void udcFac_ValueTextChanged(object sender, EventArgs e)
        {
            udcMat.Init();
            udcFlow.Init();
            udcOper.Init();
            udcMat.sFactory = udcFac.txtValue;
            udcFlow.sFactory = udcFac.txtValue;
            udcOper.sFactory = udcFac.txtValue;
        }

        private void udcMat_ValueTextChanged(object sender, EventArgs e)
        {
            udcFlow.Init();
            udcOper.Init();
            udcFlow.ParentValue = udcMat.txtValue;
        }

        private void udcMat_SubValueTextChanged(object sender, EventArgs e)
        {
            udcFlow.Init();
            udcOper.Init();
            udcFlow.ParentSubValue = udcMat.txtSubValue;
        }
        
        private void udcFlow_ValueTextChanged(object sender, EventArgs e)
        {
            udcOper.Init();
            udcOper.ParentValue = udcFlow.txtValue;
        }

        private void udcFlow_SubValueTextChanged(object sender, EventArgs e)
        {
            udcOper.Init();
            udcOper.ParentSubValue = udcFlow.txtSubValue;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string QueryCond = null;

            CheckField();
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, dtpDelayDate.Value.ToString("yyyyMMdd"));

            QueryCond = FwxCmnFunction.PackCondition(QueryCond, udcFac.txtValue);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, udcMat.txtValue);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, udcMat.txtSubValue);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, udcFlow.txtValue);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, udcFlow.txtSubValue);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, udcOper.txtValue);

            spdData_Sheet1.RowCount = 0;

            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("TST1105", QueryCond);
            if (dt.Rows.Count == 0)
            {
                spdData.Visible = false;
            }
            else
            {
                spdData.Visible = true;
            }
            spdData_Sheet1.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                CmnFunction.FitColumnHeader(spdData);
            }
            dt.Dispose();
        }
        
        private Boolean CheckField()
        {
            Boolean Check = false;

            if (dtpDelayDate.Text.TrimEnd() == "")
            {
                MessageBox.Show("Please Select Delayed Date");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

    }
}
