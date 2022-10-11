using BusinessLayer.Interface;
using CommanLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class UserController : ControllerBase
    {
        private readonly IUserInterfaceBL userBL;
        public UserController(IUserInterfaceBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult Registration(Registration registration)
        {
            try
            {
                var result = userBL.UserRegistration(registration);
                if (!result.Equals(null))
                {
                    return this.Ok(new { success = true, message = "User Registration Successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User Registration UnSuccessfull" });
                }
            }
            catch (Exception)
            {
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
                    return this.Ok(new
                    {
                        success = true,
                        message = "Login Successfull",
                        tokan = result
                    });
                }
                else
                {
                    return this.Unauthorized(new
                    {
                        Success = false,
                        message = "Invalid Email or Password",
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
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
                    return this.Ok(new
                    {
                        success = true,
                        message = "Email Send Successfully",
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "EMail has not send",
                    });
                }
            }
            catch (Exception)
            {
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
                if (!user.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Password Reset successfully",
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Invalid Email ",
                    });
                }               
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
