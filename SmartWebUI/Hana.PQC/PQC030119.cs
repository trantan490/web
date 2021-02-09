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
    public partial class PQC030119 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private DataTable dtOper = null;
        private DataTable dtOperGrp = null;

        // 2010-12-08-임종우 : 스프레드에 데이터 누적 표시 하기 위해 DataTable 선언.
        private DataTable dtSum = null; 

        /// <summary>
        /// 클  래  스: PQC030119<br/>
        /// 클래스요약: 불량 lot list 조회<br/>
        /// 작  성  자: 길민수<br/>
        /// 최초작성일: 2010-12-01<br/>
        /// 상세  설명: 불량 lot list 조회<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: <br />
        /// 2010-12-08-임종우 : 스프레드에 데이터 누적 기능 & 초기화 기능 추가 함 (문구K 요청)
        /// </summary>
        public PQC030119()
        {
            InitializeComponent();

            dtOper = new DataTable();
            dtOperGrp = new DataTable();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);

        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {

            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "CUSTOMER", "CUSTOMER", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "FAMILY", "FAMILY", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "LD_COUNT", "LD_COUNT", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "PKG", "PKG", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "TYPE1", "TYPE1", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "TYPE2", "TYPE2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "DENSITY", "DENSITY", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "GENERATION", "GENERATION", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID", "MAT_ID", true);
             
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

            spdData.RPT_AddBasicColumn("LOT ID", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            //BG
            spdData.RPT_AddBasicColumn("B/G", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("B/G Lot #", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("WBL Lot #", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //SAW
            spdData.RPT_AddBasicColumn("SAW", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("BLADE LOT #", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 110);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //DA1
            spdData.RPT_AddBasicColumn("D/A 1", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("L/F or PCB LOT#", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 125);
            spdData.RPT_AddBasicColumn("Epoxy Lot #", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //DA2
            spdData.RPT_AddBasicColumn("D/A 2", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("L/F or PCB LOT#", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 125);
            spdData.RPT_AddBasicColumn("Epoxy Lot #", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //DA3
            spdData.RPT_AddBasicColumn("D/A 3", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("L/F or PCB LOT#", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 125);
            spdData.RPT_AddBasicColumn("Epoxy Lot #", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //WB1
            spdData.RPT_AddBasicColumn("W/B 1", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Wire lot #", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 28, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //WB2
            spdData.RPT_AddBasicColumn("W/B 2", 0, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Wire lot #", 0, 30, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 31, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 32, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //WB3
            spdData.RPT_AddBasicColumn("W/B 3", 0, 33, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Wire lot #", 0, 34, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 35, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 36, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //MD
            spdData.RPT_AddBasicColumn("M/D", 0, 37, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EMC Lot #", 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 125);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 39, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 40, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //MARK
            spdData.RPT_AddBasicColumn("MARK facilities", 0, 41, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 43, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //TRIM
            spdData.RPT_AddBasicColumn("Trim equipment", 0, 44, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 45, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 46, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //PMC
            spdData.RPT_AddBasicColumn("PMC Equipment", 0, 47, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 48, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 49, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //SBA
            spdData.RPT_AddBasicColumn("SBA Equipment", 0, 50, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("SBA LOT#", 0, 51, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 52, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 53, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //Plating
            spdData.RPT_AddBasicColumn("Plating company", 0, 54, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 110);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 55, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 56, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //SBA
            spdData.RPT_AddBasicColumn("SST Equipment #", 0, 57, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("MD LOT#", 0, 58, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 59, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 60, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //FORM
            spdData.RPT_AddBasicColumn("Form Equipment", 0, 61, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 62, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 63, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //FORM
            spdData.RPT_AddBasicColumn("T/F Equipment #", 0, 64, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN TIME", 0, 65, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("OUT TIME", 0, 66, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
           
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

            //GridColumnInit();

            if (txtLotID.Text.Length < 3)
            {

                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD040", GlobalVariable.gcLanguage));
                return;

            }

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

                // 2010-12-08-임종우 : 스프레드에 데이터 누적 표시 기능 추가 함.
                if (dtSum == null)
                {
                    dtSum = dt;
                }
                else
                {
                    dtSum.Merge(dt);
                }

                spdData.DataSource = dtSum;              
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
            if (txtLotID.Text.Trim().Length == 0)
            {
                //원래 Message는 LOT_ID가 필요합니다 이지만 LOT_ID를 입력해주세요와 같은 의미이기에 하나로 통일
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD009", GlobalVariable.gcLanguage));
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
            string sLotid = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            sLotid = txtLotID.Text;
            //string[] strDate = cdvFromToDate.getSelectDate();
            //int LastDate = strDate.Length;

            // Decode 반복문 셋팅
            string strDecode = string.Empty;


            strSqlString.Append("        SELECT A.LOT_ID AS LOT_ID " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0040'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS BG_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A0040' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS BG_RES_LOT  " + "\n");
            strSqlString.Append("        , ' ' \n");

            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0040'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS BG_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A0040'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS BG_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0200'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS SAW_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A0200' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS SAW_RES_LOT  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0200'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS SAW_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A0200'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS SAW_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0400'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS DA1_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A0400' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND (M_MAT_ID LIKE 'R14%' " + "\n");
            strSqlString.Append("        OR M_MAT_ID LIKE 'R16%') " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS DA1_RES_LF_PCB_LOT  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A0400' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND M_MAT_ID LIKE 'R18%' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS DA1_RES_EPOXY_LOT " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0400'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS DA1_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A0400'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS DA1_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0401'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS DA2_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A0401' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND (M_MAT_ID LIKE 'R14%' " + "\n");
            strSqlString.Append("        OR M_MAT_ID LIKE 'R16%') " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS DA2_RES_LF_PCB_LOT  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A0401' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND M_MAT_ID LIKE 'R18%' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS DA2_RES_EPOXY_LOT " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0401'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS DA2_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A0401'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS DA2_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0402'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS DA3_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A0402' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND (M_MAT_ID LIKE 'R14%' " + "\n");
            strSqlString.Append("        OR M_MAT_ID LIKE 'R16%') " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS DA3_RES_LF_PCB_LOT  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A0402' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND M_MAT_ID LIKE 'R18%' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS DA3_RES_EPOXY_LOT " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0402'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS DA3_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A0402'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS DA3_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0600'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS WB1_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A0600' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND M_MAT_ID LIKE 'R19%' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS WB1_RES_LOT " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0600'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS WB1_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A0600'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS WB1_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0601'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS WB2_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A0601' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND M_MAT_ID LIKE 'R19%' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS WB2_RES_LOT " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0601'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS WB2_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A0601'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS WB2_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0602'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS WB3_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A0602' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND M_MAT_ID LIKE 'R19%' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS WB3_RES_LOT " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A0602'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS WB3_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A0602'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS WB3_END_TIME " + "\n");
            strSqlString.Append("                " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1000'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS MD_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A1000' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND M_MAT_ID LIKE 'R15%' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS MD_RES_LOT " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1000'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS MD_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A1000'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS MD_END_TIME " + "\n");
            strSqlString.Append("                " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1150'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS MD_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1150'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS MD_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A1150'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS MD_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1200'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS TRIM_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1200'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS TRIM_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A1200'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS TRIM_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1230'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS PMC_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1230'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS PMC_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A1230'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS PMC_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1300'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS SBA_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A1300' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND M_MAT_ID LIKE 'R17%' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS MD_RES_LOT " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1300'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS SBA_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A1300'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS SBA_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT TRAN_COMMENT FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A1450'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS PMC_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1450'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS PMC_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A1450'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS PMC_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1750'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS SST_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT LOT_ID FROM MESMGR.CRESMATHIS@RPTTOMES " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND START_LOT_ID = A.LOT_ID " + "\n");
            strSqlString.Append("        AND P_OPER ='A1750' " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("        AND M_MAT_ID LIKE 'R212%' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) AS MD_RES_LOT " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1750'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS SST_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A1750'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS SST_END_TIME " + "\n");
            strSqlString.Append("        , (SELECT START_RES_ID FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1800'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS FORM_RES_ID  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='START'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OPER ='A1800'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS FORM_START_TIME  " + "\n");
            strSqlString.Append("        , (SELECT TRAN_TIME FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE LOT_ID = A.LOT_ID   " + "\n");
            strSqlString.Append("        AND TRAN_CODE ='END'  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND OLD_OPER ='A1800'  " + "\n");
            //strSqlString.Append("        AND LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append("        AND HIST_DEL_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("        ) AS FORM_END_TIME " + "\n");

            strSqlString.Append("      , (SELECT START_RES_ID FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("         WHERE LOT_ID = A.LOT_ID  " + "\n");
            strSqlString.Append("           AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("           AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND OPER ='A1900' " + "\n");
            strSqlString.Append("           AND LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("           AND HIST_DEL_FLAG <> 'Y' " + "\n");
            strSqlString.Append("       ) AS TF_RES_ID" + "\n");
            strSqlString.Append("     , (SELECT TRAN_TIME FROM RWIPLOTHIS" + "\n");
            strSqlString.Append("         WHERE LOT_ID = A.LOT_ID  " + "\n");
            strSqlString.Append("           AND TRAN_CODE ='START' " + "\n");
            strSqlString.Append("           AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND OPER ='A1900' " + "\n");
            strSqlString.Append("           AND LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("           AND HIST_DEL_FLAG <> 'Y' " + "\n");
            strSqlString.Append("       ) AS TF_START_TIME" + "\n");
            strSqlString.Append("     , (SELECT TRAN_TIME FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("         WHERE LOT_ID = A.LOT_ID  " + "\n");
            strSqlString.Append("           AND TRAN_CODE ='END' " + "\n");
            strSqlString.Append("           AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND OLD_OPER ='A1900' " + "\n");
            strSqlString.Append("           AND LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("           AND HIST_DEL_FLAG <> 'Y' " + "\n");
            strSqlString.Append("       ) AS TF_END_TIME" + "\n");

            strSqlString.Append("        FROM (SELECT * FROM RWIPLOTHIS  " + "\n");
            strSqlString.Append("        WHERE 1=1  " + "\n");
            strSqlString.Append("        AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("        AND LOT_ID = '" + sLotid + "' " + "\n");
            strSqlString.Append("        AND ROWNUM = 1 " + "\n");
            strSqlString.Append("        ) A " + "\n");
 


            #region 상세 조회에 따른 SQL문 생성
            

            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #endregion

        #region Event Handler

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

        private void rbtBrief_CheckedChanged(object sender, EventArgs e)
        {
            //cdvStep.Visible = !(rbtBrief.Checked);
        }

        #endregion

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                btnView_Click(sender, e);

        }

        // 2010-12-08-임종우 : 스프레드 초기화
        private void btnReset_Click(object sender, EventArgs e)
        {
            spdData.ActiveSheet.Rows.Count = 0;
            dtSum.Clear();
        }        
    }
}

