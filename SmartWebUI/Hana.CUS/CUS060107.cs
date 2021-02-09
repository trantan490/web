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


namespace Hana.CUS
{
    public partial class CUS060107 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: CUS060107<br/>
        /// 클래스요약: FCI 설비 가동률<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2009-09-16<br/>
        /// 상세  설명: 고객사 FCI 'FLX03A' 설비 가동률<br/>
        /// 변경  내용: <br/>
        /// 2009-11-09-임종우 : Code 재 맵핑 - HD/TEM 삭제
        /// 2010-03-11-임종우 : Code 재 맵핑 - MATL CODE 부분
        /// 2011-03-08-임종우 : FLX01A, FLX02A 설비 데이터 표시 되도록 수정 & 설비번호 컬럼 추가 (김동인 요청)
        /// 2011-07-14-배수민 : V9301A 설비 추가 요청 (박민정S 요청)
        /// 2011-08-25-배수민 : 설비 FLX04A, T2K01A 추가 요청 (박민정S 요청)
        /// </summary>


        public CUS060107()
        {
            InitializeComponent();
            //SortInit(); 

            // 해당일의 1일부터 오늘까지로 초기화
            cdvFromToDate.AutoBinding(DateTime.Today.ToString("yyyy-MM") + "-01",DateTime.Today.ToString("yyyy-MM-dd"));
            GridColumnInit();
            this.SetFactory("HMAT1");
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;
        }


        #region 유효성 검사 및 초기화
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.txtValue.Trim() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("TESTER(MONTH)", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("GROUP", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("DEVICE", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("RES_ID", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Daily Input", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

            spdData.RPT_AddBasicColumn("WORKING HOUR", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("STD", 1, 5, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("ACTL", 1, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_MerageHeaderColumnSpan(0, 5, 2);

            spdData.RPT_AddBasicColumn("UTILI-ZATION", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("%", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);
                        
            spdData.RPT_AddBasicColumn("RETEST", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Q.A-TEST", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("DOWN TIME DESCRIPTION", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 600);
            spdData.RPT_AddBasicColumn("MATL", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("HANDLER", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("SETUP", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("ENGINEER", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("PM/CAM", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("NO WORK", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("TESTER", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("NOSCHD", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("OTHER", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("TOTAL", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);
            spdData.RPT_MerageHeaderColumnSpan(0, 10, 10);

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 9, 2);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "B.MAT_GRP_1", "MAT_GRP_1", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "B.MAT_GRP_2", "MAT_GRP_2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "B.MAT_GRP_3", "MAT_GRP_3", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "B.MAT_GRP_4", "MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "B.MAT_GRP_5", "MAT_GRP_5", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "B.MAT_GRP_6", "MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "B.MAT_GRP_7", "MAT_GRP_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "B.MAT_GRP_8", "MAT_GRP_8", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_CMF_7", "B.MAT_CMF_7", "MAT_CMF_7", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot ID", "LOT_ID", "A.LOT_ID", "LOT_ID", true);     
        } 
        #endregion


        #region SQL 쿼리 Build 
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
          
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string Start_Tran_Time = null;
            string End_Tran_Time = null;

            Start_Tran_Time = cdvFromToDate.ExactFromDate;
            End_Tran_Time = cdvFromToDate.ExactToDate;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            strSqlString.Append("SELECT DWH.TRAN_DATE " + "\n");
            //strSqlString.Append("     , 'IFLEX RF' AS MODEL " + "\n");
            strSqlString.Append("     , DECODE(DWH.RES, 'FLX01A', 'IFLEX RF', 'FLX02A', 'IFLEX RF', 'FLX03A', 'IFLEX RF', 'FLX04A', 'IFLEX RF', 'T2K01A', 'T2000', 'V9301A', 'VERIGY') AS MODEL " + "\n");
            strSqlString.Append("     , LOT.DEVICE" + "\n");
            strSqlString.Append("     , DWH.RES" + "\n");
            strSqlString.Append("     , LOT.LOT_QTY" + "\n");
            strSqlString.Append("     , 24 AS STD" + "\n");
            strSqlString.Append("     , ROUND((24 - (MATL + HANDLER + SETUP + ENGINEER + PM_CAM + NO_WORK + TESTER + NOSCHD + OTHER)),2) AS ACT" + "\n");
            strSqlString.Append("     , ROUND((24 - (MATL + HANDLER + SETUP + ENGINEER + PM_CAM + NO_WORK + TESTER + NOSCHD + OTHER)) / 24 * 100, 2) AS PER" + "\n");            
            strSqlString.Append("     , ROUND(RETEST,2) AS RETEST" + "\n");
            strSqlString.Append("     , ROUND(QA_TEST,2) AS QA_TEST" + "\n");
            strSqlString.Append("     , ROUND(MATL,2) AS MATL" + "\n");
            strSqlString.Append("     , ROUND(HANDLER,2) AS HANDLER" + "\n");
            strSqlString.Append("     , ROUND(SETUP,2) AS SETUP" + "\n");
            strSqlString.Append("     , ROUND(ENGINEER,2) AS ENGINEER" + "\n");
            strSqlString.Append("     , ROUND(PM_CAM,2) AS PM_CAM" + "\n");
            strSqlString.Append("     , ROUND(NO_WORK,2) AS NO_WORK" + "\n");            
            strSqlString.Append("     , ROUND(TESTER,2) AS TESTER" + "\n");
            strSqlString.Append("     , ROUND(NOSCHD,2) AS NOSCHD" + "\n");
            strSqlString.Append("     , ROUND(OTHER,2) AS OTHER" + "\n");
            strSqlString.Append("     , ROUND(MATL + HANDLER + SETUP + ENGINEER + PM_CAM + NO_WORK + TESTER + NOSCHD + OTHER, 2) AS TOTAL" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT DOWN_DATE AS TRAN_DATE, RES" + "\n");
            strSqlString.Append("             , SUM(DECODE(UP_CODE,'H100',DOWN_TIME,0)) + SUM(DECODE(SUBSTR(UP_CODE,1,2),'H9',DOWN_TIME,0)) AS RETEST" + "\n");
            strSqlString.Append("             , SUM(DECODE(UP_CODE,'E300',DOWN_TIME,'H800',DOWN_TIME,0)) AS QA_TEST" + "\n");
            strSqlString.Append("             , SUM(DECODE(SUBSTR(UP_CODE,1,2),'H1',DECODE(SUBSTR(UP_CODE,4,1),'0',0,DOWN_TIME),'H2',DECODE(SUBSTR(UP_CODE,4,1),'0',0,DOWN_TIME),'H3',DECODE(SUBSTR(UP_CODE,4,1),'0',0,DOWN_TIME),'H4',DECODE(SUBSTR(UP_CODE,4,1),'0',0,DOWN_TIME),'H6',DOWN_TIME,'H7',DOWN_TIME,0)) AS MATL" + "\n");
            strSqlString.Append("             , SUM(DECODE(SUBSTR(UP_CODE,1,2),'A4',DOWN_TIME,0)) AS HANDLER" + "\n");
            strSqlString.Append("             , SUM(DECODE(SUBSTR(UP_CODE,1,1),'D',DOWN_TIME,0)) AS SETUP" + "\n");
            strSqlString.Append("             , SUM(DECODE(UP_CODE,'E900',DOWN_TIME,0)) AS ENGINEER" + "\n");
            strSqlString.Append("             , SUM(DECODE(SUBSTR(UP_CODE,1,1),'C',DOWN_TIME,0)) AS PM_CAM" + "\n");
            strSqlString.Append("             , SUM(DECODE(SUBSTR(UP_CODE,1,1),'B',DOWN_TIME,0)) AS NO_WORK" + "\n");
            strSqlString.Append("             , SUM(DECODE(SUBSTR(UP_CODE,1,2),'A5',DOWN_TIME,0)) AS TESTER" + "\n");
            strSqlString.Append("             , SUM(DECODE(UP_CODE,'E200',DOWN_TIME,0)) AS NOSCHD" + "\n");
            strSqlString.Append("             , SUM(DECODE(SUBSTR(UP_CODE,1,1),'F',DOWN_TIME,'G',DOWN_TIME,0)) + SUM(DECODE(SUBSTR(UP_CODE,1,2),'H2',DECODE(SUBSTR(UP_CODE,4,1),'0',DOWN_TIME,0),'H3',DECODE(SUBSTR(UP_CODE,4,1),'0',DOWN_TIME,0),'H4',DECODE(SUBSTR(UP_CODE,4,1),'0',DOWN_TIME,0),'H5',DOWN_TIME,0)) + SUM(DECODE(UP_CODE,'E100',DOWN_TIME,'E400',DOWN_TIME,'E500',DOWN_TIME,'E600',DOWN_TIME,0)) AS OTHER" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT TO_CHAR(TO_DATE(DOWN_TRAN_TIME, 'YYYYMMDDHH24MISS') + 2/24, 'YYYYMMDD') AS DOWN_DATE" + "\n");
            strSqlString.Append("                     , RES_ID AS RES" + "\n");
            strSqlString.Append("                     , UP_NEW_STS_1 AS UP_CODE" + "\n");
            strSqlString.Append("                     , CASE WHEN UP_TRAN_TIME <> ' '" + "\n");
            strSqlString.Append("                            THEN ROUND(TO_CHAR(TO_DATE(UP_TRAN_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(DOWN_TRAN_TIME, 'YYYYMMDDHH24MISS'))* 24,3)" + "\n");
            strSqlString.Append("                            ELSE ROUND(TO_CHAR(SYSDATE - TO_DATE(DOWN_TRAN_TIME, 'YYYYMMDDHH24MISS')),3)" + "\n");
            strSqlString.Append("                       END AS DOWN_TIME" + "\n");
            strSqlString.Append("                  FROM CRASRESDWH" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND RES_ID IN ('FLX01A','FLX02A','FLX03A','FLX04A','T2K01A','V9301A')" + "\n");
            strSqlString.Append("                   AND DOWN_TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("               )" + "\n");            
            strSqlString.Append("         GROUP BY DOWN_DATE, RES" + "\n");            
            strSqlString.Append("       ) DWH" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT TRAN_DATE, END_RES_ID, DEVICE, SUM(LOT_QTY) AS LOT_QTY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT TO_CHAR(TO_DATE(TRAN_TIME, 'YYYYMMDDHH24MISS') + 2/24, 'YYYYMMDD') AS TRAN_DATE" + "\n");
            strSqlString.Append("                     , LOT.END_RES_ID" + "\n");
            strSqlString.Append("                     , MAT.MAT_CMF_7 AS DEVICE" + "\n");
            strSqlString.Append("                     , LOT.START_QTY_1 AS LOT_QTY" + "\n");
            strSqlString.Append("                  FROM CWIPLOTEND LOT" + "\n");
            strSqlString.Append("                     , MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND LOT.FACTORY=MAT.FACTORY" + "\n");
            strSqlString.Append("                   AND LOT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.Append("                   AND LOT.FACTORY='" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND LOT.OLD_OPER = 'T0100'" + "\n");
            strSqlString.Append("                   AND LOT.END_RES_ID IN ('FLX01A','FLX02A','FLX03A','FLX04A','T2K01A','V9301A')" + "\n");
            strSqlString.Append("                   AND LOT.TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY TRAN_DATE, END_RES_ID, DEVICE" + "\n");            
            strSqlString.Append("       ) LOT" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND DWH.TRAN_DATE=LOT.TRAN_DATE(+)" + "\n");
            strSqlString.Append("   AND DWH.RES = LOT.END_RES_ID(+)" + "\n");
            strSqlString.Append(" ORDER BY TRAN_DATE, DWH.RES" + "\n");

            //상세 조회에 따른 SQL문 생성                                        
            //if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //    strSqlString.AppendFormat("       AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //    strSqlString.AppendFormat("       AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //    strSqlString.AppendFormat("       AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //    strSqlString.AppendFormat("       AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //    strSqlString.AppendFormat("       AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //    strSqlString.AppendFormat("       AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //    strSqlString.AppendFormat("       AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //    strSqlString.AppendFormat("       AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);


            //strSqlString.Append("                )                     " + "\n");
            //strSqlString.AppendFormat("       GROUP BY {0}     " + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();            
        }

        #endregion


        #region EVENT 처리
        private void btnView_Click(object sender, EventArgs e)
        {
            //DurationTime_Check();

            DataTable dt = null;
            if (CheckField() == false) return;
            
            spdData_Sheet1.RowCount = 0;
            
            try
            {                
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                
                GridColumnInit();

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;
                
                //by John
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 9, null, null, btnSort);
                //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 11;

                //by John (두줄짜리이상 + 부분합계.)
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 9, 10, 4, udcCUSFromToCondition1.CountFromToValue, btnSort);
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 9, 10, 4, udcCUSCondition1.SelectCount, btnSort);
                //// 서브토탈시작할 컬럼, 서브토탈 몇개쓸껀지, 몇 컬럼까지 서브토탈 ~ 까지 안함, 자동채움시작컬럼 전컬럼, 디바이드 로우수                                                                                        
                
                //구분항목 값 생성(구분이 들어가는 위치임. 0부터 시작)                

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_FillColumnData(9, new string[] {"IN", "OUT", "EOH", "BOH" });

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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null);
            spdData.ExportExcel();
        }
        #endregion
    }
}
