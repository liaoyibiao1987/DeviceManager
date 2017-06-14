using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Hardware;

namespace DeviceManager
{
    public partial class MainForm : Form
    {
        HardwareClass hc = new HardwareClass();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //枚举硬件列表
            string[] HardList = hc.DevClassList();
            foreach (string s in HardList)
            {
                listBox1.Items.Add(s);
            }
            hc.AllowNotifications(Handle, true);
            label1.Text = "计算机内的设备数量为：" + listBox1.Items.Count.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //清理非托管资源
            hc.Dispose(Handle);
            hc = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //启用硬件
            string[] dev = new string[1];
            hc.Dispose(Handle);
            dev[0] = listBox1.SelectedItem.ToString();
            hc.SetState(dev, true);
            hc.Dispose(Handle);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //停用硬件
            string[] dev = new string[1];
            hc.Dispose(Handle);
            dev[0] = listBox1.SelectedItem.ToString();
            hc.SetState(dev, false);
            hc.Dispose(Handle);
        }
    }
}