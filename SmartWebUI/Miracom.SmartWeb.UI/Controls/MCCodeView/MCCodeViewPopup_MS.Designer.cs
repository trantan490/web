namespace Miracom.SmartWeb.UI.Controls.MCCodeView
{
    partial class MCCodeViewPopup_MS
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_MCTextBox5 = new System.Windows.Forms.TextBox();
            this.m_MCTextBox4 = new System.Windows.Forms.TextBox();
            this.m_panel = new System.Windows.Forms.Panel();
            this.m_MCTextBox3 = new System.Windows.Forms.TextBox();
            this.m_MCTextBox2 = new System.Windows.Forms.TextBox();
            this.m_MCTextBox1 = new System.Windows.Forms.TextBox();
            this.m_MCCodeDropList = new Miracom.UI.Controls.MCCodeView.MCCodeDropList();
            this.pnlTextBox = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_MCCodeDropList)).BeginInit();
            this.pnlTextBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_MCTextBox5
            // 
            this.m_MCTextBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_MCTextBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_MCTextBox5.Location = new System.Drawing.Point(40, 1);
            this.m_MCTextBox5.Name = "m_MCTextBox5";
            this.m_MCTextBox5.Size = new System.Drawing.Size(60, 20);
            this.m_MCTextBox5.TabIndex = 5;
            this.m_MCTextBox5.Tag = "5";
            // 
            // m_MCTextBox4
            // 
            this.m_MCTextBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_MCTextBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_MCTextBox4.Location = new System.Drawing.Point(30, 1);
            this.m_MCTextBox4.Name = "m_MCTextBox4";
            this.m_MCTextBox4.Size = new System.Drawing.Size(10, 20);
            this.m_MCTextBox4.TabIndex = 4;
            this.m_MCTextBox4.Tag = "4";
            // 
            // m_panel
            // 
            this.m_panel.BackColor = System.Drawing.SystemColors.Control;
            this.m_panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panel.Location = new System.Drawing.Point(0, 105);
            this.m_panel.Name = "m_panel";
            this.m_panel.Size = new System.Drawing.Size(100, 1);
            this.m_panel.TabIndex = 6;
            // 
            // m_MCTextBox3
            // 
            this.m_MCTextBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_MCTextBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_MCTextBox3.Location = new System.Drawing.Point(20, 1);
            this.m_MCTextBox3.Name = "m_MCTextBox3";
            this.m_MCTextBox3.Size = new System.Drawing.Size(10, 20);
            this.m_MCTextBox3.TabIndex = 3;
            this.m_MCTextBox3.Tag = "3";
            // 
            // m_MCTextBox2
            // 
            this.m_MCTextBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_MCTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_MCTextBox2.Location = new System.Drawing.Point(10, 1);
            this.m_MCTextBox2.Name = "m_MCTextBox2";
            this.m_MCTextBox2.Size = new System.Drawing.Size(10, 20);
            this.m_MCTextBox2.TabIndex = 2;
            this.m_MCTextBox2.Tag = "2";
            // 
            // m_MCTextBox1
            // 
            this.m_MCTextBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_MCTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_MCTextBox1.Location = new System.Drawing.Point(0, 1);
            this.m_MCTextBox1.Name = "m_MCTextBox1";
            this.m_MCTextBox1.Size = new System.Drawing.Size(10, 20);
            this.m_MCTextBox1.TabIndex = 1;
            this.m_MCTextBox1.Tag = "1";
            // 
            // m_MCCodeDropList
            // 
            this.m_MCCodeDropList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_MCCodeDropList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_MCCodeDropList.EnableSortIcon = true;
            this.m_MCCodeDropList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_MCCodeDropList.FullRowSelect = true;
            this.m_MCCodeDropList.GridLines = true;
            this.m_MCCodeDropList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_MCCodeDropList.HideSelection = false;
            this.m_MCCodeDropList.Location = new System.Drawing.Point(0, 28);
            this.m_MCCodeDropList.MultiSelect = false;
            this.m_MCCodeDropList.Name = "m_MCCodeDropList";
            this.m_MCCodeDropList.Size = new System.Drawing.Size(100, 78);
            this.m_MCCodeDropList.TabIndex = 7;
            this.m_MCCodeDropList.UseCompatibleStateImageBehavior = false;
            this.m_MCCodeDropList.View = System.Windows.Forms.View.Details;
            // 
            // pnlTextBox
            // 
            this.pnlTextBox.BackColor = System.Drawing.Color.Transparent;
            this.pnlTextBox.Controls.Add(this.m_MCTextBox5);
            this.pnlTextBox.Controls.Add(this.m_MCTextBox4);
            this.pnlTextBox.Controls.Add(this.m_MCTextBox3);
            this.pnlTextBox.Controls.Add(this.m_MCTextBox2);
            this.pnlTextBox.Controls.Add(this.m_MCTextBox1);
            this.pnlTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTextBox.Location = new System.Drawing.Point(0, 106);
            this.pnlTextBox.Name = "pnlTextBox";
            this.pnlTextBox.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.pnlTextBox.Size = new System.Drawing.Size(100, 22);
            this.pnlTextBox.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 28);
            this.panel1.TabIndex = 9;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 24);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "V";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // MCCodeViewPopup_MS
            // 
            this.ClientSize = new System.Drawing.Size(100, 128);
            this.Controls.Add(this.m_panel);
            this.Controls.Add(this.m_MCCodeDropList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlTextBox);
            this.Name = "MCCodeViewPopup_MS";
            ((System.ComponentModel.ISupportInitialize)(this.m_MCCodeDropList)).EndInit();
            this.pnlTextBox.ResumeLayout(false);
            this.pnlTextBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox m_MCTextBox5;
        private System.Windows.Forms.TextBox m_MCTextBox4;
        private System.Windows.Forms.Panel m_panel;
        private System.Windows.Forms.TextBox m_MCTextBox3;
        private System.Windows.Forms.TextBox m_MCTextBox2;
        private System.Windows.Forms.TextBox m_MCTextBox1;
        internal Miracom.UI.Controls.MCCodeView.MCCodeDropList m_MCCodeDropList;
        private System.Windows.Forms.Panel pnlTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
    }
}
