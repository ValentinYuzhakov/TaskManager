using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Configurations
{
    public class TaskFolderConfiguration : IEntityTypeConfiguration<TaskFolder>
    {
        public void Configure(EntityTypeBuilder<TaskFolder> builder)
        {
            builder
                .Property(p => p.FolderType)
                .HasDefaultValue(FolderType.Default);
        }
    }
}
