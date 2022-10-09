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
    }
}
