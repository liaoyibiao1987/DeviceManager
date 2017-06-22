using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WSDInstaller
{
    public partial class Installer : Form
    {
        public const string SERVICENAME = "WSDService";
        public static string frameworkInstallDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
        public static string sysDisk = System.Environment.SystemDirectory.Substring(0, 3);
        public static string dotNetPath = frameworkInstallDir + @"InstallUtil.exe";
        public static string serviceEXEPath = Application.StartupPath + @"\WSDdeviceManager.exe";//把服务的exe程序拷贝到了当前运行目录下，所以用此路径 
        public static string serviceInstallCommand = string.Format(@"{0} -i {1}", dotNetPath, serviceEXEPath);//安装服务时使用的dos命令  

        public static string stopservice = string.Format("sc stop {0}", SERVICENAME);
        public static string deleteservice = string.Format("sc delete {0}", SERVICENAME);
        public static string serviceUninstallCommand = string.Format(@"{0} -U {1}", dotNetPath, serviceEXEPath);//卸载服务时使用的dos命令  
        public Installer()
        {
            InitializeComponent();
        }

        private void Installer_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//使最大化窗口失效
                                     // this.ShowInTaskbar = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        }
        public static string Cmd(string[] cmd)
        {
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = "cmd.exe";
            //startInfo.Arguments = "/c C:\\Windows\\System32\\cmd.exe";
            //startInfo.RedirectStandardInput = true;
            //startInfo.RedirectStandardOutput = true;
            //startInfo.RedirectStandardError = true;
            //startInfo.UseShellExecute = false;
            //startInfo.Verb = "RunAs";

            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.AutoFlush = true;
            for (int i = 0; i < cmd.Length; i++)
            {
                p.StandardInput.WriteLine(cmd[i].ToString());
            }
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            return strRst;
        }

        public static bool CloseProcess(string ProcName)
        {
            bool result = false;
            System.Collections.ArrayList procList = new System.Collections.ArrayList();
            string tempName = "";
            int begpos;
            int endpos;
            foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
            {
                tempName = thisProc.ToString();
                begpos = tempName.IndexOf("(") + 1;
                endpos = tempName.IndexOf(")");
                tempName = tempName.Substring(begpos, endpos - begpos);
                procList.Add(tempName);
                if (tempName == ProcName)
                {
                    if (!thisProc.CloseMainWindow())
                        thisProc.Kill(); // 当发送关闭窗口命令无效时强行结束进程  
                    result = true;
                }
            }
            return result;
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(dotNetPath))
                {
                    string[] cmd = new string[] { serviceInstallCommand };
                    string result = Cmd(cmd);
                    txtResult.Text = result;
                }
            }
            catch
            {
            }
            finally
            {
                CloseProcess("cmd.exe");
            }

            Thread.Sleep(1000);
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(dotNetPath))
                {
                    string[] cmd = new string[3] { stopservice, deleteservice, serviceUninstallCommand };
                    string result = Cmd(cmd);
                    txtResult.Text = result;
                }
            }
            catch
            {
            }
            finally
            {
                CloseProcess("cmd.exe");
            }
            Thread.Sleep(1000);
        }
    }
}
