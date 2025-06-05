namespace Scrapuncle.Models
{
    public class DealerDashboard
    {
        public int TotalPickups { get; set; }
        //public int PendingPickups { get; set; }
        public int CompletedToday { get; set; }
        public List<Pickup> RecentPickups { get; set; }
    }
}
