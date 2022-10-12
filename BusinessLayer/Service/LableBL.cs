using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
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
    }
}
