namespace Scrapuncle.Models
{
    public class DealerProfile
    {
        public string Id { get; set; } // to track the logged-in user
        public string FullName { get; set; }
        public string BusinessName { get; set; }
        public string Email { get; set; } // display only
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string ServiceablePincodes { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? ExistingProfileImagePath { get; set; }
    }
}
