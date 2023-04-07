using System;

namespace KVMWebAPI.Infrastructure.Utility.RequestFilters
{
    public class FilterSearchStringAttribute : Attribute
    {
        public bool IsFilterActive { get; set; }
    }
}