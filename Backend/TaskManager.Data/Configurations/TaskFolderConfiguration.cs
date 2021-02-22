using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Configurations
{
    public class TaskFolderConfiguration : IEntityTypeConfiguration<TaskFolder>
    {
        public void Configure(EntityTypeBuilder<TaskFolder> builder)
        {
            builder
                .Property(p => p.Type)
                .HasDefaultValue(FolderType.Default);
        }
    }
}
