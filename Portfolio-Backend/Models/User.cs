using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Portfolio_Backend.Models
{
    [Index(nameof(User.Email), IsUnique = true)]
    [Index(nameof(User.UserName), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }


        public string Email { get; set; } = string.Empty;
    }
}
