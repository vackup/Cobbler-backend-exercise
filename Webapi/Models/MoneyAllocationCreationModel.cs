using System.ComponentModel.DataAnnotations;
using Entities;

namespace Webapi.Models
{
    public class MoneyAllocationCreationModel
    {
        public int? ProjectId { get; set; }

        public int? PersonId { get; set; }

        public decimal MoneyToAllocate { get; set; }
    }
}