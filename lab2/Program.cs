using System;

namespace MiniTaskManager
{
    struct TaskMeta{
        // поля
        public int id = 1;
        public DateTime create_at = DateTime.Now;
        public string status;

        // инициалицаия
        public TaskMeta(string status) {
            this.status = status;
        }
        public TaskMeta(string status, int id) : this(status){
            this.id = id;
        }
        public TaskMeta(string status, int id, DateTime create_at) : this(status, id){
            this.create_at = create_at;
        }

        // вывод информации структуры
        public void Print(){
            Console.WriteLine($"Id {id}\nсоздано {create_at}\nстатус {status}");
        }

        // изменение структуры
        public TaskMeta ChangeStatus(string newStatus) {
            return this with {status = newStatus};
        }
        public TaskMeta ChangeId(int newId) {
            return this with {id = newId};
        }
        public TaskMeta ChangeCreateAt(DateTime newCreate_at) {
            return this with {create_at = newCreate_at};
        }

        public TaskMeta CopyWithCreate(int newId, string newStatus) =>
            this with {id = newId, status = newStatus};
        public TaskMeta CopyWithStatus(int newId, DateTime newCreate_at) =>
            this with {id = newId, create_at = newCreate_at};
        public TaskMeta CopyWithId(string newStatus, DateTime newCreate_at) =>
            this with {status = newStatus, create_at = newCreate_at};

    }
    
    class Task{
        // поля

        public string title = "def_title";
        public string description = "Without commments";
        public TaskMeta meta;

        // инициализация
        public Task(string title, string description, TaskMeta meta){
            this.title = title;
            this.description = description;
            this.meta = meta;
        }

        // вывод информации класса
        public void Print(){
            Console.WriteLine($"Задание {title}\nОписание {description}\nДоп.данные");
            meta.Print();
        }

        // изменение класса
        public Task ChangeTitle(string newTitle) {
            return new Task(newTitle, this.description, this.meta);
        }
        public Task ChangeDescr(string newDescr) {
            return new Task(this.title, newDescr, this.meta);
        }
        public Task ChangeMeta(TaskMeta newMeta) {
            return new Task(this.title, this.description, newMeta);
        }

        // Копирование класса с изменёнными параметрами
        public Task CopyWithTitle(string newDescription, TaskMeta newMeta) =>
            new Task(title, newDescription, newMeta);
        
        public Task CopyWithDescr(string newTitle, TaskMeta newMeta) =>
            new Task(newTitle, description, newMeta);
        
        public Task CopyWithMeta(string newTitle, string newDescr) =>
            new Task(newTitle, newDescr, meta);
    }

    class Program{
        static void Main(){
            TaskMeta oldWorkMeta = new TaskMeta("Выполнить", 1);
            Task oldWork = new Task("Разобраться с костюмами", "Разобраться с костюмами в актовом усиленно", oldWorkMeta);
            Console.WriteLine("Класс и структура до изменений");
            oldWork.Print();

            Task newWork = oldWork;
            TaskMeta newWorkMeta = oldWorkMeta;

            newWorkMeta.id = 2;
            newWork.title = "Новые костюмы";
            // изменения в классах и структурах
            Console.WriteLine("\nСравнения изменений структур и классов");
            Console.WriteLine("\nСтарый класс после изменений");
            oldWork.Print();
            Console.WriteLine("\nСтарая структура после изменений");
            oldWorkMeta.Print();

            Console.WriteLine("\nНовый класс после изменений");
            newWork.Print();
            Console.WriteLine("\nНовая структура после изменений");
            newWorkMeta.Print();

            newWorkMeta = newWorkMeta.ChangeStatus("В процессе");
            newWork = newWork.CopyWithDescr("Костюмы", newWorkMeta);
            Console.WriteLine("\nНовые класс и структура после копирований с изменениями");
            newWork.Print();
        }
    }
}
