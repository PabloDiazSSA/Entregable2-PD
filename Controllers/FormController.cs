using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entregable2_PD.Models.DBO.DTO;
using Entregable2_PD.Models.Response;
using Entregable2_PD.Models.DBO.Models;
using System.Security.Claims;

namespace Entregable2_PD.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Policy = "AUTHORIZED")]
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IConfiguration _config;
        private readonly ILogger<FormController> _logger;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public FormController(IConfiguration config, ILogger<FormController> logger)
        {
            _config = config;
            _logger = logger;
        }
        /// <summary>
        /// Metodo de ejemplos de seguridad usando regex y datannotations en campos de un formulario
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("Set")]
        public ClsResponse<dynamic> SetForm(FormDto form)
        {
            try
            {
                if (HttpContext.User.Identity is not ClaimsIdentity identity)
                {
                    return new ClsResponse<dynamic> { Error = true, ErrorMessage = "Unauthenticated" };
                }
                foreach (var claim in identity.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                }
                var rToken = Jwt.ValidateToken(identity, _config);
                var user = rToken;

                if (user is null || user.Role is null)
                {
                    return new ClsResponse<dynamic> { Error = true, ErrorMessage = "Unauthenticated" };
                }

                if (user.Role.ToUpper() != "ADMIN")
                {
                    return new ClsResponse<dynamic> { Error = true, ErrorMessage = "Unauthorized" };
                }

                return new ClsResponse<dynamic> { Error = true, Data = form };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message, ex);
                return new ClsResponse<dynamic> { Error = true, ErrorMessage = "Ocurrio un error, reintentarlo más tarde" };

            }
        
        }


    }
}
