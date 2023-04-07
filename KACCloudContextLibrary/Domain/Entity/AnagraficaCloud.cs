using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KACCloudContextLibrary.Domain.Entity
{
    public class AnagraficaCloud : EntityBase
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CodiceFiscale { get; set; }

    }
}
