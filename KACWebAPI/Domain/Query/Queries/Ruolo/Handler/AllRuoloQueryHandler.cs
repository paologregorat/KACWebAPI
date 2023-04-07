using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Query.Abstract;
using KACWebAPI.Domain.Query.Queries.Ruolo.Concrete;


namespace KACWebAPI.Domain.Query.Queries.Ruolo.Handler;

public class AllRuoloQueryHandler : IQueryHandler<AllRuoloQuery, IEnumerable<RuoloDTO>>
{
    private readonly RuoloBusiness _business;

    public AllRuoloQueryHandler(RuoloBusiness business)
    {
        _business = business;
    }

    public IEnumerable<RuoloDTO> Get()
    {
        return _business.GetAllDTO();
    }


}