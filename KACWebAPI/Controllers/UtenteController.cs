using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

using KVMWebAPI.Infrastructure.Utility.RequestFilters;

using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using KACWebAPI.Domain.Command.Commands.Utente;
using KACWebAPI.Business.Concrete;
using KACCloudContextLibrary.Domain.EntityNoDB;
using KACWebAPI.Business.Abstract;
using KACCloudContextLibrary.Domain.Entity;
using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACWebAPI.Domain.Query.Queries.Utente.Concrete;
using KACWebAPI.Domain.Query.Queries.Utente.Handler;
using KVMWebAPI.Domain.Query.Queries.Allarme.Concrete;
using Microsoft.AspNetCore.Authentication;

namespace KVMWebAPI.Controllers
{
    [Authorize]
    [Route("")]
    public class UtenteController : WebControllerBase
    {

        private readonly UtenteBusiness _business;

        public UtenteController(IUtenteBusiness business)
        {
            _business = (UtenteBusiness)business;
        }

        [AllowAnonymous]
        [HttpPost("v1/utenti/login")]
        public IActionResult Logon([FromBody] Account item)
        {
            try
            {
                var userName = item.Username;
                var password = item.Password;

                var command = new CreateTokenCommand(userName, password);

                var handler = UtenteCommandHandlerFactory.Build(command, _business);
                var response = handler.Execute();
                if (response.Success)
                {
                   

                    return Ok(response);
                }

                throw new Exception("Login fallito");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.InnerException);
            }
        }

        [HttpGet("v1/utenti")]
        public async Task<IActionResult> GetAll([FromQuery] UtenteFilters? request)
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                if (!IsValid(accessToken))
                {
                    return Unauthorized();
                }
                var query = new AllUtenteQuery();
                var handler = UtenteQueryHandlerFactory.Build(query, _business);
                var res = handler.Get();
                res = res.Filter(request).StringSearch(request);
                var count = res.Count();
                res = res.OrderBy(request.Sort).SkipNullable(request.OffSet).TakeNullable(request.Limit);
                var toReturn = ApiResults<UtenteDTO>.GetPagedApiResult(res, count, request);
                return Ok(toReturn);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.InnerException);
            }
        }

        [HttpPost("v1/utenti")]
        public async Task<IActionResult> Post([FromBody] Utente item)
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                if (!IsValid(accessToken))
                {
                    return Unauthorized();
                }
                
                var command = new SaveUtenteCommand(item);

                var handler = UtenteCommandHandlerFactory.Build(command, _business);
                var response = handler.Execute();
                if (response.Success)
                {
                    return Ok(response);
                }

                throw new Exception(response.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.InnerException);
            }
        }

        [HttpGet("v1/utenti/{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                if (!IsValid(accessToken))
                {
                    return Unauthorized();
                }
                var query = new OneUtenteQuery(id);
                var handler = UtenteQueryHandlerFactory.Build(query, _business);
                var res = (UtenteDTO)handler.Get();
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.InnerException);
            }
        }


    }
}
