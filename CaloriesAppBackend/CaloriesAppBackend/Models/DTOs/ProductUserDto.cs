using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Models
{
    public class ProductUserDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int Weight { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public int Calorie { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbohydrate { get; set; }
        public int GlycemicIndex { get; set; }
    }
}
