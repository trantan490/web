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
using System.Windows.Forms;

namespace Miracom.SmartWeb.UI.Controls
{
	/// <summary>
    /// udcDateTimePicker에 대한 요약 설명입니다.
	/// </summary>
    /// 
	
	public enum DataTimePickerType { General, SpinedYM, FromSpinedYM, ToSpinedYM, FromDate, ToDate, YYYY, 기간일, 기간월};

	public class udcDateTimePicker : System.Windows.Forms.DateTimePicker
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		/// 

		private System.ComponentModel.Container components = null;

		//  DataTimePicker 의 타입
		private DataTimePickerType _type = DataTimePickerType.General;
		
		// 변경되기 이전의 값을 가지고 있음
		private System.DateTime previousValue;
		// 직접 숫자키를 친 경우 년도를 계산하는것을 무시하기 위하여
		private bool isHumonInput = false;

		#region
		
		public DataTimePickerType Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
				Type_Changed();
			}
		} 
		
		#endregion
        
		public udcDateTimePicker(System.ComponentModel.IContainer container)
		{
			///
			/// Windows.Forms 클래스 컴퍼지션 디자이너 지원에 필요합니다.
			///
			container.Add(this);
			InitializeComponent();

			//
			// TODO: InitializeComponent를 호출한 다음 생성자 코드를 추가합니다.
			//
			Type_Changed();
			this.MouseEnter +=new EventHandler(udcDateTimePicker_MouseEnter);

		}

		public udcDateTimePicker()
		{
			///
			/// Windows.Forms 클래스 컴퍼지션 디자이너 지원에 필요합니다.
			///
			InitializeComponent();
		
			//
			// TODO: InitializeComponent를 호출한 다음 생성자 코드를 추가합니다.
			//
			Type_Changed();
			this.MouseEnter +=new EventHandler(udcDateTimePicker_MouseEnter);
            
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


        public string SelectedValue()
        {
            return this.Value.ToString("yyyyMMdd");               
        }


      
        #region 구성 요소 디자이너에서 생성한 코드
		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
//			this.Format = System.Windows.Forms.DateTimePickerFormat.Custom;			
			this.Font = new System.Drawing.Font("Verdana", 8);
		}
		#endregion
                     
		private void Type_Changed()
		{
			if ( _type == DataTimePickerType.General )
			{
				this.CustomFormat = "yyyy-MM-dd";
                this.Format = DateTimePickerFormat.Custom;
				this.ShowUpDown = false; 
				this.Value = System.DateTime.Today;
			}
			else if ( _type == DataTimePickerType.FromDate )
			{
				this.CustomFormat = "yyyy-MM-dd";
                this.Format = DateTimePickerFormat.Custom;
				this.ShowUpDown = false; 
				this.Value = System.DateTime.Today;
			}
			else if ( _type == DataTimePickerType.ToDate )
			{
				this.CustomFormat = "yyyy-MM-dd";
                this.Format = DateTimePickerFormat.Custom; 
				this.ShowUpDown = false; 
				this.Value = System.DateTime.Today;
			}
			else if ( _type == DataTimePickerType.SpinedYM || _type == DataTimePickerType.FromSpinedYM || _type == DataTimePickerType.ToSpinedYM)
			{
				this.CustomFormat = "yyyy-MM";
                this.Format = DateTimePickerFormat.Custom;
				this.ShowUpDown = true; 

				// 말일이 될경우 버그가 발생함 그래서 매월 1 일로 자동 셋팅
				this.Value = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1);
				
				// 이전값 세팅
				previousValue = this.Value;
				this.ValueChanged +=new EventHandler(udcDateTimePicker_ValueChanged);
				this.KeyPress +=new System.Windows.Forms.KeyPressEventHandler(udcDateTimePicker_KeyPress);
				this.KeyUp +=new System.Windows.Forms.KeyEventHandler(udcDateTimePicker_KeyUp);
			}
			else if ( _type == DataTimePickerType.YYYY )
			{
				this.CustomFormat = "yyyy";
                this.Format = DateTimePickerFormat.Custom;
				this.ShowUpDown = true; 
				// 말일이 될경우 버그가 발생함 그래서 매월 1 일로 자동 셋팅
				this.Value = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1);
			}
			
			else if ( _type == DataTimePickerType.기간월 )
			{
				this.CustomFormat = "yyyy-MM-dd";
                this.Format = DateTimePickerFormat.Custom;                
				this.ShowUpDown = false; 
				this.Value = System.DateTime.Today;
			}
			
			else if ( _type == DataTimePickerType.기간일 )
			{
				this.CustomFormat = "yyyy-MM-dd";
                this.Format = DateTimePickerFormat.Custom;
				this.ShowUpDown = false; 
				this.Value = System.DateTime.Today;
			}


		}

		// SpinedYM 타입인 경우만 들어옴
		private void udcDateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			if ( isHumonInput == true )
				return;

            // 2010-01-06-임종우 AutoBinding 때에 하기 Logic 처리 하지 않음.
            if (CommonLib.sAutoBind == "Auto")
                return;

			// 12월에서 1월로 올라간 경우
			if ( previousValue.Month == 12 && this.Value.Month == 1 )
			{
				previousValue = this.Value;
				this.Value = new System.DateTime(Value.Year+1, Value.Month, 1);
			}
			
			// 1월에서 12월로 내려간 경우
			if ( previousValue.Month == 1 && this.Value.Month == 12 )
			{
				previousValue = this.Value;
				this.Value = new System.DateTime(Value.Year-1, Value.Month, 1);
			}
			
			// 이전값 세팅
			previousValue = this.Value;
		}

		private void udcDateTimePicker_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			isHumonInput = false;
		}

		private void udcDateTimePicker_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			isHumonInput = true;
		}

		private void udcDateTimePicker_MouseEnter(object sender, EventArgs e)
		{
 
		}
	}
}
