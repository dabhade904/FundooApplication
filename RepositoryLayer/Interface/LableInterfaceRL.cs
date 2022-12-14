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
        public bool RemoveLable(long userId, long lableId);
        public List<LableEntity> GetAllLable(long noteId, long userId);
        public bool EditLable(long userId, string oldLableName, string newLableName);  
    }
}
