using BusinessLayer.Interface;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LableController : ControllerBase
    {
        private readonly LableInterfaceBL lableInterfaceBL;
        private readonly FundooContext fundooContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;


        public LableController(LableInterfaceBL lableInterfaceBL, FundooContext fundooContext, IMemoryCache memoryCache,IDistributedCache distributedCache)
        {
            this.lableInterfaceBL = lableInterfaceBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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
        [HttpDelete("DeleteLabel")]
        public IActionResult RemoveLable(long labelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result=lableInterfaceBL.RemoveLable(userId,labelId);
                if (!result.Equals(null) && !result.Equals(0))
                {
                    return Ok(new
                    {
                        success = true,
                        message = "label Removed",
                        data = result
                    }) ;
                }
                else
                {
                    return BadRequest(new
                    {
                        success=false,
                        message="Lable not found"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetData")]
        public async Task<IActionResult> GetData()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var labelList = new List<LableEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelList = JsonConvert.DeserializeObject<List<LableEntity>>(serializedLabelList);
            }
            else
            {
                labelList = await fundooContext.LableTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(labelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(labelList);
        }
    }
}
