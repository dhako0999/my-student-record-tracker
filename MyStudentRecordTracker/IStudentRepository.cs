using System.Collections.Generic;

namespace HelloWorld
{
    public interface IStudentRepository
    {
        List<Student> Load();
        void Save(List<Student> students);
        
        
    }
}