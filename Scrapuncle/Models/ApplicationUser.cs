using Microsoft.AspNetCore.Identity;

namespace Scrapuncle.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
        public string Location { get; set; }

        public string Role { get; set; }


        //public virtual ICollection<Pickup> Pickups { get; set; }


        // dealer specific fields
        public string? BusinessName { get; set; }

        // storing pincodes as comma-separated string
        public string? ServiceablePincodes { get; set; }

        public string? ProfileImagePath { get; set; }
    }
}
