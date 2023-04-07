using KACCloudContextLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KACCloudContextLibrary.Domain.Entity
{
    public class Ruolo : EntityBase
    {
        public string Nome { get; set; }
        [JsonIgnore]
        public IEnumerable<Utente> Utenti { get; }
        public int? Abilitazione { get; set; }

    }
}
