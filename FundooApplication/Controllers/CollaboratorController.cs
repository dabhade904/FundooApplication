using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommanLayer.Model;
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
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly CollaboratorInterfaceBL collaboratorInterfaceBL;
        private readonly FundooContext fundooContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<UserController> logger;
        public CollaboratorController(CollaboratorInterfaceBL collaboratorInterfaceBL, FundooContext fundooContext, IMemoryCache memoryCache, IDistributedCache distributedCache, ILogger<UserController> logger)
        {
            this.collaboratorInterfaceBL = collaboratorInterfaceBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.logger = logger;
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
                    logger.LogInformation("Collaborator Added Successfully");
                    return this.Ok(new
                    {
                        success = true,
                        message = "Collaborator Added Successfully",
                        data = result
                    });
                }
                else
                {
                    logger.LogInformation("something went wrong");
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "something went wrong"
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Collaborator Removed");
                    return this.Ok(new
                    {
                        success = true,
                        message = "Collaborator Removed",
                        data = result
                    });
                }
                else
                {
                    logger.LogInformation("something went wrong");
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "something went wrong"
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        [HttpGet("GetDetails")]
        public IActionResult  RetriveDetails(long noteId)
        {
            try
            {
                var result= collaboratorInterfaceBL.RetriveDetails(noteId);
                if (!result.Equals(null)&&!result.Count.Equals(0))
                {
                    logger.LogInformation("Data Fetched");
                    return this.Ok(new
                    {
                        success = true,
                        message = "Data Fetched ",
                        data = result
                    });
                }
                else
                {
                    logger.LogInformation("Data Not Found");
                    return this.BadRequest(new
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
        [HttpGet("GetData")]
        public async Task<IActionResult> GetData()
        {
            var cacheKey = "noteList";
            string serializedLabelList;
            var collabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedLabelList);
            }
            else
            {
                collabList = await fundooContext.CollaboratorTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(collabList);
        }
    }
}
