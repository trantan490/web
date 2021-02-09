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
    public partial class PRD010606 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010606<br/>
        /// 클래스요약: 공정별 생산량<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: 공정별 일드<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// </summary>
        /// 
        public PRD010606()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();           
            GridColumnInit();
            rdbLive.Checked = true;
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
            spdData.RPT_AddBasicColumn("LOTID", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
                        
                             
            
            if (cdvOper.FromText != "")
            {
                // 선택한 OPER의 DESC 가져옴
                StringBuilder strSqlString = new StringBuilder();
                strSqlString.Append("            SELECT OPER, OPER_DESC " + "\n");
                strSqlString.Append("              FROM MWIPOPRDEF " + "\n");
                strSqlString.Append("             WHERE 1=1" + "\n");
                strSqlString.Append("               AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("               AND OPER IN ({0})" + "\n", cdvOper.getInQuery());
                strSqlString.Append("             ORDER BY DECODE(OPER_CMF_2, ' ', 99999, TO_NUMBER(OPER_CMF_2)) ASC" + "\n");

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());      

                int iColindex = 0;
                for (int i = 0; i <  dt.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(dt.Rows[i][0].ToString()+" (" + dt.Rows[i][1].ToString() + ")" , 0, 10 + iColindex, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

                    spdData.RPT_AddBasicColumn("IN", 1, 10 + iColindex, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    iColindex++;
                    spdData.RPT_AddBasicColumn("OUT", 1, 10 + iColindex, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    iColindex++;
                    spdData.RPT_AddBasicColumn("YIELD", 1, 10 + iColindex, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);
                    iColindex++;

                    spdData.RPT_MerageHeaderColumnSpan(0, 10+iColindex-3, 3);
                }
            }
            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 9, 2);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", false);
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

            sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
            sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();


            strSqlString.AppendFormat("            SELECT {0} " + "\n", QueryCond3);

            ListView FromToItem = new ListView();
            FromToItem = cdvOper.getItemsFromToValue();            

            if (cdvOper.FromText != "")
            {
                for (int i = 0; i < FromToItem.Items.Count; i++)
                {
                    strSqlString.Append("                 , SUM(NVL(IN" + i + ",0))   " + "\n");
                    strSqlString.Append("                 , SUM(NVL(OUT" + i + ",0))  " + "\n");
                    strSqlString.Append("                 , DECODE(SUM(NVL(IN" + i + ",0)), 0, 0, ROUND(SUM(NVL(OUT" + i + ",0))/SUM(NVL(IN" + i + ",0))*100,3)) AS YIELD " + "\n");
                }
            }

            strSqlString.Append("              FROM (  " + "\n");
            strSqlString.AppendFormat("                   SELECT {0} " + "\n", QueryCond1);

            for (int i = 0; i < FromToItem.Items.Count; i++)
            {
                if (FromToItem.Items[i].Text == "AZ010" || FromToItem.Items[i].Text == "TZ010" || FromToItem.Items[i].Text == "EZ010")
                {
                    strSqlString.Append("                        , SUM(CASE WHEN OPER = '" + FromToItem.Items[i].Text + "'" + "\n");
                    strSqlString.Append("                                   THEN OPER_IN_QTY_1 " + "\n");
                    strSqlString.Append("                                   ELSE 0 " + "\n");
                    strSqlString.Append("                          END) AS IN" + i + "\n");
                }
                else
                {
                    strSqlString.Append("                        , SUM(CASE WHEN OLD_OPER = '" + FromToItem.Items[i].Text + "'" + "\n");
                    strSqlString.Append("                                   THEN OLD_OPER_IN_QTY_1 " + "\n");
                    strSqlString.Append("                                   ELSE 0 " + "\n");
                    strSqlString.Append("                          END) AS IN" + i + "\n");
                }
            }            
            for (int i = 0; i < FromToItem.Items.Count; i++)
            {
                if (FromToItem.Items[i].Text == "AZ010" || FromToItem.Items[i].Text == "TZ010" || FromToItem.Items[i].Text == "EZ010")
                {
                    strSqlString.Append("                        , SUM(CASE WHEN OPER = '" + FromToItem.Items[i].Text + "'" + "\n");
                    strSqlString.Append("                                   THEN SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                   ELSE 0 " + "\n");
                    strSqlString.Append("                          END) AS OUT" + i + "\n");
                }
                else
                {
                    strSqlString.Append("                        , SUM(CASE WHEN OLD_OPER = '" + FromToItem.Items[i].Text + "'" + "\n");
                    strSqlString.Append("                                   THEN OPER_IN_QTY_1 " + "\n");
                    strSqlString.Append("                                   ELSE 0 " + "\n");
                    strSqlString.Append("                          END) AS OUT" + i + "\n");
                }                
            }

            strSqlString.Append("                     FROM (  " + "\n");                                                  
            strSqlString.AppendFormat("                          SELECT {0}, B.OPER, B.OLD_OPER  " + "\n", QueryCond2);
            strSqlString.Append("                               , OPER_IN_QTY_1, OLD_OPER_IN_QTY_1, C.SHIP_QTY_1  " + "\n");
            strSqlString.Append("                            FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                               , CWIPLOTEND B" + "\n");
            strSqlString.Append("                               ,(" + "\n");
            strSqlString.Append("                                 SELECT * " + "\n");
            strSqlString.Append("                                   FROM VWIPSHPLOT" + "\n");
            strSqlString.Append("                                  WHERE 1=1" + "\n");
            strSqlString.Append("                                    AND FROM_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                                    AND TO_FACTORY = 'CUSTOMER'" + "\n");
            strSqlString.Append("                                    AND FROM_OPER IN ('AZ010','AZ009','TZ010','EZ010', 'F0000')" + "\n");
            strSqlString.Append("                                    AND LOT_TYPE ='W'" + "\n");
            strSqlString.Append("                                    AND OWNER_CODE = 'PROD' " + "\n");
            strSqlString.AppendFormat("                                    AND TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            strSqlString.Append("                                ) C                                     " + "\n");
            strSqlString.Append("                           WHERE 1=1" + "\n");
            strSqlString.Append("                             AND B.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                             AND B.MAT_VER = 1" + "\n");
            
            if(cdvOper.txtFromValue != "A0000" && cdvOper.txtToValue != "AZ010")
            {
                strSqlString.AppendFormat("                             AND B.OLD_OPER IN({0})" + "\n", cdvOper.getInQuery());
                strSqlString.AppendFormat("                             AND B.OPER IN({0})" + "\n", cdvOper.getInQuery());
            }            
            
            
            if(rdbShip.Checked == false)
            {   // 2010-04-30-임종우 : END_TIME -> TRAN_TIME 으로 변경..왜 쌩뚱맞게 END_TIME 이냐고..
                strSqlString.AppendFormat("                             AND B.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            }            
            
            strSqlString.Append("                             AND B.FACTORY = A.FACTORY(+)" + "\n");
            strSqlString.Append("                             AND B.MAT_ID = A.MAT_ID(+)" + "\n");
            strSqlString.Append("                             AND B.FACTORY = C.FROM_FACTORY(+)" + "\n");            
            strSqlString.Append("                             AND A.MAT_VER(+) = 1" + "\n");


            if(txtSearchProduct.Text != "%" &&txtSearchProduct.Text != "")
            strSqlString.Append("                             AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

            if (txtSearchLot.Text != "%" && txtSearchLot.Text != "")
            strSqlString.Append("                             AND B.LOT_ID LIKE '" + txtSearchLot.Text + "'" + "\n");
            
            if(rdbShip.Checked == true)
            {
                strSqlString.Append("                             AND B.LOT_ID IN C.LOT_ID" + "\n");
            }
            else
            {
                strSqlString.Append("                             AND B.LOT_ID = C.LOT_ID(+)" + "\n");
            }            

            
            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                             AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

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

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                             AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("                          )       " + "\n");
            strSqlString.AppendFormat("                 GROUP BY {0}, OPER, OLD_OPER, OPER_IN_QTY_1, OLD_OPER_IN_QTY_1  " + "\n", QueryCond1);
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

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                
                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 9, null, null, btnSort);
                //토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                
                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);
                                
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                
                // 데이타가 0인 컬럼을 삭제 
                spdData.RPT_RemoveZeroColumn(10, 1);

                
                // YIELD 부분의  TOTAL값 및 SUB TOTAL을 계산하지 않고 직접 계산 
                string subtotal = null;                               

                for (int i = 0; i < spdData.ActiveSheet.Columns.Count;i++ )
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "YIELD")
                    {
                        if(Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 2].Value) != 0)
                        spdData.ActiveSheet.Cells[0, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 2].Value)) * 100).ToString();

                        for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)
                        {
                            for (int k = 0; k < sub + 1; k++)
                            {
                                if (spdData.ActiveSheet.Cells[j, k].Value != null)
                                    subtotal = spdData.ActiveSheet.Cells[j, k].Value.ToString();

                                subtotal.Trim();
                                if (subtotal.Length > 5)
                                {
                                    if (subtotal.Substring(subtotal.Length - 5, 5) == "Total")
                                    {
                                        if (Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 2].Value) != 0)
                                        {
                                            spdData.ActiveSheet.Cells[j, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 2].Value)) * 100).ToString();
                                        }
                                        else
                                        {
                                            spdData.ActiveSheet.Cells[j, i].Value = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }                                        
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

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {            
            this.cdvOper.sFactory = cdvFactory.txtValue;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }       

    }
}
