using DigitalInvestigationAPI.Data;
using DigitalInvestigationAPI.Models;
using DigitalInvestigationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigitalInvestigationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestigationController : ControllerBase
    {
        private readonly DigitalInvestigationContext _context;
        private readonly InvestigationRepository _repository;
        public InvestigationController(DigitalInvestigationContext context, InvestigationRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        //This endpoint/Api will get all investigations in our database
        [HttpGet]
        public async Task<IActionResult> GetInvestigation()
        {
            var investigations = await _context.Investigations.ToListAsync();
            return Ok(investigations);
        }

        //The Api will get a student by it's id.
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetInvestigationById( int id)
        {
            var investigation = await _context.Investigations.FindAsync(id);
            if (investigation == null)
            {
                return NotFound("Investigation not found");
            }
            return Ok(investigation);
        }

        // This APi will submit new investigations

        [HttpPost]
        public async Task<IActionResult> CreateInvestigation(InvestigationClass newInvestigation)
        {
            if(newInvestigation.SuspectID <= 0)
            {
                return BadRequest("A suspect must be selected.");
            }
            if (string.IsNullOrWhiteSpace(newInvestigation.Conclusion))
            {
                return BadRequest("A conclusion is required.");
            }

            newInvestigation.DateStarted = DateTime.Now;
            _context.Investigations.Add(newInvestigation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetInvestigationById), new { id = newInvestigation.InvestigationID }, newInvestigation);
        }

        //This API will get investigation Results using dapper
        //EF Core is used for the normal database operations and,
        //Dapper is used for the required investigation-results operation.
        [HttpGet("results")]
        public async Task<IActionResult> GetInvestigationResults()
        {
            var results = await _repository.GetInvestigationResults();
            return Ok(results);
        }
    }
}
