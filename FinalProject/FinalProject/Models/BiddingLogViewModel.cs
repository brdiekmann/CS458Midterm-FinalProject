namespace FinalProject.Models
{
    public class BiddingLogViewModel
    {
        public string Status { get; set; }
        public string OperatorId { get; set; }
        public string Operation {  get; set; }
        public string Note { get; set; }
        public DateTime Timestamp { get; set; }
        public int ProjectBidId { get; set; }
    }
}
