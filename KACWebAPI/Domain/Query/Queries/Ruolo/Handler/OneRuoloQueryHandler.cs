using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Query.Abstract;
using KACWebAPI.Domain.Query.Queries.Ruolo.Concrete;


namespace KACWebAPI.Domain.Query.Queries.Ruolo.Handler
{
    public class OneRuoloQueryHandler : IQueryHandler<OneRuoloQuery, RuoloDTO>
    {
        private readonly OneRuoloQuery _query;
        private readonly RuoloBusiness _business;
        public OneRuoloQueryHandler(OneRuoloQuery query, RuoloBusiness business)
        {
            _query = query;
            _business = business;
        }
        public RuoloDTO Get()
        {
            return _business.GetByIdDTO(_query.ID);

        }
    }
}
