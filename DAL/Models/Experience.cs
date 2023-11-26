using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Portfolio_Backend.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }  

        [Required]
        public string Title{ get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime? Start { get; set; } 

        public DateTime? End { get; set; }

        [JsonIgnore]
        public virtual User? User{ get; set; }
    }
}
