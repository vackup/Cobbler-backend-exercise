using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace Entities
{
    public class MoneyAllocation : BaseEntity<Guid>
    {
        public int UserId { get; set; }
        public Guid? BudgetId { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
        public decimal MoneyAllocated { get; set; }
        public DateTime AllocationDate { get; set; }
    }
}