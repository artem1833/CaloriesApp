using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Models
{
    public class Interpretation
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int SubType { get; set; }
    }
}
