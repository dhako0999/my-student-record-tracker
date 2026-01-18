using System;
using System.Collections.Generic;
using System.Linq;


namespace HelloWorld
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;
        private readonly List<Student> _students;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
            _students = _repository.Load();
        }

        public int Count => _students.Count;

        public void AddStudent(string name, int score)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty");
            }

            var student = new Student(name, score);
            _students.Add(student);
            _repository.Save(_students);
        }

        public List<Student> GetStudentsSorted()
        {
            return _students.OrderByDescending(s => ScoreUtils.ClampScore(s.Score)).ToList();
        }

        public Dictionary<char, int> GetGradeDistribution()
        {
            var counts = _students
                .GroupBy(s => ScoreUtils.LetterGrade(s.Score))
                .ToDictionary(g => g.Key, g => g.Count());

            char[] grades = { 'A', 'B', 'C', 'D', 'F' };
            var result = new Dictionary<char, int>();

            foreach (char grade in grades)
            {
                result[grade] = counts.TryGetValue(grade, out int count) ? count : 0;
            }

            return result;
        }


        public bool TryFindStudent(string name, out Student student)
        {
            student = null;

            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            student = _students.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));

            return student != null;
        }
    }
}