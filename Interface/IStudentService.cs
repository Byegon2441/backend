using System.Collections.Generic;

namespace backend.Interface
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudent();
        Student GetStudentByID(string lineUserId);



    }
}