using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace Entities
{
    public class MoneyAllocation : BaseEntity<int>
    {
        public Budget Budget { get; set; }

        public Person Person { get; set; }

        public Project Project { get; set; }

        public decimal MoneyAllocated { get; set; }

        public DateTime AllocationDate { get; set; }
        public int UserId { get; set; }
        public int? PersonId { get; set; }
        public int? ProjectId { get; set; }
        public int? BudgetId { get; set; }
    }
}