using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

// 2013-09-26-������ : H101�� �̿��Ͽ� EAI�� Message�� �����ϱ� ���Ͽ� �߰���.
using com.miracom.transceiverx;
using com.miracom.transceiverx.session;
using com.miracom.transceiverx.message;

namespace Hana.REG
{
    public partial class REG080101 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        
        /// <summary>
        /// Ŭ  ��  ��: REG080101<br/>
        /// Ŭ�������: PLAN ��ȸ<br/>
        /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
        /// �����ۼ���: 2013-09-23<br/>
        /// ��  ����: PLAN ��ȸ<br/>
        /// ����  ����: <br/> 
		/// </summary>

		#region " Variable Definition "

		// 2013-09-25-������ : ���������� ����ϱ� ���Ͽ� ������.
		String sWorkWeek = "";

		#endregion


		#region " Program Initial "
		
		public REG080101()
        {
			DataTable dt = null;
			String QRY = "";

            InitializeComponent();
			
			GridColumnInit();

			this.Title = "����Ȯ��ü��(PLAN ��ȸ/����)";

			cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvPlanVersion.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvPlanWeek.sFactory = GlobalVariable.gsAssyDefaultFactory;

            // 2013-09-25-������ : ���Ͽ� ���� ��ȹ�׸��� ǥ���Ѵ�.
            QRY = "SELECT TO_CHAR(SYSDATE, 'D') FROM DUAL";
			dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY);

			sWorkWeek = dt.Rows[0][0].ToString();
        }

		private void GridColumnInit()
        {
			/************************************************
			 * comment : sheet�� column header�� �����Ѵ�.
			 * 
			 * created by : bee-jae jung(2013-09-23-������)
			 * 
			 * modified by : bee-jae jung(2013-09-23-������)
			 ************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				SS01.RPT_ColumnInit();
				SS01.RPT_AddBasicColumn2("FACTORY", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, true, 70);
				SS01.RPT_AddBasicColumn2("PLAN_WEEK", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, true, 80);
				SS01.RPT_AddBasicColumn2("VERSION", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, true, 80);
				SS01.RPT_AddBasicColumn2("CUSTOMER", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, true, 70);
				SS01.RPT_AddBasicColumn2("MAT_ID", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, true, 100);
				SS01.RPT_AddBasicColumn2("GUBUN", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, true, 80);
				SS01.RPT_AddBasicColumn2("D0_QTY", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D1_QTY", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D2_QTY", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D3_QTY", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D4_QTY", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D5_QTY", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D6_QTY", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D7_QTY", 0, 13, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D8_QTY", 0, 14, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D9_QTY", 0, 15, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D10_QTY", 0, 16, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D11_QTY", 0, 17, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D12_QTY", 0, 18, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D13_QTY", 0, 19, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70); 
				SS01.RPT_AddBasicColumn2("SHIP_QTY", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("RATE", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.PercentDouble2, true, 70);
				SS01.RPT_AddBasicColumn2("REMAINS", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("PROBLEM_CLASS", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, false, 100);
				SS01.RPT_AddBasicColumn2("PROBLEM_TYPE", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, false, 100);
				SS01.RPT_AddBasicColumn2("PROBLEM_COMMENT", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, false, 100);
				// 2013-09-24-������ : row�� merge�ϱ� ����.
				for (int iRow = 0; iRow <= 25; iRow++)
				{
					SS01.RPT_MerageHeaderRowSpan(0, iRow, 2);
				}
				SS01.RPT_AddBasicColumn2("WIP", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("HMK3", 1, 26, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("FINISH", 1, 27, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("MOLD", 1, 28, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("W/B", 1, 29, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("D/A", 1, 30, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("SAW", 1, 31, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				SS01.RPT_AddBasicColumn2("STOCK", 1, 32, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, true, 70);
				// 2013-09-24-������ : column�� merge�ϱ� ����.
				SS01.RPT_MerageHeaderColumnSpan(0, 26, 7);
				SS01.RPT_ColumnConfigFromTable(btnSort);
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
			}
			finally
			{
				LoadingPopUp.LoadingPopUpHidden();
				Cursor.Current = Cursors.Default;
			}
        }

        #endregion


        #region " Common Function "
        
		private Boolean fnDataFind()
        {
			/************************************************
			 * comment : sheet�� column header�� �����Ѵ�.
			 * 
			 * created by : bee-jae jung(2013-09-23-������)
			 * 
			 * modified by : bee-jae jung(2013-09-23-������)
			 ************************************************/
			FarPoint.Win.Spread.CellType.ComboBoxCellType PROBLEM_CLASS = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
			FarPoint.Win.Spread.CellType.ComboBoxCellType PROBLEM_TYPE = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            String QRY = "";
			DataTable dt = null;
			Int32 iRow = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				
				LoadingPopUp.LoadIngPopUpShow(this);

				PROBLEM_CLASS.Items = new string[] {""
				, "1. ǰ�� gate ����"
				, "2. ���� ����"
				, "3. �� ���� ���� ����"
				, "4. ���� ����"
				, "5. ������ �߻�"
				, "6. ��ȹ�� ���� ��û ���� ����"
				, "7. ǰ�� ��ü ����"
				, "8. ���� ����"};

				PROBLEM_TYPE.Items = new string[] { "", "ACO", "SL" };

				// 2013-09-25-������ : ���ں��� ���Ͽ� �´� �����͸� �����ϱ� ���� QUERY
				QRY = "SELECT A.FACTORY, A.PLAN_WEEK, A.VERSION, A.CUSTOMER, A.MAT_ID, A.GUBUN \n"
					+ "		, A.D0_QTY, A.D1_QTY, A.D2_QTY, A.D3_QTY, A.D4_QTY, A.D5_QTY, A.D6_QTY \n"
					+ "		, A.D7_QTY, A.D8_QTY, A.D9_QTY, A.D10_QTY, A.D11_QTY, A.D12_QTY, A.D13_QTY \n"
					+ "		, B.SHIP_QTY \n"
					+ "		, CASE WHEN NVL(CASE WHEN TO_CHAR(SYSDATE, 'D') = 7 THEN D0_QTY \n"
					+ "							 WHEN TO_CHAR(SYSDATE, 'D') = 1 THEN D1_QTY \n"
					+ "							 WHEN TO_CHAR(SYSDATE, 'D') = 2 THEN D2_QTY \n"
					+ "							 WHEN TO_CHAR(SYSDATE, 'D') = 3 THEN D3_QTY \n"
					+ "							 WHEN TO_CHAR(SYSDATE, 'D') = 4 THEN D4_QTY \n"
					+ "							 WHEN TO_CHAR(SYSDATE, 'D') = 5 THEN D5_QTY \n"
					+ "							 WHEN TO_CHAR(SYSDATE, 'D') = 6 THEN D6_QTY \n"
					+ "							 ELSE 0 \n"
					+ "						END, 0) > 0 \n"
					+ "			   THEN ROUND((NVL(B.SHIP_QTY, 0) / NVL(CASE WHEN TO_CHAR(SYSDATE, 'D') = 7 THEN D0_QTY \n"
					+ "														 WHEN TO_CHAR(SYSDATE, 'D') = 1 THEN D1_QTY \n"
					+ "														 WHEN TO_CHAR(SYSDATE, 'D') = 2 THEN D2_QTY \n"
					+ "														 WHEN TO_CHAR(SYSDATE, 'D') = 3 THEN D3_QTY \n"
					+ "														 WHEN TO_CHAR(SYSDATE, 'D') = 4 THEN D4_QTY \n"
					+ "														 WHEN TO_CHAR(SYSDATE, 'D') = 5 THEN D5_QTY \n"
					+ "														 WHEN TO_CHAR(SYSDATE, 'D') = 6 THEN D6_QTY \n"
					+ "														 ELSE 0 \n"
					+ "													 END, 0)) * 100, 2) \n"
					+ "			   ELSE 0 END AS RATE \n"
					+ "		, (NVL(CASE WHEN TO_CHAR(SYSDATE, 'D') = 7 THEN D0_QTY \n"
					+ "				    WHEN TO_CHAR(SYSDATE, 'D') = 1 THEN D1_QTY \n"
					+ "				    WHEN TO_CHAR(SYSDATE, 'D') = 2 THEN D2_QTY \n"
					+ "				    WHEN TO_CHAR(SYSDATE, 'D') = 3 THEN D3_QTY \n"
					+ "				    WHEN TO_CHAR(SYSDATE, 'D') = 4 THEN D4_QTY \n"
					+ "				    WHEN TO_CHAR(SYSDATE, 'D') = 5 THEN D5_QTY \n"
					+ "				    WHEN TO_CHAR(SYSDATE, 'D') = 6 THEN D6_QTY \n"
					+ "				    ELSE 0 \n"
					+ "		       END, 0) - NVL(B.SHIP_QTY, 0)) AS REMAINS\n"
					+ "		, A.RESV_FIELD_1 AS PROBLEM_CLASS, A.RESV_FIELD_2 AS PROBLEM_TYPE, A.RESV_FIELD_3 AS PROBLEM_COMMENT \n"
					+ "		, C.HMK3A AS HMK3, C.FINISH AS FINISH, C.MOLD AS MOLD, C.WB AS WB, C.DA AS DA, C.SAW AS SAW, C.HMK2A AS HMK2A \n"
					+ "  FROM (SELECT FACTORY AS FACTORY, PLAN_WEEK AS PLAN_WEEK, VERSION AS VERSION, CUSTOMER AS CUSTOMER, MAT_ID AS MAT_ID, GUBUN AS GUBUN \n"
					+ "				, D0_QTY AS D0_QTY, D1_QTY AS D1_QTY, D2_QTY AS D2_QTY, D3_QTY AS D3_QTY, D4_QTY AS D4_QTY, D5_QTY AS D5_QTY, D6_QTY AS D6_QTY \n"
					+ "				, D7_QTY AS D7_QTY, D8_QTY AS D8_QTY, D9_QTY AS D9_QTY, D10_QTY AS D10_QTY, D11_QTY AS D11_QTY, D12_QTY AS D12_QTY, D13_QTY AS D13_QTY \n"
					+ "				, RESV_FIELD_1 AS RESV_FIELD_1, RESV_FIELD_2 AS RESV_FIELD_2, RESV_FIELD_3 AS RESV_FIELD_3 \n"
					+ "          FROM CWIPPLNWEK_N@RPTTOMES \n"
					+ "         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
					+ "           AND PLAN_WEEK = '" + cdvPlanWeek.Text + "' \n"
					+ "           AND VERSION = '" + cdvPlanVersion.Text + "' \n"
					+ "           AND MAT_ID LIKE '" + txtMatID.Text + "%' \n"
					+ "           AND GUBUN = '3' \n"
					+ "			UNION ALL \n"
					+ "		   SELECT FACTORY AS FACTORY, PLAN_WEEK AS PLAN_WEEK, VERSION AS VERSION, CUSTOMER AS CUSTOMER, MAT_ID AS MAT_ID, '4' AS GUBUN \n"
					+ "				, 0 AS D0_QTY, 0 AS D1_QTY, 0 AS D2_QTY, 0 AS D3_QTY, 0 AS D4_QTY, 0 AS D5_QTY, 0 AS D6_QTY \n"
					+ "				, 0 AS D7_QTY, 0 AS D8_QTY, 0 AS D9_QTY, 0 AS D10_QTY, 0 AS D11_QTY, 0 AS D12_QTY, 0 AS D13_QTY \n"
					+ "				, RESV_FIELD_1 AS RESV_FIELD_1, RESV_FIELD_2 AS RESV_FIELD_2, RESV_FIELD_3 AS RESV_FIELD_3 \n"
					+ "          FROM CWIPPLNWEK_N@RPTTOMES \n"
					+ "         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
					+ "           AND PLAN_WEEK = '" + cdvPlanWeek.Text + "' \n"
					+ "           AND VERSION = '" + cdvPlanVersion.Text + "' \n"
					+ "           AND MAT_ID LIKE '" + txtMatID.Text + "%' \n"
					+ "           AND GUBUN = '3' \n"
					+ "			  AND MAT_ID NOT IN (SELECT MAT_ID \n"
					+ "								   FROM CWIPPLNWEK_N@RPTTOMES \n"
					+ "								  WHERE FACTORY = '" + cdvFactory.Text + "' \n"
					+ "									AND PLAN_WEEK = '" + cdvPlanWeek.Text + "' \n"
					+ "									AND VERSION = '" + cdvPlanVersion.Text + "' \n"
					+ "									AND MAT_ID LIKE '" + txtMatID.Text + "%' \n"
					+ "									AND GUBUN = '4') \n"
					+ "			UNION ALL \n"
					+ "		   SELECT FACTORY AS FACTORY, PLAN_WEEK AS PLAN_WEEK, VERSION AS VERSION, CUSTOMER AS CUSTOMER, MAT_ID AS MAT_ID, GUBUN AS GUBUN \n"
					+ "				, D0_QTY AS D0_QTY, D1_QTY AS D1_QTY, D2_QTY AS D2_QTY, D3_QTY AS D3_QTY, D4_QTY AS D4_QTY, D5_QTY AS D5_QTY, D6_QTY AS D6_QTY \n"
					+ "				, D7_QTY AS D7_QTY, D8_QTY AS D8_QTY, D9_QTY AS D9_QTY, D10_QTY AS D10_QTY, D11_QTY AS D11_QTY, D12_QTY AS D12_QTY, D13_QTY AS D13_QTY \n"
					+ "				, RESV_FIELD_1 AS RESV_FIELD_1, RESV_FIELD_2 AS RESV_FIELD_2, RESV_FIELD_3 AS RESV_FIELD_3 \n"
					+ "			 FROM CWIPPLNWEK_N@RPTTOMES \n"
					+ "			WHERE FACTORY = '" + cdvFactory.Text + "' \n"
					+ "			  AND PLAN_WEEK = '" + cdvPlanWeek.Text + "' \n"
					+ "			  AND VERSION = '" + cdvPlanVersion.Text + "' \n"
					+ "           AND MAT_ID LIKE '" + txtMatID.Text + "%' \n"
					+ "			  AND GUBUN = '4') A \n"
					+ "	 , (SELECT CM_KEY_1 AS FACTORY, WORK_WEEK, MAT_ID, '3' AS GUBUN, SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1) AS SHIP_QTY \n"
					+ "          FROM RSUMFACMOV \n"
					+ "         WHERE WORK_DATE >= (SELECT MIN(SYS_DATE) FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND PLAN_YEAR || PLAN_WEEK = '" + cdvPlanWeek.Text + "') \n"
					+ "           AND WORK_DATE <= (SELECT MAX(SYS_DATE) FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND PLAN_YEAR || PLAN_WEEK = '" + cdvPlanWeek.Text + "') \n"
					+ "           AND FACTORY = 'CUSTOMER' \n"
					+ "           AND CM_KEY_1 = '" + cdvFactory.Text + "' \n"
					+ "           AND LOT_TYPE = 'W' \n"
					+ "         GROUP BY CM_KEY_1, MAT_ID, WORK_WEEK) B \n"
					+ "		, (SELECT FACTORY, MAT_ID \n"
					+ "				, '3' AS GUBUN \n"
					+ "				, SUM(HMK3A) AS HMK3A \n"
					+ "				, SUM(FINISH) AS FINISH \n"
					+ "				, SUM(MOLD) AS MOLD \n"
					+ "				, SUM(WB) AS WB \n"
					+ "				, SUM(DA) AS DA \n"
					+ "				, SUM(SAW) AS SAW \n"
					+ "				, SUM(HMK2A) AS HMK2A \n"
					+ "			 FROM (SELECT A.FACTORY, A.MAT_ID \n"
					+ "						, SUM(CASE B.OPER_GRP_1 WHEN 'HMK3A' THEN A.QTY_1 ELSE 0 END) AS HMK3A \n"
					+ "						, SUM(CASE WHEN B.OPER_GRP_1 IN ('M/K', 'S/B/A', 'SAW-SORTER', 'AVI') THEN A.QTY_1 ELSE 0 END) AS FINISH \n"
					+ "						, SUM(CASE B.OPER_GRP_1 WHEN 'MOLD' THEN A.QTY_1 ELSE 0 END) AS MOLD \n"
					+ "						, SUM(CASE B.OPER_GRP_1 WHEN 'W/B' THEN A.QTY_1 ELSE 0 END) AS WB \n"
					+ "						, SUM(CASE B.OPER_GRP_1 WHEN 'D/A' THEN A.QTY_1 ELSE 0 END) AS DA \n"
					+ "						, SUM(CASE B.OPER_GRP_1 WHEN 'SAW' THEN A.QTY_1 ELSE 0 END) AS SAW \n"
					+ "						, SUM(CASE B.OPER_GRP_1 WHEN 'HMK2A' THEN A.QTY_1 ELSE 0 END) AS HMK2A \n"
					+ "					 FROM RWIPLOTSTS A, MWIPOPRDEF B \n"
					+ "					WHERE A.FACTORY = B.FACTORY \n"
					+ "					  AND A.OPER = B.OPER \n"
					+ "					  AND A.FACTORY = '" + cdvFactory.Text + "' \n"
					+ "					  AND A.LOT_TYPE = 'W' \n"
					+ "					  AND A.LOT_DEL_FLAG = ' ' \n"
					+ "					GROUP BY A.FACTORY, A.MAT_ID) \n"
					+ "			GROUP BY FACTORY, MAT_ID) C \n"
					+ " WHERE A.FACTORY = B.FACTORY(+) \n"
					+ "   AND A.PLAN_WEEK = B.WORK_WEEK(+) \n"
					+ "   AND A.MAT_ID = B.MAT_ID(+) \n"
					+ "   AND A.GUBUN = B.GUBUN(+) \n"
					+ "	  AND A.FACTORY = C.FACTORY(+) \n"
					+ "   AND A.MAT_ID = C.MAT_ID(+) \n"
					+ "   AND A.GUBUN = C.GUBUN(+) \n"
					+ " ORDER BY A.PLAN_WEEK ASC, A.VERSION ASC, A.CUSTOMER ASC, A.MAT_ID ASC, A.GUBUN ASC";
				
				if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
				{
					Clipboard.SetText(QRY.Replace((char)Keys.Tab, ' '));
				}

				SS01.Sheets[0].RowCount = 0;

				dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY.Replace((char)Keys.Tab, ' '));

				if (dt.Rows.Count <= 0)
				{
					dt.Dispose();
					return false;
				}
				
				SS01.DataSource = dt;
				SS01.RPT_AutoFit(false);
				dt.Dispose();

				// 2013-09-25-������ : work week�� ���� ��ȹ��¥�� ǥ�ÿ��θ� �����Ѵ�.
				switch (sWorkWeek)
				{
					case "7":		// 2013-09-25-������ : �����
						SS01.Sheets[0].Columns[6].Visible = true; SS01.Sheets[0].Columns[6].Width = 70;
						SS01.Sheets[0].Columns[7].Visible = true; SS01.Sheets[0].Columns[7].Width = 70;
						SS01.Sheets[0].Columns[8].Visible = true; SS01.Sheets[0].Columns[8].Width = 70;
						SS01.Sheets[0].Columns[9].Visible = false;
						SS01.Sheets[0].Columns[10].Visible = false;
						SS01.Sheets[0].Columns[11].Visible = false;
						SS01.Sheets[0].Columns[12].Visible = false;
						SS01.Sheets[0].Columns[13].Visible = false;
						SS01.Sheets[0].Columns[14].Visible = false;
						SS01.Sheets[0].Columns[15].Visible = false;
						SS01.Sheets[0].Columns[16].Visible = false;
						SS01.Sheets[0].Columns[17].Visible = false;
						SS01.Sheets[0].Columns[18].Visible = false;
						SS01.Sheets[0].Columns[19].Visible = false;
						break;
					case "1":		// 2013-09-25-������ : �Ͽ���
						SS01.Sheets[0].Columns[6].Visible = false;
						SS01.Sheets[0].Columns[7].Visible = true; SS01.Sheets[0].Columns[7].Width = 70;
						SS01.Sheets[0].Columns[8].Visible = true; SS01.Sheets[0].Columns[8].Width = 70;
						SS01.Sheets[0].Columns[9].Visible = true; SS01.Sheets[0].Columns[9].Width = 70;
						SS01.Sheets[0].Columns[10].Visible = false;
						SS01.Sheets[0].Columns[11].Visible = false;
						SS01.Sheets[0].Columns[12].Visible = false;
						SS01.Sheets[0].Columns[13].Visible = false;
						SS01.Sheets[0].Columns[14].Visible = false;
						SS01.Sheets[0].Columns[15].Visible = false;
						SS01.Sheets[0].Columns[16].Visible = false;
						SS01.Sheets[0].Columns[17].Visible = false;
						SS01.Sheets[0].Columns[18].Visible = false;
						SS01.Sheets[0].Columns[19].Visible = false;
						break;
					case "2":		// 2013-09-25-������ : ������
						SS01.Sheets[0].Columns[6].Visible = false;
						SS01.Sheets[0].Columns[7].Visible = false;
						SS01.Sheets[0].Columns[8].Visible = true; SS01.Sheets[0].Columns[8].Width = 70;
						SS01.Sheets[0].Columns[9].Visible = true; SS01.Sheets[0].Columns[9].Width = 70;
						SS01.Sheets[0].Columns[10].Visible = true; SS01.Sheets[0].Columns[10].Width = 70;
						SS01.Sheets[0].Columns[11].Visible = false;
						SS01.Sheets[0].Columns[12].Visible = false;
						SS01.Sheets[0].Columns[13].Visible = false;
						SS01.Sheets[0].Columns[14].Visible = false;
						SS01.Sheets[0].Columns[15].Visible = false;
						SS01.Sheets[0].Columns[16].Visible = false;
						SS01.Sheets[0].Columns[17].Visible = false;
						SS01.Sheets[0].Columns[18].Visible = false;
						SS01.Sheets[0].Columns[19].Visible = false;
						break;
					case "3":		// 2013-09-25-������ : ȭ����
						SS01.Sheets[0].Columns[6].Visible = false;
						SS01.Sheets[0].Columns[7].Visible = false;
						SS01.Sheets[0].Columns[8].Visible = false;
						SS01.Sheets[0].Columns[9].Visible = true; SS01.Sheets[0].Columns[9].Width = 70;
						SS01.Sheets[0].Columns[10].Visible = true; SS01.Sheets[0].Columns[10].Width = 70;
						SS01.Sheets[0].Columns[11].Visible = true; SS01.Sheets[0].Columns[11].Width = 70;
						SS01.Sheets[0].Columns[12].Visible = false;
						SS01.Sheets[0].Columns[13].Visible = false;
						SS01.Sheets[0].Columns[14].Visible = false;
						SS01.Sheets[0].Columns[15].Visible = false;
						SS01.Sheets[0].Columns[16].Visible = false;
						SS01.Sheets[0].Columns[17].Visible = false;
						SS01.Sheets[0].Columns[18].Visible = false;
						SS01.Sheets[0].Columns[19].Visible = false;
						break;
					case "4":		// 2013-09-25-������ : ������
						SS01.Sheets[0].Columns[6].Visible = false;
						SS01.Sheets[0].Columns[7].Visible = false;
						SS01.Sheets[0].Columns[8].Visible = false;
						SS01.Sheets[0].Columns[9].Visible = false;
						SS01.Sheets[0].Columns[10].Visible = true; SS01.Sheets[0].Columns[10].Width = 70;
						SS01.Sheets[0].Columns[11].Visible = true; SS01.Sheets[0].Columns[11].Width = 70;
						SS01.Sheets[0].Columns[12].Visible = true; SS01.Sheets[0].Columns[12].Width = 70;
						SS01.Sheets[0].Columns[13].Visible = false;
						SS01.Sheets[0].Columns[14].Visible = false;
						SS01.Sheets[0].Columns[15].Visible = false;
						SS01.Sheets[0].Columns[16].Visible = false;
						SS01.Sheets[0].Columns[17].Visible = false;
						SS01.Sheets[0].Columns[18].Visible = false;
						SS01.Sheets[0].Columns[19].Visible = false;
						break;
					case "5":		// 2013-09-25-������ : �����
						SS01.Sheets[0].Columns[6].Visible = false;
						SS01.Sheets[0].Columns[7].Visible = false;
						SS01.Sheets[0].Columns[8].Visible = false;
						SS01.Sheets[0].Columns[9].Visible = false;
						SS01.Sheets[0].Columns[10].Visible = false;
						SS01.Sheets[0].Columns[11].Visible = true; SS01.Sheets[0].Columns[11].Width = 70;
						SS01.Sheets[0].Columns[12].Visible = true; SS01.Sheets[0].Columns[12].Width = 70;
						SS01.Sheets[0].Columns[13].Visible = true; SS01.Sheets[0].Columns[13].Width = 70;
						SS01.Sheets[0].Columns[14].Visible = false;
						SS01.Sheets[0].Columns[15].Visible = false;
						SS01.Sheets[0].Columns[16].Visible = false;
						SS01.Sheets[0].Columns[17].Visible = false;
						SS01.Sheets[0].Columns[18].Visible = false;
						SS01.Sheets[0].Columns[19].Visible = false;
						break;
					case "6":		// 2013-09-25-������ : �ݿ���
						SS01.Sheets[0].Columns[6].Visible = false;
						SS01.Sheets[0].Columns[7].Visible = false;
						SS01.Sheets[0].Columns[8].Visible = false;
						SS01.Sheets[0].Columns[9].Visible = false;
						SS01.Sheets[0].Columns[10].Visible = false;
						SS01.Sheets[0].Columns[11].Visible = false;
						SS01.Sheets[0].Columns[12].Visible = true; SS01.Sheets[0].Columns[12].Width = 70;
						SS01.Sheets[0].Columns[13].Visible = true; SS01.Sheets[0].Columns[13].Width = 70;
						SS01.Sheets[0].Columns[14].Visible = true; SS01.Sheets[0].Columns[14].Width = 70;
						SS01.Sheets[0].Columns[15].Visible = false;
						SS01.Sheets[0].Columns[16].Visible = false;
						SS01.Sheets[0].Columns[17].Visible = false;
						SS01.Sheets[0].Columns[18].Visible = false;
						SS01.Sheets[0].Columns[19].Visible = false;
						break;
				}

				// 2013-09-24-������ : sheet���� gubun�� 3�� �׸��� lock�Ѵ�.
				for (iRow = 0; iRow < SS01.Sheets[0].RowCount; iRow++)
				{
					if (SS01.Sheets[0].Cells[iRow, 5].Text == "3")
					{
						SS01.Sheets[0].Rows[iRow].Locked = true;
						SS01.Sheets[0].Rows[iRow].BackColor = Color.MistyRose;
					}
					else if (SS01.Sheets[0].Cells[iRow, 5].Text == "4")
					{
						SS01.Sheets[0].Cells[iRow, 23].CellType = PROBLEM_CLASS;
						SS01.Sheets[0].Cells[iRow, 24].CellType = PROBLEM_TYPE;
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				LoadingPopUp.LoadingPopUpHidden();
				CmnFunction.ShowMsgBox(ex.Message);
				return false;
			}
			finally
			{
				LoadingPopUp.LoadingPopUpHidden();
				Cursor.Current = Cursors.Default;
			}
        }

		private Boolean fnDataSave()
		{
			/************************************************
			 * comment : sheet�� ������ DB�� �����Ѵ�.
			 * 
			 * created by : bee-jae jung(2013-09-24-ȭ����)
			 * 
			 * modified by : bee-jae jung(2013-09-24-ȭ����)
			 ************************************************/
			Int32 iRow = 0;
			String DQRY = "", DQRY2 = "";
			String IQRY = "", IQRY2 = "";
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				LoadingPopUp.LoadIngPopUpShow(this);

				// 2013-09-24-������ : Sheet�� ������ DB�� �����ϱ� ���Ͽ� Query���� �����Ѵ�.
				for (iRow = 0; iRow < SS01.Sheets[0].RowCount; iRow++)
				{
					if (SS01.Sheets[0].Rows[iRow].Label == "I")
					{
						/********************************************************************************************/
						// comment : RWIPPLNWEK_N Table�� �����͸� ����/�����Ѵ�.
						/********************************************************************************************/
						// 2013-09-26-������ : RWIPPLNWEK_N�� �����͸� �����ϱ� ���� ������ �����͸� �����Ѵ�.
						DQRY = FwxCmnFunction.PackCondition(DQRY, DateTime.Now.ToString("yyyyMMdd"));																			// base_date
						DQRY = FwxCmnFunction.PackCondition(DQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 0].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 0].Value));	// factory
						DQRY = FwxCmnFunction.PackCondition(DQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 1].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 1].Value));	// plan_week
						DQRY = FwxCmnFunction.PackCondition(DQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 2].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 2].Value));	// version
						DQRY = FwxCmnFunction.PackCondition(DQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 3].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 3].Value));	// customer
						DQRY = FwxCmnFunction.PackCondition(DQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 4].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 4].Value));	// mat_id
						DQRY = FwxCmnFunction.PackCondition(DQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 5].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 5].Value));	// gubun
						
						if (CmnFunction.oComm.DeleteData("RWIPPLNWEK_N", "D1", DQRY) != true) 
						{
							LoadingPopUp.LoadingPopUpHidden();
							CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
							return false;
						}

						// 2013-09-26-������ : RWIPPLNWEK_N�� ����� �����͸� �����Ѵ�.
						IQRY = FwxCmnFunction.PackCondition(IQRY, DateTime.Now.ToString("yyyyMMdd"));																			// base_date
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 0].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 0].Value));	// factory
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 1].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 1].Value));	// plan_week
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 2].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 2].Value));	// version
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 3].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 3].Value));	// customer
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 4].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 4].Value));	// mat_id
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 5].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 5].Value));	// gubun
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 6].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 6].Value));		// d0_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 7].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 7].Value));		// d1_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 8].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 8].Value));		// d2_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 9].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 9].Value));		// d3_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 10].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 10].Value));	// d4_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 11].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 11].Value));	// d5_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 12].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 12].Value));	// d6_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 13].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 13].Value));	// d7_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 14].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 14].Value));	// d8_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 15].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 15].Value));	// d9_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 16].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 16].Value));	// d10_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 17].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 17].Value));	// d11_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 18].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 18].Value));	// d12_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 19].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 19].Value));	// d13_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 20].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 20].Value));	// ship_qty
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 21].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 21].Value));	// rate
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 22].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 22].Value));	// remains
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 23].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 23].Value));	// problem_class
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 24].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 24].Value));	// problem_type
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 25].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 25].Value));	// ploblem_comment
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 26].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 26].Value));	// hmka3
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 27].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 27].Value));	// finish
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 28].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 28].Value));	// mold
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 29].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 29].Value));	// w/b
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 30].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 30].Value));	// d/a
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 31].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 31].Value));	// saw
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(SS01.Sheets[0].Cells[iRow, 32].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 32].Value));	// stock
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(DateTime.Now.ToString("yyyyMMddHHmmss")));													// create_time
						IQRY = FwxCmnFunction.PackCondition(IQRY, Convert.ToString(GlobalVariable.gsUserID));																	// create_user_id
						IQRY = FwxCmnFunction.PackCondition(IQRY, "");																											// update_time
						IQRY = FwxCmnFunction.PackCondition(IQRY, "");																											// update_user_id

						if (CmnFunction.oComm.InsertData("RWIPPLNWEK_N", "I1", IQRY) != true)
						{
							LoadingPopUp.LoadingPopUpHidden();
							CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
							return false;
						}
						/********************************************************************************************/


						/********************************************************************************************/
						// comment : CWIPPLNWEK_N Table�� �����͸� ����/�����Ѵ�.
						/********************************************************************************************/
						// 2013-09-26-������ : CWIPPLNWEK_N�� �����͸� �����ϱ� ���� ������ �����͸� �����Ѵ�.
						DQRY2 = FwxCmnFunction.PackCondition(DQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 0].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 0].Value));	// factory
						DQRY2 = FwxCmnFunction.PackCondition(DQRY2, (SS01.Sheets[0].Cells[iRow, 1].Value == null ? "" : Convert.ToString(SS01.Sheets[0].Cells[iRow, 1].Value).Substring(0, 4)));	// plan_year
						DQRY2 = FwxCmnFunction.PackCondition(DQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 2].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 2].Value));	// version
						DQRY2 = FwxCmnFunction.PackCondition(DQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 3].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 3].Value));	// customer
						DQRY2 = FwxCmnFunction.PackCondition(DQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 5].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 5].Value));	// gubun
						DQRY2 = FwxCmnFunction.PackCondition(DQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 1].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 1].Value));	// plan_week
						DQRY2 = FwxCmnFunction.PackCondition(DQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 4].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 4].Value));	// mat_id

						if (CmnFunction.oComm.DeleteData("CWIPPLNWEK_N", "D1", DQRY2) != true)
						{
							LoadingPopUp.LoadingPopUpHidden();
							CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
							return false;
						}
						
						// 2013-09-26-������ : CWIPPLNWEK_N�� ������ ��ȹ�� �����ϱ� ���Ͽ� Query���� �����Ѵ�.
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 0].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 0].Value));		// factory
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, (SS01.Sheets[0].Cells[iRow, 1].Value == null ? "" : Convert.ToString(SS01.Sheets[0].Cells[iRow, 1].Value).Substring(0, 4)));	// plan_year
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 2].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 2].Value));		// version
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 3].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 3].Value));		// customer
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 5].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 5].Value));		// gubun
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 1].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 1].Value));		// plan_week
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 4].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 4].Value));		// mat_id
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "");																											// base_mat_id
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 6].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 6].Value));		// d0_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 7].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 7].Value));		// d1_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 8].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 8].Value));		// d2_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 9].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 9].Value));		// d3_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 10].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 10].Value));		// d4_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 11].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 11].Value));		// d5_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 12].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 12].Value));		// d6_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 13].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 13].Value));		// d7_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 14].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 14].Value));		// d8_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 15].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 15].Value));		// d9_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 16].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 16].Value));		// d10_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 17].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 17].Value));		// d11_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 18].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 18].Value));		// d12_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 19].Value == null ? 0 : SS01.Sheets[0].Cells[iRow, 19].Value));		// d13_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w1_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w2_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w3_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w4_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w5_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w6_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w7_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w8_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w9_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w10_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w11_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, "0");																											// w12_qty
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 23].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 23].Value));	// resv_field_1
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 24].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 24].Value));	// resv_field_2
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(SS01.Sheets[0].Cells[iRow, 25].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 25].Value));	// resv_field_3
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_field_4
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_field_5
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_field_6
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_field_7
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_field_8
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_field_9
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_field_10
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_flag_1
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_flag_2
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_flag_3
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_flag_4
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// resv_flag_5
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, Convert.ToString(DateTime.Now.ToString("yyyyMMddHHmmss")));														// create_time
						IQRY2 = FwxCmnFunction.PackCondition(IQRY2, " ");																											// update_time

						// 2013-09-24-������ : SAP���� ����ϴ� ��ȹTable�� �����͸� �����Ѵ�.
						if (CmnFunction.oComm.InsertData("CWIPPLNWEK_N", "I1", IQRY2) != true)
						{
							LoadingPopUp.LoadingPopUpHidden();
							CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
							return false;
						}
						/********************************************************************************************/
					}
				}

				// 2013-09-24-������ : �۾��� ���������� ó���Ͽ��� ���
				LoadingPopUp.LoadingPopUpHidden();
				CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));
				return true;
			}
			catch (Exception ex)
			{
				LoadingPopUp.LoadingPopUpHidden();
				CmnFunction.ShowMsgBox(ex.Message);
				return false;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private Boolean fnDataSend()
        {
			/************************************************
			 * comment : sheet�� ������ EAI�� �����ϴ� �Լ�
			 * 
			 * created by : bee-jae jung(2013-09-26-�����)
			 * 
			 * modified by : bee-jae jung(2013-09-26-�����)
			 ************************************************/
			String sChannel = "";
			String sMessage = "";
			String sSystime = "";			
			Int32 iRow = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2013-09-26-������ : database�� systime�� ��ȸ�Ѵ�.
				sSystime = CmnFunction.oComm.GetFuncDataString("GetSystemTime", "");

				sChannel = "/PP/PP_015";
				sMessage = "<?xml version='1.0' encoding='UTF-8'?>";
				sMessage = sMessage + "<EaiSend>";

				for (iRow = 0; iRow < SS01.Sheets[0].RowCount; iRow++)
				{
					if (SS01.Sheets[0].Rows[iRow].Label == "I")
					{
						sMessage = sMessage + "<PP_015>";
						sMessage = sMessage + "<SSOUR>" + (SS01.Sheets[0].Cells[iRow, 1].Value == null ? "" : Convert.ToString(SS01.Sheets[0].Cells[iRow, 1].Value).Substring(0, 4)) + "</SSOUR>";	// plan_year
						sMessage = sMessage + "<VRSIO>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 2].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 2].Value) + "</VRSIO>";					// version
						sMessage = sMessage + "<UPCHE>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 3].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 3].Value) + "</UPCHE>";					// customer
						sMessage = sMessage + "<MESCD>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 4].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 4].Value) + "</MESCD>";					// mat_id
						sMessage = sMessage + "<GUBUN>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 5].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 5].Value) + "</GUBUN>";					// gubun
						sMessage = sMessage + "<MENGE1>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 6].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 6].Value) + "</MENGE1>";					// d0_qty
						sMessage = sMessage + "<MENGE2>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 7].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 7].Value) + "</MENGE2>";					// d1_qty
						sMessage = sMessage + "<MENGE3>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 8].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 8].Value) + "</MENGE3>";					// d2_qty
						sMessage = sMessage + "<MENGE4>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 9].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 9].Value) + "</MENGE4>";					// d3_qty
						sMessage = sMessage + "<MENGE5>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 10].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 10].Value) + "</MENGE5>";				// d4_qty
						sMessage = sMessage + "<MENGE6>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 11].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 11].Value) + "</MENGE6>";				// d5_qty
						sMessage = sMessage + "<MENGE7>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 12].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 12].Value) + "</MENGE7>";				// d6_qty
						sMessage = sMessage + "<MENGE8>0</MENGE8>";																																	// w1_qty
						sMessage = sMessage + "<MENGE9>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 13].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 13].Value) + "</MENGE9>";				// d7_qty
						sMessage = sMessage + "<MENGEA>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 14].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 14].Value) + "</MENGEA>";				// d8_qty
						sMessage = sMessage + "<MENGEB>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 15].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 15].Value) + "</MENGEB>";				// d9_qty
						sMessage = sMessage + "<MENGEC>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 16].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 16].Value) + "</MENGEC>";				// d10_qty
						sMessage = sMessage + "<MENGED>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 17].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 17].Value) + "</MENGED>";				// d11_qty
						sMessage = sMessage + "<MENGEE>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 18].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 18].Value) + "</MENGEE>";				// d12_qty
						sMessage = sMessage + "<MENGEF>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 19].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 19].Value) + "</MENGEF>";				// d13_qty
						sMessage = sMessage + "<MENGEG>0</MENGEG>";																																	// w2_qty
						sMessage = sMessage + "<MENGEH>0</MENGEH>";																																	// w3_qty
						sMessage = sMessage + "<MENGEI>0</MENGEI>";																																	// w4_qty
						sMessage = sMessage + "<MENGEJ>0</MENGEJ>";																																	// w5_qty
						sMessage = sMessage + "<MENGEK>0</MENGEK>";																																	// w6_qty
						sMessage = sMessage + "<MENGEL>0</MENGEL>";																																	// w7_qty
						sMessage = sMessage + "<MENGEM>0</MENGEM>";																																	// w8_qty
						sMessage = sMessage + "<MENGEN>0</MENGEN>";																																	// w9_qty
						sMessage = sMessage + "<MENGEO>0</MENGEO>";																																	// w10_qty
						sMessage = sMessage + "<MENGEP>0</MENGEP>";																																	// w11_qty
						sMessage = sMessage + "<MENGEQ>0</MENGEQ>";																																	// w12_qty
						sMessage = sMessage + "<ZSITE>" + Convert.ToString(SS01.Sheets[0].Cells[iRow, 0].Value == null ? "" : SS01.Sheets[0].Cells[iRow, 0].Value) + "</ZSITE>";					// factory
						sMessage = sMessage + "</PP_015>";
					}
				}
				sMessage = sMessage + "</EaiSend>";

				// 2013-09-26-������ : ������ Message�� EAI�� ���Ͽ� SAP�� �����Ѵ�.
				if (sMessage.Trim() != "")
				{
					try
					{
						//���� ����(Inner-Station, PUSH Mode)
						Session ioiSession = Transceiver.createSession("CasterRequestReply", Session_Fields.SESSION_INNER_STATION_MODE | Session_Fields.SESSION_PUSH_DELIVERY_MODE);
						//���� ���� ��� �ɼ�
						ioiSession.setAutoRecovery(true);
						ioiSession.setDefaultTTL(30000);
						//���� ����(���߼����� ����)
						ioiSession.connect("12.230.54.35");
						//�޽��� ���� �� Channel, TTL (Timeout), DeliveryMode, Data ����
						com.miracom.transceiverx.message.Message req = ioiSession.createMessage();

						req.putTTL(30000);
						req.putDeliveryMode(DeliveryType.REQUEST);
						req.putChannel(sChannel);

						req.putData(sMessage);
						//�޽��� ����
						com.miracom.transceiverx.message.Message rep = ioiSession.sendRequest(req);
						//���� �޽��� ó��
						if (rep == null)
						{
							throw new TrxException(TrxException.INVALID_MESSAGE);
						}
						Encoding en = Encoding.UTF8;
						String strRep = en.GetString(rep.getData());

						//���� ���� ���� �� ����
						ioiSession.disconnect();
						ioiSession.destroy();

						// 2013-09-27-������ : receive ���� message�� �Ʒ� �޼����� �����ϰ� ���� ������...
						if (strRep.Contains("HANA.Project.PP.PP_015.PP_015_Rep_M") == false || strRep.Contains("<RET_CD>S</RET_CD>") == false)
						{
							return false;
						}
					}
					catch (Exception ex)
					{
						CmnFunction.ShowMsgBox(ex.Message);
						CmnFunction.ShowMsgBox(ex.StackTrace);
						return false;
					}
					finally
					{
						Cursor.Current = Cursors.Default;
					}
				}
				
				return true;
			}
			catch (Exception ex)
			{
				LoadingPopUp.LoadingPopUpHidden();
				CmnFunction.ShowMsgBox(ex.Message);
				return false;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		#endregion


        #region Controls Events

		private void cdvPlanWeek_ValueButtonPress(object sender, EventArgs e)
		{
			/************************************************
			 * comment : ��ȹ������ ���� �ڷ������ �����Ѵ�.
			 * 
			 * created by : bee-jae jung(2013-09-23-������)
			 * 
			 * modified by : bee-jae jung(2013-09-23-������)
			 ************************************************/
			String QRY = "";
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				QRY = "SELECT DISTINCT PLAN_WEEK AS PLAN_WEEK, ' ' AS DATA \n"
					+ "  FROM CWIPPLNWEK_N@RPTTOMES \n"
					+ " ORDER BY PLAN_WEEK DESC";

				cdvPlanWeek.sDynamicQuery = QRY;
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void cdvPlanVersion_ValueButtonPress(object sender, EventArgs e)
		{
			/************************************************
			 * comment : ��ȹ������ ���� �ڷ������ �����Ѵ�.
			 * 
			 * created by : bee-jae jung(2013-09-23-������)
			 * 
			 * modified by : bee-jae jung(2013-09-23-������)
			 ************************************************/
			String QRY = "";
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				QRY = "SELECT DISTINCT VERSION AS VERSION, ' ' AS DATA \n"
					+ "  FROM CWIPPLNWEK_N@RPTTOMES \n"
					+ " ORDER BY VERSION DESC \n";

				cdvPlanVersion.sDynamicQuery = QRY;
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void btnView_Click(object sender, EventArgs e)
        {
			/************************************************
			 * comment : ��ȸ��ư�� Ŭ���ϸ� �߻��ϴ� �̺�Ʈ
			 * 
			 * created by : bee-jae jung(2013-09-23-������)
			 * 
			 * modified by : bee-jae jung(2013-09-23-������)
			 ************************************************/
            try
            {
				Cursor.Current = Cursors.WaitCursor;

				// 2013-09-23-������ : ��ȹ������ �˻��Ѵ�.
				if (cdvPlanWeek.Text.Trim() == "" || cdvPlanWeek.Text.Trim() == "ALL")
				{
					CmnFunction.ShowMsgBox(RptMessages.GetMessage("REG001", GlobalVariable.gcLanguage));
					return;
				}

				// 2013-09-23-������ : ��ȹ���� ������ �˻��Ѵ�.
				if (cdvPlanVersion.Text.Trim() == "" || cdvPlanVersion.Text.Trim() == "ALL")
				{
					CmnFunction.ShowMsgBox(RptMessages.GetMessage("REG002", GlobalVariable.gcLanguage));
					return;
				}
				
				fnDataFind();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
			/************************************************
			 * comment : excel �������� ��ư�� Ŭ���ϸ�
			 * 
			 * created by : bee-jae jung(2013-09-23-������)
			 * 
			 * modified by : bee-jae jung(2013-09-23-������)
			 ************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				if (SS01.ActiveSheet.Rows.Count > 0)
				{
					ExcelHelper.Instance.subMakeExcel(SS01, this.lblTitle.Text, null, null, true);
				}
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
        }
		
		private void btnSave_Click(object sender, EventArgs e)
		{
			/************************************************
			 * comment : save ��ư�� Ŭ���ϸ�
			 * 
			 * created by : bee-jae jung(2013-09-24-ȭ����)
			 * 
			 * modified by : bee-jae jung(2013-09-24-ȭ����)
			 ************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2013-09-26-������ : SAP�� ������ ������ �����ϸ�
				if (fnDataSend() == true)
				{
					// 2013-09-26-������ : ������ ������ �����ϸ�, ����� �����͸� ��ȸ�Ѵ�.
					if (fnDataSave() == true)
					{
						fnDataFind();
					}
				}
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void SS01_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
		{
			/************************************************
			 * comment : sheet�� �����͸� �����Ѵ�.
			 * 
			 * created by : bee-jae jung(2013-09-24-ȭ����)
			 * 
			 * modified by : bee-jae jung(2013-09-24-ȭ����)
			 ************************************************/
			Int32 iCol_D0 = 0, iCol_D1 = 0, iCol_D2 = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2013-09-24-������ : ����Type�� �����ϸ�
				if (e.Column == 23)
				{
					// 2013-09-24-������ : �Է�/������ ��Ÿ����.
					if (Convert.ToString(SS01.Sheets[0].Cells[e.Row, e.Column].Tag) != "")
					{
						SS01.Sheets[0].Rows[e.Row].Label = "U";
					}
					else
					{
						SS01.Sheets[0].Rows[e.Row].Label = "I";
					}
				}
				else if (e.Column == 24)
				{
					switch (sWorkWeek)
					{
						case "7":		// 2013-09-25-������ : �����
							iCol_D0 = 6;
							iCol_D1 = 7;
							iCol_D2 = 8;
							break;
						case "1":		// 2013-09-25-������ : �Ͽ���
							iCol_D0 = 7;
							iCol_D1 = 8;
							iCol_D2 = 9;
							break;
						case "2":		// 2013-09-25-������ : ������
							iCol_D0 = 8;
							iCol_D1 = 9;
							iCol_D2 = 10;
							break;
						case "3":		// 2013-09-25-������ : ȭ����
							iCol_D0 = 9;
							iCol_D1 = 10;
							iCol_D2 = 11;
							break;
						case "4":		// 2013-09-25-������ : ������
							iCol_D0 = 10;
							iCol_D1 = 11;
							iCol_D2 = 12;
							break;
						case "5":		// 2013-09-25-������ : �����
							iCol_D0 = 11;
							iCol_D1 = 12;
							iCol_D2 = 13;
							break;
						case "6":		// 2013-09-25-������ : �ݿ���
							iCol_D0 = 12;
							iCol_D1 = 13;
							iCol_D2 = 14;
							break;
					}

					// 2013-09-24-������ : SL/ACO�� ���� ������ �߻��� ������ D1, D2�� ��ȹ�� �ݿ��Ѵ�.
					//				  SL : D1 ��ȹ�� �ݿ��Ѵ�.
					//				 ACO : D2 ��ȹ�� �ݿ��Ѵ�.
					if (SS01.Sheets[0].Cells[e.Row, e.Column].Text == "SL")
					{
						// 2013-09-24-������ : D1 ��ȹ�� �����Ѵ�.
						SS01.Sheets[0].Cells[e.Row, iCol_D1].Value = Convert.ToInt32(SS01.Sheets[0].Cells[e.Row - 1, iCol_D1].Value) + Math.Abs(Convert.ToInt32(SS01.Sheets[0].Cells[e.Row - 1, iCol_D0].Value));
						// 2013-09-24-������ : D2 ��ȹ�� �����Ѵ�.
						SS01.Sheets[0].Cells[e.Row, iCol_D2].Value = 0;
						// 2013-09-24-������ : �Է�/������ ��Ÿ����.
						if (Convert.ToString(SS01.Sheets[0].Cells[e.Row, iCol_D1].Tag) != "")
						{
							SS01.Sheets[0].Rows[e.Row].Label = "U";
						}
						else
						{
							SS01.Sheets[0].Rows[e.Row].Label = "I";
						}
					}
					else if (SS01.Sheets[0].Cells[e.Row, e.Column].Text == "ACO")
					{
						// 2013-09-24-������ : D1 ��ȹ�� �����Ѵ�.
						SS01.Sheets[0].Cells[e.Row, iCol_D1].Value = 0;
						// 2013-09-24-������ : D2 ��ȹ�� �����Ѵ�.
						SS01.Sheets[0].Cells[e.Row, iCol_D2].Value = Convert.ToInt32(SS01.Sheets[0].Cells[e.Row - 1, iCol_D2].Value) + Math.Abs(Convert.ToInt32(SS01.Sheets[0].Cells[e.Row - 1, iCol_D0].Value));
						// 2013-09-24-������ : �Է�/������ ��Ÿ����.
						if (Convert.ToString(SS01.Sheets[0].Cells[e.Row, iCol_D2].Tag) != "")
						{
							SS01.Sheets[0].Rows[e.Row].Label = "U";
						}
						else
						{
							SS01.Sheets[0].Rows[e.Row].Label = "I";
						}
					}
					else
					{
						// 2013-09-24-������ : �ű��Է��� ��Ÿ����.
						SS01.Sheets[0].Rows[e.Row].Label = "";
						// 2013-09-24-������ : D1 ��ȹ�� �����Ѵ�.
						SS01.Sheets[0].Cells[e.Row, iCol_D1].Value = 0;
						// 2013-09-24-������ : D2 ��ȹ�� �����Ѵ�.
						SS01.Sheets[0].Cells[e.Row, iCol_D2].Value = 0;
					}
				}
				else if (e.Column == 25)
				{
					// 2013-09-24-������ : �Է�/������ ��Ÿ����.
					if (Convert.ToString(SS01.Sheets[0].Cells[e.Row, e.Column].Tag) != "")
					{
						SS01.Sheets[0].Rows[e.Row].Label = "U";
					}
					else
					{
						SS01.Sheets[0].Rows[e.Row].Label = "I";
					}
				}
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

        #endregion
    }

}