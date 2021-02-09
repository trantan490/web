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
    public partial class PRD010419 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010419<br/>
        /// 클래스요약: 단위 공정별 activity 조회<br/>
        /// 작  성  자: 하나마이크론 김민우<br/>
        /// 최초작성일: 2012-06-05<br/>
        /// 상세  설명: 단위 공정별 activity 조회(임태성K 요청)<br/>
        /// 변경  내용: <br/>  
        /// SAMPLE: 2012-03-20-김민우 CANCEL_FLAG 추가 및 CANCEL_FLAG = 'Y' 인 것은 붉은 색으로 표시(정비재C 요청)
        /// </summary>
        public PRD010419()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                InitializeComponent();
                SortInit();
                GridColumnInit();
                lblTitle.Text = GlobalVariable.gsSelFuncName;
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

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvCustomer.Text.Equals("") || cdvCustomer.Text.Equals("ALL"))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD038", GlobalVariable.gcLanguage));
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {            
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("SAP Item Code (BG)", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SAP item code (ASSY, TEST)", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MES PART No", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("FLOW", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Routing (STEP)", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Operation Name", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Average UPEH", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("ST/1000ea", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 80);
                spdData.RPT_AddBasicColumn("the number per person", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("LABR-F", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 80);
                spdData.RPT_AddBasicColumn("DEPRE", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 80);
                spdData.RPT_AddBasicColumn("LABR-V", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("UTMAT", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 80);
                spdData.RPT_AddBasicColumn("OTHER", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("PACK", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
            finally
            {
                Cursor.Current = Cursor.Current;
            }
        }
        #endregion


        #region " Common Function "
        
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns> 
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();


            strSqlString.Append("SELECT VENDOR_MAT_ID_BG,BASE_MAT_ID, A.MAT_ID, FIRST_FLOW, A.OPER, A.OPER_DESC, B.UPEH " + "\n");
            strSqlString.Append("       , ROUND(DECODE(B.UPEH,0,0,1000/B.UPEH),3) AS ST " + "\n");
            strSqlString.Append("       , A.OPER_CMF_6 " + "\n");
            strSqlString.Append("       , ROUND(DECODE(A.OPER_CMF_6,' ','',ROUND(DECODE(B.UPEH,0,0,1000/B.UPEH),3)/ A.OPER_CMF_6),3) AS LABR_F " + "\n");
            strSqlString.Append("       , ROUND(DECODE(B.UPEH,0,0,1000/B.UPEH),3) AS DEPRE " + "\n");
            strSqlString.Append("       , ROUND(DECODE(A.OPER_CMF_6,' ','',ROUND(DECODE(B.UPEH,0,0,1000/B.UPEH),3)/ A.OPER_CMF_6),2) AS LABR_V " + "\n");
            strSqlString.Append("       , ROUND(DECODE(B.UPEH,0,0,1000/B.UPEH),3) AS UTMAT " + "\n");
            strSqlString.Append("       , ROUND(DECODE(A.OPER_CMF_6,' ','',ROUND(DECODE(B.UPEH,0,0,1000/B.UPEH),3)/ A.OPER_CMF_6),2) AS OTHER " + "\n");
            strSqlString.Append("  FROM ( " + "\n");

            strSqlString.Append("        SELECT VENDOR_MAT_ID AS VENDOR_MAT_ID_BG,BASE_MAT_ID,MAT.MAT_ID,MAT.FIRST_FLOW " + "\n");
            strSqlString.Append("             , OPR.OPER, OPR.OPER_DESC, OPR.OPER_CMF_6 " + "\n");
            strSqlString.Append("          FROM MWIPMATDEF MAT, " + "\n");
            strSqlString.Append("               MWIPFLWOPR@RPTTOMES FLW, " + "\n");
            strSqlString.Append("               MWIPOPRDEF OPR " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND MAT.FACTORY = FLW.FACTORY " + "\n");
            strSqlString.Append("           AND MAT.FACTORY = OPR.FACTORY " + "\n");
            strSqlString.Append("           AND MAT.MAT_GRP_1 = '" + cdvCustomer.Text + "'" + "\n");
            strSqlString.Append("           AND MAT.MAT_VER = 1 " + "\n");
            strSqlString.Append("           AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND MAT.FIRST_FLOW = FLW.FLOW " + "\n");
            strSqlString.Append("           AND FLW.OPER = OPR.OPER " + "\n");
            strSqlString.Append("           AND (VENDOR_MAT_ID LIKE '" + txtSapOrderId.Text + "'" + "\n");
            strSqlString.Append("                OR BASE_MAT_ID LIKE '" + txtSapOrderId.Text + "')" + "\n");
            if (cdvOper.Text != "ALL" && cdvOper.Text != "")
                strSqlString.Append("           AND OPR.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            strSqlString.Append("       ) A, " + "\n"); 
            strSqlString.Append("       (SELECT MAT_ID,OPER, ROUND(SUM(UPEH)/COUNT(*)) AS UPEH FROM CRASUPHDEF " + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("        GROUP BY MAT_ID,OPER " + "\n");
            strSqlString.Append("       ) B " + "\n");
            strSqlString.Append("   WHERE A.OPER = B.OPER(+) " + "\n");
            strSqlString.Append("     AND A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("ORDER BY A.MAT_ID, A.OPER  " + "\n");
          
            /*
            strSqlString.Append("SELECT VENDOR_MAT_ID AS VENDOR_MAT_ID_BG,BASE_MAT_ID,MAT.MAT_ID,MAT.FIRST_FLOW " + "\n");
            strSqlString.Append("       , OPR.OPER, OPR.OPER_DESC, UPH.UPEH, ROUND(DECODE(UPH.UPEH,0,0,1000/UPH.UPEH),3) AS ST " + "\n");
            strSqlString.Append("       , OPR.OPER_CMF_6 " + "\n");
            strSqlString.Append("       , ROUND(DECODE(OPR.OPER_CMF_6,' ','',ROUND(DECODE(UPH.UPEH,0,0,1000/UPH.UPEH),3)/ OPR.OPER_CMF_6),3) AS LABR_F " + "\n");
            strSqlString.Append("       , ROUND(DECODE(UPH.UPEH,0,0,1000/UPH.UPEH),3) AS DEPRE " + "\n");
            strSqlString.Append("       , ROUND(DECODE(OPR.OPER_CMF_6,' ','',ROUND(DECODE(UPH.UPEH,0,0,1000/UPH.UPEH),3)/ OPR.OPER_CMF_6),2) AS LABR_V " + "\n");
            strSqlString.Append("       , ROUND(DECODE(UPH.UPEH,0,0,1000/UPH.UPEH),3) AS UTMAT " + "\n");
            strSqlString.Append("       , ROUND(DECODE(OPR.OPER_CMF_6,' ','',ROUND(DECODE(UPH.UPEH,0,0,1000/UPH.UPEH),3)/ OPR.OPER_CMF_6),2) AS OTHER " + "\n");
            strSqlString.Append("  FROM MWIPMATDEF MAT, " + "\n");
            strSqlString.Append("       MWIPFLWOPR@RPTTOMES FLW, " + "\n");
            strSqlString.Append("       MWIPOPRDEF OPR, " + "\n");
            strSqlString.Append("       (SELECT MAT_ID,OPER, ROUND(SUM(UPEH)/COUNT(*)) AS UPEH FROM CRASUPHDEF " + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n"); 
            strSqlString.Append("        GROUP BY MAT_ID,OPER " + "\n");
            strSqlString.Append("       ) UPH " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND MAT.FACTORY = FLW.FACTORY " + "\n");
            strSqlString.Append("   AND MAT.FACTORY = OPR.FACTORY " + "\n");
            strSqlString.Append("   AND MAT.MAT_GRP_1 = '" + cdvCustomer.Text + "'" + "\n");
            strSqlString.Append("   AND MAT.MAT_VER = 1 " + "\n");
            strSqlString.Append("   AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND MAT.FIRST_FLOW = FLW.FLOW " + "\n");
            strSqlString.Append("   AND FLW.OPER = OPR.OPER " + "\n");
            strSqlString.Append("   AND FLW.OPER = UPH.OPER " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = UPH.MAT_ID " + "\n");
            strSqlString.Append("   AND (VENDOR_MAT_ID LIKE '" + txtSapOrderId.Text + "'" + "\n");
            strSqlString.Append("        OR BASE_MAT_ID LIKE '" + txtSapOrderId.Text + "')" + "\n");
            if (cdvOper.Text != "ALL" && cdvOper.Text != "")
            strSqlString.Append("   AND OPR.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            strSqlString.Append("ORDER BY MAT.MAT_ID, OPR.OPER  " + "\n");
            */

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();

        }

        #endregion

        #region " Controls Event "

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;

            GridColumnInit();

            spdData_Sheet1.RowCount = 0;

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
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                spdData.ExportExcel();
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
        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvOper.sFactory = cdvFactory.txtValue;
            cdvCustomer.sFactory = cdvFactory.txtValue;
        }
    }
}
