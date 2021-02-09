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

namespace Hana.CUS
{
    public partial class CUS060106 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: CUS060106<br/>
        /// 클래스요약: 고객사 공정별 실적<br/>
        /// 작  성  자: 하나마이크론 김준용<br/>
        /// 최초작성일: 2009-07-21<br/>
        /// 상세  설명: 고객사 공정별 실적<br/>
        /// 변경  내용: <br/>
        /// </summary>
        

        public CUS060106()
        {
            InitializeComponent();
            OptionInit();
            SortInit();
            GridColumnInit(); //해더 한줄짜리 샘플      
            cdvFromTo.AutoBinding(DateTime.Today.AddDays(-2).ToString("yyyy-MM-dd"), DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"));

            //this.udcWIPCondition2.Enabled = false;
            //this.udcWIPCondition3.Enabled = false;
            //this.udcWIPCondition4.Enabled = false;
            //this.udcWIPCondition5.Enabled = false;
            //this.udcWIPCondition6.Enabled = false;
            //this.udcWIPCondition7.Enabled = false;
            //this.udcWIPCondition8.Enabled = false;

            //관리자만 고객사 화면에서 CUSTOMER 별로 조회 가능
            //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            //{
            //    this.udcWIPCondition1.Enabled = true;
            //}          
            //else
            //{
            //    this.udcWIPCondition1.Enabled = false;            
            //}
        }

        /// <summary>
        /// 옵션 초기화
        /// </summary>
        private void OptionInit()
        {
            //this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            //cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                this.udcWIPCondition1.Enabled = true;
            }
            else
            {
                this.udcWIPCondition1.Enabled = false;
            }
        }


        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }          
            return true;
        }

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;

            string sFrom = cdvFromTo.ExactFromDate;
            string sTo = cdvFromTo.ExactToDate;
                        
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            strSqlString.AppendFormat("SELECT  {0} , " + "\n" , QueryCond1);
            strSqlString.Append("        HIS.CUSTOMER_LOT_ID, " + "\n");
            strSqlString.Append("        HIS.CREATE_TIME, " + "\n");
            strSqlString.Append("        SUM(HMK2) AS \"HMK2\", " + "\n");
            strSqlString.Append("        SUM(BG) AS \"BG\", " + "\n");
            strSqlString.Append("        SUM(SP) AS \"SP\", " + "\n");
            strSqlString.Append("        SUM(SAW) AS \"SAW\", " + "\n");
            strSqlString.Append("        SUM(DA) AS \"DA\", " + "\n");
            strSqlString.Append("        SUM(WB) AS \"WB\", " + "\n");
            strSqlString.Append("        SUM(MOLD) AS \"MOLD\", " + "\n");
            strSqlString.Append("        SUM(MK) AS \"MK\", " + "\n");
            strSqlString.Append("        SUM(TRIM) AS \"TRIM\", " + "\n");
            strSqlString.Append("        SUM(TIN) AS \"TIN\", " + "\n");
            strSqlString.Append("        SUM(SBA) AS \"SBA\", " + "\n");
            strSqlString.Append("        SUM(SIG) AS \"SIG\", " + "\n");
            strSqlString.Append("        SUM(AVI) AS \"AVI\", " + "\n");
            strSqlString.Append("        SUM(VI) AS \"VI\", " + "\n");
            strSqlString.Append("        SUM(HMK3A) AS \"HMK3A\" " + "\n");
            strSqlString.Append("FROM    ( ");
            strSqlString.Append("        SELECT HIS.OLD_FACTORY AS FACTORY, " + "\n");
            strSqlString.Append("               HIS.MAT_ID AS MAT_ID, " + "\n");
            strSqlString.Append("               HIS.LOT_CMF_4 AS CUSTOMER_LOT_ID, " + "\n");
            strSqlString.Append("               HIS.LOT_CMF_14 AS CREATE_TIME, " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A0000',SUM(QTY_1),0) AS \"HMK2\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A0040',SUM(QTY_1),0) AS \"BG\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A0360',SUM(QTY_1),0) AS \"SP\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A0200',SUM(QTY_1),0) AS \"SAW\", " + "\n");
            strSqlString.Append("               DECODE(SUBSTR(HIS.OLD_OPER,0,4),'A040',SUM(QTY_1),0) AS \"DA\", " + "\n");
            strSqlString.Append("               DECODE(SUBSTR(HIS.OLD_OPER,0,4),'A060',SUM(QTY_1),0) AS \"WB\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A1000',SUM(QTY_1),0) AS \"MOLD\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A1150',SUM(QTY_1),'A1500',SUM(QTY_1),0) AS \"MK\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A1900',SUM(QTY_1),0) AS \"TRIM\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A1450',SUM(QTY_1),0) AS \"TIN\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A1300',SUM(QTY_1),0) AS \"SBA\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A1750',SUM(QTY_1),0) AS \"SIG\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A2000',SUM(QTY_1),0) AS \"AVI\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'A2100',SUM(QTY_1),0) AS \"VI\", " + "\n");
            strSqlString.Append("               DECODE(HIS.OLD_OPER,'AZ010',SUM(QTY_1),0) AS \"HMK3A\" " + "\n");
            strSqlString.Append("        FROM   RWIPLOTHIS HIS, " + "\n");
            strSqlString.Append("               MWIPOPRDEF OPR " + "\n");
            strSqlString.Append("        WHERE  1 = 1     " + "\n");
            strSqlString.Append("               AND HIS.TRAN_TIME BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");
            strSqlString.Append("               AND OPR.FACTORY = HIS.OLD_FACTORY " + "\n");
            strSqlString.Append("               AND OPR.OPER = HIS.OLD_OPER " + "\n");
            strSqlString.Append("               AND HIS.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");

            if (GlobalVariable.gsUserGroup != "ADMIN_GROUP" && GlobalVariable.gsUserGroup != "HANA_ADMIN_GROUP" + "\n")
            {
                strSqlString.Append("               AND HIS.MAT_ID LIKE '" + GlobalVariable.gsCustomer + "%' " + "\n");
            }

            strSqlString.Append("               AND HIS.TRAN_CODE IN ('END','SHIP') " + "\n");
            strSqlString.Append("               AND CREATE_CODE NOT IN ('RETN') " + "\n");
            strSqlString.Append("               AND HIS.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("               AND HIS.HIST_DEL_FLAG = ' ' " + "\n");

            if (txtLotID.Text != "%" && txtLotID.Text != "")
            {
                strSqlString.Append("               AND HIS.LOT_CMF_4 LIKE '" + txtLotID.Text + "' " + "\n" + "\n");
            }

            strSqlString.Append("         GROUP BY HIS.OLD_FACTORY,HIS.MAT_ID,HIS.LOT_CMF_4,HIS.LOT_CMF_14,HIS.OLD_OPER " + "\n");
            strSqlString.Append("        ) HIS, " + "\n");
            strSqlString.Append("        MWIPMATDEF MAT " + "\n");
            strSqlString.Append("WHERE   1=1 " + "\n");
            strSqlString.Append("        AND HIS.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("        AND HIS.MAT_ID = MAT.MAT_ID " + "\n");
           
            //상세 조회에 따른 SQL문 생성
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("    AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            }

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.AppendFormat(" GROUP BY {0},HIS.CUSTOMER_LOT_ID,HIS.CREATE_TIME " + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY {0},HIS.CUSTOMER_LOT_ID, HIS.CREATE_TIME " + "\n", QueryCond1);
            
            return strSqlString.ToString();

        }

        private String MakeSqlStringTest()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;

            string sFrom = cdvFromTo.ExactFromDate;
            string sTo = cdvFromTo.ExactToDate;
                        
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            strSqlString.AppendFormat("SELECT  {0} , " + "\n", QueryCond1);
            strSqlString.Append("        HIS.CUSTOMER_LOT_ID, " + "\n");
            strSqlString.Append("        HIS.CREATE_TIME," + "\n");
            strSqlString.Append("        SUM(TEST_IN_QTY) AS TEST_IN_QTY," + "\n");
            strSqlString.Append("        DECODE(SUM(TEST_QTY),0,0,ROUND((SUM(GOOD_QTY)/SUM(TEST_QTY))*100,2)) AS YIELD," + "\n");
            strSqlString.Append("        SUM(TEST_QTY) AS TEST_QTY," + "\n");
            strSqlString.Append("        SUM(GOOD_QTY) AS GOOD_QTY," + "\n");
            strSqlString.Append("        (SELECT SUM(LOSS_QTY) FROM RWIPLOTLSM WHERE LOT_ID IN" + "\n");
            strSqlString.Append("        ( SELECT LOT_ID FROM RWIPLOTSTS WHERE LOT_CMF_4=HIS.CUSTOMER_LOT_ID )" + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("        AND OPER IN ('T0100')" + "\n");
            strSqlString.Append("        ) AS FAIL_QTY," + "\n");
            strSqlString.Append("        (SELECT SUM(LOSS_QTY) FROM RWIPLOTLSM WHERE LOT_ID IN" + "\n");
		    strSqlString.Append("        ( SELECT LOT_ID FROM RWIPLOTSTS WHERE LOT_CMF_4=HIS.CUSTOMER_LOT_ID )" + "\n");
		    strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("        AND OPER NOT IN ('T0100')" + "\n");
		    strSqlString.Append("        ) AS LOSS_QTY, " + "\n");
            strSqlString.Append("        SUM(HMK3T)," + "\n");
		    strSqlString.Append("        SUM(TEST)," + "\n");
		    strSqlString.Append("        SUM(QA2)," + "\n");
		    strSqlString.Append("        SUM(BAKE)," + "\n");
		    strSqlString.Append("        SUM(VI)," + "\n");
		    strSqlString.Append("        SUM(TR)," + "\n");
		    strSqlString.Append("        SUM(PK)," + "\n");
		    strSqlString.Append("        SUM(HMK4T)" + "\n");
            strSqlString.Append("        FROM    (" +  "\n");
            strSqlString.Append("                SELECT  FACTORY," +  "\n");
            strSqlString.Append("                        MAT_ID," +  "\n");
            strSqlString.Append("                        CUSTOMER_LOT_ID," +  "\n");
            strSqlString.Append("                        CREATE_TIME," +  "\n");
            strSqlString.Append("                        LOT_ID," +  "\n");
            strSqlString.Append("                        SUM(TEST_QTY) AS TEST_QTY," +  "\n");
            strSqlString.Append("                        SUM(GOOD_QTY) AS GOOD_QTY" +  "\n");
            strSqlString.Append("                FROM    (" +  "\n");
            strSqlString.Append("                        SELECT HIS.FACTORY AS FACTORY," +  "\n");
            strSqlString.Append("                           HIS.MAT_ID AS MAT_ID," +  "\n");
            strSqlString.Append("                           (SELECT LOT_CMF_4 FROM RWIPLOTSTS WHERE LOT_ID=HIS.LOT_ID) AS CUSTOMER_LOT_ID, " +  "\n");
            strSqlString.Append("                           (SELECT LOT_CMF_14 FROM RWIPLOTSTS WHERE LOT_ID=HIS.LOT_ID) AS CREATE_TIME," +  "\n");
            strSqlString.Append("                           HIS.OPER," +  "\n");
            strSqlString.Append("                           HIS.LOT_ID," +  "\n");
            strSqlString.Append("                           DECODE(HIS.OPER,'T0100',SUM(HIS.OPER_IN_QTY_1)) AS TEST_QTY," +  "\n");
            strSqlString.Append("                           DECODE(HIS.OPER,'T0100',SUM(HIS.END_QTY_1),0) AS GOOD_QTY" +  "\n");
            strSqlString.Append("                        FROM   RSUMWIPLTH HIS" +  "\n");
            strSqlString.Append("                        WHERE  1 = 1 " +  "\n");
            strSqlString.Append("                               AND HIS.OPER_IN_TIME BETWEEN '" + sFrom + "' AND '" + sTo + "'" +  "\n");
            strSqlString.Append("                               AND HIS.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " +  "\n");
            strSqlString.Append("                               AND HIS.OPER IN ('T0000','T0100')" +  "\n");
            strSqlString.Append("                               AND HIS.LOT_TYPE = 'W'" + "\n");
            if (txtLotID.Text != "%" && txtLotID.Text != "")
            {
                strSqlString.Append("                               AND HIS.LOT_ID IN ( SELECT LOT_ID FROM RWIPLOTSTS WHERE LOT_CMF_4 LIKE '" + txtLotID.Text + "')" + "\n");
            }
            strSqlString.Append("                         GROUP BY HIS.FACTORY,HIS.MAT_ID,HIS.LOT_ID,HIS.OPER" +  "\n");
            strSqlString.Append("                        )" +  "\n");
            strSqlString.Append("                GROUP BY FACTORY,MAT_ID,CUSTOMER_LOT_ID,CREATE_TIME,LOT_ID" +  "\n");
            strSqlString.Append("                ) HIS," +  "\n");
            strSqlString.Append("                (" +  "\n");
            strSqlString.Append("                SELECT LOT_ID," +  "\n");
            strSqlString.Append("                       DECODE(OPER,'T0000',SUM(QTY_1)) AS \"HMK3T\"," +  "\n");
            strSqlString.Append("                       DECODE(OPER,'T0090',SUM(QTY_1),'T0100',SUM(QTY_1),'T0200',SUM(QTY_1),'T0500',SUM(QTY_1),'T0550',SUM(QTY_1),'T0600',SUM(QTY_1),'T0700',SUM(QTY_1),'T0800',SUM(QTY_1)) AS \"TEST\"," +  "\n");
            strSqlString.Append("                       DECODE(OPER,'T1040',SUM(QTY_1)) AS \"QA2\"," +  "\n");
            strSqlString.Append("                       DECODE(OPER,'T1080',SUM(QTY_1)) AS \"BAKE\"," +  "\n");
            strSqlString.Append("                       DECODE(OPER,'T1100',SUM(QTY_1)) AS \"VI\"," +  "\n");
            strSqlString.Append("                       DECODE(OPER,'T0540',SUM(QTY_1),'T1200',SUM(QTY_1)) AS \"TR\"," +  "\n");
            strSqlString.Append("                       DECODE(OPER,'T1300',SUM(QTY_1)) AS \"PK\"," +  "\n");
            strSqlString.Append("                       DECODE(OPER,'TZ010',SUM(QTY_1)) AS \"HMK4T\"" +  "\n");
            strSqlString.Append("                       FROM    RWIPLOTSTS" +  "\n");
            strSqlString.Append("                       WHERE   1=1" +  "\n");
            strSqlString.Append("                               AND FACTORY='" + GlobalVariable.gsTestDefaultFactory + "'" +  "\n");
            if (txtLotID.Text != "%" && txtLotID.Text != "")
            {
                strSqlString.Append("                               AND LOT_ID IN ( SELECT LOT_ID FROM RWIPLOTSTS WHERE LOT_CMF_4 LIKE '" + txtLotID.Text + "' )" + "\n");
            }
            strSqlString.Append("                       GROUP BY LOT_ID,OPER" +  "\n");
            strSqlString.Append("                ) STS," +  "\n");
            strSqlString.Append("                (" +  "\n");
            strSqlString.Append("                            SELECT  LOT_ID," +  "\n");
            strSqlString.Append("                                    SUM(OPER_IN_QTY_1) AS TEST_IN_QTY" +  "\n");
            strSqlString.Append("                            FROM    RWIPLOTHIS" +  "\n");
            strSqlString.Append("                            WHERE   1=1" +  "\n");
            strSqlString.Append("                                    AND FACTORY='" + GlobalVariable.gsTestDefaultFactory + "'" +  "\n");
            strSqlString.Append("                                    AND OPER = 'T000N'" +  "\n");
            strSqlString.Append("                                    AND HIST_DEL_FLAG=' '" +  "\n");
            if (txtLotID.Text != "%" && txtLotID.Text != "")
            {
                strSqlString.Append("                                    AND LOT_ID IN ( SELECT LOT_ID FROM RWIPLOTSTS WHERE LOT_CMF_4 LIKE '" + txtLotID.Text + "' )" + "\n");
            }
            strSqlString.Append("                            GROUP BY LOT_ID" +  "\n");
            strSqlString.Append("                ) MOV," + "\n");
            strSqlString.Append("                MWIPMATDEF MAT" +  "\n");
            strSqlString.Append("                WHERE   1=1" +  "\n");
            strSqlString.Append("                        AND HIS.FACTORY = MAT.FACTORY" +  "\n");
            strSqlString.Append("                        AND HIS.MAT_ID = MAT.MAT_ID" +  "\n");
            strSqlString.Append("                        AND HIS.LOT_ID = STS.LOT_ID" + "\n");
            strSqlString.Append("                        AND HIS.LOT_ID = MOV.LOT_ID" + "\n");

            if (GlobalVariable.gsUserGroup != "ADMIN_GROUP" && GlobalVariable.gsUserGroup != "HANA_ADMIN_GROUP" + "\n")
            {
                strSqlString.Append("               AND HIS.MAT_ID LIKE '" + GlobalVariable.gsCustomer + "%' " + "\n");
            }

            //상세 조회에 따른 SQL문 생성
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("    AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            }

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            strSqlString.AppendFormat(" GROUP BY {0},HIS.CUSTOMER_LOT_ID,HIS.CREATE_TIME " + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY {0},HIS.CUSTOMER_LOT_ID,HIS.CREATE_TIME " + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private void Sample02_Load(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (CheckField() == false)
                return;

            DataTable dt = null;
            GridColumnInit();

            try
            {
                spdData_Sheet1.RowCount = 0;
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);

                if(cdvFactory.Text.Equals(GlobalVariable.gsAssyDefaultFactory)) dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                else if(cdvFactory.Text.Equals(GlobalVariable.gsTestDefaultFactory)) dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringTest());


                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 11, null, null, btnSort);
                // 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함


                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
            spdData.ExportExcel();
        }


        //한줄짜리 해더 샘플
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Lot ID", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("CREATE TIME", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);

            if (cdvFactory.Text.Equals(GlobalVariable.gsAssyDefaultFactory))
            {   
                spdData.RPT_AddBasicColumn("HMK2", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("B/G", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("S/P", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SAW", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("MOLD", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("M/K", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("T/F", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TIN", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("S/B/A", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SIG", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("AVI", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("VI", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HMK3A", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            }
            else if (cdvFactory.Text.Equals(GlobalVariable.gsTestDefaultFactory))
            {
                spdData.RPT_AddBasicColumn("TEST IN", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("YIELD", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                spdData.RPT_AddBasicColumn("TEST", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("GOOD", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("FAIL", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("LOSS", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HMK3T", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TEST", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("QA2", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("BAKE", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("V/I", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("T/R", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("P/K", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HMK4T", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            }
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            
        }


        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4",  false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8",  false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_CMF_7", true);
        }              

    }
}

