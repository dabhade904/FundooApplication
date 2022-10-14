using BusinessLayer.Interface;
using CommanLayer.Model;
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
using System.Text;

using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteInterfaceBL noteInterfaceBL;
        private readonly FundooContext fundooContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;


        public NoteController(NoteInterfaceBL noteInterfaceBL, FundooContext fundooContext, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.noteInterfaceBL = noteInterfaceBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [HttpPost("Notes")]
        public IActionResult AddNotes(Note note)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = noteInterfaceBL.AddNotes(userId, note);
                if (!result.Equals(null))
                {
                    return this.Ok(new 
                    { 
                        success = true, 
                        message = "Note Added Successfull",
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
        [HttpDelete("DeleteNotes")]
        public IActionResult DeleteNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = noteInterfaceBL.DeleteNotes(userId, noteId);
                if (!result.Equals(null))
                {
                    return this.Ok(new 
                    {
                        success = true, 
                        message = "Note Deleted",
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

        [HttpGet("GetNotes")]
        public IActionResult GetNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = noteInterfaceBL.GetNotes(userId);
                if (!result.Equals(null))
                {
                    return this.Ok(new { 
                        success = true,
                        message = "Retrive all notes",
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
        [HttpPut("UpdateNote")]
        public IActionResult UpdateNotes(long noteId, Note note)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = noteInterfaceBL.UpdateNotes(noteId, userId, note);
                if (!result.Equals(null))
                {
                    return this.Ok(new 
                    {
                        success = true, 
                        message = "Note Update Succesfully",
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
        [HttpPut("PinNote")]
        public IActionResult PinNotes(long noteId)
        {
            try
            {
                var result = noteInterfaceBL.PinNotes(noteId);
                if (!result.Equals(null))
                {
                    return this.Ok(new {
                        success = true, 
                        message = "Pin Note Succesfully",
                        data = result 
                    });
                }
                else
                {
                    return this.BadRequest(new {
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
        [HttpPut("TrashNote")]
        public IActionResult TrashNotes(long noteId)
        {
            try
            {
                var result = noteInterfaceBL.TrashNotes(noteId);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Note Trash Succesfully",
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
        [HttpPut("ArchiveNote")]
        public IActionResult ArchiveNotes(long noteId)
        {
            var result = noteInterfaceBL.ArchiveNotes(noteId);
            if (!result.Equals(null))
            {
                return this.Ok(new
                {
                    success = true,
                    message = "Archive Note succesafully",
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
        [HttpPost("ColorNotes")]
        public IActionResult ColorNotes(long noteId, string color)
        {
            try
            {
                var result = noteInterfaceBL.ColorNotes(noteId, color);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Color Aplay for Notes successfully",
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
        [HttpPost("UploadImage")]
        public IActionResult UploadImage(long noteId, IFormFile file)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = noteInterfaceBL.UploadImage(noteId, userId, file);
                if (!result.Equals(null))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Image Upload successfully",
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
        [HttpGet("GetData")]
        public async Task<IActionResult> GetData()
        {
            var cacheKey = "noteList";
            string serializedLabelList;
            var noteList = new List<NoteEntity>();
            var redisNoteList = await distributedCache.GetAsync(cacheKey);
            if (redisNoteList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisNoteList);
                noteList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedLabelList);
            }
            else
            {
                noteList = await fundooContext.NoteTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(noteList);
                redisNoteList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNoteList, options);
            }
            return Ok(noteList);
        }
    }
}
