using Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeviceManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnumerateDevices_Click(object sender, EventArgs e)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();

            HardwareClass.EnumerateDevices(0, "ports", sb1, sb2, sb3, sb4);
            int x = 0;
        }

        private void btnGetNomarlDevice_Click(object sender, EventArgs e)
        {
            List<DeviceEntity> list = HardwareClass.GetNomarlDevice();
            int x = 0;
        }

        private void btnGetAllDevice_Click(object sender, EventArgs e)
        {
            List<DeviceEntity> list = HardwareClass.GetAllDevice();
            int x = 0;
        }

        private void btnGetHiddenDevice_Click(object sender, EventArgs e)
        {
            List<DeviceEntity> list = HardwareClass.GetHiddenDevice();
            int x = 0;
        }
    }
}
