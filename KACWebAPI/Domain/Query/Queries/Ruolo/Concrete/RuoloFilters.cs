using KVMWebAPI.Infrastructure.Utility.RequestFilters;

namespace KVMWebAPI.Domain.Query.Queries.Allarme.Concrete
{
    public class RuoloFilters : GetSortPaged
    {
        [FilterAttribute(Type = FilterTypeEnum.StringEqual)]
        public string? Nome { get; set; }

    }
}
