
using DigitalInvestigationAPI.Data;
using DigitalInvestigationAPI.Models;
using DigitalInvestigationAPI.Repositories;
using DigitalInvestigationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalInvestigationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestigationController : ControllerBase
    {
        private readonly DigitalInvestigationContext _context;
        private readonly InvestigationRepository _repository;
        private readonly InvestigationDocketService _docketService;

        public InvestigationController(
            DigitalInvestigationContext context,
            InvestigationRepository repository,
            InvestigationDocketService docketService)
        {
            _context = context;
            _repository = repository;
            _docketService = docketService;
        }

        // =====================================================
        // GET ALL INVESTIGATIONS
        // =====================================================

        [HttpGet]
        public async Task<IActionResult> GetInvestigation()
        {
            var investigations = await _context.Investigations.ToListAsync();

            return Ok(investigations);
        }

        // =====================================================
        // GET INVESTIGATION BY ID
        // =====================================================

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInvestigationById(int id)
        {
            var investigation = await _context.Investigations
                .FindAsync(id);

            if (investigation == null)
            {
                return NotFound("Investigation not found");
            }

            return Ok(investigation);
        }

        // =====================================================
        // SUBMIT INVESTIGATION AND GENERATE DOCKET
        // =====================================================

        [HttpPost]
        public async Task<IActionResult> CreateInvestigation(
            InvestigationClass newInvestigation)
        {
            // =================================================
            // CHECK SUSPECT
            // =================================================

            if (newInvestigation.SuspectID <= 0)
            {
                return BadRequest("A suspect must be selected.");
            }

            // =================================================
            // CHECK CONCLUSION
            // =================================================

            if (string.IsNullOrWhiteSpace(newInvestigation.Conclusion))
            {
                return BadRequest("A conclusion is required.");
            }

            // =================================================
            // CHECK THAT THE CASE EXISTS
            // =================================================

            var caseExists = await _context.Cases
                .AnyAsync(c => c.CaseID == newInvestigation.CaseID);

            if (!caseExists)
            {
                return BadRequest("The selected case does not exist.");
            }

            // =================================================
            // CHECK THAT THE SUSPECT EXISTS
            // =================================================

            var suspectExists = await _context.Suspects
                .AnyAsync(s => s.SuspectID == newInvestigation.SuspectID);

            if (!suspectExists)
            {
                return BadRequest("The selected suspect does not exist.");
            }

            // =================================================
            // SET INVESTIGATION DATE
            // =================================================

            newInvestigation.DateStarted = DateTime.UtcNow;

            // =================================================
            // SAVE INVESTIGATION
            // =================================================

            _context.Investigations.Add(newInvestigation);

            await _context.SaveChangesAsync();

            // =================================================
            // GENERATE PDF DOCKET
            // =================================================

            byte[] pdfBytes;

            try
            {
                pdfBytes = await _docketService.GenerateDocketAsync(
                    newInvestigation.InvestigationID
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    $"Investigation was saved, but the docket could not be generated. Error: {ex.Message}"
                );
            }

            // =================================================
            // GENERATE DOCKET NUMBER
            // =================================================

            string docketNumber =
                $"TP-{newInvestigation.DateStarted.Year}-{newInvestigation.InvestigationID:D4}";

            // =================================================
            // GENERATE PDF FILE NAME
            // =================================================

            string fileName =
                $"TracePoint-Docket-{docketNumber}.pdf";

            // =================================================
            // RETURN PDF TO REACT
            // =================================================

            return File(
                pdfBytes,
                "application/pdf",
                fileName
            );
        }

        // =====================================================
        // GET INVESTIGATION RESULTS USING DAPPER
        // =====================================================

        [HttpGet("results")]
        public async Task<IActionResult> GetInvestigationResults()
        {
            var results = await _repository.GetInvestigationResults();

            return Ok(results);
        }
    }
}
