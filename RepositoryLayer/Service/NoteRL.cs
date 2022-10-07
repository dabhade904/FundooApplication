using CommanLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using System;
using System.IO;

namespace RepositoryLayer.Service
{
    public class NoteRL: NoteInterfaceRL
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
                noteEntity.time_edited= model.time_edited;
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
       
    }
}
