using BusinessLayer.Interface;
using CommanLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

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
      
        [Route("Register")]
        [HttpPost]
        public IActionResult Registration(UserRegistration userRegistration)
        {
            try
            {
                var result = userBL.UserRegitrations(userRegistration);
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
    }
}
