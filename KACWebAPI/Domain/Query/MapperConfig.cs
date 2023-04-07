using AutoMapper;
using KACCloudContextLibrary.Domain.Entity;
using KACCloudContextLibrary.DomainDTO.EntityDTO;

namespace KVMWebAPI.Domain.Query
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Utente, UtenteDTO>();
                cfg.CreateMap<Ruolo, RuoloDTO>();
            });

            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
    
}
