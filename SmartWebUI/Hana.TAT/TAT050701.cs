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

namespace Hana.TAT
{
    public partial class TAT050701 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private String startDay = null;
        private String endDay = null;
        private String sFrom = null;
        private String stPkg = null;
        private String stOper = null;
        private DataTable dtPkg = null;
        private ListView sPkgList = new ListView();
        private String stCheckdData = null;    // 투입대기, 출하대기 체크 선택 유무에 따라 Data 쿼리 저장
        private String stCheckdObject = null;  // 투입대기, 출하대기 체크 선택 유무에 따라 목표 쿼리 저장
        private String stLotType = null;
        private int pkgLength = 0;

        /// <summary>
        /// 클  래  스: TAT050701<br/>
        /// 클래스요약: 그룹별 TAT<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-02-09<br/>
        /// 상세  설명: 공정 그룹별 TAT trend 를 조회한다.<br/>
        /// 변경  내용: <br/>
        /// 2010-07-28-임종우 : 공정기준 변경 - 'S/P', 'SMT' 공정 기존 'SAW'에서 'D/A' 로 변경 (임태성 요청)
        /// 2010-11-18-임종우 : LOT TYPE 별 검색 가능하도록 수정 (민재훈 요청)
        /// 2011-01-04-임종우 : SAW 공정그룹은 QC_GATE2 와 SAW 공정 분리 함 (임태성 요청)
        /// 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
        /// 2011-08-26-김민우 : MCP Stack 구분 추가 표시 (임태성K 요청)
        /// </summary>
        public TAT050701()
        {
            InitializeComponent();

            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            //SortInit();
            cdvDate.Value = DateTime.Now.AddDays(-1);
            GridColumnInit(); //헤더 한줄짜리 
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화   
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cbLotType.Text = "ALL";
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "MAT_GRP_1", "MAT.MAT_GRP_1", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT_GRP_2", "MAT.MAT_GRP_2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT_GRP_3", "MAT.MAT_GRP_3", true);
            // ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Depart", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME='H_DEPARTMENT' AND KEY_1 = OPER_GRP_2 AND ROWNUM=1), '-') AS TEAM", "OPER_GRP_2", "OPER_GRP_2", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT_GRP_4", "MAT.MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT_GRP_5", "MAT.MAT_GRP_5", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT_GRP_6", "MAT.MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT_GRP_7", "MAT.MAT_GRP_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT_GRP_8", "MAT.MAT_GRP_8", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT_CMF_10", "MAT.MAT_CMF_10", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT_ID", "MAT.MAT_ID", false);
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            try
            {
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("GROUP", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);

                if (cdvFactory.Text != "")
                {
                    // PKG 선택하였을때 선택한 PKG 이용
                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    {
                        for (int i = 0; i < sPkgList.Items.Count; i++)
                        {
                            spdData.RPT_AddBasicColumn(sPkgList.Items[i].Text, 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("goal", 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            spdData.RPT_AddBasicColumn("Cumulative monthly", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            spdData.RPT_AddBasicColumn("Daily Performance", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);

                            spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.Columns.Count - 3, 3);
                        }

                        //spdData.RPT_AddDynamicColumn(udcWIPCondition3, true, new string[] { "목표", "월누적", "일실적" }, 0, 2, Visibles.True, Frozen.False, Align.Right, Merge.False, new Formatter[] { Formatter.Double2, Formatter.Double2, Formatter.Double2 }, 70);
                    }
                    else // PKG 선택 안하였을때 PKG List 구하여 사용하기
                    {
                        for (int i = 0; i < dtPkg.Rows.Count; i++)
                        {
                            spdData.RPT_AddBasicColumn(dtPkg.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("goal", 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            spdData.RPT_AddBasicColumn("Cumulative monthly", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            spdData.RPT_AddBasicColumn("Daily Performance", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);

                            spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.Columns.Count - 3, 3);
                        }
                    }
                }
               // spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!CheckField()) return;

            DataTable dt = null;

            // PKG 선택 안하였을때..즉 ALL 일때 PKG List 구하여 사용하기
            dtPkg = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlPackage());
            
            //PKG 선택한 공정 가져오기
            sPkgList = udcWIPCondition3.getSelectedItems();

            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;
                this.Refresh();
                udcChartFX1.RPT_2_ClearData();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }


                ////표구성에따른 항목 Display
                spdData.RPT_ColumnConfigFromTable(btnSort);

                //by John
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 10, 10, null, null, btnSort);
                //구분항목 값 생성(구분이 들어가는 위치임. 0부터 시작)
                //spdData.RPT_FillColumnData(10, new string[] { "TAT", "INQTY", "OUTQTY", "WIP"});

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 11;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 4, true, Align.Center, VerticalAlign.Center);

                spdData.DataSource = dt;

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                //--------------------------------------------

                spdData.ActiveSheet.Rows[0].BackColor = Color.LemonChiffon;
                spdData.ActiveSheet.Rows[1].BackColor = Color.Aqua;
                spdData.ActiveSheet.Rows[2].BackColor = Color.Aqua;

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
        #endregion

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (udcWIPCondition1.Text == "ALL" || udcWIPCondition1.Text == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD033", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion

        #region MakeSqlPackage
        // PKG 선택 안하였을때..즉 ALL 일때 PKG List 구하여 사용하기
        private string MakeSqlPackage()
        {
            StringBuilder strSqlString = new StringBuilder();

            if (ckbStack.Checked == true)
            {
                strSqlString.AppendFormat("SELECT DISTINCT DECODE(MAT_GRP_4,'-','',DECODE(MAT_GRP_3, 'MCP', SUBSTR(MAT_GRP_4, -1), '')) || MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            }
            else
            {
                strSqlString.AppendFormat("SELECT DISTINCT MAT_GRP_10 " + "\n");
            }

            strSqlString.AppendFormat("  FROM MWIPMATDEF " + "\n");
            strSqlString.AppendFormat(" WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.AppendFormat("   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.AppendFormat("   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.AppendFormat("   AND MAT_GRP_10 <> '-' " + "\n");


            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            /*
            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND MAT_GRP_10 {0} " + "\n", udcWIPCondition10.SelectedValueToQueryString);
            */
            #endregion

            strSqlString.AppendFormat(" ORDER BY MAT_GRP_10 " + "\n");

            return strSqlString.ToString();
        }
        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {            
            string strDecode = string.Empty;
            string strDecode2 = string.Empty;
            string stCustomerCode = string.Empty;  // 업체코드-"SE,HX,OTHERS"(목표)            
            string strCheckquery = string.Empty;   // 목표값에 해당 고객사 존재하는지 체크 하기 위한 쿼리 저장
            StringBuilder strSqlString = new StringBuilder();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            //------ 해당월의 시작일, 종료일 구하기 - 시작 -----------
            string sTemp = Convert.ToDateTime(cdvDate.Text).ToString("yyyy-MM") + "-01";
            
            startDay = Convert.ToDateTime(cdvDate.Text).ToString("yyyyMM") + "01";
            endDay = Convert.ToDateTime(sTemp).AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            //------ 해당월의 시작일, 종료일 구하기 - 종료 -----------

            sFrom = Convert.ToDateTime(cdvDate.Text).ToString("yyyyMMdd");  // 검색일자
            stLotType = cbLotType.Text; // Lot Type

            //------ 투입대기, 출하대기 체크 유무에 따라 해당 쿼리 생성 - 시작 -----------
            if (ckbHMK2A.Checked == false && ckbHMK3A.Checked == false)
            {
                stCheckdData = "' ','HMK2A','HMK3A'";
                stCheckdObject = "                       AND KEY_4 NOT IN ('HMK2A','HMK3A')";
            }
            else if (ckbHMK2A.Checked == false && ckbHMK3A.Checked == true)
            {
                stCheckdData = "' ','HMK2A'";
                stCheckdObject = "                       AND KEY_4 <> 'HMK2A'";
            }
            else if (ckbHMK2A.Checked == true && ckbHMK3A.Checked == false)
            {
                stCheckdData = "' ','HMK3A'";
                stCheckdObject = "                       AND KEY_4 <> 'HMK3A'";
            }
            else
            {
                stCheckdData = "' '";
                stCheckdObject = "";
            }
            //------ 투입대기, 출하대기 체크 유무에 따라 해당 쿼리 생성 - 종료 -----------

            //------ 선택한 업체의 해당일의 목표값이 존재하는지 체크 하는 부분 - 시작 -----------
            strCheckquery += "SELECT COUNT(KEY_2) " + "\n";
            strCheckquery += "  FROM MGCMTBLDAT " + "\n";
            strCheckquery += " WHERE TABLE_NAME = 'H_RPT_TAT_MAINOBJECT' " + "\n";
            strCheckquery += "   AND KEY_1 <= '" + sFrom + "'" + "\n";
            strCheckquery += "   AND DATA_1 >= '" + sFrom + "'" + "\n";
            strCheckquery += "   AND KEY_2 = '" + udcWIPCondition1.Text + "'" + "\n";

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strCheckquery);
            
            if (Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                stCustomerCode = udcWIPCondition1.Text; //목표값 존재하면 해당 업체 코드로 검색 되게..
            }
            else
            {
                stCustomerCode = "OTHERS"; //목표값 존재하지 않으면 OTHERS 로 검색 되게..
            }
            //------ 선택한 업체의 해당일의 목표값이 존재하는지 체크 하는 부분 - 종료 -----------


            strSqlString.Append("SELECT MAT_GRP_1 AS CUSTOMER, OPER_GRP_2 AS \"GROUP\"" + "\n");

            // PKG 선택하였을때 선택한 PKG 이용
            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            {
                for (int i = 0; i < sPkgList.Items.Count; i++)
                {
                    // 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
                    if (ckbTime.Checked == true)
                    {
                        strSqlString.Append("     , NVL(B.OBJECT" + i + ",0) * 24, ROUND(A.MON" + i + " * 24, 2), ROUND(A.DAY" + i + " * 24, 2) " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("     , NVL(B.OBJECT" + i + ",0), ROUND(A.MON" + i + ", 2), ROUND(A.DAY" + i + ", 2) " + "\n");
                    }
                }
            }
            else // PKG 선택 안하였을때 PKG List 구하여 사용하기
            {
                for (int i = 0; i < dtPkg.Rows.Count; i++)
                {
                    // 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
                    if (ckbTime.Checked == true)
                    {
                        strSqlString.Append("     , NVL(B.OBJECT" + i + ",0) * 24, ROUND(A.MON" + i + " * 24, 2), ROUND(A.DAY" + i + " * 24, 2) " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("     , NVL(B.OBJECT" + i + ",0), ROUND(A.MON" + i + ", 2), ROUND(A.DAY" + i + ", 2) " + "\n");
                    }
                }
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT MAT_GRP_1, OPER_GRP_2 " + "\n");

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            {
                for (int i = 0; i < sPkgList.Items.Count; i++)
                {
                    strSqlString.Append("             , SUM(MON" + i + ") AS MON" + i + "\n");
                    strSqlString.Append("             , SUM(DAY" + i + ") AS DAY" + i + "\n");
                }

                strDecode += udcWIPCondition3.getDecodeQuery("DECODE(MAT_GRP_10 ", " SUM(TAT_QTY)/SUM(SHIP_QTY), 0)", "MON").Replace(", DECODE", "                      , DECODE");
                strDecode += udcWIPCondition3.getDecodeQuery("DECODE(MAT_GRP_10 ", " DECODE(SUM(SHIP_QTY_DAY),0,0,SUM(TAT_QTY_DAY)/SUM(SHIP_QTY_DAY)), 0)", "DAY").Replace(", DECODE", "                      , DECODE");
                strDecode2 += udcWIPCondition3.getDecodeQuery("MAX(DECODE(KEY_3 ", " DATA_2))", "OBJECT").Replace(", MAX(DECODE", "                 , MAX(DECODE");
            }
            else
            {
                for (int i = 0; i < dtPkg.Rows.Count; i++)
                {
                    strSqlString.Append("             , SUM(MON" + i + ") AS MON" + i + "\n");
                    strSqlString.Append("             , SUM(DAY" + i + ") AS DAY" + i + "\n");

                    ////2011-08-26 김민우 : MCP Stack 구분 추가 표시
                    ////2013-05-13 김민우 : MAT_GRP_10 기준 Stack 구분 추가 표시
                    pkgLength = dtPkg.Rows[i][0].ToString().Length - 1;   
                    if ((dtPkg.Rows[i][0].ToString().Substring(0,1).Equals("2")))
                    {
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'2',SUM(TAT_QTY)/SUM(SHIP_QTY), 0),0) AS MON" + i + "\n";
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'2',DECODE(SUM(SHIP_QTY_DAY),0,0,SUM(TAT_QTY_DAY)/SUM(SHIP_QTY_DAY)), 0),0) AS DAY" + i + "\n";
                        strDecode2 += "                 , MAX(DECODE(KEY_3, '" + dtPkg.Rows[i][0].ToString() + "' ,DATA_2)) AS OBJECT" + i + "\n";
                    }
                    else if ((dtPkg.Rows[i][0].ToString().Substring(0, 1).Equals("3")))
                    {
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'3',SUM(TAT_QTY)/SUM(SHIP_QTY), 0),0) AS MON" + i + "\n";
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'3',DECODE(SUM(SHIP_QTY_DAY),0,0,SUM(TAT_QTY_DAY)/SUM(SHIP_QTY_DAY)), 0),0) AS DAY" + i + "\n";
                        strDecode2 += "                 , MAX(DECODE(KEY_3, '" + dtPkg.Rows[i][0].ToString() + "',DATA_2 )) AS OBJECT" + i + "\n";
                    }
                    else if ((dtPkg.Rows[i][0].ToString().Substring(0, 1).Equals("4")))
                    {
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'4',SUM(TAT_QTY)/SUM(SHIP_QTY), 0),0) AS MON" + i + "\n";
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'4',DECODE(SUM(SHIP_QTY_DAY),0,0,SUM(TAT_QTY_DAY)/SUM(SHIP_QTY_DAY)), 0),0) AS DAY" + i + "\n";
                        strDecode2 += "                 , MAX(DECODE(KEY_3, '" + dtPkg.Rows[i][0].ToString() + "',DATA_2 )) AS OBJECT" + i + "\n";
                    }
                    else if ((dtPkg.Rows[i][0].ToString().Substring(0, 1).Equals("5")))
                    {
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'5',SUM(TAT_QTY)/SUM(SHIP_QTY), 0),0) AS MON" + i + "\n";
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'5',DECODE(SUM(SHIP_QTY_DAY),0,0,SUM(TAT_QTY_DAY)/SUM(SHIP_QTY_DAY)), 0),0) AS DAY" + i + "\n";
                        strDecode2 += "                 , MAX(DECODE(KEY_3, '" + dtPkg.Rows[i][0].ToString() + "',DATA_2 )) AS OBJECT" + i + "\n";
                    }
                    else if ((dtPkg.Rows[i][0].ToString().Substring(0, 1).Equals("6")))
                    {
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'6',SUM(TAT_QTY)/SUM(SHIP_QTY), 0),0) AS MON" + i + "\n";
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'6',DECODE(SUM(SHIP_QTY_DAY),0,0,SUM(TAT_QTY_DAY)/SUM(SHIP_QTY_DAY)), 0),0) AS DAY" + i + "\n";
                        strDecode2 += "                 , MAX(DECODE(KEY_3, '" + dtPkg.Rows[i][0].ToString() + "',DATA_2 )) AS OBJECT" + i + "\n";
                    }
                    else if ((dtPkg.Rows[i][0].ToString().Substring(0, 1).Equals("7")))
                    {
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'7',SUM(TAT_QTY)/SUM(SHIP_QTY), 0),0) AS MON" + i + "\n";
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'7',DECODE(SUM(SHIP_QTY_DAY),0,0,SUM(TAT_QTY_DAY)/SUM(SHIP_QTY_DAY)), 0),0) AS DAY" + i + "\n";
                        strDecode2 += "                 , MAX(DECODE(KEY_3, '" + dtPkg.Rows[i][0].ToString() + "',DATA_2 )) AS OBJECT" + i + "\n";
                    }
                    else if ((dtPkg.Rows[i][0].ToString().Substring(0, 1).Equals("8")))
                    {
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'8',SUM(TAT_QTY)/SUM(SHIP_QTY), 0),0) AS MON" + i + "\n";
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString().Substring(1, pkgLength) + "', DECODE(SUBSTR(MAT_GRP_4,-1),'8',DECODE(SUM(SHIP_QTY_DAY),0,0,SUM(TAT_QTY_DAY)/SUM(SHIP_QTY_DAY)), 0),0) AS DAY" + i + "\n";
                        strDecode2 += "                 , MAX(DECODE(KEY_3, '" + dtPkg.Rows[i][0].ToString() + "',DATA_2 )) AS OBJECT" + i + "\n";
                    }
                    else
                    {
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString() + "', SUM(TAT_QTY)/SUM(SHIP_QTY), 0) AS MON" + i + "\n";
                        strDecode += "                      , DECODE(MAT_GRP_10, '" + dtPkg.Rows[i][0].ToString() + "', DECODE(SUM(SHIP_QTY_DAY),0,0,SUM(TAT_QTY_DAY)/SUM(SHIP_QTY_DAY)), 0) AS DAY" + i + "\n";
                        strDecode2 += "                 , MAX(DECODE(KEY_3, '" + dtPkg.Rows[i][0].ToString() + "', DATA_2)) AS OBJECT" + i + "\n";
                    }
               
                    
                    
                }
            }
            
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                 SELECT MAT_GRP_1, OPER_GRP_2" + "\n");
            strSqlString.Append(strDecode + "\n");
            strSqlString.Append("                   FROM (" + "\n");
            ////2011-08-26 김민우 : MCP Stack 구분 추가 표시
            ////2013-05-13 김민우 : MAT_GRP_10 기준 Stack 구분 추가 표시
            
            if (ckbStack.Checked == true)
            {
                strSqlString.Append("                        SELECT C.MAT_GRP_1, C.MAT_GRP_10, C.MAT_GRP_4, A.OPER_GRP_2" + "\n");
            }
            else
            {
                strSqlString.Append("                        SELECT C.MAT_GRP_1, C.MAT_GRP_10, A.OPER_GRP_2" + "\n");
            }
            //strSqlString.Append("                        SELECT C.MAT_GRP_1, C.MAT_GRP_10, C.MAT_GRP_3, C.MAT_GRP_4, A.OPER_GRP_2" + "\n");

            strSqlString.Append("                               , SUM(A.TAT_QTY) AS TAT_QTY , SUM(B.SHIP_QTY) AS SHIP_QTY" + "\n");
            strSqlString.Append("                               , SUM(DECODE(SHIP_DATE, '" + sFrom + "', NVL(A.TAT_QTY,0), 0)) TAT_QTY_DAY" + "\n");
            strSqlString.Append("                               , SUM(DECODE(SHIP_DATE, '" + sFrom + "', NVL(B.SHIP_QTY,0), 0)) SHIP_QTY_DAY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT SHIP_DATE, MAT_ID, OPER_GRP_2, SUM(TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT TAT.SHIP_DATE" + "\n");
            strSqlString.Append("                                             , TAT.MAT_ID" + "\n");
            strSqlString.Append("                                             , CASE OPR.OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A' WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPR.OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n");
            strSqlString.Append("                                                                   WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE'" + "\n");
            strSqlString.Append("                                                                   WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n");
            strSqlString.Append("                                                                   ELSE 'FINISH'" + "\n");
            strSqlString.Append("                                               END OPER_GRP_2" + "\n");

            // 2010-11-18-임종우 : Lot_Type별 검색 추가
            if (stLotType.Equals("P%"))
            {
                strSqlString.Append("                                             , TOTAL_TAT_QTY_P AS TOTAL_TAT_QTY " + "\n");
            }
            else if (stLotType.Equals("E%"))
            {
                strSqlString.Append("                                             , TOTAL_TAT_QTY_E AS TOTAL_TAT_QTY " + "\n");
            }
            else
            {
                strSqlString.Append("                                             , TOTAL_TAT_QTY" + "\n");
            }
                        
            strSqlString.Append("                                          FROM CSUMTATMAT@RPTTOMES TAT                             " + "\n");
            strSqlString.Append("                                             , MWIPOPRDEF OPR" + "\n");
            strSqlString.Append("                                          WHERE 1=1   " + "\n");
            strSqlString.Append("                                            AND TAT.FACTORY = OPR.FACTORY    " + "\n");
            strSqlString.Append("                                            AND TAT.OPER = OPR.OPER    " + "\n");
            strSqlString.Append("                                            AND TAT.TOTAL_TIME <> 0    " + "\n");
            strSqlString.Append("                                            AND TAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'       " + "\n");
            strSqlString.Append("                                            AND OPR.OPER_GRP_1 NOT IN (" + stCheckdData + ")    " + "\n");
            strSqlString.Append("                                            AND TAT.SHIP_DATE BETWEEN '" + startDay + "' AND '" + endDay + "'" + "\n");
            strSqlString.Append("                                         )" + "\n");
            strSqlString.Append("                                   GROUP BY SHIP_DATE, MAT_ID, OPER_GRP_2" + "\n");
            strSqlString.Append("                                UNION ALL" + "\n");
            strSqlString.Append("                                SELECT SHIP_DATE, MAT_ID, OPER_GRP_2, SUM(TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT TAT.SHIP_DATE" + "\n");
            strSqlString.Append("                                             , TAT.MAT_ID" + "\n");
            strSqlString.Append("                                             , 'ASSY_TAT' AS OPER_GRP_2" + "\n");

            // 2010-11-18-임종우 : Lot_Type별 검색 추가
            if (stLotType.Equals("P%"))
            {
                strSqlString.Append("                                             , TOTAL_TAT_QTY_P AS TOTAL_TAT_QTY " + "\n");
            }
            else if (stLotType.Equals("E%"))
            {
                strSqlString.Append("                                             , TOTAL_TAT_QTY_E AS TOTAL_TAT_QTY " + "\n");
            }
            else
            {
                strSqlString.Append("                                             , TOTAL_TAT_QTY" + "\n");
            }
                        
            strSqlString.Append("                                          FROM CSUMTATMAT@RPTTOMES TAT                             " + "\n");
            strSqlString.Append("                                             , MWIPOPRDEF OPR" + "\n");
            strSqlString.Append("                                          WHERE 1=1   " + "\n");
            strSqlString.Append("                                            AND TAT.FACTORY = OPR.FACTORY    " + "\n");
            strSqlString.Append("                                            AND TAT.OPER = OPR.OPER    " + "\n");
            strSqlString.Append("                                            AND TAT.TOTAL_TIME <> 0    " + "\n");
            strSqlString.Append("                                            AND TAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'       " + "\n");
            strSqlString.Append("                                            AND OPR.OPER_GRP_1 NOT IN (" + stCheckdData + ")    " + "\n");
            strSqlString.Append("                                            AND TAT.SHIP_DATE BETWEEN '" + startDay + "' AND '" + endDay + "'" + "\n");
            strSqlString.Append("                                         )" + "\n");
            strSqlString.Append("                                   GROUP BY SHIP_DATE, MAT_ID, OPER_GRP_2" + "\n");
            strSqlString.Append("                                UNION ALL" + "\n");
            strSqlString.Append("                                SELECT SHIP_DATE, MAT_ID, OPER_GRP_2, SUM(TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT TAT.SHIP_DATE" + "\n");
            strSqlString.Append("                                             , TAT.MAT_ID" + "\n");
            strSqlString.Append("                                             , CASE OPR.OPER_GRP_1 WHEN 'HMK2A' THEN 'FRONT' WHEN 'B/G' THEN 'FRONT' WHEN 'SAW' THEN 'FRONT' WHEN 'S/P' THEN 'FRONT'" + "\n");
            strSqlString.Append("                                                                   WHEN 'SMT' THEN 'FRONT' WHEN 'D/A' THEN 'FRONT' WHEN 'W/B' THEN 'FRONT' WHEN 'GATE' THEN 'FRONT'" + "\n");
            strSqlString.Append("                                                                   ELSE 'BACK_END'" + "\n");
            strSqlString.Append("                                               END OPER_GRP_2" + "\n");

            // 2010-11-18-임종우 : Lot_Type별 검색 추가
            if (stLotType.Equals("P%"))
            {
                strSqlString.Append("                                             , TOTAL_TAT_QTY_P AS TOTAL_TAT_QTY " + "\n");
            }
            else if (stLotType.Equals("E%"))
            {
                strSqlString.Append("                                             , TOTAL_TAT_QTY_E AS TOTAL_TAT_QTY " + "\n");
            }
            else
            {
                strSqlString.Append("                                             , TOTAL_TAT_QTY" + "\n");
            }
            
            strSqlString.Append("                                          FROM CSUMTATMAT@RPTTOMES TAT                             " + "\n");
            strSqlString.Append("                                             , MWIPOPRDEF OPR" + "\n");
            strSqlString.Append("                                          WHERE 1=1   " + "\n");
            strSqlString.Append("                                            AND TAT.FACTORY = OPR.FACTORY    " + "\n");
            strSqlString.Append("                                            AND TAT.OPER = OPR.OPER    " + "\n");
            strSqlString.Append("                                            AND TAT.TOTAL_TIME <> 0    " + "\n");
            strSqlString.Append("                                            AND TAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'       " + "\n");
            strSqlString.Append("                                            AND OPR.OPER_GRP_1 NOT IN (" + stCheckdData + ")    " + "\n");
            strSqlString.Append("                                            AND TAT.SHIP_DATE BETWEEN '" + startDay + "' AND '" + endDay + "'" + "\n");
            strSqlString.Append("                                         )" + "\n");
            strSqlString.Append("                                   GROUP BY SHIP_DATE, MAT_ID, OPER_GRP_2" + "\n");
            strSqlString.Append("                               ) A" + "\n");
            strSqlString.Append("                             , (     " + "\n");
            strSqlString.Append("                                 SELECT TAT.FACTORY" + "\n");
            strSqlString.Append("                                      , TAT.MAT_ID" + "\n");
            strSqlString.Append("                                      , TAT.SHIP_DATE AS WORK_DATE" + "\n");

            // 2010-11-18-임종우 : Lot_Type별 검색 추가
            if (stLotType.Equals("P%"))
            {
                strSqlString.Append("                                      , SHIP_QTY_P AS SHIP_QTY " + "\n");
            }
            else if (stLotType.Equals("E%"))
            {
                strSqlString.Append("                                      , SHIP_QTY_E AS SHIP_QTY " + "\n");
            }
            else
            {
                strSqlString.Append("                                      , SHIP_QTY" + "\n");
            }
                        
            strSqlString.Append("                                   FROM CSUMTATMAT@RPTTOMES TAT    " + "\n");
            strSqlString.Append("                                  WHERE 1=1     " + "\n");
            strSqlString.Append("                                    AND TAT.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'     " + "\n");
            strSqlString.Append("                                    AND OPER ='AZ010'     " + "\n");
            strSqlString.Append("                                    AND TAT.SHIP_DATE BETWEEN '" + startDay + "' AND '" + endDay + "'" + "\n");
            strSqlString.Append("                               ) B" + "\n");
            strSqlString.Append("                             , MWIPMATDEF C" + "\n");
            strSqlString.Append("                         WHERE 1=1    " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID    " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID" + "\n");
            strSqlString.Append("                           AND A.SHIP_DATE = B.WORK_DATE" + "\n");
            strSqlString.Append("                           AND C.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND C.MAT_VER = 1    " + "\n");
            strSqlString.Append("                           AND C.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND C.DELETE_FLAG <> 'Y'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion
            ////2011-08-26 김민우 : MCP Stack 구분 추가 표시
            ////2013-05-13 김민우 : MAT_GRP_10 기준 Stack 구분 추가 표시
            /*
            strSqlString.Append("                        GROUP BY C.MAT_GRP_1, C.MAT_GRP_10, C.MAT_GRP_3, C.MAT_GRP_4, A.OPER_GRP_2         " + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                   GROUP BY MAT_GRP_1, OPER_GRP_2, MAT_GRP_10, MAT_GRP_3, MAT_GRP_4" + "\n");
            */
            if (ckbStack.Checked == true)
            {
                strSqlString.Append("                        GROUP BY C.MAT_GRP_1, C.MAT_GRP_10, C.MAT_GRP_4, A.OPER_GRP_2         " + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                   GROUP BY MAT_GRP_1, OPER_GRP_2, MAT_GRP_10, MAT_GRP_4" + "\n");
            }
            else
            {
                strSqlString.Append("                        GROUP BY C.MAT_GRP_1, C.MAT_GRP_10, A.OPER_GRP_2         " + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                   GROUP BY MAT_GRP_1, OPER_GRP_2, MAT_GRP_10" + "\n");
            }
            
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("          GROUP BY MAT_GRP_1, OPER_GRP_2" + "\n");
            strSqlString.Append("          ) A" + "\n");
            strSqlString.Append("       , (" + "\n");
            strSqlString.Append("            SELECT KEY_4" + "\n");                       
            strSqlString.Append(strDecode2 + "\n");                        
            strSqlString.Append("              FROM (" + "\n");
            strSqlString.Append("                    SELECT KEY_3,KEY_4,TO_NUMBER(DATA_2) AS DATA_2" + "\n");
            strSqlString.Append("                      FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("                     WHERE TABLE_NAME = 'H_RPT_TAT_MAINOBJECT'" + "\n");
            strSqlString.Append("                       AND KEY_1 <= '" + sFrom + "'" + "\n");
            strSqlString.Append("                       AND DATA_1 >= '" + sFrom + "'" + "\n");
            strSqlString.Append("                       AND KEY_2 = '" + stCustomerCode + "'" + "\n");
            strSqlString.Append(stCheckdObject + "\n");
            strSqlString.Append("                     UNION ALL" + "\n");
            strSqlString.Append("                    SELECT KEY_3, 'ASSY_TAT' AS KEY_4" + "\n");
            strSqlString.Append("                         , SUM(DATA_2) AS DATA_2" + "\n");
            strSqlString.Append("                      FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("                     WHERE TABLE_NAME = 'H_RPT_TAT_MAINOBJECT'" + "\n");
            strSqlString.Append("                       AND KEY_1 <= '" + sFrom + "'" + "\n");
            strSqlString.Append("                       AND DATA_1 >= '" + sFrom + "'" + "\n");
            strSqlString.Append("                       AND KEY_2 = '" + stCustomerCode + "'" + "\n");
            strSqlString.Append(stCheckdObject + "\n");
            strSqlString.Append("                     GROUP BY KEY_3" + "\n");
            strSqlString.Append("                     UNION ALL" + "\n");
            strSqlString.Append("                    SELECT KEY_3, 'FRONT' AS KEY_4" + "\n");
            strSqlString.Append("                         , SUM(DECODE(KEY_4, 'HMK2A', DATA_2, 'SAW', DATA_2, 'QC_GATE2', DATA_2, 'D/A', DATA_2, 'W/B', DATA_2, 'GATE', DATA_2)) AS DATA_2" + "\n");
            strSqlString.Append("                      FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("                     WHERE TABLE_NAME = 'H_RPT_TAT_MAINOBJECT'" + "\n");
            strSqlString.Append("                       AND KEY_1 <= '" + sFrom + "'" + "\n");
            strSqlString.Append("                       AND DATA_1 >= '" + sFrom + "'" + "\n");
            strSqlString.Append("                       AND KEY_2 = '" + stCustomerCode + "'" + "\n");
            strSqlString.Append(stCheckdObject + "\n");
            strSqlString.Append("                     GROUP BY KEY_3" + "\n");
            strSqlString.Append("                     UNION ALL" + "\n");
            strSqlString.Append("                    SELECT KEY_3, 'BACK_END' AS KEY_4" + "\n");
            strSqlString.Append("                         , SUM(DECODE(KEY_4, 'MOLD', DATA_2, 'FINISH', DATA_2, 'HMK3A', DATA_2)) AS DATA_2" + "\n");
            strSqlString.Append("                      FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("                     WHERE TABLE_NAME = 'H_RPT_TAT_MAINOBJECT'" + "\n");
            strSqlString.Append("                       AND KEY_1 <= '" + sFrom + "'" + "\n");
            strSqlString.Append("                       AND DATA_1 >= '" + sFrom + "'" + "\n");
            strSqlString.Append("                       AND KEY_2 = '" + stCustomerCode + "'" + "\n");
            strSqlString.Append(stCheckdObject + "\n");
            strSqlString.Append("                     GROUP BY KEY_3" + "\n");
            strSqlString.Append("                   )" + "\n");
            strSqlString.Append("             GROUP BY KEY_4" + "\n");
            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append(" WHERE A.OPER_GRP_2 = B.KEY_4(+)" + "\n");
            strSqlString.Append(" ORDER BY DECODE(OPER_GRP_2,'ASSY_TAT', 1, 'FRONT', 2, 'BACK_END', 3, 'HMK2A', 4, 'SAW', 5, 'QC_GATE2', 6, 'D/A', 7, 'W/B', 8, 'GATE', 9, 'MOLD', 10, 'FINISH', 11)" + "\n");

          

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region ShowChart

        private void ShowChart(DataTable dt, String stObject)
        {
            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.DataSource = dt;
            udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Lines;

            udcChartFX1.ConstantLines[0].Value = Convert.ToDouble(stObject);
            udcChartFX1.ConstantLines[0].Color = Color.Red;
            //udcChartFX1.ConstantLines[0].Text = stObject; 

            udcChartFX1.LegendBox = false;
            udcChartFX1.PointLabels = true;
            udcChartFX1.Chart3D = false;
            udcChartFX1.MultipleColors = false;
            udcChartFX1.LineWidth = 3;
            udcChartFX1.MarkerShape = SoftwareFX.ChartFX.MarkerShape.Circle;
            udcChartFX1.MarkerSize = ((short)(4));
            //udcChartFX1.AxisX.Title.Text = stPkg + "TAT";  

            // Y축 Max 값 선택하기. 목표값과 데이터 값중 큰 거로..
            if (udcChartFX1.AxisY.Max > Convert.ToDouble(stObject))
            {
                udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
            }
            else
            {
                udcChartFX1.AxisY.Max = Convert.ToDouble(stObject) * 1.2;
            }

            //udcChartFX1.SerLegBox = true;
            //udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            udcChartFX1.RecalcScale();
            udcChartFX1.AxisY.DataFormat.Decimals = 2;
        }
        #endregion
               

        #region ToExcel

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
        }

        #endregion

        #region 월 누적 Cell을 클릭 했을 경우의 ShowChart
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (spdData.ActiveSheet.Cells[e.Row, e.Column].Column.Label == "월누적")
            {
                string stObject = null;

                int iCol = (e.Column / 3) - 1 ;
                stPkg = dtPkg.Rows[iCol][0].ToString();

                stOper = spdData.ActiveSheet.Cells[e.Row, 1].Value.ToString();

                if (spdData.ActiveSheet.Cells[e.Row, e.Column - 1].Value == null)
                {
                    stObject = "0";
                }
                else
                {                    
                    stObject = spdData.ActiveSheet.Cells[e.Row, e.Column - 1].Value.ToString();
                }

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlChart(stPkg, stOper));

                if (dt != null && dt.Rows.Count > 0)
                {
                    ShowChart(dt, stObject);
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region MakeSqlChart
        private string MakeSqlChart(string stPkg, string stOper)
        {
            StringBuilder strSqlString = new StringBuilder();

            // 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
            if (ckbTime.Checked == true)
            {
                strSqlString.Append("SELECT B.SYS_DATE, NVL(A.TAT,0) * 24 AS TAT" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT B.SYS_DATE, NVL(A.TAT,0) AS TAT" + "\n");
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.SHIP_DATE" + "\n");
            strSqlString.Append("             , DECODE(SUM(B.SHIP_QTY),0,0,ROUND(SUM(A.TAT_QTY)/SUM(B.SHIP_QTY),2)) AS TAT" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT SHIP_DATE, MAT_ID, OPER_GRP_2, SUM(TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT TAT.SHIP_DATE" + "\n");
            strSqlString.Append("                             , TAT.MAT_ID" + "\n");
            strSqlString.Append("                             , CASE OPR.OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A' WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPR.OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n");
            strSqlString.Append("                                                   WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE'" + "\n");
            strSqlString.Append("                                                   WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n");
            strSqlString.Append("                                                   ELSE 'FINISH'" + "\n");
            strSqlString.Append("                               END OPER_GRP_2" + "\n");

            // 2010-11-18-임종우 : Lot_Type별 검색 추가
            if (stLotType.Equals("P%"))
            {
                strSqlString.Append("                             , TOTAL_TAT_QTY_P AS TOTAL_TAT_QTY " + "\n");
            }
            else if (stLotType.Equals("E%"))
            {
                strSqlString.Append("                             , TOTAL_TAT_QTY_E AS TOTAL_TAT_QTY " + "\n");
            }
            else
            {
                strSqlString.Append("                             , TOTAL_TAT_QTY" + "\n");
            }
                        
            strSqlString.Append("                          FROM CSUMTATMAT@RPTTOMES TAT " + "\n");
            strSqlString.Append("                             , MWIPOPRDEF OPR" + "\n");
            strSqlString.Append("                          WHERE 1=1   " + "\n");
            strSqlString.Append("                            AND TAT.FACTORY = OPR.FACTORY " + "\n");
            strSqlString.Append("                            AND TAT.OPER = OPR.OPER " + "\n");
            strSqlString.Append("                            AND TAT.TOTAL_TIME <> 0 " + "\n");
            strSqlString.Append("                            AND TAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                            AND OPR.OPER_GRP_1 NOT IN (" + stCheckdData + ")    " + "\n");            
            strSqlString.Append("                            AND TAT.SHIP_DATE BETWEEN '" + startDay + "' AND '" + endDay + "'" + "\n");
            strSqlString.Append("                         )" + "\n");
            strSqlString.Append("                   GROUP BY SHIP_DATE, MAT_ID, OPER_GRP_2" + "\n");
            strSqlString.Append("                UNION ALL" + "\n");
            strSqlString.Append("                SELECT SHIP_DATE, MAT_ID, OPER_GRP_2, SUM(TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT TAT.SHIP_DATE" + "\n");
            strSqlString.Append("                             , TAT.MAT_ID" + "\n");
            strSqlString.Append("                             , 'ASSY_TAT' AS OPER_GRP_2" + "\n");

            // 2010-11-18-임종우 : Lot_Type별 검색 추가
            if (stLotType.Equals("P%"))
            {
                strSqlString.Append("                             , TOTAL_TAT_QTY_P AS TOTAL_TAT_QTY " + "\n");
            }
            else if (stLotType.Equals("E%"))
            {
                strSqlString.Append("                             , TOTAL_TAT_QTY_E AS TOTAL_TAT_QTY " + "\n");
            }
            else
            {
                strSqlString.Append("                             , TOTAL_TAT_QTY" + "\n");
            }
                        
            strSqlString.Append("                          FROM CSUMTATMAT@RPTTOMES TAT " + "\n");
            strSqlString.Append("                             , MWIPOPRDEF OPR" + "\n");
            strSqlString.Append("                          WHERE 1=1   " + "\n");
            strSqlString.Append("                            AND TAT.FACTORY = OPR.FACTORY " + "\n");
            strSqlString.Append("                            AND TAT.OPER = OPR.OPER " + "\n");
            strSqlString.Append("                            AND TAT.TOTAL_TIME <> 0 " + "\n");
            strSqlString.Append("                            AND TAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");            
            strSqlString.Append("                            AND OPR.OPER_GRP_1 NOT IN (" + stCheckdData + ")    " + "\n");
            strSqlString.Append("                            AND TAT.SHIP_DATE BETWEEN '" + startDay + "' AND '" + endDay + "'" + "\n");
            strSqlString.Append("                         )" + "\n");
            strSqlString.Append("                   GROUP BY SHIP_DATE, MAT_ID, OPER_GRP_2" + "\n");
            strSqlString.Append("                UNION ALL" + "\n");
            strSqlString.Append("                SELECT SHIP_DATE, MAT_ID, OPER_GRP_2, SUM(TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT TAT.SHIP_DATE" + "\n");
            strSqlString.Append("                             , TAT.MAT_ID" + "\n");
            strSqlString.Append("                             , CASE OPR.OPER_GRP_1 WHEN 'HMK2A' THEN 'FRONT' WHEN 'B/G' THEN 'FRONT' WHEN 'SAW' THEN 'FRONT' WHEN 'S/P' THEN 'FRONT'" + "\n");
            strSqlString.Append("                                                   WHEN 'SMT' THEN 'FRONT' WHEN 'D/A' THEN 'FRONT' WHEN 'W/B' THEN 'FRONT' WHEN 'GATE' THEN 'FRONT'" + "\n");
            strSqlString.Append("                                                   ELSE 'BACK_END'" + "\n");
            strSqlString.Append("                               END OPER_GRP_2" + "\n");

            // 2010-11-18-임종우 : Lot_Type별 검색 추가
            if (stLotType.Equals("P%"))
            {
                strSqlString.Append("                             , TOTAL_TAT_QTY_P AS TOTAL_TAT_QTY " + "\n");
            }
            else if (stLotType.Equals("E%"))
            {
                strSqlString.Append("                             , TOTAL_TAT_QTY_E AS TOTAL_TAT_QTY " + "\n");
            }
            else
            {
                strSqlString.Append("                             , TOTAL_TAT_QTY" + "\n");
            }
                        
            strSqlString.Append("                          FROM CSUMTATMAT@RPTTOMES TAT " + "\n");
            strSqlString.Append("                             , MWIPOPRDEF OPR" + "\n");
            strSqlString.Append("                          WHERE 1=1 " + "\n");
            strSqlString.Append("                            AND TAT.FACTORY = OPR.FACTORY " + "\n");
            strSqlString.Append("                            AND TAT.OPER = OPR.OPER " + "\n");
            strSqlString.Append("                            AND TAT.TOTAL_TIME <> 0 " + "\n");
            strSqlString.Append("                            AND TAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                            AND OPR.OPER_GRP_1 NOT IN (" + stCheckdData + ")    " + "\n");            
            strSqlString.Append("                            AND TAT.SHIP_DATE BETWEEN '" + startDay + "' AND '" + endDay + "'" + "\n");
            strSqlString.Append("                         )" + "\n");
            strSqlString.Append("                   GROUP BY SHIP_DATE, MAT_ID, OPER_GRP_2" + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , (     " + "\n");
            strSqlString.Append("                 SELECT TAT.FACTORY" + "\n");
            strSqlString.Append("                      , TAT.MAT_ID" + "\n");
            strSqlString.Append("                      , TAT.SHIP_DATE AS WORK_DATE" + "\n");

            // 2010-11-18-임종우 : Lot_Type별 검색 추가
            if (stLotType.Equals("P%"))
            {
                strSqlString.Append("                      , SHIP_QTY_P AS SHIP_QTY " + "\n");
            }
            else if (stLotType.Equals("E%"))
            {
                strSqlString.Append("                      , SHIP_QTY_E AS SHIP_QTY " + "\n");
            }
            else
            {
                strSqlString.Append("                      , SHIP_QTY" + "\n");
            }
                        
            strSqlString.Append("                   FROM CSUMTATMAT@RPTTOMES TAT    " + "\n");
            strSqlString.Append("                  WHERE 1=1     " + "\n");
            strSqlString.Append("                    AND TAT.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'     " + "\n");
            strSqlString.Append("                    AND OPER ='AZ010'     " + "\n");
            strSqlString.Append("                    AND TAT.SHIP_DATE BETWEEN '" + startDay + "' AND '" + endDay + "'" + "\n");
            strSqlString.Append("               ) B" + "\n");
            strSqlString.Append("             , MWIPMATDEF C" + "\n");
            strSqlString.Append("         WHERE 1=1    " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID    " + "\n");
            strSqlString.Append("           AND A.MAT_ID = C.MAT_ID" + "\n");
            strSqlString.Append("           AND A.SHIP_DATE = B.WORK_DATE" + "\n");
            strSqlString.Append("           AND C.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND C.MAT_VER = 1    " + "\n");
            strSqlString.Append("           AND C.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("           AND C.DELETE_FLAG <> 'Y'" + "\n");            
            strSqlString.Append("           AND OPER_GRP_2 = '" + stOper + "'" + "\n");
            /*
            if (stPkg.Equals("2MCP"))
            {
                strSqlString.Append("           AND MAT_GRP_3 = 'MCP'" + "\n");
                strSqlString.Append("           AND SUBSTR(MAT_GRP_4,-1) = '2'" + "\n");
            }
            else if (stPkg.Equals("3MCP"))
            {
                strSqlString.Append("           AND MAT_GRP_3 = 'MCP'" + "\n");
                strSqlString.Append("           AND SUBSTR(MAT_GRP_4,-1) = '3'" + "\n");
            }
            else
            {
                strSqlString.Append("           AND MAT_GRP_3 = '" + stPkg + "'" + "\n");
            }
             */
            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("         GROUP BY A.SHIP_DATE" + "\n");
            strSqlString.Append("         ) A" + "\n");
            strSqlString.Append("      , ( " + "\n");
            strSqlString.Append("         SELECT SYS_DATE " + "\n");
            strSqlString.Append("           FROM MWIPCALDEF " + "\n");
            strSqlString.Append("          WHERE CALENDAR_ID = 'HM' " + "\n");

            //검색일이 현재 달이면 검색일까지 표시
            if (sFrom.Substring(0, 6) == DateTime.Now.ToString("yyyyMM"))
            {
                strSqlString.Append("            AND SYS_DATE BETWEEN '" + startDay + "' AND '" + sFrom + "'" + "\n");
            }
            else //검색일이 현재 달이 아니면 한달 데이터 표시
            {
                strSqlString.Append("            AND SYS_DATE BETWEEN '" + startDay + "' AND '" + endDay + "'" + "\n");
            }

            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append(" WHERE A.SHIP_DATE(+) = B.SYS_DATE" + "\n");
            strSqlString.Append(" ORDER BY B.SYS_DATE" + "\n");
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion MakeSqlChart

        private void TAT050701_Load(object sender, EventArgs e)
        {
            //기본적으로 Detail이 보이도록..
            pnlWIPDetail.Visible = true;
        }
    }
}