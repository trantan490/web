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
    public partial class PRD010207 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private String[] dayArry = new String[7];
        private String[] dayArry2 = new String[7];
        private Int32[] standardDay = new Int32[7]; //������
        private String stRemainDay = null;  // �ܿ���
        private String stThisWeek = null; // �� ����
        private String stNextWeek = null; // ���� ����
        private String stNextWeekStartDay = null; // ���� ���� ������
        private String stNextWeekEndDay = null; // ���� ���� ��������
        private Boolean weekStatus = false; // ���� ���� ���� Ȯ��
        private String stLastWeekEndDay = null; // ������ �������� (���� ������ �������� �������� ���ϱ� ����...)

        /// <summary>
        /// Ŭ  ��  ��: PRD010207<br/>
        /// Ŭ�������: �Ｚ �޸� SOP ����<br/>
        /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
        /// �����ۼ���: 2010-09-24<br/>
        /// ��  ����: �Ｚ �޸� SOP ����<br/>
        /// ����  ����: <br/> 
        /// 2010-10-05-������ : LOT TYPE P%�� ��ȸ �ǵ��� ���� (���¼� ��û)
        /// 2010-11-01-������ : �Ｚ ���� �ڵ� �������� ���� ���� ��� ����.
        /// 2011-05-23-������ : Middle Part �������� ���� (���¼� ��û)
        /// 2011-06-08-������ : �ְ� ��ȹ ���� ���� - ���� ���� + ���� �ش� ���� ���� ��� (���¼� ��û)
        /// 2011-06-09-������ : SAW GROUP �߰� �� ���, ���� ���� ���� (�輺�� ��û)
        /// 2011-06-13-������ : SEC_VERSION �� ���� �����ϵ��� ���� (�輺�� ��û)
        /// 2011-07-21-������ : �������(����, REMAIN, ������) �߰�, ��� ������ ���� ���� (�輺�� ��û)
        /// 2011-08-30-��ο� : ���� REMAIN �߰�(Logic: ������ȹ - ������� ����(SHIP) - �������(��̳��̼�~���ϴ��)
        /// 2011-09-01-����� : ���� �������� ��� ����, ���� ����,���� ship ������ ǥ��ǵ��� ���� (�輺�� ��û)
        /// 2011-09-06-����� : ������� Remain�� ����, ���� ����� �ƴ� ���� ������� ���� (�輺�� ��û)
        /// 2011-11-07-��ο� : ������� ����(���� ��ȸ�� ��ȸ��¥ �ߺ����� �������� ���� �ذ�), ���� remain ����
        /// 2012-01-18-��ο� : DA REMAIN ���� (WIP_OLD 'DA' THEN 'A0550' --> 'DA' THEN 'A0500') (�輺�� ��û) 
        /// 2012-05-15-������ : MCP, DDP, QDP PKG �и� �۾�
        /// 2012-07-10-������ : OTD ��ȹ�� ���� ��ǰ�� ���, ������ �����ϸ� ǥ�� �ǵ��� ���� (�赿�� ��û)
        /// 2012-10-26-������ : PVI ����� QC Gate, PVI �������� �и� (�輺�� ��û)
        /// 2013-07-16-������ : ���� ��� ��ȸ ���� ����
        /// 
        /// </summary>
        public PRD010207()
        {
            InitializeComponent();
            cdvDate.Value = DateTime.Now;
            SortInit();
            GridColumnInit();            
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvLotType.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        #region �ʱ�ȭ �� ��ȿ�� �˻�
        /// <summary 1. ��ȿ�� �˻�>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// 2. ��� ����
        /// </summary>
        private void GridColumnInit()
        {
            string weekColumn = null;

            spdData.RPT_ColumnInit();
            GetDayArray();

            // �����Ͽ� ���� ���� ���� �Ǵ��� Ȯ��
            CheckedChangeWeek();

            // ���� ���濡 ���� ���� ǥ��
            if (weekStatus == false)
            {
                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {
                    weekColumn = "WW" + stThisWeek + " (" + cdvDate.Value.AddDays(-1).ToString("MM.dd") + " ����)";
                }
                else
                {
                    weekColumn = "WW" + stThisWeek + " (" + cdvDate.Value.ToString("MM.dd") + " ����)";
                }
            }
            else
            {
                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {
                    weekColumn = "WW" + stNextWeek + " (" + cdvDate.Value.AddDays(-1).ToString("MM.dd") + " ����)";
                }
                else
                {
                    weekColumn = "WW" + stNextWeek + " (" + cdvDate.Value.ToString("MM.dd") + " ����)";
                }
            }

            try
            {
                if (ckbKpcs.Checked == false)
                {
                    spdData.RPT_AddBasicColumn("Package", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Density", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Part NO", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Code", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("org", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn(weekColumn, 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                   
                    spdData.RPT_AddBasicColumn("actual", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("remain", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                                        
                    spdData.RPT_MerageHeaderColumnSpan(0, 6, 3);

                    spdData.RPT_AddBasicColumn(cdvDate.Value.ToString("MM.dd"), 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("remain", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 9, 3);

                    spdData.RPT_AddBasicColumn("Lack of work", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("WIP TTL", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    #region ���� ���ÿ� ���� ��� �κ�
                    if (cboGroup.Text == "WAFER")
                    {
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }

                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else if (cboGroup.Text == "SAW")
                    {
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }

                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else if (cboGroup.Text == "DA")
                    {                        
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                                                
                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else if (cboGroup.Text == "WB")
                    {
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }

                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else if (cboGroup.Text == "MOLD")
                    {
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }

                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else
                    {
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }

                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }

                    #endregion

                    spdData.RPT_MerageHeaderColumnSpan(0, 14, 17);

                    spdData.RPT_AddBasicColumn("OTD PLAN", 0, 31, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[0], 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[1], 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[2], 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[3], 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[4], 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[5], 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[6], 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 31, 7);

                    spdData.RPT_AddBasicColumn("Yesterday's performance by operation", 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("WF", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("SAW", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("D/A", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("W/B", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("T/F,SST", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("SHIP", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 38, 7);

                    if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                    {
                        spdData.RPT_AddBasicColumn("Performance by Operation" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 45, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("Performance by Operation" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 45, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    }
                                        
                    spdData.RPT_AddBasicColumn("WF", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("SAW", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("D/A", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("W/B", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("T/F,SST", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("SHIP", 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 45, 7);

                    spdData.RPT_AddBasicColumn("Current standard", 0, 52, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(cboGroup.Text + " ����", 1, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(cboGroup.Text + " remain", 1, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                    spdData.RPT_AddBasicColumn("Input remain", 1, 54, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Progress rate", 1, 55, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 52, 4);

                    spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 5, 2);                    
                    spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                    spdData.RPT_ColumnConfigFromTable(btnSort);                    
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Package", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Density", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Part NO", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Code", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("org", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("Work week", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("remain", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 6, 3);

                    spdData.RPT_AddBasicColumn("date", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("remain", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 9, 3);

                    spdData.RPT_AddBasicColumn("Lack of work", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("WIP TTL", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);

                    #region ���� ���ÿ� ���� ��� �κ�

                    if (cboGroup.Text == "WAFER")
                    {
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }

                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    }
                    else if (cboGroup.Text == "SAW")
                    {
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }

                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    }
                    else if (cboGroup.Text == "DA")
                    {
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }

                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    }
                    else if (cboGroup.Text == "WB")
                    {
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }

                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    }
                    else if (cboGroup.Text == "MOLD")
                    {
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }

                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    }
                    else if (cboGroup.Text == "FINISH")
                    {
                        if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("��� ��Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                        }

                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    }

                    #endregion

                    spdData.RPT_MerageHeaderColumnSpan(0, 14, 17);

                    spdData.RPT_AddBasicColumn("OTD PLAN", 0, 31, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[0], 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[1], 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[2], 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[3], 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[4], 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[5], 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry2[6], 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 31, 7);

                    spdData.RPT_AddBasicColumn("Yesterday's performance by operation", 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("WF", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("SAW", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("D/A", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("W/B", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("T/F,SST", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("SHIP", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 38, 7);

                    if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                    {
                        spdData.RPT_AddBasicColumn("Performance by Operation" + DateTime.Now.ToString("yy.MM.dd HH:mm") + ")", 0, 44, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("Performance by Operation" + cdvDate.Value.ToString("yy.MM.dd") + "22:00)", 0, 44, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    }
                                        
                    spdData.RPT_AddBasicColumn("WF", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("SAW", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("D/A", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("W/B", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("T/F,SST", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("SHIP", 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 45, 7);

                    spdData.RPT_AddBasicColumn("Current standard", 0, 52, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(cboGroup.Text + " ����", 1, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(cboGroup.Text + " remain", 1, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                    spdData.RPT_AddBasicColumn("Input remain", 1, 54, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Progress rate", 1, 55, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 52, 4);

                    spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                    spdData.RPT_ColumnConfigFromTable(btnSort);       
                }
                
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By ����
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "NVL(OTD.PKG, '-') AS PKG", "PKG", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "NVL(OTD.LEAD, '-') AS LEAD", "LEAD", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "NVL(OTD.DEN, '-') AS DEN", "DEN", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PART NO", "NVL(OTD.PART_NO, '-') AS PART_NO", "PART_NO", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CODE", "STD.CODE", "CODE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("ORG", "STD.ORG", "ORG", true);
        }
        #endregion


        #region SQL ���� Build
        /// <summary>
        /// 4. ���� ����
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1;
            string QueryCond2;
            string today;
            string yesterday;
            string tempQuery; // �� ���� ���ο� ���� ���� ����
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            today = cdvDate.SelectedValue();
            yesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            
            if (weekStatus == false)
            {
                tempQuery = "D0 + D1 + D2 + D3 + D4 + D5 + D6";
            }
            else
            {
                tempQuery = "D7 + D8 + D9 + D10 + D11 + D12 + D13";
            }

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
            strSqlString.Append("     , NVL(OTD.PLAN_WEEK, 0) AS PLAN_WEEK" + "\n");
            strSqlString.Append("     , NVL(SHP.SHP_WEEK, 0) AS SHP_WEEK" + "\n");
            strSqlString.Append("     , NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0)) AS REMAIN_WEEK" + "\n");

            // FINISH �϶��� �Ϻ� ��ȹ�� = �Ϻ� ��ȹ + ���� Remain Qty �� ����Ѵ�.
            if (cboGroup.Text == "FINISH")
            {
                strSqlString.Append("     , CASE WHEN NVL(OTD.DAY_PLAN, 0) - NVL(SHP.SHP_DAY_FINISH, 0) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE NVL(OTD.DAY_PLAN, 0) - NVL(SHP.SHP_DAY_FINISH, 0) " + "\n");
                strSqlString.Append("       END AS PLAN_DAY" + "\n");
                strSqlString.Append("     , NVL(SHP.SHP_DAY, 0) AS SHP_DAY" + "\n");
                strSqlString.Append("     , CASE WHEN NVL(OTD.DAY_PLAN, 0) - NVL(SHP.SHP_DAY_FINISH, 0) - NVL(SHP.SHP_DAY, 0) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE NVL(OTD.DAY_PLAN, 0) - NVL(SHP.SHP_DAY_FINISH, 0) - NVL(SHP.SHP_DAY, 0)" + "\n");
                strSqlString.Append("       END AS REMAIN_DAY" + "\n");
            }
            else // FINISH �� �ƴҶ����� �Ϻ� ��ȹ�� = �ְ� Remain / �ܿ��� �� ����Ѵ�.
            {
                strSqlString.Append("     , CASE WHEN NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0)) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0))) / " + stRemainDay + ", 0)" + "\n");
                strSqlString.Append("       END AS PLAN_DAY" + "\n");
                strSqlString.Append("     , NVL(SHP.SHP_DAY, 0) AS SHP_DAY" + "\n");
                strSqlString.Append("     , CASE WHEN ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0))) / " + stRemainDay + ", 0) - NVL(SHP.SHP_DAY, 0) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0))) / " + stRemainDay + ", 0) - NVL(SHP.SHP_DAY, 0)" + "\n");
                strSqlString.Append("       END AS REMAIN_DAY" + "\n");
            }

            if (cboGroup.Text == "WAFER")
            {
                //strSqlString.Append("     , CASE WHEN ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QTY, 0))) / " + stRemainDay + ", 0) - NVL(WIP.V4, 0) < 0 THEN 0" + "\n");
                //strSqlString.Append("            ELSE ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QTY, 0))) / " + stRemainDay + ", 0) - NVL(WIP.V4, 0)" + "\n");
                strSqlString.Append("     , (NVL(OTD.PLAN_WEEK, 0) - NVL(SHP.SHP_WEEK, 0) - NVL(SHP.SHP_DAY, 0)) - NVL(WIP.V4, 0) AS REMAIN_WIP" + "\n");
            }
            else if (cboGroup.Text == "SAW")
            {
                //strSqlString.Append("     , CASE WHEN ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QTY, 0))) / " + stRemainDay + ", 0) - NVL(SHP.SHP_DAY, 0) - NVL(WIP.V2, 0) < 0 THEN 0" + "\n");
                //strSqlString.Append("            ELSE ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QTY, 0))) / " + stRemainDay + ", 0) - NVL(SHP.SHP_DAY, 0) - NVL(WIP.V2, 0)" + "\n");
                strSqlString.Append("     , (NVL(OTD.PLAN_WEEK, 0) - NVL(SHP.SHP_WEEK, 0) - NVL(SHP.SHP_DAY, 0)) - NVL(WIP.V2, 0) AS REMAIN_WIP" + "\n");
            }
            else if (cboGroup.Text == "DA")
            {
                //strSqlString.Append("     , CASE WHEN ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QTY, 0))) / " + stRemainDay + ", 0) - NVL(SHP.SHP_DAY, 0) - NVL(WIP.V4, 0) < 0 THEN 0" + "\n");
                //strSqlString.Append("            ELSE ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QTY, 0))) / " + stRemainDay + ", 0) - NVL(SHP.SHP_DAY, 0) - NVL(WIP.V4, 0)" + "\n");
                strSqlString.Append("     , (NVL(OTD.PLAN_WEEK, 0) - NVL(SHP.SHP_WEEK, 0) - NVL(SHP.SHP_DAY, 0)) - NVL(WIP.V4, 0) AS REMAIN_WIP" + "\n");
            }
            else if (cboGroup.Text == "WB")
            {
                //strSqlString.Append("     , CASE WHEN ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QTY, 0))) / " + stRemainDay + ", 0) - NVL(SHP.SHP_DAY, 0) - NVL(WIP.V5, 0) < 0 THEN 0" + "\n");
                //strSqlString.Append("            ELSE ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QTY, 0))) / " + stRemainDay + ", 0) - NVL(SHP.SHP_DAY, 0) - NVL(WIP.V5, 0)" + "\n");
                strSqlString.Append("     , (NVL(OTD.PLAN_WEEK, 0) - NVL(SHP.SHP_WEEK, 0) - NVL(SHP.SHP_DAY, 0)) - NVL(WIP.V5, 0) AS REMAIN_WIP" + "\n");
            }
            else if (cboGroup.Text == "MOLD")
            {
                //strSqlString.Append("     , CASE WHEN ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QTY, 0))) / " + stRemainDay + ", 0) - NVL(SHP.SHP_DAY, 0) - NVL(WIP.WIP_MOLD, 0) < 0 THEN 0" + "\n");
                //strSqlString.Append("            ELSE ROUND((NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QTY, 0))) / " + stRemainDay + ", 0) - NVL(SHP.SHP_DAY, 0) - NVL(WIP.WIP_MOLD, 0)" + "\n");
                strSqlString.Append("     , (NVL(OTD.PLAN_WEEK, 0) - NVL(SHP.SHP_WEEK, 0) - NVL(SHP.SHP_DAY, 0)) - NVL(WIP.WIP_MOLD, 0) AS REMAIN_WIP" + "\n");
            }
            else
            {
                //strSqlString.Append("     , CASE WHEN (NVL(OTD.DAY_PLAN, 0) - NVL(SHP.SHP_DAY_FINISH, 0)) - NVL(SHP.SHP_DAY, 0) - NVL(WIP.WIP_FINISH, 0) < 0 THEN 0" + "\n");
                //strSqlString.Append("            ELSE (NVL(OTD.DAY_PLAN, 0) - NVL(SHP.SHP_DAY_FINISH, 0)) - NVL(SHP.SHP_DAY, 0) - NVL(WIP.WIP_FINISH, 0)" + "\n");
                strSqlString.Append("     , (NVL(OTD.PLAN_WEEK, 0) - NVL(SHP.SHP_WEEK, 0) - NVL(SHP.SHP_DAY, 0)) - NVL(WIP.WIP_FINISH, 0) AS REMAIN_WIP" + "\n");
            }

            //strSqlString.Append("       END AS REMAIN_WIP" + "\n");
            strSqlString.Append("     , NVL(WIP.TOTAL, 0)" + "\n");
            strSqlString.Append("     , NVL(WIP.V17, 0), NVL(WIP.V16, 0), NVL(WIP.V15, 0), NVL(WIP.V14, 0), NVL(WIP.V13, 0), NVL(WIP.V12, 0)" + "\n");
            strSqlString.Append("     , NVL(WIP.V11, 0), NVL(WIP.V10, 0), NVL(WIP.V9, 0), NVL(WIP.V8, 0), NVL(WIP.V7, 0)" + "\n");
            strSqlString.Append("     , NVL(WIP.V6, 0), NVL(WIP.V5, 0), NVL(WIP.V3, 0) + NVL(WIP.V4, 0), NVL(WIP.V2, 0), NVL(WIP.V1, 0), NVL(WIP.V0, 0)" + "\n");
            strSqlString.Append("     , NVL(OTD.D0, 0)" + "\n");
            strSqlString.Append("     , NVL(OTD.D1, 0)" + "\n");
            strSqlString.Append("     , NVL(OTD.D2, 0)" + "\n");
            strSqlString.Append("     , NVL(OTD.D3, 0)" + "\n");
            strSqlString.Append("     , NVL(OTD.D4, 0)" + "\n");
            strSqlString.Append("     , NVL(OTD.D5, 0)" + "\n");
            strSqlString.Append("     , NVL(OTD.D6, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.RCV_Y, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.SAW_Y, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.DA_Y, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.WB_Y, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.MD_Y, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.SIG_Y, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.SHP_Y, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.RCV_T, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.SAW_T, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.DA_T, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.WB_T, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.MD_T, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.SIG_T, 0)" + "\n");
            strSqlString.Append("     , NVL(SHP.SHP_T, 0)" + "\n");

            //2011-11-08-��ο� : ���󺹱�
            //2011-11-07-��ο� : ���� ��¥�϶��� NVL(SHP.SHP_DAY, 0) ���� �ȵǵ���
            strSqlString.Append("     , NVL(SHP.SHP_WEEK, 0) + NVL(SHP.SHP_DAY, 0) AS REAL_SHP " + "\n");
            /*
            if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("     , NVL(SHP.SHP_WEEK, 0) + NVL(SHP.SHP_DAY, 0) AS REAL_SHP " + "\n");
            }
            else
            {
                strSqlString.Append("     , NVL(SHP.SHP_WEEK, 0) AS REAL_SHP " + "\n");
            }
            */
            strSqlString.Append("     , (NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QTY, 0))) - NVL(SHP.SHP_T, 0) AS REAL_REMAIN " + "\n"); //SHP_DAY���� SHP_T�� ����
            //strSqlString.Append("     , ROUND(DECODE(NVL(OTD.PLAN_WEEK, 0), 0, 0, (NVL(SHP.SHP_WEEK, 0) + NVL(WIP_OLD.QY, 0) + NVL(SHP.SHP_DAY, 0)) / NVL(OTD.PLAN_WEEK, 0) * 100), 2) AS REAL_JINDO " + "\n");
            // 2011-08-30-��ο� : ���� REMAIN �߰�
            // 2011-11-07-��ο� : ���� REMAIN���� �����κ� ���� �������� ���� 
            // 2011-11-08-��ο� : ���󺹱�
            strSqlString.Append("     , NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_ALL, 0) + NVL(WIP_ETC,0)) AS R_MAIN " + "\n");
            /*
            if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("     , NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) + NVL(SHP.SHP_DAY, 0) + NVL(WIP_ETC,0)) AS R_MAIN " + "\n");
            }
            else
            {
                strSqlString.Append("     , NVL(OTD.PLAN_WEEK, 0) - (NVL(SHP.SHP_WEEK, 0) +  + NVL(WIP_ETC,0)) AS R_MAIN " + "\n");
            }
            */
            strSqlString.Append("     , ROUND(CASE WHEN NVL(OTD.PLAN_WEEK, 0) = 0 THEN 0 " + "\n");
            strSqlString.Append("                  ELSE (CASE WHEN (NVL(SHP.SHP_WEEK, 0) + NVL(SHP.SHP_DAY, 0)) / NVL(OTD.PLAN_WEEK, 0) * 100 > 100 THEN 100 " + "\n");
            strSqlString.Append("                             ELSE (NVL(SHP.SHP_WEEK, 0) + NVL(SHP.SHP_DAY, 0)) / NVL(OTD.PLAN_WEEK, 0) * 100 " + "\n");
            strSqlString.Append("                        END)" + "\n");
            strSqlString.Append("             END, 2) AS REAL_JINDO" + "\n");
            strSqlString.Append("  FROM (" + "\n");            
            strSqlString.Append("        SELECT DISTINCT SUBSTR(A.MAT_ID, -3) AS CODE" + "\n");
            strSqlString.Append("             , NVL(DECODE(A.MAT_GRP_3, 'MCP', '-', 'DDP', '-', 'QDP', '-', B.ATTR_VALUE), '-') AS ORG " + "\n");
            strSqlString.Append("          FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT * " + "\n");
            strSqlString.Append("                  FROM MATRNAMSTS " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND ATTR_TYPE = 'MAT_ETC' " + "\n");
            strSqlString.Append("                   AND ATTR_NAME = 'SEC_VERSION' " + "\n");
            strSqlString.Append("                   AND ATTR_KEY LIKE 'SEK%' " + "\n");
            strSqlString.Append("               ) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.ATTR_KEY(+) " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SEK%'" + "\n");
            strSqlString.Append("           AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("       ) STD" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT PKG, LEAD, CODE, ORG, WORKWEEK1" + "\n");
            strSqlString.Append("             , " + tempQuery + " AS PLAN_WEEK" + "\n");

            // ���� ���������� ���� ������ �ҽ��� ������.. ���� �ڵ尡 ���������� ���� ������ ���� �ϵ� �ڵ� ���ֱ� ����..
            //strSqlString.Append("             , CASE WHEN '" + cboGroup.Text + "' = 'WAFER' THEN (" + "\n");
            //strSqlString.Append("                                               CASE WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = 4 THEN D7 + D8 + D9 + D10 + D11 + D12 + D13" + "\n");
            //strSqlString.Append("                                                    WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = 5 THEN D7 + D8 + D9 + D10 + D11 + D12 + D13" + "\n");
            //strSqlString.Append("                                                    WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = 6 THEN D7 + D8 + D9 + D10 + D11 + D12 + D13" + "\n");
            //strSqlString.Append("                                                    ELSE D0 + D1 + D2 + D3 + D4 + D5 + D6" + "\n");
            //strSqlString.Append("                                                END" + "\n");
            //strSqlString.Append("                                              ) " + "\n");
            //strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'DA' THEN (" + "\n");
            //strSqlString.Append("                                               CASE WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = 5 THEN D7 + D8 + D9 + D10 + D11 + D12 + D13" + "\n");
            //strSqlString.Append("                                                    WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = 6 THEN D7 + D8 + D9 + D10 + D11 + D12 + D13" + "\n");
            //strSqlString.Append("                                                    ELSE D0 + D1 + D2 + D3 + D4 + D5 + D6" + "\n");
            //strSqlString.Append("                                                END" + "\n");
            //strSqlString.Append("                                          )" + "\n");
            //strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'WB' THEN (" + "\n");
            //strSqlString.Append("                                               CASE WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = 6 THEN D7 + D8 + D9 + D10 + D11 + D12 + D13                                                " + "\n");
            //strSqlString.Append("                                                    ELSE D0 + D1 + D2 + D3 + D4 + D5 + D6" + "\n");
            //strSqlString.Append("                                                END" + "\n");
            //strSqlString.Append("                                          ) " + "\n");
            //strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'MOLD' THEN (" + "\n");
            //strSqlString.Append("                                               CASE WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = 6 THEN D7 + D8 + D9 + D10 + D11 + D12 + D13" + "\n");
            //strSqlString.Append("                                                    ELSE D0 + D1 + D2 + D3 + D4 + D5 + D6" + "\n");
            //strSqlString.Append("                                                END" + "\n");
            //strSqlString.Append("                                            ) " + "\n");
            //strSqlString.Append("                    ELSE D0 + D1 + D2 + D3 + D4 + D5 + D6" + "\n");
            //strSqlString.Append("               END AS PLAN_WEEK" + "\n");

            strSqlString.Append("             , CASE WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = " + standardDay[0] + " THEN D0" + "\n");
            strSqlString.Append("                    WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = " + standardDay[1] + " THEN D0 + D1" + "\n");
            strSqlString.Append("                    WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = " + standardDay[2] + " THEN D0 + D1 + D2" + "\n");
            strSqlString.Append("                    WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = " + standardDay[3] + " THEN D0 + D1 + D2 + D3" + "\n");
            strSqlString.Append("                    WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = " + standardDay[4] + " THEN D0 + D1 + D2 + D3 + D4" + "\n");
            strSqlString.Append("                    WHEN TO_CHAR(TO_DATE('" + today + "','YYYYMMDD'), 'D') = " + standardDay[5] + " THEN D0 + D1 + D2 + D3 + D4 + D5" + "\n");            
            strSqlString.Append("                    ELSE D0 + D1 + D2 + D3 + D4 + D5 + D6" + "\n");
            strSqlString.Append("               END AS DAY_PLAN" + "\n");
            strSqlString.Append("             , CASE WHEN '" + cboGroup.Text + "' = 'WAFER' THEN D3" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'SAW' THEN D2" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'DA' THEN D2" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'WB' THEN D1" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'MOLD' THEN D1" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'FINISH' THEN D0" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END AS D0" + "\n");
            strSqlString.Append("             , CASE WHEN '" + cboGroup.Text + "' = 'WAFER' THEN D4" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'SAW' THEN D3" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'DA' THEN D3" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'WB' THEN D2" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'MOLD' THEN D2" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'FINISH' THEN D1" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END AS D1" + "\n");
            strSqlString.Append("             , CASE WHEN '" + cboGroup.Text + "' = 'WAFER' THEN D5" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'SAW' THEN D4" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'DA' THEN D4" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'WB' THEN D3" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'MOLD' THEN D3" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'FINISH' THEN D2" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END AS D2" + "\n");
            strSqlString.Append("             , CASE WHEN '" + cboGroup.Text + "' = 'WAFER' THEN D6" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'SAW' THEN D5" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'DA' THEN D5" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'WB' THEN D4" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'MOLD' THEN D4" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'FINISH' THEN D3" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END AS D3" + "\n");
            strSqlString.Append("             , CASE WHEN '" + cboGroup.Text + "' = 'WAFER' THEN D7" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'SAW' THEN D6" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'DA' THEN D6" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'WB' THEN D5" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'MOLD' THEN D5" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'FINISH' THEN D4" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END AS D4" + "\n");
            strSqlString.Append("             , CASE WHEN '" + cboGroup.Text + "' = 'SAW' THEN D7" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'DA' THEN D7" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'WB' THEN D6" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'MOLD' THEN D6" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'FINISH' THEN D5" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END AS D5" + "\n");
            strSqlString.Append("             , CASE WHEN '" + cboGroup.Text + "' = 'WB' THEN D7" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'MOLD' THEN D7" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'FINISH' THEN D6" + "\n");            
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END AS D6" + "\n");
            strSqlString.Append("          FROM CWIPPLNOTD@RPTTOMES" + "\n");
            strSqlString.Append("         WHERE WORKWEEK1 = (SELECT PLAN_YEAR||LPAD(PLAN_WEEK,2, '0') FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + cdvDate.SelectedValue() + "')" + "\n");
            strSqlString.Append("       ) OTD" + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT SUBSTR(D.MAT_ID, -3) AS CODE" + "\n");
            strSqlString.Append("             , NVL(DECODE(D.MAT_GRP_3, 'MCP', '-', 'DDP', '-', 'QDP', '-', E.ATTR_VALUE), '-') AS ORG " + "\n");

            // 2011-06-08-������ : ��ü ���� ��� Ship �������� ���� ��. (���¼� ��û)
            strSqlString.Append("             , SUM(C.SHP_WEEK) AS SHP_WEEK" + "\n");
            // 2011-08-30-��ο� : ������� ����(SHIP)
            strSqlString.Append("             , SUM(C.SHP_ALL) AS SHP_ALL" + "\n");
            
            //strSqlString.Append("             , SUM(CASE WHEN '" + cboGroup.Text + "' = 'WAFER' THEN A.RCV_WEEK" + "\n");
            //strSqlString.Append("                        WHEN '" + cboGroup.Text + "' = 'DA' THEN B.DA_WEEK" + "\n");
            //strSqlString.Append("                        WHEN '" + cboGroup.Text + "' = 'WB' THEN B.WB_WEEK" + "\n");
            //strSqlString.Append("                        WHEN '" + cboGroup.Text + "' = 'MOLD' THEN B.MD_WEEK" + "\n");
            //strSqlString.Append("                        WHEN '" + cboGroup.Text + "' = 'FINISH' THEN C.SHP_WEEK" + "\n");
            //strSqlString.Append("                        ELSE 0" + "\n");
            //strSqlString.Append("                   END) AS SHP_WEEK" + "\n");

            strSqlString.Append("             , SUM(CASE WHEN '" + cboGroup.Text + "' = 'WAFER' AND MAT_GRP_5 IN ('-','1st') THEN A.RCV_TODAY" + "\n");
            strSqlString.Append("                        WHEN '" + cboGroup.Text + "' = 'SAW' AND MAT_GRP_5 IN ('-','1st') THEN B.SAW_TODAY" + "\n");
            strSqlString.Append("                        WHEN '" + cboGroup.Text + "' = 'DA' AND MAT_GRP_5 IN ('-','Merge') THEN B.DA_TODAY" + "\n");
            strSqlString.Append("                        WHEN '" + cboGroup.Text + "' = 'WB' AND MAT_GRP_5 IN ('-','Merge') THEN B.WB_TODAY" + "\n");
            strSqlString.Append("                        WHEN '" + cboGroup.Text + "' = 'MOLD' THEN B.MD_TODAY" + "\n");
            strSqlString.Append("                        WHEN '" + cboGroup.Text + "' = 'FINISH' THEN C.SHP_TODAY" + "\n");
            strSqlString.Append("                        ELSE 0" + "\n");
            strSqlString.Append("                   END) AS SHP_DAY" + "\n");
            strSqlString.Append("             , SUM(DECODE(MAT_GRP_5, '-', A.RCV_YESTERDAY, '1st', A.RCV_YESTERDAY, 0)) AS RCV_Y" + "\n");
            strSqlString.Append("             , SUM(DECODE(MAT_GRP_5, '-', B.SAW_YESTERDAY, '1st', B.SAW_YESTERDAY, 0)) AS SAW_Y" + "\n");
            strSqlString.Append("             , SUM(DECODE(MAT_GRP_5, '-', B.DA_YESTERDAY, 'Merge', B.DA_YESTERDAY, 0)) AS DA_Y" + "\n");
            strSqlString.Append("             , SUM(DECODE(MAT_GRP_5, '-', B.WB_YESTERDAY, 'Merge', B.WB_YESTERDAY, 0)) AS WB_Y" + "\n");
            strSqlString.Append("             , SUM(B.MD_YESTERDAY) AS MD_Y" + "\n");
            strSqlString.Append("             , SUM(B.SIG_YESTERDAY) AS SIG_Y" + "\n");
            strSqlString.Append("             , SUM(C.SHP_YESTERDAY) AS SHP_Y" + "\n");
            strSqlString.Append("             , SUM(DECODE(MAT_GRP_5, '-', A.RCV_TODAY, '1st', A.RCV_TODAY, 0)) AS RCV_T" + "\n");
            strSqlString.Append("             , SUM(DECODE(MAT_GRP_5, '-', B.SAW_TODAY, '1st', B.SAW_TODAY, 0)) AS SAW_T" + "\n");
            strSqlString.Append("             , SUM(DECODE(MAT_GRP_5, '-', B.DA_TODAY, 'Merge', B.DA_TODAY, 0)) AS DA_T" + "\n");
            strSqlString.Append("             , SUM(DECODE(MAT_GRP_5, '-', B.WB_TODAY, 'Merge', B.WB_TODAY, 0)) AS WB_T" + "\n");
            strSqlString.Append("             , SUM(B.MD_TODAY) AS MD_T" + "\n");
            strSqlString.Append("             , SUM(B.SIG_TODAY) AS SIG_T" + "\n");
            strSqlString.Append("             , SUM(C.SHP_TODAY) AS SHP_T" + "\n");
            strSqlString.Append("             , SUM(C.SHP_DAY_FINISH) AS SHP_DAY_FINISH" + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");

            // ���� ���� ���� Ȯ��.
            // ���� ���� �ȵ� : ��ȸ�� ���� ���� ��
            // ���� ���� �� : �� �� ������ ���� ũ�ų� ���� ��ȸ�� ���� ���� ��
            if (weekStatus == false)
            {
                // ���� ��ȸ�ÿ��� ���� ������ ���� ���� ����.. ���� ��ȸ�ÿ��� ���� ���� ����
                // �� ���� �������� ��� ���� ������ �ȳ����� ������ �Ⱓ�� �������� ���������� �� ���� �������� �������� ���� ������.
                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                     , SUM(CASE WHEN '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE < '" + today + "' THEN NVL(RCV_QTY_1,0)" + "\n");
                }
                else
                {
                    strSqlString.Append("                     , SUM(CASE WHEN '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE <= '" + today + "' THEN NVL(RCV_QTY_1,0)" + "\n");
                }
            }
            else
            {
                // ���� ��ȸ�ÿ��� ���� ������ ���� ���� ����.. ���� ��ȸ�ÿ��� ���� ���� ����
                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                     , SUM(CASE WHEN '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE < '" + today + "' THEN NVL(RCV_QTY_1,0)" + "\n");
                }
                else
                {
                    strSqlString.Append("                     , SUM(CASE WHEN '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE <= '" + today + "' THEN NVL(RCV_QTY_1,0)" + "\n");
                }
            }

            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS RCV_WEEK                     " + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE,'" + yesterday + "', NVL(RCV_QTY_1,0), 0)) AS RCV_YESTERDAY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE,'" + today + "', NVL(RCV_QTY_1,0), 0)) AS RCV_TODAY" + "\n");
            strSqlString.Append("                  FROM VSUMWIPRCV " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + stLastWeekEndDay + "' AND '" + stNextWeekEndDay + "'" + "\n");
            strSqlString.Append("                   AND MAT_ID LIKE 'SEK%'" + "\n");
            strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) A " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");

            // ���� ���� ���� Ȯ��.
            // ���� ���� �ȵ� : ��ȸ�� ���� ���� ��
            // ���� ���� �� : �� �� ������ ���� ũ�ų� ���� ��ȸ�� ���� ���� ��
            if (weekStatus == false)
            {
                // ���� ��ȸ�ÿ��� ���� ������ ���� ���� ����.. ���� ��ȸ�ÿ��� ���� ���� ����
                // �� ���� �������� ��� ���� ������ �ȳ����� ������ �Ⱓ�� �������� ���������� �� ���� �������� �������� ���� ������.
                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'SAW' AND '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE < '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS SAW_WEEK" + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'D/A' AND '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE < '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS DA_WEEK" + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'W/B' AND '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE < '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS WB_WEEK           " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'MOLD' AND '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE < '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS MD_WEEK" + "\n");
                }
                else
                {
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'SAW' AND '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE <= '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS SAW_WEEK" + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'D/A' AND '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE <= '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS DA_WEEK" + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'W/B' AND '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE <= '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS WB_WEEK           " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'MOLD' AND '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE <= '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS MD_WEEK" + "\n");
                }
            }
            else
            {
                // ���� ��ȸ�ÿ��� ���� ������ ���� ���� ����.. ���� ��ȸ�ÿ��� ���� ���� ����
                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'SAW' AND '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE < '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS SAW_WEEK" + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'D/A' AND '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE < '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS DA_WEEK" + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'W/B' AND '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE < '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS WB_WEEK           " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'MOLD' AND '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE < '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS MD_WEEK" + "\n");                    
                }
                else
                {
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'SAW' AND '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE <= '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS SAW_WEEK" + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'D/A' AND '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE <= '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS DA_WEEK" + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'W/B' AND '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE <= '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS WB_WEEK           " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_7 = 'MOLD' AND '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE <= '" + today + "' THEN QTY" + "\n");
                    strSqlString.Append("                                ELSE 0" + "\n");
                    strSqlString.Append("                           END) AS MD_WEEK" + "\n");
                }
            }

            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'SAW', DECODE(WORK_DATE,'" + yesterday + "', QTY,0), 0)) AS SAW_YESTERDAY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'SAW', DECODE(WORK_DATE,'" + today + "', QTY,0), 0)) AS SAW_TODAY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'D/A', DECODE(WORK_DATE,'" + yesterday + "', QTY,0), 0)) AS DA_YESTERDAY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'D/A', DECODE(WORK_DATE,'" + today + "', QTY,0), 0)) AS DA_TODAY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'W/B', DECODE(WORK_DATE,'" + yesterday + "', QTY,0), 0)) AS WB_YESTERDAY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'W/B', DECODE(WORK_DATE,'" + today + "', QTY,0), 0)) AS WB_TODAY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'MOLD', DECODE(WORK_DATE,'" + yesterday + "', QTY,0), 0)) AS MD_YESTERDAY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'MOLD', DECODE(WORK_DATE,'" + today + "', QTY,0), 0)) AS MD_TODAY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'SIG', DECODE(WORK_DATE,'" + yesterday + "', QTY,0), 0)) AS SIG_YESTERDAY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'SIG', DECODE(WORK_DATE,'" + today + "', QTY,0), 0)) AS SIG_TODAY" + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT A.MAT_ID,A.WORK_DATE,B.OPER,B.OPER_GRP_7 " + "\n");
            strSqlString.Append("                              , SUM(A.S1_END_QTY_1 + A.S2_END_QTY_1 + A.S3_END_QTY_1 + A.S1_END_RWK_QTY_1 + A.S2_END_RWK_QTY_1 + A.S3_END_RWK_QTY_1 ) AS QTY " + "\n");
            strSqlString.Append("                           FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("                              , MWIPOPRDEF B " + "\n");
            strSqlString.Append("                          WHERE 1=1 " + "\n");
            strSqlString.Append("                            AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                            AND A.OPER = B.OPER " + "\n");
            strSqlString.Append("                            AND A.CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                            AND B.OPER_GRP_7 IN ('SAW','D/A','W/B','MOLD','SIG') " + "\n");
            strSqlString.Append("                            AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                            AND A.MAT_ID LIKE 'SEK%'" + "\n");
            strSqlString.Append("                            AND A.CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                            AND A.WORK_DATE BETWEEN '" + stLastWeekEndDay + "' AND '" + stNextWeekEndDay + "'" + "\n");
            strSqlString.Append("                            AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                          GROUP BY A.MAT_ID, A.WORK_DATE,B.OPER,B.OPER_GRP_7 " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) B" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                 SELECT MAT_ID" + "\n");

            // ���� ���� ���� Ȯ��.
            // ���� ���� �ȵ� : ��ȸ�� ���� ���� ��
            // ���� ���� �� : �� �� ������ ���� ũ�ų� ���� ��ȸ�� ���� ���� ��
            if (weekStatus == false)
            {
                // ���� ��ȸ�ÿ��� ���� ������ ���� ���� ����.. ���� ��ȸ�ÿ��� ���� ���� ����
                // �� ���� �������� ��� ���� ������ �ȳ����� ������ �Ⱓ�� �������� ���������� �� ���� �������� �������� ���� ������.
                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                      , SUM(CASE WHEN '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE < '" + today + "' THEN (S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1)" + "\n");
                }
                else
                {
                    strSqlString.Append("                      , SUM(CASE WHEN '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE <= '" + today + "' THEN (S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1)" + "\n");
                }
            }
            else
            {
                // ���� ��ȸ�ÿ��� ���� ������ ���� ���� ����.. ���� ��ȸ�ÿ��� ���� ���� ����
                if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                      , SUM(CASE WHEN '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE < '" + today + "' THEN (S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1)" + "\n");
                }
                else
                {
                    strSqlString.Append("                      , SUM(CASE WHEN '" + stNextWeekStartDay + "' <= WORK_DATE AND WORK_DATE <= '" + today + "' THEN (S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1)" + "\n");
                }
            }

            strSqlString.Append("                                 ELSE 0" + "\n");
            strSqlString.Append("                            END) AS SHP_WEEK" + "\n");
            //2011-08-30-��ο� ������� ����(SHIP)
            //2011-11-05-������ : ���� ��ȸ �ȵǾ� �ϴ� �ּ� ó��...������ Ȯ�� �ʿ�.
            
            //if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
            //{
                strSqlString.Append("                      , SUM(CASE WHEN '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE <= '" + today + "' THEN (S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1)" + "\n");
                strSqlString.Append("                                 ELSE 0" + "\n");
                strSqlString.Append("                            END) AS SHP_ALL" + "\n");
            //}
            

            // FINISH ���ý� �ֺ� ������ ���� ���ϱ����� ���� �� ��������..
            strSqlString.Append("                      , SUM(CASE WHEN '" + stLastWeekEndDay + "' < WORK_DATE AND WORK_DATE < '" + today + "' THEN (S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1)" + "\n");
            strSqlString.Append("                                 ELSE 0" + "\n");
            strSqlString.Append("                            END) AS SHP_DAY_FINISH" + "\n");

            strSqlString.Append("                      , SUM(DECODE(WORK_DATE, '" + yesterday + "', (S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1), 0)) AS SHP_YESTERDAY " + "\n");
            strSqlString.Append("                      , SUM(DECODE(WORK_DATE, '" + today + "', (S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1), 0)) AS SHP_TODAY" + "\n");
            strSqlString.Append("                   FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                  WHERE 1=1 " + "\n");
            strSqlString.Append("                    AND FACTORY = 'CUSTOMER' " + "\n");
            strSqlString.Append("                    AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                    AND CM_KEY_2 = 'PROD' " + "\n");            
            strSqlString.Append("                    AND MAT_ID LIKE 'SEK%'" + "\n");
            strSqlString.Append("                    AND CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                    AND WORK_DATE BETWEEN '" + stLastWeekEndDay + "' AND '" + stNextWeekEndDay + "'" + "\n");
            strSqlString.Append("                    AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                  GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) C" + "\n");
            strSqlString.Append("             , MWIPMATDEF D" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT * " + "\n");
            strSqlString.Append("                  FROM MATRNAMSTS" + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND ATTR_TYPE = 'MAT_ETC' " + "\n");
            strSqlString.Append("                   AND ATTR_NAME = 'SEC_VERSION' " + "\n");
            strSqlString.Append("                   AND ATTR_KEY LIKE 'SEK%' " + "\n");
            strSqlString.Append("               ) E" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND D.MAT_ID = A.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND D.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND D.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND D.MAT_ID = E.ATTR_KEY(+)" + "\n");
            strSqlString.Append("           AND D.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND D.MAT_ID LIKE 'SEK%'" + "\n");
            strSqlString.Append("           AND D.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("           AND D.MAT_GRP_5 <> 'Middle'" + "\n");
            strSqlString.Append("         GROUP BY SUBSTR(D.MAT_ID, -3), NVL(DECODE(D.MAT_GRP_3, 'MCP', '-', 'DDP', '-', 'QDP', '-', E.ATTR_VALUE), '-')" + "\n");
            strSqlString.Append("       ) SHP" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT CODE, SEC_VERSION AS ORG" + "\n");
            strSqlString.Append("             , CASE WHEN '" + cboGroup.Text + "' = 'SAW' THEN V0 + V1 + V2" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'DA' THEN V0 + V1 + V2 + V3 + V4" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'WB' THEN V0 + V1 + V2 + V3 + V4 + V5" + "\n");
            strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'MOLD' THEN V0 + V1 + V2 + V3 + V4 + V5 + V6 + V7 + V8 " + "\n");
            strSqlString.Append("                    ELSE TOTAL" + "\n");
            strSqlString.Append("               END AS TOTAL" + "\n");
            strSqlString.Append("             , V6 + V7 AS WIP_MOLD" + "\n");
            strSqlString.Append("             , V9 + V10 + V11 + V12 + V13 + V14 + V15 + V16 + V17 AS WIP_FINISH" + "\n");
            strSqlString.Append("             , V0, V1, V2, V3, V4, V5" + "\n");
            strSqlString.Append("             , V6, V7, V8, V9, V10, V11" + "\n");
            strSqlString.Append("             , V12, V13, V14, V15, V16, V17" + "\n");
            // 2011-08-30-��ο� : �������(��̳��̼�~���ϴ��)
            strSqlString.Append("             , (V1 + V2 + V3 + V4 + V5 + V6 + V7 + V8 + V9 + V10 + V11 + V12 + V13 + V14 + V15 + V16 + V17) AS WIP_ETC" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT SUBSTR(MAT_ID, -3) AS CODE " + "\n");
            strSqlString.Append("                     , SEC_VERSION" + "\n");
            strSqlString.Append("                     , SUM(NVL(QTY, 0)) TOTAL" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK2A', QTY, 0)) V0  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'B/G', QTY, 0)) V1  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SAW', QTY, 0)) V2  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'S/P', QTY, 0)) V3  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'D/A', QTY, 0)) V4  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'W/B', QTY, 0)) V5  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'GATE', QTY, 0)) V6  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'MOLD', QTY, 0)) V7  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'CURE', QTY, 0)) V8  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'M/K', QTY, 0)) V9  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TRIM', QTY, 0)) V10  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TIN', QTY, 0)) V11 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'S/B/A', QTY, 0)) V12  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SIG', QTY, 0)) V13  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'AVI', QTY, 0)) V14  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A2050', QTY, 0)) V15  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A2100', QTY, 0)) V16  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK3A', QTY, 0)) V17  " + "\n");
            strSqlString.Append("                  FROM (   " + "\n");
            strSqlString.Append("                         SELECT A.MAT_ID, A.OPER, B.OPER_GRP_1, NVL(DECODE(C.MAT_GRP_3, 'MCP', '-', 'DDP', '-', 'QDP', '-', D.ATTR_VALUE), '-') AS SEC_VERSION " + "\n");
            strSqlString.Append("                              , QTY_1 AS QTY   " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                           FROM RWIPLOTSTS A  " + "\n");
            }
            else
            {
                strSqlString.Append("                           FROM RWIPLOTSTS_BOH A  " + "\n");
            }

            strSqlString.Append("                              , MWIPOPRDEF B  " + "\n");
            strSqlString.Append("                              , ( " + "\n");
            strSqlString.Append("                                 SELECT * " + "\n");
            strSqlString.Append("                                   FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                    AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                    AND MAT_ID LIKE 'SEK%' " + "\n");
            strSqlString.Append("                                    AND (MAT_GRP_5 IN ('1st','Merge', '-') OR MAT_GRP_5 LIKE 'Middle%') " + "\n");
            strSqlString.Append("                                ) C " + "\n");
            strSqlString.Append("                              , ( " + "\n");
            strSqlString.Append("                                 SELECT * " + "\n");
            strSqlString.Append("                                   FROM MATRNAMSTS " + "\n");
            strSqlString.Append("                                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                    AND ATTR_TYPE = 'MAT_ETC' " + "\n");
            strSqlString.Append("                                    AND ATTR_NAME = 'SEC_VERSION'  " + "\n");
            strSqlString.Append("                                    AND ATTR_KEY LIKE 'SEK%'" + "\n");
            strSqlString.Append("                                ) D " + "\n");
            strSqlString.Append("                          WHERE 1 = 1" + "\n");
            strSqlString.Append("                            AND A.FACTORY = B.FACTORY(+)  " + "\n");
            strSqlString.Append("                            AND A.FACTORY = C.FACTORY  " + "\n");
            strSqlString.Append("                            AND A.OPER = B.OPER(+)" + "\n");
            strSqlString.Append("                            AND A.MAT_ID = C.MAT_ID " + "\n");
            strSqlString.Append("                            AND A.MAT_ID = D.ATTR_KEY(+) " + "\n");
            strSqlString.Append("                            AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                            AND A.MAT_VER = 1  " + "\n");
            strSqlString.Append("                            AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                            AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                            AND A.MAT_ID LIKE 'SEK%'  " + "\n");
            strSqlString.Append("                            AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                            AND A.OPER LIKE 'A%'" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
            {
                strSqlString.Append("                            AND A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");
            }

            strSqlString.Append("                       )  " + "\n");
            strSqlString.Append("                 GROUP BY SUBSTR(MAT_ID, -3), SEC_VERSION " + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("       ) WIP " + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT SUBSTR(A.MAT_ID, -3) AS CODE " + "\n");
            strSqlString.Append("             , NVL(DECODE(B.MAT_GRP_3, 'MCP', '-', 'DDP', '-', 'QDP', '-', C.ATTR_VALUE), '-') AS ORG " + "\n");
            strSqlString.Append("             , SUM(A.QTY_1) QTY  " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("          FROM RWIPLOTSTS A  " + "\n");
            }
            else
            {
                strSqlString.Append("          FROM RWIPLOTSTS_BOH A  " + "\n");
            }
                        
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT * " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_ID LIKE 'SEK%' " + "\n");
            strSqlString.Append("                   AND (MAT_GRP_5 IN ('1st','Merge', '-') OR MAT_GRP_5 LIKE 'Middle%') " + "\n");
            strSqlString.Append("               ) B " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT * " + "\n");
            strSqlString.Append("                  FROM MATRNAMSTS " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND ATTR_TYPE = 'MAT_ETC' " + "\n");
            strSqlString.Append("                   AND ATTR_NAME = 'SEC_VERSION'  " + "\n");
            strSqlString.Append("                   AND ATTR_KEY LIKE 'SEK%'" + "\n");
            strSqlString.Append("               ) C " + "\n");

            // ���� ������ ������ ���� �������� -1���� ����� �������� (������ ����������)
            // ������ ����Ǹ� ���� �������� -���� ����� �����´� (���� ����������)
            //if (weekStatus == false)
            //{
            //    strSqlString.Append("         WHERE CUTOFF_DT = '" + stLastWeekEndDay + "22'" + "\n");
            //}
            //else
            //{
            //    strSqlString.Append("         WHERE CUTOFF_DT = '" + dayArry[6] + "22'" + "\n");
            //}
                        
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("           AND A.MAT_ID = C.ATTR_KEY(+)" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
            {
                strSqlString.Append("           AND A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");
            }

            // 2011-09-06-����� : ���� ����� �ƴ� ���� ����� ������ ������� ���� �׷��� �ϴ� �ּ�ó��
            // 2011-06-08-������ : ������ ���� ����� ������ ������� ���� (���¼� ��û)
            //strSqlString.Append("           AND A.CUTOFF_DT = '" + yesterday + "22'" + "\n");

            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SEK%'" + "\n");
            strSqlString.Append("           AND A.LOT_DEL_FLAG = ' '   " + "\n");
            strSqlString.Append("           AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("           AND A.OPER >= (CASE WHEN '" + cboGroup.Text + "' = 'WAFER' THEN 'A0000'" + "\n");
            strSqlString.Append("                               WHEN '" + cboGroup.Text + "' = 'SAW' THEN 'A0310'" + "\n");
            strSqlString.Append("                               WHEN '" + cboGroup.Text + "' = 'DA' THEN 'A0500'" + "\n");
            strSqlString.Append("                               WHEN '" + cboGroup.Text + "' = 'WB' THEN 'A0800'" + "\n");
            strSqlString.Append("                               WHEN '" + cboGroup.Text + "' = 'MOLD' THEN 'A1100'" + "\n");

            // AZ010���� ũ�� ����� ����.. �� ����Ÿ 0���� ������ �ϱ� ����
            strSqlString.Append("                               ELSE 'AZ020' " + "\n");
            strSqlString.Append("                           END)" + "\n");
            strSqlString.Append("         GROUP BY SUBSTR(A.MAT_ID, -3), NVL(DECODE(B.MAT_GRP_3, 'MCP', '-', 'DDP', '-', 'QDP', '-', C.ATTR_VALUE), '-') " + "\n");
            strSqlString.Append("       ) WIP_OLD" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND STD.CODE = OTD.CODE(+)" + "\n");
            strSqlString.Append("   AND STD.CODE = SHP.CODE(+)" + "\n");
            strSqlString.Append("   AND STD.CODE = WIP.CODE(+)" + "\n");
            strSqlString.Append("   AND STD.CODE = WIP_OLD.CODE(+)" + "\n");
            strSqlString.Append("   AND STD.ORG = OTD.ORG(+)" + "\n");
            strSqlString.Append("   AND STD.ORG = SHP.ORG(+)" + "\n");
            strSqlString.Append("   AND STD.ORG = WIP.ORG(+)" + "\n");
            strSqlString.Append("   AND STD.ORG = WIP_OLD.ORG(+)" + "\n");

            // ��ȹ�� 0���� ū �� �鸸 ������...
            strSqlString.Append("   AND NVL(OTD.PLAN_WEEK, 0) + NVL(SHP.SHP_WEEK, 0) + NVL(OTD.D0, 0) + NVL(OTD.D1, 0) + NVL(OTD.D2, 0) + NVL(OTD.D3, 0) + NVL(OTD.D4, 0) + NVL(OTD.D5, 0) + NVL(OTD.D6, 0) + NVL(WIP.TOTAL, 0) > 0" + "\n");

            strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond2);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
        
            return strSqlString.ToString();
        }

        private void GetDayArray()
        {
            DataTable dt = null;
            StringBuilder strSqlString = new StringBuilder();            
            DateTime temp;

            // ������ ���� (�� ~ ��)
            standardDay[0] = 7;
            standardDay[1] = 1;
            standardDay[2] = 2;
            standardDay[3] = 3;
            standardDay[4] = 4;
            standardDay[5] = 5;
            standardDay[6] = 6;

            // ���� �ڵ� ���� ������ �ӽ� ���(ȭ ~ ��)
            //standardDay[0] = 3;
            //standardDay[1] = 4;
            //standardDay[2] = 5;
            //standardDay[3] = 6;
            //standardDay[4] = 7;
            //standardDay[5] = 1;
            //standardDay[6] = 2;  
 
            // ��ȸ�� ���ؿ� ���� ���� �������� ����..
            string stNextDay = cdvDate.Value.AddDays(7).ToString("yyyyMMdd");

            // �ܿ��� ���ϴ� ���� (�� ~ ��)
            //          D0(��)  D1(��)  D2(��)  D3(ȭ)  D4(��)  D5(��)  D6(��)
            // WAFER      4       3       2       1       7       6       5
            // DA         5       4       3       2       1       7       6
            // WB         6       5       4       3       2       1       7
            // MOLD       6       5       4       3       2       1       7
            // FINISH     7       6       5       4       3       2       1     
            strSqlString.Append("SELECT SYS_DATE, PLAN_WEEK" + "\n");
            strSqlString.Append("     , CASE WHEN '" + cboGroup.Text + "' = 'WAFER' THEN (" + "\n");
            strSqlString.Append("                                                         CASE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[0] + " THEN 4 " + "\n");
            strSqlString.Append("                                                              WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[1] + " THEN 3 " + "\n");
            strSqlString.Append("                                                              WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[2] + " THEN 2 " + "\n");
            strSqlString.Append("                                                              WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[3] + " THEN 1 " + "\n");
            strSqlString.Append("                                                              WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[4] + " THEN 7 " + "\n");
            strSqlString.Append("                                                              WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[5] + " THEN 6 " + "\n");
            strSqlString.Append("                                                              ELSE 5 " + "\n");
            strSqlString.Append("                                                         END" + "\n");
            strSqlString.Append("                                                        )" + "\n");

            // 2011-06-09-������ : SAW �߰� - DA ���ذ� �����ϰ� �� (���¼� ��û)
            strSqlString.Append("            WHEN '" + cboGroup.Text + "' = 'SAW' THEN (" + "\n");
            strSqlString.Append("                                                       CASE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[0] + " THEN 5 " + "\n");
            strSqlString.Append("                                                            WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[1] + " THEN 4 " + "\n");
            strSqlString.Append("                                                            WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[2] + " THEN 3 " + "\n");
            strSqlString.Append("                                                            WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[3] + " THEN 2 " + "\n");
            strSqlString.Append("                                                            WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[4] + " THEN 1 " + "\n");
            strSqlString.Append("                                                            WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[5] + " THEN 7 " + "\n");
            strSqlString.Append("                                                            ELSE 6 " + "\n");
            strSqlString.Append("                                                       END" + "\n");
            strSqlString.Append("                                                      )" + "\n");

            strSqlString.Append("            WHEN '" + cboGroup.Text + "' = 'DA' THEN (" + "\n");
            strSqlString.Append("                                                      CASE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[0] + " THEN 5 " + "\n");
            strSqlString.Append("                                                           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[1] + " THEN 4 " + "\n");
            strSqlString.Append("                                                           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[2] + " THEN 3 " + "\n");
            strSqlString.Append("                                                           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[3] + " THEN 2 " + "\n");
            strSqlString.Append("                                                           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[4] + " THEN 1 " + "\n");
            strSqlString.Append("                                                           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[5] + " THEN 7 " + "\n");
            strSqlString.Append("                                                           ELSE 6 " + "\n");
            strSqlString.Append("                                                      END" + "\n");
            strSqlString.Append("                                                     )" + "\n");
            strSqlString.Append("            WHEN '" + cboGroup.Text + "' = 'FINISH' THEN (" + "\n");
            strSqlString.Append("                                                          CASE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[0] + " THEN 7 " + "\n");
            strSqlString.Append("                                                               WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[1] + " THEN 6 " + "\n");
            strSqlString.Append("                                                               WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[2] + " THEN 5 " + "\n");
            strSqlString.Append("                                                               WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[3] + " THEN 4 " + "\n");
            strSqlString.Append("                                                               WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[4] + " THEN 3 " + "\n");
            strSqlString.Append("                                                               WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[5] + " THEN 2 " + "\n");
            strSqlString.Append("                                                               ELSE 1 " + "\n");
            strSqlString.Append("                                                          END" + "\n");
            strSqlString.Append("                                                         )" + "\n");
            strSqlString.Append("           ELSE (" + "\n");
            strSqlString.Append("                 CASE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[0] + " THEN 6 " + "\n");
            strSqlString.Append("                      WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[1] + " THEN 5 " + "\n");
            strSqlString.Append("                      WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[2] + " THEN 4 " + "\n");
            strSqlString.Append("                      WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[3] + " THEN 3 " + "\n");
            strSqlString.Append("                      WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[4] + " THEN 2 " + "\n");
            strSqlString.Append("                      WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD'), 'D') = " + standardDay[5] + " THEN 1 " + "\n");
            strSqlString.Append("                      ELSE 7 " + "\n");
            strSqlString.Append("                 END" + "\n");
            strSqlString.Append("                )" + "\n");
            strSqlString.Append("       END AS REMAIN_DAY " + "\n");
            strSqlString.Append("  FROM MWIPCALDEF " + "\n");
            strSqlString.Append(" WHERE CALENDAR_ID = 'OTD' " + "\n");
            strSqlString.Append("   AND (PLAN_YEAR,PLAN_WEEK) IN (SELECT PLAN_YEAR,PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE IN ('" + cdvDate.SelectedValue() + "', '" + stNextDay + "'))" + "\n");
            strSqlString.Append(" ORDER BY SYS_DATE " + "\n");

            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

            dayArry[0] = dt.Rows[0][0].ToString(); // �� �� ��������
            dayArry[1] = dt.Rows[1][0].ToString();
            dayArry[2] = dt.Rows[2][0].ToString();
            dayArry[3] = dt.Rows[3][0].ToString();
            dayArry[4] = dt.Rows[4][0].ToString();
            dayArry[5] = dt.Rows[5][0].ToString();
            dayArry[6] = dt.Rows[6][0].ToString(); // �� �� ����������

            dayArry2[0] = dt.Rows[0][0].ToString().Substring(4, 2) + "." + dt.Rows[0][0].ToString().Substring(6, 2);
            dayArry2[1] = dt.Rows[1][0].ToString().Substring(4, 2) + "." + dt.Rows[1][0].ToString().Substring(6, 2);
            dayArry2[2] = dt.Rows[2][0].ToString().Substring(4, 2) + "." + dt.Rows[2][0].ToString().Substring(6, 2);
            dayArry2[3] = dt.Rows[3][0].ToString().Substring(4, 2) + "." + dt.Rows[3][0].ToString().Substring(6, 2);
            dayArry2[4] = dt.Rows[4][0].ToString().Substring(4, 2) + "." + dt.Rows[4][0].ToString().Substring(6, 2);
            dayArry2[5] = dt.Rows[5][0].ToString().Substring(4, 2) + "." + dt.Rows[5][0].ToString().Substring(6, 2);
            dayArry2[6] = dt.Rows[6][0].ToString().Substring(4, 2) + "." + dt.Rows[6][0].ToString().Substring(6, 2);

            temp = Convert.ToDateTime(dt.Rows[0][0].ToString().Substring(0, 4) + "-" + dt.Rows[0][0].ToString().Substring(4, 2) + "-" + dt.Rows[0][0].ToString().Substring(6, 2));

            stLastWeekEndDay = temp.AddDays(-1).ToString("yyyyMMdd"); // �� �� ���� �������� ���� ���� ��������
            stNextWeekStartDay = dt.Rows[7][0].ToString();// �� �� ���� ����
            stNextWeekEndDay = dt.Rows[13][0].ToString(); // �� �� ���������� ��������
            stThisWeek = dt.Rows[0][1].ToString(); // �������� ���� ��������
            stNextWeek = dt.Rows[13][1].ToString(); // �������� ���� ���� ��������
            stRemainDay = dt.Rows[0][2].ToString(); // �ܿ��� ��������
        }

        // ���� ���� ���� Ȯ��
        private void CheckedChangeWeek()
        {
            weekStatus = false;

            // �Ｚ���� �ڵ� ���� ������ �ӽ÷� ����� (ȭ~��)
            //if (cdvDate.Value.DayOfWeek == DayOfWeek.Saturday)
            //{
            //    // ������� WAFER�� ���� �����.
            //    if (cboGroup.Text == "WAFER")
            //        weekStatus = true;                    
            //}
            //else if (cdvDate.Value.DayOfWeek == DayOfWeek.Sunday)
            //{
            //    // �Ͽ����� WAFER, DA �� ���� �����.
            //    if (cboGroup.Text == "WAFER" || cboGroup.Text == "DA")
            //        weekStatus = true;
            //}
            //else if (cdvDate.Value.DayOfWeek == DayOfWeek.Monday)
            //{
            //    // �������� WAFER, DA, WB, MOLD �� ���� �����.
            //    if (cboGroup.Text == "WAFER" || cboGroup.Text == "DA" || cboGroup.Text == "WB" || cboGroup.Text == "MOLD")
            //        weekStatus = true;
            //}
            //else
            //{
            //    weekStatus = false;
            //}

            // 11�� 1�� �Ｚ ���� �ڵ� ���� �Ǹ� ��� ���� (��~��)
            if (cdvDate.Value.DayOfWeek == DayOfWeek.Wednesday)
            {
                // �������� WAFER�� ���� �����.
                if (cboGroup.Text == "WAFER")
                    weekStatus = true;
            }
            else if (cdvDate.Value.DayOfWeek == DayOfWeek.Thursday)
            {
                // ������� WAFER, DA �� ���� �����.
                // 2011-06-09-������ : SAW �߰�
                if (cboGroup.Text == "WAFER" || cboGroup.Text == "SAW" || cboGroup.Text == "DA")
                    weekStatus = true;
            }
            else if (cdvDate.Value.DayOfWeek == DayOfWeek.Friday)
            {
                // �ݿ����� WAFER, DA, WB, MOLD �� ���� �����.
                // 2011-06-09-������ : SAW �߰�
                if (cboGroup.Text == "WAFER" || cboGroup.Text == "SAW" || cboGroup.Text == "DA" || cboGroup.Text == "WB" || cboGroup.Text == "MOLD")
                    weekStatus = true;
            }
            else
            {
                weekStatus = false;
            }
        }

        #endregion


        #region EVENT ó��
        /// <summary>
        /// 6. View ��ư Action
        /// </summary>
        private void btnView_Click(object sender, EventArgs e)
        {
            int iOperStart = 0;
            int iOperEnd = 0;
  
            DataTable dt = null;            

            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //�׷��� ���� 1������ SubTotal�� ����ϱ� ���ؼ� ù��° ���� ������
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2); 

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 6, null, null, btnSort);                
                //����Ÿ���̺�, ��Ż ������ ���ѹ�, ���� ���� ������Ż � ������, ù������ ���° ������ TOTAL �������
                //spdData.Sheets[0].FrozenColumnCount = 3;
                //spdData.RPT_AutoFit(false);

                //Total�κ� ������
                spdData.RPT_FillDataSelectiveCells("Total", 0, 6, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                dt.Dispose();

                // ������ ���� �÷��� ������ �÷��� ã�� ����...
                // 2011-06-09-������ : SAW �߰�
                if (cboGroup.Text == "SAW")
                {
                    iOperStart = 27;                    
                }
                else if (cboGroup.Text == "DA")
                {
                    iOperStart = 26;
                }
                else if (cboGroup.Text == "WB")
                {
                    iOperStart = 25;
                }
                else if (cboGroup.Text == "MOLD")
                {
                    iOperStart = 22;
                }
                else
                {
                    iOperStart = 14;
                }

                iOperEnd = 30;

                // ���� �׷��� WAFER�� �ƴϸ� ����� ���� ǥ�� ��.
                if (cboGroup.Text != "WAFER")
                {
                    // ���ϰ�ȹ�� ���� ��� ���� ǥ�� �߰�
                    for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                    {
                        int sum = 0;
                        int value = 0;

                        if (spdData.ActiveSheet.Cells[i, 30].BackColor.IsEmpty) // subtotal �κ� ���ܽ�Ű�� ����.
                        {
                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 11].Value) > 0 && Convert.ToInt32(spdData.ActiveSheet.Cells[i, 13].Value) > 0) // ����remain ���� ���Total �� 0 �̻��� �͸�...
                            {
                                for (int y = iOperStart; y <= iOperEnd; y++) // ���� �÷���ȣ
                                {
                                    value = Convert.ToInt32(spdData.ActiveSheet.Cells[i, y].Value);
                                    sum += value;

                                    if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 11].Value) > sum)
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

                SetAvgVertical2(0, 55);
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
        /// 2010-09-03-������ : AVG ���ϱ�.. SubTotal, GrandTotal ���Ҷ� Ư�� �÷� ������ ���� ���Ҷ�..        
        /// 
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        public void SetAvgVertical2(int nSampleNormalRowPos, int nColPos)
        {
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            double iassyMon = 0;
            double itargetMon = 0;
            double ijindo = 0;

            iassyMon = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 52].Value);
            itargetMon = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 6].Value);

            // �и��� 0�� �ƴҰ�쿡�� ����Ѵ�..
            if (itargetMon != 0)
            {
                ijindo = (iassyMon / itargetMon) * 100;

                if (ijindo > 100)
                    spdData.ActiveSheet.Cells[0, nColPos].Value = 100;
                else
                    spdData.ActiveSheet.Cells[0, nColPos].Value = ijindo;
                
                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                    {
                        iassyMon = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 52].Value);
                        itargetMon = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 6].Value);

                        if (itargetMon != 0)
                        {
                            ijindo = (iassyMon / itargetMon) * 100;
                            
                            if (ijindo > 100)
                                spdData.ActiveSheet.Cells[i, nColPos].Value = 100;
                            else
                                spdData.ActiveSheet.Cells[i, nColPos].Value = ijindo;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                StringBuilder Condition = new StringBuilder();
                //Condition.AppendFormat("��������: {0}        Lot Type: {1} " + "\n", cdvDate.Text, cdvLotType.Text);
                //Condition.AppendFormat("���Ͻ�������: {0}    SYSLSI: {1}     ������: {2}     ������: {3}    �ܿ��ϼ�: " + lblRemain.Text.ToString() + "    ", lblToday.Text.ToString(), lblSyslsi.Text.ToString(), lblMagam.Text.ToString(), lblJindo.Text.ToString());
                //Condition.Append(" ||   ");
                //Condition.AppendFormat("���Ͻ�������: {0}    MEMO:   {1}     ������: {2}     ������: {3}    �ܿ��ϼ�: " + lblRemain2.Text.ToString(), lblYesterday.Text.ToString(), lblMemo.Text.ToString(), lblMagam2.Text.ToString(), lblJindo2.Text.ToString());

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvLotType.sFactory = cdvFactory.txtValue;
        }
        #endregion
    }       
}
