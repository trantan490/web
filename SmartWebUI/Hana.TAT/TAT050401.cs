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

using System.Windows.Forms.DataVisualization.Charting;

namespace Hana.TAT
{
    public partial class TAT050401 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private DataTable dtOper = null;
        private DataTable dtChart = null;

        /// <summary>
        /// 클  래  스: TAT050401<br/>
        /// 클래스요약: TAT Trend by operation<br/>
        /// 작  성  자: 미라콤 김태순<br/>
        /// 최초작성일: 2009-01-28<br/>
        /// 상세  설명: TAT trend by oper를 조회한다.<br/>
        /// 변경  내용: <br/>
        /// 2009-12-14-임종우 : 생산실적공정Group(OPER_GRP_7) -> 생산재공공정Group(OPER_GRP_1)으로 변경 (임태성 요청)
        /// 2009-12-15-임종우 : SHIP 수량 중복 되는 부분 수정.
        /// 2010-01-26-임종우 : LOT TYPE 검색 추가 (P%, E%)
        /// 2010-01-27-임종우 : Detail 검색에서 (ALL, ACUM) 삭제, (WAIT&RUN) 추가
        /// 2010-01-28-임종우 : FLOW 검색 추가-너무 많은 공정으로 인해..(Detail 검색시에 필수 선택 해야 함. 요약검색은 사용하지 않음.)
        /// 2010-02-04-임종우 : FLOW 검색 ALL 검색 가능하도록 변경 함.
        /// 2010-02-24-임종우 : Detail 검색 시 Total Wait, Total Run 추가 표시.
        /// 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
        /// 2011-09-16-김민우 : WIP_TAT(재공 TAT) 추가
        /// 2012-02-16-임종우 : 공정 리스트 가져오는 부분 쿼리 튜닝.
        /// 2013-05-03-임종우 : Package 2 그룹 조건 추가 (김동인 요청)
        /// 2014-05-20-임종우 : WIP_TAT HMKT 도 조회 가능하도록 수정
        /// 2014-12-31-임종우 : MAT ID 검색 기능 추가 (임태성K 요청)
        ///                     그룹 정보에 PKG CODE 추가 (임태성K 요청)
        /// 2015-03-03-오득연 : ChartFX -> MS Chart로 변경
        /// 2017-08-17-임종우 : Un-Kit Hold Time 제외 검색 기능 추가 (임태성C 요청)
        /// 2020-01-22-김미경 : FLOW 검색 오류 수정;해당 FLOW 안에 있는 공정 기준으로 조회되는 것으로 보임. FLOW 기준으로 조회되도록 수정 (왕경식 S)
        /// 2020-02-24-김미경 : Flow 내에 속하지 않는 공정들만 조회하는 경우 오류 발생.
        /// </summary>
        public TAT050401()
        {
            InitializeComponent();
            udcDurationDate1.AutoBinding();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리 
            cboType.SelectedIndex = 0;
            //udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 
            udcMSChart1.RPT_1_ChartInit();  //차트 초기화.

            //InitChart();
            cbLotType.Text = "ALL";
        }

        #region SortInit


        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();

            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Date", "SHIP_DATE", "SHIP_DATE", "(SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE SYS_DATE = TAT.SHIP_DATE AND CALENDAR_ID='HM') AS SHIP_DATE", "(SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_MONTH,2,'0') FROM MWIPCALDEF WHERE SYS_DATE = TAT.SHIP_DATE AND CALENDAR_ID='HM') AS SHIP_DATE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Bumping Type", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation Flow", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Type", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL Plating", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Final Bump", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Sub. Material", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Size", "MAT_CMF_14", "MAT_CMF_14", "MAT_CMF_14", "MAT_CMF_14", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Thickness", "MAT_CMF_2", "MAT_CMF_2", "MAT_CMF_2", "MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Flat Type", "MAT_CMF_3", "MAT_CMF_3", "MAT_CMF_3", "MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Wafer Orientation", "MAT_CMF_4", "MAT_CMF_4", "MAT_CMF_4", "MAT_CMF_4", false);

            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Date", "SHIP_DATE", "SHIP_DATE", "(SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE SYS_DATE = TAT.SHIP_DATE AND CALENDAR_ID='HM') AS SHIP_DATE", "(SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_MONTH,2,'0') FROM MWIPCALDEF WHERE SYS_DATE = TAT.SHIP_DATE AND CALENDAR_ID='HM') AS SHIP_DATE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", true);
                // 2009.06.15 Bongjun Park, PinType 추가
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PinType", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Majog Code", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package_2", "MAT_GRP_10", "MAT_GRP_10", "MAT_GRP_10", "MAT_GRP_10", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Code", "MAT_CMF_11", "MAT_CMF_11", "MAT_CMF_11", "MAT_CMF_11", false);
            }
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
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();

                if (cdvFactory.txtValue.Equals("HMKB1"))
                {
                    spdData.RPT_AddBasicColumn("Date", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Sub. Material", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Size", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Date", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PinType", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Family", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Majog Code", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Package_2", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Package_2", 0, 12, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

                }

                spdData.RPT_AddBasicColumn("TAT", 0, 13, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Double2, 60);

                if (rdoDetail.Checked == true)
                {
                    if (dtOper != null)
                    {
                        if (cboType.Text.ToString().CompareTo("WAIT&RUN") == 0)
                        {
                            int headerCount = 16;

                            spdData.RPT_AddBasicColumn("WAIT", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 60);
                            spdData.RPT_AddBasicColumn("RUN", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 60);
                            spdData.RPT_MerageHeaderRowSpan(0, 14, 2);
                            spdData.RPT_MerageHeaderRowSpan(0, 15, 2);

                            for (int i = 0; i < dtOper.Rows.Count; i++)
                            {
                                spdData.RPT_AddBasicColumn(dtOper.Rows[i][0].ToString(), 0, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 120);
                               
                                spdData.RPT_AddBasicColumn("Wait", 1, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;
                                spdData.RPT_AddBasicColumn("WaitSUM", 1, headerCount, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;
                                spdData.RPT_AddBasicColumn("WaitCNT", 1, headerCount, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;

                                spdData.RPT_AddBasicColumn("Run", 1, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;
                                spdData.RPT_AddBasicColumn("RunSUM", 1, headerCount, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;
                                spdData.RPT_AddBasicColumn("RunCNT", 1, headerCount, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;

                                spdData.RPT_MerageHeaderColumnSpan(0, headerCount - 6, 6);
                            }
                        }
                        else
                        {
                            int headerCount = 14;

                            for (int i = 0; i < dtOper.Rows.Count; i++)
                            {
                                spdData.RPT_AddBasicColumn(dtOper.Rows[i][0].ToString(), 0, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                                spdData.RPT_AddBasicColumn(cboType.Text.ToString(), 1, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;
                                spdData.RPT_AddBasicColumn("SUM", 1, headerCount, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;
                                spdData.RPT_AddBasicColumn("CNT", 1, headerCount, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;
                                spdData.RPT_MerageHeaderColumnSpan(0, headerCount - 3, 3);

                            }
                        }
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
                    spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                }
                else
                {
                    // 2009.06.12 Bongjun Park Hearder의 공정 범위를 불러와 보여준다.
                    if (dtOper != null)
                    {
                        if (ckbWR.Checked == false)
                        {
                            for (int i = 0; i < dtOper.Rows.Count; i++)
                            {
                                spdData.RPT_AddBasicColumn(dtOper.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Double2, 40);
                                spdData.RPT_AddBasicColumn("CSUM", 0, spdData.ActiveSheet.Columns.Count, Visibles.False, Frozen.False, Align.Right, Merge.True, Formatter.Double2, 40);
                                spdData.RPT_AddBasicColumn("CCNT", 0, spdData.ActiveSheet.Columns.Count, Visibles.False, Frozen.False, Align.Right, Merge.True, Formatter.Double2, 40);
                            }
                        }
                        else
                        {
                            int headerCount = 16;

                            spdData.RPT_AddBasicColumn("WAIT", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 60);
                            spdData.RPT_AddBasicColumn("RUN", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 60);
                            spdData.RPT_MerageHeaderRowSpan(0, 14, 2);
                            spdData.RPT_MerageHeaderRowSpan(0, 15, 2);

                            for (int i = 0; i < dtOper.Rows.Count; i++)
                            {
                                spdData.RPT_AddBasicColumn(dtOper.Rows[i][0].ToString(), 0, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 120);

                                spdData.RPT_AddBasicColumn("Wait", 1, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;
                                spdData.RPT_AddBasicColumn("WaitSUM", 1, headerCount, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;
                                spdData.RPT_AddBasicColumn("WaitCNT", 1, headerCount, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;

                                spdData.RPT_AddBasicColumn("Run", 1, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;
                                spdData.RPT_AddBasicColumn("RunSUM", 1, headerCount, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;
                                spdData.RPT_AddBasicColumn("RunCNT", 1, headerCount, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                                headerCount++;

                                spdData.RPT_MerageHeaderColumnSpan(0, headerCount - 6, 6);
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
                            spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
                            spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                            spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                        }
                    }
                }

                spdData.RPT_ColumnConfigFromTable_New(btnSort); //Group항목이 있을경우 반드시 선업해줄것.

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
            if (!CheckField()) return;

            DataTable dt = null;

            // 공정 코드 가져오기
            dtOper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeOperTable());            
            if (dtOper.Rows.Count == 0)
            {
                dtOper.Dispose();
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                return;
            }

            dtChart = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlChart());

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

                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort);

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);


                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                //--------------------------------------------

                // Sub Total 평균값으로 구하기(TAT)
                //SetAvgVertical(1, 10, true);
                int countOper = 0;
                if (rdoDetail.Checked == true && cboType.Text.ToString().CompareTo("WAIT&RUN") == 0)
                {
                    //2010-02-24-임종우 : TOTAL_WAIT, TOTAL_RUN 컬럼을 추가 하여 각 3개씩 더한다.
                    countOper = dtOper.Rows.Count*6;

                    for (int i = 0; i < countOper; i += 3)
                    {
                        SetAvgVertical(1, 16 + i, true);
                    }

                    // 각각의 TAT 컬럼 SUM 값 구하여 Total TAT 컬럼 값 넣어주기
                    SetAvgVertical2(1, 16, true);
                }
                else if (rdoDetail.Checked == false && ckbWR.Checked == true) // 요약이면서 Wait & Run 선택일 경우
                {                    
                    countOper = dtOper.Rows.Count * 6;

                    for (int i = 0; i < countOper; i += 3)
                    {
                        SetAvgVertical(1, 16 + i, true);
                    }
                                     
                    SetAvgVertical2(1, 16, true);
                }
                else
                {
                    countOper = dtOper.Rows.Count*3;

                    for (int i = 0; i < countOper; i += 3)
                    {
                        SetAvgVertical(1, 14 + i, true);
                    }

                    // 각각의 TAT 컬럼 SUM 값 구하여 Total TAT 컬럼 값 넣어주기
                    SetAvgVertical2(1, 14, false);
                }

                //Chart 생성
                if (spdData.ActiveSheet.RowCount > 0)
                    ShowChart(0);
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

        public void SetAvgVertical2(int nSampleNormalRowPos, int nColPos, bool bDetail)
        {            
            double sum = 0;
            double wsum = 0;
            double rsum = 0;

            for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                //2010-02-04-임종우 : ACUM 제거로 인해 사용중지
                //if (cboType.Text.ToString().CompareTo("WAIT&RUN") == 0 && rdoDetail.Checked == true)
                //{
                //    for (int i = 0; i < spdData.ActiveSheet.Columns.Count - 1; i = i + 6)
                //    {
                //        if (spdData.ActiveSheet.Columns.Count <= nColPos + i)
                //            continue;
                //        sum += Convert.ToDouble(spdData.ActiveSheet.Cells[j, nColPos + i].Value);
                //    }
                //}
                //else
                //{

                // 2010-02-25-임종우 : Detail 이면서 Wait&Run 일 경우(Total_Wait, Total_Run 의 값도 구해줌)
                if (bDetail)
                {
                    for (int i = 0; i < spdData.ActiveSheet.Columns.Count - 1; i = i + 3)
                    {
                        if (spdData.ActiveSheet.Columns.Count <= nColPos + i)
                            continue;
                        sum += Convert.ToDouble(spdData.ActiveSheet.Cells[j, nColPos + i].Value);

                        if (spdData.ActiveSheet.Cells[j, nColPos + i].Column.Label == "Wait")
                        {
                            wsum += Convert.ToDouble(spdData.ActiveSheet.Cells[j, nColPos + i].Value);
                        }
                        else if (spdData.ActiveSheet.Cells[j, nColPos + i].Column.Label == "Run")
                        {
                            rsum += Convert.ToDouble(spdData.ActiveSheet.Cells[j, nColPos + i].Value);
                        }
                    }
                    //}
                    spdData.ActiveSheet.Cells[j, 13].Value = sum;
                    spdData.ActiveSheet.Cells[j, 14].Value = wsum;
                    spdData.ActiveSheet.Cells[j, 15].Value = rsum;

                }
                else
                {
                    for (int i = 0; i < spdData.ActiveSheet.Columns.Count - 1; i = i + 3)
                    {
                        if (spdData.ActiveSheet.Columns.Count <= nColPos + i)
                            continue;
                        sum += Convert.ToDouble(spdData.ActiveSheet.Cells[j, nColPos + i].Value);
                    }
                    spdData.ActiveSheet.Cells[j, 13].Value = sum;
                }

                sum = 0;
                wsum = 0;
                rsum = 0;
            }

        }

        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull)
        {
            double sum = 0;
            double cnt = 0;            
            double totalSum = 0;
            double totalCnt = 0;
            double subSum = 0; // 동일 분류의 서브 토탈의 합
            double subCnt = 0; // 동일 분류의 서브 토탈의 합


            double divide = 0;
            double totalDivide = 0;
            double subDivide = 0; // 동일 분류의 서브 토탈 수량

            int count = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, nColPos].BackColor == color)
                {
                    sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos + 1].Value);
                    cnt += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos + 2].Value);
                    if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos + 1].Value == null || spdData.ActiveSheet.Cells[i, nColPos + 1].Value.ToString().Trim() == ""))
                        continue;

                    divide += 1;
                }
                else
                {
                    if (divide != 0)
                    {
                        // 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
                        if (ckbTime.Checked == true)
                        {
                            if (cnt == 0)
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = sum / 1 * 24;
                            }
                            else
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = sum / cnt * 24;
                            }
                        }
                        else
                        {
                            if (cnt == 0)
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = sum / 1;
                            }
                            else
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = sum / cnt;
                            }
                        }

                        if (count > 2) // Group 항목에서 체크된게 2개 이상인것(서브토탈이 2 Depth 이상인것)
                        {
                            subSum += sum;
                            subCnt += cnt;
                            subDivide += divide;

                            if (spdData.ActiveSheet.Cells[i, nColPos].BackColor == spdData.ActiveSheet.Cells[i + 1, nColPos].BackColor)
                            {
                                // 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
                                if (ckbTime.Checked == true)
                                {
                                    if (subCnt == 0)
                                    {
                                        spdData.ActiveSheet.Cells[i + 1, nColPos].Value = subSum / 1 * 24;
                                    }
                                    else
                                    {
                                        spdData.ActiveSheet.Cells[i + 1, nColPos].Value = subSum / subCnt * 24;
                                    }
                                }
                                else
                                {
                                    if (subCnt == 0)
                                    {
                                        spdData.ActiveSheet.Cells[i + 1, nColPos].Value = subSum / 1;
                                    }
                                    else
                                    {
                                        spdData.ActiveSheet.Cells[i + 1, nColPos].Value = subSum / subCnt;
                                    }
                                }

                                subSum = 0;
                                subCnt = 0;
                                subDivide = 0;
                            }
                        }
                    }

                    totalSum += sum;
                    totalCnt += cnt;
                    totalDivide += divide;

                    sum = 0;
                    cnt = 0;
                    divide = 0;
                }
            }
            if (totalDivide != 0)
            {
                // 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
                if (ckbTime.Checked == true)
                {
                    if (totalCnt == 0)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / 1 * 24;
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / totalCnt * 24;
                    }
                }
                else
                {
                    if (totalCnt == 0)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / 1;
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / totalCnt;
                    }
                }
            }

            return;
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

            if (rdoSummary.Checked == false)
            {
                if (cdvFlow.Text == "")
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD032", GlobalVariable.gcLanguage));
                    return false;
                }
                // 2010-02-01-임종우 : ALL 선택 막혀있던 것 해제- 오류나면 나는데로..(임태성 요청)
                //else if (cdvFlow.Text == "ALL")
                //{
                //    CmnFunction.ShowMsgBox("너무 많은 FLOW를 선택하였습니다.");
                //    return false;
                //}
            }

            return true;
        }

        #endregion

        private String MakeOperTable()
        {
            // 2009-12-15-임종우 요약 조회시 생산재공공정Group으로 변경.
            StringBuilder strSqlString = new StringBuilder();

            // 2012-02-16-임종우 : 공정 리스트 가져오는 부분 쿼리 튜닝.
            if (rdoDetail.Checked == true)
            {
                strSqlString.Append("SELECT OPER" + "\n");                         
            }
            else
            {
                strSqlString.Append("SELECT OPER_GRP_1 " + "\n");
            }

            strSqlString.Append("  FROM MWIPOPRDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND FACTORY='" + cdvFactory.Text.ToString() + "'" + "\n");

            if (cdvOper.FromText != "" && cdvOper.ToText == "")
            {
                //strSqlString.Append("   AND OPER >= '" + cdvOper.FromText + "'" + "\n");
                strSqlString.Append("   AND DECODE(OPER_CMF_2, ' ', 99999, TO_NUMBER(OPER_CMF_2)) >= (SELECT OPER_CMF_2 FROM MWIPOPRDEF WHERE OPER = '" + cdvOper.FromText + "')" + "\n");
            }
            else if (cdvOper.FromText == "" && cdvOper.ToText != "")
            {
                //strSqlString.Append("   AND OPER <= '" + cdvOper.ToText + "'" + "\n");
                strSqlString.Append("   AND DECODE(OPER_CMF_2, ' ', 99999, TO_NUMBER(OPER_CMF_2)) <= (SELECT OPER_CMF_2 FROM MWIPOPRDEF WHERE OPER = '" + cdvOper.ToText + "')" + "\n");
            }
            else if (cdvOper.FromText != "" && cdvOper.ToText != "")
            {
                //strSqlString.Append("   AND OPER BETWEEN '" + cdvOper.FromText + "' AND '" + cdvOper.ToText + "'  " + "\n");
                strSqlString.Append("   AND DECODE(OPER_CMF_2, ' ', 99999, TO_NUMBER(OPER_CMF_2)) BETWEEN (SELECT OPER_CMF_2 FROM MWIPOPRDEF WHERE OPER = '" + cdvOper.FromText + "') AND (SELECT OPER_CMF_2 FROM MWIPOPRDEF WHERE OPER = '" + cdvOper.ToText + "') " + "\n");
            }

            if (rdoDetail.Checked == true)
            {
                strSqlString.Append("   AND OPER IN (" + "\n");
                strSqlString.Append("                SELECT DISTINCT OPER" + "\n");
                strSqlString.Append("                  FROM MWIPFLWOPR@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + cdvFactory.Text.ToString() + "'" + "\n");                

                /// 2010-02-04-임종우 : FLOW 검색 ALL 검색 가능하도록 변경 함.                
                if (cdvFlow.Text == "ALL")
                {
                    strSqlString.Append("                   AND FLOW IN (" + "\n");
                    strSqlString.Append("                                SELECT DISTINCT FIRST_FLOW " + "\n");
                    strSqlString.Append("                                  FROM MWIPMATDEF " + "\n");
                    strSqlString.Append("                                 WHERE FACTORY = '" + cdvFactory.Text.ToString() + "'" + "\n");           
                    strSqlString.Append("                                   AND MAT_TYPE = 'FG' " + "\n");
                    strSqlString.Append("                                   AND DELETE_FLAG = ' ' " + "\n");                    

                    if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                    {
                        strSqlString.AppendFormat("                                   AND MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
                    }

                    #region 상세 조회에 따른 SQL문 생성
                    if (cdvFactory.Text.Trim() == "HMKB1")
                    {
                        if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                        if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                        if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                        if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                        if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                        if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                        if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                        if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                        if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                        if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                        if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                        if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                    }
                    else
                    {
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                    }
                    #endregion

                    strSqlString.Append("                               )" + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND FLOW " + cdvFlow.SelectedValueToQueryString + "\n");
                }
                                
                strSqlString.Append("               )" + "\n");
                strSqlString.Append(" ORDER BY DECODE(OPER_CMF_2, ' ', 99999, TO_NUMBER(OPER_CMF_2)), OPER" + "\n");
            }
            else
            {
                strSqlString.Append("   AND OPER_CMF_4 <> ' ' " + "\n");
                strSqlString.Append(" GROUP BY OPER_GRP_1" + "\n");
                strSqlString.Append(" ORDER BY TO_NUMBER(MIN(OPER_CMF_4)) ASC" + "\n");
                //strSqlString.Append(")ORDER BY OPER_GRP_7 DESC" + "\n");
            }

            //System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        #region MakeSqlChart
        private string MakeSqlChart()
        {
            StringBuilder strSqlString = new StringBuilder();
            string sFrom = udcDurationDate1.HmFromDay;
            string sTo = udcDurationDate1.HmToDay;

            if (rdoDetail.Checked == true)
            {
                strSqlString.Append("SELECT OPR.OPER AS OPER" + "\n");

                // 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
                if (ckbTime.Checked == true)
                {

                    // 2011-09-15-김민우 : WIP_TAT(재공 TAT) 체크 시
                    if (ckbWIPTAT.Checked == true)
                    {
                        // 2011-09-16-김민우 : AZ010 공정 LOT 수량은 END가 아닌 MOVE
                        strSqlString.Append("     , ROUND(((SUM(S1_QUEUE_TIME+S2_QUEUE_TIME+S3_QUEUE_TIME+S4_QUEUE_TIME))/60/60) " + "\n");

                        // 오타 에러 인것 같다. 'AZ010' 이건 A1 공장만 해당 되지 않는가 ?????
                        if (cdvFactory.Text.Trim() == "HMKB1")
                            strSqlString.Append("     / DECODE(OPR.OPER,'BZ010', DECODE(SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT),0,1,SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT))" + "\n");
                        else
                            strSqlString.Append("     / DECODE(OPR.OPER,'AZ010', DECODE(SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT),0,1,SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT))" + "\n");

                        strSqlString.Append("                              , DECODE(SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT),0,1,SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT))),2) AS TAT" + "\n");

                        //strSqlString.Append("     / DECODE(OPR.OPER,'AZ010', SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT)" + "\n");
                        //strSqlString.Append("                              , SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT)),2) AS TAT" + "\n");
                        //strSqlString.Append("     , ROUND(((SUM(S1_QUEUE_TIME+S2_QUEUE_TIME+S3_QUEUE_TIME+S4_QUEUE_TIME))/60/60) / SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT),2) AS TAT" + "\n");
                    }
                    else
                    {
                        //2010-01-27-임종우 Lot_Type별 검색 추가
                        if (cbLotType.Text.Equals("P%"))
                        {
                            strSqlString.Append("     , ROUND(SUM(TOTAL_TAT_QTY_P)/SUM(SHIP.SHIP_QTY) * 24,2) AS TAT" + "\n");
                        }
                        else if (cbLotType.Text.Equals("E%"))
                        {
                            strSqlString.Append("     , ROUND(SUM(TOTAL_TAT_QTY_E)/SUM(SHIP.SHIP_QTY) * 24,2) AS TAT" + "\n");
                        }
                        else
                        {
                            strSqlString.Append("     , ROUND(SUM(TOTAL_TAT_QTY)/SUM(SHIP.SHIP_QTY) * 24,2) AS TAT" + "\n");
                        }
                    }
                }
                else
                {
                    // 2011-09-15-김민우 : WIP_TAT(재공 TAT) 체크 시
                    if (ckbWIPTAT.Checked == true)
                    {
                        // 2011-09-16-김민우 : AZ010 공정 LOT 수량은 END가 아닌 MOVE
                        strSqlString.Append("     , ROUND(((SUM(S1_QUEUE_TIME+S2_QUEUE_TIME+S3_QUEUE_TIME+S4_QUEUE_TIME))/60/60/24) " + "\n");

                        if (cdvFactory.Text.Trim() == "HMKB1")
                            strSqlString.Append("     / DECODE(OPR.OPER,'BZ010', DECODE(SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT),0,1,SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT))" + "\n");
                        else
                            strSqlString.Append("     / DECODE(OPR.OPER,'AZ010', DECODE(SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT),0,1,SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT))" + "\n");

                        strSqlString.Append("                              , DECODE(SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT),0,1,SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT))),2) AS TAT" + "\n");

                        //strSqlString.Append("     / DECODE(OPR.OPER,'AZ010', SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT)" + "\n");
                        //strSqlString.Append("                              , SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT)),2) AS TAT" + "\n");
                        //strSqlString.Append("     , ROUND(((SUM(S1_QUEUE_TIME+S2_QUEUE_TIME+S3_QUEUE_TIME+S4_QUEUE_TIME))/60/60/24) / SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT),2) AS TAT" + "\n");
                    }
                    else
                    {
                        //2010-01-27-임종우 Lot_Type별 검색 추가
                        if (cbLotType.Text.Equals("P%"))
                        {
                            strSqlString.Append("     , ROUND(SUM(TOTAL_TAT_QTY_P)/SUM(SHIP.SHIP_QTY),2) AS TAT" + "\n");
                        }
                        else if (cbLotType.Text.Equals("E%"))
                        {
                            strSqlString.Append("     , ROUND(SUM(TOTAL_TAT_QTY_E)/SUM(SHIP.SHIP_QTY),2) AS TAT" + "\n");
                        }
                        else
                        {
                            strSqlString.Append("     , ROUND(SUM(TOTAL_TAT_QTY)/SUM(SHIP.SHIP_QTY),2) AS TAT" + "\n");
                        }
                    }
                }
                // 2011-09-15-김민우 : WIP_TAT(재공 TAT) 체크 시
                if (ckbWIPTAT.Checked == true)
                {
                    strSqlString.Append("  FROM RSUMWIPMOV TAT " + "\n");
                    strSqlString.Append("      , MWIPOPRDEF OPR " + "\n");
                    strSqlString.Append("      , MWIPMATDEF MAT" + "\n");
                    strSqlString.Append("  WHERE TAT.FACTORY = '" + cdvFactory.Text.ToString() + "'" + "\n");
                    strSqlString.Append("    AND TAT.FACTORY = MAT.FACTORY" + "\n");
                    strSqlString.Append("    AND TAT.FACTORY = OPR.FACTORY" + "\n");
                    strSqlString.Append("    AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
                    strSqlString.Append("    AND MAT.MAT_VER = 1" + "\n");
                    strSqlString.Append("    AND MAT.MAT_TYPE = 'FG' " + "\n");
                    strSqlString.Append("    AND MAT.DELETE_FLAG = ' ' " + "\n");
                    strSqlString.Append("    AND OPR.OPER_GRP_1 NOT IN (' ')" + "\n");
                    strSqlString.Append("    AND OPR.OPER = TAT.OPER" + "\n");
                    strSqlString.Append("    AND TAT.OPER BETWEEN '" + dtOper.Rows[0]["OPER"].ToString() + "' AND '" + dtOper.Rows[dtOper.Rows.Count - 1]["OPER"].ToString() + "'  " + "\n");
                    strSqlString.AppendFormat("    AND TAT.WORK_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
                    if (cbLotType.Text.Equals("P%"))
                    {
                        strSqlString.Append("    AND TAT.CM_KEY_3 LIKE 'P%' " + "\n");
                    }
                    else if (cbLotType.Text.Equals("E%"))
                    {
                        strSqlString.Append("    AND TAT.CM_KEY_3 LIKE 'E%' " + "\n");
                    }

                }
                else
                {

                    strSqlString.Append("  FROM CSUMTATMAT@RPTTOMES TAT " + "\n");
                    strSqlString.Append("      , MWIPOPRDEF OPR " + "\n");
                    strSqlString.Append("      , MWIPMATDEF MAT" + "\n");
                    strSqlString.Append("      , (      " + "\n");
                    strSqlString.Append("        SELECT TAT.FACTORY " + "\n");
                    strSqlString.Append("             , TAT.MAT_ID " + "\n");
                    strSqlString.Append("             , TAT.SHIP_DATE AS WORK_DATE " + "\n");

                    //2010-01-27-임종우 Lot_Type별 검색 추가
                    if (cbLotType.Text.Equals("P%"))
                    {
                        strSqlString.Append("             , SHIP_QTY_P AS SHIP_QTY " + "\n");
                    }
                    else if (cbLotType.Text.Equals("E%"))
                    {
                        strSqlString.Append("             , SHIP_QTY_E AS SHIP_QTY" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("             , SHIP_QTY " + "\n");
                    }

                    strSqlString.Append("          FROM CSUMTATMAT@RPTTOMES TAT " + "\n");
                    strSqlString.Append("         WHERE 1=1      " + "\n");
                    strSqlString.Append("           AND TAT.FACTORY='" + cdvFactory.Text.ToString() + "'      " + "\n");

                    if (cdvFactory.Text.ToString().Equals(GlobalVariable.gsAssyDefaultFactory))
                    {
                        strSqlString.AppendFormat("           AND OPER ='AZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("HMKB1"))
                    {
                        strSqlString.AppendFormat("           AND OPER ='BZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("HMKE1"))
                    {
                        strSqlString.AppendFormat("           AND OPER ='EZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals(GlobalVariable.gsTestDefaultFactory))
                    {
                        strSqlString.AppendFormat("           AND OPER ='TZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("FGS"))
                    {
                        strSqlString.AppendFormat("           AND OPER ='F000N'      " + "\n");
                    }
                    strSqlString.AppendFormat("           AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
                    strSqlString.Append("        ) SHIP      " + "\n");
                    strSqlString.Append("  WHERE TAT.FACTORY = '" + cdvFactory.Text.ToString() + "'" + "\n");
                    strSqlString.Append("    AND TAT.FACTORY = MAT.FACTORY" + "\n");
                    strSqlString.Append("    AND TAT.FACTORY = OPR.FACTORY" + "\n");
                    strSqlString.Append("    AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
                    strSqlString.Append("    AND TAT.MAT_ID = SHIP.MAT_ID" + "\n");
                    strSqlString.Append("    AND TAT.SHIP_DATE = SHIP.WORK_DATE       " + "\n");
                    strSqlString.Append("    AND MAT.MAT_VER = 1" + "\n");
                    strSqlString.Append("    AND MAT.MAT_TYPE = 'FG' " + "\n");
                    strSqlString.Append("    AND MAT.DELETE_FLAG = ' ' " + "\n");
                    strSqlString.Append("    AND TAT.TOTAL_TIME <> 0" + "\n");
                    strSqlString.Append("    AND OPR.OPER_GRP_1 NOT IN (' ')" + "\n");
                    strSqlString.Append("    AND OPR.OPER = TAT.OPER" + "\n");

                    strSqlString.Append("    AND TAT.OPER BETWEEN '" + dtOper.Rows[0]["OPER"].ToString() + "' AND '" + dtOper.Rows[dtOper.Rows.Count - 1]["OPER"].ToString() + "'  " + "\n");

                    //if (cdvOper.FromText != "" && cdvOper.ToText != "")
                    //{
                    //    strSqlString.Append("    AND TAT.OPER BETWEEN '" + cdvOper.FromText + "' AND '" + cdvOper.ToText + "'  " + "\n");
                    //}
                    //else if (cdvOper.FromText != "" && cdvOper.ToText == "")
                    //{
                    //    strSqlString.Append("    AND TAT.OPER >= '" + cdvOper.FromText + "'" + "\n");
                    //}
                    //else if (cdvOper.FromText == "" && cdvOper.ToText != "")
                    //{
                    //    strSqlString.Append("    AND TAT.OPER <= '" + cdvOper.ToText + "'" + "\n");
                    //}

                    strSqlString.AppendFormat("    AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
                }
            }
            else
            {
                strSqlString.Append("SELECT DAT.OPER " + "\n");
                strSqlString.Append("     , DAT.TOT_TAT " + "\n");
                strSqlString.Append(" FROM( " + "\n");
                strSqlString.Append("       SELECT TAT.OPER " + "\n");

                if (ckbTime.Checked == true)
                {
                    if (ckbWIPTAT.Checked == true)
                    {
                        strSqlString.Append("            , ROUND(SUM(TAT.TOTAL_TAT_QTY)/SUM(TAT.SHIP_QTY) * 24,2) AS TOT_TAT " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("            , ROUND(SUM(TAT.TOTAL_TAT_QTY)/SUM(SHIP.SHIP_QTY)*24,2) AS TOT_TAT " + "\n");
                    }
                }
                else
                {

                    if (ckbWIPTAT.Checked == true)
                    {
                        strSqlString.Append("            , ROUND(SUM(TAT.TOTAL_TAT_QTY)/SUM(TAT.SHIP_QTY),2) AS TOT_TAT " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("            , ROUND(SUM(TAT.TOTAL_TAT_QTY)/SUM(SHIP.SHIP_QTY),2) AS TOT_TAT " + "\n");
                    }
                }
                strSqlString.Append("        FROM( " + "\n");
                strSqlString.Append("              SELECT TAT.MAT_ID " + "\n");
                if (ckbWIPTAT.Checked == true)
                {
                    strSqlString.Append("                   , TAT.WORK_DATE " + "\n");
                }
                else
                {
                    strSqlString.Append("                   , TAT.SHIP_DATE " + "\n");
                }
                strSqlString.Append("                   , OPR.OPER_GRP_1 AS OPER " + "\n");
                if (ckbWIPTAT.Checked == true)
                {
                    strSqlString.Append("                   , SUM((S1_QUEUE_TIME+S2_QUEUE_TIME+S3_QUEUE_TIME+S4_QUEUE_TIME) /60/60/24) AS TOTAL_TAT_QTY " + "\n");
                    // 2011-09-16-김민우 : AZ010 공정 LOT 수량은 END가 아닌 MOVE

                    if (cdvFactory.Text.Trim() == "HMKB1")
                        strSqlString.Append("                   , DECODE(OPER_GRP_1,'HMK3B', SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT) " + "\n");
                    else
                        strSqlString.Append("                   , DECODE(OPER_GRP_1,'HMK3A', SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT) " + "\n");

                    strSqlString.Append("                                              , SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT)) AS SHIP_QTY " + "\n");
                    //strSqlString.Append("                   , SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT) AS SHIP_QTY " + "\n");
                    strSqlString.Append("                FROM RSUMWIPMOV TAT " + "\n");
                    strSqlString.Append("                   , MWIPOPRDEF OPR " + "\n");
                    strSqlString.Append("                   , MWIPMATDEF MAT " + "\n");
                    strSqlString.Append("               WHERE TAT.FACTORY = '" + cdvFactory.Text.ToString() + "'" + "\n");
                    strSqlString.Append("                 AND TAT.FACTORY = MAT.FACTORY" + "\n");
                    strSqlString.Append("                 AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
                    strSqlString.Append("                 AND MAT.MAT_VER = 1" + "\n");
                    strSqlString.Append("                 AND OPR.OPER_GRP_1 NOT IN (' ')" + "\n");
                    strSqlString.Append("                 AND TAT.FACTORY = OPR.FACTORY" + "\n");
                    strSqlString.Append("                 AND OPR.OPER = TAT.OPER" + "\n");
                    if (cdvOper.FromText != "" && cdvOper.ToText != "")
                    {
                        strSqlString.Append("                 AND TAT.OPER BETWEEN '" + cdvOper.FromText + "' AND '" + cdvOper.ToText + "'  " + "\n");
                    }
                    else if (cdvOper.FromText != "" && cdvOper.ToText == "")
                    {
                        strSqlString.Append("                 AND TAT.OPER >= '" + cdvOper.FromText + "'" + "\n");
                    }
                    else if (cdvOper.FromText == "" && cdvOper.ToText != "")
                    {
                        strSqlString.Append("                 AND TAT.OPER <= '" + cdvOper.ToText + "'" + "\n");
                    }

                    strSqlString.AppendFormat("                 AND TAT.WORK_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
                }
                else
                {
                    //2010-01-27-임종우 Lot_Type별 검색 추가
                    if (cbLotType.Text.Equals("P%"))
                    {
                        strSqlString.Append("                   , SUM(TOTAL_TAT_QTY_P) AS TOTAL_TAT_QTY " + "\n");
                    }
                    else if (cbLotType.Text.Equals("E%"))
                    {
                        strSqlString.Append("                   , SUM(TOTAL_TAT_QTY_E) AS TOTAL_TAT_QTY " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                   , SUM(TOTAL_TAT_QTY) AS TOTAL_TAT_QTY " + "\n");
                    }

                    strSqlString.Append("                FROM CSUMTATMAT@RPTTOMES TAT " + "\n");
                    strSqlString.Append("                   , MWIPOPRDEF OPR " + "\n");
                    strSqlString.Append("                   , MWIPMATDEF MAT " + "\n");
                    strSqlString.Append("               WHERE TAT.FACTORY = '" + cdvFactory.Text.ToString() + "'" + "\n");
                    strSqlString.Append("                 AND TAT.FACTORY = MAT.FACTORY" + "\n");
                    strSqlString.Append("                 AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
                    strSqlString.Append("                 AND MAT.MAT_VER = 1" + "\n");
                    strSqlString.Append("                 AND TAT.TOTAL_TIME <> 0" + "\n");
                    strSqlString.Append("                 AND OPR.OPER_GRP_1 NOT IN (' ')" + "\n");
                    strSqlString.Append("                 AND TAT.FACTORY = OPR.FACTORY" + "\n");
                    strSqlString.Append("                 AND OPR.OPER = TAT.OPER" + "\n");

                    if (cdvOper.FromText != "" && cdvOper.ToText != "")
                    {
                        strSqlString.Append("                 AND TAT.OPER BETWEEN '" + cdvOper.FromText + "' AND '" + cdvOper.ToText + "'  " + "\n");
                    }
                    else if (cdvOper.FromText != "" && cdvOper.ToText == "")
                    {
                        strSqlString.Append("                 AND TAT.OPER >= '" + cdvOper.FromText + "'" + "\n");
                    }
                    else if (cdvOper.FromText == "" && cdvOper.ToText != "")
                    {
                        strSqlString.Append("                 AND TAT.OPER <= '" + cdvOper.ToText + "'" + "\n");
                    }

                    strSqlString.AppendFormat("                 AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
                }
            }

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("                 AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }

            #region 상세 조회에 따른 SQL문 생성

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
            }
            else
            {
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                 AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            }
            #endregion

            if (rdoDetail.Checked == true)
            {
                strSqlString.Append("  GROUP BY OPR.OPER" + "\n");
                strSqlString.Append("  ORDER BY OPR.OPER" + "\n");
            }
            else
            {
                if (ckbWIPTAT.Checked == true)
                {
                    strSqlString.Append("               GROUP BY TAT.MAT_ID, TAT.WORK_DATE, OPR.OPER_GRP_1 " + "\n");
                    strSqlString.Append("            ) TAT" + "\n");
                    strSqlString.Append("        GROUP BY TAT.OPER " + "\n");
                }
                else
                {
                    strSqlString.Append("               GROUP BY TAT.MAT_ID, TAT.SHIP_DATE, OPR.OPER_GRP_1 " + "\n");
                    strSqlString.Append("            ) TAT" + "\n");
                    strSqlString.Append("          , (" + "\n");
                    strSqlString.Append("              SELECT TAT.FACTORY " + "\n");
                    strSqlString.Append("                   , TAT.MAT_ID " + "\n");
                    strSqlString.Append("                   , TAT.SHIP_DATE AS WORK_DATE " + "\n");

                    //2010-01-27-임종우 Lot_Type별 검색 추가
                    if (cbLotType.Text.Equals("P%"))
                    {
                        strSqlString.Append("                   , SHIP_QTY_P AS SHIP_QTY " + "\n");
                    }
                    else if (cbLotType.Text.Equals("E%"))
                    {
                        strSqlString.Append("                   , SHIP_QTY_E AS SHIP_QTY" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                   , SHIP_QTY " + "\n");
                    }

                    strSqlString.Append("                FROM CSUMTATMAT@RPTTOMES TAT " + "\n");
                    strSqlString.Append("                   , MWIPMATDEF MAT " + "\n");
                    strSqlString.Append("               WHERE 1=1      " + "\n");
                    strSqlString.Append("                 AND TAT.FACTORY='" + cdvFactory.Text.ToString() + "'      " + "\n");
                    strSqlString.Append("                 AND TAT.FACTORY = MAT.FACTORY      " + "\n");
                    strSqlString.Append("                 AND TAT.MAT_ID = MAT.MAT_ID      " + "\n");
                    strSqlString.Append("                 AND MAT.DELETE_FLAG = ' '      " + "\n");

                    if (cdvFactory.Text.ToString().Equals(GlobalVariable.gsAssyDefaultFactory))
                    {
                        strSqlString.AppendFormat("                 AND OPER ='AZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("HMKB1"))
                    {
                        strSqlString.AppendFormat("                 AND OPER ='BZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("HMKE1"))
                    {
                        strSqlString.AppendFormat("                 AND OPER ='EZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals(GlobalVariable.gsTestDefaultFactory))
                    {
                        strSqlString.AppendFormat("                 AND OPER ='TZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("FGS"))
                    {
                        strSqlString.AppendFormat("                 AND OPER ='F000N'      " + "\n");
                    }
                    strSqlString.AppendFormat("                 AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
                    strSqlString.Append("            ) SHIP     " + "\n");
                    strSqlString.Append("        WHERE 1=1 " + "\n");
                    strSqlString.Append("          AND TAT.MAT_ID = SHIP.MAT_ID " + "\n");
                    strSqlString.Append("          AND TAT.SHIP_DATE = SHIP.WORK_DATE " + "\n");
                    strSqlString.Append("        GROUP BY TAT.OPER " + "\n");
                }
                strSqlString.Append("     ) DAT" + "\n");
                strSqlString.Append("   , (" + "\n");
                strSqlString.Append("       SELECT * " + "\n");
                strSqlString.Append("        FROM(  " + "\n");
                strSqlString.Append("              SELECT OPER_GRP_1 " + "\n");
                strSqlString.Append("                   , TO_NUMBER(OPER_CMF_4) " + "\n");
                strSqlString.Append("                   , ROW_NUMBER() OVER(PARTITION BY TO_NUMBER(OPER_CMF_4) ORDER BY TO_NUMBER(OPER_CMF_4)) AS RN " + "\n");
                strSqlString.Append("                FROM MWIPOPRDEF " + "\n");
                strSqlString.Append("               WHERE OPER_CMF_4 != ' ' " + "\n");
                strSqlString.Append("                 AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("            )  " + "\n");
                strSqlString.Append("        WHERE RN=1  " + "\n");
                strSqlString.Append("     ) SORT" + "\n");
                strSqlString.Append(" WHERE DAT.OPER = SORT.OPER_GRP_1 " + "\n");
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion MakeSqlChart



        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;

            string sFrom = null;
            string sTo = null;
            //string sGroupBy = null;
            //string sHeader = null;
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            sFrom = udcDurationDate1.HmFromDay;
            sTo = udcDurationDate1.HmToDay;


            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            if (ckbWIPTAT.Checked == true)
            {
                QueryCond1 = QueryCond1.Replace("SHIP_DATE", "WORK_DATE");
                QueryCond2 = QueryCond2.Replace("SHIP_DATE", "WORK_DATE");
                QueryCond3 = QueryCond3.Replace("SHIP_DATE", "WORK_DATE");
                QueryCond4 = QueryCond4.Replace("SHIP_DATE", "WORK_DATE");
            }

            #region Detail
            if (rdoDetail.Checked == true)
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
                
                if (cboType.Text.ToString().CompareTo("WAIT&RUN") == 0)
                {
                    if (ckbTime.Checked == true)
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(CTOT) * 24,2) AS  CTOT   " + "\n");
                        strSqlString.AppendFormat("     , 0 AS  Wait   " + "\n");
                        strSqlString.AppendFormat("     , 0 AS  Run   " + "\n");

                        for (int i = 0; i < dtOper.Rows.Count; i++)
                        {
                            strSqlString.AppendFormat("     , ROUND(SUM(W" + i + ") * 24,2) AS W" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WSUM" + i + "),2) AS WSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WCNT" + i + "),2) AS WCNT" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(R" + i + ") * 24,2) AS R" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RSUM" + i + "),2) AS RSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RCNT" + i + "),2) AS RCNT" + i + "\n");
                        }
                    }
                    else
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(CTOT),2) AS  CTOT   " + "\n");
                        strSqlString.AppendFormat("     , 0 AS  Wait   " + "\n");
                        strSqlString.AppendFormat("     , 0 AS  Run   " + "\n");

                        for (int i = 0; i < dtOper.Rows.Count; i++)
                        {
                            strSqlString.AppendFormat("     , ROUND(SUM(W" + i + "),2) AS W" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WSUM" + i + "),2) AS WSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WCNT" + i + "),2) AS WCNT" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(R" + i + "),2) AS R" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RSUM" + i + "),2) AS RSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RCNT" + i + "),2) AS RCNT" + i + "\n");
                        }
                    }
                }
                else if (cboType.Text.ToString().CompareTo("WAIT") == 0)
                {
                    if (ckbTime.Checked == true)
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(WTOT) * 24,2) AS  WTOT   " + "\n");
                        for (int i = 0; i < dtOper.Rows.Count; i++)
                        {
                            strSqlString.AppendFormat("     , ROUND(SUM(W" + i + ") * 24,2) AS W" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WSUM" + i + "),2) AS WSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WCNT" + i + "),2) AS WCNT" + i + "\n");
                        }
                    }
                    else
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(WTOT),2) AS  WTOT   " + "\n");
                        for (int i = 0; i < dtOper.Rows.Count; i++)
                        {
                            strSqlString.AppendFormat("     , ROUND(SUM(W" + i + "),2) AS W" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WSUM" + i + "),2) AS WSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WCNT" + i + "),2) AS WCNT" + i + "\n");
                        }
                    }
                }
                else if (cboType.Text.ToString().CompareTo("RUN") == 0)
                {
                    if (ckbTime.Checked == true)
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(RTOT) * 24,2) AS  RTOT   " + "\n");
                        for (int i = 0; i < dtOper.Rows.Count; i++)
                        {
                            strSqlString.AppendFormat("     , ROUND(SUM(R" + i + ") * 24,2) AS R" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RSUM" + i + "),2) AS RSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RCNT" + i + "),2) AS RCNT" + i + "\n");
                        }
                    }
                    else
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(RTOT),2) AS  RTOT   " + "\n");
                        for (int i = 0; i < dtOper.Rows.Count; i++)
                        {
                            strSqlString.AppendFormat("     , ROUND(SUM(R" + i + "),2) AS R" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RSUM" + i + "),2) AS RSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RCNT" + i + "),2) AS RCNT" + i + "\n");
                        }
                    }
                }

                strSqlString.AppendFormat("  FROM ( " + "\n");
                strSqlString.AppendFormat("        SELECT {0} " + "\n", QueryCond1);

                if (cboType.Text.ToString().CompareTo("WAIT&RUN") == 0)
                {
                    strSqlString.AppendFormat("              , DECODE(SUM(SHIP_QTY),0,0,SUM(TOTAL_TAT_QTY)/SUM(SHIP_QTY)) AS CTOT   " + "\n");

                    for (int i = 0; i < dtOper.Rows.Count; i++)
                    {                      
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', DECODE(SUM(SHIP_QTY),0,0,SUM(WAIT_TAT_QTY)/SUM(SHIP_QTY)), 0) AS W" + i + "\n");
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', SUM(WAIT_TAT_QTY), 0) AS WSUM" + i + "\n");
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', SUM(SHIP_QTY), 0) AS WCNT" + i + "\n");
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', DECODE(SUM(SHIP_QTY),0,0,SUM(PROC_TAT_QTY)/SUM(SHIP_QTY)), 0) AS R" + i + "\n");
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', SUM(PROC_TAT_QTY), 0) AS RSUM" + i + "\n");
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', SUM(SHIP_QTY), 0) AS RCNT" + i + "\n");
                    }
                }
                else if (cboType.Text.ToString().CompareTo("WAIT") == 0)
                {
                    strSqlString.AppendFormat("              , DECODE(SUM(SHIP_QTY),0,0,SUM(WAIT_TAT_QTY)/SUM(SHIP_QTY)) AS WTOT   " + "\n");
                    for (int i = 0; i < dtOper.Rows.Count; i++)
                    {
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', DECODE(SUM(SHIP_QTY),0,0,SUM(WAIT_TAT_QTY)/SUM(SHIP_QTY)), 0) AS W" + i + "\n");
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', SUM(WAIT_TAT_QTY), 0) AS WSUM" + i + "\n");
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', SUM(SHIP_QTY), 0) AS WCNT" + i + "\n");
                    }
                }
                else if (cboType.Text.ToString().CompareTo("RUN") == 0)
                {
                    strSqlString.AppendFormat("              , DECODE(SUM(SHIP_QTY),0,0,SUM(WAIT_TAT_QTY)/SUM(SHIP_QTY)) AS RTOT   " + "\n");
                    for (int i = 0; i < dtOper.Rows.Count; i++)
                    {
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', DECODE(SUM(SHIP_QTY),0,0,SUM(PROC_TAT_QTY)/SUM(SHIP_QTY)), 0) AS R" + i + "\n");
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', SUM(PROC_TAT_QTY), 0) AS RSUM" + i + "\n");
                        strSqlString.AppendFormat("              , DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', SUM(SHIP_QTY), 0) AS RCNT" + i + "\n");
                    }
                }
                strSqlString.AppendFormat("           FROM ( " + "\n");

                //2010-02-10-임종우 : 일,주,월 선택에 의해 쿼리 수정
                if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "DAY")
                {
                    strSqlString.AppendFormat("                  SELECT {0}, OPER " + "\n", QueryCond2);
                }
                else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.AppendFormat("                  SELECT {0}, OPER " + "\n", QueryCond3);
                }
                else
                {
                    strSqlString.AppendFormat("                  SELECT {0}, OPER " + "\n", QueryCond4);
                }
                // 2011-09-15-김민우 : WIP_TAT(재공 TAT) 체크 시
                if (ckbWIPTAT.Checked == true)
                {
                    // 2011-09-16-김민우 : AZ010 공정 LOT 수량은 END가 아닌 MOVE
                    
                    if (cdvFactory.Text.Trim() == "HMKB1")
                        strSqlString.AppendFormat("                       , DECODE(TAT.OPER,'BZ010', (S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT)" + "\n"); // END QTY이지만 고쳐야 할게 많아 SHIP_QTY로   
                    else
                        strSqlString.AppendFormat("                       , DECODE(TAT.OPER,'AZ010', (S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT)" + "\n"); // END QTY이지만 고쳐야 할게 많아 SHIP_QTY로   

                    strSqlString.AppendFormat("                                                , (S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT)) AS SHIP_QTY" + "\n"); // END QTY이지만 고쳐야 할게 많아 SHIP_QTY로   
                    //strSqlString.AppendFormat("                       , (S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT) AS SHIP_QTY" + "\n"); // END QTY이지만 고쳐야 할게 많아 SHIP_QTY로   

                    strSqlString.AppendFormat("                       , ((S1_QUEUE_TIME+S2_QUEUE_TIME+S3_QUEUE_TIME+S4_QUEUE_TIME) - (S1_PROC_TIME+S2_PROC_TIME+S3_PROC_TIME+S4_PROC_TIME)) /60/60/24 AS WAIT_TAT_QTY" + "\n");
                    strSqlString.AppendFormat("                       , (S1_PROC_TIME+S2_PROC_TIME+S3_PROC_TIME+S4_PROC_TIME) /60/60/24 AS PROC_TAT_QTY" + "\n");
                    strSqlString.AppendFormat("                       , (S1_QUEUE_TIME+S2_QUEUE_TIME+S3_QUEUE_TIME+S4_QUEUE_TIME) /60/60/24 AS TOTAL_TAT_QTY" + "\n");
                    strSqlString.AppendFormat("                    FROM RSUMWIPMOV TAT" + "\n");
                    strSqlString.AppendFormat("                       , MWIPMATDEF MAT" + "\n");
                    strSqlString.AppendFormat("                   WHERE TAT.FACTORY = '" + cdvFactory.Text.ToString() + "'" + "\n");
                    strSqlString.AppendFormat("                     AND TAT.FACTORY = MAT.FACTORY" + "\n");
                    strSqlString.AppendFormat("                     AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
                    strSqlString.AppendFormat("                     AND MAT.MAT_VER = 1 " + "\n");
                    strSqlString.AppendFormat("                     AND MAT.MAT_TYPE = 'FG'" + "\n");
                    strSqlString.AppendFormat("                     AND MAT.DELETE_FLAG <> 'Y' " + "\n");
                    strSqlString.AppendFormat("                     AND WORK_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

                    if (cdvFlow.Text.Trim() != "")
                    {
                        strSqlString.AppendFormat("                 AND MAT.FIRST_FLOW " + cdvFlow.SelectedValueToQueryString + "\n");
                    }

                    if (cbLotType.Text.Equals("P%"))
                    {
                        strSqlString.AppendFormat("                     AND TAT.CM_KEY_3 LIKE 'P%' " + "\n");
                    }
                    else if (cbLotType.Text.Equals("E%"))
                    {
                        strSqlString.AppendFormat("                     AND TAT.CM_KEY_3 LIKE 'E%' " + "\n");
                    }
                }
                else
                {
                    //2010-01-28-임종우 Lot_Type별 검색 추가
                    if (cbLotType.Text.Equals("P%"))
                    {
                        if (ckbHold.Checked == true)
                        {
                            strSqlString.AppendFormat("                       , TAT.TOTAL_TAT_QTY_P - TAT.HOLD_TAT_QTY AS TOTAL_TAT_QTY" + "\n");
                            strSqlString.AppendFormat("                       , TAT.WAIT_TAT_QTY_P - TAT.HOLD_TAT_QTY AS WAIT_TAT_QTY" + "\n");
                        }
                        else
                        {
                            strSqlString.AppendFormat("                       , TAT.TOTAL_TAT_QTY_P AS TOTAL_TAT_QTY" + "\n");
                            strSqlString.AppendFormat("                       , TAT.WAIT_TAT_QTY_P AS WAIT_TAT_QTY" + "\n");
                        }

                        strSqlString.AppendFormat("                       , TAT.PROC_TAT_QTY_P AS PROC_TAT_QTY" + "\n");
                    }
                    else if (cbLotType.Text.Equals("E%"))
                    {
                        if (ckbHold.Checked == true)
                        {
                            strSqlString.AppendFormat("                       , TAT.TOTAL_TAT_QTY_E - TAT.HOLD_TAT_QTY AS TOTAL_TAT_QTY" + "\n");
                            strSqlString.AppendFormat("                       , TAT.WAIT_TAT_QTY_E - TAT.HOLD_TAT_QTY AS WAIT_TAT_QTY" + "\n");                            
                        }
                        else
                        {
                            strSqlString.AppendFormat("                       , TAT.TOTAL_TAT_QTY_E AS TOTAL_TAT_QTY" + "\n");
                            strSqlString.AppendFormat("                       , TAT.WAIT_TAT_QTY_E AS WAIT_TAT_QTY" + "\n");                            
                        }

                        strSqlString.AppendFormat("                       , TAT.PROC_TAT_QTY_E AS PROC_TAT_QTY" + "\n");
                    }
                    else
                    {
                        if (ckbHold.Checked == true)
                        {
                            strSqlString.AppendFormat("                       , TAT.TOTAL_TAT_QTY - TAT.HOLD_TAT_QTY AS TOTAL_TAT_QTY" + "\n");
                            strSqlString.AppendFormat("                       , TAT.WAIT_TAT_QTY - TAT.HOLD_TAT_QTY AS WAIT_TAT_QTY" + "\n");
                        }
                        else
                        {
                            strSqlString.AppendFormat("                       , TAT.TOTAL_TAT_QTY AS TOTAL_TAT_QTY" + "\n");
                            strSqlString.AppendFormat("                       , TAT.WAIT_TAT_QTY AS WAIT_TAT_QTY" + "\n");
                        }

                        strSqlString.AppendFormat("                       , TAT.PROC_TAT_QTY AS PROC_TAT_QTY" + "\n");
                    }

                    strSqlString.AppendFormat("                       , SHIP.SHIP_QTY AS SHIP_QTY" + "\n");
                    strSqlString.AppendFormat("                    FROM CSUMTATMAT@RPTTOMES TAT, MWIPMATDEF MAT       " + "\n");
                    strSqlString.AppendFormat("                       , (      " + "\n");
                    strSqlString.AppendFormat("                           SELECT TAT.FACTORY, TAT.MAT_ID, TAT.SHIP_DATE AS WORK_DATE " + "\n");

                    //2010-01-27-임종우 Lot_Type별 검색 추가
                    if (cbLotType.Text.Equals("P%"))
                    {
                        strSqlString.AppendFormat("                                , SHIP_QTY_P AS SHIP_QTY      " + "\n");
                    }
                    else if (cbLotType.Text.Equals("E%"))
                    {
                        strSqlString.AppendFormat("                                , SHIP_QTY_E AS SHIP_QTY      " + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("                                , SHIP_QTY      " + "\n");
                    }

                    strSqlString.AppendFormat("                             FROM CSUMTATMAT@RPTTOMES TAT      " + "\n");
                    strSqlString.AppendFormat("                            WHERE 1=1      " + "\n");
                    strSqlString.AppendFormat("                              AND TAT.FACTORY='" + cdvFactory.Text.ToString() + "'      " + "\n");

                    if (cdvFactory.Text.ToString().Equals(GlobalVariable.gsAssyDefaultFactory))
                    {
                        strSqlString.AppendFormat("                              AND OPER ='AZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("HMKB1"))
                    {
                        strSqlString.AppendFormat("                              AND OPER ='BZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("HMKE1"))
                    {
                        strSqlString.AppendFormat("                              AND OPER ='EZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals(GlobalVariable.gsTestDefaultFactory))
                    {
                        strSqlString.AppendFormat("                              AND OPER ='TZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("FGS"))
                    {
                        strSqlString.AppendFormat("                              AND OPER ='F000N'      " + "\n");
                    }

                    strSqlString.AppendFormat("                              AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
                    strSqlString.AppendFormat("                         ) SHIP      " + "\n");
                    strSqlString.AppendFormat("                   WHERE 1=1      " + "\n");
                    strSqlString.AppendFormat("                     AND TAT.FACTORY = MAT.FACTORY    " + "\n");
                    strSqlString.AppendFormat("                     AND TAT.MAT_ID = MAT.MAT_ID      " + "\n");
                    strSqlString.AppendFormat("                     AND TAT.MAT_ID = SHIP.MAT_ID       " + "\n");
                    strSqlString.AppendFormat("                     AND TAT.SHIP_DATE = SHIP.WORK_DATE       " + "\n");
                    strSqlString.AppendFormat("                     AND MAT.MAT_VER = 1 " + "\n");
                    strSqlString.AppendFormat("                     AND MAT.MAT_TYPE = 'FG' " + "\n");
                    strSqlString.AppendFormat("                     AND MAT.DELETE_FLAG <> 'Y' " + "\n");
                    strSqlString.AppendFormat("                     AND TAT.FACTORY = '" + cdvFactory.Text.ToString() + "'        " + "\n");
                    strSqlString.AppendFormat("                     AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

                    if (cdvFlow.Text.Trim() != "")
                    {
                        strSqlString.AppendFormat("                 AND MAT.FIRST_FLOW " + cdvFlow.SelectedValueToQueryString + "\n");
                    }
                } 
                if (cdvOper.FromText != "" && cdvOper.ToText != "")
                {
                    strSqlString.AppendFormat("                     AND TAT.OPER BETWEEN '" + cdvOper.FromText + "' AND '" + cdvOper.ToText + "' " + "\n");
                }
                else if (cdvOper.FromText != "" && cdvOper.ToText == "")
                {
                    strSqlString.AppendFormat("                     AND TAT.OPER >= '" + cdvOper.FromText + "'" + "\n");
                }
                else if (cdvOper.FromText == "" && cdvOper.ToText != "")
                {
                    strSqlString.AppendFormat("                     AND TAT.OPER <= '" + cdvOper.ToText + "'" + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("                     AND TAT.OPER BETWEEN '" + dtOper.Rows[0]["OPER"].ToString() + "' AND '" + dtOper.Rows[dtOper.Rows.Count - 1]["OPER"].ToString() + "'  " + "\n");                    
                }

                if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("                     AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
                }

                #region 상세 조회에 따른 SQL문 생성
                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                    if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                    if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                    if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                    if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                    if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                    if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                    if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                    if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                    if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                    if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                    if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                }
                else
                {
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                        strSqlString.AppendFormat("                     AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                }
                #endregion

                strSqlString.AppendFormat("                ) A " + "\n");
                
                //1212

                strSqlString.AppendFormat("          GROUP BY {0}, OPER  " + "\n", QueryCond1);
                strSqlString.AppendFormat("       )         " + "\n");
                strSqlString.AppendFormat("  GROUP BY {0}" + "\n", QueryCond1);
                strSqlString.AppendFormat("  ORDER BY {0}" + "\n", QueryCond1);
            }
            #endregion

            #region 요약
            else
            {
                //Option 요약
                strSqlString.AppendFormat("SELECT {0}  " + "\n", QueryCond1);

                if (ckbWR.Checked == true)
                {
                    if (ckbTime.Checked == true)
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(CTOT) * 24,2) AS  CTOT   " + "\n");
                        strSqlString.AppendFormat("     , 0 AS  Wait   " + "\n");
                        strSqlString.AppendFormat("     , 0 AS  Run   " + "\n");

                        for (int i = 0; i < dtOper.Rows.Count; i++)
                        {
                            strSqlString.AppendFormat("     , ROUND(SUM(W" + i + ") * 24,2) AS W" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WSUM" + i + "),2) AS WSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WCNT" + i + "),2) AS WCNT" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(R" + i + ") * 24,2) AS R" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RSUM" + i + "),2) AS RSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RCNT" + i + "),2) AS RCNT" + i + "\n");
                        }
                    }
                    else
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(CTOT),2) AS  CTOT   " + "\n");
                        strSqlString.AppendFormat("     , 0 AS  Wait   " + "\n");
                        strSqlString.AppendFormat("     , 0 AS  Run   " + "\n");

                        for (int i = 0; i < dtOper.Rows.Count; i++)
                        {
                            strSqlString.AppendFormat("     , ROUND(SUM(W" + i + "),2) AS W" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WSUM" + i + "),2) AS WSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(WCNT" + i + "),2) AS WCNT" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(R" + i + "),2) AS R" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RSUM" + i + "),2) AS RSUM" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(RCNT" + i + "),2) AS RCNT" + i + "\n");
                        }
                    }
                }
                else
                {

                    if (ckbTime.Checked == true)
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(CTOT) * 24,2) AS  CTOT " + "\n");

                        for (int i = 0; i < dtOper.Rows.Count; i++)
                        {
                            strSqlString.AppendFormat("     , ROUND(SUM(C" + i + ") * 24,2) AS C" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(CTAT_QTY" + i + "),2) AS CTAT_QTY" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(CSHIP_QTY" + i + "),2) AS CSHIP_QTY" + i + "\n");
                        }
                    }
                    else
                    {
                        strSqlString.AppendFormat("     , ROUND(SUM(CTOT),2) AS  CTOT " + "\n");

                        for (int i = 0; i < dtOper.Rows.Count; i++)
                        {
                            strSqlString.AppendFormat("     , ROUND(SUM(C" + i + "),2) AS C" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(CTAT_QTY" + i + "),2) AS CTAT_QTY" + i + "\n");
                            strSqlString.AppendFormat("     , ROUND(SUM(CSHIP_QTY" + i + "),2) AS CSHIP_QTY" + i + "\n");
                        }
                    }
                }

                strSqlString.AppendFormat("  FROM ( " + "\n");
                strSqlString.AppendFormat("        SELECT {0} " + "\n", QueryCond1);
                strSqlString.AppendFormat("             , ROUND(SUM(DECODE(SHIP_QTY, 0, 0, TAT_QTY/SHIP_QTY)),2) AS  CTOT   " + "\n");

                if (ckbWR.Checked == true)
                {
                    for (int i = 0; i < dtOper.Rows.Count; i++)
                    {
                        strSqlString.AppendFormat("             , ROUND(SUM(DECODE(OPER_GRP_1, '" + dtOper.Rows[i]["OPER_GRP_1"].ToString() + "', DECODE(SHIP_QTY, 0, 0, WAIT_TAT_QTY/SHIP_QTY), 0)), 2) AS W" + i + "\n");
                        strSqlString.AppendFormat("             , ROUND(SUM(DECODE(OPER_GRP_1, '" + dtOper.Rows[i]["OPER_GRP_1"].ToString() + "', WAIT_TAT_QTY, 0)), 2) AS WSUM" + i + "\n");
                        strSqlString.AppendFormat("             , ROUND(SUM(DECODE(OPER_GRP_1, '" + dtOper.Rows[i]["OPER_GRP_1"].ToString() + "', SHIP_QTY, 0)), 2) AS WCNT" + i + "\n");
                        strSqlString.AppendFormat("             , ROUND(SUM(DECODE(OPER_GRP_1, '" + dtOper.Rows[i]["OPER_GRP_1"].ToString() + "', DECODE(SHIP_QTY, 0, 0, PROC_TAT_QTY/SHIP_QTY), 0)), 2) AS R" + i + "\n");
                        strSqlString.AppendFormat("             , ROUND(SUM(DECODE(OPER_GRP_1, '" + dtOper.Rows[i]["OPER_GRP_1"].ToString() + "', PROC_TAT_QTY, 0)), 2) AS RSUM" + i + "\n");
                        strSqlString.AppendFormat("             , ROUND(SUM(DECODE(OPER_GRP_1, '" + dtOper.Rows[i]["OPER_GRP_1"].ToString() + "', SHIP_QTY, 0)), 2) AS RCNT" + i + "\n");
                    }
                }
                else
                {
                    for (int i = 0; i < dtOper.Rows.Count; i++)
                    {
                        strSqlString.AppendFormat("             , ROUND(SUM(DECODE(OPER_GRP_1, '" + dtOper.Rows[i]["OPER_GRP_1"].ToString() + "', DECODE(SHIP_QTY, 0, 0, TAT_QTY/SHIP_QTY), 0)), 2) AS C" + i + "\n");
                        strSqlString.AppendFormat("             , ROUND(SUM(DECODE(OPER_GRP_1, '" + dtOper.Rows[i]["OPER_GRP_1"].ToString() + "', TAT_QTY, 0)), 2) AS CTAT_QTY" + i + "\n");
                        strSqlString.AppendFormat("             , ROUND(SUM(DECODE(OPER_GRP_1, '" + dtOper.Rows[i]["OPER_GRP_1"].ToString() + "', SHIP_QTY, 0)), 2) AS CSHIP_QTY" + i + "\n");
                    }
                }

                strSqlString.AppendFormat("          FROM (" + "\n");
                strSqlString.AppendFormat("                SELECT {0} " + "\n", QueryCond1);
                strSqlString.AppendFormat("                     , OPER_GRP_1 " + "\n");
                strSqlString.AppendFormat("                     , SUM(TAT_QTY) AS TAT_QTY " + "\n");
                strSqlString.AppendFormat("                     , SUM(WAIT_TAT_QTY) AS WAIT_TAT_QTY " + "\n");
                strSqlString.AppendFormat("                     , SUM(PROC_TAT_QTY) AS PROC_TAT_QTY " + "\n");
                strSqlString.AppendFormat("                     , SUM(SHIP_QTY) AS SHIP_QTY " + "\n");
                strSqlString.AppendFormat("                  FROM ( " + "\n");

                //2010-01-25-임종우 일,주,월 데이터 그룹핑 추가.
                if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.AppendFormat("                        SELECT {0}, OPER_GRP_1, TAT_QTY, WAIT_TAT_QTY, PROC_TAT_QTY, SHIP_QTY " + "\n", QueryCond3);
                }
                else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.AppendFormat("                        SELECT {0}, OPER_GRP_1, TAT_QTY, WAIT_TAT_QTY, PROC_TAT_QTY, SHIP_QTY " + "\n", QueryCond4);
                }
                else
                {
                    strSqlString.AppendFormat("                        SELECT {0}, OPER_GRP_1, TAT_QTY, WAIT_TAT_QTY, PROC_TAT_QTY, SHIP_QTY " + "\n", QueryCond2);
                }
                
                strSqlString.AppendFormat("                          FROM ( " + "\n");
                strSqlString.AppendFormat("                                SELECT {0}" + "\n", QueryCond1);
                strSqlString.AppendFormat("                                     , MAT.MAT_ID" + "\n");
                strSqlString.AppendFormat("                                     , OPR.OPER_GRP_1" + "\n");

                // 2011-09-15-김민우 : WIP_TAT(재공 TAT) 체크 시
                if (ckbWIPTAT.Checked == true)
                {
                    strSqlString.AppendFormat("                                     , SUM((S1_QUEUE_TIME+S2_QUEUE_TIME+S3_QUEUE_TIME+S4_QUEUE_TIME) /60/60/24) AS TAT_QTY" + "\n");
                    // 2011-09-16-김민우 : AZ010 공정 LOT 수량은 END가 아닌 MOVE

                    if (cdvFactory.Text.Trim() == "HMKB1")
                        strSqlString.AppendFormat("                                     , DECODE(OPER_GRP_1,'HMK3B', SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT) " + "\n");
                    else
                        strSqlString.AppendFormat("                                     , DECODE(OPER_GRP_1,'HMK3A', SUM(S1_MOVE_LOT+S2_MOVE_LOT+S3_MOVE_LOT+S4_MOVE_LOT) " + "\n");

                    strSqlString.AppendFormat("                                     , SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT+S4_END_LOT)) AS SHIP_QTY " + "\n");                    
                    strSqlString.AppendFormat("                                  FROM RSUMWIPMOV TAT " + "\n");
                    strSqlString.AppendFormat("                                     , MWIPMATDEF MAT " + "\n");
                    strSqlString.AppendFormat("                                     , MWIPOPRDEF OPR " + "\n");
                    strSqlString.AppendFormat("                                 WHERE 1=1 " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.FACTORY = MAT.FACTORY    " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.MAT_ID = MAT.MAT_ID      " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.FACTORY = OPR.FACTORY     " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.OPER = OPR.OPER     " + "\n");
                    strSqlString.AppendFormat("                                   AND MAT.MAT_VER = 1     " + "\n");
                    strSqlString.AppendFormat("                                   AND MAT.MAT_TYPE = 'FG' " + "\n");
                    strSqlString.AppendFormat("                                   AND MAT.DELETE_FLAG <> 'Y' " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.FACTORY = '" + cdvFactory.Text.ToString() + "'        " + "\n");
                    strSqlString.AppendFormat("                                   AND OPR.OPER_GRP_1 NOT IN (' ')     " + "\n");                  

                    if (cdvOper.FromText != "" && cdvOper.ToText != "")
                    {
                        strSqlString.AppendFormat("                                   AND TAT.OPER BETWEEN '" + cdvOper.FromText + "' AND '" + cdvOper.ToText + "' " + "\n");
                    }
                    else if (cdvOper.FromText != "" && cdvOper.ToText == "")
                    {
                        strSqlString.AppendFormat("                                   AND TAT.OPER >= '" + cdvOper.FromText + "'" + "\n");
                    }
                    else if (cdvOper.FromText == "" && cdvOper.ToText != "")
                    {
                        strSqlString.AppendFormat("                                   AND TAT.OPER <= '" + cdvOper.ToText + "'" + "\n");
                    }

                    //기간 선택시 SQL 조건문 생성
                    strSqlString.AppendFormat("                                   AND TAT.WORK_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

                    if (cbLotType.Text.Equals("P%"))
                    {
                        strSqlString.AppendFormat("                                   AND TAT.CM_KEY_3 LIKE 'P%' " + "\n");
                    }
                    else if (cbLotType.Text.Equals("E%"))
                    {
                        strSqlString.AppendFormat("                                   AND TAT.CM_KEY_3 LIKE 'E%' " + "\n");
                    }

                    if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                    {
                        strSqlString.AppendFormat("                                   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
                    }

                    #region 상세 조회에 따른 SQL문 생성
                    if (cdvFactory.Text.Trim() == "HMKB1")
                    {
                        if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                        if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                        if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                        if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                        if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                        if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                        if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                        if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                        if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                        if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                        if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                        if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                    }
                    else
                    {
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                    }
                    #endregion

                    strSqlString.AppendFormat("                                 GROUP BY {0}, MAT.MAT_ID, OPER_GRP_1  " + "\n", QueryCond1);
                    strSqlString.AppendFormat("                               ) TAT    " + "\n");                    
                }
                else
                {

                    //2010-01-28-임종우 Lot_Type별 검색 추가
                    if (cbLotType.Text.Equals("P%"))
                    {
                        strSqlString.AppendFormat("                                     , SUM(TOTAL_TAT_QTY_P) AS TAT_QTY" + "\n");
                        strSqlString.AppendFormat("                                     , SUM(WAIT_TAT_QTY_P) AS WAIT_TAT_QTY" + "\n");
                        strSqlString.AppendFormat("                                     , SUM(PROC_TAT_QTY_P) AS PROC_TAT_QTY" + "\n");
                    }
                    else if (cbLotType.Text.Equals("E%"))
                    {
                        strSqlString.AppendFormat("                                     , SUM(TOTAL_TAT_QTY_E) AS TAT_QTY" + "\n");
                        strSqlString.AppendFormat("                                     , SUM(WAIT_TAT_QTY_E) AS WAIT_TAT_QTY" + "\n");
                        strSqlString.AppendFormat("                                     , SUM(PROC_TAT_QTY_E) AS PROC_TAT_QTY" + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("                                     , SUM(TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
                        strSqlString.AppendFormat("                                     , SUM(WAIT_TAT_QTY) AS WAIT_TAT_QTY" + "\n");
                        strSqlString.AppendFormat("                                     , SUM(PROC_TAT_QTY) AS PROC_TAT_QTY" + "\n");
                    }

                    strSqlString.AppendFormat("                                  FROM CSUMTATMAT@RPTTOMES TAT " + "\n");
                    strSqlString.AppendFormat("                                     , MWIPMATDEF MAT " + "\n");
                    strSqlString.AppendFormat("                                     , MWIPOPRDEF OPR " + "\n");
                    strSqlString.AppendFormat("                                 WHERE 1=1 " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.FACTORY = MAT.FACTORY    " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.MAT_ID = MAT.MAT_ID      " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.FACTORY = OPR.FACTORY     " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.OPER = OPR.OPER     " + "\n");
                    strSqlString.AppendFormat("                                   AND MAT.MAT_VER = 1     " + "\n");
                    strSqlString.AppendFormat("                                   AND MAT.MAT_TYPE = 'FG' " + "\n");
                    strSqlString.AppendFormat("                                   AND MAT.DELETE_FLAG <> 'Y' " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.TOTAL_TIME <> 0     " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.FACTORY = '" + cdvFactory.Text.ToString() + "'        " + "\n");
                    strSqlString.AppendFormat("                                   AND OPR.OPER_GRP_1 NOT IN (' ')     " + "\n");                   

                    if (cdvOper.FromText != "" && cdvOper.ToText != "")
                    {
                        strSqlString.AppendFormat("                                   AND TAT.OPER BETWEEN '" + cdvOper.FromText + "' AND '" + cdvOper.ToText + "' " + "\n");
                    }
                    else if (cdvOper.FromText != "" && cdvOper.ToText == "")
                    {
                        strSqlString.AppendFormat("                                   AND TAT.OPER >= '" + cdvOper.FromText + "'" + "\n");
                    }
                    else if (cdvOper.FromText == "" && cdvOper.ToText != "")
                    {
                        strSqlString.AppendFormat("                                   AND TAT.OPER <= '" + cdvOper.ToText + "'" + "\n");
                    }

                    //기간 선택시 SQL 조건문 생성
                    strSqlString.AppendFormat("                                   AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

                    if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                    {
                        strSqlString.AppendFormat("                                   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
                    }

                    #region 상세 조회에 따른 SQL문 생성
                    if (cdvFactory.Text.Trim() == "HMKB1")
                    {
                        if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                        if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                        if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                        if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                        if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                        if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                        if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                        if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                        if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                        if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                        if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                        if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                    }
                    else
                    {
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("                                   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                    }
                    #endregion

                    strSqlString.AppendFormat("                                 GROUP BY {0}, MAT.MAT_ID, OPER_GRP_1  " + "\n", QueryCond1);
                    strSqlString.AppendFormat("                               ) TAT    " + "\n");

                    // 2009-12-15-임종우 SHIP 수량 중복 문제로 쿼리 수정함.(추가)
                    strSqlString.AppendFormat("                             , (      " + "\n");
                    strSqlString.AppendFormat("                                SELECT TAT.FACTORY " + "\n");
                    strSqlString.AppendFormat("                                     , TAT.MAT_ID " + "\n");
                    strSqlString.AppendFormat("                                     , TAT.SHIP_DATE AS WORK_DATE " + "\n");

                    //2010-01-28-임종우 Lot_Type별 검색 추가
                    if (cbLotType.Text.Equals("P%"))
                    {
                        strSqlString.AppendFormat("                                     , SHIP_QTY_P AS SHIP_QTY " + "\n");
                    }
                    else if (cbLotType.Text.Equals("E%"))
                    {
                        strSqlString.AppendFormat("                                     , SHIP_QTY_E AS SHIP_QTY " + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("                                     , SHIP_QTY " + "\n");
                    }
                    
                    strSqlString.AppendFormat("                                  FROM CSUMTATMAT@RPTTOMES TAT     " + "\n");
                    strSqlString.AppendFormat("                                 WHERE 1=1      " + "\n");
                    strSqlString.AppendFormat("                                   AND TAT.FACTORY='" + cdvFactory.Text.ToString() + "'      " + "\n");

                    if (cdvFactory.Text.ToString().Equals(GlobalVariable.gsAssyDefaultFactory))
                    {
                        strSqlString.AppendFormat("                                   AND OPER ='AZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("HMKB1"))
                    {
                        strSqlString.AppendFormat("                                   AND OPER ='BZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("HMKE1"))
                    {
                        strSqlString.AppendFormat("                                   AND OPER ='EZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals(GlobalVariable.gsTestDefaultFactory))
                    {
                        strSqlString.AppendFormat("                                   AND OPER ='TZ010'      " + "\n");
                    }
                    else if (cdvFactory.Text.ToString().Equals("FGS"))
                    {
                        strSqlString.AppendFormat("                                   AND OPER ='F000N'      " + "\n");
                    }

                    strSqlString.AppendFormat("                                   AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
                    strSqlString.AppendFormat("                               ) B      " + "\n");                    
                    strSqlString.AppendFormat("                         WHERE 1=1     " + "\n");
                    strSqlString.AppendFormat("                           AND TAT.MAT_ID = B.MAT_ID     " + "\n");
                    strSqlString.AppendFormat("                           AND TAT.SHIP_DATE = B.WORK_DATE     " + "\n");
                    
                }
                strSqlString.AppendFormat("                       ) " + "\n");
                strSqlString.AppendFormat("                 GROUP BY {0}, OPER_GRP_1 " + "\n", QueryCond1);                
                strSqlString.AppendFormat("               ) " + "\n");
                strSqlString.AppendFormat("         GROUP BY {0}" + "\n", QueryCond1);
                strSqlString.AppendFormat("       ) " + "\n");
                strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond1);
                strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond1);
            }
            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
                        
            return strSqlString.ToString();
        }

        #endregion

        private void InitChart()
        {
            //Series series1 = null;
            //chart1.Series.Clear();
            
            //series1 = new Series();
            //chart1.Series.Add(series1);

            //chart1.Series["Series1"].IsValueShownAsLabel = true;
            //chart1.Series["Series1"].Color = Color.Black;
            //chart1.Series["Series1"].MarkerStyle = MarkerStyle.Circle;
            //chart1.Series["Series1"].MarkerSize = 10;
            //chart1.Series["Series1"].MarkerColor = Color.Black;
            //chart1.Series["Series1"].BorderWidth = 4;
            //chart1.Series["Series1"].IsVisibleInLegend = false;

            //chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            //chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            //chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            //chart1.ChartAreas[0].AxisX.IsLabelAutoFit = true;
            //chart1.ChartAreas[0].AxisX.LabelAutoFitStyle =
            //        LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont | LabelAutoFitStyles.WordWrap;
            //chart1.ChartAreas[0].AxisY.IsInterlaced = true;
            //chart1.ChartAreas[0].AxisY.InterlacedColor = Color.FromArgb(80, Color.DarkGray);
        }

        #region ShowChart

        private void ShowChart(int columnCount)
        {
            // 차트 설정
            //udcChartFX1.RPT_2_ClearData();
            //udcChartFX1.RPT_3_OpenData(1, dtChart.Rows.Count);
            udcMSChart1.RPT_2_ClearData();
            udcMSChart1.RPT_3_OpenData(1, dtChart.Rows.Count);

            int[] total_rows = new Int32[dtChart.Rows.Count];
            //int[] columnsHeader = new Int32[dtChart.Rows.Count + 1];

            for (int i = 0; i < dtChart.Rows.Count; i++)
            {
                total_rows[i] = 0 + i;
            }

            //double max1 = udcChartFX1.RPT_4_AddData(dtChart, total_rows, new int[] { columnCount + 1 }, SeriseType.Rows, DataTypes.Initeger);
            double max1 = udcMSChart1.RPT_4_AddData(dtChart, total_rows, new int[] { columnCount + 1 }, SeriseType.Rows, DataTypes.Initeger);
            
            //udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            
            //udcChartFX1.Series[0].Color = System.Drawing.Color.Black;
            udcMSChart1.Series[0].Color = System.Drawing.Color.Black;
            udcMSChart1.Series[0].IsVisibleInLegend = false;
            udcMSChart1.Series[0].IsValueShownAsLabel = true;
            udcMSChart1.Series[0].MarkerStyle = MarkerStyle.Circle;
            udcMSChart1.Series[0].MarkerSize = 8;
            //
            //udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(dtChart);

            //udcChartFX1.PointLabels = true;
            //udcChartFX1.AxisY.Gridlines = false;
            //udcChartFX1.AxisY.DataFormat.Decimals = 2;
            udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;


            //MS chart
            //InitChart();

            // Set series chart type
            //chart1.Series["Series1"].ChartType = SeriesChartType.Line;
            
            //if (rdoDetail.Checked == true)
            //    chart1.Series["Series1"].Points.DataBindXY(dtChart.Rows, "OPER", dtChart.Rows, "TAT");
            //else
            //    chart1.Series["Series1"].Points.DataBindXY(dtChart.Rows, "OPER", dtChart.Rows, "TOT_TAT");
            
        }
        #endregion

        #endregion

        #region ToExcel

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
            spdData.ExportExcel();
        }

        #endregion

        #region Cell을 더블클릭 했을 경우의 ShowChart_Selected()
        /// <summary>
        /// CellDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            int i = 0;
            i = e.Row;

            ShowChart_Selected(i);              // 챠트 그리기
        }
        #endregion

        #region ShowChart_Selected
        private void ShowChart_Selected(int rowCount)
        {
            string unit = null;

            //udcChartFX1.RPT_2_ClearData();
            //udcChartFX1.RPT_3_OpenData(1, udcDurationDate1.SubtractBetweenFromToDate + 1);

            udcMSChart1.RPT_2_ClearData();
            udcMSChart1.RPT_3_OpenData(1, udcDurationDate1.SubtractBetweenFromToDate + 1);

            int[] acum_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] wsit_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] run_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] columnsHeader = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];

            for (int i = 0; i < dtOper.Rows.Count; i++)  // Viewoption에 따라 컬럼수 정의
            {
                columnsHeader[i] = 10 + i;
                run_columns[i] = 10 + i;
                //tat_columns[i] = 9 + i;
            }

            double max1 = 0;
            int[] rows = new Int32[1];
            rows[0] = rowCount;

            //max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount }, run_columns, SeriseType.Rows);
            max1 = udcMSChart1.RPT_4_AddData(spdData, new int[] { rowCount }, run_columns, SeriseType.Rows);

            //udcChartFX1.RPT_5_CloseData();
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, unit, AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            //udcChartFX1.Series[0].Color = System.Drawing.Color.Black;
            udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, unit, AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcMSChart1.Series[0].Color = System.Drawing.Color.Black;
            
            //udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            ////udcChartFX1.RPT_8_SetSeriseLegend(spdData, rows, 9-1, SoftwareFX.ChartFX.Docked.Top);    // 그래프 설명
            
            //udcChartFX1.PointLabels = true;         // 라인 그래프 일경우 정점에 수치 표시
            //udcChartFX1.AxisY.Gridlines = true;     // Y축 그리드 표시
            //udcChartFX1.AxisY.DataFormat.Decimals = 3;
            
            udcMSChart1.Series[0].IsValueShownAsLabel = true;
            udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
        }
        #endregion

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;
            }

            this.SetFactory(cdvFactory.txtValue);            
            cdvOper.sFactory = cdvFactory.txtValue;

            SortInit();
        }

        private void rdoSummary_CheckedChanged(object sender, EventArgs e)
        {
            //2010-01-28-임종우 : 요약 선택시 Flow와 option선택 못하도록 막음.
            if (rdoSummary.Checked == true)
            {
                cdvFlow.Enabled = false;
                cboType.Enabled = false;
                lblType1.Enabled = false;
                label1.Enabled = false;
                
                ckbWR.Enabled = true;
            }
            else
            {
                cdvFlow.Enabled = true;
                cboType.Enabled = true;
                lblType1.Enabled = true;
                label1.Enabled = true;

                ckbWR.Enabled = false;
                ckbWR.Checked = false;
            }
        }

        private void cdvFlow_ValueButtonPress(object sender, EventArgs e)
        {            
            StringBuilder strQuery = new StringBuilder();

            strQuery.Append("SELECT DISTINCT FLW.FLOW AS Code, '' AS Data " + "\n");
            strQuery.Append("  FROM MWIPMATFLW@RPTTOMES FLW " + "\n");
            strQuery.Append("     , MWIPMATDEF MAT " + "\n");
            strQuery.Append(" WHERE FLW.FACTORY = MAT.FACTORY " + "\n");
            strQuery.Append("   AND FLW.MAT_ID = MAT.MAT_ID " + "\n");
            strQuery.Append("   AND FLW.FACTORY= '" + cdvFactory.txtValue + "'" + "\n");
            strQuery.Append("   AND MAT.MAT_TYPE = 'FG' " + "\n");
            strQuery.Append("   AND MAT.DELETE_FLAG = ' ' " + "\n");
            strQuery.Append("   AND FLW.FLOW <> ' ' " + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strQuery.AppendFormat("   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }
            
            #region 상세 조회에 따른 SQL문 생성
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
            }
            else
            {
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strQuery.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            }
            #endregion

            strQuery.Append(" ORDER BY FLOW " + "\n");

            cdvFlow.sDynamicQuery = strQuery.ToString();
        }

        private void ckbWR_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbWR.Checked == true)
            {
                ckbWIPTAT.Enabled = false;
                ckbWIPTAT.Checked = false;
            }
            else
            {
                ckbWIPTAT.Enabled = true;
            }
        }

        //private void chart1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    System.Windows.Forms.DataVisualization.Charting.Chart iChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
        //    iChart = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;

        //    try
        //    {
        //        HitTestResult result = iChart.HitTest(e.X, e.Y);
        //        string SeriesPoint = string.Empty;
        //        int seriesCnt = iChart.Series.Count;

        //        if (result != null)
        //        {
        //            if (seriesCnt > 1)
        //            {
        //                for (int p = 0; p < iChart.Series.Count; p++)
        //                {
        //                    iChart.Series[p].BorderWidth = 4;
        //                    iChart.Series[p].MarkerSize = 10;
        //                    iChart.Series[p].BorderDashStyle = ChartDashStyle.Solid;
        //                    iChart.Series[p].BorderColor = Color.Transparent;                            
        //                }
        //            }
        //            else
        //            {
        //                for (int p = 0; p < iChart.Series[0].Points.Count; p++)
        //                {
        //                    iChart.Series[0].Points[p].BorderWidth = 4;
        //                    iChart.Series[0].Points[p].MarkerSize = 10;
        //                    iChart.Series[0].Points[p].BorderDashStyle = ChartDashStyle.Solid;
        //                    iChart.Series[0].Points[p].BorderColor = Color.Transparent;
        //                }                        
        //            }
        //        }

        //        if (result.ChartElementType == ChartElementType.DataPoint)
        //        {
        //            SeriesPoint = result.Series.Name.ToString();
        //            DataPoint point2 = iChart.Series[SeriesPoint].Points[result.PointIndex];
        //            DataPoint tooltipPoint = iChart.Series[SeriesPoint].Points[result.PointIndex];

        //            tooltipPoint.ToolTip = iChart.Series[SeriesPoint].Name + "\n"
        //                       + iChart.Series[SeriesPoint].Points[result.PointIndex].AxisLabel + "\n"
        //                       + iChart.Series[SeriesPoint].Points[result.PointIndex].YValues[0].ToString();
                    

        //            if (seriesCnt > 1)
        //            {
        //                for (int p = 0; p < iChart.Series.Count; p++)
        //                {
        //                    iChart.Series[p].BorderWidth = 1;
        //                    iChart.Series[p].MarkerSize = 5;
        //                    iChart.Series[p].BorderDashStyle = ChartDashStyle.Solid;
        //                }

        //                iChart.Series[SeriesPoint].BorderWidth = 2;
        //                iChart.Series[SeriesPoint].MarkerSize = 12;
        //                iChart.Series[SeriesPoint].BorderDashStyle = ChartDashStyle.Dot;
        //                iChart.Series[SeriesPoint].BorderColor = Color.Red;                        
        //            }
        //            else
        //            {
        //                point2.BorderWidth = 6;
        //                point2.MarkerSize = 12;
        //                point2.BorderDashStyle = ChartDashStyle.Dot;
        //                point2.BorderColor = Color.Red;
        //            }
        //        }

        //        if (result.ChartElementType == ChartElementType.LegendItem)
        //        {
        //            SeriesPoint = result.Series.Name.ToString();

        //            for (int p = 0; p < iChart.Series.Count; p++)
        //            {
        //                iChart.Series[p].BorderWidth = 1;
        //                iChart.Series[p].MarkerSize = 5;
        //                iChart.Series[p].BorderDashStyle = ChartDashStyle.Solid;
        //            }

        //            iChart.Series[SeriesPoint].BorderWidth = 6;
        //            iChart.Series[SeriesPoint].MarkerSize = 12;
        //            iChart.Series[SeriesPoint].BorderDashStyle = ChartDashStyle.Dot;
        //            iChart.Series[SeriesPoint].BorderColor = Color.Red;                                      
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LoadingPopUp.LoadingPopUpHidden();
        //        CmnFunction.ShowMsgBox(ex.Message);
        //    }
        //}

    }
}
