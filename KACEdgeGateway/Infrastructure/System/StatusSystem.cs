using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGateway.Infrastructure.System
{
    public sealed class StatusSystem
    {
        private Setting _settings;

        public Setting Settings => _settings;
        public void SetSettings(Setting settings)
        {
            _settings = settings;
        }

        //private RabbitMQCommunicator _rabbitMQCommunicator;
        private StatusSystem()
        {

        }

        private static StatusSystem? _instance;

        public static StatusSystem GetInstance()
        {
            if (_instance == null)
            {
                _instance = new StatusSystem();
            }
            return _instance;
        }

    }
}
