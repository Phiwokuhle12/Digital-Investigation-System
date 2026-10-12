namespace DigitalInvestigationAPI.Models
{
    public class Evidence
    {
        public int EvidenceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCollected { get; set; }
    }
}
