using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PeopleManagement
{
    class Person {
        private string firstName = string.Empty;
        [JsonInclude]
        public string FirstName
        {
            get => firstName;
            set {
                if (string.IsNullOrWhiteSpace(value)){
                    Console.WriteLine("Имя не может быть пустым или состоять из пробелов");
                    firstName = "Неизвестный";
                } else if (value.Length > 50){
                    lastName = "Длинный";
                } else{
                    firstName = value;
                }
            }
        }

        private string lastName;
        [JsonInclude]
        public string LastName
        {
            get => lastName;
            private set {
                if (string.IsNullOrWhiteSpace(value)){
                    Console.WriteLine("Фамилия не может быть пустым или состоять из пробелов");
                    lastName = "Неизвестный";
                } else if (value.Length > 50){
                    lastName = "Длинный";
                } else {
                    lastName = value;
                }
            }
        }

        private string patronymic;
        [JsonInclude]
        public string Patronymic
        {
            get => patronymic;
            private set {
                if (string.IsNullOrWhiteSpace(value)){
                    patronymic = string.Empty;
                }
                if (value.Length > 50){
                    patronymic = "Длинный";
                } else {
                    patronymic = value;
                }
            }
        }

        private DateTime birthday;
        [JsonInclude]
        public DateTime Birthday
        {
            get => birthday;
            private set => birthday = value;
        }

        private string phoneNumber;
        [JsonInclude]
        public string PhoneNumber
        {
            get => phoneNumber;
            private set{
                if (string.IsNullOrWhiteSpace(value)) {
                    Console.WriteLine("Строка с номером не должна быть пустой");
                    phoneNumber = "79876543210";
                } else
                if (value.Length != 11) {
                    Console.WriteLine("Длинна номера не может быть больше или меньше 11 символов");
                    phoneNumber = "79876543210";
                }
                else {
                    phoneNumber = value;
                }
            }
        }
        
        private char gender;
        [JsonInclude]
        public char Gender
        {
            get => gender;
            private set{
                if (value == 'м' || value == 'М') {
                    gender = 'М';
                } else if (value == 'ж' || value == 'Ж') {
                    gender = 'Ж';
                } else {
                    Console.WriteLine("Неверное значение при присвоении пола");
                    gender = 'N';
                }
            }
        }

        // Инициализация
        public Person(){
            FirstName = "Неизвестен";
            LastName = "Неизвестный";
            Patronymic = "Неизвестнович";
            Birthday = new DateTime(2005, 7, 27);
            PhoneNumber = "79876543210";
            Gender = 'N';
        }

        // инициализация без отчества
        public Person(string firstName, string lastName, DateTime date, string phoneNumber, char gender){
            FirstName = firstName;
            LastName = lastName;
            Birthday = date;
            PhoneNumber = phoneNumber;
            Gender = gender;
        }

        // инициалиация с отчеством
        public Person(string firstName, string lastName, string patronymic, DateTime date, string phoneNumber, char gender){
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Birthday = date;
            PhoneNumber = phoneNumber;
            Gender = gender;
        }

        // вывод информации
        public string Print() {
            return $"{firstName} {lastName} {patronymic} {birthday} {phoneNumber} {gender}";
        }
    }

    static class People {

    } 

    class Program{
        static void Main(){
            Person unknown = new();
            Console.WriteLine(unknown.Print());
            Person igor = new("Игорь", "Провалинский", "Павлович", new DateTime(2002, 7, 7), "71234567890", 'М');
            Person egor = new("Егор", "Перевозчиков", "Максимович", new DateTime(2006, 8, 15), "71234567980", 'М');
            Person irina = new("Ирина", "Анисимова", "Владимировна", new DateTime(2004, 4, 15), "79876543210", 'Ж');
            Console.WriteLine(igor.Print());
            Console.WriteLine(egor.Print());
            Console.WriteLine(irina.Print());

        }
    }
}