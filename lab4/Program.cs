using System;

namespace InvestorsInformation {
    class Investor{
        // поля и их свойства
        private string Name {get; set;} = string.Empty;   // имя инвестора
        private string company = string.Empty;             // название компании
        public string Company
        {
            get => company;
            private set => company = value;
        }
        
        private int amountLots = 0;            // количество лотов акций
        public int AmountLots
        {
            get => amountLots;
            private set => amountLots = value < 0 ? 0 : value; // проверка на неотрицательность
        }

        public bool IsSmallInvestor => amountLots < 25;
        

        // конструкторы инициализации
        public Investor(){
            Name = string.Empty;
            Company = string.Empty;
            AmountLots = 0;
        }
        public Investor(string name) {
            Name = name;
        }
        public Investor(string name, string company) : this(name) {
            Company = company;
        }
        public Investor(string name, string company, int amount) : this(name, company) {
            AmountLots = amount;
        }

        // Получение информации
        public string GetInfo(){
            return $"Инвестор {Name} приобрёл у компании {company} {amountLots} лотов акций";
        }

        public string GetInfo(bool includeIntProperty){
            if (includeIntProperty) {
                return this.GetInfo();
            } else {
                return $"Инвестор {Name} приобрёл акции в компании {company}";
            };
        }
    }
    
    class Program{
        static void Main(){
            Investor unknown = new();
            Console.WriteLine(unknown.GetInfo());

            Investor peter = new("Пётр");
            Investor igor = new("Игорь", "Татнефть");
            Investor oleg = new("Олег", "Татнефть", 27);
            Console.WriteLine(peter.GetInfo(true));
            Console.WriteLine(igor.GetInfo(false));
            Console.WriteLine(oleg.GetInfo(true));
        }
    }
}
