namespace lab2
{
    public class UsbDrive
    {
        public string SerialNumber { get; set; }
        
        public bool IsIgnored { get; set; }
        
        public bool IsFirstConnection { get; set; }
        
        public UsbDrive(string serialNumber)
        {
            SerialNumber = serialNumber;
            IsIgnored = false;
            IsFirstConnection = true;
        }
        
        public UsbDrive(string serialNumber, bool isIgnored, bool isFirstConnection)
        {
            SerialNumber = serialNumber;
            IsIgnored = isIgnored;
            IsFirstConnection = isFirstConnection;
        }
        
    }
}