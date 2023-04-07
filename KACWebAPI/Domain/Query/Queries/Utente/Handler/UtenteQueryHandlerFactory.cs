using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Query.Abstract;
using KACWebAPI.Domain.Query.Queries.Utente.Concrete;


namespace KACWebAPI.Domain.Query.Queries.Utente.Handler
{
    public static class UtenteQueryHandlerFactory
    {
        public static IQueryHandler<AllUtenteQuery, IEnumerable<UtenteDTO>> Build(AllUtenteQuery query, UtenteBusiness business)
        {
            return new AllUtenteQueryHandler(business);
        }

        public static IQueryHandler<OneUtenteQuery, UtenteDTO> Build(OneUtenteQuery query, UtenteBusiness business)
        {
            return new OneUtenteQueryHandler(query, business);
        }

    }
}