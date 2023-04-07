using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

using KVMWebAPI.Infrastructure.Utility.RequestFilters;

using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using KACWebAPI.Domain.Command.Commands.Ruolo;
using KACWebAPI.Business.Concrete;
using KACCloudContextLibrary.Domain.EntityNoDB;
using KACWebAPI.Business.Abstract;
using KACCloudContextLibrary.Domain.Entity;
using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACWebAPI.Domain.Query.Queries.Ruolo.Concrete;
using KACWebAPI.Domain.Query.Queries.Ruolo.Handler;
using KVMWebAPI.Domain.Query.Queries.Allarme.Concrete;
using Microsoft.AspNetCore.Authentication;

namespace KVMWebAPI.Controllers
{
    [Authorize]
    [Route("")]
    public class RuoloController : WebControllerBase
    {

        private readonly RuoloBusiness _business;

        public RuoloController(IRuoloBusiness business)
        {
            _business = (RuoloBusiness)business;
        }


        [HttpGet("v1/ruoli")]
        public async Task<IActionResult> GetAll([FromQuery] RuoloFilters? request)
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                if (!IsValid(accessToken))
                {
                    return Unauthorized();
                }
                var query = new AllRuoloQuery();
                var handler = RuoloQueryHandlerFactory.Build(query, _business);
                var res = handler.Get();
                res = res.Filter(request).StringSearch(request);
                var count = res.Count();
                res = res.OrderBy(request.Sort).SkipNullable(request.OffSet).TakeNullable(request.Limit);
                var toReturn = ApiResults<RuoloDTO>.GetPagedApiResult(res, count, request);
                return Ok(toReturn);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.InnerException);
            }
        }

        [HttpPost("v1/ruoli")]
        public async Task<IActionResult> Post([FromBody] Ruolo item)
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                if (!IsValid(accessToken))
                {
                    return Unauthorized();
                }
                
                var command = new SaveRuoloCommand(item);

                var handler = RuoloCommandHandlerFactory.Build(command, _business);
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

        [HttpGet("v1/ruoli/{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                if (!IsValid(accessToken))
                {
                    return Unauthorized();
                }
                var query = new OneRuoloQuery(id);
                var handler = RuoloQueryHandlerFactory.Build(query, _business);
                var res = (RuoloDTO)handler.Get();
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.InnerException);
            }
        }


    }
}
