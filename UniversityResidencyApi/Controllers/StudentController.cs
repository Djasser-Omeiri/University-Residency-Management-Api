using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessAccessLayer;
using DataAccessLayer;

namespace UniversityResidencyApi.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> AddStudent(StudentDTO newStudent)
        {
            if (newStudent is null || string.IsNullOrEmpty(newStudent.FirstName) || string.IsNullOrEmpty(newStudent.LastName) || newStudent.Age < 16
                || newStudent.RoomID <= 0)
            {
                return BadRequest("Student data is invalid.");
            }

            Student student = new Student(new StudentDTO(newStudent.StudentID, newStudent.FirstName, newStudent.LastName, newStudent.Age, newStudent.RoomID));
            if (!student.Save())
            {
                return StatusCode(500, "Failed to add the student");
            }
            newStudent.StudentID = student.StudentID;
            return CreatedAtAction(nameof(GetStudent), new { id = newStudent.StudentID }, newStudent);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudent(int id, StudentDTO updatedStudent)
        {
            if (updatedStudent is null || string.IsNullOrEmpty(updatedStudent.FirstName) || string.IsNullOrEmpty(updatedStudent.LastName) || updatedStudent.Age < 16
                || updatedStudent.RoomID <= 0)
            {
                return BadRequest("Student data is invalid.");
            }
            Student student = Student.GetStudentByID(id);
            if (student is null)
            {
                return NotFound();
            }
            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.Age = updatedStudent.Age;
            student.RoomID = updatedStudent.RoomID;
            if (!student.Save())
            {
                return StatusCode(500, "Failed to update the student");
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> GetStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest("The student id is invalid.");
            }
            Student student = Student.GetStudentByID(id);
            if (student is null)
            {
                return NotFound();
            }
            StudentDTO studentDTO = student.SDTO;
            return Ok(studentDTO);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {
            List<StudentDTO> studentDtos = Student.GetAllStudents();
            if (studentDtos == null || studentDtos.Count == 0)
            {
                return NotFound("No Students Found");
            }
            return Ok(studentDtos);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest("The student id is invalid.");
            }
            if (Student.DeleteStudent(id))
                return NoContent();
            else
                return NotFound("The student was not found.");
        }


    }
}
