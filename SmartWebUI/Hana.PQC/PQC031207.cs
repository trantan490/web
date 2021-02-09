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

using System.Data.OleDb;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;

/****************************************************
 * Comment : 공정검사 품질불량현황
 *
 * Created By : KIM MINWOO (2013-05-27-월요일)
 *
 * Modified By : KIM MINWOO (2013-05-27-월요일)
 * 2013-05-27-김민우: 
 ****************************************************/

namespace Hana.PQC
{
    public partial class PQC031207 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PQC031207 : Program Initial "
        
        private DataTable dtOper = null;
        private DataTable dtWeek = null;
        private DataTable dtDay = null;
        
        public PQC031207()
        {
            InitializeComponent();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvBaseDate.Value = DateTime.Today;
        }

        #endregion


        #region " Common Function "

        private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
        {
            try
            {
                
               
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

        private void fnSSSortInit()
        {
          
            try
            {
               
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

        // 공정 정보 가져오기(DATA_2)
        private string MakeSqlOper()
        {
            StringBuilder strSqlString = new StringBuilder();
            
            strSqlString.AppendFormat(" SELECT DISTINCT DATA_2 FROM MGCMTBLDAT@RPTTOMES GCM " + "\n");
            strSqlString.AppendFormat("  WHERE GCM.FACTORY = '" + cdvFactory.Text + "'"+ "\n");
            strSqlString.AppendFormat("    AND GCM.TABLE_NAME = 'OPER_DEFINE_QC' " + "\n");
            strSqlString.AppendFormat(" ORDER BY DECODE(DATA_2,'BG',1,'SAW',2,'DA',3,'WB',4,'MD',5,'SBA',6,'SST',7,'MK',8,'TF',9,'기타',10,11) " + "\n");
            return strSqlString.ToString();
        }


        // 6주 전 주차 가져오기
        private string MakeSqlWeek()
        {
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.AppendFormat("SELECT DECODE(PLAN_WEEK - 5, -4, 49, -3, 50, -2, 51, -1, 52,0,53, PLAN_WEEK - 5) AS WW1, " + "\n");
            strSqlString.AppendFormat("PLAN_WEEK AS WW5, PLAN_YEAR" + "\n");
            strSqlString.AppendFormat("FROM   MWIPCALDEF" + "\n");
            strSqlString.AppendFormat("WHERE  CALENDAR_ID = 'QC'" + "\n");
            strSqlString.AppendFormat("AND    SYS_DATE = '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "'" + "\n");
            return strSqlString.ToString();
        }

        // 6주 전 주차 시작일 가져오기
        private string MakeSqlDay()
        {

            dtWeek = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlWeek());

            string week6ago = dtWeek.Rows[0][0].ToString();
            string thisWeek = dtWeek.Rows[0][1].ToString();
            string year;

            if (Convert.ToUInt16(week6ago) > Convert.ToUInt16(thisWeek))
            {
                year = cdvBaseDate.Value.AddYears(-1).ToString("yyyy");
            }
            else
            {
                year = cdvBaseDate.Value.ToString("yyyy");
            }


            StringBuilder strSqlString = new StringBuilder();
            strSqlString.AppendFormat("SELECT SYS_DATE FROM MWIPCALDEF " + "\n");
            strSqlString.AppendFormat(" WHERE  CALENDAR_ID = 'QC'" + "\n");
            strSqlString.AppendFormat("   AND    PLAN_YEAR = '"+ year + "'"+ "\n");
            strSqlString.AppendFormat("   AND PLAN_WEEK = '" + week6ago + "'" + "\n");
            strSqlString.AppendFormat(" ORDER BY SYS_DATE" + "\n");
            return strSqlString.ToString();
        }


        // 품질 Issue 월별 건수 
        private string MakeChartMon()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat(" SELECT PLAN_YEAR||PLAN_MONTH , " + "\n");

            for (int i = 0; i < dtOper.Rows.Count; i++)
            {
                strSqlString.AppendFormat("        SUM(DECODE(DATA_2, '" + dtOper.Rows[i][0].ToString() + "', CNT, 0)) AS " + dtOper.Rows[i][0].ToString());
                if (i != dtOper.Rows.Count - 1)
                {
                    strSqlString.AppendFormat("," + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("\n");
                }
            }
            strSqlString.AppendFormat("    FROM   (SELECT SUBSTR(B.PLAN_YEAR,3,2) AS PLAN_YEAR, DECODE(LENGTH(B.PLAN_MONTH),2, TO_CHAR(B.PLAN_MONTH), '0'||B.PLAN_MONTH) AS PLAN_MONTH , A.* " + "\n");
            strSqlString.AppendFormat("              FROM   (SELECT SUBSTR(STS.ISSUE_DATE, 0, 8) AS WORK_DAY, DATA_2, COUNT(*) AS CNT " + "\n");
            strSqlString.AppendFormat("                        FROM CCSTISUSTS@RPTTOMES STS , " + "\n");
            strSqlString.AppendFormat("                             MGCMTBLDAT@RPTTOMES GCM " + "\n");
            strSqlString.AppendFormat("                       WHERE STS.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.AppendFormat("                         AND STS.FACTORY = GCM.FACTORY " + "\n");
            strSqlString.AppendFormat("                         AND STS.ISSUE_DATE BETWEEN '" + cdvBaseDate.Value.AddMonths(-5).ToString("yyyyMM") + "01000000' AND '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "235959' " + "\n");
            strSqlString.AppendFormat("                         AND GCM.TABLE_NAME = 'OPER_DEFINE_QC' " + "\n");
            strSqlString.AppendFormat("                         AND STS.OPER = GCM.KEY_1 " + "\n");
            strSqlString.AppendFormat("                      GROUP BY SUBSTR(STS.ISSUE_DATE, 0, 8), DATA_2 " + "\n");
            strSqlString.AppendFormat("                      ORDER BY SUBSTR(STS.ISSUE_DATE, 0, 8) ) A, " + "\n");
            strSqlString.AppendFormat("                     (SELECT * " + "\n");
            strSqlString.AppendFormat("                        FROM   MWIPCALDEF@RPTTOMES " + "\n");
            strSqlString.AppendFormat("                       WHERE  CALENDAR_ID = 'QC' " + "\n");
            strSqlString.AppendFormat("                         AND    SYS_DATE BETWEEN '" + cdvBaseDate.Value.AddMonths(-5).ToString("yyyyMM") + "01' AND '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' ) B " + "\n");
            strSqlString.AppendFormat("                       WHERE  A.WORK_DAY(+) = B.SYS_DATE " + "\n");
            strSqlString.AppendFormat("                      ORDER BY B.SYS_DATE ) " + "\n");
            strSqlString.AppendFormat(" GROUP BY PLAN_YEAR||PLAN_MONTH " + "\n");
            strSqlString.AppendFormat(" ORDER BY PLAN_YEAR||PLAN_MONTH  " + "\n");
            return strSqlString.ToString();
        }

        // 품질 Issue 주별 건수 
        private string MakeChartWeek()
        {


            dtDay = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDay());
            string day6ago = dtDay.Rows[0][0].ToString();


            StringBuilder strSqlString = new StringBuilder();
            strSqlString.AppendFormat(" SELECT PLAN_YEAR||PLAN_WEEK , " + "\n");

            for (int i = 0; i < dtOper.Rows.Count; i++)
            {
                strSqlString.AppendFormat("        SUM(DECODE(DATA_2, '" + dtOper.Rows[i][0].ToString() + "', CNT, 0)) AS " + dtOper.Rows[i][0].ToString());
                if (i != dtOper.Rows.Count - 1)
                {
                    strSqlString.AppendFormat("," + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("\n");
                }
            }
            strSqlString.AppendFormat("    FROM   (SELECT SUBSTR(B.PLAN_YEAR,3,2) AS PLAN_YEAR,DECODE(LENGTH(B.PLAN_WEEK),2,TO_CHAR(B.PLAN_WEEK),'0'||B.PLAN_WEEK) AS PLAN_WEEK, A.* " + "\n");
            strSqlString.AppendFormat("              FROM   (SELECT SUBSTR(STS.ISSUE_DATE, 0, 8) AS WORK_DAY, DATA_2, COUNT(*) AS CNT " + "\n");
            strSqlString.AppendFormat("                        FROM CCSTISUSTS@RPTTOMES STS , " + "\n");
            strSqlString.AppendFormat("                             MGCMTBLDAT@RPTTOMES GCM " + "\n");
            strSqlString.AppendFormat("                       WHERE STS.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.AppendFormat("                         AND STS.FACTORY = GCM.FACTORY " + "\n");
            strSqlString.AppendFormat("                         AND STS.ISSUE_DATE BETWEEN '" + day6ago + "000000' AND '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "235959' " + "\n");
            strSqlString.AppendFormat("                         AND GCM.TABLE_NAME = 'OPER_DEFINE_QC' " + "\n");
            strSqlString.AppendFormat("                         AND STS.OPER = GCM.KEY_1 " + "\n");
            strSqlString.AppendFormat("                      GROUP BY SUBSTR(STS.ISSUE_DATE, 0, 8), DATA_2 " + "\n");
            strSqlString.AppendFormat("                      ORDER BY SUBSTR(STS.ISSUE_DATE, 0, 8) ) A, " + "\n");
            strSqlString.AppendFormat("                     (SELECT * " + "\n");
            strSqlString.AppendFormat("                        FROM   MWIPCALDEF@RPTTOMES " + "\n");
            strSqlString.AppendFormat("                       WHERE  CALENDAR_ID = 'QC' " + "\n");
            strSqlString.AppendFormat("                         AND    SYS_DATE BETWEEN '" + day6ago + "' AND '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' ) B " + "\n");
            strSqlString.AppendFormat("                       WHERE  A.WORK_DAY(+) = B.SYS_DATE " + "\n");
            strSqlString.AppendFormat("                      ORDER BY B.SYS_DATE ) " + "\n");
            strSqlString.AppendFormat(" GROUP BY PLAN_YEAR||PLAN_WEEK " + "\n");
            strSqlString.AppendFormat(" ORDER BY PLAN_YEAR||PLAN_WEEK  " + "\n");
            return strSqlString.ToString();
        }

        // 월별 품질 Issue 일별 건수 
        private string MakeChartDay()
        {
            StringBuilder strSqlString = new StringBuilder();


            strSqlString.AppendFormat(" SELECT SUBSTR(SYS_DATE,5,4) , " + "\n");

            for (int i = 0; i < dtOper.Rows.Count; i++)
            {
                strSqlString.AppendFormat("        SUM(DECODE(DATA_2, '" + dtOper.Rows[i][0].ToString() + "', CNT, 0)) AS " + dtOper.Rows[i][0].ToString());
                if (i != dtOper.Rows.Count-1)
                {
                    strSqlString.AppendFormat(","+ "\n");
                }
                else
                {
                    strSqlString.AppendFormat("\n");
                }
            }
            strSqlString.AppendFormat("    FROM   (SELECT B.SYS_DATE, A.* " + "\n");
            strSqlString.AppendFormat("              FROM   (SELECT SUBSTR(STS.ISSUE_DATE, 0, 8) AS WORK_DAY, DATA_2, COUNT(*) AS CNT " + "\n");
            strSqlString.AppendFormat("                        FROM CCSTISUSTS@RPTTOMES STS , " + "\n");
            strSqlString.AppendFormat("                             MGCMTBLDAT@RPTTOMES GCM " + "\n");
            strSqlString.AppendFormat("                       WHERE STS.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.AppendFormat("                         AND STS.FACTORY = GCM.FACTORY " + "\n");
            strSqlString.AppendFormat("                         AND STS.ISSUE_DATE BETWEEN '" + cdvBaseDate.Value.AddDays(-14).ToString("yyyyMMdd") + "000000' AND '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "235959' " + "\n");
            strSqlString.AppendFormat("                         AND GCM.TABLE_NAME = 'OPER_DEFINE_QC' " + "\n");
            strSqlString.AppendFormat("                         AND STS.OPER = GCM.KEY_1 " + "\n");
            strSqlString.AppendFormat("                      GROUP BY SUBSTR(STS.ISSUE_DATE, 0, 8), DATA_2 " + "\n");
            strSqlString.AppendFormat("                      ORDER BY SUBSTR(STS.ISSUE_DATE, 0, 8) ) A, " + "\n");
            strSqlString.AppendFormat("                     (SELECT * " + "\n");
            strSqlString.AppendFormat("                        FROM   MWIPCALDEF@RPTTOMES " + "\n");
            strSqlString.AppendFormat("                       WHERE  CALENDAR_ID = 'QC' " + "\n");
            strSqlString.AppendFormat("                         AND    SYS_DATE BETWEEN '" + cdvBaseDate.Value.AddDays(-14).ToString("yyyyMMdd") + "' AND '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' ) B " + "\n");
            strSqlString.AppendFormat("                       WHERE  A.WORK_DAY(+) = B.SYS_DATE " + "\n");
            strSqlString.AppendFormat("                      ORDER BY B.SYS_DATE ) " + "\n");
            strSqlString.AppendFormat(" GROUP BY SYS_DATE " + "\n");
            strSqlString.AppendFormat(" ORDER BY SYS_DATE  " + "\n");
            return strSqlString.ToString();
        }

        // 당월 공정별 품질 Issue 발생 건
        private string MakeChartThisMon()
        {
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.AppendFormat(" SELECT B.DATA_3, NVL(CNT,0)" + "\n"); 
            strSqlString.AppendFormat("   FROM (" + "\n");
            strSqlString.AppendFormat("         SELECT DATA_3" + "\n");
            strSqlString.AppendFormat("              , COUNT(*) AS CNT" + "\n");
            strSqlString.AppendFormat("           FROM CCSTISUSTS@RPTTOMES STS ," + "\n");
            strSqlString.AppendFormat("                MGCMTBLDAT@RPTTOMES GCM" + "\n");
            strSqlString.AppendFormat("          WHERE STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.AppendFormat("            AND STS.FACTORY = GCM.FACTORY" + "\n");
            strSqlString.AppendFormat("            AND STS.ISSUE_DATE BETWEEN '" + cdvBaseDate.Value.ToString("yyyyMM") + "01000000' AND '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "235959' " + "\n");
            strSqlString.AppendFormat("            AND GCM.TABLE_NAME = 'OPER_DEFINE_QC'" + "\n");
            strSqlString.AppendFormat("            AND STS.OPER = GCM.KEY_1" + "\n");
            strSqlString.AppendFormat("         GROUP BY DATA_3" + "\n");
            strSqlString.AppendFormat("        ) A," + "\n");
            strSqlString.AppendFormat("        (" + "\n");
            strSqlString.AppendFormat("         SELECT DISTINCT DATA_3 FROM MGCMTBLDAT@RPTTOMES GCM" + "\n");
            strSqlString.AppendFormat("          WHERE GCM.TABLE_NAME = 'OPER_DEFINE_QC'" + "\n");
            strSqlString.AppendFormat("            AND GCM.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.AppendFormat("        ) B" + "\n");
            strSqlString.AppendFormat("  WHERE A.DATA_3(+) = B.DATA_3" + "\n");
            strSqlString.AppendFormat(" ORDER BY DECODE(B.DATA_3, 'DP', 1, 'DA', 2, 'WB', 3, 'MD', 4, 'FINISH', 5, '기타', 6, 7)" + "\n");
            
            return strSqlString.ToString();
        }

        // 당월 공정별 품질 Issue 발생 비율
        private string MakeChartThisMonRatio()
        {
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.AppendFormat(" SELECT CAUSE_TYPE, ROUND((RATIO_TO_REPORT(COUNT(CAUSE_TYPE)) OVER()) * 100,1) AS RATIO" + "\n"); 
            strSqlString.AppendFormat("   FROM CCSTISUSTS@RPTTOMES STS" + "\n");
            strSqlString.AppendFormat("      , CCSTISUDAT@RPTTOMES DAT" + "\n");
            strSqlString.AppendFormat("  WHERE STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.AppendFormat("    AND STS.FACTORY = DAT.FACTORY" + "\n");
            strSqlString.AppendFormat("    AND STS.ISSUE_DATE BETWEEN '" + cdvBaseDate.Value.ToString("yyyyMM") + "01000000' AND '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "235959' " + "\n");
            strSqlString.AppendFormat("    AND STS.ISSUE_NO = DAT.ISSUE_NO" + "\n");
            strSqlString.AppendFormat("    AND CAUSE_TYPE  <> ' '" + "\n");
            strSqlString.AppendFormat("GROUP BY CAUSE_TYPE" + "\n");

            return strSqlString.ToString();
        }

        private void fnMakeChart(int columnCount)
        {
            // 차트 설정
            
            cf01.RPT_2_ClearData();
            cf01.Stacked = SoftwareFX.ChartFX.Stacked.Normal;
            cf01.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            cf01.DataSource = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartMon());
            cf01.RPT_5_CloseData();
            cf01.SerLegBox = true;
            cf01.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            cf01.LegendBox = false;
            cf01.PointLabels = false;
            cf01.AxisX.Staggered = false;
            cf01.AxisY.Gridlines = false;
            cf01.AxisX.Font = new Font("Arial", 6, FontStyle.Bold);
            cf01.AxisY.Max = cf01.AxisY.Max * 1.5;
            cf01.AxisY.Step = 1;
            cf01.AxisY.LabelsFormat.Decimals = 0;
            cf01.Scrollable = false;
            

            cf02.RPT_2_ClearData();
            cf02.Stacked = SoftwareFX.ChartFX.Stacked.Normal;
            cf02.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            cf02.DataSource = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartWeek());
            cf02.RPT_5_CloseData();
            cf02.SerLegBox = true;
            cf02.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            cf02.LegendBox = false;
            cf02.PointLabels = false;
            cf02.AxisX.Staggered = false;
            cf02.AxisY.Gridlines = false;
            cf02.AxisY.Step = 1;
            cf02.AxisY.LabelsFormat.Decimals = 0;
            cf02.Scrollable = false;
            cf02.AxisY.Max = cf02.AxisY.Max * 1.5;
            cf02.AxisX.Font = new Font("Arial", 6, FontStyle.Bold);

            cf03.RPT_2_ClearData();
            cf03.Stacked = SoftwareFX.ChartFX.Stacked.Normal;
            cf03.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            cf03.DataSource = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartDay());
            cf03.RPT_5_CloseData();
            cf03.SerLegBox = true;
            cf03.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            cf03.LegendBox = false;
            cf03.PointLabels = false;
            cf03.AxisX.Staggered = false;
            cf03.AxisY.Gridlines = false;
            cf03.AxisY.Step = 1;
            cf03.AxisY.LabelsFormat.Decimals = 0;
            cf03.Scrollable = false;
            cf03.AxisX.Font = new Font("Arial", 6, FontStyle.Bold);
            cf03.AxisY.Max = cf03.AxisY.Max * 1.5;

            cf04.RPT_2_ClearData();
            cf04.Stacked = SoftwareFX.ChartFX.Stacked.Normal;
            cf04.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            cf04.DataSource = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartThisMon());
            cf04.RPT_5_CloseData();
            cf04.SerLegBox = false;
            cf04.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            cf04.LegendBox = false;
            cf04.PointLabels = false;
            cf04.AxisX.Staggered = false;
            cf04.AxisY.Gridlines = false;
            cf04.AxisY.Step = 1;
            cf04.AxisY.LabelsFormat.Decimals = 0;
            cf04.Scrollable = false;
            cf04.AxisX.Font = new Font("Arial", 7, FontStyle.Bold);
            cf04.AxisY.Max = cf04.AxisY.Max * 1.5;
            cf04.Titles[0].Text = cdvBaseDate.Value.ToString("MM") + "월 공정별 발생 건수"; 


            cf05.RPT_2_ClearData();
            cf05.Stacked = SoftwareFX.ChartFX.Stacked.Normal;
            cf05.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
            cf05.DataSource = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartThisMonRatio());
            cf05.RPT_5_CloseData();
            cf05.SerLegBox = false;
            cf05.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            cf05.LegendBox = true;
            cf05.PointLabels = true;
            cf05.AxisX.Staggered = true;
            cf05.AxisY.Gridlines = true;
            cf05.AxisY.Step = 1;
            cf05.AxisY.LabelsFormat.Decimals = 0;
            cf05.Scrollable = false;
            cf05.AxisX.Font = new Font("Arial", 15, FontStyle.Bold);
            cf05.AxisY.Max = cf05.AxisY.Max * 1.5;
            cf05.Titles[0].Text = cdvBaseDate.Value.ToString("MM") + "월 사고 발생 4M 비율"; 
            cf05.Titles[0].Alignment = StringAlignment.Center;
       
        }


      

        #endregion


        #region " Form Event "

        
        private void btnView_Click(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : View Button을 클릭하면
             * 
             * Created By : bee-jae jung(2010-05-11-화요일)
             * 
             * Modified By : bee-jae jung(2010-05-11-화요일)
             ****************************************************/
          
            // 공정별 정의 가져오기 
            dtOper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlOper());
 
            try
            {
                fnMakeChart(0);
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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : Excel Export Button을 클릭하면
             * 
             * Created By : bee-jae jung(2010-05-11-화요일)
             * 
             * Modified By : bee-jae jung(2010-05-11-화요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

               
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

        #endregion

              


    }
}
