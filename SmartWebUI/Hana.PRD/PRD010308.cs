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
    public partial class PRD010308 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        /// <summary>
        /// 클  래  스: PRD010308<br/>
        /// 클래스요약: 정체 재공 현황<br/>
        /// 작  성  자: 하나마이크론 김민우<br/>
        /// 최초작성일: 2011-05-03<br/>
        /// 상세  설명: 정체일을 선택하여 정체 재공을 구분하여 조회<br/>
        /// 변경  내용: <br/>
        /// </summary>
        
        DataTable dtDay = null;
        String[] stDay;
        string todate;


        public PRD010308()
        {
            InitializeComponent();
            cdvDate.Value = DateTime.Today;
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvFromOper.sFactory = GlobalVariable.gsAssyDefaultFactory;
            //this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            //cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

        }

        private Boolean CheckField()
        {

            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        private void GridColumnInit()
        {

            string squery = "";
            

            todate = cdvDate.Value.ToString("yyyyMMdd");

            squery = "SELECT TO_CHAR(TO_DATE('" + todate + "'),'YYYYMMDD') AS DD \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-1,'YYYYMMDD') AS DD1 \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-2,'YYYYMMDD') AS DD2 \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-3,'YYYYMMDD') AS DD3 \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-4,'YYYYMMDD') AS DD4 \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-5,'YYYYMMDD') AS DD5 \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-6,'YYYYMMDD') AS DD6 \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-7,'YYYYMMDD') AS DD7 \n"
                               + "	 FROM DUAL";
            dtDay = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery.Replace((char)Keys.Tab, ' '));

            stDay = new String[] { dtDay.Rows[0][0].ToString() + "22", dtDay.Rows[0][1].ToString() + "22", dtDay.Rows[0][2].ToString() + "22", dtDay.Rows[0][3].ToString() + "22", dtDay.Rows[0][4].ToString() + "22", dtDay.Rows[0][5].ToString() + "22", dtDay.Rows[0][6].ToString() + "22" };

            spdData.RPT_ColumnInit();
            //GetDayList();
            spdData.RPT_AddBasicColumn("STEP", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("구분", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.ActiveSheet.Columns[1].Tag = "구분";
            spdData.RPT_AddBasicColumn(dtDay.Rows[0][6].ToString(), 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 100);
            spdData.RPT_AddBasicColumn(dtDay.Rows[0][5].ToString(), 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 100);
            spdData.RPT_AddBasicColumn(dtDay.Rows[0][4].ToString(), 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 100);
            spdData.RPT_AddBasicColumn(dtDay.Rows[0][3].ToString(), 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 100);
            spdData.RPT_AddBasicColumn(dtDay.Rows[0][2].ToString(), 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 100);
            spdData.RPT_AddBasicColumn(dtDay.Rows[0][1].ToString(), 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 100);
            spdData.RPT_AddBasicColumn(dtDay.Rows[0][0].ToString(), 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 100);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

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
                               
                // 첫 컬럼 색상 지정
                spdData.ActiveSheet.Columns[0].BackColor = Color.Bisque;

              
                if(spdData.ActiveSheet.RowCount > 0)
                    ShowChart(0);
                
                dt.Dispose();

            }
            catch(Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();


            if (cdvFactory.Text.Trim().Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                strSqlString.Append("SELECT STEP, GUBUN" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][6].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK2A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY,'WAFER 공급',QTY)" + "\n");
                strSqlString.Append("                                       ,'SAW',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'QC_GATE2',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'D/A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'W/B',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'MOLD',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'FINISH',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS A" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][5].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK2A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY,'WAFER 공급',QTY)" + "\n");
                strSqlString.Append("                                       ,'SAW',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'QC_GATE2',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'D/A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'W/B',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'MOLD',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'FINISH',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS B" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][4].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK2A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY,'WAFER 공급',QTY)" + "\n");
                strSqlString.Append("                                       ,'SAW',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'QC_GATE2',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'D/A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'W/B',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'MOLD',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'FINISH',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS C" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][3].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK2A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY,'WAFER 공급',QTY)" + "\n");
                strSqlString.Append("                                       ,'SAW',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'QC_GATE2',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'D/A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'W/B',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'MOLD',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'FINISH',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS D" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][2].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK2A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY,'WAFER 공급',QTY)" + "\n");
                strSqlString.Append("                                       ,'SAW',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'QC_GATE2',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'D/A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'W/B',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'MOLD',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'FINISH',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS E" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][1].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK2A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY,'WAFER 공급',QTY)" + "\n");
                strSqlString.Append("                                       ,'SAW',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'QC_GATE2',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'D/A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'W/B',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'MOLD',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'FINISH',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS F" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][0].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK2A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY,'WAFER 공급',QTY)" + "\n");
                strSqlString.Append("                                       ,'SAW',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'QC_GATE2',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'D/A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'W/B',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'MOLD',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'FINISH',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3A',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS G" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT SUBSTR(CUTOFF_DT,0,8) AS WORK_DATE" + "\n");
                strSqlString.Append("             , STEP" + "\n");
                strSqlString.Append("             , GUBUN" + "\n");

                if (chkKpcs.Checked == true)
                {
                    strSqlString.Append("             , QTY/1000 AS QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("             , QTY" + "\n");
                }
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT CUTOFF_DT" + "\n");
                strSqlString.Append("                     , CASE OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A' WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPR.OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n");
                strSqlString.Append("                                       WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE'" + "\n");
                strSqlString.Append("                                       WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n");
                strSqlString.Append("                                       ELSE 'FINISH'" + "\n");
                strSqlString.Append("                       END AS STEP" + "\n");
                strSqlString.Append("                     , DECODE(CUTOFF_DT,'" + stDay[1] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][1].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                       ,'" + stDay[2] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][2].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                       ,'" + stDay[3] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][3].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                       ,'" + stDay[4] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][4].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                       ,'" + stDay[5] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][5].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                       ,'" + stDay[6] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][6].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                if (!DateTime.Now.ToString("yyyyMMdd").Equals(todate))
                {
                    strSqlString.Append("                                       ,'" + stDay[0] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][0].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                }
                strSqlString.Append("                       ) AS GUBUN" + "\n");
                strSqlString.Append("                     , SUM(LOT.QTY_1) AS QTY" + "\n");
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH LOT" + "\n");
                strSqlString.Append("                     , MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("                 WHERE  1=1" + "\n");
                strSqlString.Append("                   AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND LOT.FACTORY = OPR.FACTORY" + "\n");
                strSqlString.Append("                   AND LOT.OPER = OPR.OPER" + "\n");
                strSqlString.Append("                   AND LOT.OWNER_CODE = 'PROD'" + "\n");
                strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND LOT.MAT_VER = 1 " + "\n");
                strSqlString.Append("                   AND LOT.LOT_TYPE = 'W'" + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("                   AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

                if (cmbProduct.Text.Trim() != "ALL")
                    strSqlString.AppendFormat("                   AND LOT.LOT_CMF_5 LIKE '{0}'" + "\n", cmbProduct.Text);


                strSqlString.Append("                   AND LOT.OPER NOT IN ('00001','00002')" + "\n");
                strSqlString.Append("                   AND CUTOFF_DT IN ('" + stDay[6] + "','" + stDay[5] + "','" + stDay[4] + "','" + stDay[3] + "','" + stDay[2] + "','" + stDay[1] + "','" + stDay[0] + "')" + "\n");
                strSqlString.Append("                 GROUP BY CASE OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A' WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPR.OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n");
                strSqlString.Append("                                          WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE'" + "\n");
                strSqlString.Append("                                          WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n");
                strSqlString.Append("                                          ELSE 'FINISH'" + "\n");
                strSqlString.Append("                          END," + "\n");
                strSqlString.Append("                          CUTOFF_DT" + "\n");
                strSqlString.Append("                        , DECODE(CUTOFF_DT,'" + stDay[1] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][1].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                          ,'" + stDay[2] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][2].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                          ,'" + stDay[3] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][3].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                          ,'" + stDay[4] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][4].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                          ,'" + stDay[5] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][5].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                          ,'" + stDay[6] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][6].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                if (!DateTime.Now.ToString("yyyyMMdd").Equals(todate))
                {
                    strSqlString.Append("                                       ,'" + stDay[0] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][0].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                }
                strSqlString.Append("                                 )" + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("        UNION ALL" + "\n");
                strSqlString.Append("        SELECT SUBSTR(CUTOFF_DT,0,8) AS WORK_DATE" + "\n");
                strSqlString.Append("             , 'TOTAL' AS STEP" + "\n");
                strSqlString.Append("             , DECODE(CUTOFF_DT,'" + stDay[1] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][1].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                               ,'" + stDay[2] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][2].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                               ,'" + stDay[3] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][3].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                               ,'" + stDay[4] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][4].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                               ,'" + stDay[5] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][5].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                               ,'" + stDay[6] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][6].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                if (!DateTime.Now.ToString("yyyyMMdd").Equals(todate))
                {
                    strSqlString.Append("                               ,'" + stDay[0] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][0].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                }
                strSqlString.Append("               ) AS GUBUN" + "\n");
                if (chkKpcs.Checked == true)
                {
                    strSqlString.Append("             , SUM(LOT.QTY_1)/1000 AS QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("             , SUM(LOT.QTY_1) AS QTY" + "\n");
                }
                strSqlString.Append("          FROM RWIPLOTSTS_BOH LOT" + "\n");
                strSqlString.Append("             , MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("         WHERE  1=1" + "\n");
                strSqlString.Append("           AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND LOT.FACTORY = OPR.FACTORY " + "\n");
                strSqlString.Append("           AND LOT.OPER = OPR.OPER" + "\n");
                strSqlString.Append("           AND LOT.OWNER_CODE = 'PROD'" + "\n");
                strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("           AND LOT.MAT_VER = 1" + "\n");
                strSqlString.Append("           AND LOT.LOT_TYPE = 'W'" + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("           AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                if (cmbProduct.Text.Trim() != "ALL")
                    strSqlString.AppendFormat("                   AND LOT.LOT_CMF_5 LIKE '{0}'" + "\n", cmbProduct.Text);
                strSqlString.Append("           AND LOT.OPER NOT IN ('00001','00002')" + "\n");
                strSqlString.Append("           AND CUTOFF_DT IN ('" + stDay[6] + "','" + stDay[5] + "','" + stDay[4] + "','" + stDay[3] + "','" + stDay[2] + "','" + stDay[1] + "','" + stDay[0] + "')" + "\n");
                strSqlString.Append("         GROUP BY CUTOFF_DT" + "\n");
                strSqlString.Append("                , DECODE(CUTOFF_DT,'" + stDay[1] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][1].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                  ,'" + stDay[2] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][2].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                  ,'" + stDay[3] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][3].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                  ,'" + stDay[4] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][4].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                  ,'" + stDay[5] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][5].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                  ,'" + stDay[6] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][6].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                if (!DateTime.Now.ToString("yyyyMMdd").Equals(todate))
                {
                    strSqlString.Append("                                  ,'" + stDay[0] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][0].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                }
                strSqlString.Append("                         )" + "\n");
                strSqlString.Append("        UNION ALL" + "\n");
                strSqlString.Append("        SELECT WORK_DATE" + "\n");
                strSqlString.Append("             , STEP" + "\n");
                strSqlString.Append("             , 'OUT'" + "\n");
                if (chkKpcs.Checked == true)
                {
                    strSqlString.Append("             , SUM(QTY)/1000 AS QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("             , SUM(QTY) AS QTY" + "\n");
                }
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                 SELECT WORK_DATE" + "\n");
                strSqlString.Append("                      , CASE OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A' WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPR.OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n");
                strSqlString.Append("                                        WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE'" + "\n");
                strSqlString.Append("                                        WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n");
                strSqlString.Append("                                        ELSE 'FINISH'" + "\n");
                strSqlString.Append("                        END AS STEP" + "\n");
                strSqlString.Append("                      , DECODE(MOV.OPER,'AZ010', SUM(S1_MOVE_QTY_1+S2_MOVE_QTY_1+S3_MOVE_QTY_1+S4_MOVE_QTY_1)" + "\n");
                strSqlString.Append("                                       ,'AZ009', SUM(S1_MOVE_QTY_1+S2_MOVE_QTY_1+S3_MOVE_QTY_1+S4_MOVE_QTY_1)" + "\n");
                strSqlString.Append("                                       , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S4_END_QTY_1)) AS QTY" + "\n");
                strSqlString.Append("                          FROM RSUMWIPMOV MOV" + "\n");
                strSqlString.Append("                             , MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("                         WHERE  1=1" + "\n");
                strSqlString.Append("                           AND MOV.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                           AND MOV.FACTORY = OPR.FACTORY" + "\n");
                strSqlString.Append("                           AND MOV.OPER = OPR.OPER" + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("   AND MOV.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                if (cmbProduct.Text.Trim() != "ALL")
                    strSqlString.AppendFormat("                   AND MOV.CM_KEY_3 LIKE '{0}'" + "\n", cmbProduct.Text);
                strSqlString.Append("                           AND WORK_DATE BETWEEN '" + dtDay.Rows[0][6].ToString() + "' AND '" + dtDay.Rows[0][0].ToString() + "'" + "\n");
                strSqlString.Append("                         GROUP BY WORK_DATE" + "\n");
                strSqlString.Append("                                , CASE OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A' WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPR.OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n");
                strSqlString.Append("                                                  WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE'" + "\n");
                strSqlString.Append("                                                  WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n");
                strSqlString.Append("                                                  ELSE 'FINISH'" + "\n");
                strSqlString.Append("                                  END" + "\n");
                strSqlString.Append("                                , MOV.OPER" + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         WHERE STEP <> 'HMK2A'" + "\n");
                strSqlString.Append("         GROUP BY WORK_DATE, STEP" + "\n");
                strSqlString.Append("        UNION ALL" + "\n");
                strSqlString.Append("        SELECT WORK_DATE" + "\n");
                strSqlString.Append("             , 'HMK2A' AS STEP" + "\n");
                strSqlString.Append("             , 'WAFER 공급' AS GUBUN" + "\n");
                if (chkKpcs.Checked == true)
                {
                    strSqlString.Append("             , SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2+S4_OPER_IN_QTY_2)/1000 AS QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("             , SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2+S4_OPER_IN_QTY_2) AS QTY" + "\n");
                }
                strSqlString.Append("          FROM RSUMWIPMOV" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND WORK_DATE BETWEEN '" + dtDay.Rows[0][6].ToString() + "' AND '" + dtDay.Rows[0][0].ToString() + "'" + "\n");
                strSqlString.Append("           AND OPER = 'A0000' " + "\n");
                strSqlString.Append("        GROUP BY WORK_DATE" + "\n");
              
                if (DateTime.Now.ToString("yyyyMMdd").Equals(todate))
                {
                    strSqlString.Append("        UNION ALL" + "\n");
                    strSqlString.Append("        SELECT '" + dtDay.Rows[0][0].ToString() + "' AS WORK_DATE" + "\n");
                    strSqlString.Append("             , CASE OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A' WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPR.OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n");
                    strSqlString.Append("                               WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE'" + "\n");
                    strSqlString.Append("                               WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n");
                    strSqlString.Append("                               ELSE 'FINISH'" + "\n");
                    strSqlString.Append("               END AS STEP" + "\n");
                    strSqlString.Append("             , DECODE(SIGN((24*" + txtHoldDate.Text + "-((SYSDATE - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체') AS GUBUN" + "\n");
                    strSqlString.Append("             , SUM(LOT.QTY_1) AS QTY" + "\n");
                    strSqlString.Append("          FROM RWIPLOTSTS LOT" + "\n");
                    strSqlString.Append("             , MWIPOPRDEF OPR" + "\n");
                    strSqlString.Append("         WHERE  1=1" + "\n");
                    strSqlString.Append("           AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("           AND LOT.FACTORY = OPR.FACTORY" + "\n");
                    strSqlString.Append("           AND LOT.OPER = OPR.OPER" + "\n");
                    strSqlString.Append("           AND LOT.OWNER_CODE = 'PROD'" + "\n");
                    strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("           AND LOT.MAT_VER = 1" + "\n");
                    strSqlString.Append("           AND LOT.LOT_TYPE = 'W'" + "\n");
                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("   AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                    if (cmbProduct.Text.Trim() != "ALL")
                        strSqlString.AppendFormat("                   AND LOT.LOT_CMF_5 LIKE '{0}'" + "\n", cmbProduct.Text);
                    strSqlString.Append("           AND LOT.OPER NOT IN ('00001','00002')" + "\n");
                    strSqlString.Append("         GROUP BY CASE OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A' WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPR.OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n");
                    strSqlString.Append("                                  WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE'" + "\n");
                    strSqlString.Append("                                  WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n");
                    strSqlString.Append("                                  ELSE 'FINISH'" + "\n");
                    strSqlString.Append("                  END," + "\n");
                    strSqlString.Append("                  DECODE(SIGN((24*" + txtHoldDate.Text + "-((SYSDATE - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                    strSqlString.Append("        UNION ALL" + "\n");
                    strSqlString.Append("        SELECT '" + dtDay.Rows[0][0].ToString() + "' AS WORK_DATE" + "\n");
                    strSqlString.Append("             , 'TOTAL' AS STEP" + "\n");
                    strSqlString.Append("             , DECODE(SIGN((24*" + txtHoldDate.Text + "-((SYSDATE - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체') AS GUBUN" + "\n");
                    if (chkKpcs.Checked == true)
                    {
                        strSqlString.Append("             , SUM(LOT.QTY_1)/1000 AS QTY" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("             , SUM(LOT.QTY_1) AS QTY" + "\n");
                    }
                    strSqlString.Append("          FROM RWIPLOTSTS LOT" + "\n");
                    strSqlString.Append("             , MWIPOPRDEF OPR" + "\n");
                    strSqlString.Append("         WHERE  1=1" + "\n");
                    strSqlString.Append("           AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("           AND LOT.FACTORY = OPR.FACTORY" + "\n");
                    strSqlString.Append("           AND LOT.OPER = OPR.OPER" + "\n");
                    strSqlString.Append("           AND LOT.OWNER_CODE = 'PROD'" + "\n");
                    strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("           AND LOT.MAT_VER = 1" + "\n");
                    strSqlString.Append("           AND LOT.LOT_TYPE = 'W'" + "\n");
                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("           AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                    if (cmbProduct.Text.Trim() != "ALL")
                        strSqlString.AppendFormat("                   AND LOT.LOT_CMF_5 LIKE '{0}'" + "\n", cmbProduct.Text);
                    strSqlString.Append("           AND LOT.OPER NOT IN ('00001','00002')" + "\n");
                    strSqlString.Append("         GROUP BY  DECODE(SIGN((24*" + txtHoldDate.Text + "-((SYSDATE - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                }
                strSqlString.Append("       )" + "\n");
                if (cdvFromOper.Text != "ALL" && cdvFromOper.Text != "")
                    strSqlString.AppendFormat("                   WHERE STEP {0} " + "\n", cdvFromOper.SelectedValueToQueryString);
                strSqlString.Append(" GROUP BY STEP" + "\n");
                strSqlString.Append("        , GUBUN" + "\n");
                strSqlString.Append(" ORDER BY DECODE(STEP,'TOTAL',1,'HMK2A',2,'SAW',3,'QC_GATE2',4,'D/A',5,'W/B',6,'GATE',7,'MOLD',8,'FINISH',9,'HMK3A',10)" + "\n");
                strSqlString.Append("        , DECODE(GUBUN,'정상',1,'정체',2,'WAFER 공급',3,'OUT',4)" + "\n");
            }
            //1212
            else if(cdvFactory.Text.Trim().Equals(GlobalVariable.gsTestDefaultFactory))
            {
                strSqlString.Append("SELECT STEP, GUBUN" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][6].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'TEST',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'V/I',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK4T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS A" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][5].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'TEST',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'V/I',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK4T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS B" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][4].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'TEST',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'V/I',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK4T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS C" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][3].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'TEST',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'V/I',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK4T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS D" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][2].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'TEST',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'V/I',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK4T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS E" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][1].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'TEST',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'V/I',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK4T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS F" + "\n");
                strSqlString.Append("     , SUM(DECODE(WORK_DATE,'" + dtDay.Rows[0][0].ToString() + "'\n");
                strSqlString.Append("                           ,DECODE(STEP,'TOTAL',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK3T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'TEST',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'GATE',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'V/I',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY)" + "\n");
                strSqlString.Append("                                       ,'HMK4T',DECODE(GUBUN,'정상',QTY,'정체',QTY,'OUT',QTY))" + "\n");
                strSqlString.Append("       )) AS G" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT SUBSTR(CUTOFF_DT,0,8) AS WORK_DATE" + "\n");
                strSqlString.Append("             , STEP" + "\n");
                strSqlString.Append("             , GUBUN" + "\n");
                if (chkKpcs.Checked == true)
                {
                    strSqlString.Append("             , QTY/1000 AS QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("             , QTY" + "\n");
                }
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT CUTOFF_DT" + "\n");
                strSqlString.Append("                     , CASE OPR.OPER WHEN 'T0000' THEN 'HMK3T'" + "\n");
                strSqlString.Append("                                     WHEN 'T0070' THEN 'TEST' WHEN 'T0090' THEN 'TEST' WHEN 'T0100' THEN 'TEST'" + "\n");
                strSqlString.Append("                                     WHEN 'T0200' THEN 'TEST' WHEN 'T0400' THEN 'TEST' WHEN 'T0500' THEN 'TEST'" + "\n");
                strSqlString.Append("                                     WHEN 'T0540' THEN 'TEST' WHEN 'T0550' THEN 'TEST' WHEN 'T0600' THEN 'TEST'" + "\n");
                strSqlString.Append("                                     WHEN 'T0650' THEN 'TEST' WHEN 'T0700' THEN 'TEST' WHEN 'T0800' THEN 'TEST' WHEN 'T0900' THEN 'TEST'" + "\n");
                strSqlString.Append("                                     WHEN 'T0080' THEN 'GATE' WHEN 'T0300' THEN 'GATE' WHEN 'T0560' THEN 'GATE'" + "\n");
                strSqlString.Append("                                     WHEN 'T1040' THEN 'GATE' WHEN 'T1060' THEN 'GATE' WHEN 'T0670' THEN 'GATE'" + "\n");
                strSqlString.Append("                                     WHEN 'T1080' THEN 'V/I' WHEN 'T1100' THEN 'V/I' WHEN 'T1150' THEN 'V/I'" + "\n");
                strSqlString.Append("                                     WHEN 'T1200' THEN 'V/I' WHEN 'T1250' THEN 'V/I' WHEN 'T1300' THEN 'V/I'" + "\n");
                strSqlString.Append("                                     WHEN 'TZ010' THEN 'HMK4T'" + "\n");
                strSqlString.Append("                       END AS STEP" + "\n");
                strSqlString.Append("                     , DECODE(CUTOFF_DT,'" + stDay[1] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][1].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                       ,'" + stDay[2] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][2].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                       ,'" + stDay[3] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][3].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                       ,'" + stDay[4] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][4].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                       ,'" + stDay[5] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][5].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                       ,'" + stDay[6] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][6].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                if (!DateTime.Now.ToString("yyyyMMdd").Equals(todate))
                {
                    strSqlString.Append("                                       ,'" + stDay[0] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][0].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                }
                strSqlString.Append("                       ) AS GUBUN" + "\n");
                strSqlString.Append("                     , SUM(LOT.QTY_1) AS QTY" + "\n");
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH LOT" + "\n");
                strSqlString.Append("                     , MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("                 WHERE  1=1" + "\n");
                strSqlString.Append("                   AND LOT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND LOT.FACTORY = OPR.FACTORY" + "\n");
                strSqlString.Append("                   AND LOT.OPER = OPR.OPER" + "\n");
                strSqlString.Append("                   AND LOT.OWNER_CODE = 'PROD'" + "\n");
                strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND LOT.MAT_VER = 1 " + "\n");
                strSqlString.Append("                   AND LOT.LOT_TYPE = 'W'" + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("   AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                if (cmbProduct.Text.Trim() != "ALL")
                    strSqlString.AppendFormat("                   AND LOT.LOT_CMF_5 LIKE '{0}'" + "\n", cmbProduct.Text);

                strSqlString.Append("                   AND LOT.OPER NOT IN ('T000N','T0050','T1050')" + "\n");
                strSqlString.Append("                   AND CUTOFF_DT IN ('" + stDay[6] + "','" + stDay[5] + "','" + stDay[4] + "','" + stDay[3] + "','" + stDay[2] + "','" + stDay[1] + "','" + stDay[0] + "')" + "\n");
                strSqlString.Append("                 GROUP BY CASE OPR.OPER WHEN 'T0000' THEN 'HMK3T'" + "\n");
                strSqlString.Append("                                        WHEN 'T0070' THEN 'TEST' WHEN 'T0090' THEN 'TEST' WHEN 'T0100' THEN 'TEST'" + "\n");
                strSqlString.Append("                                        WHEN 'T0200' THEN 'TEST' WHEN 'T0400' THEN 'TEST' WHEN 'T0500' THEN 'TEST'" + "\n");
                strSqlString.Append("                                        WHEN 'T0540' THEN 'TEST' WHEN 'T0550' THEN 'TEST' WHEN 'T0600' THEN 'TEST'" + "\n");
                strSqlString.Append("                                        WHEN 'T0650' THEN 'TEST' WHEN 'T0700' THEN 'TEST' WHEN 'T0800' THEN 'TEST' WHEN 'T0900' THEN 'TEST'" + "\n");
                strSqlString.Append("                                        WHEN 'T0080' THEN 'GATE' WHEN 'T0300' THEN 'GATE' WHEN 'T0560' THEN 'GATE'" + "\n");
                strSqlString.Append("                                        WHEN 'T1040' THEN 'GATE' WHEN 'T1060' THEN 'GATE' WHEN 'T0670' THEN 'GATE'" + "\n");
                strSqlString.Append("                                        WHEN 'T1080' THEN 'V/I' WHEN 'T1100' THEN 'V/I' WHEN 'T1150' THEN 'V/I'" + "\n");
                strSqlString.Append("                                        WHEN 'T1200' THEN 'V/I' WHEN 'T1250' THEN 'V/I' WHEN 'T1300' THEN 'V/I'" + "\n");
                strSqlString.Append("                                        WHEN 'TZ010' THEN 'HMK4T'" + "\n");
                strSqlString.Append("                          END," + "\n");
                strSqlString.Append("                          CUTOFF_DT" + "\n");
                strSqlString.Append("                        , DECODE(CUTOFF_DT,'" + stDay[1] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][1].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                          ,'" + stDay[2] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][2].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                          ,'" + stDay[3] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][3].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                          ,'" + stDay[4] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][4].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                          ,'" + stDay[5] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][5].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                          ,'" + stDay[6] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][6].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                if (!DateTime.Now.ToString("yyyyMMdd").Equals(todate))
                {
                    strSqlString.Append("                                       ,'" + stDay[0] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][0].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                }
                strSqlString.Append("                                 )" + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("        UNION ALL" + "\n");
                strSqlString.Append("        SELECT SUBSTR(CUTOFF_DT,0,8) AS WORK_DATE" + "\n");
                strSqlString.Append("             , 'TOTAL' AS STEP" + "\n");
                strSqlString.Append("             , DECODE(CUTOFF_DT,'" + stDay[1] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][1].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                               ,'" + stDay[2] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][2].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                               ,'" + stDay[3] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][3].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                               ,'" + stDay[4] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][4].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                               ,'" + stDay[5] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][5].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                               ,'" + stDay[6] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][6].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                if (!DateTime.Now.ToString("yyyyMMdd").Equals(todate))
                {
                    strSqlString.Append("                               ,'" + stDay[0] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][0].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                }
                strSqlString.Append("               ) AS GUBUN" + "\n");

                if (chkKpcs.Checked == true)
                {
                    strSqlString.Append("             , SUM(LOT.QTY_1)/1000 AS QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("             , SUM(LOT.QTY_1) AS QTY" + "\n");
                }
                strSqlString.Append("          FROM RWIPLOTSTS_BOH LOT" + "\n");
                strSqlString.Append("             , MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("         WHERE  1=1" + "\n");
                strSqlString.Append("           AND LOT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND LOT.FACTORY = OPR.FACTORY " + "\n");
                strSqlString.Append("           AND LOT.OPER = OPR.OPER" + "\n");
                strSqlString.Append("           AND LOT.OWNER_CODE = 'PROD'" + "\n");
                strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("           AND LOT.MAT_VER = 1" + "\n");
                strSqlString.Append("           AND LOT.LOT_TYPE = 'W'" + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("   AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                if (cmbProduct.Text.Trim() != "ALL")
                    strSqlString.AppendFormat("                   AND LOT.LOT_CMF_5 LIKE '{0}'" + "\n", cmbProduct.Text);
                strSqlString.Append("           AND LOT.OPER NOT IN ('T000N','T0050','T1050')" + "\n");
                strSqlString.Append("           AND CUTOFF_DT IN ('" + stDay[6] + "','" + stDay[5] + "','" + stDay[4] + "','" + stDay[3] + "','" + stDay[2] + "','" + stDay[1] + "','" + stDay[0] + "')" + "\n");
                strSqlString.Append("         GROUP BY CUTOFF_DT" + "\n");
                strSqlString.Append("                , DECODE(CUTOFF_DT,'" + stDay[1] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][1].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                  ,'" + stDay[2] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][2].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                  ,'" + stDay[3] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][3].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                  ,'" + stDay[4] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][4].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                  ,'" + stDay[5] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][5].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                strSqlString.Append("                                  ,'" + stDay[6] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][6].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                if (!DateTime.Now.ToString("yyyyMMdd").Equals(todate))
                {
                    strSqlString.Append("                                  ,'" + stDay[0] + "',DECODE(SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + dtDay.Rows[0][0].ToString() + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                }
                strSqlString.Append("                         )" + "\n");
                strSqlString.Append("        UNION ALL" + "\n");
                strSqlString.Append("        SELECT WORK_DATE" + "\n");
                strSqlString.Append("             , STEP" + "\n");
                strSqlString.Append("             , 'OUT'" + "\n");
                if (chkKpcs.Checked == true)
                {
                    strSqlString.Append("             , SUM(QTY)/1000 AS QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("             , SUM(QTY) AS QTY" + "\n");
                }
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                 SELECT WORK_DATE" + "\n");
                strSqlString.Append("                      , CASE OPR.OPER WHEN 'T0000' THEN 'HMK3T'" + "\n");
                strSqlString.Append("                                      WHEN 'T0070' THEN 'TEST' WHEN 'T0090' THEN 'TEST' WHEN 'T0100' THEN 'TEST'" + "\n");
                strSqlString.Append("                                      WHEN 'T0200' THEN 'TEST' WHEN 'T0400' THEN 'TEST' WHEN 'T0500' THEN 'TEST'" + "\n");
                strSqlString.Append("                                      WHEN 'T0540' THEN 'TEST' WHEN 'T0550' THEN 'TEST' WHEN 'T0600' THEN 'TEST'" + "\n");
                strSqlString.Append("                                      WHEN 'T0650' THEN 'TEST' WHEN 'T0700' THEN 'TEST' WHEN 'T0800' THEN 'TEST' WHEN 'T0900' THEN 'TEST'" + "\n");
                strSqlString.Append("                                      WHEN 'T0080' THEN 'GATE' WHEN 'T0300' THEN 'GATE' WHEN 'T0560' THEN 'GATE'" + "\n");
                strSqlString.Append("                                      WHEN 'T1040' THEN 'GATE' WHEN 'T1060' THEN 'GATE' WHEN 'T0670' THEN 'GATE'" + "\n");
                strSqlString.Append("                                      WHEN 'T1080' THEN 'V/I' WHEN 'T1100' THEN 'V/I' WHEN 'T1150' THEN 'V/I'" + "\n");
                strSqlString.Append("                                      WHEN 'T1200' THEN 'V/I' WHEN 'T1250' THEN 'V/I' WHEN 'T1300' THEN 'V/I'" + "\n");
                strSqlString.Append("                                      WHEN 'TZ010' THEN 'HMK4T'" + "\n");
                strSqlString.Append("                        END AS STEP" + "\n");
                strSqlString.Append("                      , DECODE(MOV.OPER,'TZ010', SUM(S1_MOVE_QTY_1+S2_MOVE_QTY_1+S3_MOVE_QTY_1+S4_MOVE_QTY_1)" + "\n");
                strSqlString.Append("                                       , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S4_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) AS QTY" + "\n");
                strSqlString.Append("                          FROM RSUMWIPMOV MOV" + "\n");
                strSqlString.Append("                             , MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("                         WHERE  1=1" + "\n");
                strSqlString.Append("                           AND MOV.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                           AND MOV.FACTORY = OPR.FACTORY" + "\n");
                strSqlString.Append("                           AND MOV.OPER = OPR.OPER" + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("   AND MOV.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                if (cmbProduct.Text.Trim() != "ALL")
                    strSqlString.AppendFormat("                   AND MOV.CM_KEY_3 LIKE '{0}'" + "\n", cmbProduct.Text);
                strSqlString.Append("                           AND WORK_DATE BETWEEN '" + dtDay.Rows[0][6].ToString() + "' AND '" + dtDay.Rows[0][0].ToString() + "'" + "\n");
                strSqlString.Append("                        GROUP BY WORK_DATE" + "\n");
                strSqlString.Append("                               , CASE OPR.OPER WHEN 'T0000' THEN 'HMK3T'" + "\n");
                strSqlString.Append("                                               WHEN 'T0070' THEN 'TEST' WHEN 'T0090' THEN 'TEST' WHEN 'T0100' THEN 'TEST'" + "\n");
                strSqlString.Append("                                               WHEN 'T0200' THEN 'TEST' WHEN 'T0400' THEN 'TEST' WHEN 'T0500' THEN 'TEST'" + "\n");
                strSqlString.Append("                                               WHEN 'T0540' THEN 'TEST' WHEN 'T0550' THEN 'TEST' WHEN 'T0600' THEN 'TEST'" + "\n");
                strSqlString.Append("                                               WHEN 'T0650' THEN 'TEST' WHEN 'T0700' THEN 'TEST' WHEN 'T0800' THEN 'TEST' WHEN 'T0900' THEN 'TEST'" + "\n");
                strSqlString.Append("                                               WHEN 'T0080' THEN 'GATE' WHEN 'T0300' THEN 'GATE' WHEN 'T0560' THEN 'GATE'" + "\n");
                strSqlString.Append("                                               WHEN 'T1040' THEN 'GATE' WHEN 'T1060' THEN 'GATE' WHEN 'T0670' THEN 'GATE'" + "\n");
                strSqlString.Append("                                               WHEN 'T1080' THEN 'V/I' WHEN 'T1100' THEN 'V/I' WHEN 'T1150' THEN 'V/I'" + "\n");
                strSqlString.Append("                                               WHEN 'T1200' THEN 'V/I' WHEN 'T1250' THEN 'V/I' WHEN 'T1300' THEN 'V/I'" + "\n");
                strSqlString.Append("                                               WHEN 'TZ010' THEN 'HMK4T'" + "\n");
                strSqlString.Append("                                  END" + "\n");
                strSqlString.Append("                                , MOV.OPER" + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         WHERE STEP <> 'HMK2A'" + "\n");
                strSqlString.Append("         GROUP BY WORK_DATE, STEP" + "\n");
               
                if (DateTime.Now.ToString("yyyyMMdd").Equals(todate))
                {
                    strSqlString.Append("        UNION ALL" + "\n");
                    strSqlString.Append("        SELECT '" + dtDay.Rows[0][0].ToString() + "' AS WORK_DATE" + "\n");
                    strSqlString.Append("             , CASE OPR.OPER WHEN 'T0000' THEN 'HMK3T'" + "\n");
                    strSqlString.Append("                             WHEN 'T0070' THEN 'TEST' WHEN 'T0090' THEN 'TEST' WHEN 'T0100' THEN 'TEST'" + "\n");
                    strSqlString.Append("                             WHEN 'T0200' THEN 'TEST' WHEN 'T0400' THEN 'TEST' WHEN 'T0500' THEN 'TEST'" + "\n");
                    strSqlString.Append("                             WHEN 'T0540' THEN 'TEST' WHEN 'T0550' THEN 'TEST' WHEN 'T0600' THEN 'TEST'" + "\n");
                    strSqlString.Append("                             WHEN 'T0650' THEN 'TEST' WHEN 'T0700' THEN 'TEST' WHEN 'T0800' THEN 'TEST' WHEN 'T0900' THEN 'TEST'" + "\n");
                    strSqlString.Append("                             WHEN 'T0080' THEN 'GATE' WHEN 'T0300' THEN 'GATE' WHEN 'T0560' THEN 'GATE'" + "\n");
                    strSqlString.Append("                             WHEN 'T1040' THEN 'GATE' WHEN 'T1060' THEN 'GATE' WHEN 'T0670' THEN 'GATE'" + "\n");
                    strSqlString.Append("                             WHEN 'T1080' THEN 'V/I' WHEN 'T1100' THEN 'V/I' WHEN 'T1150' THEN 'V/I'" + "\n");
                    strSqlString.Append("                             WHEN 'T1200' THEN 'V/I' WHEN 'T1250' THEN 'V/I' WHEN 'T1300' THEN 'V/I'" + "\n");
                    strSqlString.Append("                             WHEN 'TZ010' THEN 'HMK4T'" + "\n");
                    strSqlString.Append("               END AS STEP" + "\n");
                    strSqlString.Append("             , DECODE(SIGN((24*" + txtHoldDate.Text + "-((SYSDATE - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체') AS GUBUN" + "\n");
                    strSqlString.Append("             , SUM(LOT.QTY_1) AS QTY" + "\n");
                    strSqlString.Append("          FROM RWIPLOTSTS LOT" + "\n");
                    strSqlString.Append("             , MWIPOPRDEF OPR" + "\n");
                    strSqlString.Append("         WHERE  1=1" + "\n");
                    strSqlString.Append("           AND LOT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("           AND LOT.FACTORY = OPR.FACTORY" + "\n");
                    strSqlString.Append("           AND LOT.OPER = OPR.OPER" + "\n");
                    strSqlString.Append("           AND LOT.OWNER_CODE = 'PROD'" + "\n");
                    strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("           AND LOT.MAT_VER = 1" + "\n");
                    strSqlString.Append("           AND LOT.LOT_TYPE = 'W'" + "\n");
                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("   AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                    if (cmbProduct.Text.Trim() != "ALL")
                        strSqlString.AppendFormat("                   AND LOT.LOT_CMF_5 LIKE '{0}'" + "\n", cmbProduct.Text);
                    strSqlString.Append("           AND LOT.OPER NOT IN ('T000N','T0050','T1050')" + "\n");
                    strSqlString.Append("         GROUP BY CASE OPR.OPER WHEN 'T0000' THEN 'HMK3T'" + "\n");
                    strSqlString.Append("                                WHEN 'T0070' THEN 'TEST' WHEN 'T0090' THEN 'TEST' WHEN 'T0100' THEN 'TEST'" + "\n");
                    strSqlString.Append("                                WHEN 'T0200' THEN 'TEST' WHEN 'T0400' THEN 'TEST' WHEN 'T0500' THEN 'TEST'" + "\n");
                    strSqlString.Append("                                WHEN 'T0540' THEN 'TEST' WHEN 'T0550' THEN 'TEST' WHEN 'T0600' THEN 'TEST'" + "\n");
                    strSqlString.Append("                                WHEN 'T0650' THEN 'TEST' WHEN 'T0700' THEN 'TEST' WHEN 'T0800' THEN 'TEST' WHEN 'T0900' THEN 'TEST'" + "\n");
                    strSqlString.Append("                                WHEN 'T0080' THEN 'GATE' WHEN 'T0300' THEN 'GATE' WHEN 'T0560' THEN 'GATE'" + "\n");
                    strSqlString.Append("                                WHEN 'T1040' THEN 'GATE' WHEN 'T1060' THEN 'GATE' WHEN 'T0670' THEN 'GATE'" + "\n");
                    strSqlString.Append("                                WHEN 'T1080' THEN 'V/I' WHEN 'T1100' THEN 'V/I' WHEN 'T1150' THEN 'V/I'" + "\n");
                    strSqlString.Append("                                WHEN 'T1200' THEN 'V/I' WHEN 'T1250' THEN 'V/I' WHEN 'T1300' THEN 'V/I'" + "\n");
                    strSqlString.Append("                                WHEN 'TZ010' THEN 'HMK4T'" + "\n");
                    strSqlString.Append("                  END," + "\n");
                    strSqlString.Append("                  DECODE(SIGN((24*" + txtHoldDate.Text + "-((SYSDATE - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                    strSqlString.Append("        UNION ALL" + "\n");
                    strSqlString.Append("        SELECT '" + dtDay.Rows[0][0].ToString() + "' AS WORK_DATE" + "\n");
                    strSqlString.Append("             , 'TOTAL' AS STEP" + "\n");
                    strSqlString.Append("             , DECODE(SIGN((24*" + txtHoldDate.Text + "-((SYSDATE - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체') AS GUBUN" + "\n");
                    if (chkKpcs.Checked == true)
                    {
                        strSqlString.Append("             , SUM(LOT.QTY_1)/1000 AS QTY" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("             , SUM(LOT.QTY_1) AS QTY" + "\n");
                    }
                    strSqlString.Append("          FROM RWIPLOTSTS LOT" + "\n");
                    strSqlString.Append("             , MWIPOPRDEF OPR" + "\n");
                    strSqlString.Append("         WHERE  1=1" + "\n");
                    strSqlString.Append("           AND LOT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("           AND LOT.FACTORY = OPR.FACTORY" + "\n");
                    strSqlString.Append("           AND LOT.OPER = OPR.OPER" + "\n");
                    strSqlString.Append("           AND LOT.OWNER_CODE = 'PROD'" + "\n");
                    strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("           AND LOT.MAT_VER = 1" + "\n");
                    strSqlString.Append("           AND LOT.LOT_TYPE = 'W'" + "\n");
                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("   AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                    if (cmbProduct.Text.Trim() != "ALL")
                        strSqlString.AppendFormat("                   AND LOT.LOT_CMF_5 LIKE '{0}'" + "\n", cmbProduct.Text);
                    strSqlString.Append("           AND LOT.OPER NOT IN ('T000N','T0050','T1050')" + "\n");
                    strSqlString.Append("         GROUP BY  DECODE(SIGN((24*" + txtHoldDate.Text + "-((SYSDATE - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))),1,'정상','정체')" + "\n");
                }
                strSqlString.Append("       )" + "\n");
                if (cdvFromOper.Text != "ALL" && cdvFromOper.Text != "")
                    strSqlString.AppendFormat("                   WHERE STEP {0} " + "\n", cdvFromOper.SelectedValueToQueryString);
                strSqlString.Append(" GROUP BY STEP" + "\n");
                strSqlString.Append("        , GUBUN" + "\n");
                strSqlString.Append(" ORDER BY DECODE(STEP,'TOTAL',1,'HMK3T',2,'TEST',3,'GATE',4,'V/I',5,'HMK4T',6)" + "\n");
                strSqlString.Append("        , DECODE(GUBUN,'정상',1,'정체',2,'OUT',3)" + "\n");
            }


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }



        private string MakePopSqlString(String strDate, String step)
        {
            StringBuilder strSqlString = new StringBuilder();

            if (cdvFactory.Text.Trim().Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                strSqlString.AppendFormat("SELECT * FROM(" + "\n");
            }
            
            
            strSqlString.Append("SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'H_CUSTOMER' AND ROWNUM=1 AND KEY_1 = MAT.MAT_GRP_1), '-') Customer " + "\n");
            strSqlString.Append("     , MAT.MAT_GRP_3 AS Pakage " + "\n");
            strSqlString.Append("     , MAT.MAT_GRP_6 AS Lead " + "\n");
            strSqlString.Append("     , NVL(MAT.MAT_CMF_10, '-')AS PIN_TYPE" + "\n");
            strSqlString.Append("     , MAT.MAT_ID  " + "\n");
            strSqlString.Append("     , LOT.LOT_ID" + "\n");
            strSqlString.Append("     , LOT.OPER  " + "\n");
            strSqlString.Append("     , SUM(LOT.QTY_1) QTY " + "\n");
            strSqlString.Append("     , TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - DECODE(LOT.LOT_CMF_14, ' ', TO_DATE(LOT.LOT_CMF_14,'YYYYMMDDHH24MISS'), TO_DATE(LOT.LOT_CMF_14,'YYYYMMDDHH24MISS')), 2) AS \"당사일\"  " + "\n");
            strSqlString.Append("     , TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - TO_DATE(LOT.OPER_IN_TIME,'YYYYMMDDHH24MISS'), 2) AS \"공정일\" " + "\n");

            if (cdvFactory.Text.Trim().Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                strSqlString.AppendFormat("     , CASE OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A'" + "\n");
                strSqlString.AppendFormat("                       WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPR.OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n");
                strSqlString.AppendFormat("                       WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE'" + "\n");
                strSqlString.AppendFormat("                       WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n");
                strSqlString.AppendFormat("                       ELSE 'FINISH' END AS OPER_GRP" + "\n");
            }


            if (!DateTime.Now.ToString("yyyyMMdd").Equals(strDate.Substring(0, 8)))
                strSqlString.Append("  FROM RWIPLOTSTS_BOH LOT  " + "\n");
            else
                strSqlString.Append("  FROM RWIPLOTSTS LOT  " + "\n");
            strSqlString.Append("     , MWIPOPRDEF OPR " + "\n");
            strSqlString.Append("     , MWIPMATDEF MAT  " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");

            if (!DateTime.Now.ToString("yyyyMMdd").Equals(strDate.Substring(0,8)))
            {
                strSqlString.Append("   AND LOT.CUTOFF_DT = '" + strDate.Substring(0, 10) + "' " + "\n");
            }   
            strSqlString.Append("   AND LOT.FACTORY = OPR.FACTORY " + "\n");
            strSqlString.Append("   AND LOT.OPER = OPR.OPER " + "\n");

            // 공정
            if (cdvFactory.Text.Trim().Equals(GlobalVariable.gsTestDefaultFactory))
            {
                strSqlString.AppendFormat("   AND OPR.OPER IN " + step + "\n");
            }
            // Product
            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("   AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
            }
            if (cmbProduct.Text.Trim() != "ALL")
                strSqlString.AppendFormat("                   AND LOT.LOT_CMF_5 LIKE '{0}'" + "\n", cmbProduct.Text);
            strSqlString.Append("   AND LOT.FACTORY = MAT.FACTORY  " + "\n");
            strSqlString.Append("   AND LOT.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("   AND LOT.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("   AND LOT.MAT_VER = 1 " + "\n");
            strSqlString.Append("   AND LOT.LOT_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("   AND LOT.OWNER_CODE = 'PROD'  " + "\n");
            if (cmbProduct.Text.Trim() != "ALL")
                strSqlString.AppendFormat("                   AND LOT.LOT_CMF_5 LIKE '{0}'" + "\n", cmbProduct.Text);
            strSqlString.Append("   AND SIGN((24*" + txtHoldDate.Text + "-(((TO_DATE('" + strDate + "', 'YYYYMMDDHH24MISS')) - TO_DATE(OPER_IN_TIME,'YYYYMMDDhh24miss')) * 24))) <> 1" + "\n");
            strSqlString.Append(" GROUP BY MAT.FACTORY, LOT.LOT_ID, MAT.MAT_GRP_1 , MAT.MAT_GRP_3, MAT.MAT_GRP_6, MAT_CMF_10, MAT.MAT_ID, LOT.OPER, LOT.LOT_CMF_14, LOT.OPER_IN_TIME " + "\n");

            if (cdvFactory.Text.Trim().Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                strSqlString.AppendFormat("     , CASE OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A'" + "\n");
                strSqlString.AppendFormat("                       WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPR.OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n");
                strSqlString.AppendFormat("                       WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE'" + "\n");
                strSqlString.AppendFormat("                       WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n");
                strSqlString.AppendFormat("                       ELSE 'FINISH' END" + "\n");
            }
            
            strSqlString.Append(" ORDER BY LOT.LOT_ID, MAT.MAT_GRP_1 , MAT.MAT_GRP_3, MAT.MAT_GRP_6, MAT_CMF_10, MAT.MAT_ID, LOT.OPER, LOT.LOT_CMF_14, LOT.OPER_IN_TIME " + "\n");
            if (cdvFactory.Text.Trim().Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                strSqlString.AppendFormat(")WHERE OPER_GRP = " + step + "\n");
            }
            
            
            return strSqlString.ToString();
        }

        private void ShowChart(int rowCount)
        {
            double max_temp = 0;
            double max1 = 0;


            string s_query = "";


            if (cdvFactory.Text.Trim().Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                s_query = "SELECT WORK_DATE, SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1+S4_FAC_OUT_QTY_1)" + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS SHIP_QTY \n"
                       + "  FROM RSUMFACMOV \n"
                       + " WHERE FACTORY = 'CUSTOMER' \n"
                       + "   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' \n"
                       + "   AND CM_KEY_2 = 'PROD' \n"
                       + "   AND LOT_TYPE ='W' \n"
                       + "   AND WORK_DATE BETWEEN '" + dtDay.Rows[0][6].ToString() + "' AND '" + dtDay.Rows[0][0].ToString() + "' \n"
                       + " GROUP BY WORK_DATE";
            }
            else
            {
                s_query = "SELECT WORK_DATE, SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1+S4_FAC_OUT_QTY_1)" + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS SHIP_QTY \n"
                       + "  FROM RSUMFACMOV \n"
                       + " WHERE FACTORY = 'CUSTOMER' \n"
                       + "   AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "' \n"
                       + "   AND CM_KEY_2 = 'PROD' \n"
                       + "   AND LOT_TYPE ='W' \n"
                       + "   AND WORK_DATE BETWEEN '" + dtDay.Rows[0][6].ToString() + "' AND '" + dtDay.Rows[0][0].ToString() + "' \n"
                       + " GROUP BY WORK_DATE";
            }
            DataTable dtShipQty = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", s_query.Replace((char)Keys.Tab, ' '));


           
            
            
            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(3, 7);
            int[] normal_columns = new Int32[7];
            int[] delay_columns = new Int32[7];
            int[] columnsHeader = new Int32[7];

            for (int i = 0; i < delay_columns.Length; i++)
            {
                columnsHeader[i] = 2 + i;
                delay_columns[i] = 2 + i;
                normal_columns[i] = 2 + i;
              
            }

            //정상 재공
            max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, normal_columns, SeriseType.Rows);
            
            //정체 재공
            double max2 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 1 }, delay_columns, SeriseType.Rows);
            
            double max3 = udcChartFX1.RPT_4_AddData_Original(dtShipQty, new int[] { 0,1,2,3,4,5,6 }, new int[] { 1 }, SeriseType.Column, DataTypes.Initeger);

            if (max1 > max_temp)
            {
                max_temp = max1;
            }

            String legendDescShip = "SHIP [단위 : ea]";
            String legendDescLot = "재공 [단위 : ea]";
            
            if (chkKpcs.Checked == true)
            {
                legendDescShip = "SHIP [단위 : kpcs]";
                legendDescLot = "재공 [단위 : kpcs]";
            }
            
           
            udcChartFX1.RPT_6_SetGallery(ChartType.Stacked, 0, 1, legendDescLot, AsixType.Y, DataTypes.Initeger, (max2 + max1) * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Stacked, 1, 1, legendDescLot, AsixType.Y, DataTypes.Initeger, (max2 + max1) * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 2, 1, legendDescShip, AsixType.Y2, DataTypes.Initeger, max3 * 2);

            udcChartFX1.Series[0].Color = System.Drawing.Color.LightSkyBlue;
            udcChartFX1.Series[1].Color = System.Drawing.Color.Bisque;
            
            udcChartFX1.Series[0].PointLabels = true;
            udcChartFX1.Series[1].PointLabels = true;
            udcChartFX1.Series[2].PointLabels = true;

            udcChartFX1.Series[0].PointLabelColor = Color.Black;
            udcChartFX1.Series[1].PointLabelColor = Color.DarkCyan;
           
            udcChartFX1.Series[0].PointLabelAlign = SoftwareFX.ChartFX.LabelAlign.Center;

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "정상", "정체", "SHIP" }, SoftwareFX.ChartFX.Docked.Top);
            udcChartFX1.AxisY.Gridlines = true;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;   
  
        }

       
       
        private void cdvFromOper_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return;

            }
            else
            {

                if (cdvFactory.Text.Trim().Equals(GlobalVariable.gsAssyDefaultFactory))
                {
                    strQuery += " SELECT Code, Data FROM(" + "\n";
                    strQuery += " SELECT '' AS Data, CASE OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A' WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n";
                    strQuery += "                                    WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE' " + "\n";
                    strQuery += "                                    WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n";
                    strQuery += "                                    ELSE 'FINISH'" + "\n";
                    strQuery += "                    END AS Code" + "\n";
                    strQuery += "   FROM MWIPOPRDEF" + "\n";
                    strQuery += "  GROUP BY CASE OPER_GRP_1 WHEN 'HMK2A' THEN 'HMK2A' WHEN 'B/G' THEN 'SAW' WHEN 'SAW' THEN DECODE(OPER, 'A0300', 'QC_GATE2', 'SAW') WHEN 'S/P' THEN 'D/A'" + "\n";
                    strQuery += "                           WHEN 'SMT' THEN 'D/A' WHEN 'D/A' THEN 'D/A' WHEN 'W/B' THEN 'W/B' WHEN 'GATE' THEN 'GATE'" + "\n";
                    strQuery += "                           WHEN 'MOLD' THEN 'MOLD' WHEN 'CURE' THEN 'MOLD' WHEN 'HMK3A' THEN 'HMK3A'" + "\n";
                    strQuery += "                           ELSE 'FINISH' END" + "\n";
                    strQuery += " UNION ALL SELECT '' AS Data, 'TOTAL' AS Code FROM DUAL)" + "\n";
                    strQuery += " ORDER BY DECODE(Code,'TOTAL',1,'HMK2A',2,'SAW',3,'QC_GATE2',4,'D/A',5,'W/B',6,'GATE',7,'MOLD',8,'FINISH',9,'HMK3A',10)" + "\n";
                }
                else
                {
                    strQuery += " SELECT Code, Data FROM(" + "\n";
                    strQuery += " SELECT '' AS Data, CASE OPER WHEN 'T0000' THEN 'HMK3T'" + "\n";
                    strQuery += "                              WHEN 'T0070' THEN 'TEST' WHEN 'T0090' THEN 'TEST' WHEN 'T0100' THEN 'TEST'" + "\n";
                    strQuery += "                              WHEN 'T0200' THEN 'TEST' WHEN 'T0400' THEN 'TEST' WHEN 'T0500' THEN 'TEST'" + "\n";
                    strQuery += "                              WHEN 'T0540' THEN 'TEST' WHEN 'T0550' THEN 'TEST' WHEN 'T0600' THEN 'TEST'" + "\n";
                    strQuery += "                              WHEN 'T0650' THEN 'TEST' WHEN 'T0700' THEN 'TEST' WHEN 'T0800' THEN 'TEST' WHEN 'T0900' THEN 'TEST'" + "\n";
                    strQuery += "                              WHEN 'T0080' THEN 'GATE' WHEN 'T0300' THEN 'GATE' WHEN 'T0560' THEN 'GATE'" + "\n";
                    strQuery += "                              WHEN 'T1040' THEN 'GATE' WHEN 'T1060' THEN 'GATE' WHEN 'T0670' THEN 'GATE'" + "\n";
                    strQuery += "                              WHEN 'T1080' THEN 'V/I' WHEN 'T1100' THEN 'V/I' WHEN 'T1150' THEN 'V/I'" + "\n";
                    strQuery += "                              WHEN 'T1200' THEN 'V/I' WHEN 'T1250' THEN 'V/I' WHEN 'T1300' THEN 'V/I'" + "\n";
                    strQuery += "                              WHEN 'TZ010' THEN 'HMK4T' ELSE 'TOTAL' END AS Code" + "\n";

                    strQuery += "   FROM MWIPOPRDEF" + "\n";
                    strQuery += "  GROUP BY CASE OPER WHEN 'T0000' THEN 'HMK3T'" + "\n";
                    strQuery += "                     WHEN 'T0070' THEN 'TEST' WHEN 'T0090' THEN 'TEST' WHEN 'T0100' THEN 'TEST'" + "\n";
                    strQuery += "                     WHEN 'T0200' THEN 'TEST' WHEN 'T0400' THEN 'TEST' WHEN 'T0500' THEN 'TEST'" + "\n";
                    strQuery += "                     WHEN 'T0540' THEN 'TEST' WHEN 'T0550' THEN 'TEST' WHEN 'T0600' THEN 'TEST'" + "\n";
                    strQuery += "                     WHEN 'T0650' THEN 'TEST' WHEN 'T0700' THEN 'TEST' WHEN 'T0800' THEN 'TEST' WHEN 'T0900' THEN 'TEST'" + "\n";
                    strQuery += "                     WHEN 'T0080' THEN 'GATE' WHEN 'T0300' THEN 'GATE' WHEN 'T0560' THEN 'GATE'" + "\n";
                    strQuery += "                     WHEN 'T1040' THEN 'GATE' WHEN 'T1060' THEN 'GATE' WHEN 'T0670' THEN 'GATE'" + "\n";
                    strQuery += "                     WHEN 'T1080' THEN 'V/I' WHEN 'T1100' THEN 'V/I' WHEN 'T1150' THEN 'V/I'" + "\n";
                    strQuery += "                     WHEN 'T1200' THEN 'V/I' WHEN 'T1250' THEN 'V/I' WHEN 'T1300' THEN 'V/I'" + "\n";
                    strQuery += "                     WHEN 'TZ010' THEN 'HMK4T' ELSE 'TOTAL' END) " + "\n";
                    strQuery += " ORDER BY DECODE(Code,'TOTAL',1,'HMK3T',2,'TEST',3,'GATE',4,'V/I',5,'HMK4T',6)" + "\n";
                }
            }

            cdvFromOper.sDynamicQuery = strQuery;
        }
       
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {            
            string step_oper = null;
            DataTable dt = null;

            if (e.Column <= spdData.ActiveSheet.Columns["구분"].Index)
              return;
            if (spdData.ActiveSheet.Cells[e.Row, e.Column].Text == " ")
              return;

          if (!spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("TOTAL") && spdData.ActiveSheet.Cells[e.Row, 1].Text.Equals("정체"))
          {
              
              //if (e.Row == 1 || e.Row == 3 || e.Row == 6 || e.Row == 9 || e.Row == 12 || e.Row == 15 || e.Row == 18
               // || e.Row == 21 || e.Row == 24 || e.Row == 27)
             // {

                  if (cdvFactory.Text.Trim().Equals(GlobalVariable.gsTestDefaultFactory))
                  {
                      if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("HMK3T"))
                      {
                          step_oper = "('T0000')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("TEST"))
                      {
                          step_oper = "('T0070', 'T0090',  'T0100', 'T0200', 'T0400', 'T0500', 'T0540', 'T0550', 'T0600', 'T0650', 'T0700', 'T0800', 'T0900')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("GATE"))
                      {
                          step_oper = "('T0080', 'T0300', 'T0560',  'T1040', 'T1060', 'T0670')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("V/I"))
                      {
                          step_oper = "('T1080', 'T1100', 'T1150',  'T1200', 'T1250', 'T1300')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("HMK4T"))
                      {
                          step_oper = "('TZ010')";
                      }
                  }
                  else if (cdvFactory.Text.Trim().Equals(GlobalVariable.gsAssyDefaultFactory))
                  {
                      if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("HMK2A"))
                      {
                          step_oper = "('HMK2A')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("SAW"))
                      {
                          step_oper = "('SAW')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("QC_GATE2"))
                      {
                          step_oper = "('QC_GATE2')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("D/A"))
                      {
                          step_oper = "('D/A')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("W/B"))
                      {
                          step_oper = "('W/B')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("GATE"))
                      {
                          step_oper = "('GATE')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("MOLD"))
                      {
                          step_oper = "('MOLD')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("FINISH"))
                      {
                          step_oper = "('FINISH')";
                      }
                      else if (spdData.ActiveSheet.Cells[e.Row, 0].Text.Equals("HMK3A"))
                      {
                          step_oper = "('HMK3A')";
                      }

                  }
                  string strTitle = "date: " + spdData.ActiveSheet.Columns[e.Column].Label + " 공정: " + spdData.ActiveSheet.Cells[e.Row, 0].Text + " 정체 LOT(정체일: " + txtHoldDate.Text + "일)";
                  dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakePopSqlString(spdData.ActiveSheet.Columns[e.Column].Label + "220000", step_oper));
                  System.Windows.Forms.Form frm = new PRD010306_P1(strTitle, dt);
                  frm.ShowDialog();


              //}
          }

        }
        
        
    }
}
