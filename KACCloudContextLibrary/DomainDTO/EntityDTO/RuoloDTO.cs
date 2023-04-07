using KACCloudContextLibrary.Domain.Entity;
using KVMContext.DomainDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KACCloudContextLibrary.DomainDTO.EntityDTO
{
    public class RuoloDTO : EntityDTOBase
    {
        public string Nome { get; set; }
        public int? Abilitazione { get; set; }

    }
}
