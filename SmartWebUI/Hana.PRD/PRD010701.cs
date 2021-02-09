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
    public partial class PRD010701 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// Ŭ  ��  ��: PRD010701<br/>
        /// Ŭ�������: ���� ����<br/>
        /// ��  ��  ��: �̶��� ���¼�<br/>
        /// �����ۼ���: 2009-01-19<br/>
        /// ��  ����: ���� ���� ��Ȳ ��ȸ.<br/>
        /// ����  ����: <br/>
        /// ��  ��  ��: �ϳ�����ũ�� ���ؿ�<br />
        /// Excel Export ���� ��� ����<br />
        /// 2012-08-23-������ : ��ü ���� Ʃ��
        /// 2013-08-01-������ : ���� HOLD CODE �������� ���� CODE �߰� ('H55',H74')
        /// 2013-10-17-��ο� : LOT TYPE ALL, P%, E% �������κ���
        /// 2017-10-31-������ : ���� ���� �߰� ('A1460')
        /// 2018-04-20-������ : ���� ���� -> ���� ���� ȭ��� ����(�հ�� ��û)
        /// </summary>
        public PRD010701()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit(); //��� ����¥�� 
            
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(cdvFactory.txtValue);
            //cdvType.sFactory = cdvFactory.txtValue;
            cdvPSite.sFactory = cdvFactory.txtValue;
            cdvFactory.Enabled = false;
            cdvPSite.Text = "ALL";
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS Customer", "Customer", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 AS Family", "Family", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 AS Package", "Package", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 AS Type1", "Type1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 AS Type2", "Type2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 AS LDCount", "LDCount", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 AS Density", "Density", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 AS Generation", "Generation", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10 AS PinType", "PinType", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID AS MAT_ID", "MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7 AS CustDevice", "CustDevice", false);
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
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LotNo", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Oper", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("ShipDate", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("ShipQty", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("RecvDate", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("RecvQty", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("TAT", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
            spdData.RPT_AddBasicColumn("Vendor WIP", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("LossQty", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 80);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group�׸��� ������� �ݵ�� �������ٰ�.
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

                //�׷��� ���� 1������ SubTotal�� ����ϱ� ���ؼ� ù��° ���� ������
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (����¥��)
                ////1.Griid �հ� ǥ��
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 13, null, null, btnSort);

                ////2. Į�� ����(�ʿ��ϴٸ�..)
                spdData.Sheets[0].FrozenColumnCount = 11;

                ////3. Total�κ� ������
                spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                spdData.ActiveSheet.Columns[9].AllowAutoSort = true;
                spdData.ActiveSheet.Columns[11].AllowAutoSort = true;
                spdData.ActiveSheet.Columns[12].AllowAutoSort = true;

                //5. TAT �� ��հ� ���ϱ�(GrandTotal�κи� ����. SubTotal�� ����)
                double sum = 0;
                double avr = 0;

                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++) // GrandTotal �κ� �����ϱ� ���� 1���� ����
                {
                    sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, 17].Value);
                }

                avr = sum / (spdData.ActiveSheet.Rows.Count - 1); // GrandTotal Count �����ϱ� ���� -1����.
                spdData.ActiveSheet.Cells[0, 17].Value = avr.ToString();

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

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;

            string sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
            string sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;


            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond1);
            strSqlString.AppendFormat("     , HLD.LOT_ID " + "\n");
            strSqlString.AppendFormat("     , HLD.OPER " + "\n");
            strSqlString.AppendFormat("     , TO_CHAR(TO_DATE(HLD.HOLD_TRAN_TIME, 'YYYYMMDDHH24MISS'),'YYYY-MM-DD PM HH12:MI:SS') AS SHIP_TIME" + "\n");
            strSqlString.AppendFormat("     , HLD.QTY_1 AS SHIP_QTY " + "\n");
            strSqlString.AppendFormat("     , DECODE(TRIM(HLD.RELEASE_TRAN_TIME), NULL, NULL, TO_CHAR(TO_DATE(HLD.RELEASE_TRAN_TIME, 'YYYYMMDDHH24MISS'),'YYYY-MM-DD PM HH12:MI:SS')) AS RECV_TIME " + "\n");
            strSqlString.AppendFormat("     , DECODE(TRIM(HLD.RELEASE_TRAN_TIME), NULL, 0, HLD.QTY_1) AS RECV_QTY " + "\n");
            strSqlString.AppendFormat("     , TRUNC(DECODE(TRIM(HLD.RELEASE_TRAN_TIME), NULL, SYSDATE,TO_DATE(HLD.RELEASE_TRAN_TIME,'YYYYMMDDHH24MISS')) - TO_DATE(HLD.HOLD_TRAN_TIME,'YYYYMMDDHH24MISS'),2) AS TAT " + "\n");
            strSqlString.AppendFormat("     , DECODE(TRIM(HLD.RELEASE_TRAN_TIME), NULL, HLD.QTY_1, 0) AS CUST_QTY " + "\n");
            strSqlString.AppendFormat("     , ' ' AS LOSS_QTY" + "\n");
            strSqlString.AppendFormat("  FROM RWIPLOTHLD HLD " + "\n");
            strSqlString.AppendFormat("     , MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat(" WHERE 1=1" + "\n");
            strSqlString.AppendFormat("   AND HLD.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.AppendFormat("   AND HLD.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.AppendFormat("   AND HLD.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.AppendFormat("   AND HLD.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.AppendFormat("   AND HLD.HOLD_CODE IN ('S0', 'H55', 'H74') " + "\n");

            if (cdvPSite.Text != "ALL" && cdvPSite.Text != "")            
                strSqlString.AppendFormat("   AND HLD.CMF_2 " + cdvPSite.SelectedValueToQueryString + "\n");            
            /*            
            if (cdvType.Text != "ALL" && cdvType.Text != "")            
                strSqlString.AppendFormat("   AND HLD.CMF_3 " + cdvType.SelectedValueToQueryString + "\n");
            */
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("   AND HLD.CMF_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            if (cdvOper.Text != "ALL" && cdvOper.Text != "")
            {
                strSqlString.AppendFormat("   AND HLD.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            }
            else
            {
                strSqlString.AppendFormat("   AND HLD.OPER IN ('A1050', 'A1440', 'A1450', 'A1460') " + "\n");
            }

            if (rbtRecv.Checked == true)
            {
                strSqlString.AppendFormat("   AND HLD.RELEASE_TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            }
            else
            {
                strSqlString.AppendFormat("   AND HLD.HOLD_TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            }           

            //�� ��ȸ�� ���� SQL�� ����                        
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

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            if (rbtRecv.Checked == true)
                strSqlString.AppendFormat("   AND TRIM(HLD.RELEASE_TRAN_TIME) IS NOT NULL " + "\n");
                                  
            strSqlString.AppendFormat(" ORDER BY {0}, SHIP_TIME " + "\n", QueryCond2);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #endregion

        #region ToExcel

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
        #endregion

        #region FactorySelectChanged

        /// <summary>
        /// FactorySelectChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvType.sFactory = cdvFactory.txtValue;
            cdvPSite.sFactory = cdvFactory.txtValue;
        }
        #endregion

        private void cdvOper_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT OPER AS CODE, OPER_DESC AS DATA " + "\n";
            strQuery += "  FROM MWIPOPRDEF " + "\n";
            strQuery += " WHERE 1=1 " + "\n";
            strQuery += "   AND FACTORY = '" + cdvFactory.Text + "'" + "\n";
            strQuery += "   AND OPER IN ('A1050', 'A1440', 'A1450', 'A1460') " + "\n";
            strQuery += " ORDER BY OPER " + "\n";

            cdvOper.sDynamicQuery = strQuery;
        }
    }
}

