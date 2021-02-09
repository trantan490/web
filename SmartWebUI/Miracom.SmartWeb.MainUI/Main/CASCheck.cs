using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Miracom.SmartWeb.UI
{
    public partial class CASCheck : UserControl
    {
        public string CAS_STATUS
        {
            get
            {
                return "SUCCESS";
            }
        }

        public CASCheck()
        {
            InitializeComponent();
        }

    }
}
