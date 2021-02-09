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
    public partial class PRD010608 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] selectDate = null;

        /// <summary>
        /// 클  래  스: PRD010608<br/>
        /// 클래스요약: 입고 현황(시간대별)<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-03-18<br/>
        /// 상세  설명: 시간대별 입고 현황 조회 화면<br/>
        /// 변경  내용: <br/>
        /// 2010-03-22-임종우 : STOCK 에서 고객사로 RETURN 된 수량 제외 함.(윤수현 요청)
        /// 2012-09-12-임종우 : WF INCH 그룹정보 추가 표시 (남정호 요청)
        /// 2013-10-17-김민우 : LOT TYPE ALL, P%, E% 구분으로변경
        /// 2014-05-02-임종우 : PIN TYPE 그룹정보 추가 표시 (오세만 요청)
        /// </summary>
        /// 
        public PRD010608()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.Enabled = false;
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
                spdData.RPT_AddBasicColumn("LD Count", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("WF Inch", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("Code", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);                

                if (cdvFactory.Text != "")
                {
                    if (ckbKpcs.Checked == false)
                    {
                        for (int i = 0; i < selectDate.Length; i++)
                        {
                            spdData.RPT_AddBasicColumn(selectDate[i].ToString(), 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                            spdData.RPT_AddBasicColumn("dawn", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("2시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("5시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_MerageHeaderColumnSpan(1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 2, 2);

                            spdData.RPT_AddBasicColumn("weekly", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                            spdData.RPT_AddBasicColumn("11시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("16시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("19시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_MerageHeaderColumnSpan(1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 3, 3);

                            spdData.RPT_AddBasicColumn("night", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                            spdData.RPT_AddBasicColumn("22시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                            spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 6, 6);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < selectDate.Length; i++)
                        {
                            spdData.RPT_AddBasicColumn(selectDate[i].ToString(), 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                            spdData.RPT_AddBasicColumn("dawn", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 60);
                            spdData.RPT_AddBasicColumn("2시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 60);
                            spdData.RPT_AddBasicColumn("5시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 60);
                            spdData.RPT_MerageHeaderColumnSpan(1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 2, 2);

                            spdData.RPT_AddBasicColumn("weekly", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                            spdData.RPT_AddBasicColumn("11시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 60);
                            spdData.RPT_AddBasicColumn("16시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 60);
                            spdData.RPT_AddBasicColumn("19시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 60);
                            spdData.RPT_MerageHeaderColumnSpan(1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 3, 3);

                            spdData.RPT_AddBasicColumn("night", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                            spdData.RPT_AddBasicColumn("22시", 2, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 60);

                            spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 6, 6);
                        }
                    }

                }
                spdData.RPT_MerageHeaderRowSpan(0, 0, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 3);

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "B.MAT_GRP_6", "MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "B.MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("WF Inch", "MAT_CMF_14", "B.MAT_CMF_14", "MAT_CMF_14", false);
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
                                 
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            sStart_Tran_Time = cdvFromToDate.ExactFromDate;
            sEnd_Tran_Time = cdvFromToDate.ExactToDate;
                        
            strSqlString.AppendFormat("SELECT {0}, CODE " + "\n", QueryCond3);

            if (ckbKpcs.Checked == false)
            {
                for (int i = 0; i < selectDate.Length; i++)
                {
                    strSqlString.Append("     , SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "02', QTY, 0)) AS T02_" + i + "\n");
                    strSqlString.Append("     , SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "05', QTY, 0)) AS T05_" + i + "\n");
                    strSqlString.Append("     , SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "11', QTY, 0)) AS T11_" + i + "\n");
                    strSqlString.Append("     , SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "16', QTY, 0)) AS T16_" + i + "\n");
                    strSqlString.Append("     , SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "19', QTY, 0)) AS T19_" + i + "\n");
                    strSqlString.Append("     , SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "22', QTY, 0)) AS T22_" + i + "\n");
                }
            }
            else
            {
                for (int i = 0; i < selectDate.Length; i++)
                {
                    strSqlString.Append("     , ROUND(SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "02', QTY, 0))/1000,1) AS T02_" + i + "\n");
                    strSqlString.Append("     , ROUND(SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "05', QTY, 0))/1000,1) AS T05_" + i + "\n");
                    strSqlString.Append("     , ROUND(SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "11', QTY, 0))/1000,1) AS T11_" + i + "\n");
                    strSqlString.Append("     , ROUND(SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "16', QTY, 0))/1000,1) AS T16_" + i + "\n");
                    strSqlString.Append("     , ROUND(SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "19', QTY, 0))/1000,1) AS T19_" + i + "\n");
                    strSqlString.Append("     , ROUND(SUM(DECODE(CREATE_DAY||TIME_BASE, '" + selectDate[i].ToString() + "22', QTY, 0))/1000,1) AS T22_" + i + "\n");
                }
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT {0} " + "\n", QueryCond2);
            strSqlString.Append("             , B.MAT_CMF_11 AS CODE, A.CREATE_DAY, A.TIME_BASE, SUM(A.QTY_1) AS QTY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT LOT_ID, FACTORY, MAT_ID, QTY_1, OPER_IN_TIME, GET_WORK_DATE(OPER_IN_TIME,'D') AS CREATE_DAY" + "\n");
            strSqlString.Append("                     , CASE WHEN ('090000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '040000') THEN '05'" + "\n");
            strSqlString.Append("                            WHEN ('150000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '090000') THEN '11'" + "\n");
            strSqlString.Append("                            WHEN ('180000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '150000') THEN '16'" + "\n");
            strSqlString.Append("                            WHEN ('210000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '180000') THEN '19'" + "\n");
            strSqlString.Append("                            WHEN ('220000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '210000') THEN '22'" + "\n");
            strSqlString.Append("                            ELSE '02'" + "\n");
            strSqlString.Append("                       END AS TIME_BASE" + "\n");
            strSqlString.Append("                  FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            //strSqlString.Append("                   AND MAT_VER = 1 " + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("                   AND OLD_OPER IN ('A0000', 'A000N') " + "\n");
            }
            else if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                   AND OLE_OPER IN ('T0000', 'T000N') " + "\n");
            }
                        
            strSqlString.Append("                   AND TRAN_CODE = 'CREATE'          " + "\n");
            strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "' " + "\n");
            //strSqlString.Append("                   AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            // 2010-03-22-임종우 : STOCK 에서 고객사로 RETURN 된 수량 제외 함.(윤수현 요청)  ---시작---
            strSqlString.Append("                 UNION ALL " + "\n");
            strSqlString.Append("                SELECT LOT_ID, '" + cdvFactory.txtValue + "' AS FACTORY, MAT_ID, -QTY_1 AS QTY_1, OPER_IN_TIME, GET_WORK_DATE(OPER_IN_TIME,'D') AS CREATE_DAY" + "\n");
            strSqlString.Append("                     , CASE WHEN ('090000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '040000') THEN '05'" + "\n");
            strSqlString.Append("                            WHEN ('150000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '090000') THEN '11'" + "\n");
            strSqlString.Append("                            WHEN ('180000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '150000') THEN '16'" + "\n");
            strSqlString.Append("                            WHEN ('210000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '180000') THEN '19'" + "\n");
            strSqlString.Append("                            WHEN ('220000' > SUBSTR(OPER_IN_TIME,9,6) AND SUBSTR(OPER_IN_TIME,9,6) >= '210000') THEN '22'" + "\n");
            strSqlString.Append("                            ELSE '02'" + "\n");
            strSqlString.Append("                       END AS TIME_BASE" + "\n");
            strSqlString.Append("                  FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("                 WHERE FACTORY = 'RETURN' " + "\n");
            strSqlString.Append("                   AND OLD_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");            

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("                   AND OLD_OPER IN ('A0000', 'A000N') " + "\n");
            }
            else if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                   AND OLE_OPER IN ('T0000', 'T000N') " + "\n");
            }

            strSqlString.Append("                   AND TRAN_CODE = 'SHIP' " + "\n");
            strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "' " + "\n");
            //strSqlString.Append("                   AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }


            // 2010-03-22-임종우 : STOCK 에서 고객사로 RETURN 된 수량 제외 함.(윤수현 요청)  ---끝---

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

            strSqlString.AppendFormat("        GROUP BY {0}, B.MAT_CMF_11, A.CREATE_DAY, A.TIME_BASE" + "\n", QueryCond2);            
            strSqlString.Append("       )" + "\n");
            strSqlString.AppendFormat("GROUP BY {0}, CODE" + "\n", QueryCond1);
            strSqlString.AppendFormat("ORDER BY {0}, CODE" + "\n", QueryCond1);
            
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
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 6, null, null, btnSort);
                //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
                
                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 6, 0, 1, true, Align.Center, VerticalAlign.Center);
                
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //2010-03-19-임종우 : 영역별 SubTotal, GrandTotal 구하기
                SetTotalValue();
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

        //2010-03-19-임종우 : 영역별 SubTotal, GrandTotal 구하기
        private void SetTotalValue()
        {
            double[] iSum_2 = new double[selectDate.Length]; //새벽 Sum
            double[] iSum_11 = new double[selectDate.Length]; //주간 Sum
            double[] iSum_22 = new double[selectDate.Length]; //야간 Sum
            double[] iGrandSum = new double[selectDate.Length]; //Grand Sum
            int j = 0;

            Color color = spdData.ActiveSheet.Cells[1, 0].BackColor;

            for (int y = 0; y < spdData.ActiveSheet.Rows.Count; y++)
            {
                if (spdData.ActiveSheet.Cells[y, 0].BackColor != color) // subtotal 찾기
                {
                    for (int x = 6; x < spdData.ActiveSheet.Columns.Count; x = x + 6) //value 값부터 컬럼 끝까지 영역별(새벽,주간,야간) subTotal Sum
                    {
                        iSum_2[j] = Convert.ToDouble(spdData.ActiveSheet.Cells[y, x].Value) + Convert.ToDouble(spdData.ActiveSheet.Cells[y, x + 1].Value);
                        iSum_11[j] = Convert.ToDouble(spdData.ActiveSheet.Cells[y, x + 2].Value) + Convert.ToDouble(spdData.ActiveSheet.Cells[y, x + 3].Value) + Convert.ToDouble(spdData.ActiveSheet.Cells[y, x + 4].Value);
                        iSum_22[j] = Convert.ToDouble(spdData.ActiveSheet.Cells[y, x + 5].Value);
                        iGrandSum[j] = Convert.ToDouble(spdData.ActiveSheet.Cells[0, x].Value) + Convert.ToDouble(spdData.ActiveSheet.Cells[0, x + 1].Value) + Convert.ToDouble(spdData.ActiveSheet.Cells[0, x + 2].Value) + Convert.ToDouble(spdData.ActiveSheet.Cells[0, x + 3].Value) + Convert.ToDouble(spdData.ActiveSheet.Cells[0, x + 4].Value) + Convert.ToDouble(spdData.ActiveSheet.Cells[0, x + 5].Value);

                        // 새벽 SUM
                        spdData.ActiveSheet.Cells[y, x].ColumnSpan = 2;
                        spdData.ActiveSheet.Cells[y, x].Value = iSum_2[j];
                        
                        // 주간 SUM
                        spdData.ActiveSheet.Cells[y, x + 2].ColumnSpan = 3;
                        spdData.ActiveSheet.Cells[y, x + 2].Value = iSum_11[j];

                        // 야간 SUM
                        spdData.ActiveSheet.Cells[y, x + 5].Value = iSum_22[j];

                        // Grand SUM
                        spdData.ActiveSheet.Cells[0, x].ColumnSpan = 6;
                        spdData.ActiveSheet.Cells[0, x].Value = iGrandSum[j];

                        // subTotal, GrandTotal은 병합 된 값이기에 숫자이지만 가운데 정렬로 변경 함.
                        spdData.ActiveSheet.Rows[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        spdData.ActiveSheet.Rows[y].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        j++;
                    }
                }
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
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();           
        }
        #endregion

        private void PRD010608_Load(object sender, EventArgs e)
        {
            //기본적으로 Detail이 보이도록..
            pnlWIPDetail.Visible = true;
        }
    }
}
