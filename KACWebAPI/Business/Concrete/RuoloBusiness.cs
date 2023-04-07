using KVMWebAPI.Domain.Command.Commands;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAPI_CQRS.Domain.Infrastructure.Authorization;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using KVMWebAPI.Infrastructure.Utility.RequestFilters;
using static KVMWebAPI.Controllers.WebControllerBase;
using KACWebAPI.Business.Abstract;
using KACCloudContextLibrary.Domain;
using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACCloudContextLibrary.Domain.Entity;
using KACWebAPI.Domain.Command.Commands.Ruolo;
using KACWebAPI.Domain.Query.Queries.Ruolo.Concrete;
using KACWebAPI.Infrastructure.Utility;
using KACWebAPI.Domain.Query.Abstract;

namespace KACWebAPI.Business.Concrete
{
    public class RuoloBusiness : GenericBusiness<Ruolo, IRuoloSerializer, RuoloDTO>, IRuoloBusiness
    {
        private RuoloSerializer _serializer;
        public RuoloBusiness(KACCloudContext context, IRuoloSerializer serializer) : base(context, serializer, new string[0])
        {
        }
      
    }

}