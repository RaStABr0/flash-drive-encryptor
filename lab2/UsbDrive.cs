﻿using System.ComponentModel.DataAnnotations;

namespace lab2
{
    public class UsbDrive
    {
        [Key]
        public string SerialNumber { get; set; }
        
        public bool IsIgnored { get; set; }
        
        public bool IsFirstConnection { get; set; }
        
        public bool IsEncrypted { get; set; }
        
        public UsbDrive(string serialNumber)
        {
            SerialNumber = serialNumber;
            IsIgnored = false;
            IsFirstConnection = true;
            IsEncrypted = false;
        }
        
        public UsbDrive(string serialNumber, bool isIgnored, bool isFirstConnection)
        {
            SerialNumber = serialNumber;
            IsIgnored = isIgnored;
            IsFirstConnection = isFirstConnection;
        }
        
    }
}