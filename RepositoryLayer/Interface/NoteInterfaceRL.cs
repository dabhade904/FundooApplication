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
    }
}
