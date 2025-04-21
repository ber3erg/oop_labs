using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PeopleManagement
{
    class Person {
        private static int counter = 0;
        private int id;
        public int Id => id; // значение Id определяется в конструкторе уеличивая число counter

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

        private string patronymic = string.Empty;
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
            id = ++counter;
            FirstName = firstName;
            LastName = lastName;
            Birthday = date;
            PhoneNumber = phoneNumber;
            Gender = gender;

            PeopleRegistry.Register(this); // вызов регистрации нового человека
        }

        // инициалиация с отчеством. Сохраняется только отчество, а затем вызывается другой конструктор
        public Person(string firstName, string lastName, string patronymic, DateTime date, string phoneNumber, char gender) 
        : this(firstName, lastName, date, phoneNumber, gender){
            Patronymic = patronymic;
        }

        // вывод информации
        public string GetInfo() {
            return $"{firstName}\t {lastName}\t {patronymic}\t {birthday}\t {phoneNumber}\t {gender}";
        }

        public string GetFullName(){
            return $"{firstName} {lastName} {patronymic}";
            
        }
    }

    static class PeopleRegistry{
        private static Dictionary<int, Person> people = new Dictionary<int, Person>();

        // регистрация человека в словаре
        public static void Register(Person person){
            people[person.Id] = person;
        }

        public static Person GetPersonById(int id){
            return people.ContainsKey(id) ? people[id] : new Person();
        }

        public static int GetAgeById(int id){
            int age = 0;
            if (people.ContainsKey(id)){
                age = DateTime.Today.Year - people[id].Birthday.Year;
            }
            if (people[id].Birthday > DateTime.Today.AddYears(-age)){
                age--;
            }
            return age;
        }

        public static string GetFullNameById(int id){
            if (people.ContainsKey(id)){
                return $"{people[id].FirstName} {people[id].LastName} {people[id].Patronymic}";
            }
            else return "Неизвестный";
        }

        public static int GetIdByFullName(string fullName){

            foreach (var entry in people)
            {
                Person person = entry.Value;

                // Формируем полное имя из полей
                string currentFullName = $"{person.FirstName} {person.LastName} {person.Patronymic}";

                // Сравниваем без учёта регистра и лишних пробелов
                if (currentFullName.Trim().Equals(fullName.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return entry.Key; // PersonalId
                }
            }

            Console.WriteLine("Человек с таким ФИО не найден.");
            return -1;
        }

        // Вывод информции о всех людях
        public static void PrintInfoPeople(){
            foreach (Person person in people.Values){
                Console.WriteLine(person.GetInfo());
            }
        }
    } 

    class University{
        private string name;
        // свойства для json сериализации и десериализации
        [JsonInclude]
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) {
                    Console.WriteLine("Ошибка в присвоении Названия. Значение не может быть пустым! теперь он неизвесный");
                    name = "Неизвестный";
                } else if (value.Length > 110) {
                    name = "Длинный";
                } else 
                name = value;
            }
        }
        
        private List<Student> listOfStudents = new List<Student>();
        [JsonInclude]
        public List<Student> ListOfStudents
        {
            get => listOfStudents;
            private set => listOfStudents = value;
        }

        private int newBookNumber = 1;

        // инициализация
        public University() {
            name = "неизвестный";
            listOfStudents = new List<Student>{};
        }
        public University(string name){
            this.name = name;
        }
        public University(string name, List<Student> listOfStudents): this(name){
            this.listOfStudents = listOfStudents;
        }

        // ----------------- Вложенный класс Student -----------------
        public class Student : Person
        {
            private float averageScore;
            [JsonInclude]
            public float AverageScore
            {
                get => averageScore;
                private set
                {
                
                    if (value < 0) {
                        Console.WriteLine("Ошибка в присвоении среднего балла. Он не может быть отрицательным.");
                        averageScore = 0;
                    } else if (value > 5) {
                        Console.WriteLine("Ошибка в присвоении среднего балла. Он не может быть больше 5");
                        averageScore = 0;
                    }
                    else averageScore = value;
                }
            }
            public int BookNumber {get; private set;}

            // создадим инициализацию специально для создания студента из человека
            public Student(Person basePerson, int bookNumber) 
                : base(basePerson.FirstName, basePerson.LastName, basePerson.Patronymic, basePerson.Birthday, basePerson.PhoneNumber, basePerson.Gender){
                BookNumber = bookNumber;
                AverageScore = 0f;
            }

            public string GetAllInfoStudent() {
                return $"{BookNumber} {FirstName}\t {LastName}\t {Patronymic}\t {Birthday}\t {AverageScore}";
            }            
        }

        // ---------------------------------------------


        // Добавляет студента в список университетский
        // автоматически добавляет ему значения зачётной книжки и среднего балла пока равного 0
        public Student AddStudent(Person person){
            Student newStudent = new(person, newBookNumber++);
            listOfStudents.Add(newStudent);
            return newStudent;
        }

        // удаление студента из университета
        // Поскольку мы удаляем студента из университета, то создаём обратно класс студента
        public Person Delete(Student student){
            int ind = this.Search(student);
            Person deletingStudent = new(
                student.FirstName, 
                student.LastName,
                student.Patronymic,
                student.Birthday,
                student.PhoneNumber,
                student.Gender);
            listOfStudents.RemoveAt(ind);
            return deletingStudent;
        }

        // поиск студента в университете
        public int Search(Student student){
            List<Student> newList = this.ListOfStudents;
            for (int i = 0; i < newList.Count; i++)
            {
                if (newList[i] == student) {
                    return i;
                }
            }
            return 9999;
        }

        public string GetStudentNameByBook(int bookNumber){
            foreach (var student in listOfStudents)
            {
                if (student.BookNumber == bookNumber)
                {
                    return $"{student.GetFullName()}";
                }
            }

            return "Неизвестный";
        }

        // вывод данных обо всех студентах университета
        public void Print(){
            Console.WriteLine($"Студенты университета {this.name}");
            foreach(Student stud in this.listOfStudents){
                Console.WriteLine(stud.GetAllInfoStudent());
            }
        }
    }
    

    class Program{
        static void Main(){
            Person unknown = new();
            Console.WriteLine(unknown.GetInfo());
            Person igor = new("Игорь", "Провалинский", "Павлович", new DateTime(2002, 7, 7), "71234567890", 'М');
            Person egor = new("Егор", "Перевозчиков", "Максимович", new DateTime(2006, 8, 15), "71234567980", 'М');
            Person irina = new("Ирина", "Анисимова", "Владимировна", new DateTime(2004, 4, 15), "79876543210", 'Ж');
            Person ozernikov = new("Сергей", "Озерников", "Игоревич", new DateTime(2003, 6, 20), "79786543210", 'М');
            PeopleRegistry.PrintInfoPeople();

            Console.WriteLine(PeopleRegistry.GetPersonById(2).GetInfo());

            University mospolytech = new("Московский политех");
            University.Student egorStudent = mospolytech.AddStudent(egor);
            University.Student irinaStudent = mospolytech.AddStudent(irina);
            University.Student ozernikovStudent = mospolytech.AddStudent(ozernikov);

            University gubkino = new("Университет имени Губкина");
            University.Student igorStudent = gubkino.AddStudent(igor);

            Console.WriteLine($"Пользователь {PeopleRegistry.GetFullNameById(2)} имеет возраст {PeopleRegistry.GetAgeById(2)}");
            Console.WriteLine($"Пользователь {irina.GetFullName()} имеет id {PeopleRegistry.GetIdByFullName(irina.GetFullName())}");

            Console.WriteLine($"Студента ВУЗА {mospolytech.Name}с зачётной книжкой 2 зовут {mospolytech.GetStudentNameByBook(2)}");
        }
    }  
}

namespace DataAccess
{
    class StudentsRepository{

        private string filename;

        public StudentsRepository(string file){
            this.filename = file;
        }

        public void Pull_to_json_file(UniversityAndStudents.University univ){
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string jsonstring = JsonSerializer.Serialize(univ, options);
            var utf8WithBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
            File.WriteAllText(this.filename, jsonstring, utf8WithBom);
        }

        public List<UniversityAndStudents.University> Get_from_json_file(UniversityAndStudents.University univ){
            string jsonFromFile = File.ReadAllText(this.filename, Encoding.UTF8);
            List<UniversityAndStudents.University> universities = new List<UniversityAndStudents.University>();
            if (jsonFromFile[0] == '[') {
                universities = JsonSerializer.Deserialize<List<UniversityAndStudents.University>>(jsonFromFile);
            }
            else {
                UniversityAndStudents.University university = JsonSerializer.Deserialize<UniversityAndStudents.University>(jsonFromFile);
                universities.Add(university);
            }
            return universities;
        }
    }
}