using LinqComLambda.Entities;
using System;

namespace LinqComLambda
{
    internal class Program
    {
        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach(T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            
            Category c1 = new Category() {Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() {Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() {Id = 3, Name = "SmartPhones", Tier = 3 };

            List<Product> products = new List<Product>()
            {
                new Product() {Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new Product() {Id = 2, Name = "Notebook", Price = 800.0, Category = c2 },
                new Product() {Id = 3, Name = "Saw", Price = 2200.0, Category = c1 },
                new Product() {Id = 4, Name = "Hammer", Price = 100, Category = c1 },
                new Product() {Id = 5, Name = "Iphone 15", Price = 5100.0, Category = c3 },
                new Product() {Id = 6, Name = "Samsung S4", Price = 3100.0, Category = c3 },
            };

            //var result = products.Where(p => p.Category.Tier == 2 && p.Price < 900.0);
            var result = 
                from p in products
                where p.Category.Tier == 1 && p.Price < 900.0
                select p;

            var result2 = products.Where(p => p.Category.Name == "Tools").Select(p => p.Name);

            var result3 = products.Where(p => p.Name[0] == 'C').Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name });

            var result4 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);

            var result5 = result4.Skip(1).Take(1);

            Print("Tire 1 and price < 900:", result);
            Print("Names of products from tools", result2);
            Print("Names started with C and anonymous object", result3 );
            Print("Category 1, orderby Price the Name", result4);
            Print("Tier order by price then by name skip 2 take 4", result5);
            var result6 = products.FirstOrDefault();
            Console.WriteLine("First test1 " + result6);

            var result7 = products.Where((p) => p.Price > 1000).First();
            Console.WriteLine("First or default test2 " + result7);

            var result8 = products.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine("Single or default test1 " + result8);

            var result10 = products.Max(p => p.Price);
            Console.WriteLine("Max price: " + result10);

            var result11 = products.Min(p => p.Price);
            Console.WriteLine("Min price: " + result11);

            var result12 = products.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("Sum category 1 prices: " + result12);

            var result13 = products.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("Sum category 1 prices: " + result13);

            var result14 = products.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Sum category 1 prices: " + result14);

            var result15 = products.Where(p => p.Category.Id == 1).Select(p => p.Price).Aggregate((x, y) => x + y) ;
            Console.WriteLine("Category 1 aggregate sum: " + result15);

            //var result16 = products.GroupBy(p => p.Category.Id == 1);
            //foreach(IGrouping<Category, Product> group in result16)
            //{
            //    Console.WriteLine("Category " + group.Key.Name + ":");
            //    foreach(Product product in group)
            //    {
            //        Console.WriteLine();
            //    }
            //    Console.WriteLine();
            //}
            

        }
    }
}