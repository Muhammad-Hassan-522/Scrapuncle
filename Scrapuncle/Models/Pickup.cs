using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scrapuncle.Models
{
    public class Pickup
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pickup date is required")]
        [Display(Name = "Pickup Date")]
        [DataType(DataType.Date)]
        public DateTime PickupDate { get; set; }

        [Required(ErrorMessage = "Pickup time is required")]
        [Display(Name = "Pickup Time")]
        public string PickupTime { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Full Address")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters")]
        public string FullAddress { get; set; }

        [Display(Name = "Landmark")]
        [StringLength(100)]
        public string Landmark { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [Display(Name = "Pincode")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must be 6 digits")]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Address type is required")]
        [Display(Name = "Address Type")]
        public string AddressType { get; set; }

        [Required(ErrorMessage = "Estimated weight is required")]
        [Display(Name = "Estimated Weight")]
        public string EstimatedWeight { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(500)]
        public string Remarks { get; set; }

        // Additional fields for system use
        public string Status { get; set; } = "SCHEDULED";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // For displaying formatted time slot
        [Display(Name = "Pickup Time Slot")]
        public string FormattedTimeSlot => PickupTime == "11-15" ? "11:00 AM - 3:00 PM" : "3:00 PM - 8:00 PM";

        // Full formatted address
        public string CompleteAddress => $"{FullAddress}, {Landmark}, {Pincode} ({AddressType})";


        public string UserId { get; set; } // fk to ApplicationUser
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }


        public string? DealerId { get; set; } // assigned dealer

        [ForeignKey("DealerId")]
        public virtual ApplicationUser Dealer { get; set; }


    }
}
