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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Miracom.SmartWeb;

namespace Miracom.SmartWeb.UI.Controls
{
	/// <summary>
	/// UserControl1�� ���� ��� �����Դϴ�.
	/// </summary>
	public class LoadingPopUp : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components = null;

		private static LoadingPopUp loading;
		
		// ���� �˾�â�� ���
        public static void LoadIngPopUpShow(System.Windows.Forms.Control parent)
        {
            if (loading == null)
            {
                loading = new LoadingPopUp();
            }

            Rectangle pos = parent.RectangleToScreen(parent.ClientRectangle);
            loading.Location = new Point(pos.Left + pos.Width / 2 - loading.Size.Width / 2, pos.Top + pos.Height / 2 - loading.Size.Height / 2);
            loading.Show();
            loading.Focus();
        }         

		// ���� �˾�û�� ����
		public static void LoadingPopUpHidden()
		{
			if ( loading != null )
				loading.Hide();
		}

		private LoadingPopUp()
		{
			// �� ȣ���� Windows.Forms Form �����̳ʿ� �ʿ��մϴ�.
			InitializeComponent();

			// TODO: InitializeComponent�� ȣ���� ���� �ʱ�ȭ �۾��� �߰��մϴ�.
			this.TopMost = true;
		}

		/// <summary> 
		/// ��� ���� ��� ���ҽ��� �����մϴ�.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region ���� ��� �����̳ʿ��� ������ �ڵ�
		/// <summary> 
		/// �����̳� ������ �ʿ��� �޼����Դϴ�. 
		/// �� �޼����� ������ �ڵ� ������� �������� ���ʽÿ�.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LoadingPopUp));
			// 
			// LoadingPopUp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(251, 104);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.ForeColor = System.Drawing.Color.Transparent;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "LoadingPopUp";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.TransparencyKey = System.Drawing.Color.Red;

		}
		#endregion

		private void timer1_Tick(object sender, System.EventArgs e)
		{
		
		}
	}
}
