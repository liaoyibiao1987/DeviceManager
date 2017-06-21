using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSDdeviceManager.Win32s
{
    public class DeviceEntity
    {
        private string deviceName = "";
        public string DeviceName
        {
            get
            {
                return deviceName;
            }

            set
            {
                deviceName = value;
            }
        }

        private string devicePIDVID = "";

        public string DevicePIDVID
        {
            get
            {
                return devicePIDVID;
            }

            set
            {
                devicePIDVID = value;
            }
        }

        private string deviceID = "";
        public string DeviceID
        {
            get
            {
                return deviceID;
            }

            set
            {
                deviceID = value;
            }
        }

        private string realTimePath = "";
        public string RealTimePath
        {
            get
            {
                return realTimePath;
            }

            set
            {
                realTimePath = value;
            }
        }
        private string installState = "";
        public string InstallState
        {
            get
            {
                return installState;
            }

            set
            {
                installState = value;
            }
        }

        private bool isHiddenDevice = false;
        public bool IsHiddenDevice
        {
            get
            {
                return isHiddenDevice;
            }

            set
            {
                isHiddenDevice = value;
            }
        }

    }
}
