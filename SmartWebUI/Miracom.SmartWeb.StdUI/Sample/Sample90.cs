using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

namespace Miracom.SmartWeb.UI.Sample
{
    public partial class Sample90 : Miracom.SmartWeb.UI.Controls.udcCUSReportUpload001
    {
        public Sample90()
        {
            InitializeComponent();
        }

        private void Sample90_Load(object sender, EventArgs e)
        {
        }

        protected override void GridColumnInit()
        {
            spdUploadData.RPT_ColumnInit();
            spdUploadData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdUploadData.RPT_AddBasicColumn("Pkg Type", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdUploadData.RPT_AddBasicColumn("Lead", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdUploadData.RPT_AddBasicColumn("Density", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdUploadData.RPT_AddBasicColumn("Tech", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdUploadData.RPT_AddBasicColumn("Input plan", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
        }
    }
}
