using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace UniversityAndStudents 
{
    // класс Студент
    class Student{
        private string name;
        private string last_name;
        private int age;
        private float average_score;

        // инициализация
        public Student(){
            name = "неизвестный";
            last_name = "Неизвестный";
            age = 9;
            average_score = 2f;
        }

        public Student(string name, string last_name, int age, float average_score) {
            Name = name;
            Last_name = last_name;
            Age = age;
            Average_score = average_score;
        }

        // Для получения строки с полным именем
        public string Get_full_name(){
            return $"{name} {last_name}";
        }

        public void Print(){
            Console.WriteLine($"{name}\t {last_name}\t");
        }

        public void PrintAllInformation() {
            Console.WriteLine($"{name}\t {last_name}\t {age}\t {average_score}");
        }

        // для возможности сериализации и десериализации
        [JsonInclude]
        public string Name
        {
            get => name;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value)) {
                    Console.WriteLine("Ошибка в присвоении Имени. Значение не может быть пустым! теперь она неизвестный");
                    name = "Неизвестный";
                } else if (value.Length > 50) {
                    name = "Длинный";
                } else 
                name = value;
            }
        }
        
        [JsonInclude]
        public string Last_name
        {
            get => last_name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) {
                    Console.WriteLine("Ошибка в присвоении фамилии. Значение не может быть пустым! теперь она неизвестный");
                    last_name = "Неизвестный";
                } else if (value.Length > 50) {
                    last_name = "Длинный";
                } else 
                last_name = value;
            }
        }

        [JsonInclude]
        public int Age
        {
            get => age;
            private set
            {
                if (value < 0) {
                    Console.WriteLine("Ошибка в присвоении возраста. Возраст не может быть отрицательным! Теперь он равен 0");
                    age = 0;
                }
                else age = value;
            }
        }

        [JsonInclude]
        public float Average_score
        {
            get => average_score;
            private set
            {
            
                if (value < 0) {
                    Console.WriteLine("Ошибка в присвоении среднего балла. Он не может быть отрицательным.");
                    average_score = 0;
                } else if (value > 5) {
                    Console.WriteLine("Ошибка в присвоении среднего балла. Он не может быть больше 5");
                    average_score = 0;
                }
                else average_score = value;
            }
        }

        
    }

    class University{
        private string name;
        private List<Student> list_of_students = new List<Student>();

        // инициализация
        public University() {
            name = "неизвестный";
            list_of_students = new List<Student>{};
        }
        public University(string name){
            this.name = name;
        }
        public University(string name, List<Student> list_of_students): this(name){
            this.list_of_students = list_of_students;
        }

        // добавление студента в университет
        public void Add(Student student){
            list_of_students.Add(student);
        }

        // поиск студента в университете
        public int Search(Student student){
            List<Student> new_list = this.list_of_students;
            for (int i = 0; i < new_list.Count; i++)
            {
                if (new_list[i] == student) {
                    return i;
                }
            }
            return 9999;
            
        }

        // удаление студента из университета
        public void Delete(Student student){
            int ind = this.Search(student);
            list_of_students.RemoveAt(ind);
        }

        // вывод данных обо всех студентах университета
        public void Print(){
            Console.WriteLine($"Студенты университета {this.name}");
            foreach(Student stud in this.list_of_students){
                stud.Print();
            }
        }

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
        
        [JsonInclude]
        public List<Student> List_of_students
        {
            get => list_of_students;
            private set => list_of_students = value;
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

    class Program{
        static void Main() 
        {
            StudentsRepository uploader_json = new("UniversitiesInformation.json");
            UniversityAndStudents.Student Bob = new("Боб", "Видосов", 18, 4.5f);
            UniversityAndStudents.Student Igor = new("Игорь", " \n \t", -4, 4.8f);
            UniversityAndStudents.Student Egor = new("Егор", "Перевозчиков", 19, 3.7f);
            UniversityAndStudents.University Mospolytech = new("Mocковский политех");
            Mospolytech.Add(Bob);
            Mospolytech.Add(Igor);
            Mospolytech.Add(Egor);

            uploader_json.Pull_to_json_file(Mospolytech);
            List<UniversityAndStudents.University> universities = uploader_json.Get_from_json_file(Mospolytech);

            foreach (UniversityAndStudents.University university in universities){
                university.Print();
            }
        }
    }
}
