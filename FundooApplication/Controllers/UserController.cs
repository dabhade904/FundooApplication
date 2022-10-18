using BusinessLayer.Interface;
using CommanLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class UserController : ControllerBase
    {
        private readonly IUserInterfaceBL userBL;
        private readonly ILogger<UserController> logger;
        public UserController(IUserInterfaceBL userBL, ILogger<UserController> logger)
        {
            this.userBL = userBL;
            this.logger = logger;
        }
        [HttpPost("Register")]
        public IActionResult Registration(Registration registration)
        {
            try
            {
                var result = userBL.UserRegistration(registration);
                if (!result.Equals(null))
                {
                    logger.LogInformation("User Registration Succesfull");
                    return this.Ok(new
                    { 
                        success = true,
                        message = "User Registration Successfull",
                        data = result 
                    });
                }
                else
                {
                    logger.LogInformation("User Registration UnSuccesfull");
                    return this.BadRequest(new 
                    {
                        success = false,
                        message = "User Registration UnSuccessfull" 
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(UserLogin model) 
        {
            try
            {
                var result = userBL.Login(model);
                if (!result.Equals(null))
                {
                    logger.LogInformation("User Login Succesfull");
                    return this.Ok(new
                    {
                        success = true,
                        message = "Login Successfull",
                        tokan = result
                    });
                }
                else
                {
                    logger.LogInformation("User Login UnSuccesfull");
                    return this.Unauthorized(new
                    {
                        Success = false,
                        message = "Invalid Email or Password",
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string emailId)
        {
            try
            {
                var result = userBL.ForgetPassword(emailId);
                if (!result.Equals(null))
                {
                    logger.LogInformation("Email Send Successfully");
                    return this.Ok(new
                    {
                        success = true,
                        message = "Email Send Successfully",
                    });
                }
                else
                {
                    logger.LogInformation("EMail has not send");
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "EMail has not send",
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string newPassword,string confirmPassword)
        { 
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var user = userBL.ResetPassword(email,newPassword,confirmPassword);
                if (!user.Equals(null)&& !user.Equals(false))
                {
                    logger.LogInformation("Password Reset successfully");
                    return this.Ok(new
                    {
                        success = true,
                        message = "Password Reset successfully",
                    });
                }
                else
                {
                    logger.LogInformation("Invalid Password");
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Invalid Password ",
                    });
                }               
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
