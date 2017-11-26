using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Laba_5
{
    public partial class DeviceView : Form
    {
        private static readonly DeviceManager Manager = new DeviceManager();
        private List<Device> _deviceList;

        public DeviceView()
        {
            InitializeComponent();
        }

        private void LoadForm(object sender, EventArgs e)
        {
            _deviceList = new List<Device>();
            ReloadForm();
        }
        private void ReloadForm()
        {
            waitGif.Visible = true;
            devicesList.Items.Clear();
            _deviceList = Manager.DeviseListCreate();

            foreach (Device device in _deviceList)
            {
                var deviceInfo = new ListViewItem(device.Guid);

                deviceInfo.SubItems.AddRange(new[]
                {
                    device.HardwareId,
                    device.Manufacturer,
                    device.Provider,
                    device.DriverDescription,
                    device.SysPath,
                    device.DevicePath,
                    device.Status,
                    device.CanDisable.ToString()
                });
                devicesList.Items.Add(deviceInfo);
            }
            waitGif.Visible = false;
        }

        private void DisableEnableDevice(object sender, MouseEventArgs e)
        {
            var device = _deviceList[devicesList.HitTest(e.Location).Item.Index];            
            if (device.CanDisable)
            {
                device.DisEnaDevice("Disable");
                MessageBox.Show("Disable complete", "Status", MessageBoxButtons.OK);
                device.CanDisable = false;
            }
            else
            {
                device.DisEnaDevice("Enable");
                MessageBox.Show("Enable complete", "Status", MessageBoxButtons.OK);
                device.CanDisable = true;
            }
            ReloadForm();
        }
    }
}
