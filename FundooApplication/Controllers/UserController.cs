using BusinessLayer.Interface;
using CommanLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistration userRegistration)
        {
            try
            {
                var result = userBL.UserRegitrations(userRegistration);
                if(result != null)
                {
                    return this.Ok(new {success=true,message="User registration succesfull",data=result});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User registration Unsuccesfull" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }       
    }
}
