using CommanLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollaboratorRL: CollaboratorInterfaceRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public CollaboratorRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public CollabEntity CreateCollaborator(long userId, long noteId, string emailId)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity.collabEmail = emailId;
                collabEntity.noteID = noteId;
                collabEntity.UserId = userId;
                collabEntity.modifyDate = DateTime.Now;
                fundooContext.CollaboratorTable.Add(collabEntity);
                int result= fundooContext.SaveChanges();
                if (!result.Equals(null))
                {
                    return collabEntity;
                }
                else
                {
                    return null; 
                }
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
                var result = fundooContext.CollaboratorTable.Where(e => e.collabId == collabId).FirstOrDefault();
                if (!result.Equals(null))
                {
                    fundooContext.Remove(result);
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

    }
}
