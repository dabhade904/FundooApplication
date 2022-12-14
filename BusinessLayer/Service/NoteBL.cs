using BusinessLayer.Interface;
using CommanLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
namespace BusinessLayer.Service
{
    public class NoteBL: NoteInterfaceBL
    {
        private readonly  NoteInterfaceRL noteInterfaceRL;
        public NoteBL(NoteInterfaceRL noteInterfaceRL)
        {
            this.noteInterfaceRL = noteInterfaceRL;
        }
        public NoteEntity AddNotes( long userId,Note note)
        {
            try
            {
                return noteInterfaceRL.AddNotes(userId,note);
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
                return noteInterfaceRL.DeleteNotes(noteId, userId);
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
                return noteInterfaceRL.GetNotes(userId);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public bool UpdateNotes(long noteId, long userId, Note note)
        {
            try
            {
                return noteInterfaceRL.UpdateNotes(noteId, userId, note);
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
                return noteInterfaceRL.PinNotes(noteId);
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
                return noteInterfaceRL.TrashNotes(noteId);
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
                return noteInterfaceRL.ArchiveNotes(noteId);
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
                return noteInterfaceRL.ColorNotes(noteId,color);
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
                return noteInterfaceRL.UploadImage(noteId, userId,file);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
