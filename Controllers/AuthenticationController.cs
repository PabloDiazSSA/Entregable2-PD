﻿using Microsoft.AspNetCore.Mvc;
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
using System.Security.Principal;

namespace Entregable2_PD.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DB _db;
        private readonly IConfiguration _config;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="config"></param>
        public AuthenticationController(DB db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        /// <summary>
        /// Registro de usuarios
        /// </summary>
        /// <param name="signupUserDto">SignupUserDto</param>
        /// <returns>clsResponse</returns>
        [HttpPost("Signup")]
        public async Task<ClsResponse<dynamic>> SignUp(SignupUserDto signupUserDto)
        {
            UserModel user = new();
            user.Action = "Signup";
            user.Salt = HelperCryptography.GenerateSalt();
            if (signupUserDto is null || signupUserDto.Password is null)
            {
                return new ClsResponse<dynamic>() { Error= true , ErrorMessage = "Error en registro" };
            }
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
        /// <param name="userDto"></param>
        /// <returns>JWT token</returns>
        [HttpPost("Authenticate")]
        public ClsResponse<string> Login(UserDto userDto)
        {
            ClsResponse<string> cls = new ClsResponse<string>();
            cls.Error = true;
            cls.Message = "Falló la Autenticacion";
            try
            {
                if (userDto is null || userDto.Email is null)
                {
                    return cls;
                }
                UserModel userIntA = new UserModel()
                {
                    Name = userDto.Email.Split('@')[0],
                    Email = _config.GetSection("CRED:EmailIntA").Value,
                    Password = HelperCryptography.EncriptarPassword(_config.GetSection("CRED:PwdIntA").Value, _config.GetSection("CRED:SaltIntA").Value),
                    Salt = _config.GetSection("CRED:SaltIntA").Value,
                    Type = "AUTHORIZED", //(userDto.Email.Contains("ssamexico") ? "AUTHORIZED" : "UNAUTHORIZED"),
                    Role = (userDto.Email.Contains("administrador") ? "ADMIN" : "USER"),
                    RegDate = DateTime.Now,
                };

                UserModel userIntU = new UserModel()
                {
                    Name = userDto.Email.Split('@')[0],
                    Email = _config.GetSection("CRED:EmailIntU").Value,
                    Password = HelperCryptography.EncriptarPassword(_config.GetSection("CRED:PwdIntU").Value, _config.GetSection("CRED:SaltIntU").Value),
                    Salt = _config.GetSection("CRED:SaltIntU").Value,
                    Role = (userDto.Email.Contains("administrador") ? "ADMIN" : "USER"),
                    RegDate = DateTime.Now,
                };

                UserModel userExtA = new UserModel()
                {
                    Name = userDto.Email.Split('@')[0],
                    Email = _config.GetSection("CRED:EmailExtA").Value,
                    Password = HelperCryptography.EncriptarPassword(_config.GetSection("CRED:PwdExtA").Value, _config.GetSection("CRED:SaltExtA").Value),
                    Salt = _config.GetSection("CRED:SaltExtA").Value,
                    Role = (userDto.Email.Contains("administrador") ? "ADMIN" : "USER"),
                    RegDate = DateTime.Now,
                };

                UserModel userExtU = new UserModel()
                {
                    Name = userDto.Email.Split('@')[0],
                    Email = _config.GetSection("CRED:EmailExtU").Value,
                    Password = HelperCryptography.EncriptarPassword(_config.GetSection("CRED:PwdExtU").Value, _config.GetSection("CRED:SaltExtU").Value),
                    Salt = _config.GetSection("CRED:SaltExtU").Value,
                    Role = (userDto.Email.Contains("administrador") ? "ADMIN" : "USER"),
                    RegDate = DateTime.Now,
                };


                var DbUsers = new List<UserModel>
                {
                    userIntA,
                    userIntU,
                    userExtA,
                    userExtU
                };

                UserModel DbUser = DbUsers.First(x => x.Email == userDto.Email.ToLower());
                if (DbUser is null || DbUser.Password is null || DbUser.Salt is null || userDto.Password is null)
                {
                    cls.Message = $"Credenciales inválidas. ";
                    return cls;
                }
              

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

                //Ciframos de nuevo para comparar
                byte[] temporal = HelperCryptography.EncriptarPassword(userDto.Password, DbUser.Salt);

                //Comparamos los arrays para comprobar si el cifrado es el mismo
                if (!HelperCryptography.compareArrays(DbUser.Password, temporal))
                {
                    return cls;
                }


                var tokenStr = GenerateToken(DbUser);
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
                if (administrator is null || administrator.Name is null || administrator.Email is null || administrator.Type is null || administrator.Role is null)
                {
                    throw new ArgumentException("Error generando token");
                }

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
                if (jwt is null || jwt.Key is null)
                {
                    throw new ArgumentException("Error generando token");
                }
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
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                throw;
            }

        }
    }
}