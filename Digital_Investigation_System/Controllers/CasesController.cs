using DigitalInvestigationAPI.Data;
using Microsoft.AspNetCore.Mvc;
using DigitalInvestigationAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
 
namespace DigitalInvestigationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly DigitalInvestigationContext _context;

        public CasesController(DigitalInvestigationContext context)
        {
            _context = context;
        }

        //This method retrieves all cases from the database and returns them as a JSON response.
        [HttpGet]
        public async Task<IActionResult> GetCases()
        {
            var cases = await _context.Cases.ToListAsync();
            return Ok(cases);

        }
        //This method retrieves a specific case by its ID from the database and returns it as a JSON response. If the case is not found, it returns a 404 Not Found response.
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCaseById(int id)
        {
            var caseItem = await _context.Cases.FindAsync(id);
            if (caseItem == null)
            {
                return NotFound("Case not found");
            }
            return Ok(caseItem);
        }

        //This method creates a new case in the database. It accepts a CaseClass object as input, adds it to the database, and returns a 201 Created response with the location of the newly created case.
        [HttpPost]
        public async Task<IActionResult> CreateCase(CaseClass caseItem)
        {
            _context.Cases.Add(caseItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCaseById), new { id = caseItem.CaseID }, caseItem);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCase(int id, CaseClass caseItem)
        {
            var existingCase = await _context.Cases.FindAsync(id);
            if (existingCase == null)
            {
                return NotFound("Case not found");
            }
            existingCase.CaseName = caseItem.CaseName;
            existingCase.Description = caseItem.Description;
            existingCase.Status = caseItem.Status;
            await _context.SaveChangesAsync();
            return Ok(existingCase);
        }

        //This method deletes a specific case by its ID from the database. If the case is found, it removes it and returns a 200 OK response with a message indicating that the case was deleted. If the case is not found, it returns a 404 Not Found response.
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCase(int id)
        {
            var caseItem = await _context.Cases.FindAsync(id);
            if (caseItem == null)
            {
                return NotFound("Case not found");
            }
            _context.Cases.Remove(caseItem);
            await _context.SaveChangesAsync();
            return Ok($"Case with ID {id} deleted.");
        }

    }
}
