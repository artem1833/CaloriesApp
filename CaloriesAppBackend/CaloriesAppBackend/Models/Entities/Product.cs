using CaloriesAppBackend.Data;
using CaloriesAppBackend.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("UnitOfMeasureInterpretation")]
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
        public ApplicationUser User { get; set; }
        public UnitOfMeasureInterpretation UnitOfMeasureInterpretation { get; set; }

    }
}
