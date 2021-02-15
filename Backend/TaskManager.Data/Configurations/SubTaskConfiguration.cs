using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Configurations
{
    class SubTaskConfiguration : IEntityTypeConfiguration<SubTask>
    {
        public void Configure(EntityTypeBuilder<SubTask> builder)
        {
            builder
                .Property(t => t.TaskStatus)
                .HasDefaultValue(TaskStatus.InProgress);

            builder
                .Property(t => t.CreationDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
