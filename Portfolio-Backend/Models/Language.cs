using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_Backend.Models
{
    public class Language
    {
        public int Id { get; set; } 

        public string Description { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
