using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface LableInterfaceRL
    {
        public LableEntity CreateLable(long userId,long noteId,string lableName);
        public bool DeleteLable(long userId,string lableName);
        public List<LableEntity> GetAllLable(long noteId, long userId);
    }
}
