using CaloriesAppBackend.Models;
using CaloriesAppBackend.Models.Entities;
using System.Linq;

namespace CaloriesAppBackend.Data
{
    public class DbInitializer
    {
        public static void Initialize(CaloriesContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;   
            }

            var genderInterpretations = new[]
            {
                new GenderInterpretation{Name = "Мужской", Type = 1},
                new GenderInterpretation{Name = "Женский", Type = 2}
            };

            foreach (var genderInterpretation in genderInterpretations)
            {
                context.GenderInterpretations.Add(genderInterpretation);
            }

            var unitOfMeasureInterpretations = new[]
            {
                new UnitOfMeasureInterpretation{Name = "гр", Type = 1},
                new UnitOfMeasureInterpretation{Name = "шт", Type = 2}
            };

            foreach (var unitOfMeasureInterpretation in unitOfMeasureInterpretations)
            {
                context.UnitOfMeasureInterpretations.Add(unitOfMeasureInterpretation);
            }

            var purposeInterpretations = new[]
            {
                new PurposeInterpretation{Name = "Сохранить вес", Type = 1},
                new PurposeInterpretation{Name = "Сбросить вес", Type = 2},
                new PurposeInterpretation{Name = "Набрать вес", Type = 3}
            };

            foreach (var purposeInterpretation in purposeInterpretations)
            {
                context.PurposeInterpretations.Add(purposeInterpretation);
            }

            var physicalActivityInterpretations = new[]
            {
                new PhysicalActivityInterpretation{Name = "Сидячий образ жизни", Type = 1},
                new PhysicalActivityInterpretation{Name = "Умеренная активность", Type = 2},
                new PhysicalActivityInterpretation{Name = "Средняя (занятия 3-5 раз в неделю)", Type = 3},
                new PhysicalActivityInterpretation{Name = "Активные люди (интенсивные нагрузки)", Type = 4},
                new PhysicalActivityInterpretation{Name = "Спортсмены (6-7 раз в неделю)", Type = 5}
            };

            foreach (var physicalActivityInterpretation in physicalActivityInterpretations)
            {
                context.PhysicalActivityInterpretations.Add(physicalActivityInterpretation);
            }

            var products = new[]
            {
                new Product{Name = "Творог", UnitOfMeasure = 2, Calorie = 200, Protein = 22, Fat = 1, Carbohydrate = 3, GlycemicIndex = 1, Weight = 100},
                new Product{Name = "Хлеб из цельной пшеницы	", UnitOfMeasure = 1, Calorie = 247, Protein = 13, Fat = 3, Carbohydrate = 35, GlycemicIndex = 14 , Weight = 100},
                 new Product{Name = "Баранки сдобные", UnitOfMeasure = 2, Calorie = 348, Protein = 8, Fat = 8, Carbohydrate = 60, GlycemicIndex = 42, Weight = 100},
                new Product{Name = "Котлеты рубленые, баранина", UnitOfMeasure = 1, Calorie = 240, Protein = 14, Fat = 15, Carbohydrate = 13, GlycemicIndex = 6 , Weight = 100},
                new Product{Name = "Макароны из муки 1 сорта", UnitOfMeasure = 2, Calorie = 333, Protein = 1, Fat = 2, Carbohydrate = 68, GlycemicIndex = 34, Weight = 100},
                new Product{Name = "Гречка", UnitOfMeasure = 2, Calorie = 200, Protein = 22, Fat = 1, Carbohydrate = 3, GlycemicIndex = 1, Weight = 100},
                new Product{Name = "Банан", UnitOfMeasure = 1, Calorie = 340, Protein = 50, Fat = 3, Carbohydrate = 35, GlycemicIndex = 14 , Weight = 150},
                 new Product{Name = "Плов", UnitOfMeasure = 2, Calorie = 348, Protein = 8, Fat = 8, Carbohydrate = 60, GlycemicIndex = 42, Weight = 100},
                new Product{Name = "Картофель", UnitOfMeasure = 2, Calorie = 333, Protein = 1, Fat = 2, Carbohydrate = 68, GlycemicIndex = 34, Weight = 100}
            };
            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();
            
        }
    }
}
