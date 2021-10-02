using System;
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

        public event Action<string> DevicePlugged;
        
        public event Action<string> DeviceUnplugged;
        
        public UsbFlashController()
        {
            var plugQuery = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");
            _plugWatcher = new ManagementEventWatcher(plugQuery);
            _plugWatcher.EventArrived += OnDevicePlugged;
            _plugWatcher.Start();
            
            var unplugQuery = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 1");
            _unplugWatcher = new ManagementEventWatcher(unplugQuery);
            _unplugWatcher.EventArrived += OnDeviceUnplugged;
            _unplugWatcher.Start();
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
        }

        ~UsbFlashController() => Dispose();
    }
}