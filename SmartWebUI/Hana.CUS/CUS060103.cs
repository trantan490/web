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
    public partial class CUS060103 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: CUS060103<br/>
        /// 클래스요약: 고객사 YIELD<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: 고객사 YIELD<br/>
        /// 변경  내용: <br/>
        /// </summary>
        /// 
        public CUS060103()
        {
            InitializeComponent();

            this.cdvOper.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFromToDate.AutoBinding();
            SortInit();           
            GridColumnInit();
            
            //관리자만 고객사 화면에서 CUSTOMER 별로 조회 가능
            //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            //{
            //    this.udcWIPCondition1.Enabled = true;
            //}
            //else
            //{
            //    this.udcWIPCondition1.Enabled = false;
            //}
        }


        #region 유효성 검사 및 초기화
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvOper.FromText.TrimEnd() == "" || cdvOper.ToText.TrimEnd() == "")
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
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LOTID", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

            ListView FromToItem = new ListView();
            FromToItem = cdvOper.getItemsFromToValue();

            if (cdvOper.FromText != "")
            {
                int iColindex = 0;
                for (int i = 0; i <  FromToItem.Items.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(FromToItem.Items[i].Text + "IN", 0, 10 + iColindex, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    iColindex++;
                    spdData.RPT_AddBasicColumn(FromToItem.Items[i].Text + "OUT", 0, 10 + iColindex, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    iColindex++;
                    spdData.RPT_AddBasicColumn(FromToItem.Items[i].Text + "YIELD", 0, 10 + iColindex, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                    iColindex++;
                }
            }

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID", "B.MAT_ID", "MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot ID", "LOT_ID", "B.LOT_ID", "LOT_ID", true);     
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
            string QueryCond3 = null;
            string sStart_Tran_Time = null;
            sStart_Tran_Time = string.Empty;
            string sEnd_Tran_Time = null;
            sEnd_Tran_Time = string.Empty;
            string bbbb = null;
            bbbb = string.Empty;
                        
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;


            strSqlString.AppendFormat("            SELECT {0} " + "\n", QueryCond1);

            ListView FromToItem = new ListView();
            FromToItem = cdvOper.getItemsFromToValue();

            if (cdvOper.FromText != "")
            {
                for (int i = 0; i < FromToItem.Items.Count; i++)
                {
                    strSqlString.Append("                 , SUM(IN" + i + ")   " + "\n");
                    strSqlString.Append("                 , SUM(OUT" + i + ")  " + "\n");
                    strSqlString.Append("                 , CONCAT(DECODE(SUM(OUT" + i + "), 0, 0, ROUND(SUM(OUT" + i + ")/SUM(IN" + i + ")*100,3)), '%') " + "\n");
                }
            }

            strSqlString.Append("              FROM (  " + "\n");
            strSqlString.AppendFormat("                   SELECT {0} " + "\n", QueryCond1);

            bbbb = cdvOper.getDecodeQuery("DECODE(OPER", "OPER_IN_QTY_1,0)", "IN");
            strSqlString.AppendFormat("{0}", bbbb);
            bbbb = cdvOper.getDecodeQuery("DECODE(OPER", "END_QTY_1,0)", "OUT");
            strSqlString.AppendFormat("{0}", bbbb);

            strSqlString.Append("                     FROM (  " + "\n");
            strSqlString.AppendFormat("                          SELECT {0}, B.OPER  " + "\n", QueryCond2);
            strSqlString.Append("                               , OPER_IN_QTY_1, END_QTY_1         " + "\n");
            strSqlString.Append("                            FROM MWIPMATDEF A, RSUMWIPLTH B      " + "\n");
            strSqlString.Append("                           WHERE B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                             AND A.FACTORY = B.FACTORY  " + "\n");
            strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID  " + "\n");
            strSqlString.Append("                             AND A.MAT_VER = 1  " + "\n");
            strSqlString.Append("                             AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");
            strSqlString.Append("                             AND B.LOT_ID LIKE '" + txtSearchLot.Text + "'" + "\n");
                        
            strSqlString.Append("                             AND B.OPER IN (" + cdvOper.getInQuery() + ")" + "\n");

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            {
                sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
                sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();

                strSqlString.AppendFormat("                             AND B.END_TIME BETWEEN '{0}' AND '{1}' " + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            }
            
            strSqlString.Append("                             AND B.END_QTY_1 <> 0  AND B.OPER_IN_QTY_1 <> 0                                           " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                ;
            }
            else
            {
                strSqlString.Append("                             AND A.MAT_GRP_1 = '" + GlobalVariable.gsCustomer + "'" + "\n");
            }

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                             AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                             AND A.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                             AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                             AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                             AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                             AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                             AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                             AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            strSqlString.Append("                          )       " + "\n");
            strSqlString.AppendFormat("                 GROUP BY {0}, OPER, END_QTY_1 , OPER_IN_QTY_1         " + "\n", QueryCond1);
            strSqlString.Append("                   )          " + "\n");
            strSqlString.AppendFormat("          GROUP BY {0} " + "\n", QueryCond1);
            strSqlString.AppendFormat("          ORDER BY {0} " + "\n", QueryCond1);

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

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2); 
                
                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 9, null, null, btnSort);
                //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_FillColumnData(9, new string[] {"IN", "OUT", "EOH", "BOH" });

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //YIELD 부분의  TOTAL값 및 SUB TOTAL을 계산하지 않고 직접 계산 

                string subtotal = null;

                for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "YIELD")
                    {
                        spdData.ActiveSheet.Cells[0, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 2].Value)) * 100).ToString();

                        for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)
                        {
                            for (int k = 0; k < sub+ 1; k++)
                            {
                                if (spdData.ActiveSheet.Cells[j, k].Value != null)
                                    subtotal = spdData.ActiveSheet.Cells[j, k].Value.ToString();

                                subtotal.Trim();
                                if (subtotal.Length > 5)
                                {
                                    if (subtotal.Substring(subtotal.Length - 5, 5) == "Total")
                                    {
                                        if (Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 2].Value) == 0 || Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 1].Value) == 0)
                                        {
                                            spdData.ActiveSheet.Cells[j, i].Value = 0;
                                        }
                                        else
                                        {
                                            spdData.ActiveSheet.Cells[j, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 2].Value)) * 100).ToString();
                                        }
                                    }
                                } // if (subtotal.Length > 5)
                            } //for (int k = sub + 1; k > 0; k--)
                        } //for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)
                    } // if (spdData.ActiveSheet.Columns[i].Label == "Progress rate")
                }  


                
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
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null);
            spdData.ExportExcel();
        }
        #endregion
    }
}
