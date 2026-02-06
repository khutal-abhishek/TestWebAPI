using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebAPI.Model;

namespace TestWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly TestDbContext _context;

        public TestController(TestDbContext context)
        {
            _context = context;
        }

        // ✅ GET: /Test
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Tests.ToListAsync();
            return Ok(data);
        }

        // ✅ GET: /Test/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null)
                return NotFound();

            return Ok(test);
        }

        // ✅ POST: /Test
        [HttpPost]
        public async Task<IActionResult> Create(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = test.Id }, test);
        }

        // ✅ PUT: /Test/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Test test)
        {
            if (id != test.Id)
                return BadRequest("ID mismatch");

            var exists = await _context.Tests.AnyAsync(x => x.Id == id);
            if (!exists)
                return NotFound();

            _context.Entry(test).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE: /Test/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null)
                return NotFound();

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
