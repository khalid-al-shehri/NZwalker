using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZwalker.Controllers{

    [Route("api/[controller]")]
    [ApiController]

    public class Students1Controller: ControllerBase {

        [HttpGet]
        public IActionResult GetAllStudents(){
            string[] students = new string[] {"Khalid", "Mohammed", "Saeed", "Hassan"};

            return Ok(students);
        }

    }

}