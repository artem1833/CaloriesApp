using CaloriesAppBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var products = new Product[]
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

            var interpretations = new Interpretation[]
            {
                new Interpretation{Name = "гр", Type = 1, SubType = 1},
                new Interpretation{Name = "шт", Type = 1, SubType = 2},
                new Interpretation{Name = "Мужской", Type = 2, SubType = 1},
                new Interpretation{Name = "Женский", Type = 2, SubType = 2},
                new Interpretation{Name = "Сохранить вес", Type = 3, SubType = 1},
                new Interpretation{Name = "Сбросить вес", Type = 3, SubType = 2},
                new Interpretation{Name = "Набрать вес", Type = 3, SubType = 3},
                new Interpretation{Name = "Сидячий образ жизни", Type = 4, SubType = 1},
                new Interpretation{Name = "Умеренная активность", Type = 4, SubType = 2},
                new Interpretation{Name = "Средняя (занятия 3-5 раз в неделю)", Type = 4, SubType = 3},
                new Interpretation{Name = "Активные люди (интенсивные нагрузки)", Type = 4, SubType = 4},
                new Interpretation{Name = "Спортсмены (6-7 раз в неделю)", Type = 4, SubType = 5},
            };
            foreach (var interpretation in interpretations)
            {
                context.Interpretations.Add(interpretation);
            }

            context.SaveChanges();
            
        }
    }
}
