using System.ComponentModel.DataAnnotations;

namespace DigitalInvestigationAPI.Models
{
    public class InvestigationClass
    {
        [Key] 
        public int InvestigationID { get; set; }

        [Required] 
        public int CaseID { get; set; }

        [Required] 
        public int SuspectID { get; set; }

        [Required]
        public string Conclusion { get; set; } = ""; // Stores the user's drawn signature


        [Required] 
        public DateTime DateStarted { get; set; }
    }
}