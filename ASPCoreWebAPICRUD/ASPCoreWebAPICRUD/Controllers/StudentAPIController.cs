using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly CodeFirstDBContext context;

        // CodeFirstDBContext is a database name
        public StudentAPIController(CodeFirstDBContext context)
        {
            this.context = context;
        }

        //Get All Students
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);
        }

        //GET SINGLE Students
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }


        //GET SINGLE Students
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student std)
        {
            await context.Students.AddAsync(std);
            await context.SaveChangesAsync();

            return Ok(std);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id,Student std)
        { 
            if(id != std.Id)
            {
                return BadRequest();
            }

            context.Entry(std).State = EntityState.Modified;
            await context.SaveChangesAsync();
            
            return Ok(std);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var std = await context.Students.FindAsync(id);
            if(std == null)
            {
                return BadRequest();
            }
            context.Students.Remove(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }
    }
    }
