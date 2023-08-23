using Entity.DTO_s;
using Entity.Models;

namespace DAL.Interfaces
{
    public interface IStudentsRepository
    {
        public Student AddStudent(Student student);
        public Student UpdateStudentInformation(Student student);
        public bool DeleteStudent(int id);
        public IEnumerable<Student> getAllStudent();
        public Student getStudentByID(int studentID);
        public IEnumerable<studentDto> SearchForStudent(string name, int grade);


    }
}
