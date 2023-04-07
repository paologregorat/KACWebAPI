using KVMWebAPI.Infrastructure.Utility.RequestFilters;

namespace KVMWebAPI.Domain.Query.Queries.Allarme.Concrete
{
    public class UtenteFilters : GetSortPaged
    {
        [FilterAttribute(Type = FilterTypeEnum.StringEqual)]
        public string? Nome { get; set; }

        [FilterAttribute(Type = FilterTypeEnum.StringEqual)]
        public string? Cognome { get; set; }

        [FilterAttribute(Type = FilterTypeEnum.StringEqual)]
        public string? UserName { get; set; }

    }
}
