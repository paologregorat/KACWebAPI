using System.ComponentModel;

namespace KVMWebAPI.Infrastructure.System
{
    public class StatusSystem
    {
        private static StatusSystem _instance;
           
        private Setting _settings;

        private StatusSystem() { }

        public static StatusSystem GetInstance()
        {
            if (_instance == null)
            {
                _instance = new StatusSystem();
            }
            return _instance;
        }
         
        public void SetSettings(Setting settings)
        {
            _settings = settings;
        }

        public Setting Settings => _settings;

    }

    
}
