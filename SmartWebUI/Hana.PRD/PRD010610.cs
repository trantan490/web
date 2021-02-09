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


namespace Hana.PRD
{
    public partial class PRD010610 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] selectDate = null;

        /// <summary>
        /// 클  래  스: PRD010610<br/>
        /// 클래스요약: 실적 현황 TREND<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-06-14<br/>
        /// 상세  설명: 실적 현황 시간대별 TREND 조회 화면<br/>
        /// 변경  내용: <br/>        
        /// </summary>
        /// 
        public PRD010610()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.Enabled = false;
            this.cdvLotType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            SortInit();           
            GridColumnInit();
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        ///
        /// </summary>        
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            // Customer 무조건 선택 하여야 함.
            if (udcWIPCondition1.Text == "ALL" || udcWIPCondition1.Text == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD033", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary 2. 헤더 생성> 
        ///
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;

            selectDate = cdvFromToDate.getSelectDate();
            try
            {
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Classification", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);

                if (cdvFactory.Text != "")
                {
                    if (ckbKpcs.Checked == false)
                    {
                        spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TOTAL", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("Ratio", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
                    }
                    else
                    {
                        spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TOTAL", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 60);
                        spdData.RPT_AddBasicColumn("Ratio", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
                    }

                } 

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary 3. SortInit>
        /// 
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "B.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "B.MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "B.MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "B.MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "B.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "B.MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "B.MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "B.MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT_CMF_10", "B.MAT_CMF_10", "MAT_CMF_10", false);            
        }
        #endregion
        

        #region SQL 쿼리 Build
        /// <summary 4. SQL 쿼리 Build>
        /// 
        /// </summary>
        /// <returns> strSqlString </returns>
      
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string sStart_Tran_Time = string.Empty;
            string sEnd_Tran_Time = string.Empty;
            string bbbb = string.Empty;
            string strDecode = string.Empty;
            int iCnt = cdvFromToDate.SubtractBetweenFromToDate + 1;
                                 
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            sStart_Tran_Time = cdvFromToDate.ExactFromDate;
            sEnd_Tran_Time = cdvFromToDate.ExactToDate;

            if (ckbKpcs.Checked == false)
            {
                strDecode += cdvFromToDate.getDecodeQuery("SUM(DECODE(SHIP_DAY", "QTY, 0))", "A").Replace(", SUM(DECODE(", "     , SUM(DECODE(");
            }
            else
            {
                strDecode += cdvFromToDate.getDecodeQuery("ROUND(SUM(DECODE(SHIP_DAY", "QTY, 0))/1000,1)", "A").Replace(", ROUND(SUM(DECODE(", "     , ROUND(SUM(DECODE(");
            }

            strSqlString.AppendFormat("SELECT {0}, TIME_BASE " + "\n", QueryCond3);
            strSqlString.Append(strDecode);

            if (ckbKpcs.Checked == false)
            {
                strSqlString.Append("     , SUM(QTY) AS QTY" + "\n");                
            }
            else
            {
                strSqlString.Append("     , ROUND(SUM(QTY)/1000,1) AS QTY" + "\n");                
            }

            strSqlString.Append("     , ROUND(RATIO_TO_REPORT(SUM(QTY)) OVER () * 100, 2) AS PER" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT {0} " + "\n", QueryCond2);
            strSqlString.Append("             , A.SHIP_DAY, A.TIME_BASE, SUM(A.SHIP_QTY_1) AS QTY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT LOT_ID, FROM_FACTORY AS FACTORY, MAT_ID, SHIP_QTY_1, TRAN_TIME, GET_WORK_DATE(TRAN_TIME,'D') AS SHIP_DAY" + "\n");

            // TIME_BASE 선택에 의한 기준 시간 변경.
            if (cdvTimeBase.Text == "SE")
            {
                strSqlString.Append("                     , CASE WHEN ('210000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '090000') THEN '주간(09시-21시)'" + "\n");
                strSqlString.Append("                            WHEN ('220000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '210000') THEN '야간(21시-22시)'" + "\n");
                strSqlString.Append("                            ELSE '새벽(22시-09시)'" + "\n");
            }
            else if (cdvTimeBase.Text == "4HR")
            {
                strSqlString.Append("                     , CASE WHEN ('060000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '020000') THEN '02시-06시'" + "\n");
                strSqlString.Append("                            WHEN ('100000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '060000') THEN '06시-10시'" + "\n");
                strSqlString.Append("                            WHEN ('140000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '100000') THEN '10시-14시'" + "\n");
                strSqlString.Append("                            WHEN ('180000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '140000') THEN '14시-18시'" + "\n");
                strSqlString.Append("                            WHEN ('220000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '180000') THEN '18시-22시'" + "\n");
                strSqlString.Append("                            ELSE '22시-02시'" + "\n");
            }
            else if (cdvTimeBase.Text == "6HR")
            {
                strSqlString.Append("                     , CASE WHEN ('100000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '040000') THEN '04시-10시'" + "\n");
                strSqlString.Append("                            WHEN ('160000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '100000') THEN '10시-16시'" + "\n");
                strSqlString.Append("                            WHEN ('220000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '160000') THEN '16시-22시'" + "\n");                
                strSqlString.Append("                            ELSE '22시-04시'" + "\n");
            }
            else if (cdvTimeBase.Text == "8HR")
            {
                strSqlString.Append("                     , CASE WHEN ('140000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '060000') THEN '06시-14시'" + "\n");
                strSqlString.Append("                            WHEN ('220000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '140000') THEN '14시-22시'" + "\n");                
                strSqlString.Append("                            ELSE '22시-06시'" + "\n");
            }
            else if (cdvTimeBase.Text == "12HR")
            {
                strSqlString.Append("                     , CASE WHEN ('220000' > SUBSTR(TRAN_TIME,9,6) AND SUBSTR(TRAN_TIME,9,6) >= '100000') THEN '10시-22시'" + "\n");                
                strSqlString.Append("                            ELSE '22시-10시'" + "\n");
            }

            strSqlString.Append("                       END AS TIME_BASE" + "\n");
            strSqlString.Append("                  FROM VWIPSHPLOT " + "\n");
            strSqlString.Append("                 WHERE FROM_FACTORY  = '" + cdvFactory.Text + "'" + "\n");
            
            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                   AND TO_FACTORY IN ('CUSTOMER', 'FGS') " + "\n");
            }
            else
            {
                strSqlString.Append("                   AND TO_FACTORY = 'CUSTOMER' " + "\n");

            }

            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");

            // T1300 공정은 추후 삭제 해야함!!   (2009.03.02) - SHIP 공정이 아닌곳에서 SHIP이 되었음
            strSqlString.Append("                   AND FROM_OPER IN ('AZ010','AZ009','TZ010','EZ010', 'F0000', 'T1300')" + "\n");
            strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "' " + "\n");

            if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
            {
                strSqlString.Append("                   AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , MWIPMATDEF B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY  " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID            " + "\n");
            strSqlString.Append("           AND B.MAT_VER = 1  " + "\n");
                        
            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.AppendFormat("        GROUP BY {0}, A.SHIP_DAY, A.TIME_BASE" + "\n", QueryCond2);            
            strSqlString.Append("       )" + "\n");
            strSqlString.AppendFormat("GROUP BY {0}, TIME_BASE" + "\n", QueryCond1);

            // TIME_BASE 선택에 의한 데이터 정렬
            if (cdvTimeBase.Text == "SE")
            {
                strSqlString.AppendFormat("ORDER BY {0}, DECODE(TIME_BASE,'새벽(22시-09시)',1,'주간주간(09시-21시)',2,3)" + "\n", QueryCond1);
            }
            else if (cdvTimeBase.Text == "4HR")
            {
                strSqlString.AppendFormat("ORDER BY {0}, DECODE(TIME_BASE,'22시-02시',1,'02시-06시',2,'06시-10시',3,'10시-14시',4,'14시-18시',5,'18시-22시',6,7)" + "\n", QueryCond1);
            }
            else if (cdvTimeBase.Text == "6HR")
            {
                strSqlString.AppendFormat("ORDER BY {0}, DECODE(TIME_BASE,'22시-04시',1,'04시-10시',2,'10시-16시',3,4)" + "\n", QueryCond1);
            }
            else if (cdvTimeBase.Text == "8HR")
            {
                strSqlString.AppendFormat("ORDER BY {0}, DECODE(TIME_BASE,'22시-06시',1,'06시-14시',2,3)" + "\n", QueryCond1);
            }
            else if (cdvTimeBase.Text == "12HR")
            {
                strSqlString.AppendFormat("ORDER BY {0}, DECODE(TIME_BASE,'22시-10시',1,2)" + "\n", QueryCond1);
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #region EVENT 처리
        /// <summary 5. View>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;         
            if (CheckField() == false) return;                      
            
            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);              
                this.Refresh();
                GridColumnInit();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());                                

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(1);
                                
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 10, null, null, btnSort);
                //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
                
                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);
                
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

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

        /// <summary 6. Excel Export>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null);
            //spdData.ExportExcel();           
        }
        #endregion

        private void PRD010610_Load(object sender, EventArgs e)
        {
            //기본적으로 Detail이 보이도록..
            pnlWIPDetail.Visible = true;
        }
    }
}
