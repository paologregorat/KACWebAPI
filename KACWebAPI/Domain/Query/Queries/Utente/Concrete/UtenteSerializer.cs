using KACCloudContextLibrary.Domain;
using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KVMContext.DomainDTO;
using KVMWebAPI.Domain.Query;
using Microsoft.EntityFrameworkCore;


namespace KACWebAPI.Domain.Query.Queries.Utente.Concrete
{
    public class UtenteSerializer : IUtenteSerializer
    {
        public EntityDTOBase SerializeSingle(EntityBase toSerialize)
        {
            if (toSerialize == default)
            {
                return null;
            }

            var mapper = MapperConfig.InitializeAutomapper();
            var result = mapper.Map<UtenteDTO>(toSerialize);
            return result;
        }
    }
}