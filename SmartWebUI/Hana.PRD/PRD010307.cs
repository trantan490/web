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
    public partial class PRD010307 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010307<br/>
        /// 클래스요약: 제공 실사 LIST 출력<br/>
        /// 작  성  자: 미라콤 양형석<br/>
        /// 최초작성일: 2008-12-06<br/>
        /// 상세  설명: 제공 실사 LIST 출력.<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2011-06-02-임종우 : 전체 로직 수정 함 (임태성 요청)
        /// 2011-07-05-임종우 : 금일 조회 시에도 TIME_BASE 선택 가능하도록 수정 (김동인 요청)
        /// 2013-10-14-김민우: LOT TYPE ALL, P%, E% 구분으로변경
        /// 2015-12-22-임종우 : 당일에 한해서 시간대별로 검색 가능하도록 변경 - 연말재고실사로 인해..
        /// 2016-03-28-임종우 : S/O NO 그룹 조건 추가 (이동진S 요청)
        /// 2018-03-23-임종우 : Egis 업체 PCB Strip, PCB BAD Mark 정보 추가 (백성호D 요청)
        /// </summary>
        public PRD010307()
        {
            InitializeComponent();
            //udcDurationDate1.AutoBinding();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리

            cdvDate.Value = DateTime.Today;
            //cboTimeBase.SelectedIndex = 22;  
            cboTimeBase.SelectedIndex = 21;          
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1 as Customer", "MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2 as Family", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3 as PKG", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4 as Type1", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5 as Type2", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6 as LEAD", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7 as Density", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8 as GEN", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Fab", "LOT_CMF_6 as FAB", "LOT_CMF_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT_CMF_10 as PIN_TYPE", "MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID AS PRODUCT", "MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "OPER || '(' || OPER_DESC || ')' AS Step", "OPER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Run No", "LOT_CMF_4 AS RUN_NO", "LOT_CMF_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot No", "LOT_ID AS LOT_NO", "LOT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT_CMF_7 AS CUST_DEVICE", "MAT_CMF_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("S/O NO", "LOT_CMF_3 AS SO_NO", "LOT_CMF_3", false);
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {            
            spdData.RPT_ColumnInit();

            if (ckMerge.Checked)
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LEAD", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DEN", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("GEN", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("FAB", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("STEP", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("RUN NO", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
            }
            else
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LEAD", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DEN", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("GEN", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("FAB", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("STEP", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("RUN NO", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.False, Formatter.String, 90);
            }

            spdData.RPT_AddBasicColumn("LOT_NO", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("CUST_DEVICE", 0, 14, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("SO_NO", 0, 15, Visibles.False, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("LOT TYPE", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("GOOD", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
            spdData.RPT_AddBasicColumn("REJECT", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
            spdData.RPT_AddBasicColumn("CV", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
            spdData.RPT_AddBasicColumn("Input quantity", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
            spdData.RPT_AddBasicColumn("RECV TIME", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("ISSUE TIME", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("Operation Date", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
            spdData.RPT_AddBasicColumn("Our company day", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);

            if (ckbPCB.Checked == true)
            {
                spdData.RPT_AddBasicColumn("PCB Strip", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("PCB BAD Mark", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            }

            //spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.String,60);
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
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

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                spdData.isShowZero = true;

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 17, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 17, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                
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
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvStep.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
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

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQuery;

            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            bool isRealTime = false;

            // 2011-07-05-임종우 : 금일 22시 조회시에만 실시간... 금일 06시 조회시 과거 데이터 사용.. (김동인 요청)
            //if (strDate.Equals(DateTime.Now.ToString("yyyyMMdd")) && cboTimeBase.Text == "22 hours")
            if (strDate.Equals(DateTime.Now.ToString("yyyyMMdd")) && cboTimeBase.SelectedIndex == 21)
            {
                strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                isRealTime = true;
            }
            else
            {
                strDate = strDate + cboTimeBase.Text.Substring(0, 2) + "0000";
                
                //if(cboTimeBase.Text == "06 hours")
                //    strDate = strDate + "060000";
                //else
                //    strDate = strDate + "220000";

                isRealTime = false;
            }

            strSqlString.AppendFormat("SELECT {0}  " + "\n", QueryCond1);
            strSqlString.Append("     , LOT_CMF_5 AS LOT_TYPE " + "\n");
            strSqlString.Append("     , QTY_1 AS GOOD " + "\n");

            if (isRealTime)
            {
                strSqlString.Append("     , '' AS REJECT  " + "\n");
                strSqlString.Append("     , '' AS CV  " + "\n");
            }
            else
            {
                strSqlString.Append("     , REJECT_QTY AS REJECT  " + "\n");
                strSqlString.Append("     , CV_QTY AS CV  " + "\n");
            }

            strSqlString.Append("     , CREATE_QTY_1 AS QTY  " + "\n");
            strSqlString.Append("     , DECODE(LOT_CMF_14, ' ', ' ', TO_CHAR(TO_DATE(LOT_CMF_14, 'YYYYMMDDHH24MISS'), 'YY/MM/DD HH24:MI:SS')) AS RECV_TIME  " + "\n");
            strSqlString.Append("     , DECODE(RESV_FIELD_1, ' ', ' ', TO_CHAR(TO_DATE(RESV_FIELD_1, 'YYYYMMDDHH24MISS'), 'YY/MM/DD HH24:MI:SS')) AS ISSUE_TIME " + "\n");
            strSqlString.AppendFormat("     , TRUNC(TO_DATE('{0}', 'YYYYMMDDHH24MISS') - TO_DATE(NVL(TRIM(OPER_IN_TIME),'{0}'), 'YYYYMMDDHH24MISS'), 2) AS OPERINTIMEDIFF " + "\n", strDate);
            strSqlString.AppendFormat("     , TRUNC(TO_DATE('{0}', 'YYYYMMDDHH24MISS') - TO_DATE(LOT_CMF_14, 'YYYYMMDDHH24MISS'), 2) AS CREATETIMEDIFF " + "\n", strDate);

            // 2018-03-23-임종우 : PCB Strip 정보
            if (ckbPCB.Checked == true)
            {
                strSqlString.Append("     , NVL(TO_NUMBER((SELECT ATTR_VALUE FROM MATRNAMSTS@RPTTOMES WHERE ATTR_KEY = M_LOT_ID AND ATTR_TYPE = 'LOT' AND FACTORY = FACTORY AND ATTR_NAME = 'PCB_STRIP')),0) AS PCB_STRIP" + "\n");
                strSqlString.Append("     , NVL(TO_NUMBER((SELECT ATTR_VALUE FROM MATRNAMSTS@RPTTOMES WHERE ATTR_KEY = M_LOT_ID AND ATTR_TYPE = 'LOT' AND FACTORY = FACTORY AND ATTR_NAME = 'PCB_BAD_QTY')),0) AS PCB_BAD_QTY" + "\n");
            }

            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT LOT.*, OPR.OPER_DESC" + "\n");
            strSqlString.Append("             , MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_7, MAT_CMF_10" + "\n");

            if (ckbPCB.Checked == true)
            {
                strSqlString.Append("             , CASE WHEN LOT.MAT_ID LIKE 'EI%' THEN (  " + "\n");
                strSqlString.Append("                                                     SELECT LOT_ID  " + "\n");
                strSqlString.Append("                                                       FROM CRESMATHIS@RPTTOMES  " + "\n");
                strSqlString.Append("                                                      WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                                        AND START_LOT_ID = LOT.LOT_ID " + "\n");
                strSqlString.Append("                                                        AND M_MAT_ID IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_TYPE = 'PB' AND DELETE_FLAG = ' ') " + "\n");
                strSqlString.Append("                                                    ) END AS M_LOT_ID " + "\n");
            }
            
            if(isRealTime)
                strSqlString.Append("          FROM RWIPLOTSTS LOT " + "\n");
            else
                strSqlString.Append("          FROM RWIPLOTSTS_BOH LOT " + "\n");
            
            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("             , MWIPOPRDEF OPR " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND LOT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("           AND LOT.FACTORY = OPR.FACTORY " + "\n");
            strSqlString.Append("           AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("           AND LOT.OPER = OPR.OPER " + "\n");
            strSqlString.Append("           AND LOT.MAT_VER = 1 " + "\n");
            strSqlString.Append("           AND LOT.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND LOT.OWNER_CODE = 'PROD' " + "\n");
            strSqlString.Append("           AND LOT.LOT_TYPE = 'W' " + "\n");

            #region 조회조건(FACTORY, STEP, LOT_TYPE, PRODUCT, DATE)

            strSqlString.Append("           AND LOT.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

            if (cdvStep.Text != "ALL" && cdvStep.Text.Trim() != "")
                strSqlString.Append("           AND LOT.OPER " + cdvStep.SelectedValueToQueryString + "\n");                
            /*
            if (cdvLotType.Text != "ALL" && cdvLotType.Text.Trim() != "")
                strSqlString.Append("        AND LOT.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            */
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND LOT.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.AppendFormat("           AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

            if(!isRealTime)
                strSqlString.AppendFormat("           AND LOT.CUTOFF_DT = '{0}'" + "\n", strDate.Substring(0, 10));

            #endregion

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            if (ckbPCB.Checked == true)
                strSqlString.Append("        AND LOT.MAT_ID LIKE 'EI%'" + "\n");

            strSqlString.Append("       ) " + "\n");
            strSqlString.AppendFormat("ORDER BY {0}, CREATE_TIME, RESV_FIELD_1" + "\n", QueryCond2);

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
        #endregion

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvStep.sFactory = cdvFactory.txtValue;
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        private void cdvDate_ValueChanged(object sender, EventArgs e)
        {            
            if (cdvDate.Value.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                //cboTimeBase.Enabled = false;
                cboTimeBase.Items.Clear();
                cboTimeBase.Items.Add("00 hours");
                cboTimeBase.Items.Add("01 hours");
                cboTimeBase.Items.Add("02 hours");
                cboTimeBase.Items.Add("03 hours");
                cboTimeBase.Items.Add("04 hours");
                cboTimeBase.Items.Add("05 hours");
                cboTimeBase.Items.Add("06 hours");
                cboTimeBase.Items.Add("07 hours");
                cboTimeBase.Items.Add("08 hours");
                cboTimeBase.Items.Add("09 hours");
                cboTimeBase.Items.Add("10 hours");
                cboTimeBase.Items.Add("11 hours");
                cboTimeBase.Items.Add("12 hours");
                cboTimeBase.Items.Add("13 hours");
                cboTimeBase.Items.Add("14 hours");
                cboTimeBase.Items.Add("15 hours");
                cboTimeBase.Items.Add("16 hours");
                cboTimeBase.Items.Add("17 hours");
                cboTimeBase.Items.Add("18 hours");
                cboTimeBase.Items.Add("19 hours");
                cboTimeBase.Items.Add("20 hours");
                cboTimeBase.Items.Add("21 hours");
                cboTimeBase.Items.Add("22 hours");
                cboTimeBase.Items.Add("23 hours");

                cboTimeBase.Text = "22 hours";
            }
            else
            {
                //cboTimeBase.Enabled = true;
                cboTimeBase.Items.Clear();
                cboTimeBase.Items.Add("06 hours");
                cboTimeBase.Items.Add("22 hours");

                cboTimeBase.Text = "22 hours";
            }
        }
    }
}

