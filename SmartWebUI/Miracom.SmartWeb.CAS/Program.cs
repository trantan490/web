using System;
using System.IO;
using System.Xml;
using System.Configuration;
using Miracom.SmartWeb.FWX;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Diagnostics;
using System.Net;

namespace Miracom.SmartWeb.CAS
{
    static class Program
    {

        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [System.STAThread]
        static void Main()
        {
            int i = 0;
            bool bRet = false;

            string sMainUrl = string.Empty;
            //string FolderPath = Directory.GetDirectoryRoot(Environment.SystemDirectory) + "SmartWeb";
            //string OptionFile = FolderPath + @"\SmartWebOption.xml";
            SecuritySetup CASSetup = new SecuritySetup();

            CASSetup.LoadProperties();
            bRet = CASSetup.SetupMultiUrl();

            /* Added By YJJung 140430 CAS 32/64 Bit 호환 */
            SecuritySetup64bit_Ex();
            /* Added End */

            //.NET 4.0 migration 2014.01 by ImMihwa
            //EnableIEHosting 레지스트리 등록
            EnableIEHosting();

            if (bRet == true)
            {
                try
                {

                    // 1. SmartWeb Option 설정
                    SetSmartWebOption(CASSetup);
                    
                    // 2. SmartWeb URL을 가져온다
                    sMainUrl = GetSmartWebUrl();

                    //SaveTrustedSite
                    if (sMainUrl != string.Empty)
                    {
                        // 3. 신뢰할 수 있는 사이트로 등록                        
                        SaveTrustedSite(sMainUrl);

                    }
                    
                    //4. Internet Browser를 닫는다
                    CloseSmartWebReport();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 32/64Bit 별로 Client PC의 호환성에 이슈가 있기에, 64Bit Client PC 에서도 구동 가능  
        /// </summary>
        /// <returns></returns>
        private static void SecuritySetup64bit_Ex()
        {

            string sWow64;
            string sDirPathFw;
            
            try
            {
                
                sWow64 = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToString();
                string m_permissionSet = ""; //관련된 이름 및 설명이 포함된 권한 집합을 정의합니다.
                string m_permissionURL = ""; //특정 증명 정보 집합이 적용되는 URL/Site.
                string[] URLArrays = null;

                m_permissionSet = ConfigurationManager.AppSettings["PermissionSet"];
                m_permissionURL = ConfigurationManager.AppSettings["PermissionURL"];
                URLArrays = m_permissionURL.Split(new char[] { ',' });
                /* CAS 를 Any CPU로 배포 후, Client 32 PC에서 32Bit IE 로 실행 시 32Bit Framework에 코드 그룹 넣기 */
                    
                //if (sWow64 != "x86" && Directory.Exists(Directory.GetDirectoryRoot(Environment.SystemDirectory) 
                //    + @"Windows\Microsoft.NET\Framework64\V2.0.50727"))
                //{
                //    sDirPathFw = Directory.GetDirectoryRoot(Environment.SystemDirectory) + @"Windows\Microsoft.NET\Framework\V2.0.50727";
                //    ExecuteCommandCAS(sDirPathFw, url);
                //}
                /* CAS 를 32Bit로 배포 후, Client 64 PC에서 실행 시 64Bit Framework에 코드 그룹 넣기 */
                if (sWow64 == "x86" && Directory.Exists(Directory.GetDirectoryRoot(Environment.SystemDirectory)
                    + @"Windows\Microsoft.NET\Framework64\V2.0.50727"))
                {
                    sDirPathFw = Directory.GetDirectoryRoot(Environment.SystemDirectory) + @"Windows\Microsoft.NET\Framework64\V2.0.50727";

                    ProcessStartInfo cmd = new ProcessStartInfo();
                    Process process = new Process();
                    cmd.FileName = @"cmd";
                    cmd.WindowStyle = ProcessWindowStyle.Hidden;
                    cmd.CreateNoWindow = true;
                    cmd.UseShellExecute = false;
                    cmd.RedirectStandardInput = true;
                    cmd.RedirectStandardError = true;
                    process.EnableRaisingEvents = false;
                    process.StartInfo = cmd;
                    process.Start();
                    process.StandardInput.Write(sDirPathFw + "\\caspol.exe -rs" + Environment.NewLine);
                    foreach (string permURL in URLArrays)
                    {
                        process.StandardInput.Write(sDirPathFw + "\\caspol.exe -m -pp off -ag 1 -url " + permURL + " " + m_permissionSet + Environment.NewLine);
                    }
                    process.StandardInput.Close();
                    process.WaitForExit();
                    process.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SmartWebOption Xml파일을 생성한다.
        /// </summary>
        /// <param name="CASSetup"></param>
        private static void SetSmartWebOption(SecuritySetup CASSetup)
        {
            //string OptionFile = Directory.GetDirectoryRoot(Environment.SystemDirectory) + @"\SmartWeb\SmartWebOption.xml";
            // MIRACOM V2 : 2020,08.10 : MainForm Load 시 Option 과 일치화시킴. 
            string OptionFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SmartWeb\SmartWebOption.xml";


            try
            {
                //Directory가 존재 하지 않은 경우 Directory를 생성 한다.
                //if (!Directory.Exists(Directory.GetDirectoryRoot(Environment.SystemDirectory) + @"\SmartWeb"))
                //    Directory.CreateDirectory(Directory.GetDirectoryRoot(Environment.SystemDirectory) + @"\SmartWeb");

                // MIRACOM V2 : 2020,08.10 : MainForm Load 시 Option 과 일치화시킴. 
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SmartWeb"))
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SmartWeb");


                XmlWriterSettings settings = new XmlWriterSettings();
                //settings.Indent = true;
                //settings.IndentChars = ("    ");
                using (XmlWriter writer = XmlWriter.Create(OptionFile, settings))
                {
                    writer.WriteStartElement("configuration");
                    writer.WriteStartElement("appSettings");

                    writer.WriteStartElement("add");
                    writer.WriteAttributeString("key", "Language");
                    writer.WriteAttributeString("value", "2");
                    writer.WriteEndElement();

                    writer.WriteStartElement("add");
                    writer.WriteAttributeString("key", "AutoLogOutTime");
                    writer.WriteAttributeString("value", "20");
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndElement();

                    writer.Flush();
                    writer.Close();
                }
                if (CASSetup.reSet == true) System.Windows.Forms.MessageBox.Show("SmartWeb Report modified security setting.", "SmartWeb Report Security Setting");
                else System.Windows.Forms.MessageBox.Show("SmartWeb Report finished Security Setting.", "SmartWeb Report Security Setting");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SmartWeb의 Root URL을 가져온다.
        /// </summary>
        /// <returns>string형식의 Root URL</returns>
        private static string GetSmartWebUrl()
        {
            string sUrl = string.Empty;
            string[] sRootUrl = null;

            try
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    sUrl = ApplicationDeployment.CurrentDeployment.ActivationUri.AbsoluteUri;
                    sUrl = sUrl.Replace("http://", "");
                    sRootUrl = sUrl.Split('/');

                    if (sRootUrl.Length > 0) return sRootUrl[0].ToString();
                    else return string.Empty;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 열려있는 Browser 중 Title이 Smart Web Report인 Browser를 닫는다.
        /// </summary>
        private static void CloseSmartWebReport()
        {
            try
            {
                System.Diagnostics.Process[] proc = System.Diagnostics.Process.GetProcessesByName("iexplore");

                MessageBox.Show("It closes CAS page. After reopening, it can use report.", "SmartWeb Report");

                for (int i = 0; i < proc.Length; i++)
                {
                    if (proc[i].MainWindowTitle.Contains("Smart Web"))
                    {
                        proc[i].CloseMainWindow();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 신뢰할 수 있는 사이트로 등록한다.
        /// </summary>
        /// <param name="url">현재 접속한 Root URL</param>
        private static void SaveTrustedSite(string url)
        {
            string[] sKeyNames = null;
            bool IsTrustedSite = false;
            int iCnt = 0;
            System.Net.IPAddress OutUrlAddr = new IPAddress(65536);
            Microsoft.Win32.RegistryKey SoftwareKey = null;
            Microsoft.Win32.RegistryKey MicrosoftKey = null;
            Microsoft.Win32.RegistryKey WindowsKey = null;
            Microsoft.Win32.RegistryKey CurrentVersionKey = null;
            Microsoft.Win32.RegistryKey InternetSettingsKey = null;
            Microsoft.Win32.RegistryKey ZoneMapKey = null;
            Microsoft.Win32.RegistryKey RangesKey = null;
            Microsoft.Win32.RegistryKey DomainsKey = null;
            Microsoft.Win32.RegistryKey TempKey = null;
            Microsoft.Win32.RegistryKey SaveKey = null;


            try
            {

                SoftwareKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", false);
                MicrosoftKey = SoftwareKey.OpenSubKey("Microsoft", false);
                WindowsKey = MicrosoftKey.OpenSubKey("Windows", false);
                CurrentVersionKey = WindowsKey.OpenSubKey("CurrentVersion", false);
                InternetSettingsKey = CurrentVersionKey.OpenSubKey("Internet Settings", false);
                ZoneMapKey = InternetSettingsKey.OpenSubKey("ZoneMap", false);
                DomainsKey = ZoneMapKey.OpenSubKey("Domains", true);
                RangesKey = ZoneMapKey.OpenSubKey("Ranges", true);
                sKeyNames = RangesKey.GetSubKeyNames();
                //RangesKey.CreateSubKey("Range99");
                //TempKey = RangesKey.OpenSubKey("Range99", true);
                //SaveKey = TempKey;

                //SaveKey.SetValue(":Range", url);
                //SaveKey.SetValue("http", 2, Microsoft.Win32.RegistryValueKind.DWord);
                /* Updated By YJJung 141020 for the different saving path between domain and IP address. */
                if (IPAddress.TryParse(url, out OutUrlAddr))
                {
                    for (int i = 0; i < sKeyNames.Length; i++)
                    {
                        TempKey = RangesKey.OpenSubKey(sKeyNames[i].ToString(), false);
                        if (url == TempKey.GetValue(":Range").ToString())
                        {
                            IsTrustedSite = true;
                        }
                        iCnt++;
                    }

                    if (IsTrustedSite == false)
                    {
                        RangesKey.CreateSubKey("Range" + (iCnt + 1).ToString());
                        SaveKey = RangesKey.OpenSubKey("Range" + (iCnt + 1).ToString(), true);

                        SaveKey.SetValue(":Range", url);
                        SaveKey.SetValue("http", 2, Microsoft.Win32.RegistryValueKind.DWord);
                    }
                }
                else
                {
                    DomainsKey.CreateSubKey(url);
                    SaveKey = DomainsKey.OpenSubKey(url, true);
                    SaveKey.SetValue("http", 2, Microsoft.Win32.RegistryValueKind.DWord);
                }
                
                MessageBox.Show("It registered trusted site.", "SmartWeb Report");
                //SoftwareKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", false);
                //MicrosoftKey = SoftwareKey.OpenSubKey("Microsoft", false);
                //WindowsKey = MicrosoftKey.OpenSubKey("Windows", false);
                //CurrentVersionKey = WindowsKey.OpenSubKey("CurrentVersion", false);
                //InternetSettingsKey = CurrentVersionKey.OpenSubKey("Internet Settings", false);
                //ZoneMapKey = InternetSettingsKey.OpenSubKey("ZoneMap", false);
                //RangesKey = ZoneMapKey.OpenSubKey("Ranges", true);

                //sKeyNames = RangesKey.GetSubKeyNames();

                //for (int i = 0; i < sKeyNames.Length; i++)
                //{
                //    TempKey = RangesKey.OpenSubKey(sKeyNames[i].ToString(), false);
                //    if (url == TempKey.GetValue(":Range").ToString())
                //    {
                //        IsTrustedSite = true;
                //    }
                //    iCnt++;
                //}

                //if (IsTrustedSite == false)
                //{
                //    if (MessageBox.Show("신뢰할 수 있는 사이트로 등록하셔야 정상적으로 사용 가능합니다. 등록하시겠습니까?", "SmartWeb Report", MessageBoxButtons.YesNo) == DialogResult.No) return;

                //    RangesKey.CreateSubKey("Range" + (iCnt + 1).ToString());
                //    SaveKey = RangesKey.OpenSubKey("Range" + (iCnt + 1).ToString(), true);

                //    SaveKey.SetValue(":Range", url);
                //    SaveKey.SetValue("http", 2, Microsoft.Win32.RegistryValueKind.DWord);
                //    MessageBox.Show("신뢰할 수 있는 사이트로 등록하였습니다.", "SmartWeb Report");
                //}

                //sKeyNames = RangesKey.GetSubKeyNames();

                //for (int i = 0; i < sKeyNames.Length; i++)
                //{
                //    //TempKey = DomainsKey.OpenSubKey(sKeyNames[i].ToString(), false);
                //    //if (url.Split('.') == TempKey.ToString())
                //    //{
                //    //}

                //    if (url == sKeyNames[i].ToString())
                //    {
                //        IsTrustedSite = true;
                //        break;
                //    }

                //    //iCnt++;
                //}

                //if (IsTrustedSite == false)
                //{
                //    if (MessageBox.Show("Registering trusted site, it can use normally. Does you register?", "SmartWeb Report", MessageBoxButtons.YesNo) == DialogResult.No) return;

                //DomainsKey.CreateSubKey("wia.co.kr");

                //TempKey = DomainsKey.OpenSubKey("wia.co.kr", true);

                //TempKey.CreateSubKey("pomes");
                //SaveKey = TempKey.OpenSubKey("pomes", true);





                //DomainsKey.CreateSubKey("winpac.co.kr");
                //TempKey = DomainsKey.OpenSubKey("winpac.co.kr", true);
                //SaveKey = TempKey;
                //SaveKey.CreateSubKey("rt");
                //TempKey = SaveKey.OpenSubKey("rt", true);
                //TempKey.SetValue("http", 2, Microsoft.Win32.RegistryValueKind.DWord);

                
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (SoftwareKey != null) SoftwareKey.Close();
                if (MicrosoftKey != null) MicrosoftKey.Close();
                if (WindowsKey != null) WindowsKey.Close();
                if (CurrentVersionKey != null) CurrentVersionKey.Close();
                if (InternetSettingsKey != null) InternetSettingsKey.Close();
                if (ZoneMapKey != null) ZoneMapKey.Close();
                if (RangesKey != null) RangesKey.Close();
                if (TempKey != null) TempKey.Close();
                if (SaveKey != null) SaveKey.Close();
            }
        }
        
        /// <summary>
        /// .net 4.0 migration registry key
        /// </summary>
        public static void EnableIEHosting()
        {
            Microsoft.Win32.RegistryKey SoftwareKey = null;
            Microsoft.Win32.RegistryKey MSKey = null;
            Microsoft.Win32.RegistryKey FrameWorkKey = null;
            
            try
            {
                SoftwareKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software", false);
                MSKey = SoftwareKey.OpenSubKey("Microsoft", false);
                FrameWorkKey = MSKey.OpenSubKey(".NETFramework", true);
                FrameWorkKey.SetValue("EnableIEHosting", 1, Microsoft.Win32.RegistryValueKind.DWord);

                /* Added By YJJung For 64 Bit Compatibility */
                if (Directory.Exists(Directory.GetDirectoryRoot(Environment.SystemDirectory)
                    + @"Windows\Microsoft.NET\Framework64\V2.0.50727"))
                {

                    SoftwareKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node", false);
                    MSKey = SoftwareKey.OpenSubKey("Microsoft", false);
                    FrameWorkKey = MSKey.OpenSubKey(".NETFramework", true);
                    FrameWorkKey.SetValue("EnableIEHosting", 1, Microsoft.Win32.RegistryValueKind.DWord);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(SoftwareKey != null) SoftwareKey.Close();
                if (MSKey != null) MSKey.Close();
                if (FrameWorkKey != null) FrameWorkKey.Close();
            }
        }

    }
}