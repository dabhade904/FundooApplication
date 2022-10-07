using CommanLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserInterfaceRL
    {
      //  private readonly AppSettings appSettings;
        private readonly FundooContext fundooContext;
        private readonly IConfiguration config;
        public UserRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this.config = config;
        }
        public UserEntity UserRegistration(Registration registration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = registration.FirstName;
                userEntity.LastName = registration.LastName;
                userEntity.EmailId = registration.EmailId;
                userEntity.Password = registration.Password;
                fundooContext.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string JwtMethod(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("Jwt:key")]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                tokenKey, SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
                var loginData = fundooContext.UserTable.SingleOrDefault(x => 
                                                        x.EmailId == userLogin.EmailId && 
                                                        x.Password == userLogin.Password);
                if (loginData != null)
                {
                    var token = JwtMethod(loginData.EmailId, loginData.UserId);
                    return token;
                }
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        public string ForgetPassword(string emailId)
        {
            try
            {
                var emailCheck = fundooContext.UserTable.FirstOrDefault(e => e.EmailId == emailId);
                if(emailCheck != null)
                {
                    var takan = JwtMethod(emailCheck.EmailId, emailCheck.UserId);
                    var msmqObjModel = new MSMQModel();
                    msmqObjModel.sendData2Queue(takan);
                    return takan;
                   
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string emailId, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword.Equals(confirmPassword))
                {
                    var passwordResult = fundooContext.UserTable.FirstOrDefault(e => e.EmailId == emailId);
                    passwordResult.Password=newPassword;
                    fundooContext.SaveChanges(); 
                    return true;
                }
                else
                {
                    return false;
                }               
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
