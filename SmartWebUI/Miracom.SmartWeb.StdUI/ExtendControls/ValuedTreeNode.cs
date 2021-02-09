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
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace Miracom.SmartWeb.UI.Controls
{
	/// <summary>
	/// ValuedTreeNode�� ���� ��� �����Դϴ�.
	/// </summary>
	public class ValuedTreeNode : System.Windows.Forms.TreeNode
	{
		/// <summary>
		/// �ʼ� �����̳� �����Դϴ�.
		/// </summary>
	
		private string m_value;
		private bool _isChecked;
		private int _seq;
			
		public ValuedTreeNode() : base()
		{
		}

		public ValuedTreeNode(string Text, string Value)
		{
			base.Text = Text;
			this.m_value = Value;
		}
		
		public new string Text
		{
			get
			{
				return base.Text;
			}

			set
			{
				base.Text = value;
			}
		}

		public string Value
		{
			get
			{
				return m_value;
			}

			set
			{
				m_value = value;
			}
		}

		public bool IsChecked
		{
			get
			{
				return _isChecked;
			}
			set
			{
				_isChecked = value;
			}
		}

		public int Seq
		{
			get
			{
				return _seq;
			}
			set
			{
				_seq = value;
			}
		}

	}
}
