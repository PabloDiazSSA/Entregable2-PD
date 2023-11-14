using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Entregable2_PD.Data;
using Entregable2_PD.Models.DBO.DTO;
using Entregable2_PD.Models.DBO.Models;
using Entregable2_PD.Models.Response;
using Entregable2_PD.Tools;
using Entregable2_PD.Tools.Converters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Entregable2_PD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DB _db;
        private IConfiguration _config;
        public AuthenticationController(DB db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        /// <summary>
        /// Registro de usuarios
        /// </summary>
        /// <param name="user"></param>
        /// <returns>clsResponse<dynamic></returns>
        [HttpPost("Signup")]
        public async Task<clsResponse<dynamic>> SignUp(SignupUserDto signupUserDto)
        {
            UserModel user = new();
            user.Action = "Signup";
            user.Salt = HelperCryptography.GenerateSalt();
            user.Password = HelperCryptography.EncriptarPassword(signupUserDto.Password, user.Salt);
            user.Name = signupUserDto.Name;
            user.LastName = signupUserDto.LastName;
            user.Email = signupUserDto.Email;
            user.Type = signupUserDto.Type;

            return await _db.ExecuteSpAsync<UserModel>(_config.GetSection("SP:Auth").Value, ModelToParams.GetParams<UserModel>(user));
        }

        /// <summary>
        /// Obtiene el token para autorizar la comunicacion dependiendo los permisos del usuario
        /// </summary>
        /// <param name="UserDto"></param>
        /// <returns>JWT token</returns>
        [HttpPost("Authenticate")]
        public async Task<clsResponse<string>> Login(UserDto userDto)
        {
            clsResponse<string> cls = new clsResponse<string>();
            cls.Error = true;
            cls.Message = "Falló la Autenticacion";
            try
            {
                var U = _config.GetSection("CRED:EmailIntA").Value;
                var P = _config.GetSection("CRED:PwdIntA").Value;
                var S = _config.GetSection("CRED:SaltIntA").Value;
                var UT = _config.GetSection("CRED:EmailIntU").Value;
                var PT = _config.GetSection("CRED:PwdIntU").Value;
                var ST = _config.GetSection("CRED:SaltIntU").Value;

                var UE = _config.GetSection("CRED:EmailExtA").Value;
                var PE = _config.GetSection("CRED:PwdExtA").Value;
                var SE = _config.GetSection("CRED:SaltExtA").Value;
                var UTE = _config.GetSection("CRED:EmailExtU").Value;
                var PTE = _config.GetSection("CRED:PwdExtU").Value;
                var STE = _config.GetSection("CRED:SaltExtU").Value;

                if (userDto.Email.ToLower() != U || userDto.Password != P)
                {
                    if (userDto.Email.ToLower() != UT || userDto.Password != PT)
                    {
                        if (userDto.Email.ToLower() != UE || userDto.Password != PE)
                        {
                            if (userDto.Email.ToLower() != UTE || userDto.Password != PTE)
                            {
                                cls.Message = $"Credenciales inválidas. ";
                                return cls;
                            }
                        }
                    }
                }
                //REspusta de bd con dato de usuario
                UserModel userModel = new UserModel()
                {
                    Id = 0,
                    Name = (userDto.Email).Split('@')[0],
                    Email = userDto.Email,
                    Password = HelperCryptography.EncriptarPassword(userDto.Password, (userDto.Email.Contains("administrador") ? (userDto.Email.Contains("ssamexico") ? S : SE) : (userDto.Email.Contains("ssamexico") ? ST : STE))),
                    Salt = (userDto.Email.Contains("administrador") ? (userDto.Email.Contains("ssamexico") ? S : SE) : (userDto.Email.Contains("ssamexico") ? ST : STE)),
                    Type = "AUTHORIZED", //(userDto.Email.Contains("ssamexico") ? "AUTHORIZED" : "UNAUTHORIZED"),
                    Role = (userDto.Email.Contains("administrador") ? "ADMIN" : "USER"),
                    RegDate = DateTime.Now,

                };

#if !DEBUG
                UserModel userModel = new UserModel();
                userModel.Action = "Authenticate";
                userModel.Email = userDto.Email;
                var result = await _db.ExecuteSpAsync<UserModel>(_config.GetSection("SP:Auth").Value, ModelToParams.GetParams<UserModel>(userModel));
                if (result.Error)
                {
                    cls.Message = result.Message;
                    return cls;
                }

                userModel = result.Data;
#endif
                //Debemos comparar con la base de datos el password haciendo de nuevo el cifrado con cada salt de usuario
                byte[] passUsuario = userModel.Password;
                string salt = userModel.Salt;
                //Ciframos de nuevo para comparar
                byte[] temporal = HelperCryptography.EncriptarPassword(userDto.Password, salt);

                //Comparamos los arrays para comprobar si el cifrado es el mismo
                if(!HelperCryptography.compareArrays(passUsuario, temporal))
                {
                    return cls;
                }


                var tokenStr = GenerateToken(userModel);
                cls.Data = null;
                cls.Error = false;
                cls.Message = "Success";
                cls.Token = tokenStr;

                return cls;
            }
            catch (Exception ex)
            {
                string msg = "No se pudo Loguear favor de reintentar más tarde.";
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                cls.ErrorMessage = msg;
                return cls;
                throw;
            }
        }

        private string GenerateToken(UserModel administrator)
        {
            try
            {
                var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Name, administrator.Name),
                new Claim(ClaimTypes.Email, administrator.Email),
                new Claim("Type", administrator.Type),
                new Claim("Role", administrator.Role)
            };
                var jwt = _config.GetSection("JWT").Get<Jwt>();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config.GetSection("JWT:expirationToken").Value)),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(securityToken);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
