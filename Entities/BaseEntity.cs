using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public abstract class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
