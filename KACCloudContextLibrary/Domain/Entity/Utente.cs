using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace KACCloudContextLibrary.Domain.Entity
{
    public class Utente : EntityBase
    {
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public Guid RuoloID { get; set; }
        public Ruolo Ruolo { get; set;}
    }
}
