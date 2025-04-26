using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberRatingSystem.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Review text is required!")]
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Barber")]
        public int BarberId { get; set; }
        public Barber Barber { get; set; }
    }
}
