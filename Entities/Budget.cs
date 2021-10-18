using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Budget : BaseEntity<int>
    {
        [Required]
        public int User { get; set; }

        [Required]
        public decimal InitialMoneyToAllocate { get; set; }
        
        [Required]
        public DateTime CreationDate { get; set; }

        public List<MoneyAllocation> MoneyAllocations { get; set; }
    }
}