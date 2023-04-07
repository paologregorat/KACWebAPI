using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGateway.Infrastructure.System
{
    public class Setting
    {
        public string DataBaseType { get; set; }
        public string ConnectionString { get; set; }
        public string ListenerType { get; set; }
        public string CommunicatorType { get; set; }
        public string HostNameQueueListener { get; set; }
        public int PortQueueListener { get; set; }
        public string HostNameQueueCommunicator { get; set; }
        public int PortQueueCommunicator { get; set; }
    }

    public class SettingFile
    {
        public Setting Settings { get; set; }
    }
}
