using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI.Controls
{
    public partial class udcPeriod : UserControl
    {
        private udcPeriod.ePeriodType m_cond_period_type;
        private string m_cond_factory = "SYSTEM";
        private char m_cond_c_step;
        private bool m_cond_b_calendar_base;
        private m_work_time WorkTime_tmp = new m_work_time();
        private m_factory_shift Factory_Shift = new m_factory_shift();

        private struct m_factory_shift
        {
            public m_factory_shift_shift[] shift;
        }

        private struct m_factory_shift_shift
        {
            public string shift_day_flag;
            public string shift_start;
        }

        private struct m_work_time
        {
            public string work_date;
            public int work_days;
            public int work_month;
            public int last_shift;
            public int work_shift;
            public int work_week;
            public int work_year;
        }

        //Period Type
        public enum ePeriodType
        {
            PERIOD = 0,
            DATE = 1,
            COMBO = 2
        }

        //Period List
        public enum ePeriod
        {
            PERIOD_PERIOD = 0,
            PERIOD_DATE = 1,
            PERIOD_WEEK = 2,
            PERIOD_CURRENT_DAY = 3,
            PERIOD_PREVIOUS_DAY = 4,
            PERIOD_CURRENT_WEEK = 5,
            PERIOD_PREVIOUS_WEEK = 6,
            PERIOD_CURRENT_MONTH = 7,
            PERIOD_PREVIOUS_MONTH = 8
        }

        public udcPeriod()
        {
            InitializeComponent();
            Init();
        }

        #region "Control Events"

        private System.EventHandler SelectedItemChangedEvent;
        public event System.EventHandler SelectedItemChanged
        {
            add
            {
                SelectedItemChangedEvent = (System.EventHandler)System.Delegate.Combine(SelectedItemChangedEvent, value);
            }
            remove
            {
                SelectedItemChangedEvent = (System.EventHandler)System.Delegate.Remove(SelectedItemChangedEvent, value);
            }
        }

        private void cboPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_PERIOD)
            {
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;

                dtpFromYear.Visible = false;
                numFromWeek.Visible = false;
            }
            else if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_DATE)
            {
                dtpFromDate.Visible = true;
                dtpToDate.Visible = false;
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = false;

                dtpFromYear.Visible = false;
                numFromWeek.Visible = false;
            }
            else if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_WEEK)
            {              
                dtpToDate.Visible = false;
                dtpFromDate.Visible = false;
                
                dtpFromYear.Visible = true;
                numFromWeek.Visible = true;

                WorkTime_tmp = Get_Current_Work_Time(DateTime.Now.ToString("yyyyMMddHHmmss"), 0);
                dtpFromYear.Text = CmnFunction.ChangeDateTimeString(WorkTime_tmp.work_date, "yyyyMMdd", "yyyy-MM-dd");
                numFromWeek.Value = WorkTime_tmp.work_week;
            }
            else if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_CURRENT_DAY)
            {
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;

                dtpFromYear.Visible = false;
                numFromWeek.Visible = false;

                dtpFromDate.Value = DateTime.Now;
                dtpToDate.Value = DateTime.Now;
            }
            else if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_PREVIOUS_DAY)
            {
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;

                dtpFromYear.Visible = false;
                numFromWeek.Visible = false;

                dtpFromDate.Value = DateTime.Now.AddDays(-1);
                dtpToDate.Value = DateTime.Now.AddDays(-1);
            }
            else if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_CURRENT_WEEK)
            {
                int iCurrentDayOfWeek;

                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;

                dtpFromYear.Visible = false;
                numFromWeek.Visible = false;

                iCurrentDayOfWeek = (int)DateTime.Now.DayOfWeek;
                dtpFromDate.Value = DateTime.Now.AddDays(-iCurrentDayOfWeek + 1);
                dtpToDate.Value = DateTime.Now.AddDays(-iCurrentDayOfWeek + 1 + 6);
            }
            else if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_PREVIOUS_WEEK)
            {
                int iCurrentDayOfWeek;

                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;

                dtpFromYear.Visible = false;
                numFromWeek.Visible = false;

                iCurrentDayOfWeek = (int)DateTime.Now.DayOfWeek;
                dtpFromDate.Value = DateTime.Now.AddDays(-iCurrentDayOfWeek + 1 - 6 - 1);
                dtpToDate.Value = DateTime.Now.AddDays(-iCurrentDayOfWeek + 1 + 6 - 6 - 1);
            }
            else if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_CURRENT_MONTH)
            {
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;

                dtpFromYear.Visible = false;
                numFromWeek.Visible = false;

                string sCurrentDate = DateTime.Now.ToString("yyyyMMdd");
                sCurrentDate = sCurrentDate.Substring(0, 6) + "01";
                DateTime dtTemp;
                if (CmnFunction.StringToDateTime(sCurrentDate, "yyyyMMdd", out dtTemp))
                {
                    dtpFromDate.Text = dtTemp.ToString();
                }

                sCurrentDate = DateTime.Now.AddMonths(1).ToString("yyyyMMdd");
                sCurrentDate = sCurrentDate.Substring(0, 6) + "01";
                if (CmnFunction.StringToDateTime(sCurrentDate, "yyyyMMdd", out dtTemp))
                {
                    dtpToDate.Text = dtTemp.AddDays(-1).ToString();
                }
            }
            else if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_PREVIOUS_MONTH)
            {
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;

                dtpFromYear.Visible = false;
                numFromWeek.Visible = false;

                string sCurrentDate = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                sCurrentDate = sCurrentDate.Substring(0, 6) + "01";
                DateTime dtTemp;
                if (CmnFunction.StringToDateTime(sCurrentDate, "yyyyMMdd", out dtTemp))
                {
                    dtpFromDate.Text = dtTemp.ToString();
                }

                sCurrentDate = DateTime.Now.ToString("yyyyMMdd");
                sCurrentDate = sCurrentDate.Substring(0, 6) + "01";
                if (CmnFunction.StringToDateTime(sCurrentDate, "yyyyMMdd", out dtTemp))
                {
                    dtpToDate.Text = dtTemp.AddDays(-1).ToString();
                }
            }

            if (SelectedItemChangedEvent != null)
                SelectedItemChangedEvent(this, e);
        }

        #endregion

        #region "Properties"

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ComboBox GetPeriodCombo
        {
            get
            {
                return cboPeriod;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public udcPeriod.ePeriodType PeriodType
        {
            get
            {
                return m_cond_period_type;
            }
            set
            {
                m_cond_period_type = value;
                ChangePeriodCondtion();
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool CalendarBase
        {
            get
            {
                return m_cond_b_calendar_base;
            }
            set
            {
                m_cond_b_calendar_base = value;
            }
        }

        public string Factory
        {
            get
            {
                return m_cond_factory;
            }
            set
            {
                m_cond_factory = value;
            }
        }

        public char Step
        {
            get
            {
                return m_cond_c_step;
            }
            set
            {
                m_cond_c_step = value;
            }
        }

        #endregion

        public void Init()
        {
            cboPeriod.SelectedIndex = 0;
        }

        private void udcPeriod_FontChanged(object sender, EventArgs e)
        {
            cboPeriod.Font = this.Font;
            dtpFromDate.Font = this.Font;
            dtpToDate.Font = this.Font;
        }

        private void ChangePeriodCondtion()
        {
            switch (m_cond_period_type)
            {
                case udcPeriod.ePeriodType.PERIOD:
                    lblPeriod.Visible = true;
                    lblPeriod.Text = "Period";
                    cboPeriod.SelectedIndex = 0;
                    cboPeriod.Visible = false;
                    cboPeriod.Enabled = false;
                    break;
                case udcPeriod.ePeriodType.DATE:
                    lblPeriod.Visible = true;
                    lblPeriod.Text = "Date";
                    cboPeriod.SelectedIndex = 1;
                    cboPeriod.Visible = false;
                    cboPeriod.Enabled = false;
                    break;
                case udcPeriod.ePeriodType.COMBO:
                    lblPeriod.Visible = false;
                    cboPeriod.SelectedIndex = 0;
                    cboPeriod.Visible = true;
                    cboPeriod.Enabled = true;
                    break;
            }
        }

        public string GetFromDate()
        {
            string sShift1TIme = "";

            if (CalendarBase == false) sShift1TIme = "000000";
            if (CalendarBase == true) sShift1TIme = GetShift1Time();

            if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_WEEK)
            {
                SetDate(dtpFromYear.Value.Year, System.Convert.ToInt32(numFromWeek.Value));
            }

            return dtpFromDate.Value.ToString("yyyyMMdd") + sShift1TIme;
        }

        public string GetToDate()
        {
            string sShift1TIme = "";

            if (CalendarBase == false) sShift1TIme = "000000";
            if (CalendarBase == true) sShift1TIme = GetShift1Time();

            if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_DATE)
            {
                dtpToDate.Value = dtpFromDate.Value;
            }
            else if (cboPeriod.SelectedIndex == (int)udcPeriod.ePeriod.PERIOD_WEEK)
            {
                SetDate(dtpFromYear.Value.Year, System.Convert.ToInt32(numFromWeek.Value));
            }

            return dtpToDate.Value.ToString("yyyyMMdd") + sShift1TIme;
        }

        private void SetDate(int Year, int Week)
        {
            DataTable rtDataTable = new DataTable();
          
            string QueryCond = null;
            int i;

            try
            {
                CmnFunction.oComm.SetUrl();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, dtpFromYear.Value.Year.ToString());
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, numFromWeek.Value.ToString());

                rtDataTable = CmnFunction.oComm.SelectData("MWIPCALDEF", "2", QueryCond);

                if (rtDataTable.Rows.Count == 0)
                {
                    QueryCond = null;
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, "SYSTEM");
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, dtpFromYear.Value.Year.ToString());
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, numFromWeek.Value.ToString());

                    rtDataTable = CmnFunction.oComm.SelectData("MWIPCALDEF", "2", QueryCond);
                }

                if (rtDataTable.Rows.Count == 0)
                {
                    //default

                }
                if (rtDataTable.Rows.Count > 0)
                {
                    i = 0;

                    DateTime dtTemp;
                    if (CmnFunction.StringToDateTime(rtDataTable.Rows[i]["FROM_DATE"].ToString(), "yyyyMMdd", out dtTemp))
                    {
                        dtpFromDate.Text = dtTemp.ToString();
                    }

                    if (CmnFunction.StringToDateTime(rtDataTable.Rows[i]["TO_DATE"].ToString(), "yyyyMMdd", out dtTemp))
                    {
                        dtpToDate.Text = dtTemp.ToString();
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return;
            }
        }

        private string GetShift1Time()
        {
            DataTable rtDataTable = new DataTable();
            string QueryCond = null;
            string sShift1TIme = "";

            try
            {
                CmnFunction.oComm.SetUrl();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);

                rtDataTable = CmnFunction.oComm.SelectData("MWIPFACDEF", "1", QueryCond);

                if (rtDataTable.Rows.Count == 0)
                {
                    QueryCond = null;
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, "SYSTEM");

                    rtDataTable = CmnFunction.oComm.SelectData("MWIPFACDEF", "1", QueryCond);
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    sShift1TIme = rtDataTable.Rows[0]["SHIFT_1_START"].ToString() + "00";
                }
                else
                {
                    sShift1TIme = "000000";
                }

                rtDataTable.Dispose();

                return sShift1TIme;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return "000000";
            }
        }

        private m_factory_shift Get_Factory_Shift()
        {
            m_factory_shift FactoryShift_tmp = new m_factory_shift();

            DataTable rtDataTable = new DataTable();
            string QueryCond = null;

            try
            {
                CmnFunction.oComm.SetUrl();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);

                rtDataTable = CmnFunction.oComm.SelectData("MWIPFACDEF", "1", QueryCond);

                if (rtDataTable.Rows.Count == 0)
                {
                    QueryCond = null;
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, "SYSTEM");

                    rtDataTable = CmnFunction.oComm.SelectData("MWIPFACDEF", "1", QueryCond);
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    int i = 0;

                    FactoryShift_tmp.shift = new m_factory_shift_shift[4];
                    FactoryShift_tmp.shift[0] = new m_factory_shift_shift();
                    FactoryShift_tmp.shift[1] = new m_factory_shift_shift();
                    FactoryShift_tmp.shift[2] = new m_factory_shift_shift();
                    FactoryShift_tmp.shift[3] = new m_factory_shift_shift();

                    FactoryShift_tmp.shift[0].shift_day_flag = rtDataTable.Rows[i]["SHIFT_1_DAY_FLAG"].ToString();
                    FactoryShift_tmp.shift[0].shift_start = rtDataTable.Rows[i]["SHIFT_1_START"].ToString();

                    FactoryShift_tmp.shift[1].shift_day_flag = rtDataTable.Rows[i]["SHIFT_2_DAY_FLAG"].ToString();
                    FactoryShift_tmp.shift[1].shift_start = rtDataTable.Rows[i]["SHIFT_2_START"].ToString();

                    FactoryShift_tmp.shift[2].shift_day_flag = rtDataTable.Rows[i]["SHIFT_3_DAY_FLAG"].ToString();
                    FactoryShift_tmp.shift[2].shift_start = rtDataTable.Rows[i]["SHIFT_3_START"].ToString();

                    FactoryShift_tmp.shift[3].shift_day_flag = rtDataTable.Rows[i]["SHIFT_4_DAY_FLAG"].ToString();
                    FactoryShift_tmp.shift[3].shift_start = rtDataTable.Rows[i]["SHIFT_4_START"].ToString();
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }

                return FactoryShift_tmp;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return FactoryShift_tmp;
            }
        }

        private m_work_time Get_Current_Work_Time(string strDateTime, int iDay)
        {
            return Get_Current_Work_Time(strDateTime, iDay, 0);
        }
        private m_work_time Get_Current_Work_Time(string strDateTime, int iDay, int iMonth)
        {
            DataTable rtDataTable = new DataTable();
            m_work_time WorkTime_tmp = new m_work_time();
            DateTime CurrentDateTime;

            string QueryCond = null;
            int i;

            try
            {
                Factory_Shift = Get_Factory_Shift();

                for (i = 0; i < 4; i++)
                {
                    if (Factory_Shift.shift[i].shift_day_flag == Factory_Shift.shift[i + 1].shift_day_flag)
                    {
                        if (System.Convert.ToInt32(strDateTime.Substring(8, 4)) >= System.Convert.ToInt32(Factory_Shift.shift[i].shift_start) && System.Convert.ToInt32(strDateTime.Substring(8, 4)) < System.Convert.ToInt32(Factory_Shift.shift[i + 1].shift_start))
                        {
                            WorkTime_tmp.work_shift = i + 1;
                            break;

                        }
                    }
                    else if (Factory_Shift.shift[i].shift_day_flag != Factory_Shift.shift[i + 1].shift_day_flag && Factory_Shift.shift[i + 1].shift_day_flag != " ")
                    {
                        if (System.Convert.ToInt32(strDateTime.Substring(8, 4)) >= System.Convert.ToInt32(Factory_Shift.shift[i].shift_start) || System.Convert.ToInt32(strDateTime.Substring(8, 4)) < System.Convert.ToInt32(Factory_Shift.shift[i + 1].shift_start))
                        {
                            WorkTime_tmp.work_shift = i + 1;
                            break;
                        }
                    }
                    else if (Factory_Shift.shift[i + 1].shift_day_flag == " ")
                    {
                        WorkTime_tmp.work_shift = i + 1;
                        break;
                    }
                }

                if (Factory_Shift.shift[0].shift_day_flag == "T")
                {
                    WorkTime_tmp.last_shift = 1;
                }
                else
                {
                    WorkTime_tmp.last_shift = 0;
                }
                if (Factory_Shift.shift[1].shift_day_flag == "T")
                {
                    WorkTime_tmp.last_shift = 2;
                }
                if (Factory_Shift.shift[2].shift_day_flag == "T")
                {
                    WorkTime_tmp.last_shift = 3;
                }
                if (Factory_Shift.shift[3].shift_day_flag == "T")
                {
                    WorkTime_tmp.last_shift = 4;
                }

                CurrentDateTime = new DateTime(System.Convert.ToInt32(strDateTime.Substring(0, 4)),
                                               System.Convert.ToInt32(strDateTime.Substring(4, 2)),
                                               System.Convert.ToInt32(strDateTime.Substring(6, 2)),
                                               System.Convert.ToInt32(strDateTime.Substring(8, 2)),
                                               System.Convert.ToInt32(strDateTime.Substring(10, 2)),
                                               System.Convert.ToInt32(strDateTime.Substring(12, 2)));

                if (iDay != 0)
                {
                    CurrentDateTime = CurrentDateTime.AddDays(-iDay);
                }

                if (iMonth != 0)
                {
                    CurrentDateTime = CurrentDateTime.AddMonths(-iMonth);
                }

                if (System.Convert.ToInt32(strDateTime.Substring(8, 4)) < System.Convert.ToInt32(Factory_Shift.shift[0].shift_start))
                {
                    CurrentDateTime = CurrentDateTime.AddDays(-1);
                }

                CmnFunction.oComm.SetUrl();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Year.ToString());
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Month.ToString());
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Day.ToString());

                rtDataTable = CmnFunction.oComm.SelectData("MWIPCALDEF", "1", QueryCond);

                if (rtDataTable.Rows.Count == 0)
                {
                    QueryCond = null;
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, "SYSTEM");
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Year.ToString());
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Month.ToString());
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Day.ToString());

                    rtDataTable = CmnFunction.oComm.SelectData("MWIPCALDEF", "1", QueryCond);
                }

                if (rtDataTable.Rows.Count == 0)
                {
                    //default

                }
                if (rtDataTable.Rows.Count > 0)
                {
                    i = 0;

                    WorkTime_tmp.work_year = System.Convert.ToInt32(rtDataTable.Rows[i]["PLAN_YEAR"]);
                    WorkTime_tmp.work_month = System.Convert.ToInt32(rtDataTable.Rows[i]["PLAN_MONTH"]);
                    WorkTime_tmp.work_week = System.Convert.ToInt32(rtDataTable.Rows[i]["PLAN_WEEK"]);
                    WorkTime_tmp.work_days = System.Convert.ToInt32(rtDataTable.Rows[i]["JULIAN_DAY"]);

                    int iTime;
                    iTime = CurrentDateTime.Hour * 100 + CurrentDateTime.Minute;

                    if (rtDataTable.Rows[i]["PREV_DAY_FG"].ToString() == "Y")
                    {
                        if (System.Convert.ToInt32(rtDataTable.Rows[i]["START_TIME"]) <= iTime)
                        {
                            CurrentDateTime.AddDays(-1);
                        }
                    }
                    else
                    {
                        if (System.Convert.ToInt32(rtDataTable.Rows[i]["START_TIME"]) > iTime)
                        {
                            CurrentDateTime.AddDays(-1);
                        }
                    }

                    WorkTime_tmp.work_date = CurrentDateTime.ToString("yyyyMMdd");
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }

                return WorkTime_tmp;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return WorkTime_tmp;
            }
        }
    }
}
