using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;

namespace Miracom.UI
{
	namespace Controls
	{
		
		public class MCPopupFormBase : System.Windows.Forms.Form
        {
            protected bool m_bVisibleColumnHeader;
            protected int m_iSelectedSubItemIndex;
            protected int m_iDisplaySubItemIndex;
            protected bool m_bDroppedDownFlag = false;

            #region " Windows Form 디자이너에서 생성한 코드 "

            public MCPopupFormBase()
			{
				
				
				InitializeComponent();
				
				
				
			}
			
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
			
			private System.ComponentModel.IContainer components = null;
	
			[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
			{
                this.SuspendLayout();
                // 
                // MCPopupFormBase
                // 
                this.BackColor = System.Drawing.SystemColors.Control;
                this.ClientSize = new System.Drawing.Size(168, 69);
                this.ControlBox = false;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "MCPopupFormBase";
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                this.Text = "MCPopupFormBase";
                this.ResumeLayout(false);

			}
			
			#endregion

            #region " Properties"

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool VisibleColumnHeader
            {
                get
                {
                    return m_bVisibleColumnHeader;
                }
                set
                {
                    m_bVisibleColumnHeader = value;
                }
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public int SelectedSubItemIndex
            {
                get
                {
                    return m_iSelectedSubItemIndex;
                }
                set
                {
                    m_iSelectedSubItemIndex = value;
                }
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public int DisplaySubItemIndex
            {
                get
                {
                    return m_iDisplaySubItemIndex;
                }
                set
                {
                    m_iDisplaySubItemIndex = value;
                }
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool DroppedDownFlag
            {
                get
                {
                    return m_bDroppedDownFlag;
                }
                set
                {
                    m_bDroppedDownFlag = value;
                }
            }

            #endregion

            public virtual void RaiseSelectionchanged()
            {

            }
			
			protected override void WndProc(ref System.Windows.Forms.Message m)
			{
				
				if (m.Msg == System.Convert.ToInt32(MCEnums.Msgs.WM_MOUSEACTIVATE))
				{
                    m.Result = new IntPtr(System.Convert.ToInt32(MCEnums.MouseActivateFlags.MA_NOACTIVATE));
                    return;
				}

                base.WndProc(ref m);
				
			}
			
		}
		
	}
	
}
