using System;
using DataAccessLayer.Models.Abstraction;

namespace DataAccessLayer.Models
{
    public class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
        public DateTime Created { get; set; }
    }
}
