using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace Miracom.UI
{
	namespace Controls
	{
		
        public class MCControlBase : System.Windows.Forms.UserControl, ISupportInitialize
		{
			
			protected bool m_bIniting = false;
            private Color m_BorderColor = Color.DarkGray;
            private Color m_BorderHotColor = Color.Black;

            private MCViewStyle m_ViewStyle = null;

        #region " Windows Form 디자이너에서 생성한 코드 "

            public MCControlBase()
			{
                InitializeComponent();

                m_ViewStyle = new MCViewStyle();
			
			}

            //UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
                    if (this.m_ViewStyle != null)
                    {
                        this.m_ViewStyle.Dispose();
                        this.m_ViewStyle = null;
                    }
                    
				}

				base.Dispose(disposing);
			}

            private void InitializeComponent()
            {
                this.SuspendLayout();
                // 
                // MCControlBase
                // 
                this.Name = "MCControlBase";
                this.Size = new System.Drawing.Size(150, 20);
                this.ResumeLayout(false);

            }
			
        #endregion
			
    		public void BeginInit()
			{
				
				m_bIniting = true;
				
			}
			
			public void EndInit()
			{
				
				m_bIniting = false;
				OnEndedInitialize();
				
			}
		
			public virtual void OnEndedInitialize()
			{
				
			}
			
		#region "Property Implements"
			
			#region "Property IsMouseInControl"
			
			[Description("Gets IsMouseInControl"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool IsMouseInControl
			{
				get
				{
					Point mPos = Control.MousePosition;
					bool retVal = this.ClientRectangle.Contains(this.PointToClient(mPos));
					return retVal;
				}
			}
			
			#endregion

            [Description("Gets or sets BorderColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public Color BorderColor
            {
                get
                {
                    return m_BorderColor;
                }
                set
                {
                    if (m_BorderColor.Equals(value) == false)
                    {
                        m_BorderColor = value;
                    }

                }
            }

            [Description("Gets or sets BorderHotColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public Color BorderHotColor
            {
                get
                {
                    return m_BorderHotColor;
                }
                set
                {
                    if (m_BorderHotColor.Equals(value) == false)
                    {
                        m_BorderHotColor = value;
                    }
                }
            }

            [Category("User Style"), Description("Gets controls active viewStyle"), TypeConverter(typeof(ExpandableObjectConverter)), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public MCViewStyle MCViewStyle
            {
                get
                {
                    return m_ViewStyle;
                }

            }
			
		#endregion
			
		}

        public class MCViewStyle
        {
            private Color m_BorderColor = Color.DarkGray;
            private Color m_BorderHotColor = Color.Black;

            public Color BorderColor
            {
                get
                {
                    return m_BorderColor;
                }
                set
                {
                    if (m_BorderColor.Equals(value) == false)
                    {
                        m_BorderColor = value;
                    }

                }
            }

            public Color BorderHotColor
            {
                get
                {
                    return m_BorderHotColor;
                }
                set
                {
                    if (m_BorderHotColor.Equals(value) == false)
                    {
                        m_BorderHotColor = value;
                    }
                }
            }


            internal void Dispose()
            {

                
            }
        }
		
	}
	
}
