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
    public partial class CUS060108 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: CUS060108<br/>
        /// 클래스요약: FCI 정산 Report<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2009-10-28<br/>
        /// 상세  설명: 고객사 FCI 정산 Report<br/>
        /// 변경  내용: <br/>
        /// 2012-02-29-임종우 : FGS, CUSTOMER 검색 기능 추가 (김형규 요청)
        /// 2014-05-28-임종우 : 제품기준정보 삭제 된 제품도 표시 되도록 수정 (박민정 요청)
        /// 2017-07-25-임종우 : 전체 고객사 조회 가능하도록 변경 (엄재운C 요청)
        /// </summary>
        /// 
        public CUS060108()
        {
            InitializeComponent();
            //SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsTestDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;
            cdvCustomer.sFactory = cdvFactory.txtValue;
            cdvFromToDate.AutoBinding(DateTime.Today.ToString("yyyy-MM") + "-01", DateTime.Today.ToString("yyyy-MM-dd"));
        }

        #region 유효성 검사 및 초기화

        /// <summary>
        /// 1. 유효성 검사
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.txtValue.Trim() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvCustomer.Text.Equals("") || cdvCustomer.Text.Equals("ALL"))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD038", GlobalVariable.gcLanguage));
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
            spdData.RPT_AddBasicColumn("DEVICE", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("RUN ID", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("BODY SIZE", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("ASSY IN", 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("ASSY OUT", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);            
            spdData.RPT_AddBasicColumn("ASSY LOSS", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("TEST IN", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("TEST OUT", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("TEST FAIL", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("T&R LOSS", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("SHIP QTY", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

            spdData.RPT_ColumnConfigFromTable(btnSort);
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("RUN ID", "SHP.LOT_CMF_4", true);
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
            string Start_Tran_Time = null;
            string End_Tran_Time = null;

            Start_Tran_Time = cdvFromToDate.ExactFromDate;
            End_Tran_Time = cdvFromToDate.ExactToDate;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            strSqlString.Append("SELECT MAT.MAT_CMF_7 " + "\n");
            strSqlString.Append("     , SHP.LOT_CMF_4 " + "\n");
            strSqlString.Append("     , MAT.MAT_CMF_9 " + "\n");
            strSqlString.Append("     , '' AS ASSY_IN" + "\n");
            strSqlString.Append("     , '' AS ASSY_OUT" + "\n");
            strSqlString.Append("    , (  SELECT SUM(LSM.LOSS_QTY)" + "\n");
            strSqlString.Append("           FROM RWIPLOTLSM LSM," + "\n");
            strSqlString.Append("                RWIPLOTSTS LOT" + "\n");
            strSqlString.Append("          WHERE LSM.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("            AND LSM.MAT_VER = 1  " + "\n");
            strSqlString.Append("            AND LSM.HIST_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("            AND LSM.LOT_ID=LOT.LOT_ID" + "\n");
            strSqlString.Append("            AND LOT.LOT_CMF_4 = SHP.LOT_CMF_4" + "\n");
            strSqlString.Append("       ) AS ASSY_LOSS" + "\n");
            strSqlString.Append("     , '' AS TEST_IN" + "\n");
            strSqlString.Append("     , '' AS TEST_OUT" + "\n");
            strSqlString.Append("    , (  SELECT SUM(LSM.LOSS_QTY)" + "\n");
            strSqlString.Append("           FROM RWIPLOTLSM LSM," + "\n");
            strSqlString.Append("                RWIPLOTSTS LOT" + "\n");
            strSqlString.Append("          WHERE LSM.FACTORY   = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("            AND LSM.MAT_VER = 1  " + "\n");
            strSqlString.Append("            AND LSM.OPER IN ('T0100')" + "\n");
            strSqlString.Append("            AND LSM.HIST_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("            AND LSM.LOT_ID=LOT.LOT_ID" + "\n");
            strSqlString.Append("            AND LOT.LOT_CMF_4 = SHP.LOT_CMF_4" + "\n");
            strSqlString.Append("       ) AS TEST_LOSS" + "\n");
            strSqlString.Append("     , (  SELECT SUM(LSM.LOSS_QTY)" + "\n");
            strSqlString.Append("           FROM RWIPLOTLSM LSM," + "\n");
            strSqlString.Append("                RWIPLOTSTS LOT" + "\n");
            strSqlString.Append("          WHERE LSM.FACTORY   = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("            AND LSM.MAT_VER = 1  " + "\n");
            strSqlString.Append("            AND LSM.OPER IN ('T0200', 'T0300', 'T0500', 'T0540', 'T0550', 'T0560', 'T0600', 'T0650', 'T0670', 'T0700', 'T0800', 'T0900', 'T1040', 'T1080', 'T1100', 'T1200', 'T1300', 'TZ010')" + "\n");
            strSqlString.Append("            AND LSM.HIST_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("            AND LSM.LOT_ID=LOT.LOT_ID" + "\n");
            strSqlString.Append("            AND LOT.LOT_CMF_4 = SHP.LOT_CMF_4" + "\n");
            strSqlString.Append("       ) AS TR_LOSS" + "\n");
            strSqlString.Append("      , SHP.SHIP_QTY" + "\n");
            strSqlString.Append("FROM (" + "\n");
            strSqlString.Append("       SELECT LOT_CMF_4, SUM(QTY) AS SHIP_QTY, MAT_ID " + "\n");
            strSqlString.Append("         FROM (" + "\n");
                                                // 최초 Merge 때의 수량 가져오기(더해진 Lot 이력)
            strSqlString.Append("                SELECT LOT_ID, OLD_QTY_1 AS QTY, LOT_CMF_4, MAT_ID" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT ROW_NUMBER() OVER (PARTITION BY LOT_ID ORDER BY HIST_SEQ) NUM, A.* " + "\n");
            strSqlString.Append("                          FROM RWIPLOTHIS A" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND LOT_ID IN ( " + "\n");
            strSqlString.Append("                                           SELECT LOT_ID" + "\n");
            strSqlString.Append("                                             FROM VWIPSHPLOT" + "\n");
            strSqlString.Append("                                            WHERE 1=1" + "\n");
            strSqlString.Append("                                              AND TO_FACTORY = '" + cboToFactory.Text + "'" + "\n");
            strSqlString.Append("                                              AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                              AND OWNER_CODE = 'PROD'" + "\n");
            //strSqlString.Append("                                              AND LOT_CMF_2 = 'FC'" + "\n");       
            strSqlString.Append("                                              AND LOT_CMF_2 = '" + cdvCustomer.Text + "'" + "\n");            
            strSqlString.Append("                                              AND TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                                         )" + "\n");            
            strSqlString.Append("                           AND FACTORY IN ('HMKA','" + GlobalVariable.gsTestDefaultFactory + "')" + "\n");
            strSqlString.Append("                           AND OLD_MAT_VER = 1" + "\n");
            strSqlString.Append("                           AND TRAN_CODE='MERGE'" + "\n");
            strSqlString.Append("                           AND OPER IN ('A2300', 'T1300')" + "\n");
            //strSqlString.Append("                           AND LOT_CMF_2='FC'" + "\n");
            strSqlString.Append("                           AND LOT_CMF_2 = '" + cdvCustomer.Text + "'" + "\n");
            strSqlString.Append("                           AND FROM_TO_FLAG='T'" + "\n");
            strSqlString.Append("                           AND HIST_DEL_FLAG <> 'Y'" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 WHERE NUM = 1" + "\n");
            strSqlString.Append("                 UNION ALL" + "\n");
                                                //  Merge 수량 가져오기..Ship 된 Lot 만 가져오기
            strSqlString.Append("                SELECT HIS.FROM_TO_LOT_ID, HIS.QTY_1-OLD_QTY_1, STS.LOT_CMF_4, STS.MAT_ID " + "\n");
            strSqlString.Append("                  FROM RWIPLOTHIS HIS," + "\n");
            strSqlString.Append("                       RWIPLOTSTS STS" + "\n");
            strSqlString.Append("                 WHERE HIS.LOT_ID IN ( " + "\n");
            strSqlString.Append("                                       SELECT LOT_ID" + "\n");
            strSqlString.Append("                                         FROM VWIPSHPLOT" + "\n");
            strSqlString.Append("                                        WHERE 1=1" + "\n");            
            strSqlString.Append("                                          AND TO_FACTORY = '" + cboToFactory.Text + "'" + "\n");
            strSqlString.Append("                                          AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                          AND OWNER_CODE = 'PROD'" + "\n");
            //strSqlString.Append("                                          AND LOT_CMF_2 = 'FC'" + "\n");
            strSqlString.Append("                                          AND LOT_CMF_2 = '" + cdvCustomer.Text + "'" + "\n"); 
            strSqlString.Append("                                          AND TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                                     )" + "\n");
            strSqlString.Append("                   AND HIS.FACTORY IN ('HMKA','" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
            strSqlString.Append("                   AND HIS.TRAN_CODE='MERGE'" + "\n");
            strSqlString.Append("                   AND HIS.FROM_TO_FLAG='T'" + "\n");
            strSqlString.Append("                   AND HIS.OPER IN ('A2300', 'T1300')" + "\n");
            strSqlString.Append("                   AND HIS.HIST_DEL_FLAG <> 'Y'" + "\n");
            strSqlString.Append("                   AND HIS.FROM_TO_LOT_ID = STS.LOT_ID" + "\n");
            strSqlString.Append("                 UNION ALL" + "\n");
                                                // Merge 안하고 Ship 된 이력
            strSqlString.Append("                SELECT LOT_ID, SHIP_QTY_1, LOT_CMF_4, MAT_ID" + "\n");
            strSqlString.Append("                  FROM (" + "\n");            
            strSqlString.Append("                         SELECT LOT_ID,SHIP_QTY_1,LOT_CMF_4, MAT_ID" + "\n");
            strSqlString.Append("                           FROM VWIPSHPLOT" + "\n");
            strSqlString.Append("                          WHERE 1=1" + "\n");            
            strSqlString.Append("                            AND OWNER_CODE = 'PROD'" + "\n");
            strSqlString.Append("                            AND TO_FACTORY = '" + cboToFactory.Text + "'" + "\n");
            strSqlString.Append("                            AND LOT_TYPE = 'W'" + "\n");
            //strSqlString.Append("                            AND LOT_CMF_2 = 'FC'" + "\n");
            strSqlString.Append("                            AND LOT_CMF_2 = '" + cdvCustomer.Text + "'" + "\n");
            strSqlString.Append("                            AND TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 WHERE LOT_ID NOT IN (SELECT LOT_ID FROM RWIPLOTMRG WHERE FROM_TO_FLAG='T' AND OPER IN ('A2300', 'T1300'))          " + "\n");
            strSqlString.Append("              )" + "\n");
            strSqlString.Append("        GROUP BY LOT_CMF_4, MAT_ID" + "\n");
            strSqlString.Append("     ) SHP" + "\n");
            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND SHP.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            //strSqlString.Append("   AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("ORDER BY MAT.MAT_CMF_7, SHP.LOT_CMF_4" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region EVEND 처리

        private void btnView_Click(object sender, EventArgs e)
        {
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

        #endregion

    }
}