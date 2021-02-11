using System;

namespace TaskManager.Domain.Models
{
    public abstract class Entity<T>
    {
        public T Id { get; set; }
    }

    public abstract class Entity : Entity<Guid>
    {
    }
}
