using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.PQC
{

    /// <summary>
    /// 클  래  스: PQC031001_P1<br/>
    /// 클래스요약: 정체 재공 현황 팝업<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2009-02-03<br/>
    /// 상세  설명: 정체 재공 현황 팝업<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PQC031001_P1 : Form
    {

        public PQC031001_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 팝업에서 보여줄 Title과 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="dt">해당 공정그룹의 정체 Lot 현황</param>
        public PQC031001_P1(string title, string Pattern, string Mindate, string title1)                                // 2010-08-27-안시홍 : title1 추가
        {
            InitializeComponent();

            this.Text = title;



            DataTable Data_dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", DataChart(Pattern, Mindate, title1));     // 2010-08-27-안시홍 : title1 추가
            DataTable Line_dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", LineChart(Mindate));

            int columnCount = 0;
            double max1 = 0;
            double max_temp = 0;

            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();


            // contion attribute 를 이용한 0 point label hidden
            SoftwareFX.ChartFX.ConditionalAttributes contion = udcChartFX1.ConditionalAttributes[0];
            contion.Condition.From = 0;
            contion.Condition.To = 0;
            contion.PointLabels = false;


            // 차트 설정
            udcChartFX1.RPT_3_OpenData(1, Data_dt.Rows.Count);
            int[] tat_columns = new Int32[Data_dt.Rows.Count];
            int[] columnsHeader = new Int32[Data_dt.Rows.Count];
            int[] dt_columns = new Int32[Data_dt.Rows.Count];


            for (int i = 0; i < dt_columns.Length; i++)
            {
                tat_columns[i] = 0 + i;
                dt_columns[i] = 0 + i;
                columnsHeader[i] = 0 + i;
            }

            max1 = udcChartFX1.RPT_4_AddData(Data_dt, tat_columns, new int[] { columnCount + 1 }, SeriseType.Column);
            max_temp = max1;
            max1 = udcChartFX1.RPT_4_AddData(Data_dt, dt_columns, new int[] { columnCount + 2 }, SeriseType.Column);
            if (max1 > max_temp)
            {
                max_temp = max1;
            }
            max1 = max_temp;

            udcChartFX1.RPT_5_CloseData();

            ////각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Curve, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Curve, 1, 1, "", AsixType.Y2, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.Series[0].Color = System.Drawing.Color.Red;
            udcChartFX1.Series[1].Color = System.Drawing.Color.Blue;
            udcChartFX1.Series[0].LineWidth = 2;                // 2010-08-10-안시홍 : 굵은 그래프 선(RED)으로 변경
            udcChartFX1.Series[1].LineWidth = 2;                // 2010-08-10-안시홍 : 굵은 그래프 선(BLUE)으로 변경


            //경과시간 세로Line 
            udcChartFX1.ConstantLines[0].Value = Convert.ToInt16(Line_dt.Rows[0][0].ToString());
            udcChartFX1.ConstantLines[0].Axis = SoftwareFX.ChartFX.AxisItem.X;
            udcChartFX1.ConstantLines[0].Width = 2;                                             // 2010-08-10-안시홍 : 굵은 세로 그래프 선(GREEN)으로 변경
            udcChartFX1.ConstantLines[0].Font = new System.Drawing.Font("Tahoma", 15.25F);      // 2010-08-10-안시홍 : 글자 굵게 변경.
            udcChartFX1.ConstantLines[0].Text = Line_dt.Rows[0][0].ToString() + "분";
            udcChartFX1.ConstantLines[0].Color = System.Drawing.Color.Green;

            //udcChartFX1.ConstantLines[1].Value = Convert.ToInt16(Line_dt.Rows[0][0].ToString());
            //udcChartFX1.ConstantLines[1].Axis = SoftwareFX.ChartFX.AxisItem.Y;
            //udcChartFX1.ConstantLines[1].Width = 1;
            //udcChartFX1.ConstantLines[1].Text = Line_dt.Rows[0][0].ToString() + "℃";
            //udcChartFX1.ConstantLines[1].Color = System.Drawing.Color.Green;



            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(Data_dt);
            udcChartFX1.Highlight.PointAttributes.PointLabelColor = System.Drawing.Color.Transparent;
            udcChartFX1.AxisY.Gridlines = true;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;

            // 2010-08-10-안시홍 : 가로축, 세로축 수정
            udcChartFX1.AxisX.Title.Text = "Minutes";
            udcChartFX1.AxisY.Title.Text = "Temperature (℃)";
            udcChartFX1.AxisX.Font = new System.Drawing.Font("Tahoma", 9.25F);
            udcChartFX1.AxisY.Font = new System.Drawing.Font("Tahoma", 9.25F);
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("Tahoma", 9.25F);
            udcChartFX1.AxisX.Step = 5;
            udcChartFX1.AxisY.Step = 5;
            udcChartFX1.AxisY2.Step = 5;
            udcChartFX1.AxisX.Min = 0;
            udcChartFX1.AxisY.Min = 20;
            udcChartFX1.AxisY.Max = 210;
            udcChartFX1.AxisY2.Min = 20;
            udcChartFX1.AxisY2.Max = 210;

            // 2010-08-30-안시홍 : 30분 간격의 세로 구분선 추가
            udcChartFX1.AxisX.MinorGridlines = true;
            udcChartFX1.AxisX.MinorStep = 30;

            // 2010-08-10-안시홍 : 범례 표시
            udcChartFX1.SerLeg[0] = "기준온도";
            udcChartFX1.SerLeg[1] = "현재온도";
            udcChartFX1.SerLegBox = true;
            udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;

        }

        private string DataChart(string Pattern, string Mindate, string title1)
        {
            /************************************************
             * comment : OVEN CHART를 위한 쿼리를 실행한다.
             * 
             * created by :
             * 
             * modified by : Si-hong An (2010-08-27-금요일)
             ************************************************/
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT OPER, MARK, REAL_DATA" + "\n");
            strSqlString.Append("   FROM(" + "\n");
            strSqlString.Append("       SELECT ROWNUM NUM, OPER, MARK" + "\n");
            strSqlString.Append("           FROM(" + "\n");
            strSqlString.Append("               SELECT OPER, DECODE(MARK, '', LAG(MARK) OVER (ORDER BY ROWNUM), MARK) AS MARK" + "\n");
            strSqlString.Append("                   FROM( " + "\n");
            strSqlString.Append("                       SELECT OPER, NVL(ROUND(FROM_TEMP+(TO_TEMP-FROM_TEMP)/TIME_GAP*(OPER-FROM_TIME)),TO_TEMP) AS MARK" + "\n");
            strSqlString.Append("                           FROM (" + "\n");
            strSqlString.Append("                               SELECT NVL(LAG(TIME)OVER(ORDER BY SEQUENCE),0) FROM_TIME, TIME TO_TIME, TIME_GAP, NVL(LAG(TEMP)OVER(ORDER BY SEQUENCE),25) FROM_TEMP, TEMP TO_TEMP" + "\n");
            strSqlString.Append("                                   FROM (" + "\n");
            strSqlString.Append("                                       SELECT SEQUENCE, SUM(TIME) OVER (ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME, TIME TIME_GAP,  TEMP" + "\n");
            strSqlString.Append("                                           FROM MRASOVNDEF@RPTTOMES" + "\n");
            strSqlString.Append("                                           WHERE 1=1" + "\n");
            strSqlString.Append("                                               AND PATTERN_NO = '" + Pattern + "'" + "\n");
            strSqlString.Append("                                   ) " + "\n");
            strSqlString.Append("                           ) DEF," + "\n");
            strSqlString.Append("                           (" + "\n");
            strSqlString.Append("                               SELECT OPER" + "\n");
            strSqlString.Append("                                   FROM (SELECT 0 AS OPER FROM DUAL UNION ALL" + "\n");
            strSqlString.Append("                                   SELECT LEVEL AS OPER FROM DUAL WHERE LEVEL >= 0 CONNECT BY LEVEL <= (" + "\n");
            strSqlString.Append("                                       SELECT MAX(TIME) AS TIME" + "\n");
            strSqlString.Append("                                           FROM(" + "\n");
            strSqlString.Append("                                               SELECT SUM(TIME)OVER(ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME" + "\n");
            strSqlString.Append("                                                   FROM MRASOVNDEF@RPTTOMES" + "\n");
            strSqlString.Append("                                                   WHERE 1=1" + "\n");
            strSqlString.Append("                                                       AND PATTERN_NO = '" + Pattern + "'" + "\n");
            strSqlString.Append("                                           )" + "\n");
            strSqlString.Append("                                       )" + "\n");
            strSqlString.Append("                                   )" + "\n");
            strSqlString.Append("                           ) TIME" + "\n");
            strSqlString.Append("                           WHERE (TIME.OPER >= DEF.FROM_TIME(+) and TIME.OPER < DEF.TO_TIME(+))" + "\n");
            strSqlString.Append("                   )" + "\n");
            strSqlString.Append("                   ORDER BY OPER" + "\n");
            strSqlString.Append("           )" + "\n");
            strSqlString.Append("   ) A," + "\n");
            strSqlString.Append("   (" + "\n");
            strSqlString.Append("       SELECT ROWNUM NUM, REAL_DATA" + "\n");
            strSqlString.Append("           FROM (" + "\n");
            strSqlString.Append("               SELECT TEMP AS REAL_DATA" + "\n");
            strSqlString.Append("                   FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.Append("                   WHERE 1=1" + "\n");
            strSqlString.Append("                       AND LOT_ID = (" + "\n");
            strSqlString.Append("                           SELECT MAX(LOT_ID) FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.Append("                               WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND TEMP_TIME = '" + Mindate + "'" + "\n");
            strSqlString.Append("                                   AND RES_ID = '" + title1 + "'" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                       AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                       AND RES_ID = '" + title1 + "'" + "\n");
            strSqlString.Append("                   ORDER BY REAL_DATA ASC" + "\n");
            strSqlString.Append("           )" + "\n");
            strSqlString.Append("   ) B" + "\n");
            strSqlString.Append("   WHERE A.NUM = B.NUM(+)" + "\n");
            strSqlString.Append("   ORDER BY OPER" + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        private string LineChart(string Mindate)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("         SELECT MAX(OPER)" + "\n");
            strSqlString.AppendFormat("         FROM(" + "\n");
            strSqlString.AppendFormat("             SELECT (TRUNC(MOD((TO_DATE(TEMP_TIME,'YYYYMMDDHH24MISS')-TO_DATE('" + Mindate + "','YYYYMMDDHH24MISS')),1)*24)*60) +" + "\n");
            strSqlString.AppendFormat("                 (TRUNC(MOD((TO_DATE(TEMP_TIME,'YYYYMMDDHH24MISS')-TO_DATE('" + Mindate + "','YYYYMMDDHH24MISS'))*24,1)*60)) AS OPER" + "\n");
            strSqlString.AppendFormat("             FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.AppendFormat("             WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                 AND LOT_ID = (" + "\n");
            strSqlString.AppendFormat("                     SELECT MAX(LOT_ID) FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.AppendFormat("                     WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                         AND TEMP_TIME ='" + Mindate + "'" + "\n");
            strSqlString.AppendFormat("                 )" + "\n");
            strSqlString.AppendFormat("             )" + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        //private string DataProgress(string Pattern, string Mindate)
        //{
        //    StringBuilder strSqlString = new StringBuilder();

        //    strSqlString.AppendFormat("         SELECT MAX(OPER)" + "\n");
        //    strSqlString.AppendFormat("         FROM(" + "\n");
        //    strSqlString.AppendFormat("             SELECT (TRUNC(MOD((TO_DATE(TEMP_TIME,'YYYYMMDDHH24MISS')-TO_DATE('" + Mindate + "','YYYYMMDDHH24MISS')),1)*24)*60) +" + "\n");
        //    strSqlString.AppendFormat("                 (TRUNC(MOD((TO_DATE(TEMP_TIME,'YYYYMMDDHH24MISS')-TO_DATE('" + Mindate + "','YYYYMMDDHH24MISS'))*24,1)*60)) AS OPER" + "\n");
        //    strSqlString.AppendFormat("             FROM MRASLOTOVN@RPTTOMES" + "\n");
        //    strSqlString.AppendFormat("             WHERE 1=1" + "\n");
        //    strSqlString.AppendFormat("                 AND LOT_ID = (" + "\n");
        //    strSqlString.AppendFormat("                     SELECT MAX(LOT_ID) FROM MRASLOTOVN@RPTTOMES" + "\n");
        //    strSqlString.AppendFormat("                     WHERE 1=1" + "\n");
        //    strSqlString.AppendFormat("                         AND TEMP_TIME ='" + Mindate + "'" + "\n");
        //    strSqlString.AppendFormat("                 )" + "\n");
        //    strSqlString.AppendFormat("             )" + "\n");
        //    strSqlString.AppendFormat("             UNION ALL" + "\n");
        //    strSqlString.AppendFormat("             SELECT MAX(TIME) TO_TIME" + "\n");
        //    strSqlString.AppendFormat("             FROM (" + "\n");
        //    strSqlString.AppendFormat("                 SELECT SUM(TIME)OVER(ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME" + "\n");
        //    strSqlString.AppendFormat("                     FROM MRASOVNDEF@RPTTOMES" + "\n");
        //    strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
        //    strSqlString.AppendFormat("                     AND PATTERN_NO='" + Pattern + "'" + "\n");
        //    strSqlString.AppendFormat("             )" + "\n");


        //    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
        //    return strSqlString.ToString();
        //}

    }
}