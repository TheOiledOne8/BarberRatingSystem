using System.ComponentModel.DataAnnotations;

namespace BarberRatingSystem.Models
{
    public class Barber
    {

        public int Id { get; set; }
        [StringLength(64)]
        public string? Name { get; set; }
        [StringLength(255)]

        public string? Description { get; set; }

        public string? PhotoPath { get; set; }

       ICollection <Review> Reviews { get; set; }
    }
}
