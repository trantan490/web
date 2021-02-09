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

namespace Hana.MAT
{
    public partial class MAT070207 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private DataTable dtWeek = new DataTable();

        /// <summary>
        /// 클  래  스: MAT070207<br/>
        /// 클래스요약: 원부자재 유효기간 초과 제품 관리<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2013-12-09<br/>
        /// 상세  설명: 원부자재 유효기간 초과 제품 관리(김권수 요청)<br/>
        /// 변경  내용: <br/>
        /// 2013-12-13-임종우 : 대상기준 : 유효기간 경과제품 -> 유효기간 3개월 이전 ~ 경과제품으로 AutoMail 과 동일하게 (김권수 요청)
        /// 2014-01-22-임종우 : 그룹정보 추가 - 자재그룹, 자재코드, 자재명, 적용제품, 업체 CODE, PRODUCT (김권수 요청)
        /// </summary>
        public MAT070207()
        {
            InitializeComponent();

            SortInit();
            GridColumnInit();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            this.cdvMatType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            //cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

            //cdvOper.Text = "";

        }

        #region " Constant Definition "

        #endregion

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (String.IsNullOrEmpty(cdvMatType.Text))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD003", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            GetWorkWeek();
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Material group", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Material code", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Material name", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Applicable product", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Vendor code", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PRODUCT", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Usage", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Inventory quantity", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 50);
            spdData.RPT_AddBasicColumn("Conversion amount", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("TOTAL", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 40);

            for (int i = 0; i < dtWeek.Rows.Count; i++)
            {
                spdData.RPT_AddBasicColumn("WW" + dtWeek.Rows[i][0].ToString().Substring(4, 2), 0, i + 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 50);
            }

            spdData.RPT_AddBasicColumn("BALANCE", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 40);            

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            try
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material group", "B.MAT_TYPE", "B.MAT_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material code", "A.MAT_ID", "A.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material name", "A.MAT_ID", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'MAT_MASTER' AND KEY_1 = A.MAT_ID) AS MAT_DESC", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Applicable product", "B.SAP_CODE", "B.SAP_CODE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Vendor code", "B.MAT_GRP_1", "B.MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "B.PRODUCT", "B.PRODUCT", false);  
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }

        }

        #region 시간관련 함수
        private void GetWorkWeek()
        {
            dtWeek = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());                           
        }

        #endregion

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            //string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            //QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);            
            strSqlString.Append("     , MAX(B.UNIT_QTY) AS UNIT_QTY" + "\n");
            strSqlString.Append("     , MAX(A.QTY) AS QTY" + "\n");
            strSqlString.Append("     , ROUND(MAX(A.QTY) / MAX(B.UNIT_QTY) * 1000, 0) AS CONVERT_QTY" + "\n");
            strSqlString.Append("     , SUM(C.TTL) AS TTL" + "\n");
            strSqlString.Append("     , SUM(C.W1) AS W1" + "\n");
            strSqlString.Append("     , SUM(C.W2) AS W2" + "\n");
            strSqlString.Append("     , SUM(C.W3) AS W3" + "\n");
            strSqlString.Append("     , SUM(C.W4) AS W4" + "\n");
            strSqlString.Append("     , SUM(C.W5) AS W5" + "\n");
            strSqlString.Append("     , SUM(C.W6) AS W6" + "\n");
            strSqlString.Append("     , ROUND(MAX(A.QTY) / MAX(B.UNIT_QTY) * 1000, 0) - SUM(C.TTL) AS BALANCE" + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(QUANTITY) AS QTY  " + "\n");
            strSqlString.Append("          FROM CWMSLOTSTS@RPTTOMES" + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND MAT_ID LIKE 'R%' " + "\n");
            strSqlString.Append("           AND EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND QUANTITY > 0   " + "\n");
            strSqlString.Append("           AND MOVEMENT_CODE <> 'A11' " + "\n");
            strSqlString.Append("           AND STORAGE_LOCATION <> '2205' " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT A.MAT_ID AS PRODUCT, A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_GRP_9 " + "\n");
            strSqlString.Append("             , A.MAT_CMF_7, A.MAT_CMF_10, A.MAT_CMF_13, A.VENDOR_ID AS SAP_CODE, A.MAT_ID " + "\n");
            strSqlString.Append("             , B.STEPID, B.MATCODE, B.DESCRIPT, B.PAR_BASE_QTY, B.UNIT_QTY, B.UNIT, B.RESV_FIELD_2 AS MAT_TYPE  " + "\n");
            strSqlString.Append("          FROM MWIPMATDEF A  " + "\n");
            strSqlString.Append("             , CWIPBOMDEF B " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.PARTNUMBER(+)  " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
            strSqlString.Append("           AND A.MAT_TYPE = 'FG'  " + "\n");
            strSqlString.Append("           AND A.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND B.RESV_FLAG_1(+) = 'Y'  " + "\n");
            strSqlString.Append("           AND B.RESV_FIELD_2(+) <> 'WW' " + "\n");
            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(DECODE(PLAN_WEEK, '" + dtWeek.Rows[0][0].ToString() + "', WW_QTY, 0)) AS W1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(PLAN_WEEK, '" + dtWeek.Rows[1][0].ToString() + "', WW_QTY, 0)) AS W2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(PLAN_WEEK, '" + dtWeek.Rows[2][0].ToString() + "', WW_QTY, 0)) AS W3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(PLAN_WEEK, '" + dtWeek.Rows[3][0].ToString() + "', WW_QTY, 0)) AS W4 " + "\n");
            strSqlString.Append("             , SUM(DECODE(PLAN_WEEK, '" + dtWeek.Rows[4][0].ToString() + "', WW_QTY, 0)) AS W5 " + "\n");
            strSqlString.Append("             , SUM(DECODE(PLAN_WEEK, '" + dtWeek.Rows[5][0].ToString() + "', WW_QTY, 0)) AS W6 " + "\n");
            strSqlString.Append("             , SUM(WW_QTY) AS TTL " + "\n");
            strSqlString.Append("          FROM RWIPPLNWEK" + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND PLAN_WEEK BETWEEN '" + dtWeek.Rows[0][0].ToString() + "' AND '" + dtWeek.Rows[5][0].ToString() + "'" + "\n");
            strSqlString.Append("           AND  GUBUN = '3' " + "\n");            
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) C" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MATCODE(+)" + "\n");
            strSqlString.Append("   AND B.MAT_ID = C.MAT_ID(+)" + "\n");

            if (cdvMatType.Text.TrimEnd() != "")
            {
                strSqlString.Append("   AND B.MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");
            }
            
            strSqlString.Append("   AND A.MAT_ID LIKE '" + txtMatID.Text + "'" + "\n");
            strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond1);            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }       

        private string MakeSqlString1() // 검색 월에 해당하는 주차 리스트 가져오기
        {
            string Today;           

            Today = DateTime.Now.ToString("yyyyMMdd");// cdvDate.Value.ToString("yyyyMMdd");

            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT PLAN_WEEK" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT PLAN_WEEK" + "\n");
            strSqlString.Append("             , ROW_NUMBER() OVER(ORDER BY PLAN_WEEK) AS RN" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT DISTINCT PLAN_YEAR||LPAD(PLAN_WEEK,2,'0') AS PLAN_WEEK " + "\n");
            strSqlString.Append("                  FROM MWIPCALDEF" + "\n");
            strSqlString.Append("                 WHERE CALENDAR_ID = 'OTD' " + "\n");            
            strSqlString.Append("                   AND SYS_DATE >= '" + Today + "'" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE RN <= 6" + "\n");
            strSqlString.Append(" ORDER BY PLAN_WEEK" + "\n");

            return strSqlString.ToString();
        }

        private string MakeSqlStringForPopup(string strMatId)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT A.MAT_ID " + "\n");
            strSqlString.Append("     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'MAT_MASTER' AND KEY_1 = A.MAT_ID) AS MAT_DESC " + "\n");
            strSqlString.Append("     , A.STORAGE_LOCATION " + "\n");
            strSqlString.Append("     , A.LOT_ID " + "\n");
            strSqlString.Append("     , SUBSTR(A.EPN_DATE,0,4)||'-'||SUBSTR(A.EPN_DATE,5,2)||'-'||SUBSTR(A.EPN_DATE,7,2) AS EPN_DATE " + "\n");
            strSqlString.Append("     , A.QUANTITY " + "\n");
            strSqlString.Append("     , CASE WHEN A.EPN_DATE <= TO_CHAR(SYSDATE, 'YYYYMMDD') THEN '0~0'  " + "\n");
            strSqlString.Append("            WHEN A.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDD') THEN '0~1' " + "\n");
            strSqlString.Append("            WHEN A.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDD') THEN '1~2' " + "\n");
            strSqlString.Append("            WHEN A.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDD') THEN '2~3' " + "\n");
            strSqlString.Append("       END AS MONTHS " + "\n");
            strSqlString.Append("     , TRUNC(MONTHS_BETWEEN(TO_DATE(A.EPN_DATE, 'YYYYMMDD'), SYSDATE)) AS MONTHS1" + "\n");
            strSqlString.Append("     , TRUNC(MONTHS_BETWEEN(TO_DATE(A.EPN_DATE, 'YYYYMMDD'), SYSDATE) * 30.5) AS DAY " + "\n");
            strSqlString.Append("  FROM CWMSLOTSTS@RPTTOMES A" + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND MAT_ID LIKE 'R%' " + "\n");
            strSqlString.Append("   AND EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDD') " + "\n");
            strSqlString.Append("   AND QUANTITY > 0   " + "\n");
            strSqlString.Append("   AND MOVEMENT_CODE <> 'A11' " + "\n");
            strSqlString.Append("   AND STORAGE_LOCATION <> '2205' " + "\n");
            strSqlString.Append("   AND MAT_ID = '" + strMatId + "' " + "\n");
            strSqlString.Append(" ORDER BY MONTHS, SUBSTR(A.MAT_ID,0,5), MAT_ID, STORAGE_LOCATION, EPN_DATE, LOT_ID ASC" + "\n");            

            return strSqlString.ToString();
        }

        #endregion

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

                //Total, Sub Total 적용 안하므로 아래 소스들 모두 주석 처리 
                spdData.DataSource = dt;
                                
                for (int i = 0; i <= 5; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LightYellow;                 
                }

                //spdData.ActiveSheet.Columns[0].BackColor = Color.Turquoise;
                //spdData.ActiveSheet.Columns[1].BackColor = Color.WhiteSmoke;
                //spdData.ActiveSheet.Columns[2].BackColor = Color.LightYellow;

                //spdData.Sheets[0].FrozenColumnCount = 3; // 컬럼 고정 

                ////그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                ////int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John
                ////1.Griid 합계 표시
                ////int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub, 9, null, null, btnSort);
                ////spdData.DataSource = dt;

                ////2. 칼럼 고정(필요하다면..)
                ////spdData.Sheets[0].FrozenColumnCount = 10;

                ////3. Total부분 셀머지
                ////spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);

                ////4. Column Auto Fit
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

        private void cdvMatType_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery = "SELECT KEY_1, DATA_1 FROM MGCMTBLDAT WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME='MATERIAL_TYPE' AND DATA_2 = 'Y' ORDER BY KEY_1 ";
            
            cdvMatType.sDynamicQuery = strQuery;
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column != 7)
                return;            

            string strMatId = spdData.ActiveSheet.Cells[e.Row, 1].Value.ToString();

            if (strMatId == " ")
            {
                CmnFunction.ShowMsgBox("자재코드가 표시 된 상태로 조회 하시기 바랍니다.");
                return;
            }
            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup(strMatId));

            if (dt != null && dt.Rows.Count > 0)
            {
                System.Windows.Forms.Form frm = new MAT070207_P1("", dt);
                frm.ShowDialog();
            }
        }                
    }
}
