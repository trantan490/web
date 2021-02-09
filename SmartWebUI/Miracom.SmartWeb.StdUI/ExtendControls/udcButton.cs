//-----------------------------------------------------------------------------
//
//   System      : MES Report
//   File Name   : 
//   Description : Client Common function Module 
//
//   MES Version : 4.x.x.x
//
//   History
//       - **** Do Not Modify in Site!!! ****
//       - 2008-10-01 : Created by John Seo
//
//
//   Copyright(C) 1998-2005 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------
using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.UI.Controls;

namespace Miracom.SmartWeb.UI.Controls
{
    public delegate void PopUpClosedHandeler();

    public enum AutoBindingBtnType { S_Group };

    public class udcButton : System.Windows.Forms.Button 
    {
        private Form popUp;
        private bool isPopUped;
        private AutoBindingBtnType _typeList;

        // �˾��� ����Ƕ� ��
        public event PopUpClosedHandeler PopUpClosed;

        // ���ο� ���ε��� ���� ����
        public Form BindingForm
        {
            get
            {
                return popUp;
            }
        }

        public AutoBindingBtnType AutoBindingMultiButtonType
        {
            get
            {
                return _typeList;
            }

            set
            {
                _typeList = value;
                PopUpInit();
                ButttonInit();
            }
        }
 
        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                //base.Text = "";

                Assembly _thisAssm = Assembly.GetAssembly(this.GetType());

                switch (_typeList)
                {
                    //case AutoBindingBtnType.S_Detail:
                    //    base.Text = "Detail";
                    //    break;
                    case AutoBindingBtnType.S_Group:
                        base.Text = "Group";
                        break;
                }                 
            }
        }

        public new System.Drawing.Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = new System.Drawing.Size(70, 23);
            }
        }

        public udcButton()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Click += new EventHandler(udcButton_Click);
            this.LostFocus += new EventHandler(udcButton_LostFocus);
            this.MouseEnter += new EventHandler(udcButton_MouseEnter);
            this.MouseLeave += new EventHandler(udcButton_MouseLeave);
                 
            base.Size = new System.Drawing.Size(70, 21);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            
            //������ ���� ������.(Run-Time�ô� �������)
            ButttonInit();            
        }
         
        public void AutoBinding()
        {
            if (popUp != null)
            {
                ((IBaseExtendForm)popUp).AutoBinding();
            }
        }

        #region ��ư�� ���� ���� ���� �޼ҵ�
        private void PopUpInit()
        {
            switch (_typeList)
            {
                //case AutoBindingBtnType.S_Detail:
                //    this.popUp = new Miracom.SmartWeb.UI.Controls.udcDetailForm();                   

                //    break;
                case AutoBindingBtnType.S_Group:
                    this.popUp = new Miracom.SmartWeb.UI.Controls.udcTableForm();                    
                    this.PopUpClosed += new PopUpClosedHandeler(((udcTableForm)popUp).SelectedAll);
                    break;
            }

            popUp.FormBorderStyle = FormBorderStyle.None;
            popUp.StartPosition = FormStartPosition.Manual;
            popUp.TopMost = true;
            popUp.ShowInTaskbar = false;
            popUp.Deactivate += new EventHandler(popUp_Deactivate);
            
        }

        private void udcButton_Click(object sender, EventArgs e)
        {
             
            if (!isPopUped)
            {
                ShowDropDown();
            }
            else
            {
                HiddenDropDown();
            }             
        }

        private void udcButton_LostFocus(object sender, EventArgs e)
        {
        }

        private void ShowDropDown()
        {
            isPopUped = true;
            Rectangle pos = RectangleToScreen(ClientRectangle);

            if (pos.Right - popUp.Size.Width > 0)
                popUp.Location = new Point(pos.Right - popUp.Size.Width, pos.Bottom + 1);
            else
                popUp.Location = new Point(pos.X, pos.Bottom + 1);
            popUp.Show();
            popUp.Focus();
        }

        private void HiddenDropDown()
        {
            if (popUp != null)
            {
                if (PopUpClosed != null)
                    PopUpClosed();

                isPopUped = false;
                popUp.Hide();
            }
        }

        private void popUp_Deactivate(object sender, EventArgs e)
        {
            //��ư���� ó���ϱ� ���ؼ� �ּ� ó��(2008.10.03)
            HiddenDropDown();
        }

        private void ButttonInit()
        {
            Assembly _thisAssm = Assembly.GetAssembly(this.GetType());

            switch (_typeList)
            {
                //case AutoBindingBtnType.S_Detail:
                //    this.Image = global::Miracom.SmartWeb.UI.Properties.Resources._021prjdown;
                //    base.Text = "Detail";  //2008.10.03
                //    break;
                case AutoBindingBtnType.S_Group:
                    this.Image = global::Miracom.SmartWeb.UI.Properties.Resources._007grpbar;
                    base.Text = "Group";   //2008.10.03
                    break;
            }

            this.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UseVisualStyleBackColor = true;
        }
         
        private void udcButton_MouseEnter(object sender, EventArgs e)
        {
            // ButttonInit();
            this.Focus();
        }

        private void udcButton_MouseLeave(object sender, EventArgs e)
        {
            // ButttonInit();
        }

        #endregion

    }
}
