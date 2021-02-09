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
    public partial class PRD010909 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010909<br/>
        /// 클래스요약: 반제풒 파일 생성(OMS 다운으로 인해 임시로..)<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-09-02<br/>
        /// 상세  설명: 반제풒 파일 생성(OMS 다운으로 인해 임시로..)<br/>
        /// 변경  내용: <br/>

        /// </summary>
        public PRD010909()
        {
            InitializeComponent();                                   
            SortInit();
            GridColumnInit();
            cdvFromDate.Value = DateTime.Now;
            cdvToDate.Value = DateTime.Now;
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;            
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

            if (txtTime1.Text.Length != 2 || txtTime2.Text.Length !=2)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD061", GlobalVariable.gcLanguage));
                return false;
            }

            if (Convert.ToInt32(txtTime1.Text) > 59)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD062", GlobalVariable.gcLanguage));
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
                if (rdbLot.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Statement", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Vendor code", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RECV_SITE", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SEND_AREA", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RECV_AREA", 0, 4, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Production code", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LOT_NO", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LOT_TYPE", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SEND_WF_QTY", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                    spdData.RPT_AddBasicColumn("SEND_CHIP_QTY", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("RECV_WF_QTY", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                    spdData.RPT_AddBasicColumn("RECV_CHIP_QTY", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                    spdData.RPT_AddBasicColumn("WAFER_SIZE", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("NETDIE", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PACKAGE", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("BGD", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("BURN_IN", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TEMP", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SPECIAL_MK", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PACKING", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("OTHERS", 0, 21, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("FAB_SITE", 0, 22, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EDS_SITE", 0, 23, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ASY_SITE", 0, 24, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TST_SITE", 0, 25, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ETC", 0, 26, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MO_NO", 0, 27, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("WO_NO", 0, 28, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("YYMM", 0, 29, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PRODUCT_TYPE", 0, 30, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                    spdData.RPT_AddBasicColumn("SLIP_TYPE", 0, 31, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LOT_STATUS", 0, 32, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("SHIP_DATE", 0, 33, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SLIP_DATE", 0, 34, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SEND_DATE", 0, 35, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RECV_DATE", 0, 36, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ER_DEPT", 0, 37, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ER_USER", 0, 38, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ER_CODE", 0, 39, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ER_DEPT2", 0, 40, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ER_PRIORITY", 0, 41, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PKG_CODE", 0, 42, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("INKLESS", 0, 43, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("PACK_ROOM", 0, 44, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("HALF_PRODUCT", 0, 45, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                    spdData.RPT_AddBasicColumn("HALF_QTY", 0, 46, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LOCATION", 0, 47, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);

                    spdData.RPT_ColumnConfigFromTable(btnSort);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("SLIP_NO", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SEND_SITE", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RECV_SITE", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SEND_AREA", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RECV_AREA", 0, 4, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SLIP_TYPE", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SEND_EMP", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SEND_NAME", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SEND_SECT", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RECV_EMP", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RECV_NAME", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RECV_SECT", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("YYMM", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("CREATE_DATE", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SEND_DATE", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RECV_DATE", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SLIP_STATUS", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("REMARK", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("BL_NO", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ENTRY_DATE", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("RET_NAME", 0, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RET_FLAG", 0, 21, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);

                    spdData.RPT_ColumnConfigFromTable(btnSort);
                }
                
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
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "A.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MIN(FAC_SEQ)", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "A.MAT_GRP_2", "A.MAT_GRP_2 AS FAMILY", "MIN(FAB_SEQ) AS FAMILY", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.MAT_GRP_3", "A.MAT_GRP_3 AS PACKAGE", false);            
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "A.MAT_GRP_4", "A.MAT_GRP_4 AS TYPE1", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "A.MAT_GRP_5", "A.MAT_GRP_5 AS TYPE2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "A.MAT_GRP_6", "A.MAT_GRP_6 AS \"LD COUNT\"", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "A.MAT_GRP_7", "A.MAT_GRP_7 AS DENSITY", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "A.MAT_GRP_8", "A.MAT_GRP_8 AS GENERATION", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "A.MAT_CMF_10", "A.MAT_CMF_10 AS PIN_TYPE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID AS PRODUCT", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "A.MAT_CMF_7", "A.MAT_CMF_7 AS CUST_DEVICE", false);
        }
        #endregion


        #region SQL 쿼리 Build
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            string strStartTime = null;
            string strEndTime = null;

            strStartTime = cdvFromDate.Value.ToString("yyyyMMdd") + cboTime1.Text.Substring(0, 2) + txtTime1.Text + "00";
            strEndTime = cdvToDate.Value.ToString("yyyyMMdd") + cboTime2.Text.Substring(0, 2) + txtTime2.Text + "00";

            StringBuilder strSqlString = new StringBuilder();

            if (rdbLot.Checked == true)
            {
                strSqlString.Append("SELECT V3 AS 전표" + "\n");
                strSqlString.Append("     , 'HM' AS 업체코드" + "\n");
                strSqlString.Append("     , 'OY' AS RECV_SITE" + "\n");
                strSqlString.Append("     , 'HMK3' AS SEND_AREA" + "\n");
                strSqlString.Append("     , V19 AS RECV_AREA" + "\n");
                strSqlString.Append("     , V4 AS 생산코드" + "\n");
                strSqlString.Append("     , V7 AS LOT_NO" + "\n");
                strSqlString.Append("     , V8 AS LOT_TYPE" + "\n");
                strSqlString.Append("     , 0 AS SEND_WF_QTY" + "\n");
                strSqlString.Append("     , V11 AS SEND_CHIP_QTY" + "\n");
                strSqlString.Append("     , 0 AS RECV_WF_QTY" + "\n");
                strSqlString.Append("     , V11 AS RECV_CHIP_QTY" + "\n");
                strSqlString.Append("     , 0 AS WAFER_SIZE" + "\n");
                strSqlString.Append("     , 0 AS NETDIE" + "\n");
                strSqlString.Append("     , '**' AS PACKAGE" + "\n");
                strSqlString.Append("     , ' ' AS BGD" + "\n");
                strSqlString.Append("     , ' ' AS BURN_IN" + "\n");
                strSqlString.Append("     , ' ' AS TEMP" + "\n");
                strSqlString.Append("     , ' ' AS SPECIAL_MK" + "\n");
                strSqlString.Append("     , ' ' AS PACKING" + "\n");
                strSqlString.Append("     , ' ' AS CUSTOMER" + "\n");
                strSqlString.Append("     , ' ' AS OTHERS" + "\n");
                strSqlString.Append("     , ' ' AS FAB_SITE" + "\n");
                strSqlString.Append("     , ' ' AS EDS_SITE" + "\n");
                strSqlString.Append("     , 'HM' AS ASY_SITE" + "\n");
                strSqlString.Append("     , ' ' AS TST_SITE" + "\n");
                strSqlString.Append("     , ' ' AS ETC" + "\n");
                strSqlString.Append("     , ' ' AS MO_NO" + "\n");
                strSqlString.Append("     , ' ' AS WO_NO" + "\n");
                strSqlString.Append("     , '201009' AS YYMM" + "\n");
                strSqlString.Append("     , 'ME' AS PRODUCT_TYPE" + "\n");
                strSqlString.Append("     , 'NORMAL' AS SLIP_TYPE" + "\n");
                strSqlString.Append("     , 'READY' AS LOT_STATUS" + "\n");
                strSqlString.Append("     , V21 AS SHIP_DATE" + "\n");
                strSqlString.Append("     , V21 AS SLIP_DATE" + "\n");
                strSqlString.Append("     , V21 AS SEND_DATE" + "\n");
                strSqlString.Append("     , V21 AS RECV_DATE" + "\n");
                strSqlString.Append("     , ' ' AS ER_DEPT" + "\n");
                strSqlString.Append("     , ' ' AS ER_USER" + "\n");
                strSqlString.Append("     , ' ' AS ER_CODE" + "\n");
                strSqlString.Append("     , ' ' AS ER_DEPT2" + "\n");
                strSqlString.Append("     , ' ' AS ER_PRIORITY" + "\n");
                strSqlString.Append("     , ' ' AS PKG_CODE" + "\n");
                strSqlString.Append("     , V31 AS INKLESS" + "\n");
                strSqlString.Append("     , ' ' AS PACK_ROOM" + "\n");
                strSqlString.Append("     , ' ' AS HALF_PRODUCT" + "\n");
                strSqlString.Append("     , ' ' AS HALF_QTY" + "\n");
                strSqlString.Append("     , ' ' AS LOCATION" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT 'LS1'||'HM' V1" + "\n");
                strSqlString.Append("             , SUBSTRB(MESMGR.F_GET_STD_TIME@RPTTOMES('20100901000000', 'SE'), 1, 8) V2" + "\n");
                strSqlString.Append("             , DECODE(SUBSTR(A.MAT_ID,1,3),'SEK',NVL(TRIM(B.RESV_5), '-'),'SES',NVL(TRIM(B.INVOICE_NO), '-')) V3" + "\n");
                strSqlString.Append("             , NVL(TRIM(MAT.MAT_CMF_7), '-') V4" + "\n");
                strSqlString.Append("             , NVL(TRIM(MAT.MAT_CMF_8), '-') V5" + "\n");
                strSqlString.Append("             , NVL(TRIM(A.LOT_CMF_4), '-') V6" + "\n");
                strSqlString.Append("             , NVL(TRIM(A.LOT_CMF_1), '-') V7" + "\n");
                strSqlString.Append("             , DECODE(SUBSTR(A.MAT_ID, 1, 3), 'SES', DECODE(SUBSTR(A.LOT_CMF_5, 1, 1),'E',NVL(A.LOT_CMF_5,'-'), NVL(SUBSTR(A.LOT_CMF_5, 1, 1), '-')), NVL(TRIM(A.LOT_CMF_5), '-')) V8" + "\n");
                strSqlString.Append("             , MESMGR.F_GET_SEC_RETCODE@RPTTOMES(A.LOT_CMF_4, '" + GlobalVariable.gsAssyDefaultFactory + "', A.LOT_CMF_3) V9" + "\n");
                strSqlString.Append("             , MESMGR.F_GET_WAFER_QTY@RPTTOMES(A.LOT_ID) V10" + "\n");
                strSqlString.Append("             , NVL(TRIM(A.QTY_1), 0) V11" + "\n");
                strSqlString.Append("             , MESMGR.F_GET_LOSS_QTY_FACTORY_TOTAL@RPTTOMES(A.LOT_ID, '" + GlobalVariable.gsAssyDefaultFactory + "') V12" + "\n");
                strSqlString.Append("             , ' ' V13" + "\n");
                strSqlString.Append("             , A.OLD_FLOW V14" + "\n");
                strSqlString.Append("             , ' ' V15" + "\n");
                strSqlString.Append("             , ' ' V16" + "\n");
                strSqlString.Append("             , ' ' V17" + "\n");
                strSqlString.Append("             , 'HMK3' V18" + "\n");
                strSqlString.Append("             , DECODE(SUBSTR(A.MAT_ID, 1, 3), 'SEK', 'YUTS', 'SES',DECODE(SUBSTR(A.LOT_CMF_5,1,1),'E','KUTS','P','YUTS','YUTS'),TRIM(NVL(C.INV_DATA3, 'YUTS'))) V19" + "\n");
                strSqlString.Append("             , ' ' V20" + "\n");
                strSqlString.Append("             , TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') V21" + "\n");
                strSqlString.Append("             , ' ' V22" + "\n");
                strSqlString.Append("             , ' ' V23" + "\n");
                strSqlString.Append("             , ' ' V24" + "\n");
                strSqlString.Append("             , A.LOT_CMF_7 V25" + "\n");
                strSqlString.Append("             , 'S' V26" + "\n");
                strSqlString.Append("             , ' ' V27" + "\n");
                strSqlString.Append("             , DECODE(A.OLD_FACTORY, '" + GlobalVariable.gsAssyDefaultFactory + "', 'ASSY', '" + GlobalVariable.gsTestDefaultFactory + "', 'TEST', 'HMKE1', 'EDS', '-') V28" + "\n");
                strSqlString.Append("             , ' ' V29" + "\n");
                strSqlString.Append("             , ' ' V30" + "\n");
                strSqlString.Append("             , DECODE(SUBSTR(A.MAT_ID, 1, 3), 'SEK', MESMGR.PK_SEC_MES_IF.MakeInkless_M@RPTTOMES(A.OLD_FACTORY, TRIM(A.LOT_ID)), MESMGR.PK_SEC_MES_IF.MakeInkless@RPTTOMES(A.OLD_FACTORY, TRIM(A.LOT_ID))) V31" + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A ," + "\n");
                strSqlString.Append("               CWIPSHPLOT B ," + "\n");
                strSqlString.Append("               CWIPSHPINF C ," + "\n");
                strSqlString.Append("               MWIPMATDEF MAT" + "\n");
                strSqlString.Append("         WHERE A.TRAN_CODE = 'SHIP'" + "\n");
                strSqlString.Append("           AND A.FACTORY = 'CUSTOMER'" + "\n");
                strSqlString.Append("           AND A.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD'" + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("           AND A.HIST_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("           AND A.LOT_CMF_2 = 'SE'" + "\n");
                strSqlString.Append("           AND B.TYPE = ' '" + "\n");
                strSqlString.Append("           AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND C.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND B.LOT_ID = A.LOT_ID" + "\n");
                strSqlString.Append("           AND B.INVOICE_NO = C.INVOICE_NO" + "\n");
                strSqlString.Append("           AND MAT.FACTORY = A.OLD_FACTORY" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = A.MAT_ID" + "\n");
                strSqlString.Append("           AND MAT.MAT_VER = A.MAT_VER" + "\n");
                strSqlString.Append("           AND C.SHP_FAC = 'CUSTOMER'" + "\n");
                strSqlString.Append("           AND C.CUSTOMER = 'SE'" + "\n");
                strSqlString.Append("           AND A.MAT_ID LIKE 'SEK%'" + "\n");
                strSqlString.Append("           AND A.TRAN_TIME BETWEEN '" + strStartTime + "' AND '" + strEndTime + "'" + "\n");
                strSqlString.Append("         ORDER BY A.LOT_CMF_1 ASC" + "\n");
                strSqlString.Append("       )" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT V3 AS SLIP_NO" + "\n");
                strSqlString.Append("     , 'HM' AS SEND_SITE" + "\n");
                strSqlString.Append("     , 'OY' AS RECV_SITE" + "\n");
                strSqlString.Append("     , 'HMK3' AS SEND_AREA" + "\n");
                strSqlString.Append("     , 'YUTS' AS RECV_AREA" + "\n");
                strSqlString.Append("     , 'NORMAL' AS SLIP_TYPE" + "\n");
                strSqlString.Append("     , 'GOM' AS SEND_EMP" + "\n");
                strSqlString.Append("     , ' ' AS SEND_NAME" + "\n");
                strSqlString.Append("     , ' ' AS SEND_SECT" + "\n");
                strSqlString.Append("     , ' ' AS RECV_EMP" + "\n");
                strSqlString.Append("     , ' ' AS RECV_NAME" + "\n");
                strSqlString.Append("     , ' ' AS RECV_SECT" + "\n");
                strSqlString.Append("     , '201009' AS YYMM" + "\n");
                strSqlString.Append("     , V21 AS CREATE_DATE" + "\n");
                strSqlString.Append("     , V21 AS SEND_DATE" + "\n");
                strSqlString.Append("     , V21 AS RECV_DATE" + "\n");
                strSqlString.Append("     , 'READY' AS SLIP_STATUS" + "\n");
                strSqlString.Append("     , ' ' AS REMARK" + "\n");
                strSqlString.Append("     , ' ' AS BL_NO" + "\n");
                strSqlString.Append("     , ' ' AS ENTRY_DATE" + "\n");
                strSqlString.Append("     , ' ' AS RET_NAME" + "\n");
                strSqlString.Append("     , ' ' AS RET_FLAG" + "\n");               
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("       SELECT DISTINCT SUBSTRB(MESMGR.F_GET_STD_TIME@RPTTOMES('20100901000000', 'SE'), 1, 8) V2" + "\n");
                strSqlString.Append("            , DECODE(SUBSTR(A.MAT_ID,1,3),'SEK',NVL(TRIM(B.RESV_5), '-'),'SES',NVL(TRIM(B.INVOICE_NO), '-')) V3" + "\n");
                strSqlString.Append("            , DECODE(SUBSTR(A.MAT_ID,1,3),'SES',DECODE(SUBSTR(A.LOT_CMF_5,1,1),'E',NVL(A.LOT_CMF_5,'-'), NVL(SUBSTR(A.LOT_CMF_5, 1, 1), '-')), NVL(TRIM(A.LOT_CMF_5), '-')) V8" + "\n");
                strSqlString.Append("            , MESMGR.F_GET_SEC_RETCODE@RPTTOMES(A.LOT_CMF_4, '" + GlobalVariable.gsAssyDefaultFactory + "', A.LOT_CMF_3) V9" + "\n");                
                strSqlString.Append("            , ' ' V13" + "\n");
                strSqlString.Append("            , ' ' V15" + "\n");
                strSqlString.Append("            , ' ' V16" + "\n");
                strSqlString.Append("            , ' ' V17" + "\n");
                strSqlString.Append("            , 'HMK3' V18" + "\n");
                strSqlString.Append("            , DECODE(SUBSTR(A.MAT_ID, 1, 3), 'SEK', 'YUTS', 'SES',DECODE(SUBSTR(A.LOT_CMF_5,1,1),'E','KUTS','P','YUTS','YUTS'),TRIM(NVL(C.INV_DATA3, 'YUTS'))) V19" + "\n");
                strSqlString.Append("            , ' ' V20" + "\n");
                strSqlString.Append("            , TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') V21" + "\n");
                strSqlString.Append("            , ' ' V22" + "\n");
                strSqlString.Append("            , ' ' V23" + "\n");
                strSqlString.Append("            , ' ' V24" + "\n");
                strSqlString.Append("            , A.LOT_CMF_7 V25" + "\n");
                strSqlString.Append("            , 'S' V26" + "\n");
                strSqlString.Append("            , ' ' V27" + "\n");
                strSqlString.Append("            , DECODE(A.OLD_FACTORY, '" + GlobalVariable.gsAssyDefaultFactory + "', 'ASSY', '" + GlobalVariable.gsTestDefaultFactory + "', 'TEST', 'HMKE1', 'EDS', '-') V28" + "\n");
                strSqlString.Append("            , ' ' V29" + "\n");
                strSqlString.Append("            , ' ' V30" + "\n");
                strSqlString.Append("         FROM RWIPLOTHIS A" + "\n");
                strSqlString.Append("            , CWIPSHPLOT B" + "\n");
                strSqlString.Append("            , CWIPSHPINF C" + "\n");
                strSqlString.Append("            , MWIPMATDEF MAT" + "\n");
                strSqlString.Append("        WHERE A.TRAN_CODE = 'SHIP'" + "\n");
                strSqlString.Append("          AND A.FACTORY = 'CUSTOMER'" + "\n");
                strSqlString.Append("          AND A.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("          AND A.OWNER_CODE = 'PROD'" + "\n");
                strSqlString.Append("          AND A.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("          AND A.HIST_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("          AND A.LOT_CMF_2 = 'SE'" + "\n");
                strSqlString.Append("          AND B.TYPE = ' '" + "\n");
                strSqlString.Append("          AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("          AND C.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("          AND B.LOT_ID = A.LOT_ID" + "\n");
                strSqlString.Append("          AND B.INVOICE_NO = C.INVOICE_NO" + "\n");
                strSqlString.Append("          AND MAT.FACTORY = A.OLD_FACTORY" + "\n");
                strSqlString.Append("          AND MAT.MAT_ID = A.MAT_ID" + "\n");
                strSqlString.Append("          AND MAT.MAT_VER = A.MAT_VER" + "\n");
                strSqlString.Append("          AND C.SHP_FAC = 'CUSTOMER'" + "\n");
                strSqlString.Append("          AND C.CUSTOMER = 'SE'" + "\n");
                strSqlString.Append("          AND A.MAT_ID LIKE 'SEK%'" + "\n");
                strSqlString.Append("          AND A.TRAN_TIME BETWEEN '" + strStartTime + "' AND '" + strEndTime + "'" + "\n");
                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" ORDER BY V3" + "\n");                
            }

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

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 11, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
                //spdData.Sheets[0].FrozenColumnCount = 3;
                //spdData.RPT_AutoFit(false);

                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.DataSource = dt;

                spdData.RPT_AutoFit(false);

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
                if (rdbLot.Checked == true)
                {
                    spdData.ExportExcel("HM_LOT_AO");
                }
                else
                {
                    spdData.ExportExcel("HM_SLIP_AO");
                }
            }
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {

        }

        #endregion
    }       
}
