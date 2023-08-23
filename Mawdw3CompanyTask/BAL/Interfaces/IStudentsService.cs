using Entity.DTO_s;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IStudentsService
    {
        public Student AddStudent(Student student);
        public Student UpdateStudentInformation(Student student);
        public bool DeleteStudent(int id);
        public IEnumerable<Student> getAllStudent();
        public Student getStudentByID(int studentID);
        public IEnumerable<studentDto> SearchForStudent(string name, int grade);

    }
}
