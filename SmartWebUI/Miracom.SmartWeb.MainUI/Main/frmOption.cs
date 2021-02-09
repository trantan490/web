using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public partial class frmOption : Form
    {
        public frmOption()
        {
            InitializeComponent();
        }

        private Boolean setSmartWebOption()
        {
            Config SmartWebOption;

            try
            {
                SmartWebOption = new Config();
                SmartWebOption.LocalcfgFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SmartWeb\SmartWebOption.xml";
                cboLanguage.SelectedIndex = Convert.ToInt32(SmartWebOption.GetValue("//appSettings//add[@key='Language']"))-1;
                txtAutoLotoutTime.Text = SmartWebOption.GetValue("//appSettings//add[@key='AutoLogOutTime']");
                return true;
            }
            catch
            {
                return false;
            }

        }

        private void frmOption_Load(object sender, EventArgs e)
        {
            setSmartWebOption();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string OptionFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SmartWeb\SmartWebOption.xml";

            try
            {
                //Directory가 존재 하지 않은 경우 Directory를 생성 한다.
                if (System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SmartWeb") == false)
                {
                    System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SmartWeb");
                }

                System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(OptionFile, settings);

                writer.WriteStartElement("configuration");
                writer.WriteStartElement("appSettings");

                writer.WriteStartElement("add");
                writer.WriteAttributeString("key", "Language");
                writer.WriteAttributeString("value", Convert.ToString(cboLanguage.SelectedIndex + 1));
                writer.WriteEndElement();

                writer.WriteStartElement("add");
                writer.WriteAttributeString("key", "AutoLogOutTime");
                if (txtAutoLotoutTime.Text.Trim() == "")
                {
                    writer.WriteAttributeString("value", "0");            
                }
                else
                {
                    writer.WriteAttributeString("value", txtAutoLotoutTime.Text);
                
                }
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.Flush();
                writer.Close();

                // 2020-01-31-김미경 : 언어 변경에 따른 메시지 출력.
                //CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD035", GlobalVariable.gcLanguage));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            GlobalVariable.gcLanguage = Convert.ToChar(cboLanguage.SelectedIndex + 1);
            GlobalVariable.giLogOutTime = txtAutoLotoutTime.Text.Trim() == "" ? 0 : Convert.ToInt32(txtAutoLotoutTime.Text);

            this.Close();
        }
    }
}