using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{   
    public class LableRL: LableInterfaceRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public LableRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public LableEntity CreateLable(long userId, long noteId, string lableName)
        {
            try
            {
                LableEntity entity = new LableEntity();
                entity.lableName = lableName;
                entity.UserId = userId;
                entity.noteID=noteId;   
                fundooContext.LableTable.Add(entity);
                var result=fundooContext.SaveChanges();
                if(!result.Equals(null )&& result > 0)
                {
                    return entity;
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
    }
}
