using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Query.Abstract;
using KACWebAPI.Domain.Query.Queries.Utente.Concrete;


namespace KACWebAPI.Domain.Query.Queries.Utente.Handler;

public class AllUtenteQueryHandler : IQueryHandler<AllUtenteQuery, IEnumerable<UtenteDTO>>
{
    private readonly UtenteBusiness _business;

    public AllUtenteQueryHandler(UtenteBusiness business)
    {
        _business = business;
    }

    public IEnumerable<UtenteDTO> Get()
    {
        return _business.GetAllDTO();
    }


}