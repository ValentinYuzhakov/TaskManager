﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Configurations
{
    public class ToDoTaskConfiguration : IEntityTypeConfiguration<ToDoTask>
    {
        public void Configure(EntityTypeBuilder<ToDoTask> builder)
        {
            builder
                .Property(t => t.Priority)
                .HasDefaultValue(ToDoTaskPriority.None);

            builder
                .Property(t => t.Status)
                .HasDefaultValue(ToDoTaskStatus.InProgress);

            builder
                .Property(t => t.CreationDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}
