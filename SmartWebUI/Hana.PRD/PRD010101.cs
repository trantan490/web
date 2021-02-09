using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Hana.PRD
{
    public partial class PRD010101 : Miracom.SmartWeb.UI.Controls.udcCUSReportMain001
    {
        public PRD010101()
        {
            InitializeComponent();
        }

        private void PRD010101_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.OnCloseLayoutForm();
            this.Dispose();
        }
    }
}

