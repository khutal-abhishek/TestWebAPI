using Microsoft.AspNetCore.Mvc;
using TestWebAPI.Model;

namespace TestWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly TestDbContext _context;

        public TestController(TestDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTests()
        {
            var Test = _context.Tests.ToList();
            return Ok(Test);
        }

        [HttpPost]
        public IActionResult SaveTests(Test test)
        {
            _context.Tests.Add(test);
            _context.SaveChanges();
            return Ok(new {
                message = "test save successfully", 
                data =test
            });
        }

        [HttpPut]
        public IActionResult UpdateTest(Test test)
        {
            var record = _context.Tests.Find(test.Id);           
            if (record != null)
            {
                record.Name = test.Name;
                record.Phone = test.Phone;
                _context.SaveChanges();
                return Ok(new {
                    message = "test updated successfully", 
                    data =record
                });
            }
            else
            {
                 return NotFound(new { status="Error",message = "test not found" });
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteTest(int id)
        {
            var record = _context.Tests.Find(id);
            if (record != null)
            {
                _context.Tests.Remove(record);
                _context.SaveChanges();
                return Ok(new { message = "test deleted successfully" });
            }
            else
            {
                return NotFound(new { status = "Error", message = "test not found" });
            }
        }

    }
}
