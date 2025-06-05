using Microsoft.EntityFrameworkCore;
using Scrapuncle.Data;
using Scrapuncle.Models.Interface;

namespace Scrapuncle.Models.Repositories
{
    public class PickupRepository: IPickupRepository
    {
        private readonly ApplicationDbContext db;

        public PickupRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Pickup> GetAllPickups()
        {
            return db.Pickups
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }
        public Pickup? GetPickupById(int id)
        {
            return db.Pickups.Find(id);
        }

        public void AddPickup(Pickup pickup)
        {
            db.Pickups.Add(pickup);
            db.SaveChanges();
        }

        public bool UpdatePickupStatus(int pickupId, string status, string dealerId)
        {
            //db.Pickups.Update(pickup);
            //db.SaveChanges();
            //return true;
            var pickup = db.Pickups.Find(pickupId);

            if (pickup == null)
            {
                return false;
            }

            pickup.Status = status.ToUpper();
            //pickup.DealerId = "b8b09797-28bc-4378-a268-bd44bc6cb690"; // will replace it
            pickup.DealerId = dealerId;
            db.SaveChanges();
            return true;
        }

        public void DeletePickup(int id)
        {
            var pickup = db.Pickups.Find(id);
            if (pickup != null)
            {
                db.Pickups.Remove(pickup);
                db.SaveChanges();
            }
        }

        public bool CancelPickup(int pickupId)
        {
            var pickup = db.Pickups.Find(pickupId);
            if (pickup == null)
                return false;

            // Only allow cancelling of pending or accepted pickups
            //if (pickup.Status == "Completed")
            if (pickup.Status == "COMPLETED")
                return false;

            pickup.Status = "CANCELLED";
            pickup.DealerId = null;

            db.SaveChanges();
            return true;
        }

        public List<Pickup> GetUserPickups(string userId,string status)
        {
            return db.Pickups
                .Where(p => p.UserId == userId)
                .Where(p => p.Status == status)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }

        public List<Pickup> GetDealerPickups(string dealerId)
        {
            return db.Pickups
                .Where(p => p.DealerId == dealerId)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }

    }
}
