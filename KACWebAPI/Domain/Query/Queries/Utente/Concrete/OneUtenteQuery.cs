using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACWebAPI.Domain.Query.Abstract;

namespace KACWebAPI.Domain.Query.Queries.Utente.Concrete
{
    public class OneUtenteQuery : IQuery<UtenteDTO>
    {
        public Guid ID { get; private set; }
        public OneUtenteQuery(Guid id)
        {
            ID = id;
        }
    }
}