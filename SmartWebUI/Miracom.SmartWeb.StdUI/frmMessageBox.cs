using System.Data;
using System.Collections;
using System.Diagnostics;
using System;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
	public class frmMessageBox : System.Windows.Forms.Form
    {

        #region " Windows Form 디자이너에서 생성한 코드 "

        public frmMessageBox()
		{

            //이 호출은 Windows Form 디자이너에 필요합니다.
			InitializeComponent();
			
			//Init()
            //InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
			
		}

        //Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!(components == null))
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

        //Windows Form 디자이너에 필요합니다.
		private System.ComponentModel.Container components = null;
		
        //참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
        //Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
        //코드 편집기를 사용하여 수정하지 마십시오.
		internal System.Windows.Forms.Panel pnlDetailMsg;
		internal System.Windows.Forms.Panel pnlMsgTop;
		internal System.Windows.Forms.Panel pnlRetryCancel;
		internal System.Windows.Forms.Button btnRetryCancelCancel;
		internal System.Windows.Forms.Button btnRetryCancelRetry;
		internal System.Windows.Forms.Panel pnlAbortRetryIgnore;
		internal System.Windows.Forms.Button btnAbortRetryIgnoreIgnore;
		internal System.Windows.Forms.Button btnAbortRetryIgnoreRetry;
		internal System.Windows.Forms.Button btnAbortRetryIgnoreAbort;
		internal System.Windows.Forms.Panel pnlOK;
		internal System.Windows.Forms.Button btnOKOK;
		internal System.Windows.Forms.Panel pnlOKCancel;
		internal System.Windows.Forms.Button btnOKCancelCancel;
		internal System.Windows.Forms.Button btnOKCancelOK;
		internal System.Windows.Forms.Panel pnlYesNoCancel;
		internal System.Windows.Forms.Button btnYesNoCancelCancel;
		internal System.Windows.Forms.Button btnYesNoCancelNo;
		internal System.Windows.Forms.Button btnYesNoCancelYes;
		internal System.Windows.Forms.Panel pnlYesNo;
		internal System.Windows.Forms.Button btnYesNoNo;
		internal System.Windows.Forms.Button btnYesNoYes;
		internal System.Windows.Forms.Panel pnlMessage;
		internal System.Windows.Forms.Button btnDetail;
		internal System.Windows.Forms.Label lblMessage;
		internal System.Windows.Forms.TextBox txtDetailMsg;
		internal System.Windows.Forms.Button btnCopy;
		internal System.Windows.Forms.GroupBox grpMessage;
		internal System.Windows.Forms.Button btnSimple;
        private Panel panel2;
        private Label label3;
        private Panel pnlGroup;
		internal System.Windows.Forms.Label Label1;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMessageBox));
            this.pnlDetailMsg = new System.Windows.Forms.Panel();
            this.txtDetailMsg = new System.Windows.Forms.TextBox();
            this.pnlMsgTop = new System.Windows.Forms.Panel();
            this.pnlOK = new System.Windows.Forms.Panel();
            this.btnOKOK = new System.Windows.Forms.Button();
            this.pnlOKCancel = new System.Windows.Forms.Panel();
            this.btnOKCancelOK = new System.Windows.Forms.Button();
            this.btnOKCancelCancel = new System.Windows.Forms.Button();
            this.pnlRetryCancel = new System.Windows.Forms.Panel();
            this.btnRetryCancelCancel = new System.Windows.Forms.Button();
            this.btnRetryCancelRetry = new System.Windows.Forms.Button();
            this.pnlAbortRetryIgnore = new System.Windows.Forms.Panel();
            this.btnAbortRetryIgnoreIgnore = new System.Windows.Forms.Button();
            this.btnAbortRetryIgnoreRetry = new System.Windows.Forms.Button();
            this.btnAbortRetryIgnoreAbort = new System.Windows.Forms.Button();
            this.pnlYesNoCancel = new System.Windows.Forms.Panel();
            this.btnYesNoCancelCancel = new System.Windows.Forms.Button();
            this.btnYesNoCancelNo = new System.Windows.Forms.Button();
            this.btnYesNoCancelYes = new System.Windows.Forms.Button();
            this.pnlYesNo = new System.Windows.Forms.Panel();
            this.btnYesNoNo = new System.Windows.Forms.Button();
            this.btnYesNoYes = new System.Windows.Forms.Button();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnSimple = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.grpMessage = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlGroup = new System.Windows.Forms.Panel();
            this.pnlDetailMsg.SuspendLayout();
            this.pnlMsgTop.SuspendLayout();
            this.pnlOK.SuspendLayout();
            this.pnlOKCancel.SuspendLayout();
            this.pnlRetryCancel.SuspendLayout();
            this.pnlAbortRetryIgnore.SuspendLayout();
            this.pnlYesNoCancel.SuspendLayout();
            this.pnlYesNo.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.grpMessage.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDetailMsg
            // 
            this.pnlDetailMsg.Controls.Add(this.txtDetailMsg);
            this.pnlDetailMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetailMsg.Location = new System.Drawing.Point(0, 0);
            this.pnlDetailMsg.Name = "pnlDetailMsg";
            this.pnlDetailMsg.Padding = new System.Windows.Forms.Padding(3);
            this.pnlDetailMsg.Size = new System.Drawing.Size(520, 202);
            this.pnlDetailMsg.TabIndex = 1;
            this.pnlDetailMsg.Visible = false;
            // 
            // txtDetailMsg
            // 
            this.txtDetailMsg.BackColor = System.Drawing.Color.White;
            this.txtDetailMsg.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtDetailMsg.Location = new System.Drawing.Point(3, 3);
            this.txtDetailMsg.Multiline = true;
            this.txtDetailMsg.Name = "txtDetailMsg";
            this.txtDetailMsg.ReadOnly = true;
            this.txtDetailMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetailMsg.Size = new System.Drawing.Size(421, 196);
            this.txtDetailMsg.TabIndex = 0;
            // 
            // pnlMsgTop
            // 
            this.pnlMsgTop.Controls.Add(this.pnlOK);
            this.pnlMsgTop.Controls.Add(this.pnlOKCancel);
            this.pnlMsgTop.Controls.Add(this.pnlRetryCancel);
            this.pnlMsgTop.Controls.Add(this.pnlAbortRetryIgnore);
            this.pnlMsgTop.Controls.Add(this.pnlYesNoCancel);
            this.pnlMsgTop.Controls.Add(this.pnlYesNo);
            this.pnlMsgTop.Controls.Add(this.pnlMessage);
            this.pnlMsgTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMsgTop.Location = new System.Drawing.Point(0, 25);
            this.pnlMsgTop.Name = "pnlMsgTop";
            this.pnlMsgTop.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnlMsgTop.Size = new System.Drawing.Size(520, 98);
            this.pnlMsgTop.TabIndex = 0;
            // 
            // pnlOK
            // 
            this.pnlOK.Controls.Add(this.btnOKOK);
            this.pnlOK.Location = new System.Drawing.Point(174, 69);
            this.pnlOK.Name = "pnlOK";
            this.pnlOK.Size = new System.Drawing.Size(98, 96);
            this.pnlOK.TabIndex = 6;
            // 
            // btnOKOK
            // 
            this.btnOKOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOKOK.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnOKOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOKOK.Location = new System.Drawing.Point(5, 5);
            this.btnOKOK.Name = "btnOKOK";
            this.btnOKOK.Size = new System.Drawing.Size(88, 26);
            this.btnOKOK.TabIndex = 0;
            this.btnOKOK.Text = "OK";
            this.btnOKOK.Click += new System.EventHandler(this.btnOKOK_Click);
            // 
            // pnlOKCancel
            // 
            this.pnlOKCancel.Controls.Add(this.btnOKCancelOK);
            this.pnlOKCancel.Controls.Add(this.btnOKCancelCancel);
            this.pnlOKCancel.Location = new System.Drawing.Point(0, 0);
            this.pnlOKCancel.Name = "pnlOKCancel";
            this.pnlOKCancel.Size = new System.Drawing.Size(98, 96);
            this.pnlOKCancel.TabIndex = 5;
            // 
            // btnOKCancelOK
            // 
            this.btnOKCancelOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOKCancelOK.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnOKCancelOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOKCancelOK.Location = new System.Drawing.Point(5, 5);
            this.btnOKCancelOK.Name = "btnOKCancelOK";
            this.btnOKCancelOK.Size = new System.Drawing.Size(88, 26);
            this.btnOKCancelOK.TabIndex = 0;
            this.btnOKCancelOK.Text = "OK";
            this.btnOKCancelOK.Click += new System.EventHandler(this.btnOKCancelOK_Click);
            // 
            // btnOKCancelCancel
            // 
            this.btnOKCancelCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOKCancelCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnOKCancelCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOKCancelCancel.Location = new System.Drawing.Point(5, 35);
            this.btnOKCancelCancel.Name = "btnOKCancelCancel";
            this.btnOKCancelCancel.Size = new System.Drawing.Size(88, 26);
            this.btnOKCancelCancel.TabIndex = 1;
            this.btnOKCancelCancel.Text = "Cancel";
            this.btnOKCancelCancel.Click += new System.EventHandler(this.btnOKCancelCancel_Click);
            // 
            // pnlRetryCancel
            // 
            this.pnlRetryCancel.Controls.Add(this.btnRetryCancelCancel);
            this.pnlRetryCancel.Controls.Add(this.btnRetryCancelRetry);
            this.pnlRetryCancel.Location = new System.Drawing.Point(620, 1);
            this.pnlRetryCancel.Name = "pnlRetryCancel";
            this.pnlRetryCancel.Size = new System.Drawing.Size(98, 96);
            this.pnlRetryCancel.TabIndex = 3;
            // 
            // btnRetryCancelCancel
            // 
            this.btnRetryCancelCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRetryCancelCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRetryCancelCancel.Location = new System.Drawing.Point(5, 35);
            this.btnRetryCancelCancel.Name = "btnRetryCancelCancel";
            this.btnRetryCancelCancel.Size = new System.Drawing.Size(88, 26);
            this.btnRetryCancelCancel.TabIndex = 1;
            this.btnRetryCancelCancel.Text = "Cancel";
            this.btnRetryCancelCancel.Click += new System.EventHandler(this.btnRetryCancelCancel_Click);
            // 
            // btnRetryCancelRetry
            // 
            this.btnRetryCancelRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnRetryCancelRetry.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRetryCancelRetry.Location = new System.Drawing.Point(5, 5);
            this.btnRetryCancelRetry.Name = "btnRetryCancelRetry";
            this.btnRetryCancelRetry.Size = new System.Drawing.Size(88, 26);
            this.btnRetryCancelRetry.TabIndex = 0;
            this.btnRetryCancelRetry.Text = "Retry";
            this.btnRetryCancelRetry.Click += new System.EventHandler(this.btnRetryCancelRetry_Click);
            // 
            // pnlAbortRetryIgnore
            // 
            this.pnlAbortRetryIgnore.Controls.Add(this.btnAbortRetryIgnoreIgnore);
            this.pnlAbortRetryIgnore.Controls.Add(this.btnAbortRetryIgnoreRetry);
            this.pnlAbortRetryIgnore.Controls.Add(this.btnAbortRetryIgnoreAbort);
            this.pnlAbortRetryIgnore.Location = new System.Drawing.Point(718, 1);
            this.pnlAbortRetryIgnore.Name = "pnlAbortRetryIgnore";
            this.pnlAbortRetryIgnore.Size = new System.Drawing.Size(98, 96);
            this.pnlAbortRetryIgnore.TabIndex = 4;
            // 
            // btnAbortRetryIgnoreIgnore
            // 
            this.btnAbortRetryIgnoreIgnore.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.btnAbortRetryIgnoreIgnore.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAbortRetryIgnoreIgnore.Location = new System.Drawing.Point(5, 65);
            this.btnAbortRetryIgnoreIgnore.Name = "btnAbortRetryIgnoreIgnore";
            this.btnAbortRetryIgnoreIgnore.Size = new System.Drawing.Size(88, 26);
            this.btnAbortRetryIgnoreIgnore.TabIndex = 2;
            this.btnAbortRetryIgnoreIgnore.Text = "Ignore";
            this.btnAbortRetryIgnoreIgnore.Click += new System.EventHandler(this.btnAbortRetryIgnoreIgnore_Click);
            // 
            // btnAbortRetryIgnoreRetry
            // 
            this.btnAbortRetryIgnoreRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnAbortRetryIgnoreRetry.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAbortRetryIgnoreRetry.Location = new System.Drawing.Point(5, 35);
            this.btnAbortRetryIgnoreRetry.Name = "btnAbortRetryIgnoreRetry";
            this.btnAbortRetryIgnoreRetry.Size = new System.Drawing.Size(88, 26);
            this.btnAbortRetryIgnoreRetry.TabIndex = 1;
            this.btnAbortRetryIgnoreRetry.Text = "Retry";
            this.btnAbortRetryIgnoreRetry.Click += new System.EventHandler(this.btnAbortRetryIgnoreRetry_Click);
            // 
            // btnAbortRetryIgnoreAbort
            // 
            this.btnAbortRetryIgnoreAbort.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnAbortRetryIgnoreAbort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAbortRetryIgnoreAbort.Location = new System.Drawing.Point(5, 5);
            this.btnAbortRetryIgnoreAbort.Name = "btnAbortRetryIgnoreAbort";
            this.btnAbortRetryIgnoreAbort.Size = new System.Drawing.Size(88, 26);
            this.btnAbortRetryIgnoreAbort.TabIndex = 0;
            this.btnAbortRetryIgnoreAbort.Text = "Abort";
            this.btnAbortRetryIgnoreAbort.Click += new System.EventHandler(this.btnAbortRetryIgnoreAbort_Click);
            // 
            // pnlYesNoCancel
            // 
            this.pnlYesNoCancel.Controls.Add(this.btnYesNoCancelCancel);
            this.pnlYesNoCancel.Controls.Add(this.btnYesNoCancelNo);
            this.pnlYesNoCancel.Controls.Add(this.btnYesNoCancelYes);
            this.pnlYesNoCancel.Location = new System.Drawing.Point(522, 1);
            this.pnlYesNoCancel.Name = "pnlYesNoCancel";
            this.pnlYesNoCancel.Size = new System.Drawing.Size(98, 96);
            this.pnlYesNoCancel.TabIndex = 2;
            // 
            // btnYesNoCancelCancel
            // 
            this.btnYesNoCancelCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnYesNoCancelCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnYesNoCancelCancel.Location = new System.Drawing.Point(5, 65);
            this.btnYesNoCancelCancel.Name = "btnYesNoCancelCancel";
            this.btnYesNoCancelCancel.Size = new System.Drawing.Size(88, 26);
            this.btnYesNoCancelCancel.TabIndex = 0;
            this.btnYesNoCancelCancel.Text = "Cancel";
            this.btnYesNoCancelCancel.Click += new System.EventHandler(this.btnYesNoCancelCancel_Click);
            // 
            // btnYesNoCancelNo
            // 
            this.btnYesNoCancelNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnYesNoCancelNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnYesNoCancelNo.Location = new System.Drawing.Point(5, 35);
            this.btnYesNoCancelNo.Name = "btnYesNoCancelNo";
            this.btnYesNoCancelNo.Size = new System.Drawing.Size(88, 26);
            this.btnYesNoCancelNo.TabIndex = 2;
            this.btnYesNoCancelNo.Text = "No";
            this.btnYesNoCancelNo.Click += new System.EventHandler(this.btnYesNoCancelNo_Click);
            // 
            // btnYesNoCancelYes
            // 
            this.btnYesNoCancelYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYesNoCancelYes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnYesNoCancelYes.Location = new System.Drawing.Point(5, 5);
            this.btnYesNoCancelYes.Name = "btnYesNoCancelYes";
            this.btnYesNoCancelYes.Size = new System.Drawing.Size(88, 26);
            this.btnYesNoCancelYes.TabIndex = 1;
            this.btnYesNoCancelYes.Text = "Yes";
            this.btnYesNoCancelYes.Click += new System.EventHandler(this.btnYesNoCancelYes_Click);
            // 
            // pnlYesNo
            // 
            this.pnlYesNo.Controls.Add(this.btnYesNoNo);
            this.pnlYesNo.Controls.Add(this.btnYesNoYes);
            this.pnlYesNo.Location = new System.Drawing.Point(424, 1);
            this.pnlYesNo.Name = "pnlYesNo";
            this.pnlYesNo.Size = new System.Drawing.Size(98, 96);
            this.pnlYesNo.TabIndex = 1;
            // 
            // btnYesNoNo
            // 
            this.btnYesNoNo.BackColor = System.Drawing.Color.White;
            this.btnYesNoNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnYesNoNo.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnYesNoNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYesNoNo.Location = new System.Drawing.Point(5, 35);
            this.btnYesNoNo.Name = "btnYesNoNo";
            this.btnYesNoNo.Size = new System.Drawing.Size(88, 26);
            this.btnYesNoNo.TabIndex = 1;
            this.btnYesNoNo.Text = "No";
            this.btnYesNoNo.UseVisualStyleBackColor = false;
            this.btnYesNoNo.Click += new System.EventHandler(this.btnYesNoNo_Click);
            // 
            // btnYesNoYes
            // 
            this.btnYesNoYes.BackColor = System.Drawing.Color.White;
            this.btnYesNoYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYesNoYes.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnYesNoYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYesNoYes.Location = new System.Drawing.Point(5, 5);
            this.btnYesNoYes.Name = "btnYesNoYes";
            this.btnYesNoYes.Size = new System.Drawing.Size(88, 26);
            this.btnYesNoYes.TabIndex = 0;
            this.btnYesNoYes.Text = "Yes";
            this.btnYesNoYes.UseVisualStyleBackColor = false;
            this.btnYesNoYes.Click += new System.EventHandler(this.btnYesNoYes_Click);
            // 
            // pnlMessage
            // 
            this.pnlMessage.Controls.Add(this.btnDetail);
            this.pnlMessage.Controls.Add(this.btnSimple);
            this.pnlMessage.Controls.Add(this.btnCopy);
            this.pnlMessage.Controls.Add(this.grpMessage);
            this.pnlMessage.Controls.Add(this.Label1);
            this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMessage.Location = new System.Drawing.Point(0, 3);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMessage.Size = new System.Drawing.Size(424, 92);
            this.pnlMessage.TabIndex = 0;
            // 
            // btnDetail
            // 
            this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnDetail.Image")));
            this.btnDetail.Location = new System.Drawing.Point(380, 62);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(16, 22);
            this.btnDetail.TabIndex = 0;
            this.btnDetail.TabStop = false;
            this.btnDetail.Tag = "VIEW";
            this.btnDetail.Visible = false;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnSimple
            // 
            this.btnSimple.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSimple.Enabled = false;
            this.btnSimple.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSimple.Image = ((System.Drawing.Image)(resources.GetObject("btnSimple.Image")));
            this.btnSimple.Location = new System.Drawing.Point(381, 62);
            this.btnSimple.Name = "btnSimple";
            this.btnSimple.Size = new System.Drawing.Size(16, 22);
            this.btnSimple.TabIndex = 0;
            this.btnSimple.TabStop = false;
            this.btnSimple.Tag = "VIEW";
            this.btnSimple.Visible = false;
            this.btnSimple.Click += new System.EventHandler(this.btnSimple_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.BackColor = System.Drawing.SystemColors.Control;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.Location = new System.Drawing.Point(398, 62);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(21, 22);
            this.btnCopy.TabIndex = 0;
            this.btnCopy.TabStop = false;
            this.btnCopy.Tag = "";
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Visible = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // grpMessage
            // 
            this.grpMessage.Controls.Add(this.lblMessage);
            this.grpMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpMessage.Location = new System.Drawing.Point(3, -6);
            this.grpMessage.Name = "grpMessage";
            this.grpMessage.Size = new System.Drawing.Size(421, 96);
            this.grpMessage.TabIndex = 0;
            this.grpMessage.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Location = new System.Drawing.Point(3, 17);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(415, 76);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label1
            // 
            this.Label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label1.Location = new System.Drawing.Point(3, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(418, 86);
            this.Label1.TabIndex = 0;
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(520, 25);
            this.panel2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Message Inforamtion";
            // 
            // pnlGroup
            // 
            this.pnlGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGroup.Controls.Add(this.pnlMsgTop);
            this.pnlGroup.Controls.Add(this.panel2);
            this.pnlGroup.Controls.Add(this.pnlDetailMsg);
            this.pnlGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGroup.Location = new System.Drawing.Point(0, 0);
            this.pnlGroup.Name = "pnlGroup";
            this.pnlGroup.Size = new System.Drawing.Size(522, 204);
            this.pnlGroup.TabIndex = 1;
            // 
            // frmMessageBox
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(522, 204);
            this.Controls.Add(this.pnlGroup);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMessageBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message Inforamtion";
            this.Load += new System.EventHandler(this.frmMessageBox_Load);
            this.Closed += new System.EventHandler(this.frmMessageBox_Closed);
            this.pnlDetailMsg.ResumeLayout(false);
            this.pnlDetailMsg.PerformLayout();
            this.pnlMsgTop.ResumeLayout(false);
            this.pnlOK.ResumeLayout(false);
            this.pnlOKCancel.ResumeLayout(false);
            this.pnlRetryCancel.ResumeLayout(false);
            this.pnlAbortRetryIgnore.ResumeLayout(false);
            this.pnlYesNoCancel.ResumeLayout(false);
            this.pnlYesNo.ResumeLayout(false);
            this.pnlMessage.ResumeLayout(false);
            this.grpMessage.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlGroup.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		
		#endregion
		
		#region " Variable Definition "
		
		private const int MSG_BOX_HEIGHT = 128;
		private const int MSG_BOX_DETAIL_HEIGHT = 108;
		
		#endregion
		
		#region " Function Implementations"

        // Show()
        //       - Set sMessage Box Option
        // Return Value
        //       - System.Windows.Forms.DialogResult
        // Arguments
        //       - ByVal sMessage As String
        //       - Optional ByVal sCaption As String = ""
        //       - Optional ByVal btnStyle As MessageBoxButtons = MessageBoxButtons.OK
        //       - Optional ByVal iDefaultFocus As Integer = 1
        //
        public System.Windows.Forms.DialogResult Show(string sMessage, string sCaption, MessageBoxButtons btnStyle, int iDefaultFocus)
        {

            try
            {
                pnlOK.Dock = DockStyle.None;
                pnlOKCancel.Dock = DockStyle.None;
                pnlYesNo.Dock = DockStyle.None;
                pnlYesNoCancel.Dock = DockStyle.None;
                pnlRetryCancel.Dock = DockStyle.None;
                pnlAbortRetryIgnore.Dock = DockStyle.None;

                pnlOK.Visible = false;
                pnlOKCancel.Visible = false;
                pnlYesNo.Visible = false;
                pnlYesNoCancel.Visible = false;
                pnlRetryCancel.Visible = false;
                pnlAbortRetryIgnore.Visible = false;

                //Button Show
                switch (btnStyle)
                {
                    case MessageBoxButtons.OK:

                        pnlOK.Visible = true;
                        pnlOK.Dock = DockStyle.Fill;
                        btnOKOK.Select();
                        break;
                    case MessageBoxButtons.OKCancel:

                        pnlOKCancel.Visible = true;
                        pnlOKCancel.Dock = DockStyle.Fill;
                        switch (iDefaultFocus)
                        {
                            case 1:

                                btnOKCancelOK.TabIndex = 0;
                                btnOKCancelCancel.TabIndex = 1;
                                btnOKCancelOK.Select();
                                break;
                            case 2:

                                btnOKCancelOK.TabIndex = 1;
                                btnOKCancelCancel.TabIndex = 0;
                                btnOKCancelCancel.Select();
                                break;
                        }
                        break;
                    case MessageBoxButtons.YesNo:

                        pnlYesNo.Visible = true;
                        pnlYesNo.Dock = DockStyle.Fill;
                        switch (iDefaultFocus)
                        {
                            case 1:

                                btnYesNoYes.TabIndex = 0;
                                btnYesNoNo.TabIndex = 1;
                                btnYesNoYes.Select();
                                break;
                            case 2:

                                btnYesNoNo.TabIndex = 1;
                                btnYesNoYes.TabIndex = 0;
                                btnYesNoNo.Select();
                                break;
                        }
                        break;
                    case MessageBoxButtons.YesNoCancel:

                        pnlYesNoCancel.Visible = true;
                        pnlYesNoCancel.Dock = DockStyle.Fill;
                        switch (iDefaultFocus)
                        {
                            case 1:

                                btnYesNoCancelYes.TabIndex = 0;
                                btnYesNoCancelNo.TabIndex = 1;
                                btnYesNoCancelCancel.TabIndex = 2;
                                btnYesNoCancelYes.Select();
                                break;
                            case 2:

                                btnYesNoCancelYes.TabIndex = 2;
                                btnYesNoCancelNo.TabIndex = 0;
                                btnYesNoCancelCancel.TabIndex = 1;
                                btnYesNoCancelNo.Select();
                                break;
                            case 3:

                                btnYesNoCancelYes.TabIndex = 1;
                                btnYesNoCancelNo.TabIndex = 2;
                                btnYesNoCancelCancel.TabIndex = 0;
                                btnYesNoCancelCancel.Select();
                                break;
                        }
                        break;
                    case MessageBoxButtons.RetryCancel:

                        pnlRetryCancel.Visible = true;
                        pnlRetryCancel.Dock = DockStyle.Fill;
                        switch (iDefaultFocus)
                        {
                            case 1:

                                btnRetryCancelRetry.TabIndex = 0;
                                btnRetryCancelCancel.TabIndex = 1;
                                btnRetryCancelRetry.Select();
                                break;
                            case 2:

                                btnRetryCancelRetry.TabIndex = 1;
                                btnRetryCancelCancel.TabIndex = 0;
                                btnRetryCancelCancel.Select();
                                break;
                        }
                        break;
                    case MessageBoxButtons.AbortRetryIgnore:

                        pnlAbortRetryIgnore.Visible = true;
                        pnlAbortRetryIgnore.Dock = DockStyle.Fill;
                        switch (iDefaultFocus)
                        {
                            case 1:

                                btnAbortRetryIgnoreAbort.TabIndex = 0;
                                btnAbortRetryIgnoreRetry.TabIndex = 1;
                                btnAbortRetryIgnoreIgnore.TabIndex = 2;
                                btnAbortRetryIgnoreAbort.Select();
                                break;
                            case 2:

                                btnAbortRetryIgnoreAbort.TabIndex = 2;
                                btnAbortRetryIgnoreRetry.TabIndex = 0;
                                btnAbortRetryIgnoreIgnore.TabIndex = 1;
                                btnAbortRetryIgnoreRetry.Select();
                                break;
                            case 3:

                                btnAbortRetryIgnoreAbort.TabIndex = 1;
                                btnAbortRetryIgnoreRetry.TabIndex = 2;
                                btnAbortRetryIgnoreIgnore.TabIndex = 0;
                                btnAbortRetryIgnoreIgnore.Select();
                                break;
                        }
                        break;
                    default:

                        break;
                }

                this.Height = MSG_BOX_HEIGHT;

                //sMessage Show
                lblMessage.Text = sMessage;
                btnDetail.Visible = false;
                this.Text = sCaption;

                this.StartPosition = FormStartPosition.CenterParent;

                return this.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmMessageBox.Show()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
                return System.Windows.Forms.DialogResult.None;
            }

        }

        // Show()
		//       - Set sMessage Box Option
		// Return Value
		//       - System.Windows.Forms.DialogResult
		// Arguments
		//       - ByVal sMessage As String
		//       - Optional ByVal sCaption As String = ""
		//       - Optional ByVal btnStyle As MessageBoxButtons = MessageBoxButtons.OK
		//       - Optional ByVal iDefaultFocus As Integer = 1
		//
        public System.Windows.Forms.DialogResult Show(string sMessage, string sCaption, MessageBoxButtons btnStyle, int iDefaultFocus, ref Form owner)
		{
			
			try
			{
				pnlOK.Dock = DockStyle.None;
				pnlOKCancel.Dock = DockStyle.None;
				pnlYesNo.Dock = DockStyle.None;
				pnlYesNoCancel.Dock = DockStyle.None;
				pnlRetryCancel.Dock = DockStyle.None;
				pnlAbortRetryIgnore.Dock = DockStyle.None;
				
				pnlOK.Visible = false;
				pnlOKCancel.Visible = false;
				pnlYesNo.Visible = false;
				pnlYesNoCancel.Visible = false;
				pnlRetryCancel.Visible = false;
				pnlAbortRetryIgnore.Visible = false;
				
				//Button Show
				switch (btnStyle)
				{
					case MessageBoxButtons.OK:
						
						pnlOK.Visible = true;
						pnlOK.Dock = DockStyle.Fill;
						btnOKOK.Select();
						break;
					case MessageBoxButtons.OKCancel:
						
						pnlOKCancel.Visible = true;
						pnlOKCancel.Dock = DockStyle.Fill;
						switch (iDefaultFocus)
						{
							case 1:
								
								btnOKCancelOK.TabIndex = 0;
								btnOKCancelCancel.TabIndex = 1;
								btnOKCancelOK.Select();
								break;
							case 2:
								
								btnOKCancelOK.TabIndex = 1;
								btnOKCancelCancel.TabIndex = 0;
								btnOKCancelCancel.Select();
								break;
						}
						break;
					case MessageBoxButtons.YesNo:
						
						pnlYesNo.Visible = true;
						pnlYesNo.Dock = DockStyle.Fill;
						switch (iDefaultFocus)
						{
							case 1:
								
								btnYesNoYes.TabIndex = 0;
								btnYesNoNo.TabIndex = 1;
								btnYesNoYes.Select();
								break;
							case 2:
								
								btnYesNoNo.TabIndex = 1;
								btnYesNoYes.TabIndex = 0;
								btnYesNoNo.Select();
								break;
						}
						break;
					case MessageBoxButtons.YesNoCancel:
						
						pnlYesNoCancel.Visible = true;
						pnlYesNoCancel.Dock = DockStyle.Fill;
						switch (iDefaultFocus)
						{
							case 1:
								
								btnYesNoCancelYes.TabIndex = 0;
								btnYesNoCancelNo.TabIndex = 1;
								btnYesNoCancelCancel.TabIndex = 2;
								btnYesNoCancelYes.Select();
								break;
							case 2:
								
								btnYesNoCancelYes.TabIndex = 2;
								btnYesNoCancelNo.TabIndex = 0;
								btnYesNoCancelCancel.TabIndex = 1;
								btnYesNoCancelNo.Select();
								break;
							case 3:
								
								btnYesNoCancelYes.TabIndex = 1;
								btnYesNoCancelNo.TabIndex = 2;
								btnYesNoCancelCancel.TabIndex = 0;
								btnYesNoCancelCancel.Select();
								break;
						}
						break;
					case MessageBoxButtons.RetryCancel:
						
						pnlRetryCancel.Visible = true;
						pnlRetryCancel.Dock = DockStyle.Fill;
						switch (iDefaultFocus)
						{
							case 1:
								
								btnRetryCancelRetry.TabIndex = 0;
								btnRetryCancelCancel.TabIndex = 1;
								btnRetryCancelRetry.Select();
								break;
							case 2:
								
								btnRetryCancelRetry.TabIndex = 1;
								btnRetryCancelCancel.TabIndex = 0;
								btnRetryCancelCancel.Select();
								break;
						}
						break;
					case MessageBoxButtons.AbortRetryIgnore:
						
						pnlAbortRetryIgnore.Visible = true;
						pnlAbortRetryIgnore.Dock = DockStyle.Fill;
						switch (iDefaultFocus)
						{
							case 1:
								
								btnAbortRetryIgnoreAbort.TabIndex = 0;
								btnAbortRetryIgnoreRetry.TabIndex = 1;
								btnAbortRetryIgnoreIgnore.TabIndex = 2;
								btnAbortRetryIgnoreAbort.Select();
								break;
							case 2:
								
								btnAbortRetryIgnoreAbort.TabIndex = 2;
								btnAbortRetryIgnoreRetry.TabIndex = 0;
								btnAbortRetryIgnoreIgnore.TabIndex = 1;
								btnAbortRetryIgnoreRetry.Select();
								break;
							case 3:
								
								btnAbortRetryIgnoreAbort.TabIndex = 1;
								btnAbortRetryIgnoreRetry.TabIndex = 2;
								btnAbortRetryIgnoreIgnore.TabIndex = 0;
								btnAbortRetryIgnoreIgnore.Select();
								break;
						}
						break;
					default:
						
						break;
				}
				
				this.Height = MSG_BOX_HEIGHT;
				
				//sMessage Show
				lblMessage.Text = sMessage;
				btnDetail.Visible = false;
				this.Text = sCaption;
				
				this.StartPosition = FormStartPosition.CenterParent;
				
				if (owner == null)
				{
					return this.ShowDialog();
				}
				else
				{
					return this.ShowDialog(owner);
				}
				
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.Show()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
				return System.Windows.Forms.DialogResult.None;
			}

		}

        //Show()
        //      - Set sMessage Box Option
        //Return Value
        //      - System.Windows.Forms.DialogResult
        //Arguments
        //      - ByVal MsgString As clsMsgString
        //      - Optional ByVal sCaption As String = ""
        //      - Optional ByVal btnStyle As MessageBoxButtons = MessageBoxButtons.OK
        //      - Optional ByVal iDefaultFocus As Integer = 1
        public System.Windows.Forms.DialogResult Show(ref ReturnMessageString MsgString, string sCaption, MessageBoxButtons btnStyle, int iDefaultFocus)
        {

            try
            {
                int i;

                pnlOK.Dock = DockStyle.None;
                pnlOKCancel.Dock = DockStyle.None;
                pnlYesNo.Dock = DockStyle.None;
                pnlYesNoCancel.Dock = DockStyle.None;
                pnlRetryCancel.Dock = DockStyle.None;
                pnlAbortRetryIgnore.Dock = DockStyle.None;

                pnlOK.Visible = false;
                pnlOKCancel.Visible = false;
                pnlYesNo.Visible = false;
                pnlYesNoCancel.Visible = false;
                pnlRetryCancel.Visible = false;
                pnlAbortRetryIgnore.Visible = false;

                //Button Show
                switch (btnStyle)
                {
                    case MessageBoxButtons.OK:

                        pnlOK.Visible = true;
                        pnlOK.Dock = DockStyle.Fill;
                        btnOKOK.Select();
                        break;
                    case MessageBoxButtons.OKCancel:

                        pnlOKCancel.Visible = true;
                        pnlOKCancel.Dock = DockStyle.Fill;
                        switch (iDefaultFocus)
                        {
                            case 1:

                                btnOKCancelOK.TabIndex = 0;
                                btnOKCancelCancel.TabIndex = 1;
                                btnOKCancelOK.Select();
                                break;
                            case 2:

                                btnOKCancelOK.TabIndex = 1;
                                btnOKCancelCancel.TabIndex = 0;
                                btnOKCancelCancel.Select();
                                break;
                        }
                        break;
                    case MessageBoxButtons.YesNo:

                        pnlYesNo.Visible = true;
                        pnlYesNo.Dock = DockStyle.Fill;
                        switch (iDefaultFocus)
                        {
                            case 1:

                                btnYesNoYes.TabIndex = 0;
                                btnYesNoNo.TabIndex = 1;
                                btnYesNoYes.Select();
                                break;
                            case 2:

                                btnYesNoNo.TabIndex = 1;
                                btnYesNoYes.TabIndex = 0;
                                btnYesNoNo.Select();
                                break;
                        }
                        break;
                    case MessageBoxButtons.YesNoCancel:

                        pnlYesNoCancel.Visible = true;
                        pnlYesNoCancel.Dock = DockStyle.Fill;
                        switch (iDefaultFocus)
                        {
                            case 1:

                                btnYesNoCancelYes.TabIndex = 0;
                                btnYesNoCancelNo.TabIndex = 1;
                                btnYesNoCancelCancel.TabIndex = 2;
                                btnYesNoCancelYes.Select();
                                break;
                            case 2:

                                btnYesNoCancelYes.TabIndex = 2;
                                btnYesNoCancelNo.TabIndex = 0;
                                btnYesNoCancelCancel.TabIndex = 1;
                                btnYesNoCancelNo.Select();
                                break;
                            case 3:

                                btnYesNoCancelYes.TabIndex = 1;
                                btnYesNoCancelNo.TabIndex = 2;
                                btnYesNoCancelCancel.TabIndex = 0;
                                btnYesNoCancelCancel.Select();
                                break;
                        }
                        break;
                    case MessageBoxButtons.RetryCancel:

                        pnlRetryCancel.Visible = true;
                        pnlRetryCancel.Dock = DockStyle.Fill;
                        switch (iDefaultFocus)
                        {
                            case 1:

                                btnRetryCancelRetry.TabIndex = 0;
                                btnRetryCancelCancel.TabIndex = 1;
                                btnRetryCancelRetry.Select();
                                break;
                            case 2:

                                btnRetryCancelRetry.TabIndex = 1;
                                btnRetryCancelCancel.TabIndex = 0;
                                btnRetryCancelCancel.Select();
                                break;
                        }
                        break;
                    case MessageBoxButtons.AbortRetryIgnore:

                        pnlAbortRetryIgnore.Visible = true;
                        pnlAbortRetryIgnore.Dock = DockStyle.Fill;
                        switch (iDefaultFocus)
                        {
                            case 1:

                                btnAbortRetryIgnoreAbort.TabIndex = 0;
                                btnAbortRetryIgnoreRetry.TabIndex = 1;
                                btnAbortRetryIgnoreIgnore.TabIndex = 2;
                                btnAbortRetryIgnoreAbort.Select();
                                break;
                            case 2:

                                btnAbortRetryIgnoreAbort.TabIndex = 2;
                                btnAbortRetryIgnoreRetry.TabIndex = 0;
                                btnAbortRetryIgnoreIgnore.TabIndex = 1;
                                btnAbortRetryIgnoreRetry.Select();
                                break;
                            case 3:

                                btnAbortRetryIgnoreAbort.TabIndex = 1;
                                btnAbortRetryIgnoreRetry.TabIndex = 2;
                                btnAbortRetryIgnoreIgnore.TabIndex = 0;
                                btnAbortRetryIgnoreIgnore.Select();
                                break;
                        }
                        break;
                    default:

                        break;
                }

                this.Height = MSG_BOX_HEIGHT;

                //sMessage Show
                lblMessage.Text = CmnFunction.Trim(MsgString.ErrorMsg);

                this.Text = sCaption;
                if (MsgString.MsgCode.Trim() != "")
                {
                    this.Text += " [" + MsgString.MsgCode.Trim() + "]";
                }

                if (MsgString.DBErrorMsg.Trim() != "" || MsgString.FieldMsg.Count > 0)
                {
                    string sFieldMsg = "";
                    string sMsg = "";
                    int iStartIndex;
                    int iEndIndex;
                    btnDetail.Visible = true;
                    txtDetailMsg.Text = "";
                    if (MsgString.DBErrorMsg.Trim() != "")
                    {
                        txtDetailMsg.Text += MsgString.DBErrorMsg.Trim();
                        txtDetailMsg.Text += "\r\n";
                        txtDetailMsg.Text += "\r\n";
                    }
                    if (MsgString.FieldMsg.Count > 0)
                    {
                        for (i = 0; i <= MsgString.FieldMsg.Count - 1; i++)
                        {
                            sFieldMsg = CmnFunction.Trim(MsgString.FieldMsg[i]);
                            iStartIndex = 0;
                            iEndIndex = sFieldMsg.IndexOf(":");
                            if (iEndIndex >= 0)
                            {
                                sMsg = sFieldMsg.Substring(iStartIndex, (iEndIndex - iStartIndex) + 1);
                                if (sMsg.Trim() != "")
                                {
                                    txtDetailMsg.Text += sMsg.Trim();
                                    txtDetailMsg.Text += "\r\n";
                                }
                            }
                            while (true)
                            {
                                iStartIndex = iEndIndex + 1;
                                if (iStartIndex >= sFieldMsg.Length)
                                {
                                    break;
                                }
                                iEndIndex = sFieldMsg.IndexOf(",", iStartIndex);
                                if (iEndIndex >= iStartIndex)
                                {
                                    sMsg = sFieldMsg.Substring(iStartIndex, (iEndIndex - iStartIndex));
                                    if (sMsg.Trim() != "")
                                    {
                                        txtDetailMsg.Text += sMsg.Trim();
                                        txtDetailMsg.Text += "\r\n";
                                    }
                                }
                                else
                                {
                                    sMsg = sFieldMsg.Substring(iStartIndex, (sFieldMsg.Length - iStartIndex));
                                    if (sMsg.Trim() != "")
                                    {
                                        txtDetailMsg.Text += sMsg.Trim();
                                        txtDetailMsg.Text += "\r\n";
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    btnDetail.Visible = false;
                }

                this.StartPosition = FormStartPosition.CenterParent;

                return this.ShowDialog();

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmMessageBox.Show()\n" + ex.Message);
                return System.Windows.Forms.DialogResult.None;
            }
        }

        #endregion

		#region " Event Implementations"

		private void frmMessageBox_Load(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Hide();
				btnYesNoYes.Font = new System.Drawing.Font(btnYesNoYes.Font.Name, GlobalVariable.giMessageSize);
				btnYesNoNo.Font = new System.Drawing.Font(btnYesNoNo.Font.Name, GlobalVariable.giMessageSize);
				btnOKCancelOK.Font = new System.Drawing.Font(btnOKCancelOK.Font.Name, GlobalVariable.giMessageSize);
				btnOKCancelCancel.Font = new System.Drawing.Font(btnOKCancelCancel.Font.Name, GlobalVariable.giMessageSize);
				btnOKOK.Font = new System.Drawing.Font(btnOKOK.Font.Name, GlobalVariable.giMessageSize);
				btnYesNoCancelYes.Font = new System.Drawing.Font(btnYesNoCancelYes.Font.Name, GlobalVariable.giMessageSize);
				btnYesNoCancelNo.Font = new System.Drawing.Font(btnYesNoCancelNo.Font.Name, GlobalVariable.giMessageSize);
				btnYesNoCancelCancel.Font = new System.Drawing.Font(btnYesNoCancelCancel.Font.Name, GlobalVariable.giMessageSize);

				lblMessage.Font = new System.Drawing.Font(lblMessage.Font.Name, GlobalVariable.giMessageSize);
				txtDetailMsg.Font = new System.Drawing.Font(txtDetailMsg.Font.Name, GlobalVariable.giMessageSize);
				
				this.Height = MSG_BOX_HEIGHT;
				LanguageFunction.ToClientLanguage(this);
				this.Show();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.frmMessageBox_Load()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnOKOK_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnOKOK_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnOKCancelOK_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnOKCancelOK_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnOKCancelCancel_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnOKCancelCancel_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnYesNoCancelCancel_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnYesNoCancelCancel_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnRetryCancelCancel_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnRetryCancelCancel_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnYesNoYes_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnYesNoYes_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnYesNoCancelYes_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnYesNoCancelYes_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnYesNoNo_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnYesNoNo_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnYesNoCancelNo_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnYesNoCancelNo_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnRetryCancelRetry_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnRetryCancelRetry_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnAbortRetryIgnoreAbort_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnAbortRetryIgnoreAbort_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnAbortRetryIgnoreRetry_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnAbortRetryIgnoreRetry_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnAbortRetryIgnoreIgnore_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Close();
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnAbortRetryIgnoreIgnore_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnDetail_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				btnSimple.Visible = true;
				btnSimple.Enabled = true;
				btnDetail.Visible = false;
				btnDetail.Enabled = false;
				
				pnlDetailMsg.Visible = true;
				this.Height = MSG_BOX_HEIGHT + MSG_BOX_DETAIL_HEIGHT;
				
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnDetail_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnSimple_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				btnSimple.Visible = false;
				btnSimple.Enabled = false;
				btnDetail.Visible = true;
				btnDetail.Enabled = true;
				
				pnlDetailMsg.Visible = false;
				this.Height = MSG_BOX_HEIGHT;
				
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnSimple_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void btnCopy_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				Clipboard.SetDataObject(lblMessage.Text, true);
				
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.btnCopy_Click()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		private void frmMessageBox_Closed(object sender, System.EventArgs e)
		{
			
			try
			{
				this.Dispose(false);
			}
			catch (Exception ex)
			{
                MessageBox.Show("frmMessageBox.frmMessageBox_Closed()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK);
			}
			
		}
		
		#endregion
		
	}
	
}
