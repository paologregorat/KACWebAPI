using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACWebAPI.Domain.Query.Abstract;

namespace KACWebAPI.Domain.Query.Queries.Ruolo.Concrete
{
    public class OneRuoloQuery : IQuery<RuoloDTO>
    {
        public Guid ID { get; private set; }
        public OneRuoloQuery(Guid id)
        {
            ID = id;
        }
    }
}