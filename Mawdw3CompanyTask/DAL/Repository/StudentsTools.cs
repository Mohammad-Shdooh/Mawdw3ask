using DAL.Interfaces;
using Entity.DTO_s;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class StudentsTools :IStudentsRepository 
    {
        private readonly Mawdw3TaskContext context;
        public StudentsTools(Mawdw3TaskContext context)
        {
            this.context = context;
        }

        public Student AddStudent(Student student)
        {
            try
            {
                var Entity = context.Students.Add(student).Entity;
                context.SaveChanges();
                return Entity;
            }catch(Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        public Student UpdateStudentInformation(Student student)
        {
            try
            {
                var Entity = context.Students.Update(student).Entity;
                context.SaveChanges();
                return Entity;
            }
            catch (Exception ex )
            {
                throw new NotFoundException(ex.Message);
            }

        }
        public bool DeleteStudent(int id)
        {
            try
            {
                var Entity = context.Students.Find(id);
               
                context.Students.Remove(Entity);
                context.SaveChanges(); 
                return true;
            }catch(Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }
        public IEnumerable<Student> getAllStudent()
        {
            try
            {
                var students = context.Students.ToList();
                if(students == null)
                {
                    throw new NotFoundException("There are no student .");

                }
                return students;

            }catch(Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        public Student getStudentByID(int studentID)
        {
            try
            {
                var Entity = context.Students.Find(studentID);
                if(Entity == null)
                {
                    throw new NotFoundException("User not found.");
                }
                return Entity;
            }
            catch(Exception ex)
            {
                throw new NotFoundException(ex.Message);
                
            }

        }
        public IEnumerable<studentDto> SearchForStudent(string name, int grade)
        {
            try
            {
                var listOfEntity = context.Students.Join(
               context.Grades,
               student => student.GradeId,
               grade => grade.Id,
               (student, grade) => new { Student = student, Grade = grade }
               )
               .Where(obj => ((obj.Student.FirstName == name ||
                              obj.Student.LastName == name ||
                              obj.Student.FirstName + " " + obj.Student.LastName == name) &&
                              obj.Grade.Grade1 == grade)
                             )
               .Select(
               obj => new studentDto
               {
                   Id = obj.Student.Id,
                   FullName = obj.Student.FirstName + " " + obj.Student.LastName,
                   gradeName = obj.Grade.Name
               })
               .ToList();
                return listOfEntity;
            }catch(Exception ex)
            {
                 throw new BadRequestException(ex.Message);
            }
             
        }

    }
}
