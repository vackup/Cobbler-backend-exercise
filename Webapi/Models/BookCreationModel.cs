using System.ComponentModel.DataAnnotations;
using Entities;

namespace Webapi.Models
{
    public class BookCreationModel
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
    }
}