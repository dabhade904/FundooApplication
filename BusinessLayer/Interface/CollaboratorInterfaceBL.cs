using CommanLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface CollaboratorInterfaceBL   
    {
        public CollabEntity CreateCollaborator(long userId, long noteId, string emailId);
        public bool RemoveCollaborator(long collabId);
    }
}
