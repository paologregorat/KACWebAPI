using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Query.Abstract;
using KACWebAPI.Domain.Query.Queries.Ruolo.Concrete;


namespace KACWebAPI.Domain.Query.Queries.Ruolo.Handler
{
    public static class RuoloQueryHandlerFactory
    {
        public static IQueryHandler<AllRuoloQuery, IEnumerable<RuoloDTO>> Build(AllRuoloQuery query, RuoloBusiness business)
        {
            return new AllRuoloQueryHandler(business);
        }

        public static IQueryHandler<OneRuoloQuery, RuoloDTO> Build(OneRuoloQuery query, RuoloBusiness business)
        {
            return new OneRuoloQueryHandler(query, business);
        }

    }
}