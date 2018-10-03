using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Models
{
    public class UserInfo
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public int PhysicalActivity { get; set; }
        public int Purpose { get; set; }
        public int NormOfCalories { get; set; }
        public int PurposeOfCalories { get; set; }
        public int NormOfProteins { get; set; }
        public int NormOfFats { get; set; }
        public int NormOfCarbohydrates { get; set; }
        //public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
