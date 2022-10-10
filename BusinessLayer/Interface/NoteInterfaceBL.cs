using CommanLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface NoteInterfaceBL
    {
        public NoteEntity AddNotes(long userId, Note notes);
        public bool DeleteNotes(long noteId, long userId);
        public List<NoteEntity> GetNotes(long userId);
        public bool UpdateNotes(long noteId, long userId, Note note);
        public bool PinNotes(long noteId);
        public bool TrashNotes(long noteId);
        public bool ArchiveNotes(long noteId);
        public bool ColorNotes(long noteId,string color);
        public string UploadImage(long noteId,long userId, IFormFile file);


    }
}
