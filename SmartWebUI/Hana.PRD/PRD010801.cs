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
    public partial class PRD010801 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010801<br/>
        /// 클래스요약: Lot History<br/>
        /// 작  성  자: 하나마이크론 김준용<br/>
        /// 최초작성일: 2009-02-09<br/>
        /// 상세  설명: Lot 이력 조회<br/>
        /// 맨날 LOSS 이력 검색하기 귀찮아서 만듬
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2011-03-23-임종우 : PART 기준 정보 표시 추가
        /// </summary>
        public PRD010801()
        {
            InitializeComponent();
            //cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit(); //헤더 한줄짜리 
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) as Customer", "Customer", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 as Family", "Family", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 as Package", "Package", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 as Type1", "Type1", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 as Type2", "Type2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 as LDCount", "LDCount", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 as Density", "Density", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 as Generation", "Generation", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID AS MAT_ID", "MAT_ID", false);            
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
            spdData.RPT_AddBasicColumn("Hist Seq", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Transection", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Tran Time", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("From Qty", 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("To Qty", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Tran Qty", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("From Oper", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("From Oper Desc", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("To Oper", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("To Oper Desc", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("From Factory", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("To Factory", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("User", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("From Lot ID", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("To Lot ID", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Start Ras", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("End Ras", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Hold Time", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Realease TIME", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("Comment", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            

            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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

            StringBuilder lotSqlString = new StringBuilder();

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

                LabelTextChange();
                            
                spdData.DataSource = dt;

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue();

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 9, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 1;

                ////3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);

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

        #endregion

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            //string QueryCond1 = null;
            //string QueryCond2 = null;

            //string sStart_Tran_Time = null;
            //string sEnd_Tran_Time = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            //QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            //QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("SELECT  HIST_SEQ, " + "\n");
            strSqlString.Append("        TRAN_CODE, " + "\n");
            strSqlString.Append("        TRAN_TIME, " + "\n");
            strSqlString.Append("        FROM_QTY, " + "\n");
            strSqlString.Append("        TO_QTY, " + "\n");
            strSqlString.Append("        TRAN_QTY, " + "\n");
            strSqlString.Append("        FROM_OPER, " + "\n");
            strSqlString.Append("        FROM_OPER_DESC, " + "\n");
            strSqlString.Append("        TO_OPER, " + "\n");
            strSqlString.Append("        TO_OPER_DESC, " + "\n");
            strSqlString.Append("        FROM_FACTORY, " + "\n");
            strSqlString.Append("        TO_FACTORY, " + "\n");
            strSqlString.Append("        USER_DESC, " + "\n");
            strSqlString.Append("        FROM_LOT_ID, " + "\n");
            strSqlString.Append("        TO_LOT_ID, " + "\n");
            strSqlString.Append("        START_RES_ID, " + "\n");
            strSqlString.Append("        END_RES_ID, " + "\n");
            strSqlString.Append("        HOLD_TIME, " + "\n");
            strSqlString.Append("        REALEASE_TIME, " + "\n");
            strSqlString.Append("        TRAN_COMMENT " + "\n");
            strSqlString.Append("FROM    ( " + "\n");
            strSqlString.Append("        (  " + "\n");
            strSqlString.Append("        SELECT  HIS.HIST_SEQ, " + "\n");
            strSqlString.Append("                1 AS SORT,  " + "\n");
            strSqlString.Append("                HIS.TRAN_CODE,  " + "\n");
            strSqlString.Append("                TO_CHAR(TO_DATE(HIS.TRAN_TIME,'YYYYMMDDHH24MISS')) AS TRAN_TIME, " + "\n");
            strSqlString.Append("                TO_CHAR(HIS.OLD_QTY_1) AS FROM_QTY,  " + "\n");
            strSqlString.Append("                TO_CHAR(HIS.QTY_1) AS TO_QTY, " + "\n");
            strSqlString.Append("                '' AS TRAN_QTY, " + "\n");
            strSqlString.Append("                HIS.OLD_OPER AS FROM_OPER,  " + "\n");
            strSqlString.Append("                OPR1.OPER_DESC AS FROM_OPER_DESC, " + "\n");
            strSqlString.Append("                HIS.OPER AS TO_OPER, " + "\n");
            strSqlString.Append("                OPR2.OPER_DESC AS TO_OPER_DESC, " + "\n");
            strSqlString.Append("                HIS.OLD_FACTORY AS FROM_FACTORY, " + "\n");
            strSqlString.Append("                HIS.FACTORY AS TO_FACTORY,  " + "\n");
            strSqlString.Append("                TRIM(USR.USER_DESC) || '(' || TRIM(HIS.TRAN_USER_ID) || ')' AS USER_DESC, " + "\n");
            strSqlString.Append("                '' AS FROM_LOT_ID, " + "\n");
            strSqlString.Append("                '' AS TO_LOT_ID,  " + "\n");
            strSqlString.Append("                HIS.START_RES_ID,  " + "\n");
            strSqlString.Append("                HIS.END_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS HOLD_TIME, " + "\n");
            strSqlString.Append("                '' REALEASE_TIME, " + "\n");
            strSqlString.Append("                HIS.TRAN_COMMENT  " + "\n");
            strSqlString.Append("        FROM    RWIPLOTHIS HIS,  " + "\n");
            strSqlString.Append("                MWIPOPRDEF OPR1, " + "\n");
            strSqlString.Append("                MWIPOPRDEF OPR2, " + "\n");
            strSqlString.Append("                RWEBUSRDEF USR  " + "\n");
            strSqlString.Append("        WHERE   1=1  " + "\n");
            strSqlString.Append("                AND HIS.LOT_ID ='" + txtLotID.Text + "'  " + "\n");
            strSqlString.Append("                AND HIS.OLD_OPER = OPR1.OPER  " + "\n");
            strSqlString.Append("                AND HIS.OPER = OPR2.OPER " + "\n");
            strSqlString.Append("                AND HIS.OLD_FACTORY = OPR1.FACTORY " + "\n");
            strSqlString.Append("                AND HIS.FACTORY = OPR2.FACTORY " + "\n");
            strSqlString.Append("                AND HIS.TRAN_USER_ID = USR.USER_ID  " + "\n");
            strSqlString.Append("                AND HIS.HIST_DEL_FLAG = ' '   " + "\n");
            strSqlString.Append("        )  " + "\n");
            strSqlString.Append("        UNION  " + "\n");
            strSqlString.Append("        (  " + "\n");
            strSqlString.Append("        SELECT  HIST_SEQ,  " + "\n");
            strSqlString.Append("                2 AS SORT, " + "\n");
            strSqlString.Append("                LOSS_CODE AS TRAN_CODE,  " + "\n");
            strSqlString.Append("                TO_CHAR(TO_DATE(TRAN_TIME,'YYYYMMDDHH24MISS')) AS TRAN_TIME, " + "\n");
            strSqlString.Append("                '' AS FROM_QTY, " + "\n");
            strSqlString.Append("                '' AS TO_QTY, " + "\n");
            strSqlString.Append("                TO_CHAR(LOSS_QTY) AS TRAN_QTY,  " + "\n");
            strSqlString.Append("                '' AS FROM_OPER,  " + "\n");
            strSqlString.Append("                '' AS FROM_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS TO_OPER , " + "\n");
            strSqlString.Append("                '' AS TO_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS FROM_FACTORY,  " + "\n");
            strSqlString.Append("                '' AS TO_FACTORY, " + "\n");
            strSqlString.Append("                '' AS USER_DESC,  " + "\n");
            strSqlString.Append("                '' AS FROM_LOT_ID, " + "\n");
            strSqlString.Append("                '' AS TO_LOT_ID, " + "\n");
            strSqlString.Append("                '' AS START_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS END_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS HOLD_TIME, " + "\n");
            strSqlString.Append("                '' REALEASE_TIME, " + "\n");
            strSqlString.Append("                '' AS TRAN_COMMENT  " + "\n");
            strSqlString.Append("        FROM    RWIPLOTLSM " + "\n");
            strSqlString.Append("        WHERE   1=1  " + "\n");
            strSqlString.Append("                AND LOT_ID = '" + txtLotID.Text + "'  " + "\n");
            strSqlString.Append("                AND HIST_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("        )  " + "\n");
            strSqlString.Append("        UNION " + "\n");
            strSqlString.Append("        ( " + "\n");
            strSqlString.Append("        SELECT  HIST_SEQ, " + "\n");
            strSqlString.Append("                2 AS SORT, " + "\n");
            strSqlString.Append("                BONUS_CODE_1, " + "\n");
            strSqlString.Append("                TO_CHAR(TO_DATE(TRAN_TIME,'YYYYMMDDHH24MISS')) AS TRAN_TIME, " + "\n");
            strSqlString.Append("                '' AS FROM_QTY, " + "\n");
            strSqlString.Append("                '' AS TO_QTY, " + "\n");
            strSqlString.Append("                TO_CHAR(BONUS_QTY_1) AS TRAN_QTY, " + "\n");
            strSqlString.Append("                '' AS FROM_OPER,  " + "\n");
            strSqlString.Append("                '' AS FROM_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS TO_OPER , " + "\n");
            strSqlString.Append("                '' AS TO_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS FROM_FACTORY,  " + "\n");
            strSqlString.Append("                '' AS TO_FACTORY, " + "\n");
            strSqlString.Append("                '' AS USER_DESC,  " + "\n");
            strSqlString.Append("                '' AS FROM_LOT_ID, " + "\n");
            strSqlString.Append("                '' AS TO_LOT_ID, " + "\n");
            strSqlString.Append("                '' AS START_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS END_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS HOLD_TIME, " + "\n");
            strSqlString.Append("                '' REALEASE_TIME, " + "\n");
            strSqlString.Append("                '' AS TRAN_COMMENT          " + "\n");
            strSqlString.Append("        FROM    RWIPLOTBNS HIS " + "\n");
            strSqlString.Append("        WHERE   1=1 " + "\n");
            strSqlString.Append("                AND LOT_ID = '" + txtLotID.Text + "' " + "\n");
            strSqlString.Append("                AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("        ) " + "\n");
            strSqlString.Append("        UNION " + "\n");
            strSqlString.Append("        ( " + "\n");
            strSqlString.Append("        SELECT  HIST_SEQ, " + "\n");
            strSqlString.Append("                2 AS SORT, " + "\n");
            strSqlString.Append("                'SPLIT' AS TRAN_CODE, " + "\n");
            strSqlString.Append("                TO_CHAR(TO_DATE(TRAN_TIME,'YYYYMMDDHH24MISS')) AS TRAN_TIME, " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'T',TO_CHAR(FROM_TO_QTY_1),'') AS FROM_QTY, " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'F',TO_CHAR(FROM_TO_QTY_1),'') AS TO_QTY, " + "\n");
            strSqlString.Append("                '' AS TRAN_QTY, " + "\n");
            strSqlString.Append("                '' AS FROM_OPER,  " + "\n");
            strSqlString.Append("                '' AS FROM_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS TO_OPER , " + "\n");
            strSqlString.Append("                '' AS TO_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS FROM_FACTORY,  " + "\n");
            strSqlString.Append("                '' AS TO_FACTORY, " + "\n");
            strSqlString.Append("                '' AS USER_DESC,  " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'T',FROM_TO_LOT_ID,'') AS FROM_LOT_ID, " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'F',FROM_TO_LOT_ID,'') AS TO_LOT_ID, " + "\n");
            strSqlString.Append("                '' AS START_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS END_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS HOLD_TIME, " + "\n");
            strSqlString.Append("                '' REALEASE_TIME, " + "\n");
            strSqlString.Append("                '' AS TRAN_COMMENT  " + "\n");
            strSqlString.Append("        FROM    RWIPLOTSPL " + "\n");
            strSqlString.Append("        WHERE   1=1 " + "\n");
            strSqlString.Append("                AND LOT_ID = '" + txtLotID.Text + "' " + "\n");
            strSqlString.Append("                AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("        ) " + "\n");
            strSqlString.Append("        UNION " + "\n");
            strSqlString.Append("        ( " + "\n");
            strSqlString.Append("        SELECT  HIST_SEQ, " + "\n");
            strSqlString.Append("                2 AS SORT, " + "\n");
            strSqlString.Append("                'MERGE' AS TRAN_CODE, " + "\n");
            strSqlString.Append("                TO_CHAR(TO_DATE(TRAN_TIME,'YYYYMMDDHH24MISS')) AS TRAN_TIME, " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'T',TO_CHAR(FROM_TO_QTY_1),'') AS FROM_QTY, " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'F',TO_CHAR(FROM_TO_QTY_1),'') AS TO_QTY, " + "\n");
            strSqlString.Append("                '' AS TRAN_QTY, " + "\n");
            strSqlString.Append("                '' AS FROM_OPER,  " + "\n");
            strSqlString.Append("                '' AS FROM_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS TO_OPER , " + "\n");
            strSqlString.Append("                '' AS TO_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS FROM_FACTORY,  " + "\n");
            strSqlString.Append("                '' AS TO_FACTORY, " + "\n");
            strSqlString.Append("                '' AS USER_DESC,  " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'T',FROM_TO_LOT_ID,'') AS FROM_LOT_ID, " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'F',FROM_TO_LOT_ID,'') AS TO_LOT_ID, " + "\n");
            strSqlString.Append("                '' AS START_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS END_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS HOLD_TIME, " + "\n");
            strSqlString.Append("                '' REALEASE_TIME, " + "\n");
            strSqlString.Append("                '' AS TRAN_COMMENT  " + "\n");
            strSqlString.Append("        FROM    RWIPLOTMRG " + "\n");
            strSqlString.Append("        WHERE   1=1 " + "\n");
            strSqlString.Append("                AND LOT_ID = '" + txtLotID.Text + "' " + "\n");
            strSqlString.Append("                AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("        ) " + "\n");
            // 2009-05-27-정비재 : LOT의 COMBINE 내역을 추가함
            /************************************************************************************************************************/
            strSqlString.Append("        UNION " + "\n");
            strSqlString.Append("        ( " + "\n");
            strSqlString.Append("        SELECT  HIST_SEQ, " + "\n");
            strSqlString.Append("                2 AS SORT, " + "\n");
            strSqlString.Append("                'COMBINE' AS TRAN_CODE, " + "\n");
            strSqlString.Append("                TO_CHAR(TO_DATE(TRAN_TIME,'YYYYMMDDHH24MISS')) AS TRAN_TIME, " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'T',TO_CHAR(FROM_TO_QTY_1),'') AS FROM_QTY, " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'F',TO_CHAR(FROM_TO_QTY_1),'') AS TO_QTY, " + "\n");
            strSqlString.Append("                '' AS TRAN_QTY, " + "\n");
            strSqlString.Append("                '' AS FROM_OPER,  " + "\n");
            strSqlString.Append("                '' AS FROM_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS TO_OPER , " + "\n");
            strSqlString.Append("                '' AS TO_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS FROM_FACTORY,  " + "\n");
            strSqlString.Append("                '' AS TO_FACTORY, " + "\n");
            strSqlString.Append("                '' AS USER_DESC,  " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'T',FROM_TO_LOT_ID,'') AS FROM_LOT_ID, " + "\n");
            strSqlString.Append("                DECODE(FROM_TO_FLAG,'F',FROM_TO_LOT_ID,'') AS TO_LOT_ID, " + "\n");
            strSqlString.Append("                '' AS START_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS END_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS HOLD_TIME, " + "\n");
            strSqlString.Append("                '' REALEASE_TIME, " + "\n");
            strSqlString.Append("                '' AS TRAN_COMMENT  " + "\n");
            strSqlString.Append("        FROM    RWIPLOTCMB " + "\n");
            strSqlString.Append("        WHERE   1=1 " + "\n");
            strSqlString.Append("                AND LOT_ID = '" + txtLotID.Text + "' " + "\n");
            strSqlString.Append("                AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("        ) " + "\n");
            /************************************************************************************************************************/
            strSqlString.Append("        UNION " + "\n");
            strSqlString.Append("        ( " + "\n");
            strSqlString.Append("        SELECT  HIST_SEQ,  " + "\n");
            strSqlString.Append("                2 AS SORT,  " + "\n");
            strSqlString.Append("                HOLD_CODE AS TRAN_CODE,  " + "\n");
            strSqlString.Append("                TO_CHAR(DECODE(HOLD_TRAN_TIME,' ','',TO_DATE(HOLD_TRAN_TIME,'YYYYMMDDHH24MISS'))) AS TRAN_TIME,  " + "\n");
            strSqlString.Append("                '' AS FROM_QTY,  " + "\n");
            strSqlString.Append("                '' AS TO_QTY,  " + "\n");
            strSqlString.Append("                TO_CHAR(QTY_1) AS TRAN_QTY,  " + "\n");
            strSqlString.Append("                '' AS FROM_OPER,   " + "\n");
            strSqlString.Append("                '' AS FROM_OPER_DESC,  " + "\n");
            strSqlString.Append("                '' AS TO_OPER ,  " + "\n");
            strSqlString.Append("                '' AS TO_OPER_DESC,  " + "\n");
            strSqlString.Append("                '' AS FROM_FACTORY,   " + "\n");
            strSqlString.Append("                '' AS TO_FACTORY,  " + "\n");
            strSqlString.Append("                '' AS USER_DESC,   " + "\n");
            strSqlString.Append("                '' AS FROM_LOT_ID,  " + "\n");
            strSqlString.Append("                '' AS TO_LOT_ID,  " + "\n");
            strSqlString.Append("                '' AS START_RES_ID,   " + "\n");
            strSqlString.Append("                '' AS END_RES_ID,   " + "\n");
            strSqlString.Append("                TO_CHAR(DECODE(HOLD_TRAN_TIME,' ','',TO_DATE(HOLD_TRAN_TIME,'YYYYMMDDHH24MISS'))) AS HOLD_TIME,  " + "\n");
            strSqlString.Append("                TO_CHAR(DECODE(RELEASE_TRAN_TIME,' ','',TO_DATE(RELEASE_TRAN_TIME,'YYYYMMDDHH24MISS'))) REALEASE_TIME,  " + "\n");
            strSqlString.Append("                DECODE(HOLD_CODE,'S0', " + "\n");
            strSqlString.Append("                    ( " + "\n");
            strSqlString.Append("                    SELECT SHP_FAC                                                               " + "\n");
            strSqlString.Append("                     FROM CWIPSHPLOT LOT, CWIPSHPINF INF                                          " + "\n");
            strSqlString.Append("                    WHERE 1=1                                                                    " + "\n");
            strSqlString.Append("                      AND LOT.FACTORY = INF.FACTORY                                                " + "\n");
            strSqlString.Append("                      AND LOT.INVOICE_NO = INF.INVOICE_NO                                          " + "\n");
            strSqlString.Append("                      AND LOT.TYPE = 'PLATING'                                                     " + "\n");
            strSqlString.Append("                      AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                      AND LOT.LOT_ID='"+ txtLotID.Text +"' " + "\n");
            strSqlString.Append("                    ),HOLD_COMMENT) AS TRAN_COMMENT   " + "\n");
            strSqlString.Append("        FROM    RWIPLOTHLD  " + "\n");
            strSqlString.Append("        WHERE   1=1  " + "\n");
            strSqlString.Append("                AND LOT_ID = '"+ txtLotID.Text +"'  " + "\n");
            strSqlString.Append("                AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("        ) " + "\n");
            strSqlString.Append("        UNION " + "\n");
            strSqlString.Append("        ( " + "\n");
            strSqlString.Append("        SELECT  HIST_SEQ, " + "\n");
            strSqlString.Append("                0 AS SORT, " + "\n");
            strSqlString.Append("                'SHIP' AS TRAN_CODE, " + "\n");
            strSqlString.Append("                TO_CHAR(TO_DATE(TRAN_TIME,'YYYYMMDDHH24MISS')) AS TRAN_TIME, " + "\n");
            strSqlString.Append("                '' AS FROM_QTY, " + "\n");
            strSqlString.Append("                '' AS TO_QTY, " + "\n");
            strSqlString.Append("                TO_CHAR(QTY_1) AS TRAN_QTY, " + "\n");
            strSqlString.Append("                '' AS FROM_OPER,  " + "\n");
            strSqlString.Append("                '' AS FROM_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS TO_OPER , " + "\n");
            strSqlString.Append("                '' AS TO_OPER_DESC, " + "\n");
            strSqlString.Append("                '' AS FROM_FACTORY,  " + "\n");
            strSqlString.Append("                FACTORY AS TO_FACTORY, " + "\n");
            strSqlString.Append("                '' AS USER_DESC,  " + "\n");
            strSqlString.Append("                '' AS FROM_LOT_ID, " + "\n");
            strSqlString.Append("                '' AS TO_LOT_ID, " + "\n");
            strSqlString.Append("                '' AS START_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS END_RES_ID,  " + "\n");
            strSqlString.Append("                '' AS HOLD_TIME, " + "\n");
            strSqlString.Append("                '' REALEASE_TIME, " + "\n");
            strSqlString.Append("                '' AS TRAN_COMMENT  " + "\n");
            strSqlString.Append("        FROM    RWIPLOTSHP " + "\n");
            strSqlString.Append("        WHERE   1=1 " + "\n");
            strSqlString.Append("                AND LOT_ID = '" + txtLotID.Text + "' " + "\n");
            strSqlString.Append("                AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("        ) " + "\n");
            strSqlString.Append("        ) " + "\n");
            strSqlString.Append("ORDER BY 1 DESC,SORT ASC  " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlString2() // text box에 들어갈 기준정보 
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT * " + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT A.FACTORY " + "\n"); 
            strSqlString.Append("             , A.MAT_ID " + "\n");  
            strSqlString.Append("             , QTY_1 " + "\n");     
            strSqlString.Append("             , LOT_STATUS " + "\n"); 
            strSqlString.Append("             , LOT_CMF_2 || '(' || (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = LOT_CMF_2 AND ROWNUM=1) || ')'  AS CUSTOMER " + "\n");
            strSqlString.Append("             , LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("             , LOT_CMF_5 AS LOT_TYPE " + "\n");
            strSqlString.Append("             , LOT_CMF_6 AS FAB_SITE " + "\n");
            strSqlString.Append("             , LOT_CMF_7 AS ASSY_SITE " + "\n");
            strSqlString.Append("             , LOT_CMF_8 AS TEST_SITE " + "\n");
            strSqlString.Append("             , LOT_CMF_9 AS GRADE " + "\n");
            strSqlString.Append("             , LOT_CMF_10 AS DATE_CODE " + "\n");
            strSqlString.Append("             , LOT_CMF_11 AS SHIP_SITE " + "\n");
            strSqlString.Append("             , TO_CHAR(TO_DATE(LOT_CMF_14, 'YYYYMMDD HH24MISS'), 'YYYY/MM/DD HH24:MI:SS') AS CREATE_TIME " + "\n");
            //strSqlString.Append("             , TO_CHAR(TO_DATE( " + "\n");
            //strSqlString.Append("                               (SELECT FAC_IN_TIME  " + "\n");
            //strSqlString.Append("                                  FROM RWIPLOTHIS  " + "\n");
            //strSqlString.Append("                                 WHERE LOT_ID=(SELECT LOT_CMF_4 FROM RWIPLOTSTS WHERE LOT_ID='" + txtLotID.Text + "') " + "\n");
            //strSqlString.Append("                                   AND TRAN_CODE='CREATE' " + "\n");
            //strSqlString.Append("                                   AND HIST_DEL_FLAG=' ' " + "\n");
            //strSqlString.Append("                               ),'YYYYMMDD HH24MISS' " + "\n");
            //strSqlString.Append("                              ),'YYYY/MM/DD HH24:MI:SS' " + "\n");
            //strSqlString.Append("                      ) AS CREATE_TIME  " + "\n");
            strSqlString.Append("             , TO_CHAR(TO_DATE(LAST_TRAN_TIME,'YYYYMMDD HH24MISS'),'YYYY/MM/DD HH24:MI:SS') AS LAST_TRAN_TIME " + "\n");
            strSqlString.Append("             , LOT_PRIORITY " + "\n");
            strSqlString.Append("             , FLOW " + "\n");
            strSqlString.Append("             , OPER " + "\n");
            strSqlString.Append("             , MAT_GRP_2 AS FAMILY" + "\n");
            strSqlString.Append("             , MAT_GRP_3 AS PACKAGE" + "\n");
            strSqlString.Append("             , MAT_GRP_4 AS TYPE1" + "\n");
            strSqlString.Append("             , MAT_GRP_5 AS TYPE2" + "\n");
            strSqlString.Append("             , MAT_GRP_6 AS LD_COUNT" + "\n");
            strSqlString.Append("             , MAT_GRP_7 AS DENSITY" + "\n");
            strSqlString.Append("             , MAT_GRP_8 AS GENERATION" + "\n");
            strSqlString.Append("             , MAT_GRP_9 AS MAJOR_CODE" + "\n");
            strSqlString.Append("          FROM RWIPLOTSTS A" + "\n");
            strSqlString.Append("             , MWIPMATDEF B" + "\n");
            strSqlString.Append("         WHERE A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND LOT_ID='" + txtLotID.Text + "'" + "\n");
            strSqlString.Append("       ) " + "\n");
            strSqlString.Append(" WHERE ROWNUM = 1" + "\n");

            return strSqlString.ToString();
        }

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
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            //spdData.ExportExcel();           
        }
        #endregion

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PRD010801_Load(object sender, EventArgs e)
        {
            txtLotID.Focus();
        }

        private void txtLotID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(sender, e);
            }
        }

        private void txtLotID_TextChanged(object sender, EventArgs e)
        {
            // 입력한 LotID 를 대문자로 변경한다.

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                txtLotID.Text = txtLotID.Text.ToUpper();
                txtLotID.SelectionStart = txtLotID.Text.Length;
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

        /// <summary>
        /// TextBox 에 기준정보 표시 부분
        /// </summary>
        private void LabelTextChange()
        {
            DataTable dtProductDesc = null;

            try
            {
                dtProductDesc = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());

                txtLine.Text = dtProductDesc.Rows[0][0].ToString();        // FACTORY
                txtProduct.Text = dtProductDesc.Rows[0][1].ToString();     // MAT_ID
                txtQty.Text = dtProductDesc.Rows[0][2].ToString();         // QTY
                txtCurState.Text = dtProductDesc.Rows[0][3].ToString();    // LOT_STATUS
                txtCustomer.Text = dtProductDesc.Rows[0][4].ToString();    // CUSTOMER
                txtRunId.Text = dtProductDesc.Rows[0][5].ToString();       // RUN_ID
                txtType.Text = dtProductDesc.Rows[0][6].ToString();        // Lot Type
                txtFabSite.Text = dtProductDesc.Rows[0][7].ToString();     // Fab Site
                txtAssySite.Text = dtProductDesc.Rows[0][8].ToString();    // Assy Site
                txtTestSite.Text = dtProductDesc.Rows[0][9].ToString();    // Test Site
                txtNCF.Text = dtProductDesc.Rows[0][10].ToString();        // NCF
                txtDateCode.Text = dtProductDesc.Rows[0][11].ToString();   // Date Code
                txtShipSite.Text = dtProductDesc.Rows[0][12].ToString();   // Ship Site 
                txtCtime.Text = dtProductDesc.Rows[0][13].ToString();      // Create Time
                txtLtime.Text = dtProductDesc.Rows[0][14].ToString();      // Last Time
                txtPriority.Text = dtProductDesc.Rows[0][15].ToString();   // Priority
                txtFlow.Text = dtProductDesc.Rows[0][16].ToString();       // Flow
                txtStepseq.Text = dtProductDesc.Rows[0][17].ToString();    // Oper
                txtFamily.Text = dtProductDesc.Rows[0][18].ToString();     // Family
                txtPackage.Text = dtProductDesc.Rows[0][19].ToString();    // Package
                txtType1.Text = dtProductDesc.Rows[0][20].ToString();      // Type1
                txtType2.Text = dtProductDesc.Rows[0][21].ToString();      // Type2
                txtLDCount.Text = dtProductDesc.Rows[0][22].ToString();    // Lead Count
                txtDensity.Text = dtProductDesc.Rows[0][23].ToString();    // Density
                txtGeneration.Text = dtProductDesc.Rows[0][24].ToString(); // Generation
                txtMajorCode.Text = dtProductDesc.Rows[0][25].ToString();  // Major Code

                if (dtProductDesc.Rows.Count == 0)
                {
                    dtProductDesc.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                dtProductDesc.Dispose();
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
    }
}

