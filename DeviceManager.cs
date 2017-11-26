using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;

namespace Laba_5
{
    class DeviceManager
    {
        public List<Device> DeviseListCreate()
        {
            var connectionScope = new ManagementScope();
            var serialQuery = new SelectQuery("SELECT * FROM Win32_PnPEntity");
            var deviceList = new ManagementObjectSearcher(connectionScope, serialQuery);

            return GetDevices(deviceList.Get());
        }

        private List<Device> GetDevices(ManagementObjectCollection deviceList)
        {
            var devices = new List<Device>();
            foreach (ManagementObject item in deviceList)
            {
                string tempGUID = "",
                    tempHardware = "",
                    tempManufacturer = "",
                    tempProvider = "",
                    tempDesc = "",
                    tempSys = "",
                    tempPath = "",
                    tempStatus = "";

                if (item["ClassGuid"] != null)
                {
                    tempGUID = item["ClassGuid"].ToString();
                }
                if (item["HardwareID"] != null)
                {
                    tempHardware = String.Join("\r\n", (string[])item["HardwareID"]) ;
                }
                if (item["Manufacturer"] != null)
                {
                    tempManufacturer = item["Manufacturer"].ToString();
                }
                if (item["Caption"] != null)
                {
                    tempManufacturer = item["Caption"].ToString();
                }
                tempPath = item["DeviceID"].ToString();
                tempStatus = item["Status"].ToString();

                var driver = item.GetRelated("Win32_SystemDriver");
                foreach (var driverProperty in driver)
                {
                    tempDesc += driverProperty["Description"].ToString();
                    tempSys += driverProperty["PathName"].ToString();
                }
                devices.Add(new Device()
                {
                    Guid = tempGUID,
                    HardwareId = tempHardware,
                    Manufacturer = tempManufacturer,
                    Provider = tempProvider,
                    DriverDescription = tempDesc,
                    SysPath = tempSys,
                    DevicePath = tempPath,
                    Status = tempStatus,
                    CanDisable = tempStatus.Contains("OK") ? true : false
                });
            }
            return devices;
        }
    }
}
