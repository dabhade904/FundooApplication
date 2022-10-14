using CommanLayer.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface CollaboratorInterfaceRL
    {
        public CollabEntity CreateCollaborator(long userId, long noteId, string emailId);
        public bool RemoveCollaborator(long collabId);
        public List<CollabEntity> RetriveDetails(long noteId);

    }
}
