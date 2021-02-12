﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TaskManager.Domain.Models;

namespace TaskManager.Data
{
    public class DatabaseContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<ToDoTask> Tasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
        public DbSet<TaskFolder> TaskFolders { get; set; }


        public DatabaseContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ToDoTask>()
                .HasMany(t => t.Folders)
                .WithMany(f => f.ToDoTasks)
                .UsingEntity(j => j.ToTable("TaskFolderToDoTask"));

            base.OnModelCreating(builder);
        }
    }
}