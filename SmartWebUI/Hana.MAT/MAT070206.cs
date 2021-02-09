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

namespace Miracom.SmartWeb.UI
{
    public partial class MAT070206 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: MAT070206<br/>
        /// 클래스요약: 원부자재 유효기간 관리<br/>
        /// 작  성  자: 배수민<br/>
        /// 최초작성일: 2011-03-29<br/>
        /// 상세  설명: 원부자재 유효기간 관리<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 배수민<br />
        /// 2015-10-14-임종우 : 3~6 개월 구간 추가 (이슬기 요청)
        /// 2016-07-28-임종우 : 공정재고 데이터 추가 (임태성K 요청)
        /// </summary>
        public MAT070206()
        {
            InitializeComponent();

            SortInit();
            GridColumnInit();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            this.cdvMatType.sFactory = GlobalVariable.gsAssyDefaultFactory;

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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Material group", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Material code", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Material name", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Storage location", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LOT_NO", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("Effective date", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("quantity", 0, 6, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Months", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("months", 0, 8, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 40);
            spdData.RPT_AddBasicColumn("일", 0, 9, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 40);
            spdData.RPT_AddBasicColumn("Classification", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
       
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            try
            {  
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }

        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
                        
            strSqlString.Append("SELECT * " + "\n");
            strSqlString.Append("  FROM (" + "\n");            
            strSqlString.Append("        SELECT SUBSTR(A.MAT_ID,0,5) AS MAT_GROUP, A.MAT_ID AS MAT_ID, B.DATA_1 AS MAT_DESC, A.STORAGE_LOCATION AS LOCATION, A.LOT_ID AS LOT_ID " + "\n");
            strSqlString.Append("             , SUBSTR(A.EPN_DATE,0,4)||'-'||SUBSTR(A.EPN_DATE,5,2)||'-'||SUBSTR(A.EPN_DATE,7,2) AS EPN_DATE, A.QUANTITY AS QTY " + "\n");
            strSqlString.Append("             , CASE WHEN A.EPN_DATE <= TO_CHAR(SYSDATE, 'YYYYMMDD') THEN '0~0'" + "\n");            
            strSqlString.Append("                    WHEN A.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDD') THEN '0~1' " + "\n");
            strSqlString.Append("                    WHEN A.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDD') THEN '1~2' " + "\n");
            strSqlString.Append("                    WHEN A.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDD') THEN '2~3' " + "\n");
            strSqlString.Append("                    WHEN A.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,6), 'YYYYMMDD') THEN '3~6' " + "\n");           
            strSqlString.Append("               END AS MONTHS " + "\n");
            strSqlString.Append("             , TRUNC(MONTHS_BETWEEN(TO_DATE(A.EPN_DATE,'YYYYMMDD'), SYSDATE)) AS 개월 " + "\n");
            strSqlString.Append("             , TRUNC(MONTHS_BETWEEN(TO_DATE(A.EPN_DATE, 'YYYYMMDD'), SYSDATE) * 30.5) AS 일 " + "\n");
            strSqlString.Append("             , '1-창고' AS GUBUN" + "\n");
            strSqlString.Append("           FROM CWMSLOTSTS@RPTTOMES A " + "\n");
            strSqlString.Append("              , (SELECT KEY_1, DATA_1, DATA_6 FROM MGCMTBLDAT WHERE TABLE_NAME = 'MAT_MASTER') B " + "\n");
            strSqlString.Append("          WHERE 1=1 " + "\n");
            strSqlString.Append("            AND A.CLIENTID = '100' " + "\n");
            
            if (cdvMatType.Text.TrimEnd() != "")
            {
                strSqlString.Append("            AND B.DATA_6='" + cdvMatType.txtValue + "'\n");
            }

            strSqlString.Append("            AND A.MAT_ID LIKE '" + txtMatID.Text + "'" + "\n");
            strSqlString.Append("            AND (A.EPN_DATE BETWEEN TO_CHAR(SYSDATE,'YYYYMMDD') AND TO_CHAR(ADD_MONTHS(SYSDATE,6), 'YYYYMMDD') " + "\n");
            strSqlString.Append("             OR A.EPN_DATE <= TO_CHAR(SYSDATE, 'YYYYMMDD')) " + "\n");
            strSqlString.Append("            AND A.QUANTITY != 0 " + "\n");
            strSqlString.Append("            AND A.EPN_DATE <> ' ' " + "\n");
            strSqlString.Append("            AND A.MOVEMENT_CODE <> 'A11' " + "\n");
            strSqlString.Append("            AND A.STORAGE_LOCATION <> '2205' " + "\n");
            strSqlString.Append("            AND A.MAT_ID = B.KEY_1 " + "\n");
            strSqlString.Append("          UNION ALL " + "\n");
            strSqlString.Append("         SELECT SUBSTR(A.MAT_ID,0,5) AS MAT_GROUP" + "\n");
            strSqlString.Append("             , A.MAT_ID AS MAT_ID" + "\n");
            strSqlString.Append("             , B.MAT_DESC" + "\n");
            strSqlString.Append("             , A.OPER AS LOCATION" + "\n");
            strSqlString.Append("             , A.LOT_ID AS LOT_ID " + "\n");
            strSqlString.Append("             , SUBSTR(A.LOT_CMF_16,0,4)||'-'||SUBSTR(A.LOT_CMF_16,5,2)||'-'||SUBSTR(A.LOT_CMF_16,7,2) AS EPN_DATE" + "\n");
            strSqlString.Append("             , A.QTY_1 AS QTY          " + "\n");
            strSqlString.Append("             , CASE WHEN A.LOT_CMF_16 <= TO_CHAR(SYSDATE, 'YYYYMMDD') THEN '0~0' " + "\n");
            strSqlString.Append("                    WHEN A.LOT_CMF_16 <= TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDD') THEN '0~1' " + "\n");
            strSqlString.Append("                    WHEN A.LOT_CMF_16 <= TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDD') THEN '1~2' " + "\n");
            strSqlString.Append("                    WHEN A.LOT_CMF_16 <= TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDD') THEN '2~3' " + "\n");
            strSqlString.Append("                    WHEN A.LOT_CMF_16 <= TO_CHAR(ADD_MONTHS(SYSDATE,6), 'YYYYMMDD') THEN '3~6' " + "\n");
            strSqlString.Append("               END AS MONTHS " + "\n");
            strSqlString.Append("             , TRUNC(MONTHS_BETWEEN(TO_DATE(A.LOT_CMF_16,'YYYYMMDDHH24MISS'), SYSDATE)) AS 개월 " + "\n");
            strSqlString.Append("             , TRUNC(MONTHS_BETWEEN(TO_DATE(A.LOT_CMF_16, 'YYYYMMDDHH24MISS'), SYSDATE) * 30.5) AS 일" + "\n");
            strSqlString.Append("             , '2-공정' AS GUBUN" + "\n");
            strSqlString.Append("          FROM MWIPLOTSTS A" + "\n");
            strSqlString.Append("             , MWIPMATDEF B" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND A.LOT_DEL_FLAG = ' '   " + "\n");
            strSqlString.Append("           AND A.QTY_1 != 0" + "\n");
            strSqlString.Append("           AND A.OPER NOT LIKE 'A%'" + "\n");
            strSqlString.Append("           AND A.OPER <> 'V0000'" + "\n");
            strSqlString.Append("           AND B.DELETE_FLAG = ' '" + "\n");

            if (cdvMatType.Text.TrimEnd() != "")
            {
                strSqlString.Append("           AND B.MAT_TYPE='" + cdvMatType.txtValue + "'\n");
            }
                        
            strSqlString.Append("           AND (A.LOT_CMF_16 BETWEEN TO_CHAR(SYSDATE,'YYYYMMDD') AND TO_CHAR(ADD_MONTHS(SYSDATE,6), 'YYYYMMDD') " + "\n");
            strSqlString.Append("            OR A.LOT_CMF_16 <= TO_CHAR(SYSDATE, 'YYYYMMDD'))     " + "\n");
            strSqlString.Append("           AND A.LOT_CMF_16 <> ' ' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE '" + txtMatID.Text + "'" + "\n");            
            strSqlString.Append("       ) A " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");

            //if (cboPeriod.Text.Trim() == "유효기간초과")
            if (cboPeriod.SelectedIndex == 1)
            {                
                strSqlString.Append("   AND MONTHS = '0~0' " + "\n");
            }
            //else if (cboPeriod.Text.Trim() == "0~1개월사이")
            else if (cboPeriod.SelectedIndex == 2)
            {
                strSqlString.Append("   AND MONTHS = '0~1' " + "\n");
            }
            //else if (cboPeriod.Text.Trim() == "1~2개월사이")
            else if (cboPeriod.SelectedIndex == 3)
            {
                strSqlString.Append("   AND MONTHS = '1~2' " + "\n");
            }
            //else if (cboPeriod.Text.Trim() == "2~3개월사이")
            else if (cboPeriod.SelectedIndex == 4)
            {
                strSqlString.Append("   AND MONTHS = '2~3' " + "\n");
            }
            //else if (cboPeriod.Text.Trim() == "3~6개월사이")
            else if (cboPeriod.SelectedIndex == 5)
            {
                strSqlString.Append("   AND MONTHS = '3~6' " + "\n");
            }

            if (cdvGubun.Text != "ALL")
            {
                strSqlString.Append("   AND GUBUN = '" + cdvGubun.Text + "' " + "\n");
            }

            strSqlString.Append(" ORDER BY GUBUN, MONTHS, MAT_GROUP, MAT_ID, LOCATION, EPN_DATE, LOT_ID ASC " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

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

                // 2012-04-17-배수민 : 주요 컬럼마다 색깔 부여
                for (int i = 0; i < spdData.ActiveSheet.ColumnCount; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "Material group")
                    {
                        spdData.ActiveSheet.Columns[i].BackColor = Color.Azure;
                    }
                    else if (spdData.ActiveSheet.Columns[i].Label == "Effective date")
                    {
                        spdData.ActiveSheet.Columns[i].BackColor = Color.LightYellow;
                    }
                }  

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
            spdData.ExportExcel();
        }

        private void cdvMatType_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery = "SELECT KEY_1, DATA_1 FROM MGCMTBLDAT WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME='MAT_TYPE' ORDER BY KEY_1 ";
            
            cdvMatType.sDynamicQuery = strQuery;
        }
    }
}
