using CaloriesAppBackend.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaloriesAppBackend.Models
{
    public class UserInfo
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [ForeignKey("GenderInterpretation")]
        public int Gender { get; set; }

        [Required]
        [ForeignKey("PhysicalActivityInterpretation")]
        public int PhysicalActivity { get; set; }

        [Required]
        [ForeignKey("PurposeInterpretation")]
        public int Purpose { get; set; }
        public int NormOfCalories { get; set; }
        public int PurposeOfCalories { get; set; }
        public int NormOfProteins { get; set; }
        public int NormOfFats { get; set; }
        public int NormOfCarbohydrates { get; set; }
        public ApplicationUser User { get; set; }
        public GenderInterpretation GenderInterpretation { get; set; }
        public PurposeInterpretation PurposeInterpretation { get; set; }
        public PhysicalActivityInterpretation PhysicalActivityInterpretation { get; set; }

    }
}
