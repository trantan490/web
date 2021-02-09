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
    public partial class YLD040602 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// Ŭ  ��  ��: YLD040602<br/>
        /// Ŭ�������: PKG �� ���� ��ȸ<br/>
        /// ��  ��  ��: �ϳ�����ũ�� ��ο�<br/>
        /// �����ۼ���: 2010-12-09<br/>
        /// ��  ����: PKG �� ���� ��ȸ.<br/>
        /// ����  ����: 2011-05-19-��ο�: ���� Report �� PKG�� YIELD ȭ�� ���� ����(���ڸ� Ⱦ���� ǥ��) - in, out, yield �� ǥ��<br/>
        ///             2013-10-17-��ο�: LOT TYPE ALL, P%, E% �������κ���
        /// </summary>
        public YLD040602()
        {
            InitializeComponent();
            OptionInit(); // �ʱ�ȭ
            SortInit(); // group option �ʱ�ȭ
            GridColumnInit(); //��� ����¥��
        }

        #region 0.�ʱ�ȭ
        /// <summary>
        /// 0.�ʱ�ȭ
        /// </summary>
        private void OptionInit()
        {

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory); // Assembly �� ����
            cdvFromToDate.AutoBinding(DateTime.Now.AddDays(-1).ToString(), DateTime.Now.AddDays(-1).ToString());
            //cdvFromToDate.AutoBinding(DateTime.Now.AddDays(-1).ToString(), DateTime.Now.AddDays(-1).ToString());
            //cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            //cdvStartDate.Value = DateTime.Today.AddDays(-2);
            //cdvEndDate.Value = DateTime.Today.AddDays(-1);

            //udcDate.= DateTime.Today.AddDays(-2).ToString() + "220000";
            //udcDate.End_Tran_Time = DateTime.Today.AddDays(-1).ToString() + "215959";
            //udcDate.AutoBinding(DateTime.Now.AddDays(-2).ToString(), DateTime.Now.AddDays(-1).ToString());

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", false);
        }

        #endregion

        #region 2.�����������

        /// <summary>
        /// 2.�����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            //spdData.ActiveSheet.Columns.Remove(0, spdData.ActiveSheet.Columns.Count);

            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Classification", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            
            spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group�׸��� ������� �ݵ�� �������ٰ�.


        }

        #endregion

        #region 3.��ȸ

        /// <summary>
        /// 3.��ȸ
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

                //�׷��� ���� 1������ SubTotal�� ����ϱ� ���ؼ� ù��° ���� ������
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                
                int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, sub + 1, 8, 9, 3, cdvFromToDate.SubtractBetweenFromToDate + 1, btnSort);
                spdData.RPT_FillDataSelectiveCells("Total", 0, 8, 0, 3, true, Align.Center, VerticalAlign.Center);
                spdData.RPT_FillColumnData(8, new string[] { "IN", "OUT", "YIELD" });
                spdData.RPT_AutoFit(false);

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    SetGrandYield(4, cdvFromToDate.SubtractBetweenFromToDate + 9 - i);
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
            string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            selectDate = cdvFromToDate.getSelectDate();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            // ����
            strSqlString.Append("        SELECT " + QueryCond1 + ",''" + "\n");


            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("                    , SUM(DECODE(WORK_MONTH, '{0}', IN_QTY,0)) AS A1" + "\n", selectDate[i].ToString());
                }

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("                    , SUM(DECODE(WORK_MONTH, '{0}', OUT_QTY,0)) AS A2" + "\n", selectDate[i].ToString());
                }

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("                    , ROUND((SUM(DECODE(WORK_MONTH, '{0}', OUT_QTY))/SUM(DECODE(WORK_MONTH, '{0}', IN_QTY)))*100,3) AS A3" + "\n", selectDate[i].ToString());
                }
                
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("                    , SUM(DECODE(WORK_WEEK, '{0}', IN_QTY,0)) AS A1" + "\n", selectDate[i].ToString());
                }

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("                    , SUM(DECODE(WORK_WEEK, '{0}', OUT_QTY,0)) AS A2" + "\n", selectDate[i].ToString());
                }

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("                    , ROUND((SUM(DECODE(WORK_WEEK, '{0}', OUT_QTY))/SUM(DECODE(WORK_WEEK, '{0}', IN_QTY)))*100,3) AS A3" + "\n", selectDate[i].ToString());
                }
                
            }
            else
            {
                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("                    , SUM(DECODE(WORK_DATE, '{0}', IN_QTY,0)) AS A1" + "\n", selectDate[i].ToString());
                }

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("                    , SUM(DECODE(WORK_DATE, '{0}', OUT_QTY,0)) AS A2" + "\n", selectDate[i].ToString());
                }

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strSqlString.AppendFormat("                    , ROUND((SUM(DECODE(WORK_DATE, '{0}', OUT_QTY))/SUM(DECODE(WORK_DATE, '{0}', IN_QTY)))*100,3) AS A3" + "\n", selectDate[i].ToString());
                }
                
            }
           
            strSqlString.Append("          FROM RSUMMATYLD " + "\n");
            strSqlString.Append("          WHERE 1=1 " + "\n");

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("            AND WORK_MONTH BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                strSqlString.Append("            AND WORK_WEEK BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyy") + cdvFromToDate.FromWeek.Text + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyy") + cdvFromToDate.ToWeek.Text + "'" + "\n");
            }
            else
            {
                strSqlString.Append("            AND WORK_DATE BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "'" + "\n");
            }
            //strSqlString.Append("            AND LOT_TYPE " + cdvLotType.SelectedValueToQueryString + "\n");
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("            AND LOT_TYPE LIKE '" + cdvLotType.Text + "'" + "\n");
            }
            strSqlString.Append("          AND MAT_ID NOT IN ( " + "\n");
            strSqlString.Append("                      SELECT MAT_ID " + "\n");
            strSqlString.Append("                        FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                         AND MAT_GRP_1 = 'HX' " + "\n");
            strSqlString.Append("                         AND MAT_GRP_2 = 'BOC' " + "\n");
            strSqlString.Append("                         AND MAT_GRP_3 = 'BOC' " + "\n");
            strSqlString.Append("                         AND MAT_GRP_6 = '60' " + "\n");
            strSqlString.Append("                         AND MAT_GRP_7 IN ('256M','512M') " + "\n");
            strSqlString.Append("                        AND MAT_GRP_8 = 'Orion' " + "\n");
            strSqlString.Append("                        )  " + "\n");

            if (ckbCustomerShip.Checked == true)
                strSqlString.Append("                  AND TO_FACTORY = 'CUSTOMER'" + "\n");

            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("            AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("            AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("            AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("            AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("            AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("            AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("            AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("            AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            strSqlString.Append("        GROUP BY " + QueryCond1 + "\n");
            strSqlString.Append("        ORDER BY " + QueryCond1 + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #region " User Define Function "

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


        #endregion

        #region " Event Handler "

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel �ٷ� ���̱�
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();
        }

        private void rbtBrief_CheckedChanged(object sender, EventArgs e)
        {
            //cdvStep.Visible = !(rbtBrief.Checked);
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

        private void YLD040602_Load(object sender, EventArgs e)
        {
            //�⺻������ Detail�� ���̵���..
            pnlWIPDetail.Visible = true;

        }


        public void SetGrandYield(int nSampleNormalRowPos, int nColPos)
        {
    
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            for (int i = 0;  i < spdData.ActiveSheet.Rows.Count; i++)
            {
                
                // GrandTotal Yield �� ���ϱ�
                if (i == 2)
                {
                    spdData.ActiveSheet.Cells[2, nColPos].Value = Math.Round((Convert.ToDouble(spdData.ActiveSheet.Cells[1, nColPos].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, nColPos].Value)) * 100,3);
                }
            
                // subTotal Yield �� ���ϱ�                
                else if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                {
                    if(spdData.ActiveSheet.Cells[i, 8].Text.Equals("YIELD"))
                        if (!Convert.ToString(spdData.ActiveSheet.Cells[i - 1, nColPos].Value).Equals("0"))
                        {
                            spdData.ActiveSheet.Cells[i, nColPos].Value = Math.Round((Convert.ToDouble(spdData.ActiveSheet.Cells[i - 1, nColPos].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i - 2, nColPos].Value)) * 100, 3);
                        }
                        else
                        {
                            spdData.ActiveSheet.Cells[i, nColPos].Value = "0";
                        }
                }
         
            }

            return;
        }


    }
}

