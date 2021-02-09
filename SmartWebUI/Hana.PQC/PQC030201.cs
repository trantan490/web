using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

using System.Windows.Forms.DataVisualization.Charting;

namespace Hana.PQC
{
    public partial class PQC030201 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        
        DACrux.SPC.Control.ucFastSPCChart oro = null;

        //DataTable dt = new DataTable();       //임시 주석처리
        DataTable dtview = new DataTable();

        //private DataTable dtX;       // Xbar용
        //private DataTable dtR;       // R용

        //private double dAvg;
        //private double dSt;
        //private double dUcl;
        //private double dLcl;
        //private double dUsl;
        //private double dLsl;
        //private cSpc spc = new cSpc();   //수학 함수 모아둔 Class선언.

        //Series series = null;

        #region " PQC030201 : Program Initial "

        /// <summary>
        /// 클  래  스: PQC030201<br/>
        /// 클래스요약: Xbar - R Chart<br/>
        /// 작  성  자: 장은희<br/>
        /// 최초작성일: 2009-09-10 <br/>
        /// 상세  설명: SPC Xbar - R Chart<br/>
        /// 변경  내용: 
        /// 변 경 자 :  <br />
        /// 2011-11-21-임종우 : CHART ID, 설비 검색 조건 추가 함.
        /// 2011-11-24-임종우 : 엑셀 다운로드 기능 추가, 엑셀 다운로드 용 스프레드 추가.
        /// </summary>
        /// 
        public PQC030201()
        {
            InitializeComponent();            
            udcFromToDate.AutoBinding();
            ////차트 초기화. 
            //udcChartFXSpc1.RPT_1_ChartInit();
            //udcChartFXSpc2.RPT_1_ChartInit();

            // 기본공정은 HMKA1으로 설정
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvRasId.sFactory = cdvFactory.Text;
        }

        #endregion


        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvChartID.Text == "ALL")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD042", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvCharID.Text == "ALL")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD043", GlobalVariable.gcLanguage));
                return false;
            }
            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 " : SPREAD가 없으니 필요없음

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();

            try
            {
                spdData.RPT_AddBasicColumn("CHART ID", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("TRAN TIME", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LOT ID", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("RES ID", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("X-Bar", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("STDDEV", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("RANGE", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("MIN", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("MAX", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("USL", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LSL", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        #endregion


        #region " SortInit : Group By 설정 " : SPREAD가 없으니 필요없다.

        /// <summary>
        /// 3. Group By 정의 
        /// </summary>
        private void SortInit()
        {

        }

        #endregion


        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string[] selectDate1 = new string[udcFromToDate.SubtractBetweenFromToDate + 1];
            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;
            string strDate = string.Empty;

            int Between = udcFromToDate.SubtractBetweenFromToDate + 1;

            selectDate1 = udcFromToDate.getSelectDate();

            switch (udcFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strDate = "WORK_DATE";
                    break;
                case "WEEK":
                    strDate = "WORK_WEEK";
                    break;
                case "MONTH":
                    strDate = "WORK_MONTH";
                    break;
                default:
                    strDate = "WORK_DATE";
                    break;
            }
            
            // 쿼리 

            strSqlString.Append("SELECT LOT_ID, TRAN_TIME, CHAR_ID" + " \n");
            strSqlString.Append("     , VALUE_1 , VALUE_2, VALUE_3, VALUE_4, VALUE_5, VALUE_6, VALUE_7, VALUE_8, VALUE_9, VALUE_10, VALUE_11, VALUE_12, VALUE_13 " + "\n");
            strSqlString.Append("     , VALUE_14, VALUE_15, VALUE_16, VALUE_17, VALUE_18, VALUE_19, VALUE_20, VALUE_21, VALUE_22, VALUE_23, VALUE_24, VALUE_25 " + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT EDC.LOT_ID, EDC.CHAR_ID, SUBSTR(EDC.TRAN_TIME,0,8) AS TRAN_TIME" + " \n");
            strSqlString.Append("             , EDC.VALUE_1 , EDC.VALUE_2, EDC.VALUE_3, EDC.VALUE_4, EDC.VALUE_5, EDC.VALUE_6, EDC.VALUE_7, EDC.VALUE_8 " + "\n");
            strSqlString.Append("             , EDC.VALUE_9, EDC.VALUE_10, EDC.VALUE_11, EDC.VALUE_12, EDC.VALUE_13, EDC.VALUE_14, EDC.VALUE_15, EDC.VALUE_16  " + "\n");
            strSqlString.Append("             , EDC.VALUE_17, EDC.VALUE_18, EDC.VALUE_19, EDC.VALUE_20, EDC.VALUE_21, EDC.VALUE_22, EDC.VALUE_23, EDC.VALUE_24, EDC.VALUE_25  " + "\n");
            strSqlString.Append("          FROM MESMGR.MEDCLOTDAT@RPTTOMES EDC " + "\n");
            strSqlString.Append("             , MESMGR.MWIPMATDEF@RPTTOMES WIP " + "\n");
            strSqlString.Append("             , MESMGR.MSPCCHTDEF@RPTTOMES CHT " + "\n");
            strSqlString.Append("         WHERE 1 = 1" + "\n");
            strSqlString.Append("           AND EDC.FACTORY = WIP.FACTORY  " + "\n");
            strSqlString.Append("           AND EDC.MAT_ID = WIP.MAT_ID " + "\n");
            strSqlString.Append("           AND EDC.FACTORY = CHT.FACTORY " + "\n");
            strSqlString.Append("           AND EDC.COL_SET_ID = CHT.COL_SET_ID " + "\n");
            strSqlString.Append("           AND EDC.CHAR_ID = CHT.CHAR_ID " + "\n");
            strSqlString.Append("           AND EDC.FACTORY = '" + cdvFactory.Text.ToString() + "' " + "\n");
            strSqlString.Append("           AND EDC.TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
            strSqlString.Append("           AND WIP.MAT_VER = 1 " + "\n");
            strSqlString.Append("           AND EDC.CHAR_ID " + cdvCharID.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND EDC.VALUE_1 <> ' '  " + "\n");
            strSqlString.Append("           AND EDC.MEAS_RES_ID " + cdvRasId.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND EDC.COL_SET_ID IN (SELECT COL_SET_ID FROM MEDCCOLDEF WHERE   1=1  AND FACTORY = '" + cdvFactory.Text.ToString() + "'  AND COL_GRP_10='Measure' AND DELETE_FLAG <> 'Y' )  " + "\n");
            strSqlString.Append("           AND CHT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND CHT.CHART_ID " + cdvChartID.SelectedValueToQueryString + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND WIP.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND WIP.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND WIP.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND WIP.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND WIP.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND WIP.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND WIP.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND WIP.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND WIP.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion
            
            strSqlString.Append("       )  " + "\n");

            //// [2009-09-04,장] 속도가 느린 관계로 잠시 보류중,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,보류안함, 보류하면 안될듯..,,,,,,,[0908]
            strSqlString.Append(" ORDER BY TRAN_TIME, LOT_ID" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // 2011-11-24-임종우 : 엑셀 다운로드용 쿼리
        private string MakeSqlString2()
        {
            StringBuilder strSqlString = new StringBuilder();

            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;

            // 쿼리 
            strSqlString.Append("SELECT CHART_ID, TRAN_TIME, LOT_ID, A.MAT_ID, B.MAT_GRP_3, B.MAT_GRP_6, RES_ID, AVERAGE, STDDEV, RANGE,  MIN_VALUE, MAX_VALUE, USL, LSL" + "\n");
            strSqlString.Append("  FROM (       " + "\n");
            strSqlString.Append("        SELECT A.*" + "\n");
            strSqlString.Append("             , (SELECT MAT_ID FROM RWIPLOTSTS WHERE LOT_ID = A.LOT_ID) AS MAT_ID" + "\n");
            strSqlString.Append("          FROM MESMGR.MSPCCALDAT@RPTTOMES A " + "\n");
            strSqlString.Append("             , MESMGR.MWIPCALDEF@RPTTOMES B" + "\n");
            strSqlString.Append("         WHERE 1=1         " + "\n");
            strSqlString.Append("           AND SUBSTR(TRAN_TIME, 1, 8) = B.SYS_DATE" + "\n");
            strSqlString.Append("           AND B.CALENDAR_ID = 'HM'" + "\n");
            strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("           AND CHART_ID " + cdvChartID.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
            strSqlString.Append("           AND RES_ID " + cdvRasId.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND VALUE_COUNT = 10" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , MWIPMATDEF B" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND B.MAT_TYPE = 'FG'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND WIP.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND WIP.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND WIP.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND WIP.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND WIP.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND WIP.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND WIP.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND WIP.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND WIP.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append(" ORDER BY CHART_ID, TRAN_TIME, LOT_ID" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion



        #region " MakeChart : Chart 처리 "

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="rowCount">Row Number</param>
        //private void ShowChart(int rowCount)
        private void ShowChart()
        {
            // Xbar-Chart 구현 시
            //udcChartFXSpc1.RPT_1_ChartInit();
            //udcChartFXSpc1.RPT_2_ClearData();
            //udcChartFXSpc1.RPT_10_Xbar_GetData(dtview, 3);    //(dt, 3)
                        
            //Chart_Xbar_SetData(dtview, 3);

           //R Chart 구현 시
            //udcChartFXSpc2.RPT_1_ChartInit();
            //udcChartFXSpc2.RPT_2_ClearData();
            //udcChartFXSpc2.RPT_10_R_GetData(dtview, 3); //(dt, 3)
            
            //Chart_R_SetData(dtview, 3);

            oro.DataSource = dtview;
            oro.DrawSPC();
        }


        //private void Chart_Xbar_SetData(DataTable a_dt, int iValue1)
        //{
        //    //Series series = null;
        //    chart1.Series.Clear();


        //    int iValue25 = iValue1 + 25;  // Value_25까지 있기 때문에,,            

        //    DataTable dt = new DataTable();   // 새로운 DataTable dt를 만들어서
        //    dt = a_dt;                                       // dt에다가 받아온 a_dt 를 넣어준다.
        //    dt.Columns.Add("AVG", typeof(String));         // 각 ROW별로 VALUE_1~25까지 컬럼값의 평균을 넣어줄것이다.

        //    //1. 평균 구하기(Xbar)            
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        spc.clear();

        //        for (int j = iValue1; j < iValue25; j++)
        //        {
        //            if (dt.Rows[i][j].Equals(" ") || dt.Rows[i][j].Equals(""))        // 값이 없으면 그 뒤로도 없기 때문에 
        //            {
        //                break;
        //            }
        //            else
        //            {
        //                double x = Convert.ToDouble(dt.Rows[i][j]);     // x에 컬럼값들 대입한다음에
        //                spc.Add(x);           //spc.Add에 x값을 넣어주면 spc.clear() 호출하지 않는 이상 계속 축적되어 더해지고(sum), 자기네들끼리 비교해서 min,max값 구해놓고 있다,.
        //            }
        //        }
        //        dt.Rows[i]["AVG"] = spc.mean();    // 각 row별 (value1~25까지의) 평균을 넣어준다.          
        //    }

        //    // 2. 표준편차 구하기 : 표준편차는 모든 값들에(dt에 있는)   대한 표준편차를 구한다,(여동욱k께 문의한 결과)
        //    spc.clear();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        for (int j = iValue1; j < iValue25; j++)
        //        {
        //            if (dt.Rows[i][j].Equals(" ") || dt.Rows[i][j].Equals(""))
        //            {
        //                break;
        //            }
        //            else
        //            {
        //                double x = Convert.ToDouble(dt.Rows[i][j]);
        //                spc.Add(x);
        //            }
        //        }
        //    }
        //    dSt = spc.stdev();   // 표준편차 구해서(spc.stdev()) dSt 에 넣어준다.


        //    //3. 새로운 DataTabe(dtX)를 생성해준다.
        //    dtX = new DataTable();

        //    if (dtX.Columns.Count == 0) // X-bar용
        //    {
        //        // 새로운 Datatable에 넣을 column도 정의하고            
        //        DataColumn dc = new DataColumn();
        //        dc.DataType = System.Type.GetType("System.String");
        //        dc.ColumnName = "LOT_ID";
        //        dc.Caption = "LOT_ID";
        //        dc.DefaultValue = "";
        //        dtX.Columns.Add(dc);

        //        DataColumn dd = new DataColumn();
        //        dd.DataType = System.Type.GetType("System.Double");
        //        dd.ColumnName = "AVG";
        //        dd.Caption = "AVG";
        //        dd.DefaultValue = 0.00;
        //        dtX.Columns.Add(dd);
        //    }


        //    // 4. 1번에서 구한 각Row들의 평균을 날짜별로 Group By 해서 각각 총 평균을 구해서 DATE, AVG 컬럼만 뽑아서
        //    //     새로운 DataTable(dtX)를 생성해서 넣어준다.

        //    // TranTime 기준을 Lot ID 기준으로 변경 - Xbar Chart 기준 변경..여동욱K요청(2009.09.28 임종우)
        //    spc.clear();    // 수학관련해서 모두 초기화하고
        //    string sLot = " ";
        //    int k = 0;

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (!sLot.Equals(dt.Rows[i][0]))
        //        {
        //            if (i == 0) // 만약 맨 첫줄이라면
        //            {
        //                //dtX.Rows.Add();           // 아무일도 안한다.
        //            }
        //            else if (i != 0)
        //            {
        //                dtX.Rows.Add();                         // row한줄 추가하고
        //                dtX.Rows[k]["LOT_ID"] = sLot;        //  바로 전의 값들을  새로운 DataTable dtX에 넣어준다. Lot ID넣어주고
        //                dtX.Rows[k]["AVG"] = spc.mean();         // 평균넣어주고
        //                spc.clear();                           // 수학관련해서 초기화해주고: 왜냐하면 다음 번 값들을 새로 받아야 하니깐
        //                k++;
        //            }
        //            sLot = Convert.ToString(dt.Rows[i][0]);                 // sLot 에 해당 컬럼값(LOT ID) 대입하고
        //            double x = Convert.ToDouble(dt.Rows[i]["AVG"]);
        //            spc.Add(x);
        //        }
        //        else
        //        {
        //            if (i == dt.Rows.Count - 1)
        //            {
        //                dtX.Rows.Add();
        //                dtX.Rows[k]["LOT_ID"] = sLot;
        //                dtX.Rows[k]["AVG"] = spc.mean();
        //            }
        //            else
        //            {
        //                double x = Convert.ToDouble(dt.Rows[i]["AVG"]);
        //                spc.Add(x);
        //            }
        //        }
        //    }

        //    // Chart에 뿌려주기 위해서 모든 AVG값들의 평균을 구하고 2번에서 구한 표준편차를 이용해서 Chart에 넘겨줄 UCL, LCL, USL, LSL을 구한다.

        //    if (dtX.Rows.Count != 0)
        //    {
        //        spc.clear();
        //        dAvg = 0.0;

        //        for (int i = 0; i < dtX.Rows.Count; i++)
        //        {
        //            double x = Convert.ToDouble(dtX.Rows[i]["AVG"]);
        //            spc.Add(x);
        //        }
        //        dAvg = Math.Round(spc.mean(), 2);

        //        dUcl = Math.Round(dAvg + (3 * dSt), 2);
        //        dLcl = Math.Round(dAvg - (3 * dSt), 2);
        //        dUsl = Math.Round(dAvg + (4 * dSt), 2);
        //        dLsl = Math.Round(dAvg - (4 * dSt), 2);


        //        // Chart 그리고
        //        //RPT_3_1_SetData(dtX, "X-bar Chart");

        //        //DataTable dtNew = new DataTable();   // 새로운 DataTable dt를 만들어서
        //        //dtNew = dtX;                         // dt에다가 받아온 a_dt 를 넣어준다.

        //        // MS 차트 데이터 바인딩 로직 넣어야함...
        //        //this.DataSource = dtNew;
        //        //this.AxisX.Title.Text = sTitle;

        //        DataTable dtNew = new DataTable();
        //        dtNew = dtX;

        //        series = new Series();
        //        series.ChartType = SeriesChartType.Line;
        //        series.IsValueShownAsLabel = true;
        //        series.IsVisibleInLegend = false;

        //        chart1.Series.Add(series);

        //        for (int i = 0; i < dtNew.Rows.Count; i++)
        //        {
        //            chart1.Series[0].Points.AddXY(dtNew.Rows[i][0].ToString(), Convert.ToDouble(dtNew.Rows[i][1]));
        //        }

        //        //chart1.Series[0].YAxisType = AxisType.Secondary;
                
        //        chart1.Series[0].Color = Color.Black;
        //        chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
        //        chart1.Series[0].MarkerSize = 10;
        //        chart1.Series[0].BorderWidth = 3;

        //        //chart1.ChartAreas[0].CursorX.AutoScroll = true;
        //        chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
        //        chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

        //        //chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
        //        //chart1.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Number;

        //        chart1.ChartAreas[0].AxisX.ScrollBar.Size = 10;               
        //        chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
                
        //        chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
        //        chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
        //        chart1.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;

        //        chart1.ChartAreas[0].AxisY.Maximum = dUsl;
        //        chart1.ChartAreas[0].AxisY.Minimum = dLsl;

        //        chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
        //        chart1.ChartAreas[0].AxisY2.Minimum = 0;
        //        chart1.ChartAreas[0].AxisY2.Maximum = 8;          

        //        chart1.ChartAreas[0].AxisY2.CustomLabels.Add(dLcl, dLcl, "LCL=" + "\n" + dLcl);               
        //        //this.AxisY2.Label[1] = "LCL=" + "\n" + dLcl;


        //        /*                
        //        StripLine stripLow = new StripLine();
        //        stripLow.IntervalOffset = 1;
        //        stripLow.StripWidth = 6;
        //        stripLow.BackColor = Color.Red;

        //        StripLine stripMed = new StripLine();
        //        stripMed.IntervalOffset = 2;
        //        stripMed.StripWidth = 4;
        //        stripMed.BackColor = Color.Yellow;

        //        StripLine stripHigh = new StripLine();
        //        stripHigh.IntervalOffset = 3;
        //        stripHigh.StripWidth = 2;
        //        stripHigh.BackColor = Color.Chartreuse;

        //        StripLine stripCL = new StripLine();
        //        stripCL.IntervalOffset = 4;
        //        stripCL.StripWidth = 0;
        //        stripCL.BorderColor = Color.DimGray;
        //        stripCL.BorderWidth = 1;
                
        //        chart1.ChartAreas[0].AxisY2.StripLines.Add(stripLow);
        //        chart1.ChartAreas[0].AxisY2.StripLines.Add(stripMed);
        //        chart1.ChartAreas[0].AxisY2.StripLines.Add(stripHigh);
        //        chart1.ChartAreas[0].AxisY2.StripLines.Add(stripCL);
        //        */

        //        //chart1.ChartAreas[0].AxisY2.CustomLabels.Add(0, 1, "LCL=" + "\n" + dLcl);


        //        //RPT_3_2_SetColor(dLcl, dAvg, dUcl, dUsl, dLsl);    // 색깔 칠해주고
        //        /*
        //        // Y2 좌표를 총 8등분해서 나타낸다(나중에 색깔 칠하고 값넣기 편하게)
        //        this.AxisY2.Min = 0;
        //        this.AxisY2.Max = 8;
        //        this.AxisY2.Step = 1;

        //        //////// 색깔칠하기
        //        this.Series[0].Color = System.Drawing.Color.Black;

        //        SoftwareFX.ChartFX.AxisSection section1 = this.AxisY2.Sections[0];
        //        section1.From = 1;
        //        section1.To = 7;
        //        section1.BackColor = System.Drawing.Color.Red;

        //        SoftwareFX.ChartFX.AxisSection section2 = this.AxisY2.Sections[1];
        //        section2.From = 2;
        //        section2.To = 6;
        //        section2.BackColor = System.Drawing.Color.Yellow;

        //        SoftwareFX.ChartFX.AxisSection section3 = this.AxisY2.Sections[2];
        //        section3.From = 3;
        //        section3.To = 5;
        //        section3.BackColor = System.Drawing.Color.Chartreuse;

        //        // LCL, CL, UCL 값에 가져온 값들과 함께 하기와 같이 표기한다.
        //        this.AxisY2.Label[1] = "LCL=" + "\n" + dLcl;
        //        this.AxisY2.Label[4] = "CL=" + "\n" + dCl;
        //        this.AxisY2.Label[7] = "UCL=" + "\n" + dUcl;

        //        //////CL가운데 선긋기 : 평균값을 보여주기 위해서,,,근데 0일때,,,,,,,,도 같이 줄이 보이니,,,,,,,,문제점 발견<<<해결요망//////
        //        SoftwareFX.ChartFX.ConstantLine constantLine = this.ConstantLines[0];
        //        constantLine.Value = 4;
        //        constantLine.Color = System.Drawing.Color.DimGray;
        //        constantLine.Axis = SoftwareFX.ChartFX.AxisItem.Y2;

        //        // Y축의 min과 max값도 정해주고 보여줄 필요 없기 때문에 visible=false 했음
        //        this.AxisY.Max = dUsl;
        //        this.AxisY.Min = dLsl;
        //        this.AxisY.Visible = false;
        //        */
        //    }
        //}

        /*
        private void Chart_R_SetData(DataTable a_dt, int iValue1)
        {
            chart2.Series.Clear();


            int iValue25 = iValue1 + 25;

            DataTable dt = new DataTable();   // 새로운 DataTable dt를 만들어서
            dt = a_dt;                                       // dt에다가 받아온 a_dt 를 넣어준다.
            dt.Columns.Add("MIN", typeof(String));
            dt.Columns.Add("MAX", typeof(String));


            //1. 각 Row 별로 MIN, MAX 값 구하기    
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                spc.clear();

                for (int j = iValue1; j < iValue25; j++)
                {
                    if (dt.Rows[i][j].Equals(" ") || dt.Rows[i][j].Equals(""))
                    {
                        break;
                    }
                    else
                    {
                        double x = Convert.ToDouble(dt.Rows[i][j]);
                        spc.Add(x);
                    }

                }
                dt.Rows[i]["MIN"] = spc.min();
                dt.Rows[i]["MAX"] = spc.max();
            }


            //2. 새로운 DataTabe(dtR)를 생성해준다.
            dtR = new DataTable();

            if (dtR.Columns.Count == 0) // R-bar용
            {
                // 새로운 Datatable에 넣을 column도 정의하고            
                DataColumn dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "LOT_ID";
                dc.Caption = "LOT_ID";
                dc.DefaultValue = "";
                dtR.Columns.Add(dc);

                DataColumn dd = new DataColumn();
                dd.DataType = System.Type.GetType("System.Double");
                dd.ColumnName = "R";
                dd.Caption = "R";
                dd.DefaultValue = 0.00;
                dtR.Columns.Add(dd);
            }

            // 3. 1번에서 구한 각Row들의 MIN, MAX 값 가지고 LOT_ID로 Group By 해서 각각 총 평균을 구해서 LOT_ID, R값(MAX-MIN) 을
            //     새로운 DataTable(dtR)를 생성해서 넣어준다.

            spc.clear();    // 수학관련해서 모두 초기화하고
            string sLot = " ";
            int k = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (!sLot.Equals(dt.Rows[i][0]))
                {
                    if (i == 0) // 만약 맨 첫줄이라면
                    {
                        //  dtR.Rows.Add();
                        //   아무일도 안한다.
                    }
                    else if (i != 0)
                    {
                        dtR.Rows.Add();
                        dtR.Rows[k]["LOT_ID"] = sLot;       //   바로 전의 값들을  새로운 DataTable dtR에 넣어준다. LOT_ID넣어주ㅗㄱ
                        dtR.Rows[k]["R"] = spc.gap();       //   max-min값넣어주고
                        spc.clear();                          //  수학관련해서 초기화해주고: 왜냐하면 다음 번 값들을 새로 받아야 하니깐
                        k++;
                    }
                    sLot = Convert.ToString(dt.Rows[i][0]);               //   sLot 에 해당 컬럼값(LOT_ID) 대입하고



                    for (int s = dt.Columns.IndexOf("MIN") - 1; s < dt.Columns.IndexOf("MIN") + 1; s++)
                    {
                        double x = Convert.ToDouble(dt.Rows[i][s]);
                        spc.Add(x);                                                         //        x값들이 모아져서 sum......
                    }
                }
                else
                {
                    if (i == dt.Rows.Count - 1)
                    {
                        dtR.Rows.Add();
                        dtR.Rows[k]["LOT_ID"] = sLot;
                        dtR.Rows[k]["R"] = spc.gap();
                    }
                    else
                    {
                        for (int s = dt.Columns.IndexOf("MIN") - 1; s < dt.Columns.IndexOf("MIN") + 1; s++)
                        {
                            double x = Convert.ToDouble(dt.Rows[i][s]);
                            spc.Add(x);                                                           //      x값들이 모아져서 sum......
                        }
                    }
                }

            }

            //RPT_3_1_SetData(dtR, "R Chart");

            DataTable dtNew = new DataTable();
            dtNew = dtR;

            series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.IsValueShownAsLabel = true;
            series.IsVisibleInLegend = false;

            chart2.Series.Add(series);

            for (int i = 0; i < dtNew.Rows.Count; i++)
            {
                chart2.Series[0].Points.AddXY(dtNew.Rows[i][0].ToString(), Convert.ToDouble(dtNew.Rows[i][1]));
            }

            chart2.Series[0].MarkerStyle = MarkerStyle.Circle;
            chart2.Series[0].MarkerSize = 10;
            chart2.Series[0].BorderWidth = 3;

            chart2.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].AxisX.ScrollBar.Size = 10;
            chart2.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;


            chart2.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.ChartAreas[0].AxisY.IsInterlaced = true;
            chart2.ChartAreas[0].AxisY.InterlacedColor = Color.FromArgb(80, Color.DarkGray);

            //this.AxisY.Min = 0;              // R Chart는 무조건 Y축 최소값이 0
            //this.AxisY.Gridlines = true;
        }*/

        #endregion


        #region " Button Event "
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void btnView_Click(object sender, EventArgs e)
        {
            if (CheckField() == false) return;
            LoadingPopUp.LoadIngPopUpShow(this);
            //dt = null;
            dtview = null;

            try
            {
                this.Refresh();
                //LoadingPopUp.LoadIngPopUpShow(this);

                GridColumnInit();

                dtview = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                //dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dtview.Rows.Count == 0) //dt.Rows.Count == 0
                {
                    dtview.Dispose();   //dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    //udcChartFXSpc1.RPT_1_ChartInit();
                    //udcChartFXSpc1.RPT_2_ClearData();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }
                //// 값을 뿌려주기 위해서 새로운 Chart를 불러낸다
                ShowChart();

                dtview = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());
                //dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());
                
                spdData.DataSource = dtview;
                //spdData.DataSource = dt;
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            spdData.ExportExcel();
        }

        #endregion

        private void cdvChartID_ValueButtonPress(object sender, EventArgs e)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT DISTINCT CHART_ID Code, CHART_DESC Data " + "\n");
            strSqlString.Append("  FROM MSPCCHTDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("   AND LOT_RES_FLAG = 'L' " + "\n");
            strSqlString.Append("   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND SYNC_EDC_FLAG = 'Y' " + "\n");            
            strSqlString.Append(" ORDER BY CHART_ID, CHART_DESC " + "\n");

            cdvChartID.sDynamicQuery = strSqlString.ToString();
        }


        //20150316 수정중
        #region " SPC용 수학공식 "
        /// <summary>
        /// Created by Eunhi Chang [2009-09-10]
        ///    - SPC Chart를 구현하기 위해서 필요한 수학공식들 모아둔다.
        /// </summary>

        // SPC CHART를 구현하기 위해서 필요한 수학함수 
        //  - SUM, MIN, MAX, STDEV(표준편차), GAP(MAX-MIN), MEAN(평균), N(COUNT)
        //public class cSpc
        //{
        //    private double sum, sum2, cmax, cmin;
        //    private int n;

        //    public cSpc()
        //    {
        //        clear();
        //    }

        //    public void clear()   // 초기화
        //    {
        //        n = 0;
        //        cmin = 0;
        //        cmax = 0;
        //        sum = 0.00;
        //        sum2 = 0.00;
        //    }

        //    public void Add(double x)        // 값이 들어오면 초기화 전까지는 sum하고 min값과max값들을 비교하면서 구한다.
        //    {
        //        n++;
        //        sum += x;
        //        sum2 += x * x;

        //        if (cmax < x)
        //        {
        //            cmax = x;
        //        }
        //        if (cmin == 0)
        //        {
        //            cmin = x;
        //        }
        //        else if (cmin > x)
        //        {
        //            cmin = x;
        //        }
        //        return;
        //    }

        //    public Double min()     // 들어온 값들  중에 min값 return
        //    {
        //        return cmin;
        //    }

        //    public Double max()        // 들어온 값들  중에 max값 r
        //    {
        //        return cmax;
        //    }

        //    // 수집된 데이터의 평균을 리턴
        //    public double mean()
        //    {
        //        return sum / (double)n;
        //    }

        //    // 모집단 데이터인 경우의 표준편차 리턴

        //    public double stdevp()
        //    {
        //        return Math.Sqrt(((double)n * sum2 - sum * sum) / ((double)n * (double)n));
        //    }

        //    // 모집단에서 추출된 샘플 데이터인 경우의 표준편차 리턴
        //    public double stdev()
        //    {
        //        return Math.Sqrt(((double)n * sum2 - sum * sum) / ((double)n * (double)(n - 1)));
        //    }

        //    public double gap()        // R-Chart를 구현하기 위한 R(max값 - min값) 을 구해준다.
        //    {
        //        return (cmax - cmin);
        //    }
        //}
        #endregion

        private void PQC030201_Load(object sender, EventArgs e)
        {
            
            oro = new DACrux.SPC.Control.ucFastSPCChart();

            oro.Dock = DockStyle.Fill;
            
            Panel p = new Panel();
            p.Controls.Add((UserControl)oro);
            p.Dock = DockStyle.Fill;

            this.splitContainer1.Panel1.Controls.Add(p);
        }


    }
}
