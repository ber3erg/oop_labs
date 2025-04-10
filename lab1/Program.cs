using System;

namespace New {
    class Program{
        static void Main(){
            Person Robert = new Person("Robert");
            Person Peter = new Person("Peter", 15);
            Person Tom = new Person("Tom", 24, "Tom@yandex.ru");

            Robert.Print();
            Peter.Print();
            Tom.Print();
        }   
    }
    class Person
    {
        public string name = "Ben";
        public int age = 18;
        public string email = "ben@gmail.com";
        public Person(string name)
        {
            this.name = name;
        }
        public Person(string name, int age) : this(name)
        {
            this.age = age;
        }
        public Person(string name, int age, string email) :
        this("Bob", age)
        {
            this.email = email;
        }
    
        public void Print(){
            Console.WriteLine($"Имя: {name}\nВозраст: {age}\nEmail: {email}\n");
        }
    }
}