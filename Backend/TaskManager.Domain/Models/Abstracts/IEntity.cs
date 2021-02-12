using System;

namespace TaskManager.Domain.Models.Abstracts
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}
