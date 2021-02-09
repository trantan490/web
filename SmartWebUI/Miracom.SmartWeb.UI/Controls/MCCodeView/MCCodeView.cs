using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using System.ComponentModel;

namespace Miracom.UI
{
    namespace Controls
    {
        namespace MCCodeView
        {

            [DefaultEvent("SelectedItemChanged")]
            public class MCCodeView : Miracom.UI.Controls.MCControlBase
            {
                #region " Event Handler"

                private MCCodeViewSelChangedHandler SelectedItemChangedEvent;
                public event MCCodeViewSelChangedHandler SelectedItemChanged
                {
                    add
                    {
                        SelectedItemChangedEvent = (MCCodeViewSelChangedHandler)System.Delegate.Combine(SelectedItemChangedEvent, value);
                    }
                    remove
                    {
                        SelectedItemChangedEvent = (MCCodeViewSelChangedHandler)System.Delegate.Remove(SelectedItemChangedEvent, value);
                    }
                }

                private MCCodeViewButtonPressedHandler ButtonPressedEvent;
                public event MCCodeViewButtonPressedHandler ButtonPressed
                {
                    add
                    {
                        ButtonPressedEvent = (MCCodeViewButtonPressedHandler)System.Delegate.Combine(ButtonPressedEvent, value);
                    }
                    remove
                    {
                        ButtonPressedEvent = (MCCodeViewButtonPressedHandler)System.Delegate.Remove(ButtonPressedEvent, value);
                    }
                }

                private System.EventHandler ButtonPressEvent;
                public event System.EventHandler ButtonPress
                {
                    add
                    {
                        ButtonPressEvent = (System.EventHandler)System.Delegate.Combine(ButtonPressEvent, value);
                    }
                    remove
                    {
                        ButtonPressEvent = (System.EventHandler)System.Delegate.Remove(ButtonPressEvent, value);
                    }
                }

                private System.Windows.Forms.KeyPressEventHandler TextBoxKeyPressEvent;
                public event System.Windows.Forms.KeyPressEventHandler TextBoxKeyPress
                {
                    add
                    {
                        TextBoxKeyPressEvent = (System.Windows.Forms.KeyPressEventHandler)System.Delegate.Combine(TextBoxKeyPressEvent, value);
                    }
                    remove
                    {
                        TextBoxKeyPressEvent = (System.Windows.Forms.KeyPressEventHandler)System.Delegate.Remove(TextBoxKeyPressEvent, value);
                    }
                }

                private System.EventHandler TextBoxTextChangedEvent;
                public event System.EventHandler TextBoxTextChanged
                {
                    add
                    {
                        TextBoxTextChangedEvent = (System.EventHandler)System.Delegate.Combine(TextBoxTextChangedEvent, value);
                    }
                    remove
                    {
                        TextBoxTextChangedEvent = (System.EventHandler)System.Delegate.Remove(TextBoxTextChangedEvent, value);
                    }
                }

                private System.EventHandler TextBoxLostFocusEvent;
                public event System.EventHandler TextBoxLostFocus
                {
                    add
                    {
                        TextBoxLostFocusEvent = (System.EventHandler)System.Delegate.Combine(TextBoxLostFocusEvent, value);
                    }
                    remove
                    {
                        TextBoxLostFocusEvent = (System.EventHandler)System.Delegate.Remove(TextBoxLostFocusEvent, value);
                    }
                }

                private System.EventHandler TextBoxGotFocusEvent;
            
                public event System.EventHandler TextBoxGotFocus
                {
                    add
                    {
                        TextBoxGotFocusEvent = (System.EventHandler)System.Delegate.Combine(TextBoxGotFocusEvent, value);
                    }
                    remove
                    {
                        TextBoxGotFocusEvent = (System.EventHandler)System.Delegate.Remove(TextBoxGotFocusEvent, value);
                    }
                }

                #endregion

                #region " Windows Form 디자이너에서 생성한 코드 "

                private MCCodeViewPopup m_MCCodeViewPopup = null;
                protected ImageList imlSmallIcon;
                private TextBox txtDesc;
                private MCCodeViewPopup_MS m_MCCodeViewPopup_MS = null;

                public MCCodeView()
                {
                    //이 호출은 Windows Form 디자이너에 필요합니다.
                    InitializeComponent();

                    //InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

                    this.btnButton.Visible = VisibleButton;

                }

                //UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
                protected override void Dispose(bool disposing)
                {
                    if (disposing)
                    {
                        if (this.m_MCCodeViewPopup != null)
                        {
                            this.m_MCCodeViewPopup.Dispose();
                            this.m_MCCodeViewPopup = null;
                        }

                        if (this.m_MCCodeViewPopup_MS != null)
                        {
                            this.m_MCCodeViewPopup_MS.Dispose();
                            this.m_MCCodeViewPopup_MS = null;
                        }

                        if (!(components == null))
                        {
                            components.Dispose();
                        }
                    }

                    base.Dispose(disposing);
                }

                private IContainer components;

                private System.Windows.Forms.ToolTip tipToolTip;
                private System.Windows.Forms.Panel pnlText;
                private System.Windows.Forms.TextBox txtCode;
                private System.Windows.Forms.TextBox txtCode1;       
                private System.Windows.Forms.Button btnButton;
                private System.Windows.Forms.TextBox txtDisplay;
                private System.Windows.Forms.Panel pnlDesc;
                [System.Diagnostics.DebuggerStepThrough()]
                private void InitializeComponent()
                {
                    this.components = new System.ComponentModel.Container();
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MCCodeView));
                    this.tipToolTip = new System.Windows.Forms.ToolTip(this.components);
                    this.pnlText = new System.Windows.Forms.Panel();
                    this.txtDisplay = new System.Windows.Forms.TextBox();
                    this.btnButton = new System.Windows.Forms.Button();
                    this.txtCode = new System.Windows.Forms.TextBox();
                    this.txtCode1 = new System.Windows.Forms.TextBox();
                    this.pnlDesc = new System.Windows.Forms.Panel();
                    this.txtDesc = new System.Windows.Forms.TextBox();
                    this.m_MCCodeViewPopup = new Miracom.UI.Controls.MCCodeView.MCCodeViewPopup();
                    this.m_MCCodeViewPopup_MS = new Miracom.UI.Controls.MCCodeView.MCCodeViewPopup_MS();
                    this.imlSmallIcon = new System.Windows.Forms.ImageList(this.components);
                    this.pnlText.SuspendLayout();
                    this.pnlDesc.SuspendLayout();
                    ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
                    this.SuspendLayout();
                    // 
                    // pnlText
                    // 
                    this.pnlText.BackColor = System.Drawing.Color.Transparent;
                    this.pnlText.Controls.Add(this.txtDisplay);
                    this.pnlText.Controls.Add(this.btnButton);
                    this.pnlText.Controls.Add(this.txtCode);
                    this.pnlText.Controls.Add(this.txtCode1);
                    this.pnlText.Dock = System.Windows.Forms.DockStyle.Left;
                    this.pnlText.Location = new System.Drawing.Point(0, 0);
                    this.pnlText.Name = "pnlText";
                    this.pnlText.Size = new System.Drawing.Size(150, 20);
                    this.pnlText.TabIndex = 1;
                    // 
                    // txtDisplay
                    // 
                    this.txtDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.txtDisplay.Location = new System.Drawing.Point(0, 0);
                    this.txtDisplay.Name = "txtDisplay";
                    this.txtDisplay.Size = new System.Drawing.Size(129, 20);
                    this.txtDisplay.TabIndex = 0;
                    this.txtDisplay.TextChanged += new System.EventHandler(this.txtDisplay_TextChanged);
                    this.txtDisplay.GotFocus += new System.EventHandler(this.txtDisplay_GotFocus);
                    this.txtDisplay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDisplay_KeyDown);
                    this.txtDisplay.Leave += new System.EventHandler(this.txtDisplay_LostFocus);
                    this.txtDisplay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDisplay_KeyPress);
                    this.txtDisplay.Enter += new System.EventHandler(this.txtDisplay_GotFocus);
                    this.txtDisplay.LostFocus += new System.EventHandler(this.txtDisplay_LostFocus);
                    // 
                    // btnButton
                    // 
                    this.btnButton.BackColor = System.Drawing.SystemColors.Control;
                    this.btnButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                    this.btnButton.Cursor = System.Windows.Forms.Cursors.Hand;
                    this.btnButton.Dock = System.Windows.Forms.DockStyle.Right;
                    this.btnButton.FlatAppearance.BorderSize = 0;
                    this.btnButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
                    this.btnButton.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.btnButton.Location = new System.Drawing.Point(129, 0);
                    this.btnButton.Name = "btnButton";
                    this.btnButton.Size = new System.Drawing.Size(21, 20);
                    this.btnButton.TabIndex = 1;
                    this.btnButton.Text = "...";
                    this.btnButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                    this.btnButton.UseVisualStyleBackColor = false;
                    this.btnButton.Click += new System.EventHandler(this.Button_Click);
                    // 
                    // txtCode
                    // 
                    this.txtCode.Location = new System.Drawing.Point(0, 0);
                    this.txtCode.Name = "txtCode";
                    this.txtCode.Size = new System.Drawing.Size(150, 20);
                    this.txtCode.TabIndex = 4;
                    this.txtCode.Visible = false;
                    // 
                    // txtCode1
                    // 
                    this.txtCode1.Location = new System.Drawing.Point(0, 0);
                    this.txtCode1.Name = "txtCode1";
                    this.txtCode1.Size = new System.Drawing.Size(150, 20);
                    this.txtCode1.TabIndex = 5;
                    this.txtCode1.Visible = false;
                    // 
                    // pnlDesc
                    // 
                    this.pnlDesc.BackColor = System.Drawing.Color.Transparent;
                    this.pnlDesc.Controls.Add(this.txtDesc);
                    this.pnlDesc.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.pnlDesc.Location = new System.Drawing.Point(150, 0);
                    this.pnlDesc.Name = "pnlDesc";
                    this.pnlDesc.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
                    this.pnlDesc.Size = new System.Drawing.Size(150, 20);
                    this.pnlDesc.TabIndex = 0;
                    this.pnlDesc.Visible = false;
                    // 
                    // txtDesc
                    // 
                    this.txtDesc.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.txtDesc.Location = new System.Drawing.Point(1, 0);
                    this.txtDesc.Name = "txtDesc";
                    this.txtDesc.ReadOnly = true;
                    this.txtDesc.Size = new System.Drawing.Size(149, 20);
                    this.txtDesc.TabIndex = 1;
                    this.txtDesc.TabStop = false;
                    // 
                    // m_MCCodeViewPopup
                    // 
                    this.m_MCCodeViewPopup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                    this.m_MCCodeViewPopup.BackColor = System.Drawing.Color.White;
                    this.m_MCCodeViewPopup.ClientSize = new System.Drawing.Size(127, 128);
                    this.m_MCCodeViewPopup.ControlBox = false;
                    this.m_MCCodeViewPopup.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    this.m_MCCodeViewPopup.Location = new System.Drawing.Point(0, 0);
                    this.m_MCCodeViewPopup.MaximizeBox = false;
                    this.m_MCCodeViewPopup.MinimizeBox = false;
                    this.m_MCCodeViewPopup.Name = "m_MCCodeViewPopup";
                    this.m_MCCodeViewPopup.Position = new System.Drawing.Point(0, 0);
                    this.m_MCCodeViewPopup.ShowInTaskbar = false;
                    this.m_MCCodeViewPopup.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                    this.m_MCCodeViewPopup.Text = "MCPopupFormBase";
                    this.m_MCCodeViewPopup.Visible = false;
                    this.m_MCCodeViewPopup.SelectionChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.OnPopUp_SelectionChanged);
                    // 
                    // m_MCCodeViewPopup_MS
                    // 
                    this.m_MCCodeViewPopup_MS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                    this.m_MCCodeViewPopup_MS.BackColor = System.Drawing.Color.White;
                    this.m_MCCodeViewPopup_MS.ClientSize = new System.Drawing.Size(127, 128);
                    this.m_MCCodeViewPopup_MS.ControlBox = false;
                    this.m_MCCodeViewPopup_MS.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    this.m_MCCodeViewPopup_MS.Location = new System.Drawing.Point(0, 0);
                    this.m_MCCodeViewPopup_MS.MaximizeBox = false;
                    this.m_MCCodeViewPopup_MS.MinimizeBox = false;
                    this.m_MCCodeViewPopup_MS.Name = "m_MCCodeViewPopup_MS";
                    this.m_MCCodeViewPopup_MS.Position = new System.Drawing.Point(0, 0);
                    this.m_MCCodeViewPopup_MS.ShowInTaskbar = false;
                    this.m_MCCodeViewPopup_MS.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                    this.m_MCCodeViewPopup_MS.Text = "MCPopupFormBase";
                    this.m_MCCodeViewPopup_MS.Visible = false;
                    this.m_MCCodeViewPopup_MS.SelectionChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.OnPopUp_SelectionChanged);
                    this.m_MCCodeViewPopup_MS.ButtonPressed += new Miracom.UI.MCCodeViewButtonPressedHandler(this.OnPopUp_ButtonPressed);
                    // 
                    // imlSmallIcon
                    // 
                    this.imlSmallIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlSmallIcon.ImageStream")));
                    this.imlSmallIcon.TransparentColor = System.Drawing.Color.Transparent;
                    this.imlSmallIcon.Images.SetKeyName(0, "");
                    this.imlSmallIcon.Images.SetKeyName(1, "");
                    this.imlSmallIcon.Images.SetKeyName(2, "");
                    this.imlSmallIcon.Images.SetKeyName(3, "");
                    this.imlSmallIcon.Images.SetKeyName(4, "");
                    this.imlSmallIcon.Images.SetKeyName(5, "");
                    this.imlSmallIcon.Images.SetKeyName(6, "");
                    this.imlSmallIcon.Images.SetKeyName(7, "");
                    this.imlSmallIcon.Images.SetKeyName(8, "");
                    this.imlSmallIcon.Images.SetKeyName(9, "");
                    this.imlSmallIcon.Images.SetKeyName(10, "");
                    this.imlSmallIcon.Images.SetKeyName(11, "");
                    this.imlSmallIcon.Images.SetKeyName(12, "");
                    this.imlSmallIcon.Images.SetKeyName(13, "");
                    this.imlSmallIcon.Images.SetKeyName(14, "");
                    this.imlSmallIcon.Images.SetKeyName(15, "");
                    this.imlSmallIcon.Images.SetKeyName(16, "");
                    this.imlSmallIcon.Images.SetKeyName(17, "");
                    this.imlSmallIcon.Images.SetKeyName(18, "");
                    this.imlSmallIcon.Images.SetKeyName(19, "");
                    this.imlSmallIcon.Images.SetKeyName(20, "");
                    this.imlSmallIcon.Images.SetKeyName(21, "");
                    this.imlSmallIcon.Images.SetKeyName(22, "");
                    this.imlSmallIcon.Images.SetKeyName(23, "");
                    this.imlSmallIcon.Images.SetKeyName(24, "");
                    this.imlSmallIcon.Images.SetKeyName(25, "");
                    this.imlSmallIcon.Images.SetKeyName(26, "");
                    this.imlSmallIcon.Images.SetKeyName(27, "");
                    this.imlSmallIcon.Images.SetKeyName(28, "");
                    this.imlSmallIcon.Images.SetKeyName(29, "");
                    this.imlSmallIcon.Images.SetKeyName(30, "");
                    this.imlSmallIcon.Images.SetKeyName(31, "");
                    this.imlSmallIcon.Images.SetKeyName(32, "");
                    this.imlSmallIcon.Images.SetKeyName(33, "");
                    this.imlSmallIcon.Images.SetKeyName(34, "");
                    this.imlSmallIcon.Images.SetKeyName(35, "");
                    this.imlSmallIcon.Images.SetKeyName(36, "");
                    this.imlSmallIcon.Images.SetKeyName(37, "");
                    this.imlSmallIcon.Images.SetKeyName(38, "");
                    this.imlSmallIcon.Images.SetKeyName(39, "");
                    this.imlSmallIcon.Images.SetKeyName(40, "");
                    this.imlSmallIcon.Images.SetKeyName(41, "");
                    this.imlSmallIcon.Images.SetKeyName(42, "");
                    this.imlSmallIcon.Images.SetKeyName(43, "");
                    this.imlSmallIcon.Images.SetKeyName(44, "");
                    this.imlSmallIcon.Images.SetKeyName(45, "");
                    this.imlSmallIcon.Images.SetKeyName(46, "");
                    this.imlSmallIcon.Images.SetKeyName(47, "");
                    this.imlSmallIcon.Images.SetKeyName(48, "");
                    this.imlSmallIcon.Images.SetKeyName(49, "");
                    this.imlSmallIcon.Images.SetKeyName(50, "");
                    this.imlSmallIcon.Images.SetKeyName(51, "");
                    this.imlSmallIcon.Images.SetKeyName(52, "");
                    this.imlSmallIcon.Images.SetKeyName(53, "");
                    this.imlSmallIcon.Images.SetKeyName(54, "");
                    this.imlSmallIcon.Images.SetKeyName(55, "");
                    this.imlSmallIcon.Images.SetKeyName(56, "");
                    this.imlSmallIcon.Images.SetKeyName(57, "");
                    this.imlSmallIcon.Images.SetKeyName(58, "");
                    this.imlSmallIcon.Images.SetKeyName(59, "");
                    this.imlSmallIcon.Images.SetKeyName(60, "");
                    this.imlSmallIcon.Images.SetKeyName(61, "");
                    this.imlSmallIcon.Images.SetKeyName(62, "");
                    this.imlSmallIcon.Images.SetKeyName(63, "");
                    this.imlSmallIcon.Images.SetKeyName(64, "");
                    this.imlSmallIcon.Images.SetKeyName(65, "");
                    this.imlSmallIcon.Images.SetKeyName(66, "");
                    this.imlSmallIcon.Images.SetKeyName(67, "");
                    this.imlSmallIcon.Images.SetKeyName(68, "");
                    this.imlSmallIcon.Images.SetKeyName(69, "");
                    this.imlSmallIcon.Images.SetKeyName(70, "");
                    this.imlSmallIcon.Images.SetKeyName(71, "");
                    this.imlSmallIcon.Images.SetKeyName(72, "");
                    this.imlSmallIcon.Images.SetKeyName(73, "");
                    this.imlSmallIcon.Images.SetKeyName(74, "");
                    this.imlSmallIcon.Images.SetKeyName(75, "");
                    this.imlSmallIcon.Images.SetKeyName(76, "");
                    this.imlSmallIcon.Images.SetKeyName(77, "");
                    this.imlSmallIcon.Images.SetKeyName(78, "");
                    this.imlSmallIcon.Images.SetKeyName(79, "");
                    this.imlSmallIcon.Images.SetKeyName(80, "");
                    this.imlSmallIcon.Images.SetKeyName(81, "");
                    this.imlSmallIcon.Images.SetKeyName(82, "");
                    this.imlSmallIcon.Images.SetKeyName(83, "");
                    this.imlSmallIcon.Images.SetKeyName(84, "");
                    this.imlSmallIcon.Images.SetKeyName(85, "");
                    this.imlSmallIcon.Images.SetKeyName(86, "");
                    this.imlSmallIcon.Images.SetKeyName(87, "");
                    this.imlSmallIcon.Images.SetKeyName(88, "");
                    this.imlSmallIcon.Images.SetKeyName(89, "");
                    this.imlSmallIcon.Images.SetKeyName(90, "");
                    this.imlSmallIcon.Images.SetKeyName(91, "");
                    this.imlSmallIcon.Images.SetKeyName(92, "");
                    this.imlSmallIcon.Images.SetKeyName(93, "");
                    this.imlSmallIcon.Images.SetKeyName(94, "");
                    this.imlSmallIcon.Images.SetKeyName(95, "");
                    this.imlSmallIcon.Images.SetKeyName(96, "");
                    this.imlSmallIcon.Images.SetKeyName(97, "");
                    this.imlSmallIcon.Images.SetKeyName(98, "");
                    this.imlSmallIcon.Images.SetKeyName(99, "");
                    this.imlSmallIcon.Images.SetKeyName(100, "");
                    this.imlSmallIcon.Images.SetKeyName(101, "");
                    this.imlSmallIcon.Images.SetKeyName(102, "");
                    this.imlSmallIcon.Images.SetKeyName(103, "");
                    this.imlSmallIcon.Images.SetKeyName(104, "White Image");
                    // 
                    // MCCodeView
                    // 
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                    this.BackColor = System.Drawing.SystemColors.Window;
                    this.Controls.Add(this.pnlDesc);
                    this.Controls.Add(this.pnlText);
                    this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.MCViewStyle.BorderColor = System.Drawing.Color.DarkGray;
                    this.MCViewStyle.BorderHotColor = System.Drawing.Color.Black;
                    this.Name = "MCCodeView";
                    this.Size = new System.Drawing.Size(300, 20);
                    this.FontChanged += new System.EventHandler(this.MCCodeView_FontChanged);
                    this.Resize += new System.EventHandler(this.MCCodeView_Resize);
                    this.pnlText.ResumeLayout(false);
                    this.pnlText.PerformLayout();
                    this.pnlDesc.ResumeLayout(false);
                    this.pnlDesc.PerformLayout();
                    ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
                    this.ResumeLayout(false);

                }

                #endregion

                #region "Properties Implements"

                private bool m_IsKeyPress = false;
                private string m_KeyPressText = "";

                private ListViewItem m_SelectedItem = null;
                private ListViewItem[] m_SelectedItem_ms = null;

                private Size m_DropDownSize;
                private int m_SelectedSubItemIndex = -1;
                private int m_DisplaySubItemIndex = -1;                         // 2006.07.19. Aiden Koo
                private int m_SelectedDescIndex = -1;                           // 2006.07.19. Aiden Koo
                private FlatStyle m_btnFlatStyle = FlatStyle.System;
                private bool m_bIsViewBtnImage = false;
                private string m_sBtnToolTipText = "";
                private string m_sTextBoxToolTipText = "";
                private System.Windows.Forms.BorderStyle m_StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
                private int m_iButtonWidth = 21;
                private bool m_bVisibleButton = true;
                private bool m_bVisibleDesc = false;
                private bool m_bVisibleColumnHeader = false;
                private int m_iIndex;
                private bool m_bReadOnly = false;
                private bool m_bIsPopup = true;
                private Color m_crBackColor = SystemColors.Window;
                private int m_iMaxLength = 32767;
                private object m_objFocusing = null;
                private int m_SearchSubItemIndex = 0;
                private bool m_MultiSelect = false;                             // 2008.09.23. W.Y Choi

                public object Focusing
                {
                    get
                    {
                        return m_objFocusing;
                    }
                    set
                    {
                        m_objFocusing = value;
                    }
                }

                public ListView GetListView
                {
                    get
                    {
                        tipToolTip.Active = false;
                        if (m_MultiSelect == false)
                        {
                            return ((ListView)m_MCCodeViewPopup.m_MCCodeDropList);
                        }
                        else
                        {
                            return ((ListView)m_MCCodeViewPopup_MS.m_MCCodeDropList);
                        }
                    }
                }

                public FlatStyle BtnFlatStyle
                {
                    get
                    {
                        return m_btnFlatStyle;
                    }
                    set
                    {
                        m_btnFlatStyle = value;
                        btnButton.FlatStyle = m_btnFlatStyle;
                        if (value == FlatStyle.System)
                        {
                            IsViewBtnImage = false;
                        }
                    }
                }

                public bool IsViewBtnImage
                {
                    get
                    {
                        return m_bIsViewBtnImage;
                    }
                    set
                    {
                        m_bIsViewBtnImage = value;
                        if (m_bIsViewBtnImage == true)
                        {
                            btnButton.ImageIndex = 1;
                            btnButton.Text = "";
                            btnButton.FlatStyle = FlatStyle.Flat;
                            btnButton.FlatAppearance.BorderSize = 0;
                            btnButton.BackgroundImage = global::Miracom.SmartWeb.UI.Properties.Resources.list01;
                            //if (BtnFlatStyle == FlatStyle.System)
                            //{
                            //    BtnFlatStyle = FlatStyle.Flat;
                            //    btnButton.FlatAppearance.BorderSize = 0;
                            //}
                        }
                        else
                        {
                            btnButton.ImageIndex = -1;
                            btnButton.FlatStyle = FlatStyle.System;
                            btnButton.BackgroundImage = null;
                            btnButton.Text = "...";
                            btnButton.Invalidate(false);
                        }
                    }
                }

                public string BtnToolTipText
                {
                    get
                    {
                        return m_sBtnToolTipText;
                    }
                    set
                    {
                        m_sBtnToolTipText = value;
                        tipToolTip.SetToolTip(btnButton, m_sBtnToolTipText);
                    }
                }

                public string TextBoxToolTipText
                {
                    get
                    {
                        return m_sTextBoxToolTipText;
                    }
                    set
                    {
                        m_sTextBoxToolTipText = value;
                        tipToolTip.SetToolTip(txtDisplay, m_sTextBoxToolTipText);
                    }
                }

                public BorderStyle StyleBorder
                {
                    get
                    {
                        return m_StyleBorder;
                    }
                    set
                    {
                        m_StyleBorder = value;
                        txtDisplay.BorderStyle = m_StyleBorder;
                    }
                }

                public ImageList SmallImageList
                {
                    get
                    {
                        if (m_MultiSelect == false)
                        {
                            return m_MCCodeViewPopup.m_MCCodeDropList.SmallImageList;
                        }
                        else
                        {
                            if (m_MultiSelect == false)
                            {
                                return m_MCCodeViewPopup.m_MCCodeDropList.SmallImageList;
                            }
                            else
                            {
                                return m_MCCodeViewPopup_MS.m_MCCodeDropList.SmallImageList;
                            }
                        }
                    }
                    set
                    {
                        if (m_MultiSelect == false)
                        {
                            m_MCCodeViewPopup.m_MCCodeDropList.SmallImageList = value;
                        }
                        else
                        {
                            m_MCCodeViewPopup_MS.m_MCCodeDropList.SmallImageList = value;
                        }
                    }
                }

                public int SelectedSubItemIndex
                {
                    get
                    {
                        return m_SelectedSubItemIndex;
                    }
                    set
                    {
                        m_SelectedSubItemIndex = value;

                        // 2006.07.19. Aiden Koo
                        if (m_DisplaySubItemIndex == -1)
                        {
                            m_DisplaySubItemIndex = value;
                            if (m_MultiSelect == false)
                            {
                                m_MCCodeViewPopup.DisplaySubItemIndex = value;
                            }
                            else
                            {
                                m_MCCodeViewPopup_MS.DisplaySubItemIndex = value;
                            }
                        }

                        if (m_MultiSelect == false)
                        {
                            m_MCCodeViewPopup.SelectedSubItemIndex = value;
                        }
                        else
                        {
                            m_MCCodeViewPopup_MS.SelectedSubItemIndex = value;
                        }
                    }
                }

                // 2006.07.19. Aiden Koo
                public int DisplaySubItemIndex
                {
                    get
                    {
                        return m_DisplaySubItemIndex;
                    }
                    set
                    {
                        m_DisplaySubItemIndex = value;
                        if (m_MultiSelect == false)
                        {
                            m_MCCodeViewPopup.DisplaySubItemIndex = value;
                        }
                        else
                        {
                            m_MCCodeViewPopup_MS.DisplaySubItemIndex = value;
                        }
                    }
                }

                // 2006.07.19. Aiden Koo
                public int SelectedDescIndex
                {
                    get
                    {
                        return m_SelectedDescIndex;
                    }
                    set
                    {
                        m_SelectedDescIndex = value;
                    }
                }

                public int SearchSubItemIndex
                {
                    get
                    {
                        return m_SearchSubItemIndex;
                    }
                    set
                    {
                        m_SearchSubItemIndex = value;
                    }
                }

                public override string Text
                {
                    get
                    {
                        if (m_DisplaySubItemIndex == m_SelectedSubItemIndex)
                        {
                            return txtDisplay.Text;
                        }
                        else
                        {
                            return txtCode.Text;
                        }
                    }
                    set
                    {
                        if (m_DisplaySubItemIndex == m_SelectedSubItemIndex)
                        {
                            txtDisplay.Text = value;
                        }
                        else if (m_SelectedSubItemIndex < 0 ||
                                m_DisplaySubItemIndex < 0 ||
                                m_SelectedSubItemIndex > GetListView.Columns.Count - 1 ||
                                m_DisplaySubItemIndex > GetListView.Columns.Count - 1)
                        {
                            // 2007.01.16. Aiden Koo.
                            // Index가 범위를 벗어난 경우
                            txtDisplay.Text = value;
                            txtCode.Text = value;
                        }
                        else
                        {
                            txtCode.Text = value;

                            if (value != null)
                            {
                                if (value.Trim() == "")
                                {
                                    txtDisplay.Text = "";
                                }
                                else
                                {
                                    if (GetListView.Items.Count > 0)
                                    {
                                        int i;

                                        for (i = 0; i <= GetListView.Items.Count - 1; i++)
                                        {
                                            if (GetListView.Items[i].SubItems[m_SelectedSubItemIndex].Text != null)
                                            {
                                                if (GetListView.Items[i].SubItems[m_SelectedSubItemIndex].Text.Trim() == value.Trim())
                                                {
                                                    txtDisplay.Text = GetListView.Items[i].SubItems[m_DisplaySubItemIndex].Text;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                txtDisplay.Text = "";
                            }
                        }

                        if (m_bVisibleDesc == true)
                        {
                            if (m_SelectedSubItemIndex < 0 ||
                                m_DisplaySubItemIndex < 0 ||
                                m_SelectedSubItemIndex > GetListView.Columns.Count - 1 ||
                                m_DisplaySubItemIndex > GetListView.Columns.Count - 1)
                            {
                                // 2007.01.16. Aiden Koo.
                                // Index가 범위를 벗어난 경우
                                txtDesc.Text = value;
                            }
                            else if (m_SelectedSubItemIndex != m_SelectedDescIndex)
                            {

                                if (value != null)
                                {
                                    if (value.Trim() == "")
                                    {
                                        txtDesc.Text = "";
                                    }
                                    else
                                    {
                                        if (GetListView.Items.Count > 0)
                                        {
                                            int i;

                                            for (i = 0; i <= GetListView.Items.Count - 1; i++)
                                            {
                                                if (GetListView.Items[i].SubItems[m_SelectedSubItemIndex].Text != null)
                                                {
                                                    if (GetListView.Items[i].SubItems[m_SelectedSubItemIndex].Text.Trim() == value.Trim())
                                                    {
                                                        txtDesc.Text = GetListView.Items[i].SubItems[m_SelectedDescIndex].Text;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    txtDesc.Text = "";
                                }

                            }
                        }
                    }
                }

                public string DisplayText
                {
                    get
                    {
                        return txtDisplay.Text;
                    }
                    set
                    {
                        txtDisplay.Text = value;
                    }
                }

                public string DescText
                {
                    get
                    {
                        return txtDesc.Text;
                    }
                    set
                    {
                        txtDesc.Text = value;
                    }
                }

                public string SelectedValueToQueryText
                {
                    get
                    {
                        if (txtCode.Tag == null)
                        {
                            return "";
                        }
                        else
                        {
                            return txtCode.Tag.ToString();
                        }
                    }
                    set
                    {
                        txtCode.Tag = value;
                    }
                }

                public string SelectedDescToQueryText
                {
                    get
                    {
                        if (txtCode1.Tag == null)
                        {
                            return "";
                        }
                        else
                        {
                            return txtCode1.Tag.ToString();
                        }
                    }
                    set
                    {
                        txtCode1.Tag = value;
                    }
                }


                //add by CM Koo. 2005.08.25
                public TextBox GetTextBox
                {
                    get
                    {
                        if (m_DisplaySubItemIndex == m_SelectedSubItemIndex)
                        {
                            return txtDisplay;
                        }
                        else
                        {
                            return txtCode;
                        }
                    }
                }

                public TextBox GetDisplayTextBox
                {
                    get
                    {
                        return txtDisplay;
                    }
                }

                public int TextBoxWidth
                {
                    get
                    {
                        return pnlText.Width;
                    }
                    set
                    {
                        pnlText.Width = value;
                    }
                }

                public bool VisibleButton
                {
                    get
                    {
                        return m_bVisibleButton;
                    }
                    set
                    {
                        m_bVisibleButton = value;
                        btnButton.Visible = m_bVisibleButton;
                    }
                }

                public bool VisibleDescription
                {
                    get
                    {
                        return m_bVisibleDesc;
                    }
                    set
                    {
                        m_bVisibleDesc = value;
                        pnlDesc.Visible = m_bVisibleDesc;
                    }
                }

                public bool VisibleColumnHeader
                {
                    get
                    {
                        return m_bVisibleColumnHeader;
                    }
                    set
                    {
                        m_bVisibleColumnHeader = value;
                        if (value == true)
                        {
                            if (m_MultiSelect == false)
                            {
                                this.m_MCCodeViewPopup.m_MCCodeDropList.HeaderStyle = ColumnHeaderStyle.Clickable;
                            }
                            else
                            {
                                this.m_MCCodeViewPopup_MS.m_MCCodeDropList.HeaderStyle = ColumnHeaderStyle.Clickable;
                            }
                        }
                        else
                        {
                            if (m_MultiSelect == false)
                            {
                                this.m_MCCodeViewPopup.m_MCCodeDropList.HeaderStyle = ColumnHeaderStyle.None;
                            }
                            else
                            {
                                this.m_MCCodeViewPopup_MS.m_MCCodeDropList.HeaderStyle = ColumnHeaderStyle.None;
                            }
                        }

                        if (m_MultiSelect == false)
                        {
                            this.m_MCCodeViewPopup.VisibleColumnHeader = value;
                        }
                        else
                        {
                            this.m_MCCodeViewPopup_MS.VisibleColumnHeader = value;
                        }
                    }
                }

                public int Index
                {
                    get
                    {
                        return m_iIndex;
                    }
                    set
                    {
                        m_iIndex = value;
                    }
                }

                public bool ReadOnly
                {
                    get
                    {
                        return m_bReadOnly;
                    }
                    set
                    {
                        m_bReadOnly = value;
                        txtDisplay.ReadOnly = m_bReadOnly;
                        txtDisplay.BackColor = BackColor;
                    }
                }

                public new Color BackColor
                {
                    get
                    {
                        return m_crBackColor;
                    }
                    set
                    {
                        m_crBackColor = value;
                        txtDisplay.BackColor = m_crBackColor;
                    }
                }

                public int MaxLength
                {
                    get
                    {
                        return m_iMaxLength;
                    }
                    set
                    {
                        if (value < 0 || value > 32767)
                        {
                            value = 32767;
                        }
                        m_iMaxLength = value;
                        txtDisplay.MaxLength = m_iMaxLength;
                    }
                }

                public int SelectionStart
                {
                    get
                    {
                        return txtDisplay.SelectionStart;
                    }
                    set
                    {
                        if (value < 0 || value > 32767)
                        {
                            value = 32767;
                        }
                        txtDisplay.SelectionStart = value;
                    }
                }

                public bool MultiSelect
                {
                    get
                    {
                        return m_MultiSelect;
                    }
                    set
                    {
                        m_MultiSelect = value;
                    }
                }


                [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
                public Size DropDownSize
                {
                    get
                    {
                        return m_DropDownSize;
                    }
                    set
                    {
                        m_DropDownSize = value;
                    }
                }

                [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
                public ListView.ListViewItemCollection Items
                {
                    get
                    {
                        if (m_MultiSelect == false)
                        {
                            return m_MCCodeViewPopup.Items;
                        }
                        else
                        {
                            return m_MCCodeViewPopup_MS.Items;
                        }
                    }
                }

                [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
                public ListView.ColumnHeaderCollection Columns
                {
                    get
                    {
                        if (m_MultiSelect == false)
                        {
                            return m_MCCodeViewPopup.Columns;
                        }
                        else
                        {
                            return m_MCCodeViewPopup_MS.Columns;
                        }
                    }
                }

                [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
                public ListViewItem SelectedItem
                {
                    get
                    {
                        return m_SelectedItem;
                    }
                }

                [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
                public bool IsPopup
                {
                    get
                    {
                        return m_bIsPopup;
                    }
                    set
                    {
                        m_bIsPopup = value;
                    }
                }

                [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
                public int ButtonWidth
                {
                    get
                    {
                        return m_iButtonWidth;
                    }
                    set
                    {
                        if (value < 1)
                        {
                            m_iButtonWidth = 1;
                        }
                        else
                        {
                            m_iButtonWidth = value;
                        }
                    }
                }

                #endregion

                public void Init()
                {
                    if (m_MultiSelect == false)
                    {
                        m_MCCodeViewPopup.m_MCCodeDropList.Clear();
                    }
                    else
                    {
                        m_MCCodeViewPopup_MS.m_MCCodeDropList.Clear();
                        txtDisplay.Text = "ALL";
                        txtCode.Tag = "LIKE '%'";
                        txtCode1.Tag = "LIKE '%'";
                    }
                    this.SmallImageList = this.imlSmallIcon;
                }

                public bool AddEmptyRow(int iRowCount)
                {

                    int i = 0;
                    int j = 0;
                    ListViewItem itmx = null;

                    try
                    {
                        for (i = 0; i <= iRowCount - 1; i++)
                        {
                            itmx = new ListViewItem("");
                            for (j = 0; j <= Columns.Count - 1; j++)
                            {
                                itmx.SubItems.Add("");
                            }
                            Items.Add(itmx);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "AddEmptyRow()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    return true;

                }

                public bool InsertEmptyRow(int InsertIndex, int iRowCount)
                {

                    int i = 0;
                    int j = 0;
                    ListViewItem itmx = null;

                    try
                    {
                        for (i = 0; i <= iRowCount - 1; i++)
                        {
                            itmx = new ListViewItem("");
                            for (j = 0; j <= Columns.Count - 1; j++)
                            {
                                itmx.SubItems.Add("");
                            }
                            Items.Insert(InsertIndex, itmx);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "AddEmptyRow()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    return true;

                }

                private void OnPopUp_SelectionChanged(object sender, MCCodeViewSelChanged_EventArgs e)
                {
                    m_MCCodeViewPopup.DroppedDownFlag = false;
                    
                    txtDisplay.Text = e.DisplayText;
                    txtCode.Tag = e.SelectedValueToQueryString;
                    txtCode1.Tag = e.SelectedDescToQueryString;
                    txtCode.Text = e.Text;
                    txtCode1.Text = e.Text;
                    m_SelectedItem = e.SelectedItem;

                    if (m_SelectedDescIndex > -1)
                    {
                        txtDesc.Text = e.SelectedItem.SubItems[m_SelectedDescIndex].Text;
                    }
                    else
                    {
                        txtDesc.Text = "";
                    }

                    MCCodeViewSelChanged_EventArgs oArgs = new MCCodeViewSelChanged_EventArgs(m_SelectedItem, e.SelectedSubItemIndex, e.DisplaySubItemIndex);
                    if (SelectedItemChangedEvent != null)
                        SelectedItemChangedEvent(this, oArgs);

                }

                private void OnPopUp_ButtonPressed(object sender, MCCodeViewButtonPressed_EventArgs e)
                {
                    int i;

                    m_MCCodeViewPopup_MS.DroppedDownFlag = false;
                    txtDisplay.Text = e.DisplayText;
                    txtCode.Tag = e.SelectedValueToQueryString;
                    txtCode1.Tag = e.SelectedDescToQueryString;
                    txtCode.Text = e.Text;
                    txtCode1.Text = e.Text;
                    m_SelectedItem_ms = e.SelectedItem;

                    if (m_SelectedDescIndex > -1)
                    {
                        for (i = 0; i < m_SelectedItem_ms.Length; i++)
                        {
                            if (i == 0)
                            {
                                txtDesc.Text = e.SelectedItem[i].SubItems[m_SelectedDescIndex].Text;
                            }
                            else
                            {
                                txtDesc.Text = txtDesc.Text + ", " + e.SelectedItem[i].SubItems[m_SelectedDescIndex].Text;
                            }
                        }
                    }
                    else
                    {
                        txtDesc.Text = "";
                    }

                    MCCodeViewButtonPressed_EventArgs oArgs = new MCCodeViewButtonPressed_EventArgs(m_SelectedItem_ms, e.SelectedSubItemIndex, e.DisplaySubItemIndex, e.SelectAll);
                    if (ButtonPressedEvent != null)
                        ButtonPressedEvent(this, oArgs);
                }

                private void Button_Click(System.Object sender, System.EventArgs e)
                {

                    if (ButtonPressEvent != null)
                        ButtonPressEvent(this, e);

                    if (IsPopup == false)
                    {
                        IsPopup = true;
                        return;
                    }

                    OnButtonPress();

                }

                public void OnButtonPress()
                {

                    if (m_MCCodeViewPopup_MS.DroppedDownFlag == true)
                    {
                        return;
                    }

                    ShowPopUp();

                }

                public void ListClear()
                {

                    Columns.Clear();
                    Items.Clear();

                }

                public void ShowPopUp()
                {
                    if (m_MultiSelect == false)
                    {
                        m_MCCodeViewPopup.Position = this.PointToScreen(new Point(btnButton.Left + btnButton.Width / 2, btnButton.Top + btnButton.Height / 2));
                        if (m_MCCodeViewPopup.ShowPopup(true) == false)
                        {
                            tipToolTip.SetToolTip(this.btnButton, "No Data");
                            tipToolTip.AutomaticDelay = 0;
                            tipToolTip.AutoPopDelay = 5000;
                            tipToolTip.Active = true;
                            if (!(Focusing == null))
                            {
                                //Focusing.Focus();
                            }
                            return;
                        }

                        tipToolTip.Active = false;
                        m_MCCodeViewPopup.DroppedDownFlag = true;

                        m_MCCodeViewPopup.m_MCCodeDropList.Focus();
                    }
                    else
                    {
                        m_MCCodeViewPopup_MS.Position = this.PointToScreen(new Point(btnButton.Left + btnButton.Width / 2, btnButton.Top + btnButton.Height / 2));                        
                        if (m_MCCodeViewPopup_MS.ShowPopup(true) == false)
                        {
                            tipToolTip.SetToolTip(this.btnButton, "No Data");
                            tipToolTip.AutomaticDelay = 0;
                            tipToolTip.AutoPopDelay = 5000;
                            tipToolTip.Active = true;
                            if (!(Focusing == null))
                            {
                                //Focusing.Focus();
                            }
                            return;
                        }

                        tipToolTip.Active = false;
                        m_MCCodeViewPopup_MS.DroppedDownFlag = true;

                        m_MCCodeViewPopup_MS.m_MCCodeDropList.Focus();

                    }
                }

                private void txtDisplay_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
                {

                    if (e.KeyCode == Keys.Down)
                    {
                        if (ButtonPressEvent != null)
                            ButtonPressEvent(this, e);

                        if (m_MultiSelect == false)
                        {
                            if (m_MCCodeViewPopup.DroppedDownFlag == true)
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (m_MCCodeViewPopup_MS.DroppedDownFlag == true)
                            {
                                return;
                            }
                        }
                        ShowPopUp();
                    }

                }

                private void txtDisplay_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
                {

                    if (TextBoxKeyPressEvent != null)
                        TextBoxKeyPressEvent(this, e);

                    m_IsKeyPress = true;
                    m_KeyPressText = txtDisplay.Text;

                    // Display TextBox에 값을 입력하고 엔터를 눌렀을 때에 리스트가 존재하고
                    // Code값을 화면에 표시하고
                    // Description을 표시하며 Code값의 인덱스와 Desc의 인덱스가 다르다면
                    // Code값에 대한 Desc를 화면에 표시한다.
                    if (txtDisplay.Text != null && txtDisplay.Text.Trim() != "" &&
                        e.KeyChar == 13 && GetListView.Items.Count > 0 &&
                        m_SelectedSubItemIndex == m_DisplaySubItemIndex && m_bVisibleDesc == true && m_SelectedSubItemIndex != m_SelectedDescIndex)
                    {
                        this.Text = Text;
                    }
                }

                private void txtDisplay_TextChanged(object sender, System.EventArgs e)
                {

                    if (m_DisplaySubItemIndex == m_SelectedSubItemIndex)
                    {
                        txtCode.Text = txtDisplay.Text;
                        txtCode1.Text = txtDisplay.Text;
                    }

                    if (m_bVisibleDesc == true)
                    {
                        if (txtDisplay.Text != null)
                        {
                            if (txtDisplay.Text.Trim() == "")
                            {
                                txtDesc.Text = "";
                            }
                        }
                    }

                    if (m_IsKeyPress == true && m_KeyPressText != txtDisplay.Text)
                    {
                        txtCode.Text = txtDisplay.Text;
                        txtCode1.Text = txtDisplay.Text;

                        m_IsKeyPress = false;
                        m_KeyPressText = "";
                    }

                    if (TextBoxTextChangedEvent != null)
                        TextBoxTextChangedEvent(this, e);

                }

                private void txtDisplay_LostFocus(object sender, System.EventArgs e)
                {

                    if (TextBoxLostFocusEvent != null)
                        TextBoxLostFocusEvent(this, e);

                }

                private void txtDisplay_GotFocus(object sender, System.EventArgs e)
                {

                    if (TextBoxGotFocusEvent != null)
                        TextBoxGotFocusEvent(this, e);

                }

                private void MCCodeView_Resize(object sender, System.EventArgs e)
                {
                    if (VisibleDescription == false)
                    {
                        pnlText.Width = this.Width;
                    }
                }

                private void MCCodeView_FontChanged(object sender, System.EventArgs e)
                {
                    if (m_MultiSelect == false)
                    {
                        this.m_MCCodeViewPopup.Font = this.Font;
                    }
                    else
                    {
                        this.m_MCCodeViewPopup_MS.Font = this.Font;
                    }
                    this.pnlText.Font = this.Font;
                    this.txtDisplay.Font = this.Font;
                    this.txtDesc.Font = this.Font;

                }

            }

        }
    }

}
