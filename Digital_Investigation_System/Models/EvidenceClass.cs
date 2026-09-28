using System.ComponentModel.DataAnnotations;

namespace DigitalInvestigationAPI.Models
{
    public class EvidenceClass
    {
        [Key]
        public int EvidenceID { get; set; }
        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";
        [Required]
        public string Location { get; set; } = "";
    }
}
 