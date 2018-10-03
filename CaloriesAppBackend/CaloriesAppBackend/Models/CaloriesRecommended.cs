﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Models
{
    public class CaloriesRecommended
    {
        public int NormOfCalories { get; set; }
        public int PurposeOfCalories { get; set; }
        public int NormOfProteins { get; set; }
        public int NormOfFats { get; set; }
        public int NormOfCarbohydrates { get; set; }
    }
}
