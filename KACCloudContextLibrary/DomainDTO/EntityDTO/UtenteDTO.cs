using KVMContext.DomainDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KACCloudContextLibrary.DomainDTO.EntityDTO
{
    public class UtenteDTO : EntityDTOBase
    {
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public RuoloDTO Ruolo { get; set; }

    }
}
