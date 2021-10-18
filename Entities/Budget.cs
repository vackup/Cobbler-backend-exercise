using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Budget : BaseEntity<int>
    {
        [Required]
        public int User { get; set; }

        [Required]
        public decimal MoneyToAllocate { get; set; }
        
        [Required]
        public DateTime CreationDate { get; set; }
    }
}