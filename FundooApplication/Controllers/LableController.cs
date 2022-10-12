﻿using BusinessLayer.Interface;
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
                        message="Note Deleted",
                        data=result
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success=false,
                        message="Something went wrong"
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