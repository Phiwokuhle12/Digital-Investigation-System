using DigitalInvestigationAPI.Data;
using DigitalInvestigationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigitalInvestigationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuspectsController : ControllerBase
    {
        private readonly DigitalInvestigationContext _context;
        public SuspectsController(DigitalInvestigationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetSuspects()
        {
            var suspects = await _context.Suspects.ToListAsync();
            return Ok(suspects);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSuspectById(int id)
        {
            var suspect = await _context.Suspects.FindAsync(id);
            if (suspect == null)
            {
                return NotFound("Suspect not found");
            }
            return Ok(suspect);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSuspect(SuspectClass suspect)
        {
            _context.Suspects.Add(suspect);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSuspectById), new { id = suspect.SuspectID }, suspect);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSuspect(int id, SuspectClass suspect)
        {
            var existingSuspect = await _context.Suspects.FindAsync(id);
            if (existingSuspect == null)
            {
                return NotFound("Suspect not found");
            }
            existingSuspect.SuspectName = suspect.SuspectName;
            existingSuspect.Occupation = suspect.Occupation;
            existingSuspect.Description = suspect.Description;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //This  method deletes a suspect using it's id

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSuspect(int id)
        {
            var suspect = await _context.Suspects.FindAsync(id);
            if (suspect == null)
            {
                return NotFound("Suspect not found");
            }
            _context.Suspects.Remove(suspect);
            await _context.SaveChangesAsync();
            return Ok("Suspect deleted on the database");
        }

    }
}
