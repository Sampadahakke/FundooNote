﻿using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.FundoNoteContext
{
    public class FundoContext : DbContext
    {
        public FundoContext(DbContextOptions<FundoContext> options)
           : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
            .HasIndex(u => u.email)
            .IsUnique();
        }
    }
}