using CommanLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Collections.Generic;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
namespace RepositoryLayer.Service
{
    public class NoteRL : NoteInterfaceRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration config;
        public NoteRL(FundooContext fundooContext, IConfiguration config )
        {
            this.fundooContext = fundooContext;
            this.config = config;
        }
        public NoteEntity AddNotes(long userId, Note model)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.title = model.title;
                noteEntity.discription = model.discription;
                noteEntity.reminder = model.reminder;
                noteEntity.color = model.color;
                noteEntity.img = model.img;
                noteEntity.archive = model.archive;
                noteEntity.pin = model.pin;
                noteEntity.trash = model.trash;
                noteEntity.time_created = model.time_created;
                noteEntity.time_edited = model.time_edited;
                noteEntity.UserId = userId;
                fundooContext.NoteTable.Add(noteEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return noteEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteNotes(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.NoteTable.FirstOrDefault(e => e.noteID == noteId && e.UserId == userId);
                if (!result.Equals(null))
                {
                    fundooContext.NoteTable.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<NoteEntity> GetNotes(long userId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(e=>e.UserId == userId).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateNotes(long noteId, long userId, Note note)
        {
            try
            {
                var result = fundooContext.NoteTable.FirstOrDefault(e => e.noteID == noteId && e.UserId == userId);
                if (!result.Equals(null))
                {
                    if (!note.title.Equals(null))
                        {
                        result.title = note.title;
                    }
                    if (!note.discription.Equals(null))
                    {
                        result.discription = note.discription;
                    }
                    result.time_edited= DateTime.Now;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool PinNotes(long noteId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(e => e.noteID == noteId).FirstOrDefault();
                if (!result.pin.Equals(true))
                {
                    result.pin = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool TrashNotes(long noteId)
        {
            try
            {
                var result=fundooContext.NoteTable.Where(e=>e.noteID==noteId).FirstOrDefault();
                if (result.trash == true)
                {
                    result.trash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ArchiveNotes(long noteId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(e => e.noteID == noteId).FirstOrDefault();
                /* if (!result.archive.Equals(true))*/
                if (result.archive == true)
                {
                    result.archive= false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.archive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                /*if (result.archive == true)
                {
                    result.archive = false;
                    this.fundooContext.SaveChanges();
                    return false;
                }
                result.archive = true;
                this.fundooContext.SaveChanges();
                return true;
            */
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ColorNotes(long noteId,string color)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(e => e.noteID == noteId).FirstOrDefault();
                if (!result.Equals(null))
                {
                    result.color=color;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string UploadImage(long noteId,long userId, IFormFile file)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(e => e.noteID == noteId && e.UserId ==userId).FirstOrDefault();
                if (!result.Equals(null))
                {
                    Account account = new Account(this.config["ClouldinarySettings:CloudName"], this.config["ClouldinarySettings:ApiKey"], this.config["ClouldinarySettings:SecreateKey"]);
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.img = imagePath;
                    fundooContext.SaveChanges();
                    return "Image Saved successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
