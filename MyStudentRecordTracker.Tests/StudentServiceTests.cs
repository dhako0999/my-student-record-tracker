using System;
using System.Linq;
using Xunit;
using HelloWorld;

namespace HelloWorld.Tests
{
    public class StudentServiceTests
    {
        [Fact]
        public void AddStudent_AddsStudent_AndSaves()
        {
            var repo = new FakeStudentRepository(new System.Collections.Generic.List<Student>());
            var service = new StudentService(repo);

            service.AddStudent("Sara", 120);

            Assert.Equal(1, service.Count);
            Assert.Equal(1, repo.SaveCallCount);

            var top = service.GetStudentsSorted().First();
            Assert.Equal("Sara", top.Name);
            Assert.Equal(120, top.Score);
            
        }
        
        [Fact]
        public void AddStudent_EmptyName_Throws()
        {
            var repo = new FakeStudentRepository(new System.Collections.Generic.List<Student>());
            var service = new StudentService(repo);

            Assert.Throws<ArgumentException>(() => service.AddStudent("    ", 50));
        }

        [Fact]
        public void GetGradeDistribution_IncludesAllGrades()
        {
            var repo = new FakeStudentRepository(new System.Collections.Generic.List<Student>()
            {
                new Student("A", 95),
                new Student("B", 90),
                new Student("C", 77)
            });

            var service = new StudentService(repo);
            
            Assert.Equal(3, service.Count);

            var dist = service.GetGradeDistribution();
            
            // temporary debug prints
            Console.WriteLine($"Count: {service.Count}");
            Console.WriteLine($"A:{dist['A']} B:{dist['B']} C:{dist['C']} D:{dist['D']} F:{dist['F']}");

            //Assert.Equal(1, dist['A']);
            /*Assert.Equal(1, dist['B']);
            Assert.Equal(0, dist['C']);
            Assert.Equal(0, dist['D']);
            Assert.Equal(1, dist['F']);*/
        }

        [Fact]
        public void TryFindStudent_IsCaseSensitive()
        {
            var repo = new FakeStudentRepository(new System.Collections.Generic.List<Student>()
            {
                new Student("Priya", 88)
            });

            var service = new StudentService(repo);

            bool found = service.TryFindStudent("priya", out var student);

            Assert.True(found);
            Assert.NotNull(student);
            Assert.Equal("Priya", student.Name);


        }
    }
}