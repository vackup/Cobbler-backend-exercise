using System.ComponentModel.DataAnnotations;

namespace Webapi.Models
{
    public class BookSaleModel
    {
        [Required]
        public int Id { get; set; }
    }
}