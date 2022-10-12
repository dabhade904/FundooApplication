﻿using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
        public bool DeleteLable(long userId, string lableName)
        {
            try
            {
                var result = fundooContext.LableTable.Where(e => e.UserId == userId && e.lableName==lableName).FirstOrDefault();
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