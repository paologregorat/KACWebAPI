namespace KVMWebAPI.Infrastructure.System
{
    public class Setting
    {
        public string DataBaseType { get; set; }
        public string ConnectionString { get; set; }
        public string CommunicatorType { get; set; }
        public string HostNameQueueListener { get; set; }
        public int PortQueueListener { get; set; }
        public string HostNameQueueCommunicator { get; set; }
        public int PortQueueCommunicator { get; set; }
        public int WebAPIPort { get; set; }
        public bool UseKestrel { get; set; }
    }

    public class SettingFile
    {
        public Setting Settings { get; set; }
    }
}
