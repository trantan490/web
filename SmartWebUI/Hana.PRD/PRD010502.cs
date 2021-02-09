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
    public partial class PRD010502 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private string strSelect;
        private string strDecode;

        /// <summary>
        /// 클  래  스: PRD010502<br/>
        /// 클래스요약: 설비 Arrange 공정,설비 기준<br/>
        /// 작  성  자: 미라콤 양형석<br/>
        /// 최초작성일: 2008-12-10<br/>
        /// 상세  설명: 설비 Arrange 공정,설비 기준.<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2010-07-13 김민우 : 설비 대수를 나타낼 때 한 설비가 여러공정에서 사용가능하면 사용가능한 모든 공정에 설비 대수가 중복으로 표기되어
        //                      설비 대수의 왜곡이 발생 따라서 쿼리를 수정함. MRASRESDEF 테이블의 RES_STS_9 컬럼에 현재 설비가 설정된 공정 기준으로
        //                      설비 대수를 카운트함
        /// 2010-07-28-임종우 : A1300 공정 표시 되도록 추가함(임태성 요청), 원부자재 공정 제거함.
        /// 2011-05-30-임종우 : 과거 데이터 조회 가능하도록 수정 (김보람 요청)
        /// 2011-11-29-임종우 : 설비 상세 리스트 팝업 창 추가 (황선미 요청)
        /// 2012-02-01-김민우 : MRASRESSTS의 공정정보 RES_STS_9에서 RES_STS_8로 변경
        /// 2012-10-04-임종우 : 팝업 창 순서 변경시 오류 부분 수정 (김권수 요청)
        /// </summary>
        public PRD010502()
        {
            InitializeComponent();

            
            SortInit();
            GridColumnInit();

            cdvDate.Value = DateTime.Today;
            cboTimeBase.Text = DateTime.Now.ToString("HH") + " " + LanguageFunction.FindLanguage("hours", 0);
            strSelect = string.Empty;
            strDecode = string.Empty;

            cdvStep.Text = "";
            int a = cdvStep.ValueColumns.Count;
        }

        #region " Constant Definition "

        #endregion

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvStep.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }

            // 2011-05-30-임종우 : MRASRESDEF_BOH 테이블 2011년 5월 30일 11시에 생성 하여 11시 데이터 부터 Snapshot 시작 함.
            //string strDate = cdvDate.Value.ToString("yyyyMMdd") + cboTimeBase.Text.Replace("시", "");
            string strDate = cdvDate.Value.ToString("yyyyMMdd") + cboTimeBase.Text.Substring(0, 2);

            if (Convert.ToInt32(strDate) < 2011053011)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD058", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

            int pos = spdData.ActiveSheet.Columns.Count;
            for (int i = 0; i < pos; i++)
            {
                spdData.RPT_MerageHeaderRowSpan(0, i, 2);
            }

            spdData.RPT_AddDynamicColumn(cdvStep, true, new string[] { "Equipment Model", "대수", "CAPA" }, 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, new Formatter[] { Formatter.String, Formatter.Number, Formatter.Number }, 70);
            
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "DATA_1 as Customer", "DATA_1", "GCM.DATA_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2 as Family", "MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3 as Package", "MAT_GRP_3", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4 as Type1", "MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5 as Type2", "MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6 as LD_Count", "MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7 as Density", "MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8 as Generation", "MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Production", "MAT_ID as Product", "MAT_ID", "MAT.MAT_ID", true);
        }
        
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            strSelect = string.Empty;
            strDecode = string.Empty;

            if (!cdvStep.Text.Equals("ALL"))
            {
                for (int i = 0; i < cdvStep.ValueItems.Count; i++)
                {
                    if (cdvStep.ValueItems[i].Checked)
                    {
                        strSelect += string.Format("     ,MODEL_{0}, CNT_{0}, CAPA_{0}" + "\n", cdvStep.ValueItems[i].SubItems[0].Text);
                        strDecode += string.Format("     , DECODE(CAP.OPER, '{0}', CAP.RES_MODEL, ' ') AS MODEL_{0} " + "\n", cdvStep.ValueItems[i].SubItems[0].Text);
                        strDecode += string.Format("     , DECODE(CAP.OPER, '{0}', CAP.EQP_CNT, 0) AS CNT_{0} " + "\n", cdvStep.ValueItems[i].SubItems[0].Text);
                        strDecode += string.Format("     , DECODE(CAP.OPER, '{0}', CAP.CAPA, 0) AS CAPA_{0} " + "\n", cdvStep.ValueItems[i].SubItems[0].Text);
                    }
                }
            }
            else
            {
                for (int i = 0; i < cdvStep.ValueItems.Count; i++)
                {
                    strSelect += string.Format("     ,MODEL_{0}, CNT_{0}, CAPA_{0}" + "\n", cdvStep.ValueItems[i].SubItems[0].Text);
                    strDecode += string.Format("     , DECODE(CAP.OPER, '{0}', CAP.RES_MODEL, ' ') AS MODEL_{0} " + "\n", cdvStep.ValueItems[i].SubItems[0].Text);
                    strDecode += string.Format("     , DECODE(CAP.OPER, '{0}', CAP.EQP_CNT, 0) AS CNT_{0} " + "\n", cdvStep.ValueItems[i].SubItems[0].Text);
                    strDecode += string.Format("     , DECODE(CAP.OPER, '{0}', CAP.CAPA, 0) AS CAPA_{0} " + "\n", cdvStep.ValueItems[i].SubItems[0].Text);
                }
            }


            //string strDate = cdvDate.Value.ToString("yyyyMMdd") + cboTimeBase.Text.Replace("시", "");
            string strDate = cdvDate.Value.ToString("yyyyMMdd") + cboTimeBase.Text.Substring(0, 2);
            
            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
            strSqlString.AppendFormat("{0} " + "\n", strDecode.Replace("CAP.CAPA", "TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.7 * RES.EQP_CNT, 0))").Replace("CAP.", "RES."));
            strSqlString.Append("  FROM (  " + "\n");

            // 2010-07-13 김민우 : 설비 대수를 나타낼 때 한 설비가 여러공정에서 사용가능하면 사용가능한 모든 공정에 설비 대수가 중복으로 표기되어
            //                     설비 대수의 왜곡이 발생 따라서 쿼리를 수정함. MRASRESDEF 테이블의 RES_STS_9 컬럼에 현재 설비가 설정된 공정 기준으로
            //                     설비 대수를 카운트함
            strSqlString.Append("        SELECT FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS EQP_CNT " + "\n");

            // 2011-05-30-임종우 : 과거 데이터 조회 가능하도록 수정 (김보람 요청)
            if (DateTime.Now.ToString("yyyyMMddHH").Equals(strDate)) // 현재 시간
            {
                strSqlString.Append("          FROM MRASRESDEF " + "\n");
                strSqlString.Append("         WHERE 1 = 1  " + "\n");
            }
            else
            {
                strSqlString.Append("          FROM MRASRESDEF_BOH " + "\n");
                strSqlString.Append("         WHERE 1 = 1  " + "\n");
                strSqlString.Append("           AND CUTOFF_DT = '" + strDate + "'" + "\n");
            }
            
            strSqlString.Append("           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND RES_STS_8 " + cdvStep.SelectedValueToQueryString + "\n");
            strSqlString.Append("         GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7 " + "\n");
            strSqlString.Append("       ) RES " + "\n");
            strSqlString.Append("     , CRASUPHDEF UPH " + "\n");
            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("     , MGCMTBLDAT GCM " + "\n");
            strSqlString.Append(" WHERE 1 = 1 " + "\n");

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.Append("  AND MAT.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("  AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("  AND GCM.TABLE_NAME = 'H_CUSTOMER'    " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("  AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("  AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("  AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("  AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("  AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("  AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("  AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("  AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("  AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion
            strSqlString.Append("  AND RES.FACTORY = UPH.FACTORY(+) " + "\n");
            strSqlString.Append("  AND RES.OPER = UPH.OPER(+) " + "\n");
            strSqlString.Append("  AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("  AND RES.UPEH_GRP = UPH.UPEH_GRP(+) " + "\n");
            strSqlString.Append("  AND RES.RES_STS_2 = UPH.MAT_ID(+) " + "\n");
            strSqlString.Append("  AND RES.FACTORY = MAT.FACTORY    " + "\n");
            strSqlString.Append("  AND MAT.FACTORY = GCM.FACTORY    " + "\n");
            strSqlString.Append("  AND RES.RES_STS_2 = MAT.MAT_ID     " + "\n");
            strSqlString.Append("  AND MAT.MAT_GRP_1 = GCM.KEY_1 " + "\n");

            strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond3);            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>
        private void MakeChart(DataTable DT)
        {
            
        }

        #endregion

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

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 9, null, null, btnSort);
                //spdData.DataSource = dt;

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);


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

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvStep.sFactory = cdvFactory.txtValue;

            cdvStep.SetChangedFlag(true);
            cdvStep.Text = "";

            string strQuery = string.Empty;
            //strQuery += "SELECT DISTINCT OPER CODE, (SELECT OPER_DESC FROM MWIPOPRDEF WHERE FACTORY = LTH.FACTORY AND OPER = LTH.OPER AND ROWNUM = 1) DATA " + "\n";
            //strQuery += "  FROM MRASRESLTH LTH " + "\n";
            //strQuery += " WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n";
            //strQuery += "   AND TRAN_TIME BETWEEN '" + cdvDate.Value.AddDays(-1).ToString("yyyyMMdd") + "220000" + "' AND '" + cdvDate.Value.ToString("yyyyMMdd") + "220000" + "' " + "\n";
            //strQuery += " ORDER BY OPER " + "\n";


            strQuery += "SELECT DISTINCT OPR.OPER CODE, OPR.OPER_DESC DATA" + "\n";
            strQuery += "  FROM MRASRESMFO RES, MWIPOPRDEF OPR " + "\n";
            strQuery += " WHERE RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n";
            strQuery += "   AND REL_LEVEL='R' " + "\n";

            if (cdvFactory.txtValue.Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                //2010-07-28-임종우 : A1300 공정 표시 되도록 추가함(임태성 요청), 원부자재 공정 제거함.
                strQuery += "   AND RES.OPER NOT IN ('A0100','A0150','A0250','A0320','A0330','A0340','A0350','A0370','A0380','A0390','A0500','A0501',";
                strQuery += "'A0502','A0503','A0504','A0505','A0506','A0507','A0508','A0509','A0550','A0950','A1100','A1110','A1180','A1230','A1720','A1950', 'A2050', 'A2100')" + "\n";
                strQuery += "   AND OPR.OPER LIKE 'A%'" + "\n";
            }
            else
            {
                strQuery += "   AND RES.OPER IN ('T0100','T0650','T0900','T1040','T1080','T1200')" + "\n";
            }

            strQuery += "   AND RES.FACTORY=OPR.FACTORY " + "\n";
            strQuery += "   AND RES.OPER=OPR.OPER " + "\n";
            strQuery += "ORDER BY OPR.OPER " + "\n";


            if (cdvFactory.txtValue != "")
                cdvStep.sDynamicQuery = strQuery;
            else
                cdvStep.sDynamicQuery = "";



        }

        #region MakeSqlCustomer
        //고객사 명으로 고객사 코드 가져오기 위해..
        private string MakeSqlCustomer(string customerName)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND DATA_1 = '" + customerName + "' AND ROWNUM=1" + "\n");

            return strSqlString.ToString();

        }
        #endregion

        #region MakeSqlDetail
        //상세 팝업창 쿼리
        private string MakeSqlDetail(string sRasModel, string sOper, string[] dataArry)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT RES_GRP_6 AS RES_MODEL" + "\n");
            strSqlString.AppendFormat("     , RES_ID" + "\n");
            strSqlString.AppendFormat("     , RES_UP_DOWN_FLAG AS STATUS" + "\n");
            strSqlString.AppendFormat("     , DECODE(RES_UP_DOWN_FLAG, 'D', C.DATA_1, '') AS DOWN_CODE" + "\n");
            strSqlString.AppendFormat("  FROM MRASRESDEF A" + "\n");
            strSqlString.AppendFormat("     , MWIPMATDEF B" + "\n");
            strSqlString.AppendFormat("     , MGCMTBLDAT C" + "\n");
            strSqlString.AppendFormat(" WHERE 1=1" + "\n");
            strSqlString.AppendFormat("   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.AppendFormat("   AND A.FACTORY = C.FACTORY " + "\n");
            strSqlString.AppendFormat("   AND A.RES_STS_2 = B.MAT_ID" + "\n");
            strSqlString.AppendFormat("   AND C.KEY_1 = SUBSTR(A.RES_STS_1, 1,1) " + "\n");
            strSqlString.AppendFormat("   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.AppendFormat("   AND A.RES_CMF_9 = 'Y' " + "\n");
            strSqlString.AppendFormat("   AND A.DELETE_FLAG = ' ' " + "\n");            
            strSqlString.AppendFormat("   AND A.RES_GRP_6 = '" + sRasModel + "'" + "\n");
            strSqlString.AppendFormat("   AND A.RES_STS_8 = '" + sOper + "'" + "\n");
            strSqlString.AppendFormat("   AND A.RES_TYPE = 'EQUIPMENT' " + "\n");
            strSqlString.AppendFormat("   AND C.TABLE_NAME = 'STOP_CODE' " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (dataArry[0] != " " && dataArry[0] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_1 = '" + dataArry[0] + "'" + "\n");

            if (dataArry[1] != " " && dataArry[1] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_2 = '" + dataArry[1] + "'" + "\n");

            if (dataArry[2] != " " && dataArry[2] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_3 = '" + dataArry[2] + "'" + "\n");

            if (dataArry[3] != " " && dataArry[3] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_4 = '" + dataArry[3] + "'" + "\n");

            if (dataArry[4] != " " && dataArry[4] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_5 = '" + dataArry[4] + "'" + "\n");

            if (dataArry[5] != " " && dataArry[5] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_6 = '" + dataArry[5] + "'" + "\n");

            if (dataArry[6] != " " && dataArry[6] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_7 = '" + dataArry[6] + "'" + "\n");

            if (dataArry[7] != " " && dataArry[7] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_8 = '" + dataArry[7] + "'" + "\n");

            if (dataArry[8] != " " && dataArry[8] != null)
                strSqlString.AppendFormat("   AND B.MAT_ID = '" + dataArry[8] + "'" + "\n");

            #endregion

            strSqlString.AppendFormat(" ORDER BY RES_GRP_6, RES_ID, RES_UP_DOWN_FLAG" + "\n");            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion MakeSqlDetail

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string[] dataArry = new string[9];
            string sRasModel = null;
            string sOper = null;

            Color BackColor = spdData.ActiveSheet.Cells[1, 10].BackColor;

            // GrandTotal 과 SubTotal 제외하기 위해
            if (e.Row != 0 && spdData.ActiveSheet.Cells[e.Row, e.Column].BackColor == BackColor)
            {
                if (spdData.ActiveSheet.Columns[e.Column].Label == "대수")
                {
                    // Group 정보 가져와서 담기... 상세 팝업에서 조건값으로 사용하기 위해
                    for (int i = 0; i < 9; i++)
                    {
                        if (spdData.ActiveSheet.Columns[i].Label == "Customer")
                            dataArry[0] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Family")
                            dataArry[1] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Package")
                            dataArry[2] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Type1")
                            dataArry[3] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Type2")
                            dataArry[4] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "LD Count")
                            dataArry[5] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Density")
                            dataArry[6] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Generation")
                            dataArry[7] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();                        

                        else
                            dataArry[8] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                    }

                    // 고객사 명을 고객사 코드로 변경하기 위해..
                    if (dataArry[0] != " ")
                    {
                        DataTable dtCustomer = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlCustomer(dataArry[0].ToString()));

                        dataArry[0] = dtCustomer.Rows[0][0].ToString();
                    }

                    sRasModel = spdData.ActiveSheet.Cells[e.Row, e.Column - 1].Value.ToString();
                    sOper = spdData.ActiveSheet.ColumnHeader.Cells[0, e.Column - 1].Text.Substring(0, 5);

                    DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(sRasModel, sOper, dataArry));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        System.Windows.Forms.Form frm = new PRD010502_P1("", dt);
                        frm.ShowDialog();
                    }
                }
            }
        }
    }
}
