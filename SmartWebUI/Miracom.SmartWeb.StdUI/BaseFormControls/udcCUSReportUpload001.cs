using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Miracom.SmartWeb.UI.Controls
{
    public partial class udcCUSReportUpload001 : Miracom.SmartWeb.FWX.udcUIBase
    {
        private string m_factory = "";
        private string m_plan_type = "";
        private int m_key_count = 10;
        private int m_value_count = 5;

        public udcCUSReportUpload001()
        {
            InitializeComponent();

            //화면 오픈시 메뉴의 Desc값으로 Title변경. by John Seo. 2008.10.06.
            if (GlobalVariable.gsSelFuncName != "")
                this.lblTitle.Text = GlobalVariable.gsSelFuncName;

            CmnInitFunction.InitListView(lisUploadData);
        }

        [Category("BaseForm"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Factory
        {
            get
            {
                return m_factory;
            }
            set
            {
                m_factory = value;
            }
        }

        [Category("BaseForm"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Plan_Type
        {
            get
            {
                return m_plan_type;
            }
            set
            {
                m_plan_type = value;
            }
        }

        [Category("BaseForm"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Key_Count
        {
            get
            {
                return m_key_count;
            }
            set
            {
                m_key_count = value;
            }
        }

        [Category("BaseForm"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Value_Count
        {
            get
            {
                return m_value_count;
            }
            set
            {
                m_value_count = value;
            }
        }

        protected virtual void GridColumnInit()
        {

        }

        private bool View_Plan(bool bValidation)
        {
            DataTable dt = null;
            StringBuilder strSqlString = new StringBuilder();

            int i;

            try
            {
                strSqlString.Append("      SELECT ");

                for (i = 1; i <= this.Key_Count; i++)
                {
                    if (i != 1) strSqlString.Append(", ");
                    strSqlString.Append("         KEY_FIELD_" + i.ToString());
                }

                for (i = 1; i <= this.Value_Count; i++)
                {
                    strSqlString.Append("        ,VALUE_FIELD_" + i.ToString());
                }

                strSqlString.Append("        FROM RWIPPLNDAT ");
                strSqlString.AppendFormat(" WHERE FACTORY = '{0}' ", this.Factory);
                strSqlString.AppendFormat("   AND PLAN_DATE = '{0}' ", dtpPlanDate.Value.ToString("yyyyMMdd"));
                strSqlString.AppendFormat("   AND PLAN_TYPE = '{0}' ", this.Plan_Type);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

                if (bValidation == false)
                {
                    GridColumnInit();

                    spdUploadData_Sheet1.RowCount = 0;
                    this.Refresh();

                    if (dt == null) return true;

                    //1.Griid 합계 표시
                    int[] rowType = spdUploadData.RPT_DataBindingWithSubTotal(dt, 0, this.Key_Count - 1, this.Key_Count + this.Value_Count - 1, null, null);

                    //2. 칼럼 고정(필요하다면..)
                    spdUploadData.Sheets[0].FrozenColumnCount = 0;

                    //3. Total부분 셀머지
                    spdUploadData.RPT_FillDataSelectiveCells("Total", 0, this.Key_Count + this.Value_Count - 1, 0, 1, true, Align.Center, VerticalAlign.Center);

                    //4. Column Auto Fit
                    spdUploadData.RPT_AutoFit(false);

                    return true;
                }
                else
                {
                    //Validation Check
                    if (dt.Rows.Count == 0) { return true; } else { return false; }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("udcCUSReportUpload001.View_Plan() " + "\r\n" + ex.ToString());
            }

            return true;
        }

        private void View_Plan_List()
        {
            ListViewItem itmX = null;

            DataTable dt = null;
            StringBuilder strSqlString = new StringBuilder();

            int i;

            try
            {
                CmnInitFunction.InitListView(lisUploadData);

                strSqlString.Append("      SELECT DISTINCT PLAN_DATE FROM RWIPPLNDAT ");
                strSqlString.AppendFormat(" WHERE FACTORY = '{0}' ", this.Factory);
                strSqlString.AppendFormat("   AND PLAN_DATE LIKE '{0}%' ", dtpPlanYear.Value.ToString("yyyy") + dtpPlanMonth.Value.ToString("MM"));
                strSqlString.AppendFormat("   AND PLAN_TYPE = '{0}' ", this.Plan_Type);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    itmX = new ListViewItem(CmnFunction.MakeDateFormat(dt.Rows[i][0].ToString(), DATE_TIME_FORMAT.SHORTDATE), (int)SMALLICON_INDEX.IDX_CALENDAR);
                    lisUploadData.Items.Add(itmX);
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("udcCUSReportUpload001.View_Plan_List() " + "\r\n" + ex.ToString());
            }
        }

        private bool Update_Plan_Data(string sStep)
        {
            string QueryCond = null;

            try
            {
                StringBuilder strSqlString = new StringBuilder();

                strSqlString.Append("      DELETE RWIPPLNDAT ");
                strSqlString.AppendFormat(" WHERE FACTORY = '{0}' ", this.Factory);
                strSqlString.AppendFormat("   AND PLAN_DATE = '{0}' ", dtpPlanDate.Value.ToString("yyyyMMdd"));
                strSqlString.AppendFormat("   AND PLAN_TYPE = '{0}' ", this.Plan_Type);

                CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
                QueryCond = null;

                if (sStep == "INSERT")
                {
                    int i, j;

                    for (i = 0; i < spdUploadData.Sheets[0].RowCount; i++)
                    {
                        //Key 컬럼 + Value 컬럼 = 최대 15
                        for (j = 0; j < 15; j++)
                        {
                            if (j < this.Key_Count)
                            {
                                //Key 데이터 입력
                                if (spdUploadData.Sheets[0].Cells[i, j].Text.ToUpper().IndexOf("TOTAL") < 0)
                                {
                                    if (spdUploadData.Sheets[0].Cells[i, j].Text.Trim() == "") spdUploadData.Sheets[0].Cells[i, j].Text = spdUploadData.Sheets[0].Cells[i - 1, j].Text;
                                    QueryCond += ", '" + spdUploadData.Sheets[0].Cells[i, j].Text + "'";
                                }
                                else
                                {
                                    QueryCond = null;
                                    break;
                                }
                            }
                            else
                            {
                                //Key 부분의 값이 없으면 공백으로 채워준다.
                                if (j < 10)
                                {
                                    QueryCond += ", ' '";
                                }
                                else
                                {
                                    //Value 부분읠 값을 입력
                                    if (j - this.Key_Count < spdUploadData.Sheets[0].ColumnCount)
                                    {
                                        QueryCond += ", " + spdUploadData.Sheets[0].Cells[i, spdUploadData.Sheets[0].ColumnCount - 1].Text;
                                    }
                                    else
                                    {
                                        //Value 부분의 값이 없으면 0으로 채워준다.
                                        QueryCond += ", 0";
                                    }
                                }
                            }
                        }

                        if (QueryCond != null)
                        {
                            strSqlString = new StringBuilder();

                            strSqlString.Append("\n");
                            strSqlString.Append("INSERT INTO RWIPPLNDAT (FACTORY, PLAN_DATE, PLAN_TYPE, ");
                            strSqlString.Append("            KEY_FIELD_1, KEY_FIELD_2, KEY_FIELD_3, KEY_FIELD_4, KEY_FIELD_5, ");
                            strSqlString.Append("            KEY_FIELD_6, KEY_FIELD_7, KEY_FIELD_8, KEY_FIELD_9, KEY_FIELD_10, ");
                            strSqlString.Append("            VALUE_FIELD_1, VALUE_FIELD_2, VALUE_FIELD_3, VALUE_FIELD_4, VALUE_FIELD_5, ");
                            strSqlString.Append("            CREATE_TIME, CREATE_USER_ID, UPDATE_TIME, UPDATE_USER_ID)");
                            strSqlString.AppendFormat("VALUES ('{0}', '{1}', '{2}' {3} ", GlobalVariable.gsAssyDefaultFactory, dtpPlanDate.Value.ToString("yyyyMMdd"), "WAFER", QueryCond);
                            strSqlString.AppendFormat("       ,'{0}', '{1}', '{2}', '{3}') ", DateTime.Now.ToString("yyyyMMdd"), "ADMIN", " ", " ");

                            CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
                            QueryCond = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("Data Error : " + "\r\n" + ex.ToString());
                return false;
            }
            return true;
        }

        private void udcCUSReportUpload001_Load(object sender, EventArgs e)
        {         
            if (this.DesignMode == false)
            {
                GridColumnInit();
                LanguageFunction.ToClientLanguage(this);
                View_Plan_List();
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string sFile = "";
            FarPoint.Win.Spread.FpSpread spdTemp = new FarPoint.Win.Spread.FpSpread();

            try
            {
                openFileDialog1.DefaultExt = "xls";
                openFileDialog1.Filter = "Excel files (*.xls)|*.xls|Excel files (*.xlsx)|*.xlsx";

                if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                txtFilePath.Text = openFileDialog1.FileName;
                sFile = openFileDialog1.FileName;
                spdTemp.OpenExcel(sFile, FarPoint.Excel.ExcelOpenFlags.NoFlagsSet);
                spdUploadData.Sheets.RemoveAt(0);
                spdUploadData.Sheets.Add(spdTemp.Sheets[0]);
                spdUploadData.Sheets[0].ColumnCount = this.Key_Count + this.Value_Count;
                spdUploadData.Sheets[0].RowCount = 16;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("udcCUSReportUpload001.btnOpenFile_Click() " + "\r\n" + ex.ToString());
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (View_Plan(true) == false)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD012", GlobalVariable.gcLanguage));
                return;
            }

            if (Update_Plan_Data("INSERT") == true)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));
                View_Plan_List();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF002", GlobalVariable.gcLanguage), "", MessageBoxButtons.YesNo, 2) != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            if (Update_Plan_Data("DELETE") == true)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));
                View_Plan_List();
            }
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
                CmnFunction.ShowMsgBox("udcCUSReportUpload001.btnClose_Click()" + "\r\n" + ex.Message);
            }
        }

        private void dtpPlanYear_ValueChanged(object sender, EventArgs e)
        {
            View_Plan_List();
        }

        private void dtpPlanMonth_ValueChanged(object sender, EventArgs e)
        {
            View_Plan_List();
        }

        private void lisUploadData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lisUploadData.SelectedItems.Count == 0) return;
            dtpPlanDate.Text = lisUploadData.SelectedItems[0].Text;
            View_Plan(false);
        }
    }
}
