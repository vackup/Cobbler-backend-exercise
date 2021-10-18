using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Book : BaseEntity<int>
    {
        public Budget Author { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int SalesCount { get; set; }
    }
}