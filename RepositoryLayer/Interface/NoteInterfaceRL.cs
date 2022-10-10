using CommanLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface NoteInterfaceRL
    {
        public NoteEntity AddNotes(long userId,Note notes);
        public bool DeleteNotes(long noteId, long userId);
        public List<NoteEntity> GetNotes(long userId);
        public bool UpdateNotes(long noteId, long userId, Note note);
        public bool PinNotes(long noteId);

    }
}
