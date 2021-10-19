using System.ComponentModel.DataAnnotations;
using Entities;

namespace Webapi.Models
{
    /// <summary>
    /// Model used to create or update request for Money Allocation
    /// </summary>
    public class MoneyAllocationModel
    {
        public int? ProjectId { get; set; }

        public int? PersonId { get; set; }

        public decimal MoneyToAllocate { get; set; }
    }
}