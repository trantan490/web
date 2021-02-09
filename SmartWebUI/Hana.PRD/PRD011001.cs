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
    public partial class PRD011001 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string start_date;
        string month;
        string lastMonth;
        DateTime Select_date;
        string year;
        int lastDay;
        string last_day;

        DataTable dtTarget = null;

        /// <summary>
        /// Ŭ  ��  ��: PRD011001<br/>
        /// Ŭ�������: ���� ����<br/>
        /// ��  ��  ��: ��ο�<br/>
        /// �����ۼ���: 2011-10-17<br/>
        /// ��  ����: ���� ����<br/>
        /// ����  ����: <br/> 
        /// 2011-11-21-������ : �� ��ȹ ��ǰ �ߺ� ���� ����
        /// 2011-12-26-������ : MWIPCALDEF �� �۳�,���� ������ ���� ��ġ�� ���� �߻����� SYS_YEAR -> PLAN_YEAR ���� ����

        /// </summary>
        public PRD011001()
        {
            InitializeComponent();
            SortInit2();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            GridColumnInit2();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #region �ʱ�ȭ �� ��ȿ�� �˻�
        /// <summary 1. ��ȿ�� �˻�>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cmbOper.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD064", GlobalVariable.gcLanguage));
                return false;
            }

            if (gubun.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD065", GlobalVariable.gcLanguage));
                return false;
            }


            return true;
        }

        /// <summary>
        /// 2. ��� ����
        /// </summary>
        private void GridColumnInit()
        {
            month = DateTime.Parse(cdvDate.Value.ToString()).ToString("yyyyMM");
            DataTable dt1 = null;
            string Last_Month_Last_day;
            Last_Month_Last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + month + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Last_Month_Last_day);
            Last_Month_Last_day = dt1.Rows[0][0].ToString();
            lastDay = Convert.ToInt16(Last_Month_Last_day.Substring(6, 2));
            spdData.RPT_ColumnInit();
            LabelTextChange();

            try
            {
                spdData.RPT_AddBasicColumn("Classification", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                //1�Ϻ��� ��ȸ �ش���� �������ϱ��� FOR�� ���� �÷� ����
                for (int i = 0; i < lastDay; i++)
                {
                    spdData.RPT_AddBasicColumn(i + 1 + " ��", 0, i + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }


        private void GridColumnInit2()
        {
            spdData2.RPT_ColumnInit();

            try
            {
                spdData2.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData2.RPT_AddBasicColumn("Pin Type", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                spdData2.RPT_AddBasicColumn("Product", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                
                spdData2.RPT_AddBasicColumn("monthly", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                spdData2.RPT_AddBasicColumn("Monthly plan", 1, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                spdData2.RPT_AddBasicColumn("Monthly Plan Rev", 1, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                spdData2.RPT_AddBasicColumn("A/O", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData2.RPT_AddBasicColumn("SHIP", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData2.RPT_AddBasicColumn("Progress rate", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData2.RPT_MerageHeaderColumnSpan(0, 3, 5);
                
                spdData2.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData2.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData2.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData2.RPT_ColumnConfigFromTable(btnSort);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By ����
        /// </summary>
        private void SortInit()
        {

        }
        private void SortInit2()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "A.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MIN(FAC_SEQ)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "A.MAT_CMF_10", "A.MAT_CMF_10 AS PIN_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID AS PRODUCT", true);
        }
        #endregion


        #region SQL ���� Build
        /// <summary>
        /// 4. ���� ����
        /// </summary>
        /// <returns></returns>
        //�ش� ���� ��ǥ���� �����´�.
        private string MakeSqlStringForTarget()
        {
            Select_date = DateTime.Parse(cdvDate.Text.ToString());
            lastMonth = Select_date.AddMonths(-1).ToString("yyyyMM"); // ������
            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";

            // ���������� �������� ��������
            DataTable dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year, Select_date.ToString("yyyyMMdd")));
            string Lastweek_lastday = dt1.Rows[0][0].ToString();

            // ���� �������� ���ϱ�
            DataTable dt2 = null;
            last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + month + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", last_day);
            last_day = dt2.Rows[0][0].ToString();

            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("                        SELECT NVL(SUM(PLAN_QTY_ASSY),0) AS PLAN_QTY_ASSY, NVL(SUM(RESV_FIELD1),0) AS RESV_FIELD1  " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1 " + "\n"); // ��ο� : ���� ��ȹ ��(RESV_FIELD1)
            strSqlString.Append("                                  FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND PLAN_MONTH =  '" + month + "' " + "\n");
            strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");
            strSqlString.Append("                                UNION ALL " + "\n");
            strSqlString.Append("                                SELECT A.FACTORY, A.MAT_ID, 0 AS PLAN_QTY_ASSY , '" + month + "' AS PLAN_MONTH, SUM(A.PLAN_QTY) AS RESV_FIELD1 " + "\n");
            strSqlString.Append("                                  FROM ( " + "\n");
            // ����ȹ �����̸� �������
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                                        SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                          FROM CWIPPLNDAY " + "\n");
                strSqlString.Append("                                         WHERE 1=1 " + "\n");
                strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                           AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                strSqlString.Append("                                           AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                           AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                         GROUP BY FACTORY, MAT_ID " + "\n");
            }
            else// ������ �ƴϸ� ������ ������ ���̺��� ������.
            {
                strSqlString.Append("                                        SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                          FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                         WHERE 1=1 " + "\n");
                strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                           AND SNAPSHOT_DAY = '" + cdvDate.SelectedValue() + "'" + "\n");
                strSqlString.Append("                                           AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                strSqlString.Append("                                           AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                           AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                        GROUP BY FACTORY, MAT_ID " + "\n");
            }

            strSqlString.Append("                                        UNION ALL " + "\n");
            strSqlString.Append("                                        SELECT CM_KEY_1 AS FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                             , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
            strSqlString.Append("                                          FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                           AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("                                           AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                                           AND WORK_DATE BETWEEN '" + start_date + "' AND '" + Lastweek_lastday + "'\n");
            strSqlString.Append("                                         GROUP BY CM_KEY_1, MAT_ID " + "\n");
            strSqlString.Append("                                      ) A" + "\n");
            strSqlString.Append("                                     , MWIPMATDEF B " + "\n");
            strSqlString.Append("                                   WHERE 1=1  " + "\n");
            strSqlString.Append("                                     AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                                     AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                                     AND B.MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                     AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
            strSqlString.Append("                                  GROUP BY A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("                                ) A" + "\n");
            strSqlString.Append("                                , MWIPMATDEF B " + "\n");
            strSqlString.Append("                              WHERE 1=1  " + "\n");
            strSqlString.Append("                                AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                                AND A.MAT_ID = B.MAT_ID " + "\n");
            if (gubun.Text.Equals("TOTAL"))
            {
                strSqlString.Append("                                AND B.MAT_GRP_1 <> 'BR' " + "\n");
                strSqlString.Append("                                AND B.MAT_GRP_3 <> 'COB' " + "\n");
            }
            else if (gubun.Text.Equals("SE_MEMORY"))
            {
                strSqlString.Append("                                AND B.MAT_GRP_1 IN ('SE') " + "\n");
                strSqlString.Append("                                AND B.MAT_GRP_2 IN ('', '-', 'BOC', 'EPOXY', 'FBGA', 'LGA', 'LOC', 'NAND', 'QFN', 'SAWN', 'WF biz')  " + "\n");
                strSqlString.Append("                                AND B.MAT_GRP_3 IN ('-', 'BGN', 'BOC', 'DDP', 'FBGA', 'FLGA', 'LGA', 'MCP', 'QDP', 'QFN', 'SAWN', 'TSOP1', 'TSOP2')   " + "\n");
            }
            else if (gubun.Text.Equals("SE_S-LSI"))
            {
                strSqlString.Append("                                AND B.MAT_GRP_1 IN ('SE') " + "\n");
                strSqlString.Append("                                AND B.MAT_GRP_2 IN ('LSI')  " + "\n");
            }
            else if (gubun.Text.Equals("HX_MEMORY"))
            {
                strSqlString.Append("                                AND B.MAT_GRP_1 IN ('HX') " + "\n");
            }
            else if (gubun.Text.Equals("Fabless"))
            {
                strSqlString.Append("                                AND B.MAT_GRP_1 NOT IN ('SE','HX')  " + "\n");
            }

            //�� ��ȸ�� ���� SQL�� ���� 
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            return strSqlString.ToString();
        }
        // �ش���� ���귮�� �Ϻ��� �����ش�.
        private string MakeSqlStringForProduct()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append(" SELECT  " + "\n");
            for (int i = 1; i < lastDay + 1; i++)
            {
                if (i == 1)
                {
                    strSqlString.Append("        SUM(DECODE(WORK_DATE, '" + month + "'||'01', (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'F0000') THEN SHIP_QTY_1 ELSE END_QTY_1 END),0)) ASSY_END_QTY1  " + "\n");
                }
                else
                {
                    strSqlString.Append("      , SUM(DECODE(WORK_DATE, '" + month + "'||TRIM(TO_CHAR(" + i + ",'00')), (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'F0000') THEN SHIP_QTY_1 ELSE END_QTY_1 END),0)) ASSY_END_QTY" + i + "\n");
                }
            }
            strSqlString.Append("   FROM ( " + "\n");
            strSqlString.Append("         SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3 " + "\n");
            strSqlString.Append("              , SUM(END_LOT) AS END_LOT " + "\n");
            strSqlString.Append("              , SUM(END_QTY_1) AS END_QTY_1 " + "\n");
            strSqlString.Append("              , SUM(END_QTY_2) AS END_QTY_2 " + "\n");
            strSqlString.Append("              , SUM(SHIP_QTY_1) AS SHIP_QTY_1 " + "\n");
            strSqlString.Append("              , SUM(SHIP_QTY_2) AS SHIP_QTY_2 " + "\n");
            strSqlString.Append("           FROM ( " + "\n");
            strSqlString.Append("                 SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3 " + "\n");
            strSqlString.Append("                      , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_LOT+S2_OPER_IN_LOT+S3_OPER_IN_LOT),SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT)) END_LOT " + "\n");
            strSqlString.Append("                      , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1 " + "\n");
            strSqlString.Append("                      , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2),SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2+S1_END_RWK_QTY_2 + S2_END_RWK_QTY_2 + S3_END_RWK_QTY_2)) END_QTY_2 " + "\n");
            strSqlString.Append("                      , 0 SHIP_QTY_1 " + "\n");
            strSqlString.Append("                      , 0 SHIP_QTY_2 " + "\n");
            strSqlString.Append("                   FROM RSUMWIPMOV  " + "\n");
            strSqlString.Append("                  WHERE OPER NOT IN ('AZ010','TZ010','F0000','EZ010', 'F0000') " + "\n");
            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3 " + "\n");
            strSqlString.Append("                 UNION ALL " + "\n");
            strSqlString.Append("                 SELECT CM_KEY_1 AS FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                      , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010') OPER " + "\n");
            strSqlString.Append("                      , LOT_TYPE, MAT_VER,  WORK_DATE,CM_KEY_3 " + "\n");
            strSqlString.Append("                      , 0 END_LOT, 0 END_QTY_1, 0 END_QTY_2 " + "\n");
            strSqlString.Append("                      , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) SHIP_QTY_1 " + "\n");
            strSqlString.Append("                      , SUM(S1_FAC_OUT_QTY_2+S2_FAC_OUT_QTY_2+S3_FAC_OUT_QTY_2) SHIP_QTY_2 " + "\n");
            strSqlString.Append("                   FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                  WHERE FACTORY NOT IN ('RETURN') " + "\n");
            strSqlString.Append("                 GROUP BY CM_KEY_1, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3 " + "\n");
            strSqlString.Append("                ) " + "\n");
            strSqlString.Append("         GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3 " + "\n");
            strSqlString.Append("       ) A  " + "\n");
            strSqlString.Append("      , MWIPMATDEF B " + "\n");

            strSqlString.Append("  WHERE 1=1 " + "\n");
            strSqlString.Append("    AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("    AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("    AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("    AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("    AND B.MAT_VER = 1 " + "\n");
            strSqlString.Append("    AND B.MAT_TYPE = 'FG' " + "\n");


            if (cmbOper.Text.Equals("SAW"))
            {
                strSqlString.Append("    AND A.OPER IN ('A0170','A0180','A0200') " + "\n");
            }
            else if (cmbOper.Text.Equals("D/A"))
            {
                strSqlString.Append("    AND A.OPER IN ('A0310','A0400','A0401' , 'A0402' , 'A0403','A0404' , 'A0405' , 'A0406','A0407' , 'A0409' , 'A0408') " + "\n");
            }
            else if (cmbOper.Text.Equals("W/B"))
            {
                strSqlString.Append("    AND A.OPER IN ('A0600','A0601' , 'A0602' , 'A0603','A0604' , 'A0605' , 'A0606','A0607' , 'A0600' , 'A0608','A0609') " + "\n");
            }
            else if (cmbOper.Text.Equals("MOLD"))
            {
                strSqlString.Append("    AND A.OPER IN ('A0900','A1000') " + "\n");
            }
            else if (cmbOper.Text.Equals("FINISH"))
            {
                strSqlString.Append("    AND A.OPER IN ('AZ010') " + "\n");
            }

            strSqlString.Append("    AND A.OPER LIKE '%' " + "\n");
            strSqlString.Append("    AND A.MAT_ID LIKE '%' " + "\n");
            strSqlString.Append("    AND A.CM_KEY_3 LIKE '%' " + "\n");
            strSqlString.Append("    AND A.OPER NOT IN ('00001','00002') " + "\n");
            strSqlString.Append("    AND A.WORK_DATE BETWEEN '" + start_date + "' AND '" + last_day + "'\n");

            if (gubun.Text.Equals("TOTAL"))
            {
                strSqlString.Append("    AND B.MAT_GRP_1 <> 'BR' " + "\n");
                strSqlString.Append("    AND B.MAT_GRP_3 <> 'COB' " + "\n");
            }
            else if (gubun.Text.Equals("SE_MEMORY"))
            {
                strSqlString.Append("    AND B.MAT_GRP_1 IN ('SE') " + "\n");
                strSqlString.Append("    AND B.MAT_GRP_2 IN ('', '-', 'BOC', 'EPOXY', 'FBGA', 'LGA', 'LOC', 'NAND', 'QFN', 'SAWN', 'WF biz')  " + "\n");
                strSqlString.Append("    AND B.MAT_GRP_3 IN ('-', 'BGN', 'BOC', 'DDP', 'FBGA', 'FLGA', 'LGA', 'MCP', 'QDP', 'QFN', 'SAWN', 'TSOP1', 'TSOP2')   " + "\n");
            }
            else if (gubun.Text.Equals("SE_S-LSI"))
            {
                strSqlString.Append("    AND B.MAT_GRP_1 IN ('SE') " + "\n");
                strSqlString.Append("    AND B.MAT_GRP_2 IN ('LSI')  " + "\n");
            }
            else if (gubun.Text.Equals("HX_MEMORY"))
            {
                strSqlString.Append("    AND B.MAT_GRP_1 IN ('HX') " + "\n");
            }
            else if (gubun.Text.Equals("Fabless"))
            {
                strSqlString.Append("    AND B.MAT_GRP_1 NOT IN ('SE','HX')  " + "\n");
            }
            //�� ��ȸ�� ���� SQL�� ���� 
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            return strSqlString.ToString();
        }


        // ���� ������ �������� ��������
        private string MakeSqlString2(string year, string date)
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT MIN(SYS_DATE-1) " + "\n");
            sqlString.Append("  FROM MWIPCALDEF " + "\n");
            sqlString.Append(" WHERE 1=1" + "\n");
            sqlString.Append("   AND CALENDAR_ID='SE'" + "\n");
            sqlString.Append("   AND PLAN_YEAR='" + year + "'\n");
            sqlString.Append("   AND PLAN_WEEK=(" + "\n");
            sqlString.Append("                  SELECT PLAN_WEEK " + "\n");
            sqlString.Append("                    FROM MWIPCALDEF " + "\n");
            sqlString.Append("                   WHERE 1=1 " + "\n");
            sqlString.Append("                     AND CALENDAR_ID='SE' " + "\n");
            sqlString.Append("                     AND SYS_DATE=TO_CHAR(TO_DATE('" + date + "','YYYYMMDD'),'YYYYMMDD')" + "\n");
            sqlString.Append("                 )" + "\n");

            return sqlString.ToString();
        }

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            dtTarget = null;
            dtTarget = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForTarget());

            DataTable dtProduct = null;
            dtProduct = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForProduct());

            strSqlString.Append("SELECT '�ϸ�ǥ'" + "\n");
            for (int i = 0; i < lastDay; i++)
            {
                strSqlString.Append("      , TO_CHAR(ROUND(" + Convert.ToInt64(dtTarget.Rows[0][1].ToString()) / lastDay + "/1000))\n");
            }
            strSqlString.Append("  FROM DUAL " + "\n");

            strSqlString.Append("UNION ALL" + "\n");
            strSqlString.Append("SELECT '�����ϸ�ǥ'" + "\n");
            for (int i = 0; i < lastDay; i++)
            {
                if (i == 0)
                {
                    strSqlString.Append(",TO_CHAR(ROUND(" + Convert.ToInt64(dtTarget.Rows[0][1].ToString()) / lastDay + "/1000))\n");
                }
                else
                {
                    strSqlString.Append("      ,TO_CHAR(ROUND((" + Convert.ToInt64(dtTarget.Rows[0][1].ToString()) + "-(" + Convert.ToInt64(dtProduct.Rows[0][0].ToString()));
                    for (int k = 1; k < i + 1; k++)
                    {
                        strSqlString.Append("+" + Convert.ToInt64(dtProduct.Rows[0][k].ToString()));
                    }
                    strSqlString.Append("))/(" + lastDay + "-" + i + ")/1000))\n");
                }
            }
            strSqlString.Append("  FROM DUAL " + "\n");


            strSqlString.Append("UNION ALL" + "\n");
            strSqlString.Append("SELECT '���귮'" + "\n");
            for (int i = 0; i < lastDay; i++)
            {
                strSqlString.Append("      ,TO_CHAR(ROUND(" + Convert.ToInt64(dtProduct.Rows[0][i].ToString()) + "/1000))\n");
            }
            strSqlString.Append("  FROM DUAL " + "\n");

            strSqlString.Append("UNION ALL" + "\n");
            strSqlString.Append("SELECT '��������'" + "\n");
            for (int i = 0; i < lastDay; i++)
            {
                if (i == 0)
                {
                    strSqlString.Append(",TO_CHAR(ROUND((" + Convert.ToInt64(dtProduct.Rows[0][i].ToString()) + " - ROUND(" + Convert.ToInt64(dtTarget.Rows[0][1].ToString()) / lastDay + "))/1000))\n");
                }
                else
                {
                    strSqlString.Append(",TO_CHAR(ROUND((" + Convert.ToInt64(dtProduct.Rows[0][i].ToString()) + " - ROUND(" + Convert.ToInt64(dtTarget.Rows[0][1].ToString()) / lastDay + ")");
                    for (int k = 0; k < i; k++)
                    {
                        strSqlString.Append("+" + Convert.ToInt64(dtProduct.Rows[0][k].ToString()) + " - ROUND(" + Convert.ToInt64(dtTarget.Rows[0][1].ToString()) / lastDay + ")");
                    }
                    strSqlString.Append(")/1000))\n");
                }

            }
            strSqlString.Append("  FROM DUAL " + "\n");

            strSqlString.Append("UNION ALL" + "\n");
            strSqlString.Append("SELECT '������귮'" + "\n");
            for (int i = 0; i < lastDay; i++)
            {
                if (i == 0)
                {
                    strSqlString.Append(",TO_CHAR(ROUND(" + Convert.ToInt64(dtProduct.Rows[0][0].ToString()) + "/1000))\n");
                }
                else
                {
                    strSqlString.Append("      ,TO_CHAR(ROUND((" + Convert.ToInt64(dtProduct.Rows[0][0].ToString()));
                    for (int k = 1; k < i + 1; k++)
                    {
                        strSqlString.Append("+" + Convert.ToInt64(dtProduct.Rows[0][k].ToString()));
                    }
                    strSqlString.Append(")/1000))\n");
                }
            }
            strSqlString.Append("  FROM DUAL " + "\n");


            strSqlString.Append("UNION ALL" + "\n");
            strSqlString.Append("SELECT 'ǥ������'" + "\n");
            for (int i = 1; i < lastDay + 1; i++)
            {
                strSqlString.Append("      , ROUND((" + i + "/" + lastDay + " *100),2) || '%'\n");
            }
            strSqlString.Append("  FROM DUAL " + "\n");


            strSqlString.Append("UNION ALL" + "\n");
            strSqlString.Append("SELECT '������'" + "\n");
            int target = 0;
            if (dtTarget.Rows[0][1].ToString().Equals("0"))
            {
                target = 1;
            }
            else
            {
                target = Convert.ToInt32(dtTarget.Rows[0][1].ToString());
            }
            for (int i = 0; i < lastDay; i++)
            {
                if (i == 0)
                {
                    strSqlString.Append(",ROUND(" + Convert.ToInt64(dtProduct.Rows[0][0].ToString()) + "/" + target + "*100,2) || '%'\n");
                }
                else
                {
                    strSqlString.Append("      , ROUND((" + Convert.ToInt64(dtProduct.Rows[0][0].ToString()));
                    for (int k = 1; k < i + 1; k++)
                    {
                        strSqlString.Append("+" + Convert.ToInt64(dtProduct.Rows[0][k].ToString()));
                    }
                    strSqlString.Append(")/" + target + "*100,2)|| '%'\n");
                }
            }
            strSqlString.Append("  FROM DUAL " + "\n");


            strSqlString.Append("UNION ALL" + "\n");
            strSqlString.Append("SELECT 'ǥ�ش��'" + "\n");
            for (int i = 0; i < lastDay; i++)
            {
                if (i == 0)
                {
                    strSqlString.Append(",TO_CHAR(ROUND(" + Convert.ToInt64(dtProduct.Rows[0][0].ToString()) + "/" + target + "*100,2) \n");
                    strSqlString.Append("      - ROUND(( 1/" + lastDay + " *100),2),'90.00') || '%'\n");
                }
                else
                {
                    strSqlString.Append("     , TO_CHAR(ROUND((" + Convert.ToInt64(dtProduct.Rows[0][0].ToString()));
                    for (int k = 1; k < i + 1; k++)
                    {
                        strSqlString.Append("+" + Convert.ToInt64(dtProduct.Rows[0][k].ToString()));
                    }
                    strSqlString.Append(")/" + target + "*100,2) \n");
                    strSqlString.Append("      - ROUND(((" + i + "+1)/" + lastDay + " *100),2),'90.00') || '%'\n");
                }
            }
            strSqlString.Append("  FROM DUAL " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // ASSY ���� ����
        private string MakeSqlString3()
        {
            StringBuilder strSqlString = new StringBuilder();
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            string QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            string date = cdvDate.SelectedValue();
            string month = Select_date.ToString("yyyyMM");
            string start_date = month + "01";

            // ���� �������� ���ϱ�
            DataTable dt2 = null;
            string last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + month + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", last_day);
            last_day = dt2.Rows[0][0].ToString();

            // ���������� �������� ��������
            DataTable dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year, Select_date.ToString("yyyyMMdd")));
            string Lastweek_lastday = dt1.Rows[0][0].ToString();

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
            strSqlString.Append("     , ROUND(SUM(NVL(A.ORI_PLAN,0))/1000, 1) " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(A.MON_PLAN,0))/1000, 1) " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(A.ASSY_MON,0))/1000, 1)          " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_MON,0))/1000, 1) " + "\n");
            strSqlString.Append("     , ROUND(NVL(DECODE( SUM(A.MON_PLAN), 0, 0, ROUND((SUM(A.ASSY_MON)/SUM(A.MON_PLAN))*100, 1)),0),1) AS JINDO" + "\n");
            //strSqlString.Append("     , ROUND(( SUM(NVL(A.MON_PLAN,0)) - SUM(NVL(A.ASSY_MON,0)) )/1000, 1) AS LACK   " + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7, A.MAT_CMF_13" + "\n");
            strSqlString.Append("             , SUM(B.RESV_FIELD1) MON_PLAN" + "\n");
            strSqlString.Append("             , SUM(B.PLAN_QTY_ASSY) ORI_PLAN" + "\n");
            strSqlString.Append("             , SUM(C.ASSY_MON) ASSY_MON" + "\n");
            strSqlString.Append("             , SUM(D.SHIP_MON) SHIP_MON " + "\n");
            strSqlString.Append("          FROM MWIPMATDEF A " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH, RESV_FIELD1 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(RESV_FIELD1) AS RESV_FIELD1  " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1 " + "\n"); // ��ο� : ���� ��ȹ ��(RESV_FIELD1)
            strSqlString.Append("                                  FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");
            strSqlString.Append("                                UNION ALL " + "\n");
            strSqlString.Append("                                SELECT A.FACTORY, A.MAT_ID, 0 AS PLAN_QTY_ASSY , '" + month + "' AS PLAN_MONTH, SUM(A.PLAN_QTY) AS RESV_FIELD1 " + "\n");
            strSqlString.Append("                                  FROM ( " + "\n");
            // ����ȹ �����̸� �������
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                                        SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                          FROM CWIPPLNDAY " + "\n");
                strSqlString.Append("                                         WHERE 1=1 " + "\n");
                strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                           AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                strSqlString.Append("                                           AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                           AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                         GROUP BY FACTORY, MAT_ID " + "\n");
            }
            else// ������ �ƴϸ� ������ ������ ���̺��� ������.
            {
                strSqlString.Append("                                        SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                          FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                         WHERE 1=1 " + "\n");
                strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                           AND SNAPSHOT_DAY = '" + cdvDate.SelectedValue() + "'" + "\n");
                strSqlString.Append("                                           AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                strSqlString.Append("                                           AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                           AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                        GROUP BY FACTORY, MAT_ID " + "\n");
            }
            strSqlString.Append("                                        UNION ALL " + "\n");
            strSqlString.Append("                                        SELECT CM_KEY_1 AS FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                             , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
            strSqlString.Append("                                          FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                           AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("                                           AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                                           AND WORK_DATE BETWEEN '" + start_date + "' AND '" + Lastweek_lastday + "'\n");
            strSqlString.Append("                                         GROUP BY CM_KEY_1, MAT_ID " + "\n");
            strSqlString.Append("                               ) A" + "\n");
            strSqlString.Append("                             , MWIPMATDEF B " + "\n");
            strSqlString.Append("                         WHERE 1=1  " + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                           AND B.MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                           AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
            strSqlString.Append("                         GROUP BY A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("                     ) " + "\n");
            strSqlString.Append("               GROUP BY FACTORY, MAT_ID,PLAN_MONTH " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("               ) B " + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.MAT_ID, SUM(END_QTY_1) AS ASSY_MON");
            strSqlString.Append("                  FROM (");
            strSqlString.Append("                        SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3");
            strSqlString.Append("                             , SUM(END_QTY_1) AS END_QTY_1 ");
            strSqlString.Append("                          FROM (");
            strSqlString.Append("                                SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3 ");
            strSqlString.Append("                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1 ");
            strSqlString.Append("                                  FROM RSUMWIPMOV");
            strSqlString.Append("                                 WHERE OPER NOT IN ('AZ010','TZ010','F0000','EZ010', 'F0000')");
            strSqlString.Append("                                GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3");
            strSqlString.Append("                               )");
            strSqlString.Append("                        GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3 ");
            strSqlString.Append("                       ) A");
            strSqlString.Append("                       , MWIPMATDEF B");
            strSqlString.Append("                 WHERE 1=1");
            strSqlString.Append("                   AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID");
            strSqlString.Append("                   AND A.MAT_VER = 1");
            strSqlString.Append("                   AND B.MAT_VER = 1");
            strSqlString.Append("                   AND B.MAT_TYPE = 'FG'");
            strSqlString.Append("                   AND A.WORK_DATE BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
            if (cmbOper.Text.Equals("SAW"))
            {
                strSqlString.Append("                   AND A.OPER IN ('A0170','A0180','A0200') " + "\n");
            }
            else if (cmbOper.Text.Equals("D/A"))
            {
                strSqlString.Append("                   AND A.OPER IN ('A0310','A0400','A0401' , 'A0402' , 'A0403','A0404' , 'A0405' , 'A0406','A0407' , 'A0409' , 'A0408') " + "\n");
            }
            else if (cmbOper.Text.Equals("W/B"))
            {
                strSqlString.Append("                   AND A.OPER IN ('A0600','A0601' , 'A0602' , 'A0603','A0604' , 'A0605' , 'A0606','A0607' , 'A0600' , 'A0608','A0609') " + "\n");
            }
            else if (cmbOper.Text.Equals("MOLD"))
            {
                strSqlString.Append("                   AND A.OPER IN ('A0900','A1000') " + "\n");
            }
            else if (cmbOper.Text.Equals("FINISH"))
            {
                strSqlString.Append("                   AND A.OPER IN ('AZ010') " + "\n");
            }
            strSqlString.Append("                GROUP BY A.MAT_ID");
            strSqlString.Append("               ) C" + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID, SUM(SHIP_MON) SHIP_MON " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(NVL(SHP_QTY_1, 0)) AS SHIP_MON " + "\n");
            strSqlString.Append("                          FROM VSUMWIPSHP " + "\n");
            strSqlString.Append("                         WHERE 1 = 1 " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND CM_KEY_2 = 'PROD'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND WORK_DATE BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) D " + "\n");
            strSqlString.Append("         WHERE 1 = 1 " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND B.PLAN_MONTH(+) = '" + month + "' " + "\n");
            strSqlString.Append("           AND A.MAT_TYPE= 'FG' " + "\n");
            strSqlString.Append("           AND A.FACTORY =B.FACTORY(+) " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+)  " + "\n");
            strSqlString.Append("           AND A.MAT_ID = D.MAT_ID(+) " + "\n");
            if (gubun.Text.Equals("TOTAL"))
            {
                strSqlString.Append("    AND A.MAT_GRP_1 <> 'BR' " + "\n");
                strSqlString.Append("    AND A.MAT_GRP_3 <> 'COB' " + "\n");
            }
            else if (gubun.Text.Equals("SE_MEMORY"))
            {
                strSqlString.Append("    AND A.MAT_GRP_1 IN ('SE') " + "\n");
                strSqlString.Append("    AND A.MAT_GRP_2 IN ('', '-', 'BOC', 'EPOXY', 'FBGA', 'LGA', 'LOC', 'NAND', 'QFN', 'SAWN', 'WF biz')  " + "\n");
                strSqlString.Append("    AND A.MAT_GRP_3 IN ('-', 'BGN', 'BOC', 'DDP', 'FBGA', 'FLGA', 'LGA', 'MCP', 'QDP', 'QFN', 'SAWN', 'TSOP1', 'TSOP2')   " + "\n");
            }
            else if (gubun.Text.Equals("SE_S-LSI"))
            {
                strSqlString.Append("    AND A.MAT_GRP_1 IN ('SE') " + "\n");
                strSqlString.Append("    AND A.MAT_GRP_2 IN ('LSI')  " + "\n");
            }
            else if (gubun.Text.Equals("HX_MEMORY"))
            {
                strSqlString.Append("    AND A.MAT_GRP_1 IN ('HX') " + "\n");
            }
            else if (gubun.Text.Equals("Fabless"))
            {
                strSqlString.Append("    AND A.MAT_GRP_1 NOT IN ('SE','HX')  " + "\n");
            }
            //�� ��ȸ�� ���� SQL�� ���� 
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("         GROUP BY A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7, A.MAT_CMF_13" + "\n");
            strSqlString.Append("       ) A  " + "\n");
            strSqlString.Append("  GROUP BY A.MAT_GRP_1, A.MAT_CMF_10, A.MAT_ID" + "\n");
            strSqlString.Append("HAVING (" + "\n");
            strSqlString.Append("        NVL(SUM(A.MON_PLAN), 0)+  " + "\n");
            strSqlString.Append("        NVL(SUM(A.ORI_PLAN), 0)+  " + "\n");
            strSqlString.Append("        NVL(SUM(A.ASSY_MON), 0)   " + "\n");
            strSqlString.Append("       ) <> 0" + "\n");
            strSqlString.Append("  ORDER BY A.MAT_GRP_1, A.MAT_CMF_10, A.MAT_ID" + "\n");
            return strSqlString.ToString();

        }

        #endregion


        #region EVENT ó��
        /// <summary>
        /// 6. View ��ư Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dt2 = null;

            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;
            GridColumnInit2();
            spdData2_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString3());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                if (dt2.Rows.Count == 0)
                {
                    dt2.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;
                dt.Dispose();

                //�׷��� ���� 1������ SubTotal�� ����ϱ� ���ؼ� ù��° ���� ������
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                int[] rowType = spdData2.RPT_DataBindingWithSubTotal(dt2, 0, sub + 1, 3, null, null, btnSort);
                //Total�κ� ������
                spdData2.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 1, true, Align.Center, VerticalAlign.Center);


                //YIELD �κ���  TOTAL�� �� SUB TOTAL�� ������� �ʰ� ���� ��� 

                string subtotal = null;

                for (int i = 0; i < spdData2.ActiveSheet.Columns.Count; i++)
                {
                    if (spdData2.ActiveSheet.Columns[i].Label == "Progress rate")
                    {

                        if (Convert.ToInt32(spdData2.ActiveSheet.Cells[0, i - 3].Value) == 0 || Convert.ToInt32(spdData2.ActiveSheet.Cells[0, i - 2].Value) == 0)
                        {
                            spdData2.ActiveSheet.Cells[0, i].Value = 0;
                        }
                        else
                        {
                            spdData2.ActiveSheet.Cells[0, i].Value = ((Convert.ToDouble(spdData2.ActiveSheet.Cells[0, i - 2].Value) / Convert.ToDouble(spdData2.ActiveSheet.Cells[0, i - 3].Value)) * 100).ToString();
                        }

                        for (int j = 0; j < spdData2.ActiveSheet.Rows.Count; j++)
                        {
                            for (int k = 0; k < sub + 1; k++)
                            {
                                if (spdData2.ActiveSheet.Cells[j, k].Value != null)
                                    subtotal = spdData2.ActiveSheet.Cells[j, k].Value.ToString();

                                subtotal.Trim();
                                if (subtotal.Length > 5)
                                {
                                    if (subtotal.Substring(subtotal.Length - 5, 5) == "Total")
                                    {
                                        if (Convert.ToInt32(spdData2.ActiveSheet.Cells[j, i - 3].Value) == 0 || Convert.ToInt32(spdData2.ActiveSheet.Cells[j, i - 2].Value) == 0)
                                        {
                                            spdData2.ActiveSheet.Cells[j, i].Value = 0;
                                        }
                                        else
                                        {
                                            spdData2.ActiveSheet.Cells[j, i].Value = ((Convert.ToDouble(spdData2.ActiveSheet.Cells[j, i - 2].Value) / Convert.ToDouble(spdData2.ActiveSheet.Cells[j, i - 3].Value)) * 100).ToString();
                                        }
                                    }
                                } // if (subtotal.Length > 5)
                            } //for (int k = sub + 1; k > 0; k--)
                        } //for (int j = 0; j < spdData2.ActiveSheet.Rows.Count; j++)
                    }
                }
                dt2.Dispose();
                //spdData2.DataSource = dt2;
                
                LabelTextChange();
                //Chart ����
                if (spdData.ActiveSheet.RowCount > 0)
                    fnMakeChart(spdData, spdData.ActiveSheet.RowCount);
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

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS�� �����͸� Chart�� ǥ���Ѵ�.
             * 
             * Created By : min-woo kim(2011-10-20-�����)
             * 
             * Modified By : min-woo kim(2011-10-20-�����)
             ****************************************************/
            int[] ich_mm = new int[lastDay]; int[] icols_mm = new int[lastDay]; int[] irows_mm = new int[3]; string[] stitle_mm = new string[3];

            int icol = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                udcChartFX1.RPT_1_ChartInit();
                udcChartFX1.RPT_2_ClearData();
                udcChartFX1.AxisY.Font = new System.Drawing.Font("Tahoma", 10.25F);
                udcChartFX1.AxisX.Font = new System.Drawing.Font("Tahoma", 10.25F);
                udcChartFX1.AxisY.Title.Text = "Achievement rate";

                udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Lines;
                udcChartFX1.AxisY.DataFormat.Format = 0;
                udcChartFX1.AxisY.DataFormat.Decimals = 2;

                udcChartFX1.AxisX.Staggered = false;
                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                    ich_mm[icol] = icol + 1;
                    icols_mm[icol] = icol + 1;
                }

                irows_mm[0] = 1;
                stitle_mm[0] = SS.Sheets[0].Cells[1, 0].Text;

                irows_mm[1] = 3;
                stitle_mm[1] = SS.Sheets[0].Cells[3, 0].Text;

                irows_mm[2] = 2;
                stitle_mm[2] = SS.Sheets[0].Cells[2, 0].Text;

                udcChartFX1.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
                double max1 = udcChartFX1.RPT_4_AddData(SS, new int[] { 1, 3 }, icols_mm, SeriseType.Rows);
                double max2 = udcChartFX1.RPT_4_AddData(SS, new int[] { 2 }, icols_mm, SeriseType.Rows);
                udcChartFX1.RPT_5_CloseData();
                udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.05);
                udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 2, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.05);



                udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 1);
                udcChartFX1.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Top);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                StringBuilder Condition = new StringBuilder();
                Condition.AppendFormat("today: {0}     workday: {1}     remain: {2}      ǥ��������: {3}      �����ȹ: {4}" + "\n", lblToday.Text.ToString(), lblLastDay.Text.ToString(), lblRemain.Text.ToString(), lblJindo.Text.ToString(), monplan.Text);
                ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, Condition.ToString(), null);
            }
        }

        /// <summary>
        /// Factory ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
        }

        /// <summary>
        /// 7. ��� Lebel ǥ��
        /// </summary>
        private void LabelTextChange()
        {
            DateTime getStartDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");
            string getEndDate = getStartDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            string selectday = strDate.Substring(6, 2);
            string lastday = getEndDate.Substring(6, 2);
            int remain = 0;
            double jindo = (Convert.ToDouble(selectday)) / Convert.ToDouble(lastday) * 100;
            decimal jindoPer = Math.Round(Convert.ToDecimal(jindo), 1);

            lblToday.Text = selectday + " day";
            lblLastDay.Text = lastday + " day";

            // ���� ��ȸ�� ��� �ܿ��Ͽ� ���� ������.
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday) + 1);
            }
            else
            {
                remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday));
            }

            lblRemain.Text = remain.ToString() + " day";
            lblJindo.Text = jindoPer.ToString() + "%";

            //�����ȹ��
            if (dtTarget != null)
            {
                if (dtTarget.Rows[0][1].ToString().Length >= 1)
                {
                    monplan.Text = Convert.ToString(Convert.ToInt64(dtTarget.Rows[0][1]) / 1000);
                }
            }
        }
        #endregion

    }
}
