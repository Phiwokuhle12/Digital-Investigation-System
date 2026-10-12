namespace DigitalInvestigationAPI.Models
{
    public class InvestigationResult
    {
        public int InvestigationID { get; set; }
        public String SuspectName { get; set; } = "";
        public String Conclusion { get; set; } = "";
        public DateTime DateStarted { get; set; }
    }
}
