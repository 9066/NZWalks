using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    // https:localhost:portNumber/api/Student
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudent()
        {
            string[] studentNames = new string[] { "Brajesh", "Suyash", "Dinkar" };
            return Ok(studentNames); 
        }
    }
}
