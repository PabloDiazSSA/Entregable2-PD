using Entregable2_PD.Tools.Helpers;
using System.Security.Claims;
using System.Text;

namespace Entregable2_PD.Models.DBO.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Jwt
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Issuer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Audience { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static UserModel ValidateToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity == null || !identity.Claims.Any())
                {
                    Exception exception = new(nameof(identity));
                    throw exception;
                }
                List<UserModel> db = new()
                {
                    new UserModel
                    {
                        Email = "administrador@interno.com",
                        Password = "35ffd846c3d5e1d44ee73a37a16d541bbb78f829e6c351a584934cad84ed9fb0",
                        Type = "AUTHORIZED",
                        Role = "ADMIN",
                    },
                    new UserModel
                    {
                        Email = "usuario@interno.com",
                        Password = "7da3c57c0d420776006a6cebe00141f182567842371e21707722928055062689",
                        Type = "AUTHORIZED",
                        Role = "USER",
                    },
                     new UserModel
                    {
                        Email = "administrador@externo.com",
                        Password = "6c78da1c68e521a560d49739475dd9e243e291c617e6a67cc3ee2a5c041cff4e",
                        Type = "AUTHORIZED",
                        Role = "ADMIN",
                    },
                      new UserModel
                    {
                        Email = "usuario@externo.com",
                        Password = "17e434e1a6a31d0ad82d005fa6d631f386b1dc39e5ba1c89eaa4e70fefd64e69",
                        Type = "AUTHORIZED",
                        Role = "USER",
                    },
                };

                var emailClaim = identity.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
                return db.First(x => x.Email == emailClaim);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in validateToken: " + e.ToString());
                throw;
            }
        }
    }
}
