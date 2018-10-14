using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaloriesAppBackend.Models
{
    public class Interpretation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
