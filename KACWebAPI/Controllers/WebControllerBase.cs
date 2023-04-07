using CQRSSAmple.Domain.Utility;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace KVMWebAPI.Controllers
{
    public class WebControllerBase : ControllerBase
    {
        internal enum AbilitazioneRuoliEnum
        {
            User = 10,
            PowerUser = 20,
            Admin = 30,
            SuperAdmin = 40
        }

        internal bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }

        internal Guid? UserID()
        {
            var user = ((ClaimsIdentity)User.Identity).Claims;
            return Guid.Parse(user.FirstOrDefault(x => x.Type == "ID").Value);
        }
        internal string Endpoint() => (HttpContext.GetEndpoint() as RouteEndpoint).DisplayName;

        protected async Task<dynamic> JsonBody()
        {
            var result = await JsonDocument.ParseAsync(Request.Body);
            return new JsonDynamicObject
            {
                RealObject = result.RootElement
            };
        }

    }
}