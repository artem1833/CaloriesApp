﻿namespace CaloriesAppBackend.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public string UnitOfMeasure { get; set; }
        public int Calorie { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbohydrate { get; set; }
        public int GlycemicIndex { get; set; }
    }
}
