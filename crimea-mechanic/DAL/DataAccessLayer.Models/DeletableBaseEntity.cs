using System;

namespace DataAccessLayer.Models
{
    public class DeletableBaseEntity<T> : BaseEntity<T>
    {
        public bool IsDeleted { get; set; }
        public DateTime Updated { get; set; }
    }
}
