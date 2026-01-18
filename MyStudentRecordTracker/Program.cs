using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace HelloWorld
{
    
    internal class Program
    {
        private const string DataFile = "students.json";
        

        static void SaveStudents(List<Student> students)
        {

            try
            {
                Console.WriteLine("Saving to: " + Path.GetFullPath(DataFile));
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(students, options);
                File.WriteAllText(DataFile, json);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving students: " + ex.Message);
            }

           
        }

        static List<Student> LoadStudents()
        {
            if (!File.Exists(DataFile))
            {
                return new List<Student>();
            }

            string json = File.ReadAllText(DataFile);

            return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
        }
       /* static void AddStudent(List<Student> students)
        {

            Console.Write("Please enter first name: ");

            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty!");
                return;
            }

            Console.Write("Please enter score: ");

            string score = Console.ReadLine();

            if (!int.TryParse(score, out int scoreInt))
            {
                Console.WriteLine("Score must be an integer!");
                return;
            }

            var student = new Student(name, scoreInt);
            students.Add(student);

            SaveStudents(students);
            Console.WriteLine("Student added!");
        } */

       static void AddStudent(StudentService service)
       {
           Console.Write("Enter name: ");
           string name = Console.ReadLine();

           Console.Write("Enter score (integer): ");
           string input = Console.ReadLine();

           if (!int.TryParse(input, out int score))
           {
               Console.WriteLine("Invalid score");
               return;
           }


           try
           {
                service.AddStudent(name, score);
                Console.WriteLine("Student added");
           }
           catch (ArgumentException ex)
           {
               Console.WriteLine(ex.Message);
           }
       }

        /*static void ListStudentsSorted(List<Student> students)
        {
            var listSortedStudents = students.OrderByDescending(s => ScoreUtils.ClampScore(s.Score)).ToList();

            foreach (var student in listSortedStudents)
            {
                int clampedScore = ScoreUtils.ClampScore(student.Score);
                
                Console.WriteLine($"{student.Name}: {ScoreUtils.LetterGrade(clampedScore)}");
            }
            
        }*/

        static void ListStudentsSorted(StudentService service)
        {
            var sorted = service.GetStudentsSorted();

            foreach (var student in sorted)
            {
                int clampedScore = ScoreUtils.ClampScore(student.Score);
                char grade = ScoreUtils.LetterGrade(student.Score);
                
                Console.WriteLine($"{student.Name} : {clampedScore} : Grade: ({grade})");
            }
        }

        /*static void PrintGradeDistribution(List<Student> students)
        {
            var gradeCounts = students.GroupBy(s => ScoreUtils.LetterGrade(s.Score))
                .ToDictionary(g => g.Key, g => g.Count());

            char[] grades = { 'A', 'B', 'C', 'D', 'F' };

            foreach (var grade in grades)
            {
                int count = gradeCounts.ContainsKey(grade) ? gradeCounts[grade] : 0;
                
                Console.WriteLine($"{grade}: {count}");
            }
            
            

        }*/
        
        static void PrintGradeDistribution(StudentService service)
        {
            var dist = service.GetGradeDistribution();

            foreach (var kvp in dist)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }

       /* static void FindStudentByName(List<Student> students)
        {
            Console.Write("Please enter first name: ");

            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty!");
                return;
            }

            var studentByName = students.ToDictionary(s => s.Name);

            if (studentByName.TryGetValue(name, out var student))
            {
                Console.WriteLine($"{student.ScoreMessage()}");
            }

        }*/

       static void FindStudentByName(StudentService service)
       {
           Console.Write("Enter your name: ");
           string name = Console.ReadLine();

           if (service.TryFindStudent(name, out var student))
           {
               Console.WriteLine(student.ScoreMessage());
           }
           else
           {
               Console.WriteLine("Student not found.");
           }
       }
        
        public static void Main(string[] args)
        {

           /* var students = LoadStudents();
            Console.WriteLine($"Loaded {students.Count} students from disk.");*/
           
           string dataPath = Path.Combine(AppContext.BaseDirectory, "students.json");

           var repository = new StudentRepository(dataPath);
           var service = new StudentService(repository);

           Console.WriteLine($"Loaded {service.Count} students from disk.");


            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Student Tracker === ");
                Console.WriteLine("1) Add Student");
                Console.WriteLine("2) List students (sorted)");
                Console.WriteLine("3) Grade Distribution");
                Console.WriteLine("4) Find student by name");
                Console.WriteLine("5) Quit");

                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                /*switch (choice)
                {
                    case "1":
                        AddStudent(students);
                        break;
                    case "2":
                        ListStudentsSorted(students);
                        break;
                    case "3":
                        PrintGradeDistribution(students);
                        break;
                    case "4":
                        FindStudentByName(students);
                        break;
                    case "5":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please select a number between 1 and 5");
                        break;
                }*/

                switch (choice)
                {
                    case "1":
                        AddStudent(service);
                        break;
                    case "2":
                        ListStudentsSorted(service);
                        break;
                    case "3":
                        PrintGradeDistribution(service);
                        break;
                    case "4":
                        FindStudentByName(service);
                        break;
                    case "5":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose between 1 and 5.");
                        break;
                }
            }
        }
        
        
        
    }
}

