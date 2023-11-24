using Entregable2_PD.Data.DbUsersTest;
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
        /// <param name="cfg"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static UserModel ValidateToken(ClaimsIdentity identity, IConfiguration cfg)
        {
            try
            {
                if (identity == null || !identity.Claims.Any())
                {
                    Exception exception = new(nameof(identity));
                    throw exception;
                }
                List<UserModel> db = DbUsers.ReturnUsersForTesting(cfg);

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
