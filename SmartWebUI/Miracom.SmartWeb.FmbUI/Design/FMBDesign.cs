using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using Miracom.FMBUI;
using Miracom.FMBUI.Controls;
using System.ComponentModel;
using Infragistics.Win.UltraWinToolbars;
using com.miracom.transceiverx.session;

using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public partial class FMBDesign : Miracom.SmartWeb.FWX.udcUIBase
    {
        delegate bool RefreshControlDelegate(Control.ControlCollection CtrlCollection, ref FMBUI.clsCtrlStatus ResourceStatus, int iStep);

        private RefreshControlDelegate _RefreshControlDelegate;
        
        public FMBDesign()
        {
            this.pnlFMBDesign = new udcFMBDesign(this);

            InitializeComponent();
            
            _RefreshControlDelegate = new RefreshControlDelegate(RefreshControl);
            //InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

            this.pnlFMBDesign.SuspendLayout();
            this.SuspendLayout();

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            this.pnlBackGround.Controls.Add(this.pnlFMBDesign);
            this.pnlBackGround.Controls.Add(this.pnlBottom);
            this.pnlBackGround.Controls.Add(this.pnlRight);
            //
            //pnlFMBDesign
            //
            this.pnlFMBDesign.AllowDrop = true;
            this.pnlFMBDesign.BackColor = System.Drawing.Color.White;
            this.pnlFMBDesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFMBDesign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlFMBDesign.Location = new System.Drawing.Point(10, 10);
            this.pnlFMBDesign.Name = "pnlFMBDesign";
            this.pnlFMBDesign.Size = new System.Drawing.Size(780, 580);
            this.pnlFMBDesign.TabIndex = 4;

            this.pnlFMBDesign.ResumeLayout();
            this.ResumeLayout();
			
        }

        #region " Property Implementations"

        private Point m_ptStartPos = new Point();

        private bool m_bLoading = true;
        private bool m_bDesignMode = false;
        private int m_iZoomScale = 0;
        private Size m_szOriginalDesignSize;

        private bool m_bDeleteFlag = false;
        private bool m_bSelectingCtrl = false;
        private bool m_bSelectedSelectingCtrl = false;
        private bool m_bGotFocus = false;

        [Description("Gets or sets IsLoading"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLoading
        {
            get
            {
                return m_bLoading;
            }
            set
            {

                if (m_bLoading.Equals(value) == false)
                {
                    m_bLoading = value;
                }
            }
        }

        [Description("Gets or sets IsDesignMode"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDesignMode
        {
            get
            {
                return m_bDesignMode;
            }
            set
            {


                if (m_bDesignMode.Equals(value) == false)
                {
                    if (value == false)
                    {
                        if (IsModifiedControl() == true)
                        {
                            if ((DialogResult)CmnFunction.ShowMsgBox(this.Name + " - " + modLanguageFunction.GetMessage(12), "FMB Client", MessageBoxButtons.YesNo, 1) == (DialogResult)Microsoft.VisualBasic.MsgBoxResult.Yes)
                            {
                                if (UpdateResourceListDetail() == true)
                                {
                                    CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
                                }
                            }
                        }
                    }
                    m_bDesignMode = value;
                    InitControls(value);
                    pnlFMBDesign.Invalidate(true);
                    //tsmZoomIn.Enabled = !value;
                    //tsmZoomOut.Enabled = !value;
                    //tsmSaveDesign.Enabled = value;
                    //tsmAddRes.Enabled = value;

                    ZoomScale = 0;
                }
            }
        }

        [Description("Gets or sets DesignSize"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Size DesignSize
        {
            get
            {
                return pnlBackGround.Size;
            }
            set
            {


                if (pnlBackGround.Size.Equals(value) == false)
                {
                    pnlBackGround.Size = value;
                }
            }
        }

        [Description("Gets or sets ZoomScale"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ZoomScale
        {
            get
            {
                return m_iZoomScale;
            }
            set
            {
                if (m_iZoomScale.Equals(value) == false)
                {
                    m_iZoomScale = value;
                    //if (value >= 5)
                    //{
                    //    tsmZoomIn.Enabled = false;
                    //}
                    //else
                    //{
                    //    if (IsDesignMode == true)
                    //    {
                    //        tsmZoomIn.Enabled = false;
                    //    }
                    //    else
                    //    {
                    //        tsmZoomIn.Enabled = true;
                    //    }
                    //}
                    //if (value <= -5)
                    //{
                    //    tsmZoomOut.Enabled = false;
                    //}
                    //else
                    //{
                    //    if (IsDesignMode == true)
                    //    {
                    //        tsmZoomOut.Enabled = false;
                    //    }
                    //    else
                    //    {
                    //        tsmZoomOut.Enabled = true;
                    //    }
                    //}
                }
            }
        }

        [Description("Gets or sets OriginalDesignSize"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size OriginalDesignSize
        {
            get
            {
                return m_szOriginalDesignSize;
            }
            set
            {

                if (m_szOriginalDesignSize.Equals(value) == false)
                {
                    m_szOriginalDesignSize = value;
                }
            }
        }

        [Description("Gets or sets DeleteFlag"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DeleteFlag
        {
            get
            {
                return m_bDeleteFlag;
            }
            set
            {

                if (m_bDeleteFlag.Equals(value) == false)
                {
                    m_bDeleteFlag = value;
                }
            }
        }

        [Description("Gets or sets IsSelectingCtrl"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool IsSelectingCtrl
        {
            get
            {
                return m_bSelectingCtrl;
            }
            set
            {

                if (m_bSelectingCtrl.Equals(value) == false)
                {
                    m_bSelectingCtrl = value;
                }
            }
        }

        [Description("Gets or sets IsSelectedSelectingCtrl"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool IsSelectedSelectingCtrl
        {
            get
            {
                return m_bSelectedSelectingCtrl;
            }
            set
            {

                if (m_bSelectedSelectingCtrl.Equals(value) == false)
                {
                    m_bSelectedSelectingCtrl = value;
                }
            }
        }

        [Description("Gets or sets IsGotFocus"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool IsGotFocus
        {
            get
            {
                return m_bGotFocus;
            }
            set
            {

                if (m_bGotFocus.Equals(value) == false)
                {
                    m_bGotFocus = value;
                }
            }
        }

        #endregion

        #region " Event Implementations"

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeleteFlag == true)
                {
                }
                else
                {
                    if (IsDesignMode == true)
                    {
                        if (IsModifiedControl() == true)
                        {
                            if ((DialogResult)CmnFunction.ShowMsgBox(this.Name + " - " + modLanguageFunction.GetMessage(12), "FMB Client", MessageBoxButtons.YesNo, 1) == (DialogResult)Microsoft.VisualBasic.MsgBoxResult.Yes)
                            {
                                if (UpdateResourceListDetail() == true)
                                {
                                    CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.frmFMBDesign_Closing()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

            try
            {
                Control ctrl;
                foreach (Control tempLoopVar_ctrl in this.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (!(ctrl == null))
                    {
                        ctrl.Dispose();
                        ctrl = null;
                    }
                }

                this.Dispose();

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.frmFMBDesign_Closed()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

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

        public void CtrlBase_CtrlMouseEnter(System.Object sender, System.EventArgs e)
        {

            try
            {
                if (sender == null)
                {
                    return;
                }
                if (!(sender is udcCtrlBase))
                {
                    return;
                }
                udcCtrlBase ctrl = (udcCtrlBase)sender;
                ctrl.IsDesignMode = IsDesignMode;
                if (IsDesignMode == false)
                {
                    return;
                }

                IsSelectingCtrl = true;
                IsSelectedSelectingCtrl = ctrl.IsSelected;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlMouseEnter()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        public void CtrlBase_CtrlMouseLeave(System.Object sender, System.EventArgs e)
        {

            try
            {
                if (sender == null)
                {
                    return;
                }
                if (!(sender is udcCtrlBase))
                {
                    return;
                }
                udcCtrlBase ctrl = (udcCtrlBase)sender;
                ctrl.IsDesignMode = IsDesignMode;
                if (IsDesignMode == false)
                {
                    return;
                }

                IsSelectingCtrl = false;
                IsSelectedSelectingCtrl = false;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlMouseLeave()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        public void CtrlBase_CtrlLostFocus(System.Object sender, System.EventArgs e)
        {

            try
            {
                if (sender == null)
                {
                    return;
                }
                if (!(sender is udcCtrlBase))
                {
                    return;
                }
                udcCtrlBase ctrl = (udcCtrlBase)sender;
                ctrl.IsDesignMode = IsDesignMode;
                if (IsDesignMode == false)
                {
                    return;
                }

                if (Control.ModifierKeys == Keys.Control)
                {
                    //' 1) Control Key를 누른 상태에서 이미 선택되어진 Control를 선택한 경우 -> 선택 유지
                    //' 2) Control Key를 누른 상태에서 선택되지 않은 Control를 선택한 경우 -> 선택 유지
                    //' 3) Control Key를 누른 상태에서 아무 곳이나 클릭한 경우 -> 선택 유지
                    if (IsSelectingCtrl == true)
                    {
                        // 1) Control를 선택한 경우
                        if (IsSelectedSelectingCtrl == true)
                        {
                            // 1) Control Key를 누른 상태에서 이미 선택되어진 Control를 선택한 경우 -> 선택 유지
                            ctrl.CanLostFocus = false;
                            //Debug.WriteLine("1-1")
                            //DebugWrite(ctrl)
                        }
                        else
                        {
                            // 2) Control Key를 누른 상태에서 선택되지 않은 Control를 선택한 경우 -> 선택 유지, 비활성
                            ctrl.CanLostFocus = false;
                            ctrl.IsFocused = false;
                            ctrl.RedrawCtrl();
                            //Debug.WriteLine("1-2")
                            //DebugWrite(ctrl)
                        }
                    }
                    else
                    {
                        // 3) Control Key를 누른 상태에서 아무 곳이나 클릭한 경우 -> 선택 유지
                        ctrl.CanLostFocus = false;
                        //DebugWrite(ctrl)
                        //Debug.WriteLine("1-3")
                    }
                }
                else
                {
                    //' 1) 이미 선택되어진 Control를 선택한 경우 - 선택 유지
                    //' 2) 일반적으로 다른 Control를 선택한 경우 - 선택 해제
                    //' 3) Control이 아닌 다른 곳을 선택한 경우 - 선택 유지
                    if (IsSelectingCtrl == true)
                    {
                        // 1) Control를 선택한 경우
                        if (IsSelectedSelectingCtrl == true)
                        {
                            // 1) 이미 선택되어진 Control를 선택하려는 경우 - 선택 유지
                            ctrl.CanLostFocus = false;
                            ctrl.IsFocused = false;
                            ctrl.RedrawCtrl();
                            //Debug.WriteLine("2-1")
                            //DebugWrite(ctrl)
                        }
                        else
                        {
                            // 2) 선택되지 않은 Control를 선택하려는 경우 - 선택 해제
                            RemoveSelectedControls(ctrl);
                            //Debug.WriteLine("2-2")
                            //DebugWrite(ctrl)
                        }
                    }
                    else
                    {
                        // 3) Control이 아닌 다른 곳을 선택한 경우 - 선택 유지
                        ctrl.CanLostFocus = false;
                        //Debug.WriteLine("2-3")
                        //DebugWrite(ctrl)
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlLostFocus()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        public void CtrlBase_CtrlGotFocus(System.Object sender, System.EventArgs e)
        {

            try
            {
                if (sender == null)
                {
                    return;
                }
                if (!(sender is udcCtrlBase))
                {
                    return;
                }
                udcCtrlBase ctrl = (udcCtrlBase)sender;
                ctrl.IsDesignMode = IsDesignMode;
                if (IsDesignMode == false)
                {
                    return;
                }

                IsGotFocus = true;

                if (Control.ModifierKeys == Keys.Control)
                {
                    //' 1) 이미 선택되어진 Control를 선택 해제 시 -> 비선택
                    //' 2) Control Key와 함께 선택 시 -> 선택
                    if (ctrl.IsSelected == true)
                    {
                        // 1) 이미 선택되어진 Control일 경우 -> 비선택
                        ctrl.CanGotFocus = false;
                        RemoveSelectedControls(ctrl);
                        //Debug.WriteLine("1-1")
                        //DebugWrite(ctrl)
                        return;
                    }
                    else
                    {
                        // 2) 선택되어진 Control이 아닐 경우 -> 선택
                        if (pnlFMBDesign.SelectedControlsCount() > 0)
                        {
                            //' 1) 다른 선택되어진 Control이 있을 경우
                            //' 2) 다른 선택되어진 Control에서 먼저 LostFocus Event가 발생하므로 LostFocus Event 처리 필요
                            AddSelectedControls(ctrl);
                            //Debug.WriteLine("1-2-1")
                            //DebugWrite(ctrl)
                            return;
                        }
                        else
                        {
                            // 2) 다른 선택되어진 Control이 없을 경우 - OK
                            AddSelectedControls(ctrl);
                            //Debug.WriteLine("1-2-2")
                            //DebugWrite(ctrl)
                            return;
                        }
                    }
                }
                else
                {
                    //' 1) 이미 선택되어진 Control들 중에 Focus를 활성 시킴
                    //' 2) 일반적인 선택 시
                    if (ctrl.IsSelected == true && IsSelectedContains(ctrl) == true)
                    {
                        if (ctrl.IsFocused == true)
                        {
                            return;
                        }
                        else
                        {
                            // 1) 이미 선택되어진 Control을 재선택한 경우 -> Focus 활성화, 나머지는 Focus 비활성화
                            int i;
                            for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                            {
                                if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused == true)
                                {
                                    ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused = false;
                                    ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).RedrawCtrl();
                                }
                            }
                            RemoveSelectedControls(ctrl);
                            AddSelectedControls(ctrl);
                            //Debug.WriteLine("2-1")
                            //DebugWrite(ctrl)
                            return;
                        }
                    }
                    else
                    {
                        // 2) 일반적인 선택인 경우
                        int i;
                        for (i = pnlFMBDesign.SelectedControlsCount(); i > 0; i--)
                        {
                            ((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]).IsHot = false;
                            ((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]).IsFocused = false;
                            ((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]).IsSelected = false;
                            ((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]).IsPressed = false;
                            ((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]).RedrawCtrl();
                            pnlFMBDesign.SelectedControls.Remove((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]);
                        }
                        AddSelectedControls(ctrl);
                        //Debug.WriteLine("2-2")
                        //DebugWrite(ctrl)
                        return;

                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlGotFocus()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        public void CtrlBase_CtrlMouseDown(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {

            try
            {
                if (sender == null)
                {
                    return;
                }
                if (!(sender is udcCtrlBase))
                {
                    return;
                }
                udcCtrlBase ctrl = (udcCtrlBase)sender;
                ctrl.IsDesignMode = IsDesignMode;
                if (IsDesignMode == false)
                {
                    return;
                }
                //' 1) GotFocus가 아닌 상태에서 MouseDown시 처리
                //' 2) MouseDown 상태에서 Control 이동 및 크기 변경 처리
                if (IsGotFocus == true)
                {
                    IsGotFocus = false;
                    //Debug.WriteLine("1-1")
                    //DebugWrite(ctrl)
                }
                else
                {
                    //' 1) Control Key를 누른 상태에서 이미 선택되어지고 활성상태인 경우 -> 비선택/비활성
                    //' 2) Control Key를 누른 상태에서 선택되지 않은 경우 -> 선택/활성, 나머지는 유지
                    //' 3) Control Key를 누르지 않은 상태에서 선택되지 않은 경우 -> 선택/활성, 나머지는 비선택/비활성
                    if (ctrl.IsSelected == true)
                    {
                        // 1) Control Key를 누른 상태에서 이미 선택되어지고 활성상태인 경우 -> 비선택/비활성
                        if (Control.ModifierKeys == Keys.Control)
                        {
                            RemoveSelectedControls(ctrl);
                        }
                        IsGotFocus = false;
                        //Debug.WriteLine("2-1")
                        //DebugWrite(ctrl)
                    }
                    else
                    {
                        if (Control.ModifierKeys == Keys.Control)
                        {
                            // 2) Control Key를 누른 상태에서 선택되지 않은 경우 -> 선택/활성, 나머지는 유지
                        }
                        else
                        {
                            // 3) Control Key를 누르지 않은 상태에서 선택되지 않은 경우 -> 선택/활성, 나머지는 비선택/비활성
                            int i;
                            for (i = pnlFMBDesign.SelectedControlsCount(); i > 0; i--)
                            {
                                ((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]).IsHot = false;
                                ((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]).IsFocused = false;
                                ((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]).IsSelected = false;
                                ((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]).IsPressed = false;
                                ((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]).RedrawCtrl();
                                pnlFMBDesign.SelectedControls.Remove((udcCtrlBase)pnlFMBDesign.SelectedControls[i - 1]);
                            }
                        }
                        AddSelectedControls(ctrl);
                        IsGotFocus = false;
                        //Debug.WriteLine("2-2")
                        //DebugWrite(ctrl)
                    }
                }

                if (pnlFMBDesign.SelectedControlsCount() > 1)
                {
                    m_ptStartPos = ctrl.PointToScreen(new Point(e.X, e.Y));
                    ctrl.Cursor = Cursors.SizeAll;
                    ctrl.IsPressed = true;
                    ctrl.CanMouseDown = false;
                    ctrl.RedrawCtrl();
                    //Debug.WriteLine("3-1")
                    //DebugWrite(ctrl)
                }
                else
                {
                    m_ptStartPos = new Point();
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlMouseDown()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        public void CtrlBase_CtrlMouseMove(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {

            try
            {
                if (sender == null)
                {
                    return;
                }
                if (!(sender is udcCtrlBase))
                {
                    return;
                }
                udcCtrlBase ctrl = (udcCtrlBase)sender;
                ctrl.IsDesignMode = IsDesignMode;
                if (IsDesignMode == false)
                {
                    return;
                }

                if (pnlFMBDesign.SelectedControlsCount() > 1)
                {
                    ctrl.CanMouseMove = false;
                }

                if (ctrl.IsPressed == true && !(m_ptStartPos.IsEmpty))
                {
                    Point ptScreen = ctrl.PointToScreen(new Point(e.X, e.Y));
                    Size szDelta = new Size(ptScreen.X - m_ptStartPos.X, ptScreen.Y - m_ptStartPos.Y);

                    if (Math.Abs(szDelta.Width) < SystemInformation.DragSize.Width && Math.Abs(szDelta.Height) < SystemInformation.DragSize.Height)
                    {
                        return;
                    }

                    int i;
                    for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                    {
                        if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetMoveToSize(Miracom.FMBUI.Controls.modDefines.CTRL_HITTEST_TRACKER_ALL, szDelta) == true)
                        {
                            m_ptStartPos = ptScreen;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlMouseMove()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        public void CtrlBase_CtrlMouseUp(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {

            try
            {
                if (sender == null)
                {
                    return;
                }
                if (!(sender is udcCtrlBase))
                {
                    return;
                }
                udcCtrlBase ctrl = (udcCtrlBase)sender;
                ctrl.IsDesignMode = IsDesignMode;
                if (IsDesignMode == false)
                {
                    return;
                }

                m_ptStartPos = new Point();

                if (pnlFMBDesign.SelectedControlsCount() > 1)
                {
                    ctrl.IsPressed = false;
                    ctrl.CanMouseUp = false;
                    ctrl.Cursor = Cursors.Default;
                    ctrl.RedrawCtrl();
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlMouseUp()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        public void CtrlBase_CtrlKeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
        {

            try
            {
                if (sender == null)
                {
                    return;
                }
                if (!(sender is udcCtrlBase))
                {
                    return;
                }
                udcCtrlBase ctrl = (udcCtrlBase)sender;
                ctrl.IsDesignMode = IsDesignMode;
                if (IsDesignMode == false)
                {
                    return;
                }

                if (pnlFMBDesign.SelectedControlsCount() > 0)
                {
                    ctrl.CanKeyDown = false;
                }

                int i;
                if (e.Control == true && e.Shift == false)
                {
                    if (this.Enabled == true)
                    {
                        Point ptLocation = new Point();
                        for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                        {
                            switch (e.KeyCode)
                            {
                                case Keys.Up:

                                    ptLocation = new Point(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().X, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().Y - 1);
                                    break;
                                case Keys.Right:

                                    ptLocation = new Point(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().X + 1, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().Y);
                                    break;
                                case Keys.Down:

                                    ptLocation = new Point(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().X, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().Y + 1);
                                    break;
                                case Keys.Left:

                                    ptLocation = new Point(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().X - 1, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().Y);
                                    break;
                            }
                            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Right || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
                            {
                                ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetLocation(ptLocation, false);
                            }
                        }
                    }
                }
                else if (e.Control == true && e.Shift == true)
                {
                    if (this.Enabled == true)
                    {
                        Size szSize = new Size();
                        for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                        {
                            switch (e.KeyCode)
                            {
                                case Keys.Up:

                                    szSize = new Size(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Width, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Height - 1);
                                    break;
                                case Keys.Right:

                                    szSize = new Size(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Width + 1, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Height);
                                    break;
                                case Keys.Down:

                                    szSize = new Size(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Width, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Height + 1);
                                    break;
                                case Keys.Left:

                                    szSize = new Size(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Width - 1, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Height);
                                    break;
                            }
                            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Right || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
                            {
                                if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.HorizontalLine)
                                {
                                    if (szSize.Width < modGlobalConstant.CTRL_MININUM_SIZE || szSize.Height < modGlobalConstant.LINE_MININUM_SIZE)
                                    {
                                        return;
                                    }
                                    if (szSize.Height > modGlobalConstant.CTRL_MAXIMUM_SIZE)
                                    {
                                        return;
                                    }
                                }
                                else if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.VerticalLine)
                                {
                                    if (szSize.Width < modGlobalConstant.LINE_MININUM_SIZE || szSize.Height < modGlobalConstant.CTRL_MININUM_SIZE)
                                    {
                                        return;
                                    }
                                    if (szSize.Width > modGlobalConstant.CTRL_MAXIMUM_SIZE)
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    if (szSize.Width < modGlobalConstant.CTRL_MININUM_SIZE || szSize.Height < modGlobalConstant.CTRL_MININUM_SIZE)
                                    {
                                        return;
                                    }
                                    if (szSize.Width > modGlobalConstant.CTRL_MAXIMUM_SIZE || szSize.Height > modGlobalConstant.CTRL_MAXIMUM_SIZE)
                                    {
                                        return;
                                    }
                                }
                                ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetSize(szSize, false);
                            }
                        }
                    }
                }
                else
                {
                    if (this.Enabled == true)
                    {
                        if (e.KeyCode == Keys.Delete)
                        {
                            string sFactory = "";
                            string sLayout = "";
                            bool IsRefresh = false;
                            if (pnlFMBDesign.SelectedControlsCount() < 1)
                            {
                                return;
                            }
                            if (Tag.ToString() == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                            {
                                sFactory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                                sLayout = modCommonFunction.GetStringBySeperator(Name, ":", 2);
                            }
                            else if (Tag.ToString() == modGlobalConstant.FMB_CATEGORY_GROUP)
                            {
                                sFactory = GlobalVariable.gsFactory;
                                sLayout = Name;
                            }

                            ArrayList copySelectedControls = new ArrayList();
                            //ControlCollection copySelectedControls = null;

                            for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                            {
                                copySelectedControls.Add(pnlFMBDesign.SelectedControls[i]);
                            }
                            if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(5), "FMB Client", MessageBoxButtons.YesNo, 1) == DialogResult.Yes)
                            {
                                for (i = copySelectedControls.Count - 1; i >= 0; i--)
                                {
                                    if (modCommonFunction.DeleteResource(this.Tag.ToString(), sFactory, sLayout, ((udcCtrlBase)copySelectedControls[i]).Name, ((((udcCtrlBase)copySelectedControls[i]).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource) ? modGlobalConstant.FMB_RESOURCE_TYPE : modGlobalConstant.FMB_TAG_TYPE)) == false)
                                    {
                                        return;
                                    }
                                    if (((udcCtrlBase)copySelectedControls[i]).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource && Tag.ToString() == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                                    {
                                        IsRefresh = true;
                                    }
                                }

                                if (IsRefresh == true)
                                {
                                    if (copySelectedControls.Count == 1)
                                    {
                                        if (modInterface.gIMdiForm.RefreshDesignList("3", sFactory, sLayout, ((udcCtrlBase)copySelectedControls[0]).Name, "") == false)
                                        {
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (modInterface.gIMdiForm.RefreshDesignList("5", sFactory, sLayout, "", "") == false)
                                        {
                                            return;
                                        }
                                    }
                                }
                                for (i = copySelectedControls.Count - 1; i >= 0; i--)
                                {
                                    pnlFMBDesign.Controls.Remove((udcCtrlBase)copySelectedControls[i]);
                                }
                            }
                            else
                            {
                                ctrl.CanKeyDown = false;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlKeyDown()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        public void CtrlBase_CtrlContextMenu(System.Object sender, CtrlContextMenu_EventArgs e)
        {

            try
            {
                switch (((MenuItem)sender).MergeOrder)
                {
                    case 1:

                        CtrlBase_CtrlUpdate(sender, e);
                        break;
                    case 2:

                        CtrlBase_CtrlDelete(sender, e);
                        break;
                    case 3:

                        CtrlBase_CtrlProperties(sender, e);
                        break;
                    //case 4:

                    //    CtrlBase_CtrlViewResourceStatus(sender, e);
                    //    break;
                    //case 5:

                    //    CtrlBase_CtrlViewResourceHistory(sender, e);
                    //    break;
                    //case 6:

                    //    CtrlBase_CtrlTranEvent(sender, e);
                    //    break;
                    case 7:

                        udcCtrlBase copyCtrl = (udcCtrlBase)e.CtrlSender;
                        pnlFMBDesign.ClipboardControl = copyCtrl;
                        break;
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlContextMenu()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        public void CtrlBase_CtrlUpdate(System.Object sender, CtrlContextMenu_EventArgs e)
        {



            try
            {

                if (!(e.CtrlSender is udcCtrlBase))
                {
                    return;
                }

                udcCtrlBase ctrl = (udcCtrlBase)e.CtrlSender;
                string sFactory;


                switch (ctrl.CtrlStatus.ToolType)
                {
                    case Miracom.FMBUI.Enums.eToolType.Resource:

                        frmFMBCreateResource form = new frmFMBCreateResource(modGlobalConstant.MP_STEP_UPDATE);

                        form.Tag = Tag;
                        if (Tag.ToString() == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                        {
                            sFactory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                            form.txtFactory.Text = sFactory;
                            form.cdvLayOut.Text = modCommonFunction.GetStringBySeperator(Name, ":", 2);
                        }
                        else if (Tag.ToString() == modGlobalConstant.FMB_CATEGORY_GROUP)
                        {
                            sFactory = GlobalVariable.gsFactory;
                            form.txtFactory.Text = sFactory;
                            form.cdvLayOut.Text = Name;
                        }
                        else
                        {
                            CmnFunction.ShowMsgBox("CtrlBase_CtrlUpdate() Failed.", "FMB Client", MessageBoxButtons.OK, 1);
                            return;
                        }
                        form.cdvResID.Text = ctrl.Name;
                        form.txtX.Text = ctrl.GetLocation().X.ToString();
                        form.txtY.Text = ctrl.GetLocation().Y.ToString();
                        form.txtWidth.Text = ctrl.GetSize().Width.ToString();
                        form.txtHeight.Text = ctrl.GetSize().Height.ToString();
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            clsCtrlStatus ResourceStatus = new clsCtrlStatus();
                            ResourceStatus.Key = form.cdvResID.Text;
                            ResourceStatus.SetLocation(new Point(CmnFunction.ToInt(form.txtX.Text), CmnFunction.ToInt(form.txtY.Text)));
                            ResourceStatus.SetSize(new Size(CmnFunction.ToInt(form.txtWidth.Text), CmnFunction.ToInt(form.txtHeight.Text)));
                            ResourceStatus.Text = form.txtText.Text;
                            ResourceStatus.ResTagFlag = form.txtResTagFlag.Text;
                            if (form.utcText.Color.IsSystemColor == true || form.utcText.Color.IsKnownColor == true)
                            {
                                ResourceStatus.TextColor = CmnFunction.ToInt(form.utcText.Color.ToKnownColor());
                            }
                            else
                            {
                                ResourceStatus.TextColor = form.utcText.Color.ToArgb();
                            }
                            ResourceStatus.TextFontName = System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultFontName));
                            if (form.cboSize.Text == "")
                            {
                                ResourceStatus.TextSize = 0;
                            }
                            else
                            {
                                ResourceStatus.TextSize = CmnFunction.ToInt(form.cboSize.Text);
                            }
                            ResourceStatus.TextStyle = form.cboTextStyle.SelectedIndex;
                            ResourceStatus.ToolType = ctrl.CtrlStatus.ToolType;
                            ResourceStatus.LastEvent = form.txtLastEvent.Text;
                            ResourceStatus.PrimaryStatus = form.txtPriSts.Text;
                            ResourceStatus.ProcMode = form.txtProcMode.Text;
                            if (form.txtCtrlMode.Text == "ON LINE")
                            {
                                ResourceStatus.CtrlMode = "OL";
                            }
                            else if (form.txtCtrlMode.Text == "ON LINE REAL")
                            {
                                ResourceStatus.CtrlMode = "OR";
                            }
                            else if (form.txtCtrlMode.Text == "OFF LINE")
                            {
                                ResourceStatus.CtrlMode = "OF";
                            }
                            ResourceStatus.ResourceType = form.txtResourceType.Text;
                            ResourceStatus.UpDownFlag = form.txtUpDown.Text;

                            ResourceStatus.AreaID = form.txtArea.Text;
                            ResourceStatus.SubAreaID = form.txtSubArea.Text;

                            if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.IsProcessMode)) == "P")
                            {
                                ResourceStatus.IsProcessMode = true;
                            }
                            else
                            {
                                ResourceStatus.IsProcessMode = false;
                            }
                            if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.UseEventColor)) == "Y")
                            {
                                ResourceStatus.IsUseEventColor = true;
                                ResourceStatus.EventBackColor = modGlobalVariable.gGlobalOptions.GetOptions(sFactory, ResourceStatus.LastEvent).ToArgb();
                            }
                            else
                            {
                                ResourceStatus.IsUseEventColor = false;
                            }
                            if (form.utcBack.Color.IsSystemColor == true || form.utcBack.Color.IsKnownColor == true)
                            {
                                ResourceStatus.BackColor = CmnFunction.ToInt(form.utcBack.Color.ToKnownColor());
                            }
                            else
                            {
                                ResourceStatus.BackColor = form.utcBack.Color.ToArgb();
                            }
                            ResourceStatus.ImageIndex = form.iImageIndex;
                            ResourceStatus.IsViewSignal = form.chkSignalFlag.Checked;

                            if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT && form.cdvLayOut.Text != modCommonFunction.GetStringBySeperator(Name, ":", 2))
                            {
                                string sMoveForm = sFactory + ":" + form.cdvLayOut.Text;
                                //Form frmChild;
                                bool bFindFlag = false;
                                //foreach (Form tempLoopVar_frmChild in this.MdiParent.MdiChildren)
                                //{
                                //    frmChild = tempLoopVar_frmChild;
                                //    if (frmChild is frmFMBDesign)
                                //    {
                                //        if (frmChild.Name == sMoveForm)
                                //        {
                                //            bFindFlag = true;
                                //            ((frmFMBDesign)frmChild).AddControl(ResourceStatus, true, false);
                                //            this.pnlFMBDesign.Controls.Remove(ctrl);
                                //            if (modInterface.gIMdiForm.RefreshDesignList("4", sFactory, modCommonFunction.GetStringBySeperator(Name, ":", 2), form.cdvResID.Text, form.cdvLayOut.Text) == false)
                                //            {
                                //                return;
                                //            }
                                //            break;
                                //        }
                                //    }
                                //}
                                if (bFindFlag == false)
                                {
                                    this.pnlFMBDesign.Controls.Remove(ctrl);
                                    if (modInterface.gIMdiForm.RefreshDesignList("4", sFactory, modCommonFunction.GetStringBySeperator(Name, ":", 2), form.cdvResID.Text, form.cdvLayOut.Text) == false)
                                    {
                                        return;
                                    }
                                }
                            }
                            ctrl.SetCtrlStatusData(ResourceStatus, 1, true);
                            ctrl.CtrlStatus.IsSaveFlag = false;
                        }
                        break;
                    default:

                        //frmFMBCreateTag form = new frmFMBCreateTag(modGlobalConstant.MP_STEP_UPDATE); => 변수 form 이 중복되어 form1으로 명칭 변경(2007.02.21)
                        frmFMBCreateTag form1 = new frmFMBCreateTag(modGlobalConstant.MP_STEP_UPDATE);


                        form1.Tag = Tag;
                        if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                        {
                            sFactory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                            form1.txtFactory.Text = sFactory;
                            form1.txtLayOut.Text = modCommonFunction.GetStringBySeperator(Name, ":", 2);
                        }
                        else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                        {
                            sFactory = GlobalVariable.gsFactory;
                            form1.txtFactory.Text = sFactory;
                            form1.txtLayOut.Text = Name;
                        }
                        else
                        {
                            CmnFunction.ShowMsgBox("CtrlBase_CtrlUpdate() Failed.", "FMB Client", MessageBoxButtons.OK, 1);
                            return;
                        }
                        form1.txtTagID.Text = ctrl.Name;
                        form1.txtX.Text = ctrl.GetLocation().X.ToString();
                        form1.txtY.Text = ctrl.GetLocation().Y.ToString();
                        if (ctrl.CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.VerticalLine)
                        {
                            form1.txtWidth.Text = ctrl.GetSize().Height.ToString();
                            form1.txtHeight.Text = ctrl.GetSize().Width.ToString();
                        }
                        else
                        {
                            form1.txtWidth.Text = ctrl.GetSize().Width.ToString();
                            form1.txtHeight.Text = ctrl.GetSize().Height.ToString();
                        }
                        if (form1.ShowDialog(this) == DialogResult.OK)
                        {
                            clsCtrlStatus ResourceStatus = new clsCtrlStatus();
                            ResourceStatus.Key = form1.txtTagID.Text;
                            ResourceStatus.SetLocation(new Point(CmnFunction.ToInt(form1.txtX.Text), CmnFunction.ToInt(form1.txtY.Text)));
                            if (form1.cboShape.SelectedIndex == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.VerticalLine) - 1)
                            {
                                ResourceStatus.SetSize(new Size(CmnFunction.ToInt(form1.txtHeight.Text), CmnFunction.ToInt(form1.txtWidth.Text)));
                            }
                            else
                            {
                                ResourceStatus.SetSize(new Size(CmnFunction.ToInt(form1.txtWidth.Text), CmnFunction.ToInt(form1.txtHeight.Text)));
                            }
                            ResourceStatus.ResTagFlag = form1.txtResTagFlag.Text;
                            ResourceStatus.Text = form1.txtText.Text;
                            if (form1.utcText.Color.IsSystemColor == true || form1.utcText.Color.IsKnownColor == true)
                            {
                                ResourceStatus.TextColor = CmnFunction.ToInt(form1.utcText.Color.ToKnownColor());
                            }
                            else
                            {
                                ResourceStatus.TextColor = form1.utcText.Color.ToArgb();
                            }
                            if (form1.utcBack.Color.IsSystemColor == true || form1.utcBack.Color.IsKnownColor == true)
                            {
                                ResourceStatus.BackColor = CmnFunction.ToInt(form1.utcBack.Color.ToKnownColor());
                            }
                            else
                            {
                                ResourceStatus.BackColor = form1.utcBack.Color.ToArgb();
                            }
                            ResourceStatus.TextFontName = System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultFontName));
                            if (form1.cboSize.Text == "")
                            {
                                ResourceStatus.TextSize = 0;
                            }
                            else
                            {
                                ResourceStatus.TextSize = CmnFunction.ToInt(form1.cboSize.Text);
                            }
                            ResourceStatus.TextStyle = form1.cboTextStyle.SelectedIndex;
                            ResourceStatus.ToolType = (Miracom.FMBUI.Enums.eToolType)form1.cboShape.SelectedIndex + 1;
                            ResourceStatus.IsNoEvent = form1.chkNoMouseEvent.Checked;
                            ctrl.SetCtrlStatusData(ResourceStatus, 1, true);
                            ctrl.CtrlStatus.IsSaveFlag = false;
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlUpdate()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        public void CtrlBase_CtrlDelete(System.Object sender, CtrlContextMenu_EventArgs e)
        {

            try
            {
                if (!(e.CtrlSender is udcCtrlBase))
                {
                    return;
                }

                udcCtrlBase ctrl = (udcCtrlBase)e.CtrlSender;

                if (this.pnlFMBDesign.Contains(ctrl) == false)
                {
                    return;
                }

                string sFactory = "";
                string sLayout = "";
                if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    sFactory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                    sLayout = modCommonFunction.GetStringBySeperator(Name, ":", 2);
                }
                else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                {
                    sFactory = GlobalVariable.gsFactory;
                    sLayout = Name;
                }

                if (e.FunctionName == "KEY_DELETE")
                {
                    if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(5), "FMB Client", MessageBoxButtons.YesNo, 1) == DialogResult.Yes)
                    {
                        if (modCommonFunction.DeleteResource(System.Convert.ToString(this.Tag), sFactory, sLayout, ctrl.Name, (ctrl.CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource ? modGlobalConstant.FMB_RESOURCE_TYPE : modGlobalConstant.FMB_TAG_TYPE)) == false)
                        {
                            return;
                        }
                        if (ctrl.CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource && System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                        {
                            if (modInterface.gIMdiForm.RefreshDesignList("3", sFactory, sLayout, ctrl.Name, "") == false)
                            {
                                return;
                            }
                        }
                        pnlFMBDesign.Controls.Remove(ctrl);
                    }
                }
                else
                {
                    switch (ctrl.CtrlStatus.ToolType)
                    {
                        case Miracom.FMBUI.Enums.eToolType.Resource:

                            frmFMBCreateResource form = new frmFMBCreateResource(modGlobalConstant.MP_STEP_DELETE);
                            form.Tag = Tag;
                            form.txtFactory.Text = sFactory;
                            form.cdvLayOut.Text = sLayout;
                            form.cdvResID.Text = ctrl.Name;
                            form.txtX.Text = ctrl.GetLocation().X.ToString();
                            form.txtY.Text = ctrl.GetLocation().Y.ToString();
                            form.txtWidth.Text = ctrl.GetSize().Width.ToString();
                            form.txtHeight.Text = ctrl.GetSize().Height.ToString();
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {
                                pnlFMBDesign.Controls.Remove(ctrl);
                            }
                            break;
                        default:

                            frmFMBCreateTag form1 = new frmFMBCreateTag(modGlobalConstant.MP_STEP_DELETE);
                            form1.Tag = Tag;
                            form1.txtFactory.Text = sFactory;
                            form1.txtLayOut.Text = sLayout;
                            form1.txtTagID.Text = ctrl.Name;
                            form1.txtX.Text = ctrl.GetLocation().X.ToString();
                            form1.txtY.Text = ctrl.GetLocation().Y.ToString();
                            if (ctrl.CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.VerticalLine)
                            {
                                form1.txtWidth.Text = ctrl.GetSize().Height.ToString();
                                form1.txtHeight.Text = ctrl.GetSize().Width.ToString();
                            }
                            else
                            {
                                form1.txtWidth.Text = ctrl.GetSize().Width.ToString();
                                form1.txtHeight.Text = ctrl.GetSize().Height.ToString();
                            }
                            if (form1.ShowDialog(this) == DialogResult.OK)
                            {
                                pnlFMBDesign.Controls.Remove(ctrl);
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlDelete()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        public void CtrlBase_CtrlProperties(System.Object sender, CtrlContextMenu_EventArgs e)
        {

            try
            {
                if (!(e.CtrlSender is udcCtrlBase))
                {
                    return;
                }

                udcCtrlBase ctrl = (udcCtrlBase)e.CtrlSender;

                switch (ctrl.CtrlStatus.ToolType)
                {
                    case Miracom.FMBUI.Enums.eToolType.Resource:

                        frmFMBCreateResource form = new frmFMBCreateResource(modGlobalConstant.MP_STEP_VIEW);
                        form.Tag = Tag;
                        if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                        {
                            form.txtFactory.Text = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                            form.cdvLayOut.Text = modCommonFunction.GetStringBySeperator(Name, ":", 2);
                        }
                        else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                        {
                            form.txtFactory.Text = GlobalVariable.gsFactory;
                            form.cdvLayOut.Text = Name;
                        }
                        else
                        {
                            CmnFunction.ShowMsgBox("CtrlBase_CtrlProperties() Failed.", "FMB Client", MessageBoxButtons.OK, 1);
                            return;
                        }
                        form.cdvResID.Text = ctrl.Name;
                        form.txtX.Text = ctrl.GetLocation().X.ToString();
                        form.txtY.Text = ctrl.GetLocation().Y.ToString();
                        form.txtWidth.Text = ctrl.GetSize().Width.ToString();
                        form.txtHeight.Text = ctrl.GetSize().Height.ToString();
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            //Do Nothing - Only View
                        }
                        break;
                    //Dim form As Form1 = New Form1
                    //form.ShowDialog(Me)
                    default:

                        frmFMBCreateTag form1 = new frmFMBCreateTag(modGlobalConstant.MP_STEP_VIEW);
                        form1.Tag = Tag;
                        if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                        {
                            form1.txtFactory.Text = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                            form1.txtLayOut.Text = modCommonFunction.GetStringBySeperator(Name, ":", 2);
                        }
                        else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                        {
                            form1.txtFactory.Text = GlobalVariable.gsFactory;
                            form1.txtLayOut.Text = Name;
                        }
                        else
                        {
                            CmnFunction.ShowMsgBox("CtrlBase_CtrlProperties() Failed.", "FMB Client", MessageBoxButtons.OK, 1);
                            return;
                        }
                        form1.txtTagID.Text = ctrl.Name;
                        form1.txtX.Text = ctrl.GetLocation().X.ToString();
                        form1.txtY.Text = ctrl.GetLocation().Y.ToString();
                        if (ctrl.CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.VerticalLine)
                        {
                            form1.txtWidth.Text = ctrl.GetSize().Height.ToString();
                            form1.txtHeight.Text = ctrl.GetSize().Width.ToString();
                        }
                        else
                        {
                            form1.txtWidth.Text = ctrl.GetSize().Width.ToString();
                            form1.txtHeight.Text = ctrl.GetSize().Height.ToString();
                        }
                        if (form1.ShowDialog(this) == DialogResult.OK)
                        {
                            //Do Nothing - Only View
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlProperties()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        //public void CtrlBase_CtrlViewResourceStatus(System.Object sender, CtrlContextMenu_EventArgs e)
        //{

        //    try
        //    {
        //        if (!(e.CtrlSender is udcCtrlBase))
        //        {
        //            return;
        //        }

        //        udcCtrlBase ctrl = (udcCtrlBase)e.CtrlSender;

        //        frmFMBViewResourceStatus form = new frmFMBViewResourceStatus();
        //        if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
        //        {
        //            form.cdvFactory.Text = modCommonFunction.GetStringBySeperator(Name, ":", 1);
        //        }
        //        else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
        //        {
        //            form.cdvFactory.Text = GlobalVariable.gsFactory;
        //        }
        //        form.cdvResID.Text = ctrl.Name;
        //        form.MdiParent = GlobalVariable.gfrmMDI;
        //        form.Show();
        //        form.btnView.PerformClick();

        //    }
        //    catch (Exception ex)
        //    {
        //        CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlViewResourceStatus()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
        //    }

        //}

        //public void CtrlBase_CtrlViewResourceHistory(System.Object sender, CtrlContextMenu_EventArgs e)
        //{

        //    try
        //    {
        //        if (!(e.CtrlSender is udcCtrlBase))
        //        {
        //            return;
        //        }

        //        udcCtrlBase ctrl = (udcCtrlBase)e.CtrlSender;

        //        frmFMBViewResourceHistory form = new frmFMBViewResourceHistory();
        //        if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
        //        {
        //            form.cdvFactory.Text = modCommonFunction.GetStringBySeperator(Name, ":", 1);
        //        }
        //        else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
        //        {
        //            form.cdvFactory.Text = GlobalVariable.gsFactory;
        //        }
        //        form.cdvResID.Text = ctrl.Name;
        //        form.MdiParent = GlobalVariable.gfrmMDI;
        //        form.Show();
        //        form.btnView.PerformClick();

        //    }
        //    catch (Exception ex)
        //    {
        //        CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlViewResourceHistory()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
        //    }

        //}

        //public void CtrlBase_CtrlTranEvent(System.Object sender, CtrlContextMenu_EventArgs e)
        //{

        //    try
        //    {
        //        if (!(e.CtrlSender is udcCtrlBase))
        //        {
        //            return;
        //        }

        //        udcCtrlBase ctrl = (udcCtrlBase)e.CtrlSender;

        //        GlobalVariable.gsCurrentRes_ID = ctrl.Name;
        //        Form form = modCommonFunction.GetChildForm(GlobalVariable.gfrmMDI, "frmRASTranEvent");
        //        if (form == null)
        //        {
        //            form = new frmRASTranEvent();
        //        }

        //        form.MdiParent = GlobalVariable.gfrmMDI;
        //        form.Show();

        //        GlobalVariable.gsCurrentRes_ID = "";

        //    }
        //    catch (Exception ex)
        //    {
        //        CmnFunction.ShowMsgBox("frmFMBDesign.CtrlBase_CtrlTranEvent()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
        //    }

        //}

        private void FMBDesign_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                IsLoading = true;

                if (GlobalVariable.giAutoRefreshTime > 0)
                {
                    tmrRefresh.Interval = GlobalVariable.giAutoRefreshTime * 1000;

                }

                if (GlobalVariable.gbAutoRefresh == true)
                {
                    tmrRefresh.Start();
                }
                else
                {
                    tmrRefresh.Stop();
                }

                //tsmTopFormat.Enabled = false;
                //tsmSaveDesign.Enabled = IsDesignMode;
                //tsmAddRes.Enabled = IsDesignMode;

                pnlFMBDesign.pnlTracker.BackColor = Color.FromArgb(0, Color.Beige);

                if (IsDesignMode == true)
                {
                    pnlFMBDesign.BackColor = Color.WhiteSmoke;
                }
                else
                {
                    pnlFMBDesign.BackColor = Color.White;
                }

                string sFactory;
                if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    sFactory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                }
                else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                {
                    sFactory = GlobalVariable.gsFactory;
                }
                else
                {
                    return;
                }
                if (modCommonFunction.ViewGlobalOption(sFactory) == false)
                {
                    return;
                }

                if (ViewLayout() == false)
                {
                    return;
                }

                if (ViewResourceListDetail() == false)
                {
                    return;
                }

                pnlFMBDesign.Select();

                Cursor = Cursors.Default;


                //ChangeMenuText(mnuDesign);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.frmFMBDesign_Load()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

            try
            {
                if (IsLoading == true)
                {

                    IsLoading = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.frmFMBDesign_Activated()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        private void tmrRefresh_Tick(object sender, System.EventArgs e)
        {

            try
            {

                if (IsDesignMode == true)
                {
                    return;
                }
                if (ZoomScale != 0)
                {
                    ZoomScale = 0;
                    DesignSize = OriginalDesignSize;
                }
                string sFactory;
                if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    sFactory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                }
                else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                {
                    sFactory = GlobalVariable.gsFactory;
                }
                else
                {
                    return;
                }
                if (modCommonFunction.ViewGlobalOption(sFactory) == false)
                {
                    return;
                }
                if (RefreshResourceListDetail() == false)
                {
                    return;
                }
                if (SetModifiedControl(false) == false)
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tmrRefresh_Tick()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        #endregion

        #region " Format Functions"

        public udcCtrlBase FindFocusCtrl()
        {

            udcCtrlBase ctrl = null;

            try
            {
                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused == true)
                    {
                        ctrl = (udcCtrlBase)pnlFMBDesign.SelectedControls[i];
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.FindFocusCtrl()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

            return ctrl;

        }

        public bool Format_Lefts()
        {

            try
            {
                int iLeft;
                udcCtrlBase FocusedCtrl = FindFocusCtrl();
                if (FocusedCtrl == null)
                {
                    return false;
                }
                iLeft = FocusedCtrl.GetLocation().X;
                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused == false)
                    {
                        ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetLocation(new Point(iLeft, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().Y), false);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_Lefts()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_Centers()
        {

            try
            {
                int iCenter;
                udcCtrlBase FocusedCtrl = FindFocusCtrl();
                if (FocusedCtrl == null)
                {
                    return false;
                }
                iCenter = FocusedCtrl.GetLocation().X + FocusedCtrl.GetSize().Width / 2;
                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused == false)
                    {
                        ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetLocation(new Point(iCenter - ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Width / 2, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().Y), false);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_Centers()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_Rights()
        {

            try
            {
                int iRight;
                udcCtrlBase FocusedCtrl = FindFocusCtrl();
                if (FocusedCtrl == null)
                {
                    return false;
                }
                iRight = FocusedCtrl.GetLocation().X + FocusedCtrl.GetSize().Width;
                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused == false)
                    {
                        ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetLocation(new Point(iRight - ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Width, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().Y), false);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_Rights()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_Tops()
        {

            try
            {
                int iTop;
                udcCtrlBase FocusedCtrl = FindFocusCtrl();
                if (FocusedCtrl == null)
                {
                    return false;
                }
                iTop = FocusedCtrl.GetLocation().Y;
                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused == false)
                    {
                        ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetLocation(new Point(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().X, iTop), false);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_Tops()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_Middles()
        {

            try
            {
                int iMiddle;
                udcCtrlBase FocusedCtrl = FindFocusCtrl();
                if (FocusedCtrl == null)
                {
                    return false;
                }
                iMiddle = CmnFunction.ToInt(FocusedCtrl.GetLocation().Y + FocusedCtrl.GetSize().Height / 2.0);
                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused == false)
                    {
                        ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetLocation(new Point(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().X, iMiddle - ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Height / 2), false);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_Middles()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_Bottoms()
        {

            try
            {
                int iBottom;
                udcCtrlBase FocusedCtrl = FindFocusCtrl();
                if (FocusedCtrl == null)
                {
                    return false;
                }
                iBottom = FocusedCtrl.GetLocation().Y + FocusedCtrl.GetSize().Height;
                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused == false)
                    {
                        ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetLocation(new Point(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetLocation().X, iBottom - ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Height), false);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_Bottoms()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_Width()
        {

            try
            {
                int iWidth;
                udcCtrlBase FocusedCtrl = FindFocusCtrl();
                if (FocusedCtrl == null)
                {
                    return false;
                }
                iWidth = FocusedCtrl.GetSize().Width;
                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused == false)
                    {
                        ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetSize(new Size(iWidth, ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Height), false);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_Width()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_Height()
        {

            try
            {
                int iHeight;
                udcCtrlBase FocusedCtrl = FindFocusCtrl();
                if (FocusedCtrl == null)
                {
                    return false;
                }
                iHeight = FocusedCtrl.GetSize().Height;
                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused == false)
                    {
                        ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetSize(new Size(((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).GetSize().Width, iHeight), false);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_Height()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_Both()
        {

            try
            {
                int iWidth;
                int iHeight;
                udcCtrlBase FocusedCtrl = FindFocusCtrl();
                if (FocusedCtrl == null)
                {
                    return false;
                }
                iWidth = FocusedCtrl.GetSize().Width;
                iHeight = FocusedCtrl.GetSize().Height;
                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused == false)
                    {
                        ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).SetSize(new Size(iWidth, iHeight), false);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_Both()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_HMakeEqual()
        {

            try
            {
                ArrayList arrCtrl = new ArrayList(pnlFMBDesign.SelectedControls);
                SelectedControlSort comparer = new SelectedControlSort(SelectedControlSort.SortOrder.LEFT_ALL_ORDER);
                arrCtrl.Sort(comparer);

                int iWidth = ((udcCtrlBase)arrCtrl[arrCtrl.Count - 1]).GetLocation().X - ((udcCtrlBase)arrCtrl[0]).GetLocation().X;
                int i;
                for (i = 0; i <= arrCtrl.Count - 2; i++)
                {
                    iWidth -= ((udcCtrlBase)arrCtrl[i]).GetSize().Width;
                }

                iWidth /= arrCtrl.Count - 1;

                if (iWidth < 0)
                {
                    iWidth = 0;
                }

                int iFocusedIndex = 0;
                for (i = 0; i <= arrCtrl.Count - 1; i++)
                {
                    if (((udcCtrlBase)arrCtrl[i]).IsFocused == true)
                    {
                        iFocusedIndex = i;
                        break;
                    }
                }

                for (i = iFocusedIndex - 1; i >= 0; i--)
                {
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point((((udcCtrlBase)arrCtrl[i + 1]).GetLocation().X - iWidth) - ((udcCtrlBase)arrCtrl[i]).GetSize().Width, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y), false);
                }

                if (iFocusedIndex >= arrCtrl.Count - 1)
                {
                    return true;
                }

                for (i = iFocusedIndex + 1; i <= arrCtrl.Count - 1; i++)
                {
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i - 1]).GetLocation().X + ((udcCtrlBase)arrCtrl[i - 1]).GetSize().Width + iWidth, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y), false);
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_HMakeEqual()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_HIncrease()
        {

            try
            {
                ArrayList arrCtrl = new ArrayList(pnlFMBDesign.SelectedControls);
                SelectedControlSort comparer = new SelectedControlSort(SelectedControlSort.SortOrder.LEFT_ALL_ORDER);
                arrCtrl.Sort(comparer);

                int iFocusedIndex = 0;
                int i;
                for (i = 0; i <= arrCtrl.Count - 1; i++)
                {
                    if (((udcCtrlBase)arrCtrl[i]).IsFocused == true)
                    {
                        iFocusedIndex = i;
                        break;
                    }
                }

                int iIncWidth = 0;
                for (i = iFocusedIndex - 1; i >= 0; i--)
                {
                    iIncWidth++;
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X - iIncWidth, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y), false);
                }

                if (iFocusedIndex >= arrCtrl.Count - 1)
                {
                    return true;
                }

                iIncWidth = 0;
                for (i = iFocusedIndex + 1; i <= arrCtrl.Count - 1; i++)
                {
                    iIncWidth++;
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X + iIncWidth, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y), false);
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_HIncrease()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_HDecrease()
        {

            try
            {
                ArrayList arrCtrl = new ArrayList(pnlFMBDesign.SelectedControls);
                SelectedControlSort comparer = new SelectedControlSort(SelectedControlSort.SortOrder.LEFT_ALL_ORDER);
                arrCtrl.Sort(comparer);

                int iFocusedIndex = 0;
                int i;
                for (i = 0; i <= arrCtrl.Count - 1; i++)
                {
                    if (((udcCtrlBase)arrCtrl[i]).IsFocused == true)
                    {
                        iFocusedIndex = i;
                        break;
                    }
                }

                int iDecWidth = 0;
                for (i = iFocusedIndex - 1; i >= 0; i--)
                {
                    if ((((udcCtrlBase)arrCtrl[i]).GetLocation().X + ((udcCtrlBase)arrCtrl[i]).GetSize().Width / 2) != (((udcCtrlBase)arrCtrl[i + 1]).GetLocation().X + ((udcCtrlBase)arrCtrl[i + 1]).GetSize().Width / 2))
                    {
                        iDecWidth++;
                        ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X + iDecWidth, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y), false);
                    }
                }

                if (iFocusedIndex >= arrCtrl.Count - 1)
                {
                    return true;
                }

                iDecWidth = 0;
                for (i = iFocusedIndex + 1; i <= arrCtrl.Count - 1; i++)
                {
                    if ((((udcCtrlBase)arrCtrl[i]).GetLocation().X + ((udcCtrlBase)arrCtrl[i]).GetSize().Width / 2) != (((udcCtrlBase)arrCtrl[i - 1]).GetLocation().X + ((udcCtrlBase)arrCtrl[i - 1]).GetSize().Width / 2))
                    {
                        iDecWidth++;
                        ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X - iDecWidth, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y), false);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_HDecrease()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_HRemove()
        {

            try
            {
                ArrayList arrCtrl = new ArrayList(pnlFMBDesign.SelectedControls);
                SelectedControlSort comparer = new SelectedControlSort(SelectedControlSort.SortOrder.LEFT_ALL_ORDER);
                arrCtrl.Sort(comparer);

                int i;
                int iFocusedIndex = 0;
                for (i = 0; i <= arrCtrl.Count - 1; i++)
                {
                    if (((udcCtrlBase)arrCtrl[i]).IsFocused == true)
                    {
                        iFocusedIndex = i;
                        break;
                    }
                }

                for (i = iFocusedIndex - 1; i >= 0; i--)
                {
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i + 1]).GetLocation().X - ((udcCtrlBase)arrCtrl[i]).GetSize().Width, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y), false);
                }

                if (iFocusedIndex >= arrCtrl.Count - 1)
                {
                    return true;
                }

                for (i = iFocusedIndex + 1; i <= arrCtrl.Count - 1; i++)
                {
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i - 1]).GetLocation().X + ((udcCtrlBase)arrCtrl[i - 1]).GetSize().Width, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y), false);
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_HRemove()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_VMakeEqual()
        {

            try
            {
                ArrayList arrCtrl = new ArrayList(pnlFMBDesign.SelectedControls);
                SelectedControlSort comparer = new SelectedControlSort(SelectedControlSort.SortOrder.TOP_ALL_ORDER);
                arrCtrl.Sort(comparer);

                int iHeight = ((udcCtrlBase)arrCtrl[arrCtrl.Count - 1]).GetLocation().Y - ((udcCtrlBase)arrCtrl[0]).GetLocation().Y;
                int i;
                for (i = 0; i <= arrCtrl.Count - 2; i++)
                {
                    iHeight -= ((udcCtrlBase)arrCtrl[i]).GetSize().Height;
                }

                iHeight /= arrCtrl.Count - 1;

                if (iHeight < 0)
                {
                    iHeight = 0;
                }

                int iFocusedIndex = 0;
                for (i = 0; i <= arrCtrl.Count - 1; i++)
                {
                    if (((udcCtrlBase)arrCtrl[i]).IsFocused == true)
                    {
                        iFocusedIndex = i;
                        break;
                    }
                }

                for (i = iFocusedIndex - 1; i >= 0; i--)
                {
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X, (((udcCtrlBase)arrCtrl[i + 1]).GetLocation().Y - iHeight) - ((udcCtrlBase)arrCtrl[i]).GetSize().Height), false);
                }

                if (iFocusedIndex >= arrCtrl.Count - 1)
                {
                    return true;
                }

                for (i = iFocusedIndex + 1; i <= arrCtrl.Count - 1; i++)
                {
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X, ((udcCtrlBase)arrCtrl[i - 1]).GetLocation().Y + ((udcCtrlBase)arrCtrl[i - 1]).GetSize().Height + iHeight), false);
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_VMakeEqual()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_VIncrease()
        {

            try
            {
                ArrayList arrCtrl = new ArrayList(pnlFMBDesign.SelectedControls);
                SelectedControlSort comparer = new SelectedControlSort(SelectedControlSort.SortOrder.TOP_ALL_ORDER);
                arrCtrl.Sort(comparer);

                int iFocusedIndex = 0;
                int i;
                for (i = 0; i <= arrCtrl.Count - 1; i++)
                {
                    if (((udcCtrlBase)arrCtrl[i]).IsFocused == true)
                    {
                        iFocusedIndex = i;
                        break;
                    }
                }

                int iIncHeight = 0;
                for (i = iFocusedIndex - 1; i >= 0; i--)
                {
                    iIncHeight++;
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y - iIncHeight), false);
                }

                if (iFocusedIndex >= arrCtrl.Count - 1)
                {
                    return true;
                }

                iIncHeight = 0;
                for (i = iFocusedIndex + 1; i <= arrCtrl.Count - 1; i++)
                {
                    iIncHeight++;
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y + iIncHeight), false);
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_VIncrease()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_VDecrease()
        {

            try
            {
                ArrayList arrCtrl = new ArrayList(pnlFMBDesign.SelectedControls);
                SelectedControlSort comparer = new SelectedControlSort(SelectedControlSort.SortOrder.TOP_ALL_ORDER);
                arrCtrl.Sort(comparer);

                int iFocusedIndex = 0;
                int i;
                for (i = 0; i <= arrCtrl.Count - 1; i++)
                {
                    if (((udcCtrlBase)arrCtrl[i]).IsFocused == true)
                    {
                        iFocusedIndex = i;
                        break;
                    }
                }

                int iDecHeight = 0;
                for (i = iFocusedIndex - 1; i >= 0; i--)
                {
                    if ((((udcCtrlBase)arrCtrl[i]).GetLocation().Y + ((udcCtrlBase)arrCtrl[i]).GetSize().Height / 2) != (((udcCtrlBase)arrCtrl[i + 1]).GetLocation().Y + ((udcCtrlBase)arrCtrl[i + 1]).GetSize().Height / 2))
                    {
                        iDecHeight++;
                        ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y + iDecHeight), false);
                    }
                }

                if (iFocusedIndex >= arrCtrl.Count - 1)
                {
                    return true;
                }

                iDecHeight = 0;
                for (i = iFocusedIndex + 1; i <= arrCtrl.Count - 1; i++)
                {
                    if ((((udcCtrlBase)arrCtrl[i]).GetLocation().Y + ((udcCtrlBase)arrCtrl[i]).GetSize().Height / 2) != (((udcCtrlBase)arrCtrl[i - 1]).GetLocation().Y + ((udcCtrlBase)arrCtrl[i - 1]).GetSize().Height / 2))
                    {
                        iDecHeight++;
                        ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X, ((udcCtrlBase)arrCtrl[i]).GetLocation().Y - iDecHeight), false);
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_VDecrease()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_VRemove()
        {

            try
            {
                ArrayList arrCtrl = new ArrayList(pnlFMBDesign.SelectedControls);
                SelectedControlSort comparer = new SelectedControlSort(SelectedControlSort.SortOrder.TOP_ALL_ORDER);
                arrCtrl.Sort(comparer);

                int i;
                int iFocusedIndex = 0;
                for (i = 0; i <= arrCtrl.Count - 1; i++)
                {
                    if (((udcCtrlBase)arrCtrl[i]).IsFocused == true)
                    {
                        iFocusedIndex = i;
                        break;
                    }
                }

                for (i = iFocusedIndex - 1; i >= 0; i--)
                {
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X, (((udcCtrlBase)arrCtrl[i + 1]).GetLocation().Y) - ((udcCtrlBase)arrCtrl[i]).GetSize().Height), false);
                }

                if (iFocusedIndex >= arrCtrl.Count - 1)
                {
                    return true;
                }

                for (i = iFocusedIndex + 1; i <= arrCtrl.Count - 1; i++)
                {
                    ((udcCtrlBase)arrCtrl[i]).SetLocation(new Point(((udcCtrlBase)arrCtrl[i]).GetLocation().X, ((udcCtrlBase)arrCtrl[i - 1]).GetLocation().Y + ((udcCtrlBase)arrCtrl[i - 1]).GetSize().Height), false);
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_VMakeEqual()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_BringToFront()
        {

            try
            {
                FMB_Resource_Priority_In_Tag Resource_Priority_In = new FMB_Resource_Priority_In_Tag();
                FMB_UDR_Priority_In_Tag UDR_Priority_In = new FMB_UDR_Priority_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();

                Control ctrl;
                foreach (Control tempLoopVar_ctrl in this.pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        if (((udcCtrlBase)ctrl).IsSelected == true)
                        {
                            ((udcCtrlBase)ctrl).BringToFront();
                            if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                            {
                                Resource_Priority_In._C.h_proc_step = '1';
                                Resource_Priority_In._C.h_passport = GlobalVariable.gsPassport;
                                Resource_Priority_In._C.h_language = GlobalVariable.gcLanguage;
                                Resource_Priority_In._C.h_user_id = GlobalVariable.gsUserID;
                                Resource_Priority_In._C.h_password = GlobalVariable.gsPassword;
                                Resource_Priority_In._C.h_factory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                                Resource_Priority_In._C.layout_id = modCommonFunction.GetStringBySeperator(Name, ":", 2);
                                if (((udcCtrlBase)ctrl).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                                {
                                    Resource_Priority_In._C.res_type = modGlobalConstant.FMB_RESOURCE_TYPE;
                                }
                                else
                                {
                                    Resource_Priority_In._C.res_type = modGlobalConstant.FMB_TAG_TYPE;
                                }
                                Resource_Priority_In._C.res_id = ((udcCtrlBase)ctrl).Name;

                                if (FMBSender.FMB_Resource_Priority(Resource_Priority_In, ref Cmn_Out) == false)
                                {
                                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                                    return false;
                                }

                                if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                                {
                                    CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                                    return false;
                                }
                            }
                            else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                            {
                                UDR_Priority_In._C.h_proc_step = '1';
                                UDR_Priority_In._C.h_passport = GlobalVariable.gsPassport;
                                UDR_Priority_In._C.h_language = GlobalVariable.gcLanguage;
                                UDR_Priority_In._C.h_user_id = GlobalVariable.gsUserID;
                                UDR_Priority_In._C.h_password = GlobalVariable.gsPassword;
                                UDR_Priority_In._C.h_factory = GlobalVariable.gsFactory;

                                UDR_Priority_In._C.group = Name;
                                if (((udcCtrlBase)ctrl).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                                {
                                    UDR_Priority_In._C.res_type = modGlobalConstant.FMB_RESOURCE_TYPE;
                                }
                                else
                                {
                                    UDR_Priority_In._C.res_type = modGlobalConstant.FMB_TAG_TYPE;
                                }
                                UDR_Priority_In._C.res_id = ((udcCtrlBase)ctrl).Name;

                                if (FMBSender.FMB_UDR_Priority(UDR_Priority_In, ref Cmn_Out) == false)
                                {
                                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                                    return false;
                                }

                                if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                                {
                                    CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                                    return false;
                                }
                            }
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_BringToFront()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public bool Format_SendToBack()
        {

            try
            {
                FMB_Resource_Priority_In_Tag Resource_Priority_In = new FMB_Resource_Priority_In_Tag();
                FMB_UDR_Priority_In_Tag UDR_Priority_In = new FMB_UDR_Priority_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();


                Control ctrl;
                foreach (Control tempLoopVar_ctrl in this.pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        if (((udcCtrlBase)ctrl).IsSelected == true)
                        {
                            ((udcCtrlBase)ctrl).SendToBack();
                            if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                            {
                                Resource_Priority_In._C.h_proc_step = '2';
                                Resource_Priority_In._C.h_passport = GlobalVariable.gsPassport;
                                Resource_Priority_In._C.h_language = GlobalVariable.gcLanguage;
                                Resource_Priority_In._C.h_user_id = GlobalVariable.gsUserID;
                                Resource_Priority_In._C.h_password = GlobalVariable.gsPassword;
                                Resource_Priority_In._C.h_factory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                                Resource_Priority_In._C.layout_id = modCommonFunction.GetStringBySeperator(Name, ":", 2);
                                if (((udcCtrlBase)ctrl).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                                {
                                    Resource_Priority_In._C.res_type = modGlobalConstant.FMB_RESOURCE_TYPE;
                                }
                                else
                                {
                                    Resource_Priority_In._C.res_type = modGlobalConstant.FMB_TAG_TYPE;
                                }
                                Resource_Priority_In._C.res_id = ((udcCtrlBase)ctrl).Name;


                                if (FMBSender.FMB_Resource_Priority(Resource_Priority_In, ref Cmn_Out) == false)
                                {
                                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                                    return false;
                                }

                                if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                                {
                                    CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                                    return false;
                                }
                            }
                            else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                            {
                                UDR_Priority_In._C.h_proc_step = '2';
                                UDR_Priority_In._C.h_passport = GlobalVariable.gsPassport;
                                UDR_Priority_In._C.h_language = GlobalVariable.gcLanguage;
                                UDR_Priority_In._C.h_user_id = GlobalVariable.gsUserID;
                                UDR_Priority_In._C.h_password = GlobalVariable.gsPassword;
                                UDR_Priority_In._C.h_factory = GlobalVariable.gsFactory;

                                UDR_Priority_In._C.group = Name;
                                if (((udcCtrlBase)ctrl).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                                {
                                    UDR_Priority_In._C.res_type = modGlobalConstant.FMB_RESOURCE_TYPE;
                                }
                                else
                                {
                                    UDR_Priority_In._C.res_type = modGlobalConstant.FMB_TAG_TYPE;
                                }
                                UDR_Priority_In._C.res_id = ((udcCtrlBase)ctrl).Name;

                                if (FMBSender.FMB_UDR_Priority(UDR_Priority_In, ref Cmn_Out) == false)
                                {
                                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                                    return false;
                                }

                                if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                                {
                                    CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                                    return false;
                                }
                            }

                            return true;
                        }
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.Format_SendToBack()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        #endregion

        #region " Functions Implementations"

        public void DebugWrite(udcCtrlBase ctrl)
        {

            Debug.WriteLine(ctrl.Name + " : IsHot - " + ctrl.IsHot + ", IsSelected - " + ctrl.IsSelected + ", IsFocused - " + ctrl.IsFocused);
            int i;
            for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
            {
                Debug.WriteLine("[" + i + "]" + ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).Name + " : IsHot - " + ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsHot + ", IsSelected - " + ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsSelected + ", IsFocused - " + ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).IsFocused);
            }

        }
        public bool RefreshControlEvent(Control.ControlCollection CtrlCollection, ref FMBUI.clsCtrlStatus ResourceStatus, int iStep)
        {

            try
            {
                IAsyncResult r = BeginInvoke(_RefreshControlDelegate, new object[] { CtrlCollection, ResourceStatus, iStep });
                bool bReturn = (bool)EndInvoke(r);

                return bReturn;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.RefreshControl()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }
        // RefreshControl()
        //       - Refresh Control Status
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		- ByVal ResourceStatus As clsCtrlStatus : Control to refresh
        //
        private static bool RefreshControl(Control.ControlCollection CtrlCollection, ref FMBUI.clsCtrlStatus ResourceStatus, int iStep)
        {

            try
            {
                udcCtrlBase ctrl = modCommonFunction.GetControl(CtrlCollection, ResourceStatus.Key, ResourceStatus.ToolType);
                if (ctrl == null)
                {
                    return false;
                }

                if (ResourceStatus.IsDeleteRes == true)
                {
                    CtrlCollection.Remove(ctrl);
                    return true;
                }

                ctrl.IsRefreshed = true;

                ctrl.SetCtrlStatusData(ResourceStatus, iStep, false);

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.RefreshControl()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // ChangeMyMenus()
        //       - Change Language of Menu
        // Return Value
        //       -
        // Arguments
        //		- ByRef utbManager As UltraToolbarsManager
        //
        private void ChangeMyMenus(UltraToolbarsManager utbManager)
        {

            try
            {
                //object menu;
                if (utbManager == null)
                {
                    return;
                }
                //foreach (object tempLoopVar_menu in utbManager.Tools)
                foreach (Infragistics.Win.UltraWinToolbars.ToolBase menu in utbManager.Tools)
                {
                    //menu = tempLoopVar_menu;
                    //Menu.SharedProps.Caption = modLanguageFunction.FindLanguage(Menu.SharedProps.Caption, 2);
                    menu.SharedProps.Caption = modLanguageFunction.FindLanguage(menu.SharedProps.Caption, 2);

                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.ChangeMyMenus()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // ViewLayout()
        //       - View Layout
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        public bool ViewLayout()
        {

            try
            {
                FMB_View_LayOut_In_Tag FMB_View_LayOut_In = new FMB_View_LayOut_In_Tag();
                FMB_View_LayOut_Out_Tag FMB_View_LayOut_Out = new FMB_View_LayOut_Out_Tag();
                FMB_View_UDR_Group_In_Tag FMB_View_UDR_Group_In = new FMB_View_UDR_Group_In_Tag();
                FMB_View_UDR_Group_Out_Tag FMB_View_UDR_Group_Out = new FMB_View_UDR_Group_Out_Tag();

                if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    FMB_View_LayOut_In.h_proc_step = '1';
                    FMB_View_LayOut_In.h_passport = GlobalVariable.gsPassport;
                    FMB_View_LayOut_In.h_language = GlobalVariable.gcLanguage;
                    FMB_View_LayOut_In.h_user_id = GlobalVariable.gsUserID;
                    FMB_View_LayOut_In.h_password = GlobalVariable.gsPassword;
                    FMB_View_LayOut_In.h_factory = modCommonFunction.GetStringBySeperator(Name, ":", 1);

                    FMB_View_LayOut_In.layout_id = modCommonFunction.GetStringBySeperator(Name, ":", 2);

                    if (FMBSender.FMB_View_LayOut(FMB_View_LayOut_In, ref FMB_View_LayOut_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }

                    if (FMB_View_LayOut_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(FMB_View_LayOut_Out.h_msg_code, FMB_View_LayOut_Out.h_msg, FMB_View_LayOut_Out.h_db_err_msg, FMB_View_LayOut_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                        return false;
                    }

                    DesignSize = new Size(FMB_View_LayOut_Out.width + 20, FMB_View_LayOut_Out.height + 20);
                    OriginalDesignSize = DesignSize;

                }
                else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                {
                    FMB_View_UDR_Group_In.h_proc_step = '1';
                    FMB_View_UDR_Group_In.h_passport = GlobalVariable.gsPassport;
                    FMB_View_UDR_Group_In.h_language = GlobalVariable.gcLanguage;
                    FMB_View_UDR_Group_In.h_user_id = GlobalVariable.gsUserID;
                    FMB_View_UDR_Group_In.h_password = GlobalVariable.gsPassword;
                    FMB_View_UDR_Group_In.h_factory = GlobalVariable.gsFactory;

                    FMB_View_UDR_Group_In.group_id = Name;

                    if (FMBSender.FMB_View_UDR_Group(FMB_View_UDR_Group_In, ref FMB_View_UDR_Group_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }

                    if (FMB_View_UDR_Group_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(FMB_View_UDR_Group_Out.h_msg_code, FMB_View_UDR_Group_Out.h_msg, FMB_View_UDR_Group_Out.h_db_err_msg, FMB_View_UDR_Group_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                        return false;
                    }

                    DesignSize = new Size(FMB_View_UDR_Group_Out.width + 20, FMB_View_UDR_Group_Out.height + 20);
                    OriginalDesignSize = DesignSize;
                }
                else
                {
                    CmnFunction.ShowMsgBox("ViewLayout() Failed.", "FMB Client", MessageBoxButtons.OK, 1);
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.ViewLayout()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // ViewResourceListDetail()
        //       - View Resource/Tag List Detail
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        public bool ViewResourceListDetail()
        {

            try
            {
                FMB_View_Resource_List_In_Tag FMB_View_Resource_List_In_Detail = new FMB_View_Resource_List_In_Tag();
                FMB_View_Resource_List_Out_Detail_Tag FMB_View_Resource_List_Out_Detail = new FMB_View_Resource_List_Out_Detail_Tag();
                FMB_View_UDR_Resource_List_In_Tag FMB_View_UDR_Resource_List_In_Detail = new FMB_View_UDR_Resource_List_In_Tag();
                FMB_View_UDR_Resource_List_Out_Detail_Tag FMB_View_UDR_Resource_List_Out_Detail = new FMB_View_UDR_Resource_List_Out_Detail_Tag();
                int i;
                //string sKey = "";
                //string sText = "";
                //Point ptLocation;
                //Size szRegion;
                string sFactory = "";

                this.pnlFMBDesign.SuspendLayout();

                if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    sFactory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                }
                else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                {
                    sFactory = GlobalVariable.gsFactory;
                }

                if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    FMB_View_Resource_List_In_Detail.h_proc_step = '2';
                    FMB_View_Resource_List_In_Detail.h_passport = GlobalVariable.gsPassport;
                    FMB_View_Resource_List_In_Detail.h_language = GlobalVariable.gcLanguage;
                    FMB_View_Resource_List_In_Detail.h_user_id = GlobalVariable.gsUserID;
                    FMB_View_Resource_List_In_Detail.h_password = GlobalVariable.gsPassword;
                    FMB_View_Resource_List_In_Detail.h_factory = sFactory;

                    FMB_View_Resource_List_In_Detail.res_type = ' ';
                    FMB_View_Resource_List_In_Detail.include_del_res = ' ';
                    FMB_View_Resource_List_In_Detail.include_proc_lot_info = ' ';

                    FMB_View_Resource_List_In_Detail.layout_id = modCommonFunction.GetStringBySeperator(Name, ":", 2);
                    FMB_View_Resource_List_In_Detail.next_seq = int.MaxValue;

                    do
                    {
                        if (FMBSender.FMB_View_Resource_List_Detail(FMB_View_Resource_List_In_Detail, ref FMB_View_Resource_List_Out_Detail) == false)
                        {
                            CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                            return false;
                        }

                        if (FMB_View_Resource_List_Out_Detail.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                        {
                            CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(FMB_View_Resource_List_Out_Detail.h_msg_code, FMB_View_Resource_List_Out_Detail.h_msg, FMB_View_Resource_List_Out_Detail.h_db_err_msg, FMB_View_Resource_List_Out_Detail.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                            return false;
                        }

                        for (i = 0; i <= FMB_View_Resource_List_Out_Detail.count - 1; i++)
                        {
                            clsCtrlStatus ResourceStatus = new clsCtrlStatus();
                            ResourceStatus.Key = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_id);
                            ResourceStatus.SetLocation(new Point(CmnFunction.ToInt(FMB_View_Resource_List_Out_Detail.res_list[i].loc_x), CmnFunction.ToInt(FMB_View_Resource_List_Out_Detail.res_list[i].loc_y)));
                            ResourceStatus.SetSize(new Size(CmnFunction.ToInt(FMB_View_Resource_List_Out_Detail.res_list[i].loc_width), CmnFunction.ToInt(FMB_View_Resource_List_Out_Detail.res_list[i].loc_height)));
                            ResourceStatus.Text = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].text);
                            ResourceStatus.TextColor = FMB_View_Resource_List_Out_Detail.res_list[i].text_color;
                            ResourceStatus.TextFontName = System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultFontName));
                            ResourceStatus.TextSize = FMB_View_Resource_List_Out_Detail.res_list[i].text_size;
                            ResourceStatus.ResTagFlag = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_tag_flag);
                            if (CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].text_style) == "")
                            {
                                ResourceStatus.TextStyle = -1;
                            }
                            else
                            {
                                ResourceStatus.TextStyle = FMB_View_Resource_List_Out_Detail.res_list[i].text_style - '0';
                            }
                            ResourceStatus.BackColor = FMB_View_Resource_List_Out_Detail.res_list[i].back_color;
                            ResourceStatus.ToolType = (Enums.eToolType)FMB_View_Resource_List_Out_Detail.res_list[i].tag_type;
                            if (ResourceStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                            {
                                ResourceStatus.LastEvent = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].last_event);
                                ResourceStatus.PrimaryStatus = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_pri_sts);
                                ResourceStatus.ProcMode = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_proc_mode);
                                ResourceStatus.CtrlMode = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_ctrl_mode);
                                ResourceStatus.UpDownFlag = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_up_down_flag);
                                ResourceStatus.ResourceType = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_type);
                                ResourceStatus.AreaID = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].area_id);
                                ResourceStatus.SubAreaID = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].sub_area_id);
                                if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.IsProcessMode)) == "P")
                                {
                                    ResourceStatus.IsProcessMode = true;
                                }
                                else
                                {
                                    ResourceStatus.IsProcessMode = false;
                                }
                                if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.UseEventColor)) == "Y")
                                {
                                    ResourceStatus.IsUseEventColor = true;
                                    ResourceStatus.EventBackColor = modGlobalVariable.gGlobalOptions.GetOptions(sFactory, ResourceStatus.LastEvent).ToArgb();
                                }
                                else
                                {
                                    ResourceStatus.IsUseEventColor = false;
                                }
                                ResourceStatus.ImageIndex = FMB_View_Resource_List_Out_Detail.res_list[i].img_idx;
                                if (CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].signal_flag) == "Y")
                                {
                                    ResourceStatus.IsViewSignal = true;
                                }
                                else
                                {
                                    ResourceStatus.IsViewSignal = false;
                                }
                            }
                            else
                            {
                                if (CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].no_mouse_event) == "Y")
                                {
                                    ResourceStatus.IsNoEvent = true;
                                }
                                else
                                {
                                    ResourceStatus.IsNoEvent = false;
                                }
                            }
                            if (AddControl(ResourceStatus, false, false) == false)
                            {
                                return false;
                            }
                        }

                        FMB_View_Resource_List_In_Detail.next_seq = FMB_View_Resource_List_Out_Detail.next_seq;
                    } while (FMB_View_Resource_List_In_Detail.next_seq != 0);

                }
                else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                {
                    FMB_View_UDR_Resource_List_In_Detail.h_proc_step = '2';
                    FMB_View_UDR_Resource_List_In_Detail.h_passport = GlobalVariable.gsPassport;
                    FMB_View_UDR_Resource_List_In_Detail.h_language = GlobalVariable.gcLanguage;
                    FMB_View_UDR_Resource_List_In_Detail.h_user_id = GlobalVariable.gsUserID;
                    FMB_View_UDR_Resource_List_In_Detail.h_password = GlobalVariable.gsPassword;
                    FMB_View_UDR_Resource_List_In_Detail.h_factory = sFactory;

                    FMB_View_UDR_Resource_List_In_Detail.res_type = ' ';

                    FMB_View_UDR_Resource_List_In_Detail.group = Name;
                    FMB_View_UDR_Resource_List_In_Detail.next_seq = int.MaxValue;

                    do
                    {

                        if (FMBSender.FMB_View_UDR_Resource_List_Detail(FMB_View_UDR_Resource_List_In_Detail, ref FMB_View_UDR_Resource_List_Out_Detail) == false)
                        {
                            CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                            return false;
                        }

                        if (FMB_View_UDR_Resource_List_Out_Detail.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                        {
                            CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(FMB_View_UDR_Resource_List_Out_Detail.h_msg_code, FMB_View_UDR_Resource_List_Out_Detail.h_msg, FMB_View_UDR_Resource_List_Out_Detail.h_db_err_msg, FMB_View_UDR_Resource_List_Out_Detail.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                            return false;
                        }

                        for (i = 0; i <= FMB_View_UDR_Resource_List_Out_Detail.count - 1; i++)
                        {
                            clsCtrlStatus ResourceStatus = new clsCtrlStatus();
                            ResourceStatus.Key = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_id);
                            ResourceStatus.SetLocation(new Point(CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].loc_x), CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].loc_y)));
                            ResourceStatus.SetSize(new Size(CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].loc_width), CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].loc_height)));
                            ResourceStatus.Text = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].text);
                            ResourceStatus.TextColor = FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].text_color;
                            ResourceStatus.TextFontName = System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultFontName));
                            ResourceStatus.TextSize = FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].text_size;
                            ResourceStatus.ResTagFlag = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_tag_flag);
                            if (CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].text_style) == "")
                            {
                                ResourceStatus.TextStyle = -1;
                            }
                            else
                            {
                                ResourceStatus.TextStyle = CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].text_style.ToString());
                            }
                            ResourceStatus.BackColor = FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].back_color;
                            ResourceStatus.ToolType = (Enums.eToolType)FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].tag_type;
                            if (ResourceStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                            {
                                ResourceStatus.LastEvent = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].last_event);
                                ResourceStatus.PrimaryStatus = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_pri_sts);
                                ResourceStatus.ProcMode = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_proc_mode);
                                ResourceStatus.CtrlMode = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_ctrl_mode);
                                ResourceStatus.UpDownFlag = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_up_down_flag);
                                ResourceStatus.ResourceType = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_type);
                                ResourceStatus.AreaID = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].area_id);
                                ResourceStatus.SubAreaID = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].sub_area_id);
                                if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.IsProcessMode)) == "P")
                                {
                                    ResourceStatus.IsProcessMode = true;
                                }
                                else
                                {
                                    ResourceStatus.IsProcessMode = false;
                                }
                                if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.UseEventColor)) == "Y")
                                {
                                    ResourceStatus.IsUseEventColor = true;
                                    ResourceStatus.EventBackColor = modGlobalVariable.gGlobalOptions.GetOptions(sFactory, ResourceStatus.LastEvent).ToArgb();
                                }
                                else
                                {
                                    ResourceStatus.IsUseEventColor = false;
                                }
                                ResourceStatus.ImageIndex = CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].img_idx);
                                if (CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].signal_flag) == "Y")
                                {
                                    ResourceStatus.IsViewSignal = true;
                                }
                                else
                                {
                                    ResourceStatus.IsViewSignal = false;
                                }

                            }
                            else
                            {
                                if (CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].no_mouse_event) == "Y")
                                {
                                    ResourceStatus.IsNoEvent = true;
                                }
                                else
                                {
                                    ResourceStatus.IsNoEvent = false;
                                }
                            }
                            if (AddControl(ResourceStatus, false, false) == false)
                            {
                                return false;
                            }
                        }

                        FMB_View_UDR_Resource_List_In_Detail.next_seq = FMB_View_UDR_Resource_List_Out_Detail.next_seq;
                    } while (FMB_View_UDR_Resource_List_In_Detail.next_seq != 0);

                }
                else
                {
                    CmnFunction.ShowMsgBox("ViewResourceListDetail() Failed.", "FMB Client", MessageBoxButtons.OK, 1);
                    return false;
                }

                this.pnlFMBDesign.ResumeLayout(false);

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.ViewResourceListDetail()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // RefreshResourceListDetail()
        //       - Refresh Resource/Tag List Detail
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        public bool RefreshResourceListDetail()
        {

            try
            {
                FMB_View_Resource_List_In_Tag FMB_View_Resource_List_In_Detail = new FMB_View_Resource_List_In_Tag();
                FMB_View_Resource_List_Out_Detail_Tag FMB_View_Resource_List_Out_Detail = new FMB_View_Resource_List_Out_Detail_Tag();
                FMB_View_UDR_Resource_List_In_Tag FMB_View_UDR_Resource_List_In_Detail = new FMB_View_UDR_Resource_List_In_Tag();
                FMB_View_UDR_Resource_List_Out_Detail_Tag FMB_View_UDR_Resource_List_Out_Detail = new FMB_View_UDR_Resource_List_Out_Detail_Tag();
                int i;
                //string sKey = "";
                //string sText = "";
                //Point ptLocation;
                //Size szRegion;
                string sFactory = "";

                this.pnlFMBDesign.SuspendLayout();

                if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    sFactory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                }
                else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                {
                    sFactory = GlobalVariable.gsFactory;
                }

                if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    FMB_View_Resource_List_In_Detail.h_proc_step = '2';
                    FMB_View_Resource_List_In_Detail.h_passport = GlobalVariable.gsPassport;
                    FMB_View_Resource_List_In_Detail.h_language = GlobalVariable.gcLanguage;
                    FMB_View_Resource_List_In_Detail.h_user_id = GlobalVariable.gsUserID;
                    FMB_View_Resource_List_In_Detail.h_password = GlobalVariable.gsPassword;
                    FMB_View_Resource_List_In_Detail.h_factory = sFactory;

                    FMB_View_Resource_List_In_Detail.layout_id = modCommonFunction.GetStringBySeperator(Name, ":", 2);
                    FMB_View_Resource_List_In_Detail.next_seq = int.MaxValue;

                    do
                    {

                        if (FMBSender.FMB_View_Resource_List_Detail(FMB_View_Resource_List_In_Detail, ref FMB_View_Resource_List_Out_Detail) == false)
                        {
                            CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                            return false;
                        }

                        if (FMB_View_Resource_List_Out_Detail.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                        {
                            CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(FMB_View_Resource_List_Out_Detail.h_msg_code, FMB_View_Resource_List_Out_Detail.h_msg, FMB_View_Resource_List_Out_Detail.h_db_err_msg, FMB_View_Resource_List_Out_Detail.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                            return false;
                        }

                        for (i = 0; i <= FMB_View_Resource_List_Out_Detail.count - 1; i++)
                        {
                            clsCtrlStatus ResourceStatus = new clsCtrlStatus();
                            ResourceStatus.Key = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_id);
                            ResourceStatus.SetLocation(new Point(CmnFunction.ToInt(FMB_View_Resource_List_Out_Detail.res_list[i].loc_x), CmnFunction.ToInt(FMB_View_Resource_List_Out_Detail.res_list[i].loc_y)));
                            ResourceStatus.SetSize(new Size(CmnFunction.ToInt(FMB_View_Resource_List_Out_Detail.res_list[i].loc_width), CmnFunction.ToInt(FMB_View_Resource_List_Out_Detail.res_list[i].loc_height)));
                            ResourceStatus.Text = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].text);
                            ResourceStatus.ResTagFlag = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_tag_flag);
                            ResourceStatus.TextColor = FMB_View_Resource_List_Out_Detail.res_list[i].text_color;
                            ResourceStatus.TextFontName = System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultFontName));
                            ResourceStatus.TextSize = CmnFunction.ToInt(FMB_View_Resource_List_Out_Detail.res_list[i].text_size);
                            if (CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].text_style) == "")
                            {
                                ResourceStatus.TextStyle = -1;
                            }
                            else
                            {
                                ResourceStatus.TextStyle = CmnFunction.ToInt(FMB_View_Resource_List_Out_Detail.res_list[i].text_style.ToString());
                            }
                            ResourceStatus.BackColor = FMB_View_Resource_List_Out_Detail.res_list[i].back_color;
                            ResourceStatus.ToolType = (Enums.eToolType)FMB_View_Resource_List_Out_Detail.res_list[i].tag_type;
                            ResourceStatus.ZoomScale = 0;
                            if (ResourceStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                            {
                                ResourceStatus.LastEvent = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].last_event);
                                ResourceStatus.PrimaryStatus = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_pri_sts);
                                ResourceStatus.ProcMode = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_proc_mode);
                                ResourceStatus.CtrlMode = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_ctrl_mode);
                                ResourceStatus.UpDownFlag = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_up_down_flag);
                                ResourceStatus.ResourceType = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].res_type);
                                ResourceStatus.AreaID = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].area_id);
                                ResourceStatus.SubAreaID = CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].sub_area_id);
                                if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.IsProcessMode)) == "P")
                                {
                                    ResourceStatus.IsProcessMode = true;
                                }
                                else
                                {
                                    ResourceStatus.IsProcessMode = false;
                                }
                                if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.UseEventColor)) == "Y")
                                {
                                    ResourceStatus.IsUseEventColor = true;
                                    ResourceStatus.EventBackColor = modGlobalVariable.gGlobalOptions.GetOptions(sFactory, ResourceStatus.LastEvent).ToArgb();
                                }
                                else
                                {
                                    ResourceStatus.IsUseEventColor = false;
                                }
                                ResourceStatus.ImageIndex = FMB_View_Resource_List_Out_Detail.res_list[i].img_idx;
                                if (CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].signal_flag) == "Y")
                                {
                                    ResourceStatus.IsViewSignal = true;
                                }
                                else
                                {
                                    ResourceStatus.IsViewSignal = false;
                                }
                            }
                            else
                            {
                                if (CmnFunction.Trim(FMB_View_Resource_List_Out_Detail.res_list[i].no_mouse_event) == "Y")
                                {
                                    ResourceStatus.IsNoEvent = true;
                                }
                                else
                                {
                                    ResourceStatus.IsNoEvent = false;
                                }
                            }
                            if (RefreshControl(this.pnlFMBDesign.Controls, ref ResourceStatus, 1) == false)
                            {
                                if (AddControl(ResourceStatus, false, true) == false)
                                {
                                    return false;
                                }
                            }
                        }

                        FMB_View_Resource_List_In_Detail.next_seq = FMB_View_Resource_List_Out_Detail.next_seq;
                    } while (FMB_View_Resource_List_In_Detail.next_seq != 0);

                }
                else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                {
                    FMB_View_UDR_Resource_List_In_Detail.h_proc_step = '2';
                    FMB_View_UDR_Resource_List_In_Detail.h_passport = GlobalVariable.gsPassport;
                    FMB_View_UDR_Resource_List_In_Detail.h_language = GlobalVariable.gcLanguage;
                    FMB_View_UDR_Resource_List_In_Detail.h_user_id = GlobalVariable.gsUserID;
                    FMB_View_UDR_Resource_List_In_Detail.h_password = GlobalVariable.gsPassword;
                    FMB_View_UDR_Resource_List_In_Detail.h_factory = sFactory;

                    FMB_View_UDR_Resource_List_In_Detail.group = Name;
                    FMB_View_UDR_Resource_List_In_Detail.next_seq = int.MaxValue;

                    do
                    {

                        if (FMBSender.FMB_View_UDR_Resource_List_Detail(FMB_View_UDR_Resource_List_In_Detail, ref FMB_View_UDR_Resource_List_Out_Detail) == false)
                        {
                            CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                            return false;
                        }

                        if (FMB_View_UDR_Resource_List_Out_Detail.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                        {
                            CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(FMB_View_UDR_Resource_List_Out_Detail.h_msg_code, FMB_View_UDR_Resource_List_Out_Detail.h_msg, FMB_View_UDR_Resource_List_Out_Detail.h_db_err_msg, FMB_View_UDR_Resource_List_Out_Detail.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                            return false;
                        }

                        for (i = 0; i <= FMB_View_UDR_Resource_List_Out_Detail.count - 1; i++)
                        {
                            clsCtrlStatus ResourceStatus = new clsCtrlStatus();
                            ResourceStatus.Key = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_id);
                            ResourceStatus.SetLocation(new Point(CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].loc_x), CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].loc_y)));
                            ResourceStatus.SetSize(new Size(CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].loc_width), CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].loc_height)));
                            ResourceStatus.Text = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].text);
                            ResourceStatus.ResTagFlag = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_tag_flag);
                            ResourceStatus.TextColor = FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].text_color;
                            ResourceStatus.TextFontName = System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultFontName));
                            ResourceStatus.TextSize = CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].text_size);
                            if (CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].text_style) == "")
                            {
                                ResourceStatus.TextStyle = -1;
                            }
                            else
                            {
                                ResourceStatus.TextStyle = CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].text_style.ToString());
                            }
                            ResourceStatus.ToolType = (Enums.eToolType)FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].tag_type;
                            ResourceStatus.BackColor = FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].back_color;
                            ResourceStatus.ZoomScale = 0;
                            if (ResourceStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                            {
                                ResourceStatus.LastEvent = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].last_event);
                                ResourceStatus.PrimaryStatus = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_pri_sts);
                                ResourceStatus.ProcMode = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_proc_mode);
                                ResourceStatus.CtrlMode = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_ctrl_mode);
                                ResourceStatus.UpDownFlag = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_up_down_flag);
                                ResourceStatus.ResourceType = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].res_type);
                                ResourceStatus.AreaID = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].area_id);
                                ResourceStatus.SubAreaID = CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].sub_area_id);
                                if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.IsProcessMode)) == "P")
                                {
                                    ResourceStatus.IsProcessMode = true;
                                }
                                else
                                {
                                    ResourceStatus.IsProcessMode = false;
                                }
                                if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.UseEventColor)) == "Y")
                                {
                                    ResourceStatus.IsUseEventColor = true;
                                    ResourceStatus.EventBackColor = modGlobalVariable.gGlobalOptions.GetOptions(sFactory, ResourceStatus.LastEvent).ToArgb();
                                }
                                else
                                {
                                    ResourceStatus.IsUseEventColor = false;
                                }
                                ResourceStatus.ImageIndex = CmnFunction.ToInt(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].img_idx);
                                if (CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].signal_flag) == "Y")
                                {
                                    ResourceStatus.IsViewSignal = true;
                                }
                                else
                                {
                                    ResourceStatus.IsViewSignal = false;
                                }
                            }
                            else
                            {
                                if (CmnFunction.Trim(FMB_View_UDR_Resource_List_Out_Detail.udr_list[i].no_mouse_event) == "Y")
                                {
                                    ResourceStatus.IsNoEvent = true;
                                }
                                else
                                {
                                    ResourceStatus.IsNoEvent = false;
                                }
                            }
                            if (RefreshControl(this.pnlFMBDesign.Controls, ref ResourceStatus, 1) == false)
                            {
                                if (AddControl(ResourceStatus, false, true) == false)
                                {
                                    return false;
                                }
                            }
                        }

                        FMB_View_UDR_Resource_List_In_Detail.next_seq = FMB_View_UDR_Resource_List_Out_Detail.next_seq;
                    } while (FMB_View_UDR_Resource_List_In_Detail.next_seq != 0);
                }
                else
                {
                    CmnFunction.ShowMsgBox("RefreshResourceListDetail() Failed.", "FMB Client", MessageBoxButtons.OK, 1);
                    return false;
                }

                RefreshDeleteControls();
                SetRefreshedControl(false);

                this.pnlFMBDesign.ResumeLayout(false);

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.RefreshResourceListDetail()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // UpdateResourceListDetail()
        //       - Update Resource/Tag List Detail
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        public bool UpdateResourceListDetail()
        {

            try
            {
                FMB_Update_ResLoc_In_Tag FMB_Update_ResLoc_In = new FMB_Update_ResLoc_In_Tag();
                FMB_Update_UDR_ResLoc_In_Tag FMB_Update_UDR_ResLoc_In = new FMB_Update_UDR_ResLoc_In_Tag();
                FMB_Cmn_Out_Tag FMB_Cmn_Out = new FMB_Cmn_Out_Tag();
                Control ctrl;

                if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    FMB_Update_ResLoc_In._C.h_proc_step = modGlobalConstant.MP_STEP_UPDATE;
                    FMB_Update_ResLoc_In._C.h_passport = GlobalVariable.gsPassport;
                    FMB_Update_ResLoc_In._C.h_language = GlobalVariable.gcLanguage;
                    FMB_Update_ResLoc_In._C.h_user_id = GlobalVariable.gsUserID;
                    FMB_Update_ResLoc_In._C.h_password = GlobalVariable.gsPassword;
                    FMB_Update_ResLoc_In._C.h_factory = modCommonFunction.GetStringBySeperator(Name, ":", 1);

                    foreach (Control tempLoopVar_ctrl in pnlFMBDesign.Controls)
                    {
                        ctrl = tempLoopVar_ctrl;
                        if (ctrl is udcCtrlBase)
                        {
                            if (((udcCtrlBase)ctrl).CtrlStatus.IsSaveFlag == true)
                            {
                                FMB_Update_ResLoc_In._C.layout_id = modCommonFunction.GetStringBySeperator(Name, ":", 2);
                                FMB_Update_ResLoc_In._C.loc_width = ((udcCtrlBase)ctrl).GetSize().Width;
                                FMB_Update_ResLoc_In._C.loc_height = ((udcCtrlBase)ctrl).GetSize().Height;
                                FMB_Update_ResLoc_In._C.loc_x = ((udcCtrlBase)ctrl).GetLocation().X;
                                FMB_Update_ResLoc_In._C.loc_y = ((udcCtrlBase)ctrl).GetLocation().Y;
                                FMB_Update_ResLoc_In._C.res_id = ((udcCtrlBase)ctrl).Name;
                                if (((udcCtrlBase)ctrl).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                                {
                                    FMB_Update_ResLoc_In._C.res_type = modGlobalConstant.FMB_RESOURCE_TYPE;
                                }
                                else
                                {
                                    FMB_Update_ResLoc_In._C.res_type = modGlobalConstant.FMB_TAG_TYPE;
                                }
                                FMB_Update_ResLoc_In._C.tag_type = CmnFunction.ToInt(((udcCtrlBase)ctrl).CtrlStatus.ToolType);
                                FMB_Update_ResLoc_In._C.text = ((udcCtrlBase)ctrl).CtrlStatus.Text;
                                FMB_Update_ResLoc_In._C.text_color = ((udcCtrlBase)ctrl).CtrlStatus.TextColor;
                                FMB_Update_ResLoc_In._C.text_size = ((udcCtrlBase)ctrl).CtrlStatus.TextSize;
                                if (((udcCtrlBase)ctrl).CtrlStatus.TextStyle == -1)
                                {
                                    FMB_Update_ResLoc_In._C.text_style = '0';
                                }
                                else
                                {
                                    FMB_Update_ResLoc_In._C.text_style = (char)(((udcCtrlBase)ctrl).CtrlStatus.TextStyle + '0');
                                }
                                FMB_Update_ResLoc_In._C.back_color = ((udcCtrlBase)ctrl).CtrlStatus.BackColor;
                                if (((udcCtrlBase)ctrl).CtrlStatus.IsNoEvent == true)
                                {
                                    FMB_Update_ResLoc_In._C.no_mouse_event = 'Y';
                                }
                                else
                                {
                                    FMB_Update_ResLoc_In._C.no_mouse_event = ' ';
                                }
                                if (((udcCtrlBase)ctrl).CtrlStatus.IsViewSignal == true)
                                {
                                    FMB_Update_ResLoc_In._C.signal_flag = 'Y';
                                }
                                else
                                {
                                    FMB_Update_ResLoc_In._C.signal_flag = ' ';
                                }

                                if (FMBSender.FMB_Update_Resource_Location(FMB_Update_ResLoc_In, ref FMB_Cmn_Out) == false)
                                {
                                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                                    return false;
                                }

                                if (FMB_Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                                {
                                    CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(FMB_Cmn_Out.h_msg_code, FMB_Cmn_Out.h_msg, FMB_Cmn_Out.h_db_err_msg, FMB_Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                                    return false;
                                }

                                ((udcCtrlBase)ctrl).CtrlStatus.IsSaveFlag = false;

                            }
                        }
                    }

                }
                else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                {
                    FMB_Update_UDR_ResLoc_In._C.h_proc_step = modGlobalConstant.MP_STEP_UPDATE;
                    FMB_Update_UDR_ResLoc_In._C.h_passport = GlobalVariable.gsPassport;
                    FMB_Update_UDR_ResLoc_In._C.h_language = GlobalVariable.gcLanguage;
                    FMB_Update_UDR_ResLoc_In._C.h_user_id = GlobalVariable.gsUserID;
                    FMB_Update_UDR_ResLoc_In._C.h_password = GlobalVariable.gsPassword;
                    FMB_Update_UDR_ResLoc_In._C.h_factory = GlobalVariable.gsFactory;

                    foreach (Control tempLoopVar_ctrl in pnlFMBDesign.Controls)
                    {
                        ctrl = tempLoopVar_ctrl;
                        if (ctrl is udcCtrlBase)
                        {
                            if (((udcCtrlBase)ctrl).CtrlStatus.IsSaveFlag == true)
                            {
                                FMB_Update_UDR_ResLoc_In._C.group = Name;
                                FMB_Update_UDR_ResLoc_In._C.loc_width = ((udcCtrlBase)ctrl).GetSize().Width;
                                FMB_Update_UDR_ResLoc_In._C.loc_height = ((udcCtrlBase)ctrl).GetSize().Height;
                                FMB_Update_UDR_ResLoc_In._C.loc_x = ((udcCtrlBase)ctrl).GetLocation().X;
                                FMB_Update_UDR_ResLoc_In._C.loc_y = ((udcCtrlBase)ctrl).GetLocation().Y;
                                FMB_Update_UDR_ResLoc_In._C.res_id = ((udcCtrlBase)ctrl).Name;
                                if (((udcCtrlBase)ctrl).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                                {
                                    FMB_Update_UDR_ResLoc_In._C.res_type = modGlobalConstant.FMB_RESOURCE_TYPE;
                                }
                                else
                                {
                                    FMB_Update_UDR_ResLoc_In._C.res_type = modGlobalConstant.FMB_TAG_TYPE;
                                }
                                FMB_Update_UDR_ResLoc_In._C.tag_type = CmnFunction.ToInt(((udcCtrlBase)ctrl).CtrlStatus.ToolType);
                                FMB_Update_UDR_ResLoc_In._C.text = ((udcCtrlBase)ctrl).CtrlStatus.Text;
                                FMB_Update_UDR_ResLoc_In._C.text_color = ((udcCtrlBase)ctrl).CtrlStatus.TextColor;
                                FMB_Update_UDR_ResLoc_In._C.text_size = ((udcCtrlBase)ctrl).CtrlStatus.TextSize;
                                if (((udcCtrlBase)ctrl).CtrlStatus.TextStyle == -1)
                                {
                                    FMB_Update_UDR_ResLoc_In._C.text_style = ' ';
                                }
                                else
                                {
                                    FMB_Update_UDR_ResLoc_In._C.text_style = (char)(((udcCtrlBase)ctrl).CtrlStatus.TextStyle + '0');
                                }
                                FMB_Update_UDR_ResLoc_In._C.back_color = ((udcCtrlBase)ctrl).CtrlStatus.BackColor;
                                if (((udcCtrlBase)ctrl).CtrlStatus.IsNoEvent == true)
                                {
                                    FMB_Update_UDR_ResLoc_In._C.no_mouse_event = 'Y';
                                }
                                else
                                {
                                    FMB_Update_UDR_ResLoc_In._C.no_mouse_event = ' ';
                                }
                                if (((udcCtrlBase)ctrl).CtrlStatus.IsViewSignal == true)
                                {
                                    FMB_Update_UDR_ResLoc_In._C.signal_flag = 'Y';
                                }
                                else
                                {
                                    FMB_Update_UDR_ResLoc_In._C.signal_flag = ' ';
                                }

                                if (FMBSender.FMB_Update_UDR_ResLoc(FMB_Update_UDR_ResLoc_In, ref FMB_Cmn_Out) == false)
                                {
                                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                                    return false;
                                }

                                if (FMB_Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                                {
                                    CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(FMB_Cmn_Out.h_msg_code, FMB_Cmn_Out.h_msg, FMB_Cmn_Out.h_db_err_msg, FMB_Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                                    return false;
                                }

                                ((udcCtrlBase)ctrl).CtrlStatus.IsSaveFlag = false;

                            }
                        }
                    }

                }
                else
                {
                    CmnFunction.ShowMsgBox("UpdateResourceList() Failed.", "FMB Client", MessageBoxButtons.OK, 1);
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.UpdateResourceListDetail()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // AddControl()
        //       - Add Control to panel
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		- ByVal ResourceStatus As clsCtrlStatus : Control to add
        //
        public bool AddControl(clsCtrlStatus ResourceStatus, bool bCreate, bool bRefresh)
        {

            try
            {
                this.pnlFMBDesign.SuspendLayout();

                udcCtrlTag ctrlTag;
                switch (ResourceStatus.ToolType)
                {
                    case Miracom.FMBUI.Enums.eToolType.Resource:

                        udcCtrlResource ctrlResource = new udcCtrlResource(modGlobalVariable.gimlResource);

                        modLanguageFunction.ToClientLanguage(ctrlResource.ContextMenu.MenuItems);
                        ctrlResource.SetCtrlStatusData(ResourceStatus, 1, true);

                        ctrlResource.CtrlMouseEnter += new CtrlMouseEnterEventHandler(CtrlBase_CtrlMouseEnter);
                        ctrlResource.CtrlMouseLeave += new CtrlMouseLeaveEventHandler(CtrlBase_CtrlMouseLeave);
                        ctrlResource.CtrlMouseDown += new CtrlMouseDownEventHandler(CtrlBase_CtrlMouseDown);
                        ctrlResource.CtrlMouseUp += new CtrlMouseUpEventHandler(CtrlBase_CtrlMouseUp);
                        ctrlResource.CtrlMouseMove += new CtrlMouseMoveEventHandler(CtrlBase_CtrlMouseMove);
                        ctrlResource.CtrlGotFocus += new CtrlGotFocusEventHandler(CtrlBase_CtrlGotFocus);
                        ctrlResource.CtrlLostFocus += new CtrlLostFocusEventHandler(CtrlBase_CtrlLostFocus);
                        ctrlResource.CtrlContextMenu += new CtrlContextMenuEventHandler(CtrlBase_CtrlContextMenu);
                        ctrlResource.CtrlKeyDown += new CtrlKeyDownEventHandler(CtrlBase_CtrlKeyDown);
                        //ctrlResource.CtrlMouseEnter += new System.EventHandler(CtrlBase_CtrlMouseEnter);
                        //ctrlResource.CtrlMouseLeave += new System.EventHandler(CtrlBase_CtrlMouseLeave);
                        //ctrlResource.CtrlMouseDown += new System.EventHandler(CtrlBase_CtrlMouseDown);
                        //ctrlResource.CtrlMouseUp += new System.EventHandler(CtrlBase_CtrlMouseUp);
                        //ctrlResource.CtrlMouseMove += new System.EventHandler(CtrlBase_CtrlMouseMove);
                        //ctrlResource.CtrlGotFocus += new System.EventHandler(CtrlBase_CtrlGotFocus);
                        //ctrlResource.CtrlLostFocus += new System.EventHandler(CtrlBase_CtrlLostFocus);
                        //ctrlResource.CtrlContextMenu += new System.EventHandler(CtrlBase_CtrlContextMenu);
                        //ctrlResource.CtrlKeyDown += new System.EventHandler(CtrlBase_CtrlKeyDown);
                        pnlFMBDesign.Controls.Add(ctrlResource);
                        if (bCreate == true)
                        {
                            ctrlResource.BringToFront();
                        }
                        if (bRefresh == true)
                        {
                            ctrlResource.IsRefreshed = true;
                        }
                        modGlobalVariable.giToolType = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Null);
                        break;
                    case Miracom.FMBUI.Enums.eToolType.Rectangle:
                        //udcCtrlTag ctrlTag = new udcCtrlTag();
                        ctrlTag = new udcCtrlTag();
                        modLanguageFunction.ToClientLanguage(ctrlTag.ContextMenu.MenuItems);
                        ctrlTag.SetCtrlStatusData(ResourceStatus, 1, true);

                        //ctrlTag.CtrlMouseEnter += new System.EventHandler(CtrlBase_CtrlMouseEnter);
                        //ctrlTag.CtrlMouseLeave += new System.EventHandler(CtrlBase_CtrlMouseLeave);
                        //ctrlTag.CtrlMouseDown += new System.EventHandler(CtrlBase_CtrlMouseDown);
                        //ctrlTag.CtrlMouseUp += new System.EventHandler(CtrlBase_CtrlMouseUp);
                        //ctrlTag.CtrlMouseMove += new System.EventHandler(CtrlBase_CtrlMouseMove);
                        //ctrlTag.CtrlGotFocus += new System.EventHandler(CtrlBase_CtrlGotFocus);
                        //ctrlTag.CtrlLostFocus += new System.EventHandler(CtrlBase_CtrlLostFocus);
                        //ctrlTag.CtrlContextMenu += new System.EventHandler(CtrlBase_CtrlContextMenu);
                        //ctrlTag.CtrlKeyDown += new System.EventHandler(CtrlBase_CtrlKeyDown);
                        ctrlTag.CtrlMouseEnter += new CtrlMouseEnterEventHandler(CtrlBase_CtrlMouseEnter);
                        ctrlTag.CtrlMouseLeave += new CtrlMouseLeaveEventHandler(CtrlBase_CtrlMouseLeave);
                        ctrlTag.CtrlMouseDown += new CtrlMouseDownEventHandler(CtrlBase_CtrlMouseDown);
                        ctrlTag.CtrlMouseUp += new CtrlMouseUpEventHandler(CtrlBase_CtrlMouseUp);
                        ctrlTag.CtrlMouseMove += new CtrlMouseMoveEventHandler(CtrlBase_CtrlMouseMove);
                        ctrlTag.CtrlGotFocus += new CtrlGotFocusEventHandler(CtrlBase_CtrlGotFocus);
                        ctrlTag.CtrlLostFocus += new CtrlLostFocusEventHandler(CtrlBase_CtrlLostFocus);
                        ctrlTag.CtrlContextMenu += new CtrlContextMenuEventHandler(CtrlBase_CtrlContextMenu);
                        ctrlTag.CtrlKeyDown += new CtrlKeyDownEventHandler(CtrlBase_CtrlKeyDown);
                        pnlFMBDesign.Controls.Add(ctrlTag);
                        if (bCreate == true)
                        {
                            ctrlTag.BringToFront();
                        }
                        if (bRefresh == true)
                        {
                            ctrlTag.IsRefreshed = true;
                        }
                        modGlobalVariable.giToolType = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Null);
                        break;

                    case Miracom.FMBUI.Enums.eToolType.Ellipse:
                        //udcCtrlTag ctrlTag = new udcCtrlTag();
                        ctrlTag = new udcCtrlTag();
                        modLanguageFunction.ToClientLanguage(ctrlTag.ContextMenu.MenuItems);
                        ctrlTag.SetCtrlStatusData(ResourceStatus, 1, true);
                        //ctrlTag.CtrlMouseEnter += new System.EventHandler(CtrlBase_CtrlMouseEnter);
                        //ctrlTag.CtrlMouseLeave += new System.EventHandler(CtrlBase_CtrlMouseLeave);
                        //ctrlTag.CtrlMouseDown += new System.EventHandler(CtrlBase_CtrlMouseDown);
                        //ctrlTag.CtrlMouseUp += new System.EventHandler(CtrlBase_CtrlMouseUp);
                        //ctrlTag.CtrlMouseMove += new System.EventHandler(CtrlBase_CtrlMouseMove);
                        //ctrlTag.CtrlGotFocus += new System.EventHandler(CtrlBase_CtrlGotFocus);
                        //ctrlTag.CtrlLostFocus += new System.EventHandler(CtrlBase_CtrlLostFocus);
                        //ctrlTag.CtrlContextMenu += new System.EventHandler(CtrlBase_CtrlContextMenu);
                        //ctrlTag.CtrlKeyDown += new System.EventHandler(CtrlBase_CtrlKeyDown);
                        ctrlTag.CtrlMouseEnter += new CtrlMouseEnterEventHandler(CtrlBase_CtrlMouseEnter);
                        ctrlTag.CtrlMouseLeave += new CtrlMouseLeaveEventHandler(CtrlBase_CtrlMouseLeave);
                        ctrlTag.CtrlMouseDown += new CtrlMouseDownEventHandler(CtrlBase_CtrlMouseDown);
                        ctrlTag.CtrlMouseUp += new CtrlMouseUpEventHandler(CtrlBase_CtrlMouseUp);
                        ctrlTag.CtrlMouseMove += new CtrlMouseMoveEventHandler(CtrlBase_CtrlMouseMove);
                        ctrlTag.CtrlGotFocus += new CtrlGotFocusEventHandler(CtrlBase_CtrlGotFocus);
                        ctrlTag.CtrlLostFocus += new CtrlLostFocusEventHandler(CtrlBase_CtrlLostFocus);
                        ctrlTag.CtrlContextMenu += new CtrlContextMenuEventHandler(CtrlBase_CtrlContextMenu);
                        ctrlTag.CtrlKeyDown += new CtrlKeyDownEventHandler(CtrlBase_CtrlKeyDown);
                        pnlFMBDesign.Controls.Add(ctrlTag);
                        if (bCreate == true)
                        {
                            ctrlTag.BringToFront();
                        }
                        if (bRefresh == true)
                        {
                            ctrlTag.IsRefreshed = true;
                        }
                        modGlobalVariable.giToolType = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Null);
                        break;

                    case Miracom.FMBUI.Enums.eToolType.Triangle:
                        //udcCtrlTag ctrlTag = new udcCtrlTag();
                        ctrlTag = new udcCtrlTag();
                        modLanguageFunction.ToClientLanguage(ctrlTag.ContextMenu.MenuItems);
                        ctrlTag.SetCtrlStatusData(ResourceStatus, 1, true);
                        //ctrlTag.CtrlMouseEnter += new System.EventHandler(CtrlBase_CtrlMouseEnter);
                        //ctrlTag.CtrlMouseLeave += new System.EventHandler(CtrlBase_CtrlMouseLeave);
                        //ctrlTag.CtrlMouseDown += new System.EventHandler(CtrlBase_CtrlMouseDown);
                        //ctrlTag.CtrlMouseUp += new System.EventHandler(CtrlBase_CtrlMouseUp);
                        //ctrlTag.CtrlMouseMove += new System.EventHandler(CtrlBase_CtrlMouseMove);
                        //ctrlTag.CtrlGotFocus += new System.EventHandler(CtrlBase_CtrlGotFocus);
                        //ctrlTag.CtrlLostFocus += new System.EventHandler(CtrlBase_CtrlLostFocus);
                        //ctrlTag.CtrlContextMenu += new System.EventHandler(CtrlBase_CtrlContextMenu);
                        //ctrlTag.CtrlKeyDown += new System.EventHandler(CtrlBase_CtrlKeyDown);
                        ctrlTag.CtrlMouseEnter += new CtrlMouseEnterEventHandler(CtrlBase_CtrlMouseEnter);
                        ctrlTag.CtrlMouseLeave += new CtrlMouseLeaveEventHandler(CtrlBase_CtrlMouseLeave);
                        ctrlTag.CtrlMouseDown += new CtrlMouseDownEventHandler(CtrlBase_CtrlMouseDown);
                        ctrlTag.CtrlMouseUp += new CtrlMouseUpEventHandler(CtrlBase_CtrlMouseUp);
                        ctrlTag.CtrlMouseMove += new CtrlMouseMoveEventHandler(CtrlBase_CtrlMouseMove);
                        ctrlTag.CtrlGotFocus += new CtrlGotFocusEventHandler(CtrlBase_CtrlGotFocus);
                        ctrlTag.CtrlLostFocus += new CtrlLostFocusEventHandler(CtrlBase_CtrlLostFocus);
                        ctrlTag.CtrlContextMenu += new CtrlContextMenuEventHandler(CtrlBase_CtrlContextMenu);
                        ctrlTag.CtrlKeyDown += new CtrlKeyDownEventHandler(CtrlBase_CtrlKeyDown);
                        pnlFMBDesign.Controls.Add(ctrlTag);
                        if (bCreate == true)
                        {
                            ctrlTag.BringToFront();
                        }
                        if (bRefresh == true)
                        {
                            ctrlTag.IsRefreshed = true;
                        }
                        modGlobalVariable.giToolType = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Null);
                        break;

                    case Miracom.FMBUI.Enums.eToolType.VerticalLine:
                        //udcCtrlTag ctrlTag = new udcCtrlTag();
                        ctrlTag = new udcCtrlTag();
                        modLanguageFunction.ToClientLanguage(ctrlTag.ContextMenu.MenuItems);
                        ctrlTag.SetCtrlStatusData(ResourceStatus, 1, true);
                        //ctrlTag.CtrlMouseEnter += new System.EventHandler(CtrlBase_CtrlMouseEnter);
                        //ctrlTag.CtrlMouseLeave += new System.EventHandler(CtrlBase_CtrlMouseLeave);
                        //ctrlTag.CtrlMouseDown += new System.EventHandler(CtrlBase_CtrlMouseDown);
                        //ctrlTag.CtrlMouseUp += new System.EventHandler(CtrlBase_CtrlMouseUp);
                        //ctrlTag.CtrlMouseMove += new System.EventHandler(CtrlBase_CtrlMouseMove);
                        //ctrlTag.CtrlGotFocus += new System.EventHandler(CtrlBase_CtrlGotFocus);
                        //ctrlTag.CtrlLostFocus += new System.EventHandler(CtrlBase_CtrlLostFocus);
                        //ctrlTag.CtrlContextMenu += new System.EventHandler(CtrlBase_CtrlContextMenu);
                        //ctrlTag.CtrlKeyDown += new System.EventHandler(CtrlBase_CtrlKeyDown);
                        ctrlTag.CtrlMouseEnter += new CtrlMouseEnterEventHandler(CtrlBase_CtrlMouseEnter);
                        ctrlTag.CtrlMouseLeave += new CtrlMouseLeaveEventHandler(CtrlBase_CtrlMouseLeave);
                        ctrlTag.CtrlMouseDown += new CtrlMouseDownEventHandler(CtrlBase_CtrlMouseDown);
                        ctrlTag.CtrlMouseUp += new CtrlMouseUpEventHandler(CtrlBase_CtrlMouseUp);
                        ctrlTag.CtrlMouseMove += new CtrlMouseMoveEventHandler(CtrlBase_CtrlMouseMove);
                        ctrlTag.CtrlGotFocus += new CtrlGotFocusEventHandler(CtrlBase_CtrlGotFocus);
                        ctrlTag.CtrlLostFocus += new CtrlLostFocusEventHandler(CtrlBase_CtrlLostFocus);
                        ctrlTag.CtrlContextMenu += new CtrlContextMenuEventHandler(CtrlBase_CtrlContextMenu);
                        ctrlTag.CtrlKeyDown += new CtrlKeyDownEventHandler(CtrlBase_CtrlKeyDown);
                        pnlFMBDesign.Controls.Add(ctrlTag);
                        if (bCreate == true)
                        {
                            ctrlTag.BringToFront();
                        }
                        if (bRefresh == true)
                        {
                            ctrlTag.IsRefreshed = true;
                        }
                        modGlobalVariable.giToolType = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Null);
                        break;

                    case Miracom.FMBUI.Enums.eToolType.HorizontalLine:
                        //udcCtrlTag ctrlTag = new udcCtrlTag();
                        ctrlTag = new udcCtrlTag();
                        modLanguageFunction.ToClientLanguage(ctrlTag.ContextMenu.MenuItems);
                        ctrlTag.SetCtrlStatusData(ResourceStatus, 1, true);
                        //ctrlTag.CtrlMouseEnter += new System.EventHandler(CtrlBase_CtrlMouseEnter);
                        //ctrlTag.CtrlMouseLeave += new System.EventHandler(CtrlBase_CtrlMouseLeave);
                        //ctrlTag.CtrlMouseDown += new System.EventHandler(CtrlBase_CtrlMouseDown);
                        //ctrlTag.CtrlMouseUp += new System.EventHandler(CtrlBase_CtrlMouseUp);
                        //ctrlTag.CtrlMouseMove += new System.EventHandler(CtrlBase_CtrlMouseMove);
                        //ctrlTag.CtrlGotFocus += new System.EventHandler(CtrlBase_CtrlGotFocus);
                        //ctrlTag.CtrlLostFocus += new System.EventHandler(CtrlBase_CtrlLostFocus);
                        //ctrlTag.CtrlContextMenu += new System.EventHandler(CtrlBase_CtrlContextMenu);
                        //ctrlTag.CtrlKeyDown += new System.EventHandler(CtrlBase_CtrlKeyDown);
                        ctrlTag.CtrlMouseEnter += new CtrlMouseEnterEventHandler(CtrlBase_CtrlMouseEnter);
                        ctrlTag.CtrlMouseLeave += new CtrlMouseLeaveEventHandler(CtrlBase_CtrlMouseLeave);
                        ctrlTag.CtrlMouseDown += new CtrlMouseDownEventHandler(CtrlBase_CtrlMouseDown);
                        ctrlTag.CtrlMouseUp += new CtrlMouseUpEventHandler(CtrlBase_CtrlMouseUp);
                        ctrlTag.CtrlMouseMove += new CtrlMouseMoveEventHandler(CtrlBase_CtrlMouseMove);
                        ctrlTag.CtrlGotFocus += new CtrlGotFocusEventHandler(CtrlBase_CtrlGotFocus);
                        ctrlTag.CtrlLostFocus += new CtrlLostFocusEventHandler(CtrlBase_CtrlLostFocus);
                        ctrlTag.CtrlContextMenu += new CtrlContextMenuEventHandler(CtrlBase_CtrlContextMenu);
                        ctrlTag.CtrlKeyDown += new CtrlKeyDownEventHandler(CtrlBase_CtrlKeyDown);
                        pnlFMBDesign.Controls.Add(ctrlTag);
                        if (bCreate == true)
                        {
                            ctrlTag.BringToFront();
                        }
                        if (bRefresh == true)
                        {
                            ctrlTag.IsRefreshed = true;
                        }
                        modGlobalVariable.giToolType = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Null);
                        break;

                    case Miracom.FMBUI.Enums.eToolType.PieType1:
                        //udcCtrlTag ctrlTag = new udcCtrlTag();
                        ctrlTag = new udcCtrlTag();
                        modLanguageFunction.ToClientLanguage(ctrlTag.ContextMenu.MenuItems);
                        ctrlTag.SetCtrlStatusData(ResourceStatus, 1, true);
                        //ctrlTag.CtrlMouseEnter += new System.EventHandler(CtrlBase_CtrlMouseEnter);
                        //ctrlTag.CtrlMouseLeave += new System.EventHandler(CtrlBase_CtrlMouseLeave);
                        //ctrlTag.CtrlMouseDown += new System.EventHandler(CtrlBase_CtrlMouseDown);
                        //ctrlTag.CtrlMouseUp += new System.EventHandler(CtrlBase_CtrlMouseUp);
                        //ctrlTag.CtrlMouseMove += new System.EventHandler(CtrlBase_CtrlMouseMove);
                        //ctrlTag.CtrlGotFocus += new System.EventHandler(CtrlBase_CtrlGotFocus);
                        //ctrlTag.CtrlLostFocus += new System.EventHandler(CtrlBase_CtrlLostFocus);
                        //ctrlTag.CtrlContextMenu += new System.EventHandler(CtrlBase_CtrlContextMenu);
                        //ctrlTag.CtrlKeyDown += new System.EventHandler(CtrlBase_CtrlKeyDown);
                        ctrlTag.CtrlMouseEnter += new CtrlMouseEnterEventHandler(CtrlBase_CtrlMouseEnter);
                        ctrlTag.CtrlMouseLeave += new CtrlMouseLeaveEventHandler(CtrlBase_CtrlMouseLeave);
                        ctrlTag.CtrlMouseDown += new CtrlMouseDownEventHandler(CtrlBase_CtrlMouseDown);
                        ctrlTag.CtrlMouseUp += new CtrlMouseUpEventHandler(CtrlBase_CtrlMouseUp);
                        ctrlTag.CtrlMouseMove += new CtrlMouseMoveEventHandler(CtrlBase_CtrlMouseMove);
                        ctrlTag.CtrlGotFocus += new CtrlGotFocusEventHandler(CtrlBase_CtrlGotFocus);
                        ctrlTag.CtrlLostFocus += new CtrlLostFocusEventHandler(CtrlBase_CtrlLostFocus);
                        ctrlTag.CtrlContextMenu += new CtrlContextMenuEventHandler(CtrlBase_CtrlContextMenu);
                        ctrlTag.CtrlKeyDown += new CtrlKeyDownEventHandler(CtrlBase_CtrlKeyDown);
                        pnlFMBDesign.Controls.Add(ctrlTag);
                        if (bCreate == true)
                        {
                            ctrlTag.BringToFront();
                        }
                        if (bRefresh == true)
                        {
                            ctrlTag.IsRefreshed = true;
                        }
                        modGlobalVariable.giToolType = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Null);
                        break;

                    case Miracom.FMBUI.Enums.eToolType.PieType2:
                        //udcCtrlTag ctrlTag = new udcCtrlTag();
                        ctrlTag = new udcCtrlTag();
                        modLanguageFunction.ToClientLanguage(ctrlTag.ContextMenu.MenuItems);
                        ctrlTag.SetCtrlStatusData(ResourceStatus, 1, true);
                        //ctrlTag.CtrlMouseEnter += new System.EventHandler(CtrlBase_CtrlMouseEnter);
                        //ctrlTag.CtrlMouseLeave += new System.EventHandler(CtrlBase_CtrlMouseLeave);
                        //ctrlTag.CtrlMouseDown += new System.EventHandler(CtrlBase_CtrlMouseDown);
                        //ctrlTag.CtrlMouseUp += new System.EventHandler(CtrlBase_CtrlMouseUp);
                        //ctrlTag.CtrlMouseMove += new System.EventHandler(CtrlBase_CtrlMouseMove);
                        //ctrlTag.CtrlGotFocus += new System.EventHandler(CtrlBase_CtrlGotFocus);
                        //ctrlTag.CtrlLostFocus += new System.EventHandler(CtrlBase_CtrlLostFocus);
                        //ctrlTag.CtrlContextMenu += new System.EventHandler(CtrlBase_CtrlContextMenu);
                        //ctrlTag.CtrlKeyDown += new System.EventHandler(CtrlBase_CtrlKeyDown);
                        ctrlTag.CtrlMouseEnter += new CtrlMouseEnterEventHandler(CtrlBase_CtrlMouseEnter);
                        ctrlTag.CtrlMouseLeave += new CtrlMouseLeaveEventHandler(CtrlBase_CtrlMouseLeave);
                        ctrlTag.CtrlMouseDown += new CtrlMouseDownEventHandler(CtrlBase_CtrlMouseDown);
                        ctrlTag.CtrlMouseUp += new CtrlMouseUpEventHandler(CtrlBase_CtrlMouseUp);
                        ctrlTag.CtrlMouseMove += new CtrlMouseMoveEventHandler(CtrlBase_CtrlMouseMove);
                        ctrlTag.CtrlGotFocus += new CtrlGotFocusEventHandler(CtrlBase_CtrlGotFocus);
                        ctrlTag.CtrlLostFocus += new CtrlLostFocusEventHandler(CtrlBase_CtrlLostFocus);
                        ctrlTag.CtrlContextMenu += new CtrlContextMenuEventHandler(CtrlBase_CtrlContextMenu);
                        ctrlTag.CtrlKeyDown += new CtrlKeyDownEventHandler(CtrlBase_CtrlKeyDown);
                        pnlFMBDesign.Controls.Add(ctrlTag);
                        if (bCreate == true)
                        {
                            ctrlTag.BringToFront();
                        }
                        if (bRefresh == true)
                        {
                            ctrlTag.IsRefreshed = true;
                        }
                        modGlobalVariable.giToolType = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Null);
                        break;

                    case Miracom.FMBUI.Enums.eToolType.PieType3:
                        //udcCtrlTag ctrlTag = new udcCtrlTag();
                        ctrlTag = new udcCtrlTag();
                        modLanguageFunction.ToClientLanguage(ctrlTag.ContextMenu.MenuItems);
                        ctrlTag.SetCtrlStatusData(ResourceStatus, 1, true);
                        //ctrlTag.CtrlMouseEnter += new System.EventHandler(CtrlBase_CtrlMouseEnter);
                        //ctrlTag.CtrlMouseLeave += new System.EventHandler(CtrlBase_CtrlMouseLeave);
                        //ctrlTag.CtrlMouseDown += new System.EventHandler(CtrlBase_CtrlMouseDown);
                        //ctrlTag.CtrlMouseUp += new System.EventHandler(CtrlBase_CtrlMouseUp);
                        //ctrlTag.CtrlMouseMove += new System.EventHandler(CtrlBase_CtrlMouseMove);
                        //ctrlTag.CtrlGotFocus += new System.EventHandler(CtrlBase_CtrlGotFocus);
                        //ctrlTag.CtrlLostFocus += new System.EventHandler(CtrlBase_CtrlLostFocus);
                        //ctrlTag.CtrlContextMenu += new System.EventHandler(CtrlBase_CtrlContextMenu);
                        //ctrlTag.CtrlKeyDown += new System.EventHandler(CtrlBase_CtrlKeyDown);
                        ctrlTag.CtrlMouseEnter += new CtrlMouseEnterEventHandler(CtrlBase_CtrlMouseEnter);
                        ctrlTag.CtrlMouseLeave += new CtrlMouseLeaveEventHandler(CtrlBase_CtrlMouseLeave);
                        ctrlTag.CtrlMouseDown += new CtrlMouseDownEventHandler(CtrlBase_CtrlMouseDown);
                        ctrlTag.CtrlMouseUp += new CtrlMouseUpEventHandler(CtrlBase_CtrlMouseUp);
                        ctrlTag.CtrlMouseMove += new CtrlMouseMoveEventHandler(CtrlBase_CtrlMouseMove);
                        ctrlTag.CtrlGotFocus += new CtrlGotFocusEventHandler(CtrlBase_CtrlGotFocus);
                        ctrlTag.CtrlLostFocus += new CtrlLostFocusEventHandler(CtrlBase_CtrlLostFocus);
                        ctrlTag.CtrlContextMenu += new CtrlContextMenuEventHandler(CtrlBase_CtrlContextMenu);
                        ctrlTag.CtrlKeyDown += new CtrlKeyDownEventHandler(CtrlBase_CtrlKeyDown);
                        pnlFMBDesign.Controls.Add(ctrlTag);
                        if (bCreate == true)
                        {
                            ctrlTag.BringToFront();
                        }
                        if (bRefresh == true)
                        {
                            ctrlTag.IsRefreshed = true;
                        }
                        modGlobalVariable.giToolType = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Null);
                        break;

                    case Miracom.FMBUI.Enums.eToolType.PieType4:
                        //udcCtrlTag ctrlTag = new udcCtrlTag();
                        ctrlTag = new udcCtrlTag();
                        modLanguageFunction.ToClientLanguage(ctrlTag.ContextMenu.MenuItems);
                        ctrlTag.SetCtrlStatusData(ResourceStatus, 1, true);
                        //ctrlTag.CtrlMouseEnter += new System.EventHandler(CtrlBase_CtrlMouseEnter);
                        //ctrlTag.CtrlMouseLeave += new System.EventHandler(CtrlBase_CtrlMouseLeave);
                        //ctrlTag.CtrlMouseDown += new System.EventHandler(CtrlBase_CtrlMouseDown);
                        //ctrlTag.CtrlMouseUp += new System.EventHandler(CtrlBase_CtrlMouseUp);
                        //ctrlTag.CtrlMouseMove += new System.EventHandler(CtrlBase_CtrlMouseMove);
                        //ctrlTag.CtrlGotFocus += new System.EventHandler(CtrlBase_CtrlGotFocus);
                        //ctrlTag.CtrlLostFocus += new System.EventHandler(CtrlBase_CtrlLostFocus);
                        //ctrlTag.CtrlContextMenu += new System.EventHandler(CtrlBase_CtrlContextMenu);
                        //ctrlTag.CtrlKeyDown += new System.EventHandler(CtrlBase_CtrlKeyDown);
                        ctrlTag.CtrlMouseEnter += new CtrlMouseEnterEventHandler(CtrlBase_CtrlMouseEnter);
                        ctrlTag.CtrlMouseLeave += new CtrlMouseLeaveEventHandler(CtrlBase_CtrlMouseLeave);
                        ctrlTag.CtrlMouseDown += new CtrlMouseDownEventHandler(CtrlBase_CtrlMouseDown);
                        ctrlTag.CtrlMouseUp += new CtrlMouseUpEventHandler(CtrlBase_CtrlMouseUp);
                        ctrlTag.CtrlMouseMove += new CtrlMouseMoveEventHandler(CtrlBase_CtrlMouseMove);
                        ctrlTag.CtrlGotFocus += new CtrlGotFocusEventHandler(CtrlBase_CtrlGotFocus);
                        ctrlTag.CtrlLostFocus += new CtrlLostFocusEventHandler(CtrlBase_CtrlLostFocus);
                        ctrlTag.CtrlContextMenu += new CtrlContextMenuEventHandler(CtrlBase_CtrlContextMenu);
                        ctrlTag.CtrlKeyDown += new CtrlKeyDownEventHandler(CtrlBase_CtrlKeyDown);
                        pnlFMBDesign.Controls.Add(ctrlTag);
                        if (bCreate == true)
                        {
                            ctrlTag.BringToFront();
                        }
                        if (bRefresh == true)
                        {
                            ctrlTag.IsRefreshed = true;
                        }
                        modGlobalVariable.giToolType = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Null);
                        break;

                    case Miracom.FMBUI.Enums.eToolType.TextType:

                        //udcCtrlTag ctrlTag = new udcCtrlTag();
                        ctrlTag = new udcCtrlTag();
                        modLanguageFunction.ToClientLanguage(ctrlTag.ContextMenu.MenuItems);
                        ctrlTag.SetCtrlStatusData(ResourceStatus, 1, true);
                        //ctrlTag.CtrlMouseEnter += new System.EventHandler(CtrlBase_CtrlMouseEnter);
                        //ctrlTag.CtrlMouseLeave += new System.EventHandler(CtrlBase_CtrlMouseLeave);
                        //ctrlTag.CtrlMouseDown += new System.EventHandler(CtrlBase_CtrlMouseDown);
                        //ctrlTag.CtrlMouseUp += new System.EventHandler(CtrlBase_CtrlMouseUp);
                        //ctrlTag.CtrlMouseMove += new System.EventHandler(CtrlBase_CtrlMouseMove);
                        //ctrlTag.CtrlGotFocus += new System.EventHandler(CtrlBase_CtrlGotFocus);
                        //ctrlTag.CtrlLostFocus += new System.EventHandler(CtrlBase_CtrlLostFocus);
                        //ctrlTag.CtrlContextMenu += new System.EventHandler(CtrlBase_CtrlContextMenu);
                        //ctrlTag.CtrlKeyDown += new System.EventHandler(CtrlBase_CtrlKeyDown);
                        ctrlTag.CtrlMouseEnter += new CtrlMouseEnterEventHandler(CtrlBase_CtrlMouseEnter);
                        ctrlTag.CtrlMouseLeave += new CtrlMouseLeaveEventHandler(CtrlBase_CtrlMouseLeave);
                        ctrlTag.CtrlMouseDown += new CtrlMouseDownEventHandler(CtrlBase_CtrlMouseDown);
                        ctrlTag.CtrlMouseUp += new CtrlMouseUpEventHandler(CtrlBase_CtrlMouseUp);
                        ctrlTag.CtrlMouseMove += new CtrlMouseMoveEventHandler(CtrlBase_CtrlMouseMove);
                        ctrlTag.CtrlGotFocus += new CtrlGotFocusEventHandler(CtrlBase_CtrlGotFocus);
                        ctrlTag.CtrlLostFocus += new CtrlLostFocusEventHandler(CtrlBase_CtrlLostFocus);
                        ctrlTag.CtrlContextMenu += new CtrlContextMenuEventHandler(CtrlBase_CtrlContextMenu);
                        ctrlTag.CtrlKeyDown += new CtrlKeyDownEventHandler(CtrlBase_CtrlKeyDown);
                        pnlFMBDesign.Controls.Add(ctrlTag);
                        if (bCreate == true)
                        {
                            ctrlTag.BringToFront();
                        }
                        if (bRefresh == true)
                        {
                            ctrlTag.IsRefreshed = true;
                        }
                        modGlobalVariable.giToolType = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Null);
                        break;
                    default:

                        if (pnlFMBDesign.Focus() == true)
                        {
                            pnlFMBDesign.SetFocus(null);
                        }
                        else
                        {
                            pnlFMBDesign.Select();
                        }
                        break;
                }

                this.pnlFMBDesign.ResumeLayout(false);

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.AddControl()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        //void ctrlResource_CtrlMouseEnter(object sender, EventArgs e)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        // RedrawControls()
        //       - Redraw Controls
        // Return Value
        //       -
        // Arguments
        //		-
        //
        public void RedrawControls()
        {

            try
            {
                this.pnlFMBDesign.SuspendLayout();

                Control ctrl;
                foreach (Control tempLoopVar_ctrl in pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        ((udcCtrlBase)ctrl).RedrawCtrl();
                    }
                }

                this.pnlFMBDesign.ResumeLayout(false);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.RedrawControls()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // AddSelectedControls()
        //       - Add control to pnlFMBDesign.SelectedControls
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		- ByVal ctrl As udcCtrlBase : Control to add
        //
        public bool AddSelectedControls(udcCtrlBase ctrl)
        {

            try
            {
                string sCtrlType;
                string sSelectedType;

                // Resource Type / Tag Type 정의
                if (ctrl.CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                {
                    sCtrlType = "R:";
                }
                else
                {
                    sCtrlType = "T:";
                }

                for (int i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {
                    // Resource Type / Tag Type 인지 분기
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                    {
                        sSelectedType = "R:";
                    }
                    else
                    {
                        sSelectedType = "T:";
                    }

                    // 현재 Add 하려는 Control이 있는 경우에는 Skip
                    if (modCommonFunction.ToAsc(sSelectedType + ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).Name) == modCommonFunction.ToAsc(sCtrlType + ctrl.Name))
                    {
                        return false;
                    }
                }

                //' 이미 다른 선택되어진 Control이 있을 경우에
                //' Add 하기전에 Focus를 False로 설정한다.
                if (pnlFMBDesign.SelectedControlsCount() > 0)
                {
                    ((udcCtrlBase)pnlFMBDesign.SelectedControls[pnlFMBDesign.SelectedControlsCount() - 1]).IsFocused = false;
                    ((udcCtrlBase)pnlFMBDesign.SelectedControls[pnlFMBDesign.SelectedControlsCount() - 1]).RedrawCtrl();
                }

                // Control를 선택리스트의 마지막에 추가한다.
                ctrl.IsHot = true;
                ctrl.IsSelected = true;
                ctrl.IsFocused = true;
                ctrl.RedrawCtrl();
                pnlFMBDesign.SelectedControls.Add(ctrl);

                if (pnlFMBDesign.SelectedControlsCount() == 1)
                {
                    //tsmLefts.Enabled = false;
                    //tsmCenters.Enabled = false;
                    //tsmRights.Enabled = false;
                    //tsmTops.Enabled = false;
                    //tsmMiddles.Enabled = false;
                    //tsmBottoms.Enabled = false;
                    //tsmWidth.Enabled = false;
                    //tsmHeight.Enabled = false;
                    //tsmBoth.Enabled = false;
                    //tsmHMakeEqual.Enabled = false;
                    //tsmHIncrease.Enabled = false;
                    //tsmHDecrease.Enabled = false;
                    //tsmHRemove.Enabled = false;
                    //tsmVMakeEqual.Enabled = false;
                    //tsmVIncrease.Enabled = false;
                    //tsmVDecrease.Enabled = false;
                    //tsmVRemove.Enabled = false;
                    //tsmBring.Enabled = true;
                    //tsmSend.Enabled = true;
                    //tsmUpdateRes.Enabled = true;
                    //tsmDeleteRes.Enabled = true;
                    //tsmProperties.Enabled = true;

                }
                else if (pnlFMBDesign.SelectedControlsCount() > 1)
                {
                    // 2개 이상의 Control이 선택되어진 경우
                    //tsmLefts.Enabled = true;
                    //tsmCenters.Enabled = true;
                    //tsmRights.Enabled = true;
                    //tsmTops.Enabled = true;
                    //tsmMiddles.Enabled = true;
                    //tsmBottoms.Enabled = true;
                    //tsmWidth.Enabled = true;
                    //tsmHeight.Enabled = true;
                    //tsmBoth.Enabled = true;
                    //tsmHMakeEqual.Enabled = true;
                    //tsmHIncrease.Enabled = true;
                    //tsmHDecrease.Enabled = true;
                    //tsmHRemove.Enabled = true;
                    //tsmVMakeEqual.Enabled = true;
                    //tsmVIncrease.Enabled = true;
                    //tsmVDecrease.Enabled = true;
                    //tsmVRemove.Enabled = true;
                    //tsmBring.Enabled = false;
                    //tsmSend.Enabled = false;
                    //tsmUpdateRes.Enabled = false;
                    //tsmDeleteRes.Enabled = false;
                    //tsmProperties.Enabled = false;
                }
                else
                {
                    // 선택되어진 Control이 없을 경우 -> Format Menu 초기화
                    //tsmLefts.Enabled = false;
                    //tsmCenters.Enabled = false;
                    //tsmRights.Enabled = false;
                    //tsmTops.Enabled = false;
                    //tsmMiddles.Enabled = false;
                    //tsmBottoms.Enabled = false;
                    //tsmWidth.Enabled = false;
                    //tsmHeight.Enabled = false;
                    //tsmBoth.Enabled = false;
                    //tsmHMakeEqual.Enabled = false;
                    //tsmHIncrease.Enabled = false;
                    //tsmHDecrease.Enabled = false;
                    //tsmHRemove.Enabled = false;
                    //tsmVMakeEqual.Enabled = false;
                    //tsmVIncrease.Enabled = false;
                    //tsmVDecrease.Enabled = false;
                    //tsmVRemove.Enabled = false;
                    //tsmBring.Enabled = false;
                    //tsmSend.Enabled = false;
                    //tsmUpdateRes.Enabled = false;
                    //tsmDeleteRes.Enabled = false;
                    //tsmProperties.Enabled = false;
                }


                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.AddSelectedControls()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // RemoveSelectedControls()
        //       - Remove control from pnlFMBDesign.SelectedControls
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		- ByVal ctrl As udcCtrlBase : Control to remove
        //
        public bool RemoveSelectedControls(udcCtrlBase ctrl)
        {

            try
            {
                bool bRemoved = false;
                //string sCtrlType;
                //string sSelectedType;

                //// Resource Type / Tag Type 정의
                //if (ctrl.CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                //{
                //    sCtrlType = "R:";
                //}
                //else
                //{
                //    sCtrlType = "T:";
                //}

                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {

                    //// Resource Type / Tag Type 인지 분기
                    //if (((udcCtrlBase) pnlFMBDesign.SelectedControls[i]).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                    //{
                    //    sSelectedType = "R:";
                    //}
                    //else
                    //{
                    //    sSelectedType = "T:";
                    //}

                    // 현재 Remove 하려는 Control를 찾아 선택리스트에서 삭제
                    //if (modCommonFunction.ToAsc(sSelectedType + ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).Name) == modCommonFunction.ToAsc(sCtrlType + ctrl.Name))
                    //if (pnlFMBDesign.SelectedControls[i].Name == ctrl.Name)
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).Name == ctrl.Name)
                    {
                        ctrl.IsSelected = false;
                        ctrl.IsFocused = false;
                        ctrl.RedrawCtrl();
                        pnlFMBDesign.SelectedControls.Remove(pnlFMBDesign.SelectedControls[i]);
                        bRemoved = true;

                        // 다른 선택되어진 Control이 있을 경우에 그 Control를 Focus를 활성화한다.
                        if (pnlFMBDesign.SelectedControlsCount() > 0)
                        {
                            ((udcCtrlBase)pnlFMBDesign.SelectedControls[pnlFMBDesign.SelectedControlsCount() - 1]).IsSelected = true;
                            ((udcCtrlBase)pnlFMBDesign.SelectedControls[pnlFMBDesign.SelectedControlsCount() - 1]).IsFocused = true;
                            ((udcCtrlBase)pnlFMBDesign.SelectedControls[pnlFMBDesign.SelectedControlsCount() - 1]).RedrawCtrl();
                        }
                        break;
                    }
                }

                if (pnlFMBDesign.SelectedControlsCount() == 1)
                {
                    // 1개의 Control이 선택되어진 경우
                    //tsmLefts.Enabled = false;
                    //tsmCenters.Enabled = false;
                    //tsmRights.Enabled = false;
                    //tsmTops.Enabled = false;
                    //tsmMiddles.Enabled = false;
                    //tsmBottoms.Enabled = false;
                    //tsmWidth.Enabled = false;
                    //tsmHeight.Enabled = false;
                    //tsmBoth.Enabled = false;
                    //tsmHMakeEqual.Enabled = false;
                    //tsmHIncrease.Enabled = false;
                    //tsmHDecrease.Enabled = false;
                    //tsmHRemove.Enabled = false;
                    //tsmVMakeEqual.Enabled = false;
                    //tsmVIncrease.Enabled = false;
                    //tsmVDecrease.Enabled = false;
                    //tsmVRemove.Enabled = false;
                    //tsmBring.Enabled = true;
                    //tsmSend.Enabled = true;
                    //tsmUpdateRes.Enabled = true;
                    //tsmDeleteRes.Enabled = true;
                    //tsmProperties.Enabled = true;
                }
                else if (pnlFMBDesign.SelectedControlsCount() > 1)
                {
                    // 2개 이상의 Control이 선택되어진 경우
                    //tsmLefts.Enabled = true;
                    //tsmCenters.Enabled = true;
                    //tsmRights.Enabled = true;
                    //tsmTops.Enabled = true;
                    //tsmMiddles.Enabled = true;
                    //tsmBottoms.Enabled = true;
                    //tsmWidth.Enabled = true;
                    //tsmHeight.Enabled = true;
                    //tsmBoth.Enabled = true;
                    //tsmHMakeEqual.Enabled = true;
                    //tsmHIncrease.Enabled = true;
                    //tsmHDecrease.Enabled = true;
                    //tsmHRemove.Enabled = true;
                    //tsmVMakeEqual.Enabled = true;
                    //tsmVIncrease.Enabled = true;
                    //tsmVDecrease.Enabled = true;
                    //tsmVRemove.Enabled = true;
                    //tsmBring.Enabled = false;
                    //tsmSend.Enabled = false;
                    //tsmUpdateRes.Enabled = false;
                    //tsmDeleteRes.Enabled = false;
                    //tsmProperties.Enabled = false;
                }
                else
                {
                    // 선택되어진 Control이 없을 경우 -> Format Menu 초기화
                    //tsmLefts.Enabled = false;
                    //tsmCenters.Enabled = false;
                    //tsmRights.Enabled = false;
                    //tsmTops.Enabled = false;
                    //tsmMiddles.Enabled = false;
                    //tsmBottoms.Enabled = false;
                    //tsmWidth.Enabled = false;
                    //tsmHeight.Enabled = false;
                    //tsmBoth.Enabled = false;
                    //tsmHMakeEqual.Enabled = false;
                    //tsmHIncrease.Enabled = false;
                    //tsmHDecrease.Enabled = false;
                    //tsmHRemove.Enabled = false;
                    //tsmVMakeEqual.Enabled = false;
                    //tsmVIncrease.Enabled = false;
                    //tsmVDecrease.Enabled = false;
                    //tsmVRemove.Enabled = false;
                    //tsmBring.Enabled = false;
                    //tsmSend.Enabled = false;
                    //tsmUpdateRes.Enabled = false;
                    //tsmDeleteRes.Enabled = false;
                    //tsmProperties.Enabled = false;
                }

                //SetToolBar();

                return bRemoved;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.RemoveSelectedControls()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // InitControls()
        //       - Initialize Controls
        // Return Value
        //       -
        // Arguments
        //		- ByVal bDesignMode As Boolean : Design mode flag
        //
        public void InitControls(bool bDesignMode)
        {

            try
            {
                Control ctrl;
                foreach (Control tempLoopVar_ctrl in pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        ((udcCtrlBase)ctrl).IsHot = false;
                        ((udcCtrlBase)ctrl).IsSelected = false;
                        ((udcCtrlBase)ctrl).IsFocused = false;
                        ((udcCtrlBase)ctrl).IsPressed = false;
                        ((udcCtrlBase)ctrl).IsModified = false;
                        ((udcCtrlBase)ctrl).IsDesignMode = bDesignMode;
                    }
                }

                int i;
                for (i = pnlFMBDesign.SelectedControlsCount(); i > 0; i--)
                {
                    pnlFMBDesign.SelectedControls.RemoveAt(i - 1);
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.InitControls()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // IsContains()
        //       - Check control is contained within pnlFMBDesign.SelectedControls
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		- ByVal ctrl As udcCtrlBase : Control to check
        //
        public bool IsSelectedContains(udcCtrlBase ctrl)
        {

            try
            {
                string sCtrlType;
                string sSelectedType;

                // Resource Type / Tag Type 정의
                if (ctrl.CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                {
                    sCtrlType = "R:";
                }
                else
                {
                    sCtrlType = "T:";
                }

                int i;
                for (i = 0; i < pnlFMBDesign.SelectedControlsCount(); i++)
                {

                    // Resource Type / Tag Type 인지 분기
                    if (((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
                    {
                        sSelectedType = "R:";
                    }
                    else
                    {
                        sSelectedType = "T:";
                    }

                    // 선택리스트에 속해있는 Control일 경우에 True를 Return
                    if (modCommonFunction.ToAsc(sSelectedType + ((udcCtrlBase)pnlFMBDesign.SelectedControls[i]).Name) == modCommonFunction.ToAsc(sCtrlType + ctrl.Name))
                    {
                        return true;
                    }
                }

                // 선택리스트에 없는 경우에 False를 Return
                return false;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.IsSelectedContains()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // IsModifiedControl()
        //       - Check control is modified
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        private bool IsModifiedControl()
        {

            try
            {
                Control ctrl;
                foreach (Control tempLoopVar_ctrl in pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        if (((udcCtrlBase)ctrl).CtrlStatus.IsSaveFlag == true)
                        {
                            return true;
                        }
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.IsModifiedControl()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // SetModifiedControl()
        //       - Set control is modified
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        private bool SetModifiedControl(bool bModified)
        {

            try
            {
                Control ctrl;
                foreach (Control tempLoopVar_ctrl in pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        ((udcCtrlBase)ctrl).CtrlStatus.IsSaveFlag = bModified;
                        ((udcCtrlBase)ctrl).CtrlStatus.ZoomScale = this.ZoomScale;
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.IsModifiedControl()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // UpdateResTag()
        //       - Update Resource/Tag
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        public bool UpdateResTag()
        {

            try
            {
                Control ctrl;
                foreach (Control tempLoopVar_ctrl in this.pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        if (((udcCtrlBase)ctrl).IsSelected == true)
                        {
                            CtrlContextMenu_EventArgs eventArgs = new CtrlContextMenu_EventArgs(null, ((udcCtrlBase)ctrl));
                            CtrlBase_CtrlUpdate(null, eventArgs);
                            return true;
                        }
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.UpdateResTag()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // DeleteResTag()
        //       - Delete Resource/Tag
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        public bool DeleteResTag()
        {

            try
            {
                Control ctrl;
                foreach (Control tempLoopVar_ctrl in this.pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        if (((udcCtrlBase)ctrl).IsSelected == true)
                        {
                            CtrlContextMenu_EventArgs eventArgs = new CtrlContextMenu_EventArgs(null, ((udcCtrlBase)ctrl));
                            CtrlBase_CtrlDelete(null, eventArgs);
                            return true;
                        }
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.UpdateResTag()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // PropertiesResTag()
        //       - Property Resource/Tag
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        public bool PropertiesResTag()
        {

            try
            {
                Control ctrl;
                foreach (Control tempLoopVar_ctrl in this.pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        if (((udcCtrlBase)ctrl).IsSelected == true)
                        {
                            CtrlContextMenu_EventArgs eventArgs = new CtrlContextMenu_EventArgs(null, ((udcCtrlBase)ctrl));
                            CtrlBase_CtrlProperties(null, eventArgs);
                            return true;
                        }
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.UpdateResTag()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // InitMainMenu()
        //       - Intialize Main Menu
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //

        // EnableMainMenu()
        //       - Enable Available Menu
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //


        // RefreshDeleteControls()
        //       - Refresh Control which is deleted
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        private void RefreshDeleteControls()
        {

            try
            {
                Control ctrl;
                foreach (Control tempLoopVar_ctrl in pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        if (((udcCtrlBase)ctrl).IsRefreshed == false)
                        {
                            pnlFMBDesign.Controls.Remove(ctrl);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.RefreshDeleteControls()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // SetRefreshedControl()
        //       - Set control is refreshed
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        private void SetRefreshedControl(bool bRefreshed)
        {

            try
            {
                Control ctrl;
                foreach (Control tempLoopVar_ctrl in pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        ((udcCtrlBase)ctrl).IsRefreshed = bRefreshed;
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.SetRefreshedControl()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        #endregion

        #region " Class SelectedControlSort Implementations"

        public class SelectedControlSort : IComparer
        {


            public enum SortOrder
            {
                LEFT_ORDER = 0,
                TOP_ORDER = 1,
                LEFT_ALL_ORDER = 2,
                TOP_ALL_ORDER = 3
            }

            private SortOrder m_iSortOrder;

            public SelectedControlSort(SortOrder eSortOrder)
            {

                m_iSortOrder = eSortOrder;

            }

            public int Compare(object x, object y)
            {

                int iCon1 = 0;
                int iCon2 = 0;

                switch (m_iSortOrder)
                {
                    case SortOrder.LEFT_ORDER:

                        iCon1 = ((udcCtrlBase)x).GetLocation().X;
                        iCon2 = ((udcCtrlBase)y).GetLocation().X;
                        break;
                    case SortOrder.TOP_ORDER:

                        iCon1 = ((udcCtrlBase)x).GetLocation().Y;
                        iCon2 = ((udcCtrlBase)y).GetLocation().Y;
                        break;
                    case SortOrder.LEFT_ALL_ORDER:

                        if (((udcCtrlBase)x).GetLocation().X == ((udcCtrlBase)y).GetLocation().X && ((udcCtrlBase)x).GetLocation().Y == ((udcCtrlBase)y).GetLocation().Y)
                        {
                            return 0;
                        }
                        else
                        {
                            if (((udcCtrlBase)x).GetLocation().X == ((udcCtrlBase)y).GetLocation().X)
                            {
                                iCon1 = ((udcCtrlBase)x).GetLocation().Y;
                                iCon2 = ((udcCtrlBase)y).GetLocation().Y;
                            }
                            else
                            {
                                iCon1 = ((udcCtrlBase)x).GetLocation().X;
                                iCon2 = ((udcCtrlBase)y).GetLocation().X;
                            }
                        }
                        break;
                    case SortOrder.TOP_ALL_ORDER:

                        if (((udcCtrlBase)x).GetLocation().X == ((udcCtrlBase)y).GetLocation().X && ((udcCtrlBase)x).GetLocation().Y == ((udcCtrlBase)y).GetLocation().Y)
                        {
                            return 0;
                        }
                        else
                        {
                            if (((udcCtrlBase)x).GetLocation().Y == ((udcCtrlBase)y).GetLocation().Y)
                            {
                                iCon1 = ((udcCtrlBase)x).GetLocation().X;
                                iCon2 = ((udcCtrlBase)y).GetLocation().X;
                            }
                            else
                            {

                                iCon1 = ((udcCtrlBase)x).GetLocation().Y;
                                iCon2 = ((udcCtrlBase)y).GetLocation().Y;
                            }
                        }
                        break;
                }

                return new CaseInsensitiveComparer().Compare(iCon1, iCon2);

            }

        }

        #endregion

        public void tsmLefts_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_Lefts();
                RedrawControls();

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmLefts_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmCenters_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_Centers();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmCenters_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmRights_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_Rights();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmRights_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmTops_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_Tops();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmTops_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmMiddles_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_Middles();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmMiddles_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmBottoms_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_Bottoms();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmBottoms_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }


        public void tsmWidth_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_Width();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmWidth_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmHeight_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_Height();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmHeight_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmBoth_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_Both();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmBoth_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }


        public void tsmHMakeEqual_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_HMakeEqual();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmHMakeEqual_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmHIncrease_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_HIncrease();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmHIncrease_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmHDecrease_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_HDecrease();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmHDecrease_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmHRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_HRemove();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmHRemove_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }
        public void tsmVMakeEqual_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_VMakeEqual();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmVMakeEqual_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmVIncrease_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_VIncrease();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmVIncrease_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmVDecrease_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_VDecrease();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmVDecrease_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmVRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 2)
                {
                    return;
                }
                Format_VRemove();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmVRemove_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmBring_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 1)
                {
                    return;
                }
                Format_BringToFront();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmBring_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDesignMode == false)
                {
                    return;
                }
                if (pnlFMBDesign.SelectedControlsCount() < 1)
                {
                    return;
                }
                Format_SendToBack();
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmSend_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmReload_Click(object sender, EventArgs e)
        {
            try
            {
                string sFactory = "";
                if (IsDesignMode == true)
                {
                    if (IsModifiedControl() == true)
                    {
                        if ((DialogResult)CmnFunction.ShowMsgBox(this.Name + " - " + modLanguageFunction.GetMessage(13), "FMB Client", MessageBoxButtons.YesNo, 1) == (DialogResult)Microsoft.VisualBasic.MsgBoxResult.No)
                        {
                            return;
                        }
                    }
                }
                if (ZoomScale != 0)
                {
                    ZoomScale = 0;
                    DesignSize = OriginalDesignSize;
                }
                //string sFactory;
                if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    sFactory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                }
                else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
                {
                    sFactory = GlobalVariable.gsFactory;
                }
                else
                {
                    return;
                }
                if (modCommonFunction.ViewGlobalOption(sFactory) == false)
                {
                    return;
                }
                if (RefreshResourceListDetail() == false)
                {
                    return;
                }
                if (SetModifiedControl(false) == false)
                {
                    return;
                }
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmReload_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmZoomIn_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                double dScale = new double();

                int iWidth;
                int iHeight;
                Size szNewSize;
                Control ctrl;
                this.Cursor = Cursors.WaitCursor;
                this.SuspendLayout();
                this.pnlFMBDesign.SuspendLayout();
                ZoomScale++;
                dScale = modCommonFunction.GetScale(ZoomScale);
                iWidth = 0;
                iHeight = 0;
                szNewSize = new Size(Convert.ToInt32(OriginalDesignSize.Width * dScale), Convert.ToInt32(OriginalDesignSize.Height * dScale));
                ctrl = new Control();
                foreach (Control tempLoopVar_ctrl in this.pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        ((udcCtrlBase)ctrl).CtrlStatus.ZoomScale = ZoomScale;
                        iWidth = Convert.ToInt32((((udcCtrlBase)ctrl).GetSize().Width + Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2) * modCommonFunction.GetScale(ZoomScale));
                        iHeight = Convert.ToInt32((((udcCtrlBase)ctrl).GetSize().Height + Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2) * modCommonFunction.GetScale(ZoomScale));
                        Size szNew = new Size(iWidth, iHeight);
                        ((udcCtrlBase)ctrl).Size = szNew;
                        Point ptNew = new Point(Convert.ToInt32((((udcCtrlBase)ctrl).GetLocation().X - Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE) * modCommonFunction.GetScale(ZoomScale)), Convert.ToInt32((((udcCtrlBase)ctrl).GetLocation().Y - Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE) * modCommonFunction.GetScale(ZoomScale)));
                        ((udcCtrlBase)ctrl).Location = ptNew;
                    }
                }
                DesignSize = szNewSize;
                this.pnlFMBDesign.ResumeLayout(false);
                this.ResumeLayout();
                this.Cursor = Cursors.Default;
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmZoomIn_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmZoomOut_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                double dScale = new double();

                int iWidth;
                int iHeight;
                Size szNewSize;
                Control ctrl;
                this.Cursor = Cursors.WaitCursor;
                this.SuspendLayout();
                this.pnlFMBDesign.SuspendLayout();
                ZoomScale--;
                dScale = modCommonFunction.GetScale(ZoomScale);
                iWidth = 0;
                iHeight = 0;
                szNewSize = new Size(Convert.ToInt32(OriginalDesignSize.Width * dScale), Convert.ToInt32(OriginalDesignSize.Height * dScale));
                ctrl = new Control();
                foreach (Control tempLoopVar_ctrl in this.pnlFMBDesign.Controls)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        ((udcCtrlBase)ctrl).CtrlStatus.ZoomScale = ZoomScale;
                        iWidth = Convert.ToInt32((((udcCtrlBase)ctrl).GetSize().Width + Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2) * modCommonFunction.GetScale(ZoomScale));
                        iHeight = Convert.ToInt32((((udcCtrlBase)ctrl).GetSize().Height + Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2) * modCommonFunction.GetScale(ZoomScale));
                        if (((udcCtrlBase)ctrl).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.HorizontalLine || ((udcCtrlBase)ctrl).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.VerticalLine)
                        {
                            if (iWidth < Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2 + modGlobalConstant.LINE_MININUM_SIZE)
                            {
                                iWidth = Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2 + modGlobalConstant.LINE_MININUM_SIZE;
                            }
                            if (iHeight < Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2 + modGlobalConstant.LINE_MININUM_SIZE)
                            {
                                iHeight = Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2 + modGlobalConstant.LINE_MININUM_SIZE;
                            }
                        }
                        else
                        {
                            if (iWidth < Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2 + modGlobalConstant.CTRL_MININUM_SIZE)
                            {
                                iWidth = Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2 + modGlobalConstant.CTRL_MININUM_SIZE;
                            }
                            if (iHeight < Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2 + modGlobalConstant.CTRL_MININUM_SIZE)
                            {
                                iHeight = Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE * 2 + modGlobalConstant.CTRL_MININUM_SIZE;
                            }
                        }
                        Size szNew = new Size(iWidth, iHeight);
                        ((udcCtrlBase)ctrl).Size = szNew;
                        Point ptNew = new Point(Convert.ToInt32((((udcCtrlBase)ctrl).GetLocation().X - Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE) * modCommonFunction.GetScale(ZoomScale)), Convert.ToInt32((((udcCtrlBase)ctrl).GetLocation().Y - Miracom.FMBUI.Controls.modDefines.CTRL_TRACKER_SIZE) * modCommonFunction.GetScale(ZoomScale)));
                        ((udcCtrlBase)ctrl).Location = ptNew;
                    }
                }
                DesignSize = szNewSize;
                this.pnlFMBDesign.ResumeLayout(false);
                this.ResumeLayout();
                this.Cursor = Cursors.Default;
                RedrawControls();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmFMBDesign.tsmZoomOut_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
        }

        public void tsmDesignMode_Click(object sender, EventArgs e)
        {
            if (IsDesignMode == false)
            {
                if (ZoomScale != 0)
                {
                    ZoomScale = 0;
                    DesignSize = OriginalDesignSize;
                    if (RefreshResourceListDetail() == false)
                    {
                        return;
                    }
                }
                IsDesignMode = true;
                //tsmTopFormat.Enabled = true;
                if (SetModifiedControl(false) == false)
                {
                    return;
                }
                //tsmDesignMode.Checked = true;
            }
            else
            {
                IsDesignMode = false;
                //tsmTopFormat.Enabled = false;
                //tsmDesignMode.Checked = false;
            }
        }

        public void tsmSaveDesign_Click(object sender, EventArgs e)
        {
            if (IsDesignMode == false)
            {
                return;
            }

            if (UpdateResourceListDetail() == true)
            {
                CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
            }
            else
            {
                return;
            }
        }

        public void tsmAddRes_Click(object sender, EventArgs e)
        {
            if (IsDesignMode == false)
            {
                return;
            }
            string sGroupID = "";
            string sLayOut = "";
            string sFactory = "";
            if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
            {
                sFactory = modCommonFunction.GetStringBySeperator(Name, ":", 1);
                sLayOut = modCommonFunction.GetStringBySeperator(Name, ":", 2);
            }
            else if (System.Convert.ToString(Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
            {
                sFactory = GlobalVariable.gsFactory;
                sGroupID = Name;
            }
            else
            {
                return;
            }
            frmFMBAddMultiResources form = new frmFMBAddMultiResources(sFactory, sGroupID, sLayOut);
            form.Tag = Tag;
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                int i;
                for (i = 0; i <= form.lisResourceList.CheckedItems.Count - 1; i++)
                {
                    clsCtrlStatus ResourceStatus = new clsCtrlStatus();
                    ResourceStatus.Key = form.lisResourceList.CheckedItems[i].Text;
                    ResourceStatus.SetLocation(new Point(CmnFunction.ToInt(form.lisResourceList.CheckedItems[i].SubItems[11].Text), CmnFunction.ToInt(form.lisResourceList.CheckedItems[i].SubItems[12].Text)));
                    ResourceStatus.SetSize(new Size(CmnFunction.ToInt(form.lisResourceList.CheckedItems[i].SubItems[9].Text), CmnFunction.ToInt(form.lisResourceList.CheckedItems[i].SubItems[10].Text)));
                    ResourceStatus.Text = form.lisResourceList.CheckedItems[i].SubItems[4].Text;
                    if (Color.FromName(form.lisResourceList.CheckedItems[i].SubItems[5].Text).ToKnownColor() > 0)
                    {
                        ResourceStatus.TextColor = CmnFunction.ToInt(Color.FromName(form.lisResourceList.CheckedItems[i].SubItems[5].Text).ToKnownColor());
                    }
                    else
                    {
                        ResourceStatus.TextColor = modCommonFunction.ConvertStringToColor(form.lisResourceList.CheckedItems[i].SubItems[5].Text);
                    }
                    if (Color.FromName(form.lisResourceList.CheckedItems[i].SubItems[6].Text).ToKnownColor() > 0)
                    {
                        ResourceStatus.BackColor = CmnFunction.ToInt(Color.FromName(form.lisResourceList.CheckedItems[i].SubItems[6].Text).ToKnownColor());
                    }
                    else
                    {
                        ResourceStatus.BackColor = modCommonFunction.ConvertStringToColor(form.lisResourceList.CheckedItems[i].SubItems[6].Text);
                    }
                    ResourceStatus.TextFontName = System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultFontName));
                    if (form.lisResourceList.CheckedItems[i].SubItems[7].Text == "")
                    {
                        ResourceStatus.TextSize = 0;
                    }
                    else
                    {
                        ResourceStatus.TextSize = CmnFunction.ToInt(form.lisResourceList.CheckedItems[i].SubItems[7].Text);
                    }
                    ResourceStatus.TextStyle = CmnFunction.ToInt(@Enum.Parse(typeof(FontStyle), CmnFunction.Trim(form.lisResourceList.CheckedItems[i].SubItems[8].Text)));
                    ResourceStatus.ToolType = Miracom.FMBUI.Enums.eToolType.Resource;
                    ResourceStatus.LastEvent = form.lisResourceList.CheckedItems[i].SubItems[13].Text;
                    ResourceStatus.PrimaryStatus = form.lisResourceList.CheckedItems[i].SubItems[14].Text;
                    ResourceStatus.ProcMode = form.lisResourceList.CheckedItems[i].SubItems[15].Text;
                    ResourceStatus.CtrlMode = form.lisResourceList.CheckedItems[i].SubItems[16].Text;
                    ResourceStatus.ResourceType = form.lisResourceList.CheckedItems[i].SubItems[18].Text;
                    ResourceStatus.UpDownFlag = form.lisResourceList.CheckedItems[i].SubItems[17].Text;
                    ResourceStatus.AreaID = form.lisResourceList.CheckedItems[i].SubItems[2].Text;
                    ResourceStatus.SubAreaID = form.lisResourceList.CheckedItems[i].SubItems[3].Text;
                    ResourceStatus.ResTagFlag = "R";
                    if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.IsProcessMode)) == "P")
                    {
                        ResourceStatus.IsProcessMode = true;
                    }
                    else
                    {
                        ResourceStatus.IsProcessMode = false;
                    }
                    if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.UseEventColor)) == "Y")
                    {
                        ResourceStatus.IsUseEventColor = true;
                        ResourceStatus.EventBackColor = modGlobalVariable.gGlobalOptions.GetOptions(sFactory, ResourceStatus.LastEvent).ToArgb();
                    }
                    else
                    {
                        ResourceStatus.IsUseEventColor = false;
                    }
                    ResourceStatus.ImageIndex = CmnFunction.ToInt(form.lisResourceList.CheckedItems[i].SubItems[19].Text);
                    ResourceStatus.IsViewSignal = true;

                    if (AddControl(ResourceStatus, true, false) == false)
                    {
                        return;
                    }
                }
                if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    if (modInterface.gIMdiForm.RefreshDesignList("5", sFactory, sLayOut, "", "") == false)
                    {
                        return;
                    }
                }
            }
        }

        public void tsmUpdateRes_Click(object sender, EventArgs e)
        {
            if (IsDesignMode == true)
            {
                UpdateResTag();
            }
        }

        public void tsmDeleteRes_Click(object sender, EventArgs e)
        {
            if (IsDesignMode == true)
            {
                DeleteResTag();
            }
        }

        public void tsmProperties_Click(object sender, EventArgs e)
        {
            if (IsDesignMode == true)
            {
                PropertiesResTag();
            }
        }

             


        
    }
}