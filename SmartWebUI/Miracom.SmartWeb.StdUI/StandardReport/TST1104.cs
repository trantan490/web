using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Miracom.SmartWeb.UI
{
    public partial class TST1104 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public TST1104()
        {
            InitializeComponent();
        }

        private void TST1104_Load(object sender, EventArgs e)
        {
            cdvFactory.Init();
            FunctionList();
            lisOper.SmallImageList = imlToolBar16;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnCloseLayoutForm();
                this.Dispose();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        private void cdvFactory_ButtonPress(object sender, EventArgs e)
        {
            cdvFactory.Init();
            CmnInitFunction.InitListView(cdvFactory.GetListView);
            cdvFactory.Columns.Add("Factory", 50, HorizontalAlignment.Left);
            cdvFactory.Columns.Add("Desc", 100, HorizontalAlignment.Left);
            cdvFactory.SelectedSubItemIndex = 0;
            RptListFunction.ViewFactoryList(cdvFactory.GetListView);
        }

        private void lisFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lisFunction.SelectedIndices.Count > 0)
            {
                txtFunctionName.Text = lisFunction.SelectedItems[0].SubItems[1].Text;
                if (rbnOper.Checked)
                {
                    CmnInitFunction.InitListView(lisProcOper);
                    CmnInitFunction.InitListView(lisOper);
                    ProcOperationList("1");

                }
                else
                {
                    CmnInitFunction.InitListView(lisProcOper);
                    CmnInitFunction.InitListView(lisOper);
                    ProcOperationList("2");
                }
            }
            else
            {
                txtFunctionName.Text = "";
                CmnInitFunction.InitListView(lisProcOper);
            }
        }

        private void rbnOper_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnOper.Checked)
            {
                CmnInitFunction.InitListView(lisProcOper);
                CmnInitFunction.InitListView(lisOper);
                GubunOprGrpList("OPR");
                ProcOperationList("1");
            }
        }

        private void rbnOpergrp_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnOpergrp.Checked)
            {
                CmnInitFunction.InitListView(lisProcOper);
                CmnInitFunction.InitListView(lisOper);
                GubunOprGrpList("OPRGRP");
                ProcOperationList("2");
            }
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            int seq = 0;
            
            try
            {
                if (lisFunction.Items.Count != 0 
                    && lisFunction.SelectedItems.Count != 0 
                    && lisOper.Items.Count != 0 
                    && lisOper.SelectedItems.Count != 0)
                {
                    if (lisProcOper.Items.Count == 0) seq = 0;
                    else
                    {
                        if (lisProcOper.SelectedItems.Count == 0) seq = lisProcOper.Items.Count;
                        else seq = lisProcOper.SelectedItems[0].Index+1;
                    }
                    for (int i = 0; i < lisOper.SelectedItems.Count; i++) 
                    {
                        if (FindOper(lisOper.SelectedItems[i].SubItems[0].Text))
                        {
                            if (!UpdateProcOper("1", lisOper.SelectedItems[i].SubItems[0].Text.Trim(), seq, lisOper.SelectedItems[i].SubItems[1].Text.Trim())) break;
                        }
                        seq +=1;
                    }

                    if (rbnOper.Checked)
                    {
                        CmnInitFunction.InitListView(lisProcOper);
                        CmnInitFunction.InitListView(lisOper);
                        ProcOperationList("1");
                    }
                    else
                    {
                        CmnInitFunction.InitListView(lisProcOper);
                        CmnInitFunction.InitListView(lisOper);
                        ProcOperationList("2");
                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        private void btnDetach_Click(object sender, EventArgs e)
        {
            int seq = 0;

            try
            {
                if (lisFunction.Items.Count != 0 
                    && lisFunction.SelectedItems.Count != 0
                    && lisProcOper.Items.Count != 0
                    && lisProcOper.SelectedItems.Count != 0)
                {
                    //for (int i = 0; i < lisProcOper.Items.Count; i++)
                    //{
                    //    if (lisProcOper.Items[i].SubItems[0].Text != lisProcOper.SelectedItems[0].SubItems[0].Text)
                    //    {
                    //        seq = i+1;
                    //    }
                    //}
                    //for (int j = 0; j < lisProcOper.SelectedItems.Count; j++)
                    //{
                    //    if (!UpdateProcOper("2", lisProcOper.SelectedItems[j].SubItems[0].Text.Trim(), seq, lisProcOper.SelectedItems[j].SubItems[1].Text.Trim())) break;
                    //}


                    if (lisProcOper.Items.Count == 0) seq = 0;
                    else
                    {
                        if (lisProcOper.SelectedItems.Count == 0) seq = 0;
                        else seq = lisProcOper.SelectedItems[0].Index;
                    }
                    for (int i = 0; i < lisProcOper.SelectedItems.Count; i++)
                    {
                        if (!UpdateProcOper("2", lisProcOper.SelectedItems[i].SubItems[0].Text.Trim(), seq, lisProcOper.SelectedItems[i].SubItems[1].Text.Trim())) break;
                    }


                    if (rbnOper.Checked)
                    {
                        CmnInitFunction.InitListView(lisProcOper);
                        CmnInitFunction.InitListView(lisOper);
                        ProcOperationList("1");
                    }
                    else
                    {
                        CmnInitFunction.InitListView(lisProcOper);
                        CmnInitFunction.InitListView(lisOper);
                        ProcOperationList("2");
                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        private void cdvFactory_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            if (rbnOper.Checked)
            {
                CmnInitFunction.InitListView(lisOper);
                GubunOprGrpList("OPR");
            }
            else
            {
                CmnInitFunction.InitListView(lisOper);
                GubunOprGrpList("OPRGRP");
            }
        }



        #region Added Function 
        
        private Boolean CheckField() //Factory 선택여부
        {
            Boolean Check = false;

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show("Please Input Factory Code.");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

        private void FunctionList() //메뉴리스트
        {
            CmnInitFunction.InitListView(lisFunction);
            RptListFunction.ViewFunctionList(lisFunction, "1", "");
        }

        private void ProcOperationList(String sStep) //등록된 Oper/OperGroup Function Item리스트
        {
            string QueryCond = null;
            string QueryData = null;

            if (CheckField() && lisFunction.SelectedIndices.Count > 0)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
                QueryData = lisFunction.SelectedItems[0].SubItems[0].Text.Trim();
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, QueryData);
                switch (sStep)
                {
                    case "1":
                        QueryData = "OPR";
                        break;
                    case "2":
                        QueryData = "OPRGRP";
                        break;
                }
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, QueryData);

                RptListFunction.ViewFuncItemList(lisProcOper, "1", QueryCond);
                GubunOprGrpList(QueryData);
            }
        }        
        
        private void GubunOprGrpList(string sGubun) //Oper 또는 OperGroup 리스트
        {
            if (sGubun == "OPR")
            {
                RptListFunction.ViewOperationList(lisOper, cdvFactory.Text);
            }
            else
            {
                RptListFunction.ViewCmfGcmGrpList(lisOper, cdvFactory.Text, "GRP_OPER", GlobalConstant.MP_GCM_OPER_GRP[0]);
            }
        }

        private Boolean FindOper(string sOper) 
        {
            try
            {
                int i;
                if (lisProcOper.Items.Count == 0) return true;
                else
                {
                    for (i = 0; i < lisProcOper.Items.Count; i++)
                    {
                        if (lisProcOper.Items[i].SubItems[0].Text == sOper) return false;
                    }
                  return true;
                }
            } 
            catch(Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        private Boolean UpdateProcOper(string sStep, string sOper, int iSeq, string sOperDesc)// 수정- Oper/OperGroup Function Item리스트
        {
            string QueryData = "";
            string QueryCondUpdt = null;
            string QueryCond = null;
            try
            {
                QueryCondUpdt = FwxCmnFunction.PackCondition(QueryCondUpdt, GlobalVariable.gsUserID);

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
                QueryCondUpdt = FwxCmnFunction.PackCondition(QueryCondUpdt, cdvFactory.Text);

                QueryData = lisFunction.SelectedItems[0].SubItems[0].Text.Trim();
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, QueryData);
                QueryCondUpdt = FwxCmnFunction.PackCondition(QueryCondUpdt, QueryData);

                if (rbnOper.Checked) QueryData = "OPR";
                else QueryData = "OPRGRP";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, QueryData);
                QueryCondUpdt = FwxCmnFunction.PackCondition(QueryCondUpdt, QueryData);

                int seq = iSeq + 1;
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, seq.ToString());
                QueryCondUpdt = FwxCmnFunction.PackCondition(QueryCondUpdt, seq.ToString());

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sOper);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sOperDesc);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsUserID);

                if (sStep == "1")//insert
                {
                    if (CmnFunction.oComm.UpdateData("RWEBFUNITM", "1", QueryCondUpdt))
                    {
                        if(!CmnFunction.oComm.InsertData("RWEBFUNITM", "1", QueryCond))
                        {
                            CmnFunction.ShowMsgBox("RPT-0004");
                            return false;
                        }
                        else
                        {
                            CmnInitFunction.InitListView(lisProcOper);
                            CmnInitFunction.InitListView(lisOper);
                            return true;
                        }
                    }
                    else
                    {
                        CmnInitFunction.InitListView(lisProcOper);
                        CmnInitFunction.InitListView(lisOper);
                        return true;
                    }
                }

                else if (sStep == "2")//delete
                {
                    if (CmnFunction.oComm.DeleteData("RWEBFUNITM", "1", QueryCond))
                    {
                        if (!CmnFunction.oComm.UpdateData("RWEBFUNITM", "2", QueryCondUpdt))
                        {
                            CmnFunction.ShowMsgBox("RPT-0004");
                            return false;
                        }
                        else
                        {
                            CmnInitFunction.InitListView(lisProcOper);
                            CmnInitFunction.InitListView(lisOper);
                            return true;
                        }
                    }
                    else
                    {
                        CmnInitFunction.InitListView(lisProcOper);
                        CmnInitFunction.InitListView(lisOper);
                        return true;
                    }
                }
                else
                {
                    CmnInitFunction.InitListView(lisProcOper);
                    CmnInitFunction.InitListView(lisOper);
                    return true;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        #endregion


    }
}
