using System;

namespace ShopManagement
{
    public class Product
    {
        public string Name { get; private set; }
        public int Amount { get; private set; }
        public double Price { get; private set; }
        public string? Description { get; private set; }
        public int? DiscountPercent { get; private set; }
        public double? Rating { get; private set; }

        // инициализация
        public Product(string name, int amount, double price){
            Name = name;
            Amount = amount;
            Price = price;
        }

        public Product(string name, int amount, double price, string description) : this (name, amount, price){
            Description = description;
        }

        public Product(string name, int amount, double price, string description, int discount) 
        : this (name, amount, price, description)
        {
            DiscountPercent = discount;
        }

        public Product(string name, int amount, double price, string description, int discount, double rating) 
        : this (name, amount, price, description, discount)
        {
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
        static void Main(){
            Product product1 = new Product("Ручка", 50, 15.99);
                Product product2 = new Product("Тетрадь", 100, 39.99, "96 листов, клетка");
                Product product3 = new Product("Линейка", 80, 12.50, "Пластиковая, 30 см", 10);
                Product product4 = new Product("Калькулятор", 20, 599.99, "Инженерный калькулятор", 15, 4.8);

                product1.Print();
                product2.Print();
                product3.Print();
                product4.Print();
        }
    }
}
