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
    public partial class MAT070301 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: MAT070301<br/>
        /// 클래스요약: 원부자재 결산<br/>
        /// 작  성  자: 하나마이크론 정병주<br/>
        /// 최초작성일: 2010-04-01<br/>
        /// 상세  설명: 자재 결산<br/>
        /// 변경  내용: <br/>
        #region " MAT070301 : Program Initial "

        public MAT070301()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
            cdvFromToDate.AutoBinding();

            this.cdvMatType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.udcMat_Code.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            
            string strquery = string.Empty;
            strquery = "SELECT KEY_1 ,DATA_1  FROM MGCMTBLDAT WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME='MATERIAL_TYPE' AND DATA_2='Y'";
            cdvMatType.sDynamicQuery = strquery;
        }

        #endregion
        

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            int iIdx;
            try
            {
                spdData.RPT_ColumnInit();
                iIdx = 0;
                spdData.RPT_AddBasicColumn(" ", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                iIdx += 1;
                spdData.RPT_AddBasicColumn("MAT_ID", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                // BOH
                iIdx += 1;
                spdData.RPT_AddBasicColumn("BOH", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 40);
                // IN
                iIdx += 1;
                spdData.RPT_AddBasicColumn("IN", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 40);
                // EOH
                iIdx += 1;
                spdData.RPT_AddBasicColumn("EOH", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 40);
                // USED
                iIdx += 1;
                spdData.RPT_AddBasicColumn("USED", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 40);
                // USED
                iIdx += 1;
                spdData.RPT_AddBasicColumn("BONUS", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 40);
                // LOSS
                iIdx += 1;
                spdData.RPT_AddBasicColumn("LOSS", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 40);
                // LOSS
                iIdx += 1;
                spdData.RPT_AddBasicColumn("ADAPT", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 40);
                iIdx += 1;

                spdData.RPT_AddBasicColumn("WIP cleanup", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 40);
                //RECYCLE
                iIdx += 1;
                spdData.RPT_AddBasicColumn("HIST_DEL", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 40);

                // USED
                iIdx += 1;
                spdData.RPT_AddBasicColumn("DIFF", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 40);

                iIdx += 1;
                spdData.RPT_AddBasicColumn("OPER_OUT", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 40);

                // Group항목이 있을경우 반드시 선언해줄것.
                //spdData.RPT_ColumnConfigFromTable(btnSort);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion


        #region " SortInit : Group By 설정 "

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            try
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "B.MAT_CMF_10", "PIN_TYPE", "B.MAT_CMF_10", true);
                // FAMILY
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "MAT_GRP_2", "B.MAT_GRP_2", true);
                }
                // PACKAGE
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "MAT_GRP_3", "B.MAT_GRP_3", true);
                }
                // TYPE1
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "MAT_GRP_4", "B.MAT_GRP_4", true);
                }
                // TYPE2
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "MAT_GRP_5", "B.MAT_GRP_5", true);
                }
                // LEAD COUNT
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LEAD_COUNT", "B.MAT_GRP_6", "MAT_GRP_6", "B.MAT_GRP_6", true);
                }
                // DENSITY
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "MAT_GRP_7", "B.MAT_GRP_7", true);
                }
                // GENERATION
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "MAT_GRP_8", "B.MAT_GRP_8", true);
                }
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "MAT_ID", "A.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RETURN_TYPE", "A.RETURN_TYPE", "RETURN_TYPE", "A.RETURN_TYPE", true);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion

        

        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            /************************************************/
            /* COMMENT BY  : MES에서 사용할 결산용 QUERY문	*/
            /* 												*/
            /* CREATED BY  : 정병주  (2010-04-01)		*/
            /* 												*/
            /* MODIFIED BY : 정병주  (2010-04-01)		*/
            /************************************************/
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null, QueryCond2 = null, QueryCond3 = null;
            string strFromDate = null, strToDate = null, strFromCutOffDate = null, strToCutOffDate = null;
            string By_Mat_type = "";
            string By_Mat_id = "", matcode = "";
            string By_Qty = "";

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            DateTime dtDate;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            // 2010 - 03 - 31 정병주 : 자재별로 검색하는 조건이 틀리기 때문에 TYPE을 변환시킴.
            if (cdvMatType.Text == "AE") {By_Mat_type = "A"; matcode = "R18%";}
            if (cdvMatType.Text == "LF") {By_Mat_type = "L"; matcode = "R14%";}
            if (cdvMatType.Text == "PB") {By_Mat_type = "P"; matcode = "R16%";}
            if (cdvMatType.Text == "GW") {By_Mat_type = "G"; matcode = "R19%";}
            if (cdvMatType.Text == "MC") {By_Mat_type = "E"; matcode = "R15%";}
            if (cdvMatType.Text == "BD") {By_Mat_type = "B"; matcode = "R212%";}
            if (cdvMatType.Text == "SB") {By_Mat_type = "S"; matcode = "R17%";}
            if (cdvMatType.Text == "TE") { By_Mat_type = "Z"; matcode = "R211%";}

            if (ChkLot.Checked == true) By_Qty = "COUNT";
            else By_Qty = "SUM";

            if (udcMat_Code.Text.Trim() != "") matcode = udcMat_Code.Text.Trim();

                switch (cdvFromToDate.DaySelector.SelectedValue.ToString())
                {
                    // 2010 - 04 - 01 정병주 : 기간을 정할때 일별로만 할 수 있다.
                    //                         월 또는 주단위로 하면 속도가 매우 느림 ;
                    case "DAY":
                        if (rbTime01.Checked == true)
                        {
                            dtDate = DateTime.Parse(cdvFromToDate.FromDate.Text);
                            dtDate = dtDate.AddDays(-1);
                            strFromDate = dtDate.ToString("yyyyMMdd") + "220001";
                            strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "220000";
                        }
                        else if (rbTime02.Checked == true)
                        {
                            strFromDate = cdvFromToDate.FromDate.Text.Replace("-", "") + "060001";
                            strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "060000";
                        }
                        else if (rbTime03.Checked == true)
                        {
                            strFromDate = cdvFromToDate.FromDate.Text.Replace("-", "") + "140001";
                            strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "140000";
                        }
                        strFromCutOffDate = strFromDate.Substring(0, 10);
                        strToCutOffDate = strToDate.Substring(0, 10);
                        break;

                    default:
                        MessageBox.Show("결산기간은 일(Day) 단위로만 할 수 있습니다!" + "\r\n\r\n"
                                      + "일(Day) 단위를 선택하여 주십시오!", this.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return "";
                }

                    strSqlString.Append("     SELECT MAT_INFO.*," + "\n");
                    strSqlString.Append("            DECODE(LOT_INFO.PRD_OUT, '', 0, LOT_INFO.PRD_OUT) AS PRD_OUT" + "\n"); 
                    strSqlString.Append("         FROM   (" + "\n");
                    strSqlString.AppendFormat("   SELECT '{0}' AS MAT_TYPE, " + "\n", cdvMatType.Text.Trim());
                    strSqlString.Append("                    MAT_ID," + "\n");
                    strSqlString.Append("                    BOH," + "\n");
                    strSqlString.Append("                    INASSY," + "\n");
                    strSqlString.Append("                    EOH," + "\n");
                    strSqlString.Append("                    USEDASSY," + "\n");
                    strSqlString.Append("                    BNS_QTY," + "\n");
                    strSqlString.Append("                    LOSS," + "\n");
                    strSqlString.Append("                    ADAPT," + "\n");
                    strSqlString.Append("                    TIDY," + "\n");
                    strSqlString.Append("                    RECYCLE," + "\n");
                    strSqlString.Append("                            CASE" + "\n");
                    strSqlString.Append("                              WHEN DIFF > 0 THEN (BOH + INASSY - (USEDASSY+ EOH + ADAPT+ TIDY) + (BNS_QTY - LOSS) - RECYCLE)" + "\n");
                    strSqlString.Append("                              WHEN DIFF < 0 THEN (BOH + INASSY - (USEDASSY+ EOH + ADAPT+ TIDY) + (BNS_QTY - LOSS) + RECYCLE)" + "\n");
                    strSqlString.Append("                              ELSE 0" + "\n");
                    strSqlString.Append("                            END AS DIFF" + "\n");
                    strSqlString.Append("             FROM   (SELECT 'PB' AS MAT_TYPE," + "\n");
                    strSqlString.Append("                            MAT_ID ," + "\n");
                    strSqlString.Append("                            SUM(BOH) AS BOH," + "\n");
                    strSqlString.Append("                            SUM(INASSY) AS INASSY," + "\n");
                    strSqlString.Append("                            SUM(EOH) AS EOH," + "\n");
                    strSqlString.Append("                            SUM(TERMQTY) AS USEDASSY," + "\n");
                    strSqlString.Append("                            SUM(LOSS) AS LOSS," + "\n");
                    strSqlString.Append("                            SUM(ADAPT) AS ADAPT," + "\n");
                    strSqlString.Append("                            SUM(TIDY) AS TIDY," + "\n");
                    strSqlString.Append("                            SUM(RECYCLE) AS RECYCLE," + "\n");
                    strSqlString.Append("                            SUM(BNS_QTY) AS BNS_QTY," + "\n");
                    strSqlString.Append("                            SUM(BOH + INASSY - (TERMQTY+ EOH+ LOSS)) AS DIFF" + "\n");
                    strSqlString.Append("                     FROM   (SELECT MAT_ID AS MAT_ID," + "\n");
                    strSqlString.Append("                                    " + By_Qty + "(QTY_1) AS BOH," + "\n");
                    strSqlString.Append("                                    0 AS INASSY ," + "\n");
                    strSqlString.Append("                                    0 AS EOH," + "\n");
                    strSqlString.Append("                                    0 AS LOSS," + "\n");
                    strSqlString.Append("                                    0 AS RECYCLE," + "\n");
                    strSqlString.Append("                                    0 AS TERMQTY," + "\n");
                    strSqlString.Append("                                    0 AS BNS_QTY," + "\n");
                    strSqlString.Append("                                    0 AS ADAPT," + "\n");
                    strSqlString.Append("                                    0 AS TIDY" + "\n");
                    strSqlString.Append("                             FROM   RWIPLOTSTS_BOH" + "\n");
                    strSqlString.Append("                             WHERE  FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                             AND    OPER > 'V0000'" + "\n");
                    strSqlString.AppendFormat("         AND    LOT_TYPE = '{0}' " + "\n", By_Mat_type);
                    strSqlString.AppendFormat("         AND    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.AppendFormat("         AND    CUTOFF_DT = '{0}' " + "\n", strFromCutOffDate);
                    if (cdvMatType.Text == "MC" && ChkEmc.Checked == false) strSqlString.Append("               AND    LOT_CMF_18 = ' '" + "\n");
                    strSqlString.Append("                             AND    QTY_1 > 0" + "\n");
                    strSqlString.Append("                             GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                             UNION" + "\n");
                    strSqlString.Append("     SELECT MAT_ID AS MAT_ID ," + "\n");
                    strSqlString.Append("                                    0 AS BOH ," + "\n");
                    strSqlString.Append("                                    " + By_Qty + "(QTY_1) AS INASSY," + "\n");
                    strSqlString.Append("                                    0 AS EOH," + "\n");
                    strSqlString.Append("                                    0 AS LOSS," + "\n");
                    strSqlString.Append("                                    0 AS RECYCLE," + "\n");
                    strSqlString.Append("                                    0 AS TERMQTY," + "\n");
                    strSqlString.Append("                                    0 AS BNS_QTY," + "\n");
                    strSqlString.Append("                                    0 AS ADAPT," + "\n");
                    strSqlString.Append("                                    0 AS TIDY" + "\n");
                    strSqlString.Append("                             FROM   RWIPLOTHIS" + "\n");
                    strSqlString.Append("                             WHERE  FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                             AND    TRAN_CODE = 'CREATE'" + "\n");
                    strSqlString.AppendFormat("                       AND    LOT_TYPE = '{0}' " + "\n", By_Mat_type);
                    strSqlString.Append("                             AND    OPER >= 'V0000'" + "\n");
                    strSqlString.AppendFormat("                       AND    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.AppendFormat("                       AND    TRAN_TIME BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.Append("                             GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                             UNION" + "\n");
                    strSqlString.Append("     SELECT MAT_ID AS MAT_ID ," + "\n");
                    strSqlString.Append("                                    0 AS BOH ," + "\n");
                    strSqlString.Append("                                    0 AS INASSY ," + "\n");
                    strSqlString.Append("                                    " + By_Qty + "(QTY_1) AS EOH," + "\n");
                    strSqlString.Append("                                    0 AS LOSS," + "\n");
                    strSqlString.Append("                                    0 AS RECYCLE," + "\n");
                    strSqlString.Append("                                    0 AS TERMQTY," + "\n");
                    strSqlString.Append("                                    0 AS BNS_QTY," + "\n");
                    strSqlString.Append("                                    0 AS ADAPT," + "\n");
                    strSqlString.Append("                                    0 AS TIDY" + "\n");
                    strSqlString.Append("                             FROM   RWIPLOTSTS_BOH" + "\n");
                    strSqlString.Append("                             WHERE  FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                             AND    OPER > 'V0000'" + "\n");
                    strSqlString.AppendFormat("                       AND    LOT_TYPE = '{0}' " + "\n", By_Mat_type);
                    strSqlString.AppendFormat("                       AND    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.AppendFormat("                       AND    CUTOFF_DT = '{0}' " + "\n", strToCutOffDate);
                    if (cdvMatType.Text == "MC" && ChkEmc.Checked == false) strSqlString.Append("               AND    LOT_CMF_18 = ' '" + "\n");
                    strSqlString.Append("                             AND    QTY_1 > 0" + "\n");
                    strSqlString.Append("                             GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                             UNION" + "\n");
                    strSqlString.Append("     SELECT MAT_ID AS MAT_ID ," + "\n");
                    strSqlString.Append("                                    0 AS BOH ," + "\n");
                    strSqlString.Append("                                    0 AS INASSY," + "\n");
                    strSqlString.Append("                                    0 AS EOH," + "\n");
                    strSqlString.Append("                                    " + By_Qty + "(TOTAL_LOSS_QTY) AS LOSS," + "\n");
                    strSqlString.Append("                                    0 AS RECYCLE," + "\n");
                    strSqlString.Append("                                    0 AS TERMQTY," + "\n");
                    strSqlString.Append("                                    0 AS BNS_QTY," + "\n");
                    strSqlString.Append("                                    0 AS ADAPT," + "\n");
                    strSqlString.Append("                                    0 AS TIDY" + "\n");
                    strSqlString.Append("                             FROM   RWIPLOTLOS" + "\n");
                    strSqlString.Append("                             WHERE  FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.AppendFormat("                       AND    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.Append("                             AND    OPER > 'V0000'" + "\n");
                    strSqlString.AppendFormat("                       AND    TRAN_TIME BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.Append("                             GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                             UNION" + "\n");
                    strSqlString.Append("    SELECT MAT_ID AS MAT_ID ," + "\n");
                    strSqlString.Append("                       0 AS BOH ," + "\n");
                    strSqlString.Append("                       0 AS INASSY," + "\n");
                    strSqlString.Append("                       0 AS EOH," + "\n");
                    strSqlString.Append("                      0 AS LOSS," + "\n");
                    strSqlString.Append("                      " + By_Qty + "(QTY_1) AS RECYCLE," + "\n");
                    strSqlString.Append("                      0 AS TERMQTY," + "\n");
                    strSqlString.Append("                      0 AS BNS_QTY," + "\n");
                    strSqlString.Append("                      0 AS ADAPT," + "\n");
                    strSqlString.Append("                      0 AS TIDY" + "\n");
                    strSqlString.Append("               FROM   (SELECT MAT_ID," + "\n");
                    strSqlString.Append("                              " + By_Qty + "(QTY_1) AS QTY_1" + "\n");
                    strSqlString.Append("                       FROM   RWIPLOTHIS" + "\n");
                    strSqlString.Append("                       WHERE  FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                       AND    OPER > 'V0000'" + "\n");
                    strSqlString.AppendFormat("                       AND    LOT_TYPE = '{0}' " + "\n", By_Mat_type);
                    strSqlString.AppendFormat("                       AND    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.AppendFormat("                       AND    HIST_DEL_TIME BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.Append("                       AND    HIST_DEL_FLAG = 'Y'" + "\n");
                    strSqlString.Append("                       AND    TRAN_CODE IN( 'TERMINATE'," + "\n");
                    strSqlString.Append("                                      'LOSS')" + "\n");
                    strSqlString.Append("                       AND    LOT_ID IN(SELECT DISTINCT LOT_ID" + "\n");
                    strSqlString.Append("                              FROM   RWIPLOTSTS_BOH" + "\n");
                    strSqlString.Append("                              WHERE  FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                              AND    OPER > 'V0000'" + "\n");
                    strSqlString.AppendFormat("                        AND    LOT_TYPE = '{0}' " + "\n", By_Mat_type);
                    strSqlString.AppendFormat("                        AND    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.AppendFormat("                        AND    CUTOFF_DT IN( '{0}','{1}'))" + "\n", strFromCutOffDate, strToCutOffDate);
                    strSqlString.Append("                      GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                      UNION" + "\n");
                    strSqlString.Append("           SELECT MAT_ID," + "\n");
                    strSqlString.Append("                            " + By_Qty + "(QTY_1) AS QTY_1" + "\n");
                    strSqlString.Append("                     FROM   MWIPLOTDEL@RPTTOMES" + "\n");
                    strSqlString.Append("                     WHERE  HIST_SEQ = 1" + "\n");
                    strSqlString.AppendFormat("                     AND    HIST_DEL_TIME BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("                     AND    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.Append("                           AND    LOT_ID IN(SELECT DISTINCT LOT_ID" + "\n");
                    strSqlString.Append("                             FROM   RWIPLOTSTS_BOH" + "\n");
                    strSqlString.Append("                             WHERE  FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                             AND    OPER > 'V0000'" + "\n");
                    strSqlString.AppendFormat("                       AND    LOT_TYPE = '{0}' " + "\n", By_Mat_type);
                    strSqlString.AppendFormat("                       AND    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.AppendFormat("                       AND    CUTOFF_DT IN( '{0}','{1}'))" + "\n", strFromCutOffDate, strToCutOffDate);
                    strSqlString.Append("                     GROUP BY MAT_ID)" + "\n");
                    strSqlString.Append("             GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                             UNION" + "\n");
                    strSqlString.Append("     SELECT MAT_ID AS MAT_ID ," + "\n");
                    strSqlString.Append("                                    0 AS BOH ," + "\n");
                    strSqlString.Append("                                    0 AS INASSY," + "\n");
                    strSqlString.Append("                                    0 AS EOH," + "\n");
                    strSqlString.Append("                                    0 AS LOSS," + "\n");
                    strSqlString.Append("                                    0 AS RECYCLE," + "\n");
                    strSqlString.Append("                                    " + By_Qty + "(QTY_1) AS TERMQTY," + "\n");
                    strSqlString.Append("                                    0 AS BNS_QTY," + "\n");
                    strSqlString.Append("                                    0 AS ADAPT," + "\n");
                    strSqlString.Append("                                    0 AS TIDY" + "\n");
                    strSqlString.Append("                             FROM   (SELECT MAT_ID," + "\n");
                    strSqlString.Append("                                            " + By_Qty + "(QTY_1) AS QTY_1" + "\n");
                    strSqlString.Append("                                     FROM   RWIPLOTHIS" + "\n");
                    strSqlString.Append("                                     WHERE  FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.AppendFormat("                               AND    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.AppendFormat("                               AND    TRAN_TIME BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.Append("                                     AND    LOT_DEL_FLAG = 'Y'" + "\n");
                    strSqlString.Append("                                     AND    TRAN_CODE = 'TERMINATE'" + "\n");
                    strSqlString.Append("                                     AND    OPER > 'V0000'" + "\n");
                    strSqlString.Append("                                     AND    LOT_DEL_CODE != 'M06'" + "\n");
                    strSqlString.Append("                                     GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                                     UNION" + "\n");
                    strSqlString.Append("     SELECT MAT_ID," + "\n");
                    strSqlString.Append("                                            " + By_Qty + "(OLD_QTY_1 - QTY_1 ) AS QTY_1" + "\n");
                    strSqlString.Append("                                     FROM   RWIPLOTHIS" + "\n");
                    strSqlString.AppendFormat("                               WHERE    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.AppendFormat("                               AND    TRAN_TIME BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("                               AND   HIST_DEL_TIME NOT BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.Append("                                     AND    TRAN_CODE = 'ADAPT'" + "\n");
                    strSqlString.Append("                                     AND    TRAN_CMF_5 IN ( 'ADAPT-RES_DETTACH', 'ADAPT-LOT_USAGE_LOCATION' )" + "\n");
                    strSqlString.Append("                                     GROUP BY MAT_ID)" + "\n");
                    strSqlString.Append("                             GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                             UNION" + "\n");
                    strSqlString.Append("     SELECT MAT_ID AS MAT_ID ," + "\n");
                    strSqlString.Append("                                    0 AS BOH ," + "\n");
                    strSqlString.Append("                                    0 AS INASSY," + "\n");
                    strSqlString.Append("                                    0 AS EOH," + "\n");
                    strSqlString.Append("                                    0 AS LOSS," + "\n");
                    strSqlString.Append("                                    0 AS RECYCLE," + "\n");
                    strSqlString.Append("                                    0 AS TERMQTY," + "\n");
                    strSqlString.Append("                                    " + By_Qty + "(TOTAL_BONUS_QTY) AS BNS_QTY," + "\n");
                    strSqlString.Append("                                    0 AS ADAPT," + "\n");
                    strSqlString.Append("                                    0 AS TIDY" + "\n");
                    strSqlString.Append("                             FROM   RWIPLOTBNS" + "\n");
                    strSqlString.AppendFormat("                       WHERE  TRAN_TIME BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.Append("                             AND    FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.AppendFormat("                       AND    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.Append("                             AND    BONUS_CODE_1 = 'BN02'" + "\n");
                    strSqlString.Append("                             GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                             UNION" + "\n");
                    strSqlString.Append("                SELECT MAT_ID AS MAT_ID ," + "\n");
                    strSqlString.Append("           0 AS BOH ," + "\n");
                    strSqlString.Append("           0 AS INASSY," + "\n");
                    strSqlString.Append("           0 AS EOH," + "\n");
                    strSqlString.Append("           0 AS LOSS," + "\n");
                    strSqlString.Append("           0 AS RECYCLE," + "\n");
                    strSqlString.Append("           0 AS TERMQTY," + "\n");
                    strSqlString.Append("           0 AS BNS_QTY," + "\n");
                    strSqlString.Append("          " + By_Qty + "(OLD_QTY_1 - QTY_1 ) AS ADAPT," + "\n");
                    strSqlString.Append("           0 AS TIDY" + "\n");
                    strSqlString.Append("            FROM   RWIPLOTHIS" + "\n");
                    strSqlString.AppendFormat("           WHERE    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.AppendFormat("           AND    TRAN_TIME BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("           AND    HIST_DEL_TIME NOT BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.Append("                 AND    TRAN_CODE = 'ADAPT'" + "\n");
                    strSqlString.Append("                 AND    TRAN_CMF_5 = ' '" + "\n");
                    strSqlString.Append("            GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("            UNION " + "\n");
                    strSqlString.Append("           SELECT MAT_ID AS MAT_ID ," + "\n");
                    strSqlString.Append("                 0 AS BOH ," + "\n");
                    strSqlString.Append("                   0 AS INASSY," + "\n");
                    strSqlString.Append("                   0 AS EOH," + "\n");
                    strSqlString.Append("                   0 AS LOSS," + "\n");
                    strSqlString.Append("                   0 AS RECYCLE," + "\n");
                    strSqlString.Append("                   0 AS TERMQTY," + "\n");
                    strSqlString.Append("                   0 AS BNS_QTY," + "\n");
                    strSqlString.Append("                   0 AS ADAPT," + "\n");
                    strSqlString.Append("                  SUM(QTY_1) AS TIDY" + "\n");
                    strSqlString.Append("                   FROM   RWIPLOTHIS" + "\n");
                    strSqlString.Append("                  WHERE  FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.AppendFormat("                AND    MAT_ID LIKE '{0}' " + "\n", matcode);
                    strSqlString.AppendFormat("               AND    TRAN_TIME BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.Append("                  AND    LOT_DEL_FLAG = 'Y'" + "\n");
                    strSqlString.Append("                  AND    TRAN_CODE = 'TERMINATE'" + "\n");
                    strSqlString.Append("                  AND    LOT_DEL_CODE = 'M06'" + "\n");
                    strSqlString.Append("                AND    OPER > 'V0000'" + "\n");
                    strSqlString.Append("                  GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                       )" + "\n");
                    strSqlString.Append("                     GROUP BY MAT_ID) ) MAT_INFO," + "\n");
                    strSqlString.Append("            (SELECT MATBOH.MATCODE AS MAT_ID," + "\n");
                    strSqlString.Append("                    " + By_Qty + "(OUT_QTY) AS PRD_OUT" + "\n");
                    strSqlString.Append("             FROM   (SELECT MATCODE," + "\n");
                    strSqlString.Append("                            PARTNUMBER," + "\n");
                    strSqlString.Append("                            STEPID" + "\n");
                    strSqlString.Append("                     FROM   CWIPBOMDEF" + "\n");
                    strSqlString.AppendFormat("               WHERE    MATCODE LIKE '{0}' " + "\n", By_Mat_id);
                    strSqlString.Append("                     ) MATBOH," + "\n");
                    strSqlString.Append("                    (SELECT MAT_ID," + "\n");
                    strSqlString.Append("                            OLD_OPER," + "\n");
                    strSqlString.Append("                            " + By_Qty + "(CREATE_QTY_1) AS OUT_QTY" + "\n");
                    strSqlString.Append("                     FROM   RWIPLOTHIS" + "\n");
                    strSqlString.Append("                     WHERE  FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.AppendFormat("               AND    TRAN_TIME BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                    strSqlString.Append("                     AND    LOT_TYPE = 'W'" + "\n");
                    strSqlString.Append("                     AND    TRAN_CODE = 'END'" + "\n");
                    strSqlString.Append("                     AND    HIST_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("                     AND    LOT_CMF_5 LIKE 'P%'" + "\n");
                    strSqlString.Append("                     GROUP BY MAT_ID, OLD_OPER) PRD_OU_QTY" + "\n");
                    strSqlString.Append("             WHERE  1 = 1" + "\n");
                    strSqlString.Append("             AND    MATBOH.PARTNUMBER = PRD_OU_QTY.MAT_ID(+)" + "\n");
                    strSqlString.Append("             AND    PRD_OU_QTY.OLD_OPER = MATBOH.STEPID" + "\n");
                    strSqlString.Append("             GROUP BY MATBOH.MATCODE ) LOT_INFO" + "\n");
                    strSqlString.Append("     WHERE  1 = 1" + "\n");
                    strSqlString.Append("     AND    MAT_INFO.MAT_ID = LOT_INFO.MAT_ID(+)" + "\n");
                    strSqlString.Append("     ORDER BY MAT_INFO.MAT_ID " + "\n");
                
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region " MakeChart : Chart 처리 "

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>
        private void MakeChart(DataTable DT)
        {

        }

        #endregion


        #region " Event 처리 "


        #region " btnView_Click : View버튼을 선택했을 때 "

        private void btnView_Click(object sender, EventArgs e)
        {
            int Com_CNT = 0;
            string strSqlString = "";
            DataTable dt = null;
            int iSubTotalCount = 0;
            if (CheckField() == false) return;

            try
            {
                if (cdvMatType.Text == "")
                {
                    return;
                }
                // 검색중 화면 표시
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                SortInit();
                GridColumnInit();

                // Query문으로 데이터를 검색한다.
                strSqlString = MakeSqlString();
                if (strSqlString.Trim() == "")
                {
                    return;
                }
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString);

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }
                if (ckbPrd_View.Checked == true)
                {
                    iSubTotalCount += 1;
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 1, iSubTotalCount, 1, null, null);
                    spdData.RPT_AutoFit(false);
                    dt.Dispose();
                    Com_CNT = spdData.ActiveSheet.ColumnCount;
                    SetAvgVertical(1, Com_CNT, true);
                }
                else
                {
                    iSubTotalCount += 1;
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, iSubTotalCount, 1, null, null);
                    spdData.RPT_AutoFit(false);
                    dt.Dispose();
                }
                
                spdData_Sheet1.Columns[9].ForeColor = Color.Red;
                if (ChkLot.Checked == true)
                {
                    spdData_Sheet1.Columns[5].Visible = false;
                    spdData_Sheet1.Columns[6].Visible = false;
                    spdData_Sheet1.Columns[7].Visible = false;
                    spdData_Sheet1.Columns[8].Visible = false;
                    spdData_Sheet1.Columns[9].Visible = false;
                    spdData_Sheet1.Columns[10].Visible = false;
                    spdData_Sheet1.Columns[11].Visible = false;
                }
                else
                {
                    spdData_Sheet1.Columns[5].Visible = true;
                    spdData_Sheet1.Columns[6].Visible = true;
                    spdData_Sheet1.Columns[7].Visible = true;
                    spdData_Sheet1.Columns[8].Visible = true;
                    spdData_Sheet1.Columns[9].Visible = true;
                    spdData_Sheet1.Columns[10].Visible = true;
                    spdData_Sheet1.Columns[11].Visible = true;
                }

                //spdData.RPT_AutoFit(true);
                //spdData.rp

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

        #endregion
        #region SubTotal 평균
        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull)
        {
            double sum = 0;
            double boh = 0;
            double ain = 0;
            double eoh = 0;
            double used = 0;
            double loss = 0;
            double diff = 0;
            double his_diff = 0;
            double s_boh = 0;
            double s_ain = 0;
            double s_eoh = 0;
            double s_used = 0;
            double s_loss = 0;
            double s_diff = 0;
            double s_his_diff = 0;
                        
            double divide = 0;
            double totalDivide = 0;

            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos - 1].BackColor;

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, nColPos - 1].BackColor == color)
                {
                    boh += Convert.ToDouble(spdData.ActiveSheet.Cells[i, 2].Value);
                    ain += Convert.ToDouble(spdData.ActiveSheet.Cells[i, 3].Value);
                    eoh += Convert.ToDouble(spdData.ActiveSheet.Cells[i, 4].Value);
                    used += Convert.ToDouble(spdData.ActiveSheet.Cells[i, 5].Value);
                    loss += Convert.ToDouble(spdData.ActiveSheet.Cells[i, 6].Value);
                    if (ckbHist_Del.Checked == true)
                    {
                        his_diff += Convert.ToDouble(spdData.ActiveSheet.Cells[i, 7].Value);
                        diff += Convert.ToDouble(spdData.ActiveSheet.Cells[i, 8].Value);
                    }
                    else
                    {
                        diff += Convert.ToDouble(spdData.ActiveSheet.Cells[i, 7].Value);
                    }

                    if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos].Value == null || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == ""))
                        continue;

                    divide += 1;
                }
                else
                {
                    if (divide != 0)
                    spdData.ActiveSheet.Cells[i, 2].Value = Math.Round(boh / divide, 0);
                    spdData.ActiveSheet.Cells[i, 3].Value = Math.Round(ain / divide, 0);
                    spdData.ActiveSheet.Cells[i, 4].Value = Math.Round(eoh / divide, 0);
                    spdData.ActiveSheet.Cells[i, 5].Value = Math.Round(used / divide, 0);
                    spdData.ActiveSheet.Cells[i, 6].Value = Math.Round(loss / divide, 0);
                    if (ckbHist_Del.Checked == true)
                    {
                        spdData.ActiveSheet.Cells[i, 7].Value = Math.Round(his_diff / divide, 0);
                        spdData.ActiveSheet.Cells[i, 8].Value = Math.Round(diff / divide, 0);
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[i, 7].Value = Math.Round(diff / divide, 0);
                    }

                    s_boh += Math.Round(boh / divide, 0);
                    s_ain += Math.Round(ain / divide, 0);
                    s_eoh += Math.Round(eoh / divide, 0);
                    s_used += Math.Round(used / divide, 0);
                    s_loss += Math.Round(loss / divide, 0);
                    if (ckbHist_Del.Checked == true)
                    {
                        s_his_diff += Math.Round(his_diff / divide, 0);
                        s_diff += Math.Round(diff / divide, 0);
                    }
                    else
                    {
                        s_diff += Math.Round(diff / divide, 0);
                    }
                    sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos - 1].Value);
                    
                    totalDivide += 1;

                    //sum = 0;
                    divide = 0;
                    boh = 0;
                    ain = 0;
                    eoh = 0;
                    used = 0;
                    loss = 0;
                    diff = 0;
                    his_diff = 0;

                }
            }
            spdData.ActiveSheet.Cells[0, 2].Value = s_boh;
            spdData.ActiveSheet.Cells[0, 3].Value = s_ain;
            spdData.ActiveSheet.Cells[0, 4].Value = s_eoh;
            spdData.ActiveSheet.Cells[0, 5].Value = s_used;
            spdData.ActiveSheet.Cells[0, 6].Value = s_loss;

            if (ckbHist_Del.Checked == true)
            {
                spdData.ActiveSheet.Cells[0, 7].Value = s_his_diff;
                spdData.ActiveSheet.Cells[0, 8].Value = s_diff;
            }
            else
            {
                spdData.ActiveSheet.Cells[0, 7].Value = s_diff;
            }
            spdData.ActiveSheet.Cells[0, nColPos - 1].Value = sum;

        }
         #endregion


        #region " btnExcelExport_Click : Excel로 내보내기 "

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
            spdData.ExportExcel();
        }

        #endregion


        #region " cdvMatType : Leave Event "

        private void cdvMatType_Leave(object sender, EventArgs e)
        {
            /****************************************************/
            /* Comment : 검색할 제품코드를 대분자로 변경한다.   */
            /*                                                  */
            /* Created By :  정병주  (2010-04-01)               */
            /*                                                  */
            /* Modified By : 정병주  (2010-04-01)               */
            /****************************************************/
            cdvMatType.Text = cdvMatType.Text.ToUpper();
        }

        #endregion

        #endregion

        private void udcMat_Code_ValueButtonPress(object sender, EventArgs e)
        {
            // 2010-04-01 정병주 : 자재 타입에 따라 mat_id를 보여준다.           
            string strquery1 = string.Empty;
            strquery1 = "SELECT DISTINCT MATCODE, RESV_FIELD_2 FROM CWIPBOMDEF WHERE RESV_FIELD_2 = '" + cdvMatType.Text.Trim() + "' ORDER BY MATCODE";
            udcMat_Code.sDynamicQuery = strquery1;

        }

        private void cdvMatType_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            udcMat_Code.Text = "";
            if (cdvMatType.Text == "MC") ChkEmc.Visible = true;
            else ChkEmc.Visible = false;

        }

    }
}
