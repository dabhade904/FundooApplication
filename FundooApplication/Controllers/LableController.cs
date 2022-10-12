using BusinessLayer.Interface;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LableController : ControllerBase
    {
        private readonly LableInterfaceBL lableInterfaceBL;
        public LableController(LableInterfaceBL lableInterfaceBL)
        {
            this.lableInterfaceBL = lableInterfaceBL;
        }
        [HttpPost("CreateLable")]
        public IActionResult CreateLable(long noteId,string lableName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result= lableInterfaceBL.CreateLable(userId, noteId, lableName);
                if (!result.Equals(null))
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Lable Created successfully",
                        data = result
                    }) ;
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Something went wrong"

                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete("DeleteLable")]
        public IActionResult DeleteLable(string lableName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result =lableInterfaceBL.DeleteLable(userId,lableName);
                if (!result.Equals(null))
                {
                    return Ok(new
                    {
                        success=true,
                        message="Lable Deleted",
                        data=result
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success=false,
                        message="Lable Not Found"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("RetriveAllLables")]
        public IActionResult GetAllLables(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = lableInterfaceBL.GetAllLable(noteId, userId);
                if (!result.Equals(null) && !result.Count.Equals(0))
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Label Fetched sucessfully",
                        data = result
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Data Not Found"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("EditLable")]
        public IActionResult EditLable(string oldLableName,string newLableName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = lableInterfaceBL.EditLable(userId,oldLableName, newLableName);
                if (!result.Equals(null) && !result.Equals(0))
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Lable Edited",
                        data = result
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "someting went wrong"
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
