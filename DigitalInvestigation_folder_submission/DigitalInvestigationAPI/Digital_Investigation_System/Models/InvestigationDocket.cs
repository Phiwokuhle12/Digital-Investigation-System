namespace DigitalInvestigationAPI.Models
{
    public class InvestigationDocket
    {
        public int DocketID { get; set; }

        public string DocketNumber { get; set; } = "";

        public int InvestigationID { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Status { get; set; } = "";
    }
}
