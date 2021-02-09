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

// 2013-09-26-정비재 : H101을 이용하여 EAI로 Message를 전송하기 위하여 추가함.
using com.miracom.transceiverx;
using com.miracom.transceiverx.session;
using com.miracom.transceiverx.message;

namespace Hana.REG
{
    public partial class REG080101 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        
        /// <summary>
        /// 클  래  스: REG080101<br/>
        /// 클래스요약: PLAN 조회<br/>
        /// 작  성  자: 하나마이크론 정비재<br/>
        /// 최초작성일: 2013-09-23<br/>
        /// 상세  설명: PLAN 조회<br/>
        /// 변경  내용: <br/> 
		/// </summary>

		#region " Variable Definition "

		// 2013-09-25-정비재 : 전역변수로 사용하기 위하여 선언함.
		String sWorkWeek = "";

		#endregion


		#region " Program Initial "
		
		public REG080101()
        {
			DataTable dt = null;
			String QRY = "";

            InitializeComponent();
			
			GridColumnInit();

			this.Title = "일일확정체계(PLAN 조회/수정)";

			cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvPlanVersion.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvPlanWeek.sFactory = GlobalVariable.gsAssyDefaultFactory;

            // 2013-09-25-정비재 : 요일에 따라서 계획항목을 표시한다.
            QRY = "SELECT TO_CHAR(SYSDATE, 'D') FROM DUAL";
			dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY);

			sWorkWeek = dt.Rows[0][0].ToString();
        }

		private void GridColumnInit()
        {
			/************************************************
			 * comment : sheet의 column header를 설정한다.
			 * 
			 * created by : bee-jae jung(2013-09-23-월요일)
			 * 
			 * modified by : bee-jae jung(2013-09-23-월요일)
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
				// 2013-09-24-정비재 : row의 merge하기 위함.
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
				// 2013-09-24-정비재 : column을 merge하기 위함.
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
			 * comment : sheet의 column header를 설정한다.
			 * 
			 * created by : bee-jae jung(2013-09-23-월요일)
			 * 
			 * modified by : bee-jae jung(2013-09-23-월요일)
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
				, "1. 품질 gate 지연"
				, "2. 실적 저조"
				, "3. 전 공정 생산 지연"
				, "4. 자재 부족"
				, "5. 부적합 발생"
				, "6. 계획외 영업 요청 물량 대응"
				, "7. 품종 교체 지연"
				, "8. 설비 고장"};

				PROBLEM_TYPE.Items = new string[] { "", "ACO", "SL" };

				// 2013-09-25-정비재 : 일자별로 요일에 맞는 데이터를 저장하기 위한 QUERY
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

				// 2013-09-25-정비재 : work week에 따라서 계획날짜의 표시여부를 결정한다.
				switch (sWorkWeek)
				{
					case "7":		// 2013-09-25-정비재 : 토요일
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
					case "1":		// 2013-09-25-정비재 : 일요일
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
					case "2":		// 2013-09-25-정비재 : 월요일
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
					case "3":		// 2013-09-25-정비재 : 화요일
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
					case "4":		// 2013-09-25-정비재 : 수요일
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
					case "5":		// 2013-09-25-정비재 : 목요일
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
					case "6":		// 2013-09-25-정비재 : 금요일
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

				// 2013-09-24-정비재 : sheet에서 gubun이 3인 항목은 lock한다.
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
			 * comment : sheet의 내용을 DB에 저장한다.
			 * 
			 * created by : bee-jae jung(2013-09-24-화요일)
			 * 
			 * modified by : bee-jae jung(2013-09-24-화요일)
			 ************************************************/
			Int32 iRow = 0;
			String DQRY = "", DQRY2 = "";
			String IQRY = "", IQRY2 = "";
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				LoadingPopUp.LoadIngPopUpShow(this);

				// 2013-09-24-정비재 : Sheet의 내용을 DB에 저장하기 위하여 Query문을 생성한다.
				for (iRow = 0; iRow < SS01.Sheets[0].RowCount; iRow++)
				{
					if (SS01.Sheets[0].Rows[iRow].Label == "I")
					{
						/********************************************************************************************/
						// comment : RWIPPLNWEK_N Table에 데이터를 삭제/저장한다.
						/********************************************************************************************/
						// 2013-09-26-정비재 : RWIPPLNWEK_N에 데이터를 저장하기 전에 기존의 데이터를 삭제한다.
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

						// 2013-09-26-정비재 : RWIPPLNWEK_N에 변경된 데이터를 저장한다.
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
						// comment : CWIPPLNWEK_N Table에 데이터를 삭제/저장한다.
						/********************************************************************************************/
						// 2013-09-26-정비재 : CWIPPLNWEK_N에 데이터를 저장하기 전에 기존의 데이터를 삭제한다.
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
						
						// 2013-09-26-정비재 : CWIPPLNWEK_N에 수정된 계획을 저장하기 위하여 Query문을 생성한다.
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

						// 2013-09-24-정비재 : SAP에서 사용하는 계획Table의 데이터를 수정한다.
						if (CmnFunction.oComm.InsertData("CWIPPLNWEK_N", "I1", IQRY2) != true)
						{
							LoadingPopUp.LoadingPopUpHidden();
							CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
							return false;
						}
						/********************************************************************************************/
					}
				}

				// 2013-09-24-정비재 : 작업을 성공적으로 처리하였을 경우
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
			 * comment : sheet의 내용을 EAI로 전송하는 함수
			 * 
			 * created by : bee-jae jung(2013-09-26-목요일)
			 * 
			 * modified by : bee-jae jung(2013-09-26-목요일)
			 ************************************************/
			String sChannel = "";
			String sMessage = "";
			String sSystime = "";			
			Int32 iRow = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2013-09-26-정비재 : database의 systime을 조회한다.
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

				// 2013-09-26-정비재 : 생성된 Message를 EAI를 통하여 SAP로 전송한다.
				if (sMessage.Trim() != "")
				{
					try
					{
						//세션 생성(Inner-Station, PUSH Mode)
						Session ioiSession = Transceiver.createSession("CasterRequestReply", Session_Fields.SESSION_INNER_STATION_MODE | Session_Fields.SESSION_PUSH_DELIVERY_MODE);
						//세션 복원 기능 옵션
						ioiSession.setAutoRecovery(true);
						ioiSession.setDefaultTTL(30000);
						//세션 연결(개발서버로 연결)
						ioiSession.connect("12.230.54.35");
						//메시지 생성 및 Channel, TTL (Timeout), DeliveryMode, Data 설정
						com.miracom.transceiverx.message.Message req = ioiSession.createMessage();

						req.putTTL(30000);
						req.putDeliveryMode(DeliveryType.REQUEST);
						req.putChannel(sChannel);

						req.putData(sMessage);
						//메시지 전송
						com.miracom.transceiverx.message.Message rep = ioiSession.sendRequest(req);
						//응답 메시지 처리
						if (rep == null)
						{
							throw new TrxException(TrxException.INVALID_MESSAGE);
						}
						Encoding en = Encoding.UTF8;
						String strRep = en.GetString(rep.getData());

						//세션 연결 종료 및 삭제
						ioiSession.disconnect();
						ioiSession.destroy();

						// 2013-09-27-정비재 : receive 받은 message가 아래 메세지를 포함하고 있지 않으면...
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
			 * comment : 계획주차에 대한 자료사전을 실행한다.
			 * 
			 * created by : bee-jae jung(2013-09-23-월요일)
			 * 
			 * modified by : bee-jae jung(2013-09-23-월요일)
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
			 * comment : 계획주차에 대한 자료사전을 실행한다.
			 * 
			 * created by : bee-jae jung(2013-09-23-월요일)
			 * 
			 * modified by : bee-jae jung(2013-09-23-월요일)
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
			 * comment : 조회버튼을 클릭하면 발생하는 이벤트
			 * 
			 * created by : bee-jae jung(2013-09-23-월요일)
			 * 
			 * modified by : bee-jae jung(2013-09-23-월요일)
			 ************************************************/
            try
            {
				Cursor.Current = Cursors.WaitCursor;

				// 2013-09-23-정비재 : 계획주차를 검사한다.
				if (cdvPlanWeek.Text.Trim() == "" || cdvPlanWeek.Text.Trim() == "ALL")
				{
					CmnFunction.ShowMsgBox(RptMessages.GetMessage("REG001", GlobalVariable.gcLanguage));
					return;
				}

				// 2013-09-23-정비재 : 계획주차 버전를 검사한다.
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
			 * comment : excel 내보내기 버튼을 클릭하면
			 * 
			 * created by : bee-jae jung(2013-09-23-월요일)
			 * 
			 * modified by : bee-jae jung(2013-09-23-월요일)
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
			 * comment : save 버튼을 클릭하면
			 * 
			 * created by : bee-jae jung(2013-09-24-화요일)
			 * 
			 * modified by : bee-jae jung(2013-09-24-화요일)
			 ************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2013-09-26-정비재 : SAP로 데이터 전송이 성공하면
				if (fnDataSend() == true)
				{
					// 2013-09-26-정비재 : 데이터 저장이 성공하면, 저장된 데이터를 조회한다.
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
			 * comment : sheet의 데이터를 변경한다.
			 * 
			 * created by : bee-jae jung(2013-09-24-화요일)
			 * 
			 * modified by : bee-jae jung(2013-09-24-화요일)
			 ************************************************/
			Int32 iCol_D0 = 0, iCol_D1 = 0, iCol_D2 = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2013-09-24-정비재 : 차질Type을 선택하면
				if (e.Column == 23)
				{
					// 2013-09-24-정비재 : 입력/수정을 나타낸다.
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
						case "7":		// 2013-09-25-정비재 : 토요일
							iCol_D0 = 6;
							iCol_D1 = 7;
							iCol_D2 = 8;
							break;
						case "1":		// 2013-09-25-정비재 : 일요일
							iCol_D0 = 7;
							iCol_D1 = 8;
							iCol_D2 = 9;
							break;
						case "2":		// 2013-09-25-정비재 : 월요일
							iCol_D0 = 8;
							iCol_D1 = 9;
							iCol_D2 = 10;
							break;
						case "3":		// 2013-09-25-정비재 : 화요일
							iCol_D0 = 9;
							iCol_D1 = 10;
							iCol_D2 = 11;
							break;
						case "4":		// 2013-09-25-정비재 : 수요일
							iCol_D0 = 10;
							iCol_D1 = 11;
							iCol_D2 = 12;
							break;
						case "5":		// 2013-09-25-정비재 : 목요일
							iCol_D0 = 11;
							iCol_D1 = 12;
							iCol_D2 = 13;
							break;
						case "6":		// 2013-09-25-정비재 : 금요일
							iCol_D0 = 12;
							iCol_D1 = 13;
							iCol_D2 = 14;
							break;
					}

					// 2013-09-24-정비재 : SL/ACO에 따라서 차질이 발생한 수량을 D1, D2의 계획에 반영한다.
					//				  SL : D1 계획에 반영한다.
					//				 ACO : D2 계획에 반영한다.
					if (SS01.Sheets[0].Cells[e.Row, e.Column].Text == "SL")
					{
						// 2013-09-24-정비재 : D1 계획을 수정한다.
						SS01.Sheets[0].Cells[e.Row, iCol_D1].Value = Convert.ToInt32(SS01.Sheets[0].Cells[e.Row - 1, iCol_D1].Value) + Math.Abs(Convert.ToInt32(SS01.Sheets[0].Cells[e.Row - 1, iCol_D0].Value));
						// 2013-09-24-정비재 : D2 계획을 수정한다.
						SS01.Sheets[0].Cells[e.Row, iCol_D2].Value = 0;
						// 2013-09-24-정비재 : 입력/수정을 나타낸다.
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
						// 2013-09-24-정비재 : D1 계획을 수정한다.
						SS01.Sheets[0].Cells[e.Row, iCol_D1].Value = 0;
						// 2013-09-24-정비재 : D2 계획을 수정한다.
						SS01.Sheets[0].Cells[e.Row, iCol_D2].Value = Convert.ToInt32(SS01.Sheets[0].Cells[e.Row - 1, iCol_D2].Value) + Math.Abs(Convert.ToInt32(SS01.Sheets[0].Cells[e.Row - 1, iCol_D0].Value));
						// 2013-09-24-정비재 : 입력/수정을 나타낸다.
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
						// 2013-09-24-정비재 : 신규입력을 나타낸다.
						SS01.Sheets[0].Rows[e.Row].Label = "";
						// 2013-09-24-정비재 : D1 계획을 수정한다.
						SS01.Sheets[0].Cells[e.Row, iCol_D1].Value = 0;
						// 2013-09-24-정비재 : D2 계획을 수정한다.
						SS01.Sheets[0].Cells[e.Row, iCol_D2].Value = 0;
					}
				}
				else if (e.Column == 25)
				{
					// 2013-09-24-정비재 : 입력/수정을 나타낸다.
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