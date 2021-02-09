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
    public partial class PRD011017 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011017<br/>
        /// 클래스요약: 제품별 UPH 조회<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2015-04-27<br/>
        /// 상세  설명: 제품별 UPH 조회(백성호 요청)<br/>
        /// 변경  내용: <br/>  
        /// 2015-05-07-임종우 : MIN, MAX 값 추가 (임태성K 요청)
        /// 2015-06-04-임종우 : WPH 로직 변경 - UPH / (NET Die * Wafer 수율)
        /// 2020-02-29-김미경 : TEST UPH 조회 공정 추가(이창훈 대리)
        /// 2020-04-08-김미경 : SAP 코드 추가 (이승희 D)
        /// </summary>
        public PRD011017()
        {
            InitializeComponent();                                   
            SortInit();
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        #region 초기화 및 유효성 검사
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
            spdData.RPT_ColumnInit();
            
            try
            {
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PACKAGE2", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD COUNT", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG CODE", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("FAMILY", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("TYPE1", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("TYPE2", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("DENSITY", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("GENERATION", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("SAP_CODE", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("OPER", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("MODEL", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("UPH", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("MIN", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("MAX", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("WPH", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Logic UPH", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                spdData.RPT_ColumnConfigFromTable(btnSort);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT_GRP_9", "MAT_GRP_9 AS MAJOR", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_10", "MAT_GRP_10 AS PKG", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT_GRP_6", "MAT_GRP_6 AS LD_COUNT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "CONV_MAT_ID", "CONV_MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT_CMF_11", "MAT_CMF_11 AS PKG_CODE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT_GRP_2 AS FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "MAT_GRP_5 AS TYPE2", true);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "MAT_GRP_7 AS DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "MAT_GRP_8 AS GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "MAT_CMF_10 AS PIN_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP_CODE", "BASE_MAT_ID,VENDOR_MAT_ID", "CASE WHEN OPER <= 'A0200' THEN VENDOR_MAT_ID ELSE BASE_MAT_ID END SAP_CODE", true);     
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

            string QueryCond1;
            string QueryCond2;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);            
            strSqlString.Append("     , OPER, RES_MODEL" + "\n");
            strSqlString.Append("     , ROUND(AVG(UPEH), 1) AS UPEH" + "\n");
            strSqlString.Append("     , ROUND(MIN(UPEH), 1) AS MIN_UPEH" + "\n");
            strSqlString.Append("     , ROUND(MAX(UPEH), 1) AS MAX_UPEH" + "\n");
            strSqlString.Append("     , ROUND(AVG(WPH), 1) AS WPH" + "\n");
            strSqlString.Append("     , MAX(DECODE(L_UPEH,' ',0,L_UPEH)) AS L_UPEH" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT B.*" + "\n");
            strSqlString.Append("             , CASE WHEN B.MAT_GRP_1 = 'HX' THEN B.MAT_CMF_10" + "\n");
            strSqlString.Append("                    WHEN B.MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(B.MAT_ID, -3)" + "\n");
            strSqlString.Append("                    ELSE B.MAT_ID" + "\n");
            strSqlString.Append("               END CONV_MAT_ID" + "\n");
            strSqlString.Append("             , A.OPER, A.RES_MODEL, A.UPEH" + "\n");
            strSqlString.Append("             , A.UPEH / (B.NET_DIE * (NVL(C.DAY_YIELD, 90) / 100)) AS WPH " + "\n");
            strSqlString.Append("             , A.RESV_FIELD_6 L_UPEH " + "\n");
            //strSqlString.Append("             , CASE WHEN A.RESV_FIELD_9 IN (' ', 0) THEN NULL" + "\n");
            //strSqlString.Append("                    ELSE 3600 / A.RESV_FIELD_9" + "\n");
            //strSqlString.Append("               END AS WPH" + "\n");
            strSqlString.Append("          FROM CRASUPHDEF A" + "\n");
            strSqlString.Append("             , VWIPMATDEF B" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID, NET_DIE, DECODE(DAY_YIELD, 0, 90, DAY_YIELD) AS DAY_YIELD" + "\n");
            strSqlString.Append("                  FROM RSUMWAFYLD" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND WORK_DATE = TO_CHAR(SYSDATE-1, 'YYYYMMDD')" + "\n");
            strSqlString.Append("               ) C" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("           AND B.MAT_TYPE = 'FG'" + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("           AND B.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }

            #region 상세조회
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
            #endregion

            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND OPER " + cdvOper.SelectedValueToQueryString + "\n");

            

            strSqlString.AppendFormat(" GROUP BY {0}, OPER, RES_MODEL " + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY {0}, OPER, RES_MODEL " + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
        }        

        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {                       
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();            

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                                
                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2); 

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 13, 13, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
        
                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);

                for (int i = 0; i <= 11; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LightYellow;
                }

                dt.Dispose();
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
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);            
            cdvOper.sFactory = cdvFactory.txtValue;

            cdvOper.SetChangedFlag(true);
            cdvOper.Text = "";
            string strQuery = string.Empty;            

            strQuery += "SELECT DISTINCT OPR.OPER CODE, OPR.OPER_DESC DATA" + "\n";
            strQuery += "  FROM MRASRESMFO RES, MWIPOPRDEF OPR " + "\n";
            strQuery += " WHERE RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n";
            strQuery += "   AND REL_LEVEL='R' " + "\n";

            if (cdvFactory.txtValue.Equals(GlobalVariable.gsAssyDefaultFactory))
            {                
                strQuery += "   AND RES.OPER NOT IN ('A0100','A0150','A0250','A0320','A0330','A0340','A0350','A0370','A0380','A0390','A0500','A0501',";
                strQuery += "'A0502','A0503','A0504','A0505','A0506','A0507','A0508','A0509','A0550','A0950','A1100','A1110','A1180','A1230','A1720','A1950', 'A2050', 'A2100')" + "\n";
                strQuery += "   AND OPR.OPER LIKE 'A%'" + "\n";
            }
            else
            {
                strQuery += "   AND RES.OPER IN ('T0100', 'T0500', 'T0650', 'T0760', 'T0900', 'T0960', 'T1040','T1080','T1200')" + "\n";
            }

            strQuery += "   AND RES.FACTORY=OPR.FACTORY " + "\n";
            strQuery += "   AND RES.OPER=OPR.OPER " + "\n";
            strQuery += "ORDER BY OPR.OPER " + "\n";

            if (cdvFactory.txtValue != "")
                cdvOper.sDynamicQuery = strQuery;
            else
                cdvOper.sDynamicQuery = "";
        }

        
        #endregion

    }       
}
