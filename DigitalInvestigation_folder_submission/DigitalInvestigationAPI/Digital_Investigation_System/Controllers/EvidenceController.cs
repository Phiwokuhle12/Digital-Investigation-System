using DigitalInvestigationAPI.Data;
using DigitalInvestigationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigitalInvestigationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvidenceController : ControllerBase
    {
        private readonly DigitalInvestigationContext _context;

        public EvidenceController(DigitalInvestigationContext context)
        {
            _context = context;
        }

        //Get api/evidence all evidence
        [HttpGet]
        public async Task<IActionResult> GetEvidence()
        {
            var evidence = await _context.Evidence.ToListAsync();

            return Ok(evidence);
        }

        //Get api/evidence/{id} evidence by id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEvidenceById(int id)
        {
            var evidence = await _context.Evidence.FindAsync(id);
            if (evidence == null)
            {
                return NotFound("Evidence not found");
            }
            return Ok(evidence);
        }
        // Replace the CreatedEvidence method with the following:

        [HttpPost]
        public async Task<IActionResult> CreatedEvidence(EvidenceClass evidence)
        {
            _context.Evidence.Add(evidence);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvidenceById), new { id = evidence.EvidenceID }, evidence);
        }

        // Fix for CS0136: Renamed the local variable in UpdateEvidence method to avoid conflict with the parameter name.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEvidence(int id, EvidenceClass evidence)
        {
            var existingEvidence = await _context.Evidence.FindAsync(id);
            if (existingEvidence == null)
            {
                return NotFound("Evidence not found");
            }
            existingEvidence.Title = evidence.Title;
            existingEvidence.Description = evidence.Description;
            existingEvidence.Location = evidence.Location;
            await _context.SaveChangesAsync();
            return Ok($"Evidence with ID {id} updated.");
        }

        //This is the endpoint to delete evidence on the database
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvidence(int id)
        {
            var evidence = await _context.Evidence.FindAsync(id);
            if(evidence == null)
            {
                return NotFound("Evidence not found");
            }
            _context.Evidence.Remove(evidence);
            await _context.SaveChangesAsync();

            return Ok($"Evidence with ID {id} delete.");
        }

    }
}
