using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
    : base(options)
        {
        }
        public DbSet<UserEntity> UserTable { get; set; }
        public DbSet<NoteEntity> NoteTable { get; set; }
        public DbSet<CollabEntity> CollaboratorTable { get; set; }
        public DbSet<LableEntity> LableTable { get; set; }
    }
}
