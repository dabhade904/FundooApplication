using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface LableInterfaceBL
    {
        public LableEntity CreateLable(long userId, long noteId, string lableName);
        public bool DeleteLable(long userId,string lableName);
        public List<LableEntity> GetAllLable(long noteId, long userId);
    }
}
