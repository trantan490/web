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

namespace Hana.RAS
{
    /// <summary>
    /// 클  래  스: RAS020701<br/>
    /// 클래스요약: 설비 기준 정보 조회<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2018-04-04<br/>
    /// 상세  설명: 설비 기준 정보 조회<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class RAS020701 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020701()
        {
            InitializeComponent();            
            SortInit();
            GridColumnInit();
        }

        #region " Function Definition "

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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("파트", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EQP Type", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Model", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Equipment name", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Discription", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 200);
            spdData.RPT_AddBasicColumn("Resource type", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Area", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Sub Area", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Location", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Mold Product Type", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("UPEH Group", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("In-line equipment status", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Timer Interval", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Send Time", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("SERIAL", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Asset Management Number", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Class", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Asset classification", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Warehousing date", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Date of manufacture", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("DISPATCH", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("AUTO ENABLE", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("USE STATE", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("MOMENT", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("MOMENT_GRP", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LOT_TRACKING", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Idle equipment", 0, 28, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("MY_MC", 0, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Suspension of the service equipment", 0, 30, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("RMS", 0, 31, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("IMS", 0, 32, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("EDITOR", 0, 33, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("MODULE_NUM", 0, 34, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("STARTED_TIME", 0, 35, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("UP_DOWN_FLAG", 0, 36, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("UP_DOWN_CODE", 0, 37, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("ARRANGE_MAT_ID", 0, 38, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("ARRANGE_OPER", 0, 39, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {            
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            strSqlString.Append("SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_1), '-') AS TEAM" + "\n");
            strSqlString.Append("     , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_2), '-') AS PART " + "\n");
            strSqlString.Append("     , RES_GRP_3 AS EQP_TYPE" + "\n");
            strSqlString.Append("     , RES_GRP_5 AS MAKER" + "\n");
            strSqlString.Append("     , RES_GRP_6 AS MODEL" + "\n");
            strSqlString.Append("     , RES_ID" + "\n");
            strSqlString.Append("     , RES_DESC" + "\n");
            strSqlString.Append("     , RES_TYPE" + "\n");
            strSqlString.Append("     , AREA_ID" + "\n");
            strSqlString.Append("     , SUB_AREA_ID" + "\n");
            strSqlString.Append("     , RES_LOCATION" + "\n");
            strSqlString.Append("     , RES_GRP_4 AS MOLD_PRODUCT_TYPE" + "\n");
            strSqlString.Append("     , RES_GRP_7 AS UPEH_GROUP" + "\n");
            strSqlString.Append("     , RES_GRP_8 AS IN_LINE" + "\n");
            strSqlString.Append("     , RES_GRP_9 AS TIMER_INTERVAL" + "\n");
            strSqlString.Append("     , RES_GRP_10 AS SEND_TIME" + "\n");
            strSqlString.Append("     , RES_CMF_1 AS SERIAL" + "\n");
            strSqlString.Append("     , RES_CMF_2 AS \"자산관리번호\"" + "\n");
            strSqlString.Append("     , RES_CMF_3 AS CLASS" + "\n");
            strSqlString.Append("     , RES_CMF_4 AS \"자산구분\"" + "\n");
            strSqlString.Append("     , RES_CMF_5 AS \"입고일자\"" + "\n");
            strSqlString.Append("     , RES_CMF_6 AS \"제작일자\"" + "\n");
            strSqlString.Append("     , RES_CMF_7 AS DISPATCH" + "\n");
            strSqlString.Append("     , RES_CMF_8 AS AUTO_ENABLE" + "\n");
            strSqlString.Append("     , RES_CMF_9 AS USE_STATE" + "\n");
            strSqlString.Append("     , RES_CMF_10 AS MOMENT" + "\n");
            strSqlString.Append("     , RES_CMF_11 AS MOMENT_GRP" + "\n");
            strSqlString.Append("     , RES_CMF_12 AS LOT_TRACKING" + "\n");
            strSqlString.Append("     , RES_CMF_13 AS \"유휴설비\"" + "\n");
            strSqlString.Append("     , RES_CMF_14 AS MY_MC" + "\n");
            strSqlString.Append("     , RES_CMF_15 AS \"운휴설비\"" + "\n");
            strSqlString.Append("     , RES_CMF_16 AS RMS" + "\n");
            strSqlString.Append("     , RES_CMF_17 AS IMS" + "\n");
            strSqlString.Append("     , RES_CMF_18 AS EDITOR" + "\n");
            strSqlString.Append("     , RES_CMF_19 AS MODULE_NUM" + "\n");
            strSqlString.Append("     , RES_CMF_20 AS STARTED_TIME" + "\n");
            strSqlString.Append("     , RES_UP_DOWN_FLAG" + "\n");
            strSqlString.Append("     , RES_STS_1 AS UP_DOWN_CODE" + "\n");
            strSqlString.Append("     , RES_STS_2 AS ARRANGE_MAT_ID" + "\n");
            strSqlString.Append("     , RES_STS_8 AS ARRANGE_OPER" + "\n");
            strSqlString.Append("  FROM MRASRESDEF RES" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND DELETE_FLAG = ' '" + "\n");

            if (ckbUseFlag.Checked == true)
            {
                strSqlString.Append("   AND RES_CMF_9 = 'Y'" + "\n");
            }

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("   AND RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("   AND RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("   AND RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("   AND RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("   AND RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("   AND RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("   AND RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            #endregion

            strSqlString.Append(" ORDER BY TEAM, PART, EQP_TYPE, MAKER, MODEL, RES_ID" + "\n");

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
 
                spdData.DataSource = dt;
 
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
            spdData.ExportExcel();            
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
        }
        #endregion

    }
}