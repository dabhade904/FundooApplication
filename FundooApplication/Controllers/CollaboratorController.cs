using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommanLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Linq;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly CollaboratorInterfaceBL collaboratorInterfaceBL;
        public CollaboratorController(CollaboratorInterfaceBL collaboratorInterfaceBL)
        {
            this.collaboratorInterfaceBL = collaboratorInterfaceBL;
        }
        [HttpPost("Collaborator")]
        public IActionResult CreateCollaborator(long noteId, string emailId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = collaboratorInterfaceBL.CreateCollaborator(userId,noteId, emailId);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Collaborator Added Successfull",
                        data = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "something went wrong"
                    });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpDelete("RemoveCollaborator")]
        public IActionResult RemoveCollaborator(long collabId)
        {
            try
            {
                var result = collaboratorInterfaceBL.RemoveCollaborator(collabId);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Collaborator Remove Successfull",
                        data = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "something went wrong"
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
