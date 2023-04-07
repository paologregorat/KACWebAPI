using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Query.Abstract;
using KACWebAPI.Domain.Query.Queries.Utente.Concrete;


namespace KACWebAPI.Domain.Query.Queries.Utente.Handler
{
    public class OneUtenteQueryHandler : IQueryHandler<OneUtenteQuery, UtenteDTO>
    {
        private readonly OneUtenteQuery _query;
        private readonly UtenteBusiness _business;
        public OneUtenteQueryHandler(OneUtenteQuery query, UtenteBusiness business)
        {
            _query = query;
            _business = business;
        }
        public UtenteDTO Get()
        {
            return _business.GetByIdDTO(_query.ID);

        }
    }
}
