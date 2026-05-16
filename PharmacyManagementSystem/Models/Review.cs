using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Models
{
    public class Review : BaseEntity
    {
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        public string Comment { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}
