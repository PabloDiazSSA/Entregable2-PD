using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entregable2_PD.Models.DBO.DTO;
using Entregable2_PD.Models.Response;
using Entregable2_PD.Models.DBO.Models;
using System.Security.Claims;

namespace Entregable2_PD.Api.Controllers
{
    [Authorize(Policy = "AUTHORIZED")]
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        [HttpPost("Set")]
        public async Task<clsResponse<dynamic>> SetForm(FormDto form)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            foreach (var claim in identity.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }
            var rToken = Jwt.validateToken(identity);
            var user = rToken;

            if (user == null)
            {
                return new clsResponse<dynamic> { Error = true, ErrorMessage = "Unauthenticated" };
            }

            if (user.Role.ToUpper() != "ADMIN")
            {
                return new clsResponse<dynamic> { Error = true, ErrorMessage = "Unauthorized" };
            }

            return new clsResponse<dynamic> { Error = true, Data = form };
        }


    }
}
