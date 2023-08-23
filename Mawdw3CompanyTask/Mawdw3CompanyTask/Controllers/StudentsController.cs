using BAL.Interfaces;
using Entity.DTO_s;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IStudentsService studentService;
        private readonly ILogger<StudentsController> logger;
        public StudentsController(IStudentsService studentService, ILogger<StudentsController> logger)
        {
            this.studentService = studentService;
            this.logger = logger;
        }

        [HttpPost("AddStudent")]
        public Student AddStudent([FromBody]Student student)
        {
            return this.studentService.AddStudent(student); 
        }

        [HttpPut("UpdateStudentInformation")]
        public Student UpdateStudentInformation(Student student)
        {
            return studentService.UpdateStudentInformation(student);
        }
        [HttpDelete("DeleteStudent")]
        public IActionResult DeleteStudent(int id )
        {
            if(studentService.DeleteStudent(id))
            return Ok("user Deleted successfully .");

            return BadRequest("cannot Delete user .");
        }
        [HttpGet("getAllStudent")]
        public IEnumerable<Student> getAllStudent()
        {
            return studentService.getAllStudent();
        }
        [HttpGet("getStudentByID")]
        public Student getStudentByID(int studentID )
        {
            return studentService.getStudentByID(studentID);
        }
        [HttpPost("SearchForStudent")]
        public IEnumerable<studentDto> SearchForStudent(string name , int grade)
        {
            return studentService.SearchForStudent(name, grade);
        }
    }
}
