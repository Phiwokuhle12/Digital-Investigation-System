using System.ComponentModel.DataAnnotations;

namespace DigitalInvestigationAPI.Models
{
    public class CaseClass
    {
        [Key]
        public int CaseID { get; set; }
        [Required]
        public string CaseName { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        [Required]
        public string Status { get; set; } = "";
    }
}
