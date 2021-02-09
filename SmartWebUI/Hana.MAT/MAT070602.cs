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
    public partial class MAT070602 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        /// <summary>
        /// 클  래  스: MAT070602<br/>
        /// 클래스요약: 자재 공정별 Movement<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2012-05-25<br/>
        /// 상세  설명: 자재 공정별 Movement<br/>
        /// 변경  내용: <br/>
        /// 2012-08-07-임종우 : M 공정 추가 (황선미 요청)
        /// </summary>
        /// 
        
        public MAT070602()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();           
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;          
        }

        #region 유효성 검사 및 초기화
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

            if (cdvOper.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
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
            spdData.RPT_AddBasicColumn("Material Type", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Material code", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Item name", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Classification", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);

            if (cdvFactory.txtValue != "")
            {
                spdData.RPT_AddDynamicColumn(cdvOper, 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            }

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            
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

            string strVal = null;            
            
            string strFromDate = cdvFromToDate.ExactFromDate;
            string strToDate = cdvFromToDate.ExactToDate;

            strSqlString.AppendFormat("SELECT B.MAT_TYPE, A.MAT_ID, B.MAT_DESC, ' ' " + "\n");            

            strVal = cdvOper.getDecodeQuery("SUM(DECODE(OPER", "BOH_QTY,0))", "BOH_QTY").Replace(", SUM(DECODE", "     , SUM(DECODE");
            strSqlString.AppendFormat("{0}", strVal);
            strVal = null;

            strVal = cdvOper.getDecodeQuery("SUM(DECODE(OPER", "IN_QTY,0))", "IN_QTY").Replace(", SUM(DECODE", "     , SUM(DECODE");
            strSqlString.AppendFormat("{0}", strVal);
            strVal = null;

            strVal = cdvOper.getDecodeQuery("SUM(DECODE(OPER", "END_QTY,0))", "END_QTY").Replace(", SUM(DECODE", "     , SUM(DECODE");
            strSqlString.AppendFormat("{0}", strVal);
            strVal = null;

            strVal = cdvOper.getDecodeQuery("SUM(DECODE(OPER", "EOH_QTY,0))", "EOH_QTY").Replace(", SUM(DECODE", "     , SUM(DECODE");
            strSqlString.AppendFormat("{0}", strVal);
            strVal = null;

            strVal = cdvOper.getDecodeQuery("SUM(DECODE(OPER", "LOSS_QTY,0))", "LOSS_QTY").Replace(", SUM(DECODE", "     , SUM(DECODE");
            strSqlString.AppendFormat("{0}", strVal);
            strVal = null;

            strSqlString.AppendFormat("  FROM ( " + "\n");
            strSqlString.AppendFormat("        SELECT MAT_ID, OPER " + "\n");
            strSqlString.AppendFormat("             , SUM(S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1) AS IN_QTY  " + "\n");            
            strSqlString.AppendFormat("             , SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) AS END_QTY " + "\n");
            strSqlString.AppendFormat("             , 0 AS BOH_QTY, 0 AS EOH_QTY, 0 AS LOSS_QTY" + "\n");
            strSqlString.AppendFormat("          FROM RSUMWIPMOV " + "\n");
            strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("           AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.AppendFormat("           AND WORK_DATE BETWEEN '{0}' AND '{1}' " + "\n", cdvFromToDate.HmFromDay, cdvFromToDate.HmToDay);
            strSqlString.AppendFormat("           AND (OPER LIKE 'V%' OR OPER LIKE 'M%') " + "\n");

            if (cdvLotType.Text != "ALL")
                strSqlString.AppendFormat("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "' " + "\n");

            strSqlString.AppendFormat("         GROUP BY MAT_ID, OPER " + "\n");
            strSqlString.AppendFormat("         HAVING SUM(S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1) + SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) > 0 " + "\n");
            strSqlString.AppendFormat("         UNION ALL " + "\n");
            strSqlString.AppendFormat("        SELECT MAT_ID, OPER " + "\n");
            strSqlString.AppendFormat("             , 0 AS IN_QTY, 0 AS END_QTY, SUM(EOH_QTY_1) AS BOH_QTY, 0 AS EOH_QTY, 0 AS LOSS_QTY " + "\n");
            strSqlString.AppendFormat("          FROM RSUMWIPEOH " + "\n");
            strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("           AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.AppendFormat("           AND WORK_DATE = '{0}' " + "\n", cdvFromToDate.ExactFromDate.Substring(0, 8));
            strSqlString.AppendFormat("           AND LOT_TYPE <> 'W' " + "\n");
            strSqlString.AppendFormat("           AND SHIFT = '3' " + "\n");
            strSqlString.AppendFormat("           AND (OPER LIKE 'V%' OR OPER LIKE 'M%') " + "\n");

            if (cdvLotType.Text != "ALL")
                strSqlString.AppendFormat("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "' " + "\n");

            strSqlString.AppendFormat("         GROUP BY MAT_ID, OPER " + "\n");            
            strSqlString.AppendFormat("         UNION ALL " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvFromToDate.HmToDay)
            {
                strSqlString.AppendFormat("        SELECT MAT_ID, OPER " + "\n");
                strSqlString.AppendFormat("             , 0 AS IN_QTY, 0 AS END_QTY, 0 AS BOH_QTY, SUM(QTY_1) AS EOH_QTY, 0 AS LOSS_QTY " + "\n");
                strSqlString.AppendFormat("          FROM RWIPLOTSTS " + "\n");
                strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
                strSqlString.AppendFormat("           AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.AppendFormat("           AND LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.AppendFormat("           AND LOT_TYPE <> 'W' " + "\n");
                strSqlString.AppendFormat("           AND (OPER LIKE 'V%' OR OPER LIKE 'M%') " + "\n");
                strSqlString.AppendFormat("           AND QTY_1 > 0 " + "\n");

                if (cdvLotType.Text != "ALL")
                    strSqlString.AppendFormat("           AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "' " + "\n");

                strSqlString.AppendFormat("         GROUP BY MAT_ID, OPER " + "\n");
            }
            else
            {
                strSqlString.AppendFormat("        SELECT MAT_ID, OPER " + "\n");
                strSqlString.AppendFormat("             , 0 AS IN_QTY, 0 AS END_QTY, 0 AS BOH_QTY, SUM(EOH_QTY_1) AS EOH_QTY, 0 AS LOSS_QTY " + "\n");
                strSqlString.AppendFormat("          FROM RSUMWIPEOH " + "\n");
                strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
                strSqlString.AppendFormat("           AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.AppendFormat("           AND WORK_DATE = '{0}' " + "\n", cdvFromToDate.HmToDay);
                strSqlString.AppendFormat("           AND LOT_TYPE <> 'W' " + "\n");
                strSqlString.AppendFormat("           AND SHIFT = '3' " + "\n");
                strSqlString.AppendFormat("           AND (OPER LIKE 'V%' OR OPER LIKE 'M%') " + "\n");

                if (cdvLotType.Text != "ALL")
                    strSqlString.AppendFormat("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "' " + "\n");

                strSqlString.AppendFormat("         GROUP BY MAT_ID, OPER " + "\n");                
            }

            strSqlString.AppendFormat("         UNION ALL " + "\n");
            strSqlString.AppendFormat("        SELECT MAT_ID, OPER " + "\n");
            strSqlString.AppendFormat("             , 0 AS IN_QTY, 0 AS END_QTY, 0 AS BOH_QTY, 0 AS EOH_QTY, SUM(LOSS_QTY) AS LOSS" + "\n");
            strSqlString.AppendFormat("          FROM RWIPLOTLSM " + "\n");
            strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("           AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.AppendFormat("           AND TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", cdvFromToDate.ExactFromDate, cdvFromToDate.ExactToDate);
            strSqlString.AppendFormat("           AND (OPER LIKE 'V%' OR OPER LIKE 'M%') " + "\n");
            strSqlString.AppendFormat("         GROUP BY MAT_ID, OPER " + "\n");
            strSqlString.AppendFormat("       ) A " + "\n");
            strSqlString.AppendFormat("     , MWIPMATDEF B " + "\n");
            strSqlString.AppendFormat(" WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("   AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.AppendFormat("   AND A.MAT_ID LIKE '{0}'  " + "\n", txtSearchProduct.Text);
            strSqlString.AppendFormat("   AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            strSqlString.AppendFormat("   AND B.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.AppendFormat("   AND B.DELETE_FLAG = ' ' " + "\n");
            strSqlString.AppendFormat(" GROUP BY B.MAT_TYPE, A.MAT_ID, B.MAT_DESC " + "\n");
            strSqlString.AppendFormat(" ORDER BY B.MAT_TYPE, A.MAT_ID, B.MAT_DESC " + "\n");

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
                LoadingPopUp.LoadIngPopUpShow(this);            
                this.Refresh();

                
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                GridColumnInit();

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 3, 4, 5, cdvOper.SelectCount, btnSort);            
                               
                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 5, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_FillColumnData(3, new string[] { "BOH", "IN", "OUT", "EOH", "LOSS" });

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
                
        #endregion

        private void cdvOper_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT OPER AS CODE, OPER_DESC AS DATA " + "\n";
            strQuery += "  FROM MWIPOPRDEF " + "\n";
            strQuery += " WHERE 1=1 " + "\n";
            strQuery += "   AND FACTORY = '" + cdvFactory.Text + "'" + "\n";
            strQuery += "   AND (OPER LIKE 'V%' OR OPER LIKE 'M%')  " + "\n";
            strQuery += " ORDER BY OPER " + "\n";

            cdvOper.sDynamicQuery = strQuery;
        }

     }
}
