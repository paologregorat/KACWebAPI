using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KACCloudContextLibrary.Domain.EntityNoDB
{
    public class AdministratorCambioPassword
    {
        public Guid UtenteID { get; set; }
        public string NuovaPassword { get; set; }
    }
}
