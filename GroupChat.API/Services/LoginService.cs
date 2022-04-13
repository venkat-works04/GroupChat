using GroupChat.API.DataModels;
using GroupChat.API.Interfaces;
using GroupChat.ViewModels.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GroupChat.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly GroupChatDbContext _dbContext;
        private readonly ILogger<LoginService> _logger;
        private readonly IConfiguration _configuration;
        public LoginService(GroupChatDbContext dbContext, ILogger<LoginService> logger, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._configuration = configuration;
        }

        public async Task<LoginResponseModel> ValidateLogin(LoginModel loginRequest)
        {
            LoginResponseModel objResponseModel = new LoginResponseModel();
            try
            {
                var userLogin =  _dbContext.UserLogins.Where(u => u.UserName == loginRequest.UserName.Trim() && u.Password == loginRequest.Password).FirstOrDefault();

                if (userLogin != null)
                {
                    if (userLogin.IsActive)
                    {
                        objResponseModel.UserInformation.UserId = userLogin.UserId;
                        objResponseModel.UserInformation.FullName = _dbContext.Members.Where(m=>m.MemberId == userLogin.MemberId).FirstOrDefault().FullName;
                        objResponseModel.UserInformation.MemberId = userLogin.MemberId;
                        objResponseModel.Status = 1;
                        objResponseModel.Message = "Success";
                        objResponseModel.JWToken = GenerateJSONWebToken(objResponseModel.UserInformation);

                        userLogin.LastLogin = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        objResponseModel.Status = 3;
                        objResponseModel.Message = "Please contact the administrator";
                    }
                }
                else
                {
                    objResponseModel.Status = 2;
                    objResponseModel.Message = "Invalid Username or Password";
                }

                return objResponseModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return objResponseModel;
            }
        }

        private string GenerateJSONWebToken(LoginUserInformation userInformation)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTTokens:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("UserId", userInformation.UserId.ToString()),
                new Claim("MemberId", userInformation.MemberId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_configuration["JWTTokens:Issuer"],
                                             _configuration["JWTTokens:Issuer"],
                                             claims,
                                             expires: DateTime.Now.AddHours(12),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}