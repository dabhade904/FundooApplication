using BusinessLayer.Interface;
using CommanLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteInterfaceBL noteInterfaceBL;
        private readonly FundooContext fundooContext;
        public NoteController(NoteInterfaceBL noteInterfaceBL, FundooContext fundooContext)
        {
            this.noteInterfaceBL = noteInterfaceBL;
            this.fundooContext = fundooContext;
        }
        [HttpPost("Notes")]
        public IActionResult AddNotes(Note note)
        {
            try
            {
                long userId=Convert.ToInt32(User.Claims.FirstOrDefault(e=>e.Type=="userId").Value);
                var result = noteInterfaceBL.AddNotes(userId,note);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Added Successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
