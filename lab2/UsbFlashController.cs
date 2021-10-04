using System;
using System.IO;
using System.Linq;
using System.Management;

namespace lab2
{
    /// <summary>
    /// Класс для управления подключением устройств.
    /// </summary>
    public class UsbFlashController : IDisposable
    {
        private readonly ManagementEventWatcher _plugWatcher;
        
        private readonly ManagementEventWatcher _unplugWatcher;

        private readonly Db _db;

        public event Action<string> DevicePlugged;
        
        public event Action<string> DeviceUnplugged;
        
        public UsbFlashController()
        {
            _db = new Db();

            var plugQuery = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");
            _plugWatcher = new ManagementEventWatcher(plugQuery);
            _plugWatcher.EventArrived += OnDevicePlugged;
            _plugWatcher.Start();
            
            var unplugQuery = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 3");
            _unplugWatcher = new ManagementEventWatcher(unplugQuery);
            _unplugWatcher.EventArrived += OnDeviceUnplugged;
            _unplugWatcher.Start();
        }
        
        public UsbDrive GetDevice(string name)
        {
            var searcher = new ManagementObjectSearcher(@"Select * From Win32_LogicalDisk");
            var devices = searcher.Get();

            var pluggedDeviceId = string.Empty;
            
            foreach (var device in devices)
            {
                const int removableDeviceType = 2;
                var driveType = (uint) device["DriveType"];

                if (driveType != removableDeviceType) continue;

                var letter =  device["DeviceID"].ToString();

                if (!letter.Equals(name)) continue; 
                
                pluggedDeviceId = device["VolumeSerialNumber"].ToString();

                break;
            }

            if (string.IsNullOrEmpty(pluggedDeviceId))
            {
                throw new DriveNotFoundException("Drive not found!");
            }

            var pluggedDevice = _db.UsbDrives
                .FirstOrDefault(ud => ud.SerialNumber.Equals(pluggedDeviceId));

            if (pluggedDevice != null) return pluggedDevice;
            
            pluggedDevice = new UsbDrive(pluggedDeviceId);
            _db.UsbDrives.Add(pluggedDevice);
            _db.SaveChanges();
            
            return pluggedDevice;
        }

        private void OnDevicePlugged(object sender, EventArrivedEventArgs e)
        {
            var driveName = e.NewEvent["DriveName"].ToString();
            DevicePlugged?.Invoke(driveName);
        }

        private void OnDeviceUnplugged(object sender, EventArrivedEventArgs e)
        {
            var driveName = e.NewEvent["DriveName"].ToString();
            DeviceUnplugged?.Invoke(driveName);
        }
        
        public void Dispose()
        {
            _plugWatcher.Stop();
            _plugWatcher.Dispose();
            
            _unplugWatcher.Stop();
            _unplugWatcher.Dispose();
        }

        ~UsbFlashController() => Dispose();
    }
}