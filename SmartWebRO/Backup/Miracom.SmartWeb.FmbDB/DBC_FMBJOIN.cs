//-----------------------------------------------------------------------------
//
//   System      : MES
//   File Name   : DBC_MFMBENVDEF.cs
//   Description : MES Data Access Layer
//
//   DB Type     : Oracle
//
//   MES Version : 4.0.0
//
//   Function List
//       - New() : Creator for Object
//       - Select() : Select Table
//       -
//
//   Detail Description
//       - 
//       -
//
//   History
//       - 2008-05-29 : Created by DBLib Generator
//
//
//   Copyright(C) 1998-2007 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

//using
using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb
{
    public class DBC_FMBJOIN
        : ICloneable
    {


        #region " Fields Definition "

        // Fields
        private string m_factory;                                // VARCHAR(10)
        private string m_res_id;                                 // VARCHAR(20)
        private string m_res_desc;                               // VARCHAR(50)
        private string m_attached_flag;                          // VARCHAR(1)
        private string m_layout_id;                              // VARCHAR(20)
        private string m_updown_flag;                            // VARCHAR(1)
        
        private string m_group_id;                               // VARCHAR(20)
        private string m_res_tag_flag;                           // VARCHAR(1)
        private int m_seq;
        private int m_loc_x;
        private int m_loc_y;
        private int m_loc_width;
        private int m_loc_height;
        private string m_text;                                   // VARCHAR(40)
        private int m_text_size;
        private int m_text_color;
        private string m_text_style;                             // VARCHAR(1)
        private int m_tag_type;
        private int m_back_color;
        private string m_create_user_id;                         // VARCHAR(20)
        private string m_create_time;                            // VARCHAR(14)
        private string m_update_user_id;                         // VARCHAR(20)
        private string m_update_time;                            // VARCHAR(14)
        private string m_no_mouse_event;                         // VARCHAR(1)
        private string m_signal_flag;                            // VARCHAR(1)

        private string m_res_type;                               // VARCHAR(20)
        private string m_area_id;                                // VARCHAR(20)
        private string m_sub_area_id;                            // VARCHAR(20)
        private string m_res_location;                           // VARCHAR(20)
        private string m_proc_rule;                              // VARCHAR(1)
        private int m_max_proc_count;                            // INTEGER(3)
        private string m_res_up_down_flag;                       // VARCHAR(1)
        private string m_res_pri_sts;                            // VARCHAR(30)
        private string m_res_sts_1;                              // VARCHAR(30)
        private string m_res_sts_2;                              // VARCHAR(30)
        private string m_res_sts_3;                              // VARCHAR(30)
        private string m_res_sts_4;                              // VARCHAR(30)
        private string m_res_sts_5;                              // VARCHAR(30)
        private string m_res_sts_6;                              // VARCHAR(30)
        private string m_res_sts_7;                              // VARCHAR(30)
        private string m_res_sts_8;                              // VARCHAR(30)
        private string m_res_sts_9;                              // VARCHAR(30)
        private string m_res_sts_10;                             // VARCHAR(30)
        private string m_lot_id;                                 // VARCHAR(25)
        private string m_sublot_id;                              // VARCHAR(30)
        private string m_crr_id;                                 // VARCHAR(20)
        private string m_res_ctrl_mode;                          // VARCHAR(2)
        private string m_res_proc_mode;                          // VARCHAR(10)
        private string m_last_recipe_id;                         // VARCHAR(30)
        private int m_proc_count;                                // INTEGER(3)
        private string m_last_start_time;                        // VARCHAR(14)
        private string m_last_end_time;                          // VARCHAR(14)
        private string m_last_down_time;                         // VARCHAR(14)
        private int m_last_down_hist_seq;                        // INTEGER(10)
        private string m_last_event_id;                          // VARCHAR(12)
        private string m_last_event_time;                        // VARCHAR(14)
        private int m_last_active_hist_seq;                      // INTEGER(10)
        private int m_last_hist_seq;                             // INTEGER(10)

        #endregion

        #region " Variable Definition "
        DB_Common _dbc;      //DB Connection
        // TODO : Declare Variable for Query Condition

        #endregion

        #region " Property Definition "

        /// <summary>
        /// Gets or sets the 'FACTORY' value.
        /// </summary>
        public string FACTORY
        {
            get
            {
                if (m_factory == null)
                {
                    m_factory = " ";
                }
                return m_factory;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_factory = " ";
                }
                m_factory = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_ID' value.
        /// </summary>
        public string RES_ID
        {
            get
            {
                if (m_res_id == null)
                {
                    m_res_id = " ";
                }
                return m_res_id;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_id = " ";
                }
                m_res_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_DESC' value.
        /// </summary>
        public string RES_DESC
        {
            get
            {
                if (m_res_desc == null)
                {
                    m_res_desc = " ";
                }
                return m_res_desc;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_desc = " ";
                }
                m_res_desc = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'ATTACHED_FLAG' value.
        /// </summary>
        public string ATTACHED_FLAG
        {
            get
            {
                if (m_attached_flag == null)
                {
                    m_attached_flag = " ";
                }
                return m_attached_flag;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_attached_flag = " ";
                }
                m_attached_flag = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_TYPE' value.
        /// </summary>
        public string RES_TYPE
        {
            get
            {
                if (m_res_type == null)
                {
                    m_res_type = " ";
                }
                return m_res_type;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_type = " ";
                }
                m_res_type = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LAYOUT_ID' value.
        /// </summary>
        public string LAYOUT_ID
        {
            get
            {
                if (m_layout_id == null)
                {
                    m_layout_id = " ";
                }
                return m_layout_id;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_layout_id = " ";
                }
                m_layout_id = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the 'UPDOWN_FLAG' value.
        /// </summary>
        public string UPDOWN_FLAG
        {
            get
            {
                if (m_updown_flag == null)
                {
                    m_updown_flag = " ";
                }
                return m_updown_flag;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_updown_flag = " ";
                }
                m_updown_flag = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'SEQ' value.
        /// </summary>
        public int SEQ
        {
            get
            {
                return m_seq;
            }
            set
            {
                m_seq = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LOC_X' value.
        /// </summary>
        public int LOC_X
        {
            get
            {
                return m_loc_x;
            }
            set
            {
                m_loc_x = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LOC_Y' value.
        /// </summary>
        public int LOC_Y
        {
            get
            {
                return m_loc_y;
            }
            set
            {
                m_loc_y = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LOC_WIDTH' value.
        /// </summary>
        public int LOC_WIDTH
        {
            get
            {
                return m_loc_width;
            }
            set
            {
                m_loc_width = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LOC_HEIGHT' value.
        /// </summary>
        public int LOC_HEIGHT
        {
            get
            {
                return m_loc_height;
            }
            set
            {
                m_loc_height = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'TEXT' value.
        /// </summary>
        public string TEXT
        {
            get
            {
                if (m_text == null)
                {
                    m_text = " ";
                }
                return m_text;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_text = " ";
                }
                m_text = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'TEXT_SIZE' value.
        /// </summary>
        public int TEXT_SIZE
        {
            get
            {
                return m_text_size;
            }
            set
            {
                m_text_size = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'TEXT_COLOR' value.
        /// </summary>
        public int TEXT_COLOR
        {
            get
            {
                return m_text_color;
            }
            set
            {
                m_text_color = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'TEXT_STYLE' value.
        /// </summary>
        public string TEXT_STYLE
        {
            get
            {
                if (m_text_style == null)
                {
                    m_text_style = " ";
                }
                return m_text_style;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_text_style = " ";
                }
                m_text_style = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'TAG_TYPE' value.
        /// </summary>
        public int TAG_TYPE
        {
            get
            {
                return m_tag_type;
            }
            set
            {
                m_tag_type = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'BACK_COLOR' value.
        /// </summary>
        public int BACK_COLOR
        {
            get
            {
                return m_back_color;
            }
            set
            {
                m_back_color = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'CREATE_USER_ID' value.
        /// </summary>
        public string CREATE_USER_ID
        {
            get
            {
                if (m_create_user_id == null)
                {
                    m_create_user_id = " ";
                }
                return m_create_user_id;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_create_user_id = " ";
                }
                m_create_user_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'CREATE_TIME' value.
        /// </summary>
        public string CREATE_TIME
        {
            get
            {
                if (m_create_time == null)
                {
                    m_create_time = " ";
                }
                return m_create_time;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_create_time = " ";
                }
                m_create_time = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'UPDATE_USER_ID' value.
        /// </summary>
        public string UPDATE_USER_ID
        {
            get
            {
                if (m_update_user_id == null)
                {
                    m_update_user_id = " ";
                }
                return m_update_user_id;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_update_user_id = " ";
                }
                m_update_user_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'UPDATE_TIME' value.
        /// </summary>
        public string UPDATE_TIME
        {
            get
            {
                if (m_update_time == null)
                {
                    m_update_time = " ";
                }
                return m_update_time;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_update_time = " ";
                }
                m_update_time = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'NO_MOUSE_EVENT' value.
        /// </summary>
        public string NO_MOUSE_EVENT
        {
            get
            {
                if (m_no_mouse_event == null)
                {
                    m_no_mouse_event = " ";
                }
                return m_no_mouse_event;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_no_mouse_event = " ";
                }
                m_no_mouse_event = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'SIGNAL_FLAG' value.
        /// </summary>
        public string SIGNAL_FLAG
        {
            get
            {
                if (m_signal_flag == null)
                {
                    m_signal_flag = " ";
                }
                return m_signal_flag;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_signal_flag = " ";
                }
                m_signal_flag = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'AREA_ID' value.
        /// </summary>
        public string AREA_ID
        {
            get
            {
                if (m_area_id == null)
                {
                    m_area_id = " ";
                }
                return m_area_id;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_area_id = " ";
                }
                m_area_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'SUB_AREA_ID' value.
        /// </summary>
        public string SUB_AREA_ID
        {
            get
            {
                if (m_sub_area_id == null)
                {
                    m_sub_area_id = " ";
                }
                return m_sub_area_id;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_sub_area_id = " ";
                }
                m_sub_area_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_LOCATION' value.
        /// </summary>
        public string RES_LOCATION
        {
            get
            {
                if (m_res_location == null)
                {
                    m_res_location = " ";
                }
                return m_res_location;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_location = " ";
                }
                m_res_location = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'PROC_RULE' value.
        /// </summary>
        public string PROC_RULE
        {
            get
            {
                if (m_proc_rule == null)
                {
                    m_proc_rule = " ";
                }
                return m_proc_rule;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_proc_rule = " ";
                }
                m_proc_rule = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'MAX_PROC_COUNT' value.
        /// </summary>
        public int MAX_PROC_COUNT
        {
            get
            {
                return m_max_proc_count;
            }
            set
            {
                m_max_proc_count = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_UP_DOWN_FLAG' value.
        /// </summary>
        public string RES_UP_DOWN_FLAG
        {
            get
            {
                if (m_res_up_down_flag == null)
                {
                    m_res_up_down_flag = " ";
                }
                return m_res_up_down_flag;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_up_down_flag = " ";
                }
                m_res_up_down_flag = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_PRI_STS' value.
        /// </summary>
        public string RES_PRI_STS
        {
            get
            {
                if (m_res_pri_sts == null)
                {
                    m_res_pri_sts = " ";
                }
                return m_res_pri_sts;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_pri_sts = " ";
                }
                m_res_pri_sts = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_STS_1' value.
        /// </summary>
        public string RES_STS_1
        {
            get
            {
                if (m_res_sts_1 == null)
                {
                    m_res_sts_1 = " ";
                }
                return m_res_sts_1;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_sts_1 = " ";
                }
                m_res_sts_1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_STS_2' value.
        /// </summary>
        public string RES_STS_2
        {
            get
            {
                if (m_res_sts_2 == null)
                {
                    m_res_sts_2 = " ";
                }
                return m_res_sts_2;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_sts_2 = " ";
                }
                m_res_sts_2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_STS_3' value.
        /// </summary>
        public string RES_STS_3
        {
            get
            {
                if (m_res_sts_3 == null)
                {
                    m_res_sts_3 = " ";
                }
                return m_res_sts_3;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_sts_3 = " ";
                }
                m_res_sts_3 = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_STS_4' value.
        /// </summary>
        public string RES_STS_4
        {
            get
            {
                if (m_res_sts_4 == null)
                {
                    m_res_sts_4 = " ";
                }
                return m_res_sts_4;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_sts_4 = " ";
                }
                m_res_sts_4 = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_STS_5' value.
        /// </summary>
        public string RES_STS_5
        {
            get
            {
                if (m_res_sts_5 == null)
                {
                    m_res_sts_5 = " ";
                }
                return m_res_sts_5;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_sts_5 = " ";
                }
                m_res_sts_5 = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_STS_6' value.
        /// </summary>
        public string RES_STS_6
        {
            get
            {
                if (m_res_sts_6 == null)
                {
                    m_res_sts_6 = " ";
                }
                return m_res_sts_6;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_sts_6 = " ";
                }
                m_res_sts_6 = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_STS_7' value.
        /// </summary>
        public string RES_STS_7
        {
            get
            {
                if (m_res_sts_7 == null)
                {
                    m_res_sts_7 = " ";
                }
                return m_res_sts_7;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_sts_7 = " ";
                }
                m_res_sts_7 = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_STS_8' value.
        /// </summary>
        public string RES_STS_8
        {
            get
            {
                if (m_res_sts_8 == null)
                {
                    m_res_sts_8 = " ";
                }
                return m_res_sts_8;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_sts_8 = " ";
                }
                m_res_sts_8 = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_STS_9' value.
        /// </summary>
        public string RES_STS_9
        {
            get
            {
                if (m_res_sts_9 == null)
                {
                    m_res_sts_9 = " ";
                }
                return m_res_sts_9;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_sts_9 = " ";
                }
                m_res_sts_9 = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_STS_10' value.
        /// </summary>
        public string RES_STS_10
        {
            get
            {
                if (m_res_sts_10 == null)
                {
                    m_res_sts_10 = " ";
                }
                return m_res_sts_10;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_sts_10 = " ";
                }
                m_res_sts_10 = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LOT_ID' value.
        /// </summary>
        public string LOT_ID
        {
            get
            {
                if (m_lot_id == null)
                {
                    m_lot_id = " ";
                }
                return m_lot_id;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_lot_id = " ";
                }
                m_lot_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'SUBLOT_ID' value.
        /// </summary>
        public string SUBLOT_ID
        {
            get
            {
                if (m_sublot_id == null)
                {
                    m_sublot_id = " ";
                }
                return m_sublot_id;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_sublot_id = " ";
                }
                m_sublot_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'CRR_ID' value.
        /// </summary>
        public string CRR_ID
        {
            get
            {
                if (m_crr_id == null)
                {
                    m_crr_id = " ";
                }
                return m_crr_id;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_crr_id = " ";
                }
                m_crr_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_CTRL_MODE' value.
        /// </summary>
        public string RES_CTRL_MODE
        {
            get
            {
                if (m_res_ctrl_mode == null)
                {
                    m_res_ctrl_mode = " ";
                }
                return m_res_ctrl_mode;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_ctrl_mode = " ";
                }
                m_res_ctrl_mode = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_PROC_MODE' value.
        /// </summary>
        public string RES_PROC_MODE
        {
            get
            {
                if (m_res_proc_mode == null)
                {
                    m_res_proc_mode = " ";
                }
                return m_res_proc_mode;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_res_proc_mode = " ";
                }
                m_res_proc_mode = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LAST_RECIPE_ID' value.
        /// </summary>
        public string LAST_RECIPE_ID
        {
            get
            {
                if (m_last_recipe_id == null)
                {
                    m_last_recipe_id = " ";
                }
                return m_last_recipe_id;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_last_recipe_id = " ";
                }
                m_last_recipe_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'PROC_COUNT' value.
        /// </summary>
        public int PROC_COUNT
        {
            get
            {
                return m_proc_count;
            }
            set
            {
                m_proc_count = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LAST_START_TIME' value.
        /// </summary>
        public string LAST_START_TIME
        {
            get
            {
                if (m_last_start_time == null)
                {
                    m_last_start_time = " ";
                }
                return m_last_start_time;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_last_start_time = " ";
                }
                m_last_start_time = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LAST_END_TIME' value.
        /// </summary>
        public string LAST_END_TIME
        {
            get
            {
                if (m_last_end_time == null)
                {
                    m_last_end_time = " ";
                }
                return m_last_end_time;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_last_end_time = " ";
                }
                m_last_end_time = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LAST_DOWN_TIME' value.
        /// </summary>
        public string LAST_DOWN_TIME
        {
            get
            {
                if (m_last_down_time == null)
                {
                    m_last_down_time = " ";
                }
                return m_last_down_time;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_last_down_time = " ";
                }
                m_last_down_time = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LAST_DOWN_HIST_SEQ' value.
        /// </summary>
        public int LAST_DOWN_HIST_SEQ
        {
            get
            {
                return m_last_down_hist_seq;
            }
            set
            {
                m_last_down_hist_seq = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LAST_EVENT_ID' value.
        /// </summary>
        public string LAST_EVENT_ID
        {
            get
            {
                if (m_last_event_id == null)
                {
                    m_last_event_id = " ";
                }
                return m_last_event_id;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_last_event_id = " ";
                }
                m_last_event_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LAST_EVENT_TIME' value.
        /// </summary>
        public string LAST_EVENT_TIME
        {
            get
            {
                if (m_last_event_time == null)
                {
                    m_last_event_time = " ";
                }
                return m_last_event_time;
            }
            set
            {
                if (value == null || value == "")
                {
                    m_last_event_time = " ";
                }
                m_last_event_time = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LAST_ACTIVE_HIST_SEQ' value.
        /// </summary>
        public int LAST_ACTIVE_HIST_SEQ
        {
            get
            {
                return m_last_active_hist_seq;
            }
            set
            {
                m_last_active_hist_seq = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'LAST_HIST_SEQ' value.
        /// </summary>
        public int LAST_HIST_SEQ
        {
            get
            {
                return m_last_hist_seq;
            }
            set
            {
                m_last_hist_seq = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'GROUP_ID' value.
        /// </summary>
        public string GROUP_ID
        {
            get
            {
                return m_group_id;
            }
            set
            {
                m_group_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the 'RES_TAG_FLAG' value.
        /// </summary>
        public string RES_TAG_FLAG
        {
            get
            {
                return m_res_tag_flag;
            }
            set
            {
                m_res_tag_flag = value;
            }
        }

        
        #endregion

        #region " Function Definition "

        /// <summary>
        /// Creator for Object
        /// <summary>
        public DBC_FMBJOIN(ref DB_Common dbc)
        {
            this._dbc = dbc;
            Init();
        }

        /// <summary>
        /// Initialization
        /// <summary>
        public bool Init()
        {
            FACTORY = " ";
            RES_ID = " ";
            RES_DESC = " ";
            ATTACHED_FLAG = " ";
            LAYOUT_ID = " ";
            UPDOWN_FLAG = " ";
            GROUP_ID = " ";
            RES_TAG_FLAG = " ";
            SEQ = 0;
            LOC_X = 0;
            LOC_Y = 0;
            LOC_WIDTH = 0;
            LOC_HEIGHT = 0;
            TEXT = " ";
            TEXT_SIZE = 0;
            TEXT_COLOR = 0;
            TEXT_STYLE = " ";
            TAG_TYPE = 0;
            BACK_COLOR = 0;
            CREATE_USER_ID = " ";
            CREATE_TIME = " ";
            UPDATE_USER_ID = " ";
            UPDATE_TIME = " ";
            NO_MOUSE_EVENT = " ";
            SIGNAL_FLAG = " ";
            RES_TYPE = " ";
            AREA_ID = " ";
            SUB_AREA_ID = " ";
            RES_LOCATION = " ";
            PROC_RULE = " ";
            MAX_PROC_COUNT = 0;
            RES_UP_DOWN_FLAG = " ";
            RES_PRI_STS = " ";
            RES_STS_1 = " ";
            RES_STS_2 = " ";
            RES_STS_3 = " ";
            RES_STS_4 = " ";
            RES_STS_5 = " ";
            RES_STS_6 = " ";
            RES_STS_7 = " ";
            RES_STS_8 = " ";
            RES_STS_9 = " ";
            RES_STS_10 = " ";
            LOT_ID = " ";
            SUBLOT_ID = " ";
            CRR_ID = " ";
            RES_CTRL_MODE = " ";
            RES_PROC_MODE = " ";
            LAST_RECIPE_ID = " ";
            PROC_COUNT = 0;
            LAST_START_TIME = " ";
            LAST_END_TIME = " ";
            LAST_DOWN_TIME = " ";
            LAST_DOWN_HIST_SEQ = 0;
            LAST_EVENT_ID = " ";
            LAST_EVENT_TIME = " ";
            LAST_ACTIVE_HIST_SEQ = 0;
            LAST_HIST_SEQ = 0;

            return true;
        }

        /// <summary>
        /// Clone object
        /// </summary>
        /// <returns>object</returns>
        public object Clone()
        {
            DBC_FMBJOIN FMBJOIN = null;
            try
            {
                FMBJOIN = new DBC_FMBJOIN(ref _dbc);

                FMBJOIN.FACTORY = this.FACTORY;
                FMBJOIN.RES_ID = this.RES_ID;
                FMBJOIN.RES_DESC = this.RES_DESC;
                FMBJOIN.ATTACHED_FLAG = this.ATTACHED_FLAG;
                FMBJOIN.LAYOUT_ID = this.LAYOUT_ID;
                FMBJOIN.UPDOWN_FLAG = this.UPDOWN_FLAG;
                FMBJOIN.GROUP_ID = this.GROUP_ID;
                FMBJOIN.RES_TAG_FLAG = this.RES_TAG_FLAG;
                FMBJOIN.SEQ = this.SEQ;
                FMBJOIN.LOC_X = this.LOC_X;
                FMBJOIN.LOC_Y = this.LOC_Y;
                FMBJOIN.LOC_WIDTH = this.LOC_WIDTH;
                FMBJOIN.LOC_HEIGHT = this.LOC_HEIGHT;
                FMBJOIN.TEXT = this.TEXT;
                FMBJOIN.TEXT_SIZE = this.TEXT_SIZE;
                FMBJOIN.TEXT_COLOR = this.TEXT_COLOR;
                FMBJOIN.TEXT_STYLE = this.TEXT_STYLE;
                FMBJOIN.TAG_TYPE = this.TAG_TYPE;
                FMBJOIN.BACK_COLOR = this.BACK_COLOR;
                FMBJOIN.CREATE_USER_ID = this.CREATE_USER_ID;
                FMBJOIN.CREATE_TIME = this.CREATE_TIME;
                FMBJOIN.UPDATE_USER_ID = this.UPDATE_USER_ID;
                FMBJOIN.UPDATE_TIME = this.UPDATE_TIME;
                FMBJOIN.NO_MOUSE_EVENT = this.NO_MOUSE_EVENT;
                FMBJOIN.SIGNAL_FLAG = this.SIGNAL_FLAG;
                FMBJOIN.RES_TYPE = this.RES_TYPE;
                FMBJOIN.AREA_ID = this.AREA_ID;
                FMBJOIN.SUB_AREA_ID = this.SUB_AREA_ID;
                FMBJOIN.RES_LOCATION = this.RES_LOCATION;
                FMBJOIN.PROC_RULE = this.PROC_RULE;
                FMBJOIN.MAX_PROC_COUNT = this.MAX_PROC_COUNT;
                FMBJOIN.RES_UP_DOWN_FLAG = this.RES_UP_DOWN_FLAG;
                FMBJOIN.RES_PRI_STS = this.RES_PRI_STS;
                FMBJOIN.RES_STS_1 = this.RES_STS_1;
                FMBJOIN.RES_STS_2 = this.RES_STS_2;
                FMBJOIN.RES_STS_3 = this.RES_STS_3;
                FMBJOIN.RES_STS_4 = this.RES_STS_4;
                FMBJOIN.RES_STS_5 = this.RES_STS_5;
                FMBJOIN.RES_STS_6 = this.RES_STS_6;
                FMBJOIN.RES_STS_7 = this.RES_STS_7;
                FMBJOIN.RES_STS_8 = this.RES_STS_8;
                FMBJOIN.RES_STS_9 = this.RES_STS_9;
                FMBJOIN.RES_STS_10 = this.RES_STS_10;
                FMBJOIN.LOT_ID = this.LOT_ID;
                FMBJOIN.SUBLOT_ID = this.SUBLOT_ID;
                FMBJOIN.CRR_ID = this.CRR_ID;
                FMBJOIN.RES_CTRL_MODE = this.RES_CTRL_MODE;
                FMBJOIN.RES_PROC_MODE = this.RES_PROC_MODE;
                FMBJOIN.LAST_RECIPE_ID = this.LAST_RECIPE_ID;
                FMBJOIN.PROC_COUNT = this.PROC_COUNT;
                FMBJOIN.LAST_START_TIME = this.LAST_START_TIME;
                FMBJOIN.LAST_END_TIME = this.LAST_END_TIME;
                FMBJOIN.LAST_DOWN_TIME = this.LAST_DOWN_TIME;
                FMBJOIN.LAST_DOWN_HIST_SEQ = this.LAST_DOWN_HIST_SEQ;
                FMBJOIN.LAST_EVENT_ID = this.LAST_EVENT_ID;
                FMBJOIN.LAST_EVENT_TIME = this.LAST_EVENT_TIME;
                FMBJOIN.LAST_ACTIVE_HIST_SEQ = this.LAST_ACTIVE_HIST_SEQ;
                FMBJOIN.LAST_HIST_SEQ = this.LAST_HIST_SEQ;
                
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                    _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
                else
                    _dbc.gErrors.SetErrors(ex);

                return null;
            }

            return FMBJOIN;
        }

        /// <summary>
        /// Copy Column Data
        /// </summary>
        /// <param name="Row">One Record of AdoDataTable</param>
        /// <returns>true or false</returns>
        public bool CopyDataRow(DataRow Row)
        {
            try
            {
                Init();

                FACTORY = (string)(Row["FACTORY"]);
                RES_ID = (string)(Row["RES_ID"]);
                RES_DESC = (string)(Row["RES_DESC"]);
                ATTACHED_FLAG = (string)(Row["ATTACHED_FLAG"]);
                LAYOUT_ID = (string)(Row["LAYOUT_ID"]);
                UPDOWN_FLAG = (string)(Row["UPDOWN_FLAG"]);
                GROUP_ID = (string)(Row["GROUP_ID"]);
                RES_TAG_FLAG = (string)(Row["RES_TAG_FLAG"]);
                SEQ = Convert.ToInt32(Row["SEQ"]);
                LOC_X = Convert.ToInt32(Row["LOC_X"]);
                LOC_Y = Convert.ToInt32(Row["LOC_Y"]);
                LOC_WIDTH = Convert.ToInt32(Row["LOC_WIDTH"]);
                LOC_HEIGHT = Convert.ToInt32(Row["LOC_HEIGHT"]);
                TEXT = (string)(Row["TEXT"]);
                TEXT_SIZE = Convert.ToInt32(Row["TEXT_SIZE"]);
                TEXT_COLOR = Convert.ToInt32(Row["TEXT_COLOR"]);
                TEXT_STYLE = (string)(Row["TEXT_STYLE"]);
                TAG_TYPE = Convert.ToInt32(Row["TAG_TYPE"]);
                BACK_COLOR = Convert.ToInt32(Row["BACK_COLOR"]);
                CREATE_USER_ID = (string)(Row["CREATE_USER_ID"]);
                CREATE_TIME = (string)(Row["CREATE_TIME"]);
                UPDATE_USER_ID = (string)(Row["UPDATE_USER_ID"]);
                UPDATE_TIME = (string)(Row["UPDATE_TIME"]);
                NO_MOUSE_EVENT = (string)(Row["NO_MOUSE_EVENT"]);
                SIGNAL_FLAG = (string)(Row["SIGNAL_FLAG"]);
                RES_TYPE = (string)(Row["RES_TYPE"]);
                AREA_ID = (string)(Row["AREA_ID"]);
                SUB_AREA_ID = (string)(Row["SUB_AREA_ID"]);
                RES_LOCATION = (string)(Row["RES_LOCATION"]);
                PROC_RULE = (string)(Row["PROC_RULE"]);
                MAX_PROC_COUNT = Convert.ToInt32(Row["MAX_PROC_COUNT"]);
                RES_UP_DOWN_FLAG = (string)(Row["RES_UP_DOWN_FLAG"]);
                RES_PRI_STS = (string)(Row["RES_PRI_STS"]);
                RES_STS_1 = (string)(Row["RES_STS_1"]);
                RES_STS_2 = (string)(Row["RES_STS_2"]);
                RES_STS_3 = (string)(Row["RES_STS_3"]);
                RES_STS_4 = (string)(Row["RES_STS_4"]);
                RES_STS_5 = (string)(Row["RES_STS_5"]);
                RES_STS_6 = (string)(Row["RES_STS_6"]);
                RES_STS_7 = (string)(Row["RES_STS_7"]);
                RES_STS_8 = (string)(Row["RES_STS_8"]);
                RES_STS_9 = (string)(Row["RES_STS_9"]);
                RES_STS_10 = (string)(Row["RES_STS_10"]);
                LOT_ID = (string)(Row["LOT_ID"]);
                SUBLOT_ID = (string)(Row["SUBLOT_ID"]);
                CRR_ID = (string)(Row["CRR_ID"]);
                RES_CTRL_MODE = (string)(Row["RES_CTRL_MODE"]);
                RES_PROC_MODE = (string)(Row["RES_PROC_MODE"]);
                LAST_RECIPE_ID = (string)(Row["LAST_RECIPE_ID"]);
                PROC_COUNT = Convert.ToInt32(Row["PROC_COUNT"]);
                LAST_START_TIME = (string)(Row["LAST_START_TIME"]);
                LAST_END_TIME = (string)(Row["LAST_END_TIME"]);
                LAST_DOWN_TIME = (string)(Row["LAST_DOWN_TIME"]);
                LAST_DOWN_HIST_SEQ = Convert.ToInt32(Row["LAST_DOWN_HIST_SEQ"]);
                LAST_EVENT_ID = (string)(Row["LAST_EVENT_ID"]);
                LAST_EVENT_TIME = (string)(Row["LAST_EVENT_TIME"]);
                LAST_ACTIVE_HIST_SEQ = Convert.ToInt32(Row["LAST_ACTIVE_HIST_SEQ"]);
                LAST_HIST_SEQ = Convert.ToInt32(Row["LAST_HIST_SEQ"]);
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                    _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
                else
                    _dbc.gErrors.SetErrors(ex);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Select Data
        /// </summary>
        /// <param name="_step">Query Selector</param>
        /// <param name="adoDataTable">Output DataTable</param>
        /// <returns>true or false</returns>
        public bool SelectData(int _step, ref DataTable adoDataTable)
        {
            int i = 0;
            string strQuery = "";

            try
            {
                if (_dbc.gErrors.MsgInit() == false)
                    return false;

                OleDbDataAdapter adoAdapter = new OleDbDataAdapter();
                adoAdapter.SelectCommand = new OleDbCommand();

                // Set Connection, Transaction
                adoAdapter.SelectCommand.Connection = _dbc.gOleDbConnection;
                adoAdapter.SelectCommand.Transaction = _dbc.gOleDbTransaction;
                adoDataTable = new DataTable();

                switch (_step)
                {
                    case 1:
                        // Select by Primary Key
                        strQuery = "SELECT * FROM MFMBENVDEF"
                            + " WHERE FACTORY=?";

                        // Add Parameters
                        adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                        break;

                    case 101:
                        // TODO : User Select Query
                        break;

                    default:
                        // Error Handling
                        _dbc.gErrors.SqlCode = SQL_CODE.SQL_CASE_ERROR;
                        return false;
                }

                // Fill()
                adoAdapter.SelectCommand.CommandText = strQuery;
                _dbc.gErrors.AddQuery(strQuery, adoAdapter.SelectCommand.Parameters);
                adoAdapter.Fill(adoDataTable);

                // Check Data Not Found
                if (adoDataTable.Rows.Count < 1)
                {
                    _dbc.gErrors.SqlCode = SQL_CODE.SQL_NOT_FOUND;
                    return false;
                }

                // Set Fields
                for (i = 0; i <= adoDataTable.Columns.Count - 1; i++)
                {
                    switch (adoDataTable.Columns[i].ColumnName)
                    {
                        case "FACTORY":
                            FACTORY = (string)(adoDataTable.Rows[0]["FACTORY"]);
                            break;
                        case "RES_ID":
                            RES_ID = (string)(adoDataTable.Rows[0]["RES_ID"]);
                            break;
                        case "RES_DESC":
                            RES_DESC = (string)(adoDataTable.Rows[0]["RES_DESC"]);
                            break;
                        case "ATTACHED_FLAG":
                            ATTACHED_FLAG = (string)(adoDataTable.Rows[0]["ATTACHED_FLAG"]);
                            break;
                        case "LAYOUT_ID":
                            LAYOUT_ID = (string)(adoDataTable.Rows[0]["LAYOUT_ID"]);
                            break;
                        case "UPDOWN_FLAG":
                            UPDOWN_FLAG = (string)(adoDataTable.Rows[0]["UPDOWN_FLAG"]);
                            break;
                        case "GROUP_ID":
                            GROUP_ID = (string)(adoDataTable.Rows[0]["GROUP_ID"]);
                            break;
                        case "RES_TAG_FLAG":
                            RES_TAG_FLAG = (string)(adoDataTable.Rows[0]["RES_TAG_FLAG"]);
                            break;
                        case "SEQ":
                            SEQ = Convert.ToInt32(adoDataTable.Rows[0]["SEQ"]);
                            break;
                        case "LOC_X":
                            LOC_X = Convert.ToInt32(adoDataTable.Rows[0]["LOC_X"]);
                            break;
                        case "LOC_Y":
                            LOC_Y = Convert.ToInt32(adoDataTable.Rows[0]["LOC_Y"]);
                            break;
                        case "LOC_WIDTH":
                            LOC_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["LOC_WIDTH"]);
                            break;
                        case "LOC_HEIGHT":
                            LOC_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["LOC_HEIGHT"]);
                            break;
                        case "TEXT":
                            TEXT = (string)(adoDataTable.Rows[0]["TEXT"]);
                            break;
                        case "TEXT_SIZE":
                            TEXT_SIZE = Convert.ToInt32(adoDataTable.Rows[0]["TEXT_SIZE"]);
                            break;
                        case "TEXT_COLOR":
                            TEXT_COLOR = Convert.ToInt32(adoDataTable.Rows[0]["TEXT_COLOR"]);
                            break;
                        case "TEXT_STYLE":
                            TEXT_STYLE = (string)(adoDataTable.Rows[0]["TEXT_STYLE"]);
                            break;
                        case "TAG_TYPE":
                            TAG_TYPE = Convert.ToInt32(adoDataTable.Rows[0]["TAG_TYPE"]);
                            break;
                        case "BACK_COLOR":
                            BACK_COLOR = Convert.ToInt32(adoDataTable.Rows[0]["BACK_COLOR"]);
                            break;
                        case "CREATE_USER_ID":
                            CREATE_USER_ID = (string)(adoDataTable.Rows[0]["CREATE_USER_ID"]);
                            break;
                        case "CREATE_TIME":
                            CREATE_TIME = (string)(adoDataTable.Rows[0]["CREATE_TIME"]);
                            break;
                        case "UPDATE_USER_ID":
                            UPDATE_USER_ID = (string)(adoDataTable.Rows[0]["UPDATE_USER_ID"]);
                            break;
                        case "UPDATE_TIME":
                            UPDATE_TIME = (string)(adoDataTable.Rows[0]["UPDATE_TIME"]);
                            break;
                        case "NO_MOUSE_EVENT":
                            NO_MOUSE_EVENT = (string)(adoDataTable.Rows[0]["NO_MOUSE_EVENT"]);
                            break;
                        case "SIGNAL_FLAG":
                            SIGNAL_FLAG = (string)(adoDataTable.Rows[0]["SIGNAL_FLAG"]);
                            break;
                        case "RES_TYPE":
                            RES_TYPE = (string)(adoDataTable.Rows[0]["RES_TYPE"]);
                            break;
                        case "AREA_ID":
                            AREA_ID = (string)(adoDataTable.Rows[0]["AREA_ID"]);
                            break;
                        case "SUB_AREA_ID":
                            SUB_AREA_ID = (string)(adoDataTable.Rows[0]["SUB_AREA_ID"]);
                            break;
                        case "RES_LOCATION":
                            RES_LOCATION = (string)(adoDataTable.Rows[0]["RES_LOCATION"]);
                            break;
                        case "PROC_RULE":
                            PROC_RULE = (string)(adoDataTable.Rows[0]["PROC_RULE"]);
                            break;
                        case "MAX_PROC_COUNT":
                            MAX_PROC_COUNT = Convert.ToInt32(adoDataTable.Rows[0]["MAX_PROC_COUNT"]);
                            break;
                        case "RES_UP_DOWN_FLAG":
                            RES_UP_DOWN_FLAG = (string)(adoDataTable.Rows[0]["RES_UP_DOWN_FLAG"]);
                            break;
                        case "RES_PRI_STS":
                            RES_PRI_STS = (string)(adoDataTable.Rows[0]["RES_PRI_STS"]);
                            break;
                        case "RES_STS_1":
                            RES_STS_1 = (string)(adoDataTable.Rows[0]["RES_STS_1"]);
                            break;
                        case "RES_STS_2":
                            RES_STS_2 = (string)(adoDataTable.Rows[0]["RES_STS_2"]);
                            break;
                        case "RES_STS_3":
                            RES_STS_3 = (string)(adoDataTable.Rows[0]["RES_STS_3"]);
                            break;
                        case "RES_STS_4":
                            RES_STS_4 = (string)(adoDataTable.Rows[0]["RES_STS_4"]);
                            break;
                        case "RES_STS_5":
                            RES_STS_5 = (string)(adoDataTable.Rows[0]["RES_STS_5"]);
                            break;
                        case "RES_STS_6":
                            RES_STS_6 = (string)(adoDataTable.Rows[0]["RES_STS_6"]);
                            break;
                        case "RES_STS_7":
                            RES_STS_7 = (string)(adoDataTable.Rows[0]["RES_STS_7"]);
                            break;
                        case "RES_STS_8":
                            RES_STS_8 = (string)(adoDataTable.Rows[0]["RES_STS_8"]);
                            break;
                        case "RES_STS_9":
                            RES_STS_9 = (string)(adoDataTable.Rows[0]["RES_STS_9"]);
                            break;
                        case "RES_STS_10":
                            RES_STS_10 = (string)(adoDataTable.Rows[0]["RES_STS_10"]);
                            break;
                        case "LOT_ID":
                            LOT_ID = (string)(adoDataTable.Rows[0]["LOT_ID"]);
                            break;
                        case "SUBLOT_ID":
                            SUBLOT_ID = (string)(adoDataTable.Rows[0]["SUBLOT_ID"]);
                            break;
                        case "CRR_ID":
                            CRR_ID = (string)(adoDataTable.Rows[0]["CRR_ID"]);
                            break;
                        case "RES_CTRL_MODE":
                            RES_CTRL_MODE = (string)(adoDataTable.Rows[0]["RES_CTRL_MODE"]);
                            break;
                        case "RES_PROC_MODE":
                            RES_PROC_MODE = (string)(adoDataTable.Rows[0]["RES_PROC_MODE"]);
                            break;
                        case "LAST_RECIPE_ID":
                            LAST_RECIPE_ID = (string)(adoDataTable.Rows[0]["LAST_RECIPE_ID"]);
                            break;
                        case "PROC_COUNT":
                            PROC_COUNT = Convert.ToInt32(adoDataTable.Rows[0]["PROC_COUNT"]);
                            break;
                        case "LAST_START_TIME":
                            LAST_START_TIME = (string)(adoDataTable.Rows[0]["LAST_START_TIME"]);
                            break;
                        case "LAST_END_TIME":
                            LAST_END_TIME = (string)(adoDataTable.Rows[0]["LAST_END_TIME"]);
                            break;
                        case "LAST_DOWN_TIME":
                            LAST_DOWN_TIME = (string)(adoDataTable.Rows[0]["LAST_DOWN_TIME"]);
                            break;
                        case "LAST_DOWN_HIST_SEQ":
                            LAST_DOWN_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_DOWN_HIST_SEQ"]);
                            break;
                        case "LAST_EVENT_ID":
                            LAST_EVENT_ID = (string)(adoDataTable.Rows[0]["LAST_EVENT_ID"]);
                            break;
                        case "LAST_EVENT_TIME":
                            LAST_EVENT_TIME = (string)(adoDataTable.Rows[0]["LAST_EVENT_TIME"]);
                            break;
                        case "LAST_ACTIVE_HIST_SEQ":
                            LAST_ACTIVE_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_ACTIVE_HIST_SEQ"]);
                            break;
                        case "LAST_HIST_SEQ":
                            LAST_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_HIST_SEQ"]);
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                    _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
                else
                    _dbc.gErrors.SetErrors(ex);

                return false;
            }

            return true;
        }

        public bool FillResLocList(int _step, ref DataTable adoDataTable)
        {
            string strQuery = "";

            try
            {
                if (_dbc.gErrors.MsgInit() == false)
                    return false;
                OleDbDataAdapter adoAdapter = new OleDbDataAdapter();
                adoAdapter.SelectCommand = new OleDbCommand();

                // Set Connection, Transaction
                adoAdapter.SelectCommand.Connection = _dbc.gOleDbConnection;
                adoAdapter.SelectCommand.Transaction = _dbc.gOleDbTransaction;
                adoDataTable = new DataTable();

                switch (_step)
                {
                    case 1:
                        // Select All Record
                        strQuery = "SELECT * FROM MFMBENVDEF"
                            + " ORDER BY FACTORY ASC";

                        break;

                    case 101:
                        // TODO : User Select Query
                        break;

                    default:
                        // Error Handling
                        _dbc.gErrors.SqlCode = SQL_CODE.SQL_CASE_ERROR;
                        return false;
                }

                // Fill()
                adoAdapter.SelectCommand.CommandText = strQuery;
                _dbc.gErrors.AddQuery(strQuery, adoAdapter.SelectCommand.Parameters);
                adoAdapter.Fill(adoDataTable);

                // Check Data Not Found
                if (adoDataTable.Rows.Count < 1)
                {
                    _dbc.gErrors.SqlCode = SQL_CODE.SQL_NOT_FOUND;
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                    _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
                else
                    _dbc.gErrors.SetErrors(ex);

                return false;
            }

            return true;
        }

        public bool FillResDetail(int _step, ref DataTable adoDataTable)
        {
            string strQuery = "";

            try
            {
                if (_dbc.gErrors.MsgInit() == false)
                    return false;
                OleDbDataAdapter adoAdapter = new OleDbDataAdapter();
                adoAdapter.SelectCommand = new OleDbCommand();

                // Set Connection, Transaction
                adoAdapter.SelectCommand.Connection = _dbc.gOleDbConnection;
                adoAdapter.SelectCommand.Transaction = _dbc.gOleDbTransaction;
                adoDataTable = new DataTable();

                switch (_step)
                {
                    case 1:
                        // Select All Record
                        strQuery = "SELECT * FROM MFMBENVDEF"
                            + " ORDER BY FACTORY ASC";

                        break;

                    case 101:
                        // TODO : User Select Query
                        break;

                    default:
                        // Error Handling
                        _dbc.gErrors.SqlCode = SQL_CODE.SQL_CASE_ERROR;
                        return false;
                }

                // Fill()
                adoAdapter.SelectCommand.CommandText = strQuery;
                _dbc.gErrors.AddQuery(strQuery, adoAdapter.SelectCommand.Parameters);
                adoAdapter.Fill(adoDataTable);

                // Check Data Not Found
                if (adoDataTable.Rows.Count < 1)
                {
                    _dbc.gErrors.SqlCode = SQL_CODE.SQL_NOT_FOUND;
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                    _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
                else
                    _dbc.gErrors.SetErrors(ex);

                return false;
            }

            return true;
        }

        public bool FillUdrDetail(int _step, ref DataTable adoDataTable)
        {
            string strQuery = "";

            try
            {
                if (_dbc.gErrors.MsgInit() == false)
                    return false;
                OleDbDataAdapter adoAdapter = new OleDbDataAdapter();
                adoAdapter.SelectCommand = new OleDbCommand();

                // Set Connection, Transaction
                adoAdapter.SelectCommand.Connection = _dbc.gOleDbConnection;
                adoAdapter.SelectCommand.Transaction = _dbc.gOleDbTransaction;
                adoDataTable = new DataTable();

                switch (_step)
                {
                    case 1:
                        strQuery = "SELECT A.*, B.RES_DESC, B.RES_TYPE, B.AREA_ID,B.SUB_AREA_ID, B.RES_LOCATION, B.PROC_RULE, B.MAX_PROC_COUNT,"
                            + " B.RES_UP_DOWN_FLAG, B.RES_PRI_STS, B.RES_STS_1, B.RES_STS_2, B.RES_STS_3, B.RES_STS_4, B.RES_STS_5,"
                            + " B.RES_STS_6, B.RES_STS_7, B.RES_STS_8, B.RES_STS_9, B.RES_STS_10, B.RES_CTRL_MODE, B.RES_PROC_MODE, B.LAST_RECIPE_ID,"
                            + " B.PROC_COUNT, B.LAST_START_TIME, B.LAST_END_TIME, B.LAST_DOWN_TIME, B.LAST_DOWN_HIST_SEQ, B.LAST_EVENT_ID,"
                            + " B.LAST_EVENT_TIME, B.LAST_ACTIVE_HIST_SEQ, B.LAST_HIST_SEQ"
                            + " FROM MFMBUDRLOC A, MRASRESDEF B"
                            + " WHERE A.FACTORY = ?"
                            + " AND A.GROUP_ID = ?"
                            + " AND A.FACTORY = B.FACTORY"
                            + " AND A.RES_ID = B.RES_ID"
                            + " AND A.RES_TYPE = 'R'"
                            + " AND B.DELETE_FLAG <> 'Y'"
                            + " AND A.SEQ >= ?"
                            + " ORDER BY A.SEQ";

                        adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                        adoAdapter.SelectCommand.Parameters.Add("@GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
                        adoAdapter.SelectCommand.Parameters.Add("@SEQ", OleDbType.Integer).Value = SEQ;

                        break;

                    case 2:
                        strQuery = "SELECT * FROM (SELECT A.*, B.RES_DESC, B.RES_TYPE RTYPE, B.AREA_ID,B.SUB_AREA_ID, B.RES_LOCATION, B.PROC_RULE, B.MAX_PROC_COUNT,"
                            + " B.RES_UP_DOWN_FLAG, B.RES_PRI_STS, B.RES_STS_1, B.RES_STS_2, B.RES_STS_3, B.RES_STS_4, B.RES_STS_5,"
                            + " B.RES_STS_6, B.RES_STS_7, B.RES_STS_8, B.RES_STS_9, B.RES_STS_10, B.RES_CTRL_MODE, B.RES_PROC_MODE, B.LAST_RECIPE_ID,"
                            + " B.PROC_COUNT, B.LAST_START_TIME, B.LAST_END_TIME, B.LAST_DOWN_TIME, B.LAST_DOWN_HIST_SEQ, B.LAST_EVENT_ID,"
                            + " B.LAST_EVENT_TIME, B.LAST_ACTIVE_HIST_SEQ, B.LAST_HIST_SEQ"
                            + " FROM MFMBUDRLOC A, MRASRESDEF B"
                            + " WHERE A.FACTORY = ?"
                            + " AND A.GROUP_ID = ?"
                            + " AND A.FACTORY = B.FACTORY"
                            + " AND A.RES_ID = B.RES_ID"
                            + " AND A.RES_TYPE = 'R'"
                            + " AND B.DELETE_FLAG <> 'Y'"
                            + " UNION"
                            + " SELECT A.* ,' ',' ',' ' RTYPE,' ',' ',' ',0,' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',"
                            + " ' ',0,' ',' ',' ',0,' ',' ',0,0"
                            + " FROM MFMBUDRLOC A"
                            + " WHERE A.FACTORY = ?"
                            + " AND A.GROUP_ID = ?"
                            + " AND A.RES_TYPE = 'T')"
                            + " WHERE SEQ <= ?"
                            + " ORDER BY SEQ DESC";
                            
                        adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                        adoAdapter.SelectCommand.Parameters.Add("@GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
                        adoAdapter.SelectCommand.Parameters.Add("@SEQ", OleDbType.Integer).Value = SEQ;
                        adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                        adoAdapter.SelectCommand.Parameters.Add("@GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
                        adoAdapter.SelectCommand.Parameters.Add("@SEQ", OleDbType.Integer).Value = SEQ;
                 
                        break;

                    case 101:
                        // TODO : User Select Query
                        break;

                    default:
                        // Error Handling
                        _dbc.gErrors.SqlCode = SQL_CODE.SQL_CASE_ERROR;
                        return false;
                }

                // Fill()
                adoAdapter.SelectCommand.CommandText = strQuery;
                _dbc.gErrors.AddQuery(strQuery, adoAdapter.SelectCommand.Parameters);
                adoAdapter.Fill(adoDataTable);

                // Check Data Not Found
                if (adoDataTable.Rows.Count < 1)
                {
                    _dbc.gErrors.SqlCode = SQL_CODE.SQL_NOT_FOUND;
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                    _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
                else
                    _dbc.gErrors.SetErrors(ex);

                return false;
            }

            return true;
        }

#endregion

    }

}