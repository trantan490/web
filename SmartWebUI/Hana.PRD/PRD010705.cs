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
    public partial class PRD010705 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// Ŭ  ��  ��: PRD010705<br/>
        /// Ŭ�������: TEST ���� ���� ����<br/>
        /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
        /// �����ۼ���: 2016-03-03<br/>
        /// ��  ����: TEST ���� ���� ����<br/>
        /// ����  ����: <br/>
        /// </summary>
        public PRD010705()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit(); //��� ����¥�� 
            
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;
            this.SetFactory(cdvFactory.txtValue);            
            cdvFactory.Enabled = false;            
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS Customer", "Customer", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 AS Family", "Family", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 AS Package", "Package", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 AS Type1", "Type1", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 AS Type2", "Type2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 AS LDCount", "LDCount", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 AS Density", "Density", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 AS Generation", "Generation", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10 AS PinType", "PinType", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID AS MAT_ID", "MAT_ID", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7 AS CustDevice", "CustDevice", false);
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
            
            spdData.RPT_AddBasicColumn("PRODUCT", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 120);            
            spdData.RPT_AddBasicColumn("LOT ID", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("OPER", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            
            spdData.RPT_AddBasicColumn("Outsourcing release", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("DATE", 1, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("QTY", 1, 4, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_MerageHeaderColumnSpan(0, 3, 2);

            spdData.RPT_AddBasicColumn("Own Carry-in", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("DATE", 1, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("��ǰ", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("LOSS", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("CV", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("TTL", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_MerageHeaderColumnSpan(0, 5, 5);

            spdData.RPT_AddBasicColumn("Outsourcing WIP", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("TAT", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 70);

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 10, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 11, 2);

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
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (����¥��)
                ////1.Griid �հ� ǥ��
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 3, null, null, btnSort);

                ////2. Į�� ����(�ʿ��ϴٸ�..)
                //spdData.Sheets[0].FrozenColumnCount = 11;

                ////3. Total�κ� ������
                spdData.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //spdData.ActiveSheet.Columns[9].AllowAutoSort = true;
                //spdData.ActiveSheet.Columns[11].AllowAutoSort = true;
                //spdData.ActiveSheet.Columns[12].AllowAutoSort = true;

                //5. TAT �� ��հ� ���ϱ�(GrandTotal�κи� ����. SubTotal�� ����)
                double sum = 0;
                double avr = 0;

                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++) // GrandTotal �κ� �����ϱ� ���� 1���� ����
                {
                    sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, 11].Value);
                }

                avr = sum / (spdData.ActiveSheet.Rows.Count - 1); // GrandTotal Count �����ϱ� ���� -1����.
                spdData.ActiveSheet.Cells[0, 11].Value = avr.ToString();

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

            if (rbtShip.Checked == true)
            {
                strSqlString.Append("SELECT MAT_ID" + "\n");
                strSqlString.Append("     , LOT_ID" + "\n");
                strSqlString.Append("     , OPER" + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(SHIP_TIME, 'YYYYMMDDHH24MISS'),'YYYY-MM-DD PM HH12:MI:SS') AS SHIP_TIME" + "\n");
                strSqlString.Append("     , SHIP_QTY" + "\n");
                strSqlString.Append("     , DECODE(TRIM(RECV_TIME), NULL, NULL, TO_CHAR(TO_DATE(RECV_TIME, 'YYYYMMDDHH24MISS'),'YYYY-MM-DD PM HH12:MI:SS')) AS RECV_TIME" + "\n");
                strSqlString.Append("     , RECV_QTY" + "\n");
                strSqlString.Append("     , LOSS" + "\n");
                strSqlString.Append("     , CV_LOSS" + "\n");
                strSqlString.Append("     , RECV_QTY + LOSS + CV_LOSS AS TTL" + "\n");
                strSqlString.Append("     , DECODE(RECV_TIME, NULL, SHIP_QTY, 0) AS GMT_WIP" + "\n");
                strSqlString.Append("     , TRUNC(DECODE(TRIM(RECV_TIME), NULL, SYSDATE,TO_DATE(RECV_TIME,'YYYYMMDDHH24MISS')) - TO_DATE(SHIP_TIME,'YYYYMMDDHH24MISS'),2) AS TAT" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.MAT_ID" + "\n");
                strSqlString.Append("             , A.LOT_ID" + "\n");
                strSqlString.Append("             , A.OPER" + "\n");
                strSqlString.Append("             , A.TRAN_TIME AS SHIP_TIME" + "\n");
                strSqlString.Append("             , A.QTY_1 AS SHIP_QTY" + "\n");
                strSqlString.Append("             , (SELECT TRAN_TIME FROM CWIPLOTEND WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OLD_OPER = 'T0030'AND HIST_DEL_FLAG = ' ' AND LOT_ID = A.LOT_ID) AS RECV_TIME" + "\n");
                strSqlString.Append("             , NVL((SELECT QTY_1 FROM CWIPLOTEND WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OLD_OPER = 'T0030'AND HIST_DEL_FLAG = ' ' AND LOT_ID = A.LOT_ID),0) AS RECV_QTY" + "\n");
                strSqlString.Append("             , NVL((SELECT SUM(LOSS_QTY) FROM RWIPLOTLSM WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OPER = 'T0030' AND HIST_DEL_FLAG = ' ' AND LOSS_CODE <> '100' AND LOT_ID = A.LOT_ID),0) AS LOSS" + "\n");
                strSqlString.Append("             , NVL((SELECT SUM(LOSS_QTY) FROM RWIPLOTLSM WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OPER = 'T0030' AND HIST_DEL_FLAG = ' ' AND LOSS_CODE = '100'  AND LOT_ID = A.LOT_ID),0) AS CV_LOSS" + "\n");
                strSqlString.Append("          FROM CWIPLOTEND A" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND TRAN_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "'" + "\n");
                strSqlString.Append("           AND OPER = 'T0030'" + "\n");
                strSqlString.Append("           AND HIST_DEL_FLAG = ' '" + "\n");

                if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("           AND MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
                }

                strSqlString.Append("       ) " + "\n");
                strSqlString.Append(" ORDER BY SHIP_TIME  " + "\n");
            }
            else
            {
                strSqlString.Append("SELECT MAT_ID" + "\n");
                strSqlString.Append("     , LOT_ID" + "\n");
                strSqlString.Append("     , OPER" + "\n");
                strSqlString.Append("     , SHIP_TIME" + "\n");
                strSqlString.Append("     , SHIP_QTY" + "\n");
                strSqlString.Append("     , RECV_TIME" + "\n");
                strSqlString.Append("     , RECV_QTY" + "\n");
                strSqlString.Append("     , LOSS" + "\n");
                strSqlString.Append("     , CV_LOSS" + "\n");
                strSqlString.Append("     , RECV_QTY + LOSS + CV_LOSS AS TTL" + "\n");
                strSqlString.Append("     , GMT_WIP" + "\n");
                strSqlString.Append("     , TAT" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.MAT_ID" + "\n");
                strSqlString.Append("             , A.LOT_ID" + "\n");
                strSqlString.Append("             , A.OLD_OPER AS OPER" + "\n");
                strSqlString.Append("             , TO_CHAR(TO_DATE(A.OLD_OPER_IN_TIME, 'YYYYMMDDHH24MISS'),'YYYY-MM-DD PM HH12:MI:SS') AS SHIP_TIME" + "\n");
                strSqlString.Append("             , A.OLD_OPER_IN_QTY_1 AS SHIP_QTY" + "\n");
                strSqlString.Append("             , TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDDHH24MISS'),'YYYY-MM-DD PM HH12:MI:SS') AS RECV_TIME" + "\n");
                strSqlString.Append("             , A.QTY_1 AS RECV_QTY" + "\n");
                strSqlString.Append("             , NVL((SELECT SUM(LOSS_QTY) FROM RWIPLOTLSM WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OPER = 'T0030' AND HIST_DEL_FLAG = ' ' AND LOSS_CODE <> '100' AND LOT_ID = A.LOT_ID),0) AS LOSS" + "\n");
                strSqlString.Append("             , NVL((SELECT SUM(LOSS_QTY) FROM RWIPLOTLSM WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OPER = 'T0030' AND HIST_DEL_FLAG = ' ' AND LOSS_CODE = '100'  AND LOT_ID = A.LOT_ID),0) AS CV_LOSS" + "\n");
                strSqlString.Append("             , 0 AS GMT_WIP" + "\n");
                strSqlString.Append("             , TRUNC(TO_DATE(A.TRAN_TIME,'YYYYMMDDHH24MISS') - TO_DATE(A.OLD_OPER_IN_TIME,'YYYYMMDDHH24MISS'),2) AS TAT" + "\n");
                strSqlString.Append("             , START_QTY_1" + "\n");
                strSqlString.Append("          FROM CWIPLOTEND A     " + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND TRAN_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "'" + "\n");
                strSqlString.Append("           AND OLD_OPER = 'T0030'" + "\n");
                strSqlString.Append("           AND HIST_DEL_FLAG = ' ' " + "\n");

                if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("           AND MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
                }

                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" ORDER BY SHIP_TIME" + "\n");
            }

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
        }
        #endregion

 
    }
}

