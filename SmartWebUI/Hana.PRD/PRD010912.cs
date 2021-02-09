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
    public partial class PRD010912 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] strDate = new string[7];

        /// <summary>
        /// 클  래  스: PRD010912<br/>
        /// 클래스요약: Issue제품 공정별 계획 대비 실적<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2017-07-19<br/>
        /// 상세  설명: Issue제품 공정별 계획 대비 실적 (임태성C 요청)<br/> 
        /// 변경  내용: <br/>   
        /// 2017-09-11-임종우 : Hynix 제품 06시 기준으로 변경 (박형순 요청)
        /// </summary>
        public PRD010912()
        {
            InitializeComponent();
            
            cdvDate.Value = DateTime.Now;
            SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;            
        }


        #region 유효성검사 및 초기화
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
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
            int j = 13;

            GetWorkDay();

            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("Family", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("PKG Code", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);  
          
            spdData.RPT_AddBasicColumn("Operation", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
            
            for (int i = 0; i < strDate.Length; i++)
            {
                spdData.RPT_AddBasicColumn(strDate[i].ToString(), 0, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, j + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_MerageHeaderColumnSpan(0, j, 2);

                j = j + 2;                
            }

            spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("plan", 1, spdData.ActiveSheet.ColumnCount -1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("actual", 1, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnCount - 2, 2); ;

            for (int y = 0; y <= 12; y++)
            {
                spdData.RPT_MerageHeaderRowSpan(0, y, 2);
            }

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.            
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                        
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "MAT.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT_GRP_9", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_10", "MAT.MAT_GRP_10", "MAT.MAT_GRP_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT_CMF_11", "MAT.MAT_CMF_11", "MAT.MAT_CMF_11", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "CONV_MAT_ID", "MAT.CONV_MAT_ID", "MAT.CONV_MAT_ID", true);            
        }
        #endregion

        #region 시간관련 함수
        private void GetWorkDay()
        {
            DateTime Now = cdvDate.Value;

            for (int i = 0; i < 7; i++)
            {
                strDate[i] = Now.ToString("yyyyMMdd");                
                Now = Now.AddDays(1);
            }

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
                        
            string sFrom = null;
            string sTo = null;            
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값

            sFrom = strDate[0].ToString();
            sTo = strDate[6].ToString();

            //string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            //selectDate = cdvFromToDate.getSelectDate();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            
            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }


            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond3);
            strSqlString.Append("     , DAT.OPER" + "\n");

            for (int i = 0; i < strDate.Length; i++)
            {
                strSqlString.AppendFormat("     , ROUND(SUM(DECODE(DAT.WORK_DATE, '{0}', DAT.PLN_QTY,0)) / {1}, 0) AS PLN_QTY_{2}" + "\n", strDate[i].ToString(), sKpcsValue, i.ToString());
                strSqlString.AppendFormat("     , ROUND(SUM(DECODE(DAT.WORK_DATE, '{0}', DAT.END_QTY,0)) / {1}, 0) AS END_QTY_{2}" + "\n", strDate[i].ToString(), sKpcsValue, i.ToString());                
            }

            strSqlString.Append("     , ROUND(SUM(DAT.PLN_QTY) / " + sKpcsValue + ", 0) AS PLN_TTL" + "\n");
            strSqlString.Append("     , ROUND(SUM(DAT.END_QTY) / " + sKpcsValue + ", 0) AS SHP_TTL" + "\n"); 
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT CASE WHEN MAT_GRP_1 = 'HX' THEN MAT_CMF_10 ELSE MAT_ID END CONV_MAT_ID" + "\n");
            strSqlString.Append("             , MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_10, MAT_CMF_11" + "\n");
            strSqlString.Append("          FROM MWIPMATDEF" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND MAT_TYPE = 'FG' " + "\n");

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("           AND MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
            }

            #region 상세 조회에 따른 SQL문 생성

            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

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

            strSqlString.Append("       ) MAT" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, WORK_DATE, OPER" + "\n");
            strSqlString.Append("             , SUM(PLN_QTY) AS PLN_QTY" + "\n");
            strSqlString.Append("             , SUM(END_QTY) AS END_QTY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , PLAN_DATE AS WORK_DATE" + "\n");
            strSqlString.Append("                     , OPER" + "\n");
            strSqlString.Append("                     , QTY_1 AS PLN_QTY" + "\n");
            strSqlString.Append("                     , 0 AS END_QTY" + "\n");
            strSqlString.Append("                  FROM CPLNISSMAT@RPTTOMES" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND PLAN_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");            

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("                   AND LOT_TYPE LIKE '" + cdvType.Text + "' " + "\n");
            }

            strSqlString.Append("                 UNION ALL" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , WORK_DATE" + "\n");
            strSqlString.Append("                     , OPER" + "\n");
            strSqlString.Append("                     , 0 AS PLN_QTY" + "\n");
            strSqlString.Append("                     , (S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY" + "\n");
            strSqlString.Append("                  FROM RSUMWIPMOV " + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");            
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1 > 0 " + "\n");
            strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%' " + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
            }

            strSqlString.Append("                 UNION ALL" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , WORK_DATE" + "\n");
            strSqlString.Append("                     , OPER" + "\n");
            strSqlString.Append("                     , 0 AS PLN_QTY" + "\n");
            strSqlString.Append("                     , (S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY" + "\n");
            strSqlString.Append("                  FROM CSUMWIPMOV " + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1 > 0 " + "\n");
            strSqlString.Append("                   AND MAT_ID LIKE 'HX%' " + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
            }

            strSqlString.Append("                 UNION ALL" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , WORK_DATE" + "\n");
            strSqlString.Append("                     , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010','HMKS1','SZ010', 'HMKB1', 'BZ010') AS OPER" + "\n");
            strSqlString.Append("                     , 0 AS PLN_QTY" + "\n");
            strSqlString.Append("                     , (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS END_QTY " + "\n");
            strSqlString.Append("                  FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'  " + "\n");
            strSqlString.Append("                   AND FACTORY NOT IN ('RETURN') " + "\n");
            strSqlString.Append("                   AND CM_KEY_1='" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1 > 0   " + "\n");
            strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%' " + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
            }

            strSqlString.Append("                 UNION ALL" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , WORK_DATE" + "\n");
            strSqlString.Append("                     , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010','HMKS1','SZ010', 'HMKB1', 'BZ010') AS OPER" + "\n");
            strSqlString.Append("                     , 0 AS PLN_QTY" + "\n");
            strSqlString.Append("                     , (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS END_QTY " + "\n");
            strSqlString.Append("                  FROM CSUMFACMOV " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'  " + "\n");
            strSqlString.Append("                   AND FACTORY NOT IN ('RETURN') " + "\n");
            strSqlString.Append("                   AND CM_KEY_1='" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1 > 0   " + "\n");
            strSqlString.Append("                   AND MAT_ID LIKE 'HX%' " + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
            }

            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID, WORK_DATE, OPER" + "\n");            
            strSqlString.Append("       ) DAT" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = DAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("GROUP BY {0}, DAT.OPER" + "\n", QueryCond2);
            strSqlString.Append("HAVING SUM(DAT.PLN_QTY) > 0" + "\n");
            strSqlString.AppendFormat("ORDER BY {0}, OPER" + "\n", QueryCond1);

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

            DataTable dt = null;
            if (CheckField() == false) return;

            GridColumnInit();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                         
                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 12, null, null, btnSort);
                //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 12, 0, 1, true, Align.Center, VerticalAlign.Center);
                
                               
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
        
        /// <summary>
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

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);

            SortInit();
        }
        #endregion      

    }
}