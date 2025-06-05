namespace Scrapuncle.Models.Interface
{
    public interface IPickupRepository
    {
        public List<Pickup> GetAllPickups();
        public Pickup? GetPickupById(int id);
        public void AddPickup(Pickup pickup);
        public bool UpdatePickupStatus(int pickupId, string status, string dealerId);
        public void DeletePickup(int id);
        public bool CancelPickup(int pickupId);
        public List<Pickup> GetUserPickups(string username, string status);
        public List<Pickup> GetDealerPickups(string dealerId);

    }
}
