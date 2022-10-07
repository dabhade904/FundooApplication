using CommanLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface NoteInterfaceBL
    {
        public NoteEntity AddNotes(long userId, Note notes);
    }
}
