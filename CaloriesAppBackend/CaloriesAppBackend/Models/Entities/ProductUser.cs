using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
