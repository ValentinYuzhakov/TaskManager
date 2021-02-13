using System;

namespace TaskManager.Domain.Models.Abstracts
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}
