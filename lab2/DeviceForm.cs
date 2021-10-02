using System;
using System.Windows.Forms;

namespace lab2
{
    public partial class DeviceForm : Form
    {
        private static readonly UsbFlashController FLASH_CONTROLLER = new();
        
        private readonly MethodInvoker _showSync;
        
        private readonly MethodInvoker _hideSync;
        
        public DeviceForm()
        {
            InitializeComponent();
            
            _showSync = Show;
            _hideSync = Hide;
            
            FLASH_CONTROLLER.DevicePlugged += driveName =>
            {
                if (!IsHandleCreated) return;
                
                Invoke(_showSync);
            };

            FLASH_CONTROLLER.DeviceUnplugged += driveName =>
            {
                if (!IsHandleCreated) return;
                
                Invoke(_hideSync);
            };
        }

        private new void Show()
        {
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;       
        }

        private new void Hide()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }
        
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) => Show();

        private void DeviceForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            
            Hide();
        }
    }
}