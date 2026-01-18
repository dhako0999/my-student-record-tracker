using System.Collections.Generic;
using HelloWorld;

namespace HelloWorld.Tests
{
    public class FakeStudentRepository : IStudentRepository
    {
        private List<Student> _students;

        public int SaveCallCount { get; private set; }

        public FakeStudentRepository(List<Student> initialStudents)
        {
            _students = new List<Student>(initialStudents);
        }

        public List<Student> Load()
        {
            return new List<Student>(_students);
        }

        public void Save(List<Student> students)
        {
            SaveCallCount++;
            _students = new List<Student>(students);
        }
        
        
    }
}