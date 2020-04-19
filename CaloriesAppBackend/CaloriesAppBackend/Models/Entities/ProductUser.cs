using CaloriesAppBackend.Data;
using System.ComponentModel.DataAnnotations;

namespace CaloriesAppBackend.Models
{
    public class ProductUser
    {
        public int Id { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
