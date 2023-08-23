using BAL.Interfaces;
using DAL.Interfaces;
using DAL.Repository;
using Entity.DTO_s;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class StudentsToolsService : IStudentsService
    {
        private readonly IStudentsRepository studentRepo;
        public StudentsToolsService(IStudentsRepository studentRepo)
        {
            this.studentRepo = studentRepo;
        }

        public Student AddStudent(Student student)
        {
            return studentRepo.AddStudent(student);
        }
        public Student UpdateStudentInformation(Student student)
        {
            return studentRepo.UpdateStudentInformation(student);
        }
        public bool DeleteStudent(int id)
        {
            return studentRepo.DeleteStudent(id);
        }
        public IEnumerable<Student> getAllStudent() 
        {
            return studentRepo.getAllStudent();
        }
        public Student getStudentByID(int studentID)
        {
            return studentRepo.getStudentByID(studentID);
        }
        public IEnumerable<studentDto> SearchForStudent(string name, int grade)
        {
            return studentRepo.SearchForStudent(name, grade);
        }
    }
}
