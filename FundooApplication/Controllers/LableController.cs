using BusinessLayer.Interface;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserController> logger;
        public LableController(LableInterfaceBL lableInterfaceBL, FundooContext fundooContext, IMemoryCache memoryCache,IDistributedCache distributedCache, ILogger<UserController> logger)
        {
            this.lableInterfaceBL = lableInterfaceBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.logger = logger;   
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
                    logger.LogInformation("Lable Created successfully");
                    return Ok(new
                    {
                        success = true,
                        message = "Lable Created successfully",
                        data = result
                    }) ;
                }
                else
                {
                    logger.LogInformation("Something went wrong");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Something went wrong"
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Lable Deleted");
                    return Ok(new
                    {
                        success=true,
                        message="Lable Deleted",
                        data=result
                    });
                }
                else
                {
                    logger.LogInformation("Lable Not Found");
                    return BadRequest(new
                    {
                        success=false,
                        message="Lable Not Found"
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Label Fetched sucessfully");
                    return Ok(new
                    {
                        success = true,
                        message = "Label Fetched sucessfully",
                        data = result
                    });
                }
                else
                {
                    logger.LogInformation("Data Not Found");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Data Not Found"
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Lable Edited");
                    return Ok(new
                    {
                        success = true,
                        message = "Lable Edited",
                        data = result
                    });
                }
                else
                {
                    logger.LogInformation("someting went wrong");
                    return BadRequest(new
                    {
                        success = false,
                        message = "someting went wrong"
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Label Removed");
                    return Ok(new
                    {
                        success = true,
                        message = "Label Removed",
                        data = result
                    }) ;
                }
                else
                {
                    logger.LogInformation("Lable not found");
                    return BadRequest(new
                    {
                        success=false,
                        message="Lable not found"
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
