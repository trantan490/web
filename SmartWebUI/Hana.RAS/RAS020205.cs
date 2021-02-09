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


namespace Hana.Ras
{
    /// <summary>
    /// 클  래  스: RAS020205<br/>
    /// 클래스요약: 정지 코드별<br/>
    /// 작  성  자: 하나마이크론 김준용<br/>
    /// 최초작성일: 2009-04-30<br/>
    /// 상세  설명: 정지 코드별 조회<br/>
    /// 변경  내용: <br/>
    /// </summary>
    /// 

    public partial class RAS020205 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020205()
        {
            InitializeComponent();
            cdvFromTo.DaySelector.SelectedValue = "DAY";
            SortInit();  
            GridColumnInit(); //해더 한줄짜리 샘플
            cdvFromTo.AutoBinding();
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

            if (cdvDownCode.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("RAS001", GlobalVariable.gcLanguage));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;  //컬럼 완전 초기화
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("파트", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Model", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Code", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddDynamicColumn(cdvFromTo, 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);          
        
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "DEPART", "DEPART", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY=RES.FACTORY AND TABLE_NAME='H_DEPARTMENT' AND KEY_1=RES.RES_GRP_1) AS DEPART", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "PART", "PART", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY=RES.FACTORY AND TABLE_NAME='H_DEPARTMENT' AND KEY_1=RES.RES_GRP_2) AS PART", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "MODEL", "MODEL", "RES.RES_GRP_6 AS MODEL", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment", "RES_ID", "RES_ID", "RES.RES_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DOWN CODE", "DOWN_CODE", "DOWN_CODE", "HIS.DOWN_NEW_STS_1 AS DOWN_CODE", false);
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
                        
            string strDate = string.Empty;

            int Between = cdvFromTo.SubtractBetweenFromToDate + 1;

            string[] selectDate1 = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            selectDate1 = cdvFromTo.getSelectDate();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;
            string strFromdy = cdvFromTo.HmFromDay;

            strSqlString.AppendFormat("SELECT  " + QueryCond1 + " \n");
            strSqlString.AppendFormat("        " +cdvFromTo.getRepeatQuery("NVL(SUM(CNT","),0) AS","CNT") + "\n");
            strSqlString.AppendFormat("        FROM    (" + "\n");
            strSqlString.AppendFormat("                 SELECT  " + QueryCond2 + "\n");
            strSqlString.AppendFormat("                 " + cdvFromTo.getDecodeQuery("DECODE(WORK_DAY", "COUNT(*)) AS", "CNT"));
            strSqlString.AppendFormat("                 FROM    (" + "\n");
            strSqlString.AppendFormat("                          SELECT  " + QueryCond3 + "\n");
            strSqlString.AppendFormat("                                  ,GET_WORK_DATE(DOWN_TRAN_TIME,'D') AS WORK_DAY" + "\n");
            strSqlString.AppendFormat("                          FROM    CRASRESDWH HIS," + "\n");
            strSqlString.AppendFormat("                                  MRASRESDEF RES" + "\n");
            strSqlString.AppendFormat("                          WHERE   1=1" + "\n");
            strSqlString.AppendFormat("                                  AND RES.FACTORY = HIS.FACTORY" + "\n");
            strSqlString.AppendFormat("                                  AND RES.RES_ID = HIS.RES_ID" + "\n");
            strSqlString.AppendFormat("                                  AND RES.FACTORY = '" + cdvFactory.Text + "'" +" \n");
            strSqlString.AppendFormat("                                  AND HIS.DOWN_NEW_STS_1 LIKE '" + cdvDownCode.Text + "%'" + "\n");
            strSqlString.AppendFormat("                                  AND HIS.DOWN_NEW_STS_1 " + cdvDownCodeDetail.SelectedValueToQueryString + "\n");
            strSqlString.AppendFormat("                                  AND HIS.DOWN_TRAN_TIME BETWEEN '" + cdvFromTo.getFromTranTime() + "' AND '" + cdvFromTo.getToTranTime() + "'" + "\n");
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("                                  AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("                                  AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("                                  AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("                                  AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("                                  AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("                                  AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
            strSqlString.AppendFormat("                         )" + "\n");
            strSqlString.AppendFormat("                 GROUP BY " + QueryCond1 +",WORK_DAY" + "\n");
            strSqlString.AppendFormat("                )" + "\n");
            strSqlString.AppendFormat("GROUP BY " + QueryCond1 + "\n");
            strSqlString.AppendFormat("ORDER BY " + QueryCond1 + "\n");

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
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 5, null, null);

                //4. Column Auto Fit4
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
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
            spdData.ExportExcel();
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvDownCode.sFactory = cdvFactory.txtValue;
            cdvDownCode.Init();
            cdvDownCodeDetail.Init();
        }
        #endregion

        private void cdvLoss_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            cdvDownCodeDetail.Init();
            cdvDownCodeDetail.sDynamicQuery = "SELECT DISTINCT KEY_2,'' AS DATA_1 FROM CWIPSITDEF WHERE FACTORY = '" + cdvFactory.Text + "'" + "AND TABLE_NAME = 'MAINT_CODE' AND LENGTH(KEY_2) = 4 AND KEY_2 LIKE '" + cdvDownCode.Text + "%'" + "ORDER BY KEY_2";
        }

    }
}