using System.ComponentModel.DataAnnotations;

namespace DigitalInvestigationAPI.Models
{
    public class SuspectClass
    {
        [Key]
        public int SuspectID { get; set; }
        [Required]
        public string SuspectName { get; set; } = " ";
        [Required]
        public string Occupation { get; set; } = " ";
        [Required]
        public string Description { get; set; } = " ";
        
    }
}
