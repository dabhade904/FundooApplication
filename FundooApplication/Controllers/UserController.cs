using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommanLayer.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;

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
      //  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [Route("Register")]
        [HttpPost]
        public IActionResult Regispostration(Registration registration)
        {
            try
            {
                var result = userBL.UserRegistration(registration);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "User Registration Succesfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User Registration UnSuccesfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [AllowAnonymous] 
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(UserLogin model) 
        {
            try
            {
                var result = userBL.Login(model);
                if(result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Login Succesfull",
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
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Email Send Successfully",
                        tokan = result
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
    }
}
