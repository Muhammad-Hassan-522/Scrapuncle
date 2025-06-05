namespace Scrapuncle.Models
{
    public class AdminDashboard
    {
        public int TotalUsers { get; set; }
        public int TotalDealers { get; set; }
        public int TotalPickups { get; set; }
        public List<Pickup> PendingPickups { get; set; }
    }
}
