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
    public partial class PRD010406 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// Ŭ  ��  ��: PRD010406<br/>
        /// Ŭ�������: TEST IN ���<br/>
        /// ��  ��  ��: �ϳ�����ũ�� ���ؿ�<br/>
        /// �����ۼ���: 2009-03-02<br/>
        /// ��  ����: TEST IN ���<br/>
        /// IML TEST IN ������ Ship �� Lot ���<br />
        /// Batch ���α׷��� �����Ͽ� IML �����͸� ���� �� ����� �����ش�.<br />
        /// ���� ���̺� 
        /// ����  ����: <br/>
        /// ��  ��  ��: �ϳ�����ũ�� ���ؿ�<br />
        /// Excel Export ���� ��� ����<br />
		/// 
		/// ��  ��  ��: �ϳ�����ũ�� ���ؿ�<br />
		/// Lot Type �˻� ��� �߰�<br />
        /// </summary>
        public PRD010406()
        {
            InitializeComponent();
            OptionInIt(); // �ʱ�ȭ
            SortInit();
            GridColumnInit(); //��� ����¥�� 
        }

        /// <summary>
        /// 
        /// </summary>
        private void OptionInIt()
        {
            cdvDate.AutoBinding(DateTime.Now.AddDays(-2).ToString(), DateTime.Now.AddDays(-1).ToString());
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) as Customer", "Customer", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 as Family", "Family", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 as Package", "Package", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 as Type1", "Type1", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 as Type2", "Type2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 as LDCount", "LDCount", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 as Density", "Density", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 as Generation", "Generation", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID AS MAT_ID", "MAT_ID", false);            
        }

        #endregion

        #region �����������

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {   
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Pin Type", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Lot ID", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

            if (checkHis.Checked == true)
            {
                spdData.RPT_AddBasicColumn("TEST Lot ID", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            }
            else
            {
                spdData.RPT_AddBasicColumn("TEST Lot ID", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            }

            spdData.RPT_AddBasicColumn("Test In Qty", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Ship Qty", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Bonus", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Loss", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Ship Time", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

            if (checkHis.Checked == true)
            {
                spdData.RPT_AddBasicColumn("Ship Flag", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            }
            else
            {
                spdData.RPT_AddBasicColumn("Ship Flag", 0, 9, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            }

            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group�׸��� ������� �ݵ�� �������ٰ�.
        }

        #endregion

        #region ��ȸ

        /// <summary>
        /// ��ȸ
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

                spdData.DataSource = dt;

                //�׷��� ���� 1������ SubTotal�� ����ϱ� ���ؼ� ù��° ���� ������
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue();

                ////by John (����¥��)
                ////1.Griid �հ� ǥ��
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 2, 4, null, null, btnSort);

                ////2. Į�� ����(�ʿ��ϴٸ�..)
                spdData.Sheets[0].FrozenColumnCount = 1;

                ////3. Total�κ� ������
                spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center);

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

        #endregion

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            //string QueryCond1 = null;
            //string QueryCond2 = null;

            //string sStart_Tran_Time = null;
            //string sEnd_Tran_Time = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            //QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            //QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("SELECT  MAT.MAT_CMF_10 AS PKG, " + "\n");
            strSqlString.Append("        SHP.MAT_ID AS PRODUCT, " + "\n");
            strSqlString.Append("        SHP.LOT_ID, " + "\n");
            strSqlString.Append("        SHP.TEST_LOT_ID, " + "\n");
            strSqlString.Append("        SHP.TEST_IN_QTY_1, " + "\n");
            strSqlString.Append("        SHP.TEST_OUT_QTY_1, " + "\n");
            strSqlString.Append("        SHP.TEST_BONUS_QTY_1, " + "\n");
            strSqlString.Append("        SHP.TEST_LOSS_QTY_1, " + "\n");
            strSqlString.Append("        TO_CHAR(TO_DATE(SHP.SHIP_TIME,'YYYYMMDDHH24MISS')) AS SHIP_TIME, " + "\n");
            strSqlString.Append("        SHP.SHIP_LOT_FLAG " + "\n");
            strSqlString.Append("FROM    CSUMTSTSHP@RPTTOMES SHP, " + "\n");
			strSqlString.Append("        MWIPMATDEF MAT " + "\n");
            strSqlString.Append("WHERE   1=1 " + "\n");
            strSqlString.Append("        AND SHP.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("        AND MAT.DELETE_FLAG = ' ' " + "\n");
			strSqlString.Append("        AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");

            if (checkHis.Checked == false)
            {
                strSqlString.Append("        AND SHP.SHIP_LOT_FLAG='Y' " + "\n");
            }


			strSqlString.Append("        AND SHP.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
			strSqlString.Append("        AND SHP.MAT_ID LIKE '" + txtProduct.Text + "' " + "\n");
            strSqlString.Append("        AND SHP.SHIP_TIME BETWEEN '"+ cdvDate.Start_Tran_Time +"' AND '"+ cdvDate.End_Tran_Time +"' " + "\n");
            strSqlString.Append("ORDER BY SHP.MAT_ID,SHP.LOT_ID " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region ToExcel

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage));
                return;
            }
            
            // Excel �ٷ� ���̱�
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
        }
        #endregion

        private void txtProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(sender, e);
            }
        }

        private void PRD010406_Load_1(object sender, EventArgs e)
        {
            txtProduct.Focus();
        }

		private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
		{
			this.SetFactory(cdvFactory.txtValue);
			cdvLotType.sFactory = cdvFactory.txtValue;
		}
    }
}

