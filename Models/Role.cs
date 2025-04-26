using System.ComponentModel.DataAnnotations;

namespace BarberRatingSystem.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
        public Role Create(int roleId)
        {
            Role role = new Role();
            if (roleId == 1)
            {
                role.Id = 1;
                role.Name = "User";
            }
            else
            {
                role.Id = 2;
                role.Name = "Admin";
            }
            return role;
        }
    }
}
