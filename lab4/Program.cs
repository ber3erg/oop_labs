using System;

namespace InvestorsInformation {
    class Investor{
        // поля и их свойства
        private string Name {get; set;} = "Неизвестный";   // имя инвестора
        private string company = "Неизвестная компания";             // название компании
        public string Company
        {
            get => company;
            private set => company = value;
        }
        
        private int amountLots;            // количество лотов акций
        public int AmountLots
        {
            get => amountLots;
            private set => amountLots = value < 0 ? 0 : value; // проверка на неотрицательность
        }

        // private bool Is_small_investor { get { return amount_lots < 25 ? true : false;}}

        // private bool Is_small_investor
        // {
        //     get => amount_lots < 25;
        // }

        public bool IsSmallInvestor => amountLots < 25;
        

        // конструкторы инциализации
        public Investor(){
            Name = "Неизвестный";
            Company = "Неизвестная компания";
            AmountLots = 0;
        }

        // Получение информации
        public string GetInfo(){
            return $"{Name} {company} {amountLots} {IsSmallInvestor}";
        }
    }
    
    class Program{
        static void Main(){
            Investor unknown = new();
            Console.WriteLine(unknown.GetInfo());
        }
    }
}
