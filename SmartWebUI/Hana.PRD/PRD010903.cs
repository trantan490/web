using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.PRD
{
    public partial class PRD010903 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private String stToday = null;
        private String stStartDay = null;
        private String[] dayArry = new String[7];
        private String[] dayArry2 = new String[7];
        private Int32 remain = 0;
        private Int32 iPlanStart = 0;

        DataTable Oper_Group = null;
        /// <summary>
        /// 클  래  스: PRD010903<br/>
        /// 클래스요약: 삼성 메모리 IN / OUT PLAN<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-07-22<br/>
        /// 상세  설명: 삼성 메모리 IN / OUT PLAN을 조회 한다.<br/>
        /// 변경  내용: <br/>      
        /// 2010-07-29-임종우 : 일목표 음수값도 표현 되도록 수정 (임태성 요청)
        /// 2010-07-30-임종우 : 컬럼 순서 변경, 재공기준&계획기준 조회 가능하도록 수정 (임태성 요청)
        /// 2010-09-09-임종우 : 제품 속성 중 SEC_VERSION 표시 요청 (임태성 요청)
        /// 2010-09-15-임종우 : GROUP 조건 변경-PKG,LD_COUNT,DEN,GER,PRODUCT,SEC_V 으로 변경 (임태성 요청)
        /// 2010-09-15-임종우 : 전일 데이터 조회 가능 하도록 수정 (임태성 요청)
        /// 2010-09-15-임종우 : OUT PLAN 조회시 공정별 WIP 표시 되도록 변경 (임태성 요청)
        /// 2010-09-16-임종우 : 월 계획 체크 박스에 의한 숨기기 기능 추가 (임태성 요청)
        /// 2010-09-16-임종우 : OUT PLAN 조회 시 생산관리팀에서 업로드하는 계획 값 표시 (임태성 요청)
        /// 2010-09-16-임종우 : 금일 차이(계획2-실적) 분에 대한 재공 음영 표시 추가 (임태성 요청)
        /// 2010-09-16-임종우 : LOT_TYPE은 P%로 고정 시킴. (임태성 요청)
        /// 2010-09-16-임종우 : 계획2 값은 굵은 글씨체로 표시 함 (임태성 요청)
        /// 2010-09-22-임종우 : 실적값에 Return 수량 포함 시킴 (임태성 요청)
        /// 2010-09-24-임종우 : 계획 2에 대한 차이 2 값 표시 추가 및 계획 1값 표시용 체크 박스 추가 (임태성 요청)
        /// 2010-10-05-임종우 : LOT TYPE P%만 조회 되도록 수정(임태성 요청)
        /// 2011-07-11-임종우 : 삼성 FP+ 계획 값 기준으로 변경(김성업 요청)        
        /// 2011-07-11-임종우 : IN 조회 시 실적은 CREATE -> ISSUE , FRONT -> STOCK, B/E -> BG/SAW 변경, 칩부족 추가 (김성업 요청)        
        /// 2011-07-22-임종우 : OUT 조회시 FP+ 계획 값 기준으로 변경 (김성업 요청)
        /// 2012-03-20-김민우 : Sec_Version 기본으로 안보이기 (김성업 요청)
        /// </summary>
        public PRD010903()
        {
            InitializeComponent();

            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit(); //헤더 한줄짜리 
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "MAT.MAT_GRP_1", "MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", "MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", "MAT_GRP_7", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", "MAT_GRP_8", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", "MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.PART_NO", "MAT.PART_NO", "DAT.PART_NO", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Sec_Version", "NVL(ATT.ATTR_VALUE, '-') AS ATTR_VALUE", "ATT.ATTR_VALUE", "ATTR_VALUE", false);
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
            try
            {
                GetDayArray();

                //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lead", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Sec_Version", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

                // 2010-09-15-임종우 : 전일 데이터 조회 가능 하도록 수정 (임태성 요청)
                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {
                    spdData.RPT_AddBasicColumn("Current standard (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Historical standard" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                }
                                                                                
                if (rdbIN.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("plan", 1, 11, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("actual", 1, 12, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Difference", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 11, 3);

                    spdData.RPT_AddBasicColumn("WIP", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("STOCK", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BG/SAW", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("TTL", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Chip shortage", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 14, 4);

                    spdData.RPT_AddBasicColumn("Warehousing Plan", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn(dayArry[0], 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn(dayArry[1], 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn(dayArry[2], 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn(dayArry[3], 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn(dayArry[4], 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn(dayArry[5], 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn(dayArry[6], 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 18, 7);

                    //spdData.RPT_AddBasicColumn("Production status", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    //spdData.RPT_AddBasicColumn("Monthly plan", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    //spdData.RPT_AddBasicColumn("Cumulative Performance", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    //spdData.RPT_AddBasicColumn("residual quantity", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    //spdData.RPT_AddBasicColumn("a daily goal", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    //spdData.RPT_MerageHeaderColumnSpan(0, 24, 4);
                }
                else
                {
                    if (ckbPlan.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("Plan 1", 1, 11, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("Plan 2", 1, 12, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("actual", 1, 13, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("Difference", 1, 14, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("Difference2", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("Plan 1", 1, 11, Visibles.False, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("Plan 2", 1, 12, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("actual", 1, 13, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("Difference", 1, 14, Visibles.False, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("Difference2", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }

                    spdData.RPT_MerageHeaderColumnSpan(0, 11, 5);

                    if (cdvFactory.txtValue != "")
                    {
                        Oper_Group = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2("DESC")); // 해당 FACTORY의 공정 그룹을 가져옴(공정순서 거꾸로..)

                        spdData.RPT_AddBasicColumn("WIP", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("TOTAL", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                        for (int i = 0; i < Oper_Group.Rows.Count; i++)
                        {
                            spdData.RPT_AddBasicColumn(Oper_Group.Rows[i][0].ToString(), 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        }

                        spdData.RPT_MerageHeaderColumnSpan(0, 16, Oper_Group.Rows.Count + 1);

                        //spdData.RPT_AddBasicColumn("TTL", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                        //spdData.RPT_AddBasicColumn("B/E", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                        //spdData.RPT_AddBasicColumn("FRONT", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                        //spdData.RPT_MerageHeaderColumnSpan(0, 14, 3);

                        spdData.RPT_AddBasicColumn("Shipping plan", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn(dayArry[0], 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn(dayArry[1], 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn(dayArry[2], 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn(dayArry[3], 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn(dayArry[4], 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn(dayArry[5], 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn(dayArry[6], 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.Columns.Count - 7, 7);
                    }
                }

                // 2010-09-16-임종우 : 월 계획 체크 박스에 의한 숨기기 기능 추가 (임태성 요청)
                if (ckbMon.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Production status", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Monthly plan", 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Cumulative Performance", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("a daily goal", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.Columns.Count - 4, 4);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Production status", 0, spdData.ActiveSheet.Columns.Count, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Monthly plan", 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Cumulative Performance", 1, spdData.ActiveSheet.Columns.Count, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, spdData.ActiveSheet.Columns.Count, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("a daily goal", 1, spdData.ActiveSheet.Columns.Count, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.Columns.Count - 4, 4);
                }                

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 2);

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
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
            int iOperStart = 0;
            int iOperEnd = 0;

            if (!CheckField()) return;

            DataTable dt = null;
            DataTable dt1 = null;

            StringBuilder strSqlString = new StringBuilder();

            // 계획값 기준 시간 가져오기(Plan Revision Time)   
            if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.AppendFormat("SELECT MAX(TO_DATE(INSERT_TIME,'YYYY-MM-DD HH24MISS')) FROM CWIPPLNASY@RPTTOMES");
            }
            else
            {
                strSqlString.AppendFormat("SELECT MAX(TO_DATE(IN_TIME,'YYYY-MM-DD HH24MISS')) FROM ISTMPLNASY@RPTTOMES WHERE PLAN_FLAG = 'I' AND WORK_DAY = '" +  cdvDate.SelectedValue() + "'");
            }

            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
            lblPlanTime.Text = dt1.Rows[0][0].ToString();
                    
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


                ////표구성에따른 항목 Display
                spdData.RPT_ColumnConfigFromTable(btnSort);

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 11, null, null, btnSort);


                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                //--------------------------------------------


                // 2010-09-16-임종우 : 공정의 시작 컬럼과 끝나는 컬럼을 찾기 위해...
                for (int i = 0; i < spdData.ActiveSheet.ColumnCount; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "HMK2A")
                    {
                        iOperEnd = i;
                        iPlanStart = i + 1;
                    }

                    if (spdData.ActiveSheet.Columns[i].Label == "HMK3A")
                    {
                        iOperStart = i;                        
                    }
                }

                // 2010-09-16-임종우 : 금일 차이(계획2-실적) 분에 대한 재공 음영 표시 추가 (임태성 요청)
                if (rdbOUT.Checked == true)
                {
                    // 2010-09-16-임종우 : 계획2, 차이2 값은 굵은 글씨체로 표시 함 (임태성 요청)
                    spdData.ActiveSheet.Columns[12].Font = new Font("굴림", 8, FontStyle.Bold);
                    spdData.ActiveSheet.Columns[15].Font = new Font("굴림", 8, FontStyle.Bold);

                    for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                    {                        
                        int sum = 0;
                        int value = 0;

                        if (spdData.ActiveSheet.Cells[i, 15].BackColor.IsEmpty) // subtotal 부분 제외시키기 위함.
                        {
                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 15].Value) > 0) // 계획2 - 실적인 차이가 0 이상인 것만...
                            {
                                for (int y = iOperStart; y <= iOperEnd; y++) // 공정 컬럼번호
                                {
                                    value = Convert.ToInt32(spdData.ActiveSheet.Cells[i, y].Value);
                                    sum += value;

                                    if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 15].Value) > sum)
                                    {
                                        spdData.ActiveSheet.Cells[i, y].ForeColor = Color.Red;
                                        spdData.ActiveSheet.Cells[i, y].BackColor = Color.Pink;
                                    }
                                    else
                                    {
                                        spdData.ActiveSheet.Cells[i, y].ForeColor = Color.Blue;
                                        spdData.ActiveSheet.Cells[i, y].BackColor = Color.LightGreen;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
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
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            //if (udcWIPCondition1.Text == "ALL" || udcWIPCondition1.Text == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD033", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            string stMonth = string.Empty;

            StringBuilder strSqlString = new StringBuilder();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            //stToday = Convert.ToDateTime(cdvDate.Text).ToString("yyyyMMdd"); // 현재 조회일
            stToday = cdvDate.Value.ToString("yyyyMMdd"); // 현재 조회일
            stStartDay = cdvDate.Value.ToString("yyyyMM") + "01"; //조회일 기준 해당 월의 시작일

            stMonth = cdvDate.Value.ToString("yyyyMM");

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            #region
            if (rdbIN.Checked == true)
            {
                strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond3);
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D0, 0)) AS TODAY_PLAN" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.TODAY_QTY, 0)) AS TODAY_QTY" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D0, 0) - NVL(DAT.TODAY_QTY, 0)) AS DAY_DEF" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.STOCK, 0)) AS STOCK" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.BG_SAW, 0)) AS BG_SAW" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.TTL, 0)) AS TTL" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D0, 0) - NVL(DAT.TODAY_QTY, 0)) - SUM(NVL(DAT.STOCK, 0)) AS CHIP_DEF" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D0, 0)) AS D0" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D1, 0)) AS D1" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D2, 0)) AS D2" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D3, 0)) AS D3" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D4, 0)) AS D4" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D5, 0)) AS D5" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D6, 0)) AS D6" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.MON_PLAN, 0)) AS MON_PLAN" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.MON_QTY, 0)) AS MON_QTY" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.MON_DEF, 0)) AS MON_DEF" + "\n");
                strSqlString.AppendFormat("     , SUM(ROUND(DAT.MON_DEF / " + remain + ", 0)) AS TARGET" + "\n");
                strSqlString.AppendFormat("  FROM (" + "\n");
                strSqlString.AppendFormat("        SELECT {0}" + "\n", QueryCond1);                
                strSqlString.AppendFormat("             , SUM(NVL(SHP.TODAY_QTY, 0)) AS TODAY_QTY" + "\n");                
                strSqlString.AppendFormat("             , SUM(NVL(WIP.STOCK, 0)) AS STOCK" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(WIP.BG_SAW, 0)) AS BG_SAW" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(WIP.STOCK, 0) + NVL(WIP.BG_SAW, 0)) AS TTL" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(PLN.PLAN_QTY, 0)) AS MON_PLAN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(SHP.MON_QTY, 0)) AS MON_QTY" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(PLN.PLAN_QTY, 0) - NVL(SHP.MON_QTY, 0)) AS MON_DEF" + "\n");        
                strSqlString.AppendFormat("          FROM (" + "\n");
                strSqlString.AppendFormat("                SELECT FACTORY, PLAN_MONTH, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY" + "\n");
                strSqlString.AppendFormat("                  FROM CWIPPLNMON" + "\n");
                strSqlString.AppendFormat("                 WHERE PLAN_MONTH = '" + stMonth + "'" + "\n");
                strSqlString.AppendFormat("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.AppendFormat("                   AND MAT_ID LIKE 'SEK%'" + "\n");
                strSqlString.AppendFormat("                 GROUP BY FACTORY, PLAN_MONTH, MAT_ID" + "\n");
                strSqlString.AppendFormat("               ) PLN" + "\n");
                
                // 입고수량 (Return 포함)                
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT MAT_ID" + "\n");
                strSqlString.AppendFormat("                     , SUM(QTY_1) AS MON_QTY" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(WORK_DATE, '" + stToday + "', QTY_1, 0)) AS TODAY_QTY" + "\n");
                strSqlString.AppendFormat("                  FROM (" + "\n");
                strSqlString.AppendFormat("                        SELECT MAT_ID, WORK_DATE, S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S4_END_QTY_1  AS QTY_1" + "\n");
                strSqlString.AppendFormat("                          FROM RSUMWIPMOV" + "\n");
                strSqlString.AppendFormat("                         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("                           AND WORK_DATE BETWEEN '" + stStartDay + "' AND '" + stToday + "'" + "\n");
                strSqlString.AppendFormat("                           AND LOT_TYPE = 'W'" + "\n");
                strSqlString.AppendFormat("                           AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.AppendFormat("                           AND MAT_ID LIKE 'SEK%'  " + "\n");
                strSqlString.AppendFormat("                           AND OPER = 'A0000'" + "\n");
                strSqlString.AppendFormat("                           AND CM_KEY_3 LIKE 'P%'" + "\n");                
                strSqlString.AppendFormat("                       )" + "\n");
                strSqlString.AppendFormat("                 GROUP BY MAT_ID " + "\n");
                strSqlString.AppendFormat("               ) SHP" + "\n");
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT MAT_ID" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(OPER, 'STOCK', QTY_1, 0)) AS STOCK" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(OPER, 'BG_SAW', QTY_1, 0)) AS BG_SAW" + "\n");
                strSqlString.AppendFormat("                  FROM (" + "\n");
                strSqlString.AppendFormat("                        SELECT STS.LOT_ID" + "\n");
                strSqlString.AppendFormat("                             , STS.MAT_ID" + "\n");
                strSqlString.AppendFormat("                             , CASE OPR.OPER_GRP_1 WHEN 'HMK2A' THEN 'STOCK' " + "\n");
                strSqlString.AppendFormat("                                                   ELSE 'BG_SAW'" + "\n");
                strSqlString.AppendFormat("                               END OPER" + "\n");
                strSqlString.AppendFormat("                             , STS.QTY_1" + "\n");

                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {
                    strSqlString.AppendFormat("                          FROM RWIPLOTSTS STS" + "\n");
                    strSqlString.AppendFormat("                             , MWIPOPRDEF OPR" + "\n");
                    strSqlString.AppendFormat("                         WHERE 1=1" + "\n");
                    strSqlString.AppendFormat("                           AND STS.FACTORY = OPR.FACTORY" + "\n");
                    strSqlString.AppendFormat("                           AND STS.OPER = OPR.OPER" + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("                          FROM RWIPLOTSTS_BOH STS " + "\n");
                    strSqlString.AppendFormat("                             , MWIPOPRDEF OPR" + "\n");
                    strSqlString.AppendFormat("                         WHERE 1=1" + "\n");
                    strSqlString.AppendFormat("                           AND STS.FACTORY = OPR.FACTORY" + "\n");
                    strSqlString.AppendFormat("                           AND STS.OPER = OPR.OPER" + "\n");
                    strSqlString.AppendFormat("                           AND CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");
                }

                strSqlString.AppendFormat("                           AND STS.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.AppendFormat("                           AND STS.LOT_TYPE = 'W'" + "\n");
                strSqlString.AppendFormat("                           AND STS.OWNER_CODE = 'PROD'" + "\n");
                strSqlString.AppendFormat("                           AND STS.LOT_DEL_FLAG = ' '" + "\n");
                strSqlString.AppendFormat("                           AND STS.MAT_ID LIKE 'SEK%'" + "\n");
                strSqlString.AppendFormat("                           AND STS.LOT_CMF_5 LIKE 'P%' " + "\n");
                strSqlString.AppendFormat("                           AND OPR.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW') " + "\n");
                strSqlString.AppendFormat("                       )" + "\n");
                strSqlString.AppendFormat("                 GROUP BY MAT_ID" + "\n");
                strSqlString.AppendFormat("               ) WIP" + "\n");
                
                // 2010-09-16-임종우 : OUT PLAN 조회 시 생산관리팀에서 업로드하는 계획 값 표시 (임태성 요청)
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT MAT_ID" + "\n");
                strSqlString.AppendFormat("                     , SUM(PLAN_QTY) AS PLAN2_QTY " + "\n");
                strSqlString.AppendFormat("                  FROM CWIPPLNASY@RPTTOMES" + "\n");
                strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
                strSqlString.AppendFormat("                   AND PLAN_FLAG = 'M'   " + "\n");
                strSqlString.AppendFormat("                   AND WORK_DAY = '" + dayArry2[0] + "'" + "\n");
                strSqlString.AppendFormat("                   AND PLAN_DATE = '" + dayArry2[0] + "'" + "\n");
                strSqlString.AppendFormat("                 GROUP BY MAT_ID " + "\n");
                strSqlString.AppendFormat("               ) PLAN2" + "\n");


                // 2010-09-09-임종우 : 제품 속성 중 SEC_VERSION 표시 요청 (임태성 요청)
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT * " + "\n");
                strSqlString.AppendFormat("                  FROM MATRNAMSTS " + "\n");
                strSqlString.AppendFormat("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.AppendFormat("                   AND ATTR_TYPE = 'MAT_ETC' " + "\n");
                strSqlString.AppendFormat("                   AND ATTR_NAME = 'SEC_VERSION' " + "\n");
                strSqlString.AppendFormat("               ) ATT" + "\n");
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT A.*, SUBSTR(MAT_CMF_7, 1, LENGTH(MAT_CMF_7)-6) || '___' || SUBSTR(MAT_CMF_7, -3) AS PART_NO " + "\n");
                strSqlString.AppendFormat("                  FROM MWIPMATDEF A " + "\n");
                strSqlString.AppendFormat("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.AppendFormat("                   AND MAT_TYPE = 'FG' " + "\n");
                strSqlString.AppendFormat("                   AND MAT_ID LIKE 'SEK%' " + "\n");
                strSqlString.AppendFormat("                   AND DELETE_FLAG = ' ' " + "\n");
                strSqlString.AppendFormat("               ) MAT" + "\n");
                strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("           AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
                strSqlString.AppendFormat("           AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
                strSqlString.AppendFormat("           AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");                
                strSqlString.AppendFormat("           AND MAT.MAT_ID = PLAN2.MAT_ID(+)" + "\n");
                strSqlString.AppendFormat("           AND MAT.MAT_ID = ATT.ATTR_KEY(+)" + "\n");

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

                strSqlString.AppendFormat("         GROUP BY {0}" + "\n", QueryCond2);
                strSqlString.AppendFormat("       ) DAT" + "\n");
                
                // IN PLAN의 경우 LOT 정보가 존재 하지 않으므로 계획 테이블의 제품기준으로 수량 표시
                strSqlString.AppendFormat("     , (" + "\n");
                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {                    
                    strSqlString.AppendFormat("        SELECT PLN.PART_NO" + "\n");
                    strSqlString.AppendFormat("             , SUM(CASE WHEN PLAN_DATE <= '" + dayArry2[0] + "' THEN PLN.PLAN_QTY" + "\n");
                    strSqlString.AppendFormat("                        ELSE 0" + "\n");
                    strSqlString.AppendFormat("                   END) AS D0" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[1] + "', PLN.PLAN_QTY, 0)) AS D1" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[2] + "', PLN.PLAN_QTY, 0)) AS D2" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[3] + "', PLN.PLAN_QTY, 0)) AS D3" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[4] + "', PLN.PLAN_QTY, 0)) AS D4" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[5] + "', PLN.PLAN_QTY, 0)) AS D5" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[6] + "', PLN.PLAN_QTY, 0)) AS D6" + "\n");
                    strSqlString.AppendFormat("          FROM CWIPPLNASY@RPTTOMES PLN" + "\n");
                    strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                    strSqlString.AppendFormat("           AND PLN.WORK_DAY = '" + dayArry2[0] + "'" + "\n");
                    strSqlString.AppendFormat("           AND PLN.PLAN_FLAG = 'I'   " + "\n");
                    strSqlString.AppendFormat("         GROUP BY PLN.PART_NO " + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("        SELECT PLN.PART_NO" + "\n");
                    strSqlString.AppendFormat("             , SUM(CASE WHEN PLAN_DATE <= '" + dayArry2[0] + "' THEN PLN.PLAN_QTY" + "\n");
                    strSqlString.AppendFormat("                        ELSE 0" + "\n");
                    strSqlString.AppendFormat("                   END) AS D0" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[1] + "', PLN.PLAN_QTY, 0)) AS D1" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[2] + "', PLN.PLAN_QTY, 0)) AS D2" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[3] + "', PLN.PLAN_QTY, 0)) AS D3" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[4] + "', PLN.PLAN_QTY, 0)) AS D4" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[5] + "', PLN.PLAN_QTY, 0)) AS D5" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[6] + "', PLN.PLAN_QTY, 0)) AS D6" + "\n");
                    strSqlString.AppendFormat("          FROM ISTMPLNASY@RPTTOMES PLN" + "\n");
                    strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                    strSqlString.AppendFormat("           AND PLN.WORK_DAY = '" + dayArry2[0] + "'" + "\n");
                    strSqlString.AppendFormat("           AND PLN.PLAN_FLAG = 'I'   " + "\n");
                    strSqlString.AppendFormat("         GROUP BY PLN.PART_NO " + "\n");
                }

                strSqlString.AppendFormat("       ) SPN" + "\n");
                strSqlString.AppendFormat(" WHERE DAT.PART_NO = SPN.PART_NO(+)  " + "\n");
                strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond3);
                strSqlString.AppendFormat("HAVING ( " + "\n");

                // 2010-09-16-임종우 : 월생산 현황 체크 박스 유무에 따라 데이터 표시 변경.
                if (ckbMon.Checked == true)
                {
                    strSqlString.AppendFormat("        SUM(NVL(DAT.MON_PLAN, 0)) + SUM(NVL(DAT.MON_QTY, 0)) + SUM(NVL(SPN.D0, 0)) + " + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("        SUM(NVL(SPN.D0, 0)) + " + "\n");
                }

                // 2010-09-15-임종우 : IN/OUT 계획 선택에 의한 재공 표시 부분 수정으로 인해 변경 함.
                strSqlString.AppendFormat("        SUM(NVL(DAT.TODAY_QTY, 0)) + SUM(NVL(DAT.STOCK, 0) + NVL(DAT.BG_SAW, 0)) + " + "\n");
                strSqlString.AppendFormat("        SUM(NVL(SPN.D0, 0)) + SUM(NVL(SPN.D1, 0)) + SUM(NVL(SPN.D2, 0)) + " + "\n");
                strSqlString.AppendFormat("        SUM(NVL(SPN.D3, 0)) + SUM(NVL(SPN.D4, 0)) + SUM(NVL(SPN.D5, 0)) + SUM(NVL(SPN.D6, 0)) " + "\n");
                strSqlString.AppendFormat("       ) <> 0 " + "\n");
                strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond3);
            }
            #endregion

            #region OUT
            else
            {
                strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond3);
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D0, 0)) AS TODAY_PLAN1" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D0, 0) + NVL(PLAN2.PLAN2_QTY, 0)) AS TODAY_PLAN2" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.TODAY_QTY, 0)) AS TODAY_QTY" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D0, 0) - NVL(DAT.TODAY_QTY, 0)) AS DAY_DEF1" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D0, 0) + NVL(PLAN2.PLAN2_QTY, 0) - NVL(DAT.TODAY_QTY, 0)) AS DAY_DEF2" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.TOTAL, 0)) AS TOTAL" + "\n");
                
                for (int i = 0; i < Oper_Group.Rows.Count; i++)  // 공정 그룹의 재공을 가져옴
                {
                    strSqlString.AppendFormat("     , SUM(NVL(DAT.V{0}, 0)) AS V{1} " + "\n", i, i);
                }

                strSqlString.AppendFormat("     , SUM(NVL(SPN.D0, 0)) AS D0" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D1, 0)) AS D1" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D2, 0)) AS D2" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D3, 0)) AS D3" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D4, 0)) AS D4" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D5, 0)) AS D5" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(SPN.D6, 0)) AS D6" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.MON_PLAN, 0)) AS MON_PLAN" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.MON_QTY, 0)) AS MON_QTY" + "\n");
                strSqlString.AppendFormat("     , SUM(NVL(DAT.MON_DEF, 0)) AS MON_DEF" + "\n");
                strSqlString.AppendFormat("     , SUM(ROUND(DAT.MON_DEF / " + remain + ", 0)) AS TARGET" + "\n");
                strSqlString.AppendFormat("  FROM (" + "\n" );
                strSqlString.AppendFormat("        SELECT {0}" + "\n", QueryCond1);                
                strSqlString.AppendFormat("             , SUM(NVL(SHP.TODAY_QTY, 0)) AS TODAY_QTY" + "\n");                
                strSqlString.AppendFormat("             , SUM(NVL(WIP.TOTAL, 0)) AS TOTAL" + "\n");
             
                for (int i = 0; i < Oper_Group.Rows.Count; i++)  // 공정 그룹의 재공을 가져옴
                {
                    strSqlString.AppendFormat("             , SUM(NVL(WIP.V{0}, 0)) AS V{1} " + "\n", i, i);
                }

                strSqlString.AppendFormat("             , SUM(NVL(PLN.PLAN_QTY, 0)) AS MON_PLAN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(SHP.MON_QTY, 0)) AS MON_QTY" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(PLN.PLAN_QTY, 0) - NVL(SHP.MON_QTY, 0)) AS MON_DEF" + "\n");
                
                // 잔여일이 0보다 작거나 같으면 목표는 0
                //if (remain > 0)
                //{
                //    strSqlString.AppendFormat("     , CASE WHEN SUM(NVL(PLN.PLAN_QTY, 0) - NVL(SHP.MON_QTY, 0)) < 0 THEN 0" + "\n");
                //    strSqlString.AppendFormat("            ELSE SUM(ROUND((NVL(PLN.PLAN_QTY, 0) - NVL(SHP.MON_QTY, 0)) / " + remain + ", 0))" + "\n");
                //    strSqlString.AppendFormat("       END TARGET" + "\n");
                //}
                //else
                //{
                //    strSqlString.AppendFormat("     , 0 AS TARGET" + "\n");
                //}
                strSqlString.AppendFormat("          FROM (" + "\n");
                strSqlString.AppendFormat("                SELECT FACTORY, PLAN_MONTH, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY" + "\n");
                strSqlString.AppendFormat("                  FROM CWIPPLNMON" + "\n");
                strSqlString.AppendFormat("                 WHERE PLAN_MONTH = '" + stMonth + "'" + "\n");
                strSqlString.AppendFormat("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.AppendFormat("                   AND MAT_ID LIKE 'SEK%'" + "\n");
                strSqlString.AppendFormat("                 GROUP BY FACTORY, PLAN_MONTH, MAT_ID" + "\n");
                strSqlString.AppendFormat("               ) PLN" + "\n");

                // 출하수량
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("                     , SUM(NVL(SHP_QTY_1, 0)) AS MON_QTY " + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(WORK_DATE,'" + stToday + "', NVL(SHP_QTY_1, 0), 0)) AS TODAY_QTY " + "\n");

                // 2010-09-22-임종우 : 실적값에 Return 수량 포함 시킴 (임태성 요청)
                //strSqlString.AppendFormat("          FROM VSUMWIPSHP " + "\n");
                strSqlString.AppendFormat("                  FROM VSUMWIPSHP_ONLY " + "\n");
                strSqlString.AppendFormat("                 WHERE 1 = 1 " + "\n");
                strSqlString.AppendFormat("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.AppendFormat("                   AND CM_KEY_2 = 'PROD'" + "\n");
                strSqlString.AppendFormat("                   AND CM_KEY_3 LIKE 'P%'" + "\n");
                strSqlString.AppendFormat("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.AppendFormat("                   AND WORK_DATE BETWEEN '" + stStartDay + "' AND '" + stToday + "'" + "\n");
                strSqlString.AppendFormat("                   AND MAT_ID LIKE 'SEK%'" + "\n");
                strSqlString.AppendFormat("                 GROUP BY MAT_ID" + "\n");
                strSqlString.AppendFormat("               ) SHP" + "\n");
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("                     , SUM(QTY) TOTAL " + "\n");

                for (int i = 0; i < Oper_Group.Rows.Count; i++)  // 공정 그룹의 재공을 가져옴
                {
                    strSqlString.AppendFormat("                     , SUM(DECODE(OPER_GRP_1, '{0}', QTY, 0)) V{1} " + "\n", Oper_Group.Rows[i][0].ToString(), i);
                }

                strSqlString.AppendFormat("                  FROM (" + "\n");
                strSqlString.AppendFormat("                        SELECT A.MAT_ID " + "\n");
                strSqlString.AppendFormat("                             , B.OPER_GRP_1 " + "\n");
                strSqlString.AppendFormat("                             , SUM(A.QTY_1)AS QTY  " + "\n");

                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {
                    strSqlString.AppendFormat("                          FROM RWIPLOTSTS A" + "\n");
                    strSqlString.AppendFormat("                             , MWIPOPRDEF B" + "\n");
                    strSqlString.AppendFormat("                         WHERE 1=1" + "\n");
                    strSqlString.AppendFormat("                           AND A.FACTORY = B.FACTORY " + "\n");
                    strSqlString.AppendFormat("                           AND A.OPER = B.OPER " + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("                          FROM RWIPLOTSTS_BOH A " + "\n");
                    strSqlString.AppendFormat("                             , MWIPOPRDEF B" + "\n");
                    strSqlString.AppendFormat("                         WHERE 1=1" + "\n");
                    strSqlString.AppendFormat("                           AND A.FACTORY = B.FACTORY " + "\n");
                    strSqlString.AppendFormat("                           AND A.OPER = B.OPER " + "\n");
                    strSqlString.AppendFormat("                           AND CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");
                }


                strSqlString.AppendFormat("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.AppendFormat("                           AND A.MAT_ID LIKE 'SEK%' " + "\n");
                strSqlString.AppendFormat("                           AND A.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.AppendFormat("                           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.AppendFormat("                           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.AppendFormat("                           AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
                strSqlString.AppendFormat("                         GROUP BY A.MAT_ID, B.OPER_GRP_1   " + "\n");                
                strSqlString.AppendFormat("                       )  " + "\n");
                strSqlString.AppendFormat("                 GROUP BY MAT_ID " + "\n");
                strSqlString.AppendFormat("               ) WIP" + "\n");

                // 2010-09-09-임종우 : 제품 속성 중 SEC_VERSION 표시 요청 (임태성 요청)
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT * " + "\n");
                strSqlString.AppendFormat("                  FROM MATRNAMSTS " + "\n");
                strSqlString.AppendFormat("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.AppendFormat("                   AND ATTR_TYPE = 'MAT_ETC' " + "\n");
                strSqlString.AppendFormat("                   AND ATTR_NAME = 'SEC_VERSION' " + "\n");
                strSqlString.AppendFormat("               ) ATT" + "\n");
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT A.*, SUBSTR(MAT_CMF_7, 1, LENGTH(MAT_CMF_7)-6) || '___' || SUBSTR(MAT_CMF_7, -3) AS PART_NO " + "\n");
                strSqlString.AppendFormat("                  FROM MWIPMATDEF A " + "\n");
                strSqlString.AppendFormat("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.AppendFormat("                   AND MAT_TYPE = 'FG' " + "\n");
                strSqlString.AppendFormat("                   AND MAT_ID LIKE 'SEK%' " + "\n");
                strSqlString.AppendFormat("                   AND DELETE_FLAG = ' ' " + "\n");
                strSqlString.AppendFormat("               ) MAT" + "\n");
                strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("           AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
                strSqlString.AppendFormat("           AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
                strSqlString.AppendFormat("           AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");                                
                strSqlString.AppendFormat("           AND MAT.MAT_ID = ATT.ATTR_KEY(+)" + "\n");

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

                strSqlString.AppendFormat("         GROUP BY {0}" + "\n", QueryCond2);
                strSqlString.AppendFormat("       ) DAT" + "\n");

                // OUT PLAN의 경우 계획테이블에 LOT이 존재 하므로 해당 LOT 에 대한 현재 재공 수량을 표시 한다.
                strSqlString.AppendFormat("     , (" + "\n");
                
                //if (cboType.SelectedItem.ToString() == "재공 기준") // 재공 기준으로 수량 표시
                if (cboType.SelectedIndex == 0) // 재공 기준으로 수량 표시
                {                    
                    //strSqlString.AppendFormat("        SELECT SUBSTR(PLN.PART_NO, 1, LENGTH(PLN.PART_NO)-6) || '___' || SUBSTR(PLN.PART_NO, -3) AS PART_NO" + "\n");
                    strSqlString.AppendFormat("        SELECT PLN.PART_NO" + "\n");
                    strSqlString.AppendFormat("             , SUM(CASE WHEN PLAN_DATE <= '" + dayArry2[0] + "' THEN STS.QTY_1" + "\n");
                    strSqlString.AppendFormat("                        ELSE 0" + "\n");
                    strSqlString.AppendFormat("                   END) AS D0" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[1] + "', STS.QTY_1, 0)) AS D1" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[2] + "', STS.QTY_1, 0)) AS D2" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[3] + "', STS.QTY_1, 0)) AS D3" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[4] + "', STS.QTY_1, 0)) AS D4" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[5] + "', STS.QTY_1, 0)) AS D5" + "\n");
                    strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[6] + "', STS.QTY_1, 0)) AS D6" + "\n");

                    if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                    {
                        strSqlString.AppendFormat("          FROM CWIPPLNASY@RPTTOMES PLN" + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("          FROM ISTMPLNASY@RPTTOMES PLN" + "\n");
                    }

                    strSqlString.AppendFormat("             , RWIPLOTSTS STS" + "\n");
                    strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                    strSqlString.AppendFormat("           AND PLN.LOT_NO = STS.LOT_ID" + "\n");
                    strSqlString.AppendFormat("           AND PLN.WORK_DAY = '" + dayArry2[0] + "'" + "\n");
                    strSqlString.AppendFormat("           AND PLN.PLAN_FLAG = 'O'   " + "\n");
                    strSqlString.AppendFormat("           AND STS.LOT_CMF_5 LIKE 'P%'" + "\n");
                    strSqlString.AppendFormat("         GROUP BY PLN.PART_NO " + "\n");
                }
                else // 삼성 계획 수량으로 표시
                {
                    if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                    {
                        //strSqlString.AppendFormat("        SELECT SUBSTR(PART_NO, 1, LENGTH(PART_NO)-6) || '___' || SUBSTR(PART_NO, -3) AS PART_NO" + "\n");
                        strSqlString.AppendFormat("        SELECT PLN.PART_NO" + "\n");
                        strSqlString.AppendFormat("             , SUM(CASE WHEN PLAN_DATE <= '" + dayArry2[0] + "' THEN PLN.PLAN_QTY" + "\n");
                        strSqlString.AppendFormat("                        ELSE 0" + "\n");
                        strSqlString.AppendFormat("                   END) AS D0" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[1] + "', PLN.PLAN_QTY, 0)) AS D1" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[2] + "', PLN.PLAN_QTY, 0)) AS D2" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[3] + "', PLN.PLAN_QTY, 0)) AS D3" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[4] + "', PLN.PLAN_QTY, 0)) AS D4" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[5] + "', PLN.PLAN_QTY, 0)) AS D5" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[6] + "', PLN.PLAN_QTY, 0)) AS D6" + "\n");
                        strSqlString.AppendFormat("          FROM CWIPPLNASY@RPTTOMES PLN" + "\n");
                        strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                        strSqlString.AppendFormat("           AND PLN.WORK_DAY = '" + dayArry2[0] + "'" + "\n");
                        strSqlString.AppendFormat("           AND PLN.PLAN_FLAG = 'O'   " + "\n");
                        strSqlString.AppendFormat("         GROUP BY PLN.PART_NO " + "\n");
                    }
                    else
                    {
                        //strSqlString.AppendFormat("        SELECT SUBSTR(PART_NO, 1, LENGTH(PART_NO)-6) || '___' || SUBSTR(PART_NO, -3) AS PART_NO" + "\n");
                        strSqlString.AppendFormat("        SELECT PLN.PART_NO" + "\n");
                        strSqlString.AppendFormat("             , SUM(CASE WHEN PLAN_DATE <= '" + dayArry2[0] + "' THEN PLN.PLAN_QTY" + "\n");
                        strSqlString.AppendFormat("                        ELSE 0" + "\n");
                        strSqlString.AppendFormat("                   END) AS D0" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[1] + "', PLN.PLAN_QTY, 0)) AS D1" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[2] + "', PLN.PLAN_QTY, 0)) AS D2" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[3] + "', PLN.PLAN_QTY, 0)) AS D3" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[4] + "', PLN.PLAN_QTY, 0)) AS D4" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[5] + "', PLN.PLAN_QTY, 0)) AS D5" + "\n");
                        strSqlString.AppendFormat("             , SUM(DECODE(PLAN_DATE, '" + dayArry2[6] + "', PLN.PLAN_QTY, 0)) AS D6" + "\n");
                        strSqlString.AppendFormat("          FROM ISTMPLNASY@RPTTOMES PLN" + "\n");
                        strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                        strSqlString.AppendFormat("           AND PLN.WORK_DAY = '" + dayArry2[0] + "'" + "\n");
                        strSqlString.AppendFormat("           AND PLN.PLAN_FLAG = 'O'   " + "\n");
                        strSqlString.AppendFormat("         GROUP BY PLN.PART_NO " + "\n");
                    }
                }
                
                strSqlString.AppendFormat("       ) SPN" + "\n");

                // 2010-09-16-임종우 : OUT PLAN 조회 시 생산관리팀에서 업로드하는 계획 값 표시 (임태성 요청)
                strSqlString.AppendFormat("     , (" + "\n");
                strSqlString.AppendFormat("        SELECT MAT_ID AS PART_NO" + "\n");
                strSqlString.AppendFormat("             , SUM(PLAN_QTY) AS PLAN2_QTY " + "\n");
                strSqlString.AppendFormat("          FROM CWIPPLNASY@RPTTOMES" + "\n");
                strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("           AND PLAN_FLAG = 'M'   " + "\n");
                strSqlString.AppendFormat("           AND WORK_DAY = '" + dayArry2[0] + "'" + "\n");
                strSqlString.AppendFormat("           AND PLAN_DATE = '" + dayArry2[0] + "'" + "\n");
                strSqlString.AppendFormat("         GROUP BY MAT_ID " + "\n");
                strSqlString.AppendFormat("       ) PLAN2" + "\n");

                strSqlString.AppendFormat(" WHERE DAT.PART_NO = SPN.PART_NO(+)  " + "\n");
                strSqlString.AppendFormat("   AND DAT.PART_NO = PLAN2.PART_NO(+)  " + "\n");
                strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond3);
                strSqlString.AppendFormat("HAVING ( " + "\n");

                // 2010-09-16-임종우 : 월생산 현황 체크 박스 유무에 따라 데이터 표시 변경.
                if (ckbMon.Checked == true)
                {
                    strSqlString.AppendFormat("        SUM(NVL(DAT.MON_PLAN, 0)) + SUM(NVL(DAT.MON_QTY, 0)) + SUM(NVL(SPN.D0, 0)) + " + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("        SUM(NVL(SPN.D0, 0)) + " + "\n");
                }

                strSqlString.AppendFormat("        SUM(NVL(DAT.TODAY_QTY, 0)) + SUM(NVL(DAT.TOTAL, 0)) + SUM(NVL(PLAN2.PLAN2_QTY, 0)) +" + "\n");
                strSqlString.AppendFormat("        SUM(NVL(SPN.D0, 0)) + SUM(NVL(SPN.D1, 0)) + SUM(NVL(SPN.D2, 0)) + " + "\n");
                strSqlString.AppendFormat("        SUM(NVL(SPN.D3, 0)) + SUM(NVL(SPN.D4, 0)) + SUM(NVL(SPN.D5, 0)) + SUM(NVL(SPN.D6, 0)) " + "\n");
                strSqlString.AppendFormat("       ) <> 0 " + "\n");
                strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond3);
            }
            #endregion





            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

           

        #region ToExcel

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null);
        }

        #endregion

        #region 월 누적 Cell을 클릭 했을 경우의 팝업창
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //string stType = spdData.ActiveSheet.Cells[e.Row, e.Column].Column.Label.ToString();
                        
            string stType = null;
            string[] dataArry = new string[11];
            int j = 0;
                        
            // OUT PLAN 이면서 계획 일자 클릭 시 팝업 창 띄움.
            if (rdbOUT.Checked == true && e.Column >= iPlanStart && e.Column <= iPlanStart + 6 && spdData.ActiveSheet.Cells[e.Row, e.Column].Text != "")
            {
                Color BackColor = spdData.ActiveSheet.Cells[1, iPlanStart].BackColor;

                // GrandTotal 과 SubTotal 제외하기 위해
                if (e.Row != 0 && spdData.ActiveSheet.Cells[e.Row, iPlanStart].BackColor == BackColor)                
                {
                    // Group 정보 가져와서 담기... 상세 팝업에서 조건값으로 사용하기 위해
                    for (int i = 0; i < 11; i++)
                    {
                        dataArry[i] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                    }

                    // 고객사 명을 고객사 코드로 변경하기 위해..
                    if (dataArry[0] != " ")
                    {
                        DataTable dtCustomer = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlCustomer(dataArry[0].ToString()));

                        dataArry[0] = dtCustomer.Rows[0][0].ToString();
                    }

                    // 클릭한 컬럼의 위치를 비교하여 날짜 값 가져오기
                    for (int i = j + iPlanStart; i <= iPlanStart + 6; i++)
                    {
                        if (e.Column == i)
                            stType = dayArry2[j];
                        j++;
                    }
                                        
                    DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(stType, dataArry));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        System.Windows.Forms.Form frm = new PRD010903_P1("", dt);
                        frm.ShowDialog();
                    }
                }
            }
            else
            {
                return;
            }
        }
        #endregion

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
        private string MakeSqlDetail(string stType, string[] dataArry)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT ROWNUM, A.*" + "\n");
            strSqlString.AppendFormat("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT STS.OPER" + "\n");
            strSqlString.AppendFormat("             , STS.MAT_ID" + "\n");
            strSqlString.AppendFormat("             , STS.LOT_ID" + "\n");
            strSqlString.AppendFormat("             , STS.QTY_1" + "\n");
            strSqlString.AppendFormat("             , DECODE(HOLD_FLAG, 'Y', 'HOLD', DECODE(STS.QTY_1, 0, DECODE(OPER, 'A0400', 'SPLIT', 'MERGE'), DECODE(OPER, ' ', 'SHIP', DECODE(LOT_STATUS, 'PROC', 'RUN', 'WAIT')))) AS STATUS " + "\n");
            strSqlString.AppendFormat("             , DECODE(STS.LOT_CMF_14, ' ', 0, ROUND(SYSDATE - TO_DATE(STS.LOT_CMF_14, 'YYYYMMDDHH24MISS'), 2)) AS TAT" + "\n");
            strSqlString.AppendFormat("          FROM CWIPPLNASY@RPTTOMES PLN" + "\n");
            strSqlString.AppendFormat("             , RWIPLOTSTS STS" + "\n");
            strSqlString.AppendFormat("             , MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("         WHERE 1=1" + "\n");
            strSqlString.AppendFormat("           AND PLN.WORK_DAY = '" + dayArry2[0] + "'" + "\n");
            strSqlString.AppendFormat("           AND PLN.PLAN_FLAG = 'O'  " + "\n");

            // 7일중에 첫째일 즉 조회일 이면 계획일이 본인보다 작은것도 모두 가져온다.
            if (dayArry2[0] == stType)
            {
                strSqlString.AppendFormat("           AND PLN.PLAN_DATE <= '" + stType + "'" + "\n");
            }
            else
            {
                strSqlString.AppendFormat("           AND PLN.PLAN_DATE = '" + stType + "'" + "\n");
            }

            strSqlString.AppendFormat("           AND PLN.LOT_NO = STS.LOT_ID " + "\n");
            strSqlString.AppendFormat("           AND PLN.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (dataArry[0] != " ")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 = '" + dataArry[0] + "'" + "\n");

            if (dataArry[1] != " ")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 = '" + dataArry[1] + "'" + "\n");

            if (dataArry[2] != " ")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 = '" + dataArry[2] + "'" + "\n");

            if (dataArry[3] != " ")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 = '" + dataArry[3] + "'" + "\n");

            if (dataArry[4] != " ")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 = '" + dataArry[4] + "'" + "\n");

            if (dataArry[5] != " ")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 = '" + dataArry[5] + "'" + "\n");

            if (dataArry[6] != " ")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 = '" + dataArry[6] + "'" + "\n");

            if (dataArry[7] != " ")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 = '" + dataArry[7] + "'" + "\n");

            if (dataArry[8] != " ")
                strSqlString.AppendFormat("           AND MAT.MAT_CMF_10 = '" + dataArry[8] + "'" + "\n");

            if (dataArry[9] != " ")
                strSqlString.AppendFormat("           AND MAT.MAT_CMF_7 = '" + dataArry[9] + "'" + "\n");

            strSqlString.AppendFormat("         ORDER BY OPER, MAT_ID, LOT_ID, STATUS" + "\n");
            strSqlString.AppendFormat("       ) A" + "\n");
                        
            #endregion
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion MakeSqlDetail

        #region 날짜 구하기
        private void GetDayArray()
        {
            DateTime getStartDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");  // 조회일 기준 해당 월의 시작일

            string getEndDate = getStartDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");         // 조회일 기준 해당월의 마지막 일
            string getYesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");                   // 조회일 기준 어제 날짜

            string yesterday = getYesterday.Substring(6, 2);
            string lastday = getEndDate.Substring(6, 2);

            // 잔여일

            if (rdbIN.Checked == true)
            {
                remain = Convert.ToInt32(lastday) - Convert.ToInt32(yesterday) -3 ;  //총 일수 - 전일 - 3day
            }
            else
            {
                remain = Convert.ToInt32(lastday) - Convert.ToInt32(yesterday);      //총 일수 - 전일
            }

            // 잔여일이 0일이면 1일로 변경 - 일 목표 계산시 분모 값이 0이 되는거 방지 위해...
            if (remain <= 0)
            {
                remain = 1;
            }
            
            dayArry[0] = cdvDate.Value.ToString("MM.dd");
            dayArry[1] = cdvDate.Value.AddDays(1).ToString("MM.dd");
            dayArry[2] = cdvDate.Value.AddDays(2).ToString("MM.dd");
            dayArry[3] = cdvDate.Value.AddDays(3).ToString("MM.dd");
            dayArry[4] = cdvDate.Value.AddDays(4).ToString("MM.dd");
            dayArry[5] = cdvDate.Value.AddDays(5).ToString("MM.dd");
            dayArry[6] = cdvDate.Value.AddDays(6).ToString("MM.dd");

            dayArry2[0] = cdvDate.Value.ToString("yyyyMMdd");
            dayArry2[1] = cdvDate.Value.AddDays(1).ToString("yyyyMMdd");
            dayArry2[2] = cdvDate.Value.AddDays(2).ToString("yyyyMMdd");
            dayArry2[3] = cdvDate.Value.AddDays(3).ToString("yyyyMMdd");
            dayArry2[4] = cdvDate.Value.AddDays(4).ToString("yyyyMMdd");
            dayArry2[5] = cdvDate.Value.AddDays(5).ToString("yyyyMMdd");
            dayArry2[6] = cdvDate.Value.AddDays(6).ToString("yyyyMMdd");
        }

        private string MakeSqlString2(string type)   // 공정그룹을 순서대로 가져옴
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT OPER_GRP_1" + "\n");
            strSqlString.AppendFormat("  FROM MWIPOPRDEF " + "\n");
            strSqlString.AppendFormat(" WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.AppendFormat("   AND OPER_CMF_4 <> ' '    " + "\n");
            strSqlString.AppendFormat(" GROUP BY OPER_GRP_1" + "\n");
            strSqlString.AppendFormat(" ORDER BY TO_NUMBER(MIN(OPER_CMF_4)) " + type + "\n");

            return strSqlString.ToString();
        }

        #endregion

        private void rdbOUT_CheckedChanged(object sender, EventArgs e)
        {
            // OUT 선택시 재공 or 계획 기준 선택 창 띄움.
            if (rdbOUT.Checked)
            {
                cboType.Visible = true;
            }
            else
            {
                cboType.Visible = false;
            }
        }
    }
}