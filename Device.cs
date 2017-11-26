using System.Linq;
using System.Management;


namespace Laba_5
{
    class Device
    {
        public string Guid { get; set; }
        public string HardwareId { get; set; }
        public string Manufacturer { get; set; }
        public string Provider { get; set; }
        public string DriverDescription { get; set; }
        public string SysPath { get; set; }
        public string DevicePath { get; set; }
        public string Status { get; set; }
        public bool CanDisable { get; set; }

        public void DisEnaDevice(string method)
        {
            var deviceList = new ManagementObjectSearcher("SELECT * FROM Win32_PNPEntity");

            var tempCurrentElement = deviceList.Get()
                 .OfType<ManagementObject>()
                 .FirstOrDefault(x => x["DeviceID"].ToString().Contains(DevicePath)); ;
          
            if (tempCurrentElement != null)
            {
                tempCurrentElement.InvokeMethod(method, new object[] { false });
            }
        }
    }
}
