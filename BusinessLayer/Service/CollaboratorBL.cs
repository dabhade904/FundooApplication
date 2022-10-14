using BusinessLayer.Interface;
using CommanLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollaboratorBL: CollaboratorInterfaceBL
    {
        private readonly CollaboratorInterfaceRL collaboratorRL;
        public CollaboratorBL(CollaboratorInterfaceRL collaboratorRL)
        {
            this.collaboratorRL = collaboratorRL;
        }
        public CollabEntity CreateCollaborator(long userId, long noteId, string emailId)
        {
            try
            {
                return collaboratorRL.CreateCollaborator(userId,noteId,emailId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool RemoveCollaborator(long collabId)
        {
            try
            {
                return collaboratorRL.RemoveCollaborator(collabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<CollabEntity> RetriveDetails(long lableId)
        {
            try
            {
                return collaboratorRL.RetriveDetails(lableId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
