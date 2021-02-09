using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.UI.Controls;


namespace Hana.CUS
{
    public partial class CUS060201 : Miracom.SmartWeb.UI.Controls.udcCUSReportUpload001
    {
        public CUS060201()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Column Header 초기화 - 항상 override 되어야 함
        /// </summary>
        protected override void GridColumnInit()
        {
            spdUploadData.RPT_ColumnInit();
            spdUploadData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdUploadData.RPT_AddBasicColumn("Pkg Type", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdUploadData.RPT_AddBasicColumn("Lead", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdUploadData.RPT_AddBasicColumn("Density", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdUploadData.RPT_AddBasicColumn("Tech", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdUploadData.RPT_AddBasicColumn("투입계획", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
        }
    }
}
