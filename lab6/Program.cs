using System;

namespace ShopManagement
{
    public class Product
    {
        public string Name { get; private set; }
        public int Amount { get; private set; }
        public double Price { get; private set; }
        public string? Description { get; set; }
        public int? DiscountPercent { get; set; }
        public double? Rating { get; set; }

        // инициализация
        public Product(string name, int amount, double price, string? description = null, int? discountPercent = null, double? rating = null){
            Name = name!;
            Amount = amount;
            Price = price;
            Description = description;
            DiscountPercent = discountPercent;
            Rating = rating;
        }

        

        public void Print(){
            Console.WriteLine($"Название товара: {Name}");
            Console.WriteLine($"Кол-во: {Amount}");
            Console.WriteLine($"Стоимость: {Price}");
            Console.WriteLine($"Описание: {(Description ?? "Нет информации")}");
            Console.WriteLine($"Скидка: {(DiscountPercent.HasValue ? DiscountPercent + "%" : "Нет информации")}");
            Console.WriteLine($"Рейтинг: {(Rating.HasValue ? Rating.ToString() : "Нет информации")}");
            Console.WriteLine();
            
        }
    }

    public class Program{
        public static void SetDefaultsIfNull(Product product)
        {
            product.Description ??= "Нет описания";
            product.DiscountPercent ??= 0;
            product.Rating ??= 0.0;
        }
        public static string GetProductDescription(Product? product)
        {
            return product?.Description ?? "Описание отсутствует";
        }

        static void Main(){
            Product? product1 = new Product("Ручка", 50, 3.99);
            Product? product2 = null;
            Console.WriteLine(GetProductDescription(product1));
            Console.WriteLine(GetProductDescription(product2));
        }
    }
}
