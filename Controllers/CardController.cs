using Microsoft.AspNetCore.Mvc;
using Entregable2_PD.Models.DBO.DTO;
using Entregable2_PD.Models.Response;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Entregable2_PD.Tools.Converters;
using System.Security.Claims;
using Entregable2_PD.Models.DBO.Models;
using Entregable2_PD.Tools;

namespace Entregable2_PD.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Policy = "AUTHORIZED")]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IConfiguration _config;
        /// <summary>
        /// 
        /// </summary>
        public static string A { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public static string AC { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public static byte[]? AE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static string B { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public static string BC { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public CardController(IConfiguration config)
        {
            _config = config;   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        [HttpPost("Set")]
        public ClsResponse<dynamic> SetCard(CardDto card)
        {
            ClsResponse<dynamic> response = new ClsResponse<dynamic>();
            response.Error = true;
            response.ErrorMessage = "Can not set credit card";
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity is null)
            {
                return response;
            }
            // Log the user's claims
            foreach (var claim in identity.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            try
            {
                var rToken = Jwt.ValidateToken(identity);
                var user = rToken;

                if (user == null)
                {
                    response.ErrorMessage = "Unauthenticated";
                    return response;
                }

                if (user.Role is null)
                {
                    return response;
                }
                if (user.Role.ToUpper() != "ADMIN")
                {
                    response.ErrorMessage = "Unauthorized";
                    return response;
                }
                if (card.Comment is null)
                {
                    return response;
                }
                card.Comment = Tools.Helpers.SanitizeString.RemoveHtml(card.Comment);

                response.Error = false;
                //Get Key from config to encrypt and decript
                var encrypt = _config.GetSection("Encrypt").Get<EncritpAes>();
                if (encrypt.Key is null || encrypt.Iv is null)
                {
                    return response;
                }
                byte[] key = Encoding.UTF8.GetBytes(encrypt.Key);
                byte[] iv = Encoding.UTF8.GetBytes(encrypt.Iv);

                if (card.CardNumber is null)
                {
                    return response;
                }
                //Save A
                A = card.CardNumber;
                //Mask credit Card Number (A)
                string NM = CardNumberTo.MaskCreditCard(A);
                //Encode credit card number with SHA256 (A) And Save like AC 
                AC = HelperCryptography.EncodeSHA256Hash(A);
                //Encrypt credit card number with AES256 using hard key (A) And Save like AE
                AE = HelperCryptography.EncryptStringToBytes_Aes(A, key, iv);
                //Decrypt credit card number with AES256 using hard key (AE) and Save like B
                B = HelperCryptography.DecryptStringToBytes_Aes(AE, key, iv);
                //Encode credit card number with SHA256 (B) and save like BC
                BC = HelperCryptography.EncodeSHA256Hash(B);
                //Compare Hashes AC with BC
                if (AC != BC)
                {
                    return response;
                }

                response.Data = $"Masked:{NM} AC:{AC} BC:{BC}";
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return response;
            }

        }


    }
}
