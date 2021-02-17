using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TaskManager.Data.Configurations;
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
            builder.ApplyConfiguration(new ToDoTaskConfiguration());
            builder.ApplyConfiguration(new SubTaskConfiguration());
            builder.ApplyConfiguration(new TaskFolderConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
