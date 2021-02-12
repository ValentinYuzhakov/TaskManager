using System;

namespace TaskManager.Domain.Models.Abstracts
{
    public abstract class Entity<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }

    public abstract class Entity : Entity<Guid>
    {
    }
}
