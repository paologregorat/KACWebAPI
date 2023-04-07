using KACCloudContextLibrary.Domain;
using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KVMContext.DomainDTO;
using KVMWebAPI.Domain.Query;
using Microsoft.EntityFrameworkCore;


namespace KACWebAPI.Domain.Query.Queries.Ruolo.Concrete
{
    public class RuoloSerializer : IRuoloSerializer
    {
        public EntityDTOBase SerializeSingle(EntityBase toSerialize)
        {
            if (toSerialize == default)
            {
                return null;
            }

            var mapper = MapperConfig.InitializeAutomapper();
            var result = mapper.Map<RuoloDTO>(toSerialize);
            return result;
        }
    }
}