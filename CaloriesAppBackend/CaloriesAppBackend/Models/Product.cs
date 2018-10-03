using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CaloriesAppBackend.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public int UnitOfMeasure { get; set; }

        [Required]
        public int Calorie { get; set; }

        [Required]
        public int Protein { get; set; }

        [Required]
        public int Fat { get; set; }

        [Required]
        public int Carbohydrate { get; set; }

        [Required]
        public int GlycemicIndex { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
