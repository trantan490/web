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
    public partial class YLD040801 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// Ŭ  ��  ��: YLD040801<br/>
        /// Ŭ�������: OPER ���� ��ȸ<br/>
        /// ��  ��  ��: �ϳ�����ũ�� ���ؿ�<br/>
        /// �����ۼ���: 2010-09-13<br/>
        /// </summary>
        public YLD040801()
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
            udcDate.AutoBinding(DateTime.Now.AddDays(-7).ToString(), DateTime.Now.AddDays(-1).ToString());
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", "MAT.MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
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
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("MAT_ID", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("OPER", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LOSS", 0, 10, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);

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
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(1);

                spdData.DataSource = dt;

                ////by John (����¥��)
                //1.Griid �հ� ǥ��
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 10, null, null, btnSort);

                ////2. Į�� ����(�ʿ��ϴٸ�..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total�κ� ������
                spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
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
        /// Make Sql
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            // �ð� ���� ����
            String strStartDate = udcDate.Start_Tran_Time;
            String strEndDate = udcDate.End_Tran_Time;
            // ����
            strSqlString.Append("SELECT  " + QueryCond1 + ",MAT.MAT_ID,OPER,SUM(LOSS_QTY) AS LOSS " + "\n");
            strSqlString.Append("FROM    RWIPLOTLSM LSM, " + "\n");
            strSqlString.Append("        MWIPMATDEF MAT " + "\n");
            strSqlString.Append("WHERE   1=1 " + "\n");
            strSqlString.Append("        AND MAT.FACTORY = LSM.FACTORY " + "\n");
            strSqlString.Append("        AND MAT.MAT_ID = LSM.MAT_ID" + "\n");
            strSqlString.Append("        AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("        AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("        AND LSM.TRAN_TIME BETWEEN '" + strStartDate + "' AND '" + strEndDate + "'" + "\n");
            strSqlString.Append("        AND LSM.HIST_DEL_FLAG = ' '" + "\n");

            #region �� ��ȸ�� ���� SQL�� ����
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            #endregion 

            strSqlString.Append("GROUP BY " + QueryCond2 + ",MAT.FACTORY,MAT.MAT_ID,LSM.OPER" + "\n");
            strSqlString.Append("ORDER BY " + QueryCond2 + ",MAT.FACTORY,MAT.MAT_ID,LSM.OPER" + "\n");

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

            //if (rbtDetail.Checked)
            //{
            //    if (cdvStep.Text.Trim().Length == 0)
            //    {
            //        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
            //        return false;
            //    }
            //}

            //if (rbtBrief.Checked)
            //{
            //    if (cdvStepGrp.Text.Trim().Length == 0)
            //    {
            //        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
            //        return false;
            //    }
            //}

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
        }

        #endregion

        private void txtSearchProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(sender, e);
            }
        }
    }
}

