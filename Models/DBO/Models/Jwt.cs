﻿using Entregable2_PD.Tools.Helpers;
using System.Security.Claims;

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
                    throw new ArgumentNullException(nameof(identity));
                }
                List<UserModel> db = new()
                {
                    new UserModel
                    {
                        Email = "administrador@interno.com",
                        Password = HelperCryptography.EncriptarPassword("Charizard006*", "12345678900"),
                        Type = "AUTHORIZED",
                        Role = "ADMIN",
                    },
                    new UserModel
                    {
                        Email = "usuario@interno.com",
                        Password = HelperCryptography.EncriptarPassword("Charizard006*", "12345678901"),
                        Type = "AUTHORIZED",
                        Role = "USER",
                    },
                     new UserModel
                    {
                        Email = "administrador@externo.com",
                        Password = HelperCryptography.EncriptarPassword("Charizard006*", "12345678902"),
                        Type = "AUTHORIZED",
                        Role = "ADMIN",
                    },
                      new UserModel
                    {
                        Email = "usuario@externo.com",
                        Password = HelperCryptography.EncriptarPassword("Charizard006*", "12345678903"),
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
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}
