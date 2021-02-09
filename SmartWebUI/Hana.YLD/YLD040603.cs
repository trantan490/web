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

namespace Hana.YLD
{
    public partial class YLD040603 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        

        /// <summary>
        /// 클  래  스: YLD040603<br/>
        /// 클래스요약: 공정별 수율 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2014-02-04<br/>
        /// 상세  설명: 공정별 수율 조회<br/>        
        /// 변경  내용: <br/>
        /// 2014-02-12-임종우 : 공정그룹 검색 기능 추가(김권수 요청)
        /// 2014-02-19-임종우 : PIN TYPE 추가 (김권수 요청)
        /// </summary>
        public YLD040603()
        {
            InitializeComponent();
            OptionInit(); // 초기화
            SortInit(); // group option 초기화
            GridColumnInit(); //헤더 한줄짜리
        }

        #region 0.초기화
        /// <summary>
        /// 0.초기화
        /// </summary>
        private void OptionInit()
        {
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory); // Assembly 로 고정
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvFromToDate.AutoBinding();
                        
            cdvFromToDate.AutoBinding(DateTime.Now.AddDays(-2).ToString(), DateTime.Now.AddDays(-1).ToString());

        }
        #endregion

        #region 1.SortInit

        /// <summary>
        /// 1.SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "B.MAT_GRP_1", "MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Major Code", "B.MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG", "B.MAT_GRP_10", "MAT_GRP_10", "MAT_GRP_10", true);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", true);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "B.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "B.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "B.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Code", "B.MAT_CMF_11", "MAT_CMF_11", "MAT_CMF_11", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "B.MAT_ID", "MAT_ID", "MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP Code", "B.VENDOR_ID", "VENDOR_ID", "VENDOR_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "B.MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", false);
        }

        #endregion

        #region 2.한줄헤더생성

        /// <summary>
        /// 2.한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            //spdData.ActiveSheet.Columns.Remove(0, spdData.ActiveSheet.Columns.Count);

            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG Code", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 140);
            spdData.RPT_AddBasicColumn("SAP Code", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Pin Type", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Operation", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);

            spdData.RPT_AddBasicColumn("TOTAL", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("OUT", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("YLD", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PPM", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 12, 4);
                        
            spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
            
            for(int i = 0; i <= 11; i++)
            {
                spdData.RPT_MerageHeaderRowSpan(0, i, 2);
            }

            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                spdData.RPT_MerageHeaderRowSpan(0, 16 + i, 2);
            }

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.

            
        }

        #endregion

        #region 3.조회

        /// <summary>
        /// 3.조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!CheckField()) return; 
            
            DataTable dt = null;            
            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;

                this.Refresh();
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

                spdData.DataSource = dt;
                                
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, 11].Text == "TOTAL")
                        spdData.ActiveSheet.Rows[i].BackColor = Color.LightBlue;
                }

                for (int i = 0; i <= 10; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LightYellow;
                }                               

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
        /// Make Sql
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            string strDate = null;
            string sFrom = null;
            string sTo = null;
            string sIn = null;
            string sOut = null;

            string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            selectDate = cdvFromToDate.getSelectDate();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
                                                    
            switch (cdvFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    sFrom = cdvFromToDate.HmFromDay;
                    sTo = cdvFromToDate.HmToDay;
                    strDate = "WORK_DATE";
                    break;
                case "WEEK":
                    sFrom = cdvFromToDate.FromWeek.SelectedValue.ToString();
                    sTo = cdvFromToDate.ToWeek.SelectedValue.ToString();
                    strDate = "WORK_WEEK";
                    break;
                case "MONTH":
                    sFrom = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
                    sTo = cdvFromToDate.ToYearMonth.Value.ToString("yyyMM");
                    strDate = "WORK_MONTH";
                    break;
                default:
                    sFrom = cdvFromToDate.HmFromDay;
                    sTo = cdvFromToDate.HmToDay;
                    strDate = "WORK_DATE";
                    break;
            }

            if (ckbCV.Checked == true)
            {               
                sIn = "IN_QTY_2";
                sOut = "OUT_QTY_2";
            }
            else
            {
                sIn = "IN_QTY";
                sOut = "OUT_QTY";
            }

            strSqlString.Append("SELECT " + QueryCond3 + "\n");
            strSqlString.Append("     , OPER_GROUP " + "\n");
            strSqlString.Append("     , TTL_IN_QTY " + "\n");
            strSqlString.Append("     , TTL_OUT_QTY " + "\n");
            strSqlString.Append("     , ROUND(TTL_OUT_QTY / TTL_IN_QTY * 100, 4) AS TTL_YIELD " + "\n");
            strSqlString.Append("     , ROUND((TTL_IN_QTY - TTL_OUT_QTY) / TTL_IN_QTY * 1000000, 2) AS PPM " + "\n");

            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlString.AppendFormat("     , DECODE(IN_QTY_D{0}, 0, 0, ROUND(OUT_QTY_D{0} / IN_QTY_D{0} * 100, 4)) AS D{0}_YIELD" + "\n", i.ToString());
            } 

            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT " + QueryCond1 + "\n");
            strSqlString.Append("             , OPER_GROUP " + "\n");
            strSqlString.Append("             , SUM(" + sIn + ") AS TTL_IN_QTY " + "\n");
            strSqlString.Append("             , SUM(" + sOut + ") AS TTL_OUT_QTY  " + "\n");

            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlString.AppendFormat("             , SUM(DECODE({0}, '{1}', " + sIn + ", 0)) AS IN_QTY_D{2}" + "\n", strDate, selectDate[i].ToString(), i.ToString());
                strSqlString.AppendFormat("             , SUM(DECODE({0}, '{1}', " + sOut + ", 0)) AS OUT_QTY_D{2}" + "\n", strDate, selectDate[i].ToString(), i.ToString());
            } 
                                   
            strSqlString.Append("             , OPER_SEQ " + "\n");
            strSqlString.Append("          FROM RSUMOPRYLD A " + "\n");
            strSqlString.Append("             , MWIPMATDEF B " + "\n");   
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + cdvFactory.Text + "'  " + "\n");            
            strSqlString.Append("           AND " + strDate + " BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");
            strSqlString.Append("           AND B.DELETE_FLAG = ' ' " + "\n");

            if (cdvOperGroup.Text != "ALL")
            {
                strSqlString.Append("           AND A.OPER_GROUP " + cdvOperGroup.SelectedValueToQueryString + "\n");
            }

            if (!txtSearchProduct.Text.Equals("%") && !String.IsNullOrEmpty(txtSearchProduct.Text))
                strSqlString.Append("           AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
                        
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND A.LOT_TYPE LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("         GROUP BY " + QueryCond1 + ", OPER_GROUP, OPER_SEQ" + "\n");
            strSqlString.Append("       ) " + "\n");                
            strSqlString.Append(" ORDER BY " + QueryCond2 + ", OPER_SEQ" + "\n");
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion 

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }
            
            return true;
        }
                        

        #region " Event Handler "

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        #endregion

        private void txtSearchProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(sender, e);
            }
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string[] dataArry = new string[11];
            string sWorkDate = null;
            string sOper = null;
            
            // YIELD 클릭 시 팝업 창 띄움
            if ((e.Column == 14 || e.Column >= 16) && spdData.ActiveSheet.Cells[e.Row, e.Column].Text != "")
            {
                string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
                selectDate = cdvFromToDate.getSelectDate();

                // Group 정보 가져와서 담기... 상세 팝업에서 조건값으로 사용하기 위해
                for (int i = 0; i <= 10; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "Customer")
                        dataArry[0] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Major Code")
                        dataArry[1] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "PKG")
                        dataArry[2] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "LD Count")
                        dataArry[3] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Type1")
                        dataArry[4] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Density")
                        dataArry[5] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Generation")
                        dataArry[6] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "PKG Code")
                        dataArry[7] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Product")
                        dataArry[8] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "SAP Code")
                        dataArry[9] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Pin Type")
                        dataArry[10] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                }

                sOper = spdData.ActiveSheet.Cells[e.Row, 11].Value.ToString();

                if (e.Column == 14)
                {
                    sWorkDate = "TOTAL";
                }
                else
                {
                    sWorkDate = selectDate[e.Column-16].ToString() ;
                }

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(sOper, dataArry, sWorkDate));

                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Windows.Forms.Form frm = new YLD040603_P1("", dt);
                    frm.ShowDialog();
                }
            }
        }

        private string MakeSqlDetail(string sOper, string[] dataArry, string sWorkDate)
        {
            StringBuilder strSqlString = new StringBuilder();

            string strDate = null;
            string sFrom = null;
            string sTo = null;
            string sIn = null;            

            switch (cdvFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    sFrom = cdvFromToDate.HmFromDay;
                    sTo = cdvFromToDate.HmToDay;
                    strDate = "WORK_DATE";
                    break;
                case "WEEK":
                    sFrom = cdvFromToDate.FromWeek.SelectedValue.ToString();
                    sTo = cdvFromToDate.ToWeek.SelectedValue.ToString();
                    strDate = "WORK_WEEK";
                    break;
                case "MONTH":
                    sFrom = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
                    sTo = cdvFromToDate.ToYearMonth.Value.ToString("yyyMM");
                    strDate = "WORK_MONTH";
                    break;
                default:
                    sFrom = cdvFromToDate.HmFromDay;
                    sTo = cdvFromToDate.HmToDay;
                    strDate = "WORK_DATE";
                    break;
            }

            if (ckbCV.Checked == true)
            {
                sIn = "IN_QTY_2";                
            }
            else
            {
                sIn = "IN_QTY";                
            }

            strSqlString.Append("WITH DT AS" + "\n");
            strSqlString.Append("(" + "\n");
            strSqlString.Append("SELECT MAT_ID" + "\n");
            strSqlString.Append("  FROM MWIPMATDEF" + "\n");
            strSqlString.Append(" WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("   AND MAT_TYPE = 'FG'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (dataArry[0] != " " && dataArry[0] != null)
                strSqlString.AppendFormat("   AND MAT_GRP_1 = '" + dataArry[0] + "'" + "\n");

            if (dataArry[1] != " " && dataArry[1] != null)
                strSqlString.AppendFormat("   AND MAT_GRP_9 = '" + dataArry[1] + "'" + "\n");

            if (dataArry[2] != " " && dataArry[2] != null)
                strSqlString.AppendFormat("   AND MAT_GRP_10 = '" + dataArry[2] + "'" + "\n");

            if (dataArry[3] != " " && dataArry[3] != null)
                strSqlString.AppendFormat("   AND MAT_GRP_6 = '" + dataArry[3] + "'" + "\n");

            if (dataArry[4] != " " && dataArry[4] != null)
                strSqlString.AppendFormat("   AND MAT_CMF_4 = '" + dataArry[4] + "'" + "\n");

            if (dataArry[5] != " " && dataArry[5] != null)
                strSqlString.AppendFormat("   AND MAT_GRP_7 = '" + dataArry[5] + "'" + "\n");

            if (dataArry[6] != " " && dataArry[6] != null)
                strSqlString.AppendFormat("   AND MAT_GRP_8 = '" + dataArry[6] + "'" + "\n");

            if (dataArry[7] != " " && dataArry[7] != null)
                strSqlString.AppendFormat("   AND MAT_CMF_11 = '" + dataArry[7] + "'" + "\n");

            if (dataArry[8] != " " && dataArry[8] != null)
                strSqlString.AppendFormat("   AND MAT_ID = '" + dataArry[8] + "'" + "\n");

            if (dataArry[9] != " " && dataArry[9] != null)
                strSqlString.AppendFormat("   AND VENDOR_ID = '" + dataArry[9] + "'" + "\n");

            if (dataArry[10] != " " && dataArry[10] != null)
                strSqlString.AppendFormat("   AND MAT_CMF_10 = '" + dataArry[10] + "'" + "\n");

            #endregion

            strSqlString.Append(")" + "\n");
            strSqlString.Append("SELECT LOSS_DESC, LOSS_CODE, RSP_OPER, LOSS_QTY" + "\n");
            strSqlString.Append("     , LOSS_RATIO" + "\n");
            strSqlString.Append("     , SUM(LOSS_RATIO) OVER (ORDER BY RNK ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS ADDEDE_RATIO " + "\n");
            strSqlString.Append("     , ROUND(LOSS_QTY / IN_QTY * 100, 3) AS IN_RATIO" + "\n");
            strSqlString.Append("     , ROUND(LOSS_QTY / IN_QTY * 1000000, 3) AS PPM" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT LOSS_DESC, LOSS_CODE, RSP_OPER, SUM(LOSS_QTY) AS LOSS_QTY" + "\n");
            strSqlString.Append("             , ROUND(RATIO_TO_REPORT(SUM(LOSS_QTY)) OVER() * 100, 2) AS LOSS_RATIO" + "\n");
            strSqlString.Append("             , RNK" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT CASE WHEN RNK > 7 THEN '기타' ELSE LOSS_DESC END AS LOSS_DESC" + "\n");
            strSqlString.Append("                     , CASE WHEN RNK > 7 THEN '-' ELSE LOSS_CODE END AS LOSS_CODE" + "\n");
            strSqlString.Append("                     , CASE WHEN RNK > 7 THEN '-' ELSE RSP_OPER END AS RSP_OPER" + "\n");
            strSqlString.Append("                     , CASE WHEN RNK > 7 THEN 99 ELSE RNK END AS RNK" + "\n");
            strSqlString.Append("                     , LOSS_QTY     " + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT LOSS_DESC, LOSS_CODE, RSP_OPER, SUM(LOSS_QTY) AS LOSS_QTY" + "\n");
            strSqlString.Append("                             , ROW_NUMBER() OVER(ORDER BY SUM(LOSS_QTY) DESC) AS RNK " + "\n");
            strSqlString.Append("                          FROM RSUMMATLOS" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                           AND GUBUN = 'SHIP'" + "\n");

            if (sWorkDate == "TOTAL")
            {
                strSqlString.Append("                           AND " + strDate + " BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");                
            }
            else
            {
                strSqlString.Append("                           AND " + strDate + " = '" + sWorkDate + "'" + "\n");
            }

            if (sOper != "TOTAL")
                strSqlString.Append("                           AND RSP_OPER = '" + sOper + "'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND LOT_TYPE LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            if (ckbCV.Checked == true)
            {
                strSqlString.Append("                           AND QC_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                           AND CV_FLAG <> 'Y' " + "\n");
            }

            strSqlString.Append("                           AND MAT_ID IN (SELECT MAT_ID FROM DT)" + "\n");
            strSqlString.Append("                         GROUP BY LOSS_DESC, LOSS_CODE, RSP_OPER" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY LOSS_DESC, LOSS_CODE, RSP_OPER, RNK" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT SUM(" + sIn + ") AS IN_QTY" + "\n");
            strSqlString.Append("          FROM RSUMOPRYLD" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");

            if (sWorkDate == "TOTAL")
            {
                strSqlString.Append("           AND " + strDate + " BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");
            }
            else
            {
                strSqlString.Append("           AND " + strDate + " = '" + sWorkDate + "'" + "\n");
            }
            
            
            strSqlString.Append("           AND OPER_GROUP = '" + sOper + "'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND LOT_TYPE LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("           AND MAT_ID IN (SELECT MAT_ID FROM DT)" + "\n");
            strSqlString.Append("       ) B" + "\n");
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private void cdvOperGroup_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DECODE(ROWNUM, 1, A, 2, B, 3, C, 4, D, 5, E, 6, F, 7, G, 8, H, 9, I, 10, J) AS Code, '' AS Data" + "\n";
            strQuery += "  FROM (SELECT 1 FROM DUAL CONNECT BY LEVEL <= 10) " + "\n";
            strQuery += "     , ( " + "\n";
            strQuery += "        SELECT 'SAW' AS A, 'D/A' AS B" + "\n";
            strQuery += "             , 'C/A' AS C, 'W/B' AS D " + "\n";
            strQuery += "             , 'M/D' AS E, 'M/K' AS F " + "\n";
            strQuery += "             , 'SBA' AS G, 'SST' AS H " + "\n";
            strQuery += "             , 'PVI' AS I, 'TOTAL' AS J " + "\n";
            strQuery += "           FROM DUAL " + "\n";
            strQuery += "       ) " + "\n";

            cdvOperGroup.sDynamicQuery = strQuery;
        }
    }
}

