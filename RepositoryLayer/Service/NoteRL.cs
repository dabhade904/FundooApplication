using CommanLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace RepositoryLayer.Service
{
    public class NoteRL : NoteInterfaceRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration config;

        public NoteRL(FundooContext fundooContext, IConfiguration config)
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
                if (result != null)
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
                if(result != null)
                {
                    if (note.title != null)
                    {
                        result.title = note.title;
                    }
                    if (note.discription != null)
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
                if (result.pin==true)
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
                if (result.archive == true)
                {
                    result.archive=false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.archive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
