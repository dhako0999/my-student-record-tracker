using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace HelloWorld
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _dataFilePath;

        public StudentRepository(string dataFilePath)
        {
            _dataFilePath = dataFilePath;
        }


        public List<Student> Load()
        {
            if (!File.Exists(_dataFilePath))
            {
                return new List<Student>();
            }

            try
            {
                string json = File.ReadAllText(_dataFilePath);
                return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
            catch
            {
                return new List<Student>();
            }
        }

        public void Save(List<Student> students)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            string json = JsonSerializer.Serialize(students, options);
            File.WriteAllText(_dataFilePath, json);
        }
    }
}