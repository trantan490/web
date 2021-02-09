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
using System.Text;

namespace Miracom.SmartWeb.UI.Controls
{
    //2010-01-06-임종우 AutoBinding 시 처리 하기 위한 전역변수 선언.
    static public class CommonLib
    {
        static string _sAutoBind;

        static public string sAutoBind
        {
            get
            {
                return _sAutoBind;
            }
            set
            {
                _sAutoBind = value;
            }
        }
    }

    public enum DateType { Duration, OneDate };

    public class udcDurationDate : System.Windows.Forms.Panel
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
        private DateType _type;
        private string _exactFromDate;
        private string _exactToDate;


		//private bool _isFirstYN = true;
		private int _restrictedDayCount = 60; //MAX조회 가능 주
        
		public udcDurationDate()
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

        [DefaultValue(DateType.Duration)]
        public DateType Type
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
                strSqlString.AppendFormat(", " + "{0}{4} " + "{1}{4}" + " {2}{4}{3}{4}" + "\n", first,second,third,fourth, i.ToString());
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

        // 값이 두군데 들어가는 Decode쿼리를 위해 추가 '08.12.22 양형석
        public string getDecodeQuery(string sFront, string sMiddle, string sBack, string sAlias)
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
                    strSqlString.AppendFormat(", " + "{0}, '{1}', {2}, '{1}', {3} {4}{5}" + "\n", sFront, sHeader, sMiddle, sBack, sAlias, i.ToString());
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
                        strSqlString.AppendFormat(", " + "{0}, '{1}', {2}, '{1}', {3} {4}{5}" + "\n", sFront, sHeader, sMiddle, sBack, sAlias, j.ToString());
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
                        strSqlString.AppendFormat(", " + "{0}, '{1}', {2}, '{1}', {3} {4}{5}" + "\n", sFront, sHeader, sMiddle, sBack, sAlias, j.ToString());
                        j++;
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        if (i < 10)
                            sHeader = this.ToWeek.SelectedValue.ToString().Substring(0, 4) + "0" + i.ToString();
                        else
                            sHeader = this.ToWeek.SelectedValue.ToString().Substring(0, 4) + i.ToString();

                        //strSqlString.AppendFormat(" , DECODE(CUTOFF_WEEK, '{0}',QTY,0) V{1}" + "\n", sHeader, j.ToString());
                        strSqlString.AppendFormat(", " + "{0}, '{1}', {2}, '{1}', {3} {4}{5}" + "\n", sFront, sHeader, sMiddle, sBack, sAlias, j.ToString());
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
                    strSqlString.AppendFormat(", " + "{0}, '{1}', {2}, '{1}', {3} {4}{5}" + "\n", sFront, sHeader, sMiddle, sBack, sAlias, i.ToString());
                }
            }

            return strSqlString.ToString();

        }

        // 하나 기준 FromDay 가져오기 (2009.07.15 임종우)
        public string HmFromDay
        {
            get
            {
                string sFrom = string.Empty;
                string sTo = string.Empty;

                if (this.DaySelector.SelectedValue.ToString() == "DAY")
                {
                    sFrom = this.FromDate.Text.Replace("-", "");
                    sTo = this.ToDate.Text.Replace("-", "");

                    return sFrom;
                }
                else if (this.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    StringBuilder strSqlString = new StringBuilder();
                    string sFromWeek;
                    string sToWeek;

                    sFrom = this.FromWeek.SelectedValue.ToString();
                    sTo = this.ToWeek.SelectedValue.ToString();

                    // 200904 -> '04'에서 0제거 4만가져오기
                    if (sFrom.Substring(4, 1) == "0")
                    {
                        sFromWeek = sFrom.Substring(4, 2).Replace('0', ' ').Trim();
                    }
                    else
                    {
                        sFromWeek = sFrom.Substring(4, 2).Trim();
                    }

                    if (sTo.Substring(4, 1) == "0")
                    {
                        sToWeek = sTo.Substring(4, 2).Replace('0', ' ').Trim();
                    }
                    else
                    {
                        sToWeek = sTo.Substring(4, 2).Trim();
                    }

                    strSqlString.Append("SELECT A.SYS_DATE, B.SYS_DATE " + "\n");
                    strSqlString.Append("  FROM " + "\n");
                    strSqlString.Append("       (" + "\n");
                    strSqlString.Append("       SELECT SYS_DATE " + "\n");
                    strSqlString.Append("         FROM MWIPCALDEF " + "\n");
                    strSqlString.Append("        WHERE 1=1 " + "\n");
                    strSqlString.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sFrom.Substring(0, 4));
                    strSqlString.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sFromWeek);
                    strSqlString.Append("     ORDER BY SYS_DATE ASC       " + "\n");
                    strSqlString.Append("       ) A," + "\n");
                    strSqlString.Append("       (" + "\n");
                    strSqlString.Append("       SELECT SYS_DATE " + "\n");
                    strSqlString.Append("         FROM MWIPCALDEF " + "\n");
                    strSqlString.Append("        WHERE 1=1 " + "\n");
                    strSqlString.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sTo.Substring(0, 4));
                    strSqlString.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sToWeek);
                    strSqlString.Append("     ORDER BY SYS_DATE DESC" + "\n");
                    strSqlString.Append("       ) B        " + "\n");
                    strSqlString.Append(" WHERE ROWNUM=1" + "\n");

                    DataTable dt = null;
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

                    sFrom = dt.Rows[0][0].ToString();
                    sTo = dt.Rows[0][1].ToString();

                    return sFrom;
                }
                else
                {
                    sFrom = this.FromYearMonth.Value.ToString("yyyyMM") + "01";
                    sTo = this.ToYearMonth.Value.ToString("yyyyMM");

                    DataTable dt = null;

                    string lastDay = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + sTo + "', 'YYYYMM')), 'YYYYMMDD') FROM DUAL)";
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", lastDay);

                    sTo = dt.Rows[0][0].ToString();

                    return sFrom;
                }
            }

        }

        // 하나 기준 toDay 가져오기 (2009.07.15 임종우)
        public string HmToDay
        {
            get
            {
                string sFrom = string.Empty;
                string sTo = string.Empty;

                if (this.DaySelector.SelectedValue.ToString() == "DAY")
                {
                    sFrom = this.FromDate.Text.Replace("-", "");
                    sTo = this.ToDate.Text.Replace("-", "");

                    return sTo;
                }
                else if (this.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    StringBuilder strSqlString = new StringBuilder();
                    string sFromWeek;
                    string sToWeek;

                    sFrom = this.FromWeek.SelectedValue.ToString();
                    sTo = this.ToWeek.SelectedValue.ToString();

                    // 200904 -> '04'에서 0제거 4만가져오기
                    if (sFrom.Substring(4, 1) == "0")
                    {
                        sFromWeek = sFrom.Substring(4, 2).Replace('0', ' ').Trim();
                    }
                    else
                    {
                        sFromWeek = sFrom.Substring(4, 2).Trim();
                    }

                    if (sTo.Substring(4, 1) == "0")
                    {
                        sToWeek = sTo.Substring(4, 2).Replace('0', ' ').Trim();
                    }
                    else
                    {
                        sToWeek = sTo.Substring(4, 2).Trim();
                    }

                    strSqlString.Append("SELECT A.SYS_DATE, B.SYS_DATE " + "\n");
                    strSqlString.Append("  FROM " + "\n");
                    strSqlString.Append("       (" + "\n");
                    strSqlString.Append("       SELECT SYS_DATE " + "\n");
                    strSqlString.Append("         FROM MWIPCALDEF " + "\n");
                    strSqlString.Append("        WHERE 1=1 " + "\n");
                    strSqlString.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sFrom.Substring(0, 4));
                    strSqlString.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sFromWeek);
                    strSqlString.Append("     ORDER BY SYS_DATE ASC       " + "\n");
                    strSqlString.Append("       ) A," + "\n");
                    strSqlString.Append("       (" + "\n");
                    strSqlString.Append("       SELECT SYS_DATE " + "\n");
                    strSqlString.Append("         FROM MWIPCALDEF " + "\n");
                    strSqlString.Append("        WHERE 1=1 " + "\n");
                    strSqlString.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sTo.Substring(0, 4));
                    strSqlString.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sToWeek);
                    strSqlString.Append("     ORDER BY SYS_DATE DESC" + "\n");
                    strSqlString.Append("       ) B        " + "\n");
                    strSqlString.Append(" WHERE ROWNUM=1" + "\n");

                    DataTable dt = null;
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

                    sFrom = dt.Rows[0][0].ToString();
                    sTo = dt.Rows[0][1].ToString();

                    return sTo;
                }
                else
                {
                    sFrom = this.FromYearMonth.Value.ToString("yyyyMM") + "01";
                    sTo = this.ToYearMonth.Value.ToString("yyyyMM");

                    DataTable dt = null;

                    string lastDay = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + sTo + "', 'YYYYMM')), 'YYYYMMDD') FROM DUAL)";
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", lastDay);

                    sTo = dt.Rows[0][0].ToString();

                    return sTo;
                }
            }

        }

        // 하나기준 FromWeek 가져오기 (2009.07.15 임종우)
        public string HmFromWeek
        {
            get
            {
                string sFrom = string.Empty;

                if (this.DaySelector.SelectedValue.ToString() == "DAY")
                {
                    sFrom = this.FromDate.Text.Replace("-", "");

                    DataTable dt = null;
                    string strSqlString = "(SELECT PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID='HM' AND SYS_DATE='" + sFrom + "')";
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString);

                    sFrom = sFrom.Substring(0, 4) + dt.Rows[0][0].ToString();

                    return sFrom;
                }
                else if (this.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    sFrom = this.FromWeek.SelectedValue.ToString();

                    return sFrom;
                }
                else
                {
                    string sFromMonth = string.Empty;

                    sFrom = this.FromYearMonth.Value.ToString("yyyyMM");

                    if (sFrom.Substring(4, 1) == "0")
                    {
                        sFromMonth = sFrom.Substring(4, 2).Replace('0', ' ').Trim();
                    }
                    else
                    {
                        sFromMonth = sFrom.Substring(4, 2).Trim();
                    }

                    DataTable dt = null;
                    string strSqlString = "(SELECT MIN(PLAN_WEEK) FROM MWIPCALDEF WHERE CALENDAR_ID='HM' AND PLAN_YEAR='" + sFrom.Substring(0, 4) + "' AND PLAN_MONTH='" + sFromMonth + "')";
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString);

                    sFrom = sFrom.Substring(0, 4) + dt.Rows[0][0].ToString();

                    return sFrom;
                }
            }
        }

        // 하나기준 ToWeek 가져오기 (2009.07.15 임종우)
        public string HmToWeek
        {
            get
            {
                string sTo = string.Empty;

                if (this.DaySelector.SelectedValue.ToString() == "DAY")
                {
                    sTo = this.ToDate.Text.Replace("-", "");

                    DataTable dt = null;
                    string strSqlString = "(SELECT PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID='HM' AND SYS_DATE='" + sTo + "')";
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString);

                    sTo = sTo.Substring(0, 4) + dt.Rows[0][0].ToString();

                    return sTo;
                }
                else if (this.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    sTo = this.ToWeek.SelectedValue.ToString();

                    return sTo;
                }
                else
                {
                    string sToMonth = string.Empty;

                    sTo = this.ToYearMonth.Value.ToString("yyyyMM");

                    if (sTo.Substring(4, 1) == "0")
                    {
                        sToMonth = sTo.Substring(4, 2).Replace('0', ' ').Trim();
                    }
                    else
                    {
                        sToMonth = sTo.Substring(4, 2).Trim();
                    }

                    DataTable dt = null;
                    string strSqlString = "(SELECT MAX(PLAN_WEEK) FROM MWIPCALDEF WHERE CALENDAR_ID='HM' AND PLAN_YEAR='" + sTo.Substring(0, 4) + "' AND PLAN_MONTH='" + sToMonth + "')";
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString);

                    sTo = sTo.Substring(0, 4) + dt.Rows[0][0].ToString();

                    return sTo;
                }
            }
        }

        // 삼성 기준 TranTime 으로 변경 
        public string getFromTranTime()
        {
            string sFrom = string.Empty;
            string sTo = string.Empty;

            if (this.DaySelector.SelectedValue.ToString() == "DAY")
            {
                sFrom = this.FromDate.Text;
                sTo = this.ToDate.Text.Replace("-", "") + "215959";

                DateTime FromDate = DateTime.Parse(sFrom);
                FromDate = FromDate.AddDays(-1);

                sFrom = FromDate.ToString("yyyyMMdd") + "220000";                

                return sFrom;
            }
            else if (this.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                StringBuilder strSqlString1 = new StringBuilder();
                string sFromWeek;
                string sToWeek;

                sFrom = this.FromWeek.SelectedValue.ToString();
                sTo = this.ToWeek.SelectedValue.ToString();

                if (sFrom.Substring(4, 1) == "0")
                {
                    sFromWeek = sFrom.Substring(4, 2).Replace('0', ' ').Trim();
                }
                else
                {
                    sFromWeek = sFrom.Substring(4, 2).Trim();
                }

                if (sTo.Substring(4, 1) == "0")
                {                 
                    sToWeek = sTo.Substring(4, 2).Replace('0', ' ').Trim();
                }
                else
                {
                    sToWeek = sTo.Substring(4, 2).Trim();
                }        

                strSqlString1.Append("SELECT A.SYS_DATE, B.SYS_DATE " + "\n");
                strSqlString1.Append("  FROM " + "\n");
                strSqlString1.Append("       (" + "\n");
                strSqlString1.Append("       SELECT SYS_DATE " + "\n");
                strSqlString1.Append("         FROM MWIPCALDEF " + "\n");
                strSqlString1.Append("        WHERE 1=1 " + "\n");
                strSqlString1.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                strSqlString1.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sFrom.Substring(0,4));
                strSqlString1.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sFromWeek);
                strSqlString1.Append("     ORDER BY SYS_DATE ASC       " + "\n");
                strSqlString1.Append("       ) A," + "\n");
                strSqlString1.Append("       (" + "\n");
                strSqlString1.Append("       SELECT SYS_DATE " + "\n");
                strSqlString1.Append("         FROM MWIPCALDEF " + "\n");
                strSqlString1.Append("        WHERE 1=1 " + "\n");
                strSqlString1.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                strSqlString1.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sTo.Substring(0,4));
                strSqlString1.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sToWeek);
                strSqlString1.Append("     ORDER BY SYS_DATE DESC" + "\n");
                strSqlString1.Append("       ) B        " + "\n");
                strSqlString1.Append(" WHERE ROWNUM=1" + "\n");

                DataTable dt1 = null;
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString1.ToString());

                sFrom = dt1.Rows[0][0].ToString();
                sFrom = sFrom.Insert(4, "-");
                sFrom = sFrom.Insert(7, "-");

                DateTime FromDate = DateTime.Parse(sFrom).AddDays(-1);
                sFrom = FromDate.ToString("yyyyMMdd");
                sFrom = sFrom + "220000";               

                return sFrom;
            }
            else
            {
                sFrom = this.FromYearMonth.Value.ToString("yyyy-MM-") + "01";
                sTo = this.ToYearMonth.Value.ToString("yyyyMM");

                DataTable dt1 = null;
                string lastday = DateTime.Now.ToString("yyyyMM").ToString();
                string ToDate = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + lastday + "', 'YYYYMM')), 'YYYYMMDD') FROM DUAL)";
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ToDate);
                lastday = dt1.Rows[0][0].ToString() + "215959";

                DateTime From = DateTime.Parse(sFrom).AddDays(-1);
                sFrom = From.ToString("yyyyMMdd").Replace("-","") + "220000";
                
                return sFrom;
            }            


        }

        // 삼성 기준 TranTime으로 변경
        public string getToTranTime()
        {
            string sFrom = string.Empty;
            string sTo = string.Empty;

            if (this.DaySelector.SelectedValue.ToString() == "DAY")
            {
                sFrom = this.FromDate.Text;
                sTo = this.ToDate.Text.Replace("-", "") + "215959";

                DateTime FromDate = DateTime.Parse(sFrom);
                FromDate = FromDate.AddDays(-1);

                sFrom = FromDate.ToString("yyyyMMdd") + "220000";

                return sTo;

            }
            else if (this.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                StringBuilder strSqlString1 = new StringBuilder();
                string sFromWeek;
                string sToWeek;
                
                sFrom = this.FromWeek.SelectedValue.ToString();
                sTo = this.ToWeek.SelectedValue.ToString();

                if (sFrom.Substring(4, 1) == "0")
                {
                    sFromWeek = sFrom.Substring(4, 2).Replace('0', ' ').Trim();
                }
                else
                {
                    sFromWeek = sFrom.Substring(4, 2).Trim();
                }

                if (sTo.Substring(4, 1) == "0")
                {
                    sToWeek = sTo.Substring(4, 2).Replace('0', ' ').Trim();
                }
                else
                {
                    sToWeek = sTo.Substring(4, 2).Trim();
                }        

                strSqlString1.Append("SELECT A.SYS_DATE, B.SYS_DATE " + "\n");
                strSqlString1.Append("  FROM " + "\n");
                strSqlString1.Append("       (" + "\n");
                strSqlString1.Append("       SELECT SYS_DATE " + "\n");
                strSqlString1.Append("         FROM MWIPCALDEF " + "\n");
                strSqlString1.Append("        WHERE 1=1 " + "\n");
                strSqlString1.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                strSqlString1.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sFrom.Substring(0, 4));
                strSqlString1.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sFromWeek);
                strSqlString1.Append("     ORDER BY SYS_DATE ASC       " + "\n");
                strSqlString1.Append("       ) A," + "\n");
                strSqlString1.Append("       (" + "\n");
                strSqlString1.Append("       SELECT SYS_DATE " + "\n");
                strSqlString1.Append("         FROM MWIPCALDEF " + "\n");
                strSqlString1.Append("        WHERE 1=1 " + "\n");
                strSqlString1.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                strSqlString1.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sTo.Substring(0, 4));
                strSqlString1.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sToWeek);
                strSqlString1.Append("     ORDER BY SYS_DATE DESC" + "\n");
                strSqlString1.Append("       ) B        " + "\n");
                strSqlString1.Append(" WHERE ROWNUM=1" + "\n");

                DataTable dt1 = null;
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString1.ToString());                                
                sTo = dt1.Rows[0][1].ToString() + "215959";

                return sTo;
            }
            else
            {
                
                sFrom = this.FromYearMonth.Value.ToString("yyyy-MM-") + "01";
                sTo = this.ToYearMonth.Value.ToString("yyyyMM");

                DataTable dt1 = null;
                string lastday = DateTime.Now.ToString("yyyyMM").ToString();
                string ToDate = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + lastday + "', 'YYYYMM')), 'YYYYMMDD') FROM DUAL)";
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ToDate);
                lastday = dt1.Rows[0][0].ToString() + "215959";

                
                DateTime From = DateTime.Parse(sFrom).AddDays(-1);
                
                sFrom = From.ToString("yyyyMMdd").Replace("-", "") + "220000";

                return lastday;
            }        

        }

        // 하이닉스 기준 TranTime 으로 변경
        public string getFromTranTime_Hynix()
        {
            string sFrom = string.Empty;
            string sTo = string.Empty;

            if (this.DaySelector.SelectedValue.ToString() == "DAY")
            {
                sFrom = this.FromDate.Text.Replace("-", "");            
                sFrom = sFrom + "060000";
                return sFrom;
            }
            else if (this.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                StringBuilder strSqlString1 = new StringBuilder();
                string sFromWeek;
                string sToWeek;

                sFrom = this.FromWeek.SelectedValue.ToString();
                sTo = this.ToWeek.SelectedValue.ToString();

                if (sFrom.Substring(4, 1) == "0")
                {
                    sFromWeek = sFrom.Substring(4, 2).Replace('0', ' ').Trim();
                }
                else
                {
                    sFromWeek = sFrom.Substring(4, 2).Trim();
                }

                if (sTo.Substring(4, 1) == "0")
                {
                    sToWeek = sTo.Substring(4, 2).Replace('0', ' ').Trim();
                }
                else
                {
                    sToWeek = sTo.Substring(4, 2).Trim();
                }

                strSqlString1.Append("SELECT A.SYS_DATE, B.SYS_DATE " + "\n");
                strSqlString1.Append("  FROM " + "\n");
                strSqlString1.Append("       (" + "\n");
                strSqlString1.Append("       SELECT SYS_DATE " + "\n");
                strSqlString1.Append("         FROM MWIPCALDEF " + "\n");
                strSqlString1.Append("        WHERE 1=1 " + "\n");
                strSqlString1.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                strSqlString1.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sFrom.Substring(0, 4));
                strSqlString1.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sFromWeek);
                strSqlString1.Append("     ORDER BY SYS_DATE ASC       " + "\n");
                strSqlString1.Append("       ) A," + "\n");
                strSqlString1.Append("       (" + "\n");
                strSqlString1.Append("       SELECT SYS_DATE " + "\n");
                strSqlString1.Append("         FROM MWIPCALDEF " + "\n");
                strSqlString1.Append("        WHERE 1=1 " + "\n");
                strSqlString1.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                strSqlString1.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sTo.Substring(0, 4));
                strSqlString1.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sToWeek);
                strSqlString1.Append("     ORDER BY SYS_DATE DESC" + "\n");
                strSqlString1.Append("       ) B        " + "\n");
                strSqlString1.Append(" WHERE ROWNUM=1" + "\n");

                DataTable dt1 = null;
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString1.ToString());
                sFrom = dt1.Rows[0][0].ToString() + "060000";
                return sFrom;
            }
            else
            {
                sFrom = this.FromYearMonth.Value.ToString("yyyyMM") + "01";
                sFrom = sFrom + "060000";
                return sFrom;
            }
        }

        // 하이닉스 기준 TranTime으로 변경  ( 다음날 06시 )
        public string getToTranTime_Hynix()
        {
            string sFrom = string.Empty;
            string sTo = string.Empty;

            if (this.DaySelector.SelectedValue.ToString() == "DAY")
            {
                sTo = this.ToDate.Text;
                DateTime ToDate = DateTime.Parse(sTo);
                ToDate = ToDate.AddDays(1);
                sTo = ToDate.ToString("yyyyMMdd") + "055959";
                return sTo;
            }
            else if (this.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                StringBuilder strSqlString1 = new StringBuilder();
                string sFromWeek;
                string sToWeek;

                sFrom = this.FromWeek.SelectedValue.ToString();
                sTo = this.ToWeek.SelectedValue.ToString();

                if (sFrom.Substring(4, 1) == "0")
                {
                    sFromWeek = sFrom.Substring(4, 2).Replace('0', ' ').Trim();
                }
                else
                {
                    sFromWeek = sFrom.Substring(4, 2).Trim();
                }

                if (sTo.Substring(4, 1) == "0")
                {
                    sToWeek = sTo.Substring(4, 2).Replace('0', ' ').Trim();
                }
                else
                {
                    sToWeek = sTo.Substring(4, 2).Trim();
                }

                strSqlString1.Append("SELECT A.SYS_DATE, B.SYS_DATE " + "\n");
                strSqlString1.Append("  FROM " + "\n");
                strSqlString1.Append("       (" + "\n");
                strSqlString1.Append("       SELECT SYS_DATE " + "\n");
                strSqlString1.Append("         FROM MWIPCALDEF " + "\n");
                strSqlString1.Append("        WHERE 1=1 " + "\n");
                strSqlString1.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                strSqlString1.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sFrom.Substring(0, 4));
                strSqlString1.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sFromWeek);
                strSqlString1.Append("     ORDER BY SYS_DATE ASC       " + "\n");
                strSqlString1.Append("       ) A," + "\n");
                strSqlString1.Append("       (" + "\n");
                strSqlString1.Append("       SELECT SYS_DATE " + "\n");
                strSqlString1.Append("         FROM MWIPCALDEF " + "\n");
                strSqlString1.Append("        WHERE 1=1 " + "\n");
                strSqlString1.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                strSqlString1.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sTo.Substring(0, 4));
                strSqlString1.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sToWeek);
                strSqlString1.Append("     ORDER BY SYS_DATE DESC" + "\n");
                strSqlString1.Append("       ) B        " + "\n");
                strSqlString1.Append(" WHERE ROWNUM=1" + "\n");

                DataTable dt1 = null;
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString1.ToString());
                sTo = dt1.Rows[0][1].ToString();
                sTo = sTo.Insert(4, "-");
                sTo = sTo.Insert(7, "-");

                DateTime ToDate = DateTime.Parse(sTo).AddDays(1);
                sTo = ToDate.ToString("yyyyMMdd") + "055959";

                return sTo;
            }
            else
            {                   
                string lastday = DateTime.Now.AddMonths(1).ToString("yyyyMM");                
                lastday = lastday + "01055959";
                return lastday;
            }

        }

		// 내부컨트롤러 반환
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

        public string ExactFromDate
        {
            get
            {
                return _exactFromDate;
            }
        }

        public string ExactToDate
        {
            get
            {
                return _exactToDate;
            }
        }


		// 실제 사용시에는 +1 해서 사용해야함..
		public int SubtractBetweenFromToDate
		{
			get
			{
				if ( this.cbo_day_select.SelectedValue == null)
                    throw new Exception (RptMessages.GetMessage("STD086", GlobalVariable.gcLanguage));
				
				if ( this.cbo_day_select.SelectedValue.ToString() == "DAY" )
				{
					DateTime dFrom = dtp_fromdate.Value;
					DateTime dTo   = dtp_todate.Value;
					TimeSpan tCount = dTo.Subtract(dFrom);
					return tCount.Days;
				}
				else if ( this.cbo_day_select.SelectedValue.ToString() == "WEEK" )
				{
					// 1년이상 차이나면 에러임.
					// 항상 To 가 From 보다 크다고 가정 아닌경우 이전 상태에서 에러를 리턴해야함
					if ( ( dtp_toyear.Value.Year - dtp_year.Value.Year ) > 1 )
                        throw new Exception (RptMessages.GetMessage("STD087", GlobalVariable.gcLanguage));

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
				else if ( this.cbo_day_select.SelectedValue.ToString() == "MONTH" )
				{
					// 항상 To 가 From 보다 크다고 가정 아닌경우 이전 상태에서 에러를 리턴해야함
					return ( dtp_tomonth.Value.Year - dtp_month.Value.Year ) * 12 + ( dtp_tomonth.Value.Month - dtp_month.Value.Month );
				}	
				else
				{
					throw new Exception(RptMessages.GetMessage("STD088", GlobalVariable.gcLanguage));
				}

			}
		}

        // 선택된 DAY, WEEK, MONTH를 반환
        public string[] getSelectDate()
        {
            System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();
            string[] SelectDate = new string[SubtractBetweenFromToDate + 1];

            if (this.DaySelector.SelectedValue.ToString() == "DAY")
            {
                for (int i = 0; i <= this.SubtractBetweenFromToDate; i++)
                {

                    SelectDate[i] = this.FromDate.Value.AddDays(i).ToString("yyyyMMdd");

                }
            }
            else if (this.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(this.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = this.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(this.ToWeek.SelectedValue.ToString().Substring(4, 2));

                //년도가 같을 경우
                if (this.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    this.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    // 12,13,14,15,16,17...
                    for (int j = 0, i = thisWeek; i <= maxWeek; i++, j++)
                    {
                        if (i < 10)
                            SelectDate[j] = this.FromWeek.SelectedValue.ToString().Substring(0, 4) + "0" + i.ToString();
                        else
                            SelectDate[j] = this.FromWeek.SelectedValue.ToString().Substring(0, 4) + i.ToString();
                    }
                }
                else //년도가 다를 경우
                {
                    int j = 0;

                    //50,51,52,53,1,2,3,4,5
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        if (i < 10)
                            SelectDate[j] = this.FromWeek.SelectedValue.ToString().Substring(0, 4) + "0" + i.ToString();
                        else
                            SelectDate[j] = this.FromWeek.SelectedValue.ToString().Substring(0, 4) + i.ToString();

                        j++;
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        if (i < 10)
                            SelectDate[j] = this.ToWeek.SelectedValue.ToString().Substring(0, 4) + "0" + i.ToString();
                        else
                            SelectDate[j] = this.ToWeek.SelectedValue.ToString().Substring(0, 4) + i.ToString();

                        j++;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= this.SubtractBetweenFromToDate; i++)
                {
                    SelectDate[i] = this.FromYearMonth.Value.AddMonths(i).ToString("yyyyMM");
                }
            }

            return SelectDate;
        }


		/// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
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
            this.pnl_week = new System.Windows.Forms.Panel();
            this.cbo_toweeks = new System.Windows.Forms.ComboBox();
            this.cbo_weeks = new System.Windows.Forms.ComboBox();
            this.pnl_month = new System.Windows.Forms.Panel();
            this.dtp_tomonth = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.dtp_month = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.dtp_year = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.dtp_toyear = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.dtp_todate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.dtp_fromdate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
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
            this.pnl_date.Controls.Add(this.dtp_fromdate);
            this.pnl_date.Location = new System.Drawing.Point(46, 0);
            this.pnl_date.Name = "pnl_date";
            this.pnl_date.Size = new System.Drawing.Size(275, 21);
            this.pnl_date.TabIndex = 84;
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
            this.dtp_year.Value = new System.DateTime(2009, 8, 16, 0, 0, 0, 0);
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
            this.dtp_toyear.Value = new System.DateTime(2009, 8, 16, 0, 0, 0, 0);
            // 
            // dtp_todate
            // 
            this.dtp_todate.CustomFormat = "yyyy-MM-dd";
            this.dtp_todate.Font = new System.Drawing.Font("굴림체", 8F);
            this.dtp_todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_todate.Location = new System.Drawing.Point(98, 0);
            this.dtp_todate.Name = "dtp_todate";
            this.dtp_todate.Size = new System.Drawing.Size(88, 20);
            this.dtp_todate.TabIndex = 79;
            this.dtp_todate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.ToDate;
            this.dtp_todate.Value = new System.DateTime(2008, 2, 20, 0, 0, 0, 0);
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
            cbo_day_select.SelectedIndexChanged -= new EventHandler(RefreshExactDate);

            if (this._type == DateType.OneDate)
            {
                dtp_fromdate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                dtp_todate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

                SetWeekCombo("ALL");

                dtp_month.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM");
                dtp_tomonth.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM");
            }
            else
            {
                dtp_fromdate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                dtp_todate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                SetWeekCombo("ALL");

                dtp_month.Text = DateTime.Now.AddDays(-1).AddMonths(-3).ToString("yyyy-MM");
                dtp_tomonth.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM");
            }

            cbo_day_select.SelectedIndexChanged += new EventHandler(RefreshExactDate);
            RefreshExactDate(null, null);
		}

        /// <summary>
        /// Auto Binding 재정의
        /// </summary>
        /// <param name="fromDate">시작일 : yyyy-MM-dd</param>
        /// <param name="endDate">종료일 : yyyy-MM-dd</param>
        public void AutoBinding(String fromDate, String endDate)
        {
            cbo_day_select.SelectedIndexChanged -= new EventHandler(RefreshExactDate);

            if (this._type != DateType.OneDate)
            {
                dtp_fromdate.Text = DateTime.Parse(fromDate).ToString("yyyy-MM-dd");
                dtp_todate.Text = DateTime.Parse(endDate).ToString("yyyy-MM-dd");

                SetWeekCombo("ALL");

                dtp_month.Text = DateTime.Parse(fromDate).AddMonths(-3).ToString("yyyy-MM");
                dtp_tomonth.Text = DateTime.Parse(fromDate).ToString("yyyy-MM");
            }

            cbo_day_select.SelectedIndexChanged += new EventHandler(RefreshExactDate);
            RefreshExactDate(null, null);
        }

        /// <summary>
        /// 2010-01-05-임종우 Month 3개월 지정 해제(사용자 마음대로..)
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="endDate"></param>
        public void AutoBindingUserSetting(String fromDate, String endDate)
        {            
            cbo_day_select.SelectedIndexChanged -= new EventHandler(RefreshExactDate);
            
            if (this._type != DateType.OneDate)
            {
                //2010-01-06-임종우 AutoBinding 일때 udcDateTimePicker_ValueChanged 로직 타지 않게 하기 위해 전역 변수 추가.
                //udcDateTimePicker_ValueChanged : 12월 -> 1월 변경, 1월-> 12월 변경 시 년도 변경해주는 로직
                CommonLib.sAutoBind = "Auto";

                dtp_fromdate.Text = DateTime.Parse(fromDate).ToString("yyyy-MM-dd");
                dtp_todate.Text = DateTime.Parse(endDate).ToString("yyyy-MM-dd");

                SetWeekCombo("ALL");

                dtp_month.Text = DateTime.Parse(fromDate).ToString("yyyy-MM");
                dtp_tomonth.Text = DateTime.Parse(endDate).ToString("yyyy-MM");
                
                CommonLib.sAutoBind = "";
            }

            cbo_day_select.SelectedIndexChanged += new EventHandler(RefreshExactDate);
            RefreshExactDate(null, null);
        }

		#endregion

        private void ControlInit()
        {
            if (this._type == DateType.OneDate)
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

			pnl_date.Location = new Point( pnl_date.Location.X, 0);
			pnl_week.Location = new Point( pnl_week.Location.X, 40);
			pnl_month.Location = new Point( pnl_month.Location.X, 40);

			// 날짜 이동시에 이벤트 
			FromDate.ValueChanged+=new EventHandler(FromDate_ValueChanged);
			ToDate.ValueChanged +=new EventHandler(ToDate_ValueChanged);
			FromYearMonth.ValueChanged +=new EventHandler(FromYearMonth_ValueChanged);
			ToYearMonth.ValueChanged +=new EventHandler(ToYearMonth_ValueChanged);
			FromYear.ValueChanged +=new EventHandler(FromYear_ValueChanged);
			ToYear.ValueChanged +=new EventHandler(ToYear_ValueChanged);
			FromWeek.SelectedIndexChanged +=new EventHandler(FromWeek_SelectedIndexChanged);
			ToWeek.SelectedIndexChanged +=new EventHandler(ToWeek_SelectedIndexChanged);

            FromDate.ValueChanged += new EventHandler(RefreshExactDate);
            ToDate.ValueChanged += new EventHandler(RefreshExactDate);
            FromYearMonth.ValueChanged += new EventHandler(RefreshExactDate);
            ToYearMonth.ValueChanged += new EventHandler(RefreshExactDate);
            FromYear.ValueChanged += new EventHandler(RefreshExactDate);
            ToYear.ValueChanged += new EventHandler(RefreshExactDate);
            FromWeek.SelectedIndexChanged += new EventHandler(RefreshExactDate);
            ToWeek.SelectedIndexChanged += new EventHandler(RefreshExactDate);
		}

        private void AddItem( )
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

            row = dt.NewRow();
            row[DisplayMember] = "주";
            row[ValueMember] = "WEEK";
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[DisplayMember] = "월";
            row[ValueMember] = "MONTH";
            dt.Rows.Add(row);

            this.cbo_day_select.DisplayMember = DisplayMember;
            this.cbo_day_select.ValueMember = ValueMember;
            this.cbo_day_select.DataSource = dt;

            cbo_day_select.SelectedIndexChanged += new EventHandler(RefreshExactDate);
        }

        private DataTable GetWeekInfo(string sYear)
        {
            string sMySql;
            DataTable dt = null;

            sMySql = "";
            sMySql = sMySql + " SELECT UNIQUE PLAN_WEEK V1, (PLAN_YEAR || LPAD(PLAN_WEEK, 2,'0')) V2 ";
            sMySql = sMySql + "   FROM MWIPCALDEF ";
            sMySql = sMySql + "  WHERE CALENDAR_ID IN ('SYSTEM', 'HM', '" + GlobalVariable.gsFactory + "') ";
            sMySql = sMySql + "    AND SYS_YEAR = NVL('" + sYear + "', TO_CHAR(SYSDATE,'yyyy'))  " ;
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

                // 2010-04-28-임종우 : week 선택시 기본 년도가 금년도로 지정되도록 설정.
                this.dtp_year.Value = DateTime.Now;
                this.dtp_toyear.Value = DateTime.Now;

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
			if ( cbo_day_select.SelectedValue.ToString() == "DAY" )            
			{
				pnl_date.Visible = true;
				pnl_week.Visible = false;
				pnl_month.Visible = false;

				pnl_date.Location = new Point( pnl_date.Location.X, 0);
				pnl_week.Location = new Point( pnl_week.Location.X, 40);
				pnl_month.Location = new Point( pnl_month.Location.X, 40);

			}	
			else if ( cbo_day_select.SelectedValue.ToString() == "WEEK" )
			{
				pnl_date.Visible = false;
				pnl_week.Visible = true;
				pnl_month.Visible = false;

				
				pnl_date.Location = new Point( pnl_date.Location.X, 40);
				pnl_week.Location = new Point( pnl_week.Location.X, 0);
				pnl_month.Location = new Point( pnl_month.Location.X, 40); 
			}
			else
			{
				pnl_date.Visible = false;
				pnl_week.Visible = false;
				pnl_month.Visible = true;
				
				pnl_date.Location = new Point( pnl_date.Location.X, 40);
				pnl_week.Location = new Point( pnl_week.Location.X, 40);
				pnl_month.Location = new Point( pnl_month.Location.X, 0);
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
			if(this.cbo_day_select.SelectedValue.ToString() == "DAY")
			{
				DateTime dFrom = dtp_fromdate.Value;
				DateTime dTo   = dtp_todate.Value;
				TimeSpan tCount = dTo.Subtract(dFrom);
				if(tCount.Days < 0 )
				{
					Message = "To Date가 From Date보다 큽니다.";
					return false;
				}
				else if(tCount.Days+1 > _restrictedDayCount)
				{
					Message = _restrictedDayCount.ToString() +"일 이상 조회는 불가능 합니다.";
					return false;
				}
			}
			else if(this.cbo_day_select.SelectedValue.ToString() == "WEEK")
			{
				int iWeek = 0;
				
				if ( ( dtp_toyear.Value.Year - dtp_year.Value.Year ) == 0 )
				{
					iWeek= (Convert.ToInt32( cbo_toweeks.SelectedValue.ToString().Substring( 4, 2 ) ) - Convert.ToInt32( cbo_weeks.SelectedValue.ToString().Substring( 4, 2 ) ));
				}
				else if( ( dtp_toyear.Value.Year - dtp_year.Value.Year ) < 0 )
				{		
					iWeek = -1;
				}
				
				if(iWeek < 0 )
				{
					Message = "To WEEK가 From WEEK 보다 큽니다.";
					return false;
				}

				if ( this.SubtractBetweenFromToDate + 1 > _restrictedDayCount )
				{
					Message = _restrictedDayCount.ToString() +"주 이상 조회는 불가능 합니다.";
					return false;
				}

			}
			else
			{
				int iMonth = (dtp_tomonth.Value.Year - dtp_month.Value.Year ) * 12 + ( dtp_tomonth.Value.Month - dtp_month.Value.Month );
				
				if(iMonth < 0 )
				{
					Message = "To Month 가 From Month보다 큽니다.";
					return false;
				}

				if ( this.SubtractBetweenFromToDate + 1 > _restrictedDayCount )
				{
					Message = _restrictedDayCount.ToString() +"월 이상 조회는 불가능 합니다.";
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
			if ( FromDate.Value.CompareTo( ToDate.Value ) > 0 )
			{
				ToDate.Value = FromDate.Value;
			}

		}

		private void ToDate_ValueChanged(object sender, EventArgs e)
		{
			// to 이 from 보다 작은 경우
			if ( ToDate.Value.CompareTo( FromDate.Value ) < 0 )
			{
				FromDate.Value = ToDate.Value;
			}
		}

		private void FromYearMonth_ValueChanged(object sender, EventArgs e)
		{
			// Type 에 맞춰서 제어해줘야함.. 

			// from 이 to 보다 큰경우
			if ( FromYearMonth.Value.CompareTo( ToYearMonth.Value ) > 0 )
			{
				ToYearMonth.Value = FromYearMonth.Value;
			}
		}

		private void ToYearMonth_ValueChanged(object sender, EventArgs e)
		{
			// to 이 from 보다 작은 경우
			if ( ToYearMonth.Value.CompareTo( FromYearMonth.Value ) < 0 )
			{
				FromYearMonth.Value = ToYearMonth.Value;
			} 
		}

		private void FromYear_ValueChanged(object sender, EventArgs e)
		{
			// Type 에 맞춰서 제어해줘야함.. 

			// from 이 to 보다 큰경우
			if ( FromYear.Value.CompareTo( ToYear.Value ) > 0 )
			{
				ToYear.Value = FromYear.Value;
			}

            SetWeekCombo("FROM");
		}

		private void ToYear_ValueChanged(object sender, EventArgs e)
		{
			// to 이 from 보다 작은 경우
			if ( ToYear.Value.CompareTo( FromYear.Value ) < 0 )
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

        
        #region 데이터 변경시 정확한 조회조건 구하기 2008.12.24 양형석
        private void RefreshExactDate(object sender, EventArgs e)
        {
            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            // cbo_day_select 값이 null 일 경우 DAY 조건으로 셋팅..null 값 오는 이유 모르겠음...(2009.09.24 임종우)
            if (this.cbo_day_select.SelectedValue == null)
            {
                strFromDate = FromDate.Value.AddDays(-1).ToString("yyyyMMdd");
                strToDate = ToDate.Value.ToString("yyyyMMdd");
            }
            else
            {
                switch (this.cbo_day_select.SelectedValue.ToString())
                {
                    case "DAY":
                        strFromDate = FromDate.Value.AddDays(-1).ToString("yyyyMMdd");
                        strToDate = ToDate.Value.ToString("yyyyMMdd");

                        break;
                    case "WEEK":
                        DataTable dt = null;
                        System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();

                        if (FromWeek.Text == "" || ToWeek.Text == "")
                        {
                            break;
                        }
                        else if (FromWeek.Text.Trim() == "1")
                        {
                            strSqlString.Append("SELECT '" + Convert.ToString((Convert.ToInt32(FromYear.Text) - 1)) + "' || '1231220000' AS PREV_DATE " + "\n");
                            strSqlString.Append("     , ( " + "\n");
                            strSqlString.Append("        SELECT MAX(SYS_DATE) || '220000' " + "\n");
                            strSqlString.Append("          FROM MWIPCALDEF " + "\n");
                            strSqlString.Append("         WHERE 1=1 " + "\n");
                            strSqlString.Append("           AND CALENDAR_ID = 'HM' " + "\n");
                            strSqlString.Append("           AND PLAN_YEAR = '" + Convert.ToString(ToYear.Value.Year) + "' " + "\n");
                            strSqlString.Append("           AND PLAN_WEEK = '" + ToWeek.Text + "' " + "\n");
                            strSqlString.Append("       ) AS POST_DATE " + "\n");
                            strSqlString.Append("  FROM DUAL " + "\n");
                        }
                        else
                        {
                            strSqlString.Append("SELECT ( " + "\n");
                            strSqlString.Append("        SELECT MAX(SYS_DATE) || '220000'  " + "\n");
                            strSqlString.Append("          FROM MWIPCALDEF " + "\n");
                            strSqlString.Append("         WHERE 1=1 " + "\n");
                            strSqlString.Append("           AND CALENDAR_ID = 'HM' " + "\n");
                            strSqlString.Append("           AND PLAN_YEAR = '" + Convert.ToString(FromYear.Value.Year) + "' " + "\n");
                            strSqlString.Append("           AND PLAN_WEEK = '" + Convert.ToString((Convert.ToInt32(FromWeek.Text) - 1)) + "' " + "\n");
                            strSqlString.Append("       ) AS PREV_DATE " + "\n");
                            strSqlString.Append("     , ( " + "\n");
                            strSqlString.Append("        SELECT MAX(SYS_DATE) || '220000' " + "\n");
                            strSqlString.Append("          FROM MWIPCALDEF " + "\n");
                            strSqlString.Append("         WHERE 1=1 " + "\n");
                            strSqlString.Append("           AND CALENDAR_ID = 'HM' " + "\n");
                            strSqlString.Append("           AND PLAN_YEAR = '" + Convert.ToString(ToYear.Value.Year) + "' " + "\n");
                            strSqlString.Append("           AND PLAN_WEEK = '" + ToWeek.Text + "' " + "\n");
                            strSqlString.Append("       ) AS POST_DATE " + "\n");
                            strSqlString.Append("  FROM DUAL " + "\n");
                        }

                        dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

                        if (dt.Rows.Count == 0 || dt.Rows[0]["PREV_DATE"].ToString().Length < 8 || dt.Rows[0]["POST_DATE"].ToString().Length < 8)
                            break;

                        string strTemp = string.Empty;
                        strTemp = dt.Rows[0]["PREV_DATE"].ToString();
                        strFromDate = String.Format("{0}{1}{2}", strTemp.Substring(0, 4), strTemp.Substring(4, 2), strTemp.Substring(6, 2));

                        strTemp = dt.Rows[0]["POST_DATE"].ToString();
                        strToDate = String.Format("{0}{1}{2}", strTemp.Substring(0, 4), strTemp.Substring(4, 2), strTemp.Substring(6, 2));

                        break;
                    case "MONTH":
                        int year = FromYearMonth.Value.Year;
                        int month = FromYearMonth.Value.Month;

                        strFromDate = new DateTime(year, month, 1).AddDays(-1).ToString("yyyyMMdd");

                        year = ToYearMonth.Value.AddMonths(1).Year;
                        month = ToYearMonth.Value.AddMonths(1).Month;

                        strToDate = new DateTime(year, month, 1).AddDays(-1).ToString("yyyyMMdd");

                        break;
                }
            }
            _exactFromDate = strFromDate + "220000";
            _exactToDate = strToDate + "215959";
        }
        #endregion
	}
}