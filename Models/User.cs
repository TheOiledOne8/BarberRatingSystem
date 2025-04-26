using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberRatingSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required!")]
        [StringLength(50)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "First name is required!")]
        [Display(Name  = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters!")]
        public string Password { get; set; }
        public int RoleId { get; set; }



    }
}
