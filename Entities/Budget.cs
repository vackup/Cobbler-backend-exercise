using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Budget : BaseEntity<Guid>
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public decimal InitialMoneyToAllocate { get; set; }
        
        [Required]
        public DateTime CreationDate { get; set; }

        public List<MoneyAllocation> MoneyAllocations { get; set; }
    }
}