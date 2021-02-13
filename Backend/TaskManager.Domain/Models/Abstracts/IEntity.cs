using System;

namespace TaskManager.Domain.Models.Abstracts
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
