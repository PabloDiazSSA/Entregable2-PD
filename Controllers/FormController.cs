﻿using Microsoft.AspNetCore.Authorization;
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
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("Set")]
        public ClsResponse<dynamic> SetForm(FormDto form)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity is null)
            {
                return new ClsResponse<dynamic> { Error = true, ErrorMessage = "Unauthenticated" };
            }
            foreach (var claim in identity.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }
            var rToken = Jwt.validateToken(identity);
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


    }
}