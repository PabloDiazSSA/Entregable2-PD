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
    [Authorize(Policy = "AUTHORIZED")]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IConfiguration _config;
        public static string A { get; set; } = string.Empty;
        public static string AC { get; set; } = string.Empty;
        public static byte[] AE { get; set; }
        public static string B { get; set; } = string.Empty;
        public static string BC { get; set; } = string.Empty;

        public CardController(IConfiguration config)
        {
            _config = config;   
        }

        [HttpPost("Set")]
        public async Task<clsResponse<dynamic>> SetCard(CardDto card)
        {
            card.Comment = Tools.Helpers.SanitizeString.RemoveHtml(card.Comment);
            clsResponse<dynamic> response = new clsResponse<dynamic>();
            response.Error = true;
            response.ErrorMessage = "Can not set credit card";
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Log the user's claims
            foreach (var claim in identity.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            try
            {
                var rToken = Jwt.validateToken(identity);
                var user = rToken;

                if (user == null)
                {
                    response.ErrorMessage = "Unauthenticated";
                    return response;
                }

                if (user.Role.ToUpper() != "ADMIN")
                {
                    response.ErrorMessage = "Unauthorized";
                    return response;
                }
                response.Error = false;
                //Get Key from config to encrypt and decript
                var encrypt = _config.GetSection("Encrypt").Get<EncritpAes>();
                byte[] key = Encoding.UTF8.GetBytes(encrypt.Key);
                byte[] iv = Encoding.UTF8.GetBytes(encrypt.Iv);

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

                response.Data = $"Masked:{NM} AC:{ AC } BC:{BC}";
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
