using Entregable2_PD.Models.DBO.Models;

namespace Entregable2_PD.Data.DbUsersTest
{
    /// <summary>
    /// 
    /// </summary>
    public class DbUsers
    {
        /// <summary>
        /// 
        /// </summary>
        protected DbUsers()
        {
                
        }
        /// <summary>
        /// retorna dbusers
        /// </summary>
        /// <returns></returns>
        public static List<UserModel> ReturnUsersForTesting(IConfiguration cfg)
        {
            try
            {
                var _config = cfg;
                UserModel userIntA = new()
                {
                    Name = _config.GetSection("CRED:EmailIntA").Value.Split('@')[0],
                    Email = _config.GetSection("CRED:EmailIntA").Value,
                    Password = _config.GetSection("CRED:PwdIntA").Value,
                    Salt = _config.GetSection("CRED:SaltIntA").Value,
                    Type = _config.GetSection("CRED:EmailIntA").Value.Contains("interno") ? "AUTHORIZED" : "UNAUTHORIZED",
                    Role = _config.GetSection("CRED:EmailIntA").Value.Contains("administrador") ? "ADMIN" : "USER",
                    RegDate = DateTime.Now,
                };

                UserModel userIntU = new()
                {
                    Name = _config.GetSection("CRED:EmailIntU").Value.Split('@')[0],
                    Email = _config.GetSection("CRED:EmailIntU").Value,
                    Password = _config.GetSection("CRED:PwdIntU").Value,
                    Salt = _config.GetSection("CRED:SaltIntU").Value,
                    Type = _config.GetSection("CRED:EmailIntU").Value.Contains("interno") ? "AUTHORIZED" : "UNAUTHORIZED",
                    Role = _config.GetSection("CRED:EmailIntU").Value.Contains("administrador") ? "ADMIN" : "USER",
                    RegDate = DateTime.Now,
                };

                UserModel userExtA = new()
                {
                    Name = _config.GetSection("CRED:EmailExtA").Value.Split('@')[0],
                    Email = _config.GetSection("CRED:EmailExtA").Value,
                    Password = _config.GetSection("CRED:PwdExtA").Value,
                    Salt = _config.GetSection("CRED:SaltExtA").Value,
                    Type = "AUTHORIZED",
                    Role = _config.GetSection("CRED:EmailExtA").Value.Contains("administrador") ? "ADMIN" : "USER",
                    RegDate = DateTime.Now,
                };

                UserModel userExtU = new()
                {
                    Name = _config.GetSection("CRED:EmailExtU").Value.Split('@')[0],
                    Email = _config.GetSection("CRED:EmailExtU").Value,
                    Password = _config.GetSection("CRED:PwdExtU").Value,
                    Salt = _config.GetSection("CRED:SaltExtU").Value,
                    Type = "UNAUTHORIZED",
                    Role = _config.GetSection("CRED:EmailExtU").Value.Contains("administrador") ? "ADMIN" : "USER",
                    RegDate = DateTime.Now,
                };
                return new List<UserModel>
                {
                    userIntA,
                    userIntU,
                    userExtA,
                    userExtU
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
           

        }
    }
}
