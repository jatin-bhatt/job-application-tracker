using System.ComponentModel.DataAnnotations;

namespace API.DTOs {
    public record JobApplicationUpdateDto {
        public int Id { get; set; }

        [Required(ErrorMessage = "Company Name is required.")]
        public required string CompanyName { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        public required string Position { get; set; }

        [Required(ErrorMessage = "Status is required.")]

        [RegularExpression("Submitted|UnderReview|Shortlisted|InterviewScheduled|InterviewCompleted|OfferExtended|Hired|Rejected|OnHold", ErrorMessage = "Invalid Status")]
        public required string Status { get; set; }

        [Required(ErrorMessage = "Application Date is required.")]
        public required DateTime ApplicationDate { get; set; }
    }
    
}
