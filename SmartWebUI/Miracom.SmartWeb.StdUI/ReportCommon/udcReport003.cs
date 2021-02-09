using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.UltraChart.Shared.Styles;

namespace Miracom.SmartWeb.UI.BaseFormControls
{
    public partial class udcReport003 : Miracom.SmartWeb.UI.BaseFormControls.udcReport001
    {
        private char m_c_step;
        private string m_s_function_name;
        private string m_s_condition;
        private Infragistics.UltraChart.Shared.Styles.ChartType m_utr_chart_type;

        public udcReport003()
        {
            InitializeComponent();
        }

        #region "Properties"

        public char Step
        {
            get
            {
                return m_c_step;
            }
            set
            {
                m_c_step = value;
            }
        }

        public string Function_Name
        {
            get
            {
                return m_s_function_name;
            }
            set
            {
                m_s_function_name = value;
            }
        }

        public string Condition
        {
            get
            {
                return m_s_condition;
            }
            set
            {
                m_s_condition = value;
            }
        }

        public Infragistics.UltraChart.Shared.Styles.ChartType Chart_Type
        {
            get
            {
                return m_utr_chart_type;
            }
            set
            {
                m_utr_chart_type = value;
            }
        }

        #endregion

        private void Make_Chart(DataTable dtTable)
        {
            utrChart.Legend.Visible = true;
            utrChart.Legend.SpanPercentage = 20;
            utrChart.Legend.Location = LegendLocation.Right;

            utrChart.ChartType = Chart_Type;
            if (Chart_Type == ChartType.DoughnutChart) utrChart.DoughnutChart.Concentric = true;

            utrChart.Data.DataSource = Modify_DataTable(dtTable);
            utrChart.Data.DataBind();

        }
        protected virtual DataTable Modify_DataTable(DataTable dtTable)
        {
            return dtTable;
        }

        protected virtual DataTable Modify_Chart_DataTable(DataTable dtTable)
        {
            return dtTable;
        }

        private void udcReport003_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
            CmnInitFunction.InitChart(utrChart);
        }

        private void udcReport003_ButtonViewClick(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dtChart = null;

            if (Step.ToString() == " " || Step.ToString() == "") return;
            if (Function_Name.Trim() == "") return;

            spdData_Sheet1.RowCount = 0;

            DateTime dtStart = DateTime.Now;
            dt = CmnFunction.oComm.GetFuncDataTable(Function_Name, Condition);
            dtChart = dt.Copy();
            DateTime dtEnd = DateTime.Now;
            TimeSpan ts = dtEnd.Subtract(dtStart);
            lblExecution.Text = "Execution Time : " + ts.TotalSeconds.ToString("#,##0.0000") + "(msecs)  ";
            lblExecution.Text += "Data Count : " + dt.Rows.Count.ToString();

            if (dt.Rows.Count == 0)
            {
                spdData.Visible = false;
                utrChart.Visible = false;
            }
            else
            {
                spdData.Visible = true;
                utrChart.Visible = true;
                spdData_Sheet1.DataSource = Modify_DataTable(dt);
                CmnFunction.FitColumnHeader(spdData);

                Make_Chart(dtChart);
            }

            dt.Dispose();
            dtChart.Dispose();
        }

        private void udcReport003_ButtonExcelExportClick(object sender, EventArgs e)
        {
            Control[] obj = null;
            string sCondition = "";

            if (pnlCondition1.Visible == true)
            {
                if (udcPeriod1.Visible == true)
                {
                    sCondition += " From Date : " + " ";
                    sCondition += " To Date : " + " ";
                }
                if (udcCondition1.Visible == true)
                {
                    sCondition += udcCondition1.ConditionText + " : " + udcCondition1.Text;
                }
            }
            if (pnlCondition2.Visible == true)
            {
                if (udcCondition2.Visible == true)
                {
                    sCondition += udcCondition2.ConditionText + " : " + udcCondition2.Text;
                }
                if (udcCondition3.Visible == true)
                {
                    sCondition += udcCondition3.ConditionText + " : " + udcCondition3.Text;
                }
            }
            if (pnlCondition3.Visible == true)
            {
                if (udcCondition4.Visible == true)
                {
                    sCondition += udcCondition4.ConditionText + " : " + udcCondition4.Text;
                }
                if (udcCondition5.Visible == true)
                {
                    sCondition += udcCondition5.ConditionText + " : " + udcCondition5.Text;
                }
            }
            if (pnlCondition4.Visible == true)
            {
                if (udcCondition6.Visible == true)
                {
                    sCondition += udcCondition6.ConditionText + " : " + udcCondition6.Text;
                }
                if (udcCondition7.Visible == true)
                {
                    sCondition += udcCondition7.ConditionText + " : " + udcCondition7.Text;
                }
            }
            if (pnlCondition5.Visible == true)
            {
                if (udcCondition8.Visible == true)
                {
                    sCondition += udcCondition8.ConditionText + " : " + udcCondition8.Text;
                }
                if (udcCondition9.Visible == true)
                {
                    sCondition += udcCondition9.ConditionText + " : " + udcCondition9.Text;
                }
            }
            if (pnlCondition6.Visible == true)
            {
                if (udcCondition10.Visible == true)
                {
                    sCondition += udcCondition10.ConditionText + " : " + udcCondition10.Text;
                }
                if (udcCondition11.Visible == true)
                {
                    sCondition += udcCondition11.ConditionText + " : " + udcCondition11.Text;
                }
            }
            if (pnlCondition7.Visible == true)
            {
                if (udcCondition12.Visible == true)
                {
                    sCondition += udcCondition12.ConditionText + " : " + udcCondition12.Text;
                }
                if (udcCondition13.Visible == true)
                {
                    sCondition += udcCondition13.ConditionText + " : " + udcCondition13.Text;
                }
            }
            if (pnlCondition8.Visible == true)
            {
                if (udcCondition14.Visible == true)
                {
                    sCondition += udcCondition14.ConditionText + " : " + udcCondition14.Text;
                }
                if (udcCondition15.Visible == true)
                {
                    sCondition += udcCondition15.ConditionText + " : " + udcCondition15.Text;
                }
            }

            if (spdData_Sheet1.RowCount == 0) return;
            obj = new Control[1];
            obj[0] = spdData;

            CmnExcelFunction.ExportToExcelEx(obj, "", 0, Title, sCondition, true, false, false, -1, -1);
        }
    }
}

