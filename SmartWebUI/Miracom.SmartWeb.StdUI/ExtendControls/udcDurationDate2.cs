//-----------------------------------------------------------------------------
//
//   System      : MES Report
//   File Name   : 
//   Description : Client Common function Module 
//
//   MES Version : 4.x.x.x
//
//   History
//       - **** Do Not Modify in Site!!! ****
//       - 2008-10-01 : Created by John Seo
//
//
//   Copyright(C) 1998-2005 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Miracom.SmartWeb.UI.Controls
{
    public enum DateType2 { Duration, OneDate };

    public class udcDurationDate2 : System.Windows.Forms.Panel
    {
        private System.Windows.Forms.ComboBox cbo_day_select;
        private System.Windows.Forms.Panel pnl_date;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker dtp_todate;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker dtp_fromdate;
        private System.Windows.Forms.Panel pnl_week;
        private System.Windows.Forms.Panel pnl_month;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker dtp_toyear;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker dtp_year;
        private System.Windows.Forms.ComboBox cbo_weeks;
        private System.Windows.Forms.ComboBox cbo_toweeks;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker dtp_month;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker dtp_tomonth;
        private System.ComponentModel.IContainer components;
        private DateType2 _type;
        private System.Windows.Forms.ComboBox dtp_from_time_select;
        private System.Windows.Forms.ComboBox dtp_to_time_select;

        //private bool _isFirstYN = true;
        private int _restrictedDayCount = 60; //MAX조회 가능 주

        public udcDurationDate2()
        {
            // 이 호출은 Windows.Forms Form 디자이너에 필요합니다.
            InitializeComponent();

            // TODO: InitializeComponent를 호출한 다음 초기화 작업을 추가합니다.
            DurationInit();

            //날짜선택에 Data Binding
            AddItem();

        }

        public int RestrictedDayCount
        {
            get
            {
                return _restrictedDayCount;
            }
            set
            {
                _restrictedDayCount = value;
            }
        }

        [DefaultValue(DateType2.Duration)]
        public DateType2 Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                ControlInit();
            }
        }

        // SQL문 생성.
        public string getRepeatQuery(string sFront, string sBack, string sAlias)
        {
            System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();
            int iCount = 0;

            iCount = this.SubtractBetweenFromToDate;

            for (int i = 0; i <= iCount; i++)
            {
                strSqlString.AppendFormat(", " + "{0}{1} " + "{2}" + " {3}{1}" + "\n", sFront, i.ToString(), sBack, sAlias);
            }

            return strSqlString.ToString();

        }

        // SQL문 생성.
        // strSql의 char '?' 를 반복 숫자로 바꾼다.
        public string getMultyRepeatQuery(string sFront, string sAlias)
        {
            System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();
            int iCount = 0;

            iCount = this.SubtractBetweenFromToDate;

            string sBack = string.Empty;
            for (int i = 0; i <= iCount; i++)
            {
                sBack = sFront.Replace("?", i.ToString());
                strSqlString.AppendFormat(", " + "{0} {2}{1}" + "\n", sBack, i.ToString(), sAlias);
            }

            return strSqlString.ToString();

        }

        public string getRepeatQuery(string first, string second, string third, string fourth)
        {
            System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();
            int iCount = 0;

            iCount = this.SubtractBetweenFromToDate;

            for (int i = 0; i <= iCount; i++)
            {
                strSqlString.AppendFormat(", " + "{0}{4} " + "{1}{4}" + " {2}{4}{3}{4}" + "\n", first, second, third, fourth, i.ToString());
            }

            return strSqlString.ToString();

        }

        // SQL문 생성.
        public string getDecodeQuery(string sFront, string sBack, string sAlias)
        {
            System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();
            string sHeader = null;

            if (this.DaySelector.SelectedValue.ToString() == "DAY")
            {
                for (int i = 0; i <= this.SubtractBetweenFromToDate; i++)
                {
                    // , DECODE(CUTOFF_DATE  , '20080801',  QTY,0) V0
                    //   ------------------              ------ -
                    sHeader = this.FromDate.Value.AddDays(i).ToString("yyyyMMdd");
                    strSqlString.AppendFormat(", " + "{0}, '{1}', {2} {3}{4}" + "\n", sFront, sHeader, sBack, sAlias, i.ToString());
                }
            }
            else if (this.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(this.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = this.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(this.ToWeek.SelectedValue.ToString().Substring(4, 2));
                int j = 0;

                //년도가 같을 경우
                if (this.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    this.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    // 12,13,14,15,16,17...
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        if (i < 10)
                            sHeader = this.FromWeek.SelectedValue.ToString().Substring(0, 4) + "0" + i.ToString();
                        else
                            sHeader = this.FromWeek.SelectedValue.ToString().Substring(0, 4) + i.ToString();

                        //strSqlString.AppendFormat(" , DECODE(CUTOFF_WEEK, '{0}',QTY,0) V{1}" + "\n", sHeader, j.ToString());
                        strSqlString.AppendFormat(", " + "{0}, '{1}', " + "{2}" + " {3}" + "{4}" + "\n", sFront, sHeader, sBack, sAlias, j.ToString());
                        j++;
                    }
                }
                else //년도가 다를 경우
                {
                    //50,51,52,53,1,2,3,4,5
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        if (i < 10)
                            sHeader = this.FromWeek.SelectedValue.ToString().Substring(0, 4) + "0" + i.ToString();
                        else
                            sHeader = this.FromWeek.SelectedValue.ToString().Substring(0, 4) + i.ToString();

                        //strSqlString.AppendFormat(" , DECODE(CUTOFF_WEEK, '{0}',QTY,0) V{1}" + "\n", sHeader, j.ToString());
                        strSqlString.AppendFormat(", " + "{0}, '{1}', " + "{2}" + " {3}" + "{4}" + "\n", sFront, sHeader, sBack, sAlias, j.ToString());
                        j++;
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        if (i < 10)
                            sHeader = this.ToWeek.SelectedValue.ToString().Substring(0, 4) + "0" + i.ToString();
                        else
                            sHeader = this.ToWeek.SelectedValue.ToString().Substring(0, 4) + i.ToString();

                        //strSqlString.AppendFormat(" , DECODE(CUTOFF_WEEK, '{0}',QTY,0) V{1}" + "\n", sHeader, j.ToString());
                        strSqlString.AppendFormat(", " + "{0}, '{1}', " + "{2}" + " {3}" + "{4}" + "\n", sFront, sHeader, sBack, sAlias, j.ToString());
                        j++;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= this.SubtractBetweenFromToDate; i++)
                {
                    // , DECODE(CUTOFF_DATE, '20080801',QTY,0) V0
                    //   -------------------            ------ -
                    sHeader = this.FromYearMonth.Value.AddMonths(i).ToString("yyyyMM");
                    strSqlString.AppendFormat(", " + "{0}, '{1}', " + "{2}" + " {3}" + "{4}" + "\n", sFront, sHeader, sBack, sAlias, i.ToString());
                }
            }

            return strSqlString.ToString();

        }

        // FromDay 가져오기 (2009.07.16 임종우)
        public string HmFromDay
        {
            get
            {
                DateTime fromDay = new DateTime();
                string sFrom = this.FromDate.Text;
                string stime = this.FromTimeSelector.SelectedItem.ToString().Substring(0, 2);
                int itime = Convert.ToInt32(stime);

                if (itime >= 22)
                {
                    fromDay = DateTime.Parse(sFrom).AddDays(1);
                    sFrom = fromDay.ToString("yyyyMMdd");
                }
                else
                {
                    sFrom = this.FromDate.Text.Replace("-", "");
                }

                return sFrom;
            }
        }

        // ToDay 가져오기 (2009.07.16 임종우)
        public string HmToDay
        {
            get
            {
                DateTime toDay = new DateTime();
                string sTo = this.ToDate.Text;
                string stime = this.ToTimeSelector.SelectedItem.ToString().Substring(0, 2);
                int itime = Convert.ToInt32(stime);

                if (itime >= 22)
                {
                    toDay = DateTime.Parse(sTo).AddDays(1);
                    sTo = toDay.ToString("yyyyMMdd");
                }
                else
                {
                    sTo = this.ToDate.Text.Replace("-", "");
                }

                return sTo;
            }
        }

        // FromWeek 가져오기 (2009.07.16 임종우)
        public string HmFromWeek
        {
            get
            {
                DateTime fromDay = new DateTime();
                string sFromWeek = string.Empty;
                string sFrom = this.FromDate.Text;
                string stime = this.FromTimeSelector.SelectedItem.ToString().Substring(0, 2);
                int itime = Convert.ToInt32(stime);

                if (itime >= 22)
                {
                    fromDay = DateTime.Parse(sFrom).AddDays(1);
                    sFrom = fromDay.ToString("yyyyMMdd");
                }
                else
                {
                    sFrom = this.FromDate.Text.Replace("-", "");
                }

                string strSqlString = "(SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID='HM' AND SYS_DATE='" + sFrom + "')";
                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString);

                sFromWeek = dt.Rows[0][0].ToString();

                return sFromWeek;
            }

        }

        // ToWeek 가져오기 (2009.07.16 임종우)
        public string HmToWeek
        {
            get
            {
                DateTime toDay = new DateTime();
                string sToWeek = string.Empty;
                string sTo = this.ToDate.Text;
                string stime = this.ToTimeSelector.SelectedItem.ToString().Substring(0, 2);
                int itime = Convert.ToInt32(stime);

                if (itime >= 22)
                {
                    toDay = DateTime.Parse(sTo).AddDays(1);
                    sTo = toDay.ToString("yyyyMMdd");
                }
                else
                {
                    sTo = this.ToDate.Text.Replace("-", "");
                }

                string strSqlString = "(SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID='HM' AND SYS_DATE='" + sTo + "')";
                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString);

                sToWeek = dt.Rows[0][0].ToString();

                return sToWeek;
            }
        }

        // FromMonth 가져오기 (2009.07.16 임종우)
        public string HmFromMonth
        {
            get
            {
                DateTime fromDay = new DateTime();
                string sFromMonth = string.Empty;
                string sFrom = this.FromDate.Text;
                string stime = this.FromTimeSelector.SelectedItem.ToString().Substring(0, 2);
                int itime = Convert.ToInt32(stime);

                if (itime >= 22)
                {
                    fromDay = DateTime.Parse(sFrom).AddDays(1);
                    sFrom = fromDay.ToString("yyyyMMdd");
                }
                else
                {
                    sFrom = this.FromDate.Text.Replace("-", "");
                }

                string strSqlString = "(SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_MONTH,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID='HM' AND SYS_DATE='" + sFrom + "')";
                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString);

                sFromMonth = dt.Rows[0][0].ToString();

                return sFromMonth;
            }
        }

        // ToMonth 가져오기 (2009.07.16 임종우)
        public string HmToMonth
        {
            get
            {
                DateTime toDay = new DateTime();
                string sToMonth = string.Empty;
                string sTo = this.ToDate.Text;
                string stime = this.ToTimeSelector.SelectedItem.ToString().Substring(0, 2);
                int itime = Convert.ToInt32(stime);

                if (itime >= 22)
                {
                    toDay = DateTime.Parse(sTo).AddDays(1);
                    sTo = toDay.ToString("yyyyMMdd");
                }
                else
                {
                    sTo = this.ToDate.Text.Replace("-", "");
                }

                string strSqlString = "(SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_MONTH,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID='HM' AND SYS_DATE='" + sTo + "')";
                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString);

                sToMonth = dt.Rows[0][0].ToString();

                return sToMonth;
            }
        }

        // 내부컨트롤러 반환
        public String Start_Tran_Time
        {
            get
            {
                string sFrom = this.FromDate.Text.Replace("-", "");                
                string tFrom = this.FromTimeSelector.SelectedItem.ToString().Substring(0,2);                
                string Tran_time = sFrom + tFrom + "0000" ;
                
                return Tran_time;
            }
        }
        public String End_Tran_Time
        {
            get
            {
                DateTime ToDate = new DateTime();
                string Tran_time = String.Empty;
                string sTo = this.ToDate.Text;


                //24시 선택시 무조건 선택일의 235959로 Return함(2009.06.08 임종우수정)
                if (ToTimeSelector.SelectedItem.ToString().Substring(0, 2).Equals("24"))
                {
                    ToDate = DateTime.Parse(sTo);
                    Tran_time = ToDate.ToString("yyyyMMdd") + "235959";
                }
                else
                {
                    sTo = sTo + " " + ToTimeSelector.SelectedItem.ToString().Substring(0, 2) + ":00";


                    ToDate = DateTime.Parse(sTo).AddHours(-1);
                    Tran_time = ToDate.ToString("yyyyMMddHH") + "5959";
                }

                return Tran_time;
            }
        }

        public ComboBox FromTimeSelector
        {
            get
            {
                return dtp_from_time_select;
            }
        }
        public ComboBox ToTimeSelector
        {
            get
            {
                return dtp_to_time_select;
            }
        }


        public ComboBox DaySelector
        {
            get
            {
                return cbo_day_select;
            }
        }

        public udcDateTimePicker FromDate
        {
            get
            {
                return dtp_fromdate;
            }
        }

        public udcDateTimePicker ToDate
        {
            get
            {
                return dtp_todate;
            }
        }

        public ComboBox FromWeek
        {
            get
            {
                return cbo_weeks;
            }
        }

        public ComboBox ToWeek
        {
            get
            {
                return cbo_toweeks;
            }
        }

        public udcDateTimePicker FromYear
        {
            get
            {
                return dtp_year;
            }
        }

        public udcDateTimePicker ToYear
        {
            get
            {
                return dtp_toyear;
            }
        }

        public udcDateTimePicker FromYearMonth
        {
            get
            {
                return dtp_month;
            }
        }

        public udcDateTimePicker ToYearMonth
        {
            get
            {
                return dtp_tomonth;
            }
        }


        // 실제 사용시에는 +1 해서 사용해야함..
        public int SubtractBetweenFromToDate
        {
            get
            {
                if (this.cbo_day_select.SelectedValue == null)
                    throw new Exception(RptMessages.GetMessage("STD086", GlobalVariable.gcLanguage));

                if (this.cbo_day_select.SelectedValue.ToString() == "DAY")
                {
                    DateTime dFrom = dtp_fromdate.Value;
                    DateTime dTo = dtp_todate.Value;
                    TimeSpan tCount = dTo.Subtract(dFrom);
                    return tCount.Days;
                }
                else if (this.cbo_day_select.SelectedValue.ToString() == "WEEK")
                {
                    // 1년이상 차이나면 에러임.
                    // 항상 To 가 From 보다 크다고 가정 아닌경우 이전 상태에서 에러를 리턴해야함
                    if ((dtp_toyear.Value.Year - dtp_year.Value.Year) > 1)
                        throw new Exception(RptMessages.GetMessage("STD087", GlobalVariable.gcLanguage));

                    if ((dtp_toyear.Value.Year - dtp_year.Value.Year) == 0)  //동일 년도
                    {
                        return (Convert.ToInt32(cbo_toweeks.SelectedValue.ToString()) - Convert.ToInt32(cbo_weeks.SelectedValue.ToString()));
                    }
                    else //년도가 다를 경우
                    {
                        int thisWeek = Convert.ToInt32(this.FromWeek.SelectedValue.ToString().Substring(4, 2));
                        int thisMax = this.FromWeek.Items.Count;
                        int maxWeek = Convert.ToInt32(this.ToWeek.SelectedValue.ToString().Substring(4, 2));

                        return thisMax - thisWeek + maxWeek;
                    }

                }
                else if (this.cbo_day_select.SelectedValue.ToString() == "MONTH")
                {
                    // 항상 To 가 From 보다 크다고 가정 아닌경우 이전 상태에서 에러를 리턴해야함
                    return (dtp_tomonth.Value.Year - dtp_month.Value.Year) * 12 + (dtp_tomonth.Value.Month - dtp_month.Value.Month);
                }
                else
                {
                    throw new Exception(RptMessages.GetMessage("STD088", GlobalVariable.gcLanguage));
                }

            }
        }

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드
        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbo_day_select = new System.Windows.Forms.ComboBox();
            this.pnl_date = new System.Windows.Forms.Panel();
            this.dtp_todate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.dtp_from_time_select = new System.Windows.Forms.ComboBox();
            this.dtp_to_time_select = new System.Windows.Forms.ComboBox();
            this.dtp_fromdate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.pnl_week = new System.Windows.Forms.Panel();
            this.cbo_toweeks = new System.Windows.Forms.ComboBox();
            this.cbo_weeks = new System.Windows.Forms.ComboBox();
            this.dtp_year = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.dtp_toyear = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.pnl_month = new System.Windows.Forms.Panel();
            this.dtp_tomonth = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.dtp_month = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.pnl_date.SuspendLayout();
            this.pnl_week.SuspendLayout();
            this.pnl_month.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbo_day_select
            // 
            this.cbo_day_select.Font = new System.Drawing.Font("굴림체", 8F);
            this.cbo_day_select.Location = new System.Drawing.Point(0, 0);
            this.cbo_day_select.Name = "cbo_day_select";
            this.cbo_day_select.Size = new System.Drawing.Size(39, 19);
            this.cbo_day_select.TabIndex = 75;
            this.cbo_day_select.SelectedIndexChanged += new System.EventHandler(this.cbo_day_select_SelectedIndexChanged);
            // 
            // pnl_date
            // 
            this.pnl_date.BackColor = System.Drawing.Color.Transparent;
            this.pnl_date.Controls.Add(this.dtp_todate);
            this.pnl_date.Controls.Add(this.dtp_from_time_select);
            this.pnl_date.Controls.Add(this.dtp_fromdate);
            this.pnl_date.Controls.Add(this.dtp_to_time_select);
            this.pnl_date.Location = new System.Drawing.Point(46, 0);
            this.pnl_date.Name = "pnl_date";
            this.pnl_date.Size = new System.Drawing.Size(430, 21);
            this.pnl_date.TabIndex = 84;
            // 
            // 
            // dtp_fromdate
            // 
            this.dtp_fromdate.CustomFormat = "yyyy-MM-dd";
            this.dtp_fromdate.Font = new System.Drawing.Font("굴림체", 8F);
            this.dtp_fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_fromdate.Location = new System.Drawing.Point(0, 0);
            this.dtp_fromdate.Name = "dtp_fromdate";
            this.dtp_fromdate.Size = new System.Drawing.Size(88, 20);
            this.dtp_fromdate.TabIndex = 78;
            this.dtp_fromdate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.FromDate;
            this.dtp_fromdate.Value = new System.DateTime(2008, 2, 20, 0, 0, 0, 0);
            // 
            // dtp_todate
            // 
            this.dtp_todate.CustomFormat = "yyyy-MM-dd";
            this.dtp_todate.Font = new System.Drawing.Font("굴림체", 8F);
            this.dtp_todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_todate.Location = new System.Drawing.Point(150, 0);
            this.dtp_todate.Name = "dtp_todate";
            this.dtp_todate.Size = new System.Drawing.Size(88, 20);
            this.dtp_todate.TabIndex = 79;
            this.dtp_todate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.ToDate;
            this.dtp_todate.Value = new System.DateTime(2008, 2, 20, 0, 0, 0, 0);
            // 
            // dtp_from_time_select
            // 
            this.dtp_from_time_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dtp_from_time_select.Font = new System.Drawing.Font("굴림체", 8F);
            this.dtp_from_time_select.FormattingEnabled = true;
            this.dtp_from_time_select.Items.AddRange(new object[] {
            "01 시",
            "02 시",
            "03 시",
            "04 시",
            "05 시",
            "06 시",
            "07 시",
            "08 시",
            "09 시",
            "10 시",
            "11 시",
            "12 시",
            "13 시",
            "14 시",
            "15 시",
            "16 시",
            "17 시",
            "18 시",
            "19 시",
            "20 시",
            "21 시",
            "22 시",
            "23 시",
            "24 시"});
            this.dtp_from_time_select.Location = new System.Drawing.Point(90, 0);
            this.dtp_from_time_select.Name = "dtp_from_time_select";
            this.dtp_from_time_select.Size = new System.Drawing.Size(55, 19);
            this.dtp_from_time_select.TabIndex = 0;
            this.dtp_from_time_select.SelectedIndex = 21;
            // 
            // dtp_to_time_select
            // 
            this.dtp_to_time_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dtp_to_time_select.Font = new System.Drawing.Font("굴림체", 8F);
            this.dtp_to_time_select.FormattingEnabled = true;
            this.dtp_to_time_select.Items.AddRange(new object[] {
            "01 시",
            "02 시",
            "03 시",
            "04 시",
            "05 시",
            "06 시",
            "07 시",
            "08 시",
            "09 시",
            "10 시",
            "11 시",
            "12 시",
            "13 시",
            "14 시",
            "15 시",
            "16 시",
            "17 시",
            "18 시",
            "19 시",
            "20 시",
            "21 시",
            "22 시",
            "23 시",
            "24 시"});
            this.dtp_to_time_select.Location = new System.Drawing.Point(240, 0);
            this.dtp_to_time_select.Name = "cbo_time_select";
            this.dtp_to_time_select.Size = new System.Drawing.Size(55, 19);
            this.dtp_to_time_select.TabIndex = 0;
            this.dtp_to_time_select.SelectedIndex = 21;
            // 
            // pnl_week
            // 
            this.pnl_week.BackColor = System.Drawing.Color.Transparent;
            this.pnl_week.Controls.Add(this.cbo_toweeks);
            this.pnl_week.Controls.Add(this.cbo_weeks);
            this.pnl_week.Controls.Add(this.dtp_year);
            this.pnl_week.Controls.Add(this.dtp_toyear);
            this.pnl_week.Location = new System.Drawing.Point(46, 24);
            this.pnl_week.Name = "pnl_week";
            this.pnl_week.Size = new System.Drawing.Size(234, 21);
            this.pnl_week.TabIndex = 85;
            // 
            // cbo_toweeks
            // 
            this.cbo_toweeks.Font = new System.Drawing.Font("굴림체", 8F);
            this.cbo_toweeks.Location = new System.Drawing.Point(180, 0);
            this.cbo_toweeks.Name = "cbo_toweeks";
            this.cbo_toweeks.Size = new System.Drawing.Size(48, 19);
            this.cbo_toweeks.TabIndex = 85;
            // 
            // cbo_weeks
            // 
            this.cbo_weeks.Font = new System.Drawing.Font("굴림체", 8F);
            this.cbo_weeks.Location = new System.Drawing.Point(60, 0);
            this.cbo_weeks.Name = "cbo_weeks";
            this.cbo_weeks.Size = new System.Drawing.Size(48, 19);
            this.cbo_weeks.TabIndex = 84;
            // 
            // dtp_year
            // 
            this.dtp_year.CustomFormat = "yyyy";
            this.dtp_year.Font = new System.Drawing.Font("굴림체", 8F);
            this.dtp_year.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_year.Location = new System.Drawing.Point(0, 0);
            this.dtp_year.Name = "dtp_year";
            this.dtp_year.ShowUpDown = true;
            this.dtp_year.Size = new System.Drawing.Size(56, 20);
            this.dtp_year.TabIndex = 83;
            this.dtp_year.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.YYYY;
            this.dtp_year.Value = new System.DateTime(2008, 2, 20, 0, 0, 0, 0);
            // 
            // dtp_toyear
            // 
            this.dtp_toyear.CustomFormat = "yyyy";
            this.dtp_toyear.Font = new System.Drawing.Font("굴림체", 8F);
            this.dtp_toyear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_toyear.Location = new System.Drawing.Point(120, 0);
            this.dtp_toyear.Name = "dtp_toyear";
            this.dtp_toyear.ShowUpDown = true;
            this.dtp_toyear.Size = new System.Drawing.Size(56, 20);
            this.dtp_toyear.TabIndex = 82;
            this.dtp_toyear.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.YYYY;
            this.dtp_toyear.Value = new System.DateTime(2008, 2, 20, 0, 0, 0, 0);
            // 
            // pnl_month
            // 
            this.pnl_month.BackColor = System.Drawing.Color.Transparent;
            this.pnl_month.Controls.Add(this.dtp_tomonth);
            this.pnl_month.Controls.Add(this.dtp_month);
            this.pnl_month.Location = new System.Drawing.Point(46, 48);
            this.pnl_month.Name = "pnl_month";
            this.pnl_month.Size = new System.Drawing.Size(234, 21);
            this.pnl_month.TabIndex = 86;
            // 
            // dtp_tomonth
            // 
            this.dtp_tomonth.CustomFormat = "yyyy-MM";
            this.dtp_tomonth.Font = new System.Drawing.Font("굴림체", 8F);
            this.dtp_tomonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_tomonth.Location = new System.Drawing.Point(90, 0);
            this.dtp_tomonth.Name = "dtp_tomonth";
            this.dtp_tomonth.ShowUpDown = true;
            this.dtp_tomonth.Size = new System.Drawing.Size(80, 20);
            this.dtp_tomonth.TabIndex = 84;
            this.dtp_tomonth.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.ToSpinedYM;
            this.dtp_tomonth.Value = new System.DateTime(2008, 2, 20, 0, 0, 0, 0);
            // 
            // dtp_month
            // 
            this.dtp_month.CustomFormat = "yyyy-MM";
            this.dtp_month.Font = new System.Drawing.Font("굴림체", 8F);
            this.dtp_month.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_month.Location = new System.Drawing.Point(0, 0);
            this.dtp_month.Name = "dtp_month";
            this.dtp_month.ShowUpDown = true;
            this.dtp_month.Size = new System.Drawing.Size(80, 20);
            this.dtp_month.TabIndex = 79;
            this.dtp_month.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.FromSpinedYM;
            this.dtp_month.Value = new System.DateTime(2008, 2, 20, 0, 0, 0, 0);
            // 
            // udcDurationDate
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnl_month);
            this.Controls.Add(this.pnl_week);
            this.Controls.Add(this.pnl_date);
            this.Controls.Add(this.cbo_day_select);
            this.Size = new System.Drawing.Size(275, 21);
            this.pnl_date.ResumeLayout(false);
            this.pnl_week.ResumeLayout(false);
            this.pnl_month.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region IBaseExtendForm 멤버

        public void AutoBinding()
        {
            if (this._type == DateType2.OneDate)
            {
                dtp_fromdate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                dtp_todate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                dtp_from_time_select.Text = dtp_from_time_select.SelectedText;
                dtp_to_time_select.Text = dtp_to_time_select.SelectedText;

                SetWeekCombo("ALL");

                dtp_month.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM");
                dtp_tomonth.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM");
            }
            else
            {
                dtp_fromdate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                dtp_todate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                dtp_from_time_select.Text = dtp_from_time_select.SelectedText;
                dtp_to_time_select.Text = dtp_to_time_select.SelectedText;

                SetWeekCombo("ALL");

                dtp_month.Text = DateTime.Now.AddDays(-1).AddMonths(-3).ToString("yyyy-MM");
                dtp_tomonth.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM");
            }

        }

        /// <summary>
        /// 시작일과 종료일을 지정하여 Binding
        /// </summary>
        /// <param name="startDate">yyyy-MM-dd 일자</param>
        /// <param name="endDate">yyyy-MM-dd 일자</param>
        public void AutoBinding(String startDate,String endDate)
        {
            if (this._type != DateType2.OneDate)
            {
                dtp_fromdate.Text = startDate;
                dtp_todate.Text = endDate;
                dtp_from_time_select.Text = dtp_from_time_select.SelectedText;
                dtp_to_time_select.Text = dtp_to_time_select.SelectedText;

                SetWeekCombo("ALL");

                dtp_month.Text = DateTime.Parse(startDate).AddMonths(-3).ToString("yyyy-MM");
                //dtp_month.Text = DateTime.Now.AddDays(-1).AddMonths(-3).ToString("yyyy-MM");
                dtp_tomonth.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM");
            }

        }

        #endregion

        private void ControlInit()
        {
            if (this._type == DateType2.OneDate)
            {
                this.dtp_todate.Visible = false;
                this.cbo_toweeks.Visible = false;
                this.dtp_toyear.Visible = false;
                this.dtp_tomonth.Visible = false;
                this.Size = new System.Drawing.Size(155, 21);
            }
            else
            {
                this.dtp_todate.Visible = true;
                this.cbo_toweeks.Visible = true;
                this.dtp_toyear.Visible = true;
                this.dtp_tomonth.Visible = true;
                this.Size = new System.Drawing.Size(275, 21);
            }

        }

        private void DurationInit()
        {
            pnl_date.Visible = true;
            pnl_week.Visible = false;
            pnl_month.Visible = false;

            //cbo_weeks.RefControl = dtp_year;
            //cbo_toweeks.RefControl = dtp_toyear;

            pnl_date.Location = new Point(pnl_date.Location.X, 0);
            pnl_week.Location = new Point(pnl_week.Location.X, 40);
            pnl_month.Location = new Point(pnl_month.Location.X, 40);

            // 날짜 이동시에 이벤트 
            FromDate.ValueChanged += new EventHandler(FromDate_ValueChanged);
            ToDate.ValueChanged += new EventHandler(ToDate_ValueChanged);
            FromYearMonth.ValueChanged += new EventHandler(FromYearMonth_ValueChanged);
            ToYearMonth.ValueChanged += new EventHandler(ToYearMonth_ValueChanged);
            FromYear.ValueChanged += new EventHandler(FromYear_ValueChanged);
            ToYear.ValueChanged += new EventHandler(ToYear_ValueChanged);
            FromWeek.SelectedIndexChanged += new EventHandler(FromWeek_SelectedIndexChanged);
            ToWeek.SelectedIndexChanged += new EventHandler(ToWeek_SelectedIndexChanged);
        }

        private void AddItem()
        {
            //DataTable dt = new DataTable("SEL_TYPE");
            System.Data.DataTable dt = new DataTable("SEL_TYPE");
            DataColumn column;
            DataRow row;

            string DisplayMember = "Text";
            string ValueMember = "Value";

            // Create new DataColumn, set DataType, 
            // ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Text";
            column.ReadOnly = true;
            column.Unique = true;
            // Add the Column to the DataColumnCollection.
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Value";
            column.ReadOnly = true;
            column.Unique = true;
            // Add the Column to the DataColumnCollection.
            dt.Columns.Add(column);

            System.Data.DataSet dataSet = new DataSet();
            // Add the new DataTable to the DataSet.
            dataSet.Tables.Add(dt);

            row = dt.NewRow();
            row[DisplayMember] = "일";
            row[ValueMember] = "DAY";
            dt.Rows.Add(row);

            //row = dt.NewRow();
            //row[DisplayMember] = "주";
            //row[ValueMember] = "WEEK";
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row[DisplayMember] = "월";
            //row[ValueMember] = "MONTH";
            //dt.Rows.Add(row);

            this.cbo_day_select.DisplayMember = DisplayMember;
            this.cbo_day_select.ValueMember = ValueMember;
            this.cbo_day_select.DataSource = dt;
        }

        private DataTable GetWeekInfo(string sYear)
        {
            string sMySql;
            DataTable dt = null;

            sMySql = "";
            sMySql = sMySql + " SELECT UNIQUE PLAN_WEEK V1, (PLAN_YEAR || LPAD(PLAN_WEEK, 2,'0')) V2 ";
            sMySql = sMySql + "   FROM MWIPCALDEF ";
            sMySql = sMySql + "  WHERE CALENDAR_ID IN ('SYSTEM', 'HM', '" + GlobalVariable.gsFactory + "') ";
            sMySql = sMySql + "    AND SYS_YEAR = NVL('" + sYear + "', TO_CHAR(SYSDATE,'yyyy'))  ";
            sMySql = sMySql + "  ORDER BY 1 ";

            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sMySql);

            return dt;

        }

        private string GetCurrWeekInfo()
        {
            string sMySql;
            DataTable dt = null;

            sMySql = "";
            sMySql = sMySql + " SELECT PLAN_WEEK V1 ";
            sMySql = sMySql + "   FROM MWIPCALDEF ";
            sMySql = sMySql + "  WHERE CALENDAR_ID IN ('SYSTEM', 'HM', '" + GlobalVariable.gsFactory + "') ";
            sMySql = sMySql + "    AND SYS_DATE = TO_CHAR(SYSDATE,'yyyymmdd')  ";
            sMySql = sMySql + "    AND ROWNUM = 1  ";

            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sMySql);

            if (dt.Rows.Count == 0)
            {
                dt.Dispose();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage) + "- Week Code");
                return "";
            }
            else
            {
                return dt.Rows[0]["V1"].ToString();
            }
        }

        public void SetWeekCombo(string sGubun)
        {
            DataTable dtFromWeek = null;
            DataTable dtToWeek = null;

            string sCurWeek = "";

            if (sGubun == "FROM")
            {
                this.cbo_weeks.Text = "";
                dtFromWeek = GetWeekInfo(this.FromYear.Text);

                this.cbo_weeks.DisplayMember = "V1";
                this.cbo_weeks.ValueMember = "V2";
                this.cbo_weeks.DataSource = dtFromWeek;
            }
            else if (sGubun == "TO")
            {
                this.cbo_toweeks.Text = "";
                dtToWeek = GetWeekInfo(this.ToYear.Text);

                this.cbo_toweeks.DisplayMember = "V1";
                this.cbo_toweeks.ValueMember = "V2";
                this.cbo_toweeks.DataSource = dtToWeek;
            }
            else if (sGubun == "ALL")
            {
                this.cbo_weeks.Text = "";
                this.cbo_toweeks.Text = "";

                sCurWeek = GetCurrWeekInfo();

                //동일한 DataTable을 Combobox에 Binding시 이벤트가 서로 같이 먹는 현상발생
                //따라서 서로다른 DataTable로 다시 생성하여 Binding함. 2008.10.07. by John Seo
                dtToWeek = GetWeekInfo(DateTime.Today.Year.ToString());
                this.cbo_toweeks.DisplayMember = "V1";
                this.cbo_toweeks.ValueMember = "V2";
                this.cbo_toweeks.DataSource = dtToWeek;
                this.cbo_toweeks.Text = sCurWeek;

                dtFromWeek = GetWeekInfo(DateTime.Today.Year.ToString());
                this.cbo_weeks.DisplayMember = "V1";
                this.cbo_weeks.ValueMember = "V2";
                this.cbo_weeks.DataSource = dtFromWeek;
                this.cbo_weeks.Text = sCurWeek;
            }
        }

        private void cbo_day_select_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbo_day_select.SelectedValue.ToString() == "DAY")
            {
                pnl_date.Visible = true;
                pnl_week.Visible = false;
                pnl_month.Visible = false;

                pnl_date.Location = new Point(pnl_date.Location.X, 0);
                pnl_week.Location = new Point(pnl_week.Location.X, 40);
                pnl_month.Location = new Point(pnl_month.Location.X, 40);

            }
            else if (cbo_day_select.SelectedValue.ToString() == "WEEK")
            {
                pnl_date.Visible = false;
                pnl_week.Visible = true;
                pnl_month.Visible = false;


                pnl_date.Location = new Point(pnl_date.Location.X, 40);
                pnl_week.Location = new Point(pnl_week.Location.X, 0);
                pnl_month.Location = new Point(pnl_month.Location.X, 40);
            }
            else
            {
                pnl_date.Visible = false;
                pnl_week.Visible = false;
                pnl_month.Visible = true;

                pnl_date.Location = new Point(pnl_date.Location.X, 40);
                pnl_week.Location = new Point(pnl_week.Location.X, 40);
                pnl_month.Location = new Point(pnl_month.Location.X, 0);
            }
        }
        #region IValidation 멤버


        /// <summary>
        /// 유효성 검사
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>

        public bool ValidationCheck(ref string Message)
        {
            if (this.cbo_day_select.SelectedValue.ToString() == "DAY")
            {
                DateTime dFrom = dtp_fromdate.Value;
                DateTime dTo = dtp_todate.Value;
                TimeSpan tCount = dTo.Subtract(dFrom);
                if (tCount.Days < 0)
                {
                    Message = "To Date가 From Date보다 큽니다.";
                    return false;
                }
                else if (tCount.Days + 1 > _restrictedDayCount)
                {
                    Message = _restrictedDayCount.ToString() + "일 이상 조회는 불가능 합니다.";
                    return false;
                }
            }
            else if (this.cbo_day_select.SelectedValue.ToString() == "WEEK")
            {
                int iWeek = 0;

                if ((dtp_toyear.Value.Year - dtp_year.Value.Year) == 0)
                {
                    iWeek = (Convert.ToInt32(cbo_toweeks.SelectedValue.ToString().Substring(4, 2)) - Convert.ToInt32(cbo_weeks.SelectedValue.ToString().Substring(4, 2)));
                }
                else if ((dtp_toyear.Value.Year - dtp_year.Value.Year) < 0)
                {
                    iWeek = -1;
                }

                if (iWeek < 0)
                {
                    Message = "To WEEK가 From WEEK 보다 큽니다.";
                    return false;
                }

                if (this.SubtractBetweenFromToDate + 1 > _restrictedDayCount)
                {
                    Message = _restrictedDayCount.ToString() + "주 이상 조회는 불가능 합니다.";
                    return false;
                }

            }
            else
            {
                int iMonth = (dtp_tomonth.Value.Year - dtp_month.Value.Year) * 12 + (dtp_tomonth.Value.Month - dtp_month.Value.Month);

                if (iMonth < 0)
                {
                    Message = "To Month 가 From Month보다 큽니다.";
                    return false;
                }

                if (this.SubtractBetweenFromToDate + 1 > _restrictedDayCount)
                {
                    Message = _restrictedDayCount.ToString() + "월 이상 조회는 불가능 합니다.";
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 날짜 변경시에 사이 날짜 조정..
        private void FromDate_ValueChanged(object sender, EventArgs e)
        {

            // Type 에 맞춰서 제어해줘야함.. 

            // from 이 to 보다 큰경우
            if (FromDate.Value.CompareTo(ToDate.Value) > 0)
            {
                ToDate.Value = FromDate.Value;
            }

        }

        private void ToDate_ValueChanged(object sender, EventArgs e)
        {
            // to 이 from 보다 작은 경우
            if (ToDate.Value.CompareTo(FromDate.Value) < 0)
            {
                FromDate.Value = ToDate.Value;
            }
        }

        private void FromYearMonth_ValueChanged(object sender, EventArgs e)
        {
            // Type 에 맞춰서 제어해줘야함.. 

            // from 이 to 보다 큰경우
            if (FromYearMonth.Value.CompareTo(ToYearMonth.Value) > 0)
            {
                ToYearMonth.Value = FromYearMonth.Value;
            }
        }

        private void ToYearMonth_ValueChanged(object sender, EventArgs e)
        {
            // to 이 from 보다 작은 경우
            if (ToYearMonth.Value.CompareTo(FromYearMonth.Value) < 0)
            {
                FromYearMonth.Value = ToYearMonth.Value;
            }
        }

        private void FromYear_ValueChanged(object sender, EventArgs e)
        {
            // Type 에 맞춰서 제어해줘야함.. 

            // from 이 to 보다 큰경우
            if (FromYear.Value.CompareTo(ToYear.Value) > 0)
            {
                ToYear.Value = FromYear.Value;
            }

            SetWeekCombo("FROM");
        }

        private void ToYear_ValueChanged(object sender, EventArgs e)
        {
            // to 이 from 보다 작은 경우
            if (ToYear.Value.CompareTo(FromYear.Value) < 0)
            {
                FromYear.Value = ToYear.Value;
            }

            SetWeekCombo("TO");
        }

        private void FromWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 년도가 다르다면 무시 -- 년도 컨트롤러가 보정함..
            if (FromYear.Value.CompareTo(ToYear.Value) == 0)
            {
                if (FromWeek.Items.Count <= 0) return;

                if (ToWeek.Items.Count <= 0) return;

                // from index 가 항상 작아야함
                if (FromWeek.SelectedIndex > ToWeek.SelectedIndex)
                {
                    ToWeek.SelectedIndex = FromWeek.SelectedIndex;
                }
            }
        }

        private void ToWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 년도가 다르다면 무시 -- 년도 컨트롤러가 보정함..
            if (FromYear.Value.CompareTo(ToYear.Value) == 0)
            {
                if (FromWeek.Items.Count <= 0) return;

                if (ToWeek.Items.Count <= 0) return;

                // from index 가 항상 작아야함 ( 인덱스가 클수록 작은 값.. )
                if (FromWeek.SelectedIndex > ToWeek.SelectedIndex)
                {
                    FromWeek.SelectedIndex = ToWeek.SelectedIndex;
                }
            }
        }
        #endregion
    }
}
