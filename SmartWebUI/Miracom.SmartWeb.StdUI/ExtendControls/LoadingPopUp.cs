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
	/// UserControl1에 대한 요약 설명입니다.
	/// </summary>
	public class LoadingPopUp : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components = null;

		private static LoadingPopUp loading;
		
		// 진행 팝업창을 띄움
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

		// 진행 팝업청을 숨김
		public static void LoadingPopUpHidden()
		{
			if ( loading != null )
				loading.Hide();
		}

		private LoadingPopUp()
		{
			// 이 호출은 Windows.Forms Form 디자이너에 필요합니다.
			InitializeComponent();

			// TODO: InitializeComponent를 호출한 다음 초기화 작업을 추가합니다.
			this.TopMost = true;
		}

		/// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
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

		#region 구성 요소 디자이너에서 생성한 코드
		/// <summary> 
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
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
