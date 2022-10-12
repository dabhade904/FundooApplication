using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LableBL: LableInterfaceBL
    {
        private readonly LableInterfaceRL lableInterfaceRL;
        public LableBL(LableInterfaceRL lableInterfaceRL)
        {
            this.lableInterfaceRL = lableInterfaceRL;
        }
        public LableEntity CreateLable(long userId, long noteId, string lableName)
        {
            try
            {
                return lableInterfaceRL.CreateLable(userId, noteId, lableName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteLable(long userId,string lableName)
        {
            try
            {
                return lableInterfaceRL.DeleteLable(userId,lableName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<LableEntity> GetAllLable(long noteId, long userId)
        {
            try
            {
                return lableInterfaceRL.GetAllLable(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
