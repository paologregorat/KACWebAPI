using KACCloudContextLibrary.Domain;
using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KVMContext.DomainDTO;

namespace KACWebAPI.Domain.Query.Abstract
{
    public interface IGenericSerializer
    {
        public EntityDTOBase SerializeSingle(EntityBase toSerialize);
    }
}
