using System;
using System.Windows.Forms;

namespace lab2
{
    public partial class DeviceForm : Form
    {
        private const string ENCRYPT_DRIVE_TEXT = "Зашифровать";
        
        private const string DECRYPT_DRIVE_TEXT = "Расшифровать";
        
        private static readonly UsbFlashController FLASH_CONTROLLER = new();

        private UsbDrive _currentDrive;

        private string _currentDrivePath;
        
        private readonly MethodInvoker _showSync;
        
        private readonly MethodInvoker _hideSync;
        
        public DeviceForm()
        {
            InitializeComponent();
            
            _showSync = Show;
            _hideSync = Hide;

            FLASH_CONTROLLER.DevicePlugged += OnDevicePlugged;
            FLASH_CONTROLLER.DeviceUnplugged += OnDeviceUnplugged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Hide();
        }

        private new void Show()
        {
            if (_currentDrive == null)
            {
                MessageBox.Show("No connected device!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }
            
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            
            _encryptButton.Text = GetEncryptButtonText(_currentDrive.IsEncrypted);
            _ignoreCheckBox.Checked = _currentDrive.IsIgnored;
        }

        private new void Hide()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }

        private void OnDevicePlugged(string name)
        {
            if (!IsHandleCreated) return;

            _currentDrivePath = name;
            _currentDrive = FLASH_CONTROLLER.GetDrive(name);
            
            if (_currentDrive.IsIgnored) return;

            Invoke(_showSync);
        }
        
        private void OnDeviceUnplugged(string name)
        {
            if (!IsHandleCreated) return;
        
            FLASH_CONTROLLER.UpdateDrive(_currentDrive);
            
            _currentDrive = null;
            _currentDrivePath = null;

            Invoke(_hideSync);
        }

        private string GetEncryptButtonText(bool inDriveEncrypted) => inDriveEncrypted 
            ? DECRYPT_DRIVE_TEXT 
            : ENCRYPT_DRIVE_TEXT;

        private void DeviceForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            
            Hide();
        }

        private void _encryptButton_Click(object sender, EventArgs e)
        {
            bool isDriveEncrypted;
            
            if (_currentDrive.IsEncrypted)
            {
                DirectoryEncryption.Encrypt(_currentDrivePath, false);
                isDriveEncrypted = false;
            }
            else
            {
                DirectoryEncryption.Encrypt(_currentDrivePath, true);
                
                _currentDrive.IsIgnored = false;
                isDriveEncrypted = true;
            }

            _currentDrive.IsEncrypted = isDriveEncrypted;
            _encryptButton.Text = GetEncryptButtonText(isDriveEncrypted);
            _ignoreCheckBox.Enabled = !isDriveEncrypted;
            _ignoreCheckBox.Checked = _currentDrive.IsIgnored;
            
            FLASH_CONTROLLER.UpdateDrive(_currentDrive);
        }

        private void _ignoreCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var isIgnored = _ignoreCheckBox.Checked;
            _currentDrive.IsIgnored = isIgnored;
            _encryptButton.Enabled = !isIgnored;
            
            FLASH_CONTROLLER.UpdateDrive(_currentDrive);
        }

        private void _notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) => Show();
    }
}