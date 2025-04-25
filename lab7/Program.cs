using System;
using System.Collections.Generic;

// Базовый класс
public class Worker
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Worker(string name, int age)
    {
        Name = name;
        Age = age;

    }

    public override string ToString()
    {
        return $"Работник: {Name}\n Возраст: {Age}";
    }
}

// Производный класс — работник с почасовой оплатой
public class HourlyWorker : Worker
{
    public double HourlyRate { get; set; }
    public int HoursWorked { get; set; }

    public HourlyWorker(string name, int age, double hourlyRate, int hoursWorked)
        : base(name, age)
    {
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
    }

    public double CalculateSalary()
    {
        return HourlyRate * HoursWorked;
    }

    public override string ToString()
    {
        return $"Почасовой работник: {Name}\n Возраст: {Age}\n Ставка: {HourlyRate}\n Часы: {HoursWorked}";
    }
}

// Производный класс — работник с окладом
public class SalariedWorker : Worker
{
    public double MonthlySalary { get; set; }

    public SalariedWorker(string name, int age, double monthlySalary)
        : base(name, age)
    {
        MonthlySalary = monthlySalary;
    }

    public double GetSalary()
    {
        return MonthlySalary;
    }

    public override string ToString()
    {
        return $"Работник с окладом: {Name}\n Возраст: {Age}\n Оклад: {MonthlySalary}";
    }
}

// Класс Предприятие
public class Enterprise
{
    private List<Worker> workers = new List<Worker>();

    public void AddWorker(Worker worker)
    {
        workers.Add(worker);
    }

    public int CountHourlyWorkers()
    {
        int count = 0;
        foreach (var worker in workers)
        {
            if (worker is HourlyWorker) count++;
        }
        return count;
    }

    public int CountSalariedWorkers()
    {
        int count = 0;
        foreach (var worker in workers)
        {
            if (worker is SalariedWorker) count++;
        }
        return count;
    }

    public string WorkersStatistics(){
        return $"Работники на окладе: {this.CountSalariedWorkers()}\nРаботники на почасовой оплате {this.CountHourlyWorkers()}";
    }

    public void PrintAllWorkers()
    {
        foreach (var worker in workers)
        {
            Console.WriteLine(worker.ToString());
        }
    }
}

// Пример использования
class Program
{
    static void Main()
    {
        var company = new Enterprise();

        company.AddWorker(new HourlyWorker("Иван", 25, 500, 160));
        company.AddWorker(new SalariedWorker("Мария", 30, 80000));
        company.AddWorker(new HourlyWorker("Сергей", 22, 450, 100));
        company.AddWorker(new SalariedWorker("Анна", 40, 95000));

        company.PrintAllWorkers();

        Console.WriteLine($"{company.WorkersStatistics()}");
    }
}