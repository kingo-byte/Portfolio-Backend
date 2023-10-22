using System.ComponentModel.DataAnnotations;

namespace Portfolio_Backend.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; } 

        [Required]
        public string Body { get; set; }
    }
}
