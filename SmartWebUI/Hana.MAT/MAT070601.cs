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
    public partial class MAT070601 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: MAT070601<br/>
        /// 클래스요약: 자재 공정별 실적 현황<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2012-05-21<br/>
        /// 상세  설명: 자재 공정별 실적 현황<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 임종우<br />
        /// 2012-06-07-임종우 : LOT TYPE 검색 기능 추가 (황선미 요청)
        /// 2016-03-02-임종우 : Rework 수량 제외 (김성업D 요청)
        /// </summary>
        public MAT070601()
        {
            InitializeComponent();

            cdvFromToDate.AutoBinding();
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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Material Type", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Material code", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Item name", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Operation", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);            
            spdData.RPT_AddBasicColumn("Total", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("Average production", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Double1, 80);

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

            string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            selectDate = cdvFromToDate.getSelectDate();

            strSqlString.AppendFormat("SELECT B.MAT_TYPE" + "\n");
            strSqlString.AppendFormat("     , A.MAT_ID" + "\n");
            strSqlString.AppendFormat("     , B.MAT_DESC" + "\n");
            strSqlString.AppendFormat("     , A.OPER" + "\n");            

            if (ckbKpcs.Checked == false)   // Kpcs 구분
            {
                strSqlString.AppendFormat("     , SUM(END_QTY) AS TOTAL_QTY" + "\n");

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("     , SUM(DECODE(WORK_DATE, '{0}', END_QTY)) AS END_QTY_{1}" + "\n", selectDate[i].ToString(), i.ToString());
                }

                strSqlString.AppendFormat("     , ROUND(SUM(END_QTY)/{0}, 1) AS AVG" + "\n", cdvFromToDate.SubtractBetweenFromToDate + 1);
            }
            else
            {
                strSqlString.AppendFormat("     , ROUND(SUM(END_QTY) / 1000, 0) AS TOTAL_QTY" + "\n");

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("     , ROUND(SUM(DECODE(WORK_DATE, '{0}', END_QTY)) / 1000, 0) AS END_QTY_{1}" + "\n", selectDate[i].ToString(), i.ToString());
                }

                strSqlString.AppendFormat("     , ROUND(SUM(END_QTY)/{0} / 1000, 1) AS AVG" + "\n", cdvFromToDate.SubtractBetweenFromToDate + 1);
            }
            
            strSqlString.AppendFormat("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT FACTORY, MAT_ID, OPER, WORK_DATE, CM_KEY_3" + "\n");
            strSqlString.AppendFormat("             , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY" + "\n");
            strSqlString.AppendFormat("          FROM RSUMWIPMOV" + "\n");
            strSqlString.AppendFormat("         WHERE 1=1" + "\n");
            strSqlString.AppendFormat("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.AppendFormat("           AND WORK_DATE BETWEEN '" + cdvFromToDate.HmFromDay + "' AND '" + cdvFromToDate.HmToDay + "'" + "\n");
            strSqlString.AppendFormat("           AND (OPER LIKE 'V%' OR OPER LIKE 'M%')" + "\n");

            if (cdvLotType.Text != "ALL")
                strSqlString.AppendFormat("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "' " + "\n");

            strSqlString.AppendFormat("         GROUP BY FACTORY, MAT_ID, OPER, WORK_DATE, CM_KEY_3 " + "\n");
            strSqlString.AppendFormat("       ) A" + "\n");
            strSqlString.AppendFormat("     , MWIPMATDEF B" + "\n");
            strSqlString.AppendFormat(" WHERE 1=1" + "\n");
            strSqlString.AppendFormat("   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.AppendFormat("   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.AppendFormat("   AND A.END_QTY > 0" + "\n");
            strSqlString.AppendFormat("   AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            strSqlString.AppendFormat("   AND A.MAT_ID LIKE '{0}'  " + "\n", txtSearchProduct.Text);
            strSqlString.AppendFormat(" GROUP BY B.MAT_TYPE, B.MAT_DESC, A.MAT_ID, A.OPER" + "\n");
            strSqlString.AppendFormat(" ORDER BY B.MAT_TYPE, B.MAT_DESC, A.MAT_ID, A.OPER" + "\n");

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

                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 4, null, null, btnSort);

                //3. Total부분 셀머지

                spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center);


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
            strQuery += "   AND (OPER LIKE 'V%' OR OPER LIKE 'M%') " + "\n";
            strQuery += " ORDER BY OPER " + "\n";

            cdvOper.sDynamicQuery = strQuery;
        }
    }
}