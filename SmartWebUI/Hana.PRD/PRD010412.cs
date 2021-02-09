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
    public partial class PRD010412 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010412<br/>
        /// 클래스요약: 제품별 공정 수불(SAP 전송용)<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2011-12-01<br/>
        /// 상세  설명: 제품별 공정 수불(SAP 전송용-정비재C 요청)<br/>
        /// 변경  내용: <br/>        
        /// 2011-12-07-임종우 : 컬럼 추가
        /// 2019-08-29-임종우 : RSAPMESSBL_DEV 테이블 검색 기능 추가 (정비재수석 요청)
        /// 2019-09-23-임종우 : 개발 테이블 조회 시 컬럼 변경 (정비재수석 요청)
        /// 2019-11-25-김미경 : RSAPMESSBL_DEV -> MSAPMESSBL_DEV 변경 (정비재 그룹장님 요청)
        /// 2019-12-03-김미경 : DIFF_ADJUSTMENT 값이 0이 아닌 값만 볼 수 있도록 CHECK BOX 생성 (정비재 그룹장님 요청)
        /// </summary>
        public PRD010412()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                InitializeComponent();
                SortInit();
                GridColumnInit();

                //cdvDate.Value = DateTime.Now;
                cdvDate.Value = DateTime.Now.AddDays(-1);                
                txtMatID.Text = "%";                
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
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
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

                if (rdbMonth.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("PART_NO (shipment)", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PART_NO(WIP)", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("SAP_CODE", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("OPER", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("BOH", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("R_IN", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("IN PUT", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("OUT", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("R_OUT", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SHIP", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    spdData.RPT_AddBasicColumn("LOSS", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("LOSS", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("CV", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("TERMINATE", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BUFFER_LOSS", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("COMBINE_TRANSFER", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 10, 5);

                    spdData.RPT_AddBasicColumn("PART CHANGE", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("PART_CHANGE_IN", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("PART_CHANGE_OUT", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 15, 2);

                    spdData.RPT_AddBasicColumn("LOT attribute changes", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_BONUS", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_LOSS", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_IN", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_OUT", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 17, 4);

                    spdData.RPT_AddBasicColumn("LOT TYPE change", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("TYPE_E_P", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("TYPE_P_E", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 21, 2);

                    spdData.RPT_AddBasicColumn("REPAIR treatment", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("REPAIR_IN", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("REPAIR_OUT", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 23, 2);

                    spdData.RPT_AddBasicColumn("EOH", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("DIFF_ADJUSTMENT", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("DIFFER", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    for (int i = 0; i <= 9; i++)
                    {
                        spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                    }

                    spdData.RPT_MerageHeaderRowSpan(0, 25, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 26, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 27, 2);
                    
                }
                else
                {
                    spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;

                    spdData.RPT_AddBasicColumn("BASE_DATE", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("BASE_TERM", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("CUTOFF_TIME", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("FACTORY", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("LOT_TYPE", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PACKAGE", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("ORDER_ID", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("BASE_MAT_ID", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("MAT_ID", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("FLOW", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("OPER", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("OPER_SEQ_NUM", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_PREV_DAY_IN_CCL", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_PREV_DAY_LOSS_CCL", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_PREV_DAY_CV_CCL", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_PREV_DAY_OUT_CCL", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_PREV_BOH_EOH_IN_ERROR", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_PREV_BOH_EOH_LOSS_ERROR", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_PREV_BOH_EOH_CV_ERROR", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_PREV_BOH_EOH_OUT_ERROR", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_BOH_REWORK_END", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_BOH", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_IN", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_OUT", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_LOSS", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_CV", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SUBUL_EOH", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("DIFF_BASIC", 0, 28, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("DIFF_ADJUSTMENT", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("OPER_IN", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("OPER_IN_RWK", 0, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("START_QTY", 0, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("START_RWK", 0, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("END_QTY", 0, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("END_RWK", 0, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("MOVE_QTY", 0, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("MOVE_RWK", 0, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("TO_RWK", 0, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("TO_HOLD", 0, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("LOSS", 0, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("LOSS_RWK", 0, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BONUS", 0, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("CV_QTY", 0, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("CHG", 0, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SHIP_CUSTOMER", 0, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SHIP_FGS", 0, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SHIP_HMKT1", 0, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SHIP_HMKA1", 0, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("EOH", 0, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("PART_CHANGE_IN", 0, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("LOTTYPE_CHNAGE_E_P", 0, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_BONUS", 0, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_REPAIR_OUT", 0, 54, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("NM_RECEIVE_CCL", 0, 55, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_LOSS_HIS_DEL", 0, 56, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("NM_OPER_IN_CCL", 0, 57, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_OPER_IN", 0, 58, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("RETURN_STOCK_IN_NORMAL_FLOW", 0, 59, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_RETURN_STOCK_IN_HIS_DEL", 0, 60, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("PART_CHANGE_IN_HIS_DEL", 0, 61, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_END_HIS_DEL", 0, 62, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_LOSS_HIS_DEL", 0, 63, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_PART_CHANGE_OUT_HIS_DEL", 0, 64, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("REPAIR_MERGE_OUT", 0, 65, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("EOH_LOT_DEL", 0, 66, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("REWORK_MERGE_OUT", 0, 67, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("EOH_LOT_DEL_END_CCL", 0, 68, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_CUTOFF_OVER_CURR_OPER", 0, 69, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("EOH_CUTOFF_OVER_CURR_OPER", 0, 70, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("EOH_LOT_DEL_OPER_IN_CCL", 0, 71, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("LOT_DEL_CREATE_CCL", 0, 72, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("MERGE_LOT_DEL_END_REBORN", 0, 73, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("MERGE_LOT_DEL_LOSS_REBORN", 0, 74, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_ETC_TRAN_LOSS_HIS_DEL", 0, 75, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_ETC_TRAN_OUT_HIS_DEL", 0, 76, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_REWORK_END", 0, 77, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SPLIT_LOT_DEL_OPER_IN_REBORN", 0, 78, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SHIP_LOT_DEL_HIS_DEL", 0, 79, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_CUTOFF_PART_CHANGE_IN", 0, 80, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("EOH_CUTOFF_PART_CHANGE_OUT", 0, 81, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_RETURN_STOCK_IN_LOSS", 0, 82, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_FLOW_IN", 0, 83, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("MERGE_FLOW_OPER_IN", 0, 84, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_FLOW_OPER_IN", 0, 85, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("REPAIR_END_OPER_IN", 0, 86, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ORDER_ID_CHANGE_IN", 0, 87, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("PART_CHANGE_OUT", 0, 88, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("LOTTYPE_CHNAGE_P_E", 0, 89, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_LOSS", 0, 90, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("TERMINATE_QTY", 0, 91, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("TERMINATE_COMBINE", 0, 92, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("EOH_REPAIR_OUT", 0, 93, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("NM_SHIP_CCL", 0, 94, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("NM_LOSS_CCL", 0, 95, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("NM_END_CCL", 0, 96, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_BONUS_HIS_DEL", 0, 97, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("RET_HM_CO", 0, 98, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_OPER_OUT", 0, 99, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("RET_HM_CO_RETURN_STOCK_IN", 0, 100, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("RETURN_STOCK_IN", 0, 101, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("PART_CHANGE_OUT_HIS_DEL", 0, 102, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_OPER_IN_HIS_DEL", 0, 103, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_LOT_DEL", 0, 104, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_PART_CHANGE_IN_HIS_DEL", 0, 105, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("REPAIR_MERGE_IN", 0, 106, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_CUTOFF_OVER_PREV_OPER", 0, 107, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("EOH_CUTOFF_OVER_PREV_OPER", 0, 108, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("MERGE_LOT_DEL_OPER_IN_REBORN", 0, 109, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("LOT_DEL_SHIP_CCL", 0, 110, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("REWORK_OPER_IN", 0, 111, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("REWORK_LOSS", 0, 112, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_ETC_TRAN_IN_HIS_DEL", 0, 113, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_LOT_DEL_END_CCL", 0, 114, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_LOT_DEL_LOSS_CCL", 0, 115, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("BOH_CUTOFF_PART_CHANGE_OUT", 0, 116, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("EOH_CUTOFF_PART_CHANGE_IN", 0, 117, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_FLOW_OUT", 0, 118, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("MERGE_FLOW_OPER_OUT", 0, 119, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ADAPT_FLOW_OPER_OUT", 0, 120, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("COMBINE_LOT_DEL_OPER_IN_REBORN", 0, 121, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ORDER_ID_CHANGE_OUT", 0, 122, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("RMK", 0, 123, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                }
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
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = B.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "B.MAT_GRP_2 AS FAMILY", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "B.MAT_GRP_3 AS PACKAGE", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "B.MAT_GRP_4 AS TYPE1", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "B.MAT_GRP_5 AS TYPE2", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "B.MAT_GRP_6", "B.MAT_GRP_6 AS \"LD COUNT\"", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "B.MAT_GRP_7 AS DENSITY", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "B.MAT_GRP_8 AS GENERATION", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "B.MAT_CMF_10", "B.MAT_CMF_10 AS PIN_TYPE", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "B.MAT_ID", "B.MAT_ID AS PRODUCT", true);
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
            string getStartTranTime = null;
            string getEndTranTime = null;

            string getStartDate = cdvDate.Value.ToString("yyyyMM") + "01";
            string getLastEndDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01").AddDays(-1).ToString("yyyyMMdd");


            //if (cboTime.Text == "22시")
            if (cboTime.SelectedIndex == 0)
            {
                getStartTranTime = getLastEndDate + "220000";
                getEndTranTime = cdvDate.SelectedValue() + "215959";
            }
            else
            {
                getStartTranTime = getStartDate + "060000";
                getEndTranTime = cdvDate.Value.AddDays(1).ToString("yyyyMMdd") + "055959";
            }

            if (rdbMonth.Checked == true)
            {
                if (ckbDev.Checked == false)
                {
                    strSqlString.Append("SELECT A.*" + "\n");
                    strSqlString.Append("     , (BOH + INPUT + R_OUT + PART_CHANGE_IN + ADAPT_BONUS + ADAPT_OPER_IN + LOTTYPE_CHNAGE_E_P + REPAIR_END_OPER_IN) - (R_IN + OUTPUT + SHIP + LOSS + CV_QTY + TERMINATE_QTY + BUFFER_LOSS + COMBINE_TRANSFER + PART_CHANGE_OUT + ADAPT_LOSS + ADAPT_OPER_OUT + LOTTYPE_CHNAGE_P_E + REPAIR_END_OPER_OUT + EOH) AS DIFF" + "\n");
                    strSqlString.Append("  FROM (" + "\n");
                    strSqlString.Append("        SELECT NVL((SELECT MAX(MCP_TO_PART) FROM RWIPMCPBOM WHERE FACTORY = A.FACTORY AND MAT_ID = A.MAT_ID), A.MAT_ID) AS MCP_TO_PART" + "\n");
                    strSqlString.Append("             , A.MAT_ID" + "\n");
                    strSqlString.Append("             , A.BASE_MAT_ID AS SAP_CODE" + "\n");
                    strSqlString.Append("             , A.OPER" + "\n");
                    strSqlString.Append("             , SUM(CASE WHEN BASE_DATE BETWEEN '" + getStartDate + "' AND '" + cdvDate.SelectedValue() + "' THEN A.BOH ELSE 0 END) AS BOH" + "\n");
                    //strSqlString.Append("             , SUM(CASE WHEN BASE_DATE = '" + getStartDate + "' THEN A.BOH ELSE 0 END) AS BOH" + "\n");
                    strSqlString.Append("             , SUM(A.RET_HM_CO) AS R_IN" + "\n");
                    strSqlString.Append("             , SUM(A.OPER_IN - A.BOH_OPER_IN_HIS_DEL - A.BOH_ETC_TRAN_IN_HIS_DEL) AS INPUT" + "\n");
                    strSqlString.Append("             , SUM(CASE WHEN A.OPER LIKE '%Z010' THEN A.MOVE_QTY - A.SHIP_CUSTOMER" + "\n");
                    strSqlString.Append("                        WHEN A.OPER LIKE '%0000' THEN A.MOVE_QTY - A.RET_HM_CO" + "\n");
                    strSqlString.Append("                        ELSE A.MOVE_QTY END) AS OUTPUT" + "\n");
                    strSqlString.Append("             , SUM(0) AS R_OUT" + "\n");
                    strSqlString.Append("             , SUM(A.SHIP_CUSTOMER) AS SHIP" + "\n");
                    strSqlString.Append("             , SUM(NVL(A.LOSS,0)) - SUM(NVL(C.BUFFER_LOSS,0)) AS LOSS" + "\n");
                    strSqlString.Append("             , SUM(A.CV_QTY) AS CV_QTY" + "\n");
                    strSqlString.Append("             , SUM(A.TERMINATE_QTY) AS TERMINATE_QTY" + "\n");
                    strSqlString.Append("             , SUM(NVL(C.BUFFER_LOSS,0)) AS BUFFER_LOSS" + "\n");
                    strSqlString.Append("             , SUM(A.TERMINATE_COMBINE) AS COMBINE_TRANSFER" + "\n");
                    strSqlString.Append("             , SUM(A.PART_CHANGE_IN) AS PART_CHANGE_IN" + "\n");
                    strSqlString.Append("             , SUM(A.PART_CHANGE_OUT) AS PART_CHANGE_OUT" + "\n");
                    strSqlString.Append("             , SUM(A.ADAPT_BONUS) AS ADAPT_BONUS" + "\n");
                    strSqlString.Append("             , SUM(A.ADAPT_LOSS) AS ADAPT_LOSS" + "\n");
                    strSqlString.Append("             , SUM(A.ADAPT_OPER_IN) AS ADAPT_OPER_IN" + "\n");
                    strSqlString.Append("             , SUM(A.ADAPT_OPER_OUT) AS ADAPT_OPER_OUT" + "\n");
                    strSqlString.Append("             , SUM(A.LOTTYPE_CHNAGE_E_P) AS LOTTYPE_CHNAGE_E_P" + "\n");
                    strSqlString.Append("             , SUM(A.LOTTYPE_CHNAGE_P_E) AS LOTTYPE_CHNAGE_P_E" + "\n");
                    strSqlString.Append("             , SUM(A.REPAIR_END_OPER_IN) AS REPAIR_END_OPER_IN " + "\n");
                    strSqlString.Append("             , SUM(A.EOH_REPAIR_OUT) AS REPAIR_END_OPER_OUT" + "\n");
                    strSqlString.Append("             , SUM(CASE WHEN BASE_DATE BETWEEN '" + getStartDate + "' AND '" + cdvDate.SelectedValue() + "' THEN A.EOH ELSE 0 END) AS EOH " + "\n");
                    //strSqlString.Append("             , SUM(CASE WHEN BASE_DATE = '" + cdvDate.SelectedValue() + "' THEN A.EOH ELSE 0 END) AS EOH " + "\n");
                    strSqlString.Append("             , SUM(DIFF_ADJUSTMENT) AS DIFF_ADJUSTMENT" + "\n");
                    strSqlString.Append("          FROM RSAPMESSBL A " + "\n");
                }
                else
                {
                    strSqlString.Append("SELECT A.*" + "\n");
                    strSqlString.Append("     , (BOH + INPUT) - (OUTPUT + LOSS + CV_QTY + EOH) AS DIFF" + "\n");
                    strSqlString.Append("  FROM (" + "\n");
                    strSqlString.Append("        SELECT NVL((SELECT MAX(MCP_TO_PART) FROM RWIPMCPBOM WHERE FACTORY = A.FACTORY AND MAT_ID = A.MAT_ID), A.MAT_ID) AS MCP_TO_PART" + "\n");
                    strSqlString.Append("             , A.MAT_ID" + "\n");
                    strSqlString.Append("             , A.BASE_MAT_ID AS SAP_CODE" + "\n");
                    strSqlString.Append("             , A.OPER" + "\n");
                    strSqlString.Append("             , SUM(CASE WHEN BASE_DATE BETWEEN '" + getStartDate + "' AND '" + cdvDate.SelectedValue() + "' THEN A.SUBUL_BOH ELSE 0 END) AS BOH" + "\n");
                    //strSqlString.Append("             , SUM(CASE WHEN BASE_DATE = '" + getStartDate + "' THEN A.SUBUL_BOH ELSE 0 END) AS BOH" + "\n");
                    strSqlString.Append("             , SUM(0) AS R_IN" + "\n");
                    strSqlString.Append("             , SUM(A.SUBUL_IN) AS INPUT" + "\n");
                    strSqlString.Append("             , SUM(A.SUBUL_OUT) AS OUTPUT" + "\n");
                    strSqlString.Append("             , SUM(0) AS R_OUT" + "\n");
                    strSqlString.Append("             , SUM(A.SHIP_CUSTOMER) AS SHIP" + "\n");
                    strSqlString.Append("             , SUM(A.SUBUL_LOSS) AS LOSS" + "\n");
                    strSqlString.Append("             , SUM(A.SUBUL_CV) AS CV_QTY" + "\n");
                    strSqlString.Append("             , SUM(A.TERMINATE_QTY) AS TERMINATE_QTY" + "\n");
                    strSqlString.Append("             , SUM(NVL(C.BUFFER_LOSS,0)) AS BUFFER_LOSS" + "\n");
                    strSqlString.Append("             , SUM(A.TERMINATE_COMBINE) AS COMBINE_TRANSFER" + "\n");
                    strSqlString.Append("             , SUM(A.PART_CHANGE_IN) AS PART_CHANGE_IN" + "\n");
                    strSqlString.Append("             , SUM(A.PART_CHANGE_OUT) AS PART_CHANGE_OUT" + "\n");
                    strSqlString.Append("             , SUM(A.ADAPT_BONUS) AS ADAPT_BONUS" + "\n");
                    strSqlString.Append("             , SUM(A.ADAPT_LOSS) AS ADAPT_LOSS" + "\n");
                    strSqlString.Append("             , SUM(A.ADAPT_OPER_IN) AS ADAPT_OPER_IN" + "\n");
                    strSqlString.Append("             , SUM(A.ADAPT_OPER_OUT) AS ADAPT_OPER_OUT" + "\n");
                    strSqlString.Append("             , SUM(A.LOTTYPE_CHNAGE_E_P) AS LOTTYPE_CHNAGE_E_P" + "\n");
                    strSqlString.Append("             , SUM(A.LOTTYPE_CHNAGE_P_E) AS LOTTYPE_CHNAGE_P_E" + "\n");
                    strSqlString.Append("             , SUM(A.REPAIR_END_OPER_IN) AS REPAIR_END_OPER_IN " + "\n");
                    strSqlString.Append("             , SUM(A.EOH_REPAIR_OUT) AS REPAIR_END_OPER_OUT" + "\n");
                    strSqlString.Append("             , SUM(CASE WHEN BASE_DATE BETWEEN '" + getStartDate + "' AND '" + cdvDate.SelectedValue() + "' THEN A.SUBUL_EOH ELSE 0 END) AS EOH " + "\n");
                    //strSqlString.Append("             , SUM(CASE WHEN BASE_DATE = '" + cdvDate.SelectedValue() + "' THEN A.SUBUL_EOH ELSE 0 END) AS EOH " + "\n");
                    strSqlString.Append("             , SUM(DIFF_ADJUSTMENT) AS DIFF_ADJUSTMENT" + "\n");
                    strSqlString.Append("          FROM MSAPMESSBL_DEV@RPTTOMES A " + "\n");
                }
                
                strSqlString.Append("             , MWIPMATDEF B " + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID, OPER, SUM(LOSS_QTY) AS BUFFER_LOSS" + "\n");
                strSqlString.Append("                  FROM RWIPLOTLSM" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                //strSqlString.Append("                   AND TRAN_TIME BETWEEN '20190609220000' AND '20190610215959'" + "\n");
                strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + getStartTranTime + "' AND '" + getEndTranTime + "'" + "\n");
                strSqlString.Append("                   AND HIST_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND OPER IN ('A0015', 'A0395')" + "\n");
                strSqlString.Append("                   AND LOSS_CODE = 'N01'" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID, OPER" + "\n");
                strSqlString.Append("               ) C" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND A.OPER = C.OPER(+)" + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("           AND A.BASE_DATE BETWEEN '" + getStartDate + "' AND '" + cdvDate.SelectedValue() + "'" + "\n");
                strSqlString.Append("           AND A.CUTOFF_TIME = '" + cboTime.Text.Replace("시", "") + "'" + "\n");
                strSqlString.Append("           AND A.MAT_ID LIKE '%'" + "\n");
                strSqlString.Append("           AND B.MAT_TYPE = 'FG' " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("           AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                if (txtMatID.Text.Trim() != "%" && txtMatID.Text.Trim() != "")
                {
                    strSqlString.Append("           AND A.MAT_ID LIKE '" + txtMatID.Text + "'" + "\n");
                }
                
                if (chkDiff.Checked == true)
                {
                    strSqlString.Append("           AND A.DIFF_ADJUSTMENT <> 0" + "\n");
                }

                strSqlString.Append("         GROUP BY A.FACTORY, A.MAT_ID, A.BASE_MAT_ID, A.OPER" + "\n");
                strSqlString.Append("       ) A" + "\n");
                strSqlString.Append(" ORDER BY MCP_TO_PART, MAT_ID, SAP_CODE, OPER" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT A.* " + "\n");

                if (ckbDev.Checked == false)
                {
                    strSqlString.Append("  FROM RSAPMESSBL A " + "\n");
                }
                else
                {
                    strSqlString.Append("  FROM MSAPMESSBL_DEV@RPTTOMES A " + "\n");
                }
                                
                strSqlString.Append("     , MWIPMATDEF B " + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("   AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("   AND A.BASE_DATE = '" + cdvDate.SelectedValue() + "'" + "\n");
                strSqlString.Append("   AND A.CUTOFF_TIME = '" + cboTime.Text.Replace("시", "") + "'" + "\n");
                //strSqlString.Append("   AND A.MAT_ID LIKE '" + txtMatID.Text + "'" + "\n");

                if (txtMatID.Text.Trim() != "%" && txtMatID.Text.Trim() != "")
                {
                    strSqlString.Append("   AND A.MAT_ID LIKE '" + txtMatID.Text + "'" + "\n");
                }

                if (chkDiff.Checked == true)
                {
                    strSqlString.Append("           AND A.DIFF_ADJUSTMENT <> 0" + "\n");
                }

                strSqlString.Append("   AND B.MAT_TYPE = 'FG' " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("  AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("  AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("  AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("  AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("  AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("  AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("  AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("  AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("  AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append(" ORDER BY A.BASE_DATE, A.BASE_TERM, A.CUTOFF_TIME, A.FACTORY, A.LOT_TYPE, A.CUSTOMER, A.PACKAGE, A.ORDER_ID, A.BASE_MAT_ID, A.MAT_ID, A.FLOW, A.OPER, A.OPER_SEQ_NUM " + "\n");
            }

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

        
    }
}
