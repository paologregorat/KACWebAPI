using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KACCloudContextLibrary.Domain.EntityNoDB
{
    public class CambioPassword
    {
        public string PasswordNuova { get; set; }
        public string PasswordVecchia { get; set; }
        public Guid? UtenteID { get; set; }

    }
}
